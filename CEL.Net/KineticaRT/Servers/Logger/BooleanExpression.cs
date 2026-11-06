/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 7/3/2006
 * Time: 2:26 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using KT = KineticaRT;
using KTC = KineticaRT.Channels;
using KTLE = KineticaRT.Logic.Expressions;
using KTLED = KineticaRT.Logic.Expressions.Design;
using KTSL = KineticaRT.Servers.Logging;
using KineticaRT.Studio.Application;
using KTV = KineticaRT.Values;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace CEL.KineticaRT.Servers.Logger
{
	/// <summary>
	/// Description of BooleanExpression.
	/// </summary>
	[Serializable]
    public class BooleanExpression : KTC.BooleanChannel, KTC.IBooleanExpressionChannel,KT.IEnabled, KTSL.ILoggerChannel
	{
		#region Fields
		
		private bool							enabled = true;
		private KTLE.Expression					expression;
		private int								index;
		private KTSL.Logger						logger;
		private KTSL.Logger						eventLogger;
		private KTC.ValueChangedEventHandler	srcHdlr;
		private KTC.ValueChangedEventHandler	serverHdlr;
		private string							loggerName = "";
		private string							eventLoggerName = "";
		
		#endregion

		#region Constructor
		
		public BooleanExpression() : base()
		{
			this.loggerName					= "";
			this.eventLoggerName			= "";
			this.index						= 1;
			this.expression					= new KTLE.Expression();
			this.expression.ObjectSearch   += new KT.ObjectSearchEventHandler( this.OnObjectSearch );
			this.srcHdlr					= new KTC.ValueChangedEventHandler( this.Expression_OnValueChanged );
			this.serverHdlr					= new KTC.ValueChangedEventHandler( this.Server_OnValueChanged );
		}
		
		public BooleanExpression( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			if ( info == null )
			{
				throw new ArgumentNullException( "info" );
			}
			this.expression = new KTLE.Expression();
			try
			{
				this.expression.Text	= info.GetString( "Text" );
				this.loggerName			= info.GetString( "Logger" );
				this.eventLoggerName	= info.GetString( "EventLogger" );
				this.index				= info.GetInt32( "Index" );
				this.enabled			= info.GetBoolean( "Enabled" );
			}
			catch (SerializationException exception1)
			{
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
			this.expression.ObjectSearch   += new KT.ObjectSearchEventHandler( this.OnObjectSearch );
			this.srcHdlr					= new KTC.ValueChangedEventHandler( this.Expression_OnValueChanged );
			this.serverHdlr					= new KTC.ValueChangedEventHandler( this.Server_OnValueChanged );
		}
		
		#endregion
		
		#region Events
		
		public event KT.EnabledEventHandler EnabledChanged;
		
		#endregion

		#region Properties
		
		//HighTriggered Not used for BooleanExpression channel
		public bool HighTriggered
		{
		      get
		      {
		            return false;
		      }
		}
		//LowTriggered Not used for BooleanExpression channel
		public bool LowTriggered
		{
		      get
		      {
		            return false;
		      }
		}
		
		[Category("Logger"),DefaultValue("true"),Description("Indicates if the channel is to be logged during recording.")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if ( this.enabled != value )
				{
					this.enabled = value;
					this.OnEnabledChanged();
				}
			}
		}

		[Category("Logger"),DefaultValue("1"),Description("Indicates the ordering of the channels in the data file.")]
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				if ( this.index != value )
				{
					this.index = value;
					this.OnPropertyChanged( "Index" );
				}
			}
		}
		
		[Category("Logger"),DefaultValue(""),Description("The name of the Logger.CsvFile component which records/replays data from/to this channel.")]
		public string Logger
		{
			get
			{
				return this.loggerName;
			}
			set
			{
				if ( this.loggerName != value )
				{
					this.loggerName = value;
					this.OnPropertyChanged( "Logger" );
				}
			}
		}
		[Category("Logger"),DefaultValue(""),Description("The name of the Logger.CsvFile component which records/replays data from/to this channel.")]
		public string EventLogger
		{
			get
			{
				return this.eventLoggerName;
			}
			set
			{
				if ( this.eventLoggerName != value )
				{
					this.eventLoggerName = value;
					this.OnPropertyChanged( "EventLogger" );
				}
			}
		}
		
		[Browsable(false)]
		public string Source
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new System.NotSupportedException( "Source" );
			}
		}

		[Editor(typeof(KTLED.ExpressionEditor),typeof(UITypeEditor)),DefaultValue(""),Description("Expression Text"),Category("NumericExprression")]
		public string Text
		{
			get
			{
				return this.expression.Text;
			}
			set
			{
				if ( this.expression.Text != value )
				{
					if ( this.Connected )
					{	
						this.expression.ValueChanged -= this.srcHdlr;
						this.expression.Disconnect();
					}
					this.expression.Text = value;
					if ( this.Connected )
					{	
						this.expression.Connect();
						this.Value = this.expression.Value;
						this.expression.ValueChanged += this.srcHdlr;
					}
					this.OnPropertyChanged( "Text" );				
				}
			}
		}

		[Category("Channel"),Description("The value of this channel.")]
		public override KTV.Value Value
		{
			get
			{
				return base.Value;
			}
		}
		
		#endregion
		
		#region Methods

		public override void Connect()
		{
			
			
			lock( this )
			{
				if ( this.Connected )
				{
					return;
				}
				this.logger = base.OnObjectSearch( this.loggerName ) as KTSL.Logger;
				this.eventLogger = base.OnObjectSearch( this.eventLoggerName ) as KTSL.Logger;
				if ( this.logger != null )
				{
					this.logger.AddChannel( this );
					this.logger.ValueChanged += this.serverHdlr;
				}
				if ( this.eventLogger != null )
				{
					this.eventLogger.AddChannel( this );
					this.eventLogger.ValueChanged += this.serverHdlr;
				}
                this.expression.ValueChanged += this.srcHdlr;//ft le handler doit etre attaché avant de setter la valeur pcq base.Value se se set sur le feedback
                this.expression.Connect();
				this.Value = this.expression.Value;
                base.Value = this.expression.Value;//ft

                base.Connect();
            }
            
		}

		public override void Disconnect()
		{
			lock( this )
			{
				if ( this.Connected )
				{
					this.expression.ValueChanged -= this.srcHdlr;
					this.expression.Disconnect();

					if ( this.logger != null )
					{
						this.logger.ValueChanged -= this.serverHdlr;
						this.logger.RemoveChannel( this );
						this.logger = null;
					}
					if ( this.eventLogger != null )
					{
						this.eventLogger.ValueChanged -= this.serverHdlr;
						this.eventLogger.RemoveChannel( this );
						this.eventLogger = null;
					}
                    base.Disconnect();
				}
			}
			
		}

		private void Expression_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			if ( ( this.logger != null ) && ( this.logger.CurrentMode != KTSL.Logger.Mode.Play ) && ( this.eventLogger != null ) && ( this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play ) )
			{
				this.Value = newVal;
			}
		}
		
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if ((info == null)) {
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue( "Text", this.expression.Text );
			info.AddValue( "Logger", this.loggerName );
			info.AddValue( "EventLogger", this.eventLoggerName );
			info.AddValue( "Index", this.index );
			info.AddValue( "Enabled", this.enabled );
		}

		private void Server_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			if ( ( newVal.ToString() == "Stopped" ) && 
			     ( this.logger != null ) && ( this.logger.CurrentMode != KTSL.Logger.Mode.Play ) &&
			     ( this.eventLogger != null ) && ( this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play ) )
			{
				//ft this.Value = this.expression.Value;
                base.Value = this.expression.Value;
			}
		}
		
		protected void OnEnabledChanged()
		{
			if ( this.EnabledChanged != null )
			{
				this.EnabledChanged( this, this.enabled ? KT.EnabledEventArgs.EnabledEvent : KT.EnabledEventArgs.DisabledEvent );
			}
		}

		#endregion
	}
}

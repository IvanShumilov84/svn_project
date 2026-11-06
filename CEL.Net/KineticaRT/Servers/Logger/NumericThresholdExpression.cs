/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 7/4/2006
 * Time: 8:53 AM
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
using System.Globalization;


namespace CEL.KineticaRT.Servers.Logger
{
	/// <summary>
	/// Description of NumericThresholdExpression.
	/// </summary>
	[Serializable]
    public class NumericThresholdExpression : CEL.KineticaRT.Channels.NumericThresholdChannel, KTC.INumericExpressionChannel, KT.IEnabled, KTSL.ILoggerChannel
	{
		#region Fields
		
		private bool							enabled = true;
		private KTLE.Expression					expression;
		private int								index;
		private KTSL.Logger						logger;
		private KTSL.Logger						eventLogger;
		private string							loggerName = "";
		private string							eventLoggerName = "";
		private KTC.ValueChangedEventHandler	srcHdlr;
		private KTC.ValueChangedEventHandler	serverHdlr;
		
		private double 							lowTrigger;
		private double							highTrigger;
		
		#endregion

		#region Constructor
		
		public NumericThresholdExpression() : base()
		{
			this.loggerName					= "";
			this.eventLoggerName					= "";
			this.index						= 1;
			this.expression					= new KTLE.Expression();
			this.expression.ObjectSearch   += new KT.ObjectSearchEventHandler( this.OnObjectSearch );
			this.lowTrigger 				= double.NegativeInfinity;
      		this.highTrigger 				= double.PositiveInfinity;
      		this.srcHdlr					= new KTC.ValueChangedEventHandler( this.Expression_OnValueChanged );
			this.serverHdlr					= new KTC.ValueChangedEventHandler( this.Server_OnValueChanged );
		}
		
		public NumericThresholdExpression( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			this.lowTrigger = double.NegativeInfinity;
      		this.highTrigger = double.PositiveInfinity;

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
  				double.TryParse(info.GetString("LowTrigger"), NumberStyles.Any, NumberFormatInfo.CurrentInfo, out this.lowTrigger);
            	double.TryParse(info.GetString("HighTrigger"), NumberStyles.Any, NumberFormatInfo.CurrentInfo, out this.highTrigger);
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

		[DefaultValue(double.PositiveInfinity), Category("Logger"), Description("The High trigger value of the channel.  This value is used for triggering the assotiated logger.")]
		public double HighTrigger
		{
		      get
		      {
		            return this.highTrigger;
		      }
		      set
		      {
		            if (this.highTrigger != value)
		            {
		                  this.highTrigger = value;
		                  this.OnPropertyChanged("HighTrigger");
		            }
		      }
		}
		public bool HighTriggered
		{
		      get
		      {
		            return (this.Value.ToDouble() > this.highTrigger);
		      }
		}
		
		[Category("Logger"), DefaultValue(double.NegativeInfinity), Description("The Low trigger value of the channel.  This value is used for triggering the assotiated logger.")]
		public double LowTrigger
		{
		      get
		      {
		            return this.lowTrigger;
		      }
		      set
		      {
		            if (this.lowTrigger != value)
		            {
		                  this.lowTrigger = value;
		                  this.OnPropertyChanged("LowTrigger");
		            }
		      }
		}
		public bool LowTriggered
		{
		      get
		      {
		            return (this.Value.ToDouble() < this.lowTrigger);
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
              
                //ft Ici j'ai change l'ordre... pour eviter de perdre des Value qui ont change comme dans le NumericThresholdItem.Connect
                // il faut attacher le handler avant de faire le connect et en en pratique on a pas besoin de faire la ligne this.Value = this.expression.Value
                //parce que l'expression generea elle-meme la value et on tombera automatiquement dans la fonction Expression_OnValueCHanged
                this.expression.ValueChanged += this.srcHdlr;
                this.expression.Connect();
                base.Connect();
			}
		}

		public override void Disconnect()
		{
			lock( this )
			{
				if ( this.Connected )
				{
                    //ft Test changer l'ordre du disconnect et le retrait de l'handler pour s'assurer que L'expression retourne le Value NotConnected au moment du disconnect
                    this.expression.Disconnect();
                    this.expression.ValueChanged -= this.srcHdlr;

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
	     	info.AddValue( "LowTrigger", this.lowTrigger.ToString());
		    info.AddValue( "HighTrigger", this.highTrigger.ToString());
		}

		private void Server_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			if (   ( newVal.ToString() == "Stopped" ) 
			    && 
			      (( this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play )
			    || ( this.logger == null && this.eventLogger == null ) 
			    || ( this.logger == null && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play) 
			    || ( this.eventLogger == null && this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play)))
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

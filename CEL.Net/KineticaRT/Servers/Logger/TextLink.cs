/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 7/3/2006
 * Time: 2:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using KT = KineticaRT;
using KTC = KineticaRT.Channels;
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
	/// Description of TextLink.
	/// </summary>
	[Serializable]
	public class TextLink : KTC.TextChannel, KTC.ITextLinkChannel,KT.IEnabled, KTSL.ILoggerChannel
	{
		#region Fields
		
		private bool							enabled = true;
		private int								index;
		private string							destination = "";
		private KTC.IChannel					destinationChannel;
		private KTSL.Logger						logger;
		private KTSL.Logger						eventLogger;
		private string							loggerName = "";
		private string							eventLoggerName = "";
		private string							source = "";
		private KTC.IChannel					sourceChannel;
		private KTC.ValueChangedEventHandler	srcHdlr;
		private KTC.ValueChangedEventHandler	serverHdlr;
		
//		private bool 			getLowTriggered = false;
//		private bool 			getHighTriggered = false;
		
		#endregion
		
		#region Constructor
		
		public TextLink() : base()
		{
			this.loggerName = "";
			this.eventLoggerName = "";
			this.index = 1;
			this.srcHdlr = new KTC.ValueChangedEventHandler( this.Source_OnValueChanged );
			this.serverHdlr	= new KTC.ValueChangedEventHandler( this.Server_OnValueChanged );
		}
		
		public TextLink( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			if ( info == null )
			{
				throw new ArgumentNullException( "info" );
			}
			try
			{
				this.destination	= info.GetString( "Destination" );
				this.source			= info.GetString( "Source" );
				this.loggerName		= info.GetString( "Logger" );
				this.eventLoggerName= info.GetString( "EventLogger" );
				this.index			= info.GetInt32( "Index" );
				this.enabled		= info.GetBoolean( "Enabled" );
			}
			catch (SerializationException exception1)
			{
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
			this.srcHdlr = new KTC.ValueChangedEventHandler( this.Source_OnValueChanged );
			this.serverHdlr	= new KTC.ValueChangedEventHandler( this.Server_OnValueChanged );
		}
		
		#endregion Constructor
		
		#region Events
		
		public event KT.EnabledEventHandler EnabledChanged;
		
		#endregion
		
		#region Properties
		
		//HighTriggered Not used for TextLink channel
		public bool HighTriggered
		{
		      get
		      {
		            return false;
		      }
		}
		//LowTriggered Not used for TextLink channel
		public bool LowTriggered
		{
		      get
		      {
		            return false;
		      }
		}

		[Category("Link"),Editor(typeof(ChannelSelectorEditor),typeof(UITypeEditor)),Description("Name of destination channel.")]
		public string Destination
		{
			get
			{
				return this.destination;
			}
			set
			{
				if ( this.destination != value )
				{
					this.destination = value;
					this.OnPropertyChanged( "Destination" );
				}
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

		[Category("Link"),Editor(typeof(ChannelSelectorEditor),typeof(UITypeEditor)),Description("Name of source channel.")]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if ( this.source != value )
				{
					this.source = value;
					this.OnPropertyChanged( "Source" );
				}
			}
		}

		[Category("Channel"),Description("The value of this channel.")]
		public override KTV.Value Value
		{
			/*get
			{
				return base.Value;
			}*/
			get
			{
				if ( this.sourceChannel == null && this.destinationChannel != null )
				{
					return this.destinationChannel.Value;
				}
				else		
					return base.Value;
			}
			set
			{
				if ( ( this.logger != null ) && ( this.logger.CurrentMode != KTSL.Logger.Mode.Play ) && ( this.eventLogger != null ) && ( this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play ) )
				{
					if ( this.destinationChannel != null )
					{
						this.destinationChannel.Value = value;
					}
				}
				base.Value = value;
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
				if ( ( this.destination != null ) && ( this.destination.Length > 0 ) )
				{
					this.destinationChannel = base.OnObjectSearch( this.destination ) as KTC.IChannel;
                    if (this.destinationChannel == null) //ft
                    {
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Destination: " + this.destination);//ft
                    }
				}
				if ( ( this.source != null ) && ( this.source.Length > 0 ) )
				{
					this.sourceChannel = base.OnObjectSearch( this.source ) as KTC.IChannel;
					if ( this.sourceChannel != null )
					{						
                        this.sourceChannel.ValueChanged += this.srcHdlr;//ft le handler doit etre attaché avant de setter la valeur pcq base.Value se se set sur le feedback
                        this.Value = this.sourceChannel.Value;
                        base.Value = this.sourceChannel.Value;//ft
					}
					else
					{
						this.Value = KTV.Values.NotConnected;
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Source: " + this.source);//ft
					}
				}
                base.Connect();
			}
		}

		public override void Disconnect()
		{
			lock( this )
			{
				if ( this.Connected )
				{
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
					if ( this.sourceChannel != null )
					{
						this.sourceChannel.ValueChanged -= this.srcHdlr;
						this.Value = KTV.Values.NotConnected;
						this.sourceChannel = null;
					}
					this.destinationChannel = null;
                    base.Disconnect();
				}
			}
		}
		
		protected void OnEnabledChanged()
		{
			if ( this.EnabledChanged != null )
			{
				this.EnabledChanged( this, this.enabled ? KT.EnabledEventArgs.EnabledEvent : KT.EnabledEventArgs.DisabledEvent );
			}
		}
		
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if ((info == null)) {
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue( "Source", this.source );
			info.AddValue( "Destination", this.destination );
			info.AddValue( "Logger", this.loggerName );
			info.AddValue( "EventLogger", this.eventLoggerName );
			info.AddValue( "Index", this.index );
			info.AddValue( "Enabled", this.enabled );
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
				if ( this.sourceChannel != null )
				{
					if (source != destination)// Pour eviter des loop de call réentrant
                    {
                        this.Value = this.sourceChannel.Value;
                    }
                    else
                    {
                        base.Value = this.sourceChannel.Value;
                    }
				}
			}
		}
		
		private void Source_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			if (   ( this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play &&  this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play ) 
			    || ( this.logger == null && this.eventLogger == null ) 
			    || ( this.logger == null && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play) 
			    || ( this.eventLogger == null && this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play))
			{
                if (source != destination)//ft Pour eviter des loop de call réentrant
                {
                    this.Value = newVal;
                }
                else
                {
                    base.Value = newVal;
                }
			}
		}

		#endregion
	}
}

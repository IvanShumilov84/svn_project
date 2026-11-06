/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 7/3/2006
 * Time: 1:48 PM
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
using System.Globalization;

namespace CEL.KineticaRT.Servers.Logger
{
	/// <summary>
	/// Description of NumericThresholdLink.
	/// </summary>
	[Serializable]
	public class NumericThresholdLink : CEL.KineticaRT.Channels.NumericThresholdChannel, KTC.INumericLinkChannel,KT.IEnabled, KTSL.ILoggerChannel, KT.IDestination
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
		
		private double 							lowTrigger;
		private double							highTrigger;
	
		#endregion
		
		#region Constructor
		
		public NumericThresholdLink() : base()
		{
			this.loggerName = "";
			this.eventLoggerName = "";
			this.index = 1;
   			this.lowTrigger 	= double.NegativeInfinity;
   			this.highTrigger 	= double.PositiveInfinity;
   			this.srcHdlr		= new KTC.ValueChangedEventHandler( this.source_OnValueChanged );
			this.serverHdlr		= new KTC.ValueChangedEventHandler( this.Server_OnValueChanged );
		}
		
		public NumericThresholdLink( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			this.lowTrigger = double.NegativeInfinity;
      		this.highTrigger = double.PositiveInfinity;

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
  				double.TryParse(info.GetString("LowTrigger"), NumberStyles.Any, NumberFormatInfo.CurrentInfo, out this.lowTrigger);
            	double.TryParse(info.GetString("HighTrigger"), NumberStyles.Any, NumberFormatInfo.CurrentInfo, out this.highTrigger);
			}
			catch (SerializationException exception1)
			{
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
  			this.srcHdlr = new KTC.ValueChangedEventHandler( this.source_OnValueChanged );
			this.serverHdlr	= new KTC.ValueChangedEventHandler( this.Server_OnValueChanged );
		}
		
		#endregion Constructor
		
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
		      		try 
		      		{
		            	return (this.Value.ToDouble() > this.highTrigger);
		      		}
		      		catch (System.NullReferenceException)
		      		{
		      			return false;
		      		}
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
		      		try
		      		{
		            	return (this.Value.ToDouble() < this.lowTrigger);
		      		}
		      		catch (System.NullReferenceException)
		      		{
		      			return false;
		      		}
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
				//ft if ( ( this.logger != null ) && ( this.logger.CurrentMode != KTSL.Logger.Mode.Play ) &&  ( this.eventLogger != null ) && ( this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play ))
                if ((this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play)
                || (this.logger == null && this.eventLogger == null)
                || (this.logger == null && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play)
                || (this.eventLogger == null && this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play))
				{
					if ( this.destinationChannel != null )
					{
						this.destinationChannel.Value = value;
					}
				}
                if (sourceChannel != destinationChannel || sourceChannel == null) //base.Value Sera updaté par le feedback voir source_OnValueChanged le la source = la destination
                {
					base.Value = value;
				}
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
                        this.sourceChannel.ValueChanged += this.srcHdlr; //ft le handler doit etre attaché avant de setter la valeur pcq base.Value se se set sur le feedback
						this.Value = this.sourceChannel.Value;
                        base.Value = this.sourceChannel.Value;
					}
					else
					{
						this.Value = KTV.Values.NotConnected;
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Source: " + this.Source);//ft
					}
				}
				else
				{
                    //Ft 20120525 
                    //J'ai retire cette ligne pcq elle override le value,  lorsque je crée un channel dans OpenTest_Sequence
                    //J'affecte des valeurs InitialValue au channel juste avant d'appeler le connect voir (Create_Engine_numeric_channel)
                    //this.Value = KTV.Values.NotConnected;
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
	     	info.AddValue( "LowTrigger", this.lowTrigger.ToString());
		    info.AddValue( "HighTrigger", this.highTrigger.ToString());
		}

		private void Server_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			if ( ( newVal.ToString() == "Stopped" ) 
			    && 
			      (( this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play )
			    || ( this.logger == null && this.eventLogger == null ) 
			    || ( this.logger == null && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play) 
			    || ( this.eventLogger == null && this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play)))
			{
				if ( this.sourceChannel != null )
				{
                    if (source != destination)//ft Pour eviter des loop de call réentrant
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
		
		private void source_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			if (   ( this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play ) 
			    || ( this.logger == null && this.eventLogger == null ) 
			    || ( this.logger == null && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play) 
			    || ( this.eventLogger == null && this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play))
			{
                if (sourceChannel != destinationChannel)//ft Pour eviter des loop de call réentrant
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

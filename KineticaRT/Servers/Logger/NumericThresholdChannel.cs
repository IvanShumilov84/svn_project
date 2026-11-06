using System;
using System.Runtime.Serialization ;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Servers.Logging
{
	/// <summary>
	/// Logging channel for recording / playback of numeric data.
	/// </summary>
    [ChangeRecord("1.2.0.4", "26 10 2006", "N. Savidge", "Added Pre and Post logging with Trigger")]
    [ChangeRecord("1.2.0.0", "10 03 2005", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2005-2006 KineticaRT Ltd.")]

    [Description("Logger Numeric Channel")]
	[Serializable]
	[DefaultProperty("Source")]
	public class NumericThresholdChannel:KineticaRT.Channels.NumericThresholdChannel, KineticaRT.Channels.INumericChannel, ISerializable, KineticaRT.Channels.INumericThresholdChannel, IEnabled, ILoggerChannel, IConnectable
	{
		private string		loggerName ;
		private string		sourceName ;
		private int			index  ;
		private Logger		logger ;
		private bool		connected ;
		private KineticaRT.Channels.IChannel source ;

		/// <summary>
		/// Constructor.
		/// </summary>
		public NumericThresholdChannel()
		{
			loggerName = "" ;
			sourceName = "" ;
			index = 1 ;
		}

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.GetObjectData (info, context) ;
			info.AddValue ("Logger", loggerName) ;
			info.AddValue ("Source", sourceName) ;
			info.AddValue ("Index", index) ;
            info.AddValue("Enabled", enabled);
            info.AddValue("LowTrigger", lowTrigger.ToString());
            info.AddValue("HighTrigger", highTrigger.ToString());
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected NumericThresholdChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try
			{
				loggerName = info.GetString ("Logger") ;
				sourceName = info.GetString ("Source") ;
				index = info.GetInt32 ("Index") ;
                enabled = info.GetBoolean("Enabled");
                //lowTrigger = info.GetDouble("LowTrigger");
                //highTrigger = info.GetDouble("HighTrigger"); infinity problem
			    System.Double.TryParse(info.GetString("LowTrigger"),System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo,out lowTrigger);
			    System.Double.TryParse(info.GetString("HighTrigger"),System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo,out highTrigger);


            }
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
		}
		#endregion

		#region IEnabled
		private bool		enabled = true ;
		/// <summary>Occurs when the value of the Value property changes.</summary>
		public event EnabledEventHandler EnabledChanged;   
		/// <summary>Called when the value of the Value property changes.</summary>
		private void OnEnabledChanged(){if (EnabledChanged!= null) EnabledChanged(this, enabled ? KineticaRT.EnabledEventArgs.EnabledEvent : KineticaRT.EnabledEventArgs.DisabledEvent);}
		/// <summary>Gets or sets the Enabled property.</summary>
		/// <value>true to enable or false to disable.</value>
		[System.ComponentModel.Description("Indicates if the channel is to be logged during recording."), System.ComponentModel.Category("Logger")] 
		[DefaultValue("true")]
		public bool Enabled
		{
			get
			{
				return enabled ;
			}

			set
			{
				if (enabled != value)
				{
					enabled = value ;
					OnEnabledChanged() ;
				}
			}
		}
		#endregion

        #region ILoggerChannel
        /// <summary>
        /// Gets the low triggered value of the channel.
        /// </summary>
        /// <value>Boolean value indicating if the current value is below the LowTrigger value.</value>
        /// <remarks>
        /// This value is used for triggering the logging when the associated logger is in Record mode.
        /// <para>Return false if not required.</para>
        /// </remarks>
        bool ILoggerChannel.LowTriggered
        {
            get
            {
                return Value.ToDouble() < lowTrigger;
            }
        }

        /// <summary>
        /// Gets the High triggered value of the channel.
        /// </summary>
        /// <value>Boolean value indicating if the current value is above the HighTrigger value.</value>
        /// <remarks>
        /// This value is used for triggering the logging when the associated logger is in Record mode.
        /// <para>Return false if not required.</para>
        /// </remarks>
        bool ILoggerChannel.HighTriggered
        {
            get
            {
                return Value.ToDouble() > highTrigger;
            }
        }
        #endregion

		#region Properties
        /// <summary>
        /// Gets or sets the Low trigger value of the channel.
        /// </summary>
        /// <value>Double value for the Low trigger value of the channel.</value>
        /// <remarks>
        /// This value is used for triggering the logging when the associated logger is in Record mode.
        /// <para>Set to System.Double.NegativeInfinity if not required.</para>
        /// </remarks>
        [System.ComponentModel.Description("The Low trigger value of the channel.  This value is used for triggering the assotiated logger."), System.ComponentModel.Category("Logger")]
        [DefaultValue(System.Double.NegativeInfinity)]
        public double LowTrigger
        {
            get
            {
                return lowTrigger;
            }
            set
            {
                if (lowTrigger != value)
                {
                    lowTrigger= value;
                    OnPropertyChanged("LowTrigger");
                }
            }
        }
        private double lowTrigger = System.Double.NegativeInfinity;

        /// <summary>
        /// Gets or sets the High trigger value of the channel.
        /// </summary>
        /// <value>Double value for the High trigger value of the channel.</value>
        /// <remarks>
        /// This value is used for triggering the logging when the associated logger is in Record mode.
        /// <para>Set to System.Double.PositiveInfinity if not required.</para>
        /// </remarks>
        [System.ComponentModel.Description("The High trigger value of the channel.  This value is used for triggering the assotiated logger."), System.ComponentModel.Category("Logger")]
        [DefaultValue(System.Double.PositiveInfinity)]
        public double HighTrigger
        {
            get
            {
                return highTrigger;
            }
            set
            {
                if (highTrigger != value)
                {
                    highTrigger = value;
                    OnPropertyChanged("HighTrigger");
                }
            }
        }
        private double highTrigger = System.Double.PositiveInfinity;

        /// <summary>
		/// Indicates the ordering of the channels in the data file.
		/// </summary>
		/// <value>
		/// -x - place in header in reverse numerical order,
		/// 0  - don't use,
		/// +x - place in each record in numerical order.
		/// </value>
		[System.ComponentModel.Description("Indicates the ordering of the channels in the data file."), System.ComponentModel.Category("Logger")] 
		[DefaultValue("1")]
		public int Index
		{
			get 
			{
				return index ;
			}
			set 
			{
				if (index != value)
				{
					index = value ;
					OnPropertyChanged("Index") ;
				}
			}
		}

		/// <summary>
		/// The name of the Logger.CsvFile component which records/replays data from/to this channel.
		/// </summary>
		/// <value>
		/// A string representing the name of the logger component to use.
		/// </value>
		[System.ComponentModel.Description("The name of the Logger.CsvFile component which records/replays data from/to this channel."), System.ComponentModel.Category("Logger")] 
		[DefaultValue("")]
		public string Logger
		{
			get 
			{
				return loggerName ;
			}
			set 
			{
				if (loggerName != value)
				{
					loggerName = value ;
					OnPropertyChanged("Logger") ;
				}
			}
		}

		/// <summary>
		/// The name of the channel to be logged/replayed.
		/// </summary>
		/// <value>
		/// A string representing the name of the channel to be logged/replayed.
		/// </value>
		[System.ComponentModel.Description("The name of the channel to be logged/replayed."), System.ComponentModel.Category("Logger")] 
		[DefaultValue("")]
		public string Source
		{
			get 
			{
				return sourceName ;
			}
			set 
			{
				if (sourceName != value)
				{
					sourceName = value ;
					OnPropertyChanged("Source") ;
				}
			}
		}
		#endregion

		#region Connect
		/// <summary>
		/// Connect to other Objects.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				if (connected) return ;

				logger = OnObjectSearch(loggerName) as Logger ;
				if (logger != null)	logger.AddChannel (this) ;

				source = OnObjectSearch(sourceName) as KineticaRT.Channels.IChannel ;
                if (source != null)
                {
                    source.ValueChanged += new KineticaRT.Channels.ValueChangedEventHandler(source_ValueChanged);
                    source.PropertyChanged += new PropertyChangedEventHandler(source_PropertyChanged);
                    Value = source.Value;

                    KineticaRT.Channels.IInfoChannel info = source as KineticaRT.Channels.IInfoChannel;
                    if (info != null)
                    {
                        this.Description = info.Description;
                        this.ToolTip = info.ToolTip;
                    }

                    KineticaRT.Channels.NumericThresholdChannel numeric = source as KineticaRT.Channels.NumericThresholdChannel;
                    if (numeric != null)
                    {
                        this.Units = numeric.Units;
                        this.DecimalDigits = numeric.DecimalDigits;
                        this.Format = numeric.Format;
                        this.Minimum = numeric.Minimum;
                        this.Maximum = numeric.Maximum;

                        this.High = numeric.High;
                        this.HighColor = numeric.HighColor;
                        this.HighHigh = numeric.HighHigh;
                        this.HighHighColor = numeric.HighHighColor;
                        this.Low = numeric.Low;
                        this.LowColor = numeric.LowColor;
                        this.LowLow = numeric.LowLow;
                        this.LowLowColor = numeric.LowLowColor;
                        this.Normal = numeric.Normal;
                        this.NormalColor = numeric.NormalColor;
                    }
                }
                else
                {
                    System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Source: " + this.sourceName);//ft
                }

				connected = true ;
			}
		}

		/// <summary>
		/// Disconnect from other Objects.
		/// </summary>
		public override void Disconnect ()
		{
			lock (this)
			{
				if (connected)
				{
					if (logger != null)
					{
						logger.RemoveChannel (this) ;
						logger = null ;
					}

					if (source != null) 
					{
						source.ValueChanged -=new KineticaRT.Channels.ValueChangedEventHandler(source_ValueChanged);
						Value = KineticaRT.Values.Values.NotConnected ;
						source = null ;
					}
					
					connected = false ;
				}
			}
			base.Disconnect () ;
		}
		
		#endregion

		private void source_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			if ((this.logger != null) && (this.logger.CurrentMode != KineticaRT.Servers.Logging.Logger.Mode.Play))
			{
				this.Value = newVal ;
			}
		}

		private void source_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			KineticaRT.Channels.IInfoChannel info = source as KineticaRT.Channels.IInfoChannel ;
			if (info != null)
			{
				switch (e.PropertyName)
				{
					case "Description" :
						this.Description = info.Description ;
						return ;
					case "ToolTip" :
						this.ToolTip = info.ToolTip ;
						return ;
				}
			}

			KineticaRT.Channels.NumericThresholdChannel numeric = source as KineticaRT.Channels.NumericThresholdChannel ;
			if (numeric != null)
			{
				switch (e.PropertyName)
				{
					case "Units" :
						this.Units = numeric.Units ;
						return ;
					case "DecimalDigits" :
						this.DecimalDigits = numeric.DecimalDigits ;
						return ;
					case "Format" :
						this.Format = numeric.Format ;
						return ;
					case "Minimum" :
						this.Minimum = numeric.Minimum ;
						return ;
					case "Maximum" :
						this.Maximum = numeric.Maximum ;
						return ;

					case "High" :
						this.High= numeric.High;
						return ;
					case "HighColor" :
						this.HighColor= numeric.HighColor;
						return ;
					case "HighHigh" :
						this.HighHigh= numeric.HighHigh;
						return ;
					case "HighHighColor" :
						this.HighHighColor= numeric.HighHighColor;
						return ;
					case "Low" :
						this.Low= numeric.Low;
						return ;
					case "LowColor" :
						this.LowColor= numeric.LowColor;
						return ;
					case "LowLow" :
						this.LowLow= numeric.LowLow;
						return ;
					case "LowLowColor" :
						this.LowLowColor= numeric.LowLowColor;
						return ;
					case "Normal" :
						this.Normal= numeric.Normal;
						return ;
					case "NormalColor" :
						this.NormalColor= numeric.NormalColor;
						return ;
				}
			}
		}
	}
}

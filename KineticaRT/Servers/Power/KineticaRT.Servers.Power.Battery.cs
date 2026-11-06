using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace KineticaRT.Servers.Power
{
    /// <summary>
    /// Component to obtain battery life information.
    /// </summary>
    [ChangeRecord("1.0", "12 03 2007", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT SubProject")]
    [Copyright("Copyright © KineticaRT Ltd 2007")]

    [Serializable]
    public class Battery : KineticaRT.Channels.InfoChannel, ISerializable
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Power", "KineticaRT.Servers.Power Messages");

        #region Construction
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
        /// <remarks>To turn off Autoplay - GPEDIT.MSC / Computer Configuration / Administrative Templates / System / Turn off Autoplay</remarks>
        public Battery()
		{
//            Initialise();
        }

//        private void Initialise()
//        {
//        }
        #endregion

        #region ISerializable
        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination for this serialization.</param>
        [System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
            info.AddValue("ShutdownDestination", shutdownDestination);
            info.AddValue("ShutdownAfter", shutdownAfter);
            info.AddValue("ShutdownWhen", shutdownWhen);
        }

        /// <summary>Constructor for the ISerializable interface.</summary>
        protected Battery(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

//            Initialise();

            try
            {
                shutdownDestination = info.GetString("ShutdownDestination");
                shutdownAfter = (TimeSpan) info.GetValue ("ShutdownAfter", typeof(TimeSpan));
                shutdownWhen = (TimeSpan)info.GetValue("ShutdownWhen", typeof(TimeSpan));
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Configuration is missing a property for " + GetType(), this.Name);
            }
        }
        #endregion

        #region Properties
        bool online ;
        /// <summary>
        /// Gets the mains online status, true if there is mains power available, false if using battery power.
        /// </summary>
        [System.ComponentModel.Description("Mains online status, true if there is mains power available, false if using battery power."), System.ComponentModel.Category("Power")]
        public bool Online
        {
            get { return online; }
        }

        DateTime shutdownTime = DateTime.MaxValue;
        TimeSpan batteryTimeRemaining = TimeSpan.FromSeconds(-1);
        /// <summary>
        /// Gets the estimated amount of battery time remaining.  
        /// Will be negative if not using battery power.
        /// </summary>
        [System.ComponentModel.Description("Estimated amount of battery time remaining."), System.ComponentModel.Category("Power")]
        public TimeSpan BatteryTimeRemaining
        {
            get { return batteryTimeRemaining; }
        }
        BatteryChargeStatus batteryStatus;
        /// <summary>
        /// Gets the battery charge status.
        /// </summary>
        [System.ComponentModel.Description("Battery charge status."), System.ComponentModel.Category("Power")]
        public BatteryChargeStatus BatteryStatus
        {
            get { return batteryStatus; }
        }
        string shutdownDestination;
        /// <summary>
        /// Gets or sets the name of a boolean channel which will be set true when it is time to shutdown.
        /// </summary>
        /// <remarks>A channel of type KineticaRT.Servers.Power.Shutdown can shut the computer down.</remarks>
        [System.ComponentModel.Description("Name of a boolean channel which will be set true when it is time to shutdown."), System.ComponentModel.Category("Power")]
        [System.ComponentModel.DefaultValue("")]
        public string ShutdownDestination
        {
            get { return shutdownDestination; }
            set
            {
                if (shutdownDestination != value)
                {
                    shutdownDestination = value;
                    shutdownDestinationChannel = OnObjectSearch(shutdownDestination) as KineticaRT.Channels.IChannel ;
                    OnPropertyChanged("ShutdownDestination");
                }
            }
        }
        TimeSpan shutdownAfter = TimeSpan.FromSeconds(-1);
        /// <summary>
        /// Gets or sets the amount of time from when the battery starts to be used to when its time to shutdown.
        /// </summary>
        [System.ComponentModel.Description("Amount of time from when the battery starts to be used to when its time to shutdown."), System.ComponentModel.Category("Power")]
        public TimeSpan ShutdownAfter
        {
            get { return shutdownAfter; }
            set
            {
                if (shutdownAfter != value)
                {
                    shutdownAfter = value;
                    OnPropertyChanged("ShutdownAfter");
                }
            }
        }
        TimeSpan shutdownWhen = TimeSpan.FromSeconds(-1);
        /// <summary>
        /// Gets or sets the amount of remaining estimated battery time at which a shutdown will be triggered.
        /// </summary>
        [System.ComponentModel.Description("Amount of remaining estimated battery time at which a shutdown will be triggered."), System.ComponentModel.Category("Power")]
        public TimeSpan ShutdownWhen
        {
            get { return shutdownWhen; }
            set
            {
                if (shutdownWhen != value)
                {
                    shutdownWhen = value;
                    OnPropertyChanged("ShutdownWhen");
                }
            }
        }
        bool shutdown;
        /// <summary>
        /// Gets the shutdown state.  Will be set true if it is time to shutdown.
        /// </summary>
        [System.ComponentModel.Description("Shutdown state.  Will be set true if it is time to shutdown."), System.ComponentModel.Category("Power")]
        public bool Shutdown
        {
            get { return shutdown; }
        }
        KineticaRT.Channels.IChannel shutdownDestinationChannel;




        #endregion

        #region Connection
        /// <summary>
        /// Connects this channel to any other channels or devices needed.
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            shutdownDestinationChannel = OnObjectSearch(shutdownDestination) as KineticaRT.Channels.IChannel;

            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Connect", Name);
        }

        /// <summary>
        /// Make any remaining connections and start updates.
        /// </summary>
        public override void Connect2()
        {
            base.Connect2();

            lock (this)
            {
                if (!connected2)
                {
                    powerModeHandler = new Microsoft.Win32.PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
                    Microsoft.Win32.SystemEvents.PowerModeChanged += powerModeHandler;

                    shutdown = false;
                    online = true;
                    Updater_Update();
                    updateHandler = new Updater.UpdateEventHandler(Updater_Update);
                    KineticaRT.Services.TenSecondUpdater.Update += updateHandler;
                    connected2 = true;
                }
            }

            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Connect2", Name);
        }

        bool connected2;


        private Microsoft.Win32.PowerModeChangedEventHandler powerModeHandler;

        void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Power: " + e.Mode.ToString(), this.Name);

            if (e.Mode == Microsoft.Win32.PowerModes.Resume)
            {
                if (shutdown)
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Resume", this.Name);
                    shutdown = false;
                    online = true;
                    shutdownDestinationChannel.Value = KineticaRT.Values.Values.False;
                }
            }
        }

        private void Updater_Update()
        {
            bool new_shutdown = false;

            bool new_online = SystemInformation.PowerStatus.PowerLineStatus != PowerLineStatus.Offline ;
            if (shutdownAfter >= TimeSpan.Zero)
            {
                if (new_online != online)
                {
                    if (new_online || shutdownAfter < TimeSpan.Zero)
                    {
                        shutdownTime = DateTime.MaxValue;
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "NoShutdown", this.Name);
                    }
                    else
                    {
                        shutdownTime = DateTime.Now + shutdownAfter;
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "ShutdownTime: " + shutdownTime.ToShortTimeString() + ", Now: " + DateTime.Now.ToShortTimeString(), this.Name);
                    }
                }
                if (!new_online && (DateTime.Now > shutdownTime))
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "ActivateShutdown", this.Name);
                    new_shutdown = true;
                }
            }
            else
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "ShutdownAfter < Zero", this.Name);
            }

            if (new_online != online)
            {
                online = new_online;
                OnPropertyChanged("Online");
            }

            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Shutdown: " + shutdownTime.ToShortTimeString()+", online: "+online.ToString(), this.Name);

            int seconds_remaining = SystemInformation.PowerStatus.BatteryLifeRemaining;
            TimeSpan new_batteryTimeRemaining = TimeSpan.FromSeconds(seconds_remaining);
            if (new_batteryTimeRemaining != batteryTimeRemaining)
            {
                batteryTimeRemaining = new_batteryTimeRemaining;
                OnPropertyChanged("BatteryTimeRemaining");
            }

            BatteryChargeStatus new_batteryStatus = SystemInformation.PowerStatus.BatteryChargeStatus;
            if (new_batteryStatus != batteryStatus)
            {
                batteryStatus = new_batteryStatus;
                OnPropertyChanged("BatteryStatus");
            }

            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "seconds_remaining: " + seconds_remaining.ToString(System.Globalization.CultureInfo.CurrentCulture), this.Name);
            if (seconds_remaining != -1)
            {
                if (shutdownWhen.TotalSeconds != -1)
                {
                    if (seconds_remaining < shutdownWhen.TotalSeconds)
                    {
                        new_shutdown = true;
                    }
                }

            }

            if (new_shutdown != shutdown)
            {
                shutdown = new_shutdown;

                if (shutdownDestinationChannel != null)
                {
                    if (shutdown)
                    {
                        shutdownDestinationChannel.Value = KineticaRT.Values.Values.True;
                    }
                    else
                    {
                        shutdownDestinationChannel.Value = KineticaRT.Values.Values.False;
                    }
                }

                OnPropertyChanged("Shutdown");
            }



            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Power: " + online, this.Name);
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Battery: " + batteryTimeRemaining, this.Name);
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "BatteryCharge: " + batteryStatus, this.Name);

            Value = new KineticaRT.Values.Text (SystemInformation.PowerStatus.BatteryChargeStatus.ToString());
        }

		/// <summary>
        /// Disconnects this channel from all other channels and devices.
        /// </summary>
		public override void Disconnect ()
		{
            lock (this)
            {
                if (connected2)
                {
                    connected2 = false;
                    KineticaRT.Services.OneSecondUpdater.Update -= updateHandler;
                    updateHandler = null;
                    Microsoft.Win32.SystemEvents.PowerModeChanged -= powerModeHandler;
                    powerModeHandler = null;
                }

                shutdownDestinationChannel = null;
            }
            base.Disconnect();
		}

        private Updater.UpdateEventHandler updateHandler;

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Diagnostics;
using System.ComponentModel;

namespace KineticaRT.Logic.Timing
{
	/// <summary>
	/// Channel which works as a stopwatch.
	/// </summary>
	[ChangeRecord ("1.0", "03 12 2007", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2007 KineticaRT Ltd.")]
	[Serializable]
    public class Stopwatch : KineticaRT.Channels.InfoChannel, System.Runtime.Serialization.ISerializable
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Logic.Timing.Stopwatch", "KineticaRT.Logic.Timing.Stopwatch Messages");

        private enum TimerState
        {
            Stopped,
            Paused,
            Running,
        }

        #region Construction
		/// <summary>
		/// Initialises a new instance of the class
		/// </summary>
        public Stopwatch ()
		{
            Initialise();
		}

        private void Initialise ()
        {
            //info = (System.Globalization.DateTimeFormatInfo)System.Globalization.DateTimeFormatInfo.CurrentInfo.Clone();
            startTime = stopTime = DateTime.Now;
            RawValue = new KineticaRT.Values.TimeSpan(System.TimeSpan.Zero);
        }
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

            info.AddValue("Interval", interval);
            info.AddValue("Enabled", enabled);
            info.AddValue("ShowMilliseconds", showMilliseconds);
        }

        /// <summary>Constructor for the ISerializable interface.</summary>
        protected Stopwatch(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

            Initialise();
            try
            {
                interval = info.GetDouble("Interval");
                enabled = info.GetBoolean("Enabled");
                showMilliseconds = info.GetBoolean("ShowMilliseconds");
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, FaultStrings.MissingProperty + GetType(), this.Name);
            }
        }
		#endregion

        #region IDisposable
        /// <summary>
        /// Destructor for class
        /// </summary>
        ~Stopwatch()
        {
            Dispose(false);
        }

        /// <summary>
        /// Closes all resources used by this class, including the update thread.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }
                // free native resources
            }
            disposed = true;
        }
        private bool disposed;

        /// <summary>
        /// Closes all resources used by this class, including the update thread.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
        #endregion

        #region IEnabled
        /// <summary>Occurs when the value of the Value property changes.</summary>
        public event EnabledEventHandler EnabledChanged;
        //        /// <summary>Called when the value of the Value property changes.</summary>
        //        private void OnEnabledChanged()
        //        {
        //            if (EnabledChanged != null)
        //            {
        //                EnabledChanged(this, trigger ? KineticaRT.EnabledEventArgs.EnabledEvent : KineticaRT.EnabledEventArgs.DisabledEvent);
        //                OnPropertyChanged("Enabled");
        //            }
        //        }
        #endregion
        
        #region Connection
        private bool connected;

        /// <summary>
        /// Connects the channel.
        /// </summary>
        public override void Connect()
		{
			base.Connect () ;

            lock (this)
            {
                if (!connected)
                {
                    connected = true;
                    ConnectUpdater();
                }
            }
        }

		/// <summary>
		/// Disconnects the channel.
		/// </summary>
		public override void Disconnect ()
		{
            lock (this)
            {
                if (connected)
                {
                    DisconnectUpdater();
                    connected = false;
                }
            }
            
            base.Disconnect () ;
		}
		#endregion

        #region Properties

        //string format = "G";
        //System.Globalization.DateTimeFormatInfo info;
        ///// <summary>
        ///// Gets or sets the format of the channel when viewed as text.
        ///// </summary>
        ///// <value>A string describing the display format of the channel.</value>
        ///// <remarks>The following formats can be used:
        ///// <ul>
        ///// <li>"d" - Short date</li>
        ///// <li>"D" - Long date</li>
        ///// <li>"f" - Long date, short time</li>
        ///// <li>"F" - Long date, long time</li>
        ///// <li>"g" - short date, short time</li>
        ///// <li>"G" - short date, long time</li>
        ///// <li>"t" - short time</li>
        ///// <li>"T" - Long time</li>
        ///// <li>"u" - Universal time (sortable)</li>
        ///// <li>"U" - Universal time</li>
        ///// <li>"dddd yyyy/MM/dd hh:mm:ss fff" - Custom</li>
        ///// </ul>
        ///// </remarks>
        //[System.ComponentModel.Description("Format of channel.  This is used to convert the value to text. Eg. d,D,f,F,g,G,t,T,u,U"), System.ComponentModel.Category("Numeric")]
        //public string Format
        //{
        //    get
        //    {
        //        return format;
        //    }
        //    set
        //    {
        //        if (format != value)
        //        {
        //            format = value;
        //            OnPropertyChanged("Format");
        //        }
        //    }
        //}


        private bool showMilliseconds;
        /// <summary>
        /// Gets or sets whether to show the miliseconds when the Value is displayed as text.
        /// </summary>
        [System.ComponentModel.Description("Whether to show the miliseconds when the Value is displayed as text."), System.ComponentModel.Category("Timer")]
        [DefaultValue(false)]
        public Boolean ShowMilliseconds
        {
            get
            {
                return showMilliseconds;
            }
            set
            {
                if (showMilliseconds != value)
                {
                    showMilliseconds = value;
                    OnPropertyChanged("showMilliseconds");
                    OnValueChanged(Value);                  // Hopefully update displays.
                }
            }
        }


        private Boolean enabled;
        /// <summary>
        /// Gets or sets the enabled state.
        /// When enabled the stopwatch will be either running or paused.
        /// </summary>
        [System.ComponentModel.Description("The enabled state, if true then the stopwatch is running or paused."), System.ComponentModel.Category("Timer")]
        [DefaultValue(false)]
        public Boolean Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    lock (this)
                    {
                        enabled = value;

                        if (EnabledChanged != null)
                        {
                            EnabledChanged(this, enabled ? KineticaRT.EnabledEventArgs.EnabledEvent : KineticaRT.EnabledEventArgs.DisabledEvent);
                            OnPropertyChanged("Enabled");
                        }

                        StartStop(enabled?TimerState.Running:TimerState.Stopped);
                    }
                }
            }
        }

        private double interval = 1;
        /// <summary>
        /// Gets or sets how long in seconds between updating of the current value, i.e. ticks of the stopwatch.
        /// </summary>
        [System.ComponentModel.Description("How long in seconds between updating of the current value."), System.ComponentModel.Category("Timer")]
        [DefaultValue(1)]
        public double Interval
        {
            get
            {
                return interval;
            }
            set
            {
                if (interval != value)
                {
                    interval = value;
                    lock (this)
                    {
                        ReconnectUpdater();
                    }
                    OnPropertyChanged("Interval");
                }
            }
        }

        private bool paused;
        /// <summary>
        /// Gets or sets the paused state, when paused the stopwatch is not running but can be restarted without being cleared.
        /// </summary>
        [System.ComponentModel.Description("The paused state of the stopwatch."), System.ComponentModel.Category("Timer")]
        [DefaultValue(false)]
        public bool Paused
        {
            get
            {
                return paused;
            }
            set
            {
                if (paused != value)
                {
                    paused = value;
                    if (paused)
                    {
                        StartStop(TimerState.Paused);
                    }
                    else if (lastState == TimerState.Running)
                    {
                        StartStop(TimerState.Running);
                    }

                    OnPropertyChanged("Paused");
                }
            }
        }

        /// <summary>
        /// Zeroes the stopwatch or gets it's zeroed state.
        /// </summary>
        [System.ComponentModel.Description("Zeroes the stopwatch or gets it's zeroed state."), System.ComponentModel.Category("Timer")]
        public bool Zeroed
        {
            get
            {
                switch (currentState)
                {
                    case TimerState.Running:
                        {
                            return false;
                        }

                    case TimerState.Paused:
                        {
                            return startTime == stopTime;
                        }

                    case TimerState.Stopped:
                        {
                            return startTime == stopTime;
                        }
                    default:
                        throw new System.OverflowException("Invalid state.");
                }
            }
            set
            {
                if (value)
                {
                    lock (this)
                    {
                        switch (currentState)
                        {
                            case TimerState.Running:
                                {
                                    startTime = DateTime.Now;
                                    OnPropertyChanged("Zeroed");
                                }
                                break;

                            case TimerState.Paused:
                                {
                                    if (!Zeroed)
                                    {
                                        startTime = stopTime;
                                        OnPropertyChanged("Zeroed");
                                    }
                                }
                                break;

                            case TimerState.Stopped:
                                {
                                    if (!Zeroed)
                                    {
                                        startTime = stopTime;
                                        OnPropertyChanged("Zeroed");
                                    }
                                }
                                break;
                            default:
                                throw new System.OverflowException("Invalid state.");
                        }
                    }

                    SetValue();
                }
            }
        }
        #endregion

        #region update
        private KineticaRT.Updater.UpdateEventHandler updateHandler;
        private KineticaRT.Updater updater;
        private DateTime startTime;
        private DateTime stopTime;

        private void DisconnectUpdater()
        {
            if (updater != null)
            {
                updater.Update -= updateHandler;
                updater.Dispose();
                updater = null;
                updateHandler = null;
            }
        }

        private void ReconnectUpdater()
        {
            if (updater != null)
            {
                DisconnectUpdater();
                updater = new KineticaRT.Updater("Timer " + Name, (int)(interval * 1000));
                updateHandler = new KineticaRT.Updater.UpdateEventHandler(updater_Update);
                updater.Update += updateHandler;
            }
        }
        private void ConnectUpdater()
        {
            if (updater == null)
            {
                updater = new KineticaRT.Updater("Timer " + Name, (int)(interval * 1000));
                updateHandler = new KineticaRT.Updater.UpdateEventHandler(updater_Update);
                updater.Update += updateHandler;
            }
        }

        private void updater_Update()
        {
            SetValue();
        }

        private void SetValue()
        {
            lock (this)
            {
                this.Value = new KineticaRT.Values.TimeSpan((currentState == TimerState.Running ? DateTime.Now : stopTime) - startTime);
            }
        }
        #endregion

        #region Logic
        private TimerState currentState;
        private TimerState lastState;

        private void StartStop(TimerState newState)
        {
            lock (this)
            {
                if (newState != currentState)
                {
                    lastState = currentState;
                    bool zeroed = Zeroed;

                    switch (newState)
                    {
                        case TimerState.Running:
                            {
                                switch (currentState)
                                {
                                    case TimerState.Stopped:
                                        startTime = DateTime.Now;
                                        currentState = TimerState.Running;
                                        ConnectUpdater();
                                        break;
                                    case TimerState.Paused:
                                        paused = false;
                                        OnPropertyChanged("Paused");
                                        startTime += DateTime.Now - stopTime;
                                        currentState = TimerState.Running;
                                        ConnectUpdater();
                                        break;
                                }
                            }
                            break;
                        case TimerState.Stopped:
                            {
                                switch (currentState)
                                {
                                    case TimerState.Running:
                                        stopTime = DateTime.Now;
                                        currentState = TimerState.Stopped;
                                        DisconnectUpdater();
                                        break;
                                    case TimerState.Paused:
                                        paused = false;
                                        OnPropertyChanged("Paused");
                                        currentState = TimerState.Stopped;
                                        DisconnectUpdater();
                                        break;
                                }
                            }
                            break;
                        case TimerState.Paused:
                            {
                                switch (currentState)
                                {
                                    case TimerState.Running:
                                        paused = true;
                                        OnPropertyChanged("Paused");
                                        stopTime = DateTime.Now;
                                        currentState = TimerState.Paused;
                                        DisconnectUpdater();
                                        break;
                                    case TimerState.Stopped:
                                        paused = true;
                                        OnPropertyChanged("Paused");
                                        currentState = TimerState.Paused;
                                        DisconnectUpdater();
                                        break;
                                }
                            }
                            break;
                    }

                    if (zeroed && !Zeroed) OnPropertyChanged("Zeroed");

                    SetValue();
                }
            }
        }
        #endregion

        /// <summary>
        /// Converts this Value to a System.String.
        /// </summary>
        /// <returns>The result.</returns>
        public override string ToString()
        {
            try
            {
                if (Value != null)
                {
                    try
                    {
                        TimeSpan display_value = (TimeSpan)System.Convert.ChangeType(Value, typeof(System.TimeSpan));
                        string time = display_value.Hours.ToString("00") + ":" + display_value.Minutes.ToString("00") + ":" + display_value.Seconds.ToString("00");
                        if (showMilliseconds) time += "." + display_value.Milliseconds.ToString("000");
                        return time;
                    }
                    catch (System.InvalidCastException)
                    {
                        return Value.ToString();
                    }
                }
                else
                {
                    return "null";
                }
            }
            catch (System.FormatException e)
            {
                return e.Message;
            }
        }
    }
}


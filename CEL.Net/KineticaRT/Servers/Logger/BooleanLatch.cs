/*
 * Created by SharpDevelop.
 * User: sylvainb
 * Date: 7/9/2012
 * Time: 9:10 AM
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
    /// Channel containing a link to another Channel containing a boolean (True/False) value.
    /// </summary>
    [Serializable]
    public class BooleanLatch : KTC.BooleanChannel, KT.IEnabled, KTSL.ILoggerChannel
    {
        #region Fields

        private bool enabled = true;
        private int index;
        private KTSL.Logger logger;
        private KTSL.Logger eventLogger;
        private string loggerName = "";
        private string eventLoggerName = "";

        private string source = "";
        private KTC.IChannel sourceChannel;
        private KTC.ValueChangedEventHandler srcHdlr;

        private KTV.Value resetValue = KTV.Values.False;
        private string reset = "";
        private KTC.IChannel resetChannel;
        private KTC.ValueChangedEventHandler resetHdlr;

        private KTC.ValueChangedEventHandler serverHdlr;

        #endregion

        #region Constructor

        public BooleanLatch()
            : base()
        {
            this.loggerName = "";
            this.eventLoggerName = "";
            this.index = 1;
            base.Value = KTV.Values.False;
            this.resetValue = KTV.Values.False;
            this.resetHdlr = new KTC.ValueChangedEventHandler(this.Reset_OnValueChanged);
            this.srcHdlr = new KTC.ValueChangedEventHandler(this.source_OnValueChanged);
            this.serverHdlr = new KTC.ValueChangedEventHandler(this.Server_OnValueChanged);
        }

        /// <summary>
        /// Constructor for the ISerializable interface.
        /// </summary>
        public BooleanLatch(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            try
            {
                this.reset = info.GetString("Reset");
                this.source = info.GetString("Source");
                this.loggerName = info.GetString("Logger");
                this.eventLoggerName = info.GetString("EventLogger");
                this.index = info.GetInt32("Index");
                this.enabled = info.GetBoolean("Enabled");
            }
            catch (SerializationException exception1)
            {
                Trace.WriteLine(exception1.Message, base.GetType().ToString());
            }
            this.resetHdlr = new KTC.ValueChangedEventHandler(this.Reset_OnValueChanged);
            this.srcHdlr = new KTC.ValueChangedEventHandler(this.source_OnValueChanged);
            this.serverHdlr = new KTC.ValueChangedEventHandler(this.Server_OnValueChanged);
        }

        #endregion Constructor

        #region Events

        public event KT.EnabledEventHandler EnabledChanged;

        #endregion

        #region Properties

        //HighTriggered Not used for BooleanLatch channel
        public bool HighTriggered
        {
            get
            {
                return false;
            }
        }
        //LowTriggered Not used for BooleanLatch channel
        public bool LowTriggered
        {
            get
            {
                return false;
            }
        }

        [Category("Link"), Editor(typeof(ChannelSelectorEditor), typeof(UITypeEditor)), Description("Name of reset channel.")]
        public string Reset
        {
            get
            {
                return this.reset;
            }
            set
            {
                if (this.reset != value)
                {
                    this.reset = value;
                    this.OnPropertyChanged("Reset");
                }
            }
        }
        [Category("Channel"), Description("The value of this channel.")]
        public KTV.Value ResetValue
        {
            get
            {
                return this.resetValue;
            }
            set
            {
                this.resetValue = value;
            }
        }

        [Category("Logger"), DefaultValue("true"), Description("Indicates if the channel is to be logged during recording.")]
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    this.OnEnabledChanged();
                }
            }
        }

        [Category("Logger"), DefaultValue("1"), Description("Indicates the ordering of the channels in the data file.")]
        public int Index
        {
            get
            {
                return this.index;
            }
            set
            {
                if (this.index != value)
                {
                    this.index = value;
                    this.OnPropertyChanged("Index");
                }
            }
        }

        [Category("Logger"), DefaultValue(""), Description("The name of the Logger.CsvFile component which records/replays data from/to this channel.")]
        public string Logger
        {
            get
            {
                return this.loggerName;
            }
            set
            {
                if (this.loggerName != value)
                {
                    this.loggerName = value;
                    this.OnPropertyChanged("Logger");
                }
            }
        }
        [Category("Logger"), DefaultValue(""), Description("The name of the Logger.CsvFile component which records/replays data from/to this channel.")]
        public string EventLogger
        {
            get
            {
                return this.eventLoggerName;
            }
            set
            {
                if (this.eventLoggerName != value)
                {
                    this.eventLoggerName = value;
                    this.OnPropertyChanged("EventLogger");
                }
            }
        }

        [Category("Link"), Editor(typeof(ChannelSelectorEditor), typeof(UITypeEditor)), Description("Name of source channel.")]
        public string Source
        {
            get
            {
                return this.source;
            }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    this.OnPropertyChanged("Source");
                }
            }
        }

        #endregion

        #region Methods

        public override void Connect()
        {
            

            lock (this)
            {
                if (this.Connected)
                {
                    return;
                }
                this.logger = base.OnObjectSearch(this.loggerName) as KTSL.Logger;
                this.eventLogger = base.OnObjectSearch(this.eventLoggerName) as KTSL.Logger;
                if (this.logger != null)
                {
                    this.logger.AddChannel(this);
                    this.logger.ValueChanged += this.serverHdlr;
                }
                if (this.eventLogger != null)
                {
                    this.eventLogger.AddChannel(this);
                    this.eventLogger.ValueChanged += this.serverHdlr;
                }
                if ((this.reset != null) && (this.reset.Length > 0))
                {
                    this.resetChannel = base.OnObjectSearch(this.reset) as KTC.IChannel;
                    if (this.resetChannel != null)
                    {
                        this.resetValue = this.resetChannel.Value;
                        this.resetChannel.ValueChanged += this.resetHdlr;
                    }
                    else
                    {
                        this.resetValue = KTV.Values.NotConnected;
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Source" + this.reset);//ft
                    }
                }
                if ((this.source != null) && (this.source.Length > 0))
                {
                    this.sourceChannel = base.OnObjectSearch(this.source) as KTC.IChannel;
                    if (this.sourceChannel != null)
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
            lock (this)
            {
                if (this.Connected)
                {
                    if (this.logger != null)
                    {
                        this.logger.ValueChanged -= this.serverHdlr;
                        this.logger.RemoveChannel(this);
                        this.logger = null;
                    }
                    if (this.eventLogger != null)
                    {
                        this.eventLogger.ValueChanged -= this.serverHdlr;
                        this.eventLogger.RemoveChannel(this);
                        this.eventLogger = null;
                    }
                    if (this.resetChannel != null)
                    {
                        this.resetChannel.ValueChanged -= this.resetHdlr;
                        this.resetValue = KTV.Values.NotConnected;
                        this.resetChannel = null;
                    }
                    if (this.sourceChannel != null)
                    {
                        this.sourceChannel.ValueChanged -= this.srcHdlr;
                        this.Value = KTV.Values.NotConnected;
                        this.sourceChannel = null;
                    }
                    base.Disconnect();
                }
            }
        }

        protected void OnEnabledChanged()
        {
            if (this.EnabledChanged != null)
            {
                this.EnabledChanged(this, this.enabled ? KT.EnabledEventArgs.EnabledEvent : KT.EnabledEventArgs.DisabledEvent);
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if ((info == null))
            {
                throw new ArgumentNullException("info");
            }
            base.GetObjectData(info, context);
            info.AddValue("Source", this.source);
            info.AddValue("Reset", this.reset);
            info.AddValue("Logger", this.loggerName);
            info.AddValue("EventLogger", this.eventLoggerName);
            info.AddValue("Index", this.index);
            info.AddValue("Enabled", this.enabled);
        }

        private void Server_OnValueChanged(KTC.IChannel sender, KTV.Value newVal)
        {
            if ((newVal.ToString() == "Stopped") &&
                 (this.logger != null) && (this.logger.CurrentMode != KTSL.Logger.Mode.Play) &&
                 (this.eventLogger != null) && (this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play))
            {
                if (this.sourceChannel != null)
                {
                    this.sourceChannel.Value = new KTV.Boolean(this.sourceChannel.Value == KTV.Values.True);
                }
                if (this.resetChannel != null)
                {
                    this.resetValue = this.resetChannel.Value;
                }
            }
        }

        private void Reset_OnValueChanged(KTC.IChannel sender, KTV.Value newVal)
        {
            if ((this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play)
             || (this.logger == null && this.eventLogger == null)
             || (this.logger == null && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play)
             || (this.eventLogger == null && this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play))
            {
                this.resetValue = newVal;

                if (newVal == KTV.Values.True) // reset latch active, set value to false
                {
                    base.Value = KTV.Values.False; //when reset is active latch value is false
                }
                else
                {
                    base.Value = this.sourceChannel.Value; //reset is not on anymore, reinit the latch value
                }
            }
        }

        private void source_OnValueChanged(KTC.IChannel sender, KTV.Value newVal)
        {
            if ((this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play)
             || (this.logger == null && this.eventLogger == null)
             || (this.logger == null && this.eventLogger != null && this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play)
             || (this.eventLogger == null && this.logger != null && this.logger.CurrentMode != KTSL.Logger.Mode.Play))
            {
                bool resetNotActive = this.resetValue != KTV.Values.True;
                bool actualValueNotLatch = base.Value != KTV.Values.True;

                if (resetNotActive && actualValueNotLatch)
                {
                    base.Value = newVal;
                }
            }
        }

        #endregion
    }
}

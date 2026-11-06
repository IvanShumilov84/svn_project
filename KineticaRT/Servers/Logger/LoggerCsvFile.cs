using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using System.Threading ;
using System.Diagnostics ;
using System.Security.Permissions;

#if  !CEL
namespace KineticaRT.Servers.Logger
{
    /// <summary>
    /// Records data to a CSV format file and replays it.
    /// </summary>
    [Description("CSV File logger")]
    [Serializable]
    [DefaultProperty("FileName")]
    public class CsvFile : Logger, ISerializable, IConnectable, IDisposable 
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Logger", "Logger Messages");

        private string		fileName ;
        private string		recordSpeed ;
        private Int64		maxFileSize ;
        private string		record ;
        private string		play ;
        private string		pause ;
        private string		comment = "" ;
        private string		step ;
        private string		stop ;
        private string		playSpeed ;
        private string		playTime ;
        private float		currentPlaySpeed = 1.0F ;
        private float		currentRecordSpeed = 10.0F ;
        private DateTime	time ;
        private DateTime	startReadTime ;
        private DateTime	currentReadTime ;
        private	string		currentReadLine ;
        private int			commaPosition ;
        private System.Threading.Thread updateThread ;
        private System.Collections.ArrayList channelList ;
        private System.Collections.ArrayList playList ;
        private	int			firstChannel ;
        private long		sleepTime ;

        private bool		connected ;
        private KineticaRT.Channels.IChannel					recordChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	recordChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					recordSpeedChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	recordSpeedChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					playSpeedChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	playSpeedChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					playChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	playChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					pauseChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	pauseChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					commentChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	commentChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					stepChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	stepChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					stopChannel ;
        private KineticaRT.Channels.ValueChangedEventHandler	stopChannelValueChangedHandler ;
        private KineticaRT.Channels.IChannel					fileNameChannel ;
        private KineticaRT.Channels.IChannel					playTimeChannel ;

        private System.IO.StreamWriter writer ;
        private System.IO.StreamReader reader ;

        private Mode	currentMode ;
        private bool	paused ;
        private bool	doStep ;

        /// <summary>
        /// Constructor for the class
        /// </summary>
        public CsvFile()
        {
            fileName = "" ;
            recordSpeed = "10" ;
            //maxFileSize = 0 ;

            record = "False" ;
            play = "False" ;
            pause = "False" ;
            comment = "" ;
            step = "False" ;
            stop = "True" ;
            playSpeed = "1" ;
            playTime = "" ;

            Initialise () ;
        }

        /// <summary>
        /// Called during construction including ISerialisable construction.
        /// </summary>
        private void Initialise ()
        {
            recordChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(recordChannel_ValueChanged)	;
            recordSpeedChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(recordSpeedChannel_ValueChanged) ;
            playSpeedChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(playSpeedChannel_ValueChanged) ;
            playChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(playChannel_ValueChanged) ;
            pauseChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(pauseChannel_ValueChanged);
            commentChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(commentChannel_ValueChanged);
            stepChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(stepChannel_ValueChanged);
            stopChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(stopChannel_ValueChanged);

            channelList = new System.Collections.ArrayList () ;
            playList = new System.Collections.ArrayList () ;
            time = DateTime.MinValue ;
            RawValue= new KineticaRT.Values.Text ("Initialised") ;
        }

        #region IDisposable
        //	Note: No unmanaged resources so destructor not required but full syntax is included for FxCop.
        private bool disposed ;

        /// <summary>
        /// Releases all resources that are in use.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize (this) ;
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if(disposing)
                {
                    // Dispose managed resources.
                    if (writer != null)
                    {
                        writer.Close () ;
                        writer = null ;
                    }
                    if (reader != null)
                    {
                        reader.Close () ;
                        reader = null ;
                    }
                    if (updateThread != null)
                    {
                        lock (updateThread)
                        {
                            updateThread.Abort () ;
                        }

                        updateThread.Join() ;
                        updateThread = null ;
                    }
                }

                disposed = true;
            }
        }

        //		~CsvFile()      
        //		{
        //			Dispose(false) ;
        //		}
        #endregion


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
            info.AddValue ("FileName",		fileName) ;
            info.AddValue ("RecordSpeed",	recordSpeed) ;
            info.AddValue ("PlaySpeed",		playSpeed) ;
            info.AddValue ("MaxFileSize",	maxFileSize) ;
            info.AddValue ("Record",		record	) ;
            info.AddValue ("Play",			play	) ;
            info.AddValue ("Pause",			pause	) ;
            info.AddValue ("Step",			step	) ;
            info.AddValue ("Stop",			stop	) ;
            info.AddValue ("Time",			playTime) ;
            info.AddValue ("Comment",		comment	) ;
        }
        /// <summary>
        /// Constructor for ISerializable interface.
        /// </summary>
        protected CsvFile (SerializationInfo info, StreamingContext context):base(info, context)
        {
            if( info == null ) throw new ArgumentNullException("info") ;

            Initialise () ;

            try
            {
                fileName	= info.GetString ("FileName") ;
                recordSpeed	= info.GetString ("RecordSpeed") ;
                playSpeed	= info.GetString ("PlaySpeed") ;
                maxFileSize	= info.GetInt64	 ("MaxFileSize") ;
                record		= info.GetString ("Record") ;
                play		= info.GetString ("Play") ;
                pause		= info.GetString ("Pause") ;
                step		= info.GetString ("Step") ;
                stop		= info.GetString ("Stop") ;
                playTime	= info.GetString ("Time") ;
                comment		= info.GetString ("Comment") ;
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// The current Mode of this logger
        /// </summary>
        /// <value>
        /// A Mode representing the current state.
        /// </value>
        [System.ComponentModel.Description("The current Mode of the logger."), System.ComponentModel.Category("Logger")] 
        [DefaultValue(Mode.Stop)]
        public override Mode CurrentMode
        {
            get 
            {
                return currentMode ;
            }
        }
        /// <summary>
        /// The name, and optional path, of the file to record to or read from.
        /// </summary>
        /// <value>
        /// A string representing the name, and optional path, of the file to record to or read from.
        /// </value>
        [System.ComponentModel.Description("The name, and optional path, of the file to record to or read from."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string FileName
        {
            get 
            {
                return fileName ;
            }
            set 
            {
                if (fileName != value)
                {
                    fileName = value ;
                    OnPropertyChanged("FileName") ;
                }
            }
        }
        /// <summary>
        /// Indicates how fast to record the data in Hz.
        /// </summary>
        /// <value>
        /// A float representing the number of records per second to be written to the file during recording.
        /// 0 = single step.
        /// </value>
        [System.ComponentModel.Description("Name of a numeric channel, indicates how fast to record the data in Hz."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("10")]
        public string RecordSpeed
        {
            get 
            {
                return recordSpeed ;
            }
            set 
            {
                if (recordSpeed != value)
                {
                    recordSpeed = value ;
                    OnPropertyChanged("RecordSpeed") ;
					
                    SetCurrentRecordSpeed () ;
                }
            }
        }
        /// <summary>
        /// Recording will stop just before the file reaches this size.  This is for protection against accidentally using excessive disk space.
        /// </summary>
        /// <value>
        /// An Int64 representing the maximum number of MBytes to be written to the file before recording terminates automatically.
        /// A value of zero indicates no limit.
        /// </value>
        [System.ComponentModel.Description("Recording will stop just before the file reaches this size.  Units - MBytes.  0 = No limit.  This is for protection against accidentally using excessive disk space."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("0")]
        public Int64 MaxFileSize
        {
            get 
            {
                return maxFileSize ;
            }
            set 
            {
                if (maxFileSize != value)
                {
                    maxFileSize = value ;
                    OnPropertyChanged("MaxFileSize") ;
                }
            }
        }
        /// <summary>
        /// Name of Boolean channel, the logger will record while this is set true.
        /// </summary>
        /// <value>
        /// A string representing the name, of the channel controling recording.
        /// </value>
        [System.ComponentModel.Description("Name of Boolean channel, the logger will record while this is set true."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string Record
        {
            get 
            {
                return record ;
            }
            set 
            {
                if (record != value)
                {
                    record = value ;
                    OnPropertyChanged("Record") ;
                }

                try
                {
                    bool new_val = System.Convert.ToBoolean (value, System.Globalization.NumberFormatInfo.CurrentInfo) ;
                    if (new_val == true)
                    {
                        recordChannel_ValueChanged (this, KineticaRT.Values.Values.True) ;
                    }
                }
                catch (System.FormatException)
                {
                }
            }
        }
        /// <summary>
        /// Name of Boolean channel, the logger will play while this is set true.
        /// </summary>
        /// <value>
        /// A string representing the name of the channel controlling playback.
        /// </value>
        [System.ComponentModel.Description("Name of Boolean channel, the logger will play while this is set true."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string Play
        {
            get 
            {
                return play ;
            }
            set 
            {
                if (play != value)
                {
                    play = value ;
                    OnPropertyChanged("Play") ;
                }

                try
                {
                    bool new_val = System.Convert.ToBoolean (value, System.Globalization.NumberFormatInfo.CurrentInfo) ;
                    if (new_val == true)
                    {
                        playChannel_ValueChanged (this, KineticaRT.Values.Values.True) ;
                    }
                }
                catch (System.FormatException)
                {
                }
            }
        }
        /// <summary>
        /// Optional - Name of Boolean channel, the logger will pause recording/playback while this is set true.
        /// </summary>
        /// <value>
        /// A string representing the name of the channel controlling pause of playback.
        /// </value>
        [System.ComponentModel.Description("Optional - Name of Boolean channel, the logger will pause recording/playback while this is set true."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string Pause
        {
            get 
            {
                return pause ;
            }
            set 
            {
                if (pause != value)
                {
                    pause = value ;
                    OnPropertyChanged("Pause") ;
                }

                try
                {
                    bool new_val = System.Convert.ToBoolean (value, System.Globalization.NumberFormatInfo.CurrentInfo) ;
                    if (new_val == true)
                    {
                        pauseChannel_ValueChanged (this, new_val ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False) ;
                    }
                }
                catch (System.FormatException)
                {
                }
            }
        }
        /// <summary>
        /// Optional - Name of channel for comments, the logger will write new values in this channel to the file if logging.
        /// </summary>
        /// <value>
        /// A string representing the name of the channel containing comments.
        /// </value>
        [System.ComponentModel.Description("Optional - Name of channel for comments, the logger will write new values in this channel to the file if logging."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string Comment
        {
            get 
            {
                return comment ;
            }
            set 
            {
                if (comment != value)
                {
                    comment = value ;
                    OnPropertyChanged("Comment") ;
                }
            }
        }
        /// <summary>
        /// Optional - Name of Boolean channel, the logger will record/play a single record when this is set true and RecordSpeed is set to 0.
        /// </summary>
        /// <value>
        /// A string representing the name of the channel that will cause a record to be written when recording.
        /// </value>
        [System.ComponentModel.Description("Optional - Name of Boolean channel, the logger will record/play a single record when this is set true and RecordSpeed is set to 0."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string Step
        {
            get 
            {
                return step ;
            }
            set 
            {
                if (step != value)
                {
                    step = value ;
                    OnPropertyChanged("Step") ;
                }

                try
                {
                    bool new_val = System.Convert.ToBoolean (value, System.Globalization.NumberFormatInfo.CurrentInfo) ;
                    if (new_val == true)
                    {
                        stepChannel_ValueChanged (this, KineticaRT.Values.Values.True) ;
                    }
                }
                catch (System.FormatException)
                {
                }
            }
        }
        /// <summary>
        /// Name of Boolean channel, the logger will stop playing or recording when this is set true.
        /// </summary>
        /// <value>
        /// A string representing the name of the channel controling the end of record or playback.
        /// </value>
        [System.ComponentModel.Description("Name of Boolean channel, the logger will stop playing or recording when this is set true."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string Stop
        {
            get 
            {
                return stop ;
            }
            set 
            {
                if (stop != value)
                {
                    stop = value ;
                    OnPropertyChanged("Stop") ;
                }

                try
                {
                    bool new_val = System.Convert.ToBoolean (value, System.Globalization.NumberFormatInfo.CurrentInfo) ;
                    if (new_val == true)
                    {
                        stopChannel_ValueChanged (this, KineticaRT.Values.Values.True) ;
                    }
                }
                catch (System.FormatException)
                {
                }
            }
        }
        /// <summary>
        /// Name of a numeric channel, the logger will replay data at the speed indicated by this channel.
        /// </summary>
        /// <value>
        /// A string representing the name of the channel controling the playback speed.
        /// A value of 1 indicates real time, 2 twice real time etc.
        /// </value>
        [System.ComponentModel.Description("Name of a numeric channel, the logger will replay data at the speed indicated by this channel."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("1")]
        public string PlaySpeed
        {
            get 
            {
                return playSpeed ;
            }
            set 
            {
                if (playSpeed != value)
                {
                    playSpeed = value ;
                    OnPropertyChanged("PlaySpeed") ;
					
                    SetCurrentPlaySpeed () ;
                }
            }
        }
        /// <summary>
        /// During playback this contains the record time of the current data.
        /// </summary>
        /// <value>
        /// A DateTime representing the recording time of the current data record.
        /// </value>
        [System.ComponentModel.Description("During playback this contains the record time of the current data."), System.ComponentModel.Category("Logger")] 
        [DefaultValue("")]
        public string Time
        {
            get 
            {
                return playTime ;
            }

            set
            {
                if (playTime != value)
                {
                    playTime = value ;
                    OnPropertyChanged("Time") ;
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
                connected = true ;

                if (recordChannel != null)
                {
                    recordChannel.ValueChanged -= recordChannelValueChangedHandler ;
                    recordChannel = null ;
                }
                if (record.Length > 0)
                {
                    recordChannel = OnObjectSearch(record) as KineticaRT.Channels.IChannel ;
                    if (recordChannel == null)
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + record, "Connect") ;
                    }
                    else
                    {
                        recordChannel.ValueChanged += recordChannelValueChangedHandler ;
                    }
                }

                if (playChannel != null)
                {
                    playChannel.ValueChanged -= playChannelValueChangedHandler ;
                    playChannel = null ;
                }
                if (play.Length > 0)
                {
                    playChannel = OnObjectSearch(play) as KineticaRT.Channels.IChannel ;
                    if (playChannel == null) 
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + play, "Connect") ;
                    }
                    else
                    {
                        playChannel.ValueChanged += playChannelValueChangedHandler;
                    }
                }

                SetCurrentRecordSpeed () ;
                SetCurrentPlaySpeed () ;

                if (pauseChannel != null)
                {
                    pauseChannel.ValueChanged -= pauseChannelValueChangedHandler ;
                    pauseChannel = null ;
                }
                if (pause.Length > 0)
                {
                    pauseChannel = OnObjectSearch(pause) as KineticaRT.Channels.IChannel ;
                    if (pauseChannel == null) 
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + pause, "Connect") ;
                    }
                    else
                    {
                        pauseChannel.ValueChanged += pauseChannelValueChangedHandler ;
                    }
                }

                if (commentChannel != null)
                {
                    commentChannel.ValueChanged -= commentChannelValueChangedHandler ;
                    commentChannel = null ;
                }
                if (comment.Length > 0)
                {
                    commentChannel = OnObjectSearch(comment) as KineticaRT.Channels.IChannel ;
                    if (commentChannel == null) 
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + comment, "Connect") ;
                    }
                    else
                    {
                        commentChannel.ValueChanged += commentChannelValueChangedHandler ;
                    }
                }

                if (stepChannel != null)
                {
                    stepChannel.ValueChanged -= stepChannelValueChangedHandler ;
                    stepChannel = null ;
                }
                if (step.Length > 0)
                {
                    stepChannel = OnObjectSearch(step) as KineticaRT.Channels.IChannel ;
                    if (stepChannel == null) 
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + step, "Connect") ;
                    }
                    else
                    {
                        stepChannel.ValueChanged += stepChannelValueChangedHandler ;
                    }
                }

                if (stopChannel != null)
                {
                    stopChannel.ValueChanged -= stopChannelValueChangedHandler ;
                    stopChannel = null ;
                }
                if (stop.Length > 0)
                {
                    stopChannel = OnObjectSearch(stop) as KineticaRT.Channels.IChannel ;
                    if (stopChannel == null) 
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + stop, "Connect") ;
                    }
                    else
                    {
                        stopChannel.ValueChanged += stopChannelValueChangedHandler ;
                    }
                }

                fileNameChannel = null ;
                if (!QuotedString(fileName))
                {
                    fileNameChannel = OnObjectSearch(fileName) as KineticaRT.Channels.IChannel ;
                    if (fileNameChannel == null) 
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + fileName, "Connect") ;
                    }
                }

                playTimeChannel = null ;
                if (!QuotedString(playTime))
                {
                    playTimeChannel = OnObjectSearch(playTime) as KineticaRT.Channels.IChannel ;
                    if (playTimeChannel == null) 
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Time channel not found-" + playTime, "Connect") ;
                    }
                }

                if (stopChannel != null) 
                {
                    stopChannel.Value = KineticaRT.Values.Values.True ;
                }
            }
        }

        /// <summary>
        /// Disconnect from other Objects.
        /// </summary>
        public override void Disconnect ()
        {
            EnsureStopped () ;

            lock (this)
            {
                if (connected)
                {
                    if (playSpeedChannel != null) 
                    {
                        playSpeedChannel.ValueChanged -= playSpeedChannelValueChangedHandler ;
                        playSpeedChannel = null ;
                    }

                    if (recordSpeedChannel != null) 
                    {
                        recordSpeedChannel.ValueChanged -= playSpeedChannelValueChangedHandler ;
                        recordSpeedChannel = null ;
                    }
                    recordChannel = null ;
                    playChannel = null ;
                    playSpeedChannel = null ;
                    pauseChannel = null ;
                    commentChannel = null ;
                    stepChannel = null ;
                    stopChannel = null ;
                    playTimeChannel = null ;
					
                    connected = false ;
                }
            }
            base.Disconnect () ;
        }
		
        #endregion

        private void recordChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            if (newVal == KineticaRT.Values.Values.True)
            {
//				playChannel.Value = KineticaRT.Values.Values.False ;
//				pauseChannel.Value = KineticaRT.Values.Values.False ;
//				stepChannel.Value = KineticaRT.Values.Values.False ;
//				stopChannel.Value = KineticaRT.Values.Values.False ;

                if (currentMode != Mode.Record)
                {
                    EnsureStopped () ;
                    OpenFileWrite () ;
                    if (writer != null)
                    {
                        currentMode = Mode.Record  ;
                        //this.Value = new KineticaRT.Values.Text ("Recording") ;
                    }
                    else
                    {
                        //recordChannel.Value = KineticaRT.Values.Values.False ;
                        //stopChannel.Value = KineticaRT.Values.Values.True ;
                    }
                }

                SetOutputs () ;
            }
        }

        private void playChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            if (newVal == KineticaRT.Values.Values.True)
            {
                if (currentMode != Mode.Play)
                {
                    EnsureStopped () ;
                    currentMode = Mode.Play ;
                    OpenFileRead () ;
                    SetOutputs () ;
                }
//				if (currentMode == Mode.play)
//				{
//					this.Value = new KineticaRT.Values.Text ("Playing") ;
//					recordChannel.Value = KineticaRT.Values.Values.False ;
//					pauseChannel.Value = KineticaRT.Values.Values.False ;
//					stepChannel.Value = KineticaRT.Values.Values.False ;
//					stopChannel.Value = KineticaRT.Values.Values.False ;
//				}
//				else
//				{
//					playChannel.Value = KineticaRT.Values.Values.False ;
//					recordChannel.Value = KineticaRT.Values.Values.False ;
//					pauseChannel.Value = KineticaRT.Values.Values.False ;
//					stepChannel.Value = KineticaRT.Values.Values.False ;
//					stopChannel.Value = KineticaRT.Values.Values.True ;
//				}
            }
        }

        private void SetCurrentRecordSpeed ()
        {
            if (recordSpeedChannel != null) 
            {
                recordSpeedChannel.ValueChanged -= recordSpeedChannelValueChangedHandler ;
                recordSpeedChannel = null ;
            }

            try
            {
                float new_val = System.Convert.ToSingle (recordSpeed,System.Globalization.NumberFormatInfo.CurrentInfo) ;
                currentRecordSpeed = new_val ;
            }
            catch (System.FormatException)
            {
                if (connected)
                {
                    if (record.Length > 0)
                    {
                        recordSpeedChannel = OnObjectSearch(recordSpeed) as KineticaRT.Channels.IChannel ;
                        if (recordSpeedChannel == null) 
                        {
                            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + recordSpeed, "Connect") ;
                        }
                        else
                        {
                            recordSpeedChannel.ValueChanged +=new KineticaRT.Channels.ValueChangedEventHandler(recordSpeedChannel_ValueChanged);
                            currentRecordSpeed = (float)recordSpeedChannel.Value.ToDouble() ;
                        }
                    }
                }
            }
        }

        private void recordSpeedChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            float difference = System.Math.Abs((float)newVal.ToDouble() - currentRecordSpeed) ;
            if (difference > 0.001)
            {
                currentRecordSpeed = (float)newVal.ToDouble() ;
            }
        }

        private void SetCurrentPlaySpeed ()
        {
            if (playSpeedChannel != null) 
            {
                playSpeedChannel.ValueChanged -= playSpeedChannelValueChangedHandler ;
                playSpeedChannel = null ;
            }

            try
            {
                float new_val = System.Convert.ToSingle (playSpeed, System.Globalization.NumberFormatInfo.CurrentInfo) ;
                currentPlaySpeed = new_val ;
            }
            catch (System.FormatException)
            {
                if (connected)
                {
                    if (play.Length > 0)
                    {
                        playSpeedChannel = OnObjectSearch(playSpeed) as KineticaRT.Channels.IChannel ;
                        if (playSpeedChannel == null) 
                        {
                            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Channel not found-" + playSpeed, "Connect") ;
                        }
                        else
                        {
                            playSpeedChannel.ValueChanged +=new KineticaRT.Channels.ValueChangedEventHandler(playSpeedChannel_ValueChanged);
                            currentPlaySpeed = (float)playSpeedChannel.Value.ToDouble() ;
                        }
                    }
                }
            }
        }

        private void playSpeedChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            float difference = System.Math.Abs((float)newVal.ToDouble() - currentPlaySpeed) ;
            if (difference > 0.01)
            {
                currentPlaySpeed = (float)newVal.ToDouble() ;
            }
        }

        private void pauseChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            //if ((this.currentMode == Mode.Play) || (this.currentMode == Mode.Record)) 
            //{
            if (newVal == KineticaRT.Values.Values.True)
            {
                paused = true ;
                //					this.Value = new KineticaRT.Values.Text ("Paused") ;
            }
            else
            {
                paused = false ;
                //					if (this.currentMode == Mode.play) 
                //					{
                //						this.Value = new KineticaRT.Values.Text ("Playing") ;
                //					}
                //					else
                //					{
                //						this.Value = new KineticaRT.Values.Text ("Recording") ;
                //					}
            }
            SetOutputs () ;
            //}
        }

        private void commentChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            if (this.currentMode == Mode.Record && updateThread != null) 
            {
                // Write new value to file.
                lock (updateThread)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder () ;
                    sb.Append (System.DateTime.Now.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo)) ;
                    sb.Append (".") ;
                    sb.Append (System.DateTime.Now.Millisecond.ToString("000",System.Globalization.NumberFormatInfo.InvariantInfo)) ;

                    if (firstChannel < channelList.Count)
                    {
                        bool first = true ;

                        for (int index = firstChannel ; index < channelList.Count ; index ++)
                        {
                            ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;

                            if (list_channel.Enabled)
                            {
                                sb.Append (",") ;
                                if (first)
                                {
                                    sb.Append ("\"Comment:") ;
                                    sb.Append (commentChannel.Value.ToString ()) ;
                                    sb.Append ("\"") ;
                                    first = false ;
                                }
                            }
                        }
                    }
			
                    writer.WriteLine (sb.ToString ()) ;
                    writer.Flush () ;
                }
            }
        }

        private void stepChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            if (newVal == KineticaRT.Values.Values.True)
            {
                doStep = true ;
                //stepChannel.Value = KineticaRT.Values.Values.False ;
            }
        }

        private void stopChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            if (newVal == KineticaRT.Values.Values.True)
            {
                switch (currentMode)
                {
                    case Mode.Record:
                        CloseFileWrite () ;
                        break ;
                    case Mode.Play:
                        CloseFileRead () ;
                        break ;
                }

                currentMode = Mode.Stop ;
                SetOutputs () ;
				
                EnsureStopped () ;
                //				recordChannel.Value = KineticaRT.Values.Values.False ;
//				if (playChannel != null)
//				{
//					playChannel.Value = KineticaRT.Values.Values.False ;
//				}
//				else if (!QuotedString(play))
//				{
//					play = "False" ; OnPropertyChanged("Play") ;
//				}
//				pauseChannel.Value = KineticaRT.Values.Values.False ;
//				stepChannel.Value = KineticaRT.Values.Values.False ;
//
//				this.Value = new KineticaRT.Values.Text ("Stopped") ;
            }
        }

        private void OpenFileRead ()
        {
            string filepath ;
			
            if (!QuotedString(fileName))
            {
                if (fileNameChannel != null)
                {
                    filepath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, fileNameChannel.Value.ToString()) ;
                }
                else
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "Open File failed: Not connected to " + fileName.Substring(1), this.Name) ; 
                    return ;
                }
            }
            else
            {
                filepath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, UnQuotedString(fileName)) ;
            }

            try
            {
                reader = new System.IO.StreamReader (filepath, true) ;

                if (reader != null) 
                {
                    FindFirstChannel () ;

                    lock (this)  // Access to updateThread.
                    {
                        updateThread = new System.Threading.Thread(new System.Threading.ThreadStart(ReadThread)) ;
                        updateThread.Name = Name + ": Player" ;
                        updateThread.Start () ;
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "Read: " + e.Message, this.Name) ; 
                this.Value = new KineticaRT.Values.Text (e.Message) ;
                currentMode = Mode.Stop ;
            }
        }

        private void OpenFileWrite ()
        {
            string filepath ;
			
            if (!QuotedString(fileName))
            {
                if (fileNameChannel != null)
                {
                    filepath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, fileNameChannel.Value.ToString()) ;
                }
                else
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "Open File failed: Not connected to " + fileName.Substring(1), this.Name) ; 
                    return ;
                }
            }
            else
            {
                filepath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, UnQuotedString(fileName)) ;
            }

            try
            {
                writer = new System.IO.StreamWriter (filepath, false, System.Text.Encoding.ASCII) ;

                if (writer != null) 
                {
                    FindFirstChannel () ;
                    HeaderWrite () ;

                    lock (this)  // Access to updateThread.
                    {
                        updateThread = new System.Threading.Thread(new System.Threading.ThreadStart(WriteThread)) ;
                        updateThread.Name = Name + ": Writer" ;
                        updateThread.Start () ;
                    }
                }
            }
            catch (System.IO.IOException e)
            {
                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "Write: " + e.Message, this.Name) ; 
                this.Value = new KineticaRT.Values.Text (e.Message) ;
                currentMode = Mode.Stop ;
            }
        }

        private ILoggerChannel FindChannel (string name)
        {
            ILoggerChannel list_channel ;

            for (int index = 0 ; index < channelList.Count ; index ++)
            {
                list_channel = (ILoggerChannel) channelList [index] ;
                if (list_channel.Description == name)
                {
                    return list_channel ;
                }
            }
		
            return null ;
        }

        private void HeaderRead ()
        {
            currentReadLine = reader.ReadLine () ;
            if (currentReadLine == null) return ;

            while (currentReadLine[0] == '"')
            {
                int name_end = currentReadLine.IndexOf ("\"",1) ;
                if (name_end > 0)
                {
                    string name = currentReadLine.Substring(1, name_end - 1) ;

                    ILoggerChannel list_channel = FindChannel (name) ;

                    if (list_channel != null)
                    {
                        if (list_channel.Enabled)
                        {
                            string value ;
                            int start_index = currentReadLine.IndexOf (",") + 1 ;
                            if (start_index != 0)
                            {
                                int end_index ;

                                if (currentReadLine[start_index] == '\"')
                                {
                                    end_index = currentReadLine.IndexOf ("\"", start_index+1) ;
                                    if (end_index != -1)
                                    {
                                        end_index = currentReadLine.IndexOf (",", end_index) ;
                                    }
                                }
                                else
                                {
                                    end_index = currentReadLine.IndexOf (",", start_index) ;
                                }
                                if (end_index == -1)
                                {
                                    end_index = currentReadLine.Length ;
                                }

                                value = currentReadLine.Substring (start_index, end_index - start_index) ;

                                if (value[0] == '\"')
                                {
                                    list_channel.Value = new KineticaRT.Values.Text (value.Substring(1,value.Length-2)) ;
                                }
                                else
                                {
                                    list_channel.Value = new KineticaRT.Values.Double (value) ;
                                }
                            }
                        }
                        else
                        {
                            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - ReadHeader - Channel not enabled: " + name, Name) ;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - ReadHeader - Channel not found: " + name, Name) ;
                    }

                    currentReadLine = reader.ReadLine () ;
                    if (currentReadLine == null) return ;
                }
            }

            // Now should be on date line.
            {
                int end_index ;
                string text ;
                end_index = currentReadLine.IndexOf (",") ;
                if (end_index < 0)
                {
                    end_index = currentReadLine.Length ;
                }

                text = currentReadLine.Substring (0,end_index) ;

                if (text != "Date Time")
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - Invalid file format. (Date Time field incorrect.)", Name) ;
                    currentReadLine = null ;
                }
                else if (end_index == currentReadLine.Length)
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - Empty File.", Name) ;
                    currentReadLine = null ;
                }
                else
                {
                    int start_index ;

                    while (true)
                    {
                        start_index = end_index + 1 ;
                        end_index = currentReadLine.IndexOf (",", start_index) ;
                        if (end_index == -1) end_index = currentReadLine.Length ;
                        text = currentReadLine.Substring (start_index+1,end_index-start_index - 2) ;

                        ILoggerChannel list_channel = FindChannel (text) ;


                        if (list_channel != null)
                        {
                            if (list_channel.Enabled)
                            {
                                playList.Add (list_channel) ;
                            }
                            else
                            {
                                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - Channel not enabled: " + text, Name) ;
                            }
                        }
                        else
                        {
                            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - Channel not found: " + text, Name) ;
                        }

                        if (end_index == currentReadLine.Length) break ;
                    }

                    startReadTime = DateTime.Now ;
                    currentReadTime = DateTime.MinValue ;
                    if (playTimeChannel != null) playTimeChannel.Value = new KineticaRT.Values.DateTime(currentReadTime) ;
                    ReadLine () ;
                }
            }
        }

        private void ReadLine ()
        {
            currentReadLine = reader.ReadLine () ;
            if (currentReadLine == null)
            {
                return ;
            }
			
            System.DateTime time ;
            commaPosition = currentReadLine.IndexOf(",") ;
            if (commaPosition < 0)
            {
                return ;
            }

            time = DateTime.Parse(currentReadLine.Substring(0, commaPosition),System.Globalization.DateTimeFormatInfo.InvariantInfo) ;
            if (currentReadTime == DateTime.MinValue) currentReadTime = time ;
            TimeSpan elapsedTime = time - currentReadTime ;
            sleepTime  = (long) (elapsedTime.Ticks / currentPlaySpeed) / 10000 ;
            currentReadTime = time ;
            if (playTimeChannel != null) playTimeChannel.Value = new KineticaRT.Values.DateTime(currentReadTime) ;
        }

        private void ReadThread()
        {
            try
            {
                try
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - ReadThread Start.", Name) ;

                    System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest ;

                    HeaderRead () ;

                    while (currentReadLine != null)
                    {
                        if (sleepTime < 0) sleepTime = 0 ;
                        System.Threading.Thread.Sleep ((int)sleepTime) ;

                        if (doStep)
                        {
                            doStep = false ;
                            stepChannel.Value = KineticaRT.Values.Values.False ;
                        }
                        else if (paused)
                        {
                            sleepTime = 333 ;
                            continue ;
                        }

                        lock (updateThread)
                        {
                            int last_position = commaPosition + 1 ;

                            //for (int index = 0 ; index < channelList.Count ; index ++)
                            foreach (ILoggerChannel list_channel in playList)
                            {
                                commaPosition = currentReadLine.IndexOf(",",last_position) ;
                                if (commaPosition == -1) commaPosition = currentReadLine.Length ;

                                if (commaPosition - last_position > 0)
                                {
                                    string value_text ;
                                    value_text = currentReadLine.Substring (last_position, commaPosition - last_position) ;

                                    if (value_text[0] == '\"')
                                    {
                                        if (value_text.StartsWith ("\"Comment:"))
                                        {
                                            System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "Playback: Skiping comment.") ;
                                        }
                                        else
                                        {
                                            list_channel.Value = new KineticaRT.Values.Text (value_text.Substring(1,value_text.Length-2)) ;
                                        }
                                    }
                                    else
                                    {
                                        double val = System.Double.Parse (value_text, System.Globalization.NumberFormatInfo.InvariantInfo) ;
                                        list_channel.Value = new KineticaRT.Values.Double (val) ;
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "Playback: Empty value") ;
                                }

                                last_position = commaPosition + 1 ;
                            }
                            //						for (int index = firstChannel ; index < channelList.Count ; index ++)
                            //						{
                            //							ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
                            //							if (list_channel.Index > 0)
                            //							{
                            //								commaPosition = currentReadLine.IndexOf(",",last_position) ;
                            //								if (commaPosition == -1) commaPosition = currentReadLine.Length ;
                            //
                            //								string value_text ;
                            //								value_text = currentReadLine.Substring (last_position, commaPosition - last_position) ;
                            //
                            //								if (value_text[0] == '\"')
                            //								{
                            //									list_channel.Value = new KineticaRT.Values.Text (value_text.Substring(1,value_text.Length-2)) ;
                            //								}
                            //								else
                            //								{
                            //									list_channel.Value = new KineticaRT.Values.Double (value_text) ;
                            //								}
                            //
                            //								last_position = commaPosition + 1 ;
                            //							}
                            //						}

                            ReadLine () ;
                        }

                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - ReadThread Update.", Name) ;
                    }
                }
                catch(ThreadAbortException e)
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - ReadThread Abort. "+e.Message, Name) ;
                }
                catch(System.ArgumentOutOfRangeException e)
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - Invalid file format. "+e.Message, Name) ;
                }
                catch 
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "CsvFile - Reading: Unhandled Exception (2).", Name) ;
                }
                finally
                {
                    lock (this)  // Access to updateThread.
                    {
                        updateThread = null ;
                    }

                    if (reader != null)
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - Close reader.", Name) ;
                        reader.Close () ;
                        reader = null ;
                    }

                    playList.Clear() ;
                    currentMode = Mode.Stop ;

                    //stopChannel.Value = KineticaRT.Values.Values.True ;

                    SetOutputs () ;
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - Read Thread Stop.", Name) ;
                }
            }
            catch 
            {
                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "CsvFile - Reading: Unhandled Exception.", Name) ;
            }
        }

        private void FindFirstChannel ()
        {
            if (channelList.Count > 0)
            {
                int index ;
                for (index = channelList.Count - 1 ; index >= 0 ; index --)
                {
                    ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
                    if (list_channel.Index <= 0)
                    {
                        //index ++ ;
                        break ;
                    }
                }

                firstChannel = index + 1 ;
            }
            else
            {
                firstChannel = 0 ;
            }
        }

        private void HeaderWrite ()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder () ;

            for (int index = 0 ; index < firstChannel ; index ++)
            {
                ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
                if ((list_channel.Index < 0) && list_channel.Enabled)
                {
                    if (list_channel.Description != null)
                    {
                        sb.Append ("\"") ;
                        sb.Append (list_channel.Description.ToString ()) ;
                        sb.Append ("\"") ;
                    }
                    sb.Append (",") ;
                    if (list_channel.Value != null)
                    {
                        if (list_channel.Value is KineticaRT.Values.Text)
                        {
                            sb.Append ("\"") ;
                            sb.Append (list_channel.Value.ToString ()) ;
                            sb.Append ("\"") ;
                        }
                        else
                        {
                            sb.Append (list_channel.Value.ToString ()) ;
                        }
                    }
                    for (int comma = 1 ; comma < (channelList.Count - firstChannel) ; comma ++)
                    {
                        sb.Append (",") ;
                    }
                    sb.Append ("\r\n") ;
                }
            }
						
            sb.Append ("Date Time") ;

            if (firstChannel < channelList.Count)
            {
                for (int index = firstChannel ; index < channelList.Count ; index ++)
                {
                    ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
                    if (list_channel.Enabled)
                    {
                        sb.Append (",") ;
                        if (list_channel.Description != null) 
                        {
                            sb.Append ("\"") ;
                            sb.Append (list_channel.Description.ToString ()) ;
                            sb.Append ("\"") ;
                        }
                    }
                }
            }
			
            writer.WriteLine (sb.ToString ()) ;
            writer.Flush () ;
        }

        private void CloseFileRead ()
        {
            System.Threading.Thread thread = updateThread ;
            if (thread != null)
            {
                lock (thread)
                {
                    thread.Abort () ;
                }

                thread.Join() ;
            }

            if (reader != null)
            {
                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - Close reader.", Name) ;
                reader.Close () ;
                reader = null ;
            }
        }
        private void CloseFileWrite ()
        {
            System.Threading.Thread thread = updateThread ;
            if (thread != null)
            {
                lock (thread)
                {
                    thread.Abort () ;
                }

                thread.Join() ;
            }
        }

        private void WriteThread()
        {
            try
            {
                try
                {
                    int next = System.Environment.TickCount ;
                    int period = (int)(1.0 / this.currentRecordSpeed * 1000.0) ;
                    bool stop = false ;

                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - WriteThread Start.", Name) ;

                    System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest ;

                    while (!stop)
                    {
                        if (!paused || doStep)
                        {
                            if (doStep)
                            {
                                doStep = false ;
                                stepChannel.Value = KineticaRT.Values.Values.False ;
                            }

                            System.DateTime time = System.DateTime.Now ;

                            lock (updateThread)
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder () ;
                                sb.Append (System.DateTime.Now.ToString(System.Globalization.DateTimeFormatInfo.InvariantInfo)) ;
                                //sb.Append (System.DateTime.Now.ToShortDateString ()) ;
                                //sb.Append (" ") ;
                                //sb.Append (System.DateTime.Now.ToLongTimeString ()) ;
                                sb.Append (".") ;
                                sb.Append (System.DateTime.Now.Millisecond.ToString("000",System.Globalization.NumberFormatInfo.InvariantInfo)) ;

                                if (firstChannel < channelList.Count)
                                {
                                    for (int index = firstChannel ; index < channelList.Count ; index ++)
                                    {
                                        ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;

                                        if (list_channel.Enabled)
                                        {
                                            sb.Append (",") ;
                                            if (list_channel.Value is KineticaRT.Values.Text)
                                            {
                                                sb.Append ("\"") ;
                                                sb.Append (list_channel.Value.ToString ()) ;
                                                sb.Append ("\"") ;
                                            }
                                            else if (list_channel.Value is KineticaRT.Values.Double)
                                            {
                                                double val = (list_channel.Value as KineticaRT.Values.Double).ToDouble () ;
                                                sb.Append (val.ToString (System.Globalization.NumberFormatInfo.InvariantInfo)) ;
                                            }
                                            else
                                            {
                                                sb.Append (list_channel.Value.ToString ()) ;
                                            }
                                        }
                                    }
                                }
			
                                writer.WriteLine (sb.ToString ()) ;
                                writer.Flush () ;

                                if ((maxFileSize != 0) && (writer.BaseStream.Position / 1000000 >= maxFileSize))
                                {
                                    stop = true ;
                                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "CsvFile - Write terminated due to reaching MaxFileSize.", Name) ;
                                }
                            }

                            if (playTimeChannel != null) playTimeChannel.Value = new KineticaRT.Values.DateTime(time) ;
                        }

                        // Wait for next update.
                        int delay = (next - System.Environment.TickCount) ;
				
                        if (delay > 2)
                        {
                            Thread.Sleep (delay);
                            next += period ;
                        }
                        else if (delay > -10)
                        {
                            //Thread.Sleep (delay);
                            next += period ;
                        }
                        else
                        {
                            Thread.Sleep (1);
                            next = System.Environment.TickCount ;
                            next += period ;
                        }

                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - WriteThread Update.", Name) ;
                    }
                }
                catch(ThreadAbortException)
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - WriteThread Abort.", Name) ;
                }
                catch 
                {
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "CsvFile - Write Thread: Unhandled Exception (2).", Name) ;
                }
                finally
                {
                    lock (this)  // Access to updateThread.
                    {
                        updateThread = null ;
                    }

                    currentMode = Mode.Stop ;

                    SetOutputs () ;
                    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - WriteThread Stop.", Name) ;

                    if (writer != null)
                    {
                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - Close writer.", Name) ;
                        writer.Close () ;
                        writer = null ;
                    }
                }
            }
            catch 
            {
                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "CsvFile - Write Thread: Unhandled Exception.", Name) ;
            }
        }

        /// <summary>
        /// Adds a channel to the channel list for recording / playback.
        /// </summary>
        /// <param name="channel">The channel to add.</param>
        public override void AddChannel (ILoggerChannel channel)
        {
            if (channel == null) throw new ArgumentNullException("channel") ;
			
            EnsureStopped () ;

            int index ; 
			
            for (index = 0 ; index < channelList.Count ; index ++)
            {
                ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
                if (list_channel.Index >= channel.Index)
                {
                    break ;
                }
            }

            channelList.Insert (index, channel) ;
        }

        /// <summary>
        /// Removes a channel from the list of channels for recording / playback
        /// </summary>
        /// <param name="channel">The channel to remove.</param>
        public override void RemoveChannel (ILoggerChannel channel)
        {
            EnsureStopped () ;
		
            channelList.Remove (channel) ;
        }

        private void EnsureStopped ()
        {
            if (currentMode != Mode.Stop)
            {
                System.Threading.Thread thread = updateThread ;
                if (thread != null)
                {
                    lock (thread)
                    {
                        thread.Abort () ;
                    }

                    thread.Join() ;
                }

                if (playList != null)
                {
                    playList.Clear() ;
                }

                if (stopChannel != null) 
                {
                    stopChannel.Value = KineticaRT.Values.Values.True ;
                }
            }

            if (doStep)
            {
                doStep = false ;
                stepChannel.Value = KineticaRT.Values.Values.False ;
            }
//			if (paused)
//			{
//				paused = false ;
//				pauseChannel.Value = KineticaRT.Values.Values.False ;
//			}
        }

        private void SetOutputs ()
        {
            if (recordChannel != null)
            {
                recordChannel.Value = currentMode == Mode.Record ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;
            }
            else if (!QuotedString(record))
            {
                record = currentMode == Mode.Record ? "True" : "False" ; OnPropertyChanged("Record") ;
            }

            if (playChannel != null)
            {
                playChannel.Value = currentMode == Mode.Play ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;
            }
            else if (!QuotedString(play))
            {
                play = currentMode == Mode.Play ? "True" : "False" ; OnPropertyChanged("Play") ;
            }
			
            if (pauseChannel != null)
            {
                pauseChannel.Value = paused ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;
            }
            else if (!QuotedString(pause))
            {
                pause = paused ? "True" : "False" ; OnPropertyChanged("Pause") ;
            }
			
            if (stopChannel != null)
            {
                stopChannel.Value = currentMode == Mode.Stop ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;
            }
            else if (!QuotedString(stop))
            {
                stop = currentMode == Mode.Stop ? "True" : "False" ; OnPropertyChanged("Stop") ;
            }

            switch (currentMode)
            {
                case Mode.Play :
                    this.Value = new KineticaRT.Values.Text (paused ? "Paused" : "Playing") ;
                    break ;
                case Mode.Record :
                    this.Value = new KineticaRT.Values.Text (paused ? "Paused" : "Recording") ;
                    break ;
                case Mode.Stop :
                    this.Value = new KineticaRT.Values.Text ("Stopped") ;
                    break ;
            }
        }

        static private bool QuotedString (string str)
        {
            if (str == null) return false ;
            if (str.Length < 2) return false ;
            if (str[0] != '"') return false ;
            if (str[str.Length-1] != '"') return false ;
            return true ;
        }

        static private string UnQuotedString (string str)
        {
            return str.Substring(1, str.Length-2) ;
        }
    }
}
//			for (int index = 0 ; index < firstChannel ; index ++)
//			{
//				ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
//				if (list_channel.Index < 0)
//				{
//					string value ;
//					int start_index = currentReadLine.IndexOf (",") + 1 ;
//					if (start_index != 0)
//					{
//						int end_index ;
//
//						if (currentReadLine[start_index] == '\"')
//						{
//							end_index = currentReadLine.IndexOf ("\"", start_index+1) ;
//							if (end_index != -1)
//							{
//								end_index = currentReadLine.IndexOf (",", end_index) ;
//							}
//						}
//						else
//						{
//							end_index = currentReadLine.IndexOf (",", start_index) ;
//						}
//						if (end_index == -1)
//						{
//							end_index = currentReadLine.Length ;
//						}
//
//						value = currentReadLine.Substring (start_index, end_index - start_index) ;
//					}
//					else
//					{
//						value = "Error: Invalid Format" ;
//					}
//
//					if (value[0] == '\"')
//					{
//						list_channel.Value = new KineticaRT.Values.Text (value.Substring(1,value.Length-2)) ;
//					}
//					else
//					{
//						list_channel.Value = new KineticaRT.Values.Double (value) ;
//					}
//
//					currentReadLine = reader.ReadLine () ;
//				}
//			}

//		private void HeaderRead ()
//		{
//			currentReadLine = reader.ReadLine () ;
//
//			for (int index = 0 ; index < firstChannel ; index ++)
//			{
//				ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
//				if (list_channel.Index < 0)
//				{
//					string value ;
//					int start_index = currentReadLine.IndexOf (",") + 1 ;
//					if (start_index != 0)
//					{
//						int end_index ;
//
//						if (currentReadLine[start_index] == '\"')
//						{
//							end_index = currentReadLine.IndexOf ("\"", start_index+1) ;
//							if (end_index != -1)
//							{
//								end_index = currentReadLine.IndexOf (",", end_index) ;
//							}
//						}
//						else
//						{
//							end_index = currentReadLine.IndexOf (",", start_index) ;
//						}
//						if (end_index == -1)
//						{
//							end_index = currentReadLine.Length ;
//						}
//
//						value = currentReadLine.Substring (start_index, end_index - start_index) ;
//					}
//					else
//					{
//						value = "Error: Invalid Format" ;
//					}
//
//					if (value[0] == '\"')
//					{
//						list_channel.Value = new KineticaRT.Values.Text (value.Substring(1,value.Length-2)) ;
//					}
//					else
//					{
//						list_channel.Value = new KineticaRT.Values.Double (value) ;
//					}
//
//					currentReadLine = reader.ReadLine () ;
//				}
//			}
//
//			startReadTime = DateTime.Now ;
//			currentReadTime = DateTime.MinValue ;
//			if (playTimeChannel != null) playTimeChannel.Value = new KineticaRT.Values.DateTime(currentReadTime) ;
//			ReadLine () ;
//		}
//
#endif

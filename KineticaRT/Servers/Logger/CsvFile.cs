using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using System.Threading ;
using System.Diagnostics ;
using System.Security.Permissions;
using KineticaRT.Channels ;
using System.IO;
using System.Linq;

namespace KineticaRT.Servers.Logging
{
	/// <summary>
	/// Records data to a CSV format file and replays it.
	/// </summary>
    [ChangeRecord("1.2.0.4", "26 10 2006", "N. Savidge", "Added Pre and Post logging with Trigger")]
    [ChangeRecord("1.2.0.0", "10 03 2005", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2005-2006 KineticaRT Ltd.")]

    [Description("CSV File logger")]
	[Serializable]
	[DefaultProperty("FileName")]
	//[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
	public class CsvFile : Logger, ISerializable, IConnectable, IDisposable 
	{
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Logger", "Logger Messages");

		private string		fileName ;
		private string		recordSpeed ;
		private Int64		maxFileSize ;
		private string		record ;
		private string		play ;
        private string      playReverse; //ft
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
        private string      archiverFileFormatVersion = "v1.0"; //ft
		private int			commaPosition ;
		private System.Threading.Thread updateThread ;
		private System.Collections.ArrayList channelList ;
		private System.Collections.ArrayList playList ;
        private System.Collections.ArrayList playListInitialValues; //ft
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
        private KineticaRT.Channels.IChannel                    playReverseChannel;//ft
        private KineticaRT.Channels.ValueChangedEventHandler    playReverseChannelValueChangedHandler;//ft
        private KineticaRT.Channels.IChannel                    pauseChannel;
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
#if ORIGINAL_CEL_ARCHIVER
        private System.IO.StreamReader reader ;
#else
        private string[] reader;
        private int lineIndex;
#endif
        private PlayingMode currentPlayingMode ;
        private Mode	currentMode ;
		private bool	paused ;
		private bool	doStep ;
		private string  recordList = "" ;
		private KineticaRT.Channels.IChannel [] recordChannels ;
		ObjectSearchEventHandler channelObjectSearchHandler ;

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
            playReverse = "False";
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
            playReverseChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(playReverseChannel_ValueChanged);
			pauseChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(pauseChannel_ValueChanged);
			commentChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(commentChannel_ValueChanged);
			stepChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(stepChannel_ValueChanged);
			stopChannelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(stopChannel_ValueChanged);
			channelObjectSearchHandler = new ObjectSearchEventHandler(OnChannelObjectSearch); 

			channelList = new System.Collections.ArrayList () ;
			playList = new System.Collections.ArrayList () ;
            playListInitialValues = new System.Collections.ArrayList();
			time = DateTime.MinValue ;
			RawValue= new KineticaRT.Values.Text ("Initialised") ;

            queue = new System.Collections.Queue();
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
					channelObjectSearchHandler = null ; 

					if (writer != null)
					{
						writer.Close () ;
						writer = null ;
					}
#if ORIGINAL_CEL_ARCHIVER
					if (reader != null)
					{
						reader.Close () ;
						reader = null ;
					}
#else
                    reader = null;
#endif
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
            info.AddValue("PlayReverse",    playReverse);
			info.AddValue ("Pause",			pause	) ;
			info.AddValue ("Step",			step	) ;
			info.AddValue ("Stop",			stop	) ;
			info.AddValue ("Time",			playTime) ;
			info.AddValue ("Comment",		comment	) ;
            info.AddValue("RecordList", recordList);
            info.AddValue("PreTriggerPeriod", preTriggerPeriod);
            info.AddValue("PostTriggerPeriod", postTriggerPeriod);
            info.AddValue("Triggered", triggered);
            info.AddValue("LatchedTrigger", latchedTrigger);
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
				fileName	        = info.GetString ("FileName") ;
				recordSpeed	        = info.GetString ("RecordSpeed") ;
				playSpeed	        = info.GetString ("PlaySpeed") ;
				maxFileSize	        = info.GetInt64	 ("MaxFileSize") ;
				record		        = info.GetString ("Record") ;
				play		        = info.GetString ("Play") ;
                playReverse         = info.GetString("PlayReverse");
				pause		        = info.GetString ("Pause") ;
				step		        = info.GetString ("Step") ;
				stop		        = info.GetString ("Stop") ;
				playTime	        = info.GetString ("Time") ;
				comment		        = info.GetString ("Comment") ;
                recordList          = info.GetString("RecordList");
                postTriggerPeriod   = info.GetInt32 ("PostTriggerPeriod");
                preTriggerPeriod    = info.GetInt32 ("PreTriggerPeriod");
                triggered           = info.GetBoolean ("Triggered");
                latchedTrigger      = info.GetBoolean ("LatchedTrigger");
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
			}
		}
		#endregion
		#region Properties

        /// <summary>
        /// Gets or sets if the trigger will stay set or release itself (return to false) automatically after being set to true.
        /// </summary>
        /// <value>If true then the trigger will stay set to true until manualy set back to false.</value>
        [System.ComponentModel.Description("If true then when Trigger is set it will remain set until cleared, otherwise it will release."), System.ComponentModel.Category("Logger")]
        [DefaultValue(true)]
        public Boolean LatchedTrigger
        {
            get
            {
                return latchedTrigger;
            }
            set
            {
                if (latchedTrigger != value)
                {
                    latchedTrigger = value;
                    OnPropertyChanged("LatchedTrigger");
                }
            }
        }
        private Boolean latchedTrigger = true;

        /// <summary>
        /// Period of time before the trigger is set for which to record data.
        /// </summary>
        /// <value>Int32 giving the time period in seconds.</value>
        [System.ComponentModel.Description("Period to record from before the start of the trigger period."), System.ComponentModel.Category("Logger")]
        [DefaultValue(0)]
        public Int32 PreTriggerPeriod
        {
            get
            {
                return preTriggerPeriod;
            }
            set
            {
                if (preTriggerPeriod != value)
                {
                    preTriggerPeriod = value;
                    OnPropertyChanged("PreTriggerPeriod");
                }
            }
        }
        private Int32 preTriggerPeriod ;

        /// <summary>
        /// Period of time after the trigger is cleared for which to record data.
        /// </summary>
        /// <value>Int32 giving the time period in seconds.</value>
        [System.ComponentModel.Description("Period to record after the end of trigger period."), System.ComponentModel.Category("Logger")]
        [DefaultValue(0)]
        public Int32 PostTriggerPeriod
        {
            get
            {
                return postTriggerPeriod;
            }
            set
            {
                if (postTriggerPeriod != value)
                {
                    postTriggerPeriod = value;
                    OnPropertyChanged("PostTriggerPeriod");
                }
            }
        }
        private Int32 postTriggerPeriod ;

		/// <summary>
		/// Gets or sets if the recording has been triggered.  Recording will only take place when in the record state and the Triggered is true or during the pre and post trigger periods.
		/// </summary>
		/// <value>Boolean value, if true then recording will occur and if false no recording will take place except in the pre and post trigger periods.</value>
        [System.ComponentModel.Description("Recording will take place while this property is true and during the pre and post trigger periods."), System.ComponentModel.Category("Logger")]
        [DefaultValue(true)]
        public bool Triggered
        {
            get
            {
                // return triggered;
                return triggerState != TriggerState.PreTrigger;
            }
            set
            {
                if (triggered != value)
                {
                    triggered = value;
                    OnPropertyChanged("Triggered");
                }
            }
        }
        private bool triggered = true;

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
                        playReverseChannel_ValueChanged(this, KineticaRT.Values.Values.False);//ft
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
        public string PlayReverse //ft
        {
            get
            {
                return playReverse;
            }
            set
            {
                if (playReverseChannel != null)//ft
                {
                    playReverseChannel.ValueChanged -= playReverseChannelValueChangedHandler;
                    playReverseChannel = null;
                }
                if (value != null && value.Length > 0)//ft
                {
                    playReverse = value;
                    OnPropertyChanged("PlayReverse");
                    playReverseChannel = OnObjectSearch(playReverse) as KineticaRT.Channels.IChannel;
                    if (playReverseChannel == null)
                    {
                        try
                        {
                            bool new_val = System.Convert.ToBoolean(value, System.Globalization.NumberFormatInfo.CurrentInfo);
                            if (new_val == true)
                            {
                                playChannel_ValueChanged(this, KineticaRT.Values.Values.False);
                                playReverseChannel_ValueChanged(this, KineticaRT.Values.Values.True);//ft
                            }
                        }
                        catch (System.FormatException)
                        {
                        }
                    }
                    else
                    {
                        playReverseChannel.ValueChanged += playReverseChannelValueChangedHandler;
                    }
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
		/// <summary>
		/// Names of channels to be recorded.
		/// </summary>
		/// <value>
		/// A string giving the names of the channels to be recorded.
		/// </value>
		/// <remarks>Seperate channel names by commas (","). They will be recorded in the same order they appear in the string.</remarks>
		[System.ComponentModel.Description("Names of channels to be recorded. Use comma ',' to seperate names."), System.ComponentModel.Category("Logger")] 
		[DefaultValue("")]
		[Browsable(false)]
		public string RecordList
		{
			get 
			{
				return recordList ;
			}
			set 
			{
				if (recordList != value)
				{
					recordList = value ;
					OnPropertyChanged("RecordList") ;
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

				// Connect recordChannels.
				if (record.Length > 0)
				{
					recordChannel = OnObjectSearch(record) as KineticaRT.Channels.IChannel ;
					if (recordChannel == null)
					{
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".RecordChannel: Channel not found-" + record, "Connect") ;
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
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".PlayChannel: Channel not found-" + play, "Connect") ;
					}
					else
					{
						playChannel.ValueChanged += playChannelValueChangedHandler;
					}
				}
                if (playReverseChannel != null)//ft
                {
                    playReverseChannel.ValueChanged -= playReverseChannelValueChangedHandler;
                    playReverseChannel = null;
                }
                if (playReverse != null && playReverse.Length > 0)//ft
                {
                    playReverseChannel = OnObjectSearch(playReverse) as KineticaRT.Channels.IChannel;
                    if (playReverseChannel == null)
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, Name + ".PlayReveseChannel: Channel not found-" + playReverse, "Connect");
                    }
                    else
                    {
                        playReverseChannel.ValueChanged += playReverseChannelValueChangedHandler;
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
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".PauseChannel: Channel not found-" + pause, "Connect") ;
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
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".CommentChannel: Channel not found-" + comment, "Connect") ;
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
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".StepChannel: Channel not found-" + step, "Connect") ;
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
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".StopChannel: Channel not found-" + stop, "Connect") ;
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
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".FileNameChannel: Channel not found-" + fileName, "Connect") ;
					}
				}

				playTimeChannel = null ;
				if (!QuotedString(playTime))
				{
					playTimeChannel = OnObjectSearch(playTime) as KineticaRT.Channels.IChannel ;
					if (playTimeChannel == null) 
					{
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ".TimeChannel : channel not found-" + playTime, "Connect") ;
					}
				}

				if (stopChannel != null) 
				{
					stopChannel.Value = KineticaRT.Values.Values.True ;
				}

				if (this.recordList.Length > 0)
				{
					string [] names = recordList.Split(new char[]{','}) ;
					recordChannels = new KineticaRT.Channels.IChannel [names.Length] ;
					int index = 0 ;
					foreach (string name in names)
					{
						string trimmed_name = name.Trim() ;
						KineticaRT.Channels.IChannel channel = OnObjectSearch(trimmed_name) as KineticaRT.Channels.IChannel ;
						if (channel != null)
						{
							INumericThresholdChannel chan = channel as INumericThresholdChannel ;
							if (chan != null)
							{
								ILoggerChannel l_channel = new Logging.NumericThresholdChannel() ;
								l_channel.Source = trimmed_name ;
								l_channel.Logger = Name ;
								l_channel.ObjectSearch += channelObjectSearchHandler ; 

								recordChannels [index++] = l_channel ;
								l_channel.Connect () ;
							}
						}

					}
				}
//
//
//IBooleanChannel
//	BooleanChannel
//
//INumericChannel
//	NumericChannel
//
//	TextChannel
//		InfoChannel
//
//INumericThresholdChannel
//	NumericThresholdChannel
//
//	InfoChannel



			}
		}

		private object OnChannelObjectSearch (string objectName)
		{
			if (objectName == Name) return this ;
			return this.OnObjectSearch (objectName) ;
		}
		
		/// <summary>
		/// Returns the named child object or null if it does not exist.
		/// </summary>
		public override object FindObject (string objectName)
		{
			return null ;
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
					if (recordChannels != null)
					{
						// Disconnect recordChannels.
						foreach (ILoggerChannel channel in recordChannels)
						{
							if (channel != null)
							{
								channel.Disconnect () ;
								//channel = null ;
							}
						}
					}

					//recordChannels = null ;











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
                    playReverseChannel = null;
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
                    currentPlayingMode = PlayingMode.Forward;
                    OpenFileRead();
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
        private void playReverseChannel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)//ft
        {
            if (currentMode == Mode.Play)
            {
                lock (updateThread)
                {
                    if (newVal == KineticaRT.Values.Values.True)
                    {
                        currentPlayingMode = PlayingMode.Backward;
                    }
                    else
                    {
                        currentPlayingMode = PlayingMode.Forward;
                    }
                }
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
            if (double.IsNaN(difference) || difference > 0.01)
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

                    AddToQueue(sb.ToString(), triggered || triggerState == TriggerState.PostTrigger);
                    //writer.WriteLine(sb.ToString());
					//writer.Flush () ;
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
				SetOutputs () ;//ft TODO bug possible de loop infinie pcq fonction réentrante du stopChannel_ValueChanged!
				
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
#if ORIGINAL_CEL_ARCHIVER
				reader = new System.IO.StreamReader (filepath, true) ;
#else
                reader = File.ReadAllLines(filepath);
#endif
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
				writer = new System.IO.StreamWriter (filepath, false, System.Text.Encoding.UTF8) ;

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

		private ILoggerChannel FindChannelByChannelDescription (string name)
		{
			ILoggerChannel list_channel ;

			for (int index = 0 ; index < channelList.Count ; index ++)
			{
				list_channel = (ILoggerChannel) channelList [index] ;
                if (list_channel.Enabled && list_channel.Description == name)
				{
					return list_channel ;
				}
			}
		
			return null ;
		}
        private ILoggerChannel FindChannelByChannelName(string name)
        {
            ILoggerChannel list_channel;

            for (int index = 0; index < channelList.Count; index++)
            {
                list_channel = (ILoggerChannel)channelList[index];
                if (list_channel.Enabled && list_channel.Name == name)
                {
                    return list_channel;
                }
            }

            return null;
        }

        private void HeaderRead ()
		{
#if ORIGINAL_CEL_ARCHIVER
			currentReadLine = reader.ReadLine () ;
			if (currentReadLine == null) return ;

            archiverFileFormatVersion = "v1.0";//ft default version
            if (currentReadLine == "Archiver v2.0")
            {
                archiverFileFormatVersion = "v2.0";
                currentReadLine = reader.ReadLine();
            }
#else
            lineIndex = 0;
            if (lineIndex < reader.GetUpperBound(0))
                currentReadLine = reader[lineIndex++];
            else
                currentReadLine = null;

            if (currentReadLine == null) return;

            archiverFileFormatVersion = "v1.0";//ft default version
            if (currentReadLine == "Archiver v2.0")
            {
                archiverFileFormatVersion = "v2.0";
                if (lineIndex < reader.GetUpperBound(0))
                    currentReadLine = reader[lineIndex++];
                else
                    currentReadLine = null;
            }
            if (currentReadLine == null) return;
#endif
			while (currentReadLine[0] == '"')
			{
				int name_end = currentReadLine.IndexOf ("\"",1) ;
				if (name_end > 0)
				{
					string name = currentReadLine.Substring(1, name_end - 1) ;

                    //ft TODO Version Info

                    ILoggerChannel list_channel =null;
                    if (archiverFileFormatVersion == "v1.0")
                    {
                        list_channel = FindChannelByChannelDescription(name);
                    }
                    else if (archiverFileFormatVersion == "v2.0")
                    {
                        list_channel = FindChannelByChannelName(name);
                    }
                    

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
#if ORIGINAL_CEL_ARCHIVER                     
					currentReadLine = reader.ReadLine () ;
#else
                    if (lineIndex < reader.GetUpperBound(0))
                        currentReadLine = reader[lineIndex++];
                    else
                        currentReadLine = null;
#endif
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

				if (text != "Date Time" && archiverFileFormatVersion == "v1.0")//ft
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
                        
						ILoggerChannel list_channel = null;
                        if (archiverFileFormatVersion == "v1.0")
                        {
                            list_channel = FindChannelByChannelDescription(text);
                        }
                        else if (archiverFileFormatVersion == "v2.0")
                        {
                            list_channel = FindChannelByChannelName(text);
                        }

						if (list_channel != null)
						{
							if (list_channel.Enabled)
							{
								playList.Add (list_channel) ; //ft ATTENTION playList et playListValues doivent correspondrent!!
                                playListInitialValues.Add(list_channel.Value);
							}
							else
							{
                                playList.Add(null);//ft pour eviter un dephasage entre les channels defini dans le header et chacune des valeur par ligne
                                playListInitialValues.Add(null);
								System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - Channel not enabled: " + text, Name) ;
							}
						}
						else
						{
                            playList.Add(null);//ft pour eviter un dephasage entre les channels defini dans le header et chacune des valeur par ligne
                            playListInitialValues.Add(null);
							System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "CsvFile - Channel not found: " + text, Name) ;
						}

						if (end_index == currentReadLine.Length) break ;
					}

					startReadTime = DateTime.Now ;
					currentReadTime = DateTime.MinValue ;
					if (playTimeChannel != null) playTimeChannel.Value = new KineticaRT.Values.DateTime(currentReadTime) ;

                    if (archiverFileFormatVersion == "v2.0")
                    {//ft Skip Line de Description et Units
#if ORIGINAL_CEL_ARCHIVER
                        currentReadLine = reader.ReadLine();
                        currentReadLine = reader.ReadLine();
#else
                        //ft Skip Line de Description et Units
                       currentReadLine = reader[lineIndex++];
                       currentReadLine = reader[lineIndex++];
#endif
                    }

					ReadLine () ;
				}
			}
		}

		private void ReadLine ()
        {
#if ORIGINAL_CEL_ARCHIVER
			currentReadLine = reader.ReadLine () ;

#else
            currentReadLine = null;
            
            if (currentPlayingMode == PlayingMode.Forward)
            {
                if (lineIndex < reader.GetUpperBound(0))
                    currentReadLine = reader[lineIndex++];
            }
            else
            {
                if (lineIndex > 3)//ne pas jouer les 3 premieres ligne de header
                    currentReadLine = reader[lineIndex--];
            }
            
#endif
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
            time = DateTime.Parse(currentReadLine.Substring(0, commaPosition), System.Globalization.DateTimeFormatInfo.InvariantInfo);
            if (currentReadTime == DateTime.MinValue) currentReadTime = time;
            TimeSpan elapsedTime = time - currentReadTime;
            sleepTime = Math.Abs((long)(elapsedTime.Ticks / currentPlaySpeed) / 10000);
            currentReadTime = time;
            if (playTimeChannel != null) playTimeChannel.Value = new KineticaRT.Values.DateTime(currentReadTime);
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

                                if(list_channel != null ) //ft Bug pcq le FindChannel ne trouvait pas certains channels et creait un dephasage entre la liste des channel du header et la liste de chaque valeur par ligne
                                {
								    if (commaPosition - last_position > 0 )
								    {
									    string value_text ;

                                        if (currentReadLine[last_position] == '\"') //ft bug dans une string qui contenait une virgule
                                        {
                                            int quote_position = currentReadLine.IndexOf("\"",last_position+1) ;
                                            commaPosition = quote_position + 1;
                                            value_text = currentReadLine.Substring(last_position, commaPosition - last_position);
                                            
                                        }
                                        else
                                        {
                                            value_text = currentReadLine.Substring(last_position, commaPosition - last_position);
                                        }

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
									    else if (value_text == "True")
									    {
										    list_channel.Value = KineticaRT.Values.Values.True ;
									    }
									    else if (value_text == "False")
									    {
										    list_channel.Value = KineticaRT.Values.Values.False ;
									    }
									    else
									    {
										    try
										    {
											    double val = System.Double.Parse (value_text, System.Globalization.NumberFormatInfo.InvariantInfo) ;
											    list_channel.Value = new KineticaRT.Values.Double (val) ;
										    }
										    catch (System.FormatException)
										    {
											    list_channel.Value = KineticaRT.Values.Values.TypeMismatch ;
										    }
										    catch (System.OverflowException)
										    {
											    list_channel.Value = KineticaRT.Values.Values.OutOfRange ;
										    }
									    }
								    }
								    else
								    {
                                        System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "Playback: Empty value") ;
								    }

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

						//System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - ReadThread Update.", Name) ;
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
                    lock (updateThread)
                    {
                        RestoreInitialValues();
                    }
					lock (this)  // Access to updateThread.
					{
						updateThread = null ;
					}

					if (reader != null)
					{
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - Close reader.", Name) ;
#if ORIGINAL_CEL_ARCHIVER
						reader.Close () ;
#endif
						reader = null ;
					}
					currentMode = Mode.Stop ;

					//stopChannel.Value = KineticaRT.Values.Values.True ;

                    SetOutputs();

					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - Read Thread Stop.", Name) ;
				}
			}
			catch 
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "CsvFile - Reading: Unhandled Exception.", Name) ;
			}
		}
       
        private enum PlayingMode
        {
            Forward = 0,
            Backward = 1
        }
        private void RestoreInitialValues()
        {
            if (playList != null) //ft Remet les channel dans l'état initial avant le playback
            {
                if (playListInitialValues != null)
                {
                    for (int i = 0; i < playList.Count; i++)
                    {
                        if (playList[i] != null && playListInitialValues[i] != null)
                        {
                            ILoggerChannel list_channel = (ILoggerChannel)playList[i];
                            KineticaRT.Values.Value initialValue = (KineticaRT.Values.Value)playListInitialValues[i];
                            list_channel.Value = initialValue;
                        }
                    }
                    playListInitialValues.Clear();//ft
                }
                playList.Clear();
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
			System.Text.StringBuilder sbLine1 = new System.Text.StringBuilder(); //ft Line1 For Channel Name
            System.Text.StringBuilder sbLine2 = new System.Text.StringBuilder(); //ft Line2 For Channel Description
            System.Text.StringBuilder sbLine3 = new System.Text.StringBuilder(); //ft Line3 For Channel Engineering Unit

			for (int index = 0 ; index < firstChannel ; index ++)
			{
				ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
				if ((list_channel.Index < 0) && list_channel.Enabled)
				{
                   
                    sbLine1.Append("\"");
                    sbLine1.Append(list_channel.Name.ToString());
                    sbLine1.Append("\"");

                    if (list_channel.Description != null)
                    {
                        sbLine2.Append("\"");
                        sbLine2.Append(list_channel.Description.ToString());
                        sbLine2.Append("\"");
                    }
                    else
                    {
                        sbLine2.Append("\"---\"");
                    }
                    INumericChannel num_channel = (INumericChannel)list_channel;
                    if (num_channel == null)
                    {
                        sbLine3.Append("\"---\"");
                       
                    }
                    else if (num_channel.Units == null)
                    {
                        sbLine3.Append("\"---\"");
                    }
                    else
                    {
                        sbLine3.Append("\"");
                        sbLine3.Append(num_channel.Units.ToString());
                        sbLine3.Append("\"");
                    }
                   
					sbLine1.Append(",");
                    sbLine2.Append(",");
                    sbLine3.Append(",");

					if (list_channel.Value != null)
					{
						if (list_channel.Value is KineticaRT.Values.Text)
						{
							sbLine1.Append ("\"") ;
							sbLine1.Append (list_channel.Value.ToString ()) ;
							sbLine1.Append ("\"") ;
						}
						else
						{
							sbLine1.Append (list_channel.Value.ToString ()) ;
						}
					}
					for (int comma = 1 ; comma < (channelList.Count - firstChannel) ; comma ++)
					{
						sbLine1.Append (",") ;
					}
					sbLine1.Append("\r\n") ;
                    sbLine2.Append("\r\n");
                    sbLine3.Append("\r\n");
				}
			}
						
			sbLine1.Append("Channel Name:");
            sbLine2.Append("Description:");
            sbLine3.Append("Time");

			if (firstChannel < channelList.Count)
			{
				for (int index = firstChannel ; index < channelList.Count ; index ++)
				{
					ILoggerChannel list_channel = (ILoggerChannel) channelList [index] ;
					if (list_channel.Enabled)
					{
                        sbLine1.Append(",");
                        sbLine2.Append(",");
                        sbLine3.Append(",");

                        //ft Channel Name
                        sbLine1.Append("\"");
                        sbLine1.Append(list_channel.Name.ToString());
                        sbLine1.Append("\"");
                        
                        //ft Channel Description
                        if (list_channel.Description != null)
                        {
                            sbLine2.Append("\"");
                            sbLine2.Append(list_channel.Description.ToString());
                            sbLine2.Append("\"");
                        }
                        else
                        {
                            sbLine2.Append("\"---\"");
                        }
                       
                        //ft Unit
                        if (list_channel is INumericChannel)
                        {
                            INumericChannel num_channel = (INumericChannel)list_channel;
                            if (num_channel == null)
                            {
                                sbLine3.Append("\"---\"");
                            }
                            else if (num_channel.Units == null)
                            {
                                sbLine3.Append("\"---\"");
                            }
                            else
                            {
                                sbLine3.Append("\"");
                                sbLine3.Append(num_channel.Units.ToString());
                                sbLine3.Append("\"");
                            }
                        }
                        else if (list_channel is ITextChannel)
                        {
                            sbLine3.Append("\"---\"");
                        }
                        else if (list_channel is IBooleanChannel)
                        {
                            sbLine3.Append("\"---\"");
                        }
					}
				}
			}
            writer.WriteLine("Archiver v2.0");
			writer.WriteLine(sbLine1.ToString());
            writer.WriteLine(sbLine2.ToString());
            writer.WriteLine(sbLine3.ToString());
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
#if ORIGINAL_CEL_ARCHIVER
				reader.Close () ;
#endif
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
			//try
			{
				try
				{
					int next = System.Environment.TickCount ;
					int period = (int)(1.0 / this.currentRecordSpeed * 1000.0) ;
					bool stop = false ;
                    triggerState = TriggerState.PreTrigger;

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
                                // What state are we in?

                                switch (triggerState)
                                {
                                    case TriggerState.PreTrigger :
                                        {
                                            if (triggered)
                                            {
                                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Trigger", Name);

                                                ShrinkQueue();
                                                WriteQueue();
                                                triggerState = TriggerState.Triggered;
                                                OnPropertyChanged("Triggered");
                                            }
                                            else
                                            {
                                                ShrinkQueue();
                                            }
                                        }
                                        break;
                                    case TriggerState.Triggered :
                                        {
                                            WriteQueue();

                                            if (!triggered)
                                            {
                                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "PostTrigger", Name);

                                                triggerState = TriggerState.PostTrigger;
                                                postTriggerEndTime = DateTime.Now + new TimeSpan(0, 0, postTriggerPeriod);
                                            }
                                            else if (!latchedTrigger)
                                            {
                                                triggered = false;
                                            }
                                        }
                                        break;
                                    case TriggerState.PostTrigger:
                                        {
                                            if (triggered)
                                            {
                                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Trigger", Name);

                                                WriteQueue();
                                                triggerState = TriggerState.Triggered;
                                            }
                                            else
                                            {
                                                if (DateTime.Now > postTriggerEndTime)
                                                {
                                                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "PreTrigger", Name);

                                                    WriteQueue();
                                                    triggerState = TriggerState.PreTrigger;
                                                    OnPropertyChanged("Triggered");
                                                }
                                            }
                                        }
                                        break;
                                }
                                
								System.Text.StringBuilder sb = new System.Text.StringBuilder () ;
								sb.Append (System.DateTime.Now.ToString("HH:mm:ss.fff", System.Globalization.DateTimeFormatInfo.InvariantInfo)) ;

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
                                                sb.Append("\"" + list_channel.Value.ToString() + "\"");
											}
											else if (list_channel.Value is KineticaRT.Values.Double)
											{
                                                KineticaRT.Values.Double ChannelValue = list_channel.Value as KineticaRT.Values.Double;
                                                if (ChannelValue != null)
                                                {
                                                    sb.Append(ChannelValue.ToDouble().ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
                                                }
                                                else // manage case where cast to double wasnt possible.
                                                {
                                                    sb.Append("\"" + "null" + "\"");
                                                }

											}
											else
											{
												sb.Append (list_channel.Value.ToString ()) ;
											}

                                            if (!triggered && (list_channel.LowTriggered || list_channel.HighTriggered))
                                            {
                                                triggered = true;
                                            }
										}
									}
								}

                                AddToQueue(sb.ToString(), triggered || triggerState == TriggerState.PostTrigger);
			
								//writer.WriteLine (sb.ToString ()) ;
								//writer.Flush () ;

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

						//System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - WriteThread Update.", Name) ;
					}
				}
				catch(ThreadAbortException)
				{
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "CsvFile - WriteThread Abort.", Name) ;
				}
                //catch 
                //{
                //    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "CsvFile - Write Thread: Unhandled Exception (2).", Name) ;
                //}
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

                        if (triggerState == TriggerState.PreTrigger)
                        {
                            EmptyQueue();
                        }
                        else
                        {
                            WriteQueue();
                        }

						writer.Close () ;
						writer = null ;
					}
				}
			}
//			catch 
//			{
//				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "CsvFile - Write Thread: Unhandled Exception.", Name) ;
//			}
		}

        class QueueItem
        {
            public DateTime time;
            public string text;
            public bool keep;
            
            public QueueItem(string text, bool keep)
            {
                time = DateTime.Now;
                this.text = text;
                this.keep = keep;
            }
        }

        System.Collections.Queue queue;

        enum TriggerState
        {
            PreTrigger,
            Triggered,
            PostTrigger,
        }

        TriggerState triggerState;
        DateTime postTriggerEndTime;

        private void AddToQueue(string text, bool keep)
        {
            //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "AddToQueue", Name);

            QueueItem item = new QueueItem(text, keep);
            queue.Enqueue(item);
        }

        private void WriteQueue()
        {
            while (queue.Count > 0)
            {
                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "WriteQueue", Name);

                QueueItem item;
                item = (QueueItem)queue.Dequeue();
                writer.WriteLine(item.text);
            }
        }

        private void EmptyQueue()
        {
            while (queue.Count > 0)
            {
                queue.Dequeue();
            }
        }

        private void ShrinkQueue()
        {
            if (queue.Count > 0)
            {
                DateTime too_old = DateTime.Now - new TimeSpan(0,0,preTriggerPeriod);
                QueueItem item;
                item = (QueueItem)queue.Peek ();
                while (item.time < too_old)
                {
                    //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "ShrinkQueue", Name);
                    queue.Dequeue();
                    if (queue.Count == 0) break;
                    item = (QueueItem)queue.Peek();
                }
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

                RestoreInitialValues();//ft
                
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
            if (playReverseChannel != null)//ft
            {
                playReverseChannel.Value = KineticaRT.Values.Values.False;
            }
            else if (!QuotedString(playReverse))
            {
                playReverse = "False"; OnPropertyChanged("PlayReverse");
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

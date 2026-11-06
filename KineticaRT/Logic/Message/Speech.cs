using System;
using System.Text ;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using SpeechLib;  //E:\temp>tlbimp "E:\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll" /sysarray /namespace:SpeechLib /out:Interop.SpeechLib.dll /keyfile:"e:\KineticaRT\KineticaRTKey.snk"
using System.Runtime.Serialization;

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// Speaks the Value of the Channel to which
	/// it is connected whenever that Value changes
	/// </summary>
    [Serializable]
    public class Speech : KineticaRT.Channels.TextChannel, ISerializable
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Logic.Message", "Message Messages");
        
        private Values.Value		value ;
		private string				source ;
		private Channels.IChannel	sourceChannel ;
		private bool				mute ;
		private string				range ;

		private SpVoiceClass						voice;
        private KineticaRT.Channels.ValueChangedEventHandler source_value_changed_handler; 

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the class
		/// </summary>
		public Speech ()
		{
            Initialise();
		}

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData (info, context) ;
		}
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("Range", range) ;
			info.AddValue ("Mute", mute) ;
            info.AddValue("VoiceName", voiceName);
            info.AddValue("Source", source);
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
        protected Speech(SerializationInfo info, StreamingContext context)
            : base(info, context)
		{
            Initialise();

			try	{range = info.GetString ("Range") ;}
			catch (System.Runtime.Serialization.SerializationException){}
			
			try	{mute = info.GetBoolean ("Mute") ;}
			catch (System.Runtime.Serialization.SerializationException){}

            try {voiceName = info.GetString("VoiceName"); }
			catch (System.Runtime.Serialization.SerializationException){}
        
            try { source = info.GetString("Source"); }
            catch (System.Runtime.Serialization.SerializationException) { }
        }
		#endregion

		private void Initialise()
		{
			value			= Values.Values.NotConnected ;
			source			= "" ;
			sourceChannel	= null ;
			mute			= false ;
			range			= "0-" ;

            source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);

			try
			{
                voice = new SpVoiceClass();
			}
			catch(Exception)
			{
				// This might fail if the user does not have
				// Speech Automation 5.1 Runtimes installed.
				System.Windows.Forms.MessageBox.Show("An error has occurred while trying to initialize SAPI 5.1.");
				voice = null ;
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the current Value
		/// </summary>
		/// <remarks>The text of a new value will be read out.</remarks>
		public override KineticaRT.Values.Value Value
		{
			get
			{
				return this.value ;
			}
			set
			{
				//if (this.value != value)
				{
					this.value = value ;

                    try
                    {
					    if (!mute && voice != null)
                    
					    {
					    	//speaking asynchronously automatically queues messages
					    	if (this.value is Message)
					    	{
					    		Message			msg		= this.value as Message ;
   
							    if (Priority.IsInRange(msg.Priority, range))
							    {
                                    //if (!string.IsNullOrEmpty(msg.Text)) voice.Speak(msg.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                                    string message = msg.ToString();
                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        try
                                        {
                                            voice.Speak(message, SpeechVoiceSpeakFlags.SVSFlagsAsync);
                                        }
                                        catch (System.Runtime.InteropServices.COMException e)
                                        {
                                            System.Diagnostics.Trace.WriteLine(e.Message, GetType().Name); 
                                        }
                                    }
							    }
							    //else ignore the message
						    }
						    else
						    {
							    voice.Speak(this.value.ToString(), SpeechVoiceSpeakFlags.SVSFlagsAsync) ; 
						    }
					    }

					    OnPropertyChanged("Value") ;
                    }

                    catch (System.Exception exception)
                    {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceError, exception.Message+": "+exception.StackTrace, System.Threading.Thread.CurrentThread.Name);
                    }

				}
			}
		}


		/// <summary>
		/// Gets or sets the name of the channel to be the object's Source
		/// </summary>
		[DefaultValue("")]
		[Description("Gets or sets the name of the channel to be the object's Source")]
		public string Source
		{
			get
			{
				return source ;
			}
			set
			{
				if (source != value)
				{
					source = value ;
					OnPropertyChanged("Source") ;
				}
			}
		}

        private string voiceName;
		/// <summary>
		/// Gets or sets the name of the Voice used to speak the Value
		/// </summary>
        [DefaultValue("")]
        [Description("The voice to be used.")]
        public string VoiceName
		{
			get
			{
                return voiceName;
			}
			set
			{
                if (voice != null && voiceName != value)
				{
                    voiceName = value;
				    OnPropertyChanged("VoiceName") ;

                    ConnectVoice();
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the voice is muted
		/// </summary>
		[DefaultValue(false)]
        [Description("Silences the voice.")]
        public bool Mute
		{
			get
			{
				return mute ;
			}
			set
			{
				if (mute != value)
				{
					mute = value ;

					if (mute && voice != null)
					{
						//stop all speaking immediately
						voice.Speak(null, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak) ;
					}
					OnPropertyChanged("Mute") ;
				}
			}
		}

		///<summary>
		///Gets or sets the string defining which Priority values are covered
		///<remarks>Strings are comma-separated inclusive ranges or single values</remarks>
		///</summary>
		///<example><ul>
		/// <li>"1-4,6,12" covers the numbers 1, 2, 3, 4, 6 and 12</li>
		/// <li>"0,6-" covers the numbers 0 and everything greater than or equal to 6</li>
		///</ul></example>
		[DefaultValue("0-")]
        [Description("Defines which Priority values are covered.  Eg \"1-4,6,12-\" covers the numbers 1, 2, 3, 4, 6 and 12 onwards")]
        public string Range
		{
			get
			{
				return range ;
			}
			set
			{
				if (range != value)
				{
					lock (this)
					{
						range = value ;
					}
					OnPropertyChanged("Range") ;
				}
			}
		}
		#endregion

		#region Events
		private void OnSourceChanged(Channels.IChannel source, Values.Value newValue)
		{
			Value = newValue ;
		}
		#endregion

		#region IConnectable

		/// <summary>
		/// Connects this object to others.
		/// </summary>
        public override void Connect()
        {
			base.Connect () ;

            lock (this)
            {
                if (sourceChannel == null) sourceChannel = OnObjectSearch(source) as KineticaRT.Channels.Channel;
                if (sourceChannel != null) sourceChannel.ValueChanged += source_value_changed_handler;
                ConnectVoice();
            }
		}

        private void ConnectVoice ()
        {
            if (!System.String.IsNullOrEmpty (voiceName))
            {
                //find the named voice (if possible)
				ISpeechObjectTokens voices = voice.GetVoices("", "");
				foreach(ISpeechObjectToken token in voices)
				{
                    string voice_description = token.GetDescription(0);
                    if (voice_description.Equals(voiceName))
					{
						voice.Voice = token as SpObjectToken ;
						//break ;
					}
				}
            }
        }

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
        public override void Disconnect()
        {
            base.Disconnect();

            if (sourceChannel != null)
            {
                sourceChannel.ValueChanged -= source_value_changed_handler;
                sourceChannel = null;
            }
		}
        
        private void Source_OnValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            this.Value = newVal;
        }
        #endregion
	}
}

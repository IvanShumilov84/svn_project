using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using SpeechLib;  //E:\temp>tlbimp "E:\Program Files\Common Files\Microsoft Shared\Speech\sapi.dll" /sysarray /namespace:SpeechLib /out:Interop.SpeechLib.dll /keyfile:"e:\KineticaRT\KineticaRTKey.snk"

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// A Component that speaks the Value of the Channel to which
	/// it is connected whenever that Value changes
	/// </summary>
	public class Voice : System.ComponentModel.Component, IConnectable, ISource
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Logic.Message", "Message Messages");
		private System.ComponentModel.Container components = null;

		private Values.Value		value ;
		private string				source ;
		private Channels.IChannel	sourceChannel ;
		private bool				mute ;
		private string				range ;

		private SpVoiceClass						voice;
		private Channels.ValueChangedEventHandler	srcHdlr ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Voice class with the specified Container
		/// </summary>
		/// <param name="container">Container that holds this component</param>
		public Voice(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();

			Initialise() ;
		}

		/// <summary>
		/// Initialises a new instance of the Voice class
		/// </summary>
		public Voice()
		{
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();

			Initialise() ;
		}

		private void Initialise()
		{
			value			= Values.Values.NotConnected ;
			source			= "" ;
			sourceChannel	= null ;
			mute			= false ;
			range			= "0-" ;

			srcHdlr = new Channels.ValueChangedEventHandler(OnSourceChanged) ;

			try
			{
				voice = new SpVoiceClass();
                voice.SetRate(9);
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

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the current Value
		/// </summary>
		/// <remarks>A sound is played when the Value becomes True</remarks>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Values.Value Value
		{
			get
			{
				return this.value ;
			}
			set
			{
				if (this.value != value)
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
								    voice.Speak(msg.Text, SpeechVoiceSpeakFlags.SVSFlagsAsync) ; 
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
		/// Gets or sets the object that is the Source of the object's Value
		/// </summary>
		[Description("Gets or sets the object that is the Source of the object's value")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Channels.IChannel SourceChannel
		{
			get
			{
				return sourceChannel;
			}
			set
			{
				if (sourceChannel != value)
				{
					//free old handler if any
					if (sourceChannel != null)		
					{
						sourceChannel.ValueChanged		-= srcHdlr ;
					}

					//attach new source
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		+= srcHdlr ;
						//NS Only display new values. Value = sourceChannel.Value ; 
					}
					OnPropertyChanged("SourceChannel") ;
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
				//try and connect to source
				SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
			}
		}

		/// <summary>
		/// Gets or sets the name of the Voice used to speak the Value
		/// </summary>
		public string VoiceName
		{
			get
			{
				if (voice != null)
				{
					return voice.Voice.GetDescription(0) ;
				}
				return null ;
			}
			set
			{
				if (voice != null && voice.Voice.GetDescription(0) != value)
				{
					//find the named voice (if possible)
					ISpeechObjectTokens voices = voice.GetVoices("", "");
					foreach(ISpeechObjectToken token in voices)
					{
						if (token.GetDescription(0).Equals(value))
						{
							voice.Voice = token as SpObjectToken ;
							OnPropertyChanged("VoiceName") ;
							break ;
						}
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the voice is muted
		/// </summary>
		[DefaultValue(false)]
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
		/// Event which fires when the value of a property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged ;   
		/// <summary>
		/// The named property has changed: generate the PropertyChanged event
		/// </summary>
		/// <param name="propertyName">Name of property that has changed</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));  //Invokes the delegates.
		}

		/// <summary>
		/// Event for requests to find an object by name.
		/// </summary>
		public event KineticaRT.ObjectSearchEventHandler ObjectSearch;   
		/// <summary>
		/// Fires an ObjectSearch event for the named object
		/// </summary>
		protected object OnObjectSearch(string objectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName);  
		}

		/// <summary>
		/// Looks for the named object
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>Always returns null</returns>
		public virtual object FindObject(string objectName)
		{
			return null ;
		}

		/// <summary>
		/// Returns the named sibling object or null if it does not exist.
		/// </summary>
		public virtual object SearchObject (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

		/// <summary>
		/// Connects this object to others.
		/// </summary>
		/// <remarks>Any buttons added by name are sought and connected to if possible</remarks>
		public virtual void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
            this.Connected = true; //ft
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
            this.Connected = false; //ft
		}
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool Connected //ft //ft
        {
            get { return connected; }
            set { connected = value; }
        }
		#endregion
	}
}

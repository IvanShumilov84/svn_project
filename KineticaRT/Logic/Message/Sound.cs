using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;    // for PInvoke
using System.Threading ;

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// A Component that makes a sound when the Value of the Channel to which
	/// it is connected becomes True
	/// </summary>
	public class Sound : System.ComponentModel.Component, IConnectable, ISource //ft
	{
		// Flags for playing sounds.  For this example, we are reading 
		// the sound from a filename, so we need only specify 
		// SND_FILENAME | SND_ASYNC
		[Flags]
			private enum SoundFlags : int 
		{
			SND_SYNC = 0x0000,  // play synchronously (default) 
			SND_ASYNC = 0x0001,  // play asynchronously 
			SND_NODEFAULT = 0x0002,  // silence (!default) if sound not found 
			SND_MEMORY = 0x0004,  // pszSound points to a memory file
			SND_LOOP = 0x0008,  // loop the sound until next sndPlaySound 
			SND_NOSTOP = 0x0010,  // don't stop any currently playing sound 
			SND_NOWAIT = 0x00002000, // don't wait if the driver is busy 
			SND_ALIAS = 0x00010000, // name is a registry alias 
			SND_ALIAS_ID = 0x00110000, // alias is a predefined ID
			SND_FILENAME = 0x00020000, // name is file name 
			SND_RESOURCE = 0x00040004  // name is resource name or atom 
		}
		
		[DllImport("winmm.dll", SetLastError=true, 
			 CallingConvention=CallingConvention.Winapi)]
		static extern bool PlaySound(
			string pszSound,
			IntPtr hMod,
			SoundFlags sf );

		#region CLASS: SoundModule
		private class SoundModule : System.Windows.Forms.UserControl
		{
			
			public delegate void PlayingChangedEventHandler(bool Playing) ;
			public event PlayingChangedEventHandler		PlayingChanged ;

			//			private AxMediaPlayer.AxMediaPlayer player;
			private string filename ;
			private Thread	thread ;
		
			/// <summary>
			/// Constructor method - creates a single soundmodule for stand alone use
			/// </summary>
			public SoundModule()
			{
			}

			/// <summary>
			/// Sets up a file
			/// </summary>
			/// <param name="file">File to be played</param>
			public void SetUpFile(string file)
			{
				filename = file ;
				//				this.isReady = false;
				//this.player.Open(file);
				//player.Name = file ;
			}

			/// <summary>
			/// Plays a sound
			/// </summary>
			public void Play()
			{
				thread = new System.Threading.Thread (new System.Threading.ThreadStart(ThreadMethod));
				thread.Name = "Message.SoundModule" ;
				thread.Priority = ThreadPriority.Lowest ;
				thread.IsBackground = true ;
				thread.Start();
			}
		
			private void ThreadMethod()
			{
				PlayingChanged(true) ;
				PlaySound (filename, IntPtr.Zero, SoundFlags.SND_FILENAME | SoundFlags.SND_NOSTOP) ;
				PlayingChanged(false) ;
			}

			/// <summary>
			/// Stops playing
			/// </summary>
			public void Stop()
			{
				PlaySound ("", IntPtr.Zero, SoundFlags.SND_NODEFAULT) ;
				PlayingChanged(false) ;
				//this.player.Stop() ;
			}
		}
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Values.Value		value ;
		private string				soundFile ;
		private string				source ;
		private Channels.IChannel	sourceChannel ;
		private bool				mute ;
		private bool				repeat ;		//repeat continuously whilst 'on'
		private string				range ;

		private bool				on ;			//true if Value is 'true'
		private bool				playing ;
		private int					repeatCount ;				
		private SoundModule			module ;
		private Channels.ValueChangedEventHandler	srcHdlr ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Sound class with the specified Container
		/// </summary>
		/// <param name="container">Container that holds this component</param>
		public Sound(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();

			Initialise() ;
		}

		/// <summary>
		/// Initialises a new instance of the Sound class
		/// </summary>
		public Sound()
		{
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();

			Initialise() ;
		}

		private void Initialise()
		{
			module = new SoundModule() ;
			
			value			= Values.Values.False ;
			soundFile		= "" ;
			source			= "" ;
			sourceChannel	= null ;
			mute			= false ;
			repeat			= false ;
			range			= "0-" ;

			repeatCount		= 0 ;
			playing			= false ;
			on				= false ;

			module.PlayingChanged += new SoundModule.PlayingChangedEventHandler(OnPlayingChanged) ;

			srcHdlr = new Channels.ValueChangedEventHandler(OnSourceChanged) ;
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
					bool start_play = false ;

					this.value = value ;

					lock (this)
					{
						on = IsValueOn() ;

						if (on)
						{
							if (!mute)
							{
								if (!playing)
								{
									playing = true ;
									start_play = true ;
								}
								else if (!repeat)
								{
									repeatCount++ ;
								}
							}
						}
					}

					if (start_play) module.Play () ; // module.BeginInvoke (new System.Windows.Forms.MethodInvoker (module.Play)) ;

					OnPropertyChanged("Value") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the file from which the object's Sound is generated
		/// </summary>
		[DefaultValue("")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SoundFile
		{
			get
			{
				return soundFile ;
			}
			set
			{
				if (soundFile != value)
				{
					soundFile = value ;
					lock (this)
					{
						module.SetUpFile(soundFile) ;
					}

					OnPropertyChanged("SoundFile") ;
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
						Value = sourceChannel.Value ;
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
		/// Gets or sets whether sound is muted
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
					if (mute)
					{
						repeatCount = 0 ;

						//stop the sound
						if (playing)		module.Stop() ;
					}
					else if (repeat && on)
					{
						//start the sound
						module.Play() ;
					}
					OnPropertyChanged("Mute") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the sound is played repeatedly whilst the Value is 'True'
		/// </summary>
		[DefaultValue(false)]
		public bool Repeat
		{
			get
			{
				return repeat ;
			}
			set
			{
				if (repeat != value)
				{
					lock (this)
					{
						repeat = value ;

						if (repeat)
						{
							repeatCount = 0 ;
						}

						if (!mute && repeat && on)
						{
							if (!playing)
							{
								//start the sound now
								module.Play() ;
							}
						}
					}
					OnPropertyChanged("Repeat") ;
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

		#region Private methods
		//returns true if value is message with in-range priority, or if value converts to 'true'
		private bool IsValueOn()
		{
			if (this.value is Message)
			{
				return Priority.IsInRange(((Message)this.value).Priority, range) ;
			}

			//else not a message - try and convert to a boolean
			IConvertible		converter = this.value as IConvertible ;
			if (converter != null)
			{
				return converter.ToBoolean(null) ;
			}
			return false ;
		}
		#endregion

		#region Events
		private void OnSourceChanged(Channels.IChannel source, Values.Value newValue)
		{
			Value = newValue ;
		}

		private void OnPlayingChanged(bool Playing)
		{
			bool start_play = false ;

			playing = Playing ;

			lock(this)
			{
				if (!playing)
				{
					if (repeat)
					{
						if (on)	start_play = true ;
					}
					else if (repeatCount > 0)
					{
						repeatCount-- ;
						start_play = true ;
					}
				}
			}

			if (start_play) module.Play() ;
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

/*
	/// <summary>
	/// A Component that makes a sound when the Value of the Channel to which
	/// it is connected becomes True
	/// </summary>
	public class Sound : System.ComponentModel.Component, IConnectable
	{
	#region CLASS: SoundModule
		private class SoundModule : System.Windows.Forms.UserControl
		{
			public delegate void PlayingChangedEventHandler(bool Playing) ;
			public event PlayingChangedEventHandler		PlayingChanged ;

			private AxMediaPlayer.AxMediaPlayer player;
		
			/// <summary>
			/// Constructor method - creates a single soundmodule for stand alone use
			/// </summary>
			public SoundModule()
			{
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Sound));
				player = new AxMediaPlayer.AxMediaPlayer();
				((System.ComponentModel.ISupportInitialize)(player)).BeginInit();
				player.Enabled = true;
				player.Name = "player";
				player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("player.OcxState")));
				player.TabIndex = 0;
				player.Visible = false;
				Controls.AddRange(new System.Windows.Forms.Control[] {player});
				player.PlayStateChange += new AxMediaPlayer._MediaPlayerEvents_PlayStateChangeEventHandler(this.playStateChange);
				((System.ComponentModel.ISupportInitialize)(player)).EndInit();
			
				// for some reason, if you put this code inside the BeginInit / EndInit section above, the control crashes
				// if you know an explanation, please let me know jason.king@profox.co.uk
				player.AutoStart = false;
				player.SendPlayStateChangeEvents = true;
			}

			/// <summary>
			/// Method triggered by the soundModule starting or stopping a file
			/// </summary>
			/// <param name="sender">The controls Active Windows Media Player</param>
			/// <param name="e">PlayStateChange arguments</param>
			private void playStateChange(object sender, AxMediaPlayer._MediaPlayerEvents_PlayStateChangeEvent e)
			{
				if (PlayingChanged != null)
				{
					bool	stopped		= (((AxMediaPlayer.AxMediaPlayer)(sender)).PlayState  == MediaPlayer.MPPlayStateConstants.mpStopped) ;

					PlayingChanged(!stopped) ;
				}
			}

			/// <summary>
			/// Sets up a file
			/// </summary>
			/// <param name="file">File to be played</param>
			public void SetUpFile(string file)
			{
//				this.isReady = false;
				this.player.Open(file);
				player.Name = file ;
			}

			/// <summary>
			/// Plays a sound
			/// </summary>
			public void Play()
			{
				if (player.FileName != "")
				{
					try
					{
						this.player.Play() ;
					}
					catch (Exception e)
					{
						Trace.WriteLine(e.Message, "Play Sound exception") ;
						PlayingChanged(false) ;
					}
				}
			}

			/// <summary>
			/// Stops playing
			/// </summary>
			public void Stop()
			{
				this.player.Stop() ;
			}
		}
	#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Values.Value		value ;
		private string				soundFile ;
		private string				source ;
		private Channels.IChannel	sourceChannel ;
		private bool				mute ;
		private bool				repeat ;		//repeat continuously whilst 'on'
		private string				range ;

		private bool				on ;			//true if Value is 'true'
		private bool				playing ;
		private int					repeatCount ;				
		private SoundModule			module ;
		private Channels.ValueChangedEventHandler	srcHdlr ;

	#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Sound class with the specified Container
		/// </summary>
		/// <param name="container">Container that holds this component</param>
		public Sound(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();

			Initialise() ;
		}

		/// <summary>
		/// Initialises a new instance of the Sound class
		/// </summary>
		public Sound()
		{
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();

			Initialise() ;
		}

		private void Initialise()
		{
			module = new SoundModule() ;
			
			value			= Values.Values.False ;
			soundFile		= "" ;
			source			= "" ;
			sourceChannel	= null ;
			mute			= false ;
			repeat			= false ;
			range			= "0-" ;

			repeatCount		= 0 ;
			playing			= false ;
			on				= false ;

			module.PlayingChanged += new SoundModule.PlayingChangedEventHandler(OnPlayingChanged) ;

			srcHdlr = new Channels.ValueChangedEventHandler(OnSourceChanged) ;
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
					bool start_play = false ;

					this.value = value ;

					lock (this)
					{
						on = IsValueOn() ;

						if (on)
						{
							if (!mute)
							{
								if (!playing)
								{
									playing = true ;
									start_play = true ;
								}
								else if (!repeat)
								{
									repeatCount++ ;
								}
							}
						}
					}

					if (start_play) module.BeginInvoke (new System.Windows.Forms.MethodInvoker (module.Play)) ;

					OnPropertyChanged("Value") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the file from which the object's Sound is generated
		/// </summary>
		[DefaultValue("")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string SoundFile
		{
			get
			{
				return soundFile ;
			}
			set
			{
				if (soundFile != value)
				{
					soundFile = value ;
					lock (this)
					{
						module.SetUpFile(soundFile) ;
					}

					OnPropertyChanged("SoundFile") ;
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
						Value = sourceChannel.Value ;
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
		/// Gets or sets whether sound is muted
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
					if (mute)
					{
						repeatCount = 0 ;

						//stop the sound
						if (playing)		module.Stop() ;
					}
					else if (repeat && on)
					{
						//start the sound
						module.Play() ;
					}
					OnPropertyChanged("Mute") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the sound is played repeatedly whilst the Value is 'True'
		/// </summary>
		[DefaultValue(false)]
		public bool Repeat
		{
			get
			{
				return repeat ;
			}
			set
			{
				if (repeat != value)
				{
					lock (this)
					{
						repeat = value ;

						if (repeat)
						{
							repeatCount = 0 ;
						}

						if (!mute && repeat && on)
						{
							if (!playing)
							{
								//start the sound now
								module.Play() ;
							}
						}
					}
					OnPropertyChanged("Repeat") ;
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

	#region Private methods
		//returns true if value is message with in-range priority, or if value converts to 'true'
		private bool IsValueOn()
		{
			if (this.value is Message)
			{
				return Priority.IsInRange(((Message)this.value).Priority, range) ;
			}

			//else not a message - try and convert to a boolean
			IConvertible		converter = this.value as IConvertible ;
			if (converter != null)
			{
				return converter.ToBoolean(null) ;
			}
			return false ;
		}
	#endregion

	#region Events
		private void OnSourceChanged(Channels.IChannel source, Values.Value newValue)
		{
			Value = newValue ;
		}

		private void OnPlayingChanged(bool Playing)
		{
			bool start_play = false ;

			playing = Playing ;

			lock(this)
			{
				if (!playing)
				{
					if (repeat)
					{
						if (on)	start_play = true ;
					}
					else if (repeatCount > 0)
					{
						repeatCount-- ;
						start_play = true ;
					}
				}
			}

			if (start_play) module.Play() ;
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
		/// Connects this object to others.
		/// </summary>
		/// <remarks>Any buttons added by name are sought and connected to if possible</remarks>
		public virtual void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
		}
	#endregion
	}
*/
}

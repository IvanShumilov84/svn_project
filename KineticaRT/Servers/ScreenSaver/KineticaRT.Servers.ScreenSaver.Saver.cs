using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace KineticaRT.Servers.ScreenSaver
{
    /// <summary>
    /// Displays a screen saver window over the top of all existing windows on a single screen/monitor.
    /// </summary>
    [ChangeRecord("1.0", "09 03 2007", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT SubProject")]
    [Copyright("Copyright © KineticaRT Ltd 2007")]

    [Serializable]
    public class Saver : KineticaRT.Channels.BooleanChannel, ISerializable
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.ScreenSaver", "KineticaRT.Servers.ScreenSaver Messages");
        #region Construction
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
        /// <remarks>To turn off Autoplay - GPEDIT.MSC / Computer Configuration / Administrative Templates / System / Turn off Autoplay</remarks>
        public Saver()
		{
//            Initialise();
            LoadForm();
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
            info.AddValue("InactivityTime", inactivityTime);
            info.AddValue("ScreenIndex", screenIndex);
            info.AddValue("ImageFileName", imageFileName);
            info.AddValue("Opacity", opacity);
            info.AddValue("ActivationTime", activationTime);
            info.AddValue("ActivationSource", activationSource);
            info.AddValue("BackColor", backColor);
        }

        /// <summary>Constructor for the ISerializable interface.</summary>
        protected Saver(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

//            Initialise();

            try
            {
                inactivityTime = info.GetInt32 ("InactivityTime");
                screenIndex = info.GetInt32("ScreenIndex");
                imageFileName = info.GetString("ImageFileName");
                opacity = info.GetDouble("Opacity");
                activationTime = (TimeSpan) info.GetValue("ActivationTime", typeof(TimeSpan));
                activationSource = info.GetString ("ActivationSource");
                backColor = (System.Drawing.Color)info.GetValue("BackColor", typeof(System.Drawing.Color));
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Configuration is missing a property for " + GetType(), this.Name);
            }

            LoadForm(); 
        }
        #endregion

        #region Properties
        int inactivityTime ; // Amount of keyboard-mouse inactive time before the saver display is activated.

        bool active;
        /// <summary>
        /// Gets or sets if the screen saver is currently displayed.
        /// </summary>
        [System.ComponentModel.Description("The current state of the screen saver."), System.ComponentModel.Category("Saver")]
        [DefaultValue(false)]
        public bool Active
        {
            get { return active; }
            set
            {
                if (value != active)
                {
                    active = value;

                    ActivateSaver(active);

                    OnPropertyChanged("Active");
                }
            }
        }

        int screenIndex;
        /// <summary>
        /// Gets or sets which monitor to display this screen saver on.  
        /// The screen saver will only cover one monitor, additional screen saver objects will be required for further monitors.
        /// </summary>
        [System.ComponentModel.Description("Index of the monitor covered by this screen saver."), System.ComponentModel.Category("Saver")]
        [DefaultValue(0)]
        public int ScreenIndex
        {
            get { return screenIndex; }
            set
            {
                if (value != screenIndex)
                {
                    if (value >= System.Windows.Forms.Screen.AllScreens.Length) throw new System.ArgumentException("Screen " + value + " does not exist.");

                    screenIndex = value;
                    OnPropertyChanged("ScreenIndex");
                    LoadForm();
                }
            }
        }

        double opacity = 1.0;
        /// <summary>
        /// Gets or sets the opacity value for the screen saver.
        /// Use 0.0 for zero cover, 0.5 for 50% cover of the underlying windows and 1.0 to completely obscure the underlying windows.
        /// </summary>
        [System.ComponentModel.Description("The opactiy value of the image.  Use 1.0 for fully opaque, 0.5 for 50% see through etc."), System.ComponentModel.Category("Saver")]
        [DefaultValue(1.0)]
        public double Opacity
        {
            get { return opacity; }
            set
            {
                if (value != opacity)
                {
                    opacity = value;
                    OnPropertyChanged("Opacity");
                    if (form!=null) form.Opacity = opacity;
                }
            }
        }

        string imageFileName = "";
        /// <summary>
        /// Gets or sets the name and optional path of a file containing an image to be displayed in the screen saver.
        /// The image is optional, if not set then the background colour will be displayed.
        /// </summary>
        [System.ComponentModel.Description("The name and optional path of a file containing an image to be displayed in the screen saver."), System.ComponentModel.Category("Saver")]
        [DefaultValue("")]
        public string ImageFileName
        {
            get { return imageFileName; }
            set
            {
                if (value != imageFileName)
                {
                    imageFileName = value;
                    OnPropertyChanged("ImageFileName");
                    LoadForm();
                }
            }
        }

        private System.Drawing.Color backColor = System.Drawing.Color.Black;
        /// <summary>
        /// Gets or sets the background colour of the screen saver.
        /// This is used if no imagefile is specified or for the borders of the image if the aspect ratio of the image is not identical to the monitors aspect ratio.
        /// </summary>
        [System.ComponentModel.Description("The background colour of the screen saver. Used for borders or if the ImageFileName is not set."), System.ComponentModel.Category("Saver")]
        public System.Drawing.Color BackColor
        {
            get { return backColor; }
            set
            {
                if (value != backColor)
                {
                    form.BackColor = value;
                    backColor = value;
                    OnPropertyChanged("BackColor");
                }
            }
        }



        string activationSource = "";
        /// <summary>
        /// Gets or sets the name of a channel containing values of type Timespan.
        /// If set then this channel is used to activate the screen saver if the value of the channel reaches that of the ActivationTime property.
        /// If not set then the screen saver must be activated by setting the Active property.
        /// </summary>
        [System.ComponentModel.Description("Name of a channel containing values of type Timespan, used to activate the screen saver if the value of the channel reaches that of the ActivationTime property."), System.ComponentModel.Category("Saver")]
        [DefaultValue("")]
        public string ActivationSource
        {
            get { return activationSource; }
            set
            {
                if (value != activationSource)
                {
                    activationSource = value;
                    OnPropertyChanged("ActivationSource");
                    Disconnect();
                    Connect();
                }
            }
        }

        TimeSpan activationTime = TimeSpan.FromMinutes(1);
        /// <summary>
        /// Gets or sets the amount of time before the screen saver is activated if using the ActivationSource property.
        /// </summary>
        [System.ComponentModel.Description("Amount of time before the screen saver is activated if using the ActivationSource property."), System.ComponentModel.Category("Saver")]
        public TimeSpan ActivationTime
        {
            get { return activationTime; }
            set
            {
                if (value != activationTime)
                {
                    activationTime = value;
                    OnPropertyChanged("ActivationTime");
                    LoadForm();
                }
            }
        }
        #endregion

        void LoadForm()
        {
            if (screenIndex >= System.Windows.Forms.Screen.AllScreens.Length) throw new System.ArgumentException("Screen " + screenIndex + " does not exist.");

            if (form != null)
            {
                form.Close();
            }
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.AllScreens[screenIndex];
            System.Drawing.Rectangle working_area = screen.WorkingArea;

            form = new SaverForm();

            form.Location = new System.Drawing.Point(30000, 300);
            form.Size = new System.Drawing.Size(10, 10);
            form.BackColor = backColor ;
            form.Opacity = opacity;
            form.Show(null);
            form.Visible = false;
            form.Location = new System.Drawing.Point(working_area.X, working_area.Y);
            form.Size = new System.Drawing.Size(working_area.Width, working_area.Height);
            if (!string.IsNullOrEmpty(imageFileName))
            {
                form.BackgroundImage = System.Drawing.Bitmap.FromFile(imageFileName);
            }
            else
            {
                form.BackgroundImage = null;
            }
           
            form.VisibleChanged += new EventHandler(form_VisibleChanged);
        }

        void form_VisibleChanged(object sender, EventArgs e)
        {
            if (active != form.Visible)
            {
                active = form.Visible;
                OnPropertyChanged("Active");
            }
        }

        private void ActivateSaver(bool activate)
        {
            Trace.WriteLineIf(traceSwitch.TraceInfo, "ActivateSaver: " + activate.ToString(), this.Name);

            if (activate)
            {
                form.Visible = true;
            }
            else
            {
                form.Visible = false;
            }

        }

        private System.Windows.Forms.Form form;

        private KineticaRT.Channels.IChannel source;

        #region Connection
        /// <summary>
        /// Connects this channel to any other channels or devices needed.
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            lock (this)
            {
                if (!connected)
                {
                    if (source != null)
                    {
                        source.ValueChanged -= valueChangedHandler;
                        valueChangedHandler = null;
                        source = null;
                    }
                    if (!string.IsNullOrEmpty(activationSource))
                    {
                        source = OnObjectSearch(activationSource) as KineticaRT.Channels.IChannel ;

                        if (source != null)
                        {
                            valueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(source_ValueChanged);
                            source.ValueChanged += valueChangedHandler;
                        }
                    }

                    connected = true;
                }
            }

            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Connect", Name);
        }
        
        KineticaRT.Channels.ValueChangedEventHandler valueChangedHandler;

        void source_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            KineticaRT.Values.TimeSpan new_time = newVal as KineticaRT.Values.TimeSpan ;

            if (activationTime != TimeSpan.Zero)
            {
                if (new_time != null)
                {
                    if (!active && new_time.ToTimeSpan() > activationTime)
                    {
                        Active = true;
                    }
                    else if (active && new_time.ToTimeSpan() < activationTime)
                    {
                        Active = false;
                    }
                }
            }
        }
        private bool connected;

		/// <summary>
        /// Disconnects this channel from all other channels and devices.
        /// </summary>
		public override void Disconnect ()
		{
            lock (this)
            {
                if (connected)
                {
                    connected = false;

                    if (source != null)
                    {
                        source.ValueChanged -= valueChangedHandler;
                        valueChangedHandler = null;
                        source = null;
                    }
                }
            }
            base.Disconnect();
		}
		#endregion

    }
}

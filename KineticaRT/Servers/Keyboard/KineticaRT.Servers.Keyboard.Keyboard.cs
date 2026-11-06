using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KineticaRT.Servers.Keyboard
{
    /// <summary>
    /// Picks up the state of a single key and sets a destination channel true while the key is pressed and false otherwise.
    /// This control will take global control of the key preventing it from being used by other applications.
    /// </summary>
    [ChangeRecord("2.0.0.2", "21 05 2008", "N. Savidge", "Added mouse event handling.")]
    [ChangeRecord("2.0.0.1", "19 05 2008", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
    [Copyright("Copyright © KineticaRT Ltd 2008")]

    public partial class Keyboard : KineticaRT.Controls.Control 
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Keyboard", "Keyboard Messages");
        private const byte VK_SHIFT = 0x10;
        private const byte VK_CONTROL = 0x11;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        private static UserActivityHook hook;

        /// <summary>
        /// Construct the object.
        /// </summary>
        public Keyboard()
        {
            InitializeComponent();
        }

        private bool keyIsDown;

        private void hook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            System.Windows.Forms.Keys shifted = e.KeyCode ;
            if ((GetKeyState(VK_SHIFT) & 0x80) == 0x80) shifted |= System.Windows.Forms.Keys.Shift;
            if ((GetKeyState(VK_CONTROL) & 0x80) == 0x80) shifted |= System.Windows.Forms.Keys.Control;

            if (key == shifted)
            {
                keyIsDown = false;
                e.Handled = true;
                statusLabel.ForeColor = System.Drawing.Color.Green;
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "KeyUp" + shifted.ToString(), Name);

                if (destinationChannel != null)
                {
                    destinationChannel.Value = KineticaRT.Values.Values.False ;
                }
            }
        }

        private void hook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            System.Windows.Forms.Keys shifted = e.KeyCode;
            if ((GetKeyState(VK_SHIFT) & 0x80) == 0x80) shifted |= System.Windows.Forms.Keys.Shift;
            if ((GetKeyState(VK_CONTROL) & 0x80) == 0x80) shifted |= System.Windows.Forms.Keys.Control;

            if (key == shifted)
            {
                keyIsDown = true;
                e.Handled = true;
                statusLabel.ForeColor = System.Drawing.Color.Red ;
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "KeyDown" + shifted.ToString(), Name);

                if (destinationChannel != null)
                {
                    destinationChannel.Value = KineticaRT.Values.Values.True;
                }
            }
        }
        
        #region Connect
        private bool connected;

        /// <summary>
        /// Connects this object to the specified <paramref name="Group"/>.
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            lock (this)
            {
                if (!connected)
                {
                    connected = true;

                    if (hook == null)
                    {
                        hook = new UserActivityHook(false, true);
                    }

                    if (keyDownHandler == null)
                    {
                        keyDownHandler = new System.Windows.Forms.KeyEventHandler(hook_KeyDown);
                        hook.KeyDown += keyDownHandler;
                        keyUpHandler = new System.Windows.Forms.KeyEventHandler(hook_KeyUp);
                        hook.KeyUp += keyUpHandler;
                    }

                    DestinationChannel = OnObjectSearch(destination) as Channels.IChannel;
                }
            }
        }

        private KeyEventHandler keyDownHandler;
        private KeyEventHandler keyUpHandler;

        /// <summary>
        /// Disconnects this object from its <paramref name="Group"/>.
        /// </summary>
        public override void Disconnect()
        {
            lock (this)
            {
                if (connected)
                {
                    if (keyDownHandler != null)
                    {
                        hook.KeyUp -= keyUpHandler;
                        keyUpHandler = null;
                        hook.KeyDown -= keyDownHandler;
                        keyDownHandler = null;
                    }

                    DestinationChannel = null;

                    connected = false;
                }
            }
            base.Disconnect();
        }
        #endregion

        private System.Windows.Forms.Keys key;

        /// <summary>
        /// Gets or sets which key to monitor.
        /// </summary>
        [Category("Keyboard")]
        [DefaultValue(System.Windows.Forms.Keys.None)]
        [Description("Which key to monitor.")]
        public System.Windows.Forms.Keys Key
        {
            get { return key; }
            set
            {
                if (value != key)
                {
                    key = value;
                    statusLabel.Text = key.ToString();
                    OnPropertyChanged("Key");
                }
            }
        }

        private Channels.IChannel destinationChannel;
        private string destination;

        /// <summary>
        /// Gets or sets the name of the object to be the Keys Destination
        /// </summary>
        [Category("Keyboard")]
        [DefaultValue("")]
        [Description("The name of the Keys Destination channel")]
        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                if (destination != value)
                {
                    destination = value;
                    OnPropertyChanged("Destination");
                }
                //try and connect to destination
                DestinationChannel = OnObjectSearch(destination) as Channels.IChannel;
            }
        }

        /// <summary>
        /// Gets or sets the object that is the Destination for the keys value
        /// </summary>
        [Category("Keyboard")]
        [Description("The object that is the Destination for the keys value")]
        public virtual Channels.IChannel DestinationChannel
        {
            get
            {
                return destinationChannel;
            }
            set
            {
                if (destinationChannel != value)
                {
                    destinationChannel = value;

                    if (destinationChannel != null) destinationChannel.Value = keyIsDown ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;

                    OnPropertyChanged("DestinationChannel");
                }
            }
        }
        private bool ShouldSerializeDestinationChannel()
        {
            return false;
        }

        private void statusLabel_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
    }
}

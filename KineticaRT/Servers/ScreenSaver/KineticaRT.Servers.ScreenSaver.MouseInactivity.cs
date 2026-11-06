using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace KineticaRT.Servers.ScreenSaver
{
    /// <summary>
    /// Counts the time since the last mouse or keyboard activity.
    /// </summary>
    [ChangeRecord("1.0", "09 03 2007", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT SubProject")]
    [Copyright("Copyright © KineticaRT Ltd 2007")]

    [Serializable]
    public class MouseInactivity : KineticaRT.Channels.InfoChannel, ISerializable
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.ScreenSaver", "KineticaRT.Servers.ScreenSaver Messages");

        UserActivityHook actHook;

        #region Construction
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
        /// <remarks>To turn off Autoplay - GPEDIT.MSC / Computer Configuration / Administrative Templates / System / Turn off Autoplay</remarks>
        public MouseInactivity()
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
            info.AddValue("InactivityTime", inactivityTime);
        }

        /// <summary>Constructor for the ISerializable interface.</summary>
        protected MouseInactivity(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

//            Initialise();

            try
            {
                inactivityTime = info.GetInt32 ("InactivityTime");
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Configuration is missing a property for " + GetType(), this.Name);
            }
        }
        #endregion

        #region Properties
        int inactivityTime ; // Amount of keyboard-mouse inactive time before the saver display is activated.

        #endregion


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
                    actHook = new UserActivityHook(); // crate an instance with global hooks
                    // hang on events
                    actHook.OnMouseActivity += new MouseEventHandler(OnMouseActivity);
                    actHook.KeyDown += new KeyEventHandler(OnKeyboardActivity);
                    //actHook.KeyPress += new KeyPressEventHandler(OnActivity);
                    //actHook.KeyUp += new KeyEventHandler(OnActivity);

                    lastActivityTime = DateTime.Now;
                    KineticaRT.Services.OneSecondUpdater.Update += new Updater.UpdateEventHandler(OneSecondUpdater_Update);
                    connected = true;
                }
            }

            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Connect", Name);
        }
        private bool connected;

        private void OneSecondUpdater_Update()
        {
            TimeSpan inactivity_time = DateTime.Now - lastActivityTime;
            Value = new KineticaRT.Values.TimeSpan(inactivity_time);
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "InactivityTime="+inactivity_time.TotalSeconds, Name);
        }

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
                    KineticaRT.Services.OneSecondUpdater.Update -= new Updater.UpdateEventHandler(OneSecondUpdater_Update);

                    actHook.OnMouseActivity -= new MouseEventHandler(OnMouseActivity);
                    actHook.KeyDown -= new KeyEventHandler(OnKeyboardActivity);
                    actHook = null;
                }
            }
            base.Disconnect();
		}

        System.DateTime lastActivityTime;

        private void OnMouseActivity(object sender, MouseEventArgs e)
        {
            OnActivity();
        }

        private void OnActivity()
        {
            lastActivityTime = DateTime.Now;
        }

        private void OnKeyboardActivity(object sender, KeyEventArgs e)
        {
            OnActivity();
        }
        #endregion
    }
}

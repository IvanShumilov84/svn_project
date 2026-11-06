using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace KineticaRT.Servers.Power
{
    /// <summary>
    /// Component to handle shutdown of the Application/Logon/Computer.
    /// </summary>
    [ChangeRecord("1.0", "12 03 2007", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT SubProject")]
    [Copyright("Copyright © KineticaRT Ltd 2007")]

    [Serializable]
    public class Shutdown : KineticaRT.Channels.BooleanChannel, ISerializable
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Power", "KineticaRT.Servers.Power Messages");

        #region Construction
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
        /// <remarks>To turn off Autoplay - GPEDIT.MSC / Computer Configuration / Administrative Templates / System / Turn off Autoplay</remarks>
        public Shutdown()
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
            info.AddValue("ShutdownMode", mode);
        }

        /// <summary>Constructor for the ISerializable interface.</summary>
        protected Shutdown(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

//            Initialise();

            try
            {
                mode = (ShutdownMode) info.GetValue("ShutdownMode", typeof(ShutdownMode));
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Configuration is missing a property for " + GetType(), this.Name);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Specifies the method to use during shutdown.
        /// </summary>
        public enum ShutdownMode
        {
            /// <summary>
            /// Do nothing on a shutdown request.
            /// </summary>
            None,
            /// <summary>
            /// Close the application.
            /// </summary>
            CloseApplication,
            /// <summary>
            /// Put the computer into hibernate mode - zero power usage and memory saved to disk.
            /// </summary>
            Hibernate,
            /// <summary>
            /// Put the computer into standby mode - minimal power usage and memory not saved.
            /// </summary>
            Suspend,
        }
        ShutdownMode mode = ShutdownMode.CloseApplication;
        /// <summary>
        /// Gets or sets the method of shutdown.
        /// </summary>
        [System.ComponentModel.Description("Method to use during shutdown."), System.ComponentModel.Category("Power")]
        [System.ComponentModel.DefaultValue(ShutdownMode.CloseApplication)]
        public ShutdownMode Mode
        {
            get { return mode; }
            set
            {
                if (mode != value)
                {
                    mode = value;
                    OnPropertyChanged("Mode");
                }
            }
        }
        #endregion

        /// <summary>Gets or sets the value of this channel.</summary>
        /// <value>A Value object representing the value of the channel.</value>
        [System.ComponentModel.Description("Value of channel."), System.ComponentModel.Category("Channel")]
        public override KineticaRT.Values.Value Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                bool shutdown = (base.Value != value) && System.Convert.ToBoolean(value,System.Globalization.CultureInfo.CurrentUICulture);

                base.Value = value;

                if (shutdown)
                {
                    switch (mode)
                    {
                        case ShutdownMode.None :
                            {
                                // Do nothing.
                            }
                            break;

                        case ShutdownMode.CloseApplication:
                            {
                                Application.Exit();
                            }
                            break;

//                        case ShutdownMode.Logoff:
//                            {
////                                WindowsController.ExitWindows(RestartOptions.LogOff, true);
//                                //ExitWindows.Shutdown(ExitWindows.ShutdownMethod.LogOff, ExitWindows.ShutdownReason.MinorOther);
//                            }
//                            break;

                        case ShutdownMode.Hibernate:
                            {
  //                              WindowsController.ExitWindows(RestartOptions.PowerOff, true);
                                //ExitWindows.Shutdown(ExitWindows.ShutdownMethod.PowerOff, ExitWindows.ShutdownReason.MinorOther);
                                System.Windows.Forms.Application.SetSuspendState(PowerState.Hibernate, true, true);
                            }
                            break;

                        case ShutdownMode.Suspend:
                            {
                                //WindowsController.ExitWindows(RestartOptions.Suspend, true );
                                System.Windows.Forms.Application.SetSuspendState(PowerState.Suspend, true, true);
                            }
                            break;
                    }
                }
            }
        }

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
                    connected = true;
                }
            }

            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Connect", Name);
        }

        //private Microsoft.Win32.PowerModeChangedEventHandler powerModeHandler;

        //void SystemEvents_PowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        //{
        //    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Power: " + e.Mode.ToString(), this.Name);
        //}

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
                }
            }
            base.Disconnect();
		}

        #endregion
    }



}

//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Runtime.Serialization;
//using System.Collections;
//using System.ComponentModel;
//using System.Data;
//using System.Diagnostics;
//using System.Security.Permissions;
//using System.Xml;

//namespace KineticaRT.Servers.Keyboard
//{
//    /// <summary>
//    /// A Keyboard Key monitor channel.
//    /// </summary>
//    [ChangeRecord("1.0", "30 06 2006", "N. Savidge", "Original version")]
//    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
//    [Copyright("Copyright © KineticaRT Ltd 2006")]

//    [Description("Keyboard key monitor")]
//    [Serializable]
//    [DefaultProperty("Key")]
//    public class Key : KineticaRT.Channels.BooleanChannel, ISerializable, IDisposable
//    {
//        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Keyboard", "Keyboard Messages");

//        /// <summary>
//        /// Destructor for class
//        /// </summary>
//        ~Key()
//        {
//            Dispose(false);
//        }

//        /// <summary>
//        /// Closes all resources used by this class, including the update thread.
//        /// </summary>
//        protected virtual void Dispose(bool disposing)
//        {
//            if (!this.disposed)
//            {
//                if (disposing)
//                {
//                    // dispose managed resources
//                }
//                // free native resources
//            }
//            disposed = true;
//        }
//        private bool disposed;

//        /// <summary>
//        /// Closes all resources used by this class, including the update thread.
//        /// </summary>
//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(true);
//        }


//        #region ISerializable
//        /// <summary>
//        /// Populates a SerializationInfo with the data needed to serialize the target object
//        /// </summary>
//        /// <param name="info">The SerializationInfo to populate with data.</param>
//        /// <param name="context">The destination for this serialization.</param>
//        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
//        public override void GetObjectData(SerializationInfo info, StreamingContext context)
//        {
//            if (info == null) throw new ArgumentNullException("info");

//            base.GetObjectData(info, context);
//            info.AddValue("KeyCode", keyCode);
//        }
//        /// <summary>
//        /// Constructor for ISerializable interface.
//        /// </summary>
//        protected Key(SerializationInfo info, StreamingContext context)
//            : base(info, context)
//        {
//            if (info == null) throw new ArgumentNullException("info");

//            base.RawValue = KineticaRT.Values.Values.NotConnected;

//            try
//            {
//                keyCode = (System.Windows.Forms.Keys) info.GetValue("KeyCode", typeof(System.Windows.Forms.Keys));
//            }
//            catch (System.Runtime.Serialization.SerializationException)
//            {
//                //System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
//            }
//        }
//        #endregion

//        #region Construction
//        /// <summary>
//        /// Initialises an instance of the class.
//        /// </summary>
//        public Key()
//        {
//            base.RawValue = KineticaRT.Values.Values.NotConnected;
//        }

//        private UserActivityHook hook;

//        private void Initialise()
//        {
//        }

//        void hook_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
//        {
//            //if (e.KeyChar == System.Windows.Forms.Keys.F8)
//            //{
//            //    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "KeyDown", Name);
//            //}
//        }

//        void hook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
//        {
//            if (e.KeyCode == System.Windows.Forms.Keys.F8)
//            {
//                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "KeyUp", Name);
//            }
//        }

//        void hook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
//        {
//            if (e.KeyCode == System.Windows.Forms.Keys.F8)
//            {
//                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "KeyDown",Name);
//            }
//            else
//            {
//            }
//        }
//        #endregion

//        #region Connect
//        private bool connected;

//        /// <summary>
//        /// Connects this object to the specified <paramref name="Group"/>.
//        /// </summary>
//        public override void Connect()
//        {
//            base.Connect();

//            lock (this)
//            {
//                if (!connected)
//                {
//                    connected = true;

//                    if (hook == null)
//                    {
//                        hook = new UserActivityHook(false, true);

//                        //del = new keyboardHookProc(hookProc);
//                        //handlea = pGCHandle.Alloc(hook, GCHandleType.Pinned);
//                        //handleb = Marshal.GetFunctionPointerForDelegate(del); 

//                        keyDownHandler = new System.Windows.Forms.KeyEventHandler(hook_KeyDown) ;
//                        hook.KeyDown += keyDownHandler ;
//                        keyUpHandler = new System.Windows.Forms.KeyEventHandler(hook_KeyUp) ;
//                        hook.KeyUp += keyUpHandler ;
//                        //hook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(hook_KeyPress);
//                    }
//                }
//            }
//        }

//        System.Windows.Forms.KeyEventHandler keyDownHandler;
//        System.Windows.Forms.KeyEventHandler keyUpHandler;


//        /// <summary>
//        /// Disconnects this object from its <paramref name="Group"/>.
//        /// </summary>
//        public override void Disconnect()
//        {
//            lock (this)
//            {
//                if (connected)
//                {
//                    hook.KeyUp -= keyUpHandler;
//                    hook.KeyDown -= keyDownHandler;
//                    hook = null;

//                    base.Value = KineticaRT.Values.Values.NotConnected;
//                    connected = false;
//                }
//            }
//            base.Disconnect();
//        }

//        #endregion

//        #region Properties

//        private System.Windows.Forms.Keys keyCode = System.Windows.Forms.Keys.F8;

//        /// <summary>
//        /// Gets or sets the key to be monitored.
//        /// </summary>
//        /// <value>Integer keyboard code of the key.</value>
//        [System.ComponentModel.Description("Key to monitor."), System.ComponentModel.Category("Keyboard")]
//        [DefaultValue(System.Windows.Forms.Keys.F8)]
//        public System.Windows.Forms.Keys KeyCode
//        {
//            get
//            {
//                return keyCode;
//            }
//            set
//            {
//                keyCode = value;
//            }
//        }
//        #endregion

//    }
//}

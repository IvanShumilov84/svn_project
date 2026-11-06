using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace KineticaRT.Logic.Test
{
    /// <summary>
	/// Test controller for reading and automating .kst test configuration files..
	/// </summary>
    [ChangeRecord("2.0.0.3", "27 05 2008", "N. Savidge", "Bug: Added disconnect of event handlers on thread abort.")]
    [ChangeRecord("2.0.0.2", "22 05 2008", "N. Savidge", "Added diagnostics to wait handling.")]
    [ChangeRecord("2.0.0.0", "30 06 2007", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
    [Copyright("Copyright © KineticaRT Ltd 2006")]

	[Serializable]
    public class Controller : KineticaRT.Channels.TextChannel, ISerializable, IDisposable, IEnabled
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Logic.Test.Controller", "KineticaRT.Logic.Test.Controller Messages");
        static private string sourceName = typeof(Controller).FullName;

		#region Construction
		/// <summary>
		/// Initialises a new instance of the class
		/// </summary>
        public Controller()
		{
            Initialise();
		}

        private void Initialise ()
        {
            statusMessage = new KineticaRT.Logic.Message.Fault("Creating", Name, "Creating.", sourceName, KineticaRT.Logic.Message.PriorityCode.Trace);
        }
		#endregion 

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
            info.AddValue("FileName", fileName);
            info.AddValue("ProfileName", profileName);
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
        protected Controller(SerializationInfo info, StreamingContext context)
            : base(info, context)
		{
			try
			{
                fileName = info.GetString("FileName");
                profileName = info.GetString("ProfileName");
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}

			Initialise() ;
		}
		#endregion

        #region IDisposable
        /// <summary>
        /// Destructor for class
        /// </summary>
        ~Controller()
        {
            Dispose(false);
        }

        /// <summary>
        /// Closes all resources used by this class, including the update thread.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }
                // free native resources
            }
            disposed = true;
        }
        private bool disposed;

        /// <summary>
        /// Closes all resources used by this class, including the update thread.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
        #endregion

        #region IEnabled
        /// <summary>Occurs when the value of the Value property changes.</summary>
        public event EnabledEventHandler EnabledChanged;


        private bool enabled;

        /// <summary>
        /// Gets or sets the running state.  When true running is enabled, when false running is disabled.
        /// Changing from false to true triggers running.
        /// </summary>
        [Description("The running state.  When true running is enabled, when false running is disabled."), System.ComponentModel.Category("Test")]
        [DefaultValue(false)]
        public Boolean Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                lock (this)
                {
                    if (value)
                    {
                        StartTest();
                    }
                    else
                    {
                        StopTest();
                    }

                }
            }
        }
        #endregion

        #region Properties

        string fileName;
        /// <summary>
        /// Gets or sets the Filename of the test configuration file.
        /// </summary>
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }

        private string profileName = string.Empty;

        /// <summary>
        /// Gets or sets the name of the test profile to be used from within the test file.
        /// </summary>
        [Description("The name of the test profile to be used from within the test file."), System.ComponentModel.Category("Test")]
        [DefaultValue("")]
        public string ProfileName
        {
            get
            {
                return profileName;
            }
            set
            {
                if (profileName != value)
                {
                    profileName = value;
                    OnPropertyChanged("ProfileName");
                }
            }
        }

        private string currentStepName;
        /// <summary>
        /// Gets the name of the current step.
        /// </summary>
        public string CurrentStepName
        {
            get
            {
                return currentStepName;
            }
        }

        private void SetCurrentStepName(string name)
        {
            currentStepName = name;
            OnPropertyChanged("CurrentStepName");
        }

        /// <summary>
        /// Gets the paused state of the controller.
        /// </summary>
        public bool Paused
        {
            get
            {
                return paused;
            }
        }

        private void SetPaused(bool value)
        {
            paused = value;
            OnPropertyChanged("Paused");
        }

        #endregion

        private System.Threading.Thread thread;

        private void StartTest()
        {
            if (!enabled)
            {
                enabled = true;
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Start attempted.", this.Name);

                if (thread != null) thread.Abort();
                thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                targetStep = 0;
                thread.Start();

                OnPropertyChanged("Enabled");
            }
        }

        private void StopTest()
        {
            if (enabled)
            {
                enabled = false;
             
                if (thread != null)
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Stop attempted.", this.Name);
                    thread.Abort();
                }

                OnPropertyChanged("Enabled");
            }
        }

        /// <summary>
        /// Moves the sequence to the start of the next step.
        /// </summary>
        public void NextStep ()
        {
            if (paused)
            {
                targetStep = stepIndex + 1;
                if (targetStep > maximumStep) targetStep = maximumStep;
            }
        }

        /// <summary>
        /// Moves the sequence to the start of the previous step.
        /// </summary>
        public void PreviousStep()
        {
            if (paused)
            {
                targetStep = stepIndex - 1;
                if (targetStep < 0) targetStep = 0;
            }
        }

        private bool paused;
        private bool pause;
        private bool stepAbort;
        private int stepIndex;
        private int subStepIndex;
        private int targetStep;
        private int maximumStep;
        private ManualResetEvent stepAbortEvent = new ManualResetEvent(false);

        /// <summary>
        /// Pauses the sequence at the begining of the next step.
        /// </summary>
        public void Pause()
        {
            if (!paused)
            {
                pause = true;
                targetStep = stepIndex + 1;
                if (targetStep > maximumStep) targetStep = maximumStep;
            }
        }

        /// <summary>
        /// Cancels the current step without completing it and pauses the sequence at the begining of the next step.
        /// </summary>
        public void Abort()
        {
            if (!paused)
            {
                pause = true;
                targetStep = stepIndex;
                stepAbort = true;
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "stepAbort", Name);
                stepAbortEvent.Set();
            }
        }

        /// <summary>
        /// Starts the sequence at the begining of the current step.
        /// </summary>
        public void Start()
        {
            StartTest();
        }

        /// <summary>
        /// Continues the sequence following a pause (at the begining of the current step.)
        /// </summary>
        public void Continue()
        {
            if (paused)
            {
                pause = false;
                SetPaused(false);
                stepAbort = false;
            }
        }

        /// <summary>
        /// Stops the sequence and returns to the start.
        /// </summary>
        public void Stop()
        {
            StopTest();
        }

        /// <summary>
        /// Starts the sequence at the begining of the current step and pauses at the begining of the next step.
        /// </summary>
        public void SingleStep()
        {
            pause = true;
            SetPaused(false);
        }

        XmlNodeList steps;

        private void ThreadProc()
        {
            try
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                SetStatus(new KineticaRT.Logic.Message.Fault("Started", Name, "Test Controller Started.", sourceName, KineticaRT.Logic.Message.PriorityCode.Information));

                if (EnabledChanged != null)
                {
                    EnabledChanged(this, KineticaRT.EnabledEventArgs.EnabledEvent);
                }


                System.Xml.XmlDocument doc = new XmlDocument();
                try
                {
                    SetStatus(new KineticaRT.Logic.Message.Fault("Loading", Name, "Test Controller Loading.", sourceName, KineticaRT.Logic.Message.PriorityCode.Information));

                    doc.Load(System.IO.Path.GetFullPath(fileName));

                    XmlNode root = doc.DocumentElement;

                    XmlNode profile = root.SelectSingleNode("TestProfile [@Name='" + profileName + "']");
                    if (profile == null)
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Profile not found: " + profileName, this.Name);
                    }
                    else
                    {
                        steps = profile.SelectNodes("Step [@Name]");

                        maximumStep = steps.Count;

                        for (stepIndex = 0; stepIndex < steps.Count; stepIndex++)
                        {
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Step: " + steps[stepIndex].OuterXml, this.Name);
                            if (pause)
                            {
                                SetPaused(true);
                                SetCurrentStepName(steps[stepIndex].Attributes["Name"].Value);
                                while (paused)
                                {
                                    if (stepIndex != targetStep)
                                    {
                                        stepIndex = targetStep;
                                        SetCurrentStepName (steps[stepIndex].Attributes["Name"].Value);
                                    }
                                    Thread.Sleep(100);
                                }
                            }

                            XmlNode step = steps[stepIndex];

                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Step: " + step.OuterXml, this.Name);
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Step: " + step.Attributes["Name"].Value, this.Name);

                            SetCurrentStepName (step.Attributes["Name"].Value);

                            XmlNodeList sub_steps = step.ChildNodes; //.SelectNodes("* [@Name]");

                            stepAbort = false;

                            for (subStepIndex = 0; subStepIndex < sub_steps.Count; subStepIndex++)
                            {
                                if (stepAbort) break;

                                XmlNode sub_step = sub_steps[subStepIndex];
                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "SubStep: " + sub_step.OuterXml, this.Name);
                                //string name = sub_step.Attributes["Name"].Value ;
                                string name = sub_step.Name;
                                switch (name)
                                {
                                    case "Setpoint":
                                        System.Xml.XmlAttribute sp_destination = sub_step.Attributes["Name"];
                                        System.Xml.XmlAttribute sp_value = sub_step.Attributes["Value"];
                                        if (sp_destination != null && sp_value != null)
                                        {
                                            SetSetPoint(sp_destination.Value, sp_value.Value);
                                            break;
                                        }
                                        sp_value = sub_step.Attributes["Expression"];
                                        if (sp_destination != null && sp_value != null)
                                        {
                                            SetSetPoint(sp_destination.Value, sp_value.Value, new KineticaRT.ObjectSearchEventHandler(OnObjectSearch));
                                        }
                                        break;

                                    case "Set":

                                        sp_destination = sub_step.Attributes["Name"];
                                        sp_value = sub_step.Attributes["Value"];
                                        if (sp_destination != null && sp_value != null)
                                        {
                                            SetSetPoint(sp_destination.Value, sp_value.Value);
                                            break;
                                        }
                                        sp_value = sub_step.Attributes["Expression"];
                                        if (sp_destination != null && sp_value != null)
                                        {
                                            SetSetPoint(sp_destination.Value, sp_value.Value, new KineticaRT.ObjectSearchEventHandler(OnObjectSearch));
                                        }
                                        break;
                                    case "Action":
                                        ExecuteAction(sub_step);
                                        break;
                                    //case "If":
                                    //    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "On", this.Name);

                                    //    if (sub_step.Attributes["Condition"] != null)
                                    //    {
                                    //        string condition = sub_step.Attributes["Condition"].Value;

                                    //        if (CheckCondition (condition,))
                                    //        {
                                    //            // Move to new step.

                                    //        }
                                    //    }
                                    //    break;
                                    case "Goto":
                                        {
                                            XmlAttribute attribute = sub_step.Attributes["Name"];
                                            if (attribute != null)
                                            {
                                                string go_step = attribute.Value;

                                                for (int index = 0; index < maximumStep; index++)
                                                {
                                                    XmlAttribute attribute2 = steps[index].Attributes["Name"];
                                                    if (attribute2 != null)
                                                    {
                                                        if (attribute2.Value == go_step)
                                                        {
                                                            stepIndex = index - 1;
                                                            subStepIndex = Int16.MaxValue - 1;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        break;

                                    case "Wait":
                                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Wait", this.Name);

                                        Int32 timeout = -1 ;
                                        string on_timeout ="";
                                        if (sub_step.Attributes["Timeout"] != null)
                                        {
                                            timeout = System.Convert.ToInt32(sub_step.Attributes["Timeout"].Value);

                                            if (sub_step.Attributes["OnTimeout"] != null)
                                            {
                                                on_timeout = (sub_step.Attributes["OnTimeout"].Value);
                                            }
                                        }

                                        if (sub_step.Attributes["Condition"] != null)
                                        {
                                            string condition = sub_step.Attributes["Condition"].Value;
                                            if (condition != null)
                                            {
                                                Wait(condition, timeout, on_timeout);
                                            }
                                        }
                                        else
                                        {
                                            string source = sub_step.Attributes["Name"].Value;
                                            string target = sub_step.Attributes["Value"].Value;
                                            if (source != null && target != null)
                                            {
                                                Wait(source + " = " + target, timeout, on_timeout);
                                            }
                                        }
                                        break;
                                    case "Stop":
                                        subStepIndex = Int16.MaxValue - 1;
                                        stepIndex = Int16.MaxValue - 1;
                                        break;
                                    default:
                                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Unknown command: " + name, this.Name);
                                        break;
                                }
                            }
                        }

                        SetStatus(new KineticaRT.Logic.Message.Fault("Finished", Name, "Test Controller Finished.", sourceName, KineticaRT.Logic.Message.PriorityCode.Information));
                    }
                }
                catch (System.IO.FileNotFoundException exception)
                {
                    SetStatus(new KineticaRT.Logic.Message.Fault("FileNotFound", Name, fileName + ": File not found.  " + exception.Message, sourceName, KineticaRT.Logic.Message.PriorityCode.Information));
                }
                catch (System.Threading.ThreadAbortException exception)
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Thread Aborted: " + exception.Message, this.Name);
                }

                lock (this)
                {
                    if (enabled)
                    {
                        enabled = false;
                        OnPropertyChanged("Enabled");
                    }
                }
            }
            catch (KineticaRT.NotConnectedException exception)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceError, "Not implemented: " + exception.Message, this.Name);
            }
            finally
            {
                SetStatus(new KineticaRT.Logic.Message.Fault("Stopped", Name, "Test Controller Stopped.", sourceName, KineticaRT.Logic.Message.PriorityCode.Information));
                thread = null;

                if (EnabledChanged != null)
                {
                    EnabledChanged(this, KineticaRT.EnabledEventArgs.DisabledEvent);
                }
            }
        }

        private KineticaRT.Logic.Message.Fault statusMessage;

        private void SetStatus(KineticaRT.Logic.Message.Fault newStatus)
        {
            if (newStatus.Code != statusMessage.Code) // Don't bother with repeated faults.
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, newStatus.ToString(), this.Name);
                statusMessage = newStatus;
                this.Value = statusMessage;
                OnPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Sets the given channel to the given value.
        /// </summary>
        /// <param name="destination">The channel to be set.</param>
        /// <param name="expression">The value to set the channel to.</param>
        /// <param name="object_search">Object search handler for looking up objects by name.</param>
        protected void SetSetPoint(string destination, string expression, ObjectSearchEventHandler object_search)
        {
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Setpoint:" + destination + ", " + expression, this.Name);

            KineticaRT.Logic.Expressions.Expression fred = new KineticaRT.Logic.Expressions.Expression();
            fred.Text = expression;
            fred.ObjectSearch += object_search;
            fred.Connect();

            KineticaRT.Channels.Channel channel = OnObjectSearch(destination) as KineticaRT.Channels.Channel;
            if (channel == null)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Setpoint: " + destination + " not found.", this.Name);
            }
            else
            {
                channel.Value = fred.Value;
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Setpoint Value: " + expression, this.Name);

                fred.ObjectSearch -= object_search;
                fred.Disconnect();
            }
        }


        /// <summary>
        /// Sets the given channel to the given value.
        /// </summary>
        /// <param name="destination">The channel to be set.</param>
        /// <param name="value">The value to set the channel to.</param>
        protected void SetSetPoint(string destination, string value)
        {
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Setpoint", this.Name);

            KineticaRT.Channels.Channel channel = OnObjectSearch(destination) as KineticaRT.Channels.Channel;
            if (channel == null)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Setpoint: " + destination + " not found.", this.Name);
            }
            else
            {
                KineticaRT.Channels.NumericChannel numeric_channel = channel as KineticaRT.Channels.NumericChannel;
                if (numeric_channel != null)
                {
                    numeric_channel.Value = new KineticaRT.Values.Double(value);
                }
                else
                {
                    KineticaRT.Channels.BooleanChannel boolean_channel = channel as KineticaRT.Channels.BooleanChannel;
                    if (boolean_channel != null)
                    {
                        boolean_channel.Value = new KineticaRT.Values.Boolean(value);
                    }
                    else
                    {
                        channel.Value = new KineticaRT.Values.Text(value);
                    }
                }
            }
        }

        #region Waiting
        private class WaitClass
        {
            private ManualResetEvent waitEvent ;
            
            public WaitClass (ManualResetEvent waitEvent)
            {
                this.waitEvent = waitEvent;
                waitEvent.Reset();
            }

            private void OnValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
            {
                if (newVal.ToDouble() != 0)
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "waitEvent.Set", counter.ToString());
                    waitEvent.Set();
                }
            }

            private static int staticCounter;
            private int counter;

            public bool Wait(string condition, KineticaRT.ObjectSearchEventHandler object_search,Int32 timeout)
            {
                counter = staticCounter++;
                bool return_value = true;
                KineticaRT.Channels.ValueChangedEventHandler value_change_event_handler = new KineticaRT.Channels.ValueChangedEventHandler(OnValueChanged);
                KineticaRT.Logic.Expressions.Expression expression = new KineticaRT.Logic.Expressions.Expression();
                expression.Text = condition;

                try
                {
                    expression.ObjectSearch += object_search;
                    expression.Connect();

                    if ((object)expression.Value == (object)KineticaRT.Values.Values.NotConnected)
                    {
                        throw new KineticaRT.NotConnectedException(condition);
                    }

                    if (expression.Value.ToDouble() == 0)
                    {
                        try
                        {
                            expression.ValueChanged += value_change_event_handler;
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "wait: " + condition, counter.ToString());
                            if (expression.Value.ToDouble() == 0) return_value = waitEvent.WaitOne(timeout, false);
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "wait complete: " + return_value.ToString(), counter.ToString());
                            // expression.ValueChanged -= value_change_event_handler ;
                        }
                        finally
                        {
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "wait end.", counter.ToString());
                            expression.ValueChanged -= value_change_event_handler;
                        }
                    }
                }
                finally
                {
                    expression.ObjectSearch -= object_search;
                    expression.Disconnect();
                }

                return return_value;
            }

            public bool Wait(KineticaRT.Channels.Channel condition, KineticaRT.ObjectSearchEventHandler object_search,Int32 timeout)
            {
                bool return_value = true;

                KineticaRT.Channels.ValueChangedEventHandler value_change_event_handler = new KineticaRT.Channels.ValueChangedEventHandler(OnValueChanged);
                if (condition.Value.ToDouble() == 0)
                {
                    try
                    {
                        condition.ValueChanged += value_change_event_handler;
                        if (condition.Value.ToDouble() == 0) return_value = waitEvent.WaitOne(timeout, false);
                    }
                    finally
                    {
                        condition.ValueChanged -= value_change_event_handler;
                    }

                }

                return return_value;
            }
        }

        /// <summary>
        /// Waits for a channel to be set true.
        /// </summary>
        /// <param name="condition">Channel to wait for.</param>
        /// <param name="timeout">Timeout period in milliseconds.</param>
        /// <param name="onTimeout">Name of step to jump to on a timeout or "" to continue.</param>
        protected void Wait(KineticaRT.Channels.Channel condition, Int32 timeout, string onTimeout)
        {
            WaitClass wait = new WaitClass(stepAbortEvent);
            if (!wait.Wait(condition, new KineticaRT.ObjectSearchEventHandler(OnObjectSearch), timeout))
            {
                // Timeout - need to jump...
                for (int index = 0; index < maximumStep; index++)
                {
                    XmlAttribute attribute = steps[index].Attributes["Name"];
                    if (attribute != null)
                    {
                        if (attribute.Value == onTimeout)
                        {
                            stepIndex = index - 1;
                            subStepIndex = Int16.MaxValue - 1;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Waits for an expression to become true.
        /// </summary>
        /// <param name="condition">Expression to wait for.</param>
        protected void Wait(string condition)
        {
            WaitClass wait = new WaitClass(stepAbortEvent);
            wait.Wait(condition, new KineticaRT.ObjectSearchEventHandler(OnObjectSearch), -1);
        }

        /// <summary>
        /// Waits for an expression to become true.
        /// </summary>
        /// <param name="condition">Expression to wait for.</param>
        /// <param name="timeout">Timeout period in milliseconds.</param>
        /// <param name="onTimeout">Name of step to jump to on a timeout or "" to continue.</param>
        protected void Wait(string condition, Int32 timeout, string onTimeout)
        {
            WaitClass wait = new WaitClass(stepAbortEvent);
            if (!wait.Wait(condition, new KineticaRT.ObjectSearchEventHandler(OnObjectSearch), timeout))
            {
                // Timeout - need to jump...
                for (int index = 0 ; index < maximumStep ; index ++)
                {
                    XmlAttribute attribute = steps[index].Attributes["Name"];
                    if (attribute != null)
                    {
                        if (attribute.Value == onTimeout)
                        {
                            stepIndex = index -1;
                            subStepIndex = Int16.MaxValue -1;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Waits for a period in milliseconds.
        /// </summary>
        /// <param name="milliseconds">Number of milliseconds to wait for.</param>
        static protected void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }
        #endregion

        /// <summary>
        /// Default action handler for the test controller.
        /// </summary>
        /// <remarks>This is normally overridden by a subclass to implement application specific actions.</remarks>
        /// <param name="node">XmlNode containing the action details.</param>
        protected virtual void ExecuteAction(XmlNode node)
        {
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceError, "Action Not Implemented: "+node.OuterXml, this.Name);
        }
    }
}


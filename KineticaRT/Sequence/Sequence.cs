using System;
using KineticaRT ;
using KineticaRT.Channels ;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics ;

namespace KineticaRT.Sequence
{

	/// <summary>
	/// KineticaRT Sequence class.
	/// </summary>
	[ChangeRecord ("1.0", "18 02 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="9792", ProjectName="KineticaRT Studio")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public class Sequence : IConnectable
	{
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Sequence", "Sequence Messages");

        #region Properties
        /// <summary>
        /// Gets or sets the sequences name.
        /// </summary>
        /// <value>The sequences name.</value>
        [System.ComponentModel.Description("Name of sequence."), System.ComponentModel.Category("Sequence")]
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string name = "";
        #endregion 

        #region PropertyChanged
        /// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   
		/// <summary>
		/// Fires the PropertyChanged event.
		/// </summary>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
		#endregion

		#region Connection
        /// <summary>
        /// Connects this object to others.
        /// </summary> 
        public virtual void Connect()
        {
            this.Connected = true; //ft
        }

        /// <summary>
        /// Connects this object to others, stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public virtual void Connect2()
        {
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
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

		#region ObjectSearch
		/// <summary>
		/// Event handler for requests from the objects children to find another object for them by name.
		/// </summary>
		public event ObjectSearchEventHandler ObjectSearch;   
		/// <summary>
		/// Fires an object search event to the parent object.
		/// </summary>
		protected object OnObjectSearch(string objectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName);  
		}

		/// <summary>
		/// Returns the named channel or null if it does not exist.
		/// </summary>
		public virtual KineticaRT.Channels.Channel GetChannel (string objectName)
		{
			KineticaRT.Channels.Channel return_value ;

			return_value = OnObjectSearch (objectName) as KineticaRT.Channels.Channel ;

			if (return_value == null)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "Failed to find Channel.", objectName) ;
			}

			return return_value ;
		}

		/// <summary>
		/// Returns the named object or null if it does not exist.
		/// </summary>
		public virtual object GetObject (string objectName)
		{
			object return_value ;

			return_value = OnObjectSearch (objectName) ;

			if (return_value == null)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, "Failed to find Object.", objectName) ;
			}

			return return_value ;
		}

		/// <summary>
		/// Returns the named object or null if it does not exist.
		/// </summary>
		public virtual object FindObject (string objectName)
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

		#endregion

		#region Threads
		/// <summary>
		/// Starts a new thread.
		/// </summary>
		/// <param name="ThreadMethod"></param>
		protected void StartThread (ThreadStart ThreadMethod)
		{
			Thread new_thread = new System.Threading.Thread (ThreadMethod);
			new_thread.Name = ThreadMethod.Target.ToString() + "." + ThreadMethod.Method.Name ;
			new_thread.Priority = ThreadPriority.BelowNormal ;
			new_thread.IsBackground = true ;
			new_thread.Start();
		}
		#endregion

		#region Misc. Functions
		/// <summary>
		/// Creates a new KineticaRT Value object with the given value.
		/// </summary>
		/// <param name="value">The value to use.</param>
		/// <returns></returns>
		protected KineticaRT.Values.Value Value (double value)
		{
			if (value == 0) return KineticaRT.Values.Values.Zero ;
			return new KineticaRT.Values.Double (value) ;
		}
		#endregion

		#region Waiting
		private class WaitClass
		{
			ManualResetEvent wait_event = new ManualResetEvent (false) ;

			private void OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
			{
				if (newVal.ToDouble() != 0) wait_event.Set () ;
			}

			public void Wait (string condition, KineticaRT.ObjectSearchEventHandler object_search)
			{
				KineticaRT.Channels.ValueChangedEventHandler vceh = new KineticaRT.Channels.ValueChangedEventHandler(OnValueChanged) ;
				KineticaRT.Logic.Expressions.Expression fred = new KineticaRT.Logic.Expressions.Expression () ;
				fred.Text = condition ;
				fred.ObjectSearch += object_search ;
				fred.Connect () ;
				if (fred.Value.ToDouble() == 0)
				{
					fred.ValueChanged += vceh ;
					if (fred.Value.ToDouble() == 0)	wait_event.WaitOne() ;
//					fred.ValueChanged -= vceh ;
				}
				
				fred.ObjectSearch -= object_search ;
				fred.Disconnect () ;
			}

			public void Wait (Channel condition)
			{
				KineticaRT.Channels.ValueChangedEventHandler vceh = new KineticaRT.Channels.ValueChangedEventHandler(OnValueChanged) ;
				if (condition.Value.ToDouble() == 0)
				{
					condition.ValueChanged += vceh ;
					if (condition.Value.ToDouble() == 0)	wait_event.WaitOne() ;
					condition.ValueChanged -= vceh ;
				}
			} 
		}

		/// <summary>
		/// Waits for a channel to be set true.
		/// </summary>
		/// <param name="condition">Channel to wait for.</param>
		protected void Wait (Channel condition)
		{
			WaitClass wait = new WaitClass () ;
			wait.Wait (condition) ;
		}

		/// <summary>
		/// Waits for an expression to become true.
		/// </summary>
		/// <param name="condition">Expression to wait for.</param>
		protected void Wait (string condition)
		{
			WaitClass wait = new WaitClass () ;
			wait.Wait (condition, new KineticaRT.ObjectSearchEventHandler(OnObjectSearch)) ;
		}

		/// <summary>
		/// Waits for a period in milliseconds.
		/// </summary>
		/// <param name="milliseconds">Number of milliseconds to wait for.</param>
		protected void Wait (int milliseconds)
		{
			Thread.Sleep (milliseconds) ;
		}
		#endregion
	}
}

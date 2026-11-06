using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Generates an Update event for Controls to update themselves.
	/// </summary>
	public class EventPool
	{
		/// <summary>
		/// Rate at which the event pool triggers (ms)
		/// </summary>
		public	uint	UpdateRate ;

		private void ThreadMethod()
		{
			//ft Thread.CurrentThread.Name = "KineticaRT Controls EventPool Worker" ;
			Thread.CurrentThread.Priority = ThreadPriority.Highest ;
			Thread.CurrentThread.IsBackground = true ;
            
			while (true)
			{
                if (UpdateEvent != null)
                {
                    UpdateEvent();  //Invokes the delegates.
                }
                if (UpdateRate != 0)
				    Thread.Sleep((int)UpdateRate);
			}
		}

		/// <summary>
		/// Event generated whenever UpdateRate ms have passed
		/// </summary>
		public event UpdateEventHandler UpdateEvent;

		/// <summary>
		/// Event Handler delegate for the UpdateEvent
		/// </summary>
		public delegate void UpdateEventHandler();

		/// <summary>
        /// Initialises a new instance of the EventPool class
		/// </summary>
        /// <param name="newUpdateRate">Rate at which the Event Pool will trigger</param>
		/// <param name="poolDescription">The name of the pool</param>
		public EventPool(uint newUpdateRate, string poolDescription)
		{
			UpdateRate = newUpdateRate ;
			Thread new_thread = new System.Threading.Thread (new System.Threading.ThreadStart(ThreadMethod));
            new_thread.Name = poolDescription;//"KineticaRT Controls EventPool Worker"; //ft
			new_thread.Start();
		}
	}
}

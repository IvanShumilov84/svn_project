using System;
using System.Threading;
using System.Diagnostics;

namespace KineticaRT.Servers.Simulate
{
	/// <summary>
	/// Generates Update events at the rate of the KineticaRT.Services.FastUpdater object.
	/// Used to synchronise all objects sharing the same EventPool.
	/// Provides a value which increases with time.
	/// </summary>
	/// <remarks>Use the SystemEventPool property of this class to synchronise your own objects with the standard wave simulators.</remarks>
	public class EventPool
	{
		static EventPool systemEventPool = new EventPool () ;
		/// <summary>Gets the EventPool object used to synchronise all the standard wave generators.</summary>
		/// <value>The standard EventPool object.</value>
		public static EventPool	SystemEventPool	{ get { return systemEventPool ;}}

		System.DateTime start = System.DateTime.Now ;

		private void update_proc()
		{
			System.TimeSpan span = System.DateTime.Now.Subtract (start) ;
			double Value = span.TotalMilliseconds / 1000.0 ;
			if (Update != null)	Update(Value) ;
		}

		/// <summary>
		/// Occurs when it is time for an update.
		/// </summary>
		public event UpdateEventHandler Update ;   

		/// <summary>
		/// Represents the method that will handle the Update event.
		/// </summary>
		/// <param name="Value">The number of seconds since the EventPool was created.</param>
		public delegate void UpdateEventHandler(double Value) ;
		
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		public EventPool()
		{
			KineticaRT.Services.FastUpdater.Update += new KineticaRT.Updater.UpdateEventHandler(update_proc);
		}
	}
}

using System;
using System.Threading;
using System.Diagnostics;
//using System.Runtime.InteropServices ;

namespace KineticaRT
{
	/// <summary>
	/// Generates an update event for objects to update themselves on.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public class Updater: IDisposable
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Updater", "KineticaRT.Updater Messages");
        //[DllImport("coredll")]
		//extern static bool CeSetThreadPriority(int handle, int priority);
		//[DllImport("coredll")]
		//extern static int CeGetCurrentThread();

		private	int		updateRate ;
		private Thread	thread ;
		private bool	stopThread ;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="name">Name of updater.</param>
		/// <param name="updateRate">Update rate in milliseconds.</param>
		public Updater(string name, int updateRate)
		{
			this.updateRate = updateRate ;
			thread = new System.Threading.Thread (new System.Threading.ThreadStart(ThreadMethod));
			thread.Name = "KineticaRT Updater: " + name ;
			thread.Priority = ThreadPriority.Highest ;
			thread.IsBackground = true ;
			thread.Start();
		}

		/// <summary>
		/// Destructor for class
		/// </summary>
		~Updater()
		{
			Dispose(false);
		}

		#region IDisposable
		private bool				disposed;
		
		/// <summary>
		/// Closes all resources used by this class, including the update thread.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		/// <summary>
		/// Closes all resources used by this class, including the update thread.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
					stopThread = true ;
					thread = null ;
				}
			}
			disposed = true;         
		}
		#endregion
			
		private void ThreadMethod()
		{
			//SetThreadPriority (GetCurrentThread(),15) ;
			//SetIdlePriority () ;
			
			int next = System.Environment.TickCount ;
			int period = updateRate ;

			while (!stopThread)
			{
                try
                {
                    if (Update != null) Update();
                }
                catch (System.Exception exception)
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceError, exception.Message+": "+exception.StackTrace, System.Threading.Thread.CurrentThread.Name);
                }

				int delay = (next - System.Environment.TickCount) ;
				
				if (delay > 2)
				{
					Thread.Sleep (delay);
					next += period ;
				}
				else if (delay > -10)
				{
					//Thread.Sleep (delay);
					next += period ;
				}
				else
				{
					Thread.Sleep (1);
					next = System.Environment.TickCount ;
					next += period ;
				}
			}
		}

		/// <summary>Gets the update rate.</summary>
		/// <value>Update rate in seconds.</value>
		public double UpdateRate
		{
			get
			{
				return updateRate / 1000.0 ;
			}
		}


		/// <summary>Represents the method that will handle the Update event.</summary>
		public delegate void UpdateEventHandler();
		/// <summary>Occurs at the specified update rate.</summary>
		public event UpdateEventHandler Update;   
	}
}

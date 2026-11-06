using System;
using System.Collections.Generic;
using System.Text;

namespace KineticaRT.Compact.Controls
{
    /// <summary>
    /// Generates an Update event for Controls to update themselves.
    /// </summary>
    public class EventPool
    {
        /// <summary>
        /// Rate at which the event pool triggers (ms)
        /// </summary>
        public uint UpdateRate;

        private void ThreadMethod()
        {
            System.Threading.Thread.CurrentThread.Name = "KineticaRT Controls EventPool Worker";
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Highest;
            System.Threading.Thread.CurrentThread.IsBackground = true;

            while (true)
            {
                if (UpdateEvent != null) UpdateEvent();  //Invokes the delegates.
                System.Threading.Thread.Sleep((int)UpdateRate);
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
        public EventPool(uint newUpdateRate)
        {
            UpdateRate = newUpdateRate;
            System.Threading.Thread new_thread = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadMethod));
            new_thread.Start();
        }
    }
}


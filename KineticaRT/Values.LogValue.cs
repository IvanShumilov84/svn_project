using System;
using System.Collections.Generic;
using System.Text;

namespace KineticaRT.Values
{
    /// <summary>
    /// Value used for logging. 
    /// </summary>
    /// <remarks>It includes a normal value and the time at which it was collected.</remarks>
    public class LogValue
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value">Value to be recorded.</param>
        /// <param name="time">Time at which the the value was collected.</param>
        public LogValue(KineticaRT.Values.Value value, System.DateTime time)
        {
            this.value = value;
            this.time = time;
        }

        private Value              value ;
        private System.DateTime    time ;

        /// <summary>
        /// Value recorded for this LogValue.
        /// </summary>
        public Value Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
       }

        /// <summary>
       /// Time at which this LogValue was recorded.
        /// </summary>
        public System.DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
    }
}

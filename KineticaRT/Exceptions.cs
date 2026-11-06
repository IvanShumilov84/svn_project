using System;
using System.Collections.Generic;
using System.Text;

namespace KineticaRT
{
    /// <summary>
    /// Exception thrown when a connection has failed.
    /// </summary>
    public class NotConnectedException:System.Exception 
    {
        /// <summary>
        /// Initialises a new instance of the class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public NotConnectedException(string message):base (message)
        {
        }
    }
}

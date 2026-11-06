using System;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides data for the ControlAdded and ControlRemoved events.
    /// </summary>
    public class ControlEventArgs
    {
    }
}

namespace KineticaRT
{
    /// <summary>
    /// Specifies the security actions that can be performed using declarative security. 
    /// </summary>
	public enum SecurityAction
	{
        /// <summary>
        /// All callers higher in the call stack are required to have been granted the permission specified by the current permission object (see Security Demands). 
        /// </summary>
		Demand ,
	}

    /// <summary>
    /// Allows security actions for SecurityPermission to be applied to code using declarative security.
    /// </summary>
	public class SecurityPermissionAttribute : Attribute
	{
		SecurityPermissionAttribute (SecurityAction action, bool SerializationFormatter) 
		{
		}

	}
	
}

namespace KineticaRT.Compact.Diagnostics
{
    /// <summary>
    /// Provides a multilevel switch to control tracing and debug output without recompiling your code.
    /// </summary>
    public class TraceSwitch
    {
        /// <summary>
        /// Initializes a new instance of the TraceSwitch class. 
        /// </summary>
        /// <param name="description">The description of the switch.</param>
        /// <param name="name">The name to display on a user interface.</param>
        public TraceSwitch(string description, string name)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the switch allows all messages. 
        /// </summary>
        public bool TraceVerbose = false;
        /// <summary>
        /// Gets a value indicating whether the switch allows informational messages. 
        /// </summary>
        public bool TraceInfo = false;
        /// <summary>
        /// Gets a value indicating whether the switch allows warning messages. 
        /// </summary>
        public bool TraceWarning = false;
        /// <summary>
        /// Gets a value indicating whether the switch allows error messages. 
        /// </summary>
        public bool TraceError = false;
    }


    /// <summary>
    /// Provides a set of methods and properties that help you trace the execution of your code.
    /// </summary>
    public class Trace
    {
        /// <summary>
        /// Class Constructor.
        /// </summary>
        static public void WriteLineIf()
        {
        }

        /// <summary>
        /// Writes information about the debug to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="text">A message to write.</param>
        static public void WriteLine(string text)
        {
        }

        /// <summary>
        /// Writes information about the debug to the trace listeners in the Listeners collection if a condition is true. 
        /// </summary>
        /// <param name="condition">true to cause a message to be written; otherwise, false. </param>
        /// <param name="message">A message to write.</param>
        /// <param name="catagory">A category name used to organize the output.</param>
        static public void WriteLineIf(bool condition, string message, string catagory)
        {
            System.Diagnostics.Debug.WriteLineIf(condition, message, catagory);
        }
    }

    /// <summary>
    /// Provides a set of methods and properties that help debug your code.
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// Class Constructor.
        /// </summary>
        static public void WriteLineIf()
        {
        }

        /// <summary>
        /// Writes information about the debug to the trace listeners in the Listeners collection.
        /// </summary>
        /// <param name="text">A message to write.</param>
        static public void WriteLine(string text)
        {
        }
    }
}


using System;
using System.ComponentModel;
using System.Runtime.Serialization ;
using System.Xml.Serialization ;
using System.Drawing;

namespace KineticaRT.Logic.Message
{
    /// <summary>
    /// Standard codes for the Priority property.
    /// </summary>
    public enum PriorityCode //???: short
    {
        /// <summary>
        /// Zero priority, not normally used.
        /// </summary>
        Zero = 0,

        /// <summary>
        /// Debug message, gives detailed information about the operation of the system during system debugging.
        /// </summary>
        Debug = 10,

        /// <summary>
        /// Trace message, used to identify what sequence of events has happened if a problem occurs.
        /// </summary>
        Trace = 20,
        
        /// <summary>
        /// Help message, used to give guidance to the user as to what to do.
        /// </summary>
        Help = 30,

        /// <summary>
        /// Information message, used to keep the user informed of system operation.
        /// </summary>
        Information = 40,

        /// <summary>
        /// Quiet Information message, gives the user additonal information over the Information message if requested.
        /// </summary>
        InformationQuiet = 50,

        /// <summary>
        /// Limit message, indicates a limit has been reached or passed.
        /// </summary>
        Limit = 60,

        /// <summary>
        /// Warning message, indicates that a fault may have occured or be about to occur. Or that a High or low limit was crossed.
        /// </summary>
        Warning = 70,

        /// <summary>
        /// Critical message, indicates that a HighHigh or a LowLow limit was crossed.
        /// </summary>
        Critical = 75,

        /// <summary>
        /// Error message, indicates that a fault has occured.
        /// </summary>
        Error = 80,

        /// <summary>
        /// Fast Stop message, indicates that a serious fault has occured and the system is being stopped in a controlled manor.
        /// </summary>
        FastStop = 90,
        
        /// <summary>
        /// Emergency Stop message, indicates that a life threatening problem has occured and the system is being shut down as quickly as possible.
        /// </summary>
        EmergencyStop = 100,

        /// <summary>
        /// Fatal message, indicates that the system has died :(
        /// </summary>
        Fatal = 110,
    }

    /// <summary>
	/// A single message.
	/// </summary>
	[System.ComponentModel.ImmutableObject(true)] 
	[Serializable]
	public class Message:KineticaRT.Values.Value
	{
        string text;
		/// <summary>Gets the text of this message object.</summary>
		/// <value>A string containing the text of this message.</value>
		public string Text { get { return text ;}}

        Color textColor;
        /// <summary>Gets the text color of this message object.</summary>
        /// <value>A string containing the text color of this message.</value>
        public Color TextColor { get { return textColor; } }

        string source;
		/// <summary>Gets the text describing the source of this message object.</summary>
		/// <value>A string containing a decription of the source of this message.</value>
		public string Source { get { return source ;}}

		PriorityCode priority ;
		/// <summary>Gets the priority of this message object.</summary>
		/// <value>A integer specifying the priority of this message.</value>
        public PriorityCode Priority { get { return priority; } }

		DateTime	timestamp ;
		/// <summary>Gets the timestamp of this message object.</summary>
		/// <value>A DateTime object containing the timestamp for this message.</value>
		public DateTime Timestamp { get { return timestamp ;}}

		#region Constructors

        /// <summary>
        /// Initialises a new instance of the class using the specified values.<br/>
        /// The message is stamped with the current time.
        /// </summary>
        /// <param name="timestamp">Date and time at which the fault occured.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="source">Text describing the source of the message.</param>
        /// <param name="priority">Integer specifying the priority of the message.</param>
        /// <param name="textColor">Color specifying the color of the message text. If specified will override priority color.</param>
        public Message(DateTime timestamp, string text, string source, PriorityCode priority, Color textColor)
        {
            this.text = text;
            this.source = source;
            this.priority = priority;
            this.timestamp = timestamp;
            this.textColor = textColor;
        }

        public Message(string text, string source, PriorityCode priority)
            : this(DateTime.Now, text, source, priority, Color.Empty)
        {            
        }

        public Message(string text, string source, PriorityCode priority, Color textColor)
            : this(DateTime.Now, text, source, priority, textColor)
        {
        }

        public Message(string text, string source, int priority)
            : this(DateTime.Now, text, source, (PriorityCode)priority, Color.Empty)
        {
        }
        public Message(DateTime timestamp, string text, string source, int priority)
            : this(timestamp, text, source, (PriorityCode)priority, Color.Empty)
        {
        }
        public Message(string text, string source, int priority, Color textColor)
            : this(DateTime.Now, text, source, (PriorityCode)priority, textColor)
        {
        }
        #endregion

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString ()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder () ;
			sb.Append (Timestamp.ToString()) ;
			sb.Append (' ') ;
			sb.Append (Priority.ToString()) ;
			sb.Append (" : ") ;
			sb.Append (Source) ;
			sb.Append (" - ") ;
			sb.Append (Text) ;
            sb.Append (" - ");
            sb.Append (TextColor);
			return sb.ToString () ;
		}

		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString (string format, IFormatProvider formatProvider)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder () ;
			sb.Append (Timestamp.ToString(format, formatProvider)) ;
			sb.Append (' ') ;
			sb.Append (((Int32)Priority).ToString(formatProvider)) ;
			sb.Append (' ') ;
			sb.Append (Source) ;
			sb.Append (' ') ;
			sb.Append (Text) ;
            sb.Append(" - ");
            sb.Append(TextColor);
            return sb.ToString();
		}
		#endregion

	

		/// <summary>
		/// Equality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is equal to RHS otherwise false.</returns>
		public static bool operator ==(Message value1, Message value2)
		{
			if ((object)value1 == (object)value2) return true ;
			if ((object)value1 == null || (object)value2 == null) return false ;
			return (value1.ToString () == value2.ToString()) ;
		}

		/// <summary>
		/// Inequality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is not equal to RHS otherwise false.</returns>
		public static bool operator !=(Message value1, Message value2)
		{
			if ((object)value1 == (object)value2) return false ;
			if ((object)value1 == null || (object)value2 == null) return true ;
			return (value1.ToString() != value2.ToString()) ;
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
            KineticaRT.Logic.Message.Message message = obj as KineticaRT.Logic.Message.Message ;

            if (message != null)
            {
                return 
                    this.priority == message.priority &&
                    this.source == message.source &&
                    this.text == message.text &&
                    this.textColor == message.textColor &&
                    this.timestamp.Year == message.timestamp.Year &&
                    this.timestamp.DayOfYear == message.timestamp.DayOfYear &&
                    this.timestamp.Hour==message.timestamp.Hour &&
                    this.timestamp.Minute==message.timestamp.Minute &&
                    this.timestamp.Second == message.timestamp.Second;
            }
			else
            {
                return (this.ToString().Equals(obj)) ;
            }
		}

		/// <summary>
		/// Returns the hash code of this object.
		/// </summary>
		public override int GetHashCode ()
		{
			return ToString().GetHashCode() ;
		}
    
        /// <summary>Compares this instance to a specified object and returns an indication of their relative values</summary>
        /// <returns>A 32-bit signed integer that indicates the relative order of the comparands. The return value has these meanings:
        ///	<ul>
        ///	<li>Less than zero - This instance is less than obj. </li>
        ///	<li>Zero - This instance is equal to obj. </li>
        ///	<li> Greater than zero - This instance is greater than obj. </li>
        ///	</ul>
        /// </returns>
        public override int CompareTo(object obj)
        {
            KineticaRT.Logic.Message.Fault it = obj as KineticaRT.Logic.Message.Fault;
            return ((object)this == (object)it) ? 0 : 1;
        }

    }
}

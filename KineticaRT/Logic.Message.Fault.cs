using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace KineticaRT.Logic.Message
{
    /// <summary>
    /// A fault message.
    /// </summary>
    [System.ComponentModel.ImmutableObject(true)]
    [Serializable]
    public class Fault : Message
    {
        #region Constructors/Destructors
        /// <summary>
        /// Initialises a new instance of the class using the specified values.<br/>
        /// The message is stamped with the current time.
        /// </summary>
        /// <param name="code">Code indicating the fault suitable for use in lookup tables.</param>
        /// <param name="channel">Identifier for the channel at fault if appropriate.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="source">Text describing the source of the message.</param>
        /// <param name="priority">Integer specifying the priority of the message.</param>
        public Fault(string code, string channel, string text, string source, PriorityCode priority)
            : base(text, source, priority)
        {
            this.code = code;
            this.channel = channel;
        }
        /// <summary>
        /// Initialises a new instance of the class using the specified values.<br/>
        /// The message is stamped with the current time.
        /// </summary>
        /// <param name="timestamp">Date and time at which the fault occured.</param>
        /// <param name="code">Code indicating the fault suitable for use in lookup tables.</param>
        /// <param name="channel">Identifier for the channel at fault if appropriate.</param>
        /// <param name="text">Text of the message.</param>
        /// <param name="source">Text describing the source of the message.</param>
        /// <param name="priority">Integer specifying the priority of the message.</param>
        public Fault(DateTime timestamp, string code, string channel, string text, string source, PriorityCode priority)
            : base(timestamp, text, source, priority, Color.Empty)
        {
            this.code = code;
            this.channel = channel;
        }
        #endregion

        #region Properties
        string code;
        /// <summary>Gets the fault or fault code.</summary>
        /// <value>A string containing the fault or fault code.</value>
        public string Code { get { return code; } }

        string channel;
        /// <summary>Gets the channel or channel ID.</summary>
        /// <value>A string containing the text of the channel or channel ID.</value>
        public string Channel { get { return channel; } }
        #endregion


                //sb.Append("<EMPH>");
                //sb.Append("</EMPH>");
                //sb.Append("<BREAK time='200ms'/>");
                //sb.Append("<VOLUME LEVEL='100'>");
                //sb.Append("</VOLUME>");
                //sb.Append("<ssml:break time='200ms'/>");


        #region Conversions
        /// <summary>
        /// Converts this Value to a System.String.
        /// </summary>
        /// <returns>The result.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(Priority.ToString());
            //sb.Append(Timestamp.ToString());
            if (!string.IsNullOrEmpty(code))
            {
                sb.Append(". ");
                sb.Append(code.ToString());
                sb.Append(". ");
            }
            if (!string.IsNullOrEmpty(Source))
            {
                sb.Append(Source);
            }
            if (!string.IsNullOrEmpty(channel))
            {
                sb.Append(" ");
                sb.Append(channel);
            }

            //if (!string.IsNullOrEmpty(Text))
            //{
            //    sb.Append(" - ");
            //    sb.Append(Text);
            //}
            
            return sb.ToString();
        }

        /// <summary>
        /// Converts this Value to a System.String.
        /// </summary>
        /// <returns>The result.</returns>
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(Timestamp.ToString(format, formatProvider));
            if (!string.IsNullOrEmpty(code))
            {
                sb.Append(' ');
                sb.Append(code.ToString());
            }
            sb.Append(' ');
            sb.Append(((Int32)Priority).ToString(formatProvider));
            if (!string.IsNullOrEmpty(Source))
            {
                sb.Append(' ');
                sb.Append(Source);
            }
            if (!string.IsNullOrEmpty(channel))
            {
                sb.Append(" . ");
                sb.Append(channel);
            }
            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(" - ");
                sb.Append(Text);
            }
            if (!TextColor.IsEmpty)
            {
                sb.Append(" - ");
                sb.Append(TextColor);
            }
            return sb.ToString();
        }
        #endregion
    }
}

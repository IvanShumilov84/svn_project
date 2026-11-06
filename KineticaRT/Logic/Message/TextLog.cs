using System;
using KineticaRT;
using System.Diagnostics ;
using System.Runtime.Serialization ;
using System.IO ;
using System.Text ;

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// Logs messages into a text file.
	/// </summary>
	[Serializable]
    [ChangeRecord("2.0.0.2", "2011 08 03", "N. Savidge", "Removed problem with duplicate logging.")]
    public class TextLog : KineticaRT.Channels.Channel, ISerializable 
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Logic.Message", "Message Messages");

        private string source = "";
		private string file_path = "" ;
		private KineticaRT.Channels.IChannel source_object ;
		private KineticaRT.Channels.ValueChangedEventHandler source_value_changed_handler ; 
		private StreamWriter stream ;
		string	format = "G" ; //"yyyy/MM/dd hh:mm:ss fff" ;
		System.Globalization.DateTimeFormatInfo dateInfo ;


		#region Construction
		/// <summary>
		/// Initialises a new instance of the class
		/// </summary>
		public TextLog ()
		{
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
			dateInfo = (System.Globalization.DateTimeFormatInfo) System.Globalization.DateTimeFormatInfo.CurrentInfo.Clone () ;
		}
		#endregion 

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData (info, context) ;
		}
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("Source", source) ;
			info.AddValue ("FilePath", file_path) ;
			info.AddValue ("Format", format) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected TextLog (SerializationInfo info, StreamingContext context):base(info, context)
		{
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
			dateInfo = (System.Globalization.DateTimeFormatInfo) System.Globalization.DateTimeFormatInfo.CurrentInfo.Clone () ;

			try	{source		= info.GetString ("Source") ;}
			catch (System.Runtime.Serialization.SerializationException){}
			
			try	{file_path		= info.GetString ("FilePath") ;}
			catch (System.Runtime.Serialization.SerializationException){}
			
			try	{format		= info.GetString ("Format") ;}
			catch (System.Runtime.Serialization.SerializationException){}
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
                if (source_object != null) source_object.ValueChanged -= source_value_changed_handler; // Added NS 20110803
                source_object = this.FindObject(source) as KineticaRT.Channels.Channel;
				if (source_object == null) source_object = OnObjectSearch (source) as KineticaRT.Channels.Channel ;
				if (source_object != null) source_object.ValueChanged += source_value_changed_handler ;

				if (stream != null) stream.Close () ;
                if (file_path != "")
                {
                    try
                    {
                        stream = new StreamWriter(file_path, true, Encoding.UTF8);
                    }
                    catch (System.IO.IOException e)
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Failed to open file: " + e.Message);
                        stream = null;
                    }
                }
				if (stream != null) stream.AutoFlush=true ;
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect () ;
			
			if (source_object != null)
			{
				source_object.ValueChanged -= source_value_changed_handler ;
				source_object = null ;
			}

			if (stream != null)
			{
				stream.Close () ;
				stream = null ;
			}
		}

		private void Source_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			this.Value = newVal ;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the format of the channel when viewed as text.
		/// </summary>
		/// <value>A string describing the display format of the channel.</value>
		/// <remarks>The following formats can be used:
		/// <ul>
		/// <li>"d" - Short date</li>
		/// <li>"D" - Long date</li>
		/// <li>"f" - Long date, short time</li>
		/// <li>"F" - Long date, long time</li>
		/// <li>"g" - short date, short time</li>
		/// <li>"G" - short date, long time</li>
		/// <li>"t" - short time</li>
		/// <li>"T" - Long time</li>
		/// <li>"u" - Universal time (sortable)</li>
		/// <li>"U" - Universal time</li>
		/// <li>"dddd yyyy/MM/dd hh:mm:ss fff" - Custom</li>
		/// </ul>
		/// </remarks>
		[System.ComponentModel.Description("Format of channel.  This is used to convert the value to text. Eg. d,D,f,F,g,G,t,T,u,U"), System.ComponentModel.Category("Numeric")] 
		public string Format
		{
			get
			{
				return format ;
			}
			set
			{
				if (format != value)
				{
					format = value ;
					OnPropertyChanged("Format") ;
				}
			}
		}

		/// <summary>Gets or sets the name of the channel to be logged.</summary>
		/// <value>A string containing the name of the channel to be logged.</value>
		/// <summary>
		/// Name of Channel to be logged.
		/// </summary>
		public string Source
		{
			get
			{
				return source ;
			}
			set
			{
				if (source != value)
				{
					source = value ;
					OnPropertyChanged("Source") ;
				}
			}
		}

		/// <summary>Gets or sets the path and name of the file to log to.</summary>
		/// <value>A string representing the path and name of file to log to.</value>
		/// <example>"C:\Logs\MyLog.txt"</example>
		public string FilePath
		{
			get
			{
				return file_path ;
			}
			set
			{
				if (file_path != value)
				{
					file_path = value ;
					OnPropertyChanged("FilePath") ;
				}
			}
		}

		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		/// <remarks>Setting Value will cause the new value to be logged to the file.</remarks>
		public override KineticaRT.Values.Value Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				base.Value = value ;

				if (value != null)
				{
					lock (this)
					{
						if (stream != null)
						{
							stream.WriteLine (value.ToString(format, dateInfo)) ;
						}
					}
				}
			}
		}
		#endregion
	}
}

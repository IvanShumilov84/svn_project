//using System;
//using KineticaRT;
//using System.Diagnostics ;
//using System.Runtime.Serialization ;
//using System.IO ;
//using System.Text ;
//using System.Xml.Serialization ;
//
//namespace KineticaRT.Logic.Message
//{
//	/// <summary>
//	/// Logs messages into an xml file.
//	/// </summary>
//	[Serializable]
//	public class XmlLog:KineticaRT.Channels.Channel, ISerializable 
//	{
//		private string source = "" ;
//		private string file_path = "" ;
//		private KineticaRT.Channels.IChannel source_object ;
//		private KineticaRT.Channels.ValueChangedEventHandler source_value_changed_handler ; 
//		private StreamWriter stream ;
//		private XmlSerializer serializer ;
//
//		#region Construction
//		/// <summary>
//		/// Initialises a new instance of the class
//		/// </summary>
//		public XmlLog ()
//		{
//			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
//			serializer = new XmlSerializer(typeof (Message));
//		}
//		#endregion 
//
//		#region ISerializable
//		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
//		{
//			GetObjectData (info, context) ;
//		}
//		/// <summary>Implements the ISerializable interface and returns the data needed to serialize the object.</summary>
//		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
//		{
//			base.GetObjectData (info, context) ;
//			info.AddValue ("Source", source) ;
//			info.AddValue ("FilePath", file_path) ;
//		}
//		/// <summary>Constructor for the ISerializable interface.</summary>
//		protected XmlLog (SerializationInfo info, StreamingContext context):base(info, context)
//		{
//			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
//
//			try
//			{
//				source		= info.GetString ("Source") ;
//				file_path	= info.GetString ("FilePath") ;
//			}
//			catch (System.Runtime.Serialization.SerializationException)
//			{
//				System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
//			}
//		}
//		#endregion
//
//		#region Connection
//		/// <summary>
//		/// Connects this object to others.
//		/// </summary>
//		public override void Connect ()
//		{
//			base.Connect () ;
//
//			lock (this)
//			{
//				source_object = this.FindObject (source) as KineticaRT.Channels.Channel ;
//				if (source_object == null) source_object = OnObjectSearch (source) as KineticaRT.Channels.Channel ;
//				if (source_object != null) source_object.ValueChanged += source_value_changed_handler ;
//
//				stream = new StreamWriter(file_path, true, Encoding.UTF8);
//				if (stream != null) stream.AutoFlush=true ;
//			}
//		}
//
//		/// <summary>
//		/// Disconnects this object from others.
//		/// </summary>
//		public override void Disconnect ()
//		{
//			base.Disconnect () ;
//			
//			lock (this)
//			{
//				if (source_object != null)
//				{
//					source_object.ValueChanged -= source_value_changed_handler ;
//					source_object = null ;
//				}
//
//				if (stream != null)
//				{
//					stream.Close () ;
//					stream = null ;
//				}
//			}
//		}
//
//		private void Source_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
//		{
//			this.Value = newVal ;
//		}
//
//		#endregion
//
//		#region Properties
//		/// <summary>Gets or sets the name of the channel to be logged.</summary>
//		/// <value>A string containing the name of the channel to be logged.</value>
//		/// <summary>
//		/// Name of Channel to be logged.
//		/// </summary>
//		public string Source
//		{
//			get
//			{
//				return source ;
//			}
//			set
//			{
//				if (source != value)
//				{
//					source = value ;
//					OnPropertyChanged("Source") ;
//				}
//			}
//		}
//
//		/// <summary>Gets or sets the path and name of the file to log to.</summary>
//		/// <value>A string representing the path and name of file to log to.</value>
//		/// <example>"C:\Logs\MyLog.txt"</example>
//		public string FilePath
//		{
//			get
//			{
//				return file_path ;
//			}
//			set
//			{
//				if (file_path != value)
//				{
//					file_path = value ;
//					OnPropertyChanged("FilePath") ;
//				}
//			}
//		}
//
//		/// <summary>Gets or sets the value of this channel.</summary>
//		/// <value>A Value object representing the value of the channel.</value>
//		/// <remarks>Setting Value will cause the new value to be logged to the file.</remarks>
//		public override KineticaRT.Values.Value Value
//		{
//			get
//			{
//				return base.Value ;
//			}
//			set
//			{
//				base.Value = value ;
//
//				if (value != null)
//				{
//					lock (this)
//					{
//						if (stream != null & serializer != null & value is Message)
//						{
//							serializer.Serialize(stream, value);
//						}
//					}
//				}
//			}
//		}
//		#endregion
//	}
//}

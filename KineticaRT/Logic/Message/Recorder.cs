using System;
using KineticaRT;
using System.Diagnostics ;
using System.Runtime.Serialization ;
using System.IO ;
using System.Text ;
using System.ComponentModel;

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// KineticaRT Channel that Records every change in a source channel.<br/>
	/// </summary>
	/// <remarks>
	/// A message is created for each change.  The message may be passed to other Message components for display, logging etc.
	/// </remarks>
	[Serializable]
	public class Recorder:KineticaRT.Channels.Channel, ISerializable 
	{
		private string	source = "" ;
		private string	destination = "" ;
		private string	source_text = "" ;
		private int		priority ;

		private KineticaRT.Channels.IChannel source_object ;
		private KineticaRT.Channels.IChannel destination_object ;
		private KineticaRT.Channels.ValueChangedEventHandler source_value_changed_handler ; 

		#region Construction
		/// <summary>
		/// Initialises a new instance of the class
		/// </summary>
		public Recorder()
		{
//			License license = null;
//			license = LicenseManager.Validate(this.GetType(), this);
//			if (license != null) {license.Dispose();license = null;}
			
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
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
			info.AddValue ("Destination", destination) ;
			info.AddValue ("SourceText", source_text) ;
			info.AddValue ("Priority", priority) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Recorder (SerializationInfo info, StreamingContext context):base(info, context)
		{
//			License license = null;
//			license = LicenseManager.Validate(this.GetType(), this);
//			if (license != null) {license.Dispose();license = null;}
			
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);

			try		{ source	= (string)info.GetValue("Source", typeof(string)); }
			catch	{ }

			try		{ destination	= (string)info.GetValue("Destination", typeof(string)); }
			catch	{ }

			try		{ source_text	= (string)info.GetValue("SourceText", typeof(string)); }
			catch	{ }

			try		{ priority	= (int)info.GetValue("Priority", typeof(int)); }
			catch	{ }

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
				source_object = this.FindObject (source) as KineticaRT.Channels.Channel ;
				if (source_object == null) source_object = OnObjectSearch (source) as KineticaRT.Channels.Channel ;
				
				if (source_object != null)
				{
					destination_object = this.FindObject (destination) as KineticaRT.Channels.Channel ;
					if (destination_object == null) destination_object = OnObjectSearch (destination) as KineticaRT.Channels.Channel ;

					source_object.ValueChanged += source_value_changed_handler ;
				}
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect () ;
			
			lock (this)
			{
				if (source_object != null)
				{
					source_object.ValueChanged -= source_value_changed_handler ;
					source_object = null ;
					destination_object = null ;
				}
			}
		}

		private void Source_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			lock (this)
			{
				Message message = new Message (newVal.ToString(), source_text, priority) ;
				Value = message ;
				if (destination_object != null) destination_object.Value = message ;
			}
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the channel to be recorded.
		/// </summary>
		/// <value>The name of the source channel.</value>
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

		/// <summary>
		/// Gets or sets the name of the destination channel.
		/// </summary>
		/// <value>The name of the destination <see cref="Channels.Channel"/>.</value>
		/// <remarks>
		/// Each message created will be placed in this channel.
		/// <para>This property may be left blank in which case the messages will only be available via the <see cref="Channels.Channel.Value"/> property.</para>
		/// </remarks> 
		public string Destination
		{
			get
			{
				return destination ;
			}
			set
			{
				if (destination != value)
				{
					destination = value ;
					OnPropertyChanged("Destination") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the text for the <see cref="Message.Source"/> property of recorded messages.
		/// </summary>
		/// <value>The text for the <see cref="Message.Source"/> property of recorded messages.</value>
		public string SourceText
		{
			get
			{
				return source_text ;
			}
			set
			{
				if (source_text != value)
				{
					source_text = value ;
					OnPropertyChanged("SourceText") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the value for the <see cref="Message.Priority"/> property of recorded messages.
		/// </summary>
		/// <value>The value for the <see cref="Message.Priority"/> property of recorded messages.</value>
		public int Priority
		{
			get
			{
				return priority ;
			}
			set
			{
				if (priority != value)
				{
					priority = value ;
					OnPropertyChanged("Priority") ;
				}
			}
		}
		#endregion
	}
}

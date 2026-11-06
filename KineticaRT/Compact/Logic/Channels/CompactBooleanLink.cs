using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;

namespace KineticaRT.Compact.Logic.Channels
{
	/// <summary>
	/// Channel containing a link to another Channel containing a boolean (True/False) value.<br/>
	/// </summary>
	/// <remarks>
	/// Can be connected to a Source channel from where it will receive its value.<br/>
	/// Can be connected to a Destination channel into which it will place its value.
	/// <para>If the Value is a type other than boolean then this class makes no attempt to convert it.  The Value property may for example contain error values or text values.</para>
	/// </remarks>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    public class BooleanLink : KineticaRT.Compact.Channels.BooleanChannel, ISerializable
	{
		bool connected ;
		string source ;
		string destination ;
        KineticaRT.Compact.Channels.IChannel sourceChannel;
        KineticaRT.Compact.Channels.IChannel destinationChannel;

		/// Initialises a new instance of the class.
		public BooleanLink()
		{
		}
		
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
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public BooleanLink (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try		{ source = (string)info.GetValue("Source", typeof(string)); }
			catch	{ }

			try		{ destination	= (string)info.GetValue("Destination", typeof(string)); }
			catch	{ }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Link")] 
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
		/// <value>The name of the destination channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a destination channel.
		/// <para>The channel will not connect to the destination until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of destination channel"),System.ComponentModel.Category("Link")] 
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

		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		[System.ComponentModel.Description("The value of this channel. "), System.ComponentModel.Category("Channel")] 
		public override Values.Value Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				if (destinationChannel != null) destinationChannel.Value = value ;
				base.Value = value ;
			}
		}
		#endregion

		#region Logic
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				if (connected && sourceChannel != null)sourceChannel.ValueChanged -= new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged);
				
				if (destination != null && destination.Length > 0)
				{
					destinationChannel = OnObjectSearch (destination) as KineticaRT.Compact.Channels.IChannel  ;
				}

				if (source != null && source.Length > 0)
				{
					sourceChannel = OnObjectSearch (source) as KineticaRT.Compact.Channels.IChannel  ;
			
					if (sourceChannel != null)
					{
						Value = sourceChannel.Value ;
						sourceChannel.ValueChanged += new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged);
					}
					else
					{
						Value = Values.Values.NotConnected ;
					}
				}

				connected = true ;
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
				if (connected)
				{
					sourceChannel.ValueChanged -= new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged);
					Value = Values.Values.NotConnected ;
					sourceChannel = null ;
					destinationChannel = null ;
					connected = false ;
				}
			}
		}

        private void This_OnValueChanged(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			Value = newVal ;
		}
		#endregion
	}
}

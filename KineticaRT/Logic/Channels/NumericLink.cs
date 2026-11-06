using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;
using KineticaRT.Channels;

namespace KineticaRT.Logic.Channels
{
	/// <summary>
	/// Channel containing a link to another Channel containing a numeric value.<br/>
	/// </summary>
	/// <remarks>
	/// Can be connected to a Source channel from where it will receive its value.<br/>
	/// Can be connected to a Destination channel into which it will place its value.
	/// <para>If the Value is a type other than numeric then this class makes no attempt to convert it.  The Value property may for example contain error values or text values.</para>
	/// </remarks>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    public class NumericLink : NumericChannel, INumericLinkChannel, ISerializable
	{
		string source ;
		string destination ;
		KineticaRT.Channels.IChannel sourceChannel ;
		KineticaRT.Channels.IChannel destinationChannel ;

		/// Initialises a new instance of the class.
		public NumericLink()
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
		protected NumericLink (SerializationInfo info, StreamingContext context):base(info, context)
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
		[Editor(typeof(KineticaRT.Studio.Application.ChannelSelectorEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
		[Editor(typeof(KineticaRT.Studio.Application.ChannelSelectorEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
		public override KineticaRT.Values.Value Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				if (destinationChannel != null) destinationChannel.Value = value ;
                //if (sourceChannel != destinationChannel || sourceChannel == null)//base.Value Sera updaté par le feedback voir source_OnValueChanged le la source = la destination
                {
                    base.Value = value;
                }
			}
		}
		#endregion

		#region Logic
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			

			lock (this)
			{
				if (this.Connected && sourceChannel != null)sourceChannel.ValueChanged -= new KineticaRT.Channels.ValueChangedEventHandler(source_OnValueChanged);
				
				if (destination != null && destination.Length > 0)
				{
					destinationChannel = OnObjectSearch (destination) as KineticaRT.Channels.IChannel  ;
                    if (this.destinationChannel == null) //ft
                    {
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Destination: " + this.destination);//ft
                    }
				}

				if (source != null && source.Length > 0)
				{
					sourceChannel = OnObjectSearch (source) as KineticaRT.Channels.IChannel  ;
			
					if (sourceChannel != null)
					{
                        sourceChannel.ValueChanged += new KineticaRT.Channels.ValueChangedEventHandler(source_OnValueChanged);//ft le handler doit etre attaché avant de setter la valeur pcq base.Value se se set sur le feedback
                        Value = sourceChannel.Value ;
                        
					}
					else
					{
						Value = KineticaRT.Values.Values.NotConnected ;
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Source: " + this.Source);//ft
					}
				}

                base.Connect();
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{	
			lock (this)
			{
				if (this.Connected)
				{
                    if (sourceChannel != null)
                    {
                        sourceChannel.ValueChanged -= new KineticaRT.Channels.ValueChangedEventHandler(source_OnValueChanged);
                        sourceChannel = null;
                    }
                    Value = KineticaRT.Values.Values.NotConnected;
					destinationChannel = null ;
                    base.Disconnect();
				}
			}
		}

		private void source_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			//Value = newVal ;
            if (source != destination)
            {
                this.Value = newVal;
            }
            else
            {
                base.Value = newVal;
            }
		}
		#endregion
	}
}

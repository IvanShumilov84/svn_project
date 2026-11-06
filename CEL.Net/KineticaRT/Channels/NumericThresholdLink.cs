using KineticaRT.Channels;
using KineticaRT.Logic.Channels;
using KineticaRT.Studio.Application;
using KineticaRT.Values;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace CEL.KineticaRT.Channels
{
	[Serializable]
	public class NumericThresholdLink : CEL.KineticaRT.Channels.NumericThresholdChannel, INumericLinkChannel
	{
		#region "Fields"

		//private bool						connected = false;
		private IChannel					destinationChannel;
		private IChannel					sourceChannel;
		private string						destination;
		private string						source;
		private ValueChangedEventHandler	srcHdlr;
		
		#endregion
		
		#region "Constructor"
		
		public NumericThresholdLink() : base()
		{
			this.srcHdlr = new ValueChangedEventHandler( this.source_OnValueChanged );
		}

		protected NumericThresholdLink(SerializationInfo info, StreamingContext context) : base( info, context )
		{
			if ((info == null)) {
				throw new ArgumentNullException("info");
			}
			try {
				this.source = info.GetString( "Source" );
			} catch (SerializationException exception1) {
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
			try {
				this.destination = info.GetString( "Destination" );
			} catch (SerializationException exception1) {
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
			this.srcHdlr = new ValueChangedEventHandler( this.source_OnValueChanged );
		}
		
		#endregion

		#region "Properties"
		
		[Category("Link"), Description("Name of destination channel."), Editor(typeof(ChannelSelectorEditor),typeof(UITypeEditor))]
		public string Destination
		{
			get
			{
				return this.destination;
			}
			set
			{
				if ( this.destination != value )
				{
					this.destination = value;
					this.OnPropertyChanged( "Destination" );
				}
			}
		}
		
		[Category("Link"), Description("Name of source channel."), Editor(typeof(ChannelSelectorEditor),typeof(UITypeEditor))]
		public string Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if ( this.source != value )
				{
					this.source = value;
					this.OnPropertyChanged( "Source" );
				}
			}
		}
		
		[Category("Channel"), Description("The value of this channel.")]
		public override Value Value
		{
			/*get
			{
				return base.Value;
			}*/
			get
			{
				if ( this.sourceChannel == null && this.destinationChannel != null )
				{
					return this.destinationChannel.Value;
				}
				else		
					return base.Value;
			}
			set
			{
				if ( this.destinationChannel != null )
				{
					this.destinationChannel.Value = value;
				}
                if (sourceChannel != destinationChannel || sourceChannel == null)//base.Value Sera updaté par le feedback voir source_OnValueChanged le la source = la destination
                {
					base.Value = value;
				}
			}
		}

		#endregion
		
		#region "Methods"
		
		public override void Connect()
		{
			
			
			lock( this )
			{
				if ( this.Connected && ( this.sourceChannel != null ) )
				{
					this.sourceChannel.ValueChanged -= this.srcHdlr;
				}
				if ( ( this.destination != null ) && ( this.destination.Length > 0 ) )
				{
					this.destinationChannel = base.OnObjectSearch( this.destination ) as IChannel;
                    if (this.destinationChannel == null ) //ft
                    {
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Destination: " + this.destination);//ft
                    }
				}
				if ( ( this.source != null ) && ( this.source.Length > 0 ) )
				{
					this.sourceChannel = base.OnObjectSearch( this.source ) as IChannel;
					if ( this.sourceChannel != null )
					{
                        this.sourceChannel.ValueChanged += this.srcHdlr; //ft le handler doit etre attaché avant de setter la valeur pcq base.Value se se set sur le feedback
                        this.Value = this.sourceChannel.Value;
                        base.Value = this.sourceChannel.Value;
					}
					else
					{
						this.Value = Values.NotConnected;
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel: " + this.Name + ": Source: " + this.source);//ft
					}
				}
				else
				{
					this.Value = Values.NotConnected;
				}
                base.Connect();
			}
		}

		public override void Disconnect()
		{
			
			lock( this )
			{
				if ( this.Connected )
				{
					if ( this.sourceChannel != null )
					{
						this.sourceChannel.ValueChanged -= this.srcHdlr;
						this.Value = Values.NotConnected;
						this.sourceChannel = null;
						this.destinationChannel = null;
                        base.Disconnect();
					}
				}
			}
		}
		
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if ((info == null)) {
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue( "Source", this.source );
			info.AddValue( "Destination", this.destination );
		}

		private void source_OnValueChanged( IChannel sender, Value newVal )
		{
             if (sourceChannel != destinationChannel)//ft Pour eviter des loop de call réentrant
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

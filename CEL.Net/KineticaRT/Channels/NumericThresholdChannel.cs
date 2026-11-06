/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 6/28/2006
 * Time: 4:52 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using KTC = KineticaRT.Channels;
using KTV = KineticaRT.Values;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CEL.KineticaRT.Channels
{
	[Serializable]
	public class NumericThresholdChannel : KTC.NumericThresholdChannel
	{
		#region "Fields"

		//private bool 							connected = false;
		private KTC.IChannel 					highSourceChannel;
		private KTC.IChannel 					highHighSourceChannel;
		private KTC.IChannel 					lowSourceChannel;
		private KTC.IChannel 					lowLowSourceChannel;
		private string 							highSource;
		private string 							highHighSource;
		private string 							lowSource;
		private string 							lowLowSource;
		private KTC.ValueChangedEventHandler	lowLowSrcHdlr;
		private KTC.ValueChangedEventHandler	lowSrcHdlr;
		private KTC.ValueChangedEventHandler	highSrcHdlr;
		private KTC.ValueChangedEventHandler	highHighSrcHdlr;
		
		#endregion

		#region "Constructor"
		
		public NumericThresholdChannel() : base()
		{
			this.highSource					= string.Empty;
			this.highHighSource				= string.Empty;
			this.lowSource					= string.Empty;
			this.lowLowSource				= string.Empty;
			this.lowLowSrcHdlr				= new KTC.ValueChangedEventHandler( this.Threshold_LowLow_OnValueChanged );
			this.lowSrcHdlr					= new KTC.ValueChangedEventHandler( this.Threshold_Low_OnValueChanged );
			this.highSrcHdlr				= new KTC.ValueChangedEventHandler( this.Threshold_High_OnValueChanged );
			this.highHighSrcHdlr			= new KTC.ValueChangedEventHandler( this.Threshold_HighHigh_OnValueChanged );
		}

		protected NumericThresholdChannel( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			if ( info == null )
			{
				throw new ArgumentNullException( "info" );
			}
			try
			{
				this.highSource					= info.GetString( "HighSource" );
				this.highHighSource				= info.GetString( "HighHighSource" );
				this.lowSource					= info.GetString( "LowSource" );
				this.lowLowSource				= info.GetString( "LowLowSource" );
			}
			catch ( SerializationException exception1 )
			{
				Trace.WriteLine( exception1.Message, base.GetType().ToString() );
			}
			this.lowLowSrcHdlr				= new KTC.ValueChangedEventHandler( this.Threshold_LowLow_OnValueChanged );
			this.lowSrcHdlr					= new KTC.ValueChangedEventHandler( this.Threshold_Low_OnValueChanged );
			this.highSrcHdlr				= new KTC.ValueChangedEventHandler( this.Threshold_High_OnValueChanged );
			this.highHighSrcHdlr			= new KTC.ValueChangedEventHandler( this.Threshold_HighHigh_OnValueChanged );
		}
		
		#endregion
		
		#region "Properties"
	
		[Category("Threshold Source"),Description("The High threshold source channel.")]
		public String HighSource
		{
			get
			{
				return this.highSource;
			}
			set
			{
				if ( value != this.highSource )
				{
					this.highSource = value;
				}
			}
		}
				
		[Category("Threshold Source"),Description("The HighHigh threshold source channel.")]
		public String HighHighSource
		{
			get
			{
				return this.highHighSource;
			}
			set
			{
				if ( value != this.highHighSource )
				{
					this.highHighSource = value;
				}
			}
		}
				
		[Category("Threshold Source"),Description("The Low threshold source channel.")]
		public String LowSource
		{
			get
			{
				return this.lowSource;
			}
			set
			{
				if ( value != this.lowSource )
				{
					this.lowSource = value;
				}
			}
		}
				
		[Category("Threshold Source"),Description("The LowLow threshold source channel.")]
		public String LowLowSource
		{
			get
			{
				return this.lowLowSource;
			}
			set
			{
				if ( value != this.lowLowSource )
				{
					this.lowLowSource = value;
				}
			}
		}

		#endregion
		
		#region "Methods"
	
		public override void Connect()
		{
			
			lock( this )
			{
				if ( this.Connected && this.Value != KTV.Values.NotConnected) //ft 
				{
					if ( this.highSourceChannel != null )
					{
						this.highSourceChannel.ValueChanged -= this.highSrcHdlr;
						this.highSourceChannel = null;
					}
					if ( this.highHighSourceChannel != null )
					{
						this.highHighSourceChannel.ValueChanged -= this.highHighSrcHdlr;
						this.highHighSourceChannel = null;
					}
					if ( this.lowSourceChannel != null )
					{
						this.lowSourceChannel.ValueChanged -= this.lowSrcHdlr;
						this.lowSourceChannel = null;
					}
					if ( this.lowLowSourceChannel != null )
					{
						this.lowLowSourceChannel.ValueChanged -= this.lowLowSrcHdlr;
						this.lowLowSourceChannel = null;
					}
				}
				
				if ( ( this.highSource != null ) && ( this.highSource.Length > 0 ) )
				{
					this.highSourceChannel = base.OnObjectSearch( this.highSource ) as KTC.IChannel;
					if ( this.highSourceChannel != null )
					{
						try
						{
							this.High = this.highSourceChannel.Value.ToDouble();
						}
						catch ( System.NullReferenceException )
						{
						}
							
						this.highSourceChannel.ValueChanged += this.highSrcHdlr;
					}
                    else //ft
                    {   
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel : " + this.Name + " highSourceChannel: " + this.highSource );
                    }
				}
				if ( ( this.highHighSource != null ) && ( this.highHighSource.Length > 0 ) )
				{
					this.highHighSourceChannel = base.OnObjectSearch( this.highHighSource ) as KTC.IChannel;
					if ( this.highHighSourceChannel != null )
					{
						try
						{
							this.HighHigh = this.highHighSourceChannel.Value.ToDouble();
						}
						catch ( System.NullReferenceException )
						{
						}
							
						this.highHighSourceChannel.ValueChanged += this.highHighSrcHdlr;
					}
                    else //ft
                    {
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel : " + this.Name + " highHighSourceChannel: " + this.highHighSource);
                    }
				}
				if ( ( this.lowSource != null ) && ( this.lowSource.Length > 0 ) )
				{
					this.lowSourceChannel = base.OnObjectSearch( this.lowSource ) as KTC.IChannel;
					if ( this.lowSourceChannel != null )
					{
						try
						{
							this.Low = this.lowSourceChannel.Value.ToDouble();
						}
						catch ( System.NullReferenceException )
						{
						}
							
						this.lowSourceChannel.ValueChanged += this.lowSrcHdlr;
					}
                    else //ft
                    {
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel : " + this.Name + " lowSourceChannel: " + this.lowSource);
                    }
				}
				if ( ( this.lowLowSource != null ) && ( this.lowLowSource.Length > 0 ) )
				{
					this.lowLowSourceChannel = base.OnObjectSearch( this.lowLowSource ) as KTC.IChannel;
					if ( this.lowLowSourceChannel != null )
					{
						try
						{
							this.LowLow = this.lowLowSourceChannel.Value.ToDouble();
						}
						catch ( System.NullReferenceException )
						{
						}
							
						this.lowLowSourceChannel.ValueChanged += this.lowLowSrcHdlr;
					}
                    else //ft
                    {
                        System.Diagnostics.Trace.WriteLine("Unable to connect channel : " + this.Name + " lowLowSourceChannel: " + this.lowLowSource);
                    }
				}
                base.Connect();
			}
		}

		public override void Disconnect()
		{
			if ( this.Connected )
			{
				if ( this.highSourceChannel != null )
				{
					this.highSourceChannel.ValueChanged -= this.highSrcHdlr;
					this.highSourceChannel = null;
				}
				if ( this.highHighSourceChannel != null )
				{
					this.highHighSourceChannel.ValueChanged -= this.highHighSrcHdlr;
					this.highHighSourceChannel = null;
				}
				if ( this.lowSourceChannel != null )
				{
					this.lowSourceChannel.ValueChanged -= this.lowSrcHdlr;
					this.lowSourceChannel = null;
				}
				if ( this.lowLowSourceChannel != null )
				{
					this.lowLowSourceChannel.ValueChanged -= this.lowLowSrcHdlr;
					this.lowLowSourceChannel = null;
				}
                base.Disconnect();
			}
			
		}
		
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			if ( info == null )
				throw new ArgumentNullException( "info" );
			base.GetObjectData( info, context );
			info.AddValue( "LowSource", this.lowSource );
			info.AddValue( "LowLowSource", this.lowLowSource );
			info.AddValue( "HighSource", this.highSource );
			info.AddValue( "HighHighSource", this.highHighSource );
		}
		
		private void Threshold_High_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			this.High = newVal.ToDouble();
		}
		
		private void Threshold_HighHigh_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			this.HighHigh = newVal.ToDouble();
		}
		
		private void Threshold_Low_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			this.Low = newVal.ToDouble();
		}
		
		private void Threshold_LowLow_OnValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			this.LowLow = newVal.ToDouble();
		}
		
		#endregion
	}
}

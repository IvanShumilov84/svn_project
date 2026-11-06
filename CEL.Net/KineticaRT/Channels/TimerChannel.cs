/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 7/5/2006
 * Time: 9:06 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using KT = KineticaRT;
using KTC = KineticaRT.Channels;
using KTV = KineticaRT.Values;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CEL.KineticaRT.Channels
{
	/// <summary>
	/// Channel which has an elapse time as its Value.
	/// </summary>
	[Serializable]
	public class TimerChannel : KTC.InfoChannel
	{
		public enum Mode
		{
			Stoped,
			Started
		}
		
		#region Fields

		private System.TimeSpan					accumulatedTime = System.TimeSpan.Zero;
		private Mode							currentMode = Mode.Stoped;
		private string							resetChannelName = string.Empty;
		private KTC.IChannel					resetChannel;
		private KTC.ValueChangedEventHandler	resetValueChangedHdlr;
		private string							startChannelName = string.Empty;
		private KTC.IChannel					startChannel;
		private System.DateTime					startDateTime;
		private KTC.ValueChangedEventHandler	startValueChangedHdlr;
		private string							stopChannelName = string.Empty;
		private KTC.IChannel					stopChannel;
		private KTC.ValueChangedEventHandler	stopValueChangedHdlr;
		private KT.Updater.UpdateEventHandler	tickUpdateHdlr;
		
		#endregion
		
		#region Constructors
		
		public TimerChannel()
		{
			this.Initialize();
		}
		
		public TimerChannel( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			this.Initialize();
			if ((info == null)) {
				throw new ArgumentNullException("info");
			}
			try {
				this.resetChannelName = info.GetString( "Reset" );
			} catch (SerializationException exception1) {
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
			try {
				this.startChannelName = info.GetString( "Start" );
			} catch (SerializationException exception1) {
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
			try {
				this.stopChannelName = info.GetString( "Stop" );
			} catch ( SerializationException exception1 ) {
				Trace.WriteLine( exception1.Message, base.GetType().ToString());
			}
		}
		
		#endregion
		
		#region Properties

		/// <summary>
		/// Name of Boolean channel that will reset the timer.
		/// </summary>
		[Category("Timer"),DefaultValue(""),Description("Name of Boolean channel that will reset the timer.")]
		public string Reset
		{
			get
			{
				return this.resetChannelName;
			}
			set
			{
				if ( this.resetChannelName != value )
				{
					this.resetChannelName = value;
					if ( ( this.resetChannelName != null ) && ( this.resetChannelName.Length > 0 ) )
					{
						this.ResetChannel = base.OnObjectSearch( value ) as KTC.IChannel;
					}
					this.OnPropertyChanged( "Reset" );
				}
			}
		}
		
		private KTC.IChannel ResetChannel
		{
			set
			{
				if ( this.resetChannel != null )
				{
					this.resetChannel.ValueChanged -= this.resetValueChangedHdlr;
				}
				this.resetChannel = value;
				if ( this.resetChannel != null )
				{
					this.resetChannel.ValueChanged += this.resetValueChangedHdlr;
				}
			}
		}
		
		/// <summary>
		/// Name of Boolean channel that will start the timer.
		/// </summary>
		[Category("Timer"),DefaultValue(""),Description("Name of Boolean channel that will start the timer.")]
		public string Start
		{
			get
			{
				return this.startChannelName;
			}
			set
			{
				if ( this.startChannelName != value )
				{
					this.startChannelName = value;
					if ( ( this.startChannelName != null ) && ( this.startChannelName.Length > 0 ) )
					{
						this.StartChannel = base.OnObjectSearch( value ) as KTC.IChannel;
					}
					this.OnPropertyChanged( "Start" );
				}
			}
		}
		
		private KTC.IChannel StartChannel
		{
			set
			{
				if ( this.startChannel != null )
				{
					this.startChannel.ValueChanged -= this.startValueChangedHdlr;
				}
				this.startChannel = value;
				if ( this.startChannel != null )
				{
					this.startChannel.ValueChanged += this.startValueChangedHdlr;
				}
				
			}
		}
		
		/// <summary>
		/// Name of Boolean channel that will stop the timer.
		/// </summary>
		[Category("Timer"),DefaultValue(""),Description("Name of Boolean channel that will stop the timer.")]
		public string Stop
		{
			get
			{
				return this.stopChannelName;
			}
			set
			{
				if ( this.stopChannelName != value )
				{
					this.stopChannelName = value;
					if ( ( this.stopChannelName != null ) && ( this.stopChannelName.Length > 0 ) )
					{
						this.StopChannel = base.OnObjectSearch( value ) as KTC.IChannel;
					}
					this.OnPropertyChanged( "Stop" );
				}
			}
		}
		
		private KTC.IChannel StopChannel
		{
			set
			{
				if ( this.stopChannel != null )
				{
					this.stopChannel.ValueChanged -= this.stopValueChangedHdlr;
				}
				this.stopChannel = value;
				if ( this.stopChannel != null )
				{
					this.stopChannel.ValueChanged += this.stopValueChangedHdlr;
				}
				
			}
		}
		
		#endregion
		
		#region Methods

		public override void Connect()
		{
			
			lock( this )
			{
				if ( !this.Connected )
				{
					if ( ( this.resetChannelName != null ) && ( this.resetChannelName.Length > 0 ) )
					{
						this.ResetChannel = base.OnObjectSearch( this.resetChannelName ) as KTC.IChannel;
					}
					if ( ( this.startChannelName != null ) && ( this.startChannelName.Length > 0 ) )
					{
						this.StartChannel = base.OnObjectSearch( this.startChannelName ) as KTC.IChannel;
					}
					if ( ( this.stopChannelName != null ) && ( this.stopChannelName.Length > 0 ) )
					{
						this.StopChannel = base.OnObjectSearch( this.stopChannelName ) as KTC.IChannel;
					}
					KT.Services.FastUpdater.Update += this.tickUpdateHdlr;

                    base.Connect();
				}
			}
		}
		
		public override void Disconnect()
		{
			lock( this )
			{
				if ( this.Connected )
				{
					KT.Services.FastUpdater.Update -= this.tickUpdateHdlr;
					this.ResetChannel = null;
					this.StartChannel = null;
					this.StopChannel = null;
                    base.Disconnect();
				}
			}
		}
		
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			if ((info == null)) {
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue( "Reset", this.resetChannelName );
			info.AddValue( "Start", this.startChannelName );
			info.AddValue( "Stop", this.stopChannelName );
		}

		private void Initialize()
		{
			this.resetValueChangedHdlr = new KTC.ValueChangedEventHandler( this.OnResetValueChanged );
			this.startValueChangedHdlr = new KTC.ValueChangedEventHandler( this.OnStartValueChanged );
			this.stopValueChangedHdlr = new KTC.ValueChangedEventHandler( this.OnStopValueChanged );
			this.tickUpdateHdlr = new KT.Updater.UpdateEventHandler( this.OnTickUpdateEvent );
			this.Value = new TimeSpan( System.TimeSpan.Zero );
		}
		
		private void OnResetValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			lock( this )
			{
				if ( newVal.Equals( KTV.Values.True ) )
				{
					if ( this.currentMode == Mode.Started )
					{
						this.startDateTime = System.DateTime.Now;
					}	
					this.accumulatedTime = System.TimeSpan.Zero;
					this.Value = new TimeSpan( System.TimeSpan.Zero );
				}
			}
		}
		
		private void OnStartValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			lock( this )
			{
				if ( newVal.Equals( KTV.Values.True ) )
				{
					if ( this.currentMode != Mode.Started )
					{
						this.startDateTime = System.DateTime.Now;
						this.Value = new TimeSpan( this.accumulatedTime );
						this.currentMode = Mode.Started;
					}
				}
			}
		}

		private void OnStopValueChanged( KTC.IChannel sender, KTV.Value newVal )
		{
			lock( this )
			{
				if ( newVal.Equals( KTV.Values.True ) )
				{
					if ( this.currentMode == Mode.Started )
					{
						this.accumulatedTime+= System.DateTime.Now - this.startDateTime;
						this.Value = new TimeSpan( this.accumulatedTime );
					}
					this.currentMode = Mode.Stoped;
				}
			}
		}
		
		private void OnTickUpdateEvent()
		{
			lock( this )
			{
				if ( this.currentMode == Mode.Started )
				{
					this.Value = new TimeSpan( this.accumulatedTime + ( System.DateTime.Now - this.startDateTime ) );
				}
			}
		}

		#endregion
	}

	[Serializable]
	public class TimeSpan : KTV.Value, IConvertible, IFormattable
	{
		#region Fields
		
		private System.TimeSpan	val;
		
		#endregion
		
		#region Constructor
		
		public TimeSpan( System.TimeSpan value )
		{
			this.val = value;
		}
		
		#endregion
		
		#region Methods

		public override int CompareTo( object obj )
		{
			if ( ( obj == null ) || ( this.GetType() != obj.GetType() ) )
			{
				return 1;
			}
			return this.val.CompareTo( ((TimeSpan)obj).val );
		}

		public override bool Equals( object obj )
		{
			if ( ( obj == null ) || ( this.GetType() != obj.GetType() ) )
			{
				return false;
			}
			TimeSpan time1 = (TimeSpan)obj;
			return this == time1;
		}

		public override int GetHashCode()
		{
			return this.val.GetHashCode();
		}
		
		public TypeCode GetTypeCode()
		{
			return Convert.GetTypeCode( this.val );
		}

		public static bool operator==( TimeSpan value1, TimeSpan value2 )
		{
			return ( value1.val == value2.val );
		}

		public static bool operator!=( TimeSpan value1, TimeSpan value2 )
		{
			return ( ! ( value1 == value2 ) );
		}
		
		public bool ToBoolean( IFormatProvider provider )
		{
			return Convert.ToBoolean( this.val, provider );
		}
		
		public byte ToByte( IFormatProvider provider )
		{
			return Convert.ToByte( this.val, provider );
		}
		
		public char ToChar( IFormatProvider provider )
		{
			return Convert.ToChar( this.val, provider );
		}

		public System.DateTime ToDateTime( IFormatProvider provider )
		{
			return Convert.ToDateTime( this.val, provider );
		}
		
		public decimal ToDecimal( IFormatProvider provider )
		{
			return Convert.ToDecimal( this.val, provider );
		}
		
		public override double ToDouble()
		{
			return this.val.TotalSeconds;
		}
		
		public double ToDouble( IFormatProvider provider )
		{
			return Convert.ToDouble( this.val, provider );
		}
		
		public short ToInt16( IFormatProvider provider )
		{
			return Convert.ToInt16( this.val, provider );
		}
		
		public int ToInt32( IFormatProvider provider )
		{
			return Convert.ToInt32( this.val, provider );
		}
		
		public long ToInt64( IFormatProvider provider )
		{
			return Convert.ToInt64( this.val, provider );
		}
		
		public sbyte ToSByte( IFormatProvider provider )
		{
			return Convert.ToSByte( this.val, provider );
		}

		public float ToSingle( IFormatProvider provider )
		{
			return Convert.ToSingle( this.val, provider );
		}

		public override string ToString()
		{
			return this.val.ToString();
		}
		
		public string ToString( IFormatProvider provider )
		{
			return Convert.ToString( this.val, provider );
		}
		
		public override string ToString( string format, IFormatProvider provider )
		{
			return Convert.ToString( this.val, provider );
		}

		public object ToType( Type conversionType, IFormatProvider provider )
		{
			return Convert.ChangeType( this.val, conversionType, provider );
		}
		
		public ushort ToUInt16( IFormatProvider provider )
		{
			return Convert.ToUInt16( this.val, provider );
		}
		
		public uint ToUInt32( IFormatProvider provider )
		{
			return Convert.ToUInt32( this.val, provider );
		}
		
		public ulong ToUInt64( IFormatProvider provider )
		{
			return Convert.ToUInt64( this.val, provider );
		}
		
		#endregion
	}
}

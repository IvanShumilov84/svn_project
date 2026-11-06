/*
 * Created by SharpDevelop.
 * User: francoist
 * Date: 11/15/2006
 * Time: 8:43 AM
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
using System.Threading;


namespace CEL.KineticaRT.Channels
{
	/// <summary>
	/// Channel which has an elapse time as its Value.
	/// </summary>
	[Serializable]
	public class PulseChannel : KTC.InfoChannel
	{
//		public enum Mode
//		{
//			Stoped,
//			Started
//		}
		
		#region Fields

		private int						pulseDelayOff = 100; //in ms
		private int						pulseDelayOn = 100; //in ms
		private Thread 					pulseThread = null;
        private bool                    exitThread = false; //ft
		
		#endregion
		
		#region Constructors
		
		public PulseChannel()
		{
			this.Initialize();
			this.pulseDelayOff = 200;
			this.pulseDelayOn = 200;
		}
		
		public PulseChannel( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			this.Initialize();
			if ((info == null)) 
			{
				throw new ArgumentNullException("info");
			}
			try
			{
				this.pulseDelayOff	= info.GetInt32( "PulseDelayOff" );
				this.pulseDelayOn	= info.GetInt32( "PulseDelayOn" );
			}
			catch (SerializationException exception1)
			{
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
		}
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			if ((info == null)) 
			{
				throw new ArgumentNullException("info");
			}
			try
			{
				base.GetObjectData(info, context);
				info.AddValue( "PulseDelayOff", this.pulseDelayOff );
				info.AddValue( "PulseDelayOn", this.pulseDelayOn );
			}
			catch (SerializationException exception1)
			{
				Trace.WriteLine(exception1.Message, base.GetType().ToString());
			}
		}
		
		#endregion
		
		#region Properties

		/// <summary>
		/// Name of Boolean channel that will reset the timer.
		/// </summary>

		[Category("Pulse"),DefaultValue(""),Description("Delay of the pulse in ms.")]
		public int PulseDelayOff
		{
			get
			{
				return this.pulseDelayOff;
			}
			set
			{
				this.pulseDelayOff = value;
				this.OnPropertyChanged( "PulseDelayOff" );
			}
		}
		[Category("Pulse"),DefaultValue(""),Description("Delay of the pulse in ms.")]
		public int PulseDelayOn
		{
			get
			{
				return this.pulseDelayOn;
			}
			set
			{
				this.pulseDelayOn = value;
				this.OnPropertyChanged( "PulseDelayOn" );
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
					pulseThread = new Thread(new ThreadStart(this.ToggleThread));
					pulseThread.Name = "PulseThread";
					pulseThread.Priority = ThreadPriority.Lowest;
					pulseThread.Start();
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
                    
                    //'Kill the Event Archiver Thread if still alive
                    exitThread = true; //ft pour eviter une exception et quitter le thread normalement
                    for(int numberIteration = 0 ; numberIteration< 2000;numberIteration++) //ft
                    {
                        System.Threading.Thread.Sleep(25); //'give a chance to the other thread to complete 2000*25ms = 10 seconds

                        //'already stop, do nothing
                        if( pulseThread == null) 
                            return;
            
                        if(! pulseThread.IsAlive)
                            break;
                    }

					//Kill the Event Archiver Thread if still alive
					if (pulseThread.IsAlive)
					{

						pulseThread.Abort();
						pulseThread.Join();
						pulseThread = null;
					}
                    base.Disconnect();
				}
			}
		}
		

		private void Initialize()
		{
			this.Value = KT.Values.Values.False;
		}

		private void ToggleThread()
		{
			try
			{
				//Infinite loop (Thread is aborted when disconnecting
                while (!exitThread) 
				{
					Thread.Sleep(25);
					if(base.Value.Equals(KT.Values.Values.False))
					{
						base.Value = KT.Values.Values.True;
						Thread.Sleep(this.PulseDelayOn);
					}
					else
					{
						base.Value = KT.Values.Values.False;
						Thread.Sleep(this.PulseDelayOff);
					}
				}	
			}
			catch (SerializationException exception1)
			{
				Trace.WriteLine( exception1.Message, base.GetType().ToString());
			}
		}

		#endregion
	}
}

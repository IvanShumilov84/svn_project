using System;
using System.Runtime.Serialization ;
using System.Security.Permissions;
using System.Diagnostics;

namespace KineticaRT.Servers.Simulate
{
	/// <summary>
	/// Generates a simulated sawtooth shaped wave
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class SawToothWave:KineticaRT.Channels.NumericThresholdChannel, ISerializable 
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Simulate", "Simulation");
		double								period = 2 ;
		double								phase ;
		double								minimum ;
		double								maximum = 1 ;
		EventPool.UpdateEventHandler		hdlr ;
		static EventPool					pool = CommonEventPool.Pool ;
		bool								connected ;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		public SawToothWave()
		{
			base.Maximum = maximum ;
			base.Minimum = minimum ;

			hdlr = new EventPool.UpdateEventHandler(update_proc);
		}

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
            info.AddValue("Period", period);
            info.AddValue("Phase", phase);
            info.AddValue("Minimum", minimum);
            info.AddValue("Maximum", maximum);
        }
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected SawToothWave (SerializationInfo info, StreamingContext context):base(info, context)
		{
            if (info == null) throw new ArgumentNullException("info");

            try
			{
				period = info.GetDouble ("Period") ;
				phase = info.GetDouble ("Phase") ;
				minimum = info.GetDouble ("Minimum") ;
				maximum = info.GetDouble ("Maximum") ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Configuration is missing a property for " + GetType(), this.Name);
            }

			base.Maximum = maximum ;
			base.Minimum = minimum ;

			hdlr = new EventPool.UpdateEventHandler(update_proc);
		}
		#endregion

		/// <summary>
		/// Called from EventPool Worker thread. Updates the value.
		/// </summary>
		private void update_proc(double newValue)
		{
			Value = new KineticaRT.Values.Double ((((((double)newValue) + (period * phase)) % period) / period) * (maximum - minimum) + minimum) ;
		}

		#region Connection
		/// <summary>
		/// Starts sawtooth wave generation.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				if (!connected)
				{
					pool.Update += hdlr ;
					connected = true ;
				}
			}		
		}

		/// <summary>
		/// Stops sawtooth wave generation.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect () ;
			
			lock (this)
			{
				if (connected)
				{
					pool.Update -= hdlr ;
					connected = false ;
				}
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the minimum value of the sawtooth wave.</summary>
		/// <value>Minimum value of the sawtooth wave.</value>
		public new double Minimum
		{
			get
			{
				return minimum ;
			}
			set
			{
				if (minimum != value)
				{
					base.Minimum = value ;
					minimum = value ;
					OnPropertyChanged("Minimum") ;
				}
			}
		}

		/// <summary>Gets or sets the maximum value of the sawtooth wave.</summary>
		/// <value>Maximum value of the sawtooth wave.</value>
		public new double Maximum
		{
			get
			{
				return maximum ;
			}
			set
			{
				if (maximum != value)
				{
					base.Maximum = value ;
					maximum = value ;
					OnPropertyChanged("Maximum") ;
				}
			}
		}

		/// <summary>Gets or sets the period of the sawtooth wave.</summary>
		/// <value>Period of the sawtooth wave in seconds.</value>
		public double Period
		{
			get
			{
				return period ;
			}
			set
			{
				if (value < 0.01) value = 0.01 ;

				if (period != value)
				{
					period = value ;
					OnPropertyChanged("Period") ;
				}
			}
		}

		/// <summary>Gets or sets the phase of the sawtooth wave relative to other waves.</summary>
		/// <value>Phase of this sawtooth wave relative to other waves as a fraction.</value>
		public double Phase
		{
			get
			{
				return phase ;
			}
			set
			{
				if (phase != value)
				{
					phase = value ;
					OnPropertyChanged("Phase") ;
				}
			}
		}
		#endregion
	}
}

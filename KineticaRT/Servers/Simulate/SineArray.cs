using System;
using System.Runtime.Serialization ;
using System.Security.Permissions;
using System.Diagnostics;

namespace KineticaRT.Servers.Simulate
{
	/// <summary>
	/// Generates an array of simulated sine waves.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class SineArray:KineticaRT.Channels.NumericChannel, ISerializable 
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Simulate", "Simulation");

        double								period = 2 ;
		double								phase ;
		double								minimum ;
		double								maximum = 1 ;
		double								phaseOffset = 0.01;
		int									noOfElements = 1 ;

		EventPool.UpdateEventHandler		hdlr ;
		static EventPool					pool = new EventPool () ;
		bool								connected ;

		/// <summary>
		/// Initialises an instance of the class
		/// </summary>
		public SineArray ()
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
            info.AddValue("PhaseOffset", phaseOffset);
            info.AddValue("NoOfElements", noOfElements);
        }
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected SineArray (SerializationInfo info, StreamingContext context):base(info, context)
		{
            if (info == null) throw new ArgumentNullException("info");

            try
			{
				period = info.GetDouble ("Period") ;
				phase = info.GetDouble ("Phase") ;
				minimum = info.GetDouble ("Minimum") ;
				maximum = info.GetDouble ("Maximum") ;
				phaseOffset = info.GetDouble ("PhaseOffset") ;
				noOfElements = info.GetInt32 ("NoOfElements") ;
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
			double [] val ;
			val = new Double [noOfElements] ;
			for (int index = 0 ; index < noOfElements ; index ++)
			{
				val [index] = (((Math.Sin (((((double)newValue) + period * (phase + (phaseOffset * index))) * 2 * Math.PI) / period)) + 1) / 2) * (maximum - minimum) + minimum ;
			}
			Value = new KineticaRT.Values.DoubleArray (val) ;
		}
		
		#region Connection
		/// <summary>
		/// Starts generating sine waves.
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
		/// Stops genrating sine waves
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
		/// <summary>Gets or sets the minimum value of the sine wave.</summary>
		/// <value>Minimum value of the sine wave.</value>
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

		/// <summary>Gets or sets the maximum value of the sine wave.</summary>
		/// <value>Maximum value of the sine wave.</value>
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

		/// <summary>Gets or sets the period of the sine wave.</summary>
		/// <value>Period of the sine wave in seconds.</value>
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

		/// <summary>Gets or sets the number of elements to be placed in the array.</summary>
		/// <value>Number of elements in the array.</value>
		public int NoOfElements
		{
			get
			{
				return noOfElements ;
			}
			set
			{
				if (noOfElements != value)
				{
					noOfElements = value ;
					OnPropertyChanged("NoOfElements") ;
				}
			}
		}

		/// <summary>Gets or sets the phase of the sinewave relative to other waves.</summary>
		/// <value>Phase of this sine wave relative to other waves as a fraction.</value>
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

		/// <summary>Gets or sets the phase offset of consecutive elements in the array.</summary>
		/// <value>Phase offset of consecutive elements in the array.</value>
		public double PhaseOffset
		{
			get
			{
				return phaseOffset ;
			}
			set
			{
				if (phaseOffset != value)
				{
					phaseOffset = value ;
					OnPropertyChanged("PhaseOffset") ;
				}
			}
		}
		#endregion
	}
}

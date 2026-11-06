using System;
using System.Runtime.Serialization ;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Diagnostics;

namespace KineticaRT.Servers.Simulate
{
	/// <summary>
	/// Generates simulated noise.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class Noise:KineticaRT.Channels.NumericThresholdChannel, ISerializable 
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Simulate", "Simulation");
		double								minimum ;
		double								maximum = 1 ;
		EventPool.UpdateEventHandler		hdlr ;
		static EventPool					pool = new EventPool () ;
		bool								connected ;
		System.Random						random = new System.Random () ;

		/// <summary> 
		/// Initialises a new instance of the class.
		/// </summary>
		public Noise ()
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
            info.AddValue("Minimum", minimum);
            info.AddValue("Maximum", maximum);
        }
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Noise (SerializationInfo info, StreamingContext context):base(info, context)
		{
            if (info == null) throw new ArgumentNullException("info");

            try
			{
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
			Value = new KineticaRT.Values.Double (random.NextDouble () * (maximum - minimum) + minimum) ;
		}

		#region Connection
		/// <summary>
		/// Starts noise generation.
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
		/// Stops noise generation.
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

		/// <summary>Gets or sets the minimum value of the noise.</summary>
		/// <value>Minimum value of the noise.</value>
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

		/// <summary>Gets or sets the maximum value of the noise.</summary>
		/// <value>Maximum value of the noise.</value>
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

		#endregion
	}
}


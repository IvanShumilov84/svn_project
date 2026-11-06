using System;
using System.Runtime.Serialization ;
using System.ComponentModel;
using System.Security.Permissions;


namespace KineticaRT.Channels
{
	/// <summary>
	/// Enumeration of possible data types
	/// </summary>
	public enum DataType
	{
		/// <summary>
		/// Mean Values
		/// </summary>
		Mean,
		/// <summary>
		/// Minimum Values
		/// </summary>
		Minimum,
		/// <summary>
		/// Maximum Values
		/// </summary>
		Maximum,
	}

	/// <summary>
	/// Mockup of tape data recorder
	/// </summary>
	public class Trace
	{
		/// <summary>
		/// New Tape
		/// </summary>
		public Trace()
		{
		}

		/// <summary>
		/// Returns data values
		/// </summary>
		/// <param name="StartTime"></param>
		/// <param name="EndTime"></param>
		/// <param name="Count"></param>
		/// <param name="Type"></param>
		/// <returns></returns>
		public virtual double[] GetValues(DateTime StartTime, DateTime EndTime, int Count, DataType Type)
		{
			double[]	data		= new double[Count] ;

			if (Type == DataType.Mean)
			{
				for (int index = 0; index < Count; index++)
				{
					data[index] = (Math.Sin(index/10.0) + 1) * EndTime.Minute - StartTime.Hour ;
				}
			}
			else if (Type == DataType.Maximum)
			{
				for (int index = 0; index < Count; index++)
				{
					data[index] = index + index % 13 ;
					data[index] = data[index] % 100 ;
				}
			}
			else
			{
//				for (int index = 0; index < Count; index++)
//				{
//					data[index] = (Math.Tan(index / 10.0) + 1) + EndTime.Second ;
//				}

				for (int index = 0; index < Count; index++)
				{
					if ((index % 50) < 5)
					{
						data[index] = System.Double.NaN ;
					}
//					else if ((index % 13) == 0)
//					{
//						data[index] = System.Double.PositiveInfinity ;
//					}
//					else if ((index % 33) == 0)
//					{
//						data[index] = System.Double.NegativeInfinity ;
//					}
					else
					{
						data[index] = (Math.Tan(index / 10.0) + 1) + EndTime.Second ;
					}
				}
			}

			return data ;
		}
	}

	interface ITrace
	{
		double[] GetValues(DateTime StartTime, DateTime EndTime, int Count, DataType Type) ;

		Channels.IChannel SourceChannel
		{
			get ;
			set ;
		}
		
		void Connect () ;
	
		void Disconnect () ;
	}

	/// <summary>
	/// Trace channel for numeric data.
	/// </summary>
	[Description("Trace channel for numeric data")]
	[Serializable]
	[DefaultProperty("Source")]
	class NumericTrace : KineticaRT.Channels.NumericChannel, ISerializable, ITrace , IConnectable
	{
		public NumericTrace () 
		{
			period = new TimeSpan (0,30,0);
		}

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.GetObjectData (info, context) ;
			info.AddValue ("Source", sourceName) ;
			info.AddValue ("Period", period) ;
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected NumericTrace (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try
			{
				sourceName = info.GetString ("Source") ;
				period = (TimeSpan)info.GetValue("Period",typeof(TimeSpan)) ;
				info.AddValue ("Period", period) ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
		}
#endregion

		/// <summary>
		/// Returns data values
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="count"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public virtual double[] GetValues(DateTime startTime, DateTime endTime, int count, DataType type)
		{
			//endTime = firstNode.Time ;
			double[]	data		= new double[count] ;
			
			lock (this)
			{
				TimeSpan interval = (endTime - startTime) ;
				interval = new TimeSpan(0,0,0,0,(int)(interval.TotalMilliseconds / count)) ;

				DateTime current_time = endTime ;
				int index = count - 1 ;

				DateTime now = DateTime.Now ; 
			
				// Future points - fill with NaN.
				while (current_time > now)
				{
					data[index--] = System.Double.NaN ;
					if (index < 0) return data ;
					current_time -= interval ;
				}

				Node current_node = firstNode ;

				// Data ahead of requested range.
				while (current_node != null && current_node.Time > endTime) current_node = current_node.Next ;

				// Data in requested range.
				switch (type)
				{
					case DataType.Mean:
					{
						double  next_total = 0 ;
						int		next_count = 0 ;

						while (index >= 0 && current_node != null && current_node.Time >= startTime)
						{
							if (current_node.Value != null)
							{
								next_total += current_node.Value.ToDouble () ;
								next_count ++ ;
							}

							if (current_node.Time < current_time)
							{
								while (index >= 0 && current_node.Time < current_time)
								{
									data[index--] = next_total / next_count ;
									current_time -= interval ;
								}
								next_total = 0 ;
								next_count = 0 ;
							}

							current_node = current_node.Next ;
						}
					}
					break ;

					case DataType.Maximum:
					{
						double  next_maximum = System.Double.MinValue ;

						while (index >= 0 && current_node != null && current_node.Time >= startTime)
						{
							if (current_node.Value != null)
							{
								next_maximum = current_node.Value.ToDouble () > next_maximum ? current_node.Value.ToDouble () : next_maximum ;
							}

							if (current_node.Time < current_time)
							{
								if (index >= 0 && current_node.Time < current_time)
								{
									while (index >= 0 && current_node.Time < current_time)
									{
										data[index--] = next_maximum ;
										current_time -= interval ;
									}
									next_maximum = System.Double.MinValue ;
								}
							}

							current_node = current_node.Next ;
						}
					}
					break ;

					case DataType.Minimum:
					{
						double  next_minimum = System.Double.MaxValue ;

						while (index >= 0 && current_node != null && current_node.Time >= startTime)
						{
							if (current_node.Value != null)
							{
								next_minimum = current_node.Value.ToDouble () < next_minimum ? current_node.Value.ToDouble () : next_minimum ;
							}

							if (current_node.Time < current_time)
							{
								if (index >= 0 && current_node.Time < current_time)
								{
									while (index >= 0 && current_node.Time < current_time)
									{
										data[index--] = next_minimum ;
										current_time -= interval ;
									}
									next_minimum = System.Double.MaxValue ;
								}
							}

							current_node = current_node.Next ;
						}
					}
					break ;
				}

				// Out of data - fill rest with NaN.
				while (index >= 0)
				{
					data[index--] = System.Double.NaN ;
				}
			}

			return data ;
		}

		/// <summary>
		/// Gets or sets period for which data is kept.
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets period for which data is kept.")]
		[Browsable (true)]
		public TimeSpan Period
		{
			get
			{
				return period ;
			}
			set
			{
				if (period != value)
				{
					period = value ;
					lock (this)
					{
						Trim () ;
					}
					OnPropertyChanged("Period") ;
				}
			}
		}
		TimeSpan period ;

		/// <summary>
		/// Gets or sets the Channel object that is the Source of the trace data
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the Channel object that is the Source of the trace data")]
		[Browsable (false)]
		public Channels.IChannel SourceChannel
		{
			get
			{
				return source;
			}
			set
			{
				if (source != value)
				{
					bool connect = connected ;
					if (connect)
					{
						Disconnect () ;
					}

					source = value ;

					if (connect)
					{
						if (source != null)
						{
							Connect() ;
						}
					}

					OnPropertyChanged("SourceChannel") ;
				}
			}
		}
		private Channels.IChannel source ;

		/// <summary>
		/// Gets or sets the name of the Source
		/// </summary>
		[Category("Trace")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the Tape to be the Source")]
		public string Source
		{
			get
			{
				return sourceName ;
			}
			set
			{
				if (sourceName != value)
				{
					sourceName = value ;
					OnPropertyChanged("Source") ;
				}

				if (connected)
				{
					//try and connect to source
					Channels.IChannel source_channel = OnObjectSearch(sourceName) as Channels.IChannel ;

					SourceChannel = source_channel ;
				}
			}
		}

		private string sourceName ;
		
		#region Connect
		private bool connected ;

		/// <summary>
		/// Connect to other Objects.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				if (connected) return ;

				if (sourceName != null) source = OnObjectSearch(sourceName) as KineticaRT.Channels.IChannel ;
				if (source != null) 
				{
					source.ValueChanged +=new KineticaRT.Channels.ValueChangedEventHandler(source_ValueChanged);
					source.PropertyChanged +=new PropertyChangedEventHandler(source_PropertyChanged);
					Value = source.Value ;

					KineticaRT.Channels.IInfoChannel info = source as KineticaRT.Channels.IInfoChannel ;
					if (info != null)
					{
						this.Description = info.Description ;
						this.ToolTip = info.ToolTip ;
					}

					KineticaRT.Channels.NumericChannel numeric = source as KineticaRT.Channels.NumericChannel ;
					if (numeric != null)
					{
						this.Units = numeric.Units ;
						this.DecimalDigits = numeric.DecimalDigits ;
						this.Format = numeric.Format ;
						this.Minimum = numeric.Minimum ;
						this.Maximum = numeric.Maximum ;
					}
				}

				connected = true ;
			}
		}

		/// <summary>
		/// Disconnect from other Objects.
		/// </summary>
		public override void Disconnect ()
		{
			lock (this)
			{
				if (connected)
				{
					if (source != null) 
					{
						source.ValueChanged -=new KineticaRT.Channels.ValueChangedEventHandler(source_ValueChanged);
						Value = KineticaRT.Values.Values.NotConnected ;
						source = null ;
					}
					
					// Free stored data.
					while (lastNode != null)
					{
						Node old = lastNode ;
						lastNode = lastNode.Last ;
						if (lastNode != null) lastNode.Next = null ;
						old.Last = null ;
						System.Diagnostics.Debug.Assert (old.Next == null, "lastNode.Last Should be null") ;
					}
					while (freeNode != null)
					{
						Node old = freeNode ;
						freeNode = freeNode.Last ;
						old.Last = null ;
						System.Diagnostics.Debug.Assert (old.Next == null, "freeNode.Last Should be null") ;
					}	
					freeCount = 0 ;


					connected = false ;
				}
			}
			base.Disconnect () ;
		}
		
		#endregion

		class Node
		{
			private DateTime startTime ;
			private KineticaRT.Values.Value value ;
			private Node next ;
			private Node last ;
//			static int nodeCount ;

			public Node (Node previousNode, KineticaRT.Values.Value value)
			{
//				nodeCount ++ ;
				Reuse (previousNode, value) ;
			}

//			~Node ()
//			{
//				nodeCount -- ;
//			}

			public void Reuse (Node previousNode, KineticaRT.Values.Value value)
			{
				startTime = System.DateTime.Now ;
				next = previousNode ;
				if (previousNode != null) previousNode.last = this ;
				this.value = value ;
			}

			public void Clear ()
			{
				this.value = null ;
				System.Diagnostics.Debug.Assert (next == null, "next Should be null") ;
			}

			public DateTime Time
			{
				get
				{
					return startTime ;
				}
			}

			public Node Next
			{
				get
				{
					return next ;
				}
				set
				{
					next = value ;
				}
			}

			public Node Last
			{
				get
				{
					return last ;
				}
				set
				{
					last = value ;
				}
			}

			public KineticaRT.Values.Value Value
			{
				get
				{
					return value ;
				}
			}
		}

		Node firstNode ;
		Node lastNode ;

		private void source_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			lock (this)
			{
				this.Value = newVal ;

				if (firstNode == null) 
				{
					lastNode = firstNode = new Node (null, newVal) ;
				}
				else
				{
					if (freeCount > 0)
					{
						Node next_free_node = freeNode.Last ;
						freeNode.Reuse (firstNode, newVal) ;
						firstNode = freeNode ;
						freeNode = next_free_node ;
						freeCount -- ;
					}
					else
					{
						firstNode = new Node (firstNode, newVal) ;
					}
				}

				Trim () ;
			}
		}
		
		private void Trim ()
		{
			while (lastNode != null && lastNode.Time < DateTime.Now - period)
			{
				Node old = lastNode ;
				lastNode = lastNode.Last ;
				if (lastNode != null) lastNode.Next = null ;

				System.Diagnostics.Debug.Assert (old.Next == null, "Next Should be null") ;
				
				if (freeCount < 1000)
				{
					old.Last = freeNode ;  // Free off the node.
					freeNode = old ;
					freeNode.Clear() ;
					freeCount ++ ;
				}
				else
				{
					old.Last = null ;
					old.Clear () ;
					//System.GC.Collect() ;
					//System.GC.WaitForPendingFinalizers () ;
				}
			}
		}

		int freeCount ;
		Node freeNode ;

		private void source_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			KineticaRT.Channels.IInfoChannel info = source as KineticaRT.Channels.IInfoChannel ;
			if (info != null)
			{
				switch (e.PropertyName)
				{
					case "Description" :
						this.Description = info.Description ;
						return ;
					case "ToolTip" :
						this.ToolTip = info.ToolTip ;
						return ;
				}
			}

			KineticaRT.Channels.NumericChannel numeric = source as KineticaRT.Channels.NumericChannel ;
			if (numeric != null)
			{
				switch (e.PropertyName)
				{
					case "Units" :
						this.Units = numeric.Units ;
						return ;
					case "DecimalDigits" :
						this.DecimalDigits = numeric.DecimalDigits ;
						return ;
					case "Format" :
						this.Format = numeric.Format ;
						return ;
					case "Minimum" :
						this.Minimum = numeric.Minimum ;
						return ;
					case "Maximum" :
						this.Maximum = numeric.Maximum ;
						return ;
				}
			}
		}
	}
}

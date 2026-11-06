using System;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Runtime.Serialization ;
using System.Reflection;
using KineticaRT.Channels;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Enumeration of possible sources of Minimum and Maximum values for a TimeTrace
	/// </summary>
	public enum TimeTraceRangeSources
	{
		/// <summary>
		/// The TimeTrace's own Minimum and Maximum properties are used
		/// </summary>
		Self,
		/// <summary>
		/// The range of the parent Control's LeftScale is used.
		/// </summary>
		LeftScale,
		/// <summary>
		/// The range of the parent Control's RightScale is used.
		/// </summary>
		RightScale,
	}

	/// <summary>
	/// Collection of TimeTrace objects designed for use with a TimeGraph
	/// </summary>
	[TypeConverter(typeof(TimeTraceCollection.TraceCollConverter))]
	public class TimeTraceCollection : SubObjectCollection
	{
		#region Type Converter
		internal class TraceCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					TimeTraceCollection	coll		= (TimeTraceCollection) value;
					TimeTrace []		traces		= new TimeTrace [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(TimeTraceCollection).GetConstructor(new Type[]{typeof(TimeTrace[])});

					//copy traces into array
					for (int index = 0; index < coll.Count; index++)
					{
						traces[index] = coll[index] ;
					}

					return new InstanceDescriptor(ctor_info, new object[]{traces}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the TimeTraceCollection class
		/// </summary>
		public TimeTraceCollection()
		{
		}

		/// <summary>
		/// Initialises a new instance of the TimeTraceCollection class, optionally adding a single TimeTrace object
		/// </summary>
		/// <param name="AddATrace">If True, the collection initialises containing a single TimeTrace, else it contains no TimeTraces</param>
		public TimeTraceCollection(bool AddATrace)
		{
			if (AddATrace)		AddTrace(Color.Red) ;
		}

		/// <summary>
		/// Initialises a new instance of the TimeTraceCollection class containing the specified trace objects
		/// </summary>
		/// <param name="traces">Array of TimeTrace objects to add to collection</param>
		public TimeTraceCollection(TimeTrace [] traces)
		{
			AddRange(traces) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the TimeTrace object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TimeTrace this[int Index]
		{
			get
			{
				return (TimeTrace) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					//((TimeTrace)List[Index]).PropertyChanged -= hdlr ;	//?? needed?
					List[Index] = value ;
					//((TimeTrace)List[Index]).PropertyChanged += hdlr ;	//?? needed?
				}
			}
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new TimeTrace of the specified color
		/// </summary>
		/// <param name="LineColor">Color of new TimeTrace</param>
		public void AddTrace(Color LineColor)
		{
			if (Count == 1)
			{
				//second trace is set to relate to the right scale
				this.List.Add(new TimeTrace(LineColor, TimeTraceRangeSources.RightScale)) ;
			}
			else
			{
				this.List.Add(new TimeTrace(LineColor)) ;
			}
		}

		/// <summary>
		/// Adds an array of TimeTrace objects to the collection
		/// </summary>
		/// <param name="newTraces">Array of TimeTraces to add</param>
		public void AddRange(TimeTrace [] newTraces)
		{
			for (int index = 0; index < newTraces.Length; index++)
			{
				this.List.Add(newTraces[index]) ;
			}
		}
		#endregion

		#region Protected internal methods (drawing etc)
		/// <summary>
		/// Draw all the TimeTraces in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="PaperRect">Rectangle in which to draw</param>
		/// <param name="StartTime">Time at start of rectangle</param>
		/// <param name="EndTime">Time at end of rectangle</param>
		/// <param name="LeftScale">Left Scale</param>
		/// <param name="RightScale">Right Scale</param>
		protected internal void Draw(Graphics g, Rectangle PaperRect, DateTime StartTime, DateTime EndTime, Scale LeftScale, Scale RightScale)
		{
			foreach (TimeTrace trace in this)
			{
				trace.Draw(g, PaperRect, StartTime, EndTime, LeftScale, RightScale) ;
			}
		}
		#endregion
	}

	
	/// <summary>
	/// TimeTrace object, designed for use on a TimeGraph.
	/// Displays the historical value of a recorded source over a period of time.
	/// </summary>
	[TypeConverterAttribute (typeof(ObjectTypeConverter))]
	[DefaultProperty("Source")]
	public class TimeTrace : SubObjectBase
	{
		#region Variables
		//property variables
		private bool						visible ;
		private Color						lineColor ;
		private int							width ;
		private double						minimum ;
		private double						maximum ;
		private TimeTraceRangeSources		rangeSource ;
		private Channels.IChannel			sourceChannel ;
		private string						source ;
		private DataType					dataType ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the TimeTrace class
		/// </summary>
		public TimeTrace()
		{
			Initialise(Color.Red, TimeTraceRangeSources.LeftScale) ;
		}
		/// <summary>
		/// Initialises a new instance of the TimeTrace class with the specified Color and range source
		/// </summary>
		/// <param name="LineColor">Color of TimeTrace line</param>
		/// <param name="RangeSource">Where TimeTrace gets it minimum and maximum values</param>
		public TimeTrace(Color LineColor, TimeTraceRangeSources RangeSource)
		{
			Initialise(LineColor, RangeSource) ;
		}
		/// <summary>
		/// Initialises a new instance of the TimeTrace class with the specified Color
		/// </summary>
		/// <param name="LineColor">Color of TimeTrace line</param>
		public TimeTrace(Color LineColor)
		{
			Initialise(LineColor, TimeTraceRangeSources.LeftScale) ;
		}

		private void Initialise(Color Clr, TimeTraceRangeSources RangeSrc)
		{
			visible				= true ;
			lineColor			= Clr ;
			width				= 1 ;
			minimum				= 0.0 ;
			maximum				= 100.0 ;
			rangeSource			= RangeSrc ;
			sourceChannel		= new NumericTrace() ;//null ;
			source				= "" ;
			dataType			= DataType.Mean ;
		}
		#endregion

		#region Properties
		///<summary>
		///Gets or sets the color of the trace line
		///</summary>
		[Category("Trace")]
		[Description("Gets or sets the color of the trace line")]
		public Color LineColor
		{
			get
			{
				return lineColor ;
			}
			set
			{
				if (lineColor != value)
				{
					lineColor = value ;
					OnPropertyChanged("LineColor") ;
				}
			}
		}
		private bool ShouldSerializeLineColor()
		{
			return lineColor != Color.Red ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetLineColor()
		{
			lineColor = Color.Red ;
		}

		///<summary>
		///Gets or sets whether the trace is visible
		///</summary>
		[Category("Trace")]
		[Description("Gets or sets whether the trace is visible")]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return visible ;
			}
			set
			{
				if (visible != value)
				{
					visible = value ;
					OnPropertyChanged("Visible") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Maximum value displayed on the Graph
		/// </summary>
		/// <remarks>Whether this value is sued depends on the setting of RangeSource</remarks>
		[Category("Trace")]
		[DefaultValue(100.0)]
		[Description("Gets or sets the Maximum value displayed on the Graph")]
		public double Maximum
		{
			get
			{
				return maximum ;
			}
			set
			{
				if (maximum != value)
				{
					maximum = value ;
					OnPropertyChanged("Maximum") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Minimum value displayed on the Graph
		/// </summary>
		/// <remarks>Whether this value is used depends on the setting of RangeSource</remarks>
		[Category("Trace")]
		[DefaultValue(0.0)]
		[Description("Gets or sets the Minimum value displayed on the Graph")]
		public double Minimum
		{
			get
			{
				return minimum ;
			}
			set
			{
				if (minimum != value)
				{
					minimum = value ;
					OnPropertyChanged("Minimum") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of the trace line in pixels
		/// </summary>
		[Category("Trace")]
		[DefaultValue(1)]
		[Description("Gets or sets the width of the trace line in pixels")]
		public int Width
		{
			get
			{
				return width ;
			}
			set
			{
				if (width != value)
				{
					width = value ;
					OnPropertyChanged("Width") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the source of the Minimum and Maximum values displayed by the trace
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the source of the Minimum and Maximum values displayed by the trace")]
		[DefaultValue(TimeTraceRangeSources.LeftScale)]
		public TimeTraceRangeSources RangeSource
		{
			get
			{
				return rangeSource ;
			}
			set
			{
				if (rangeSource != value)
				{
					rangeSource = value;
					OnPropertyChanged("RangeSource") ;
				}
			}
		}

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
				return sourceChannel;
			}
			set
			{
				if (sourceChannel != value)
				{
					if (value == null)
					{
						sourceChannel.Disconnect () ;
						sourceChannel = null ;
					}
					else
					{
						if (sourceChannel != null) sourceChannel.Disconnect () ;
						ITrace source_trace = value as ITrace ;
						if (source_trace == null)
						{
							source_trace = new NumericTrace () ;
							source_trace.Connect () ;
							source_trace.SourceChannel = value ;
						}
					
						sourceChannel = source_trace as Channels.IChannel ;
					}
					OnPropertyChanged("SourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the Tape to be the Source
		/// </summary>
		[Category("Trace")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the Tape to be the Source")]
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

				if (connected)
				{
					//try and connect to source
					Channels.IChannel source_channel = OnObjectSearch(source) as Channels.IChannel ;

					SourceChannel = source_channel ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the type of data values displayed
		/// </summary>
		[Category("Trace")]
		[DefaultValue(DataType.Mean)]
		[Description("Gets or sets the type of data values displayed")]
		public DataType DataType
		{
			get
			{
				return dataType ;
			}
			set
			{
				if (dataType != value)
				{
					dataType = value ;
					OnPropertyChanged("DataType") ;
				}
			}
		}

		#endregion

		#region Drawing
		/// <summary>
		/// Draw the Trace
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="PaperRect">Rectangle in which to draw</param>
		/// <param name="StartTime">Time at start of rectangle</param>
		/// <param name="EndTime">Time at end of rectangle</param>
		/// <param name="LeftScale">Left Scale</param>
		/// <param name="RightScale">Right Scale</param>
		protected internal void Draw(Graphics g, Rectangle PaperRect, DateTime StartTime, DateTime EndTime, Scale LeftScale, Scale RightScale)
		{
			ITrace source_trace = sourceChannel as ITrace ;
			if (!visible || source_trace == null)		return ;


			double[]	data = source_trace.GetValues(StartTime, EndTime, PaperRect.Width, dataType) ;

			//have we got some data?
			if (data != null && data.Length > 0)
			{
				Point[]		points ;//		= new Point[data.Length] ;
				Pen			pen				= new Pen(lineColor, width) ;
				double		min ;
				double		range ;
				double		fraction ;
				ArrayList	pt_list			= new ArrayList(data.Length) ;
				
				//find the range we will be drawing in
				GetRange(LeftScale, RightScale, out min, out range) ;

				for (int index = 0; index <= data.Length; index++)
				{
					if (index == data.Length || Double.IsNaN(data[index]))
					{
						if (pt_list.Count > 1)
						{
							//draw the line up to here
							points = new Point[pt_list.Count] ;
							pt_list.CopyTo(0, points, 0, pt_list.Count) ;
							pt_list.Clear() ;
							g.DrawLines(pen, points) ;
						}
					}
					else if (Double.IsInfinity(data[index]))
					{
						//draw as way off top or bottom
						fraction = Double.IsNegativeInfinity(data[index]) ? -1 : 2 ;
						pt_list.Add(new Point(PaperRect.X + index, PaperRect.Bottom - (int)(PaperRect.Height * fraction))) ;
					}
					else
					{
						fraction = (data[index] - min) / range ;
						pt_list.Add(new Point(PaperRect.X + index, PaperRect.Bottom - (int)(PaperRect.Height * fraction))) ;
					}
				}

				//g.DrawLines(pen, points) ;
				pen.Dispose() ;
			}
		}

		#endregion

		#region Other Methods
		private void GetRange(Scale LeftScale, Scale RightScale, out double Min, out double Range)
		{
			switch (rangeSource)
			{
				case TimeTraceRangeSources.LeftScale:
				{
					Min = LeftScale.Minimum ;
					Range = LeftScale.Maximum - Min ;
				}
				break ;

				case TimeTraceRangeSources.RightScale:
				{
					Min = RightScale.Minimum ;
					Range = RightScale.Maximum - Min ;
				}
				break ;
				
				default:		//self
				{
					Min = minimum ;
					Range = maximum - minimum ;
				}
				break ;
			}
		}
		#endregion

		#region Connection
		private bool connected ;

		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect() ;

			if (source != null  && this.source.Length > 0)
			{
				Channels.IChannel source_channel = OnObjectSearch(source) as Channels.IChannel ;
				SourceChannel = source_channel ;
			}
			connected = true ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (source != null  && this.source.Length > 0)
			{
				SourceChannel = null ;
			}
			connected = false ;
			base.Connect() ;
		}
		#endregion
	}
}
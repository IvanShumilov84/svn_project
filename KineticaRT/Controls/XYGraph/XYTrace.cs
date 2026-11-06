using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
//using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

namespace KineticaRT.Controls
{
	#region Declarations (enums)
	/// <summary>
	/// Enumeration of possible pen styles (shapes)
	/// </summary>
	public enum PenStyles
	{
		/// <summary>
		/// Pen in the shape of a Small Dot
		/// </summary>
		Dot			= 0,
		/// <summary>
		/// Pen in the shape of a Plus sign
		/// </summary>
		Plus		= 1,
		/// <summary>
		/// Pen in the shape of a Filled Circle
		/// </summary>
		Circle		= 2,
		/// <summary>
		/// Pen in the shape of an Unfilled Circle pen
		/// </summary>
		OpenCircle	= 3,
		/// <summary>
		/// Pen in the shape of a Cross
		/// </summary>
		Cross		= 4,
		/// <summary>
		/// Pen in the shape of a Man
		/// </summary>
		Man			= 5,
		/// <summary>
		/// Pen in the shape of a Square with a central dot
		/// </summary>
		Square		= 6,
		/// <summary>
		/// Pen in the shape of a Diamond with a central dot
		/// </summary>
		Diamond		= 7,
	}

	/// <summary>
	/// Enumeration of possible sources of Minimum and Maximum values for the XValue of an XYTrace
	/// </summary>
	public enum XRangeSources
	{
		/// <summary>
		/// The XYTrace's own XMinimum and XMaximum properties are used
		/// </summary>
		Self,
		/// <summary>
		/// If the XYTrace's XSource is an INumericChannel then its XMinimum and
		/// XMaximum are used, otherwise the XYTrace's own values are used
		/// </summary>
		Source,
		/// <summary>
		/// The range of the parent Control's TopScale is used.
		/// </summary>
		TopScale,
		/// <summary>
		/// The range of the parent Control's BottomScale is used.
		/// </summary>
		BottomScale,
	}

	/// <summary>
	/// Enumeration of possible sources of Minimum and Maximum values for the YValue of an XYTrace
	/// </summary>
	public enum YRangeSources
	{
		/// <summary>
		/// The XYTrace's own YMinimum and YMaximum properties are used
		/// </summary>
		Self,
		/// <summary>
		/// If the XYTrace's YSource is an INumericChannel then its Minimum and 
		/// Maximum are used, otherwise the XYTrace's own values are used
		/// </summary>
		Source,
		/// <summary>
		/// The range of the parent Control's LeftScale is used.
		/// </summary>
		LeftScale,
		/// <summary>
		/// The range of the parent Control's RightScale is used.
		/// </summary>
		RightScale,
	}
	#endregion

    internal class ScalingInfo
    {
        public double xMin;
        public double xRange;
        public double yMin;
        public double yRange;
        public double top;
        public double left;
        public double width;
        public double height;

        public static bool operator ==(ScalingInfo value1, ScalingInfo value2)
        {
            if (((object)value1 == null) && ((object)value2 != null)) return false;
            if (((object)value1 != null) && ((object)value2 == null)) return false;
            if (((object)value1 == null) && ((object)value2 == null)) return true;

            //return value1.left == value2.left && value1.bottom == value2.bottom && value1.xMin == value2.xMin && value1.xRange == value2.xRange && value1.yMin == value2.yMin && value1.yRange == value2.yRange;
            return value1.top == value2.top && value1.left == value2.left && value1.width == value2.width && value1.height == value2.height && value1.xMin == value2.xMin && value1.xRange == value2.xRange && value1.yMin == value2.yMin && value1.yRange == value2.yRange;
        }
        public static bool operator !=(ScalingInfo value1, ScalingInfo value2)
        {
            return !(value1 == value2);
        }
        public override bool Equals(object o)
        {
            return this == o as ScalingInfo;
        }
        public override int GetHashCode() { return (int)this.xMin; }

    } ;

    /// <summary>
	/// Collection of XYTrace objects designed for use with an XYGraph
	/// </summary>
	[TypeConverter(typeof(XYTraceCollection.TraceCollConverter))]
	public class XYTraceCollection : SubObjectCollection
	{
		private	ArrayList			storageList			= new ArrayList() ;

		#region Type Converter
		internal class TraceCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					XYTraceCollection	coll		= (XYTraceCollection) value;
					XYTrace []			traces		= new XYTrace [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(XYTraceCollection).GetConstructor(new Type[]{typeof(XYTrace[])});

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
		/// Initialises a new instance of the XYTraceCollection class with one trace
		/// </summary>
		public XYTraceCollection()
		{
			AddTrace(Color.Red) ;
		}

		/// <summary>
		/// Initialises a new instance of the XYTraceCollection class containing the specified trace objects
		/// </summary>
		/// <param name="traces">Array of XYTrace objects to add to collection</param>
		public XYTraceCollection(XYTrace [] traces)
		{
			AddRange(traces) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the XYTrace object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public XYTrace this[int Index]
		{
			get
			{
				return (XYTrace) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					//((XYTrace)List[Index]).PropertyChanged -= hdlr ;	//?? needed?
					List[Index] = value ;
					//((XYTrace)List[Index]).PropertyChanged += hdlr ;	//?? needed?
				}
			}
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new XYTrace of the specified color
		/// </summary>
		/// <param name="LineColor">Color of new XYTrace</param>
		public void AddTrace(Color LineColor)
		{
            lock (this)
            {
                this.List.Add(new XYTrace(LineColor));
            }
		}

		/// <summary>
		/// Adds an array of XYTrace objects to the collection
		/// </summary>
		/// <param name="newXYTraces">Array of XYTraces to add</param>
		public void AddRange(XYTrace [] newXYTraces)
		{
            lock (this)
            {
                for (int index = 0; index < newXYTraces.Length; index++)
                {
                    this.List.Add(newXYTraces[index]);
                }
            }
		}

		/// <summary>
		/// Copies the values in the given XYTrace into a Storage Trace,
		/// which remains permanently displayed as a reference
		/// </summary>
		/// <param name="Trace">XYTrace to store</param>
		public void StoreTrace(XYTrace Trace)
		{
            lock (this)
            {
                if (List.Contains(Trace))
                {
                    XYTrace copy;

                    Trace.CopyToStorage(out copy);

                    copy.SetStorage();
                    storageList.Add(copy);
                }
            }
		}

		/// <summary>
		/// Copies the values in the given XYTrace into a Storage Trace,
		/// which remains permanently displayed as a reference
		/// </summary>
		/// <param name="Index">Zero-based index of XYTrace to store</param>
		public void StoreTrace(int Index)
		{
            lock (this)
            {
                if (Index >= 0 && Index < this.Count)
                {
                    StoreTrace(this[Index]);
                }
            }
		}

        internal void ClearData(bool marks)
        {
            lock (this)
            {
                foreach (XYTrace trace in this)
                {
                    trace.EraseTrace(marks);
                }

                foreach (XYTrace trace in storageList)
                {
                    trace.EraseTrace(marks);
                }
            }
        }


        internal void ScaleData(ScalingInfo oldSI, ScalingInfo newSI)
        {
            lock (this)
            {
                foreach (XYTrace trace in this)
                {
                    trace.ScaleData(oldSI, newSI);
                }

                foreach (XYTrace trace in storageList)
                {
                    trace.ScaleData(oldSI, newSI);
                }
            }
        }


        #endregion

		#region Protected/Internal methods (drawing etc)
		/// <summary>
		/// Draw all the XYTraces in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="BackColor">XYGraph background color</param>
		/// <returns>True if some drawing done</returns>
		protected internal bool Draw(Graphics g, Color BackColor)
		{
			bool		drawn		= false ;

            lock (this)
            {
                //draw storage traces...
                foreach (XYTrace stg in storageList)
                {
                    if (stg.Draw(g, BackColor))
                    {
                        drawn = true;
                    }
                }

                //...and then normal traces
                foreach (XYTrace trace in this)
                {
                    if (trace.Draw(g, BackColor))
                    {
                        drawn = true;
                    }
                }
            }

			return drawn ;
		}

		/// <summary>
		/// Draws the pens on the traces
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		protected internal void DrawPens(Graphics g)
		{
            lock (this)
            {
                try
                {
                    foreach (XYTrace trace in this)
                    {
                        trace.DrawPen(g);
                    }
                }
                catch (System.InvalidOperationException)
                {
                    // Catches an error on the collection being modified.
                }
            }
		}

		/// <summary>
		/// Erase all the out of date XYTraces in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="ErasePens">True to erase out of date Pens</param>
		/// <param name="EraseTraces">True to erase out of date line segments</param>
		/// <param name="BackColor">XYGraph background color</param>
		protected internal bool Erase(Graphics g, bool ErasePens, bool EraseTraces, Color BackColor)
		{
			bool		erased		= false ;

            lock (this)
            {
                foreach (XYTrace trace in this)
                {
                    if (trace.Erase(g, ErasePens, EraseTraces, BackColor))
                    {
                        erased = true;
                    }
                }
            }
			return erased ;
		}

		/// <summary>
		/// Causes the traces to update their values and recalculate points etc
		/// </summary>
		/// <param name="CurrentTime">Now</param>
        /// <param name="paperRect">Where drawing is allowed</param>
		/// <param name="Scales">Array of parent control's scales used for range calculation</param>
		/// <param name="EraseTraces">returned as True to force trace lines to be erased</param>
		/// <param name="DrawTraces">returned as True to force trace lines to be drawn</param>
		/// <param name="ErasePens">returned as True to force pens to be erased</param>
		/// <param name="DrawPens">returned as True to force pens to be drawn</param>
		/// <param name="RefreshGraph">returned as True to force the whole graph to be redrawn</param>
        /// <param name="discardHiddenData">If true then value changes less than the current display resolution will be discarded instead of stored.</param>
        protected internal void Update(int CurrentTime, Rectangle paperRect, Scale[] Scales, ref bool EraseTraces, ref bool DrawTraces, ref bool ErasePens, ref bool DrawPens, ref bool RefreshGraph, bool discardHiddenData)
		{
            lock (this)
            {
                foreach (XYTrace trace in this)
                {
                    trace.Update(CurrentTime, paperRect, Scales, ref EraseTraces, ref DrawTraces, ref ErasePens, ref DrawPens, ref RefreshGraph, discardHiddenData);
                }
            }
		}

		/// <summary>
		/// Recalculates the traces
		/// </summary>
		protected internal void Recalculate()
		{
            lock (this)
            {
                foreach (XYTrace trace in this)
                {
                    trace.Recalculate();
                }
            }
		}

		/// <summary>
		/// Reset the display info in the traces
		/// </summary>
		/// <param name="MakeNextCurrent">True to move 'next' to 'current'. False to move 'current' to 'last'</param>
		protected internal void ProgressDisplayInfo(bool MakeNextCurrent)
		{
            lock (this)
            {
                foreach (XYTrace trace in this)
                {
                    trace.ProgressDisplayInfo(MakeNextCurrent);
                }
            }
		}
		#endregion
	}


	/// <summary>
	/// XYTrace object designed for use on an XYGraph.
	/// Displays the values over time of two source channels.
	/// </summary>
	[TypeConverterAttribute (typeof(ObjectTypeConverter))]
	[DefaultProperty("Value")]
	public class XYTrace : SubObjectBase
	{
		#region CLASS: Segment
		private class Segment
		{
			public Color		color ;
			public int			startPt ;
			public int			noOfPts ;
			public int			startTime ;
			public bool			erase ;

			public Segment(Color Color)
			{
				color		= Color ;
				startPt		= 0 ;
				noOfPts		= 0 ;
				startTime	= Environment.TickCount ;
				erase		= false ;
			}
		}
		#endregion

		#region CLASS: DisplayInfo
		private class DisplayInfo : System.Object
		{
			public	PointF		position ;
			public	PenStyles	penStyle ;

			public DisplayInfo()
			{
				position = new Point(0,0) ;
				penStyle = PenStyles.Cross ;
			}
		}
		#endregion

		#region CLASS: Mark
		private class Mark
		{
			public	PenStyles		style ;
			public	Color			color ;
			public	int				time ;
			public	PointF			position ;

			public Mark(PenStyles Style, Color Clr, int Time, PointF Position)
			{
				style	= Style ;
				color	= Clr ;
				time	= Time ;
				position= Position ;
			}
		}
		#endregion

		#region Variables
		private const int			MIN_SEGMENTS		= 20 ;
		private const int			MAX_SEGMENTS		= 10000 ;
		private const int			MAX_POINTS			= 100000 ;

		//property variables
		private bool						enabled ;
		private Color						lineColor ;
		private int							width ;
		private object						xValue ;
		private double						xMinimum ;
		private double						xMaximum ;
		private Channels.IChannel			xSourceChannel ;
		private string						xSource ;
		private XRangeSources				xRangeSource ;
		private object						yValue ;
		private double						yMinimum ;
		private double						yMaximum ;
		private Channels.IChannel			ySourceChannel ;
		private string						ySource ;
		private YRangeSources				yRangeSource ;
		private bool						fade ;
		private	int							persistTime ;			//ms
		private bool						lineVisible ;
		private PenStyles					penStyle ;
		private bool						penVisible ;
		private Color						penColor ;
		private bool						showMarks ;
		private PenStyles					markStyle ;

		//internal variables
		private Segment[]					segments ;
		private int							oldestSegment ;			//index
		private int							currentSegment ;		//index
		private double[]					xData ;
		private double[]					yData ;
		private PointF[]					points ;
		private int							currentPoint ;			//index
		private int							oldestPoint ;			//index
		private int							nextPoint ;				//index
		private	bool						storage ;
		private int							timePerSegment ;		//ms
		private	bool						currentDataValid ;
		private DisplayInfo					lastInfo ;
		private DisplayInfo					currentInfo ;
		private DisplayInfo					nextInfo ;
		private Values.Value				krtXValue ;				//double or type mismatch
		private Values.Value				krtYValue ;				//double or type mismatch
		private Queue						marks ;

		private Channels.ValueChangedEventHandler	srcHdlr ;
		private PropertyChangedEventHandler			propertyHdlr ;

        /// <summary>
        /// Array of Bitmaps for pen and mark images.
        /// </summary>
		protected Bitmap []			penBitmaps ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the XYTrace class
		/// </summary>
		public XYTrace()
		{
			Initialise(Color.Red) ;
		}
		/// <summary>
		/// Initialises a new instance of the XYTrace class with the specified Color
		/// </summary>
		/// <param name="LineColor">Color of XYTrace line</param>
		public XYTrace(Color LineColor)
		{
			Initialise(LineColor) ;
		}

		private void Initialise(Color Clr)
		{
			if (penBitmaps == null)
			{
				penBitmaps = new Bitmap[8] ;
//				image	= new Bitmap(typeof(XYGraph), "pen10.bmp") ;
				for (int index = 0; index < 8; index++)
				{
					penBitmaps[index] = new Bitmap(typeof(XYGraph), "pen" + (index+1) + ".bmp") ;
					penBitmaps[index].MakeTransparent(Color.White) ;
				}
			}

			enabled				= true ;
			lineColor			= Clr ;
			width				= 1 ;
			xValue				= 0.0 ;
			xMinimum			= 0.0 ;
			xMaximum			= 100.0 ;
			xSourceChannel		= null ;
			xSource				= "" ;
			xRangeSource		= XRangeSources.BottomScale ;
			yValue				= 0.0 ;
			yMinimum			= 0.0 ;
			yMaximum			= 100.0 ;
			ySourceChannel		= null ;
			ySource				= "" ;
			yRangeSource		= YRangeSources.LeftScale ;

			fade				= true ;
			lineVisible			= true ;
			persistTime			= 5000 ;
			penStyle			= PenStyles.Cross ;
			penVisible			= true ;
			penColor			= Clr ;
			showMarks			= true ;
			markStyle			= PenStyles.Cross ;

			srcHdlr = new Channels.ValueChangedEventHandler(OnValueChanged) ;
			propertyHdlr = new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

			krtXValue	= Values.Values.Zero ;
			krtYValue	= Values.Values.Zero ;

			storage			= false ;
			timePerSegment	= 1000 ;
			currentDataValid= true ;
			currentPoint	= 0 ;
			marks			= new Queue() ;

			lastInfo	= new DisplayInfo() ;
			currentInfo = new DisplayInfo() ;
			nextInfo	= new DisplayInfo() ;

			xData	= new double[MAX_POINTS] ;
			yData	= new double[MAX_POINTS] ;
			points	= new PointF[MAX_POINTS] ;
			points.Initialize() ;

			segments = new Segment[MAX_SEGMENTS] ;
			//segments get initialised as they are needed

			Clear(true) ;
		}
		#endregion

		#region Properties
		///<summary>
		///Gets or sets the color of the Trace Line
		///</summary>
		[Category("Trace")]
		[Description("Gets or sets the color of the Trace Line")]
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


		///<summary>
		///Gets or sets the color of the Trace Pen
		///</summary>
		///<remarks>The pen is the shape that is displayed at the front (ie current value) of the Trace line</remarks>
		[Category("Trace")]
		[Description("Gets or sets the color of the Trace Pen")]
		public Color PenColor
		{
			get
			{
				return penColor ;
			}
			set
			{
				if (penColor != value)
				{
					penColor = value ;
					OnPropertyChanged("PenColor") ;
				}
			}
		}
		private bool ShouldSerializePenColor()
		{
			return penColor != Color.Red ;
		}

		///<summary>
		///Gets or sets whether the Trace is enabled
		///</summary>
		[Category("Trace")]
		[Description("Gets or sets whether the Trace is enabled")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return enabled ;
			}
			set
			{
				if (enabled != value)
				{
					enabled = value ;
					OnPropertyChanged("Enabled") ;
				}
			}
		}

		/// <summary>
		/// Gets pr sets the current X Value
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the current X Value")]
		public object XValue
		{
			get
			{
				return xValue ;
			}
			set
			{
				if (xValue != value)
				{
					Values.Value		new_krt_value ;

					xValue = value ;

					new_krt_value = value as Values.Double ;
					if ((new_krt_value == null) && (value is System.Double)) new_krt_value = new KineticaRT.Values.Double ((System.Double)value) ;
					if (new_krt_value == null)		new_krt_value = Values.Values.TypeMismatch ;

					if (new_krt_value.CompareTo (krtXValue) != 0)
					{
						krtXValue = new_krt_value ;
					}
					OnPropertyChanged("XValue") ;
				}
			}
		}
		private bool ShouldSerializeXValue()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the object that is the Source of the Trace's X Value
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the object that is the Source of the Trace's X Value")]
		public Channels.IChannel XSourceChannel
		{
			get
			{
				return xSourceChannel;
			}
			set
			{
				if (xSourceChannel != value)
				{
					//free old handler if any
					if (xSourceChannel != null)		
					{
						xSourceChannel.ValueChanged		-= srcHdlr ;
						xSourceChannel.PropertyChanged	-= propertyHdlr ;
					}

					//attach new source
					xSourceChannel = value ;
					if (xSourceChannel != null)
					{
						xSourceChannel.ValueChanged		+= srcHdlr ;
						xSourceChannel.PropertyChanged	+= propertyHdlr ;
						XValue = xSourceChannel.Value ;
					}
					OnPropertyChanged("XSourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeXSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the object to be the X Source
		/// </summary>
		[Category("Trace")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the X Source")]
		public string XSource
		{
			get
			{
				return xSource ;
			}
			set
			{
				if (xSource != value)
				{
					xSource = value ;
					OnPropertyChanged("XSource") ;
				}
				//try and connect to source
				XSourceChannel = OnObjectSearch(xSource) as Channels.IChannel ;
			}
		}

		/// <summary>
		/// Gets or sets the Maximum X value displayed on the Graph
		/// </summary>
		/// <remarks>Whether this value is used depends on the setting of XRangeSource</remarks>
		[Category("Trace")]
		[DefaultValue(100.0)]
		[Description("Gets or sets the Maximum X value displayed on the Graph")]
		public double XMaximum
		{
			get
			{
				return xMaximum ;
			}
			set
			{
				if (xMaximum != value)
				{
					xMaximum = value ;
					OnPropertyChanged("XMaximum") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Minimum X value displayed on the Graph
		/// </summary>
		/// <remarks>Whether this value is used depends on the setting of XRangeSource</remarks>
		[Category("Trace")]
		[DefaultValue(0.0)]
		[Description("Gets or sets the Minimum X value displayed on the Graph")]
		public double XMinimum
		{
			get
			{
				return xMinimum ;
			}
			set
			{
				if (xMinimum != value)
				{
					xMinimum = value ;
					OnPropertyChanged("XMinimum") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the source of the Minimum and Maximum X values displayed
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the source of the Minimum and Maximum X values displayed")]
		[DefaultValue(XRangeSources.BottomScale)]
		public XRangeSources XRangeSource
		{
			get
			{
				return xRangeSource ;
			}
			set
			{
				if (xRangeSource != value)
				{
					xRangeSource = value;
					OnPropertyChanged("XRangeSource") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the current Y Value
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the current Y Value")]
		public object YValue
		{
			get
			{
				return yValue ;
			}
			set
			{
				if (yValue != value)
				{
					Values.Value		new_krt_value ;

					yValue = value ;

					new_krt_value = value as Values.Double ;
					if ((new_krt_value == null) && (value is System.Double)) new_krt_value = new KineticaRT.Values.Double ((System.Double)value) ;
					if (new_krt_value == null)		new_krt_value = Values.Values.TypeMismatch ;

					if (new_krt_value.CompareTo (krtYValue) != 0)
					{
						krtYValue = new_krt_value ;
					}
					OnPropertyChanged("YValue") ;
				}
			}
		}
		private bool ShouldSerializeYValue()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the object that is the Source of the Trace's Y Value
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the object that is the Source of the Trace's Y Value")]
		public Channels.IChannel YSourceChannel
		{
			get
			{
				return ySourceChannel;
			}
			set
			{
				if (ySourceChannel != value)
				{
					//free old handler if any
					if (ySourceChannel != null)		
					{
						ySourceChannel.ValueChanged		-= srcHdlr ;
						ySourceChannel.PropertyChanged	-= propertyHdlr ;
					}

					//attach new source
					ySourceChannel = value ;
					if (ySourceChannel != null)
					{
						ySourceChannel.ValueChanged		+= srcHdlr ;
						ySourceChannel.PropertyChanged	+= propertyHdlr ;
						YValue = ySourceChannel.Value ;
					}
					OnPropertyChanged("YSourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeYSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the object to be the Y Source
		/// </summary>
		[Category("Trace")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Y Source")]
		public string YSource
		{
			get
			{
				return ySource ;
			}
			set
			{
				if (ySource != value)
				{
					ySource = value ;
					OnPropertyChanged("YSource") ;
				}
				//try and connect to source
				YSourceChannel = OnObjectSearch(ySource) as Channels.IChannel ;
			}
		}

		/// <summary>
		/// Gets or sets the Maximum Y value displayed on the Graph
		/// </summary>
		/// <remarks>Whether this value is used depends on the setting of YRangeSource</remarks>
		[Category("Trace")]
		[DefaultValue(100.0)]
		[Description("Gets or sets the Maximum Y value displayed on the Graph")]
		public double YMaximum
		{
			get
			{
				return yMaximum ;
			}
			set
			{
				if (yMaximum != value)
				{
					yMaximum = value ;
					OnPropertyChanged("YMaximum") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Minimum Y value displayed on the Graph
		/// </summary>
		/// <remarks>Whether this value is used depends on the setting of YRangeSource</remarks>
		[Category("Trace")]
		[DefaultValue(0.0)]
		[Description("Gets or sets the Minimum Y value displayed on the Graph")]
		public double YMinimum
		{
			get
			{
				return yMinimum ;
			}
			set
			{
				if (yMinimum != value)
				{
					yMinimum = value ;
					OnPropertyChanged("YMinimum") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the source of the Minimum and Maximum Y values displayed
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the source of the Minimum and Maximum Y values displayed")]
		[DefaultValue(YRangeSources.LeftScale)]
		public YRangeSources YRangeSource
		{
			get
			{
				return yRangeSource ;
			}
			set
			{
				if (yRangeSource != value)
				{
					yRangeSource = value;
					OnPropertyChanged("YRangeSource") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of the Trace line in pixels
		/// </summary>
		[Category("Trace")]
		[DefaultValue(1)]
		[Description("Gets or sets the width of the Trace line in pixels")]
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
		/// Gets or sets the time that Trace line remains visible in milliseconds.
		/// </summary>
		/// <remarks>'0' means that the line remains permanently visible.</remarks>
		[Category("Trace")]
		[DefaultValue(5000)]
		[Description("Gets or sets the time that Trace line remains visible in milliseconds.")]
		public int PersistTime
		{
			get
			{
				return persistTime ;
			}
			set
			{
				if (persistTime != value)
				{
					persistTime = value ;
					OnPropertyChanged("PersistTime") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the style of the Trace Pen
		/// </summary>
		///<remarks>The pen is the shape that is displayed at the front (ie current value) of the Trace line</remarks>
		[Category("Trace")]
		[DefaultValue(PenStyles.Cross)]
		[Description("Gets or sets the style of the Trace Pen")]
		public PenStyles PenStyle
		{
			get
			{
				return penStyle ;
			}
			set
			{
				if (penStyle != value)
				{
					penStyle = value ;
					OnPropertyChanged("PenStyle") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the style of the Trace Marks
		/// </summary>
		[Category("Trace")]
		[DefaultValue(PenStyles.Cross)]
		[Description("Gets or sets the style of the trace Marks")]
		public PenStyles MarkStyle
		{
			get
			{
				return markStyle ;
			}
			set
			{
				if (markStyle != value)
				{
					markStyle = value ;
					OnPropertyChanged("MarkStyle") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the line fades out gradually or just disappears
		/// </summary>
		[Category("Trace")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the line fades out gradually or just disappears")]
		public bool Fade
		{
			get
			{
				return fade ;
			}
			set
			{
				if (fade != value)
				{
					fade = value ;
					OnPropertyChanged("Fade") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Trace line is shown
		/// </summary>
		[Category("Trace")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Trace line is shown")]
		public bool LineVisible
		{
			get
			{
				return lineVisible ;
			}
			set
			{
				if (lineVisible != value)
				{
					lineVisible = value ;
					OnPropertyChanged("LineVisible") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the pen is shown
		/// </summary>
		///<remarks>The pen is the shape that is displayed at the front (ie current value) of the Trace line</remarks>
		[Category("Trace")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the pen is shown")]
		public bool PenVisible
		{
			get
			{
				return penVisible ;
			}
			set
			{
				if (penVisible != value)
				{
					penVisible = value ;
					OnPropertyChanged("PenVisible") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the marks are shown
		/// </summary>
		[Category("Trace")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the marks are shown")]
		public bool ShowMarks
		{
			get
			{
				return showMarks ;
			}
			set
			{
				if (showMarks != value)
				{
					showMarks = value ;
					OnPropertyChanged("ShowMarks") ;
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Leaves a Mark at the current position of the Trace
		/// </summary>
		public void SetMark()
		{
			if (enabled && !storage && currentDataValid)
			{
				lock (this)
				{
					marks.Enqueue(new Mark(markStyle, penColor, Environment.TickCount, points[currentPoint])) ;
				}
			}
		}

		/// <summary>
		/// Returns a copy of this XYTrace to be kept as a reference
		/// </summary>
		/// <param name="Copy">(out) Copied trace</param>
		public void CopyToStorage(out XYTrace Copy)
		{
			Copy = new XYTrace() ;

			Copy.SetInfo(currentPoint, nextPoint, oldestPoint, points,
						 currentSegment, oldestSegment, segments,
						 marks, width, lineColor) ;

			this.Clear(true) ;
		}

		private void SetInfo(	int CurrentPt, int NextPt, int OldestPt, PointF[] Pts,
								int CurrentSeg, int OldestSeg, Segment[] Segments,
								Queue Marks, int Width, Color LineColor	)
		{
			int			total_points			= 0 ;

			Clear(true) ;

			width		= Width ;
			lineColor	= LineColor ;

			currentPoint	= CurrentPt ;
			nextPoint		= NextPt ;
			oldestPoint		= OldestPt ;
			points			= (PointF[])Pts.Clone() ;

			//may as well only have a single segment, and draw it in one go
			currentSegment	= 0 ;
			oldestSegment	= 0 ;

			for (int index = OldestSeg; ; index++)
			{
				if (index == MAX_SEGMENTS)		index = 0;

				if (Segments[index] != null)
				{
					total_points += Segments[index].noOfPts ;
				}

				if (index == CurrentSeg)		break ;
			}

			if (segments[0] == null)		segments[0] = new Segment(lineColor) ;

			segments[0].noOfPts		= total_points ;
			segments[0].startPt		= oldestPoint ;
			segments[0].startTime	= Segments[OldestSeg].startTime ;
			segments[0].color		= lineColor ;
			segments[0].erase		= false ;

			//marks = (Queue) Marks.Clone() ;		//??
			foreach (Mark mk in Marks)
			{
				Mark		new_mk	= new Mark(mk.style, mk.color, mk.time, mk.position) ;
				marks.Enqueue(mk) ;
			}
		}

		/// <summary>
		/// Sets the trace up to be a storage trace
		/// </summary>
		protected internal void SetStorage()
		{
			Disconnect() ;

			storage		= true ;
			persistTime = 0 ;
			fade		= false ;
			lineVisible = true ;

			CalculateStoredColours ();
		}
		#endregion

		#region Drawing
		/// <summary>
		/// Draw the Trace line and marks
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="BackColor">XYGraph background color</param>
		/// <returns>True if done some drawing</returns>
		protected internal bool Draw(Graphics g, Color BackColor)
		{
			if (!enabled)		return false ;

			bool		drawn		= false ;

			if (lineVisible && points != null)
			{
				if (DrawSegments (g, BackColor))
				{
					drawn = true;
				}
			}

			if (marks.Count > 0)
			{
				DrawMarks(g, BackColor);
				drawn = true ;
			}

			return drawn ;
		}

		private bool DrawSegments(Graphics g, Color BackColor)
		{
			int		index;						// array index
			bool	drawn		= false;		// true - A segment has been drawn.

			for (index = oldestSegment; ; index++)
			{
				Color		color ;

				if (index == MAX_SEGMENTS)		index = 0;

				if (segments[index] != null && segments[index].noOfPts > 1)
				{
					if (fade && (persistTime > 0))
					{
						color = FadeColor(segments[index].color, Environment.TickCount, segments[index].startTime, BackColor) ;
					}
					else
					{
						color = segments[index].color ;
					}

					DrawSingleSegment(g, index, color);

					drawn = true;
				}

				if (index == currentSegment)		break ;
			}

			return drawn;
		}

		private Color FadeColor(Color OriginalColor, int CurrentTime, int CreationTime, Color BackColor)
		{
			int				red;				// R value of segment colour.
			int				green;				// G value of segment colour.
			int				blue;				// B value of segment colour.
			double			time_fraction;		// Fraction indicating relative age of segment.

			time_fraction = (CreationTime + persistTime - (double)CurrentTime) / persistTime ;

			if (time_fraction < 0)
			{
				time_fraction = 0;
			}
			else if (time_fraction > 1)
			{
				time_fraction = 1;
			}
			// NO ELSE.

			red   = BackColor.R + (int)((OriginalColor.R - BackColor.R) * time_fraction);
			green = BackColor.G + (int)((OriginalColor.G - BackColor.G) * time_fraction);
			blue  = BackColor.B + (int)((OriginalColor.B - BackColor.B) * time_fraction);

//			return Color.FromArgb(red, green, blue);

			int			alpha		= 100 + (int)(155 * time_fraction) ;
			return Color.FromArgb(alpha, red, green, blue);
		}

		// Calculates the segment colours of a stored trace so that they
		//do not need calculating every time the trace is drawn.
		private void CalculateStoredColours()
		{
			int			index			= oldestSegment ;		// Segment index.
			Color		color			= ControlPaint.Dark(segments[index].color) ;

			if (color == segments[index].color)
			{
				color = ControlPaint.Light(segments[index].color) ;
			}

			for (index = oldestSegment; ; index++)
			{
				if (index >= MAX_SEGMENTS)			index = 0 ;

				segments[index].color = color ;

				if (index == currentSegment)		break ;
			}

			foreach (Mark mark in marks)
			{
				mark.color = color ;
			}
		}

		private void DrawSingleSegment(Graphics g, int SegmentIndex, Color SegmentColor)
		{
			Pen				pen			= new Pen(SegmentColor, width);
			PointF[]			pts			= new PointF[segments[SegmentIndex].noOfPts] ;

			Array.Copy(points, segments[SegmentIndex].startPt, pts, 0, segments[SegmentIndex].noOfPts) ;
			g.DrawLines(pen, pts) ;
	
			pen.Dispose() ;
		}


        private PointF currentPosition;

		/// <summary>
		/// Erase all the out of date parts of the XYTrace
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="ErasePens">True to erase out of date Pens</param>
		/// <param name="EraseTraces">True to erase out of date line segments</param>
		/// <param name="BackColor">XYGraph background color</param>
		protected internal bool Erase(Graphics g, bool ErasePens, bool EraseTraces, Color BackColor)
		{
			bool		erased		= false ;

			if (storage)			return false ;

			//pen...
            if (ErasePens)
            {
                    DrawPen(g, lastInfo.penStyle, currentPosition, BackColor, true);

                    erased = true;
            }

			//trace segements...
			if (EraseTraces)
			{
				if (enabled)
				{
					if (EraseSegments (g, BackColor))
					{
						erased = true ;
					}

					if (marks.Count > 0)
					{
						if (EraseMarks(g, BackColor))
						{
							erased = true ;
						}
					}
				}
			}

			return erased ;
		}

		private bool EraseSegments(Graphics g, Color BackColor)
		{
			bool		erased		= false	;

			while (segments[oldestSegment].erase)
			{
				// If line displayed, erase old segment.
				if (lineVisible)
				{
					DrawSingleSegment(g, oldestSegment, BackColor) ;
					erased = true ;
				}

				// Update trace data to remove old segment.
				if (oldestSegment == currentSegment)
				{
					Clear(false) ;
					break ;
				}

				oldestSegment++ ;
				if (oldestSegment == MAX_SEGMENTS)		oldestSegment = 0 ;
			}
			oldestPoint = segments[oldestSegment].startPt ;

			return erased ;
		}


		private bool EraseMarks(Graphics g, Color BackColor)
		{
			Mark		oldest_mark ;
			
			lock (this)
			{
				//remove and discard any marks that are too old
				oldest_mark = (Mark)marks.Peek() ;
				while(oldest_mark != null && oldest_mark.time < segments[oldestSegment].startTime)
				{
					DrawSingleMark(g, oldest_mark.style, oldest_mark.position, BackColor,true) ;

					marks.Dequeue() ;
					if (marks.Count > 0)
					{
						oldest_mark = (Mark)marks.Peek() ;
					}
					else
					{
						break ;
					}
				}

				foreach(Mark mark in marks)
				{
					DrawSingleMark(g, mark.style, mark.position, BackColor, true) ;
				}
			}

			return (marks.Count > 0) ;
		}

		/// <summary>
		/// Draws the Trace pen
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		protected internal void DrawPen(Graphics g)
		{
            if (enabled && !storage && penVisible && currentDataValid)
            {
                //DrawPen(g, currentInfo.penStyle, currentInfo.position, penColor, false);
                DrawPen(g, currentInfo.penStyle, currentPosition, penColor, false);

            }
		}

        /// <summary>
        /// Draws the image at the end of the trace - at the current position.
        /// </summary>
        /// <param name="g">Graphics object to draw on.</param>
        /// <param name="style">PenStyle to use.</param>
        /// <param name="position">The position to centre the image on.</param>
        /// <param name="color">The color to use.</param>
        /// <param name="erase">If true then the pen is being erased, otherwise it is being drawn.</param>
        protected virtual void DrawPen(Graphics g, PenStyles style, PointF position, Color color, bool erase)
        {
            DrawSingleMark(g, style, position, color, erase);
        }


		private void DrawMarks(Graphics g, Color BackColor)
		{
			if (showMarks)
			{
				Color			color ;

				lock (this)
				{
					foreach (Mark mark in marks)
					{
						if (fade && (persistTime > 0))
						{
							color = FadeColor(mark.color, Environment.TickCount, mark.time, BackColor);
						}
						else
						{
							color = mark.color ;
						}

						DrawSingleMark(g, mark.style, mark.position, color, false);
					}
				}
			}
		}

		private void DrawSingleMark(Graphics g, PenStyles Style, PointF Pt, Color Clr, bool erase)
		{
			System.Drawing.Imaging.ImageAttributes	attrs	= new System.Drawing.Imaging.ImageAttributes() ;
			System.Drawing.Imaging.ColorMap[]		map		= new System.Drawing.Imaging.ColorMap[1] ;
			Bitmap									image	= penBitmaps[(int)Style] ;

			if (image != null)
			{
            Rectangle image_rect = new Rectangle((int)Pt.X, (int)Pt.Y, image.Width, image.Height);

				image_rect.Offset(-image.Width / 2, -image.Height / 2) ;

				//map color
				map[0] = new System.Drawing.Imaging.ColorMap() ;
				map[0].OldColor = Color.Black ;
				map[0].NewColor = Clr ;

				attrs.SetRemapTable(map) ;
				g.DrawImage(image, image_rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attrs) ;
			}
		}
		#endregion

		#region Other Methods
		/// <summary>
		/// Clears the trace data
		/// </summary>
		/// <param name="marks">If True, clears marks as well</param>
		private void Clear(bool marks)
		{
			nextPoint		= 0;
			oldestPoint		= 0;
			currentSegment	= 0;
			oldestSegment	= 0;
			segments[0]		= new Segment(lineColor) ;
		}

        internal void EraseTrace(bool marks)
        {
            if (enabled)// && (persistTime > 0))
            {
                int index;			// Trace segment index.
                bool EraseTraces = false;
                bool RefreshGraph = false;

                // Check for old segments which need erasing.
                for (index = oldestSegment; ; index++)
                {
                    if (index >= MAX_SEGMENTS) index = 0;

                    if (segments[index].noOfPts > 1)
                    {
                        if (index == currentSegment)
                        {
                            StartNewSegment(ref RefreshGraph, ref EraseTraces);
                        }
                        segments[index].erase = true;
                        //EraseTraces = true ;
                    }
                    else
                    {
                        break;
                    }

                    if (index == currentSegment) break;
                }

                if (marks)
                {
                    this.marks.Clear();
                }
            }
        }

		internal void ScaleData(ScalingInfo oldSI, ScalingInfo newSI)
        {
            if (enabled)
            {
                lock (this)
                {
                    int index;			// Trace segment index.

                    for (index = oldestSegment; ; index++)
                    {
                        if (index >= MAX_SEGMENTS) index = 0;

                        int point_index = segments[index].startPt;
                        int count = 0;
                        for (; count < segments[index].noOfPts; point_index++, count++)
                        {
                            double x_fraction = (points[point_index].X - oldSI.left) / (oldSI.width - 1);
                            double x_value = (x_fraction * oldSI.xRange) + oldSI.xMin ;
                            x_fraction = (x_value - newSI.xMin) / newSI.xRange;
                            points[point_index].X = (float)(newSI.left + (x_fraction * (newSI.width - 1)));

                            double y_fraction = 1 - (((points[point_index].Y - oldSI.top) - 0.99999) / (oldSI.height - 1));
                            double y_value = (y_fraction * oldSI.yRange) + oldSI.yMin;
                            y_fraction = (y_value - newSI.yMin) / newSI.yRange;
                            points[point_index].Y = (float)newSI.top + (float)(0.99999 + (1 - y_fraction) * (newSI.height - 1));
                        }

                        if (index == currentSegment) break;
                    }

                    foreach (Mark mark in marks)
                    {
                        double x_fraction = (mark.position.X - oldSI.left) / (oldSI.width - 1);
                        double x_value = (x_fraction * oldSI.xRange) + oldSI.xMin;
                        x_fraction = (x_value - newSI.xMin) / newSI.xRange;
                        mark.position.X = (float)(newSI.left + (x_fraction * (newSI.width - 1)));

                        double y_fraction = 1 - (((mark.position.Y - oldSI.top) - 0.99999) / (oldSI.height - 1));
                        double y_value = (y_fraction * oldSI.yRange) + oldSI.yMin;
                        y_fraction = (y_value - newSI.yMin) / newSI.yRange;
                        mark.position.Y = (float)newSI.top + (float)(0.99999 + (1 - y_fraction) * (newSI.height - 1));
                        //     mark.position.X *= (mark.position.X - oldLeft) * scaleX + newLeft * scaleX;
                        //     mark.position.Y *= (mark.position.Y - oldBottom) * scaleY + newBottom * scaleY;
                    }
                }
            }
        }

        private ScalingInfo scalingInfo;

        /// <summary>
		/// Updates the trace value and recalculates points etc
		/// </summary>
		/// <param name="currentTime">Now</param>
		/// <param name="paperRect">Where drawing is allowed</param>
		/// <param name="scales">Array of parent control's scales used for range calculation</param>
		/// <param name="eraseTraces">returned as True to force trace lines to be erased</param>
		/// <param name="drawTraces">returned as True to force trace lines to be drawn</param>
		/// <param name="erasePens">returned as True to force pens to be erased</param>
		/// <param name="drawPens">returned as True to force pens to be drawn</param>
		/// <param name="refreshGraph">returned as True to force the whole graph to be redrawn</param>
        /// <param name="discardHiddenData">If true then value changes less than the current display resolution will be discarded instead of stored.</param>
        protected internal virtual void Update(int currentTime, Rectangle paperRect, Scale[] scales, ref bool eraseTraces, ref bool drawTraces, ref bool erasePens, ref bool drawPens, ref bool refreshGraph, bool discardHiddenData)
		{
//			bool	update_range		= false ;
            lock (this)
            {
                if (enabled && !storage)
                {
                    double x_fraction;				// }
                    double y_fraction;				// } Input scaled 0-1

                    if (krtXValue is Values.Double && krtYValue is Values.Double)
                    {
                        ScalingInfo newScalingInfo = new ScalingInfo();

                        //get ranges and calc values as fractions
                        GetRanges(scales, newScalingInfo);
                        newScalingInfo.top = paperRect.Top;
                        newScalingInfo.left = paperRect.Left;
                        newScalingInfo.width = paperRect.Width;
                        newScalingInfo.height = paperRect.Height;

                        //if (newScalingInfo != null)
                        //{
                        //    newScalingInfo.bottom = paperRect.Bottom;
                        //    newScalingInfo.bottom = paperRect.Left;
                        //}

                        if (scalingInfo != newScalingInfo)
                        {
                            if ((scalingInfo != null) && (newScalingInfo != null))
                            {
                                ScaleData(scalingInfo, newScalingInfo);
                                refreshGraph = true;
                            }

                            scalingInfo = newScalingInfo;
                        }

                        x_fraction = (krtXValue.ToDouble() - scalingInfo.xMin) / scalingInfo.xRange;
                        y_fraction = (krtYValue.ToDouble() - scalingInfo.yMin) / scalingInfo.yRange;
                    }
                    else
                    {
                        //invalid data (empty or error)
                        x_fraction = -0.02;
                        y_fraction = -0.02;
                    }

                    if ((currentTime - segments[currentSegment].startTime) > timePerSegment)
                    {
                        // Current segment has been going long enough, start another.
                        StartNewSegment(ref refreshGraph, ref eraseTraces);
                    }

                    if (!discardHiddenData || (x_fraction <= 1 && x_fraction >= 0 && y_fraction <= 1 && y_fraction >= 0))
                    {
                        //valid data

                        // Note: 0.99999 is to convert correctly from double value to pixel number.
                        nextInfo.position.X = (float)paperRect.Left + (float)(x_fraction * (paperRect.Width - 1));
                        nextInfo.position.Y = (float)paperRect.Top + (float)(0.99999 + (1 - y_fraction) * (paperRect.Height - 1));
                        nextInfo.penStyle = penStyle;

                        if (!discardHiddenData || ((int)nextInfo.position.X != (int)points[currentPoint].X) || ((int)nextInfo.position.Y != (int)points[currentPoint].Y))
                        {
                            //System.Diagnostics.Trace.WriteLine("newpoint", DateTime.Now.ToShortTimeString()+DateTime.Now.Millisecond.ToString());

                            // Trace has moved so record new position.
                            erasePens = true;
                            drawPens = true;
                            drawTraces = true;

                            if (segments[oldestSegment].erase && (nextPoint == oldestPoint))
                            {
                                // We are about to write over oldest_segment so erase it.
                                oldestSegment++;
                                if (oldestSegment == MAX_SEGMENTS) oldestSegment = 0;

                                oldestPoint = segments[oldestSegment].startPt;
                                refreshGraph = true;
                            }

                            if (!currentDataValid)
                            {
                                // Data is valid once again so start new segment.
                                currentDataValid = true;
                                StartNewSegment(ref refreshGraph, ref eraseTraces);

                                segments[currentSegment].noOfPts = 0;
                                segments[currentSegment].startPt = nextPoint;
                                segments[currentSegment].startTime = Environment.TickCount;
                            }

                            // Add on new point.
                            currentPoint = nextPoint;
                            points[currentPoint] = nextInfo.position;
                            segments[currentSegment].noOfPts++;

                            nextPoint++;
                            if (nextPoint == MAX_POINTS)
                            {
                                // We have reached the end of the buffer.
                                StartNewSegment(ref refreshGraph, ref eraseTraces);
                            }

                            if (((nextPoint + 1) % MAX_POINTS) == oldestPoint)
                            {
                                // Erase oldest_segment before we start writing over it.
                                if (currentSegment == oldestSegment)
                                {
                                    Clear(true);
                                    refreshGraph = true;
                                }
                                else
                                {
                                    segments[oldestSegment].erase = true;
                                    eraseTraces = true;
                                }
                            }
                        }

                        //					//check if in range
                        //					{
                        //						BOOL	in_range	= TRUE ;
                        //						parent_collection->IsTraceInRange(x_fractional_value, y_fractional_value, &in_range) ;
                        //
                        //						if (!in_range && thread_data.in_range)
                        //						{
                        //							thread_data.in_range = false ;
                        //							update_range = true ;
                        //						}
                        //						else if (in_range && !thread_data.in_range)
                        //						{
                        //							thread_data.in_range = true ;
                        //							update_range = true ;
                        //						}
                        //					}
                    }
                    else if (currentDataValid)
                    {
                        // Data just gone invalid.
                        currentDataValid = false;
                        erasePens = true;
                    }

                    //store actual values whether valid or not
                    //doing it now means current point has been incremented if necessary, otherwise
                    //the last value is overwritten
                    xData[currentPoint] = krtXValue.ToDouble();
                    yData[currentPoint] = krtYValue.ToDouble();

                    if (enabled && (persistTime > 0))
                    {
                        int index;			// Trace segment index.

                        // Check for old segments which need erasing.
                        for (index = oldestSegment; ; index++)
                        {
                            if (index >= MAX_SEGMENTS) index = 0;

                            if ((segments[index].noOfPts > 1) &&
                                ((currentTime - segments[index].startTime) > persistTime))
                            {
                                if (index == currentSegment)
                                {
                                    StartNewSegment(ref refreshGraph, ref eraseTraces);
                                }
                                segments[index].erase = true;
                                eraseTraces = true;
                            }
                            else
                            {
                                break;
                            }

                            if (index == currentSegment) break;
                        }
                    }
                }

                //			if (update_range)
                //			{
                //				KRT_UNLOCKBLOCK ;
                //				range_channel->put_Value(CKRT_Variant(thread_data.in_range)) ;
                //				KRT_RELOCKBLOCK ;
                //			}
            }
		}

		private void GetRanges(Scale[] Scales, ScalingInfo scalingInfo)
		{
			switch (xRangeSource)
			{
				case XRangeSources.BottomScale:
				{
                    scalingInfo.xMin = Scales[(int)ScaleSides.Bottom].Minimum;
                    scalingInfo.xRange = Scales[(int)ScaleSides.Bottom].Maximum - scalingInfo.xMin;
				}
				break ;
				
				case XRangeSources.TopScale:
				{
                    scalingInfo.xMin = Scales[(int)ScaleSides.Top].Minimum;
                    scalingInfo.xRange = Scales[(int)ScaleSides.Top].Maximum - scalingInfo.xMin;
				}
				break ;
				
				case XRangeSources.Source:
				{
					if (xSourceChannel is Channels.INumericChannel)
					{
						Channels.INumericChannel		num		= xSourceChannel as Channels.INumericChannel ;
                        scalingInfo.xMin = num.Minimum;
                        scalingInfo.xRange = num.Maximum - scalingInfo.xMin;
					}
					else		//default to internal range
					{
                        scalingInfo.xMin = xMinimum;
                        scalingInfo.xRange = xMaximum - xMinimum;
					}
				}
				break ;

				default:		//self
				{
                    scalingInfo.xMin = xMinimum;
                    scalingInfo.xRange = xMaximum - xMinimum;
				}
				break ;
			}

			switch (yRangeSource)
			{
				case YRangeSources.LeftScale:
				{
                    scalingInfo.yMin = Scales[(int)ScaleSides.Left].Minimum;
                    scalingInfo.yRange = Scales[(int)ScaleSides.Left].Maximum - scalingInfo.yMin;
				}
				break ;
				
				case YRangeSources.RightScale:
				{
                    scalingInfo.yMin = Scales[(int)ScaleSides.Right].Minimum;
                    scalingInfo.yRange = Scales[(int)ScaleSides.Right].Maximum - scalingInfo.yMin;
				}
				break ;
				
				case YRangeSources.Source:
				{
					if (ySourceChannel is Channels.INumericChannel)
					{
						Channels.INumericChannel		num		= ySourceChannel as Channels.INumericChannel ;
                        scalingInfo.yMin = num.Minimum;
                        scalingInfo.yRange = num.Maximum - scalingInfo.yMin;
					}
					else		//default to internal range
					{
                        scalingInfo.yMin = yMinimum;
                        scalingInfo.yRange = yMaximum - yMinimum;
					}
				}
				break ;

				default:		//self
				{
                    scalingInfo.yMin = yMinimum;
                    scalingInfo.yRange = yMaximum - yMinimum;
				}
				break ;
			}
		}

		/// <summary>
		/// Recalculates the trace
		/// </summary>
		protected internal void Recalculate()
		{
			if (!storage)
			{
				timePerSegment = ( ((persistTime / 1000) > MIN_SEGMENTS) || (persistTime == 0) )
					? 1000 : (persistTime / MIN_SEGMENTS);
		
				// Clear trace of any false data generated by update being called
				// before recalculate is called for the first time.
				if	((points != null)	&&
					(points[segments[oldestSegment].startPt].Equals(new Point()))	)
				{
					Clear(true);
				}
			}
		}

		private void StartNewSegment(ref bool RefreshGraph, ref bool EraseTraces)
		{
			if (segments[currentSegment].noOfPts > 1)
			{
				if (nextPoint >= (MAX_POINTS - 1))		nextPoint = 0 ;

				if	((nextPoint == oldestPoint)			||
					 ((nextPoint + 1) == oldestPoint)	||
					 ((nextPoint + 2) == oldestPoint)		)
				{
					// Oldest segment data is about to be written over so we need to
					// erase oldest_segment.
					if (currentSegment == oldestSegment)
					{
						Clear(true);
						RefreshGraph = true ;
					}
					else if (nextPoint == oldestPoint)
					{
						oldestSegment++;
						if (oldestSegment == MAX_SEGMENTS)		oldestSegment = 0 ;

						oldestPoint = segments[oldestSegment].startPt ;
						RefreshGraph = true ;
					}
					else
					{
						segments[oldestSegment].erase = true ;
						EraseTraces = true ;
					}
				}

				points[nextPoint] = points[currentPoint];
				currentPoint = nextPoint ;
				nextPoint++ ;

				if ((oldestSegment == (currentSegment + 1))		||
					((oldestSegment == 0) && (currentSegment == (MAX_SEGMENTS - 1)))	)
				{
					oldestSegment++;
					if (oldestSegment == MAX_SEGMENTS)		oldestSegment = 0 ;

					oldestPoint = segments[oldestSegment].startPt ;
					RefreshGraph = true ;
				}
		
				currentSegment++ ;
				if (currentSegment == MAX_SEGMENTS)		currentSegment = 0 ;


				if (segments[currentSegment] == null)		segments[currentSegment] = new Segment(lineColor) ;

				segments[currentSegment].noOfPts = 1 ;
				segments[currentSegment].startPt = currentPoint ;
				segments[currentSegment].startTime = Environment.TickCount ;
			}

			segments[currentSegment].color = lineColor ;
			segments[currentSegment].erase = false ;
		}

		/// <summary>
		/// Reset the display info in the trace
		/// </summary>
		/// <param name="MakeNextCurrent">True to move 'next' to 'current'. False to move 'current' to 'last'</param>
		protected internal void ProgressDisplayInfo(bool MakeNextCurrent)
		{
            if (MakeNextCurrent)
            {
                currentPosition = nextInfo.position;
            }

            if (enabled && !storage)
            {
                if (MakeNextCurrent)
                {
                    currentInfo.penStyle = nextInfo.penStyle;
                    currentInfo.position = nextInfo.position;
                }
                else	//make current last
                {
                    lastInfo.penStyle = currentInfo.penStyle;
                    lastInfo.position = currentInfo.position;
                }
            }
		}
		#endregion

		#region Source Event handling (value and property)
		//new value coming in
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			if (sender.Equals(xSourceChannel))
			{
				XValue = newValue as KineticaRT.Values.Double ;
			}
			else
			{
				YValue = newValue as KineticaRT.Values.Double ;
			}
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//nothing to do
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (!storage)
			{
				if (xSource != "")		XSourceChannel = OnObjectSearch(xSource) as Channels.IChannel ;
				if (ySource != "")		YSourceChannel = OnObjectSearch(ySource) as Channels.IChannel ;
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (xSourceChannel != null)			XSourceChannel = null ;
			if (ySourceChannel != null)			YSourceChannel = null ;
		}
		#endregion
	}
}

using System;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Globalization;
using System.ComponentModel.Design.Serialization;
using System.Runtime.Serialization ;
using System.Reflection;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Enumeration of possible sources of Minimum and Maximum values for a Trace
	/// </summary>
	public enum TraceRangeSources
	{
		/// <summary>
		/// The Trace's own Minimum and Maximum properties are used
		/// </summary>
		Self,
		/// <summary>
		/// If the Trace's Source is an INumericChannel then its
		/// Minimum and Maximum are used, otherwise the Trace's own values are used
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

	/// <summary>
	/// Collection of Trace objects, desined for use with a Trend
	/// </summary>
	[TypeConverter(typeof(TraceCollection.TraceCollConverter))]
	public class TraceCollection : SubObjectCollection
	{
		#region Type Converter
		internal class TraceCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					TraceCollection		coll		= (TraceCollection) value;
					Trace []			traces		= new Trace [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(TraceCollection).GetConstructor(new Type[]{typeof(Trace[])});

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
		/// Initialises a new instance of the TraceCollection class
		/// </summary>
		public TraceCollection()
		{
		}

		/// <summary>
		/// Initialises a new instance of the TraceCollection class, optionally adding a single Trace object
		/// </summary>
		/// <param name="AddATrace">If True, the collection initialises containing a single Trace, else it contains no Traces</param>
		public TraceCollection(bool AddATrace)
		{
			if (AddATrace)		AddTrace(Color.Red) ;
		}

		/// <summary>
		/// Initialises a new instance of the TraceCollection class containing the specified trace objects
		/// </summary>
		/// <param name="traces">Array of Trace objects to add to collection</param>
		public TraceCollection(Trace [] traces)
		{
			AddRange(traces) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the Trace object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Trace this[int Index]
		{
			get
			{
				return (Trace) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					//((Trace)List[Index]).PropertyChanged -= hdlr ;	//?? needed?
					List[Index] = value ;
					//((Trace)List[Index]).PropertyChanged += hdlr ;	//?? needed?
				}
			}
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new Trace of the specified color
		/// </summary>
		/// <param name="LineColor">Color of new Trace</param>
		public void AddTrace(Color LineColor)
		{
			if (Count == 1)
			{
				//second trace is set to relate to the right scale
				this.List.Add(new Trace(LineColor, TraceRangeSources.RightScale)) ;
			}
			else
			{
				this.List.Add(new Trace(LineColor)) ;
			}
		}

		/// <summary>
		/// Adds an array of Trace objects to the collection
		/// </summary>
		/// <param name="newTraces">Array of Traces to add</param>
		public void AddRange(Trace [] newTraces)
		{
			for (int index = 0; index < newTraces.Length; index++)
			{
				this.List.Add(newTraces[index]) ;
			}
		}
		#endregion

		#region Protected internal Methods (drawing etc)
		/// <summary>
		/// Draw all the Traces in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="Offset">Offset from start of data array</param>
		/// <param name="StartPoint">X position of where to start drawing</param>
		/// <param name="NoOfPoints">How many pixels to draw (horizontally)</param>
		/// <param name="RectRight">Rightmost pixel it is valid to draw</param>
		protected internal void Draw(Graphics g, int Offset, int StartPoint, int NoOfPoints, int RectRight)
		{
			foreach (Trace trace in this)
			{
				trace.Draw(g, Offset, StartPoint, NoOfPoints, RectRight) ;
			}
		}

		/// <summary>
		/// Resets the array of data held by each trace
		/// </summary>
		/// <param name="NoOfPixels">Number of pixels currently visible</param>
		/// <param name="RectBottom">Bottom of current scrolling area</param>
		protected internal void ResetData(int NoOfPixels, int RectBottom)
		{
			foreach (Trace trace in this)
			{
				trace.ResetData(NoOfPixels, RectBottom) ;
			}
		}


		/// <summary>
		/// Updates the data array held by the Traces because of scrolling
		/// </summary>
		/// <param name="ValueIndex">Index into Data array</param>
		/// <param name="yPtOffset">Offset for converting values to y-positions</param>
		/// <param name="yPtScale">Scale for converting values to y-positions</param>
		/// <param name="ScrollAmount">How far the Trend has scrolled since the last update</param>
		/// <param name="NoOfPixels">Total number of pixels in visible region</param>
		/// <param name="LeftScale">Left hand Scale object</param>
		/// <param name="RightScale">Right hand Scale object</param>
		protected internal void UpdateValues(long ValueIndex, int yPtOffset, double yPtScale, long ScrollAmount, int NoOfPixels, Scale LeftScale, Scale RightScale)
		{
			foreach (Trace trace in this)
			{
				trace.UpdateValues(ValueIndex, yPtOffset, yPtScale, ScrollAmount, NoOfPixels, LeftScale, RightScale) ;
			}
		}
		#endregion
	}

	
	/// <summary>
	/// Trace object designed for use on a Trend.
	/// Displays the Value of a source channel over a time period.
	/// </summary>
	[TypeConverterAttribute (typeof(ObjectTypeConverter))]
	[DefaultProperty("Value")]
	public class Trace : SubObjectBase
	{
		#region Variables
		//property variables
		private bool						visible ;
		private Color						lineColor ;
		private int							width ;
		private object						value ;
		private double						minimum ;
		private double						maximum ;
		private Channels.IChannel			sourceChannel ;
		private Channels.IChannel			destinationChannel ;
		private string						source ;
		private string						destination ;
		private bool						autoReturn ;
		private double						defaultValue ;
//		private VerticalScaleSides			relateToScale ;
		private TraceRangeSources			rangeSource ;

		//internal variables
		private int							lastY ;
		private int[]						point ;
		private double[]					data ;			//past values (min..max NOT 0..1)
		private Values.Value				krtValue ;		//double or type mismatch

		private Channels.ValueChangedEventHandler	srcHdlr ;
		private PropertyChangedEventHandler			propertyHdlr ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Trace class
		/// </summary>
		public Trace()
		{
			Initialise(Color.Red, TraceRangeSources.LeftScale) ;
		}
		/// <summary>
		/// Initialises a new instance of the Trace class with the specified Color and range source
		/// </summary>
		/// <param name="LineColor">Color of the Trace line</param>
		/// <param name="RangeSource">Where the Trace gets it minimum and maximum values</param>
		public Trace(Color LineColor, TraceRangeSources RangeSource)
		{
			Initialise(LineColor, RangeSource) ;
		}
		/// <summary>
		/// Initialises a new instance of the Trace class with the specified Color
		/// </summary>
		/// <param name="LineColor">Color of the Trace line</param>
		public Trace(Color LineColor)
		{
			Initialise(LineColor, TraceRangeSources.LeftScale) ;
		}

		private void Initialise(Color Clr, TraceRangeSources RangeSrc)
		{
			visible				= true ;
			lineColor			= Clr ;
			width				= 1 ;
			value				= 0.0 ;
			minimum				= 0.0 ;
			maximum				= 100.0 ;
			sourceChannel		= null ;
			destinationChannel	= null ;
			source				= "" ;
			destination			= "" ;
			autoReturn			= false ;
			defaultValue		= 0.0 ;
//			relateToScale		= VerticalScaleSides.Left ;
			rangeSource			= RangeSrc ;

			srcHdlr = new Channels.ValueChangedEventHandler(OnValueChanged) ;
			propertyHdlr = new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

            //krtValue = Values.Values.Zero;
            krtValue = Values.Values.NotConnected ;
			lastY		= 0 ;
			data		= new double[Trend.MAX_DATA_VALUES] ;    
			point		= new int[Trend.MAX_DATA_VALUES] ;

			// Initialise the line points.
			for (int index = 0; index < Trend.MAX_DATA_VALUES; index++) 
			{
				point[index] = -1 ;
			}
		}
		#endregion

		#region Properties
		///<summary>
		///Gets or sets the color of the Trace line
		///</summary>
		[Category("Trace")]
		[Description("Gets or sets the color of the Trace line")]
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
		///Gets or sets whether the Trace is visible
		///</summary>
		[Category("Trace")]
		[Description("Gets or sets whether the Trace is visible")]
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
		/// Gets or sets the current Value
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the current Value")]
		public object Value
		{
			get
			{
				return this.value ;
			}
			set
			{
				if (this.value != value)
				{
					Values.Value		new_krt_value ;

					this.value = value;

					new_krt_value = value as Values.Double ;
                    if (new_krt_value == null)
                    {
                        try
                        {
                            new_krt_value = new KineticaRT.Values.Double(System.Convert.ToDouble(value));
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                    //if ((new_krt_value == null) && (value is System.Double)) new_krt_value = new KineticaRT.Values.Double((System.Double)value);
					if (new_krt_value == null) new_krt_value = Values.Values.TypeMismatch ;

					if (new_krt_value.CompareTo (krtValue) != 0)
					{
						krtValue = new_krt_value ;

						//set destination
						if (destinationChannel != null)	destinationChannel.Value = krtValue ;
						
//						OnValueChanged() ;
					}
					OnPropertyChanged("Value") ;
				}
			}
		}
		private bool ShouldSerializeValue()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the object that is the Source of the Trace's Value
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the object that is the Source of the Trace's Value")]
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
					//free old handler if any
					if (sourceChannel != null)		
					{
						sourceChannel.ValueChanged		-= srcHdlr ;
						sourceChannel.PropertyChanged	-= propertyHdlr ;
					}

					//attach new source
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		+= srcHdlr ;
						sourceChannel.PropertyChanged	+= propertyHdlr ;
						Value = sourceChannel.Value ;
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
		/// Gets or sets the object that is the Destination for the Trace's Value
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the object that is the Destination for the Trace's Value")]
		public Channels.IChannel DestinationChannel
		{
			get
			{
				return destinationChannel;
			}
			set
			{
				if (value != destinationChannel)
				{
					destinationChannel = value ;

					if (destinationChannel != null)		destinationChannel.Value = krtValue ;

					OnPropertyChanged("DestinationChannel") ;
				}
			}
		}
		private bool ShouldSerializeDestinationChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the object to be the Source
		/// </summary>
		[Category("Trace")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Source")]
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
				//try and connect to source
				SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
			}
		}

		/// <summary>
		/// Gets or sets the name of the object to be the Destination
		/// </summary>
		[Category("Trace")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Destination")]
		public string Destination
		{
			get
			{
				return destination ;
			}
			set
			{
				if (destination != value)
				{
					destination = value ;
					OnPropertyChanged("Destination") ;
				}
				//try and connect to destination
				DestinationChannel = OnObjectSearch(destination) as Channels.IChannel ;
			}
		}

		/// <summary>
		/// Gets or sets the Maximum value that will be visible on the display
		/// </summary>
		/// <remarks>Whether this value is used depends on the RangeSource property</remarks>
		[Category("Trace")]
		[DefaultValue(100.0)]
		[Description("Gets or sets the Maximum value that will be visible on the display")]
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
		/// Gets or sets the Minimum value that will be visible on the display
		/// </summary>
		/// <remarks>Whether this value is used depends on the RangeSource property</remarks>
		[Category("Trace")]
		[DefaultValue(0.0)]
		[Description("Gets or sets the Minimum value that will be visible on the display")]
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
                    OnPropertyChanged("Minimum");
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
		/// Gets or sets the Default Value
		/// </summary>
		/// <remarks>This is the value to which the Trace reverts after a mouse drag if AutoReturn is true</remarks>
		[Category("Trace")]
		[DefaultValue(0.0)]
		[Description("Gets or sets the Default Value")]
		public double DefaultValue
		{
			get
			{
				return defaultValue ;
			}
			set
			{
				if (defaultValue != value)
				{
					defaultValue = value ;
					OnPropertyChanged("DefaultValue") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Trace reverts to its DefaultValue after dragging.
		/// </summary>
		[Category("Trace")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the Trace reverts to its DefaultValue after dragging.")]
		public bool AutoReturn
		{
			get
			{
				return autoReturn ;
			}
			set
			{
				if (autoReturn != value)
				{
					autoReturn = value ;
					OnPropertyChanged("AutoReturn") ;
				}
			}
		}
        
		/// <summary>
        /// Gets or sets whether the Trace rescales its data when the Minimum/Maximum changes.
		/// </summary>
		[Category("Trace")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the Trace rescales its data when the Minimum/Maximum changes.")]
        public bool AutoRescale
		{
			get
			{
                return autoRescale;
			}
			set
			{
                if (autoRescale != value)
				{
                    autoRescale = value;
                    OnPropertyChanged("AutoRescale");
				}
			}
		}


		/// <summary>
		/// Gets or sets the source of the Minimum and Maximum values displayed by the Trace
		/// </summary>
		[Category("Trace")]
		[Description("Gets or sets the source of the Minimum and Maximum values displayed by the Trace")]
		[DefaultValue(TraceRangeSources.LeftScale)]
		public TraceRangeSources RangeSource
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
		#endregion

		#region Drawing

        Point[] drawingPoints;
        int noOfDrawingPoints = -1;

		/// <summary>
		/// Draw the Trace
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="Offset">Offset from start of data array</param>
		/// <param name="StartPoint">X position of where to start drawing</param>
		/// <param name="NoOfPoints">How many pixels can be drawn (horizontally)</param>
		/// <param name="RectRight">Rightmost pixel to draw</param>
		protected internal void Draw(Graphics g, int Offset, int StartPoint, int NoOfPoints, int RectRight)
		{
			if (!visible)		return ;

			// Calculate and draw line.
			int			current_point ;
			int			pt_index			= Offset ;

            if (NoOfPoints != noOfDrawingPoints)
            {
                noOfDrawingPoints = NoOfPoints;
                drawingPoints = new Point[noOfDrawingPoints];
            }

            Point[] points = drawingPoints;

			for (current_point = 0; current_point < NoOfPoints; current_point++)
			{
				if (pt_index < 0)
				{
					pt_index = Trend.MAX_DATA_VALUES - 1 ;
				}
				//points[current_point] = new Point() ;
				points[current_point].X = RectRight - current_point - 1 + StartPoint ;
				//convert to fraction
                points[current_point].Y = point[pt_index--];
                if (points[current_point].Y == -1)
                {
                    break;
                }
			}

            if (current_point != NoOfPoints)
            {
                Point[] new_points = new Point[current_point];
                for (int index = 0; index < current_point; index++)
                {
                    new_points[index] = points[index];

                }
                points = new_points;
            }
            
            if (current_point > 2)
            {
                // Draw line.
                Pen pen = new Pen(lineColor, width);
                g.DrawLines(pen, points);
                pen.Dispose();
            }
		}

		#endregion

		#region Other Methods
		/// <summary>
		/// Resets the array of data held by each trace
		/// </summary>
		/// <param name="NoOfPixels">Number of pixels currently visible</param>
		/// <param name="RectBottom">Bottom of current scrolling area</param>
		protected internal void ResetData(int NoOfPixels, int RectBottom)
		{
//			for (int index = 0; index < NoOfPixels; index++) 
			for (int index = 0; index < Trend.MAX_DATA_VALUES; index++) 
			{
				//point[index] = RectBottom ;
                point[index] = -1;
            }
		}

        private double last_minimum = -1;
        private double last_range = -1;
        private bool autoRescale = false;

		/// <summary>
		/// Updates the data array held by the Trace because of scrolling
		/// </summary>
		/// <param name="ValueIndex">Index into Data array</param>
		/// <param name="yPtOffset">Offset for converting values to y-positions</param>
		/// <param name="yPtScale">Scale for converting values to y-positions</param>
		/// <param name="ScrollAmount">How far the Trend has scrolled since the last update</param>
		/// <param name="NoOfPixels">Total number of pixels in visible region</param>
		/// <param name="LeftScale">Left hand Scale object</param>
		/// <param name="RightScale">Right hand Scale object</param>
		protected internal void UpdateValues(long ValueIndex, int yPtOffset, double yPtScale, long ScrollAmount, int NoOfPixels, Scale LeftScale, Scale RightScale)
		{
            double current_fraction;
			double		current_value ;   
			int			current_y ;
			double		val_diff ;
			double		val_diff_per_pixel ;		//in value not in y_pos
			int			y_diff ;
			double		y_diff_per_pixel ;			//in y_pos not in value
			int			current_value_index ;
			double		last_value ;

            if (krtValue == KineticaRT.Values.Values.NotConnected) return;

			double	min ;
			double	range ;

			GetRange(LeftScale, RightScale, out min, out range) ;

            if (autoRescale && (min != last_minimum || range != last_range))
            {
                for (int index = 0; index < data.Length; index++)
                {
                    current_value = data[index];
                    current_fraction = (current_value - min) / range;
                    point[index] = (int)(yPtOffset - (yPtScale * current_fraction));
                }

                last_minimum = min;
                last_range = range;
            }

            current_value = (krtValue is Values.Double) ? krtValue.ToDouble() : System.Double.NaN;

            if (!System.Double.IsInfinity(current_value) && !System.Double.IsNaN(current_value))
            {
                current_fraction = (current_value - min) / range;
			}
			else
			{
				current_fraction = -0.02 ;
			}

			// Add the new pixels.
			current_value_index = (int)((ValueIndex + 1) % Trend.MAX_DATA_VALUES) ;
			current_y = (int)(yPtOffset - (yPtScale * current_fraction)) ;

			if ((ScrollAmount > 1) && (ScrollAmount < NoOfPixels))
			{
				last_value			= data [ValueIndex] ;
				val_diff			= current_value - last_value ;
				val_diff_per_pixel	= val_diff / (double)ScrollAmount ;
				y_diff				= current_y - lastY ;
				y_diff_per_pixel	= (double)y_diff / (double)ScrollAmount ;

				for (int index = 1; index < ScrollAmount ; index++) 
				{
					data[current_value_index] = last_value + (val_diff_per_pixel * index);
					point[current_value_index] = lastY + (int)(y_diff_per_pixel * index) ;

					current_value_index = (int)((current_value_index + 1) % Trend.MAX_DATA_VALUES) ;
				}
			}

			data[current_value_index] = current_value ;
			point[current_value_index] = current_y ;
			lastY = current_y ;
		}

//		private void GetRange(Scale LeftScale, Scale RightScale, out double Min, out double Range)
//		{
//			switch (relateToScale)
//			{
//				case VerticalScaleSides.Left:
//				{
//					Min = LeftScale.Minimum ;
//					Range = LeftScale.Maximum - Min ;
//				}
//				break ;
//
//				case VerticalScaleSides.Right:
//				{
//					Min = RightScale.Minimum ;
//					Range = RightScale.Maximum - Min ;
//				}
//				break ;
//				
//				default:
//				{
//					Min = minimum ;
//					Range = maximum - minimum ;
//				}
//				break ;
//			}
//		}

		private void GetRange(Scale LeftScale, Scale RightScale, out double Min, out double Range)
		{
			switch (rangeSource)
			{
				case TraceRangeSources.LeftScale:
				{
					Min = LeftScale.Minimum ;
					Range = LeftScale.Maximum - Min ;
				}
				break ;

				case TraceRangeSources.RightScale:
				{
					Min = RightScale.Minimum ;
					Range = RightScale.Maximum - Min ;
				}
				break ;
				
				case TraceRangeSources.Source:
				{
					if (sourceChannel is Channels.INumericChannel)
					{
						Channels.INumericChannel		num		= sourceChannel as Channels.INumericChannel ;
						Min = num.Minimum ;
						Range = num.Maximum - Min ;
					}
					else		//default to internal range
					{
						Min = minimum ;
						Range = maximum - minimum ;
					}
				}
				break;

				default:		//self
				{
					Min = minimum ;
					Range = maximum - minimum ;
				}
				break ;
			}
		}

		/// <summary>
		/// Returns the value of the Trace at the given index into its data array
		/// </summary>
		/// <param name="Offset">Index into data array</param>
		/// <returns>Value corresponding to given index</returns>
		public double GetPastValue(long Offset)
		{
			if (Offset >= 0 && Offset < Trend.MAX_DATA_VALUES)
			{
				return data[Offset] ;
			}
			else
			{
				return System.Double.NaN ;
			}
		}

		/// <summary>
		/// Returns the value of the Trace at the given index into its data array as a string.
		/// </summary>
		/// <param name="Offset">Index into data array</param>
		/// <returns>Value corresponding to given index as a string</returns>
		public string GetPastValueString(long Offset)
		{
			if (Offset >= 0 && Offset < Trend.MAX_DATA_VALUES)
			{
				KineticaRT.Channels.INumericChannel info_channel = this.sourceChannel as KineticaRT.Channels.INumericChannel ;
				if (info_channel == null)
				{
					return data[Offset].ToString() ;
				}
				else
				{
					return data[Offset].ToString(info_channel.Format) ;
				}
			}
			else
			{
				return System.Double.NaN.ToString() ;
			}
		}

		/// <summary>
		/// Sets a new value into the trace because of a user Drag
		/// </summary>	
		/// <param name="Fraction">Fraction to convert to value</param>
		/// <param name="LeftScale">Left hand Scale object</param>
		/// <param name="RightScale">Right hand Scale object</param>
		public void SetValueFromFraction(double Fraction, Scale LeftScale, Scale RightScale)
		{
			double			min ;
			double			range ;

			GetRange(LeftScale, RightScale, out min, out range) ;

			Value = new KineticaRT.Values.Double(Fraction * range + min) ;
		}

		#endregion

		#region Source Event handling (value and property)
		//new value coming in
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			Value = newValue ; //as KineticaRT.Values.Double ;
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
				//force a reformatting if required: same as if source channel changed
				OnPropertyChanged("SourceChannel") ;
			}
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
			if (destination != "")	DestinationChannel = OnObjectSearch(destination) as Channels.IChannel ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
			if (destinationChannel != null)		DestinationChannel = null ;
		}
		#endregion
	}
}
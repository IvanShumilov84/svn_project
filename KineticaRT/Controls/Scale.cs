using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Enumeration of positions for labels and marks relative to the scale line
	/// </summary>
	public enum ScaleMarkPositions
	{
		/// <summary>
		/// Inside the line (circular scales); Left for vertical scales; Above for Horizontal scales
		/// </summary>
		Inside,
		/// <summary>
		/// Outside the line (circular scales); Right for vertical scales; Below for Horizontal scales
		/// </summary>
		Outside,
		/// <summary>
		/// On the line
		/// </summary>
		On,
	}

	/// <summary>
	/// Scale used by various KineticaRT Controls
	/// </summary>
	[ChangeRecord ("1.0", "04 03 2003", "S. Child", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	//[TypeConverterAttribute (typeof(ExpandableObjectConverter))]
    [TypeConverterAttribute(typeof(ExpandableObjectTypeConverter))]
    [DefaultProperty("Maximum")]
    public class Scale : SubObject, ISupportInitialize
	{
		#region	Variables
		const double	VERY_SMALL_DOUBLE		= 0.0000000001 ;
		const float		VERY_SMALL_FLOAT		= 0.0000000001F ;

		//these define defaults which vary depending on parent object
		private	double				defaultPosition			= 0.8 ;
		private ScaleMarkPositions	defaultMarkPosition		= ScaleMarkPositions.Inside ;
		private ScaleMarkPositions	defaultLabelPosition	= ScaleMarkPositions.Inside ;

		//property variables
		private ThresholdCollection		thresholds ;
		private bool					enabled ;
		private bool					anticlockwise ;
		private double					zeroAngle ;			//in degrees, cw from top
		private double					sweepAngle ;		//in degrees
		private Color					color ;
		private bool					lineEnabled ;
		private double					lineWidth ;
		private int						labelAfter ;
		private double					maximum ;
		private double					minimum ;
		private double					majorMarksStart ;
		private double					majorMarksRepeat ;
		private int						minorMarksDivisions ;
		private ScaleMarkPositions		markPosition ;
		private bool					labelsEnabled ;
		private int						labelEvery ;
		private bool					labelNumeric ;
		private String[]				labelsText ;
		private int						decimalPlaces ;
		private ScaleMarkPositions		labelPosition ;
		private double					position ;
		private bool					autoAdjust ;
        private int                     engineNoRunZone1StartValue;
        private int                     engineNoRunZone1EndValue;
        private int                     engineNoRunZone2StartValue;
        private int                     engineNoRunZone2EndValue;

		//internal variables
		private bool							initialising ;
		private	string							numericLabelFormat ;
		private PropertyChangedEventHandler		propertyHdlr ;
		private ObjectSearchEventHandler		searchHdlr ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Scale class
		/// </summary>
		public Scale()
		{
			Initialise() ;
		}

		/// <summary>
		/// Initialises a new instance of the Scale class at the specified Position
		/// </summary>
		/// <param name="Position">Defaults the Position of the Scale to the specified value</param>
		public Scale(double Position)
		{
			defaultPosition			= Position ;

			Initialise() ;
		}

		/// <summary>
		/// Initialises a new instance of the Scale class with the specified Position, MarkPosition and LabelPosition
		/// </summary>
		/// <param name="Position">Defaults the Position of the Scale to the specified value</param>
		/// <param name="MarkPosition">Defaults the Position of the Marks to the specified value</param>
		/// <param name="LabelPosition">Defaults the Position of the Labels to the specified value</param>
		public Scale(double Position, ScaleMarkPositions MarkPosition, ScaleMarkPositions LabelPosition)
		{
			defaultPosition			= Position ;
			defaultMarkPosition		= MarkPosition ;
			defaultLabelPosition	= LabelPosition ;

			Initialise() ;
		}

		private void Initialise()
		{
			enabled			= true ;
			anticlockwise	= false ;
			lineEnabled		= true ;
			labelsEnabled	= true ;
			labelNumeric	= true ;
			autoAdjust		= true ;

			zeroAngle		= 210.0 ;
			sweepAngle		= 300.0 ;
			lineWidth		= 1.0 ;
			maximum			= 100.0 ;
			minimum			= 0.0 ;
			majorMarksStart = 0.0 ;
			majorMarksRepeat= 20.0 ;
			position		= defaultPosition ;
			markPosition	= defaultMarkPosition ;
			labelPosition	= defaultLabelPosition ;
			labelAfter		= 0 ;
			labelEvery		= 1 ;
			color			= Color.Black ;
			
			minorMarksDivisions = 4 ;
			decimalPlaces		= 0 ;
			numericLabelFormat	= "N0" ;

			labelsText = null ;
			thresholds = new ThresholdCollection() ;

			propertyHdlr = new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch) ;

			thresholds.PropertyChanged += propertyHdlr ;
			thresholds.ObjectSearch += searchHdlr ;

			initialising = false ;
		}

		#endregion

		#region ISupportInitialize
		/// <summary>
		/// Signals the object that initialization is starting.
		/// </summary>
		public void BeginInit()
		{
			initialising = true ;
		}

		/// <summary>
		/// Signals the object that initialization is complete.
		/// </summary>
		public void EndInit()
		{
			initialising = false ;
		}
		#endregion

		#region Properties
        private double scale = 1.0;
        /// <summary>
        /// Gets or sets the scaling factor applied to the Value property.
        /// </summary>
        [Category("Scale")]
        [Description("The scaling factor applied to the Value property.")]
        [DefaultValue(1.0)]
        public double ScalingFactor
        {
            get
            {
                return scale;
            }
            set
            {
                if (scale != value)
                {
                    scale = value;
                    OnPropertyChanged("ScalingFactor");
                }
            }
        }

        ///<summary>
		///Gets or Sets whether automatic adjustment of MajorMarksStart, MajorMarksRepeat
		///MinorMarksDivisions and DecimalPlaces occurs
		///</summary>
		[Category("Scale")]
		[DefaultValue(true)]
		[Description("Gets or Sets whether automatic adjustment of MajorMarksStart, MajorMarksRepeat, MinorMarksDivisions and DecimalPlaces occurs")]
		public bool AutoAdjust
		{
			get
			{
				return autoAdjust ;
			}
			set
			{
				if (autoAdjust != value)
				{
					autoAdjust = value ;
					AutoAdjustMarks() ;
					AutoAdjustDps() ;
					OnPropertyChanged("AutoAdjust") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the position of the Scale line as proportion of the parent Control (0..1)
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the position of the Scale line as proportion of the parent Control (0..1)")]
		//default value is variable
		public double Position
		{
			get
			{
				return position ;
			}
			set
			{
				if (position != value)
				{
					position = value ;
					if (position > 1)		position = 1 ;
					if (position < 0)		position = 0 ;

					OnPropertyChanged("Position") ;
				}
			}
		}
		private bool ShouldSerializePosition()
		{
			return position != defaultPosition ;
		}
		/// <summary>
		/// Reset the Position to its default setting
		/// </summary>
		public void ResetPosition()
		{
			position = defaultPosition ;
		}

		///<summary>
		///Gets or Sets whether the Scale increases in an Anticlockwise direction (down for vertical or right to left for horizontal)
		///</summary>
		[Category("Scale")]
		[DefaultValue(false)]
		[Description("Gets or Sets whether the scale increases in an Anticlockwise direction (down for vertical or right to left for horizontal)")]
		public bool AntiClockwise
		{
			get
			{
				return anticlockwise ;
			}
			set
			{
				if (anticlockwise != value)
				{
					anticlockwise = value ;

					OnPropertyChanged("AntiClockwise") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the zero (start) angle of the Scale (Circular scales only). Degrees clockwise from top.
		///</summary>
		[Category("Scale")]
		[DefaultValue(210.0)]
		[Description("Gets or Sets the zero (start) angle of the Scale(Circular scales only). Degrees clockwise from top.")]
		public double ZeroAngle
		{
			get
			{
				return zeroAngle ;
			}
			set
			{
				if (zeroAngle != value)
				{
					zeroAngle = value ;
					while (zeroAngle > 360)			zeroAngle -= 360 ;
					while (zeroAngle < 0)			zeroAngle += 360 ;

					OnPropertyChanged("ZeroAngle") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the Sweep angle of the Scale, in Degrees (circular scales only).
		///</summary>
		[Category("Scale")]
		[DefaultValue(300.0)]
		[Description("Gets or Sets the sweep angle of the Scale, in Degrees (circular scales only).")]
		public double SweepAngle
		{
			get
			{
				return sweepAngle ;
			}
			set
			{
				if (sweepAngle != value)
				{
					sweepAngle = value ;
					while (sweepAngle > 360)		sweepAngle -= 360 ;
					while (sweepAngle < 0)			sweepAngle += 360 ;

					OnPropertyChanged("SweepAngle") ;
				}
			}
		}

		///<summary>
		///Gets or Sets whether labels are based on the numeric value, or custom text
		///</summary>
		[Category("Scale")]
		[DefaultValue(true)]
		[Description("Gets or Sets whether labels are based on the numeric value, or custom text")]
		public bool LabelNumeric
		{
			get
			{
				return labelNumeric ;
			}
			set
			{
				if (labelNumeric != value)
				{
					labelNumeric = value ;

					OnPropertyChanged("LabelNumeric") ;
				}
			}
		}

		///<summary>
		///Gets or Sets a collection of Text labels
		///</summary>
		[Category("Scale")]
		[DefaultValue(null)]
		[Description("Gets or Sets a collection of Text labels")]
		public String[] LabelsText
		{
			get
			{
				return labelsText ;
			}
			set
			{
				if (labelsText != value)
				{
					labelsText = value ;
					OnPropertyChanged("LabelsText") ;
				}
			}
		}

		///<summary>
		///Gets or Sets a collection of Thresholds (sorted in value order)
		///</summary>
		[Category("Scale")]
		//[Editor(typeof(ThresholdEditor), typeof(UITypeEditor))]
		[Description("Gets or Sets a collection of Threshold objects (sorted in value order)")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)] 
		[Bindable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public ThresholdCollection Thresholds
		{
			get
			{
				return thresholds ;
			}
			set
			{
				if (thresholds != null)
				{
					thresholds.PropertyChanged	-= propertyHdlr ;
					thresholds.ObjectSearch		-= searchHdlr ;
				}

				thresholds = value ;
				if (thresholds != null)
				{
					thresholds.PropertyChanged	+= propertyHdlr ;
					thresholds.ObjectSearch		+= searchHdlr ;
				}

				OnPropertyChanged("Thresholds") ;
			}
		}
		private bool ShouldSerializeThresholds()
		{
			//always save if there are any items in the collection
			return thresholds.Count > 0 ;
		}

		/// <summary>
		/// Reset the Threshold Collection to its default settings
		/// </summary>
		public void ResetThresholds()
		{
			thresholds = new ThresholdCollection() ;
		}

		///<summary>
		///Gets or Sets whether the Scale is visible or not
		///</summary>
		[Category("Scale")]
		[DefaultValue(true)]
		[Description("Gets or Sets whether the Scale is visible or not")]
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

		///<summary>
		///Gets or Sets the color of the Scale (lines and text)
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the color of the Scale (lines and text)")]
		public Color Color
		{
			get
			{
				return color ;
			}
			set
			{
				if (color != value)
				{
					color = value ;
					OnPropertyChanged("Color") ;
				}
			}
		}
		private bool ShouldSerializeColor()
		{
			return color != Color.Black ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetColor()
		{
			color = Color.Black ;
		}

		///<summary>
		///Gets or Sets whether the Scale Line is Enabled
		///</summary>
		[Category("Scale")]
		[DefaultValue(true)]
		[Description("Gets or Sets whether the Scale Line is Enabled")]
		public bool LineEnabled
		{
			get
			{
				return lineEnabled ;
			}
			set
			{
				if (lineEnabled != value)
				{
					lineEnabled = value ;
					OnPropertyChanged("LineEnabled") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the Width of the Scale line in pixels
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the width of the Scale line in pixels")]
		[DefaultValue(1.0)]
		public double LineWidth
		{
			get
			{
				return lineWidth ;
			}
			set
			{
				if (lineWidth != value)
				{
					lineWidth = value ;
					if (lineWidth < 0)		position = 0 ;

					OnPropertyChanged("LineWidth") ;
				}
			}
		}

		///<summary>
		///Gets or Sets how many major marks occur before labels start
		///</summary>
        [Category("Scale")]
		[Description("Gets or Sets how many major marks occur before labels start")]
		[DefaultValue(0)]
		public int LabelAfter
		{
			get
			{
				return labelAfter ;
			}
			set
			{
				if (labelAfter != value)
				{
					labelAfter = value ;
					if (labelAfter < 0)				labelAfter = 0 ;

					OnPropertyChanged("LabelAfter") ;
				}
			}
		}

        /// <summary>
        /// Gets or sets engine no run zone1 starting value
        /// </summary>
        [Category("Dial")]
        [Description("Gets or sets engine no run zone1 starting value")]
        [DefaultValue(0)]
        public int EngineNoRunZone1StartValue
        {
            get
            {
                return engineNoRunZone1StartValue;
            }
            set
            {
                if (value != engineNoRunZone1StartValue)
                {
                    engineNoRunZone1StartValue = value;
                    OnPropertyChanged("EngineNoRunZone1StartValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets engine no run zone1 ending value
        /// </summary>
        [Category("Scale")]
        [Description("Gets or sets engine no run zone1 ending value")]
        [DefaultValue(0)]
        public int EngineNoRunZone1EndValue
        {
            get
            {
                return engineNoRunZone1EndValue;
            }
            set
            {
                if (value != engineNoRunZone1EndValue)
                {
                    engineNoRunZone1EndValue = value;
                    OnPropertyChanged("EngineNoRunZone1EndValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets engine no run zone2 starting value
        /// </summary>
        [Category("Scale")]
        [Description("Gets or sets engine no run zone2 starting value")]
        [DefaultValue(0)]
        public int EngineNoRunZone2StartValue
        {
            get
            {
                return engineNoRunZone2StartValue;
            }
            set
            {
                if (value != engineNoRunZone2StartValue)
                {
                    engineNoRunZone2StartValue = value;
                    OnPropertyChanged("EngineNoRunZone2StartValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets engine no run zone2 ending value
        /// </summary>
        [Category("Scale")]
        [Description("Gets or sets engine no run zone2 ending value")]
        [DefaultValue(0)]
        public int EngineNoRunZone2EndValue
        {
            get
            {
                return engineNoRunZone2EndValue;
            }
            set
            {
                if (value != engineNoRunZone2EndValue)
                {
                    engineNoRunZone2EndValue = value;
                    OnPropertyChanged("ExclusionZone2EndValue");
                }
            }
        }

        ///<summary>
		///Gets or Sets the Maximum value shown
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the Maximum value shown")]
		[DefaultValue(100.0)]
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
					if (!initialising)
					{
						AutoAdjustMarks() ;
						AutoAdjustDps() ;
					}
					OnPropertyChanged("Maximum") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the Minimum Value shown
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the Minimum value shown")]
		[DefaultValue(0.0)]
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
					if (!initialising)
					{
						AutoAdjustMarks() ;
						AutoAdjustDps() ;
					}
					OnPropertyChanged("Minimum") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the value at which the first Major mark is drawn
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the value at which the first Major mark is drawn")]
		[DefaultValue(0.0)]
		public double MajorMarksStart
		{
			get
			{
				return majorMarksStart ;
			}
			set
			{
				if (majorMarksStart != value)
				{
					majorMarksStart = value ;
					OnPropertyChanged("MajorMarksStart") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the Interval between major marks
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the interval between major marks")]
		[DefaultValue(20.0)]
		public double MajorMarksRepeat
		{
			get
			{
				return majorMarksRepeat ;
			}
			set
			{
				if (majorMarksRepeat != value)
				{
					majorMarksRepeat = value ;
					if (majorMarksRepeat < 0)		majorMarksRepeat = 0 ;

					OnPropertyChanged("MajorMarksRepeat") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the number of divisions between major marks
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the number of divisions between major marks")]
		[DefaultValue(4)]
		public int MinorMarksDivisions
		{
			get
			{
				return minorMarksDivisions ;
			}
			set
			{
				if (minorMarksDivisions != value)
				{
					minorMarksDivisions = value ;
					if (minorMarksDivisions < 0)		minorMarksDivisions = 0 ;

					OnPropertyChanged("MinorMarksDivisions") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the position of marks relative to the Scale line
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the position of marks relative to the Scale line")]
		//default value is variable
		public ScaleMarkPositions MarkPosition
		{
			get
			{
				return markPosition ;
			}
			set
			{
				if (markPosition != value)
				{
					markPosition = value ;
					OnPropertyChanged("MarkPosition") ;
				}
			}
		}
		private bool ShouldSerializeMarkPosition()
		{
			return markPosition != defaultMarkPosition ;
		}
		/// <summary>
		/// Reset the MarkPosition to its default setting
		/// </summary>
		public void ResetMarkPosition()
		{
			markPosition = defaultMarkPosition ;
		}

		///<summary>
		///Gets or Sets whether labels are shown
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets whether labels are shown")]
		[DefaultValue(true)]
		public bool LabelsEnabled
		{
			get
			{
				return labelsEnabled ;
			}
			set
			{
				if (labelsEnabled != value)
				{
					labelsEnabled = value ;
					OnPropertyChanged("LabelsEnabled") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the number of major marks between labels
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the number of major marks between labels")]
		[DefaultValue(1)]
		public int LabelEvery
		{
			get
			{
				return labelEvery ;
			}
			set
			{
				if (labelEvery != value)
				{
					labelEvery = value ;
					if (labelEvery < 1)		labelEvery = 1 ;

					OnPropertyChanged("LabelEvery") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the number of decimal places on numeric labels
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the number of decimal places on numeric labels")]
		[DefaultValue(0)]
		public int DecimalPlaces
		{
			get
			{
				return decimalPlaces ;
			}
			set
			{
				if (decimalPlaces != value)
				{
					decimalPlaces = value ;
					if (decimalPlaces < 0)		decimalPlaces = 0 ;

					numericLabelFormat = "N" + decimalPlaces.ToString() ;

					OnPropertyChanged("DecimalPlaces") ;
				}
			}
		}

		///<summary>
		///Gets or Sets the position of labels relative to the Scale line
		///</summary>
		[Category("Scale")]
		[Description("Gets or Sets the position of labels relative to the Scale line")]
		//default value is variable
		public ScaleMarkPositions LabelPosition
		{
			get
			{
				return labelPosition ;
			}
			set
			{
				if (labelPosition != value)
				{
					labelPosition = value ;
					OnPropertyChanged("LabelPosition") ;
				}
			}
		}
		private bool ShouldSerializeLabelPosition()
		{
			return labelPosition != defaultLabelPosition ;
		}
		/// <summary>
		/// Reset the LabelPosition to its default setting
		/// </summary>
		public void ResetLabelPosition()
		{
			labelPosition = defaultLabelPosition ;
		}
		
		#endregion

		#region Drawing
		/// <summary>
		/// Draws the Scale
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		/// <param name="Orientation">Whether to draw a Horizontal, Vertical or Circular scale</param>
		/// <param name="LabelFont">Font with which to draw the labels</param>
		/// <param name="Ratio">Proportion of the DrawRect to use</param>
		public void Draw(Graphics g, Rectangle DrawRect, ScaleOrientations Orientation, Font LabelFont, double Ratio)
		{
			if (Orientation == ScaleOrientations.Circular)
			{
				int			scale_x_offset ;
				int			scale_y_offset ;

				//calculate offset for scale radius proportion
				scale_x_offset = (int)(0.5 * (1 - position) * Ratio * DrawRect.Width) ;
				scale_y_offset = (int)(0.5 * (1 - position) * Ratio * DrawRect.Height) ;

				DrawRect.Inflate(-scale_x_offset, -scale_y_offset) ;
			}

			SetUpScaling(DrawRect) ;

			//if area has no width or height, don't even try and draw
			if (xScale <= 0 || yScale <= 0)		return ;

			switch (Orientation)
			{
				case ScaleOrientations.Circular:
				{
					DrawCircularScale (g, LabelFont, Ratio) ;
				}
				break ;
				
				case ScaleOrientations.Vertical:
				{
					DrawVerticalScale (g, LabelFont, Ratio) ;
				}
				break ;

				case ScaleOrientations.Horizontal:
				{
					DrawHorizontalScale (g, LabelFont, Ratio) ;
				}
				break ;
			}
		}

        private void DrawEngineNoRunZoneArc(Graphics g, int startZone, int endZone, double start, double sweep)
        {
            Pen engineNoRunLine_pen = new Pen(Color.Gray, 6);
            Rectangle zoneRect = new Rectangle(xOffset + 3, yOffset + 3, xScale - 6, yScale - 6);
            if (startZone < endZone &&
                startZone >= minimum &&
                endZone <= maximum)
            {
                double zoneStart = startZone / (maximum - minimum + 1) * sweep + start; // + 1 not to divide by 0
                double zoneSweep = endZone / (maximum - minimum + 1) * sweep + start - zoneStart;

                if (!anticlockwise)
                {
                    g.DrawArc(engineNoRunLine_pen, zoneRect, (float)zoneStart, (float)zoneSweep);
                }
                else
                {
                    g.DrawArc(engineNoRunLine_pen, zoneRect, (float)zoneStart, (float)-zoneSweep);
                }
            }
        }

		private void CalcPoint (float length, ref System.Drawing.PointF point)
		{
			float a = xScale * length * 0.5F ;
			float b = yScale * length * 0.5F ;
			float a2 = a*a ;
			float b2 = b*b ;
			float rd = (float)Math.Sqrt((b2*a2) / (b2 * sin_theta2 + a2 * cos_theta2));
			//point = new PointF((float)(xOffset + xScale * 0.5 + rd * sin_theta), (float)(yOffset + yScale * 0.5 + rd * cos_theta)) ;
            point.X = (float)(xOffset + xScale * 0.5 + rd * sin_theta);
            point.Y = (float)(yOffset + yScale * 0.5 + rd * cos_theta);
        }
		private float  sin_theta ;
		private float  cos_theta ;
		private float  sin_theta2 ;
		private float  cos_theta2 ;


		private void DrawCircularScale(Graphics g, Font LabelFont, double ratio)
		{
			float              val				= (float)majorMarksStart ;
			float              range ;                                    
			float              increment ;
			float              max				= (float)maximum ;
			float				min				= (float)minimum ;
			//float				start_angle		= (float)zeroAngle - 90 ;	//Arc has x axis as origin
			float				sweep_angle		= (float)sweepAngle ;
			int					major_count ;
			int					label_count ;
			Pen					line_pen		= new Pen(color, (float)lineWidth) ;

			if (!enabled) return ;

//			if (start_angle < 0)
//			{
//				start_angle += 360 ;
//			}

            if (minimum > maximum)
            {
                //max and min are the wrong way around!
                min = (float)maximum;
                max = (float)minimum;
            }
            
            if (lineEnabled)
			{
				Rectangle		rect	= new Rectangle(xOffset, yOffset, xScale, yScale) ;
				double			start	= zeroAngle ;
				double			sweep	= sweep_angle ;

				//Cannot use start_angle and sweep_angle,
				//as they don't take non-squareness into account
				ScaleAngles(anticlockwise, ref start, ref sweep) ;

				//NB: DrawArc goes clockwise from 3 o'clock
				if (!anticlockwise)
				{
					g.DrawArc(line_pen, rect, (float)start, (float)sweep) ;
				}
				else
				{
					g.DrawArc(line_pen, rect, (float)start, (float)-sweep) ;
				}

                // Eegine no run warning sections
                DrawEngineNoRunZoneArc(g, engineNoRunZone1StartValue, engineNoRunZone1EndValue, start, sweep);
                DrawEngineNoRunZoneArc(g, engineNoRunZone2StartValue, engineNoRunZone2EndValue, start, sweep);
            }
    
			major_count = 0 ;
			label_count = labelAfter ;

			if (minimum > maximum)
			{
				//max and min are the wrong way around!
				min = (float)maximum ;
				max = (float)minimum ;
			}

			// Find the first minor mark.
			// we need to check that the start value is >= minimum value
			if (val < min)
			{
				val = min ;
			}
			increment = System.Math.Abs((float)majorMarksRepeat / minorMarksDivisions) ;
			while (val >= (min + increment))
			{
				val -= increment;
				major_count ++ ;
			}
        
			// Find the range.        
			range = (max - min) ;
                         
			// Draw.                 
			if (range > 0)
			{
				bool        started_major	= false ;
				PointF		start_point		= new PointF() ;
				PointF		end_point		= new PointF() ;
				int			label_number	= 0;
				Brush		brush			= new SolidBrush(color) ;
        
				//max += increment / 2;
				max += VERY_SMALL_FLOAT ;		//skc 4/3/03
				for ( ; val <= max; val += increment)
				{                                       
					float  fractional_value ;
					float  display_angle ;
            
					fractional_value = (val - min) / range ;
					if (!anticlockwise)
					{
						display_angle = (float)zeroAngle + (sweep_angle * fractional_value) ;                     
					}
					else
					{
						display_angle = (float)zeroAngle - (sweep_angle * fractional_value) ;
					}

					//display_angle += 90 ;
					if (display_angle >= 360)
					{
						display_angle -= 360 ;
					}
					else if (display_angle < 0)
					{
						display_angle += 360 ;
					}

					sin_theta = (float)Math.Sin (display_angle * Math.PI / 180) ;
					cos_theta = (float)Math.Cos ((display_angle + 180)* Math.PI / 180) ;
					cos_theta2 = cos_theta * cos_theta ;
					sin_theta2 = sin_theta * sin_theta ;

					if ((major_count != 0) || (!started_major && val < (majorMarksStart - VERY_SMALL_DOUBLE)))
					{
						// Minor mark.
						if (markPosition == ScaleMarkPositions.Inside)
						{
                            CalcPoint(0.96F, ref start_point);
							CalcPoint (1.00F, ref end_point) ;
						}
						else if (markPosition == ScaleMarkPositions.Outside)
						{
                            CalcPoint(1.00F, ref start_point);
                            CalcPoint(1.04F, ref end_point);
						}
						else		//On line
						{
                            CalcPoint(0.98F, ref start_point);
                            CalcPoint(1.02F, ref end_point);
						}

						g.DrawLine(line_pen, start_point, end_point) ;

						if (major_count == 0)
						{
							major_count = minorMarksDivisions ;
						} 
					}
					else
					{  
						// Major mark
						started_major = true ;

						if (markPosition == ScaleMarkPositions.Inside)
						{
                            CalcPoint(0.90F, ref start_point);
                            CalcPoint(1.00F, ref end_point);
						}
						else if (markPosition == ScaleMarkPositions.Outside)
						{
                            CalcPoint(1.10F, ref start_point);
                            CalcPoint(1.00F, ref end_point);
						}
						else		//On line
						{
                            CalcPoint(0.95F, ref start_point);
                            CalcPoint(1.05F, ref end_point);
						} 

						g.DrawLine(line_pen, start_point, end_point) ;
                            
						if ((label_count == 0) && labelsEnabled)
						{
							string		label ;
							SizeF		size ;

							label = GetLabel(val, label_number) ;
							label_number++ ;

							size = g.MeasureString(label, LabelFont) ;

							if (labelPosition == ScaleMarkPositions.Inside)
							{
								if (markPosition == ScaleMarkPositions.Outside)
								{
                                    CalcPoint(0.95F, ref start_point);
								}
								else
								{
                                    CalcPoint(0.89F, ref start_point);
								}
								start_point = InteriorAdjustment(size, display_angle, start_point) ;
							}
							else		//outside
							{
								if (markPosition == ScaleMarkPositions.Outside)
								{
                                    CalcPoint(1.11F, ref start_point);
								}
								else
								{
                                    CalcPoint(1.05F, ref start_point);
								}
								start_point = ExteriorAdjustment(size, display_angle, start_point) ;
							}

							g.DrawString(label, LabelFont, brush, start_point.X, start_point.Y) ;
							label_count = labelEvery ;
						}   

						label_count -- ;
						major_count = minorMarksDivisions ;
					}                                              
                    
					major_count -- ;
				}    
				brush.Dispose() ;

				Rectangle		rect = new Rectangle(xOffset, yOffset, xScale, yScale) ;
				thresholds.Draw(g, ratio, rect, ScaleOrientations.Circular, position, this) ;
			}

			line_pen.Dispose() ;
		}

		private void DrawHorizontalScale(Graphics g, Font LabelFont, double ratio)
		{
			double              val				= majorMarksStart ;
			double				range ;                                    
			double				increment;
			int					major_count ;
			int					label_count ;                   
			double				right			= 1 - ratio ;
			double				left			= 0 + ratio ;
			double              max				= maximum ;
			double				min				= minimum ;
			Pen					line_pen		= new Pen(color, (float)lineWidth) ;

			if (!enabled)		return ;

			if (lineEnabled)
			{
				g.DrawLine(line_pen, ScaleX(left), ScaleY(position), ScaleX(right), ScaleY(position)) ;
			}
    
			// Draw marks.
			major_count = 0 ;
			label_count = labelAfter;

			if (minimum > maximum)
			{
				//max and min are the wrong way around!
				min = maximum ;
				max = minimum ;
			}

			// Find the first minor mark.
			// we need to check that the start value is >= minimum value
			if (val < min)
			{
				val = min ;
			}
			increment = System.Math.Abs(majorMarksRepeat / minorMarksDivisions) ;
			while (val >= (min + increment))
			{
				val -= increment;
				major_count ++ ;
			}
        
			// Find the range.        
			range = (max - min) ;
                     
			// Draw.                 
			if (range > 0)
			{
				bool        started_major	= false;
				int			label_number	= 0;
				Brush		brush			= new SolidBrush(color) ;
        
				//max += increment / 2;
				max += VERY_SMALL_DOUBLE ;		//skc 4/3/03
				for ( ; val <= max ; val += increment )
				{                                       
					double	fractional_value ;
					int		display_value ;
            
					fractional_value = (val - min) / range ;

					if (anticlockwise)
					{
						display_value = ScaleX (right - (right - left) * fractional_value) ;        
					}
					else
					{
						display_value = ScaleX (left + (right - left) * fractional_value) ;        
					}

					if ( (major_count != 0) || (!started_major && val < (majorMarksStart - VERY_SMALL_DOUBLE)))
					{ 
						// Minor mark.
						if (markPosition == ScaleMarkPositions.Inside)
						{
							g.DrawLine(line_pen, display_value, ScaleY (position - 0.05), display_value, ScaleY (position)) ; 
						}
						else if (markPosition == ScaleMarkPositions.Outside)
						{
							g.DrawLine(line_pen, display_value, ScaleY (position + 0.05), display_value, ScaleY (position)) ; 
						}
						else
						{
							g.DrawLine(line_pen, display_value, ScaleY (position + 0.025), display_value, ScaleY (position - 0.025) - 1) ; 
						}

						if (major_count == 0)
						{                    
							major_count = minorMarksDivisions ;
						}
					}
					else
					{  
						// Major mark.                  
						started_major = true;

						if (markPosition == ScaleMarkPositions.Inside)
						{
							g.DrawLine(line_pen, display_value, ScaleY (position - 0.1), display_value, ScaleY (position)) ; 
						}
						else if (markPosition == ScaleMarkPositions.Outside)
						{
							g.DrawLine(line_pen, display_value, ScaleY (position + 0.1), display_value, ScaleY (position)) ; 
						}
						else
						{
							g.DrawLine(line_pen, display_value, ScaleY (position + 0.05), display_value, ScaleY (position - 0.05) - 1) ; 
						}

						if ((label_count == 0) && labelsEnabled)
						{
							string		label ;
							SizeF		size		= new SizeF() ;
							int			start_y ;

							label = GetLabel(val, label_number) ;
							label_number++ ;

							size = g.MeasureString(label, LabelFont) ;

							if (labelPosition == ScaleMarkPositions.Inside)		//ie above
							{
								if (markPosition == ScaleMarkPositions.Outside)		//below
								{
									start_y  = ScaleY (position - 0.05) - (int) size.Height ;
								}
								else	//above
								{
									start_y  = ScaleY (position - 0.1) - (int) size.Height ;
								}
							}
							else		//below
							{
								if (markPosition == ScaleMarkPositions.Outside)		//below
								{
									start_y  = ScaleY (position + 0.1) ;
								}
								else	//above
								{
									start_y  = ScaleY (position + 0.05) ;
								}
							}

							g.DrawString(label, LabelFont, brush, (int)(display_value - 0.5 * size.Width), start_y) ;
							label_count = labelEvery ;
						}
                
						label_count -- ;
						major_count = minorMarksDivisions ;
					}                                              
                
					major_count -- ;
				}  
				brush.Dispose() ;
	
				Rectangle		rect = new Rectangle(xOffset, yOffset, xScale, yScale) ;
				thresholds.Draw(g, ratio, rect, ScaleOrientations.Horizontal, position, this) ;
			}
			line_pen.Dispose() ;
		}

		private void DrawVerticalScale(Graphics g, Font LabelFont, double ratio)
		{
			double              val				= majorMarksStart ;
			double				range ;                                    
			double				increment;
			int					major_count ;
			int					label_count ;                   
			double				bottom			= 0 + ratio ;
			double				top				= 1 - ratio ;
			double              max				= maximum ;
			double				min				= minimum ;
			Pen					line_pen		= new Pen(color, (float)lineWidth) ;

			if (!enabled)		return ;

			if (lineEnabled)
			{
				g.DrawLine(line_pen, ScaleX(position), ScaleY(top), ScaleX(position), ScaleY(bottom)) ;
			}
    
			// Draw marks.
			major_count = 0 ;
			label_count = labelAfter;

			if (minimum > maximum)
			{
				//max and min are the wrong way around!
				min = maximum ;
				max = minimum ;
			}

			// Find the first minor mark.
			// we need to check that the start value is >= minimum value
			if (val < min)
			{
				val = min ;
			}
			increment = System.Math.Abs(majorMarksRepeat / minorMarksDivisions) ;
			while (val >= (min + increment))
			{
				val -= increment;
				major_count ++ ;
			}
        
			// Find the range.        
			range = (max - min) ;
                     
			// Draw.                 
			if (range > 0)
			{
				bool        started_major	= false;
				int			label_number	= 0;
				Brush		brush			= new SolidBrush(color) ;
        
				//max += increment / 2;
				max += VERY_SMALL_DOUBLE ;		//skc 4/3/03
				for ( ; val <= max ; val += increment )
				{                                       
					double	fractional_value ;
					int		display_value ;
            
					fractional_value = (val - min) / range ;

					if (anticlockwise)
					{
						display_value = ScaleY (bottom + (top - bottom) * fractional_value) ;        
					}
					else
					{
						display_value = ScaleY (top - (top - bottom) * fractional_value) ;        
					}

					if ( (major_count != 0) || (!started_major && val < (majorMarksStart - VERY_SMALL_DOUBLE)))
					{ 
						// Minor mark.
						if (markPosition == ScaleMarkPositions.Inside)
						{
							g.DrawLine(line_pen, ScaleX (position - 0.05), display_value, ScaleX (position), display_value) ; 
						}
						else if (markPosition == ScaleMarkPositions.Outside)
						{
							g.DrawLine(line_pen, ScaleX (position + 0.05), display_value, ScaleX (position), display_value) ; 
						}
						else
						{
							g.DrawLine(line_pen, ScaleX (position + 0.025), display_value, ScaleX (position - 0.025) - 1, display_value) ; 
						}

						if (major_count == 0)
						{                    
							major_count = minorMarksDivisions ;
						}
					}
					else
					{  
						// Major mark.                  
						started_major = true;

						if (markPosition == ScaleMarkPositions.Inside)
						{
							g.DrawLine(line_pen, ScaleX (position - 0.1), display_value, ScaleX (position), display_value) ; 
						}
						else if (markPosition == ScaleMarkPositions.Outside)
						{
							g.DrawLine(line_pen, ScaleX (position + 0.1), display_value, ScaleX (position), display_value) ; 
						}
						else
						{
							g.DrawLine(line_pen, ScaleX (position + 0.05), display_value, ScaleX (position - 0.05) - 1, display_value) ; 
						}

						if ((label_count == 0) && labelsEnabled)
						{
							string		label ;
							SizeF		size		= new SizeF() ;
							int			start_x ;

							label = GetLabel(val, label_number) ;
							label_number++ ;

							size = g.MeasureString(label, LabelFont) ;

							if (labelPosition == ScaleMarkPositions.Inside)		//ie left
							{
								if (markPosition == ScaleMarkPositions.Outside)		//right
								{
									start_x  = ScaleX (position - 0.1) - (int) size.Width ;
								}
								else	//left
								{
									start_x  = ScaleX (position - 0.15) - (int) size.Width ;
								}
							}
							else		//right
							{
								if (markPosition == ScaleMarkPositions.Outside)		//right
								{
									start_x  = ScaleX (position + 0.15) ;
								}
								else	//left
								{
									start_x  = ScaleX (position + 0.1) ;
								}
							}

							g.DrawString(label, LabelFont, brush, start_x, (int)(display_value - 0.5 * size.Height)) ;
							label_count = labelEvery ;
						}
                
						label_count -- ;
						major_count = minorMarksDivisions ;
					}                                              
                
					major_count -- ;
				}  

				brush.Dispose() ;
	
				Rectangle		rect = new Rectangle(xOffset, yOffset, xScale, yScale) ;
				thresholds.Draw(g, ratio, rect, ScaleOrientations.Vertical, position, this) ;//ft Draw threhold marks on the bargraph (curseur)
			}

			line_pen.Dispose() ;
		}

		//  calculates an adjustment for scales labelled on the outside
		private PointF ExteriorAdjustment
		(
			SizeF	size,		//text extent
			double  angle,		//display angle (degrees)
			PointF	point		//point for adjusting
		)
		{
			PointF	adjusted = point ;

			//x
			if ( angle >= 225.0 && angle <= 315.0)
			{
				adjusted.X -= (int)size.Width ;
			}
			else if ( angle >= 45.0 && angle <= 135.0)
			{
				//no adjustment
			}
			else if ( angle >= 135.0 && angle <= 225.0)
			{
				adjusted.X -= (int)( ( (angle - 135) * size.Width) / 90.0 ) ;
			}
			else
			{
				if (angle <= 45)		angle += 360 ;

				adjusted.X -= (int)( ( (405 - angle) * size.Width) / 90.0 ) ;
			}

			//y
			if ( angle >= 45.0 && angle <= 135.0)
			{
				adjusted.Y -= (int)( ( (135 - angle) * size.Height) / 90.0 ) ;
			}
			else if ( angle >= 225.0  && angle <= 315.0)
			{
				adjusted.Y -= (int)( ( (angle - 225.0) * size.Height) / 90.0 ) ;
			}
			else if ( angle >= 135.0 && angle <= 225.0)
			{
				//no adjustment
			}
			else
			{
				adjusted.Y -= (int)size.Height ;
			}

			return adjusted ;
		}

		//calculates an adjustment for scales labelled on the inside
		private PointF InteriorAdjustment
		(
			SizeF	size,		//text extent
			double  angle,		//display angle (degrees)
			PointF	point		//point for adjusting
		)
		{
			PointF	adjusted = point ;

			//x
			if ( angle >= 200.0 && angle <= 340.0)
			{
				//no adjustment
			}
			else if ( angle >= 20.0 && angle <= 160.0)
			{
				adjusted.X -= (int)size.Width ;
			}
			else if ( angle >= 160.0 && angle <= 200.0)
			{
				adjusted.X -= (int)( ( (200.0 - angle) * size.Width) / 40.0 ) ;
			}
			else
			{
				if (angle <= 20)		angle += 360 ;

				adjusted.X -= (int)( ( (angle - 340.0) * size.Width) / 40.0 ) ;
			}

			//y
			if ( angle >= 50.0 && angle <= 130.0)
			{
				adjusted.Y -= (int)( ( (angle - 50.0) * size.Height) / 80.0 ) ;
			}
			else if ( angle >= 230.0  && angle <= 310.0)
			{
				adjusted.Y -= (int)( ( (310.0 - angle) * size.Height) / 80.0 ) ;
			}
			else if ( angle >= 130.0 && angle <= 230.0)
			{
				adjusted.Y -= (int) size.Height ;
			}
			else
			{
				//no adjustment
			}

			return adjusted ;
		}

		// Gets label corresponding to value / nth string
		private string GetLabel(double value, int labelIndex)
		{
			string		label ;

			if (!labelNumeric)		//text labels
			{
                if (labelsText.Length > labelIndex)
				{
                    label = labelsText[labelIndex];
				}
				else
				{
					label = "" ;
				}
			}
			else		//numeric labels
			{
//				string		format ;
//
//				format = "N" ;
//				format += decimalPlaces.ToString() ;

				label = (value * scale).ToString(numericLabelFormat) ;
			}

			return label ;
		}

		#endregion

		#region Other Methods
		//Calculates MajorMarksStart, MajorMarksRepeat and MinorMarksDivisions
		//based on the current min and max values
		//returns true if changed something
		private bool AutoAdjustMarks()
		{
			bool		changed				= false ;
	
			if (autoAdjust)
			{
				double		range		= maximum - minimum ;
		
				if (range > 0.0)
				{
					double		interval ;			//size of interval
					int			index ;
					double		power_ten ;			//a power of 10
					double		repeat ;
					double		min_mult ;
					int			minor ;

					// Calculate the smallest potential interval size
					interval = range / 7;

					// Calculate the smallest nice number not smaller than interval
					repeat = FirstNiceNum(interval, out index, out power_ten) ;

					while (repeat < interval)
					{
						repeat = NextNiceNum(ref index, ref power_ten) ;
					}

					//Produce the internal scale using the specified nice number
					min_mult = Math.Ceiling(minimum / repeat) ;

					// Perform some extra checking
					if ((min_mult - 1.0) * repeat >= minimum)
					{
						min_mult-- ;
					}

					//minor marks: 5 for multiples of 5, 4 for 2, 1 for 1
					switch (index)
					{
						default:	minor = 2;		break;			//1 * 10^n
						case 1:		minor = 4;		break;			//2 * 10^n
						case 2:		minor = 5;		break;			//5 * 10^n
					}

					//has anything changed?
					if (majorMarksStart != (repeat * min_mult))
					{
						changed = true;
						majorMarksStart = repeat * min_mult;
					}
			
					if (changed || majorMarksRepeat != repeat)
					{
						changed = true;
						majorMarksRepeat = repeat ;
					}

					if (changed || labelEvery != 1)
					{
						changed = true;
						labelEvery = 1 ;
					}

					if (changed || minorMarksDivisions != minor)
					{
						changed = true ;
						minorMarksDivisions = minor ;
					}
				}
			}
			return changed ;
		}

		//Calculates number of decimal places based on the current min and max
		//returns true if changed
		private bool AutoAdjustDps()
		{
			bool			changed			= false;

			if (autoAdjust)
			{
				double			range		= maximum - minimum ;

				if (range > 0.0)
				{
					int			log_range;
					short		places;

					log_range	= (int)Math.Log10(range);
					places		= (short)(2 - log_range);

					if (range >= 1)		places-- ;

					if (places > 16)
					{
						places = 16 ;
					}
					else if (places < 0)
					{
						places = 0 ;
					}

					if (decimalPlaces != places)
					{
						changed = true;
						DecimalPlaces = places ;
					}
				}
			}
			return changed;
		}

		// Calculate an initial value for the nice number
		// Index is into array
		// Returns nice number
		private double FirstNiceNum (double Interval, out int Index, out double PowerOfTen)
		{
			int      exponent;

			// Calculate an initial power of 10
			exponent = (int)Math.Floor(Math.Log10(Interval)) ;

			// Perform some extra checking
			PowerOfTen = Math.Pow(10.0, exponent) ;
			if (PowerOfTen * 10.0 <= Interval)
			{
				PowerOfTen *= 10.0;
			}

			// Initial index is always 0
			Index = 0;

			return PowerOfTen ;
		}

		// Calculate the next nice number
		// Index is into Set
		// Returns NiceNumber
		private double NextNiceNum(ref int Index, ref double PowerOfTen)
		{
			//The set of potential multipliers for nice numbers
			double[] Set = new double[]{1.0, 2.0, 5.0} ;

			// Increment the index
			(Index)++;

			// If over max index, reset to 0 and increase the power of 10
			if (Index > 2)
			{
				Index = 0;
				PowerOfTen *= 10.0;
			}

			return (Set[Index] * PowerOfTen);
		}

		/// <summary>
		/// Converts a proportion to an actual value based on the scale's current range
		/// </summary>
		/// <param name="fraction">Proportion to convert (0-1)</param>
		/// <returns>Value to which the given proportion relates</returns>
		public double FractionToValue(double fraction)
		{
			return minimum + fraction * (maximum - minimum) ;
		}
		
		/// <summary>
		/// Converts a value in the scale's current range to a proportion of the way through that range
		/// </summary>
		/// <param name="Value">Value to convert</param>
		/// <returns>Fraction of the way along the range</returns>
		public double ValueToFraction(double Value)
		{
			if (maximum != minimum)
			{
				return (Value - minimum) / (maximum - minimum) ;
			}
			return 0 ;
		}

		/// <summary>
		/// Converts a value in the scale's current range to a proportion of the way through that range
		/// </summary>
		/// <param name="Value">Value to convert</param>
		/// <returns>Fraction of the way along the range</returns>
		public float ValueToFraction(float Value)
		{
			if (maximum != minimum)
			{
				return (Value - (float)minimum) / ((float)maximum - (float)minimum) ;
			}
			return 0 ;
		}

		/// <summary>
		/// Converts a value within the scale's current range to a position.
		/// Returns -1 if out of range
		/// </summary>
		/// <param name="Value">Value to convert</param>
		/// <param name="Orientation">Orientation of the scale</param>
		/// <param name="Ratio">Used for Vertical scales only (0-1)</param>
		/// <returns>Position in degrees (0-360) for circular scales, or fraction (0-1) for straight scales</returns>
		public double ValueToPosition(double Value, ScaleOrientations Orientation, double Ratio)
		{
			double		fraction		= -1 ;
			double		position		= -1 ;

			if (maximum != minimum)
			{
				fraction = (Value - minimum) / (maximum - minimum) ;
			}

			if (fraction >= 0 && fraction <= 1)
			{
				switch(Orientation)
				{
					case ScaleOrientations.Circular:
					{
						if (anticlockwise)
						{
							position = -zeroAngle + (sweepAngle * fraction) ;
						}
						else
						{
							position = -zeroAngle - (sweepAngle * fraction) ;
						}

						while (position < 0)		position += 360 ;
					}
						break ;
					
					case ScaleOrientations.Vertical:
					{
						if (anticlockwise)
						{
							position = Ratio + (1 - 2*Ratio) * fraction ;  
						}
						else
						{
							position = 1 - Ratio - (1 - 2*Ratio) * fraction ;
						}
					}
						break ;

					case ScaleOrientations.Horizontal:
					{
						if (!anticlockwise)
						{
							position = 0.05 + 0.9 * fraction ;  
						}
						else
						{
							position = 0.95 - 0.9 * fraction ;
						}
					}
						break ;
				}
			}

			return position ;
		}

		#endregion

		#region Child Property changed handling
		private void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			//pass on that a property has changed
			OnPropertyChanged("Threshold"); 
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (thresholds != null)		thresholds.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (thresholds != null)		thresholds.Disconnect() ;
		}
		#endregion
	}
}

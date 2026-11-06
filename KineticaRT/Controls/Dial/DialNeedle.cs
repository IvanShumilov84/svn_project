using System;
using System.Collections;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization ;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using KineticaRT ;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
//	/// <summary>
//	/// Enumeration of Fill styles used by objects such as Sliders and Dials.
//	/// </summary>
//	public enum FillStyles
//	{
//		/// <summary>
//		/// Solid Fill: all in a single color
//		/// </summary>
//		Solid,
//		/// <summary>
//		/// Segmented Fill: up to the first threshold, the fill is in the defined FillColor,
//		/// then up to the next threshold it is the Threshold Color, etc
//		/// </summary>
//		Segmented,
//		/// <summary>
//		/// Permanent Fill: as Solid Fill, but the entire range of the control has fill
//		/// drawn on it, not just as far as the current value
//		/// </summary>
//		Permanent,
//	}
//
	/// <summary>
	/// Enumeration of possible Needle Styles for a Dial
	/// </summary>
	public enum DialNeedleStyles
	{
		/// <summary>
		/// Thin Line (one pixel), with central round hub
		/// </summary>
		ThinLine,
		/// <summary>
		/// Thick Line (3 pixels), with central round hub
		/// </summary>
		ThickLine,
		/// <summary>
		/// Kite-shaped shaded needle
		/// </summary>
		Shaded,
	}

	/// <summary>
	/// Collection of DialNeedle objects
	/// </summary>
	[TypeConverter(typeof(DialNeedleCollection.NeedleCollConverter))]
	public class DialNeedleCollection : SubObjectCollection
	{
		#region Type Converter
		internal class NeedleCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					DialNeedleCollection	coll		= (DialNeedleCollection) value;
					DialNeedle []			needles		= new DialNeedle [coll.Count] ;
					ConstructorInfo			ctor_info ;
					
					ctor_info = typeof(DialNeedleCollection).GetConstructor(new Type[]{typeof(DialNeedle[])});

					//copy needles into array
					for (int index = 0; index < coll.Count; index++)
					{
						needles[index] = coll[index] ;
					}

					return new InstanceDescriptor(ctor_info, new object[]{needles}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the DialNeedleCollection class with a single needle
		/// </summary>
		public DialNeedleCollection()
		{
			//Always add one needle
			AddNeedle() ;
		}

		/// <summary>
		/// Initialises a new instance of the DialNeedleCollection class containing the specified needle objects
		/// </summary>
		/// <param name="needles">Array of DialNeedle objects to add to collection</param>
		public DialNeedleCollection(DialNeedle [] needles)
		{
			AddRange(needles) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the DialNeedle object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DialNeedle this[int Index]
		{
			get
			{
				return (DialNeedle) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					List[Index] = value ;
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new DialNeedle object to the collection
		/// </summary>
		public void AddNeedle()
		{
			this.List.Add(new DialNeedle()) ;
		}

		/// <summary>
		/// Adds and array of needles to the collection
		/// </summary>
		/// <param name="newNeedles">Array of needles to add</param>
		public void AddRange(DialNeedle [] newNeedles)
		{
			for (int index = 0; index < newNeedles.Length; index++)
			{
				this.List.Add(newNeedles[index]) ;
			}
		}

		/// <summary>
		/// Draw all the needles in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		public void Draw(Graphics g, Rectangle DrawRect)
		{
			foreach (DialNeedle needle in this)
			{
				needle.Draw(g, DrawRect) ;
			}
		}

		/// <summary>
		/// Draw all the Needles' Fills
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		/// <param name="Scale">Scale object whose Thresholds may be used</param>
		/// <param name="BackBrush">Brush for control's background</param>
		public void DrawFill(Graphics g, Rectangle DrawRect, KineticaRT.Controls.Scale Scale, Brush BackBrush)
		{
			bool		multi		= (List.Count > 1) ;

			foreach (DialNeedle needle in this)
			{
				needle.DrawFill(g, DrawRect, Scale, multi, BackBrush) ;
			}
		}

		/// <summary>
		/// Tests if a given position or fraction range is close enoughto the position
		/// of any of the Needles in the collection to start a mouse drag
		/// </summary>
		/// <param name="MinFrac">Minimum fraction of range to check</param>
		/// <param name="MaxFrac">Maximum fraction of range to check</param>
		/// <param name="XPos">X Position of point to check (shaded needles only)</param>
		/// <param name="YPos">Y Position of point to check (shaded needles only)</param>
		/// <param name="Scale">Scale object to use for converting fractions to values if RangeSource is NeedleRangeSources.Scale</param>
		/// <param name="CaptureIndex">Index of needle to capture, or -1 if none are close enough</param>
		/// <returns>True if click is close enough to capture a Needle</returns>
		public bool CheckForCapture(double MinFrac, double MaxFrac, int XPos, int YPos, Scale Scale, out int CaptureIndex)
		{
			int			index		= 0 ;

			CaptureIndex = -1 ;

			foreach (DialNeedle needle in this)
			{
				if (needle.CheckForCapture(MinFrac, MaxFrac, XPos, YPos, Scale))
				{
					//don't stop, as there may be a later one
					// that would also like to do the capture.
					//(Later in list are on top)
					CaptureIndex = index ;
				}
				index++ ;
			}

			return (CaptureIndex != -1) ;
		}

		/// <summary>
		/// Recalculates the position of each Needle
		/// </summary>
		/// <param name="DrawRect">Rectangle in which to position the Needles</param>
		/// <param name="Scale">Scale object related to the Control</param>
		/// <param name="DrawAll">(out) True if a full redraw (needle and fill) will be required</param>
		/// <returns>True if any needles have moved and hence need redrawing</returns>
		public bool CalculatePosition(Rectangle DrawRect, KineticaRT.Controls.Scale Scale, out bool DrawAll)
		{
			bool		ret_val		= false ;

			DrawAll = false ;

			foreach (DialNeedle needle in this)
			{
				if (needle.CalculatePosition(DrawRect, Scale))
				{
					ret_val = true ;
				}

				if (needle.FillEnabled && needle.FillStyle != FillStyles.Permanent)
				{
					DrawAll = true ;
				}
			}

			return ret_val ;
		}
		#endregion
	}


	internal class DialNeedleStringConverter : ExpandableObjectConverter
	{
//		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
//		{
//			System.Windows.Forms.MessageBox.Show (context.PropertyDescriptor.PropertyType.Name) ;
//			return false;
//		}
//
//		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
//		{
//			System.Windows.Forms.MessageBox.Show (context.PropertyDescriptor.PropertyType.Name) ;
//			if (destinationType == typeof(string) && value is DialNeedle)
//				return "Dial Needle";
//			return base.ConvertTo (context, culture, value, destinationType);
//		}
//            
//		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
//		{
//			System.Windows.Forms.MessageBox.Show (context.PropertyDescriptor.PropertyType.Name) ;
//			return true;
//		}
//
//		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) 
//		{
//			System.Windows.Forms.MessageBox.Show (context.PropertyDescriptor.PropertyType.Name) ;
//
//			if (context.PropertyDescriptor.PropertyType.Name.Equals("DialNeedle"))
//			{
//				System.ComponentModel.PropertyDescriptorCollection propertyDescriptorCollection; 
//				string[] propNames = new string[5]; 
//				propertyDescriptorCollection = TypeDescriptor.GetProperties(typeof(DialNeedle),attributes); 
//				propNames[0] = @"Style"; 
//				propNames[1] = @"FillStyle";
//				propNames[2] = @"Width";
//				propNames[3] = @"Length";
//				propNames[4] = @"Centred";
//				propNames[5] = @"CentrePoint";
//				return propertyDescriptorCollection.Sort(propNames); 
//			}
//			return base.GetProperties(context, value, attributes);
//		} 
	}

	/// <summary>
	/// Needle object for use with a Dial
	/// </summary>
	//[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DialNeedle : KineticaRT.Controls.Needle
	{
		#region Variables and Declarations
		const double	END_STOP_OFFSET		= 7 ;		//degrees

        /// <summary>
        /// Current angle of the needle in degrees cw from top.
        /// </summary>
		protected double			currentAngle ;			
		private double				currentFraction ;
		/// <summary>
        /// The last angle at which the needle was drawn in degrees cw from top.
		/// </summary>
        protected double			lastAngle ;
		private bool				currentlyEndStop ;
		private bool				currentlyEndStopHi ;
		private Point[]				shadedPoints ;

		//property variables
		private bool				centred ;
		private double				centrePoint ;
		private DialNeedleStyles	style ;
        /// <summary>
        /// Current width of needle.
        /// </summary>
		protected double			width ;
        /// <summary>
        /// Current length of needle.
        /// </summary>
		protected double			length ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the DialNeedle class
		/// </summary>
		public DialNeedle()
		{
			centred = true ;
			centrePoint = 0 ;
			width = 0.1 ;
			length = 0.8 ;

			style = DialNeedleStyles.Shaded ;

			lastAngle = -1 ;		//start last angle at invalid
			currentAngle = 0 ;
			currentFraction = 0 ;
			currentlyEndStop = false ;
			currentlyEndStopHi = false ;

			shadedPoints = new Point[4] ;
		}
		
		#endregion


		#region Properties

        //ft
        /// <summary>
        /// Gets or sets whether the gradient is enabled
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets whether the gradient is enabled")]
        public bool GradientEnabled
        {
            get
            {
                return this.gradientEnabled;
            }
            set
            {
                if (this.gradientEnabled != value)
                {
                    this.gradientEnabled = value;
                    //AddToQueue(true);
                    OnPropertyChanged("GradientEnabled");
                }
            }
        }


        /// <summary>
        /// Gets or sets the linear gradient mode
        /// </summary>
        [Category("Gradient")]
        [DefaultValue(typeof(Color), "Navy")]
        [Description("Gets or sets the linear gradient mode")]
        public LinearGradientMode LinearGradientMode
        {
            get
            {
                return this.gradientMode;
            }
            set
            {
                if (this.gradientMode != value)
                {
                    this.gradientMode = value;
                    //if (GradientEnabled) AddToQueue(false);

                    OnPropertyChanged("LinearGradientMode");
                }
            }
        }

		/// <summary>
		/// Gets or sets the Style of the Needle
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the Style of the Needle")]
		[DefaultValue(DialNeedleStyles.Shaded)]
		public DialNeedleStyles Style
		{
			get
			{
				return style;
			}
			set
			{
//System.Windows.Forms.MessageBox.Show ("Style") ;
				if (style != value)
				{
					style = value;
					OnPropertyChanged("Style") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Fill Style associated with the Needle
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Fill Style associated with the needle (solid, segmented or permanent)")]
		[DefaultValue(FillStyles.Solid)]
		public override FillStyles FillStyle
		{
			get
			{
				return fillStyle ;
			}
			set
			{
//System.Windows.Forms.MessageBox.Show ("FillStyle") ;
				if (fillStyle != value)
				{
					fillStyle = value;
					OnPropertyChanged("FillStyle") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the width of the Needle (hub width for line needles), as a proportion of the radius of the Dial (0..1)
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the width of the Needle (hub width for line needles), as a proportion of the radius of the Dial (0..1)")]
		[DefaultValue(0.1)]
		public double Width
		{
			get
			{
				return width;
			}
			set
			{
				if (width != value)
				{
					width = value;
					OnPropertyChanged("Width") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Length of the Needle as a proportion of the radius of the Dial (0..1)
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the Length of the Needle as a proportion of the radius of the Dial (0..1)")]
		[DefaultValue(0.8)]
		public double Length
		{
			get
			{
				return length;
			}
			set
			{
				if (length != value)
				{
					length = value;
					OnPropertyChanged("Length") ;
				}
			}
		}
		/// <summary>
		/// Gets or sets whether Fill starts from the minimum value, or the CentrePoint value
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets whether Fill starts from the minimum value, or the CentrePoint value")]
		[DefaultValue(true)]
		public bool Centred
		{
			get
			{
				return centred ;
			}
			set
			{
				if (centred != value)
				{
					centred = value;
					OnPropertyChanged("Centred") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the value around which Centred Fill is drawn
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the Value around which Centred Fill is drawn")]
		[DefaultValue(0.0)]
		public double CentrePoint
		{
			get
			{
				return centrePoint ;
			}
			set
			{
				if (centrePoint != value)
				{
					centrePoint = value;
					OnPropertyChanged("CentrePoint") ;
				}
			}
		}

		#endregion

		#region Drawing
		/// <summary>
		/// Tests if a given position or fraction range is close enough
		/// to the needle's position to start a mouse drag
		/// </summary>
		/// <param name="MinFrac">Minimum fraction of range to check</param>
		/// <param name="MaxFrac">Maximum fraction of range to check</param>
		/// <param name="XPos">X Position of point to check (shaded needles only)</param>
		/// <param name="YPos">Y Position of point to check (shaded needles only)</param>
		/// <param name="Scale">Scale object to use for converting fractions to values if RangeSource is NeedleRangeSources.Scale</param>
		/// <returns>True if click is close enough to capture the Needle</returns>
		public bool CheckForCapture(double MinFrac, double MaxFrac, int XPos, int YPos, Scale Scale)
		{
			if (!enabled)			return false ;

			if (!useTempValue)
			{
				if (krtValue is Values.Double)
				{
					double		frac		= ValueToFraction(krtValue.ToDouble(), Scale) ;

					//check value range...
					if (frac >= MinFrac && frac <= MaxFrac)
					{
						return true ;
					}
					else if (style == DialNeedleStyles.Shaded)
					{
						//check position: inside needle diamond?
						GraphicsPath	path	= new GraphicsPath() ;

                        path.AddLines(shadedPoints) ;
						if (path.IsVisible(XPos, YPos))
						{
							path.Dispose() ;
							return true ;
						}
						path.Dispose() ;
					}
				}

				//not in relevant place
				return false ;
			}
			//else already captured!!

			return true;
		}
	
		/// <summary>
		/// Recalculates the position of the Needle
		/// </summary>
		/// <param name="DrawRect">Rectangle in which to position the Needle</param>
		/// <param name="objScale">Scale object related to the Control</param>
		/// <returns>True if Needle has changed angle since last redraw</returns>
		public virtual bool CalculatePosition(Rectangle DrawRect, KineticaRT.Controls.Scale objScale)
		{
			double	fraction ;
			double	current_value ;

			SetUpScaling(DrawRect) ;

			if (useTempValue)
			{
				current_value = tempValue ;
			}
			else if (krtValue is Values.Double)
			{
				current_value = krtValue.ToDouble() ;
			}
			else		//error value
			{
				//invalid angles are negative
				currentAngle = -1 ;
				currentFraction = -1 ;

				return lastAngle >= 0 ;
			}

            if (System.Double.IsNaN(current_value)) return true;

			fraction = ValueToFraction(current_value, objScale) ;

			if (fraction > 1)
			{
				currentlyEndStop	= true ;
				currentlyEndStopHi	= true ;
			}
			else if (fraction < 0)
			{
				currentlyEndStop	= true ;
				currentlyEndStopHi	= false ;
			}
			else
			{
				currentlyEndStop	= false ;
				currentlyEndStopHi	= false ;
			}

            if (objScale != null)
            {

                if (currentlyEndStop)
                {
                    if (currentlyEndStopHi)
                    {
                        if (!objScale.AntiClockwise)
                        {
                            currentAngle = objScale.ZeroAngle + objScale.SweepAngle + END_STOP_OFFSET;
                            currentFraction = 1.1;
                        }
                        else
                        {
                            currentAngle = objScale.ZeroAngle - objScale.SweepAngle - END_STOP_OFFSET;
                            currentFraction = -1.1;
                        }
                    }
                    else
                    {
                        if (!objScale.AntiClockwise)
                        {
                            currentAngle = objScale.ZeroAngle - END_STOP_OFFSET;
                            currentFraction = -0.1;
                        }
                        else
                        {
                            currentAngle = objScale.ZeroAngle + END_STOP_OFFSET;
                            currentFraction = 0.1;
                        }
                    }
                }
                else
                {
                    currentFraction = fraction;
                    if (!objScale.AntiClockwise)
                    {
                        currentAngle = objScale.ZeroAngle + (objScale.SweepAngle * fraction);
                    }
                    else
                    {
                        currentAngle = objScale.ZeroAngle - (objScale.SweepAngle * fraction);
                    }
                }
            }

			if (currentAngle < 0)
			{
				currentAngle += 360 ;
			}
			else if (currentAngle > 360)
			{
				currentAngle -= 360 ;
			}

            return System.Math.Abs(currentAngle - lastAngle) > 0.5 ;
		}

		/// <summary>
		/// Draw the needle
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		public virtual void Draw(Graphics g, Rectangle DrawRect)
		{
			base.SetUpScaling(DrawRect) ;

			if (visible && krtValue is Values.Double && !System.Double.IsNaN(krtValue.ToDouble()))
			{
				double		rad_angle		= currentAngle * Math.PI / 180 ;	//radians

				if (style != DialNeedleStyles.Shaded)
				{
					Point		tip				= new Point() ;
					Point		start			= new Point() ;
					Pen			pen ;

					//draw hub
					if (width > 0)
					{
						Brush			brush	= new SolidBrush(color) ;

						g.FillEllipse(brush, ScaleX(0.5 - 0.5 * width), ScaleY(0.5 - 0.5 * width),
							(int)(xScale * width), (int)(yScale * width)) ;
						brush.Dispose() ;
					}

					tip.X	= ScaleX (0.5 + Math.Sin(rad_angle) * 0.5 * length) ;
					tip.Y	= ScaleY (0.5 - Math.Cos(rad_angle) * 0.5 * length) ;

					start.X	= ScaleX (0.5 + Math.Sin(rad_angle) * 0.5 * width) ;
					start.Y	= ScaleY (0.5 - Math.Cos(rad_angle) * 0.5 * width) ;

					pen = new Pen(color, (style == DialNeedleStyles.ThinLine) ? 1 : 3) ;
					g.DrawLine(pen, start, tip) ;
					pen.Dispose() ;
				}
				else
				{
					shadedPoints[0].X =  ScaleX(0.5 + Math.Sin(rad_angle + Math.PI) * width * 0.5) ;
					shadedPoints[0].Y =  ScaleY(0.5 - Math.Cos(rad_angle + Math.PI) * width * 0.5) ;

					shadedPoints[1].X =  ScaleX(0.5 + Math.Sin(rad_angle - (Math.PI / 2)) * width * 0.5) ;
					shadedPoints[1].Y =  ScaleY(0.5 - Math.Cos(rad_angle - (Math.PI / 2)) * width * 0.5) ;
					
					shadedPoints[2].X =  ScaleX(0.5 + Math.Sin(rad_angle) * 0.5 * length) ; 
					shadedPoints[2].Y =  ScaleY(0.5 - Math.Cos(rad_angle) * 0.5 * length) ; 
					
					shadedPoints[3].X =  ScaleX(0.5 + Math.Sin(rad_angle + (Math.PI / 2)) * width * 0.5) ;
					shadedPoints[3].Y =  ScaleY(0.5 - Math.Cos(rad_angle + (Math.PI / 2)) * width * 0.5) ;

					if (shadedPoints[0].Equals(shadedPoints[2]))
					{
						//don't bother to draw: end and tip are the same point
					}
					else
					{
						Color				top_clr ;
						Color				bottom_clr ;
						PathGradientBrush	brush ;
						GraphicsPath		path		= new GraphicsPath() ;
						Color[]				colors ;

						CalculateSoftShading(out top_clr, out bottom_clr) ;

						//NB - this is not shaded according to angle atm
						path.AddLines(shadedPoints) ;
						brush = new PathGradientBrush(path) ;
						colors = new Color[4] ;
						colors[0] = top_clr ;
						colors[1] = color ;
						colors[2] = bottom_clr ;
						colors[3] = color ;
						brush.SurroundColors = colors ;

						g.FillPolygon(brush, shadedPoints) ;

						path.Dispose() ;
						brush.Dispose() ;
					}
				}
			}
			//else not a valid value		//draw unconnected symbol?

			//done the drawing now, so update 'last_angle'
			lastAngle = currentAngle ;
		}

		/// <summary>
		/// Draw the Needle's Fill
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="drawRect">Rectangle in which to draw</param>
		/// <param name="objScale">Scale object whose Thresholds may be used</param>
		/// <param name="MultipleNeedles">True if more than one needle exists on the control</param>
		/// <param name="BackBrush">Brush for control's background</param>
		public void DrawFill(Graphics g, Rectangle drawRect, KineticaRT.Controls.Scale objScale, bool MultipleNeedles, Brush BackBrush)
		{
			if (!fillEnabled || !(krtValue is Values.Double) || (objScale == null)) return ;
			//if (this.currentlyEndStop) return ;

			SetUpScaling(drawRect) ;
			SolidBrush brush = new SolidBrush(fillColor) ;

			if (!centred) centrePoint = minimum ;

			float					fill_start			= (float)fillStart ;
			float					fill_finish			= (float)fillFinish ;
			bool					anticlockwise		= objScale.AntiClockwise ;

			if (fillStart > fillFinish)
			{
				fill_start = (float)fillFinish ;
				fill_finish = (float)fillStart ;
			}

			Region			old_rgn		= null ;
			Rectangle		centre_rect = new Rectangle(ScaleX (0.5 - 0.5 * fill_start),ScaleY (0.5 - 0.5 * fill_start),(int)(xScale * fill_start)+ 1,(int)(yScale * fill_start)+ 1) ;

			if (MultipleNeedles)
			{
				//clip out centre in case other needles fills are there
				GraphicsPath		path		= new GraphicsPath() ;
				Region				rgn ;
				path.AddEllipse(centre_rect) ;
				rgn = new Region(path) ;
				old_rgn = g.Clip ;
				g.ExcludeClip(rgn) ;
				path.Dispose() ;
			}

            if (fillStyle == FillStyles.Segmented)
            {
                ThresholdCollection th_coll = objScale.Thresholds;
                int th_count = th_coll.Count;
                if (th_count == 0) return;
                float min_fill_fraction;
                float max_fill_fraction;

                Rectangle rect = new Rectangle();
                float sweep_angle;
                float zero_angle;
                rect.X = ScaleX(0.5 - 0.5 * fill_finish);
                rect.Y = ScaleY(0.5 - 0.5 * fill_finish);
                rect.Width = (int)(xScale * fill_finish) + 1;
                rect.Height = (int)(yScale * fill_finish) + 1;

                if (objScale.SweepAngle > 359.5)
                {
                    sweep_angle = 359.5F;
                }
                else
                {
                    sweep_angle = (float)objScale.SweepAngle;
                }
                zero_angle = (float)objScale.ZeroAngle;

                min_fill_fraction = objScale.ValueToFraction((float)centrePoint);
                if (currentFraction >= min_fill_fraction)
                {
                    int th_index;
                    float new_fraction = min_fill_fraction;
                    max_fill_fraction = (float)currentFraction;

                    for (th_index = 0; th_index < th_count; th_index++)
                    {
                        if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                        {
                            new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value);
                            if (new_fraction > min_fill_fraction) break;
                        }
                    }

                    if (th_index >= th_count) new_fraction = 1.03F;

                    while (true)
                    {
                        if (new_fraction > 1.03F) new_fraction = 1.03F;

                        float end_fraction = (new_fraction >= max_fill_fraction) ? max_fill_fraction : new_fraction;
                        float pie_sweep = sweep_angle * (end_fraction - min_fill_fraction);
                        float pie_start = zero_angle - 90 + (anticlockwise ? -sweep_angle * end_fraction : sweep_angle * min_fill_fraction);

                        g.FillPie(brush, rect, pie_start, pie_sweep);

                        if (new_fraction >= max_fill_fraction) break;
                        if (th_index >= th_count) break;
                        brush.Color = th_coll[th_index].MarkColor;
                        min_fill_fraction = new_fraction;
                        th_index++;
                        while ((th_index < th_count) && !(th_coll[th_index].Enabled && th_coll[th_index].UseForFill))
                        {
                            th_index++;
                        }
                        new_fraction = (th_index == th_count) ? max_fill_fraction : objScale.ValueToFraction((float)th_coll[th_index].Value);
                    }
                }
                else
                {
                    int th_index;
                    float new_fraction = min_fill_fraction;
                    max_fill_fraction = (float)currentFraction;

                    for (th_index = th_count - 1; th_index >= 0; th_index--)
                    {
                        if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                        {
                            new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value);
                            if (new_fraction < min_fill_fraction) break;
                        }
                    }

                    if (th_index < 0) new_fraction = -0.03F;

                    while (true)
                    {
                        if (new_fraction < -0.03F) new_fraction = -0.03F;

                        float end_fraction = (new_fraction <= max_fill_fraction) ? max_fill_fraction : new_fraction;
                        float pie_sweep = sweep_angle * (end_fraction - min_fill_fraction);
                        float pie_start = zero_angle - 90 + (anticlockwise ? -sweep_angle * end_fraction : sweep_angle * min_fill_fraction);

                        g.FillPie(brush, rect, (float)pie_start, (float)pie_sweep);

                        if (new_fraction <= max_fill_fraction) break;

                        if (th_index < 0) break;
                        brush.Color = th_coll[th_index].MarkColor;
                        min_fill_fraction = new_fraction;
                        th_index--;
                        while ((th_index >= 0) && !(th_coll[th_index].Enabled && th_coll[th_index].UseForFill))
                        {
                            th_index--;
                        }
                        new_fraction = (th_index < 0) ? max_fill_fraction : objScale.ValueToFraction((float)th_coll[th_index].Value);
                    }
                }
            }
            else if (fillStyle == FillStyles.SegmentedFromMinimum)
            {
                ThresholdCollection th_coll = objScale.Thresholds;
                int th_count = th_coll.Count;
                if (th_count == 0) return;
                Rectangle rect = new Rectangle();
                float max_fill_fraction = (float)currentFraction > 1.0F ? 1.0F : (float)currentFraction ;
                float sweep_angle;
                float zero_angle;
                rect.X = ScaleX(0.5 - 0.5 * fill_finish);
                rect.Y = ScaleY(0.5 - 0.5 * fill_finish);
                rect.Width = (int)(xScale * fill_finish);
                rect.Height = (int)(yScale * fill_finish);

                if (objScale.SweepAngle > 359.5)
                {
                    sweep_angle = 359.5F;
                }
                else
                {
                    sweep_angle = (float)objScale.SweepAngle;
                }
                zero_angle = (float)objScale.ZeroAngle;

                float center_fraction = objScale.ValueToFraction((float)centrePoint);
                int th_index;
                //th_index = 0 ;
                for (th_index = 0; th_index < th_count; th_index++)
                {
                    if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                    {
                        break;
                    }
                }
                if (th_count == th_index) return;

                float new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value);
                //brush.Color = th_coll[th_index].MarkColor ;
                float end_fraction = 0;
                bool passed_centre_point = false;


                ////////////				
                if (new_fraction < center_fraction)
                {
                    brush.Color = th_coll[th_index].MarkColor;

                    while (th_index < th_count)
                    {
                        if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                        {
                            break;
                        }
                        th_index++;
                    }
                    if (th_count == th_index) return;

                }
                else
                {
                    while (th_index < th_count)
                    {
                        if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                        {
                            break;
                        }
                        th_index++;
                    }
                    if (th_count == th_index) return;

                    if (!passed_centre_point)
                    {
                        if (new_fraction < 0.0F) new_fraction = 0.0F;
                        if (new_fraction > 1.0F) new_fraction = 1.0F;
                        float pie_sweep = sweep_angle * (end_fraction - new_fraction);
                        float pie_start = zero_angle - 90 + (anticlockwise ? -sweep_angle * end_fraction : sweep_angle * new_fraction);
                        brush.Color = fillColor;
                        g.FillPie(brush, rect, pie_start, pie_sweep);
                        passed_centre_point = true;
                    }
                    if (th_index < th_count - 1)
                    {
                        end_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value); ;
                    }
                    else
                    {
                        end_fraction = 1;
                    }
                    brush.Color = th_coll[th_index].MarkColor;
                }
                ////////////				

                if (new_fraction > max_fill_fraction) new_fraction = max_fill_fraction;
                //if (end_fraction > max_fill_fraction) end_fraction = max_fill_fraction;

                System.Drawing.Color last_colour = brush.Color;

                while (true)
                {
                    if (new_fraction < 0.0F) new_fraction = 0.0F;
                    if (new_fraction > 1.0F) new_fraction = 1.0F;
                    float pie_sweep = sweep_angle * (end_fraction - new_fraction);
                    float pie_start = zero_angle - 90 + (anticlockwise ? -sweep_angle * end_fraction : sweep_angle * new_fraction);

                    g.FillPie(brush, rect, pie_start, pie_sweep);

                    if (new_fraction >= max_fill_fraction) break;

                    end_fraction = new_fraction;
                    if (end_fraction >= 1.0) break;
                    //th_index++ ;
                    //if ((new_fraction>=1.0) ||(th_index >= th_count)) break ;

                    th_index++;
                    while ((new_fraction < 1.0) && (th_index < th_count) && !(th_coll[th_index].Enabled && th_coll[th_index].UseForFill))
                    {
                        th_index++;
                    }
                    //if ((new_fraction >= 1.0) || (th_index >= th_count)) break;


                    if (th_index == th_count)
                    {
                        new_fraction = 1.0F;
                    }
                    else
                    {
                        new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value);
                    }
                    if (new_fraction > max_fill_fraction) new_fraction = max_fill_fraction;

                    if (new_fraction < center_fraction)
                    {
                        last_colour = th_coll[th_index].MarkColor;
                        brush.Color = last_colour;
                    }
                    else
                    {
                        if (!passed_centre_point)
                        {
                            last_colour = fillColor;
                            brush.Color = last_colour;
                            pie_sweep = sweep_angle * (end_fraction - new_fraction);
                            pie_start = zero_angle - 90 + (anticlockwise ? -sweep_angle * end_fraction : sweep_angle * new_fraction);
                            g.FillPie(brush, rect, pie_start, pie_sweep);
                            passed_centre_point = true;
                        }
                        if (th_index < th_count - 1)
                        {
                            new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value); ;
                        }
                        else
                        {
                            new_fraction = 1;
                        }
                        if (new_fraction > max_fill_fraction) new_fraction = max_fill_fraction;
                        brush.Color = last_colour;
                        if (th_index < th_count) last_colour = th_coll[th_index].MarkColor;
                    }
                }
            }
            else if (fillStyle == FillStyles.Permanent)
			{
				ThresholdCollection		th_coll				= objScale.Thresholds ;
				int						th_count			= th_coll.Count ;
				if (th_count == 0) return ;
				Rectangle				rect				= new Rectangle() ;

				float		sweep_angle ;
				float		zero_angle ;
				rect.X		= ScaleX(0.5 - 0.5 * fill_finish) ;
				rect.Y		= ScaleY(0.5 - 0.5 * fill_finish) ;
				rect.Width	= (int)(xScale * fill_finish) ;
				rect.Height	= (int)(yScale * fill_finish) ;

				if (objScale.SweepAngle > 359.5)
				{
					sweep_angle = 359.5F ;
				}
				else
				{
					sweep_angle = (float)objScale.SweepAngle ;
				}
				zero_angle = (float)objScale.ZeroAngle ;

				float center_fraction = objScale.ValueToFraction((float)centrePoint) ;
				int th_index ;
                //th_index = 0 ;
                for (th_index = 0; th_index < th_count; th_index++)
                {
                    if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                    {
                        break;
                    }
                }
                if (th_count == th_index) return;

                float new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value);
                //brush.Color = th_coll[th_index].MarkColor ;
				float end_fraction = 0 ;
				bool passed_centre_point = false ;


				////////////				
				if (new_fraction < center_fraction)
				{
					brush.Color = th_coll[th_index].MarkColor ;
                    
                    while (th_index < th_count)
                    {
                        if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                        {
                            break;
                        }
                        th_index++;
                    }
                    if (th_count == th_index) return;

				}
				else
				{
                    while (th_index < th_count)
                    {
                        if (th_coll[th_index].Enabled && th_coll[th_index].UseForFill)
                        {
                            break;
                        }
                        th_index++;
                    }
                    if (th_count == th_index) return;
                    
                    if (!passed_centre_point)
					{
                        if (new_fraction < 0.0F) new_fraction = 0.0F;
                        if (new_fraction > 1.0F) new_fraction = 1.0F;
                        float pie_sweep = sweep_angle * (end_fraction - new_fraction);
						float pie_start = zero_angle - 90 + (anticlockwise ? - sweep_angle * end_fraction  : sweep_angle * new_fraction) ;
						brush.Color = fillColor ;
                        if (sweep_angle > 0.0F)
                        {
                            g.FillPie(brush, rect, pie_start, pie_sweep);
                        }
						passed_centre_point = true ;
					}
					if (th_index < th_count-1)
					{
						end_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value) ; ;
					}
					else
					{
						end_fraction = 1 ;
					}
					brush.Color = th_coll[th_index].MarkColor ;
				}
				////////////				

                System.Drawing.Color last_colour = brush.Color;

				while (true)
				{
                    if (new_fraction < 0.0F) new_fraction = 0.0F;
                    if (new_fraction > 1.0F) new_fraction = 1.0F;
					float pie_sweep = sweep_angle * (end_fraction-new_fraction) ;
					float pie_start = zero_angle - 90 + (anticlockwise ? - sweep_angle * end_fraction  : sweep_angle * new_fraction) ;

                    if (sweep_angle > 0.0F)
                    {
                        g.FillPie(brush, rect, pie_start, pie_sweep);
                    }
                    end_fraction = new_fraction;
                    if (end_fraction >= 1.0) break;
                    //th_index++ ;
					//if ((new_fraction>=1.0) ||(th_index >= th_count)) break ;

                    th_index++;
                    while ((new_fraction < 1.0) && (th_index < th_count) && !(th_coll[th_index].Enabled && th_coll[th_index].UseForFill))
                    {
                        th_index++;
                    }
                    //if ((new_fraction >= 1.0) || (th_index >= th_count)) break;


                    if (th_index == th_count)
                    {
                        new_fraction = 1.0F;
                    }
                    else
                    {
                        new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value);
                    }
                    if (new_fraction > 1.0) new_fraction = 1.0F;

					if (new_fraction < center_fraction)
					{
                        last_colour = th_coll[th_index].MarkColor;
                        brush.Color = last_colour;
                    }
					else
					{
                        if (!passed_centre_point)
						{
                            last_colour = fillColor;
                            brush.Color = last_colour;
                            pie_sweep = sweep_angle * (end_fraction - new_fraction);
							pie_start = zero_angle - 90 + (anticlockwise ? - sweep_angle * end_fraction  : sweep_angle * new_fraction) ;
							g.FillPie(brush, rect, pie_start, pie_sweep) ;
							passed_centre_point = true ;
						}
						if (th_index < th_count-1)
						{
                            new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value); ;
						}
						else
						{
                            new_fraction = 1;
						}
                        brush.Color = last_colour;
                        if (th_index < th_count)last_colour = th_coll[th_index].MarkColor;
					}
				}
			}
			else  // Solid 
			{
				ThresholdCollection		th_coll				= objScale.Thresholds ;
				int						th_count			= th_coll.Count ;
				if (th_count == 0) return ;

				Rectangle				rect				= new Rectangle() ;
				float					sweep_angle ;
				float					zero_angle ;

				rect.X		= ScaleX(0.5 - 0.5 * fillFinish) ;
				rect.Y		= ScaleY(0.5 - 0.5 * fillFinish) ;
				rect.Width	= (int)(xScale * fillFinish) ;
				rect.Height	= (int)(yScale * fillFinish) ;

				if (objScale.SweepAngle > 359.5)
				{
					sweep_angle = 359.5F ;
				}
				else
				{
					sweep_angle = (float)objScale.SweepAngle ;
				}
				zero_angle = (float)objScale.ZeroAngle ;

                if (zero_angle < 0.0F) zero_angle = 0.0F;

				float min_fill_fraction = objScale.ValueToFraction((float)centrePoint) ;
                if (min_fill_fraction < 0.0F) min_fill_fraction = 0.0F;
                float pie_sweep = Math.Min(sweep_angle * ((float)currentFraction - min_fill_fraction), sweep_angle);
				float pie_start = zero_angle - 90 + (anticlockwise ? - sweep_angle * (float)currentFraction  : sweep_angle * min_fill_fraction) ;
				brush.Color = fillColor ;
                if (sweep_angle > 0.0F)
                {
                    if(gradientEnabled)//ft Gradient Support
                    {
                        Brush gradientBrush = new LinearGradientBrush(rect, fillColor, ControlPaint.Dark(fillColor), gradientMode);
                        g.FillPie(gradientBrush, rect, pie_start, pie_sweep);
                        gradientBrush.Dispose();
                    }
                    else
                    {
                        g.FillPie(brush, rect, pie_start, pie_sweep);
                    }
                }
			}

			if (!MultipleNeedles)
			{
                //take out centre
				g.FillEllipse(BackBrush, centre_rect) ;
			}
			else if (old_rgn != null)
			{
				//restore original clipping region
				g.Clip = old_rgn ;
			}

			brush.Dispose() ;
		}	

		#endregion
	}
}

/*
  				if (currentlyEndStop) return ;
				ThresholdCollection		th_coll				= objScale.Thresholds ;
				int						th_count			= th_coll.Count ;
				if (th_count == 0) return ;

				Region			old_rgn				= null ;
				Rectangle		centre_rect ;

				centre_rect = new Rectangle(ScaleX (0.5 - 0.5 * fillStart),ScaleY (0.5 - 0.5 * fillStart),(int)(xScale * fillStart),(int)(yScale * fillStart)) ;

				if (MultipleNeedles)
				{
					//clip out centre in case other needles fills are there
					GraphicsPath		path		= new GraphicsPath() ;
					Region				rgn ;
					
					path.AddEllipse(centre_rect) ;
					rgn = new Region(path) ;

					old_rgn = g.Clip ;
					g.ExcludeClip(rgn) ;

					path.Dispose() ;
				}

				Rectangle				rect				= new Rectangle() ;
				double					new_angle ; 
				double					sweep_angle ;
				double					zero_angle ;
				double					fill_angle ;		//degrees
				int						index ;
				Threshold				thold				= null ;
				double					fraction ;
				double					pie_sweep ;
				double					pie_start ;

				if (fillStart > fillFinish)
				{
					double		temp	= fillStart ;
					fillStart	= fillFinish ;
					fillFinish = temp ;
				}

				rect.X		= ScaleX(0.5 - 0.5 * fillFinish) ;
				rect.Y		= ScaleY(0.5 - 0.5 * fillFinish) ;
				rect.Width	= (int)(xScale * fillFinish) ;
				rect.Height	= (int)(yScale * fillFinish) ;

				if (objScale.SweepAngle > 359.5)
				{
					sweep_angle = 359.5 ;
				}
				else
				{
					sweep_angle = objScale.SweepAngle ;
				}
				zero_angle = objScale.ZeroAngle ;

				if (!centred) centrePoint = minimum ;
				double center_fraction  = objScale.ValueToFraction (centrePoint) ;
				double current_fraction = objScale.ValueToFraction (krtValue.ToDouble()) ;
				double center_angle = zero_angle + sweep_angle * center_fraction ;
				bool above_center = center_fraction < current_fraction ;

				if (!objScale.AntiClockwise)
				{                      
					fill_angle = currentAngle ;

					for (index = th_count; index >= 0; index--)
					{
						if (index > 0)
						{
							thold = th_coll[index - 1] ;

							if (fillStyle == FillStyles.Solid || !thold.Enabled)
							{
								continue ;
							}
							fraction = objScale.ValueToFraction (thold.Value) ;
							if (fraction > 1 || fraction < 0)
							{
								//out of range
								continue ;
							}
							new_angle = zero_angle + sweep_angle * fraction ;
						}
						else
						{
							new_angle = zero_angle ;
						}

						if (new_angle < 0)		new_angle += 360 ;
						if (new_angle >= 360)	new_angle -= 360 ;

						//check if should actually draw (ie not all above current value)
						double	temp_new	= new_angle - zero_angle ;
						double	temp_fill	= fill_angle - zero_angle ;

						if (temp_new < 0)		temp_new += 360 ;
						if (temp_fill < 0)		temp_fill += 360 ;

						//ignore?
						if (temp_new > temp_fill)			continue ;

						//...and draw it
						pie_sweep = 360 - new_angle + fill_angle ;
						if (pie_sweep < 0)			pie_sweep += 360 ;
						if (pie_sweep >= 360)		pie_sweep -= 360 ;

						//too small to draw? - ignore?
						if (pie_sweep < 1)			continue ;

						pie_start = new_angle ;
						ScaleAngles(false, ref pie_start, ref pie_sweep) ;

						if ((!above_center && (new_angle < center_angle)) || (above_center && (new_angle > center_angle)))
						{
							brush.Color = index > 0 ? thold.MarkColor : fillColor ;
							g.FillPie(brush, rect, (float)pie_start, (float)pie_sweep) ;
						}
						else
						{
							brush.Color = index > 0 ? thold.MarkColor : fillColor ;
						}
						fill_angle = new_angle ;
					}
				}
				else		//Acw scale
				{                      
					if (fillStyle != FillStyles.Permanent)
					{
						fill_angle = currentAngle ;
					}
					else
					{
						fill_angle = zero_angle - sweep_angle ;
						if (fill_angle < 0)			fill_angle += 360 ;
						if (fill_angle >= 360)		fill_angle -= 360 ;
					}

					for (index = th_count; index >= 0; index--)
					{
						if (index > 0)
						{
							thold = th_coll[index - 1] ;
	
							if (fillStyle == FillStyles.Solid || !thold.Enabled)
							{
								continue ;
							}
							fraction = objScale.ValueToFraction(thold.Value) ;
							if (fraction > 1 || fraction < 0)
							{
								//out of range
								continue ;
							}
							//new_angle = 360 - zero_angle + sweep_angle * fraction ;
							new_angle = zero_angle - sweep_angle * fraction ;
						}
						else
						{
							new_angle = zero_angle ;
						}

						if (new_angle < 0)		new_angle += 360 ;
						if (new_angle >= 360)	new_angle -= 360 ;

						//check if should actually draw (ie not all above current value)
						double	temp_new	= zero_angle - new_angle ;
						double	temp_fill	= zero_angle - fill_angle ;

						if (temp_new < 0)		temp_new += 360 ;
						if (temp_fill < 0)		temp_fill += 360 ;

						if (temp_new > temp_fill)
						{
							continue ;
						}

						//...and draw it
						pie_sweep = 360 - fill_angle + new_angle ;
						if (pie_sweep < 0)			pie_sweep += 360 ;
						if (pie_sweep >= 360)		pie_sweep -= 360 ;
						
						if (pie_sweep < 1)
						{
							continue ;
						}

						pie_start = fill_angle ;
						ScaleAngles(false, ref pie_start, ref pie_sweep) ;
						brush.Color = index > 0 ? thold.MarkColor : fillColor ;
						g.FillPie(brush, rect, (float)pie_start, (float)pie_sweep) ;

						fill_angle = new_angle ;
					}
				}

				if (!MultipleNeedles)
				{
					//take out centre
					g.FillEllipse(BackBrush, centre_rect) ;
				}
				else if (old_rgn != null)
				{
					//restore original clipping region
					g.Clip = old_rgn ;
				}
*/




/*
  
 				if (currentlyEndStop) return ;
				ThresholdCollection		th_coll				= objScale.Thresholds ;
				int						th_count			= th_coll.Count ;
				if (th_count == 0) return ;

				Rectangle				rect				= new Rectangle() ;
				double					new_angle ; 
				double					sweep_angle ;
				double					zero_angle ;
				double					fill_angle ;		//degrees
				int						index ;
				Threshold				thold				= null ;
				double					fraction ;
				double					pie_sweep ;
				double					pie_start ;

				rect.X		= ScaleX(0.5 - 0.5 * fillFinish) ;
				rect.Y		= ScaleY(0.5 - 0.5 * fillFinish) ;
				rect.Width	= (int)(xScale * fillFinish) ;
				rect.Height	= (int)(yScale * fillFinish) ;

				if (objScale.SweepAngle > 359.5)
				{
					sweep_angle = 359.5 ;
				}
				else
				{
					sweep_angle = objScale.SweepAngle ;
				}
				zero_angle = objScale.ZeroAngle ;

				if (!objScale.AntiClockwise)
				{                      
					fill_angle = currentAngle ;

					for (index = th_count; index >= 0; index--)
					{
						if (index > 0)
						{
							thold = th_coll[index - 1] ;

							if (fillStyle == FillStyles.Solid || !thold.Enabled)
							{
								continue ;
							}
							fraction = objScale.ValueToFraction (thold.Value) ;
							if (fraction > 1 || fraction < 0)
							{
								//out of range
								continue ;
							}
							new_angle = zero_angle + sweep_angle * fraction ;
						}
						else
						{
							new_angle = zero_angle ;
						}

						if (new_angle < 0)		new_angle += 360 ;
						if (new_angle >= 360)	new_angle -= 360 ;

						//check if should actually draw (ie not all above current value)
						double	temp_new	= new_angle - zero_angle ;
						double	temp_fill	= fill_angle - zero_angle ;

						if (temp_new < 0)		temp_new += 360 ;
						if (temp_fill < 0)		temp_fill += 360 ;

						//ignore?
						if (temp_new > temp_fill)			continue ;

						//...and draw it
						pie_sweep = 360 - new_angle + fill_angle ;
						if (pie_sweep < 0)			pie_sweep += 360 ;
						if (pie_sweep >= 360)		pie_sweep -= 360 ;

						//too small to draw? - ignore?
						if (pie_sweep < 1)			continue ;

						pie_start = new_angle ;
						ScaleAngles(false, ref pie_start, ref pie_sweep) ;

						brush.Color = index > 0 ? thold.MarkColor : fillColor ;
						g.FillPie(brush, rect, (float)pie_start, (float)pie_sweep) ;

						fill_angle = new_angle ;
					}
				}
				else		//Acw scale
				{                      
					if (fillStyle != FillStyles.Permanent)
					{
						fill_angle = currentAngle ;
					}
					else
					{
						fill_angle = zero_angle - sweep_angle ;
						if (fill_angle < 0)			fill_angle += 360 ;
						if (fill_angle >= 360)		fill_angle -= 360 ;
					}

					for (index = th_count; index >= 0; index--)
					{
						if (index > 0)
						{
							thold = th_coll[index - 1] ;
	
							if (fillStyle == FillStyles.Solid || !thold.Enabled)
							{
								continue ;
							}
							fraction = objScale.ValueToFraction(thold.Value) ;
							if (fraction > 1 || fraction < 0)
							{
								//out of range
								continue ;
							}
							//new_angle = 360 - zero_angle + sweep_angle * fraction ;
							new_angle = zero_angle - sweep_angle * fraction ;
						}
						else
						{
							new_angle = zero_angle ;
						}

						if (new_angle < 0)		new_angle += 360 ;
						if (new_angle >= 360)	new_angle -= 360 ;

						//check if should actually draw (ie not all above current value)
						double	temp_new	= zero_angle - new_angle ;
						double	temp_fill	= zero_angle - fill_angle ;

						if (temp_new < 0)		temp_new += 360 ;
						if (temp_fill < 0)		temp_fill += 360 ;

						if (temp_new > temp_fill)
						{
							continue ;
						}

						//...and draw it
						pie_sweep = 360 - fill_angle + new_angle ;
						if (pie_sweep < 0)			pie_sweep += 360 ;
						if (pie_sweep >= 360)		pie_sweep -= 360 ;
						
						if (pie_sweep < 1)
						{
							continue ;
						}

						pie_start = fill_angle ;
						ScaleAngles(false, ref pie_start, ref pie_sweep) ;
						brush.Color = index > 0 ? thold.MarkColor : fillColor ;
						g.FillPie(brush, rect, (float)pie_start, (float)pie_sweep) ;

						fill_angle = new_angle ;
					}
				}
 
  */

/*
 * 
				if (!objScale.AntiClockwise)
				{                      
					fill_angle = currentAngle ;

					//...and draw it
					pie_sweep = 360 - zero_angle + fill_angle ;
					if (pie_sweep < 0)			pie_sweep += 360 ;
					if (pie_sweep >= 360)		pie_sweep -= 360 ;

					//too small to draw? - ignore?
					if (pie_sweep > 1)
					{
						pie_start = zero_angle ;
						ScaleAngles(false, ref pie_start, ref pie_sweep) ;

						brush.Color = fillColor ;
						g.FillPie(brush, rect, (float)pie_start, (float)pie_sweep) ;
					}
				}
				else		//Acw scale
				{                      
					fill_angle = currentAngle ;

					//...and draw it
					//pie_sweep = 360 - zero_angle + fill_angle ;
					pie_sweep = 360 - fill_angle + zero_angle ;
					if (pie_sweep < 0)			pie_sweep += 360 ;
					if (pie_sweep >= 360)		pie_sweep -= 360 ;

					//too small to draw? - ignore?
					if (pie_sweep > 1)
					{
						//pie_start = zero_angle ;
						pie_start = fill_angle ;
						ScaleAngles(false, ref pie_start, ref pie_sweep) ;

						brush.Color = fillColor ;
						g.FillPie(brush, rect, (float)pie_start, (float)pie_sweep) ;
					}
				}
 * 
 * 
*/
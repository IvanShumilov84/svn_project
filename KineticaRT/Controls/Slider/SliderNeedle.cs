using System;
using System.Collections;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.Serialization ;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using KineticaRT ;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Enumeration of possible Slider Needle Styles.
	/// All are variable widths
	/// </summary>
	public enum SliderNeedleStyles
	{
		/// <summary>
		/// Rectangular handle with central marker line
		/// </summary>
		Handle,
		/// <summary>
		/// Arrow that points Left or Down
		/// </summary>
		Arrow,
		/// <summary>
		/// Arrow that points Right or Up
		/// </summary>
		ReverseArrow,
		/// <summary>
		/// Simple line
		/// </summary>
		Line,
	}

	/// <summary>
	/// Collection of SliderNeedle objects
	/// </summary>
	[TypeConverter(typeof(SliderNeedleCollection.NeedleCollConverter))]
	public class SliderNeedleCollection : SubObjectCollection
	{
		#region Type Converter
		internal class NeedleCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					SliderNeedleCollection	coll		= (SliderNeedleCollection) value;
					SliderNeedle []			needles		= new SliderNeedle [coll.Count] ;
					ConstructorInfo			ctor_info ;
					
					ctor_info = typeof(SliderNeedleCollection).GetConstructor(new Type[]{typeof(SliderNeedle[])});

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
		/// Initialises a new instance of the SliderNeedleCollection class containing a single needle
		/// </summary>
		public SliderNeedleCollection()
		{
			//Always add one needle
			AddNeedle() ;
		}

		/// <summary>
		/// Initialises a new instance of the SliderMultiNeedle class containing the specified Needle objects
		/// </summary>
		/// <param name="needles">Array of SliderNeedle objects to add to collection</param>
		public SliderNeedleCollection(SliderNeedle [] needles)
		{
			AddRange(needles) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the SliderNeedle object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SliderNeedle this[int Index]
		{
			get
			{
				return (SliderNeedle) List[Index] ;
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
		/// Adds a new SliderNeedle object to the collection
		/// </summary>
		public void AddNeedle()
		{
			this.List.Add(new SliderNeedle()) ;
		}

		/// <summary>
		/// Adds the specified Needle objects to the collection
		/// </summary>
		/// <param name="newNeedles">Array of needles to add</param>
		public void AddRange(SliderNeedle [] newNeedles)
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
		/// <param name="Scale">Scale to which Needles are related</param>
		/// <param name="Orientation">Whether to draw horizontally or vertically</param>
		/// <param name="Offset">Offset defining how much of DrawRect is used</param>
		public void Draw(Graphics g, Rectangle DrawRect, KineticaRT.Controls.Scale Scale, ScaleOrientations Orientation, double Offset)
		{
			foreach (SliderNeedle needle in this)
			{
				needle.Draw(g, DrawRect, Scale, Orientation, Offset) ;
			}
		}

		/// <summary>
		/// Draw all the needles' fills
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		/// <param name="Scale">Scale to which Needles are related</param>
		/// <param name="Orientation">Whether to draw horizontally or vertically</param>
		/// <param name="Offset">Offset defining how much of DrawRect is used</param>
        /// <param name="GradientEnabled">GradientEnabled</param>
        /// <param name="GradientMode">GradientMode</param>
		//ft public void DrawFill(Graphics g, Rectangle DrawRect, KineticaRT.Controls.Scale Scale, ScaleOrientations Orientation, float Offset)
        public void DrawFill(Graphics g, Rectangle DrawRect, KineticaRT.Controls.Scale Scale, ScaleOrientations Orientation, float Offset, bool GradientEnabled, LinearGradientMode GradientMode) //ft Ajout du support pour le gradient
		{
			foreach (SliderNeedle needle in this)
			{
				needle.DrawFill(g, DrawRect, Scale, Orientation, Offset,GradientEnabled,GradientMode) ;
			}
		}

		/// <summary>
		/// Tests if a given position is close enough to a Needle's current position
		/// to start a mouse drag
		/// </summary>
		/// <param name="XPos">X Position of point to check</param>
		/// <param name="YPos">Y Position of point to check</param>
		/// <param name="Orientation">Orientation (horizontal or vertical) of the parent control</param>
		/// <param name="CaptureIndex">Index of Needle to capture, or -1 if none are close enough</param>
		/// <returns>True if click is close enough to capture a Needle</returns>
		public bool CheckForCapture(int XPos, int YPos, ScaleOrientations Orientation, out int CaptureIndex)
		{
			int			index		= 0 ;

			CaptureIndex = -1 ;

			foreach (SliderNeedle needle in this)
			{
				if (needle.CheckForCapture(XPos, YPos, Orientation))
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
		/// <param name="Orientation">Orientation (horizontal or vertical) of the Slider</param>
		/// <param name="DrawAll">(out) True if a full redraw (ie both needle and fill) will be required</param>
		/// <returns>True if any needles have moved and hence need redrawing</returns>
		public bool CalculatePosition(Rectangle DrawRect, KineticaRT.Controls.Scale Scale, ScaleOrientations Orientation, out bool DrawAll)
		{
			bool		ret_val		= false ;

			DrawAll = false ;

			foreach (SliderNeedle needle in this)
			{
				if (needle.CalculatePosition(DrawRect, Scale, Orientation))
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


	/// <summary>
	/// Needle object designed for use with a Slider control
	/// </summary>
	public class SliderNeedle : KineticaRT.Controls.Needle
	{
		#region Variables and Declarations
		private double				currentValue ;		//fraction
		private double				lastValue ;			//fraction
		private int					currentPixel ;		//last drawn x or y centre, depending on orientation

		//property variables
		private bool				centred ;
		private double				centrePoint ;
		private SliderNeedleStyles	style ;
		private int					width ;
		private double				start ;
		private double				finish ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the SliderNeedle class
		/// </summary>
		public SliderNeedle()
		{
			centred = true ;
			centrePoint = 0 ;
			start = 0.25 ;
			finish = 0.75 ;
			width = 12 ;
			style = SliderNeedleStyles.Handle ;

			currentValue = 0 ;
			lastValue = -1 ;
			currentPixel = 0 ;
		}
		
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the style of the Needle
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the style of the Needle")]
		[DefaultValue(SliderNeedleStyles.Handle)]
		public SliderNeedleStyles Style
		{
			get
			{
				return style;
			}
			set
			{
				if (style != value)
				{
					style = value;
					OnPropertyChanged("Style") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of the Needle in pixels
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the width of the Needle in pixels")]
		[DefaultValue(12)]
		public int Width
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
		/// Gets or sets the Start point of the Needle, as a proportion of the width of the parent control (0..1)
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the Start point of the Needle, as a proportion of the width of the parent control (0..1)")]
		[DefaultValue(0.25)]
		public double Start
		{
			get
			{
				return start;
			}
			set
			{
				if (start != value)
				{
					start = value;
					OnPropertyChanged("Start") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the FInish point of the Needle, as a proportion of the width of the parent control (0..1)
		/// </summary>
		[Category("Needle")]
		[Description("Gets or sets the Finish point of the Needle, as a proportion of the width of the parent control (0..1)")]
		[DefaultValue(0.75)]
		public double Finish
		{
			get
			{
				return finish ;
			}
			set
			{
				if (finish != value)
				{
					finish = value;
					OnPropertyChanged("Finish") ;
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
		/// Tests if a given position is close enough
		/// to the Needle's current position to start a mouse drag
		/// </summary>
		/// <param name="XPos">X Position of point to check</param>
		/// <param name="YPos">Y Position of point to check</param>
		/// <param name="Orientation">Orientation (horizontal or vertical) of the parent control</param>
		/// <returns>True if click is close enough to capture the Needle</returns>
		public bool CheckForCapture(int XPos, int YPos, ScaleOrientations Orientation)
		{
			if (!enabled)			return false ;

			if (!useTempValue)
			{
				//only allow if current value is valid
				if (krtValue is Values.Double)
				{
					//point has to be within so many pixels of current centre (width dependent)
					int			range	= (width > 8) ? width : 8 ;
					int			min		= currentPixel - (range / 2) ;
					int			max		= currentPixel + (range / 2) ;
					
					if (Orientation == ScaleOrientations.Vertical)
					{
						if (YPos >= min && YPos <= max)
						{
							double		n_start			= start ;
							double		n_finish		= finish ;

							if (start > finish)
							{
								n_start = finish ;
								n_finish = start ;
							}

							int x_start = ScaleX (n_start) ;
							int x_finish = ScaleX (n_finish) ;

							if (XPos >= x_start && XPos <= x_finish)
							{
								return true ;
							}
						}
					}
					else		//horizontal
					{
						if (XPos >= min && XPos <= max)
						{
							double		n_start			= start ;
							double		n_finish		= finish ;

							if (start > finish)
							{
								n_start = finish ;
								n_finish = start ;
							}

							int y_start = ScaleY (n_start) ;
							int y_finish = ScaleY (n_finish) ;

							if (YPos >= y_start && YPos <= y_finish)
							{
								return true ;
							}
						}
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
		/// <param name="DrawRect">Rectangle in which to position the Needles</param>
		/// <param name="objScale">Scale object related to the Control</param>
		/// <param name="Orientation">Orientation (horizontal or vertical) of the Parent Control</param>
		/// <returns>True if needle has moved since last drawn</returns>
		public bool CalculatePosition(Rectangle DrawRect, KineticaRT.Controls.Scale objScale, ScaleOrientations Orientation)
		{
			double	fraction ;
			double	current_value ;
			bool	moved			= false ;

			if (useTempValue)
			{
				current_value = tempValue ;
			}
			else if (krtValue is Values.Double)
			{
				current_value = krtValue.ToDouble() ;
			}
			else		//invalid value
			{
				currentValue = -1 ;		//set to an invalid value
				return true ;
			}

			fraction = ValueToFraction(current_value, objScale) ;

			if (fraction > 1)
			{
				currentValue = 1.03 ;
			}
			else if (fraction < 0)
			{
				currentValue = -0.03 ;
			}
			else
			{
				currentValue = fraction ;  
			}

			if (lastValue != currentValue)
			{
                if (lastValue > 1 || lastValue < 0 || System.Double.IsNaN(lastValue) || System.Double.IsNaN(currentValue))
				{
					//last value was an endstop
					moved = true ;
				}
				else
				{
					//check if it differs by a whole pixel from last drawn value
					double		new_pos ;
					double		old_pos ;
					double		diff ;

					if (Orientation == ScaleOrientations.Horizontal)
					{
						new_pos	= DrawRect.Width * currentValue ;
						old_pos	= DrawRect.Width * lastValue ;
					}
					else
					{
						new_pos	= DrawRect.Height * currentValue ;
						old_pos	= DrawRect.Height * lastValue ;
					}

					diff = Math.Abs(new_pos - old_pos) ;

					if (diff > 1)
					{
						moved = true ;
					}
				}
			}

			return moved ;
		}

		/// <summary>
		/// Draw the needle
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		/// <param name="objScale">Scale to which Needle is related</param>
		/// <param name="Orientation">Whether to draw horizontally or vertically</param>
		/// <param name="Offset">Offset defining how much of DrawRect is used</param>
		public void Draw(Graphics g, Rectangle DrawRect, KineticaRT.Controls.Scale objScale, ScaleOrientations Orientation, double Offset)
		{
			SetUpScaling(DrawRect) ;

			if (visible)
			{
				double		n_start			= start ;
				double		n_finish		= finish ;

				if (start > finish)
				{
					n_start = finish ;
					n_finish = start ;
				}

				if (krtValue is Values.Double)
				{
					switch (style)
					{
						case SliderNeedleStyles.Handle:
						{
							DrawNeedleHandle(g, n_start, n_finish, objScale, Orientation, Offset) ;
						}
						break ;

						case SliderNeedleStyles.Arrow:
						case SliderNeedleStyles.ReverseArrow:
						{
							DrawNeedleArrow(g, n_start, n_finish, objScale, Orientation, Offset) ;
						}
						break;

						case SliderNeedleStyles.Line:
						{
							DrawNeedleLine(g, n_start, n_finish, objScale, Orientation, Offset) ;
						}
						break ;
					}
				}
				//else don't draw the needle at all

				lastValue = currentValue ;
			}

			//used to do drawing for 'unconnected' here
		}

		//  Draws a handle-style needle
		private void DrawNeedleHandle(Graphics g, double start, double finish, KineticaRT.Controls.Scale objScale, ScaleOrientations Orientation, double Offset)
		{
			Rectangle		rect			= new Rectangle() ;
			Rectangle		light_rect ;
			Rectangle		dark_rect ;
			SolidBrush		brush			= new SolidBrush(color) ;

			g.SmoothingMode = SmoothingMode.AntiAlias ;

			if (Orientation == ScaleOrientations.Vertical)
			{
				rect.X		= ScaleX (start) ;
				rect.Width	= (int)(xScale * (finish - start)) ;
				rect.Height	= width ;

				//end stops are 8 pixels off ends
				if (objScale.AntiClockwise)
				{
					if (currentValue < 0)		//end stop low
					{
						rect.Y = ScaleY (Offset) - 8 - (int)(width / 2) ;
					}
					else if (currentValue > 1)	//end stop high
					{
						rect.Y = ScaleY (1 - Offset) + 8 - (int)(width / 2) ;
					}
					else					//in range
					{
						rect.Y = ScaleY (Offset + (1 - 2 * Offset) * currentValue) - (int)(width / 2) ;
					}
				}
				else
				{
					if (currentValue < 0)		//end stop low
					{
						rect.Y = ScaleY (1 - Offset) + 8 - (int)(width / 2) ;
					}
					else if (currentValue > 1)	//end stop high
					{
						rect.Y = ScaleY (Offset) - 8 - (int)(width / 2) ;
					}
					else					//in range
					{
						rect.Y = ScaleY ((1 - Offset) - (1 - 2 * Offset) * currentValue) - (int)(width / 2) ;
					}
				}

				currentPixel = rect.Y + width / 2 ;
			}
			else
			{
				rect.Y		= ScaleY (start) ;
				rect.Height = (int)(yScale * (finish - start)) ;
				rect.Width	= width ;

				//end stops are 3% off ends
				if (objScale.AntiClockwise)
				{
					rect.X = ScaleX ((1 - Offset) - (1 - 2 * Offset) * currentValue) - (int)(width / 2) ;
				}
				else
				{
					rect.X = ScaleX (Offset + (1 - 2 * Offset) * currentValue) - (int)(width / 2) ;
				}

				currentPixel = rect.X + width / 2 ;
			}

			g.FillRectangle(brush, rect) ;

			//NB: colors should be lighter if 'active'

			//draw shading around the edge
			KineticaRT.Controls.Control.DrawSquareBorder(g, rect, BorderType.Raised, color, 2) ;

			light_rect = rect ;
			dark_rect = rect ;

			//draw marker line (doesnt go over raising)
			if (Orientation == ScaleOrientations.Vertical)
			{
				light_rect.Height = dark_rect.Height = 2 ;

				dark_rect.Y		= rect.Y - 2 + (int)(width / 2) ;
				light_rect.Y	= dark_rect.Y + 2 ;
			}
			else
			{
				light_rect.Width = dark_rect.Width = 2 ;

				dark_rect.X		= rect.X - 2 + (int)(width / 2) ;
				light_rect.X	= dark_rect.X + 2 ;
			}

			brush.Color = ControlPaint.Light(color) ;
			g.FillRectangle(brush, light_rect) ;
			brush.Color = ControlPaint.Dark(color) ;
			g.FillRectangle(brush, dark_rect) ;

			brush.Dispose() ;
			g.SmoothingMode = SmoothingMode.None ;
		}

		//  Draws an arrow-style needle
		private void DrawNeedleArrow(Graphics g, double start, double finish, KineticaRT.Controls.Scale objScale, ScaleOrientations Orientation, double Offset)
		{
            if (double.IsNaN(currentValue)) return;
            
            Point[] pts = new Point[7];
			Brush		brush	= new SolidBrush(color) ;

			g.SmoothingMode = SmoothingMode.AntiAlias ;

			if (Orientation == ScaleOrientations.Vertical)
			{
				if (style == SliderNeedleStyles.Arrow)
				{
					pts[0].X = pts[6].X							= ScaleX (start) ;
					pts[1].X = pts[2].X = pts[4].X = pts[5].X	= ScaleX (finish - 0.06) ;
					pts[3].X									= ScaleX (finish) ;
				}
				else	//reverse arrow
				{
					pts[0].X = pts[6].X							= ScaleX (finish) ;
					pts[1].X = pts[2].X = pts[4].X = pts[5].X	= ScaleX (start + 0.06) ;
					pts[3].X									= ScaleX (start) ;
				}

				//end stops are offset by 8 pixels
				if (!objScale.AntiClockwise)
				{
					if (currentValue < 0)			//end stop low
					{
						pts[3].Y = ScaleY (1 - Offset) + 8 ;
					}
					else if (currentValue > 1)		//end stop high
					{
						pts[3].Y = ScaleY (Offset) - 8 ;
					}
					else						// in range
					{
						pts[3].Y = ScaleY ((1 - Offset) - (1 - 2 * Offset) * currentValue) ;
					}
				}
				else
				{
					if (currentValue < 0)			//end stop low
					{
						pts[3].Y = ScaleY (Offset) - 8 ;
					}	
					else if (currentValue > 1)		//end stop high
					{	
						pts[3].Y = ScaleY (1 - Offset) + 8 ;
					}	
					else					// in range
					{	
						pts[3].Y = ScaleY (Offset + (1 - 2 * Offset) * currentValue) ;
					}
				}
				pts[0].Y = pts[1].Y = pts[3].Y - (int)(width / 4) ;
				pts[5].Y = pts[6].Y = pts[3].Y + (int)(width / 4) ;
				pts[2].Y			= pts[3].Y - (int)(width / 2) ;
				pts[4].Y			= pts[3].Y + (int)(width / 2) ;

				currentPixel = pts[3].Y ;
			}
			else	//horizontal
			{
				if (style == SliderNeedleStyles.Arrow)
				{
					pts[0].Y = pts[6].Y							= ScaleY (start) ;
					pts[1].Y = pts[2].Y = pts[4].Y = pts[5].Y	= ScaleY (finish - 0.06) ;
					pts[3].Y									= ScaleY (finish) ;
				}
				else	//reverse arrow
				{
					pts[0].Y = pts[6].Y							= ScaleY (finish) ;
					pts[1].Y = pts[2].Y = pts[4].Y = pts[5].Y	= ScaleY (start + 0.06) ;
					pts[3].Y									= ScaleY (start) ;
				}

				//end stops are offset by 3%
				if (objScale.AntiClockwise)
				{
					pts[3].X = ScaleX ((1 - Offset) - (1 - 2 * Offset) * currentValue) ;
				}
				else
				{
					pts[3].X = ScaleX (Offset + (1 - 2 * Offset) * currentValue) ;
				}
				pts[0].X = pts[1].X = pts[3].X - (int)(width / 4) ;
				pts[5].X = pts[6].X = pts[3].X + (int)(width / 4) ;
				pts[2].X			= pts[3].X - (int)(width / 2) ;
				pts[4].X			= pts[3].X + (int)(width / 2) ;

				currentPixel = pts[3].X ;
			}

			g.FillPolygon(brush, pts) ;
			brush.Dispose() ;

			//##iff active?
			Pen		pen = new Pen(color) ;//ControlPaint.Light(color)) ;
			g.DrawPolygon(pen, pts) ;
			pen.Dispose() ;
			
			g.SmoothingMode = SmoothingMode.None ;
		}

		//  Draws a line-style needle
		private void DrawNeedleLine(Graphics g, double start, double finish, KineticaRT.Controls.Scale objScale, ScaleOrientations Orientation, double Offset)
		{
			if (System.Double.IsNaN(currentValue)) return ;

			Point		pt_start		= new Point() ;
			Point		pt_end			= new Point() ;

			if (Orientation == ScaleOrientations.Vertical)
			{
				pt_start.X	= ScaleX (start) ;
				pt_end.X	= ScaleX (finish) ;

				//end stops are offset by 8 pixels
				if (objScale.AntiClockwise)
				{
					if (currentValue < 0)			//end stop low
					{
						pt_start.Y = ScaleY (Offset) - 8 ;
					}
					else if (currentValue > 1)		//end stop high
					{
						pt_start.Y = ScaleY (1 - Offset) + 8 ;
					}
					else						// in range
					{
						pt_start.Y = ScaleY (Offset + (1 - 2 * Offset) * currentValue) ;
					}
				}
				else
				{
					if (currentValue < 0)			//end stop low
					{
						pt_start.Y = ScaleY (1 - Offset) + 8 ;
					}
					else if (currentValue > 1)		//end stop high
					{
						pt_start.Y = ScaleY (Offset) - 8 ;
					}
					else						// in range
					{
						pt_start.Y = ScaleY ((1 - Offset) - (1 - 2 * Offset) * currentValue) ;
					}
				}
				pt_end.Y = pt_start.Y ;

				currentPixel = pt_end.Y ;
			}
			else
			{
				pt_start.Y	= ScaleY (start) ;
				pt_end.Y	= ScaleY (finish) + 1 ;

				//end stops are offset by 3%
				if (objScale.AntiClockwise)
				{
					pt_start.X = ScaleX ((1 - Offset) - (1 - 2 * Offset) * currentValue) ;
				}
				else
				{
					pt_start.X = ScaleX (Offset + (1 - 2 * Offset) * currentValue) ;
				}
				pt_end.X = pt_start.X ;

				currentPixel = pt_end.X ;
			}

			Pen		pen = new Pen(color, (float)width) ;
			g.DrawLine(pen, pt_start, pt_end) ;
			pen.Dispose() ;
		}

		/// <summary>
		/// Draw the needle's Fill
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="drawRect">Rectangle in which to draw</param>
		/// <param name="objScale">Scale to which Needle is related</param>
		/// <param name="orientation">Whether to draw horizontally or vertically</param>
		/// <param name="offset">Offset defining how much of DrawRect is used</param>
        /// <param name="gradientEnabled">gradientEnabled</param>
        /// <param name="gradientMode">gradientMode</param>
		//ft public void DrawFill(Graphics g, Rectangle drawRect, KineticaRT.Controls.Scale objScale, ScaleOrientations orientation, float offset)
        public void DrawFill(Graphics g, Rectangle drawRect, KineticaRT.Controls.Scale objScale, ScaleOrientations orientation, float offset, bool gradientEnabled, LinearGradientMode gradientMode)//ft Ajout du support pour le gradient
		{
            if (!fillEnabled || !(krtValue is Values.Double) || double.IsNaN(currentValue)) return;

			//NS050922 if (currentValue > 1 || currentValue < 0) return ;

			SetUpScaling(drawRect) ;
			SolidBrush brush = new SolidBrush(fillColor) ;

			if (fillStyle == FillStyles.Segmented)
			{
				ThresholdCollection		th_coll				= objScale.Thresholds ;
				int						th_count			= th_coll.Count ;
				if (th_count == 0) return ;
				float					min_fill_fraction ;			
				float					max_fill_fraction ;			
				float					fill_start			= (float)fillStart ;
				float					fill_finish			= (float)fillFinish ;
				Rectangle				rect				= new Rectangle() ;
				bool					anticlockwise		= objScale.AntiClockwise ;
				float					offset_val1			= (1 - 2 * offset) ;
				float					one_minus_offset	= (1 - offset) ;

				if (fillStart > fillFinish)
				{
					fill_start = (float)fillFinish ;
					fill_finish = (float)fillStart ;
				}
			
				//set up the fill width (constant)
				if (orientation == ScaleOrientations.Vertical)
				{
					rect.X		= ScaleX (fill_start) ;
					rect.Width	= (int)(drawRect.Width * (fill_finish - fill_start)) ;
				}
				else
				{
					rect.Y	= ScaleY (fill_start) ; 
					rect.Height	= (int)(drawRect.Height * (fill_finish - fill_start)) ;
				}

				if (!centred) centrePoint = minimum ;

				min_fill_fraction = objScale.ValueToFraction((float)centrePoint) ;
				if (currentValue >= min_fill_fraction)
				{
					int th_index ;
					float new_fraction = min_fill_fraction ;
					max_fill_fraction = (float)currentValue ;

					for (th_index = 0 ; th_index < th_count; th_index++)
					{
						if (th_coll[th_index].Enabled)
						{
							new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value) ;
							if (new_fraction > min_fill_fraction) break ;
						}
					}

					if (th_index >= th_count) new_fraction = 1.03F ;

					while (true)
					{
						float end_fraction = (new_fraction >= max_fill_fraction) ? max_fill_fraction : new_fraction ;

						if (orientation == ScaleOrientations.Vertical)
						{
							if (anticlockwise)
							{
								rect.Y		= ScaleY (offset + offset_val1 * min_fill_fraction) ;
							}
							else
							{
								rect.Y		= ScaleY (one_minus_offset - offset_val1 * end_fraction) ; 
							}
							rect.Height = (int)(drawRect.Height * offset_val1 * (end_fraction-min_fill_fraction)) + 1 ;
						}
						else
						{
							if (anticlockwise)
							{
								rect.X		= ScaleX (one_minus_offset - offset_val1 * end_fraction) ; 
							}
							else
							{
								rect.X	= ScaleX (offset + offset_val1 * min_fill_fraction) ;
							}
							rect.Width = (int)(drawRect.Width * offset_val1 * (end_fraction - min_fill_fraction)) + 1 ;
						}

						g.FillRectangle(brush, rect) ;

						if (new_fraction >= max_fill_fraction) break ;
				
						if (th_index >= th_count) break ;
						brush.Color = th_coll[th_index].MarkColor ;
						min_fill_fraction = new_fraction ;
						th_index++ ;
						while ((th_index < th_count) && !th_coll[th_index].Enabled)
						{
							th_index++ ;
						}							
						new_fraction = (th_index == th_count) ? max_fill_fraction : objScale.ValueToFraction((float)th_coll[th_index].Value) ;
					}
				}
				else
				{
					int th_index ;
					float new_fraction = min_fill_fraction ;
					max_fill_fraction = (float)currentValue ;

					for (th_index = th_count - 1 ; th_index >= 0; th_index--)
					{
						new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value) ;
						if (new_fraction < min_fill_fraction) break ;
					}
				
					if (th_index < 0) new_fraction = -0.03F ;
					
					while (true)
					{
						float end_fraction = (new_fraction <= max_fill_fraction) ? max_fill_fraction : new_fraction ;

						if (orientation == ScaleOrientations.Vertical)
						{
							if (anticlockwise)
							{
								rect.Y		= ScaleY (offset + offset_val1 * end_fraction) ;
							}
							else
							{
								rect.Y		= ScaleY (one_minus_offset - offset_val1 * min_fill_fraction) ; 
							}
							rect.Height = (int)(drawRect.Height * offset_val1 * (min_fill_fraction - end_fraction)) + 1 ;
						}
						else
						{
							if (anticlockwise)
							{
								rect.X		= ScaleX (one_minus_offset - offset_val1 * min_fill_fraction) ; 
							}
							else
							{
								rect.X	= ScaleX (offset + offset_val1 * end_fraction) ;
							}
							rect.Width = (int)(drawRect.Width * offset_val1 * (min_fill_fraction - end_fraction)) + 1 ;
						}

						g.FillRectangle(brush, rect) ;

						if (new_fraction <= max_fill_fraction) break ;
			
						if (th_index < 0) break ;
						brush.Color = th_coll[th_index].MarkColor ;
						min_fill_fraction = new_fraction ;
						th_index-- ;
						while ((th_index >= 0) && !th_coll[th_index].Enabled)
						{
							th_index-- ;
						}							
						new_fraction = (th_index < 0) ? max_fill_fraction : objScale.ValueToFraction((float)th_coll[th_index].Value) ;
					}
				}
			}
			else if (fillStyle == FillStyles.Permanent)
			{
				ThresholdCollection		th_coll				= objScale.Thresholds ;
				int						th_count			= th_coll.Count ;
				if (th_count == 0) return ;
				float					fill_start			= (float)fillStart ;
				float					fill_finish			= (float)fillFinish ;
				Rectangle				rect				= new Rectangle() ;
				bool					anticlockwise		= objScale.AntiClockwise ;
				float					offset_val1			= (1 - 2 * offset) ;
				float					one_minus_offset	= (1 - offset) ;

				if (fillStart > fillFinish)
				{
					fill_start = (float)fillFinish ;
					fill_finish = (float)fillStart ;
				}
			
				//set up the fill width (constant)
				if (orientation == ScaleOrientations.Vertical)
				{
					rect.X		= ScaleX (fill_start) ;
					rect.Width	= (int)(drawRect.Width * (fill_finish - fill_start)) ;
				}
				else
				{
					rect.Y	= ScaleY (fill_start) ; 
					rect.Height	= (int)(drawRect.Height * (fill_finish - fill_start)) ;
				}

				if (!centred) centrePoint = minimum ;

				float center_fraction = objScale.ValueToFraction((float)centrePoint) ;
				int th_index ;
				float new_fraction = objScale.ValueToFraction((float)th_coll[0].Value) ;
				brush.Color = th_coll[0].MarkColor ;
				float start_fraction = 0 ;
				float end_fraction = new_fraction ;
				bool passed_centre_point = false ;

				th_index = 0 ;
				
				while (true)
				{
					if (orientation == ScaleOrientations.Vertical)
					{
						if (anticlockwise)
						{
							rect.Y		= ScaleY (offset + offset_val1 * start_fraction) ;
						}
						else
						{
							rect.Y		= ScaleY (one_minus_offset - offset_val1 * end_fraction) ; 
						}
						rect.Height = (int)(drawRect.Height * offset_val1 * (end_fraction-start_fraction)) + 1 ;
					}
					else
					{
						if (anticlockwise)
						{
							rect.X		= ScaleX (one_minus_offset - offset_val1 * end_fraction) ; 
						}
						else
						{
							rect.X	= ScaleX (offset + offset_val1 * start_fraction) ;
						}
						rect.Width = (int)(drawRect.Width * offset_val1 * (end_fraction - start_fraction)) + 1 ;
					}

					g.FillRectangle(brush, rect) ;

					th_index++ ;
					if ((start_fraction>=1.0) ||(th_index >= th_count)) break ;

					new_fraction = objScale.ValueToFraction((float)th_coll[th_index].Value) ;

					if (new_fraction < center_fraction)
					{
						start_fraction = end_fraction ;
						end_fraction = new_fraction ;
						brush.Color = th_coll[th_index].MarkColor ;
					}
					else
					{
						if (!passed_centre_point)
						{
							brush.Color = fillColor ;
							start_fraction = end_fraction ;
							end_fraction = new_fraction ; ;

							if (orientation == ScaleOrientations.Vertical)
							{
								if (anticlockwise)
								{
									rect.Y		= ScaleY (offset + offset_val1 * start_fraction) ;
								}
								else
								{
									rect.Y		= ScaleY (one_minus_offset - offset_val1 * end_fraction) ; 
								}
								rect.Height = (int)(drawRect.Height * offset_val1 * (end_fraction-start_fraction)) + 1 ;
							}
							else
							{
								if (anticlockwise)
								{
									rect.X		= ScaleX (one_minus_offset - offset_val1 * end_fraction) ; 
								}
								else
								{
									rect.X	= ScaleX (offset + offset_val1 * start_fraction) ;
								}
								rect.Width = (int)(drawRect.Width * offset_val1 * (end_fraction - start_fraction)) + 1 ;
							}

							g.FillRectangle(brush, rect) ;

							passed_centre_point = true ;
						}
						start_fraction = new_fraction ;
						if (th_index < th_count-1)
						{
							end_fraction = objScale.ValueToFraction((float)th_coll[th_index+1].Value) ; ;
						}
						else
						{
							end_fraction = 1 ;
						}
						brush.Color = th_coll[th_index].MarkColor ;
					}
				}
			}
			else  // Solid 
			{
				double					min_fill_fraction ;			
				double					max_fill_fraction ;			
				float					fill_start			= (float)fillStart ;
				float					fill_finish			= (float)fillFinish ;
				Rectangle				rect				= new Rectangle() ;

				if (fillStart > fillFinish)
				{
					fill_start = (float)fillFinish ;
					fill_finish = (float)fillStart ;
				}

				if (centred)
				{
					//centred fill
					min_fill_fraction = objScale.ValueToFraction(centrePoint) ;
					if (currentValue >= min_fill_fraction)
					{
						max_fill_fraction = currentValue ;
					}
					else
					{
						max_fill_fraction = min_fill_fraction ;
						min_fill_fraction = currentValue ;
					}
				}
				else
				{
					//normal fill
					min_fill_fraction = 0 ;
					max_fill_fraction = currentValue ;
				}

				//set up the fill width (constant)
				if (orientation == ScaleOrientations.Vertical)
				{
					rect.X		= ScaleX (fill_start) ;
					rect.Width	= (int)(drawRect.Width * (fill_finish - fill_start)) ;

					if (!objScale.AntiClockwise)
					{
						rect.Y		= ScaleY ((1 - offset) - (1 - 2 * offset) * max_fill_fraction) ; 
					}
					else
					{
						rect.Y		= ScaleY (offset + (1 - 2 * offset) * min_fill_fraction) ;
					}
					rect.Height = (int)(drawRect.Height * (1 - 2 * offset) * (max_fill_fraction - min_fill_fraction)) + 1 ;
				}
				else
				{
					rect.Y	= ScaleY (fill_start) ; 
					rect.Height	= (int)(drawRect.Height * (fill_finish - fill_start)) ;

					if (objScale.AntiClockwise)
					{
						rect.X		= ScaleX ((1 - offset) - (1 - 2 * offset) * max_fill_fraction) ; 
					}
					else
					{
						rect.X	= ScaleX (offset + (1 - 2 * offset) * min_fill_fraction) ;
					}
					rect.Width = (int)(drawRect.Width * (1 - 2 * offset) * (max_fill_fraction - min_fill_fraction)) + 1 ;
				}
                if (rect.Width > 0 && rect.Height > 0) //ft si le le width ou heignt est 0 ca lance une exception
                {
                    if (gradientEnabled)//ft Ajout du support pour painter en gradient
                    {
                        Brush gradientBrush = new LinearGradientBrush(rect, fillColor, ControlPaint.Dark(fillColor), gradientMode);
                        g.FillRectangle(gradientBrush, rect);
                        gradientBrush.Dispose();
                    }
                    else
                    {
                        g.FillRectangle(brush, rect);
                    }
                }
			}

			brush.Dispose() ;
		}	
		
		#endregion
	}
}

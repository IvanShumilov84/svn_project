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


	/// <summary>
	/// Needle object for use with a Dial
	/// </summary>
	public class DialNeedle : KineticaRT.Controls.Needle
	{
		#region Variables and Declarations
		const double	END_STOP_OFFSET		= 7 ;		//degrees

		private double				currentAngle ;			//in degrees cw from top
		private double				lastAngle ;
		private bool				currentlyEndStop ;
		private bool				currentlyEndStopHi ;
		private Point[]				shadedPoints ;

		//property variables
		private DialNeedleStyles		style ;
		private double				width ;
		private double				length ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the DialNeedle class
		/// </summary>
		public DialNeedle()
		{
			width = 0.1 ;
			length = 0.8 ;

			style = DialNeedleStyles.Shaded ;

			lastAngle = -1 ;		//start last angle at invalid
			currentAngle = 0 ;
			currentlyEndStop = false ;
			currentlyEndStopHi = false ;

			shadedPoints = new Point[4] ;
		}
		
		#endregion

		#region Properties
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
				if (style != value)
				{
					style = value;
					OnPropertyChanged("Style") ;
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
		public bool CalculatePosition(Rectangle DrawRect, KineticaRT.Controls.Scale objScale)
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

				return lastAngle >= 0 ;
			}

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

			if (currentlyEndStop)
			{
				if (currentlyEndStopHi)
				{
					if (!objScale.AntiClockwise)
					{
						currentAngle = objScale.ZeroAngle + objScale.SweepAngle + END_STOP_OFFSET ;
					}
					else
					{
						currentAngle = objScale.ZeroAngle - objScale.SweepAngle - END_STOP_OFFSET ;
					}
				}
				else
				{
					if (!objScale.AntiClockwise)
					{
						currentAngle = objScale.ZeroAngle - END_STOP_OFFSET ;
					}
					else
					{
						currentAngle = objScale.ZeroAngle + END_STOP_OFFSET ;
					}
				}
			}
			else
			{
				if (!objScale.AntiClockwise)
				{
					currentAngle = objScale.ZeroAngle + (objScale.SweepAngle * fraction) ;  
				}
				else
				{
					currentAngle = objScale.ZeroAngle - (objScale.SweepAngle * fraction) ;  
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

			return currentAngle != lastAngle ;
		}

		/// <summary>
		/// Draw the needle
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		public void Draw(Graphics g, Rectangle DrawRect)
		{
			SetUpScaling(DrawRect) ;

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
		/// <param name="DrawRect">Rectangle in which to draw</param>
		/// <param name="ScaleObj">Scale object whose Thresholds may be used</param>
		/// <param name="MultipleNeedles">True if more than one needle exists on the control</param>
		/// <param name="BackBrush">Brush for control's background</param>
		public void DrawFill(Graphics g, Rectangle DrawRect, KineticaRT.Controls.Scale ScaleObj, bool MultipleNeedles, Brush BackBrush)
		{
			SetUpScaling(DrawRect) ;

			if (fillEnabled && (krtValue is Values.Double))
			{
				if (fillStyle != FillStyles.Permanent && currentlyEndStop)
				{
					//nothing to do
					return ;
				}

				Region			old_rgn				= null ;
				Rectangle		centre_rect ;

				centre_rect = new Rectangle(ScaleX (0.5 - 0.5 * fillStart),
											ScaleY (0.5 - 0.5 * fillStart),
                        					(int)(xScale * fillStart),
											(int)(yScale * fillStart)			) ;

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
				ThresholdCollection		collTholds ;
				int						count ;
				double					fill_angle ;		//degrees
				int						index ;
				Threshold				thold				= null ;
				double					fraction ;
				double					pie_sweep ;
				double					pie_start ;
				SolidBrush				brush				= new SolidBrush(fillColor) ;

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

				if (ScaleObj.SweepAngle > 359.5)
				{
					sweep_angle = 359.5 ;
				}
				else
				{
					sweep_angle = ScaleObj.SweepAngle ;
				}
				zero_angle = ScaleObj.ZeroAngle ;

				collTholds = ScaleObj.Thresholds ;
				count = collTholds.Count ;

				if (!ScaleObj.AntiClockwise)
				{                      
					if (fillStyle != FillStyles.Permanent)
					{
						fill_angle = currentAngle ;
					}
					else
					{
						fill_angle = zero_angle + sweep_angle ;
						if (fill_angle < 0)			fill_angle += 360 ;
						if (fill_angle >= 360)		fill_angle -= 360 ;
					}

					for (index = count; index >= 0; index--)
					{
						if (index > 0)
						{
							thold = collTholds[index - 1] ;

							if (fillStyle == FillStyles.Solid || !thold.Enabled)
							{
								continue ;
							}
							fraction = ScaleObj.ValueToFraction (thold.Value) ;
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

					for (index = count; index >= 0; index--)
					{
						if (index > 0)
						{
							thold = collTholds[index - 1] ;
	
							if (fillStyle == FillStyles.Solid || !thold.Enabled)
							{
								continue ;
							}
							fraction = ScaleObj.ValueToFraction(thold.Value) ;
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

				brush.Dispose() ;
			}
		}	

		#endregion
	}
}

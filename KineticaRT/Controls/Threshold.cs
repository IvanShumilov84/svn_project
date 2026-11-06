using System;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections;
using System.Globalization;
using System.Runtime.Serialization ;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Enumeration of possible Threshold Mark styles
	/// </summary>
	public enum ThresholdMarkStyles
	{
		/// <summary>
		/// Triangle that points to scale line
		/// </summary>
		Triangle,
		/// <summary>
		/// Straight line perpendicular to scale line
		/// </summary>
		Line,
		/// <summary>
		/// Circle
		/// </summary>
		Circle,
	}

	/// <summary>
	/// Collection of Threshold objects sorted in order of Value
	/// </summary>
	[TypeConverter(typeof(ThresholdCollection.ThresholdCollConverter))]
	public class ThresholdCollection : SubObjectCollection
	{
		#region Type Converter
		internal class ThresholdCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					ThresholdCollection	coll		= (ThresholdCollection) value;
					Threshold []		tholds		= new Threshold [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(ThresholdCollection).GetConstructor(new Type[]{typeof(Threshold[])});

					//copy tholds into array
					for (int index = 0; index < coll.Count; index++)
					{
						tholds[index] = coll[index] ;
					}

					return new InstanceDescriptor(ctor_info, new object[]{tholds}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the ThresholdCollection class
		/// </summary>
		public ThresholdCollection()
		{
		}

		/// <summary>
		/// Initialises a new instance of the ThresholdCollection class and fills it with the specified Threshold objects
		/// </summary>
		/// <param name="tholds">Array of Threshold objects to add to collection</param>
		public ThresholdCollection(Threshold [] tholds)
		{
			AddRange(tholds) ;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Add a new Threshold of the specified color and Value to the collection.
		/// </summary>
		/// <remarks>The Threshold will be inserted in the correct position according to its value</remarks>
		/// <param name="Value">Value of the new Threshold</param>
		/// <param name="MarkColor">Color of the new Threshold</param>
		public void AddThreshold(double Value, Color MarkColor)
		{
			InsertThreshold(new Threshold(Value, MarkColor)) ;
		}

		/// <summary>
		/// Add a new Threshold at the specified Value to the collection.
		/// </summary>
		/// <remarks>The Threshold will be inserted in the correct position according to its value</remarks>
		/// <param name="Value">Value of the new Threshold</param>
		public void AddThreshold(double Value)
		{
			this.AddThreshold(Value, Color.Red) ;
		}

		/// <summary>
		/// Add an array of Threshold objects
		/// </summary>
		/// <param name="newThresholds">Array of Thresholds to add</param>
		public void AddRange(Threshold [] newThresholds)
		{
			for (int index = 0; index < newThresholds.Length; index++)
			{
				this.List.Add(newThresholds[index]) ;
			}
		}

		/// <summary>
		/// Draws all the Thresholds in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to do the drawing</param>
		/// <param name="Ratio">Ratio of object to draw in (0-1): used with Circular Scales</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		/// <param name="Orientation">Orientation of associated Scale</param>
		/// <param name="Position">Position of Scale (0-1): used with straight scales</param>
		/// <param name="objScale">Scale object associated with the collection</param>
		public void Draw(Graphics g, double Ratio, Rectangle DrawRect, ScaleOrientations Orientation, double Position, Scale objScale)
		{
			foreach (Threshold thold in this)
			{
				thold.Draw(g, Ratio, DrawRect, Orientation, Position, objScale) ;
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// List InsertComplete Event
		/// </summary>
		/// <param name="Index">Index of inserted Threshold</param>
		/// <param name="Value">Inserted Threshold</param>
		protected override void OnInsertComplete(int Index, object Value)
		{
			//SortItem(Index) ;

			base.OnInsertComplete(Index, Value) ;
		}

		private void SortItem(int Index)
		{
			Threshold		thold		= (Threshold)List[Index] ;

			if ( (Index > 0 && ((Threshold)List[Index - 1]).Value > thold.Value) ||
				(Index < List.Count - 1 && ((Threshold)List[Index + 1]).Value < thold.Value) )
			{
				List.RemoveAt(Index) ;
				InsertThreshold(thold) ;
			}
		}

		private void InsertThreshold(Threshold Thold)
		{
			if (List.Count == 0)
			{
				this.List.Add(Thold) ;
			}
			else
			{
				if ( Thold.Value < ((Threshold)List[0]).Value )
				{
					//add to start of list
					this.List.Insert(0, Thold) ;
				}
				else
				{
					int		index ;

					for (index = 1; index < List.Count; index++)
					{
						if ( Thold.Value < ((Threshold)List[index]).Value )
						{
							//we've found it's position!
							break ;
						}
					}
					this.List.Insert(index, Thold) ;
				}
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Threshold object currently at the given index position
		/// </summary>
		/// <remarks>If Threshold Values are changed, the collection may be re-ordered</remarks>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Threshold this[int Index]
		{
			get
			{
				return (Threshold) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					List[Index] = value ;
					SortItem(Index) ;
				}
			}
		}
		#endregion
	}


	/// <summary>
	/// Threshold mark object, usually part of a Scale
	/// </summary>
	[TypeConverterAttribute (typeof(ObjectTypeConverter))]
	[DefaultProperty("Value")]
	public class Threshold : KineticaRT.Controls.SubObject
	{
		#region Variables and declarations
		private bool				enabled ;
		private double				value ;
		private ThresholdMarkStyles	style ;
		private double				markSize ;
		private Color				color ;
		private ScaleMarkPositions	markPosition ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Threshold class
		/// </summary>
		public Threshold()
		{
			Initialise(0.0, Color.Red) ;
		}

		/// <summary>
		/// Initialises a new instance of the Threshold class with the specified Value and Color
		/// </summary>
		/// <param name="Value">Value of Threshold</param>
		/// <param name="MarkColor">Color of Threshold</param>
		public Threshold(double Value, Color MarkColor)
		{
			Initialise(Value, MarkColor) ;
		}

		/// <summary>
		/// Initialises a new instance of the Threshold class with the specified Value
		/// </summary>
		/// <param name="Value">Value of Threshold</param>
		public Threshold(double Value)
		{
			Initialise(Value, Color.Red) ;
		}

		private void Initialise(double Value, Color Clr)
		{
			this.value	= Value ;
			markSize	= 0.08 ;
			enabled		= true ;
			style		= ThresholdMarkStyles.Triangle ;
			markPosition= ScaleMarkPositions.Outside ;
			color		= Clr ;
		}

		#endregion

		#region Properties
		///<summary>
		///Gets or sets the value at which the Threshold is drawn
		///</summary>
		[Category("Threshold")]
		[Description("Gets or sets the value at which the Threshold is drawn")]
		[DefaultValue(0.0)]
		public double Value
		{
			get
			{
				return this.value ;
			}
			set
			{
				if (this.value != value)
				{
					this.value = value ;
					OnPropertyChanged("Value") ;
				}
			}
		}

		///<summary>
		///Gets or sets the Color of the Threshold mark
		///</summary>
		[Category("Threshold")]
		[Description("Gets or sets the Color of the Threshold mark")]
		public Color MarkColor
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
					OnPropertyChanged("MarkColor") ;
				}
			}
		}
		private bool ShouldSerializeMarkColor()
		{
			return color != Color.Red ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetMarkColor()
		{
			color = Color.Red ;
		}

		///<summary>
		///Gets or sets whether the Threshold is Enabled
		///</summary>
		[Category("Threshold")]
		[Description("Gets or sets whether the Threshold is Enabled")]
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

        private bool useForFill = true;
        ///<summary>
        ///Gets or sets whether the threshold is used for the display of fill on a scale.
        ///</summary>
        [Category("Threshold")]
        [Description("Whether the threshold is used for the display of fill on a scale.")]
        [DefaultValue(true)]
        public bool UseForFill
        {
            get
            {
                return useForFill;
            }
            set
            {
                if (useForFill != value)
                {
                    useForFill = value;
                    OnPropertyChanged("UseForFill");
                }
            }
        }



		///<summary>
		///Gets or sets the style of the Threshold Mark
		///</summary>
		[Category("Threshold")]
		[Description("Gets or sets the style of the Threshold Mark")]
		[DefaultValue(ThresholdMarkStyles.Triangle)]
		public ThresholdMarkStyles MarkStyle
		{
			get
			{
				return style ;
			}
			set
			{
				if (style != value)
				{
					style = value ;
					OnPropertyChanged("MarkStyle") ;
				}
			}
		}

		///<summary>
		///Gets or sets the position of the Threshold Mark relative to the Scale line
		///</summary>
		[Category("Threshold")]
		[Description("Gets or sets the position of the Threshold Mark relative to the Scale line")]
		[DefaultValue(ScaleMarkPositions.Outside)]
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

		///<summary>
		///Gets or sets the size of the Threshold Mark, as a proportion of the control (0..1)
		///</summary>
		[Category("Threshold")]
		[Description("Gets or sets the size of the Threshold Mark, as a proportion of the control (0..1)")]
		[DefaultValue(0.08)]
		public double MarkSize
		{
			get
			{
				return markSize ;
			}
			set
			{
				if (markSize != value)
				{
					markSize = value ;
					OnPropertyChanged("MarkSize") ;
				}
			}
		}

		#endregion

		#region Drawing
		/// <summary>
		/// Draws the Threshold
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="Ratio">Ratio of object to draw in (0-1): used with Circular Scales</param>
		/// <param name="DrawRect">Rectangle in which to draw</param>
		/// <param name="Orientation">Whether to draw with respect to a Horizontal, Vertical or Circular scale</param>
		/// <param name="Position">Position of Scale (0-1): used with straight scales</param>
		/// <param name="objScale">Scale object to which Threshold is related</param>
		public void Draw(Graphics g, double Ratio, Rectangle DrawRect, ScaleOrientations Orientation, double Position, Scale objScale)
		{
			if (!enabled)		return ;

			double		pos ;
			SolidBrush	brush		= new SolidBrush(color) ;
			Pen			pen			= new Pen(color, 3) ;

			pos = objScale.ValueToPosition(this.value, Orientation, Ratio) ;

			if (pos < 0)
			{
				//out of current scale range - don't draw it
				return ;
			}

			SetUpScaling(DrawRect) ;

			switch(Orientation)
			{
				case ScaleOrientations.Circular:
				{
					DrawThresholdMark (g, pos, Ratio, brush, pen) ;
				}
				break ;
				
				case ScaleOrientations.Vertical:
				{
					DrawVerticalThresholdMark (g, pos, Position, brush, pen) ;
				}
				break ;

				case ScaleOrientations.Horizontal:
				{
					DrawHorizontalThresholdMark (g, pos, Position, brush, pen) ;
				}
				break ;
			}

			brush.Dispose() ;
			pen.Dispose() ;
		}

		private void DrawVerticalThresholdMark (Graphics g, double Fraction, double Position, Brush brush, Pen pen)
		{
			int			size			= (int)(markSize * (xScale + yScale) / 2) ;
			int			half_size		= (int)(size / 2) ;
		            
			if (style == ThresholdMarkStyles.Triangle)
			{
				Point []		pts		= new Point[3] ;

				pts[0].Y = ScaleY(Fraction) ;
				pts[1].Y = ScaleY(Fraction) + half_size ;  
				pts[2].Y = ScaleY(Fraction) - half_size ;  

				if (markPosition == ScaleMarkPositions.Outside)
				{
					pts[0].X = ScaleX(Position) + 1 ;
					pts[1].X = pts[2].X = ScaleX(Position) + size + 1 ; 
				}
				else if (markPosition == ScaleMarkPositions.Inside)
				{
					pts[0].X = ScaleX(Position) ;
					pts[1].X = pts[2].X = ScaleX(Position) - size ; 
				}
				else		//on line - point triangle inwards
				{
					pts[0].X = ScaleX(Position) - half_size ;
					pts[1].X = pts[2].X = ScaleX(Position) + half_size ; 
				}

				g.FillPolygon(brush, pts) ;
			}
			else if (style == ThresholdMarkStyles.Line)
			{
				int		start_x	;
				int		finish_x ;
		                
				if (markPosition == ScaleMarkPositions.Outside)
				{
					start_x	 = ScaleX(Position) + size + 1 ;
					finish_x = ScaleX(Position) + 1 ; 
				}
				else if (markPosition == ScaleMarkPositions.Inside)
				{
					finish_x = ScaleX(Position) ;
					start_x  = ScaleX(Position) - size ; 
				}
				else		//on line
				{
					start_x	 = ScaleX(Position) + half_size ;
					finish_x = ScaleX(Position) - half_size ; 
				}

				g.DrawLine(pen, start_x, ScaleY(Fraction), finish_x, ScaleY(Fraction)) ;
			}
			else if (style == ThresholdMarkStyles.Circle)
			{
				Point	pt		= new Point(ScaleX(Position), ScaleY(Fraction) - half_size) ;
				
				if (markPosition == ScaleMarkPositions.Inside)
				{
					pt.X -= size ;
				}
				else if (markPosition == ScaleMarkPositions.On)
				{
					pt.X -= half_size ;
				}

				g.FillEllipse(brush, pt.X, pt.Y, size, size) ; 
			}
		}

		private void DrawHorizontalThresholdMark (Graphics g, double Fraction, double Position, Brush brush, Pen pen)
		{
			int			size			= (int)(markSize * (xScale + yScale) / 2) ;
			int			half_size		= (int)(size / 2) ;
		            
			if (style == ThresholdMarkStyles.Triangle)
			{
				Point []		pts		= new Point[3] ;

				pts[0].X = ScaleX(Fraction) ;
				pts[1].X = ScaleX(Fraction) + half_size ;  
				pts[2].X = ScaleX(Fraction) - half_size ;  

				if (markPosition == ScaleMarkPositions.Outside)
				{
					pts[0].Y = ScaleY(Position) ;
					pts[1].Y = pts[2].Y = ScaleY(Position) + size ; 
				}
				else if (markPosition == ScaleMarkPositions.Inside)
				{
					pts[0].Y = ScaleY(Position) ;
					pts[1].Y = pts[2].Y = ScaleY(Position) - size ; 
				}
				else		//on line - point triangle inwards
				{
					pts[0].Y = ScaleY(Position) - half_size ;
					pts[1].Y = pts[2].Y = ScaleY(Position) + half_size ; 
				}

				g.FillPolygon(brush, pts) ;
			}
			else if (style == ThresholdMarkStyles.Line)
			{
				int		start_y	;
				int		finish_y ;
		                
				if (markPosition == ScaleMarkPositions.Outside)
				{
					start_y	 = ScaleY(Position) + size + 1 ;
					finish_y = ScaleY(Position) + 1 ; 
				}
				else if (markPosition == ScaleMarkPositions.Inside)
				{
					finish_y = ScaleY(Position) ;
					start_y  = ScaleY(Position) - size ; 
				}
				else		//on line
				{
					start_y	 = ScaleY(Position) + half_size ;
					finish_y = ScaleY(Position) - half_size ; 
				}

				g.DrawLine(pen, ScaleX(Fraction), start_y, ScaleX(Fraction), finish_y) ;
			}
			else if (style == ThresholdMarkStyles.Circle)
			{
				Point	pt		= new Point(ScaleX(Fraction) - half_size, ScaleY(Position)) ;
				
				if (markPosition == ScaleMarkPositions.Inside)
				{
					pt.Y -= size ;
				}
				else if (markPosition == ScaleMarkPositions.On)
				{
					pt.Y -= half_size ;
				}

				g.FillEllipse(brush, pt.X, pt.Y, size, size) ; 
			}
		}

		private void CalcPoint (float length, float angle, out System.Drawing.PointF point)
		{
			float sin_theta = (float)Math.Sin ((angle+180) * Math.PI / 180) ;
			float cos_theta = (float)Math.Cos ((angle + 180)* Math.PI / 180) ;
			float cos_theta2 = cos_theta * cos_theta ;
			float sin_theta2 = sin_theta * sin_theta ;

			float a = xScale * length * 0.5F ;
			float b = yScale * length * 0.5F ;
			float a2 = a*a ;
			float b2 = b*b ;
			float rd = (float)Math.Sqrt((b2*a2) / (b2 * sin_theta2 + a2 * cos_theta2));
			point = new PointF((float)(xOffset + xScale * 0.5 + rd * sin_theta), (float)(yOffset + yScale * 0.5 + rd * cos_theta)) ;
		}

		private void DrawThresholdMark (Graphics g, double Angle, double Ratio, Brush brush, Pen pen)
		{
			double		width		= markSize * Ratio ;
			double		half_width	= (markSize / 2) * Ratio ;
			double		rad_angle	= Angle * Math.PI / 180 ;

			if (style == ThresholdMarkStyles.Triangle)
			{
				PointF []		pts		= new PointF[3] ;
				float r = (float)(width * (xScale+yScale) * 0.25) ;
				System.Drawing.Drawing2D.Matrix old_transform = g.Transform ;

				if (markPosition != ScaleMarkPositions.On)
				{
					CalcPoint (1.0F, (float)Angle, out pts[0]) ;

					g.TranslateTransform(pts[0].X, pts[0].Y);

					pts[0].X = 0 ;
					pts[0].Y = 0 ;

					if (markPosition == ScaleMarkPositions.Outside)
					{
						pts[1].X = + r ;
						pts[1].Y = + r ;
						pts[2].X = - r ;
						pts[2].Y = + r ;
					}
					else	//inside
					{
						pts[1].X = + r ;
						pts[1].Y = - r ;
						pts[2].X = - r ;
						pts[2].Y = - r ;
					}
				}
				else	// on line - point triangle inwards
				{
					CalcPoint (1.0F - (float)width / 2, (float)Angle, out pts[0]) ;

					g.TranslateTransform(pts[0].X, pts[0].Y);

					pts[0].X = 0 ;
					pts[0].Y = 0 ;

					pts[1].X = + r ;
					pts[1].Y = + r ;
					pts[2].X = - r ;
					pts[2].Y = + r ;
				}

				g.RotateTransform (180F-(float)Angle) ;
				g.FillPolygon(brush, pts) ;
				g.Transform = old_transform ;
			}
			else if (style == ThresholdMarkStyles.Line)
			{
				System.Drawing.Drawing2D.Matrix old_transform = g.Transform ;
				float r = (float)(width * (xScale+yScale) * 0.25) ;

				PointF	start ;
				PointF	finish		= new PointF() ;
                
				finish.X= 0 ;
				if (markPosition == ScaleMarkPositions.Outside)
				{
					CalcPoint (1.0F, (float)Angle, out start) ;
					finish.Y= r ;
				}
				else if (markPosition == ScaleMarkPositions.Inside)
				{
					CalcPoint (1.0F, (float)Angle, out start) ;
					finish.Y= -r ;
				}
				else		//on line
				{
					CalcPoint (1.0F - (float)width / 2, (float)Angle, out start) ;
					finish.Y= r ;
				}


				g.TranslateTransform(start.X, start.Y);
				start.X = 0 ;
				start.Y = 0 ;

				g.RotateTransform (180F-(float)Angle) ;

				g.DrawLine(pen, start, finish) ;
				g.Transform = old_transform ;
			}
			else if (style == ThresholdMarkStyles.Circle)
			{
				PointF	centre_point ;						//.. of circle

				System.Drawing.Drawing2D.Matrix old_transform = g.Transform ;

				CalcPoint (1.0F, (float)Angle, out centre_point) ;
		
				float r = (float)(width * (xScale+yScale) * 0.25) ;

				g.TranslateTransform(centre_point.X, centre_point.Y);

				if (markPosition == ScaleMarkPositions.Outside)
				{
					centre_point.Y	= r * 1.1F ;
				}
				else if (markPosition == ScaleMarkPositions.Inside)
				{
					centre_point.Y	= -r * 1.1F ;
				}
				else
				{
					centre_point.Y	= 0 ;
				}

				g.RotateTransform (180F-(float)Angle) ;
				g.FillEllipse(brush,  -r, centre_point.Y - r, 2 * r, 2 * r) ;
				g.Transform = old_transform ;
			}
		}
		#endregion
	}
}

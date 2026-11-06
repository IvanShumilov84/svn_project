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
	/// Enumeration of possible cursor End Styles
	/// </summary>
	public enum CursorEndStyles
	{
		/// <summary>
		/// No end caps are drawn
		/// </summary>
		None,
		/// <summary>
		/// End caps are lines perpendicular to the cursor
		/// </summary>
		Line,
		/// <summary>
		/// End caps are triangular
		/// </summary>
		Triangle,
		/// <summary>
		/// End caps are diamonds
		/// </summary>
		Diamond,
	}

	/// <summary>
	/// Collection of Cursor objects designed for use with a Trend
	/// </summary>
	[TypeConverter(typeof(CursorCollection.CursorCollConverter))]
	public class CursorCollection : SubObjectCollection
	{
		#region Type Converter
		internal class CursorCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					CursorCollection	coll		= (CursorCollection) value;
					Cursor []			cursors		= new Cursor [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(CursorCollection).GetConstructor(new Type[]{typeof(Cursor[])});

						//copy cursors into array
						for (int index = 0; index < coll.Count; index++)
						{
							cursors[index] = coll[index] ;
						}

					return new InstanceDescriptor(ctor_info, new object[]{cursors}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the CursorCollection class
		/// </summary>
		public CursorCollection()
		{
		}

		/// <summary>
		/// Initialises a new instance of the CursorCollection class containing the specified Cursor objects
		/// </summary>
		/// <param name="cursors">Array of Cursor objects to add to collection</param>
		public CursorCollection(Cursor [] cursors)
		{
			AddRange(cursors) ;
		}
		#endregion

		#region Properties	
		/// <summary>
		/// Gets or sets the Cursor object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Cursor this[int Index]
		{
			get
			{
				return (Cursor) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					//((Cursor)List[Index]).PropertyChanged -= hdlr ;	//?? needed?
					List[Index] = value ;
					//((Cursor)List[Index]).PropertyChanged += hdlr ;	//?? needed?
				}
			}
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new Cursor object of the specified color
		/// </summary>
		/// <param name="LineColor">Color of the new Cursor line</param>
		public void AddCursor(Color LineColor)
		{
			this.List.Add(new Cursor(LineColor)) ;
		}

		/// <summary>
		/// Adds an array of Cursor objects to the collection
		/// </summary>
		/// <param name="newCursors">Array of Cursors to add</param>
		public void AddRange(Cursor [] newCursors)
		{
			for (int index = 0; index < newCursors.Length; index++)
			{
				this.List.Add(newCursors[index]) ;
			}
		}

		/// <summary>
		/// Determines if any Cursors are visible
		/// </summary>
		/// <returns>True if at least one Cursor is Visible, else false</returns>
		public bool CursorVisible()
		{
			foreach (Cursor cursor in this)
			{
				if (cursor.Visible)		return true ;
			}
			return false ;
		}
		#endregion

		#region Protected internal Methods (drawing etc)
		/// <summary>
		/// Draws all the Cursors in the collection
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="FullRedraw">Whether to do a complete redraw</param>
		/// <param name="ScrollRectRight">Right hand side of area in which to draw</param>
		/// <param name="Traces">TraceCollection object from which linked Traces can be found</param>
		/// <param name="ValueIndex">Index into Trace data array</param>
		/// <param name="BackBrush">Brush for control's background</param>
		protected internal void Draw(Graphics g, bool FullRedraw, int ScrollRectRight, TraceCollection Traces, long ValueIndex, Brush BackBrush)
		{
			foreach (Cursor cursor in this)
			{
				cursor.Draw(g, FullRedraw, ScrollRectRight, Traces, ValueIndex, BackBrush) ;
			}
		}
	
		/// <summary>
		/// Recalculates the positions of the cursors
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="ScrollRect">Rectangle in which it is valid to draw</param>
		/// <param name="TrendRange">Milliseconds displayed by the Trend</param>
		/// <param name="TimePerPixel">Time (ms) for to scroll by one pixel</param>
		/// <returns>Region representing the ScrollRect minus the Cursor lines</returns>
		protected internal Region Recalculate(Graphics g, Rectangle ScrollRect, int TrendRange, int TimePerPixel)
		{
			Region			scroll_rgn		= new Region(ScrollRect) ;
			Region			temp ;

			foreach (Cursor cursor in this)
			{
				temp = cursor.Recalculate(g, ScrollRect, TrendRange, TimePerPixel) ;
				scroll_rgn.Exclude(temp) ;
			}
			return scroll_rgn ;
		}

		/// <summary>
		/// Determines if the given point is in any of the Cursors' regions
		/// </summary>
		/// <param name="X">X position of point to check</param>
		/// <param name="Y">Y position of point to check</param>
		/// <returns>Zero-based index of cursor at given position, or -1 if none</returns>
		protected internal int PointInCursorRect(int X, int Y)
		{
			int			index	= 0 ;

			foreach (Cursor cursor in this)
			{
				if (cursor.PointInCursor(X, Y))
				{
					return index ;
				}
				index++ ;
			}
			return -1 ;
		}

		/// <summary>
		/// Combines the given region with the text region of each Cursor,
		/// to create the region containing all the bits that need updating
		/// every time the Control scrolls
		/// </summary>
		/// <param name="ScrollRegion">Region with which to combine the Text regions</param>
		/// <returns>Combined region</returns>
		protected internal Region CombineTextRegions(Region ScrollRegion)
		{
			Region		rgn		= ScrollRegion ;

			foreach (Cursor cursor in this)
			{
				rgn.Union(cursor.GetTextRect()) ;
				rgn.Union(cursor.GetTextRect2()) ;
			}
			return rgn ;
		}

		/// <summary>
		/// Moves the Cursor at the given index, and recalculates the scroll region
		/// </summary>
		/// <param name="CursorIndex">Zero-based index of Cursor to move</param>
		/// <param name="g">Graphics object used for recalculation</param>
		/// <param name="XPos">X position to move the cursor to</param>
		/// <param name="ScrollRect">Rectangle within which te cursor can be moved</param>
		/// <param name="TimePerPixel">Time (ms) to scroll by one pixel</param>
		/// <param name="TrendRange">Milliseconds displayed by the Trend</param>
		/// <param name="OldCursorRgn">(out) Original cursor region, before it was moved</param>
		/// <returns>New ScrollRegion, which excludes all the Cursor Lines</returns>
		protected internal Region MoveCursor(int CursorIndex, Graphics g, int XPos, Rectangle ScrollRect, int TimePerPixel, int TrendRange, out Region OldCursorRgn)
		{
			Region		scroll_rgn		= new Region(ScrollRect) ;

			if (CursorIndex >= 0 && CursorIndex < this.Count)
			{
				OldCursorRgn = ((Cursor)List[CursorIndex]).Move(g, XPos, ScrollRect, TimePerPixel, TrendRange) ;
			}
			else
			{
				OldCursorRgn = new Region(Rectangle.Empty) ;
			}

			foreach (Cursor cursor in this)
			{
				scroll_rgn.Exclude(cursor.GetLineRegion()) ;
			}

			return scroll_rgn ;
		}
		#endregion
	}
	

	/// <summary>
	/// Cursor object designed for use on a Trend. 
	/// It tracks the value of a Trace at a defined point in time.
	/// </summary>
	[TypeConverterAttribute (typeof(ObjectTypeConverter))]
	[DefaultProperty("TraceIndex")]
	public class Cursor : SubObjectBase
	{
		#region Variables and Declarations
		static Font					numberFont	= new Font("Courier New", 8) ;

		//property variables
		private bool						visible ;
		private Color						cursorColor ;
		private int							width ;
		private int							traceIndex ;
		private double						initialPosition ;
		private CursorEndStyles				endStyle ;
		private bool						numberVisible ;
		private bool						moveable ;

		//internal variables
		private Rectangle					lineRect ;
		private Rectangle					topRect ;
		private Rectangle					bottomRect ;
		private Rectangle					textRect ;
		private Rectangle					textRect2 ;
		private int							position ;
		private int							xPosition ;
		private int							timePerPixel ;
		private Rectangle					currentTextRect ;
		private Rectangle					currentTextRect2 ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Cursor class
		/// </summary>
		public Cursor()
		{
			Initialise(Color.Black) ;
		}
		/// <summary>
		/// Initialises a new instance of the Cursor class with the specified Color
		/// </summary>
		/// <param name="clr">Cursor Color</param>
		public Cursor(Color clr)
		{
			Initialise(clr) ;
		}

		private void Initialise(Color Clr)
		{
			visible = true ;
			cursorColor = Clr ;
			width = 1 ;
			initialPosition = 1.0 ;
			endStyle = CursorEndStyles.Diamond ;
			numberVisible = true ;
			moveable = true ;
			traceIndex = 0 ;

			lineRect = Rectangle.Empty ;
			topRect = Rectangle.Empty ;
			bottomRect = Rectangle.Empty ;
			textRect = Rectangle.Empty ;
			textRect2 = Rectangle.Empty ;
			currentTextRect = Rectangle.Empty ;
			currentTextRect2 = Rectangle.Empty ;
			position = (int)(initialPosition * 1000) ;
			xPosition = 0 ;
		}

		#endregion

		#region Properties
		///<summary>
		///Gets or sets the color of the Cursor
		///</summary>
		[Category("Cursor")]
		[Description("Gets or sets the color of the Cursor")]
		public Color CursorColor
		{
			get
			{
				return cursorColor ;
			}
			set
			{
				if (cursorColor != value)
				{
					cursorColor = value ;
					OnPropertyChanged("CursorColor") ;
				}
			}
		}
		private bool ShouldSerializeCursorColor()
		{
			return cursorColor != Color.Black ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetCursorColor()
		{
			cursorColor = Color.Black ;
		}

		///<summary>
		///Gets or sets whether the Cursor is Visible
		///</summary>
		[Category("Cursor")]
		[Description("Gets or sets whether the Cursor is Visible")]
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

		///<summary>
		///Gets or sets the initial Position of the cursor in seconds
		///</summary>
		///<remarks>
		///Position is defined as 'number of seconds ago'.
		///'0' is the current time, and hence is at the right hand side of the Trend.
		///As the value increases the cursor is placed further to the left.
		///</remarks>
		[Category("Cursor")]
		[DefaultValue(1.0)]
		[Description("Gets or sets the initial Position of the cursor in seconds")]
		public double InitialPosition
		{
			get
			{
				return initialPosition ;
			}
			set
			{
				if (initialPosition != value)
				{
					initialPosition = value ;
					position = (int)(initialPosition * 1000) ;
					OnPropertyChanged("InitialPosition") ;
				}
			}
		}

		///<summary>
		///Gets or sets the style of the ends of the Cursor
		///</summary>
		[Category("Cursor")]
		[DefaultValue(CursorEndStyles.Diamond)]
		[Description("Gets or sets the style of the ends of the Cursor")]
		public CursorEndStyles EndStyle
		{
			get
			{
				return endStyle ;
			}
			set
			{
				if (endStyle != value)
				{
					endStyle = value ;
					OnPropertyChanged("EndStyle") ;
				}
			}
		}

		///<summary>
		///Gets or sets whether the numeric value of the associated Trace is displayed
		///</summary>
		///<remarks>The value, if displayed, will be the value that the Trace was at the point in time that the Cursor is marking.</remarks>
		[Category("Cursor")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the numeric value of the associated Trace is displayed")]
		public bool NumberVisible
		{
			get
			{
				return numberVisible ;
			}
			set
			{
				if (NumberVisible != value)
				{
					numberVisible = value ;
					OnPropertyChanged("NumberVisible") ;
				}
			}
		}

		///<summary>
		///Gets or sets whether the Cursor can be dragged to a new position.
		///</summary>
		[Category("Cursor")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Cursor can be dragged to a new position.")]
		public bool Moveable
		{
			get
			{
				return moveable ;
			}
			set
			{
				if (moveable != value)
				{
					moveable = value ;
					OnPropertyChanged("Moveable") ;
				}
			}
		}

		///<summary>
		///Gets or sets the zero-based index of the Trace which this cursor tracks.
		///</summary>
		///<remarks>Specify -1 to not track any Trace</remarks>
		[Category("Cursor")]
		[DefaultValue(0)]
		[Description("Gets or sets the zero-based index of the Trace which this cursor tracks (or -1 if none)")]
		public int TraceIndex
		{
			get
			{
				return traceIndex ;
			}
			set
			{
				if (traceIndex != value)
				{
					traceIndex = value ;
					OnPropertyChanged("TraceIndex") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of the Cursor in pixels
		/// </summary>
		[Category("Cursor")]
		[DefaultValue(1)]
		[Description("Gets or sets the width of the Cursor in pixels")]
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
		#endregion

		#region Drawing
		/// <summary>
		/// Draws the Cursor
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="FullRedraw">Whether to do a complete redraw</param>
		/// <param name="ScrollRectRight">Right hand side of area in which to draw</param>
		/// <param name="Traces">TraceCollection object from which the linked Trace can be found</param>
		/// <param name="ValueIndex">Index into Trace data array</param>
		/// <param name="BackBrush">Brush for control's background</param>
		protected internal void Draw(Graphics g, bool FullRedraw, int ScrollRectRight, TraceCollection Traces, long ValueIndex, Brush BackBrush)
		{
			if (!visible)		return ;

			if (FullRedraw)
			{
				SolidBrush			brush = new SolidBrush(cursorColor) ;

				g.FillRectangle(brush, lineRect) ;

				// Draw the ends.
				switch(endStyle)
				{
					case CursorEndStyles.Line:
					{
						Rectangle		rect	= topRect ;

						rect.Y = rect.Bottom - width ;
						rect.Height = width ;
						g.FillRectangle(brush, rect) ;

						rect.Y = bottomRect.Y ;
						g.FillRectangle(brush, rect) ;
					}
					break;

					case CursorEndStyles.Triangle:
					{
						Point []		point		= new Point[3] ;

						point[0] = new Point(topRect.Left, topRect.Bottom - 6) ;
						point[1] = new Point(xPosition, topRect.Bottom) ;
						point[2] = new Point(topRect.Right, topRect.Bottom - 6) ;
						g.FillPolygon(brush, point) ;

						point[0].Y = point[2].Y = bottomRect.Top + 6 ;
						point[1].Y = bottomRect.Top - 1 ;
						g.FillPolygon(brush, point) ;
					}
					break;

					case CursorEndStyles.Diamond:
					{
						Point []		point		= new Point[4] ;

						point[0] = new Point(xPosition, topRect.Top) ;
						point[1] = new Point(topRect.Right, topRect.Bottom - 6) ;
						point[2] = new Point(xPosition, topRect.Bottom) ;
						point[3] = new Point(topRect.Left, topRect.Bottom - 6) ;
						g.FillPolygon(brush, point) ;

						point[0].Y = bottomRect.Top - 1 ;
						point[1].Y = point[3].Y = bottomRect.Bottom - 6 - 1 ;
						point[2].Y = bottomRect.Bottom - 1 ;
						g.FillPolygon(brush, point) ;
					}
					break;
				}

				brush.Dispose() ;
			}

			if (numberVisible)
			{
				DrawText(g, Traces, ScrollRectRight, ValueIndex, BackBrush);
			}
		}
		
		private void DrawText(Graphics g, TraceCollection Traces, int ScrollRectRight, long ValueIndex, Brush BackBrush)
		{
			long			offset ;
			string			val ;
			string			val2 ;
			StringFormat	format		= new StringFormat() ;
			Brush			brush		= new SolidBrush(cursorColor) ;

			if (traceIndex < 0 || traceIndex >= Traces.Count)
			{
				//not looking at a (valid) trace
				return ;
			}

			offset = ValueIndex - (ScrollRectRight - xPosition) ;
			if (offset < 0)
			{
				offset += Trend.MAX_DATA_VALUES ;
			}

			val = Traces[traceIndex].GetPastValueString(offset)  ;
			//			format_padded_double (value, text, int_places.iVal, dec_places.iVal, padding_zero) ;

			//val2 = (timePerPixel * (ScrollRectRight - xPosition)).ToString("g4") ;

			TimeSpan time = new TimeSpan(0,0,0,0,position) ;
			//val2 = time.ToString() ;
			if (position < 1000 * 60)
			{
				val2 = time.Seconds.ToString() + "." +  time.Milliseconds.ToString("000") ;
			}
			else if (position < 1000 * 60 * 60)
			{
				val2 = time.Minutes.ToString() + ":" +  time.Seconds.ToString("00") ;
			}
			else if (position < 1000 * 60 * 60 * 24)
			{
				val2 = time.Hours.ToString() + ":" + time.Minutes.ToString("00") + ":" +  time.Seconds.ToString("00") ;
			}
			else 
			{
				val2 = time.Days.ToString() + "." + time.Hours.ToString("00") + ":" + time.Minutes.ToString("00") ;
			}
			
			SizeF val2_size = g.MeasureString(val2, numberFont) ;
			SizeF val_size = g.MeasureString(val, numberFont) ;
			currentTextRect = textRect ;
			currentTextRect2 = textRect2 ;
			currentTextRect.Width = (int)val2_size.Width + 1 ;
			currentTextRect2.Width = (int)val_size.Width + 1 ;

			if (currentTextRect.Left < bottomRect.Left)
			{
				format.Alignment = StringAlignment.Far ;
				currentTextRect.X += textRect.Width - currentTextRect.Width ;
				currentTextRect2.X += textRect2.Width - currentTextRect2.Width ;
			}
			else
			{
				format.Alignment = StringAlignment.Near ;
			}
			format.LineAlignment = StringAlignment.Center ;

			g.FillRectangle(BackBrush, currentTextRect) ;
			g.FillRectangle(BackBrush, currentTextRect2) ;
			g.DrawString(val2, numberFont, brush, currentTextRect, format) ;
			g.DrawString(val, numberFont, brush, currentTextRect2, format) ;
			brush.Dispose() ;
		}

		#endregion

		#region Other Methods
		/// <summary>
		/// Recalculates the positions of the cursor
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		/// <param name="ScrollRect">Rectangle in which it is valid to draw</param>
		/// <param name="TrendRange">Milliseconds displayed by the Trend</param>
		/// <param name="TimePerPixel">Time (ms) to scroll by one pixel</param>
		/// <returns>Region covered by the Cursor line</returns>
		protected internal Region Recalculate(Graphics g, Rectangle ScrollRect, int TrendRange, int TimePerPixel)
		{
			this.timePerPixel = TimePerPixel ;

			if (visible)
			{
				if (position > TrendRange)
				{
					position = 0 ;
				}
				else if (position < (((width / 2) + 1) * TimePerPixel))
				{
					position = ((width / 2) + 1) * TimePerPixel ;
				}

				xPosition		= ScrollRect.Right - (position / TimePerPixel) ;
				lineRect.X		= xPosition - (width / 2);
				lineRect.Width	= width ;
				lineRect.Y		= ScrollRect.Y ;
				lineRect.Height	= ScrollRect.Height ;

				topRect.X		= xPosition - 6 ;
				topRect.Width	= 12 ;
				topRect.Y		= ScrollRect.Y - 13 ;
				topRect.Height	= 13 ;

				bottomRect		= topRect ;
				bottomRect.Y	= ScrollRect.Bottom ;

				if (numberVisible)
				{
					// Calculate maximum pixels for number.
					SizeF	size ;
					string	temp_text ;
					int		number_length ;
					int		max_width ;

					//					number_length  = int_places.iVal + dec_places.iVal ;
					//					number_length += (dec_places.iVal == 0) ? 0 : 1 ;		// Decimal point ;
					//					number_length ++ ;										// Minus sign.
					//					if (number_length < 8)
					//					{
					//						number_length = 8 ;				//chars in '-1.#QNAN' - the error value
					//					}

					//assume for now it is 10 chars (copes with 4 SFs up to '-1.234e-56')
					number_length = 8 ;
					temp_text = new string('0', number_length) ;

					size = g.MeasureString(temp_text, numberFont) ;

					max_width = (int)size.Width ;

					textRect.X = bottomRect.X - max_width - 4 ;

					if (textRect.X <= ScrollRect.X)
					{
						textRect.X = bottomRect.Right + 2 ;
					}
					textRect.Width = max_width ;
					textRect.Height = 12 ;
					textRect.Y = ScrollRect.Bottom ;


					textRect2 = textRect ;
					textRect2.Y = ScrollRect.Top - textRect2.Height ;
				
				}
				else
				{
					textRect = Rectangle.Empty ;
					textRect2 = Rectangle.Empty ;
				}
			}
			else
			{
				lineRect = Rectangle.Empty ;
				textRect = Rectangle.Empty ;
				textRect2 = Rectangle.Empty ;
			}

			return GetLineRegion() ;
		}

		/// <summary>
		/// Returns the region covered by the Cursor Line
		/// </summary>
		/// <returns>Region covered by the Cursor Line</returns>
		protected internal Region GetLineRegion()
		{
			return new Region(lineRect) ;
		}

		/// <summary>
		/// Determines if the given point is on or near the Cursor's line
		/// </summary>
		/// <param name="X">X position of point to check</param>
		/// <param name="Y">Y position of point to check</param>
		/// <returns>True if point is on the Cursor</returns>
		protected internal bool PointInCursor(int X, int Y)
		{
			if (visible && moveable)
			{        
				Rectangle			line_rect	= lineRect ;

				line_rect.Inflate(6, 6);
                    
				if (line_rect.Contains(X, Y) || topRect.Contains(X, Y)  ||
					bottomRect.Contains(X, Y) || currentTextRect.Contains(X, Y) || currentTextRect2.Contains(X, Y)	)
				{
					return true ;
				}
			}
			return false ;
		}

		/// <summary>
		/// Moves the cursor to the given position
		/// </summary>
		/// <param name="g">Graphics object used for recalculation</param>
		/// <param name="XPos">X position to move the cursor to</param>
		/// <param name="ScrollRect">Rectangle within which te cursor can be moved</param>
		/// <param name="TimePerPixel">Time (ms) to scroll by one pixel</param>
		/// <param name="TrendRange">Milliseconds displayed by the Trend</param>
		/// <returns>Original cursor region, before it was moved</returns>
		protected internal Region Move(Graphics g, int XPos, Rectangle ScrollRect, int TimePerPixel, int TrendRange)
		{
			Region			cursor_rgn ;

			if (XPos < ScrollRect.Left + width / 2)
			{
				XPos = ScrollRect.Left + width / 2 ;
			}
			
			if (XPos >= ScrollRect.Right - width / 2)
			{
				XPos = ScrollRect.Right - 1 - width / 2 ;
			}

			position = (ScrollRect.Right - XPos) * TimePerPixel ;

			cursor_rgn = new Region(lineRect) ;
			cursor_rgn.Union(topRect) ;
			cursor_rgn.Union(bottomRect) ;
			cursor_rgn.Union(currentTextRect) ;
			cursor_rgn.Union(currentTextRect2) ;

			//recalculate this cursor
			Recalculate(g, ScrollRect, TrendRange, TimePerPixel) ;

			return cursor_rgn ;
		}

		/// <summary>
		/// Returns the Rectangle in which the Cursor text is drawn
		/// </summary>
		/// <returns>Rectangle in which the Cursor text is drawn</returns>
		protected internal Rectangle GetTextRect()
		{
			if (visible && numberVisible)
			{
				return currentTextRect ;
			}
			return Rectangle.Empty ;
		}

		/// <summary>
		/// Returns the Rectangle in which the Cursor text is drawn
		/// </summary>
		/// <returns>Rectangle in which the Cursor text is drawn</returns>
		protected internal Rectangle GetTextRect2()
		{
			if (visible && numberVisible)
			{
				return currentTextRect2 ;
			}
			return Rectangle.Empty ;
		}

		#endregion
	}
}
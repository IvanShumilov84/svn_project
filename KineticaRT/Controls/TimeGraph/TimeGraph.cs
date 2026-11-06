using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using KineticaRT;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// The KineticaRT TimeGraph control displays recorded data over a range of time
	/// </summary>
	[DefaultProperty("Traces")]
#if KRT_license
	[LicenseProviderAttribute(typeof(TimeGraph.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(TimeGraph))]
	public class TimeGraph : KineticaRT.Controls.Control
	{
		#region CLASS: Band (zooming)
		private class Band
		{
			public	bool		enabled;
			public	bool		displayed;
			public	bool		draw;
			public	Point		startPoint ;	//client coords
			public	Point		screenStart ;	//start point in screen coords
			public	Point		currentEnd;		// Displayed position.
			public	Point		nextEnd;		// where to be drawn next
			
			public Band()
			{
				enabled			= false ;
				displayed		= false ;
				draw			= false ;
				startPoint		= Point.Empty ;
				screenStart		= Point.Empty ;
				currentEnd		= Point.Empty ;
				nextEnd			= Point.Empty ;
			}

			public bool Setup(Point ScreenPoint)
			{
				if (enabled)                               
				{
					nextEnd.Y = screenStart.Y ;
					nextEnd.X = ScreenPoint.X ;

					draw = true ;
				}
				else
				{
					draw = false;
				}

//				return (draw || displayed) ;
				return (draw || displayed) && (nextEnd.X != currentEnd.X) ;
			}

			public void Draw(Rectangle ScreenRect, Color BackColor, bool Draw, bool Update)
			{
				lock(this)
				{
					if (!Draw)
					{
						if (displayed)
						{
							ControlPaint.DrawReversibleLine(screenStart, currentEnd, BackColor) ;
							displayed = false;
						}	
					}
					else if (draw)
					{
						if (Update)
						{   
							ControlPaint.DrawReversibleLine(screenStart, nextEnd, BackColor) ;
							currentEnd = nextEnd;
						}
						else
						{
							ControlPaint.DrawReversibleLine(screenStart, currentEnd, BackColor) ;
						}

						displayed = true;
					}
				}
			}
		} 
		#endregion

		#region License
#if KRT_license
		private License license = null;
		private	bool				demoMode ;
		// Do not move this class or its contents into another namespace - it would not be secure.
	
		////[LicenseProviderAttribute(typeof(xxx.LicenseProvider))]
		//private License license = null;
		////demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
		//if (license != null) {license.Dispose();license = null;}
		//This block: update key using GUIDGEN, and typeof(xxx)
		/// <summary>
		/// Implements a license provider for the class.
		/// </summary>
		public class LicenseProvider: System.ComponentModel.LicenseProvider
		{
			/// <summary>
			/// Gets a license for an instance or type of component, when given a context and whether the denial of a license throws an exception
			/// </summary>
			/// <param name="context">A LicenseContext that specifies where you can use the licensed object</param>
			/// <param name="type">A Type that represents the component requesting the license</param>
			/// <param name="instance">An object that is requesting the license</param>
			/// <param name="allowExceptions">true if a LicenseException should be thrown when the component cannot be granted a license; otherwise, false</param>
			/// <returns>A valid License</returns>
			public override System.ComponentModel.License GetLicense(LicenseContext context,Type type,object instance,bool allowExceptions)
			{
				License licence = null ;

				if (context != null)
				{
					string key = null ;

					if (context.UsageMode == LicenseUsageMode.Designtime)
					{
						Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
						if (registry != null) key = registry.GetValue ("RegKey") as string ;
						if ((key == null) || (key != "35A00F46A2ED"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "35A00F46A2ED")) key = "{35A00F46-A2ED-4921-81F9-3159226A3FDA}" ; 
						context.SetSavedLicenseKey (typeof(TimeGraph), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(TimeGraph), null) ;
					if (key == "{35A00F46-A2ED-4921-81F9-3159226A3FDA}") licence = new License ("Run") ;
					else if (allowExceptions && (context.UsageMode != LicenseUsageMode.Designtime)) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
					else licence = new License ("None") ;
				}

				return licence ;
			}

			private class License:System.ComponentModel.License 
			{
				string key ;

				public License (string key)
				{
					this.key = key ;
				}

				public override string LicenseKey 
				{
					get
					{
						return key ;
					}
				}
		
				public override void Dispose()
				{
				}
			}
		}
#endif
		#endregion

		#region Variables and Declarations
		private const double		VERY_SMALL_DOUBLE		= 0.0000000001 ;
		private const int			BORDER_WIDTH			= 5 ;
		private	const int			TIMELINE_HEIGHT			= 12 ;		//+ Font.Height
		private const double		SCROLL_PROPORTION		= 0.3 ;

		private enum Step
		{
			Seconds		= 0,
			Minutes		= 1,
			Hours		= 2,
			Days		= 3,
			Months		= 4,
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//internal variables
		static private EventPool	updatePool			= new EventPool(10) ;

		private bool				recalculateRequired ;
		private Rectangle			paperRect ;
		private	Rectangle			timeRect ;
		private int					leftScaleWidth ;
		private int					rightScaleWidth ;
		private StringFormat		labelFormat ;
		private double				pixelsPerHour ;
		private Step				step ;
		private double				pixelsPerMark ;
		private	Timer				zoomTimer ;
		private	Timer				scrollTimer ;
		private Band				band ;
		private	bool				zoomingIn ;			//}true if in process of zoom
		private	bool				zoomingOut ;		//}whether actually started or not
		private	bool				zoomed ;			//true if performed a zoom step
		private	bool				scrollingLeft ;
		private	bool				scrollingRight ;
		private	int					scrollCount ;
		private DateTime			nextUpdateTime ;
		private	TimeSpan			updateInterval ;
		private bool				recalculateUpdate ;
		private	Bitmap				bmpBackground ;
		
		private enum LockedState
		{
			off,
			waiting,
			on,
		}
		private LockedState	locked = LockedState.on ;
		
		private PropertyChangedEventHandler		propertyHdlr ;
		private ObjectSearchEventHandler		searchHdlr ;
		private EventPool.UpdateEventHandler	updateHdlr ;
		
		//property variables
		private bool						scaleWidthIsPercent ;
		private int							scaleWidth ;
		private KineticaRT.Controls.Scale	leftScale ;
		private KineticaRT.Controls.Scale	rightScale ;
		private	Color						gridColor ;
		private DateTime					viewStart ;
		private DateTime					viewEnd ;
		private bool						gridEnabled ;
		private TimeTraceCollection			traces ;
		private bool						freeze ;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the TimeGraph class
		/// </summary>
		public TimeGraph()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#endif
			
			scaleWidthIsPercent	= false ;
			scaleWidth			= 30 ;
			gridColor			= ForeColor ;
			gridEnabled			= true ;
			freeze				= false ;

			//initialise view to 2 hours centered around now
			viewEnd		= DateTime.Now.AddHours(1) ;
			viewStart	= viewEnd.AddHours(-2) ;

			recalculateRequired = true ;
			recalculateUpdate	= true ;

			leftScaleWidth		= 0 ;
			rightScaleWidth 	= 0 ;
			pixelsPerHour		= 0 ;
			paperRect			= Rectangle.Empty ;
			timeRect			= Rectangle.Empty ;
			step				= Step.Hours ;
			band				= new Band() ;
			zoomingIn			= false ;
			zoomingOut			= false ;
			zoomed				= false ;
			scrollingLeft		= false ;
			scrollingRight		= false ;
			scrollCount			= 0 ;
			bmpBackground		= null ;

			labelFormat			= new StringFormat() ;
			labelFormat.Alignment		= StringAlignment.Center ;
			labelFormat.LineAlignment	= StringAlignment.Near ;

			leftScale	= new KineticaRT.Controls.Scale(1) ;
			rightScale	= new KineticaRT.Controls.Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;
			traces		= new TimeTraceCollection(true) ;

			//connect to events
			propertyHdlr = new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch) ;
			updateHdlr = new EventPool.UpdateEventHandler(OnUpdate);

			nextUpdateTime = DateTime.Now ;
			updateInterval = TimeSpan.Zero ;

			leftScale.PropertyChanged	+= propertyHdlr ;
			rightScale.PropertyChanged	+= propertyHdlr ;
			traces.PropertyChanged		+= propertyHdlr ;
			leftScale.ObjectSearch		+= searchHdlr ;
			rightScale.ObjectSearch		+= searchHdlr ;
			traces.ObjectSearch			+= searchHdlr ;
		
			zoomTimer			= new Timer() ;
			zoomTimer.Interval	= 300 ;
			zoomTimer.Tick		+= new EventHandler(OnZoomTimer) ;
			scrollTimer			= new Timer() ;
			scrollTimer.Interval= 300 ;
			scrollTimer.Tick	+= new EventHandler(OnScrollTimer) ;

			updatePool.UpdateEvent += updateHdlr ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
#if KRT_license
			if (license != null) {license.Dispose();license = null;}
#endif
			if( disposing )
			{
				updatePool.UpdateEvent -= updateHdlr ;

				if (bmpBackground != null)
				{
					bmpBackground.Dispose() ;
				}

				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// TimeGraph
			// 
			this.Name = "TimeGraph";
			this.Size = new System.Drawing.Size(300, 150);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets if the control should track current time if current time is visible.
		/// </summary>
		[Category("TimeGraph")]
		[DefaultValue(true)]
		[Description("Gets or sets if the control should track current time if current time is visible.")]
		public bool AllowAutoScroll
		{
			get
			{
				return allowAutoScroll ;
			}

			set 
			{
				allowAutoScroll = value ;
				OnPropertyChanged("AllowAutoScroll") ;
			}
		}
		private bool allowAutoScroll = true ;

		/// <summary>
		/// Gets or Sets the style of the Control's Border
		/// </summary>
		[Category("Control")]
		[DefaultValue(BorderStyles.Raised)]
		[Description("Gets or Sets the style of the Control's Border")]
		public new BorderStyles BorderStyle
		{
			set
			{
				base.BorderStyle = value ;
				recalculateRequired = true ;
			}
		}

		/// <summary>
		/// Gets or Sets the width of the Control's Border
		/// </summary>
		/// <remarks>
		/// Values between 0 and 0.5 represent a proportion of the width of the Control.
		/// Values greater than or equal to 1 represent the number of pixels
		/// </remarks>
		[Category("Control")]
		[DefaultValue(0.1)]
		[Description("Gets or Sets the width of the Control's Border")]
		public override double BorderWidth
		{
			set
			{
				base.BorderWidth = value ;
				recalculateRequired = true ;
			}
		}

		/// <summary>
		/// Gets or Sets whether the ScaleWidth property is in pixels or a percentage of the total width
		/// </summary>
		[Category("TimeGraph")]
		[DefaultValue(false)]
		[Description("Gets or Sets whether the ScaleWidth property is in pixels or a percentage of the total width")]
		public bool ScaleWidthIsPercent
		{
			get
			{
				return scaleWidthIsPercent ;
			}
			set
			{
				if (scaleWidthIsPercent != value)
				{
					scaleWidthIsPercent = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("ScaleWidthIsPercent") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the width of the Scales (pixels or percentage depending on ScaleWidthIsPercent)
		/// </summary>
		[Category("TimeGraph")]
		[DefaultValue(30)]
		[Description("Gets or Sets the width of the Scales (pixels or percentage depending on ScaleWidthIsPercent)")]
		public int ScaleWidth
		{
			get
			{
				return scaleWidth ;
			}
			set
			{
				if (scaleWidth != value)
				{
					scaleWidth = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("ScaleWidth") ;
				}
			}
		}

		///<summary>
		///Gets the Left Scale object
		///</summary>
		[Category("TimeGraph")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Left Scale object")]
		public KineticaRT.Controls.Scale LeftScale
		{
			get 
			{
				return leftScale ;
			}
		}
		private bool ShouldSerializeLeftScale()
		{
			return !leftScale.IsDefaultState() ;
		}
		/// <summary>
		/// Reset the LeftScale to its default settings
		/// </summary>
		public void ResetLeftScale()
		{
			leftScale = new KineticaRT.Controls.Scale(1) ;
		}

		///<summary>
		///Gets the Right Scale object
		///</summary>
		[Category("TimeGraph")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Right Scale object")]
		public KineticaRT.Controls.Scale RightScale
		{
			get 
			{
				return rightScale ;
			}
		}
		private bool ShouldSerializeRightScale()
		{
			return !rightScale.IsDefaultState() ;
		}
		/// <summary>
		/// Reset the RightScale to its default settings
		/// </summary>
		public void ResetRightScale()
		{
			rightScale = new KineticaRT.Controls.Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;
		}

		/// <summary>
		/// Gets or Sets the start of the time period on view
		/// </summary>
		[Category("TimeGraph")]
		[Description("Gets or Sets the start of the time period on view")]
		public DateTime ViewStart
		{
			get
			{
				return viewStart ;
			}
			set
			{
				if (viewStart != value)
				{
					viewStart = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("ViewStart") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the end of the time period on view
		/// </summary>
		[Category("TimeGraph")]
		[Description("Gets or Sets the end of the time period on view")]
		public DateTime ViewEnd
		{
			get
			{
				return viewEnd ;
			}
			set
			{
				if (viewEnd != value)
				{
					viewEnd = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("ViewEnd") ;
				}
			}
		}

		///<summary>
		///Gets or sets the Collection of TimeTrace objects
		///</summary>
		[Category("TimeGraph")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Collection of TimeTrace objects")]
		public TimeTraceCollection Traces
		{
			get 
			{
				return traces ;
			}
			set
			{
				if (traces != null)
				{
					traces.PropertyChanged	-= propertyHdlr ;
					traces.ObjectSearch		-= searchHdlr ;
				}

				traces = value ;
				if (traces != null)
				{
					traces.PropertyChanged	+= propertyHdlr ;
					traces.ObjectSearch		+= searchHdlr ;
				}

				OnPropertyChanged("Traces") ;
			}
		}
		private bool ShouldSerializeTraces()
		{
			return !traces.IsDefaultState() ;
		}
		/// <summary>
		/// Reset the Trace Collection to its default settings
		/// </summary>
		public void ResetTraces()
		{
			traces = new TimeTraceCollection(true) ;
		}

		///<summary>
		///Gets or sets whether the graph scrolls automatically when the current time is visible
		///</summary>
		[Category("TimeGraph")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the graph scrolls automatically when the current time is visible")]
		public bool Freeze
		{
			get 
			{
				return freeze ;
			}
			set
			{
				if (freeze != value)
				{
					freeze = value ;
					recalculateUpdate = true ;
					AddToQueue(false) ;
					OnPropertyChanged("Freeze") ;
				}
			}
		}
		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which to draw</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
			CalculateDisplayRectangle() ;

			lock(this)
			{
				if (FullRedraw || resized || recalculateRequired)
				{
					if (resized)
					{
						if (bmpBackground != null)		bmpBackground.Dispose() ;
						bmpBackground = new Bitmap(ClientRectangle.Width, ClientRectangle.Height) ;

						recalculateRequired = true ;
						resized = false ;
					}

					if (recalculateRequired)
					{
						Recalculate(g) ;
						recalculateUpdate = true ;
					}

					Graphics	gBmp	= Graphics.FromImage(bmpBackground) ;

					//basic drawing (appearance etc)
					base.Repaint(true, gBmp) ;

					DrawScales(gBmp) ;
					gBmp.Dispose() ;
				}
				//else just draw paper rect and timeline

				Bitmap		bmp = (Bitmap)bmpBackground.Clone() ;
				Graphics	g2	= Graphics.FromImage(bmp) ;

				if (recalculateUpdate)
				{
					RecalculateUpdate() ;
				}

				SetDisplayRegion(g2) ;
				DrawTimeline(g2, paperRect.Top) ;

				g2.IntersectClip(paperRect) ;
				traces.Draw(g2, paperRect, viewStart, viewEnd, leftScale, rightScale) ;

#if KRT_license
				if (demoMode)		DrawDemoText(g2, DisplayRectangle, BackColor) ;
#endif

				g2.Dispose() ;

				g.DrawImage(bmp, 0, 0) ;

				bmp.Dispose() ;

				band.displayed = false ;
				if (band.enabled)
				{
					band.Draw(RectangleToScreen(timeRect), BackColor, true, true) ;
				}

				//GDIFlush g.Flush() ;
			}
		}

		private void Recalculate(Graphics g)
		{
			int			width ;
			int			overshoot ;

			paperRect = DisplayRectangle ;

			width = scaleWidthIsPercent ? (int)(DisplayRectangle.Width * scaleWidth / 100) : scaleWidth ;

			leftScaleWidth	= leftScale.Enabled ? width + 1 : BORDER_WIDTH ;
			rightScaleWidth	= rightScale.Enabled ? width + 1 : BORDER_WIDTH ;

			paperRect.X += leftScaleWidth ;
			paperRect.Width -= (leftScaleWidth + rightScaleWidth) ;

			// aesthetics
			paperRect.Y += BORDER_WIDTH ;
			paperRect.Height -= BORDER_WIDTH ;

			// calculate top and bottom overshoots
			overshoot = Math.Abs((int)(this.Font.GetHeight() / 2)) ;
			paperRect.Y += overshoot ;
			paperRect.Height -= overshoot ;

			timeRect	= paperRect ;
			timeRect.Height = TIMELINE_HEIGHT + Font.Height ;
			timeRect.Y	= paperRect.Bottom - timeRect.Height ;

			paperRect.Height -= timeRect.Height ;

			recalculateRequired = false ;
		}

		private void RecalculateUpdate()
		{
			//calc time per pixel
			TimeSpan		span			= viewEnd - viewStart ;
			bool			need_update ;

			pixelsPerHour = timeRect.Width / span.TotalHours ;
			CalculateStepSize() ;

			//need to set up the update if not frozen and 'now' is in view or 
			//might in time come into view
			need_update = !freeze && (DateTime.Now.Ticks < viewEnd.Ticks) ;

			if (need_update)
			{
				//bool		set_update		= false ;

				if (updateInterval == TimeSpan.Zero)
				{
					//connect to the update timer
					//set_update = true ;
					nextUpdateTime = DateTime.Now ;
				}
				//else already updating

				//check update timing (slower of 1/2 second or 1 pixel)
				TimeSpan	pixel_time ;

				pixel_time = TimeSpan.FromDays(((TimeSpan)(viewEnd - viewStart)).TotalDays / (double)paperRect.Width) ;
				updateInterval = pixel_time ;

//				//disallow scrolling at faster than half second intervals
//				while (updateInterval.TotalMilliseconds < 500)
//				{
//					updateInterval += pixel_time ;
//				}

				//set update now that interval has been calculated
//				if (set_update)
//				{
//					updatePool.UpdateEvent += updateHdlr ;
//				}
			}
//			else if (updateInterval != TimeSpan.Zero)
//			{
//				//disconnect update timer
//				updatePool.UpdateEvent -= updateHdlr ;
//				updateInterval = TimeSpan.Zero ;
//			}

			recalculateUpdate = false ;
		}

		private void CalculateStepSize()
		{
			double		pixels_per_second		= (int)(pixelsPerHour / 3600.0) ;

			if (pixels_per_second < 4)
			{
				double		pixels_per_minute		= (int)(pixelsPerHour / 60.0) ;

				if (pixels_per_minute < 4)
				{
					if (pixelsPerHour < 10)
					{
						double		pixels_per_day			= pixelsPerHour * 24 ;

						if (pixels_per_day < 5)
						{
							step = Step.Months ;
							pixelsPerMark = pixels_per_day * 31 ;	// Approx.
						}    
						else
						{
							step = Step.Days ;
							pixelsPerMark = pixels_per_day ;
						}
					}       
					else
					{
						step = Step.Hours;
						pixelsPerMark = pixelsPerHour ;
					}
				}
				else
				{
					step = Step.Minutes ;
					pixelsPerMark = pixels_per_minute ;
				}
			}
			else
			{
				step = Step.Seconds ;
				pixelsPerMark = pixels_per_second ;
			}
		}

		private void DrawScales(Graphics g)
		{
			Rectangle		rect = new Rectangle() ;

			rect.Y = paperRect.Y ;
			rect.Height= paperRect.Height ;

			if (leftScale.Enabled)
			{
				rect.X = DisplayRectangle.X - 1 ;
				rect.Width = leftScaleWidth ;
				leftScale.Draw(g, rect, ScaleOrientations.Vertical, this.Font, 0) ;
			}

			if (rightScale.Enabled)
			{
				rect.X = paperRect.Right ;
				rect.Width	= rightScaleWidth;
				rightScale.Draw(g, rect, ScaleOrientations.Vertical, this.Font, 0) ;
			}

			//draw horizontal gridlines
			if (gridEnabled)
			{
				Pen								pen			= new Pen(gridColor, 1) ;
				KineticaRT.Controls.Scale		scale		= null ;

				// Draw horizontal grid lines.
				if (leftScale.Enabled)
				{
					scale = leftScale ;
				}
				else if (rightScale.Enabled)
				{
					scale = rightScale ;
				}

				if (scale != null)
				{
					double			range ;

					range = scale.Maximum - scale.Minimum ;
					if (range != 0)
					{
						double			incr ;
						double			val ;
						int				major_count ;
						int				display_value ;
						double			fractional_value ;

						incr = scale.MajorMarksRepeat / scale.MinorMarksDivisions ;

						// Find the first minor mark.
						for (val = scale.MajorMarksStart ; val > scale.Minimum; val -= scale.MajorMarksRepeat);
	                    
						for (major_count = 0 ; val <= scale.Maximum; val += incr, major_count--)
						{                                       
							fractional_value = (val - scale.Minimum) / range ;
							display_value    = (int)(paperRect.Bottom - (paperRect.Height * fractional_value)) ;        
	                                    
							if ((val < (scale.MajorMarksStart - VERY_SMALL_DOUBLE)) ||  major_count != 0 )
							{  
								if (major_count == 0) major_count = scale.MinorMarksDivisions ;
							}
							else			// Major mark
							{
								g.DrawLine(pen, paperRect.Left, display_value, paperRect.Right, display_value) ;
	                                        
								major_count = scale.MinorMarksDivisions ;
							}                                              
						}
					}
				}
			}
		}

		private void DrawTimeline(Graphics g, int GridTop)
		{
			Pen			pen						= new Pen(ForeColor, 1) ;
			int			marks_per_label ;

			//draw line
			g.DrawLine(pen, timeRect.Left, timeRect.Top, timeRect.Right, timeRect.Top) ;

			marks_per_label = (int)(120.0 / pixelsPerMark) ;
			if (marks_per_label < 1)
			{
				marks_per_label = 1 ;
			}

			if (step == Step.Days)
			{
				if (marks_per_label > 15)
				{
					marks_per_label = 31 ;
				}
			}
			else if (step == Step.Hours)
			{
				// Need to use one of 1 2 3 4 6 8 12 24
				while ((24 % marks_per_label) != 0)
				{
					marks_per_label ++ ;
				}
			}
			else if (step == Step.Months)
			{
				if (marks_per_label > 12)		marks_per_label = 12 ;

				// Need to use one of 1 2 3 4 6 12
				while ((12 % marks_per_label) != 0)
				{
					marks_per_label ++ ;
				}
			}
			else // minutes or seconds
			{
				// Need to use one of 1 2 5 10 15 30 60
				while (	(marks_per_label != 1)	&&
					(marks_per_label != 2)	&&
					(marks_per_label != 5)	&&
					(marks_per_label != 10) &&
					(marks_per_label != 15) &&
					(marks_per_label != 30) &&
					(marks_per_label < 60) 		  )
				{
					marks_per_label ++ ;
				}
			}

//			switch (protect.timescale_type.iVal)
//			{
//				case timescale_actual:
//				{
					DrawTimelineActual(g, GridTop, marks_per_label, pen) ;
//				}
//					break ;
//
//				case timescale_before_now:
//				{
//					SYSTEMTIME		sys_now ;
//					GetLocalTime(&sys_now) ;
//					draw_timeline_reference(hdc, grid_rect_top, time_rect, step, marks_per_label, pen, grid_pen, sys_now) ;
//				}
//					break ;
//
//				case timescale_since_start:
//				{
//					SYSTEMTIME		sys_start ;
//					VariantTimeToSystemTime(protect.range_start.date, &sys_start) ;
//					draw_timeline_reference(hdc, grid_rect_top, time_rect, step, marks_per_label, pen, grid_pen, sys_start) ;
//				}
//					break ;
//			}

			pen.Dispose() ;
		}

		private void DrawTimelineActual(Graphics g, int GridTop, int MarksPerLabel, Pen Pen)
		{
			Pen				grid_pen				= new Pen(gridColor, 1) ;
			Brush			brush					= new SolidBrush(ForeColor) ;
			DateTime		current_time ;
			int				last_month ;
			int				last_day ;
			int				last_year ;
			int				current_pixel ;
			TimeSpan		span ;
			int				mark_count				= 0 ;
			string			label ;
			bool			first_label				= true ;

			//adjust so that time is shown relative to a sensible start point
			current_time= FindEarlierNiceTime(viewStart) ;
			last_year	= current_time.Year ;
			last_month	= current_time.Month ;
			last_day	= current_time.Day ;

			while (viewEnd > current_time)
			{
				span = viewEnd - current_time ;
				current_pixel = timeRect.Right - (int)(span.TotalHours * pixelsPerHour);
				if (current_pixel >= timeRect.Left)
				{
					if ((mark_count % MarksPerLabel) == 0 		||
						current_time.Year != last_year			||
						current_time.Month != last_month		||
						(current_time.Day != last_day && step != Step.Months && step != Step.Days) )
					{
						mark_count = 0 ;

						if (current_time.Year != last_year)
						{
							if (step == Step.Months)
							{
								label = current_time.ToString("MMM yy") ;
							}
							else
							{
								label = current_time.ToString("d MMM yy") ;
							}                     
							last_day	= current_time.Day ;
							last_month	= current_time.Month ;
							last_year	= current_time.Year ;
						}
						else if (current_time.Month != last_month)
						{
							if (step == Step.Months)
							{
								label = current_time.ToString("MMM") ;
							}
							else
							{
								label = current_time.ToString("dd MMM") ;
							}
							last_month = current_time.Month ;
							last_day = current_time.Day ;
						}
						else if (current_time.Day != last_day)
						{
							if (step == Step.Days && !first_label)
							{
								label = current_time.ToString("dd") ;
							}
							else
							{
								label = current_time.ToString("dd MMM") ;
							}
							last_day = current_time.Day ;
						}
						else if (step != Step.Seconds)
						{
							label = current_time.ToString("HH:mm") ;
						}
						else
						{
							label = current_time.ToString("HH:mm:ss") ;
						}

						//label
						SizeF		size ;
						Rectangle	rect ;

						size = g.MeasureString(label, Font) ;

						rect = new Rectangle() ;
						rect.Y = timeRect.Bottom - Font.Height - 2 ;
						rect.Height = Font.Height ;
						rect.X = current_pixel - (int)(size.Width / 2) ;
						rect.Width = (int) size.Width + 2 ;
					
						//dont draw label if text would go off edge
						if (rect.X >= timeRect.X && rect.Right <= timeRect.Right)
						{
							first_label = false ;
							g.DrawString(label, Font, brush, rect, labelFormat) ;
						}

						g.DrawLine(Pen, current_pixel, timeRect.Top, current_pixel, timeRect.Top + 9) ;
						
						//vertical grid line
						if (gridEnabled)
						{
							g.DrawLine(grid_pen, current_pixel, timeRect.Top - 1, current_pixel, GridTop) ;
						}
					}
					else
					{
                        g.DrawLine(Pen, current_pixel, timeRect.Top, current_pixel, timeRect.Top + 5) ;						
					}
				}

				mark_count ++ ;

				switch (step)
				{
					case Step.Months:	current_time = current_time.AddMonths(1) ;	break ;
					case Step.Days:		current_time = current_time.AddDays(1) ;	break ;
					case Step.Hours:	current_time = current_time.AddHours(1) ;	break ;
					case Step.Minutes:	current_time = current_time.AddMinutes(1) ;	break ;
					default:			current_time = current_time.AddSeconds(1) ;	break ;
				}
			}			

			grid_pen.Dispose() ;
			brush.Dispose() ;
		}
		#endregion

		#region Other Methods
		//returns the start of the nearest earlier 'nice' time period greater than the step
		//(ie start of hour for step in minutes etc)
		private DateTime FindEarlierNiceTime(DateTime Time)
		{
			DateTime		temp		= Time ;

			//move back to the previous exact hour
			temp = temp.AddMilliseconds(-temp.Millisecond) ;
			temp = temp.AddSeconds(-temp.Second) ;
			temp = temp.AddMinutes(-temp.Minute) ;

			if (step == Step.Hours)
			{
				//revert to start of day
				temp = temp.AddHours(-temp.Hour) ;
			}
			else if (step == Step.Days)
			{
				//revert to start of month
				temp = temp.AddDays(-temp.Day +1) ;		//set Day = 1
				temp = temp.AddHours(-temp.Hour) ;
			}
			else if (step == Step.Months)
			{
				//revert to start of year
				temp = temp.AddMonths(-temp.Month +1) ;	//set Month = 1
				temp = temp.AddDays(-temp.Day +1) ;		//set Day = 1
				temp = temp.AddHours(-temp.Hour) ;
			}
	
			return temp ;
		}

		//goes back to previous or on to next time unit as defined by step
		private DateTime AdjustTime(DateTime Time, bool MakeEarlier)
		{
			DateTime		temp		= Time ;

			if (MakeEarlier)
			{
				//move back to the start of exact second
				temp = temp.AddMilliseconds(-temp.Millisecond) ;

				if (step >= Step.Minutes)
				{
					//go back to start of minute
					temp = temp.AddSeconds(-temp.Second) ;

					if (step >= Step.Hours)
					{
						//go back to start of hour
						temp = temp.AddMinutes(-temp.Minute) ;

						if (step >= Step.Days)
						{
							//go back to start of day
							temp = temp.AddHours(-temp.Hour) ;

							if (step >= Step.Months)
							{
								//go back to start of month
								temp = temp.AddDays(-temp.Day +1) ;		//set Day = 1
							}
						}
					}
				}
			}
			else
			{
				//move on to start of next second
				temp = temp.AddMilliseconds(1000 - temp.Millisecond) ;

				if (step >= Step.Minutes)
				{
					//go on to start of next minute
					temp = temp.AddSeconds(60 - temp.Second) ;

					if (step >= Step.Hours)
					{
						//go on to start of next hour
						temp = temp.AddMinutes(60 - temp.Minute) ;

						if (step >= Step.Days)
						{
							//go on to start of next day
							temp = temp.AddHours(24 - temp.Hour) ;

							if (step >= Step.Months)
							{
								//go on to start of next month
								temp = temp.AddDays(DateTime.DaysInMonth(temp.Year, temp.Month) - temp.Day) ;
							}
						}
					}
				}
			}
		
			return temp ;
		}

		private void StartZoom(int X, int Y, bool In)
		{
			band.startPoint = new Point(X, Y) ;
			band.screenStart = PointToScreen(band.startPoint) ;
			
			band.enabled= false ;
			zoomingIn	= In ;
			zoomingOut	= !In ;
			zoomed		= false ;

			Capture = true ;

			zoomTimer.Start() ;
		}

		private void StartScroll(bool ToLeft)
		{
			scrollingLeft		= ToLeft ;
			scrollingRight		= !ToLeft ;
			scrollCount			= 0 ;
			scrollTimer.Interval= 600 ;
			
			Scroll(ToLeft) ;

			locked = LockedState.waiting ;
			
			scrollTimer.Start() ;
			
			Capture = true ;
		}

		private new void Scroll(bool ToLeft)
		{
			TimeSpan		range		= viewEnd - viewStart ;
			DateTime		new_start ;
			DateTime		new_end ;

			//range will remain the same
			//scroll to next standard time unit, or a 'nice' fraction of it
			//that involves a reasonable size scroll
			if (ToLeft)
			{
				new_start = AdjustTime(viewStart, true) ;
				if (new_start == viewStart)
				{
					new_start = AdjustTime(viewStart.AddSeconds(-1), true) ;
				}

				//...which would be a scrolling by how large a proportion?
				TimeSpan		adjustment		= viewStart - new_start ;
				double			proportion		= adjustment.TotalDays / range.TotalDays ;

				while (proportion > 0.3)
				{
					adjustment = TimeSpan.FromDays(adjustment.TotalDays / 2) ;
					new_start = viewStart - adjustment ;
					proportion = adjustment.TotalDays / range.TotalDays ;
				}

				while (proportion < 0.1)
				{
					adjustment = TimeSpan.FromDays(adjustment.TotalDays * 2) ;
					new_start = viewStart - adjustment ;
					proportion = adjustment.TotalDays / range.TotalDays ;
				}

				new_end = new_start + range ;
			}
			else
			{
				new_end = AdjustTime(viewEnd, false) ;

				//...which would be a scrolling by how large a proportion?
				TimeSpan		adjustment		= new_end - viewEnd ;
				double			proportion		= adjustment.TotalDays / range.TotalDays ;

				while (proportion > 0.3)
				{
					adjustment = TimeSpan.FromDays(adjustment.TotalDays / 2) ;
					new_end = viewEnd + adjustment ;
					proportion = adjustment.TotalDays / range.TotalDays ;
				}

				while (proportion < 0.1)
				{
					adjustment = TimeSpan.FromDays(adjustment.TotalDays * 2) ;
					new_end = viewEnd + adjustment ;
					proportion = adjustment.TotalDays / range.TotalDays ;
				}

				new_start = new_end - range ;
			}

			SetTimeView(new_start, new_end) ;

			scrollCount++ ;
			//adjust scroll rate?
			if (scrollCount == 5)
			{
				scrollTimer.Interval = 400 ;
			}
			else if (scrollCount == 10)
			{
				scrollTimer.Interval = 200 ;
			}
			else if (scrollCount == 20)
			{
				scrollTimer.Interval = 100 ;
			}
		}

		private void ZoomIn()
		{
			//is our current range big enough to allow zooming?
			if ( ((TimeSpan)(viewEnd - viewStart)).TotalSeconds > 10 )
			{
				DateTime	new_end ;
				DateTime	new_start ;
				TimeSpan	current_range		= viewEnd - viewStart ;
				double		new_hours			= 0.8 * current_range.TotalHours ;
				double		hours_on_left ;
				Point		mouse_pos			= PointToClient(Cursor.Position) ;
				double		proportion ;

				proportion = ((double)(mouse_pos.X - timeRect.X)) / timeRect.Width ;
				if (proportion < 0)
				{
					proportion = 0 ;
				}
				else if (proportion > 1)
				{
					proportion = 1 ;
				}

				hours_on_left = (current_range.TotalHours - new_hours) * proportion ;

				new_start	= viewStart + TimeSpan.FromHours(hours_on_left) ;
				new_end		= new_start + TimeSpan.FromHours(new_hours) ;

				locked = LockedState.waiting ;

				SetTimeView(new_start, new_end) ;
			}
			zoomed = true ;
		}

		private void ZoomOut()
		{
			DateTime	new_end ;
			DateTime	new_start ;
			TimeSpan	current_range		= viewEnd - viewStart ;
			double		new_hours			= 1.25 * current_range.TotalHours ;
			double		hours_on_left ;
			Point		mouse_pos			= PointToClient(Cursor.Position) ;
			double		proportion ;

			proportion = ((double)(mouse_pos.X - timeRect.X)) / timeRect.Width ;
			if (proportion < 0)
			{
				proportion = 0 ;
			}
			else if (proportion > 1)
			{
				proportion = 1 ;
			}

			hours_on_left = (current_range.TotalHours - new_hours) * proportion ;

			new_start	= viewStart + TimeSpan.FromHours(hours_on_left) ;
			new_end		= new_start + TimeSpan.FromHours(new_hours) ;

			locked = LockedState.waiting ;

			SetTimeView(new_start, new_end) ;

			zoomed = true ;
		}

		//Sets new range as given and fires required change events
		//prevents setting too small a range (< 10 sec)
		private void SetTimeView(DateTime NewStart, DateTime NewEnd)
		{
			TimeSpan		new_range		= NewEnd - NewStart ;
			bool			changed			= false ;

			if (new_range.TotalSeconds < 10)
			{
				//range too small - adjust it to 10 secs
				new_range = TimeSpan.FromSeconds(10.0) ;
				NewEnd = NewStart + new_range ;
			}

			if (NewStart != viewStart)
			{
				viewStart = NewStart ;
				OnPropertyChanged("ViewStart") ;
				changed = true ;
			}
			if (NewEnd != viewEnd)
			{
				viewEnd = NewEnd ;
				OnPropertyChanged("ViewEnd") ;
				changed = true ;
			}

			if (changed)
			{
				recalculateUpdate = true ;
				AddToQueue(false) ;
			}
		}

		//given mouse pos in client coords
		//returns true if band wants to capture mouse
		private bool SetupBand(int X, int Y)
		{
			//keep point inside time rect
			if (X < timeRect.X)			X = timeRect.X ;
			if (X > timeRect.Right)		X = timeRect.Right ;

			if ( band.Setup(PointToScreen(new Point(X, 0))) )
			{                           
				AddToQueue(false) ;
			}

			return band.enabled ;
		}

		private bool IsNowInRange()
		{
			if (DateTime.Now.Ticks > viewStart.Ticks && DateTime.Now.Ticks < viewEnd.Ticks)
			{
				return true ;
			}
			return false ;
		}
		#endregion

		#region Mouse events
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (paperRect.Contains(e.X, e.Y))
				{
					if (e.X < (paperRect.Left + (int)(SCROLL_PROPORTION * paperRect.Width)))
					{
						StartScroll(true) ;
					}
					else if (e.X > (paperRect.Left + (int)((1-SCROLL_PROPORTION) * paperRect.Width)))
					{
						StartScroll(false) ;
					}
				}
				else if (timeRect.Contains(e.X, e.Y))
				{
					StartZoom(e.X, e.Y, true) ;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (timeRect.Contains(e.X, e.Y))
				{
					StartZoom(e.X, e.Y, false) ;
				}
			}
			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Cursor			cursor			= Cursors.Default ;

			if (Capture)
			{
				if (zoomingIn || zoomingOut)
				{
					cursor = Cursors.Hand ;
				}
				else if (scrollingLeft)
				{
					cursor = Cursors.PanWest ;
				}
				else if (scrollingRight)
				{
					cursor = Cursors.PanEast ;
				}
			}
			else if (timeRect.Contains(e.X, e.Y))
			{
				cursor = Cursors.Hand ;
			}
			else if ( paperRect.Contains(e.X, e.Y))
			{
				if (e.X < (paperRect.Left + (int)(SCROLL_PROPORTION * paperRect.Width)))
				{
					cursor = Cursors.PanWest ;
				}
				else if (e.X > (paperRect.Left + (int)((1-SCROLL_PROPORTION) * paperRect.Width)))
				{
					cursor = Cursors.PanEast ;
				}
			}

			Cursor = cursor ;

			if ((zoomingIn || zoomingOut) && !band.enabled && !zoomed && timeRect.Contains(e.X, e.Y) )
			{
				//check actually moved a reasonable amount (horizontally)
				if ((e.X < (band.startPoint.X - 5)) || (e.X > (band.startPoint.X + 5)))
				{
					band.enabled = true ;
					zoomTimer.Stop() ;

					Capture = true ;
				}
			}

			SetupBand(e.X, e.Y) ;

			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (zoomingIn || zoomingOut)
			{
				zoomTimer.Stop() ;
				if (!zoomed && !band.enabled)
				{
					if (zoomingIn)
					{
						ZoomIn() ;
					}
					else		//out
					{
						ZoomOut() ;
					}
				}
			}
			zoomingIn	= false ;
			zoomingOut	= false ;
			zoomed		= false ;

			if (scrollingLeft || scrollingRight)
			{
				scrollTimer.Stop() ;
			}
			scrollingLeft	= false ;
			scrollingRight	= false ;

			if (band.enabled)
			{
				band.enabled = false ;

				Capture = SetupBand(e.X, e.Y) ;

				//ensure start and end not the same - horizontal
				if (e.X != band.startPoint.X)
				{
					DateTime	band_start;
					DateTime	band_end;
					TimeSpan	to_start ;
					TimeSpan	to_end ;

					to_start	= TimeSpan.FromHours((band.startPoint.X - timeRect.X) / pixelsPerHour) ;
					to_end		= TimeSpan.FromHours((e.X - timeRect.X) / pixelsPerHour) ;

					if (e.X > band.startPoint.X)
					{
						band_start	= viewStart + to_start ;
						band_end	= viewStart + to_end ;
					}
					else
					{
						band_end	= viewStart + to_start ;
						band_start	= viewStart + to_end ;
					}

					SetTimeView(band_start, band_end) ;
				}
			}
			Capture = false ;

			base.OnMouseUp(e) ;
		}
		#endregion

		#region Timing / Update Events
		private void OnZoomTimer(object sender, EventArgs e)
		{
			if (zoomingIn )
			{
				ZoomIn() ;
			}
			else if (zoomingOut)
			{
				ZoomOut() ;
			}
		}

		private void OnScrollTimer(object sender, EventArgs e)
		{
			if (scrollingLeft)
			{
				Scroll(true) ;
			}
			else if (scrollingRight)
			{
				Scroll(false) ;
			}
		}

		private void OnUpdate()
		{
			if (!freeze && allowAutoScroll)
			{
				if (DateTime.Now >= nextUpdateTime)
				{
					if (locked == LockedState.waiting && DateTime.Now - viewEnd < new TimeSpan(0,0,0))
					{
						locked = LockedState.on ;
					}

					if (locked == LockedState.on)
					{
						TimeSpan length = viewEnd - viewStart ;
						DateTime now = DateTime.Now ;
						SetTimeView(now - length, now) ;
					}
				
					recalculateUpdate = true ;
					AddToQueue(false) ;

					nextUpdateTime = DateTime.Now + updateInterval ;
				}
			}
		}
		#endregion

		#region Other Events
		/// <summary>
		/// Handles PropertyChanged events from sub-objects
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			recalculateRequired = true ;
			base.OnChildPropertyChanged(sender, e) ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect() ;
			foreach (TimeTrace trace in this.traces)
			{
				trace.Connect() ;
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			foreach (TimeTrace trace in this.traces)
			{
				trace.Disconnect() ;
			}
			base.Connect() ;
		}
		#endregion
	}
}

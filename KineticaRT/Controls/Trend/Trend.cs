using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using KineticaRT.Controls;
//using System.Runtime.InteropServices;
//
//[StructLayout(LayoutKind.Sequential)]
//public struct Rect 
//{
//	public int left;
//	public int top;
//	public int right;
//	public int bottom;
//}   

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Trend graph Control. 
	/// Displays the values of a number of Traces over a scrolling time period.
	/// </summary>
    [ChangeRecord("2.0.1.0", "24 01 2007", "N. Savidge", "Tidyed up the display at Freeze and startup.")]
    [ChangeRecord("1.0", "01 01 2003", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2003-2007 KineticaRT Ltd.")]

    [DefaultProperty("Traces")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Trend.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Trend))]
	public class Trend : KineticaRT.Controls.Control
	{
		#region License
#if KRT_license
		private	bool				demoMode ;
		private License license = null;
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
						if ((key == null) || (key != "A80369923133" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "A80369923133" || key == "4CD0571EE68D")) key = "{A8036992-3133-4b74-B6E5-BD37ECE63D60}" ; 
						context.SetSavedLicenseKey (typeof(Trend), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Trend), null) ;
					if (key == "{A8036992-3133-4b74-B6E5-BD37ECE63D60}") licence = new License ("Run") ;
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

		#region Definitions and Variables
		const double		VERY_SMALL_DOUBLE		= 0.0000000001 ;
		const int			BORDER_WIDTH			= 5 ;
		const int			GRID_MARK_WIDTH			= 10 ;
		
		internal const int	MAX_DATA_VALUES			= 2000 ;

		static private EventPool	update_pool		= new EventPool(10, "Trend 10 ms KineticaRT Controls EventPool Worker");

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//internal variables
		private bool				recalculateRequired ;
		private bool				drawCursors ;
		private int					gridLineInterval ;
		private int					gridMarkInterval ;
		private int					yPointOffset ;
		private double				yPointScale ;
		private Rectangle			paperRect ;
		private Rectangle			scrollRect ;
		private int					leftScaleWidth ;
		private int					rightScaleWidth ;
		private int					timePerPixel ;		//ms
		private int					actualRange ;		//ms
		private bool				redrawNeeded ;
		private long []				time ;
		private long				previousUpdateTime ;
		private long				valueIndex ;
		private long				scrollAmount ;
		private bool				traceGotMouse ;
		private int					capturedCursorIndex ;
		private int					newXPos ;
		private bool				cursorMoved ;
		private bool				redrawAll ;
		private bool				doneRecalculate ;
		private Region				scrollRgn ;			// Covers the scrolling area (excludes the cursors).
		private Region				redrawRgn ;			// Indicates the area which need redrawing.
		private Region				traceRgn ;			// area valid for traces to draw
		
		private PropertyChangedEventHandler	propertyHdlr ;
		private ObjectSearchEventHandler	searchHdlr ;
		private EventPool.UpdateEventHandler updateEventHandler;
		
		//property variables
		private bool						scaleWidthIsPercent ;
		private int							scaleWidth ;
		private double						gridLineEvery ;
		private double						gridMarkEvery ;
		private bool						gridEnabled ;
		private bool						gridMarksEnabled ;
		private double						range ;
		private double						baselinePosition ;
		private Color						gridColor ;
		private Color						gridMarkColor ;
		private bool						freeze ;
		private int							enabledTrace ;
		private KineticaRT.Controls.Scale	leftScale ;
		private KineticaRT.Controls.Scale	rightScale ;
		private TraceCollection				traces ;
		private CursorCollection			cursors ;
		private bool						useSourceFormat ;
        private bool timeLabelsEnabled;
		#endregion

		#region Initialisation and disposal
		/// <summary>
		/// Initialises a new instance of the Trend class
		/// </summary>
		public Trend()
		{
            CreateHandle();
            // This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#endif

            //timeLabelsEnabled = true;

			scaleWidthIsPercent	= false ;
			scaleWidth			= 30 ;
			gridLineEvery		= 5.0 ;
			gridMarkEvery		= 1.0 ;
			gridMarksEnabled	= true ;
			gridEnabled			= true ;
			gridColor			= Color.SteelBlue ;
			gridMarkColor		= Color.LightSteelBlue ;
			baselinePosition	= 0.0 ;
			range				= 10.0 ;
			freeze				= false ;
			enabledTrace		= 0 ;
			useSourceFormat		= true ;

			recalculateRequired = true ;
			drawCursors			= true ;
			cursorMoved			= false ;
			redrawAll			= true ;
			doneRecalculate		= false ;

			scrollRgn	= new Region() ;
			redrawRgn	= new Region() ;
			traceRgn	= new Region() ;

			timePerPixel		= 1000 ;
			actualRange			= 10000 ;
			gridLineInterval	= 5000 ;
			gridMarkInterval	= 1000 ;
			yPointOffset		= 0 ;
			yPointScale			= 0 ;
			paperRect			= Rectangle.Empty ;
			scrollRect			= Rectangle.Empty ;
			leftScaleWidth		= 0 ;
			rightScaleWidth 	= 0 ;
			redrawNeeded		= true ;
			previousUpdateTime	= System.Environment.TickCount ;
			valueIndex			= 0 ;
			scrollAmount		= 0 ;
			capturedCursorIndex = -1 ;
			traceGotMouse		= false ;
			newXPos				= 0 ;

			time = new long[MAX_DATA_VALUES] ;
			for (int i = 0; i < MAX_DATA_VALUES; i++)
			{
				time.SetValue(previousUpdateTime, i) ;
			}

			leftScale	= new KineticaRT.Controls.Scale(1) ;
			rightScale	= new KineticaRT.Controls.Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;
			traces		= new TraceCollection(true) ;
			cursors		= new CursorCollection() ;

			//connect to children
			propertyHdlr = new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch) ;

			leftScale.PropertyChanged	+= propertyHdlr ;
			rightScale.PropertyChanged	+= propertyHdlr ;
			traces.PropertyChanged		+= propertyHdlr ;
			cursors.PropertyChanged		+= propertyHdlr ;

			leftScale.ObjectSearch		+= searchHdlr ;
			rightScale.ObjectSearch		+= searchHdlr ;
			traces.ObjectSearch			+= searchHdlr ;
			cursors.ObjectSearch		+= searchHdlr ;
			
			//connect update loop
			updateEventHandler = new EventPool.UpdateEventHandler(OnUpdate);
			update_pool.UpdateEvent += updateEventHandler ; 

			//this makes no difference
			//this.SetStyle(ControlStyles.DoubleBuffer, true) ;
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
				if (updateEventHandler != null)
				{
					update_pool.UpdateEvent -= updateEventHandler ; 
					updateEventHandler = null ;
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
			// Trend
			// 
			this.Name = "Trend";
			this.Size = new System.Drawing.Size(312, 150);

		}
		#endregion

        /// <summary>
        /// Clears the data from the traces.
        /// </summary>
        public void Clear()
        {
            traces.ResetData(paperRect.Width, paperRect.Bottom);
        }

		#region Properties
		/// <summary>
		/// Gets or sets whether the Trend gets its ToolTip, Minimum and Maximum
		/// from the Source of it's primary Trace
		/// </summary>
		[Category("Trend")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Trend gets its ToolTip, Minimum and Maximum from the Source of it's primary trace")]
		public virtual bool UseSourceFormat
		{
			get
			{
				return useSourceFormat ;
			}
			set
			{
				if (useSourceFormat != value)
				{
					useSourceFormat = value ;
					if (useSourceFormat)
					{
						SetFormat() ;
					}
					AddToQueue(false) ;
					OnPropertyChanged("UseSourceFormat") ;
				}
			}
		}
        /// <summary>
        /// Gets or sets whether the Trends time lables are shown.
        /// </summary>
        [Category("Trend")]
        [DefaultValue(false)]
        [Description("Whether the time lables are shown")]
        public virtual bool TimeLabelsEnabled
        {
            get
            {
                return timeLabelsEnabled;
            }
            set
            {
                if (timeLabelsEnabled != value)
                {
                    timeLabelsEnabled = value;
                    AddToQueue(false);
                    OnPropertyChanged("TimeLabelsEnabled");
                }
            }
        }
        /// <summary>
		/// Gets or Sets the type of the Control's Border
		/// </summary>
		[Category("Control")]
        [DefaultValue(BorderType.Raised)]
		[Description("Gets or Sets the type of the Control's Border")]
        public override BorderType BorderType
		{
			get
			{
                return base.BorderType;
			}

			set
			{
                base.BorderType = value;
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
		[Description("Gets or Sets the width of the Control's Border: 0 - 0.5 as proprtion of width of control, or >= 1 as a number of pixels")]
		public override double BorderWidth
		{
			get
			{
				return base.BorderWidth ;
			}

			set
			{
				base.BorderWidth = value ;
				recalculateRequired = true ;
			}
		}

		/// <summary>
		/// Gets or sets whether the ScaleWidth property refers to pixels or a percentage of the control's total width
		/// </summary>
		[Category("Trend")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the ScaleWidth property refers to pixels or a percentage of the control's total width")]
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
		/// Gets or sets the width of Scale objects (in pixels or percent depending on the value of ScaleWidthIsPercent)
		/// </summary>
		[Category("Trend")]
		[DefaultValue(30)]
		[Description("Gets or sets the width of Scale objects (in pixels or percent depending on the value of ScaleWidthIsPercent)")]
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

		/// <summary>
		/// Gets or sets the number of Seconds between grid lines
		/// </summary>
		[Category("Trend")]
		[DefaultValue(5.0)]
		[Description("Gets or sets the number of Seconds between grid lines")]
		public double GridLineEvery
		{
			get
			{
				return gridLineEvery ;
			}
			set
			{
				if (gridLineEvery != value)
				{
					gridLineEvery = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("GridLineEvery") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of Seconds between grid marks
		/// </summary>
		[Category("Trend")]
		[DefaultValue(1.0)]
		[Description("Gets or sets the number of Seconds between grid marks")]
		public double GridMarkEvery
		{
			get
			{
				return gridMarkEvery ;
			}
			set
			{
				if (gridMarkEvery != value)
				{
					gridMarkEvery = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("GridMarkEvery") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether Grid Marks are enabled
		/// </summary>
		[Category("Trend")]
		[DefaultValue(true)]
		[Description("Gets or sets whether Grid Marks are enabled")]
		public bool GridMarksEnabled
		{
			get
			{
				return gridMarksEnabled ;
			}
			set
			{
				if (gridMarksEnabled != value)
				{
					gridMarksEnabled = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("GridMarksEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Grid is enabled
		/// </summary>
		[Category("Trend")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Grid is enabled")]
		public bool GridEnabled
		{
			get
			{
				return gridEnabled ;
			}
			set
			{
				if (gridEnabled != value)
				{
					gridEnabled = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("GridEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of seconds visible on the Trend
		/// </summary>
		[Category("Trend")]
		[DefaultValue(10.0)]
		[Description("Gets or sets the number of seconds visible on the Trend")]
		public double Range
		{
			get
			{
				return range ;
			}
			set
			{
				if (range != value)
				{
					range = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("Range") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the position of the baseline as proportion of the Trend (0..1)
		/// </summary>
		[Category("Trend")]
		[DefaultValue(0.0)]
		[Description("Gets or sets the position of the baseline as proportion of the Trend (0..1)")]
		public double BaselinePosition
		{
			get
			{
				return baselinePosition ;
			}
			set
			{
				if (baselinePosition != value)
				{
					baselinePosition = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("BaselinePosition") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the display is frozen or scrolling
		/// </summary>
		[Category("Trend")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the display is frozen or scrolling")]
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

					if (!freeze)
					{
                        //valueIndex = (int)((valueIndex + 2) % MAX_DATA_VALUES);
                        //scrollAmount += 2;

                        //valueIndex = (int)((valueIndex + 2) % MAX_DATA_VALUES);
                        //scrollAmount += 2;

                        previousUpdateTime = System.Environment.TickCount;
					}
					AddToQueue(false) ;
					OnPropertyChanged("Freeze") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the color of the Grid
		/// </summary>
		[Category("Trend")]
		[Description("Gets or sets the color of the Grid")]
		public Color GridColor
		{
			get
			{
				return gridColor;
			}
			set
			{
				if (gridColor != value)
				{
					gridColor = value;
					AddToQueue(false) ;
					OnPropertyChanged("GridColor") ;
				}
			}
		}
		private bool ShouldSerializeGridColor()
		{
			return GridColor != Color.SteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetGridColor()
		{
			gridColor = Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or sets the color of the Grid Marks
		/// </summary>
		[Category("Trend")]
		[Description("Gets or sets the color of the Grid Marks")]
		public Color GridMarkColor
		{
			get
			{
				return gridMarkColor;
			}
			set
			{
				if (gridMarkColor != value)
				{
					gridMarkColor = value;
					AddToQueue(false) ;
					OnPropertyChanged("GridMarkColor") ;
				}
			}
		}
		private bool ShouldSerializeGridMarkColor()
		{
			return GridMarkColor != Color.LightSteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetGridMarkColor()
		{
			gridMarkColor = Color.LightSteelBlue ;
		}

		///<summary>
		///Gets the Left hand Scale object
		///</summary>
		[Category("Trend")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Left hand Scale object")]
		public KineticaRT.Controls.Scale LeftScale
		{
			get 
			{
				return leftScale ;
			}
		}
		private bool ShouldSerializeLeftScale()
		{
			return true ;		//always save the scale
		}
		/// <summary>
		/// Reset the LeftScale to its default settings
		/// </summary>
		public void ResetLeftScale()
		{
			leftScale = new KineticaRT.Controls.Scale(1) ;
		}

		///<summary>
		///Gets the Right hand Scale object
		///</summary>
		[Category("Trend")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Right hand Scale object")]
		public KineticaRT.Controls.Scale RightScale
		{
			get 
			{
				return rightScale ;
			}
		}
		private bool ShouldSerializeRightScale()
		{
			return true ;		//always save the scale
		}
		/// <summary>
		/// Reset the RightScale to its default settings
		/// </summary>
		public void ResetRightScale()
		{
			rightScale = new KineticaRT.Controls.Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;
		}

		///<summary>
		///Gets or sets the Collection of Trace objects
		///</summary>
		[Category("Trend")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the Collection of Trace objects")]
		public TraceCollection Traces
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
			traces = new TraceCollection(true) ;
		}

		///<summary>
		///Gets or sets the Collection of Cursor objects
		///</summary>
		[Category("Trend")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the Collection of Cursor objects")]
		public CursorCollection Cursors
		{
			get 
			{
				return cursors ;
			}
			set
			{
				if (cursors != null)
				{
					cursors.PropertyChanged	-= propertyHdlr ;
					cursors.ObjectSearch	-= searchHdlr ;
				}

				cursors = value ;
				if (cursors != null)
				{
					cursors.PropertyChanged	+= propertyHdlr ;
					cursors.ObjectSearch	+= searchHdlr ;
				}

				OnPropertyChanged("Cursors") ;
			}
		}
		private bool ShouldSerializeCursors()
		{
			//always save if there are any items in the collection
			return cursors.Count > 0 ;
		}
		/// <summary>
		/// Reset the Cursor Collection to its default settings
		/// </summary>
		public void ResetCursors()
		{
			cursors = new CursorCollection() ;
		}

		/// <summary>
		/// Gets or sets the index of the Trace that can be updated by dragging
		/// </summary>
		/// <remarks>The index is zero-based. Specify -1 to disable user input. Mouse dragging must be done in one of the Scale regions</remarks>
		[Category("Trend")]
		[DefaultValue(0)]
		[Description("Gets or sets the zero-based index of the Trace that can be updated by dragging. Specify -1 to Disable input")]
		public int EnabledTrace
		{
			get
			{
				return enabledTrace ;
			}
			set
			{
				if (enabledTrace != value)
				{
					enabledTrace = value ;
					OnPropertyChanged("EnabledTrace") ;
				}
			}
		}

		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
            //lock (this) //ft ajout 
            {
                Region draw_region;

                CalculateDisplayRectangle();
                base.Repaint(FullRedraw, g);

                //ft lock (this)
                {
                    //default IS no smoothing
                    //				//need no smoothing, else regions don't cover everything that is drawn
                    //				g.SmoothingMode = SmoothingMode.None ;

                    SetDisplayRegion(g);

                    if (resized)
                    {
                        recalculateRequired = true;
                        resized = false;
                    }
                    if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                    {
                        backColorChanged = false;
                        backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                    }

                    if (cursorMoved)
                    {
                        Region old_cursor_rgn;

                        scrollRgn = cursors.MoveCursor(capturedCursorIndex, g, newXPos, scrollRect, timePerPixel, actualRange, out old_cursor_rgn);
                        doneRecalculate = true;

                        redrawRgn.Union(old_cursor_rgn);

                        drawCursors = true;
                        redrawNeeded = true;
                        redrawAll = true;
                        cursorMoved = false;
                    }

                    draw_region = new Region(DisplayRectangle);

                    if (FullRedraw || recalculateRequired)
                    {
                        if (recalculateRequired)
                        {
                            Recalculate(g);
                        }

                        if (!FullRedraw)
                        {
                            //this has already been done if it is a FullRedraw
                            g.FillRectangle(backColorBrush, DisplayRectangle);
                        }

                        DrawScales(g);

                        drawCursors = true;

                        g.IntersectClip(draw_region);

                        redrawNeeded = false;		//background already drawn
                        DrawText(g, false, g.Clip);

#if KRT_license
					if (demoMode)
					{
						//'demo' text
						Rectangle			rect = DisplayRectangle ;

						rect.Height = (int)DefaultFont.GetHeight() + 3 ;
						DrawDemoText(g, rect, BackColor) ;
					}
#endif
                    }
                    else
                    {
                        if (!redrawAll)
                        {
                            //this doesn't work atm if move cursor
                            //(not tested everyything else)
                            if (doneRecalculate)
                            {
                                //only recombine scroll rgn if done a recalculate
                                scrollRgn = cursors.CombineTextRegions(scrollRgn);
                                doneRecalculate = false;
                            }

                            draw_region.Intersect(scrollRgn);
                        }

                        g.IntersectClip(draw_region);
                        DrawText(g, true, g.Clip);
                    }

                    scrollAmount = 0;

                    redrawNeeded = false;
                    redrawAll = false;

                    //finished with redraw region
                    redrawRgn.MakeEmpty();

                    //GDIFlush g.Flush() ;
                    draw_region.Dispose();
                }
            }
		}

		private void Recalculate(Graphics g)
		{
			CalculateTrendRectangles() ;

			scrollRgn = cursors.Recalculate(g, scrollRect, actualRange, timePerPixel) ;

			Rectangle		rect = paperRect ;
			rect.Height++ ;
			traceRgn = new Region(rect) ;
			//traceRgn.Intersect(scrollRgn) ;

			gridLineInterval = (int)(gridLineEvery * 1000) ;
			gridMarkInterval = (int)(gridMarkEvery * 1000) ;

			yPointOffset = paperRect.Bottom ;
			yPointScale  = paperRect.Height - VERY_SMALL_DOUBLE ;

			recalculateRequired = false ;
			doneRecalculate = true ;
		}

		private void CalculateTrendRectangles()
		{
			Rectangle	old_paper_rect		= paperRect ;
			int			width ;
			int			text_overshoot		= 0;
			int			cursor_overshoot	= 0;
			int			tick_overshoot		= 1;
			int			temp ;

			paperRect = DisplayRectangle ;

			width = scaleWidthIsPercent ? (int)(DisplayRectangle.Width * scaleWidth / 100) : scaleWidth ;

			leftScaleWidth	= leftScale.Enabled ? width + 1 : BORDER_WIDTH ;
			rightScaleWidth	= rightScale.Enabled ? width + 1 : BORDER_WIDTH ;

			paperRect.X += leftScaleWidth ;
			paperRect.Width -= (leftScaleWidth + rightScaleWidth) ;

			// aesthetics
			paperRect.Y += BORDER_WIDTH ;
			paperRect.Height -= (2 * BORDER_WIDTH) ;

			//demo mode?
#if KRT_license
			if (demoMode)
			{
				int		demo_height		= (int)DefaultFont.GetHeight() + 3 ;
				paperRect.Y += demo_height ;
				paperRect.Height -= demo_height ;
			}
#endif

			// calculate top and bottom overshoots
			text_overshoot = Math.Abs((int)(this.Font.GetHeight() / 2)) ;

			if (cursors.CursorVisible())
			{
				cursor_overshoot = 13;
			}

			if (gridMarksEnabled)
			{
				tick_overshoot = GRID_MARK_WIDTH;
			}

            if (timeLabelsEnabled)
            {
                tick_overshoot += 8;
            }

			temp = Math.Max(text_overshoot, cursor_overshoot) ;
			paperRect.Y += temp ;
			paperRect.Height -= (temp + Math.Max(text_overshoot, cursor_overshoot + tick_overshoot) ) ;

			scrollRect = paperRect ;      
			scrollRect.Height += tick_overshoot ;

			timePerPixel = (int)((range * 1000) / paperRect.Width) + 1 ; // ms/pixel
			actualRange = timePerPixel * paperRect.Width ;

			if (!old_paper_rect.Equals(paperRect))
			{
				traces.ResetData(paperRect.Width, paperRect.Bottom) ;
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
		}

		private void DrawText(Graphics g, bool Scroll, Region DrawRegion)
		{
			// Background.
			if (redrawNeeded)
			{
				g.Clip = DrawRegion ;
				g.IntersectClip(redrawRgn) ;
				if (!g.IsClipEmpty)
				{
					g.FillRectangle(backColorBrush, ClientRectangle) ;
				}
			}

			//drawing to a bmp don't need to do cursors twice
//			// Cursors.
//			g.Clip = DrawRegion ;
//			cursors.Draw(g, drawCursors, scrollRect.Right, traces, valueIndex, BackColor) ;

			// Scroll area.
			g.IntersectClip(scrollRgn) ;

			//draw the lot - but to a bmp first
			Bitmap		bmp		= new Bitmap(ClientRectangle.Width, ClientRectangle.Height) ;
			Graphics	gBmp	= Graphics.FromImage(bmp) ;

			if (!g.IsClipEmpty)
			{
//				//draw the lot - but to a bmp first
//				Bitmap		bmp		= new Bitmap(ClientRectangle.Width, ClientRectangle.Height) ;
//				Graphics	gBmp	= Graphics.FromImage(bmp) ;
//
				RedrawTrendSegment(gBmp, scrollRect, DrawRegion) ;
//
//				gBmp.Dispose() ;
//				g.DrawImage(bmp, 0, 0) ;
//				bmp.Dispose() ;
//				if (!Scroll)
//				{
//					// Redraw the whole lot.
//					RedrawTrendSegment(g, scrollRect, DrawRegion) ;
//				}
//				else
//				{
//					ScrollTrend(g, DrawRegion) ;
//				}
			}

			// Cursors.
			g.Clip = DrawRegion ;
			gBmp.Clip = DrawRegion ;
			cursors.Draw(gBmp, drawCursors, scrollRect.Right, traces, valueIndex, backColorBrush) ;

			//copy back to main graphics object
			gBmp.Dispose() ;
			g.DrawImage(bmp, 0, 0) ;
			bmp.Dispose() ;
		}

//		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
//		private static extern bool ScrollDC(
//			IntPtr hdc,
//			int dx, int dy,
//			ref Rect prcScroll,
//			ref Rect prcClip,
//			IntPtr rgnUpdate,
//			ref Rect prcUpdate);

		// Scroll and then draw the uncovered parts.
		private void ScrollTrend(Graphics g, Region DrawRegion)
		{
			if (scrollAmount > 0)
			{
				Bitmap		bmp		= new Bitmap(ClientRectangle.Width, ClientRectangle.Height) ;
				Graphics	gBmp	= Graphics.FromImage(bmp) ;

				RedrawTrendSegment(gBmp, scrollRect, DrawRegion) ;

				gBmp.Dispose() ;
				g.DrawImage(bmp, 0, 0) ;
				bmp.Dispose() ;

				//Temp...(works but flickers)
				//RedrawTrendSegment(g, scrollRect, DrawRegion) ;

//				Region			update_rgn 			= new Region(scrollRect) ;
//				IntPtr			hdc	;
//				Rect			scroll_rect ;
//				Rect			update_rect			= new Rect() ;
//				IntPtr			update_rgn_ptr		= new IntPtr() ;
//				Matrix			mx					= new Matrix() ;
//				RectangleF[]	rects ;
//				Rectangle		segment				= new Rectangle() ;
//				
//				scroll_rect.left = scrollRect.Left ;
//				scroll_rect.right = scrollRect.Right ;
//				scroll_rect.top = scrollRect.Top ;
//				scroll_rect.bottom = scrollRect.Bottom ;
//
//				update_rgn_ptr = update_rgn.GetHrgn(g) ;
//
//				hdc = g.GetHdc() ;
//				ScrollDC(hdc, (int)-scrollAmount, 0, ref scroll_rect, ref scroll_rect, update_rgn_ptr, ref update_rect) ;
//				g.ReleaseHdc(hdc) ;
//
//				update_rgn = Region.FromHrgn(update_rgn_ptr) ;
//
//				if (redrawNeeded)
//				{
//					redrawRgn.Translate(-scrollAmount, 0) ;
//					update_rgn.Union(redrawRgn) ;
//				}
//
//				rects = update_rgn.GetRegionScans(mx) ;
//
//				g.Clip = DrawRegion ;
//
//				for (int index = 0; index < rects.Length; index ++)
//				{
//					segment.Size = rects[index].Size.ToSize() ;
//					segment.X = (int) rects[index].X ;
//					segment.Y = (int) rects[index].Y ;
//
//					RedrawTrendSegment(g, segment, DrawRegion);
//				}
			}
			else
			{
				RedrawTrendSegment(g, scrollRect, DrawRegion) ;
			}
		}

		private void RedrawTrendSegment(Graphics g, Rectangle Segment, Region DrawRegion)
		{
			int			start_point ;
			int			no_of_points ;
			int			offset ;
			long		start_time ;
			int			y_pos ;
			Pen			scale_pen				= new Pen(leftScale.Color, 1) ;
            Brush scale_brush = new SolidBrush(leftScale.Color);

			y_pos = paperRect.Bottom - (int)(paperRect.Height * baselinePosition) ;

			start_point = paperRect.Right - Segment.Right - 1 ;
			if (start_point < 0)
			{
				start_point = 0 ;
			}
			no_of_points = Segment.Width + 2 ;
			if ((start_point + no_of_points) > paperRect.Width)
			{
				no_of_points = paperRect.Width - start_point ;
			}
			offset = (int)(valueIndex - start_point) ;
			if (offset < 0)
			{
				offset += MAX_DATA_VALUES ;
			}
			start_time = time[offset] ;

			// Draw the background.
			g.FillRectangle(backColorBrush, Segment) ;

			// Draw the grid.
			if (gridEnabled)
			{
				Pen								pen			= new Pen(gridColor, 1) ;
				int         					x_position ;
				double      					plot_time ;
				KineticaRT.Controls.Scale		scale		= null ;

				// Draw vertical grid lines.
				plot_time  = Math.Floor((double)(start_time / gridLineInterval)) ;
				plot_time *= gridLineInterval ;

				x_position = scrollRect.Right - (int)((previousUpdateTime - plot_time) / timePerPixel) ;
	                    
				while (x_position >= Segment.X)
				{   
					if (x_position < Segment.Right)
					{
						g.DrawLine(pen, x_position, paperRect.Bottom, x_position, paperRect.Top) ;

                        if (timeLabelsEnabled)
                        {
                            if (x_position < scrollRect.Right - g.MeasureString(DateTime.Now.ToLongTimeString(),Font).Width)
                            {
                                DateTime mark_time = DateTime.Now - new TimeSpan(0, 0, 0, 0, (int)((scrollRect.Right - x_position) * timePerPixel));
                                g.DrawString(mark_time.ToLongTimeString(), Font, scale_brush, x_position, y_pos + (baselinePosition==0? 7:4));
                            }
                        }
                    }

					plot_time -= gridLineInterval ;     
					x_position = scrollRect.Right - (int)((previousUpdateTime - plot_time) / timePerPixel) ;
				}

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
							else
							{  
								// Major mark.                  
								if (display_value < Segment.Bottom)
								{
									g.DrawLine(pen, Segment.Left, display_value, Segment.Right, display_value) ;
								}
	                                        
								major_count = scale.MinorMarksDivisions ;
							}                                              
						}
					}
				}

				// Draw baseline.
				//?? y_pos + (jd.protect.baseline_position.dblVal == 0 ? 0 : 1)
				g.DrawLine(scale_pen, Segment.X, y_pos, Segment.Right, y_pos) ;
    
				//  Draw the tick marks.
				if (gridMarksEnabled)
				{
					Pen			mark_pen		= new Pen(gridMarkColor, 1) ;
			
					plot_time  = Math.Floor((double)(start_time / gridMarkInterval)) ;
					plot_time *= gridMarkInterval ;

					x_position = scrollRect.Right - (int)((previousUpdateTime - plot_time) / timePerPixel) ;
	                    
					while (x_position >= Segment.X)
					{   
						if (x_position < Segment.Right)
						{
							if (baselinePosition == 0)
							{
                                g.DrawLine(pen, x_position, y_pos + (timeLabelsEnabled ? 6 : 8), x_position, y_pos);
							}
							else
							{
								g.DrawLine(pen, x_position, y_pos + 4, x_position, y_pos - 4) ;
							}
						}

						plot_time -= gridMarkInterval ;     
						x_position = scrollRect.Right - (int)((previousUpdateTime - plot_time) / timePerPixel) ;
					}
					mark_pen.Dispose() ;
				}

				pen.Dispose() ;
			}
            scale_pen.Dispose() ;
        
			// Draw the traces.
			g.Clip = DrawRegion ;
			g.IntersectClip(traceRgn) ;
			traces.Draw(g, offset, start_point, no_of_points, paperRect.Right) ;
		}
		#endregion

		#region Other methods
		/// <summary>
		/// Sets up the tool tip and scale ranges based on trace Sources
		/// </summary>
		private void SetFormat()
		{
			if (useSourceFormat && traces.Count > 0)
			{
				Channels.IInfoChannel		ch ;
				Channels.INumericChannel	num ;

				ch = traces[0].SourceChannel as Channels.IInfoChannel ;
				if (ch != null)
				{
					//set tool tip from first trace source
					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;

					num = ch as Channels.INumericChannel ;
					if (num != null)
					{
						//set left scale from first trace source
						leftScale.Minimum	= num.Minimum ;
						leftScale.Maximum	= num.Maximum ;

						if (traces.Count < 2)
						{
							//ditto right scale if there is only one trace
							rightScale.Minimum	= num.Minimum ;
							rightScale.Maximum	= num.Maximum ;
						}
					}
				}

				if (traces.Count > 1)
				{
					num = traces[1].SourceChannel as Channels.INumericChannel ;
					if (num != null)
					{
						//set right scale from second trace source
						rightScale.Minimum	= num.Minimum ;
						rightScale.Maximum	= num.Maximum ;
					}
				}
			}
		}
		#endregion

		#region Update Timing

        private void Recalc()
        {
            //if (this.InvokeRequired)
            //{
            if (!this.IsHandleCreated) return;
            try
            {
                if (this.InvokeRequired)//ft
                {
                    this.BeginInvoke(new MethodInvoker(Recalc2));
                }
                else
                {
                    Recalc2();
                }
            }
            catch (System.InvalidOperationException)
            {
            }
                //return;
            //}
        }
        private void Recalc2()
        {
            //try
            //{
                Graphics g = CreateGraphics();
                Recalculate(g);
                g.Dispose();
                recalculateRequired = false;
            //}
            //catch (System.Exception exception)
            //{
            //}
        }

		private void OnUpdate()
		{
			//leave in pool for next time
			if (recalculateRequired)
			{
                Recalc();
			}

			lock(this)
			{
				long			current_time		= System.Environment.TickCount ;
				bool			update_value;

				if (freeze)		return ;

	  			update_value = ((current_time - previousUpdateTime) > (long)timePerPixel);

				// Update the objects value and time.
				if (update_value)
				{
					long			scroll_amount ;
					int				current_value_index ;
					long			index ;

					//round to nearest pixel
					current_time = (long)(current_time / timePerPixel) ;
					current_time = (long)(current_time * timePerPixel) ;

					scroll_amount = (current_time - previousUpdateTime) / timePerPixel ;
					if (scroll_amount < 0)		scroll_amount = 0 ;
					scrollAmount += scroll_amount ;

					// Fill in the times for the scroll bit.
					current_value_index = (int) ((valueIndex + 1) % MAX_DATA_VALUES) ;

					if ((scroll_amount > 1) && (scroll_amount < paperRect.Width))
					{
						for (index = 1; index < scroll_amount; index ++) 
						{
							time[current_value_index] = current_time ;
							current_value_index = (int)((current_value_index + 1) % MAX_DATA_VALUES) ;
						}
					}
					time[current_value_index] = current_time ;
			
					// Update the trace values.
					index = valueIndex ;

					//if (scroll_amount < 0)
					//{
					//	Debug.WriteLine(scroll_amount.ToString()) ;
					//}

					valueIndex = (long)((valueIndex + scroll_amount) % MAX_DATA_VALUES) ;
					previousUpdateTime = current_time ;
		
					traces.UpdateValues(index, yPointOffset, yPointScale, scroll_amount, paperRect.Width, leftScale, rightScale) ;
				}

				//			if (joint_data.need_rescale || joint_data.protect.auto_scale.boolVal)
				//			{
				//				if (do_auto_scale()) //loop_count))
				//				{
				//					//			loop_count = 0 ;
				//					update_value = true ;		//set this to ensure OnChange is called
				//				}
				//			}

				if (update_value)
				{
					//demand a redraw on the drawing thread, not the timing thread
					AddToQueue(false) ;
				}
			}
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
				capturedCursorIndex = cursors.PointInCursorRect(e.X, e.Y) ;

				if (capturedCursorIndex >= 0)
				{
					//cursorGotMouse = true ;
					Capture = true ;
				}
				else if (enabledTrace >= 0 && enabledTrace < traces.Count)
				{
					//see if in either scale area
					if ((e.X > scrollRect.Right && e.X < DisplayRectangle.Right ) ||
						(e.X < scrollRect.Left && e.X > DisplayRectangle.Left ) 	)
					{
						//check height
						if (e.Y > scrollRect.Top && e.Y < scrollRect.Bottom)
						{
							double		fraction ;

							traceGotMouse = true ;
							fraction = 1 - (double)(e.Y - paperRect.Top) / paperRect.Height ;
					
							traces[enabledTrace].SetValueFromFraction(fraction, leftScale, rightScale) ;
	
							Capture = true ;
						}
					}
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
			if (Capture)
			{
				if (capturedCursorIndex >= 0)
				{
					newXPos = e.X ;
					cursorMoved = true ;

					//redraw
					redrawAll = true ;
					AddToQueue(false) ;
				}
				else if (traceGotMouse)
				{
					double				fraction ;
		
					fraction = 1 - (double)(e.Y - paperRect.Top) / paperRect.Height ;
					if (fraction > 1)
					{
						fraction = 1 ;
					}
					else if (fraction < 0)
					{
						fraction = 0 ;
					}

					traces[enabledTrace].SetValueFromFraction(fraction, leftScale, rightScale) ;
				}
			}
			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (Capture)
			{
				if (capturedCursorIndex >= 0)
				{
					capturedCursorIndex = -1 ;
				}

				if (traceGotMouse)
				{
					traceGotMouse = false ;

					if (enabledTrace >= 0 && enabledTrace < traces.Count)
					{
						//reset value
						Trace			trace		= traces[enabledTrace] ;

						if (trace.AutoReturn)
						{
							trace.Value = new KineticaRT.Values.Double(trace.DefaultValue) ;
						}
					}
				}

				Capture = false ;
			}
			base.OnMouseUp(e) ;
		}
		#endregion

		#region Property Changed handling
		/// <summary>
		/// Handles PropertyChanged events from sub-objects
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SourceChannel")
			{
				SetFormat() ;
			}
			else if (e.PropertyName != "Value")
			{
				recalculateRequired = true ;

			}
			AddToQueue(false) ;	//no child properties need background and border to be redrawn
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (traces != null)			traces.Connect() ;
			if (cursors != null)		cursors.Connect() ;
			if (leftScale != null)		leftScale.Connect() ;
			if (rightScale != null)		rightScale.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (traces != null)			traces.Disconnect() ;
			if (cursors != null)		cursors.Disconnect() ;
			if (leftScale != null)		leftScale.Disconnect() ;
			if (rightScale != null)		rightScale.Disconnect() ;
		}
		#endregion
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Diagnostics;
using System.Windows.Forms;
using KineticaRT;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	internal enum ScaleSides
	{
		Left	= 0,
		Right	= 1,
		Top		= 2,
		Bottom	= 3,
	}

	/// <summary>
	/// The KineticaRT XYGraph Control displays a number of Traces
	/// whose x and y values change over time.
	/// </summary>
    [ChangeRecord("2.1.2.0", "18 04 2007", "N. Savidge", "Fixed deadlock when adding a trace. (In XYStaticTraceCollection)")]
    [ChangeRecord("1.0", "01 01 2003", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2003-2007 KineticaRT Ltd.")]
#if KRT_license
	[LicenseProviderAttribute(typeof(XYGraph.LicenseProvider))]
#endif
	[DefaultProperty("Traces")]
	[ToolboxBitmap(typeof(XYGraph))]
	public class XYGraph : KineticaRT.Controls.Control
	{
		#region CLASS: Grid
		/// <summary>
		/// XYGraph Grid class
		/// </summary>
		private class Grid
		{
			private		const double	VERY_SMALL_DOUBLE	= 0.0000000001;
			internal	const int		MAX_GRIDLINES		= 100;

			public Color		color ;
			public bool			enabled ;

			public Point[]		startPt ;
			public Point[]		endPt ;
			public int			noOfLines ;

			public Grid()
			{
				color = Color.SteelBlue ;
				enabled = true ;

				noOfLines = 0 ;

				startPt = new Point[MAX_GRIDLINES] ;
				endPt	= new Point[MAX_GRIDLINES] ;

				for (int index = 0; index < MAX_GRIDLINES; index++)
				{
					startPt[index]	= Point.Empty ;
					endPt[index]	= Point.Empty ;
				}

			}

			public void CalculateLines(Scale Scale, int Start, int End, int MinPt, int Size, bool Vertical)
			{
				int				pt_index		= 0 ;	// gridline array index.

				noOfLines = 0 ;

				if (Scale != null) 
				{   
					double		min			= Scale.Minimum ;
					double		max			= Scale.Maximum ;
					double		range		= max - min ;

					if (range > 0)
					{
						double		repeat				= Scale.MajorMarksRepeat ;
						double		marks_start			= Scale.MajorMarksStart ;
						int			divisions			= Scale.MinorMarksDivisions ;
						double		incr				= repeat / divisions ;
						double		fractional_value ;
						int			display_value ;
						int			major_count ;
						double		value;

						// Find the first minor mark.
						for (value = marks_start ; value > min ; value -= repeat );
	            
						for (major_count = 0 ; value <= max ; value += incr, major_count-- )
						{                                       
							fractional_value = (value - min) / range ;
							display_value    = (int)(MinPt + Size * fractional_value) ;        
	                            
							if ((value < (marks_start - VERY_SMALL_DOUBLE)) ||  major_count != 0 )
							{  
								if (major_count == 0) major_count = divisions ;
							}
							else
							{  
								// Major mark.                  
								if (Vertical)
								{
									startPt[pt_index].X = display_value;
									startPt[pt_index].Y = Start;
	
									endPt[pt_index].X = display_value;
									endPt[pt_index].Y = End;
								}
								else
								{
									startPt[pt_index].X = Start;
									startPt[pt_index].Y = display_value;
	
									endPt[pt_index].X = End;
									endPt[pt_index].Y = display_value;
								}

								pt_index++;
								noOfLines++;
	
								if (noOfLines >= MAX_GRIDLINES)		break ;

								major_count = divisions ;
							}                                              
						}        
					}
				}
			}

			public void DrawLines(Graphics g)
			{
				if (enabled && noOfLines > 0)
				{
					Pen			pen			= new Pen(color, 1) ;

					for (int index = 0; index < noOfLines; index++)
					{
						g.DrawLine(pen, startPt[index], endPt[index]) ;
					}
					pen.Dispose() ;
				}
			}
		}
		#endregion

		#region CLASS: Crosshairs
		private class Crosshairs
		{
			public	bool		enabled;
			public	bool		displayed;
			public	bool		needDraw;
			public	Point		nextPosition;		// Next position to be drawn at.
			public	Point		currentPosition;	// Displayed position.

			public Crosshairs()
			{
				enabled			= false ;
				displayed		= false ;
				needDraw		= false ;
				nextPosition	= Point.Empty ;
				currentPosition	= Point.Empty ;
			}

			//returns whether to capture mouse or not
			public bool Setup(int X, int Y, Rectangle PaperRect, Point ScreenPoint)
			{
				bool		capture		= false ;

				lock (this)
				{
					if (PaperRect.Contains(X, Y))
					{
						capture = enabled ;

						if (capture)                               
						{
							nextPosition = ScreenPoint ;
							needDraw = true;
						}
						else
						{
							needDraw = false;
						}
					}
					else
					{
						capture = false ;
						needDraw = false;
					}
				}

                return capture ;                            
			}

			public void Draw(Rectangle ScreenRect, Color BackColor, bool Draw, bool Update)
			{
				lock(this)
				{
					if (!Draw)
					{
						if (displayed)
						{
							DrawCrosshairs(ScreenRect, currentPosition, BackColor);
							displayed = false;
						}	
					}
					else if (needDraw)
					{
						if (Update)
						{   
							DrawCrosshairs(ScreenRect, nextPosition, BackColor);
							currentPosition = nextPosition;
						}
						else
						{
							DrawCrosshairs(ScreenRect, currentPosition, BackColor);
						}

						displayed = true;
					}
				}
			}

			private void DrawCrosshairs(Rectangle PaperRect, Point Position, Color BackColor)
			{
				Point			left		= new Point(PaperRect.Left, Position.Y) ;
				Point			right		= new Point(PaperRect.Right, Position.Y) ;
				Point			top			= new Point(Position.X, PaperRect.Top) ;
				Point			bottom		= new Point(Position.X, PaperRect.Bottom) ;

				ControlPaint.DrawReversibleLine(left, right, BackColor) ;
				ControlPaint.DrawReversibleLine(top, bottom, BackColor) ;
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
						if ((key == null) || (key != "A72F256C9A14" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "A72F256C9A14" || key == "4CD0571EE68D")) key = "{A72F256C-9A14-4bf8-8569-0C4D842C2180}" ; 
						context.SetSavedLicenseKey (typeof(XYGraph), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(XYGraph), null) ;
					if (key == "{A72F256C-9A14-4bf8-8569-0C4D842C2180}") licence = new License ("Run") ;
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
		private		const int		MIN_OFFSET			= 10;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Scale[]				scale ;		
		private bool				scaleWidthIsPercent ;
		private int					scaleWidth ;
		private Grid				horizontalGrid ;
		private Grid				verticalGrid ;
        private XYTraceCollection   traces;
        private XYStaticTraceCollection staticTraces;

        private DisplayUpdateEventHandler displayUpdateEventHandler;
		private PropertyChangedEventHandler	propertyHdlr ;
		private ObjectSearchEventHandler	searchHdlr ;
		private EventPool.UpdateEventHandler updateEventHandler;
		
		private bool				recalculateRequired ;
		private int[]				scaleWidthPixels ;
		private	Rectangle			paperRect ;
		private Region				paperRegion ;
		private Crosshairs			crosshairs ;

		private bool		needGraphRefresh ;			// Graph area needs drawing.
		private bool		needUpdateDisplay ;			// TRUE - update_draw rather than redraw
		private	bool		needTracesErasing ;			// At least one trace segment needs erasing.
		private bool		needPensErasing ;			// At least one pen needs erasing.
		private bool		needGridlinesDrawing ;		// At least one set of gridlines needs drawing.
		//private bool		needThresholdlinesDrawing ;	// At least one threshold line needs drawing.
		private	bool		needTracesDrawing ;			// At least one trace segment needs drawing.
		private bool		needPensDrawing ;			// At least one pen needs drawing.
		private bool		freeze ;

        static private EventPool updatePool = new EventPool(10, "ZYGraphs 10 ms KineticaRT EventPool Worker");

		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Creates a new instance of the XYGraph class
		/// </summary>
		public XYGraph()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
            
            CheckForIllegalCrossThreadCalls = false;
            //DoubleBuffered = true;

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#endif

			scaleWidthIsPercent		= false ;
			scaleWidth				= 30 ;
			horizontalGrid			= new Grid() ;
			verticalGrid			= new Grid() ;

			scale = new Scale[4] ;
			scaleWidthPixels = new int[4] ;

			scale[(int)ScaleSides.Left]	= new Scale(1) ;
			scale[(int)ScaleSides.Right] = new Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;
			scale[(int)ScaleSides.Top]	= new Scale(1) ;
			scale[(int)ScaleSides.Bottom]= new Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;

			traces = new XYTraceCollection() ;
            staticTraces = new XYStaticTraceCollection();

			//connect to children
			propertyHdlr = new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch) ;
            displayUpdateEventHandler = new DisplayUpdateEventHandler(OnDisplayUpdate);

			for (int index = 0; index < 4; index++)
			{
				scale[index].PropertyChanged	+= propertyHdlr ;
				scale[index].ObjectSearch		+= searchHdlr ;

				scaleWidthPixels[index] = 30 ;
			}

			traces.PropertyChanged	+= propertyHdlr ;
			traces.ObjectSearch		+= searchHdlr ;

            staticTraces.DisplayUpdate += displayUpdateEventHandler;
            //staticTraces.PropertyChanged += propertyHdlr;
            staticTraces.ObjectSearch += searchHdlr;


			recalculateRequired = true ;
			paperRect = Rectangle.Empty ;
			paperRegion = null ;

			crosshairs	= new Crosshairs() ;

			needGraphRefresh			= false ;
			needUpdateDisplay			= true ;
			ResetFlags(true, false) ;

			////connect update loop
			//updatePool.UpdateEvent += new EventPool.UpdateEventHandler(OnUpdate);
			//connect update loop
			updateEventHandler = new EventPool.UpdateEventHandler(OnUpdate);
			updatePool.UpdateEvent += updateEventHandler ; 

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
					updatePool.UpdateEvent -= updateEventHandler ; 
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
			// XYGraph
			// 
			this.Name = "XYGraph";
			this.Size = new System.Drawing.Size(200, 200);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets whether the display is frozen.
		/// </summary>
		[Category("XYGraph")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the display is frozen")]
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

//					if (!freeze)
//					{
//						valueIndex = (int)((valueIndex + 2) % MAX_DATA_VALUES) ;
//						scrollAmount += 2 ;
//
//						previousUpdateTime = System.Environment.TickCount ;
//					}
					AddToQueue(false) ;
					OnPropertyChanged("Freeze") ;
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
        /// Gets or Sets if data that can not be displayed at the current scaling is discarded or stored.
        /// </summary>
        /// <remarks>
        /// If you are going to "Zoom in" by changing the Scale.Minimum or Scale.Maximum properties then you may want to set this property to false.
        /// Setting this property to false will require additional memory and processing power for storage of the additional data.
        /// </remarks>
        [Category("XYGraph")]
        [DefaultValue(true)]
        [Description("Gets or Sets if data that can not be displayed at the current scaling is discarded or stored.")]
        public bool DiscardHiddenData
        {
            get
            {
                return discardHiddenData;
            }

            set
            {
                if (discardHiddenData != value)
                {
                    discardHiddenData = value;
                    OnPropertyChanged("DiscardHiddenData");
                }
            }
        }

        bool discardHiddenData = true;

		/// <summary>
		/// Gets or sets whether the ScaleWidth property refers to pixels or a percentage of the total width of the Control
		/// </summary>
		[Category("XYGraph")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the ScaleWidth property refers to pixels or a percentage of the total width of the Control")]
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
		/// Gets or sets the width of the Scale objects in pixels or as a percent depending on ScaleWidthIsPercent
		/// </summary>
		[Category("XYGraph")]
		[DefaultValue(30)]
		[Description("Gets or sets the width of the Scale objects in pixels or as a percent depending on ScaleWidthIsPercent")]
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
		///Gets the Left hand Scale object
		///</summary>
		[Category("XYGraph")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Left hand Scale object")]
		public KineticaRT.Controls.Scale LeftScale
		{
			get 
			{
				return scale[(int)ScaleSides.Left] ;
			}
		}
		private bool ShouldSerializeLeftScale()
		{
			return true ;		//always save the scale
		}

		///<summary>
		///Gets the Right hand Scale object
		///</summary>
		[Category("XYGraph")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Right hand Scale object")]
		public KineticaRT.Controls.Scale RightScale
		{
			get 
			{
				return scale[(int)ScaleSides.Right] ;
			}
		}
		private bool ShouldSerializeRightScale()
		{
			return true ;		//always save the scale
		}

		///<summary>
		///Gets the Top Scale object
		///</summary>
		[Category("XYGraph")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Top Scale object")]
		public KineticaRT.Controls.Scale TopScale
		{
			get 
			{
				return scale[(int)ScaleSides.Top] ;
			}
		}
		private bool ShouldSerializeTopScale()
		{
			return true ;		//always save the scale
		}

		///<summary>
		///Gets the Bottom Scale object
		///</summary>
		[Category("XYGraph")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the Bottom Scale object")]
		public KineticaRT.Controls.Scale BottomScale
		{
			get 
			{
				return scale[(int)ScaleSides.Bottom] ;
			}
		}
		private bool ShouldSerializeBottomScale()
		{
			return true ;		//always save the scale
		}

		/// <summary>
		/// Gets or sets the color of the Horizontal grid lines
		/// </summary>
		[Category("XYGraph")]
		[Description("Gets or sets the color of the Horizontal grid lines")]
		public Color HorizontalGridColor
		{
			get
			{
				return horizontalGrid.color;
			}
			set
			{
				if (horizontalGrid.color != value)
				{
					horizontalGrid.color = value;
					AddToQueue(false) ;
					OnPropertyChanged("HorizontalGridColor") ;
				}
			}
		}
		private bool ShouldSerializeHorizontalGridColor()
		{
			return horizontalGrid.color != Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or sets the color of the Vertical grid lines
		/// </summary>
		[Category("XYGraph")]
		[Description("Gets or sets the color of the Vertical grid lines")]
		public Color VerticalGridColor
		{
			get
			{
				return verticalGrid.color;
			}
			set
			{
				if (verticalGrid.color != value)
				{
					verticalGrid.color = value;
					AddToQueue(false) ;
					OnPropertyChanged("VerticalGridColor") ;
				}
			}
		}
		private bool ShouldSerializeVerticalGridColor()
		{
			return verticalGrid.color != Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or sets whether the horizontal grid is enabled
		/// </summary>
		[Category("XYGraph")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the horizontal grid is enabled")]
		public bool HorizontalGridEnabled
		{
			get
			{
				return horizontalGrid.enabled ;
			}
			set
			{
				if (horizontalGrid.enabled != value)
				{
					horizontalGrid.enabled = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("HorizontalGridEnabled") ;
				}
			}
		}

        /// <summary>
        /// Gets or sets whether the vertical grid is enabled
        /// </summary>
        [Category("XYGraph")]
        [DefaultValue(true)]
        [Description("Gets or sets whether the vertical grid is enabled")]
        public bool VerticalGridEnabled
        {
            get
            {
                return verticalGrid.enabled;
            }
            set
            {
                if (verticalGrid.enabled != value)
                {
                    verticalGrid.enabled = value;
                    recalculateRequired = true;
                    AddToQueue(false);
                    OnPropertyChanged("VerticalGridEnabled");
                }
            }
        }

        private bool crosshairsEnabled = true;

        /// <summary>
        /// Gets or sets whether the Croshairs are enabled. (Allows them to be displayed when the user clicks on the graph.)
        /// </summary>
        [Category("XYGraph")]
        [DefaultValue(true)]
        [Description("Gets or sets whether the croshairs are enabled")]
        public bool CrosshairsEnabled
        {
            get
            {
                return crosshairsEnabled;
            }
            set
            {
                if (crosshairsEnabled != value)
                {
                    crosshairsEnabled = value;
                    recalculateRequired = true;
                    AddToQueue(false);
                    OnPropertyChanged("CrosshairsEnabled");
                }
            }
        }


        ///<summary>
        ///Gets or sets the Collection of XYTrace objects
        ///</summary>
        [Category("XYGraph")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Gets or sets the Collection of XYTrace objects")]
        public XYTraceCollection Traces
        {
            get
            {
                return traces;
            }
            set
            {
                if (traces != null)
                {
                    traces.PropertyChanged -= propertyHdlr;
                    traces.ObjectSearch -= searchHdlr;
                }

                traces = value;
                if (traces != null)
                {
                    traces.PropertyChanged += propertyHdlr;
                    traces.ObjectSearch += searchHdlr;
                }

                OnPropertyChanged("Traces");
            }
        }
        private bool ShouldSerializeTraces()
        {
            return !traces.IsDefaultState();
        }

        ///<summary>
        ///Gets or sets the Collection of XYTrace objects
        ///</summary>
        [Category("XYGraph")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Description("Gets or sets the Collection of StaticTrace objects")]
        public XYStaticTraceCollection StaticTraces
        {
            get
            {
                return staticTraces;
            }
            set
            {
                if (staticTraces != null)
                {
                    staticTraces.DisplayUpdate -= displayUpdateEventHandler;
                    //staticTraces.PropertyChanged -= propertyHdlr;
                    staticTraces.ObjectSearch -= searchHdlr;
                }

                staticTraces = value;
                if (staticTraces != null)
                {
                    staticTraces.DisplayUpdate += displayUpdateEventHandler;
                    //staticTraces.PropertyChanged += propertyHdlr;
                    staticTraces.ObjectSearch += searchHdlr;
                }

                OnPropertyChanged("StaticTraces");
            }
        }
        private bool ShouldSerializeStaticTraces()
        {
            return !staticTraces.IsDefaultState();
        }

        #endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// Called from OnPaint and the Control's event pool Update
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
            //lock (this) //ft ajout 
            {
                bool redraw = true;

                CalculateDisplayRectangle();

                if (resized)
                {
                    recalculateRequired = true;
                    resized = false;
                }

                if (FullRedraw)
                {
                    base.Repaint(FullRedraw, g);
                    redraw = false;
                }

                //ensure remainder only draws inside display rect
                SetDisplayRegion(g);

                lock (this)
                {
                    if (recalculateRequired)
                    {
                        Recalculate();
                        recalculateRequired = false;
                        redraw = false;
                    }

                    if (!redraw)
                    {
                        //draw all internal bits - including the background
                        g.FillRectangle(backColorBrush, DisplayRectangle);

                        DrawScales(g);

                        ResetFlags(true, false);
                        crosshairs.displayed = false;

                        //now only inside paper region
                        System.Drawing.Region rgn = g.Clip;

                        rgn.Intersect(paperRegion);
                        g.Clip = rgn;
                    }
                    else		//just draw the paper parts that need it
                    {
                        System.Drawing.Region rgn = g.Clip;

                        rgn.Intersect(paperRegion);
                        g.Clip = rgn;

                        needUpdateDisplay = true;

                        if (needGraphRefresh)
                        {
                            //background
                            g.FillRectangle(backColorBrush, DisplayRectangle);

                            //foreground
                            ResetFlags(true, false);
                            crosshairs.displayed = false;
                        }
                    }

                    DrawLines(g);

#if KRT_license
				if (demoMode)		DrawDemoText(g, DisplayRectangle, BackColor) ;
#endif

                    needUpdateDisplay = false;
                    needGraphRefresh = false;
                    ResetFlags(false, false);
                }
            }
		}

		private void DrawScales(Graphics g)
		{
			Rectangle		rect = new Rectangle() ;

			rect.Y		= paperRect.Y ;
			rect.Height	= paperRect.Height - 1 ;

			if (scale[(int)ScaleSides.Left].Enabled)
			{
				rect.X = DisplayRectangle.X - 1 ;
				rect.Width = scaleWidthPixels[(int)ScaleSides.Left] ;
				scale[(int)ScaleSides.Left].Draw(g, rect, ScaleOrientations.Vertical, this.Font, 0) ;
			}

			if (scale[(int)ScaleSides.Right].Enabled)
			{
				rect.X = paperRect.Right ;
				rect.Width	= scaleWidthPixels[(int)ScaleSides.Right] ;
				scale[(int)ScaleSides.Right].Draw(g, rect, ScaleOrientations.Vertical, this.Font, 0) ;
			}

			rect.X		= paperRect.X ;
			rect.Width	= paperRect.Width - 1 ;

			if (scale[(int)ScaleSides.Top].Enabled)
			{
				rect.Y		= DisplayRectangle.Y - 1 ;
				rect.Height = scaleWidthPixels[(int)ScaleSides.Top] ;
				scale[(int)ScaleSides.Top].Draw(g, rect, ScaleOrientations.Horizontal, this.Font, 0) ;
			}

			if (scale[(int)ScaleSides.Bottom].Enabled)
			{
				rect.Y		= paperRect.Bottom ;
				rect.Height	= scaleWidthPixels[(int)ScaleSides.Bottom] ;
				scale[(int)ScaleSides.Bottom].Draw(g, rect, ScaleOrientations.Horizontal, this.Font, 0) ;
			}
		}

		private void DrawLines(Graphics g)
		{
			// Erase crosshairs.
			crosshairs.Draw(RectangleToScreen(paperRect), BackColor, false, false) ;

			// Erase pens and old trace segments.
			if (needUpdateDisplay)
			{
				bool erased ;

				erased = traces.Erase(g, needPensErasing, needTracesErasing, BackColor) ;

				if (erased)
				{
					needGridlinesDrawing		= true;
					//					needThresholdlinesDrawing	= true;
					needTracesDrawing			= true;
					needPensDrawing				= true;
				}
			}

			traces.ProgressDisplayInfo(true) ;

			// Draw pens now in new position to minimise flickering, they are drawn again at the end
			// of draw_lines so that they are drawn over gridlines etc.
			if (needPensDrawing)
			{
				traces.DrawPens(g) ;
			}

			// Draw gridlines.
			if (needGridlinesDrawing)
			{
				//				bool		gridlines_drawn		= false;	// true => Some gridlines have been drawn this time.

				horizontalGrid.DrawLines(g) ;
				verticalGrid.DrawLines(g) ;

				//				if (gridlines_drawn)
				//				{
				//					thread_data.need.thresholdlines_drawing = true;
				//					thread_data.need.traces_drawing = true;
				//					thread_data.need.pens_drawing = true;
				//				}
			}

//			// Draw threshold lines
//			if (thread_data.need.thresholdlines_drawing)
//			{
//				BOOL		thold_drawn ;
//		
//				tholds->Draw((long)hdc, &thold_drawn, CKRT_Ptr<IDispatch>(parent)) ;
//				if (thold_drawn)
//				{
//					thread_data.need.traces_drawing = true;
//					thread_data.need.pens_drawing = true;
//				}
//			}

			// Draw crosshairs.
			crosshairs.Draw(RectangleToScreen(paperRect), BackColor, true, needUpdateDisplay) ;

			// Draw traces.
			if (needTracesDrawing)
			{
                staticTraces.Draw(g, BackColor);
                
                if (traces.Draw(g, BackColor))
				{
					needPensDrawing = true ;
				}
			}

			// Draw pens.
			if (needPensDrawing)
			{
				traces.DrawPens(g) ;
			}

			traces.ProgressDisplayInfo(false) ;
		}
		#endregion

		#region Calculations
		private void ResetFlags(bool Draw, bool Erase)
		{
			needTracesErasing			= Erase ;
			needPensErasing				= Erase ;
			needGridlinesDrawing		= Draw ;
			//needThresholdlinesDrawing	= Draw ;
			needTracesDrawing			= Draw ;
			needPensDrawing				= Draw ;
		}

		private void Recalculate()
		{
            lock (this)
            {
                Rectangle lastSize = this.paperRect;

                // calculate the graph rectangle
                int x_width = scaleWidthIsPercent ? (int)(DisplayRectangle.Width * scaleWidth / 100) : scaleWidth;
                int y_width = scaleWidthIsPercent ? (int)(DisplayRectangle.Height * scaleWidth / 100) : scaleWidth;

                scaleWidthPixels[(int)ScaleSides.Top] = (scale[(int)ScaleSides.Top].Enabled) ? y_width : MIN_OFFSET;
                scaleWidthPixels[(int)ScaleSides.Left] = (scale[(int)ScaleSides.Left].Enabled) ? x_width : MIN_OFFSET;
                scaleWidthPixels[(int)ScaleSides.Right] = (scale[(int)ScaleSides.Right].Enabled) ? x_width : MIN_OFFSET;
                scaleWidthPixels[(int)ScaleSides.Bottom] = (scale[(int)ScaleSides.Bottom].Enabled) ? y_width : MIN_OFFSET;

                paperRect = DisplayRectangle;
                paperRect.X += scaleWidthPixels[(int)ScaleSides.Left];
                paperRect.Width -= (scaleWidthPixels[(int)ScaleSides.Left] + scaleWidthPixels[(int)ScaleSides.Right]);

                paperRect.Y += scaleWidthPixels[(int)ScaleSides.Top];
                paperRect.Height -= (scaleWidthPixels[(int)ScaleSides.Top] + scaleWidthPixels[(int)ScaleSides.Bottom]);

                //			if ((thread_data.graph_rectangle.left >= thread_data.graph_rectangle.right) ||
                //				(thread_data.graph_rectangle.top >= thread_data.graph_rectangle.bottom)	)
                //			{
                //				//			enabled = (bool) false ;
                //				for(int index = 0; index < XYGRAPH_MAX_SCALES; index++)
                //				{
                //					//				scale[index]->put_Enabled(enabled) ;
                //					//				thread_data.scale_enabled[index] = enabled.boolVal ;
                //					thread_data.scale_enabled[index] = false ;
                //				}
                //			
                //				thread_data.graph_rectangle.left	= scale_x (0);
                //				thread_data.graph_rectangle.right	= scale_x (1);
                //				thread_data.graph_rectangle.top	= scale_y (0);
                //				thread_data.graph_rectangle.bottom	= scale_y (1);
                //			}

                if (paperRegion != null)
                {
                    paperRegion.Dispose();
                    paperRegion = null;
                }

                paperRegion = new Region(paperRect);	//originally right and bottom +1

                //traces
                traces.Recalculate();

                //thresholds
                //			tholds->CalculateLines(thread_data.graph_rectangle) ;

                //grid lines
                CalculateGridlines();

                //float x_scale = (float)this.paperRect.Width / (float)lastSize.Width;
                //float y_scale = (float)this.paperRect.Height / (float)lastSize.Height;
                //traces.ScaleData(lastSize.Left, lastSize.Bottom, paperRect.Left, paperRect.Bottom, x_scale, y_scale);
                //staticTraces.ScaleData(lastSize.Left, lastSize.Bottom, paperRect.Left, paperRect.Bottom, x_scale, y_scale);
            }
        }

		private void CalculateGridlines()
		{
            Scale horizontal_scale = null;
            Scale vertical_scale = null;

			if (this.scale[(int)ScaleSides.Left].Enabled)
			{
                horizontal_scale = this.scale[(int)ScaleSides.Left];
			}
			else if (this.scale[(int)ScaleSides.Right].Enabled)
			{
                horizontal_scale = this.scale[(int)ScaleSides.Right];
			}
			//else gridlines will not be drawn.

            if (horizontalGrid.enabled) horizontalGrid.CalculateLines(horizontal_scale, paperRect.Left, paperRect.Right - 1, paperRect.Bottom - 1, -(paperRect.Height - 1), false);

            if (this.scale[(int)ScaleSides.Bottom].Enabled)
			{
                vertical_scale = this.scale[(int)ScaleSides.Bottom];
			}
			else if (this.scale[(int)ScaleSides.Top].Enabled)
			{
                vertical_scale = this.scale[(int)ScaleSides.Top];
			}
			//else gridlines will not be drawn.

            if (verticalGrid.enabled) verticalGrid.CalculateLines(vertical_scale, paperRect.Top, paperRect.Bottom - 1, paperRect.Left, paperRect.Width - 1, true);

            staticTraces.Recalculate(vertical_scale,horizontal_scale, paperRect.Left, paperRect.Right - 1, paperRect.Bottom - 1, paperRect.Top);
        }

		private void OnUpdate()
		{
			lock(this)
			{
				//leave in pool for next time
			
				if (freeze)		return ;
				if (this.DesignMode)		return ;

				int			current_time		= Environment.TickCount ;

                traces.Update(current_time, paperRect, scale, ref needTracesErasing, ref needTracesDrawing, ref needPensErasing, ref needPensDrawing, ref needGraphRefresh, discardHiddenData);

				//demand a redraw on the drawing thread, not the timing thread
				AddToQueue(false) ;
			}
		}

		#endregion

		#region Mouse Events
		/// <summary>
		/// Mouse Down event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
                if (crosshairsEnabled)
                {
                    //enable or disable crosshairs
                    crosshairs.enabled = !crosshairs.enabled;
                    Capture = crosshairs.Setup(e.X, e.Y, paperRect, this.PointToScreen(new Point(e.X, e.Y)));

                    if (crosshairs.needDraw || crosshairs.displayed)
                    {
                        AddToQueue(false);
                    }
                }
			}
			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Move event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Capture = crosshairs.Setup(e.X, e.Y, paperRect, this.PointToScreen(new Point(e.X,e.Y))) ;
			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse up event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Capture = false ;
			base.OnMouseUp(e) ;
		}
		#endregion

        private void OnDisplayUpdate (object sender, DisplayUpdateMode mode)
        {
            switch (mode)
            {
                case DisplayUpdateMode.Redraw:
                    AddToQueue(true);
                    break;

                case DisplayUpdateMode.Update:
                    AddToQueue(false);
                    break;
            }
        }
        
        #region Property Changed handling
        /// <summary>
		/// Handles PropertyChanged events from sub-objects
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "XValue" && e.PropertyName != "YValue")
			{
				recalculateRequired = true ;
			}
            
            //if (sender == LeftScale && e.PropertyName == "Maximum")
            //{
            //    foreach (XYTrace trace in traces)
            //    {
            //        if (trace.YRangeSource == YRangeSources.LeftScale)
            //        {
            //            traces.ScaleData(paperRect.X, paperRect.Y, paperRect.X, paperRect.Y, 2.0F, 2.0F);
            //        }
            //    }

            //}

            AddToQueue(false) ;	//no child properties need background and border to be redrawn
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			for (int index = 0; index < 4; index++)
			{
				if (scale[index] != null)		scale[index].Connect() ;
			}
			traces.Connect () ;
            staticTraces.Connect();
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			for (int index = 0; index < 4; index++)
			{
				if (scale[index] != null)		scale[index].Disconnect() ;
			}
			traces.Disconnect () ;
            staticTraces.Disconnect();
		}
		#endregion

		/// <summary>
		/// Clears the live data from the graph.
		/// </summary>
		public void Clear ()
		{
			traces.ClearData (true) ;
			needTracesErasing			= true ;
			needPensErasing				= true ;
			AddToQueue(false) ;
		}
	}
}

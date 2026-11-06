using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using KineticaRT.Values;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Graph control which displays an array of values as bars
	/// </summary>
	/// <remarks>Uses values from a KineticaRT.Values.DoubleArray</remarks>
	[DefaultProperty("Value")]
#if KRT_license
	[LicenseProviderAttribute(typeof(BarGraph.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(BarGraph))]
	[ChangeRecord ("1.0", "01 01 2003", "S. Child", "Original version")] 
	public class BarGraph : KineticaRT.Controls.Control
	{
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
						if ((key == null) || (key != "27C2D5B40AFE" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "27C2D5B40AFE" || key == "4CD0571EE68D")) key = "{27C2D5B4-0AFE-4bcf-973A-598BE7B3465A}" ; 
						context.SetSavedLicenseKey (typeof(BarGraph), key) ;
						// {4CD0571E-E68D-47b5-92DD-D75D7861564E}	
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(BarGraph), null) ;
					if (key == "{27C2D5B4-0AFE-4bcf-973A-598BE7B3465A}") licence = new License ("Run") ;
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
		const double		VERY_SMALL_DOUBLE	= 0.0000000001 ;
		const int			BORDER_WIDTH		= 5 ;
		const int			LABEL_HEIGHT		= 15 ;
		const int			DIGIT_WIDTH			= 5 ;		//width of number (pixels): 4 + 1 gap
		const int			DIGIT_HEIGHT		= 7 ;		//height of number (pixels)

		static Font			textFont	= new Font("Tahoma", 7) ;

		private class BarPosition
		{
			public Rectangle	Bar ;
			public Rectangle	Top ;
			public Rectangle	Bottom ;
			public Rectangle	Label ;
			public Rectangle	Number ;
		} ;

		//Required Designer variable
		private System.ComponentModel.IContainer components = null;

		//property variables
		private bool						scaleWidthIsPercent ;
		private int							scaleWidth ;
		private KineticaRT.Controls.Scale	leftScale ;
		private KineticaRT.Controls.Scale	rightScale ;
		private Channels.IChannel			sourceChannel ;
		private string						source ;
		private object						value ;
		private int							gap ;
		private int							dividerEvery ;
		private int							barWidth ;
		private bool						labelsEnabled ;
		private bool						numbersEnabled ;
		private Color						numberColor ;
		private Color						labelColor ;
		private Color						barColor ;
//		private String[]					labels ;
//		private Color[]						colors ;
		private bool						useSourceFormat ;

		//internal variables
		private bool				recalculateRequired ;
		private bool				propertyChanged ;
		private bool				newValue ;
		private bool				allVisible ;
		private bool				canUpdateNow ;
		private bool				newNumber ;
		private int					leftScaleWidth ;
		private int					rightScaleWidth ;
		private Values.Value		krtValue ;				//double array or type mismatch
		private BarPosition []		barPositioning ;
		private Rectangle			paperRect ;
		private int					numberHeight ;
		private int					lastNumberUpdate ;
		private bool				crosshairsVisible ;
		private int					crosshairsYPos ;
		private bool				crosshairsMoved ;
		private int					actualBarWidth ;
		
		private Channels.ValueChangedEventHandler	srcHdlr ;
		private PropertyChangedEventHandler			srcPropertyHdlr ;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the BarGraph class
		/// </summary>
		public BarGraph()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#endif

            //AllowDrop			= true ; SB removed because it is overriding the user values
			scaleWidthIsPercent = false ;
			scaleWidth			= 30 ;
			sourceChannel		= null ;
			source				= "" ;
			value				= null ;
			gap					= 1 ;
			dividerEvery		= 4 ;
			barWidth			= 4 ;
			numbersEnabled		= true ;
			labelsEnabled		= false ;
			labelColor			= Color.SteelBlue ;
			barColor			= Color.SteelBlue ;
			numberColor			= Color.SteelBlue ;
//			labels				= null ;
//			colors				= null ;
			useSourceFormat		= true ;

			leftScale	= new KineticaRT.Controls.Scale(1) ;
			rightScale	= new KineticaRT.Controls.Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;

			recalculateRequired	= true ;
			propertyChanged		= true ;
			newValue			= true ;
			allVisible			= false ;
			canUpdateNow		= true ;
			newNumber			= true ;
			leftScaleWidth		= 0 ;
			rightScaleWidth		= 0 ;
			krtValue			= null ;
			barPositioning		= null ;
			numberHeight		= 0 ;
			lastNumberUpdate	= 0 ;
			crosshairsVisible	= false ;
			crosshairsYPos		= 0 ;
			crosshairsMoved		= false ;
			actualBarWidth		= 1 ;

			srcHdlr			= new Channels.ValueChangedEventHandler(OnValueChanged) ;
			srcPropertyHdlr = new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

			//connect to children
			leftScale.PropertyChanged	+= new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			rightScale.PropertyChanged	+= new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			leftScale.ObjectSearch		+= new ObjectSearchEventHandler(OnObjectSearch) ;
			rightScale.ObjectSearch		+= new ObjectSearchEventHandler(OnObjectSearch) ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
#if KRT_license
			if (license != null) {license.Dispose(); license = null;}
#endif

			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// BarGraph
			// 
			this.Name = "BarGraph";
			this.Size = new System.Drawing.Size(376, 150);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the type of the Control's border
		/// </summary>
		[Category("Control")]
        [DefaultValue(BorderType.Raised)]
		[Description("Gets or sets the type of the Control's border")]
        public override BorderType BorderType
		{
			get
			{
				return borderType ;
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
		[Description("Gets or Sets the width of the Control's Border")]
		public override double BorderWidth
		{
			get
			{
				return borderWidth ;
			}
			set
			{
				base.BorderWidth = value ;
				recalculateRequired = true ;
			}
		}

		/// <summary>
		/// Gets the number of bars currently available
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets the number of bars currently available")]
		public int BarCount
		{
			get
			{
				DoubleArray		array		= krtValue as DoubleArray ;

				if (array != null)
				{
					return array.ToDoubleArray().Length ;
				}
				return 0 ;
			}
		}

//		[Category("BarGraph")]
//		public String[] Labels
//		{
//			get
//			{
//				return labels ;
//			}
//			set
//			{
//				labels = value ;
//				AddToQueue(false) ;
//				OnPropertyChanged("Labels") ;
//			}
//		}
//		[Category("BarGraph")]
//		public Color[] Colors
//		{
//			get
//			{
//				return colors ;
//			}
//			set
//			{
//				colors = value ;
//				AddToQueue(false) ;
//				OnPropertyChanged("Colors") ;
//			}
//		}

		/// <summary>
		/// Gets or sets whether the ScaleWidth property refers to pixels or a percentage of the total width of the Control
		/// </summary>
		[Category("BarGraph")]
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
		/// Gets or sets the width of the scales (pixels or percent depending on ScaleWidthIsPercent)
		/// </summary>
		[Category("BarGraph")]
		[DefaultValue(30)]
		[Description("Gets or sets the width of the scales (pixels or percent depending on ScaleWidthIsPercent)")]
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
		[Category("BarGraph")]
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
		/// Resets the Scale to its default settings
		/// </summary>
		public void ResetLeftScale()
		{
			leftScale = new KineticaRT.Controls.Scale(1) ;
		}

		///<summary>
		///Gets the Right hand Scale object
		///</summary>
		[Category("BarGraph")]
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
		/// Resets the Scale to its default settings
		/// </summary>
		public void ResetRightScale()
		{
			rightScale = new KineticaRT.Controls.Scale(0, ScaleMarkPositions.Outside, ScaleMarkPositions.Outside) ;
		}

		/// <summary>
		/// Gets or sets the object that is the Source of the graph's values
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets or sets the object that is the Source of the graph's values")]
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
						sourceChannel.PropertyChanged	-= srcPropertyHdlr ;
					}

					//attach new source
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		+= srcHdlr ;
						sourceChannel.PropertyChanged	+= srcPropertyHdlr ;
						Value = sourceChannel.Value ;

						SetFormat() ;
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
		/// Gets or sets the name of the object to be the Source of the graph's values
		/// </summary>
		[Category("BarGraph")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Source of the graph's values")]
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
		/// Gets or sets the current value of the graph
		/// </summary>
		/// <remarks>Only KineticaRT.Values.DoubleArray objects are understood</remarks>
		[Category("BarGraph")]
		[Description("Gets or sets the current value of the graph")]
		[DefaultValue(0)]
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
					lock (this)
					{
						Values.Value		new_krt_value ;

						this.value = value;

						new_krt_value = value as Values.DoubleArray ;
						if ((new_krt_value == null) && (value is System.Double [])) new_krt_value = new KineticaRT.Values.DoubleArray((double [])value) ;
						if (new_krt_value == null) new_krt_value = Values.Values.TypeMismatch ;

	//					if (new_krt_value.CompareTo (krtValue) != 0)
						{
							krtValue = new_krt_value ;

							newValue = true ;
							newNumber = true ;
							AddToQueue(false) ;
						}
					}

					OnPropertyChanged("Value") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of pixels gap between bars
		/// </summary>
		[Category("BarGraph")]
		[DefaultValue(0)]
		[Description("Gets or sets the number of pixels gap between bars")]
		public int Gap
		{
			get
			{
				return gap ;
			}
			set
			{
				if (gap != value)
				{
					gap = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("Gap") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the number of bars between dividers
		/// </summary>
		/// <remarks>'Dividers' take the form of simplified versions of a Scale</remarks>
		[Category("BarGraph")]
		[DefaultValue(4)]
		[Description("Gets or sets the number of bars between dividers")]
		public int DividerEvery
		{
			get
			{
				return dividerEvery ;
			}
			set
			{
				if (dividerEvery != value)
				{
					dividerEvery = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("DividerEvery") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the width of each bar in pixels. 
		/// </summary>
		/// <remarks>'0' means the bars are sized to fit the Control.</remarks>
		[Category("BarGraph")]
		[DefaultValue(0)]
		[Description("Gets or sets the width of each bar in pixels. '0' means the bars are sized to fit the Control.")]
		public int BarWidth
		{
			get
			{
				return barWidth ;
			}
			set
			{
				if (value < 0)		value = 0 ;

				if (barWidth != value)
				{
					barWidth = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("BarWidth") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether individual bars are labelled
		/// </summary>
		/// <remarks>Labels are the numeric index of the bar</remarks>
		[Category("BarGraph")]
		[DefaultValue(false)]
		[Description("Gets or sets whether individual bars are labelled")]
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
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("LabelsEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the current value of each bar is displayed as a number
		/// </summary>
		[Category("BarGraph")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the current value of each bar is displayed as a number")]
		public bool NumbersEnabled
		{
			get
			{
				return numbersEnabled ;
			}
			set
			{
				if (numbersEnabled != value)
				{
					numbersEnabled = value ;
					recalculateRequired = true ;
					AddToQueue(false) ;
					OnPropertyChanged("NumbersEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the color of the Labels
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets or sets the color of the Labels")]
		public Color LabelColor
		{
			get
			{
				return labelColor ;
			}
			set
			{
				if (labelColor != value)
				{
					labelColor = value ;
					AddToQueue(false) ;
					OnPropertyChanged("LabelColor") ;
				}
			}
		}
		private bool ShouldSerializeLabelColor()
		{
			return labelColor != Color.SteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetLabelColor()
		{
			labelColor = Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or sets the color of the Bars
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets or sets the color of the Bars")]
		public Color BarColor
		{
			get
			{
				return barColor ;
			}
			set
			{
				if (barColor != value)
				{
					barColor = value ;
					AddToQueue(false) ;
					OnPropertyChanged("BarColor") ;
				}
			}
		}
		private bool ShouldSerializeBarColor()
		{
			return barColor != Color.SteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetBarColor()
		{
			barColor = Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or sets the color of the Numbers
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets or sets the color of the Numbers")]
		public Color NumberColor
		{
			get
			{
				return numberColor ;
			}
			set
			{
				if (numberColor != value)
				{
					numberColor = value ;
					AddToQueue(false) ;
					OnPropertyChanged("NumberColor") ;
				}
			}
		}
		private bool ShouldSerializeNumberColor()
		{
			return numberColor != Color.SteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetNumberColor()
		{
			numberColor = Color.SteelBlue ;
		}

		///<summary>
		///Gets or sets whether the Graph gets its ToolTip and scale ranges
		/// from its Source channel
		///</summary>
		[Category("BarGraph")]
		[DefaultValue(true)]
		[Description("gets or sets whether the Graph gets its ToolTip and scale ranges from its Source channel")]
		public bool UseSourceFormat
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
					OnPropertyChanged("UseSourceFormat") ;
				}
			}
		}
		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
			//ft lock (this)
			{
				if (numbersEnabled)
				{
					//is it time to redraw the numbers?
					int		now = System.Environment.TickCount ;

					if ((now - lastNumberUpdate) >= slowPool.UpdateRate)
					{
						canUpdateNow = true ;
						lastNumberUpdate = now ;
					}
				}

				CalculateDisplayRectangle() ;

				if (resized)
				{
					recalculateRequired = true ;
					resized = false ;
					FullRedraw = true ;
				}

				//base.Repaint(FullRedraw, g) ;
				g.SmoothingMode = SmoothingMode.None;

				if (FullRedraw)
				{
					needFullRedraw = false ;

					//CalculateDisplayRectangle() ;
					g.FillRectangle(backColorBrush, DisplayRectangle);
					DrawBorder(g) ;
				}

				//ensure the remainder is drawn inside the border
				SetDisplayRegion(g) ;

				if (FullRedraw || recalculateRequired)
				{
					if (recalculateRequired)
					{
						Recalculate(g) ;
					}

					if (!FullRedraw)
					{
						//this has already been done if it is a FullRedraw
						g.FillRectangle(backColorBrush, DisplayRectangle) ;
					}

					CalcBarRects(false) ;
			
					DrawScales(g) ;

					propertyChanged = true ;
					canUpdateNow = true ;
				}
				else if (newValue)
				{
					CalcBarRects(true) ;
				}

				DrawBars(g) ;

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
		}

		private void Recalculate(Graphics g)
		{
			int			overshoot		= 0;
			int			width ;

			paperRect = DisplayRectangle ;

			width = scaleWidthIsPercent ? (int)(DisplayRectangle.Width * scaleWidth / 100) : scaleWidth ;

			leftScaleWidth	= leftScale.Enabled ? width + 1 : BORDER_WIDTH ;
			rightScaleWidth	= rightScale.Enabled ? width + 1 : BORDER_WIDTH ;

			paperRect.X += leftScaleWidth ;
			paperRect.Width -= leftScaleWidth + rightScaleWidth ;

			// calculate top and bottom text overshoot
			overshoot = Math.Abs((int)(this.Font.GetHeight() / 2)) ;
	
			// aesthetics
			paperRect.Y		+= overshoot + BORDER_WIDTH ;
			paperRect.Height-= overshoot + BORDER_WIDTH ;

			//what about space for labels and values?
			numberHeight = 6 * DIGIT_WIDTH + 6 ;		//(assumes max 6 chars in number)

			if (labelsEnabled && numbersEnabled)
			{
				paperRect.Height	-= (overshoot > (numberHeight + LABEL_HEIGHT)) ? overshoot : numberHeight + LABEL_HEIGHT ;
			}
			else if (labelsEnabled)
			{
				paperRect.Height	-= (overshoot > LABEL_HEIGHT) ? overshoot : LABEL_HEIGHT ;
			}
			else if (numbersEnabled)
			{
				paperRect.Height	-= (overshoot > numberHeight) ? overshoot : numberHeight ;
			}
			else
			{
				paperRect.Height	-= overshoot ;
			}

			if (barWidth == 0)
			{
				//work out bar widths
				int		count			= BarCount ;

				if (count > 0)
				{
					int		bar_width ;
					int		total ;
					int		divider_count	= 0 ;

					//start off without taking gaps into account
					// - ie wider than we probably need
					bar_width = (int)((double)paperRect.Width / 64.0) + 1 ;
					if (bar_width < 1)		bar_width = 1 ;

					if (dividerEvery > 0)
					{
						//how many dividers? (don't need one at far right)
						divider_count = (count - ((count - 1) % dividerEvery)) / dividerEvery ;
					}
				    
					while (bar_width > 1)
					{
						//total pixels - includes gaps for dividers
						total = count * bar_width		//bars themselves
							+ (count - 1) * gap		//gaps: don't need one after the last bar
							+ 3 * divider_count ;	//dividers: 3 pixels each
				        
						if (total <= paperRect.Width)	break ;      //it fits ok
				        
						//doesn't fit - sort it out!
						if (bar_width > 1)
						{
							bar_width -- ;
						}
						//else tough - things will just have to fall off!
					}				    

					actualBarWidth = bar_width ;
				}
			}
			else
			{
				actualBarWidth = barWidth ;
			}

			recalculateRequired = false ;
		}

		private void CalcBarRects(bool ValueOnly)
		{
			if (!(krtValue is DoubleArray))
			{
				barPositioning = null ;
			}
			else		//valid value
			{
				double []		values ;
				int				count ;
				double			zero_fraction ;
				int				zero_pos ;			//actual pixel position of value '0'
				int				index ;
				int				this_gap ;
				int				left			= paperRect.Left ;

				values = ((DoubleArray)krtValue).ToDoubleArray() ;
				count = values.Length ;

				//check count against existing known bars
				if (barPositioning == null || barPositioning.Length != count)
				{
					ValueOnly = false ;
					barPositioning = new BarPosition[count] ;

					//initialise
					for (index = 0 ; index < count; index++)
					{
						barPositioning[index] = new BarPosition() ;
					}
				}

				zero_fraction = leftScale.ValueToFraction(0) ;
//				zero_pos = (int)(paperRect.Bottom - (zero_fraction * paperRect.Height)) ;
				zero_pos = (int)(paperRect.Bottom + 1 - (zero_fraction * paperRect.Height)) ;

				for (index = 0; index < count && left < paperRect.Right; index++)
				{
					this_gap = gap ;
					if (index == count - 1)
					{
						//no gap if last bar
						this_gap = 0 ;
					}
					else if (dividerEvery > 0 && ((index + 1) % dividerEvery) == 0)
					{
						//need a divider this time
						this_gap += 3 ;
					}

					//are we re-calculating everything?
					if (!ValueOnly)
					{
						barPositioning[index].Bottom	= paperRect ;
						barPositioning[index].Top		= paperRect ;
						barPositioning[index].Bar		= paperRect ;

						barPositioning[index].Bar.X		= left ;
						barPositioning[index].Bar.Width	= actualBarWidth ;

						//check rect is inside drawing space
						if (barPositioning[index].Bar.Right > paperRect.Right)
						{
							barPositioning[index].Bar.Width = paperRect.Right - barPositioning[index].Bar.Left ;
						}

						if (barPositioning[index].Bar.Right + this_gap > paperRect.Right)
						{
							this_gap = paperRect.Right - barPositioning[index].Bar.Right ;
						}

						barPositioning[index].Top.X			= barPositioning[index].Bar.X ;
						barPositioning[index].Top.Width		= barPositioning[index].Bar.Width ;
						barPositioning[index].Bottom.X		= barPositioning[index].Bar.X ;
						barPositioning[index].Bottom.Width	= barPositioning[index].Bar.Width ;

						if (labelsEnabled || numbersEnabled)
						{
							if (numbersEnabled)
							{
								barPositioning[index].Number		= barPositioning[index].Bottom ;
								barPositioning[index].Number.Y		= barPositioning[index].Bottom.Bottom + 3 ;
								barPositioning[index].Number.Height = numberHeight - 6 ;
								barPositioning[index].Number.Width	+= gap ;
							}

							if (labelsEnabled)
							{
								barPositioning[index].Label			= barPositioning[index].Bottom ;
								barPositioning[index].Label.Height	= LABEL_HEIGHT ;
								barPositioning[index].Label.Y		= DisplayRectangle.Bottom - LABEL_HEIGHT ;
								barPositioning[index].Label.Width	+= gap ;
							}
						}
					}

					//always set the value-specific bits
					if (!System.Double.IsNaN(values[index]))
					{
						double		fraction ;
						int			pos ;
						int			adjusted_zero ;		//zero_pos adjusted to get bar correct

						fraction = leftScale.ValueToFraction(values[index]) ;
						pos = (int)(paperRect.Bottom - (fraction * paperRect.Height) ) ;

						if (fraction < zero_fraction || pos == zero_pos)
						{
							adjusted_zero = zero_pos - 1 ;
						}
						else
						{
							adjusted_zero = zero_pos ;
						}

						//convert to rect top and bottom
						if ((fraction > 1 && zero_fraction > 1) || (fraction < 0 && zero_fraction < 0))
						{
							//neither in range
							barPositioning[index].Bar.Y		= 0 ;
							barPositioning[index].Bar.Height= 0 ;
						}
						else if (fraction > 1 || zero_fraction > 1)		//one off top
						{
							barPositioning[index].Bar.Y = paperRect.Y ;

							if (zero_fraction > 1)
							{
								barPositioning[index].Bar.Height = (fraction < 0) ? paperRect.Bottom : pos ;
							}
							else
							{
								barPositioning[index].Bar.Height = (zero_fraction < 0) ? paperRect.Bottom : adjusted_zero ;
							}
							barPositioning[index].Bar.Height -= barPositioning[index].Bar.Y ;
						}
						else if (fraction < 0 || zero_fraction < 0)		//one off bottom
						{
							barPositioning[index].Bar.Y = (zero_fraction < 0) ? pos : adjusted_zero ;
//							barPositioning[index].Bar.Height = paperRect.Height - barPositioning[index].Bar.Y ;
							barPositioning[index].Bar.Height = paperRect.Height 
									- barPositioning[index].Bar.Y + paperRect.Top ;
						}
						else			//both in range
						{
							if (adjusted_zero < pos)
							{
								barPositioning[index].Bar.Y = adjusted_zero ;
//								barPositioning[index].Bar.Height = pos - adjusted_zero ;
								barPositioning[index].Bar.Height = pos - adjusted_zero + 1 ;
							}
							else
							{
								barPositioning[index].Bar.Y = pos ;
								barPositioning[index].Bar.Height = adjusted_zero - pos ;
							}
						}
					}
					else
					{
						//invalid - don't draw it (ie no height)
						barPositioning[index].Bar.Height = 0 ;
					}

					barPositioning[index].Top.Height	= barPositioning[index].Bar.Top - barPositioning[index].Top.Top ;
					barPositioning[index].Bottom.Y		= barPositioning[index].Bar.Bottom ;
					barPositioning[index].Bottom.Height	= paperRect.Bottom - barPositioning[index].Bottom.Y ;

					left += actualBarWidth + this_gap ;
				}

				allVisible = (index < count) ? false : true ;
			}
		}

		private void DrawScales(Graphics g)
		{
			Rectangle		rect		= paperRect ;

			if (leftScale.Enabled)
			{
				rect.X = DisplayRectangle.X - 1 ;
				rect.Width = leftScaleWidth ;
				leftScale.Draw(g, rect, ScaleOrientations.Vertical, this.Font, 0) ;
			}

			if (rightScale.Enabled)
			{
				rect.X	= (barPositioning != null && allVisible) ? barPositioning[barPositioning.Length-1].Bar.Right : paperRect.Right ;
				rect.Width	= rightScaleWidth;
				rightScale.Draw(g, rect, ScaleOrientations.Vertical, this.Font, 0) ;
			}
		}

		private void DrawBars(Graphics g)
		{
			lock(this)
			{
				if (!crosshairsVisible)			crosshairsMoved = false ;

				if (!(krtValue is DoubleArray))
				{
					//unconnected
					Rectangle		rect			= paperRect ;
					StringFormat	format			= new StringFormat() ;
					Brush			brush			= new SolidBrush(this.ForeColor) ;

					if (allVisible && barPositioning != null)
					{
						rect.Width = barPositioning[barPositioning.Length - 1].Bar.Right - paperRect.Right ;
					}

					format.Alignment = StringAlignment.Center ;
					format.LineAlignment = StringAlignment.Center ;
					g.DrawString("-||-", this.Font, brush, rect, format) ;
					brush.Dispose() ;
				}
				else if (barPositioning != null && barPositioning.Length > 0 && (propertyChanged || newValue || (canUpdateNow && newNumber)) )
				{
					int							this_gap ;
					double []					values			= ((DoubleArray)krtValue).ToDoubleArray() ;
					Brush						bar_brush		= new SolidBrush(barColor) ;
					Brush						label_brush		= null ;
					Brush						number_brush	= null ;
					Rectangle					gap_rect		= paperRect ;
					KineticaRT.Controls.Scale	scale			= null ;
					int							right ;
					int							left			= paperRect.X ;
					int							index ;
					int							bar_count		= barPositioning.Length ;
					StringFormat				format			= new StringFormat() ;

					format.FormatFlags = StringFormatFlags.DirectionVertical ;
					format.Alignment = StringAlignment.Near ;
					format.LineAlignment = StringAlignment.Far ;

					//numbers...
					if (numbersEnabled)
					{
						number_brush = new SolidBrush(numberColor) ;
//NS
//						if (propertyChanged)
//						{
//							Rectangle		number_rect		= new Rectangle() ;
//
//							number_rect.Y = paperRect.Bottom + 1 ;
//							number_rect.Height = numberHeight ;
//
//							number_rect.X = paperRect.X - 2 ;
//							if (allVisible)
//							{
//								number_rect.Width = barPositioning[bar_count - 1].Bar.Right + 2 - number_rect.X ;
//							}
//							else
//							{
//								number_rect.Width = paperRect.Width + 4 ;
//							}
//
//							DrawSquareBorder(g, number_rect, BorderStyles.Indented, BackColor, 2) ;
//						}
					}

					//labels...
					if (labelsEnabled && propertyChanged)
					{
						label_brush = new SolidBrush(labelColor) ;
					}

					//dividers...
					if ((propertyChanged || crosshairsMoved) && dividerEvery > 0 && dividerEvery < barPositioning.Length)
					{
						if (leftScale.Enabled)
						{
							scale = leftScale ;
						}
						else if (rightScale.Enabled)
						{
							scale = rightScale ;
						}
					}

					//draw each bar
					right = (allVisible) ? barPositioning[barPositioning.Length-1].Bar.Right : paperRect.Right ;
					
					KineticaRT.Channels.INumericChannel		numeric_channel = this.sourceChannel as KineticaRT.Channels.INumericChannel ;
					System.Globalization.NumberFormatInfo	info			= (System.Globalization.NumberFormatInfo) System.Globalization.NumberFormatInfo.CurrentInfo.Clone () ;
					string 									numeric_format	= "N" ;
					string 									text ;

					if (numeric_channel != null)
					{
						info.NumberDecimalDigits = numeric_channel.DecimalDigits ;
						numeric_format = numeric_channel.Format ;
					}

					for (index = 0; index < bar_count && left < right; index++)
					{
						this_gap = gap ;
						if ((index + 1 < bar_count) && dividerEvery > 0 && ((index + 1) % dividerEvery) == 0)
						{
							//need a divider this time
							this_gap += 3 ;
						}

						if (propertyChanged || newValue || crosshairsMoved)
						{
							//draw main bar rect
							if (barPositioning[index].Bar.Height > 0)
							{
								g.FillRectangle(bar_brush, barPositioning[index].Bar) ;
							}
							//else no bar to draw - value out of range

							//draw background above and below bar
							if (barPositioning[index].Top.Height > 0)
							{
								g.FillRectangle(backColorBrush, barPositioning[index].Top) ;
							}
							if (barPositioning[index].Bottom.Height > 0)
							{
								g.FillRectangle(backColorBrush, barPositioning[index].Bottom) ;
							}
						}

						if (propertyChanged || crosshairsMoved)
						{
							//draw gap between bars
							gap_rect.X = barPositioning[index].Bar.Right ;

							gap_rect.Width = this_gap ;

							if (gap_rect.Right > right)
							{
								gap_rect.Width = right - gap_rect.Left ;
							}

							if (gap_rect.Width > 0)
							{
								g.FillRectangle(backColorBrush, gap_rect) ;

								if (this_gap > gap)
								{
									DrawDivider(g, gap_rect, scale) ;
								}
							}

							//labels?
							if (labelsEnabled && label_brush != null)
							{
								g.FillRectangle(backColorBrush, barPositioning[index].Label) ;
//								if (labels.Length > index)
//								{
//									g.DrawString(labels.GetValue(index).ToString(), textFont, label_brush, barPositioning[index].Label, format) ;
//								}
								g.DrawString(index.ToString(), textFont, label_brush, barPositioning[index].Label, format) ;
							}
						}

						if (numeric_channel != null)
						{
							try
							{
								text = ((IFormattable)values[index]).ToString(numeric_format, info) ;
							}
							catch (System.FormatException)
							{
								text = values[index].ToString() ; 
							}
						}
						else
						{
							text = values[index].ToString() ;
						}


						//numbers?
						if (numbersEnabled == true && canUpdateNow)
						{
							g.FillRectangle(backColorBrush, barPositioning[index].Number) ;
							g.DrawString(text, textFont, number_brush, barPositioning[index].Number, format) ;
						}

						left = barPositioning[index].Bar.Right + this_gap ;
					}

					if (canUpdateNow)
					{
						newNumber = false ;
					}

					bar_brush.Dispose() ;
					if (label_brush != null)		label_brush.Dispose() ;
					if (number_brush != null)		number_brush.Dispose() ;
				}

				propertyChanged = false ;
				canUpdateNow = false ;
				newValue = false ;

				// Draw crosshairs?
				if (crosshairsVisible)
				{
					int			right ;
					Brush		brush		= new HatchBrush(HatchStyle.LargeCheckerBoard, BackColor, barColor) ;
					Pen			pen			= new Pen(brush, 1);

					right = (allVisible) ? barPositioning[barPositioning.Length - 1].Bar.Right - 1 : paperRect.Right - 1 ;
					g.DrawLine(pen, paperRect.X, crosshairsYPos, right, crosshairsYPos) ;
					brush.Dispose() ;
					pen.Dispose() ;
				}

				crosshairsMoved = false ;
			}
		}

		//  Redraws a divider (mini-scale)
		private void DrawDivider(Graphics g, Rectangle Rect, KineticaRT.Controls.Scale BaseScale)
		{
			if (BaseScale == null)			return ;

			double		range ;
			int			left		= Rect.X + Rect.Width / 2 ;	
			Pen			pen			= new Pen(BaseScale.Color, 1) ;

			g.DrawLine(pen, left, Rect.Top, left, Rect.Bottom) ;

			left-- ;

			range = BaseScale.Maximum - BaseScale.Minimum ;

			if (range > 0)
			{
				int				display_value ;
				int				major_count ;
				int				right					= left + 3 ;
				double			fractional_value ;
				double			val ;
				double			incr ;

				incr = BaseScale.MajorMarksRepeat / BaseScale.MinorMarksDivisions ;

				// Find the first minor mark.
				for (val = BaseScale.MajorMarksStart; val > BaseScale.Minimum; val -= BaseScale.MajorMarksRepeat);

				for (major_count = 0; val <= BaseScale.Maximum; val += incr, major_count--)
				{                                       
					fractional_value = (val - BaseScale.Minimum) / range ;
					display_value    = (int)(Rect.Bottom - Rect.Height * fractional_value) ;        

					if ((val < (BaseScale.MajorMarksStart - VERY_SMALL_DOUBLE)) ||  major_count != 0 )
					{  
						if (major_count == 0) major_count = BaseScale.MinorMarksDivisions ;
					}
					else	// Major mark.
					{  
						if (display_value < Rect.Bottom)
						{
							g.DrawLine(pen, left, display_value, right, display_value) ;
						}
	    
						major_count = BaseScale.MinorMarksDivisions ;
					}                                              
				}        
			}

			pen.Dispose() ;
		}
		#endregion

		#region Other Methods
		private void MoveCrosshairs(int YPos)
		{
			if (crosshairsVisible)
			{
				int			new_pos ;
				//move the crosshairs
				if (YPos >= paperRect.Bottom)
				{
					new_pos = paperRect.Bottom - 1 ;
				}
				else if (YPos < paperRect.Top)
				{
					new_pos = paperRect.Top ;
				}
				else
				{
					new_pos = YPos ;
				}

				if (new_pos != crosshairsYPos)
				{
					crosshairsYPos = new_pos ;
					crosshairsMoved = true ;
					AddToQueue(false) ;
				}
			}
		}

		/// <summary>
		/// Sets up the tool tip and scale range based on the Source channel
		/// </summary>
		private void SetFormat()
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel	ch		= sourceChannel as Channels.IInfoChannel ;

				if (ch != null)
				{
					Channels.INumericChannel num ;

					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;

					num = ch as Channels.INumericChannel ;
					if (num != null)
					{
						leftScale.Minimum	= num.Minimum ;
						rightScale.Minimum	= num.Minimum ;
						leftScale.Maximum	= num.Maximum ;
						rightScale.Maximum	= num.Maximum ;
					}
				}
			}
		}
		#endregion

		#region Mouse Events
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				//bring up a menu
				if (this.ContextMenu == null)
				{
					MenuItem []		items ;
					items = new MenuItem [1] ;
					items[0] = new MenuItem("Crosshairs", new EventHandler(OnClickMenuCrosshairs)) ;
					this.ContextMenu = new ContextMenu(items) ;
				}
				this.ContextMenu.MenuItems[0].Checked = crosshairsVisible ;

				this.ContextMenu.Show(this, new Point(e.X, e.Y)) ;
			}
			else if (e.Button == MouseButtons.Left && crosshairsVisible)
			{
				this.Capture = true ;
				MoveCrosshairs(e.Y) ;
			}

			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (this.Capture && crosshairsVisible)
			{
				MoveCrosshairs(e.Y) ;
			}

			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (this.Capture)		this.Capture = false ;

			base.OnMouseUp(e) ;
		}

		private void OnClickMenuCrosshairs(object sender, EventArgs e)
		{
			crosshairsVisible = !crosshairsVisible ;
			MoveCrosshairs(this.PointToClient(Control.MousePosition).Y) ;

			if (!crosshairsVisible)		crosshairsMoved = true ;
		}
		#endregion

		#region Drag / Drop operations
		/// <summary>
		/// DragEnter event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnDragEnter (DragEventArgs e)
		{
			//check if allowed to drop things, and if drag object holds text
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}

			base.OnDragEnter(e) ;
		}

		/// <summary>
		/// DragDrop event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnDragDrop (DragEventArgs e)
		{
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				Source = e.Data.GetData(DataFormats.Text).ToString() ;
			}

			base.OnDragDrop(e) ;
		}
		#endregion

		#region Source Event handling (value and properties)
		//new value coming in
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value v)
		{
			//get most up to date value
			Value = sender.Value ;
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
				SetFormat() ;
			}
		}
		#endregion

		#region Property Changed handling
		/// <summary>
		/// The named property has changed: generate the PropertyChanged event
		/// </summary>
		/// <param name="propertyName">Name of property that has changed</param>
		protected override void OnPropertyChanged(string propertyName)
		{
			if (propertyName != "Value")
			{
				propertyChanged = true ;
			}
			base.OnPropertyChanged(propertyName) ;
		}

		/// <summary>
		/// Handles PropertyChanged events from sub-objects
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			recalculateRequired = true ;
			propertyChanged = true ;
			AddToQueue(false) ;	//no child properties need background and border to be redrawn
		}
		#endregion

		#region Connection

        private System.Drawing.Color originalBackColor;
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
            if (originalBackColor == null || this.BackColor != Color.Fuchsia)
            {
                originalBackColor = this.BackColor;
            }
            else
            {
                this.BackColor = originalBackColor;
            }
            if (source != "")
            {
                SourceChannel = OnObjectSearch(source) as Channels.IChannel;
                if (this.sourceChannel == null) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }
			
			if (leftScale != null)		leftScale.Connect() ;
			if (rightScale != null)		rightScale.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;

			if (leftScale != null)				leftScale.Disconnect() ;
			if (rightScale != null)				rightScale.Disconnect() ;
		}
		#endregion
	}
}


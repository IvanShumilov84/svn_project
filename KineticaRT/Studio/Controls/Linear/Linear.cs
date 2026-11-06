#region Using
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KineticaRT;
using System.Drawing.Drawing2D;

#endregion 

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// KineticaRT Studio Linear control.
	/// Displays a numeric Value from a source channel on a slider,
	/// together with an optional description and units
	/// </summary>
	#region License
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(Linear.LicenseProvider))]
#endif
	#endregion
    [ChangeRecord("2.0.1.0", "07 12 2010", "N. Savidge", "Added ValueColor property.")]
    [Copyright("Copyright 2003-2010 KineticaRT Ltd.")]
    [ToolboxBitmap(typeof(Linear))]
	public class Linear : KineticaRT.Controls.Slider, ISource //ft
	{
		#region License
#if KrtStudio_license
		// Do not move this class or its contents into another namespace - it would not be secure.
	
		////[LicenseProviderAttribute(typeof(xxx.LicenseProvider))]
		//	//exception if no license
		//	License license = null;
		//	license = LicenseManager.Validate(this.GetType(), this);
		//	if (license != null) {license.Dispose();license = null;}
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
						if ((key == null) || (key != "2E373D6DCAD1"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "2E373D6DCAD1")) key = "{2E373D6D-CAD1-485e-AB6E-F76C2D4005F8}" ; 
						context.SetSavedLicenseKey (typeof(Linear), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Linear), null) ;
					if (key == "{2E373D6D-CAD1-485e-AB6E-F76C2D4005F8}") licence = new License ("Run") ;
					else if (allowExceptions) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
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

		#region Variable Declarations
		private KineticaRT.Controls.Display valueDisplay;

		// Studio.ControlBase
		private string					customDescription ;
		private TextSources				descriptionSource ;
		private Channels.IInfoChannel	sourceChannel ;
		private string					source ;
		private string					customToolTip ;
		private TextSources				toolTipSource ;
		private	ToolTip					toolTip ;
		private new PropertyChangedEventHandler	propertyHdlr ;

		// Studio.NumericControl
		private string					customUnits ;
		private bool					useCustomUnits ;
		private string					format ;

		// Studio.ScaledControl
		private	bool					useCustomRange ;
		private double					customMinimum ;
		private double					customMaximum ;
		private	bool					useCustomDecimalPlaces ;
		private int						customDecimalPlaces ;

		/// <summary>
		/// The threshold channel that this control is connected to or null if not connected to a threshold channel.
		/// </summary>
		private KineticaRT.Channels.INumericThresholdChannel thresholdChannel ;

		// Studio.Linear
		private int				scaleWidth ;
		private ScalePositions	scalePosition ;
		private Color			inputBackColor ;
		private Color			valueBackColor ;
		private Rectangle		unitsRect ;
		private Rectangle		descriptionRect ;
		private StringFormat	stringFormat ;
		private int				descriptionHeight ;
		private int				descriptionWidth ;
		private int				descriptionLines ;
		private int				unitsHeight ;
		private int				valueHeight ;
		private int				border_spacing ;

        private Color foreColor;//ft test pour optimiser en vitesse
        private volatile bool foreColorChanged = true; //ft flag utilise pour jouer sur l'objet brushForeColor afin d'éviter des CrossThread
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Linear class
		/// </summary>
		public Linear()
		{
			#region License
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif
			#endregion

			InitializeComponent();

            // valueDisplay.AllowDrop = false; SB removed because it is overriding the user values
            // base.AllowDrop = true; SB removed because it is overriding the user values
            //this.Needle.Centred = false;
            //this.Needle.FillStyle = KineticaRT.Controls.FillStyles.Segmented;
        }

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			KineticaRT.Controls.Threshold threshold1 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold2 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold3 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold4 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.SliderNeedle sliderNeedle1 = new KineticaRT.Controls.SliderNeedle();
			this.valueDisplay = new KineticaRT.Controls.Display();
			this.SuspendLayout();
			// 
			// valueSlider
			// 
			sliderNeedle1.Centred = true;
			sliderNeedle1.Enabled = false;
            sliderNeedle1.FillStyle = KineticaRT.Controls.FillStyles.Solid;
			sliderNeedle1.FillFinish = 0.8;
			sliderNeedle1.FillStart = 0.4;
			sliderNeedle1.Finish = 0.9;
			sliderNeedle1.Start = 0.4;
			sliderNeedle1.Style = KineticaRT.Controls.SliderNeedleStyles.Line;
			sliderNeedle1.Width = 4;
			Needle = sliderNeedle1;
			Scale.MajorMarksRepeat = 20;
			Scale.MinorMarksDivisions = 4;
			Scale.Position = 0.4;
			threshold1.Enabled = false;
			threshold1.MarkColor = System.Drawing.Color.LightSeaGreen;
			threshold1.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside ;
			threshold1.MarkStyle = KineticaRT.Controls.ThresholdMarkStyles.Line;
			threshold2.Enabled = false;
			threshold2.MarkColor = System.Drawing.Color.LimeGreen;
			threshold2.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside ;
			threshold2.MarkStyle = KineticaRT.Controls.ThresholdMarkStyles.Line;
			threshold3.Enabled = false;
			threshold3.MarkColor = System.Drawing.Color.Orange;
			threshold3.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside ;
			threshold3.MarkStyle = KineticaRT.Controls.ThresholdMarkStyles.Line;
			threshold4.Enabled = false;
			threshold4.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside ;
			threshold4.MarkStyle = KineticaRT.Controls.ThresholdMarkStyles.Line;
			base.scale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {threshold1,threshold2,threshold3,threshold4});
			// 
			// valueDisplay
			// 
			this.valueDisplay.InputEnabled = false;
			this.valueDisplay.Location = new System.Drawing.Point(0, 168);
			this.valueDisplay.Name = "valueDisplay";
			this.valueDisplay.NumericOnly = true;
			this.valueDisplay.Size = new System.Drawing.Size(88, 24);
			this.valueDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Fast ;
			this.valueDisplay.TabIndex = 1;
			this.valueDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.valueDisplay.UseSourceFormat = false;
            this.valueDisplay.BorderType = KineticaRT.Controls.BorderType.Indented;
			this.valueDisplay.BorderWidth = 1 ;
            this.valueDisplay.BorderColor = Color.Blue;
			this.valueDisplay.TabStop = false ;
			this.valueDisplay.BackColor = SystemColors.ControlLight ;
			// 
			// Linear
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.valueDisplay});
			this.Name = "Linear";
			this.TabIndex = 0;
			this.UseSourceFormat = false;

			// Studio.Linear.
			stringFormat = new StringFormat() ;
			stringFormat.LineAlignment	= StringAlignment.Center ;
			stringFormat.Alignment		= StringAlignment.Near ;
			descriptionLines = 1 ;
			TabStop = false ;
			
			// Studio.ControlBase
			this.ResizeRedraw = true ;
			this.SetStyle(ControlStyles.UserPaint, true) ;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//ft
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//ft 
            // AllowDrop = true ;  SB removed because it is overriding the user values
			propertyHdlr			= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;
			toolTip = new ToolTip() ;
			toolTip.ShowAlways = true ;
			customDescription		= "" ;
			descriptionSource		= TextSources.Description ;
			source					= "" ;
			sourceChannel			= null ;
			customToolTip			= "" ;
			toolTipSource			= TextSources.ToolTip ;
			
			// Studio.NumericControl
			customUnits				= "" ;
			useCustomUnits			= false ;
			format					= "" ;

			// Studio.ScaledControl
			useCustomRange			= false ;
			customMinimum			= 0 ;
			customMaximum			= 100 ;
			useCustomDecimalPlaces	= false ;
			customDecimalPlaces		= 0 ;

			scaleWidth				= 35 ;
			scalePosition			= ScalePositions.Left ;
			inputBackColor			= SystemColors.Window ;
			valueBackColor			= System.Drawing.Color.Empty ;


            //ft pourquoi cette section en double???
            //// Studio.ControlBase
            //this.ResizeRedraw = true ;
            //this.SetStyle(ControlStyles.UserPaint, true) ;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//ft
            //AllowDrop = true ;
            //propertyHdlr			= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;
            //toolTip = new ToolTip() ;
            //toolTip.ShowAlways = true ;
            //customDescription		= "" ;
            //descriptionSource		= TextSources.Description ;
            //source					= "" ;
            //sourceChannel			= null ;
            //customToolTip			= "" ;
            //toolTipSource			= TextSources.ToolTip ;

			SetScalePosition() ;
			UpdateLayout() ;
		
			this.ResumeLayout(false);
		}
		#endregion

		#region Properties
        /// Added property N Savidge 7 Dec 2010 - copied from Studio.Controls.Display.
        /// <summary>
        /// Gets or Sets the Color of the Value text
        /// </summary>
        [Category("Linear")]
        [Description("Gets or Sets the Color of the Value text")]
        public Color ValueColor 
        {
            get
            {
                return valueDisplay.ForeColor;
            }
            set
            {
                valueDisplay.ForeColor = value;
                OnPropertyChanged("ValueColor");
            }
        }
        private bool ShouldSerializeValueColor()
        {
            return valueDisplay.ForeColor != DefaultForeColor;
        }

        
        /// <summary>
		/// Gets the Description displayed. Value depends on DescriptionSource property
		/// </summary>
		[Category("Description")]
		[Description("Gets the Description displayed. Value depends on DescriptionSource property")]
		public string Description
		{
			get
			{
				string			text	= "" ;

				if (descriptionSource == TextSources.Custom)
				{
					text = customDescription ;
				}
				else if (sourceChannel != null)
				{
					if (descriptionSource == TextSources.Description)
					{
						text = sourceChannel.Description ;
					}
					else
					{
						text = sourceChannel.ToolTip ;
					}
				}
				return text ;
			}
		}


        //ft
        /// <summary>
        /// Gets or sets whether the gradient is enabled
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets whether the gradient is enabled")]
        public override bool GradientEnabled
        {
            get
            {
                return base.gradientEnabled;
            }
            set
            {
                if (base.gradientEnabled != value)
                {
                    base.gradientEnabled = value;
                    valueDisplay.GradientEnabled = value;
                    //AddToQueue(true);
                    OnPropertyChanged("GradientEnabled");
                }
            }
        }

        //ft
        /// <summary>
        /// Gets or sets the default gradient color
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets the default gradient color")]
        public override Color DefaultGradientColor
        {
            get
            {

                return valueDisplay.DefaultGradientColor;
            }
            set
            {
                if (valueDisplay.DefaultGradientColor != value)
                {
                    valueDisplay.DefaultGradientColor = value;

                    OnPropertyChanged("DefaultGradientColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Color at the left hand side of the title bar
        /// </summary>
        [Category("Gradient")]
        [DefaultValue(typeof(Color), "Navy")]
        [Description("Gets or sets the linerar gradient mode")]
        public override LinearGradientMode LinearGradientMode
        {
            get
            {
                return base.gradientMode;
            }
            set
            {
                if (base.gradientMode != value)
                {
                    base.gradientMode = value;
                    //if (GradientEnabled) AddToQueue(false);
                    valueDisplay.LinearGradientMode = value;
                    OnPropertyChanged("LinearGradientMode");
                }
            }
        }
        /// <summary>
        /// Gets or Sets the Color of the Value text
        /// </summary>
        [Category("Display")]
        [Description("Gets or Sets the Color of the Value text")]
        public override Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
                foreColorChanged = true;
                AddToQueue(false);
                OnPropertyChanged("ForeColor");
            }
        }
        private bool ShouldSerializeForeColor()
        {
            return foreColor != Color.Empty;
        }

		/// <summary>
		/// Gets or sets the Source channel to which this object is connected
		/// </summary>
		[Category("General")]
		[Description("Gets or sets the Source channel to which this object is connected")]
		[DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Channels.IInfoChannel SourceChannel
		{
			get
			{
				return sourceChannel ;
			}
			set
			{
				if (SourceChannel != value)
				{
					Needle.SourceChannel = value ;
					valueDisplay.SourceChannel = value ;

					if (valueDisplay.InputEnabled)
					{
						valueDisplay.DestinationChannel = value ;
						Needle.DestinationChannel = value ;
					}
					else
					{
						valueDisplay.DestinationChannel = null ;
						Needle.DestinationChannel = null ;
					}

					if (sourceChannel != null)		sourceChannel.PropertyChanged -= propertyHdlr ;

					sourceChannel = value ;
					if (sourceChannel != null)		sourceChannel.PropertyChanged += propertyHdlr ;

					NewRange(Minimum, Maximum) ;
					NewDecimalPlaces(DecimalPlaces) ;

					thresholdChannel = SourceChannel as KineticaRT.Channels.INumericThresholdChannel ;
					if (thresholdChannel != null)
					{
						SetFill (thresholdChannel.Normal, thresholdChannel.NormalColor) ;
						ValueScale.Thresholds[0].Enabled = true ;
						ValueScale.Thresholds[0].Value = thresholdChannel.LowLow ;
						ValueScale.Thresholds[0].MarkColor = thresholdChannel.LowLowColor ;
						ValueScale.Thresholds[1].Enabled = true ;
						ValueScale.Thresholds[1].Value = thresholdChannel.Low ;
						ValueScale.Thresholds[1].MarkColor = thresholdChannel.LowColor ;
						ValueScale.Thresholds[2].Enabled = true ;
						ValueScale.Thresholds[2].Value = thresholdChannel.High ;
						ValueScale.Thresholds[2].MarkColor = thresholdChannel.HighColor ;
						ValueScale.Thresholds[3].Enabled = true ;
						ValueScale.Thresholds[3].Value = thresholdChannel.HighHigh ;
						ValueScale.Thresholds[3].MarkColor = thresholdChannel.HighHighColor ;

                        // Added NS 11 May 2011 - make sure that the startup colours are consistant with running colours.
                        OnSourcePropertyChanged(thresholdChannel, KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs); // Called so that only one piece of code sets the actual colours.
                    }
					else
					{
						SetFill (0, System.Drawing.Color.Empty) ;
						ValueScale.Thresholds[0].Enabled = false ;
						ValueScale.Thresholds[1].Enabled = false ;
						ValueScale.Thresholds[2].Enabled = false ;
						ValueScale.Thresholds[3].Enabled = false ;
					}

					Invalidate(DisplayRectangle) ;
					OnPropertyChanged("SourceChannel") ;

					OnPropertyChanged("Minimum") ;
					OnPropertyChanged("Maximum") ;
					OnPropertyChanged("Units") ;

					NewToolTip(ToolTip) ;

					NewRange(Minimum, Maximum) ;
					NewDecimalPlaces(DecimalPlaces) ;

					Invalidate(DisplayRectangle) ;

					NewRange(Minimum, Maximum) ;
					NewText() ;
				}
			}
		}

        private bool ShouldSerializeSourceChannel()
        {
            return false;
        }

		/// <summary>
		/// Gets or sets a Custom Description
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets a Custom Description")]
		[DefaultValue("")]
		public string CustomDescription
		{
			get
			{
				return customDescription ;
			}
			set
			{
				if (value != customDescription)
				{
					customDescription = value ;
					OnPropertyChanged("CustomDescription") ;
					
					if (descriptionSource == TextSources.Custom)
					{
						OnPropertyChanged("Description") ;
					}
				}

				if (DescriptionSource == TextSources.Custom)
				{
					Invalidate(descriptionRect) ;
				}
			}
		}


		/// <summary>
		/// Gets or sets where to get the Description from
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets where to get the Description from")]
		[DefaultValue(TextSources.Description)]
		public TextSources DescriptionSource
		{
			get
			{
				return descriptionSource ;
			}
			set
			{
				if (descriptionSource != value)
				{
					descriptionSource = value ;
					OnPropertyChanged("DescriptionSource") ;
				}

				Invalidate(descriptionRect) ;
			}
		}


		/// <summary>
		/// Gets or sets the custom Tool Tip
		/// </summary>
		[Category("ToolTip")]
		[Description("Gets or sets the custom Tool Tip")]
		[DefaultValue("")]
		public string CustomToolTip
		{
			get
			{
				return customToolTip ;
			}
			set
			{
				if (value != customToolTip)
				{
					customToolTip = value ;
					OnPropertyChanged("CustomToolTip") ;
					
					if (toolTipSource == TextSources.Custom)
					{
						NewToolTip(ToolTip) ;
					}
				}
			}
		}


		/// <summary>
		/// Gets or sets where the Tool Tip gets its value
		/// </summary>
		[Category("ToolTip")]
		[Description("Gets or sets where the Tool Tip gets its value")]
		[DefaultValue(TextSources.ToolTip)]
		public TextSources ToolTipSource
		{
			get
			{
				return toolTipSource ;
			}
			set
			{
				if (toolTipSource != value)
				{
					toolTipSource = value ;
					OnPropertyChanged("ToolTipSource") ;
					NewToolTip(ToolTip) ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the font for the digital value display,
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the font for the digital value display.")]
		public System.Drawing.Font ValueFont
		{
			get
			{
				return valueDisplay.Font ;
			}
			set
			{
				valueDisplay.Font = value ;
				UpdateLayout() ;
				OnPropertyChanged("ValueFont") ;
			}
		}


		/// <summary>
		/// Gets the current Tool Tip
		/// </summary>
		[Category("ToolTip")]
		[Description("Gets the current Tool Tip")]
		public string ToolTip
		{
			get
			{
				string			text	= "" ;

				if (toolTipSource == TextSources.Custom)
				{
					text = customToolTip ;
				}
				else if (sourceChannel != null)
				{
					if (toolTipSource == TextSources.Description)
					{
						text = sourceChannel.Description ;
					}
					else
					{
						text = sourceChannel.ToolTip ;
					}
				}
				return text ;
			}
		}


		/// <summary>
		/// Gets the current Value
		/// </summary>
		[Category("General")]
		[Description("Gets the current Value")]
		public virtual Values.Value Value
		{
			get
			{
				return (sourceChannel != null) ? sourceChannel.Value : Values.Values.NotConnected ;
			}
		}


		/// <summary>
		/// Gets or sets the name of Source channel whose value is displayed
		/// </summary>
		[Category("General")]
		[Description("Gets or sets the name of Source channel whose value is displayed")]
		[DefaultValue("")]
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
					SourceChannel = OnObjectSearch(source) as KineticaRT.Channels.IInfoChannel ;

					//force a repaint, as description etc has likely changed
					Invalidate(DisplayRectangle) ;
				}
			}
		}

		
		/// <summary>
		/// Gets or sets a Custom Units string
		/// </summary>
		[Category("Units")]
		[Description("Gets or sets a Custom Units string")]
		[DefaultValue("")]
		public string CustomUnits
		{
			get
			{
				return customUnits ;
			}
			set
			{
				if (value != customUnits)
				{
					customUnits = value ;
					OnPropertyChanged("CustomUnits") ;
					
					if (useCustomUnits)
					{
						OnPropertyChanged("Units") ;
					}
				}

				if (UseCustomUnits)
				{
					Invalidate(unitsRect) ;
				}
			}
		}


		/// <summary>
		/// Gets or sets whether to use the Custom units string or the Source channel's value
		/// </summary>
		[Category("Units")]
		[Description("Gets or sets whether to use the Custom units string or the Source channel's value")]
		[DefaultValue(false)]
		public bool UseCustomUnits
		{
			get
			{
				return useCustomUnits ;
			}
			set
			{
				if (useCustomUnits != value)
				{
					useCustomUnits = value ;
					OnPropertyChanged("UseCustomUnits") ;
					OnPropertyChanged("Units") ;
				}
				Invalidate(unitsRect) ;
			}
		}


		/// <summary>
		/// Gets or sets the text formatting of the Value
		/// If specified (ie non-blank string), overrides the format of the Source
		/// </summary>
		[Category("General")]
		[Description("If specified overrides Format of Source. Eg. C,E,F3,G6,N")]
		[DefaultValue("")]
		public string Format
		{
			get
			{
				return format ;
			}
			set
			{
				if (format != value)
				{
					format = value ;
					OnPropertyChanged("Format") ;
				}
				valueDisplay.Format = format ;
			}
		}


		/// <summary>
		/// Gets the Units string displayed. Value depends on UseCustomUnits property
		/// </summary>
		[Category("Units")]
		[Description("Gets the Units string displayed. Value depends on UseCustomUnits property")]
		public string Units
		{
			get
			{
				string			text	= "" ;

				if (useCustomUnits)
				{
					text = customUnits ;
				}
				else if (SourceChannel is Channels.INumericChannel)
				{
					text = ((Channels.INumericChannel)SourceChannel).Units ;
				}
				return text ;
			}
		}


		/// <summary>
		/// Gets or Sets the Color of the Needle
		/// </summary>
		[Category("Linear")]
		[Description("Gets or Sets the Color of the Needle")]
		public Color NeedleColor
		{
			get
			{
				return Needle.Color ;
			}
			set
			{
				Needle.Color = value ;
				OnPropertyChanged("NeedleColor") ;
			}
		}
		private bool ShouldSerializeNeedleColor()
		{
			return Needle.Color != Color.SteelBlue ;
		}


		/// <summary>
		/// Gets or Sets the Color of the Fill
		/// </summary>
		[Category("Linear")]
		[Description("Gets or Sets the Color of the Fill")]
		[Browsable(true)]
		public Color FillColor
		{
			get
			{
				return fillColor ;
			}
			set
			{
                Needle.FillColor = value ;
				fillColor = value ;
				OnPropertyChanged("FillColor") ;
			}
		}
		private bool ShouldSerializeFillColor()
		{
			return fillColor != Color.LightSteelBlue ;
		}
		private Color fillColor = Color.LightSteelBlue ;

		/// <summary>
		/// Gets or Sets the Color of the Scale
		/// </summary>
		[Category("Linear")]
		[Description("Gets or Sets the Color of the Scale")]
		public Color ScaleColor
		{
			get
			{
				return Scale.Color ;
			}
			set
			{
				Scale.Color = value ;
				OnPropertyChanged("ScaleColor") ;
			}
		}
		private bool ShouldSerializeScaleColor()
		{
			return Scale.Color != Color.Black ;
		}


		/// <summary>
		/// Gets or sets whether fill is enabled
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets whether fill is enabled")]
		[DefaultValue(false)]
		public bool FillEnabled
		{
			get
			{
				return Needle.FillEnabled ;
			}
			set
			{
				Needle.FillEnabled = value ;
				OnPropertyChanged("FillEnabled") ;
			}
		}


		/// <summary>
		/// Gets or sets whether to use the custom minimum and maximum, or those of the source channel
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets whether to use the custom minimum and maximum, or those of the source channel")]
		[DefaultValue(false)]
		public bool UseCustomRange
		{
			get
			{
				return useCustomRange ;
			}
			set
			{
				if (value != useCustomRange)
				{
					useCustomRange = value ;
					OnPropertyChanged("UseCustomRange") ;
					OnPropertyChanged("Minimum") ;
					OnPropertyChanged("Maximum") ;
					NewRange(Minimum, Maximum) ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the custom Minimum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the custom Minimum value of the scale")]
		[DefaultValue(0.0)]
		public double CustomMinimum
		{
			get
			{
				return customMinimum ;
			}
			set
			{
				if (System.Math.Abs(value - customMinimum) > System.Double.MinValue)
				{
					customMinimum = value ;
					OnPropertyChanged("CustomMinimum") ;
					
					if (useCustomRange)
					{
						OnPropertyChanged("Minimum") ;
						NewRange(Minimum, Maximum) ;
					}
				}
			}
		}


		/// <summary>
		/// Gets the Minimum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets the Minimum value of the scale")]
		public double Minimum
		{
			get
			{
				double			min		= 0 ;

				if (useCustomRange)
				{
					min = customMinimum ;
				}
				else if (SourceChannel is Channels.INumericChannel)
				{
					min = ((Channels.INumericChannel)SourceChannel).Minimum ;
				}
				return min ;
			}
		}
	

		/// <summary>
		/// Gets or sets the custom Maximum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the custom Maximum value of the scale")]
		[DefaultValue(100.0)]
		public double CustomMaximum
		{
			get
			{
				return customMaximum ;
			}
			set
			{
				if (System.Math.Abs(value - customMaximum) > System.Double.MinValue)
				{
					customMaximum = value ;
					OnPropertyChanged("CustomMaximum") ;
					
					if (useCustomRange)
					{
						OnPropertyChanged("Maximum") ;
						NewRange(Minimum, Maximum) ;
					}
				}
			}
		}


		/// <summary>
		/// Gets the Maximum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets the Maximum value of the scale")]
		public double Maximum
		{
			get
			{
				double			max		= 100 ;

				if (useCustomRange)
				{
					max = customMaximum ;
				}
				else if (SourceChannel is Channels.INumericChannel)
				{
					max = ((Channels.INumericChannel)SourceChannel).Maximum ;
				}
				return max ;
			}
		}


		/// <summary>
		/// Gets or sets whether to use custom decimal places, or that of the source channel
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets whether to use custom decimal places, or that of the source channel")]
		[DefaultValue(false)]
		public bool UseCustomDecimalPlaces
		{
			get
			{
				return useCustomDecimalPlaces ;
			}
			set
			{
				if (value != useCustomDecimalPlaces)
				{
					useCustomDecimalPlaces = value ;
					OnPropertyChanged("UseCustomDecimalPlaces") ;
					OnPropertyChanged("DecimalPlaces") ;
					NewDecimalPlaces(DecimalPlaces) ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the custom Decimal Places of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the custom Decimal Places of the scale")]
		[DefaultValue(0)]
		public int CustomDecimalPlaces
		{
			get
			{
				return customDecimalPlaces ;
			}
			set
			{
				if (value != customDecimalPlaces)
				{
					customDecimalPlaces = value ;
					OnPropertyChanged("CustomDecimalPlaces") ;
					
					if (useCustomDecimalPlaces)
					{
						OnPropertyChanged("DecimalPlaces") ;
						NewDecimalPlaces(DecimalPlaces) ;
					}
				}
			}
		}
	

		/// <summary>
		/// Gets the number of Decimal Places displayed by the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets the number of Decimal Places displayed by the scale")]
		public int DecimalPlaces
		{
			get
			{
				int			dps		= 0 ;

				if (useCustomDecimalPlaces)
				{
					dps = customDecimalPlaces ;
				}
				else if (SourceChannel is Channels.INumericChannel)
				{
					dps = ((Channels.INumericChannel)SourceChannel).DecimalDigits ;
				}
				return dps ;
			}
		}



		/// <summary>
		/// Gets or Sets the Tool Tip Text that appears when hovering over the control
		/// </summary>
		[Category("Control")]
		[DefaultValue("")]
		[Description("Gets or Sets the Tool Tip Text that appears when hovering over the control")]
		[Browsable(false)]
		public override string ToolTipText
		{
			get
			{
				return base.ToolTipText ;
			}
			set
			{
				base.ToolTipText = value ;
			}
		}
	
		/// <summary>
		/// Gets or Sets whether the rate at which the Control redraws
		/// </summary>
		[Category("Control")]
		[DefaultValue(KineticaRT.Controls.UpdateSpeeds.Fast)]
		[Description("Gets or Sets the rate at which the Control redraws")]
		[Browsable(true)]
		public override KineticaRT.Controls.UpdateSpeeds UpdateSpeed
		{
			get
			{
				return base.UpdateSpeed ;
			}
			set
			{
				base.UpdateSpeed = value ;
                this.valueDisplay.UpdateSpeed = value;
            }
		}

        /// <summary>
        /// Gets or Sets the Tool Tip Text that appears when hovering over the control
        /// </summary>
        [Category("ErrorProvider")]
        [DefaultValue("")]
        [Description("Gets or Sets the Tool Tip Text that appears when hovering over the control")]
        public override bool ErrorProviderEnabled //ft
        {
            get { return valueDisplay.ErrorProviderEnabled; }
            set { valueDisplay.ErrorProviderEnabled = value; }
        }
		///<summary>
		///Gets the Scale object
		///</summary>
		[Category("Dial")]
		[Description("Gets the Scale object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new KineticaRT.Controls.Scale Scale
		{
			get 
			{
				return base.Scale ;
			}
		}
	
		/// <summary>
		/// Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum
		/// from the Source of it's primary Needle
		/// </summary>
		[Category("Dial")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum from the Source of it's primary Needle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool UseSourceFormat
		{
			get
			{
				return base.UseSourceFormat ;
			}
			set
			{
				base.UseSourceFormat = value ;
			}
		}

		///<summary>
		///Gets or sets the Needle object
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Needle object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override KineticaRT.Controls.SliderNeedle Needle
		{
			get 
			{
				return base.Needle ;
			}
			set
			{
				base.Needle = value ;
			}
		}

		/// <summary>
		/// Gets or sets the height of the description.
		/// </summary>
		/// <value >Integer number of lines to display</value>
		[Category("Description")]
		[Description("Gets or sets the height of the description as the number of lines displayed.")]
		[DefaultValue(1)]
		public int DescriptionHeight
		{
			get
			{
				return descriptionLines;
			}
			set
			{
				if (value != descriptionLines)
				{
					descriptionLines = value ;
					OnPropertyChanged("DescriptionLines") ;
					UpdateLayout() ;
				}
			}
		}
		private bool ShouldSerializeDescriptionHeight()
		{
			return descriptionLines != 1 ;
		}

		/// <summary>
		/// Gets or sets a Custom Description
		/// </summary>
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.")]
		[DefaultValue(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value ;
			}
		}
		private bool ShouldSerializeTabStop()
		{
			return base.TabStop != false ;
		}

		/// <summary>
		/// Gets or sets the orientation of the linear
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets the orientation of the linear")]
		[DefaultValue(Orientation.Vertical)]
		public override Orientation Orientation
		{
			get
			{
				return base.Orientation ;
			}
			set
			{
				base.Orientation = value ;
				UpdateLayout() ;
			}
		}

		/// <summary>
		/// Gets or sets whether the needle is enabled
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets whether the needle is enabled")]
		[DefaultValue(true)]
		public bool NeedleEnabled
		{
			get
			{
				return Needle.Visible ;
			}
			set
			{
				Needle.Visible = value ;
				OnPropertyChanged("NeedleEnabled") ;
			}
		}

		/// <summary>
		/// Gets or sets the style of the needle
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets the style of the needle")]
		[DefaultValue(KineticaRT.Controls.SliderNeedleStyles.Line)]
		public KineticaRT.Controls.SliderNeedleStyles NeedleStyle
		{
			get
			{
				return Needle.Style ;
			}
			set
			{
				Needle.Style = value ;
				OnPropertyChanged("NeedleStyle") ;
			}
		}

		/// <summary>
		/// Gets or sets the width of the needle in pixels
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets the width of the needle in pixels")]
		[DefaultValue(6)]
		public int NeedleWidth
		{
			get
			{
				return Needle.Width ;
			}
			set
			{
				Needle.Width = value ;
				OnPropertyChanged("NeedleWidth") ;
			}
		}

		/// <summary>
		/// Gets or sets the width devoted to the scale (in pixels)
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the width devoted to the scale (in pixels)")]
		[DefaultValue(35)]
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
					OnPropertyChanged("ScaleWidth") ;
					SetScalePosition() ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the position of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the position of the scale")]
		[DefaultValue(ScalePositions.Left)]
		public ScalePositions ScalePosition
		{
			get
			{
				return scalePosition ;
			}
			set
			{
				if (scalePosition != value)
				{
					scalePosition = value ;
					OnPropertyChanged("ScalePosition") ;

					switch (value)
					{
						case ScalePositions.Left:
						{
							Scale.LabelPosition = KineticaRT.Controls.ScaleMarkPositions.Inside ;
							Scale.MarkPosition	= KineticaRT.Controls.ScaleMarkPositions.Inside ;
							Scale.Enabled = true ;
						}
						break ;

						case ScalePositions.Right:
						{
							Scale.LabelPosition = KineticaRT.Controls.ScaleMarkPositions.Outside ;
							Scale.MarkPosition	= KineticaRT.Controls.ScaleMarkPositions.Outside ;
							Scale.Enabled = true ;
						}
						break ;

						case ScalePositions.Hidden:
						{
							Scale.Enabled = false ;
						}
						break ;
					}

					SetScalePosition() ;
					UpdateLayout () ;
				}
			}
		}
		private bool ShouldSerializeScalePosition()
		{
			return (scalePosition == ScalePositions.Left) ;
		}


		/// <summary>
		/// Gets or sets whether input is enabled
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets whether input is enabled")]
		[DefaultValue(false)]
		public bool InputEnabled
		{
			get
			{
				return valueDisplay.InputEnabled ;
			}
			set
			{
				if (valueDisplay.InputEnabled != value)
				{
					valueDisplay.InputEnabled = value ;
					Needle.Enabled = value ;

					if (value)
					{
						valueDisplay.BackColor = inputBackColor == System.Drawing.Color.Empty ? this.BackColor : inputBackColor ;
						valueDisplay.BackColor = inputBackColor ;
						valueDisplay.DestinationChannel = SourceChannel ;
						Needle.DestinationChannel = SourceChannel ;
					}
					else
					{
						valueDisplay.BackColor = valueBackColor == System.Drawing.Color.Empty ? this.BackColor : valueBackColor ;
						valueDisplay.DestinationChannel = null ;
						Needle.DestinationChannel = null ;
					}

					OnPropertyChanged("InputEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the background color of the input area when input is enabled
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets the background color of the input area when input is enabled")]
		public Color InputBackColor
		{
			get
			{
				return inputBackColor ;
			}
			set
			{
				if (inputBackColor != value)
				{
					inputBackColor = value ;
					OnPropertyChanged("InputBackColor") ;

					if (valueDisplay.InputEnabled)
					{
						valueDisplay.BackColor = inputBackColor ;
					}
				}
			}
		}
		private bool ShouldSerializeInputBackColor()
		{
			return inputBackColor != SystemColors.Window ;
		}

		/// <summary>
		/// Gets or sets the background color of the digital display area when input is not enabled.
		/// </summary>
		/// <remarks>Set to Empty to use threshold colors.</remarks>
		[Category("Linear")]
		[Description("Gets or sets the background color of the digital display area when input is not enabled. Set to Empty to use threshold colors.")]
		public Color ValueBackColor
		{
			get
			{
				return valueBackColor ;
			}
			set
			{
				if (valueBackColor != value)
				{
					valueBackColor = value ;
					OnPropertyChanged("ValueBackColor") ;

					if (!valueDisplay.InputEnabled)
					{
						valueDisplay.BackColor = valueBackColor == System.Drawing.Color.Empty ? this.BackColor : valueBackColor ;
					}
				}
			}
		}
		private bool ShouldSerializeValueBackColor()
		{
			return valueBackColor != System.Drawing.Color.Empty ;
		}
		#endregion

		#region Private Methods
		private void NewText()
		{
			Invalidate(descriptionRect) ;
			//BottomText = Units ;
			//TopText = Description ;
		}

		/// <summary>
		/// Correctly (re)position the scale
		/// </summary>
		private void SetScalePosition()
		{
			if (Scale == null || Needle == null) return ;

			//reposition the scale, and size the needle and fill to match
			double			percent ;
		    
			if (Orientation == Orientation.Vertical)
			{
				percent = (double)ScaleWidth / DisplayRectangle.Width ;
			}
			else		//horizontal ;
			{
				percent = (double)ScaleWidth / DisplayRectangle.Height ;
			}

			switch (ScalePosition)
			{
				case ScalePositions.Left:
				default:
				{
					Scale.Position = percent ;
					Needle.Start = percent ;
					Needle.Finish = 0.9 ;
					Needle.FillStart = percent ;
					Needle.FillFinish = 0.8 ;
				}
					break ;

				case ScalePositions.Right:
				{
					Scale.Position = 1 - percent ;
					Needle.Start = 0 ;
					Needle.Finish = 1.1 - percent ;
					Needle.FillStart = 0.2 ;
					Needle.FillFinish = 1 - percent ;
				}
					break ;

				case ScalePositions.Hidden:
				{
					Needle.Start = 0 ;
					Needle.Finish = 0.9;
					Needle.FillStart = 0 ;
					Needle.FillFinish = 0.8 ;
				}
					break ;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e == null) throw new ArgumentNullException("e") ;

			if (e == KineticaRT.Channels.Channel.ValuePropertyChangedEventArgs) return ;

			if (!useCustomRange && (e.PropertyName == "Minimum" || e.PropertyName == "Maximum"))
			{
				NewRange(Minimum, Maximum) ;
			}
			else if (!useCustomDecimalPlaces && e.PropertyName == "DecimalDigits")
			{
				NewDecimalPlaces(DecimalPlaces) ;
			}
			else if (e == KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs)
			{
//NS07122010				if (valueBackColor == Color.Empty)
//NS07122010				{
					if (thresholdChannel != null)
					{
						if (thresholdChannel.Color == Color.Empty)
						{
                            this.valueDisplay.BackColor = valueBackColor;
                            this.FillColor = valueBackColor;
                        }
                        else if (thresholdChannel.NormalColor == thresholdChannel.Color)
                        {
                            this.valueDisplay.BackColor = valueBackColor;
                            this.FillColor = thresholdChannel.Color;
                        }
                        else
						{
							this.valueDisplay.BackColor = thresholdChannel.Color ;
                            this.FillColor = thresholdChannel.Color;
						}
					}
					else
					{
						this.valueDisplay.BackColor = valueBackColor ;
                        this.FillColor = valueBackColor;
					}
//NS07122010				}
//NS07122010				else
//NS07122010				{
//NS07122010					this.valueDisplay.BackColor = valueBackColor ;
//NS07122010				}
			}
			else if (e == KineticaRT.Channels.Channel.LowLowPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[0].Value = thresholdChannel.LowLow ;
			}
			else if (e == KineticaRT.Channels.Channel.LowPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[1].Value = thresholdChannel.Low ;
			}
			else if (e == KineticaRT.Channels.Channel.NormalPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) SetFill (thresholdChannel.Normal, thresholdChannel.NormalColor) ;
			}
			else if (e == KineticaRT.Channels.Channel.HighPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[2].Value = thresholdChannel.High ;
			}			
			else if (e == KineticaRT.Channels.Channel.HighHighPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[3].Value = thresholdChannel.HighHigh ;
			}
			else if (e == KineticaRT.Channels.Channel.LowLowColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[0].MarkColor = thresholdChannel.LowLowColor ;
			}
			else if (e == KineticaRT.Channels.Channel.LowColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[1].MarkColor = thresholdChannel.LowColor ;
			}
			else if (e == KineticaRT.Channels.Channel.HighColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[2].MarkColor = thresholdChannel.HighColor ;
			}
			else if (e == KineticaRT.Channels.Channel.HighHighColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[3].MarkColor = thresholdChannel.HighHighColor ;
			}
			else if (e == KineticaRT.Channels.Channel.NormalColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) SetFill (thresholdChannel.Normal, thresholdChannel.NormalColor) ;
			}
			else
			{
				if ((DescriptionSource == TextSources.Description && e == KineticaRT.Channels.Channel.DescriptionPropertyChangedEventArgs)||
					(DescriptionSource == TextSources.ToolTip && e == KineticaRT.Channels.Channel.ToolTipPropertyChangedEventArgs)			)
				{
					Invalidate(DisplayRectangle) ;
				}
				else if (!UseCustomUnits && e == KineticaRT.Channels.Channel.UnitsPropertyChangedEventArgs)
				{
					Invalidate(DisplayRectangle) ;
				}
				else
				{
					if ((toolTipSource == TextSources.Description && e.PropertyName == "Description") ||
						(toolTipSource == TextSources.ToolTip && e.PropertyName == "ToolTip")			)
					{
						NewToolTip(ToolTip) ;
					}
				}
			}
//
//			if (e.PropertyName != "Value")
//			{
//				Invalidate(DisplayRectangle) ;
//			}
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Updates the sizes of all sections
		/// </summary>
		protected void UpdateLayout()
		{
			if (valueDisplay == null) return ;

			//Min sizes (for exception prevention)
			//Display 4x0
			//Slider 4x2

			//displayRect		= ClientRectangle ;
			CalculateDisplayRectangle () ;

			unitsRect = Rectangle.Empty ;
			descriptionRect = Rectangle.Empty ;

			if (Orientation == Orientation.Vertical)
			{
				unitsRect.X			= displayRect.Left + border_spacing ;
				descriptionRect.X	= unitsRect.Left ;
				valueDisplay.Left	= unitsRect.Left + 4 ;

				stringFormat.Alignment = StringAlignment.Center ;

				//desc, units and value are 1/12th total height
				//slider is 2/3 total height
				//Left	= unitsRect.X ;

				unitsRect.Width			= displayRect.Width - border_spacing * 2 ;
				descriptionRect.Width	= unitsRect.Width ;
				valueDisplay.Width		= unitsRect.Width - 8;

				unitsRect.Height		= unitsHeight ; //(int)(DisplayRectangle.Height / 12.0) ;
				descriptionRect.Height	= descriptionHeight ; // unitsRect.Height ;
				valueDisplay.Height		= valueHeight ; // unitsRect.Height ;
				//Height		= Math.Max(DisplayRectangle.Height - (4 * unitsRect.Height), 2) ;

				descriptionRect.Y		= ClientRectangle.Y + border_spacing ;
				unitsRect.Y				= descriptionRect.Bottom ;
				//Top			= unitsRect.Bottom ;
				valueDisplay.Top		= displayRect.Bottom ; // + unitsRect.Height ;
			}
			else		//horizontal
			{
				unitsRect.X			= ClientRectangle.X + border_spacing * 2 ;
				descriptionRect.X	= unitsRect.Left ;
				valueDisplay.Left	= unitsRect.Left ;

				stringFormat.Alignment = StringAlignment.Near ;

				//desc, units and value are 1/4 total height and 1/4 total width
				//slider is 3/4 total width
				unitsRect.Width			= descriptionWidth ;
				descriptionRect.Width	= unitsRect.Width ;
				valueDisplay.Width		= Math.Max(unitsRect.Width, 4) ;

				unitsRect.Height		= unitsHeight ; //(int)(DisplayRectangle.Height / 12.0) ;
				descriptionRect.Height	= descriptionHeight ; // unitsRect.Height ;
				valueDisplay.Height		= valueHeight ; // unitsRect.Height ;

				descriptionRect.Y		= ClientRectangle.Y + border_spacing ;
				unitsRect.Y				= descriptionRect.Bottom ;
				valueDisplay.Top		= ClientRectangle.Bottom - valueDisplay.Height - border_spacing ;
			}

			AddToQueue (true) ; //			Invalidate(DisplayRectangle) ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected void NewToolTip(string newText)
		{
			valueDisplay.ToolTipText = newText ;
			ToolTipText = newText ;
			toolTip.SetToolTip(this, ToolTip) ;
			OnPropertyChanged("ToolTip") ;
		}

		/// <summary>
		/// Set the range of scaled objects
		/// </summary>
		/// <param name="minimum">New minimum value</param>
		/// <param name="maximum">New Maximum value</param>
		protected void NewRange(double minimum, double maximum)
		{
			Scale.Minimum = minimum ;
			Scale.Maximum = maximum ;

			valueDisplay.Minimum = minimum ;
			valueDisplay.Maximum = maximum ;

			//need to set this as auto-adjust may have altered it
			NewDecimalPlaces(DecimalPlaces) ;
		}

		/// <summary>
		/// Set the decimal places of scaled objects
		/// </summary>
		/// <param name="decimalPlaces">New decimal places</param>
		protected void NewDecimalPlaces(int decimalPlaces)
		{
			Scale.DecimalPlaces = decimalPlaces ;
			UpdateLayout () ;
		}

		/// <summary>
		/// Gets the scale for the Scaled Control.
		/// </summary>
		protected KineticaRT.Controls.Scale  ValueScale
		{
			get {return Scale;}
		}

		/// <summary>
		/// Sets the fill color for this object.
		/// Used for setting the LowLow threshold color when connected to threshold channels.
		/// </summary>
		/// <param name="color">The new color.</param>
		/// <param name="centrePoint">The new center point about which the fill is drawn.</param>
		protected void SetFill (double centrePoint, System.Drawing.Color color)
		{
			Needle.CentrePoint = centrePoint ;
			if (color == System.Drawing.Color.Empty)
			{
				Needle.FillColor = fillColor ;
			}
			else
			{
				Needle.FillColor = color ;
			}
		}
		#endregion

		#region Standard Events
		/// <summary>
		/// Rezise event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e) ;
			//	Location	= DisplayRectangle.Location ;
			//	Width	= Math.Max(DisplayRectangle.Width, 2) ;
			//	Height	= Math.Max(DisplayRectangle.Height, 2) ;

			if (this.Size.Width < 20) Width = 20 ;
			if (this.Size.Height < 20) Height = 20 ;

			SetScalePosition() ;
			UpdateLayout() ;
		}
		#endregion

		#region IConnectable

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
			if (source.Length > 0)
			{
				SourceChannel = OnObjectSearch(source) as KineticaRT.Channels.IInfoChannel ;
                if (this.sourceChannel == null && source.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
			}

			SetScalePosition() ;
			UpdateLayout() ;
            base.Connect(); //ft
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)
			{
				SourceChannel = null ;
			}
            base.Disconnect(); //ft
		}
		#endregion

		#region Drag / Drop operations
		/// <summary>
		/// DragEnter event
		/// </summary>
		/// <param name="drgevent">Event Data</param>
		protected override void OnDragEnter (DragEventArgs drgevent)
		{
			if (drgevent == null) throw new ArgumentNullException("drgevent") ;

			//check if allowed to drop things, and if drag object holds text
			if (AllowDrop && drgevent.Data.GetDataPresent(DataFormats.Text)) 
			{
				drgevent.Effect = DragDropEffects.Copy;
			}
			else
			{
				drgevent.Effect = DragDropEffects.None;
			}

			base.OnDragEnter(drgevent) ;
		}

		/// <summary>
		/// DragDrop event
		/// </summary>
		/// <param name="drgevent">Event data</param>
		protected override void OnDragDrop (DragEventArgs drgevent)
		{
			if (drgevent == null) throw new ArgumentNullException("drgevent") ;

			if (AllowDrop && drgevent.Data.GetDataPresent(DataFormats.Text)) 
			{
				Source = drgevent.Data.GetData(DataFormats.Text).ToString() ;
			}

			base.OnDragDrop(drgevent) ;
		}
		#endregion
		
		#region Drawing
		/// <summary>
		/// Paint Event
		/// </summary>
		/// <param name="FullRedraw">true to redraw whole object.</param>
		/// <param name="g">Event Data</param>
		//protected override void OnPaint(PaintEventArgs e)
		public override void Repaint(bool FullRedraw, Graphics g)
		{
            //lock (this) 
            {
                if (g == null) throw new ArgumentNullException("g");

                if (FullRedraw || backColorChanged)
                {
                    if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                    {
                        backColorChanged = false;
                        backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                    }
                    if (foreColorChanged == true)//flag de type volatile pour eviter les Cross Thread Operation
                    {
                        //ft optimisation en vitesse c'est le seul endroit ou il faut jouer sur cet objet pour eviter les CrossThread
                        // le fait d'utiliser un flag volatile nous permet de synchroniseer les thread pour que le seul endroit qui
                        // joue sur cette variable soit situe dans ce Repaint UNIQUEMENT
                        foreColorChanged = false;
                        foreColorBrush.Color = foreColor;
                    }
                    //draw units and description text
                    if (descriptionRect.Width > 0)
                    {
                        g.FillRectangle(this.backColorBrush, descriptionRect);
                        g.DrawString(Description, Font, foreColorBrush, descriptionRect, stringFormat);
                    }
                    if (unitsRect.Width > 0)
                    {
                        g.FillRectangle(this.backColorBrush, unitsRect);
                        g.DrawString(Units, Font, foreColorBrush, unitsRect, stringFormat);
                    }
                }

                g.ExcludeClip(descriptionRect);
                g.ExcludeClip(unitsRect);

                base.Repaint(FullRedraw, g);
            }
		}

		/// <summary>
		/// Recalculates the DisplayRectangle based on the current ClientRectangle
		/// and Appearance.
		/// </summary>
		protected override void CalculateDisplayRectangle()
		{
			base.CalculateDisplayRectangle () ;

			if (shape == KineticaRT.Controls.Shapes.RoundedRectangle)
			{
				border_spacing = (int)(this.cornerRadius / System.Math.PI) ;
			}
			else if (shape == KineticaRT.Controls.Shapes.Ellipse)
			{
				border_spacing = (int)(this.cornerRadius / System.Math.PI) ;
			}
			else
			{
				border_spacing = 0 ;
			}
            switch (BorderType)
			{
				case KineticaRT.Controls.BorderType.Indented  :
                case KineticaRT.Controls.BorderType.Raised:
                case KineticaRT.Controls.BorderType.EditBox:
				{
					border_spacing += 2 ;
				}
					break ;

                case KineticaRT.Controls.BorderType.Flat:
                case KineticaRT.Controls.BorderType.Shaded:
                case KineticaRT.Controls.BorderType.Rounded:
                case KineticaRT.Controls.BorderType.RoundedWithInset:
                case KineticaRT.Controls.BorderType.ShadedWithInset:
				{
					border_spacing += BorderWidthPixels (ClientRectangle) ;
				}
					break ;
			}

			unitsHeight				= (int) this.Font.GetHeight()+ 2 ;
			descriptionHeight		= unitsHeight * descriptionLines ;
			valueHeight				= unitsHeight + 4 ;
			//scaleWidth				= (int)(unitsHeight * (Scale.DecimalPlaces +  + this.scale.Maximum.ToString().Length))  ; 
			if (Orientation == Orientation.Vertical)
			{
				displayRect.Height -= (int)(descriptionHeight + unitsHeight + valueHeight + (border_spacing * 2)) ;
				displayRect.Y += (int)(descriptionHeight + unitsHeight + border_spacing) ;
			}
			else // Horizontal
			{
				descriptionWidth = descriptionHeight * 3 ;
				displayRect.Width -= (int)(descriptionWidth + (border_spacing * 2)) ;
				displayRect.X += (int)(descriptionWidth + border_spacing) ;
				//displayRect.Height -= (int)(descriptionHeight + unitsHeight + valueHeight + (border_spacing * 2)) ;
				//displayRect.Y -= (int)(border_spacing) ;
			}
		}
		#endregion

		#region Mouse
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e == null) throw new ArgumentNullException("e") ;

			MouseEventArgs f ;

			if (Orientation == Orientation.Vertical)
			{
				f = new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y - displayRect.Y, e.Delta) ;
			}
			else // Horizontal
			{
				f = new MouseEventArgs(e.Button, e.Clicks, e.X - displayRect.X, e.Y, e.Delta) ;
			}
				
			//this.Parent.Focus () ;
			base.OnMouseDown (f) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e == null) throw new ArgumentNullException("e") ;

			MouseEventArgs f ;

			if (Orientation == Orientation.Vertical)
			{
				f = new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y - displayRect.Y, e.Delta) ;
			}
			else // Horizontal
			{
				f = new MouseEventArgs(e.Button, e.Clicks, e.X - displayRect.X, e.Y, e.Delta) ;
			}
				
			base.OnMouseMove (f) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e == null) throw new ArgumentNullException("e") ;

			MouseEventArgs f ;

			if (Orientation == Orientation.Vertical)
			{
				f = new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y - displayRect.Y, e.Delta) ;
			}
			else // Horizontal
			{
				f = new MouseEventArgs(e.Button, e.Clicks, e.X - displayRect.X, e.Y, e.Delta) ;
			}
				
			base.OnMouseUp (f) ;
		}
		#endregion
	}
}

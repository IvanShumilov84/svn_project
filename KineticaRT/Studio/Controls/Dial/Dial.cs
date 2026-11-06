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
	/// Displays a numeric Value from a source channel on a dial,
	/// together with an optional description and units
	/// </summary>
	#region License
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(Dial.LicenseProvider))]
#endif
	#endregion
    [ChangeRecord("2.0.1.0", "07 12 2010", "N. Savidge", "Added ValueColor property.")]
    [Copyright("Copyright 2003-2010 KineticaRT Ltd.")]
    [ToolboxBitmap(typeof(Dial))]
	public class Dial : KineticaRT.Controls.Dial, ISource //ft
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
						if ((key == null) || (key != "0B3C8DFAB528"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "0B3C8DFAB528")) key = "{0B3C8DFA-B528-4aba-BF19-1DB5A9E8F24D}" ; 
						context.SetSavedLicenseKey (typeof(Dial), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Dial), null) ;
					if (key == "{0B3C8DFA-B528-4aba-BF19-1DB5A9E8F24D}") licence = new License ("Run") ;
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

		// Studio.Dial
		private Color					valueBackColor ;

		/// <summary>
		/// The threshold channel that this control is connected to or null if not connected to a threshold channel.
		/// </summary>
		protected KineticaRT.Channels.INumericThresholdChannel thresholdChannel = null ;
        #endregion

        #region Initialisation and Disposal
        /// <summary>
        /// Initialises a new instance of the Dial class
        /// </summary>
        public Dial() : base()
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

            //this.needle.Centred = false;
            //this.needle.FillStyle = KineticaRT.Controls.FillStyles.SegmentedFromMinimum;
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
			this.valueDisplay = new KineticaRT.Controls.Display();
			this.SuspendLayout();
			// 
			// valueDial
			// 
			Needle.FillColor = System.Drawing.Color.MediumBlue;
			Needle.FillEnabled = false;
			Needle.FillStyle = KineticaRT.Controls.FillStyles.Solid ;
			Needle.FillStart = 0.9 ;
			Needle.FillFinish = 0.8 ;
			Needle.Enabled = false ;
			Scale.Position = 0.9;
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
			this.valueDisplay.Location = new System.Drawing.Point(56, 112);
			this.valueDisplay.Name = "valueDisplay";
			this.valueDisplay.NumericOnly = true;
			this.valueDisplay.Size = new System.Drawing.Size(40, 16);
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
			// Dial
			// 
			this.Controls.Add(this.valueDisplay);
			this.Name = "Dial";
			this.TabIndex = 0;
			this.UseSourceFormat = false;
			this.valueBackColor	= System.Drawing.Color.Empty ;

			// Studio.ControlBase
			this.ResizeRedraw = true ;
			this.SetStyle(ControlStyles.UserPaint, true) ;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//ft
            // AllowDrop = true ; SB removed because it is overriding the user values
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

			// Studio.ControlBase
			this.ResizeRedraw = true ;
			this.SetStyle(ControlStyles.UserPaint, true) ;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//ft
            // AllowDrop = true ;   SB removed because it is overriding the user values
			propertyHdlr			= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;
			toolTip = new ToolTip() ;
			toolTip.ShowAlways = true ;
			customDescription		= "" ;
			descriptionSource		= TextSources.Description ;
			source					= "" ;
			sourceChannel			= null ;
			customToolTip			= "" ;
			toolTipSource			= TextSources.ToolTip ;

			UpdateLayout() ;
		
			this.ResumeLayout(false);
		}
		#endregion

		#region Properties
        /// Added property N Savidge 7 Dec 2010 - copied from Studio.Controls.Display.
        /// <summary>
        /// Gets or Sets the Color of the Value text
        /// </summary>
        [Category("Dial")]
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
                return Needle.GradientEnabled;
            }
            set
            {
                if (Needle.GradientEnabled != value)
                {
                    Needle.GradientEnabled = value;
                    valueDisplay.GradientEnabled = value;
                    AddToQueue(true);
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
        /// Gets or sets the linear gradient mode
        /// </summary>
        [Category("Gradient")]
        [DefaultValue(typeof(Color), "Navy")]
        [Description("Gets or sets the linear gradient mode")]
        public override LinearGradientMode LinearGradientMode
        {
            get
            {
                return valueDisplay.LinearGradientMode;
            }
            set
            {
                if (Needle.LinearGradientMode != value)
                {
                    Needle.LinearGradientMode =  LinearGradientMode.Vertical;
                    valueDisplay.LinearGradientMode = value;
                    if (GradientEnabled) AddToQueue(false);

                    OnPropertyChanged("LinearGradientMode");
                }
            }
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
					NewDPs(DecimalPlaces) ;

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
					NewDPs(DecimalPlaces) ;

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

					NewRange(Minimum, Maximum) ;
					NewText() ;
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
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
					TopText = Description ;
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
				TopText = Description ;
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
				AdjustValueHeight () ;
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
					BottomText = Units ;
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
				BottomText = Units ;
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
		[Category("Dial")]
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
		[Category("Dial")]
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
			return fillColor != Color.LightSteelBlue  ;
		}
		private Color fillColor = Color.LightSteelBlue ;

		/// <summary>
		/// Gets or Sets the Color of the Scale
		/// </summary>
		[Category("Dial")]
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
		/// Gets or sets the background color of the digital display area when input is not enabled.
		/// </summary>
		/// <remarks>Set to Empty to use threshold colors.</remarks>
		[Category("Dial")]
		[Description("Gets or sets the background color of the digital display area when input is not enabled.  Set to Empty to use threshold colors.")]
		public Color ValueBackColor
		{
			get
			{
				return valueBackColor ;
			}
			set
			{
                //valueDisplay.BackColor = value;
                //valueBackColor = value;
                //OnPropertyChanged("ValueBackColor");
                
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

		/// <summary>
		/// Gets or sets whether fill is enabled
		/// </summary>
		[Category("Dial")]
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
        /// Gets or sets engine no run zone1 starting value
        /// </summary>
        [Category("Scale")]
        [Description("Gets or sets engine no run zone1 starting value")]
        [DefaultValue(0)]
        public int EngineNoRunZone1StartValue
        {
            get
            {
                return Scale.EngineNoRunZone1StartValue;
            }
            set
            {
                if (value != Scale.EngineNoRunZone1StartValue)
                {
                    Scale.EngineNoRunZone1StartValue = value;
                    OnPropertyChanged("EngineNoRunZone1StartValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets engine no run zone1 ending value
        /// </summary>
        [Category("Scale")]
        [Description("Gets or sets engine no run zone1 ending value")]
        [DefaultValue(0)]
        public int EngineNoRunZone1EndValue
        {
            get
            {
                return Scale.EngineNoRunZone1EndValue;
            }
            set
            {
                if (value != Scale.EngineNoRunZone1EndValue)
                {
                    Scale.EngineNoRunZone1EndValue = value;
                    OnPropertyChanged("EngineNoRunZone1EndValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets engine no run zone2 starting value
        /// </summary>
        [Category("Scale")]
        [Description("Gets or sets engine no run zone2 starting value")]
        [DefaultValue(0)]
        public int EngineNoRunZone2StartValue
        {
            get
            {
                return Scale.EngineNoRunZone2StartValue;
            }
            set
            {
                if (value != Scale.EngineNoRunZone2StartValue)
                {
                    Scale.EngineNoRunZone2StartValue = value;
                    OnPropertyChanged("EngineNoRunZone2StartValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets engine no run zone2 ending value
        /// </summary>
        [Category("Scale")]
        [Description("Gets or sets engine no run zone2 ending value")]
        [DefaultValue(0)]
        public int EngineNoRunZone2EndValue
        {
            get
            {
                return Scale.EngineNoRunZone2EndValue;
            }
            set
            {
                if (value != Scale.EngineNoRunZone2EndValue)
                {
                    Scale.EngineNoRunZone2EndValue = value;
                    OnPropertyChanged("EngineNoRunZone2EndValue");
                }
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
				if (value != customMinimum)
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
				if (value != customMaximum)
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
					NewDPs(DecimalPlaces) ;
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
						NewDPs(DecimalPlaces) ;
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
		/// Gets or sets the name of the control used by accessibility client applications.
		/// </summary>
		[Browsable(false)]
		[Category("Focus")]
		public new bool CausesValidation
		{
			get { return base.CausesValidation; }
			set { base.CausesValidation = value; }
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
        /// Gets or Sets the Tool Error Provider that appears when hovering over the control
        /// </summary>
        [Category("ErrorProvider")]
        [DefaultValue("")]
        [Description("Gets or Sets the Error Provider that appears when hovering over the control")]
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
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public new KineticaRT.Controls.Scale Scale
		{
			get 
			{
				return base.Scale ;
			}
            set
            {
                base.scale = value; 
            }

		}
		private bool ShouldSerializeScale()
		{
			return false ;
		}
	
		///<summary>
		///Gets or sets the Text shown above the centre of the Dial
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Text shown above the centre of the Dial")]
		[DefaultValue("")]
		[Browsable(false)]
		public override string TopText
		{
			get
			{
				return base.TopText ;
			}
			set
			{
				base.TopText = value ;
			}
		}
		private bool ShouldSerializeTopText()
		{
			return false ;
		}

		///<summary>
		///Gets or sets the Text shown below the centre of the Dial
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Text shown below the centre of the Dial")]
		[DefaultValue("")]
		[Browsable(false)]
		public override string BottomText
		{
			get
			{
				return base.BottomText ;
			}
			set
			{
				base.BottomText = value ;
			}
		}
		private bool ShouldSerializeBottomText()
		{
			return false ;
		}

		///<summary>
		///Gets or sets the position of the scale from the Dial's centre as a proportion of the Dial's radius (0..1)
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the position of the scale from the Dial's centre as a proportion of the Dial's radius (0..1)")]
		public double ScalePosition
		{
			get
			{
				return base.scale.Position ;
			}
			set
			{
				base.scale.Position = value ;
				base.Needle.Length = value ;
				base.Needle.FillStart = value - 0.1 ;
				base.Needle.FillFinish = value ;
				UpdateLayout () ;
			}
		}
		private bool ShouldSerializeScalePosition()
		{
			return true ;
		}

		/// <summary>
		/// Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum
		/// from the Source of it's primary Needle
		/// </summary>
		[Category("Dial")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum from the Source of it's primary Needle")]
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
		private bool ShouldSerializeUseSourceFormat()
		{
			return false ;
		}

		///<summary>
		///Gets or sets the Needle object
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Needle object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(false)]
		public override KineticaRT.Controls.DialNeedle Needle
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
		private bool ShouldSerializeNeedle()
		{
			return false ;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the sizes of all sections
		/// </summary>
		protected void UpdateLayout()
		{
			//NB - min sizes are there for exception prevention

			//Dial (min size 2x2)
			//	Location	= DisplayRectangle.Location ;
			//	Width	= Math.Max(DisplayRectangle.Width, 2) ;
			//	Height	= Math.Max(DisplayRectangle.Height, 2) ;

			//Display in relation to Dial: (min size 4x0)
			valueDisplay.Width	= Math.Max((int)((base.scale.Position*0.38) * Size.Width), 4) ;
			valueDisplay.Left	= (int)((Size.Width-valueDisplay.Width) * 0.5) ;
			AdjustValueHeight () ;
		}
		
		private void AdjustValueHeight ()
		{
			valueDisplay.Height	= (int)valueDisplay.Font.GetHeight() + 4 ; // (int)(0.1 * Size.Height) ;
			valueDisplay.Top	= (int)(Size.Height*0.5 + Size.Height * base.scale.Position * 0.48) - valueDisplay.Height ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected void NewToolTip(string NewText)
		{
			valueDisplay.ToolTipText = NewText ;
			ToolTipText = NewText ;
			toolTip.SetToolTip(this, ToolTip) ;
			OnPropertyChanged("ToolTip") ;
		}

		/// <summary>
		/// Set the range of scaled objects
		/// </summary>
		/// <param name="Min">New minimum value</param>
		/// <param name="Max">New Maximum value</param>
		protected void NewRange(double Min, double Max)
		{
			Scale.Minimum = Min ;
			Scale.Maximum = Max ;

			valueDisplay.Minimum = Min ;
			valueDisplay.Maximum = Max ;

			//need to set this as auto-adjust may have altered it
			NewDPs(DecimalPlaces) ;
		}

		/// <summary>
		/// Set the decimal places of scaled objects
		/// </summary>
		/// <param name="DPs">New decimal places</param>
		protected void NewDPs(int DPs)
		{
			Scale.DecimalPlaces = DPs ;
		}

		private void NewText()
		{
			BottomText = Units ;
			TopText = Description ;
		}
		#endregion

		#region Events
		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		protected void OnSourcePropertyChanged(object Sender, PropertyChangedEventArgs e)
		{
			if (e == KineticaRT.Channels.Channel.ValuePropertyChangedEventArgs) return ;

			if (!useCustomRange && (e.PropertyName == "Minimum" || e.PropertyName == "Maximum"))
			{
				NewRange(Minimum, Maximum) ;
			}
			else if (!useCustomDecimalPlaces && e.PropertyName == "DecimalDigits")
			{
				NewDPs(DecimalPlaces) ;
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
                            this.valueDisplay.BackColor = thresholdChannel.Color;
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
					TopText = Description ;
				}
				else if (!UseCustomUnits && e == KineticaRT.Channels.Channel.UnitsPropertyChangedEventArgs)
				{
					BottomText = Units ;
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
		}
		#endregion

		#region Protected Methods
		/// <summary>
		/// Gets the scale for the Scaled Control.
		/// </summary>
		protected KineticaRT.Controls.Scale ValueScale
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
				Needle.FillColor = this.fillColor ;
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
			Height = Width ;
			//if (this.Size.Height < 20) Height = 20 ;

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
			if (source != "")
			{
				SourceChannel = OnObjectSearch(source) as KineticaRT.Channels.IInfoChannel ;
                if (this.sourceChannel == null && source.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
			}
            base.Connect();
            valueDisplay.Connect(); //ft

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
            valueDisplay.Disconnect();
            base.Disconnect(); //ft
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
		/// DragDrop eventNumericVariable
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
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KineticaRT;
using System.Diagnostics ;
using System.Drawing.Drawing2D;//ft gradient

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// KineticaRT Studio Button control.
	/// Displays a boolean Value from a source channel,
	/// together with an optional description
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(Button.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Button))]
    public class Button : ControlBase, KineticaRT.Controls.IButton, IInputEnabledSource, IVisibleSource
	{
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Studio.Controls.Button", "Studio.Controls.Button Messages");
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
						if ((key == null) || (key != "28BFE62D89FF"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "28BFE62D89FF")) key = "{28BFE62D-89FF-428b-BDAA-2E60A7D45026}" ; 
						context.SetSavedLicenseKey (typeof(Button), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Button), null) ;
					if (key == "{28BFE62D-89FF-428b-BDAA-2E60A7D45026}") licence = new License ("Run") ;
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

		#region Variables and Declarations
		/// <summary>
		/// Enumeration of possible sources of button text
		/// </summary>
		public enum TextSources
		{
			/// <summary>
			/// Custom text
			/// </summary>
			Custom,
			/// <summary>
			/// Source channel Description property
			/// </summary>
			Description,
			/// <summary>
			/// Source Channel ToolTip property
			/// </summary>
			TrueText,
			/// <summary>
			/// No text
			/// </summary>
			None,
			/// <summary>
			/// Source Value TrueText/FalseText property matching the current state
			/// </summary>
			CurrentStateText,
			/// <summary>
			/// Source Value TrueText/FalseText property not matching the current state
			/// </summary>
			FutureStateText,
		}

		private KineticaRT.Controls.Button valueButton;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int				descriptionPercent ;
		private bool			descriptionOnLeft ;
		private StringAlignment	descriptionAlignment ;
		private Color			buttonColor ;
		private Color			buttonTextColor ;
		private Color			buttonAlternateColor ;
		private Color			buttonAlternateTextColor ;

        private Color           buttonGradientLeftColor; //ft
        private Color           buttonGradientRightColor; //ft 
        private Color           buttonGradientAlternateLeftColor;//ft
        private Color           buttonGradientAlternateRightColor; //ft


		/// <summary>
		/// Specifies how/when to use the AlternateColor property.
		/// </summary>
		public enum AlternateColorMode
		{
			/// <summary>
			/// Do not use the AlternateColor
			/// </summary>
			No		= 0 ,
			/// <summary>
			/// Use the AlternateColor
			/// </summary>
			Yes		= 1 ,
			/// <summary>
			/// Use the AlternateColor when the Value Property / source channel is set to true.
			/// </summary>
			Auto	= 2 ,
		}
		private AlternateColorMode	useAlternateColor ;
		private TextSources			textSource ;
		private string				customText ;

		private Rectangle			descriptionRect ;
		private StringFormat		stringFormat ;

		#endregion

		#region Initialisation and disposal
		/// <summary>
		/// Initialises a new instance of the Button class
		/// </summary>
		public Button()
		{
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			valueButton.ValueChanged += new KineticaRT.Controls.ValueChangedEventHandler(OnButtonValueChanged) ;
			valueButton.ObjectSearch += new ObjectSearchEventHandler(valueButton_ObjectSearch);

			stringFormat = new StringFormat() ;
			stringFormat.LineAlignment	= StringAlignment.Center ;
			stringFormat.Alignment		= StringAlignment.Near ;

			descriptionPercent		= 60 ;
			descriptionOnLeft		= true ;
			descriptionAlignment	= StringAlignment.Near ;
			buttonColor				= valueButton.BackColor ;
			buttonTextColor			= valueButton.ForeColor ;
			buttonAlternateColor	= DefaultBackColor ;
            buttonAlternateTextColor= DefaultForeColor ;

            buttonGradientLeftColor = Color.Blue; //ft
            buttonGradientRightColor = Color.DarkBlue; //ft
            buttonGradientAlternateLeftColor = Color.Blue; //ft
            buttonGradientAlternateRightColor = Color.DarkBlue; //ft

			useAlternateColor		= AlternateColorMode.No ;
			textSource				= TextSources.TrueText ;
			customText				= "" ;

			UpdateLayout() ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
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
			this.valueButton = new KineticaRT.Controls.Button();
			this.SuspendLayout();
			// 
			// valueButton
			// 
			this.valueButton.Location = new System.Drawing.Point(96, 0);
			this.valueButton.Name = "valueButton";
			this.valueButton.Size = new System.Drawing.Size(56, 24);
			this.valueButton.TabIndex = 0;
			this.valueButton.Text = "";
			//this.valueButton.Type = KineticaRT.Controls.ButtonReturnTypes.AutoReturn;
			this.valueButton.UseSourceFormat = false ;
			// 
			// Button
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueButton});
			this.Name = "Button";
			this.Size = new System.Drawing.Size(150, 24);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties


        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        [Browsable(true)]
        [Category("General")]
        public new string  Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                if (base.Name !=  value)
                {
                    base.Name = value;
                    valueButton.Name = "value" + value;
                    OnPropertyChanged("Name");
                }
            }
        }



        //ft
        /// <summary>
        /// Gets or sets whether the Title bar is enabled
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets whether the Gradient is enabled")]
        public bool GradientEnabled
        {
            get
            {
                return valueButton.GradientEnabled;
            }
            set
            {
                if (valueButton.GradientEnabled != value)
                {
                    valueButton.GradientEnabled = value;
                    //AddToQueue(true);
                    OnPropertyChanged("GradientEnabled");
                }
            }
        }

        //[Category("Gradient")]
        //[Description("Gets or sets the Color at the left hand side of the control")]
        //public Color GradientLeftColor
        //{
        //    get
        //    {
        //        return valueButton.GradientLeftColor;
        //    }
        //    set
        //    {
        //        if (valueButton.GradientLeftColor != value)
        //        {
        //            valueButton.GradientLeftColor = value;
        //            //if (GradientEnabled) AddToQueue(false);
        //            OnPropertyChanged("GradientLeftColor");
        //        }
        //    }
        //}


        ///// <summary>
        ///// Gets or sets the Color at the left hand side of the title bar
        ///// </summary>
        //[Category("Gradient")]
        //[DefaultValue(typeof(Color), "Navy")]
        //[Description("Gets or sets the Color at the right hand side of the control")]
        //public Color GradientRightColor
        //{
        //    get
        //    {
        //        return valueButton.GradientRightColor;
        //    }
        //    set
        //    {
        //        if (valueButton.GradientRightColor != value)
        //        {
        //            valueButton.GradientRightColor = value;
        //            //if (valueButton.GradientEnabled) AddToQueue(false);

        //            OnPropertyChanged("GradientRightColor");
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets the Color at the left hand side of the title bar
        /// </summary>
        [Category("Gradient")]
        [DefaultValue(typeof(Color), "Navy")]
        [Description("Gets or sets the linerar gradient mode")]
        public LinearGradientMode LinearGradientMode
        {
            get
            { 
                return valueButton.LinearGradientMode;
            }
            set
            {
                if (valueButton.LinearGradientMode != value)
                {
                    valueButton.LinearGradientMode = value;
                    //if (GradientEnabled) AddToQueue(false);

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
        public override Channels.IInfoChannel SourceChannel
		{
			set
			{
				valueButton.SourceChannel = value ;
				valueButton.DestinationChannel = value ;
				base.SourceChannel = value ;
				NewButtonText(Text) ;
				UpdateColor() ;
			}
		}

        private bool ShouldSerializeSourceChannel()
        {
            return false;
        }

		/// <summary>
		/// Gets or sets the name of the object to be the Button's InputEnabled Source
		/// </summary>
		/// <remarks>If this is set to a valid channel then it overrides the value of InputEnabled.</remarks>
		[Category("Button")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Button's InputEnabled Source. If this is set to a valid channel then it overrides the value of InputEnabled")]
		public string InputEnabledSource
		{
			get
			{
				return this.valueButton.InputEnabledSource ;
			}
			set
			{
				if (valueButton.InputEnabledSource != value)
				{
					valueButton.InputEnabledSource = value ;
					OnPropertyChanged("InputEnabledSource") ;
				}
			}
		}
        /// <summary>
        /// Gets or sets the name of the object to be the Button's TimedDelayInterval Source
        /// </summary>
        /// <remarks>If this is set to a valid channel then it overrides the value of TimedDelayInterval.</remarks>
        [Category("Button")]
        [DefaultValue("")]
        [Description("Gets or sets the name of the object to be the Button's TimedDelayInterval Source. If this is set to a valid channel then it overrides the value of TimedDelayInterval")]
        public string TimeDelayIntervalSource
        {
            get
            {
                return this.valueButton.TimeDelayIntervalSource;
            }
            set
            {
                if (valueButton.TimeDelayIntervalSource != value)
                {
                    valueButton.TimeDelayIntervalSource = value;
                    OnPropertyChanged("TimeDelayIntervalSource");
                }
            }
        }
        /// <summary>
		/// Gets or sets a Custom Description
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets a Custom Description")]
		[DefaultValue("")]
		public override string CustomDescription
		{
            get
            {
                return base.CustomDescription ;
            }

			set
			{
				base.CustomDescription = value ;

				if (DescriptionSource == KineticaRT.Studio.Controls.TextSources.Custom)
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
		[DefaultValue(KineticaRT.Studio.Controls.TextSources.Description)]
		public override KineticaRT.Studio.Controls.TextSources DescriptionSource
		{
            get
            {
                return base.DescriptionSource ;
            }
            set
			{
				base.DescriptionSource = value ;
				Invalidate(descriptionRect) ;
			}
		}

		/// <summary>
		/// Gets or Sets the Color of the button
		/// </summary>
		[Category("Button")]
		[Description("Gets or Sets the Color of the button")]
		public Color ButtonColor
		{
			get
			{
				return buttonColor ;
			}
			set
			{
				buttonColor = value ;
				UpdateColor() ;
				OnPropertyChanged("ButtonColor") ;
			}
		}
		private bool ShouldSerializeButtonColor()
		{
			return buttonColor != DefaultBackColor ;
		}

		/// <summary>
		/// Gets or Sets the alternate Color of the button
		/// </summary>
		[Category("Button")]
		[Description("Gets or Sets the alternate Color of the button")]
		public Color ButtonAlternateColor
		{
			get
			{
				return buttonAlternateColor ;
			}
			set
			{
				buttonAlternateColor = value ;
				UpdateColor() ;
				OnPropertyChanged("ButtonAlternateColor") ;
			}
		}
		private bool ShouldSerializeButtonAlternateColor()
		{
			return buttonAlternateColor != DefaultBackColor ;
		}

        //ft
        /// <summary>
        /// Gets or Sets the Color of the button
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or Sets the Color of the button")]
        public Color ButtonGradientLeftColor
        {
            get
            {
                return buttonGradientLeftColor;
            }
            set
            {
                buttonGradientLeftColor = value;
                UpdateColor();
                OnPropertyChanged("ButtonGradientLeftColor");
            }
        }
        //ft
        /// <summary>
        /// Gets or Sets the Color of the button
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or Sets the Gradient RightColor of the button")]
        public Color ButtonGradientRightColor
        {
            get
            {
                return buttonGradientRightColor;
            }
            set
            {
                buttonGradientRightColor = value;
                UpdateColor();
                OnPropertyChanged("ButtonGradientRightColor");
            }
        }
        //ft
        /// <summary>
        /// Gets or Sets the alternate Color of the button
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or Sets the gradient alternate left Color of the button")]
        public Color ButtonGradientAlternateLeftColor
        {
            get
            {
                return buttonGradientAlternateLeftColor;
            }
            set
            {
                buttonGradientAlternateLeftColor = value;
                UpdateColor();
                OnPropertyChanged("ButtonGradientAlternateLeftColor");
            }
        }
        //ft
        /// <summary>
        /// Gets or Sets the alternate Color of the button
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or Sets the gradient alternate right Color of the button")]
        public Color ButtonGradientAlternateRightColor
        {
            get
            {
                return buttonGradientAlternateRightColor;
            }
            set
            {
                buttonGradientAlternateRightColor = value;
                UpdateColor();
                OnPropertyChanged("ButtonGradientAlternateRightColor");
            }
        }

		/// <summary>
		/// Gets or Sets the alternate Color of the button
		/// </summary>
		[Category("Button")]
		[Description("Gets or Sets the alternate Color of the button text")]
		public Color ButtonAlternateTextColor
		{
			get
			{
				return buttonAlternateTextColor ;
			}
			set
			{
				buttonAlternateTextColor = value ;
				UpdateColor() ;
				OnPropertyChanged("ButtonAlternateTextColor") ;
			}
		}
		private bool ShouldSerializeButtonAlternateTextColor()
		{
			return buttonAlternateTextColor != DefaultForeColor ;
		}

		/// <summary>
		/// Gets or Sets the Color of the button text
		/// </summary>
		[Category("Button")]
		[Description("Gets or Sets the Color of the button text")]
		public Color ButtonTextColor
		{
			get
			{
				return buttonTextColor ; //valueButton.ForeColor ;
			}
			set
			{
				buttonTextColor = value ; //valueButton.ForeColor = value ;
				OnPropertyChanged("ButtonTextColor") ;
			}
		}
		private bool ShouldSerializeButtonTextColor()
		{
			return valueButton.ForeColor != DefaultForeColor ;
		}

		/// <summary>
		/// Gets or sets whether to use the standard or alternate button color
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets whether to use the standard or alternate button color")]
		[DefaultValue(false)]
		public AlternateColorMode UseAlternateColor
		{
			get
			{
				return useAlternateColor ;
			}
			set
			{
				if (value != useAlternateColor)
				{
					useAlternateColor = value ;
					UpdateColor() ;
					OnPropertyChanged("UseAlternateColor") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Percentage width devoted to the Description
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets the Percentage width devoted to the Description")]
		[DefaultValue(60)]
		public int DescriptionPercent
		{
			get
			{
				return descriptionPercent ;
			}
			set
			{
				if (value != descriptionPercent)
				{
					descriptionPercent = value ;
					OnPropertyChanged("DescriptionPercent") ;
					UpdateLayout() ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the description is to the left of the button
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets whether the description is to the left of the button")]
		[DefaultValue(true)]
		public bool DescriptionOnLeft
		{
			get
			{
				return descriptionOnLeft ;
			}
			set
			{
				if (value != descriptionOnLeft)
				{
					descriptionOnLeft = value ;
					UpdateLayout() ;
					OnPropertyChanged("DescriptionOnLeft") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Alignment of the description
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets the Alignment of the description")]
		[DefaultValue(StringAlignment.Near)]
		public StringAlignment DescriptionAlignment
		{
			get
			{
				return descriptionAlignment ;
			}
			set
			{
				if (descriptionAlignment != value)
				{
					descriptionAlignment = value ;
					stringFormat.Alignment = value ;
					OnPropertyChanged("DescriptionAlignment") ;
					Invalidate(descriptionRect) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether input is enabled
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets whether input is enabled")]
		[DefaultValue(true)]
		public bool InputEnabled
		{
			get
			{
				return valueButton.InputEnabled ;
			}
			set
			{
				valueButton.InputEnabled = value ;
				OnPropertyChanged("InputEnabled") ;
			}
		}
        /// <summary>
        /// Gets or sets whether input is enabled
        /// </summary>
        [Category("Button")]
        [Description("Gets or sets the time delay intervalin msec")]
        [DefaultValue(100)]
        public int TimeDelayInterval
        {
            get
            {
                return valueButton.TimeDelayInterval;
            }
            set
            {
                if (value <= 0)
                {
                    value = 1;
                }
                valueButton.TimeDelayInterval = value;
                OnPropertyChanged("TimeDelayInterval");
            }
        }
        /// <summary>
		/// Gets or sets where to get the button text from
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets where to get the button text from")]
		[DefaultValue(TextSources.TrueText)]
		public TextSources TextSource
		{
			get
			{
				return textSource ;
			}
			set
			{
				if (textSource != value)
				{
					textSource = value ;
					OnPropertyChanged("TextSource") ;
					NewButtonText(Text) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets Custom button text
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets Custom button text")]
		[DefaultValue("")]
		public string CustomText
		{
			get
			{
				return customText ;
			}
			set
			{
				if (value != customText)
				{
					customText = value ;
					OnPropertyChanged("CustomText") ;
					
					if (textSource == TextSources.Custom)
					{
						OnPropertyChanged("Text") ;
						NewButtonText(Text) ;
					}
				}
			}
		}

		/// <summary>
		/// Gets the Text displayed on the button. Value depends on TextSource property
		/// </summary>
		[Category("Button")]
		[Description("Gets the Text displayed on the button. Value depends on TextSource property")]
		public override string Text
		{
			get
			{
				string			text		= "" ;

				if (textSource == TextSources.Custom)
				{
					text = customText ;
				}
				else if (textSource == TextSources.None)
				{
					//nothing to do
				}
				else if (textSource == TextSources.CurrentStateText)
				{
					Channels.IBooleanChannel boolean = SourceChannel as Channels.IBooleanChannel ;

					if (boolean != null)
					{
						if ((boolean.Value != null) && KineticaRT.Values.Values.True.Equals(boolean.Value))
						{
							text = ((Channels.IBooleanChannel)SourceChannel).TrueText ;
						}
						else
						{
							text = ((Channels.IBooleanChannel)SourceChannel).FalseText ;
						}
					}
				}
				else if (textSource == TextSources.FutureStateText)
				{
					Channels.IBooleanChannel boolean = SourceChannel as Channels.IBooleanChannel ;

					if (boolean != null)
					{
						if ((boolean.Value != null) && KineticaRT.Values.Values.True.Equals(boolean.Value))
						{
							text = ((Channels.IBooleanChannel)SourceChannel).FalseText ;
						}
						else
						{
							text = ((Channels.IBooleanChannel)SourceChannel).TrueText ;
						}
					}
				}
				else if (SourceChannel != null)
				{
					if (textSource == TextSources.Description)
					{
						text = SourceChannel.Description ;
					}
					else if (SourceChannel is Channels.IBooleanChannel)
					{
						text = ((Channels.IBooleanChannel)SourceChannel).TrueText ;
					}
				}
				return text ;
			}
		}

		/// <summary>
		/// Gets or sets how the button reacts when pressed
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets how the button reacts when pressed")]
		[DefaultValue(KineticaRT.Controls.ButtonReturnTypes.Normal)]
		public KineticaRT.Controls.ButtonReturnTypes ReturnType
		{
			get
			{
				return valueButton.Type ;
			}
			set
			{
				valueButton.Type = value ;
				OnPropertyChanged("ReturnType") ;
			}
		}

		/// <summary>
		/// Gets or sets the Shape of the Control
		/// </summary>
		[Category("Button")]
		[DefaultValue(KineticaRT.Controls.Shapes.Rectangle)]
		[Description("Gets or sets the Shape of the Control (Rectangle, Rounded Rectangle, Ellipse)")]
		public KineticaRT.Controls.Shapes Shape
		{
			get
			{
				return valueButton.Shape ;
			}
			set
			{
				valueButton.Shape = value ;
				OnPropertyChanged("Shape") ;
			}
		}


        /// <summary>
		/// Gets or sets the current value
		/// </summary>
        [Category("Button")]
        [Description("Gets or sets the current value")]
        [DefaultValue(0)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override KineticaRT.Values.Value Value //ft Ajout de cette propriété pcq on heritait de celle de base(Value ds ControlBase.cs) , incompatible lorsque la source sur le control etait null, la propriété Value heritée retournait toujours NotConnected
        {
            get
            {
                Values.Value new_krt_value = KineticaRT.Values.Values.NotConnected;


                //convert to boolean
                new_krt_value = valueButton.Value as Values.Boolean;
                if (new_krt_value == null)
                {
                    if (valueButton.Value is Values.Double)
                    {
                        new_krt_value = new Values.Boolean(((Values.Double)valueButton.Value).ToDouble() != 0);
                    }
                    else if (valueButton.Value is System.Boolean)
                    {
                        new_krt_value = new KineticaRT.Values.Boolean((System.Boolean)valueButton.Value);
                    }
                    else if (valueButton.Value is Values.Text)
                    {
                        try
                        {
                            new_krt_value = new KineticaRT.Values.Boolean(System.Convert.ToBoolean(valueButton.Value));
                        }
                        catch (System.FormatException)
                        {
                            new_krt_value = KineticaRT.Values.Values.False;
                        }
                    }
                    else if (valueButton.Value is System.String)
                    {
                        try
                        {
                            new_krt_value = new KineticaRT.Values.Boolean(System.Convert.ToBoolean(valueButton.Value));
                        }
                        catch (System.FormatException)
                        {
                            new_krt_value = KineticaRT.Values.Values.False;
                        }
                    }
                }
                //disallow invalid doubles
                if ((new_krt_value == null) && (valueButton.Value is System.Double) &&
                    (!System.Double.IsInfinity((System.Double)valueButton.Value)) &&
                    (!System.Double.IsNaN((System.Double)valueButton.Value)))
                {
                    new_krt_value = new KineticaRT.Values.Boolean((System.Double)valueButton.Value != 0);
                }
                if (new_krt_value == null)
                {
                    new_krt_value = Values.Values.TypeMismatch;
                }
                return new_krt_value;
            }
        }

		#endregion

		#region Drawing
		/// <summary>
		/// Paint Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e) ;

			//draw description text
			if (descriptionRect.Width > 0)
			{
				Brush		brush = new SolidBrush(ForeColor) ;
				e.Graphics.DrawString(Description, Font, brush, descriptionRect, stringFormat) ;
				brush.Dispose() ;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the layout of value, description and units and causes a repaint
		/// </summary>
		protected override void UpdateLayout()
		{
			//Min sizes (for exception prevention)
			//Button: 0x0

			descriptionRect = new Rectangle(0, DisplayRectangle.Y, 0, DisplayRectangle.Height) ;
	
			valueButton.Height	= DisplayRectangle.Height ;

			descriptionRect.Width	= (int)(0.01 * descriptionPercent * DisplayRectangle.Width) ;
			valueButton.Width		= Math.Max(DisplayRectangle.Width - descriptionRect.Width, 0) ;

			if (descriptionOnLeft)
			{
				descriptionRect.X	= DisplayRectangle.Left ;
				valueButton.Left	= descriptionRect.Right ;
			}
			else
			{
				valueButton.Left	= DisplayRectangle.Left ;
				descriptionRect.X	= valueButton.Right ;
			}

			Invalidate(DisplayRectangle) ;
		}


		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueButton.ToolTipText = NewText ;
			base.NewToolTip(NewText) ;
		}

		private void UpdateColor()
		{
            //ft Lorsque le bouton n'as pas de source directement source sur le control dans la form, l'update de alternate couleur a vert lorsque True ne marche pas.
			//if ((Value != null) && (useAlternateColor == AlternateColorMode.Yes || (useAlternateColor == AlternateColorMode.Auto && Value.CompareTo(KineticaRT.Values.Values.True)==0)))
            //maintenant this.Value pointe retourne la valeur de valueButton.Value, plutot que la Value ds ControlBase.cs
            if ((this.Value != null) && (useAlternateColor == AlternateColorMode.Yes || (useAlternateColor == AlternateColorMode.Auto && (this.Value.CompareTo(KineticaRT.Values.Values.True)) == 0)))
			{
				valueButton.BackColor = buttonAlternateColor ;
				valueButton.ForeColor = buttonAlternateTextColor ;
                valueButton.GradientLeftColor = buttonGradientAlternateLeftColor; //ft
                valueButton.GradientRightColor = buttonGradientAlternateRightColor; //ft
			}
			else
			{
				valueButton.BackColor = buttonColor ;
				valueButton.ForeColor = buttonTextColor ;
                valueButton.GradientLeftColor = buttonGradientLeftColor; //ft
                valueButton.GradientRightColor = buttonGradientRightColor; //ft

			}
		}

		private void NewButtonText(string newText)
		{
			valueButton.Text = newText ;
		}

		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnSourcePropertyChanged(object Sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
				if ((textSource == TextSources.Description && e.PropertyName == "Description")||
					(textSource == TextSources.TrueText && e.PropertyName == "TrueText")			)
				{
					NewButtonText(Text) ;
				}
			
				Invalidate(DisplayRectangle) ;
			}
			base.OnSourcePropertyChanged(Sender, e) ;
		}
		#endregion

		#region IButton
		/// <summary>Occurs when the value of the Value property changes.</summary>
		public event KineticaRT.Controls.ValueChangedEventHandler ValueChanged ;

		private void OnButtonValueChanged(object sender, Values.Value newValue)
		{
            //ft Lorsque le bouton n'as pas de source directement source sur le control dans la form, l'update de alternate couleur a vert lorsque True ne marche pas.
           
			System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Value Change: " + newValue.ToString(), "Studio.Controls.Button: " + this.Name) ;
			UpdateColor() ;

			if ((textSource == TextSources.CurrentStateText) || (textSource == TextSources.FutureStateText))
			{
				NewButtonText(Text) ;
			}
			
			Invalidate(DisplayRectangle) ;


			if (ValueChanged != null)
			{
				ValueChanged(this, newValue) ;
			}
		}

		/// <summary>Sets the value of the button to Pressed or Unpressed</summary>
		/// <param name="Pressed">True to set the Value to Pressed; False to set the Value to Unpressed</param>
		public void SetButtonState(bool Pressed)
		{
			valueButton.Value = Pressed ? Values.Values.True : Values.Values.False ;
		}
		#endregion

		private object valueButton_ObjectSearch(string objectName)
		{
			return this.OnObjectSearch(objectName);
		}

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect() ;
			
			this.valueButton.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			this.valueButton.Disconnect () ;
			base.Disconnect() ;
		}
		#endregion
	}
}

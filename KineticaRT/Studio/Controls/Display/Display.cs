using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using KineticaRT;
using System.Drawing.Drawing2D;

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// KineticaRT Studio Display control.
	/// Displays and optionally edits a numeric, or text Value from a source channel,
	/// together with an optional description and units
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(Display.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Display))]
	public class Display : NumericControl
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
						if ((key == null) || (key != "E75538FF2708"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "E75538FF2708")) key = "{E75538FF-2708-47fb-8F14-DA3528B253C2}" ; 
						context.SetSavedLicenseKey (typeof(Display), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Display), null) ;
					if (key == "{E75538FF-2708-47fb-8F14-DA3528B253C2}") licence = new License ("Run") ;
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

		private KineticaRT.Controls.Display valueDisplay;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

//		private KineticaRT.Channels.IChannel srcHdlrChannel ;
		private KineticaRT.Channels.INumericThresholdChannel threshold_channel ;
		private LayoutsDVU		sectionLayout ;
		private int				unitsPercent ;
		private int				valuePercent ;
		private Color			inputBackColor ;
		private Color			valueBackColor ;

        private Rectangle unitsRect;
		private Rectangle		descriptionRect ;
		private StringFormat	stringFormat ;

        /// <summary>
        /// Current value of UpdateSpeed property
        /// </summary>
        private                 KineticaRT.Controls.UpdateSpeeds updateSpeed;//ft

        private SolidBrush      foreColorBrush;//ft  test pour optimiser en vitesse
        private                 Color foreColor;//ft test pour optimiser en vitesse

		//private Channels.ValueChangedEventHandler	srcHdlr ;

		#region Initialisation and disposal
		/// <summary>
		/// Initialises a new instance of the Display class
		/// </summary>
		public Display()
		{
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			stringFormat = new StringFormat() ;
			stringFormat.LineAlignment	= StringAlignment.Center ;
			stringFormat.Alignment		= StringAlignment.Near ;

			unitsPercent			= 15 ;
			valuePercent			= 25 ;
			sectionLayout			= LayoutsDVU.DescriptionValueUnits ;
			inputBackColor			= SystemColors.Window ;
			valueBackColor			= Color.Empty ;
            foreColorBrush          = new SolidBrush(Color.Empty); //ft optimisation en vitesse

			//srcHdlr		= new Channels.ValueChangedEventHandler(OnValueChanged) ;

            // valueDisplay.AllowDrop = false; SB removed because it is overriding the user values
            // base.AllowDrop = true; SB removed because it is overriding the user values
            this.valueDisplay.UpdateSpeed = this.UpdateSpeed;//ft
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//ft 
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true); //ft pour empecher le flickering du control

			UpdateLayout() ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                valueChangedHdlr = null;
                ValueChanged = null;
                foreColorBrush.Dispose();
                foreColorBrush = null;
                valueDisplay.Dispose();
                valueDisplay = null;
                if (components != null)
                    components.Dispose();
                stringFormat.Dispose();
               
            }
        }
        #endregion

        #region Component Designer generated code

        private event EventHandler valueChangedHdlr;
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.valueDisplay = new KineticaRT.Controls.Display();
            this.valueDisplay.DisplayValueChanged += OnDisplayValueChanged;
            
            this.SuspendLayout();
            // 
            // valueDisplay
            // 
            // this.valueDisplay.AllowDrop = true; SB removed because it is overriding the user values
            this.valueDisplay.BorderType = KineticaRT.Controls.BorderType.EditBox;
            this.valueDisplay.DefaultGradientColor = System.Drawing.Color.Empty;
            this.valueDisplay.GradientEnabled = false;
            this.valueDisplay.InputEnabled = false;
            this.valueDisplay.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.valueDisplay.Location = new System.Drawing.Point(104, 0);
            this.valueDisplay.Name = "valueDisplay";
            this.valueDisplay.Size = new System.Drawing.Size(56, 24);
            this.valueDisplay.TabIndex = 0;
            this.valueDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Display
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.valueDisplay);
            this.Name = "Display";
            this.Size = new System.Drawing.Size(200, 24);
            this.ResumeLayout(false);

		}
		#endregion

		#region Properties
        ///// <summary>
        ///// Gets or Sets the style of the Control's Border
        ///// </summary>
        //[Category("Control")]
        //[DefaultValue(KineticaRT.Controls.BorderStyles.EditBox)]
        //[Description("Gets or Sets the style of the Control's Border")]
        //public new KineticaRT.Controls.BorderStyles BorderStyle
        //{
        //    get { return valueDisplay.BorderStyle; }
        //    set { valueDisplay.BorderStyle = value; }
        //}

        /// <summary>
		/// Gets or Sets the style of the Control's Border
		/// </summary>
		[Category("Control")]
        [DefaultValue(KineticaRT.Controls.BorderType.EditBox)]
		[Description("Gets or Sets the style of the Control's Border")]
        public virtual KineticaRT.Controls.BorderType BorderType
		{
            get { return valueDisplay.BorderType; }
            set { valueDisplay.BorderType = value; }
		}

		/// <summary>
		/// Gets or Sets the style of the Control's Border
		/// </summary>
		[Category("Control")]
		[Description("Gets or Sets the Color of the Control's Border")]
		public virtual Color BorderColor
		{
			get	{ return valueDisplay.BorderColor; }
			set	{ valueDisplay.BorderColor = value; }
		}
		private bool ShouldSerializeBorderColor()
		{
			return valueDisplay.BorderColor != Color.LightSteelBlue ;
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
		public virtual double BorderWidth
		{
			get	{ return valueDisplay.BorderWidth; }
			set	{ valueDisplay.BorderWidth = value; }
		}

	    //ft nouvelle propriete Text pour UIAutomation pour permettre de voir la valeur du Display via la propriete default d un textbox
        /// <summary>
        /// Gets or sets the Source channel to which this object is connected
        /// </summary>
        [Category("General")]
        [Description("Gets or sets the Source channel to which this object is connected")]
        [DefaultValue(null)]
        public override String Text
        {
            get
            {
                return valueDisplay.Text;
            }

        }

        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        [Browsable(true)]
        [Category("General")]
        public override string Name //ft oveerridé la propriété de base pour nommer le nom du control valueDisplay afin de faciliter le debuggage du krt.control.display
        {
            get { return base.Name; }
            set 
            { 
                base.Name = value;
                if (valueDisplay != null)
                    valueDisplay.Name = "valueDisplay" + this.Name;
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
                return valueDisplay.GradientEnabled;
            }
            set
            {
                if (valueDisplay.GradientEnabled != value)
                {
                    valueDisplay.GradientEnabled = value;
                    
                    OnPropertyChanged("GradientEnabled");
                }
            }
        }
        //ft
        /// <summary>
        /// Gets or sets whether the Title bar is enabled
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets whether the Gradient is enabled")]
        public Color DefaultGradientColor
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
        public LinearGradientMode LinearGradientMode
        {
            get
            {
                return valueDisplay.LinearGradientMode;
            }
            set
            {
                if (valueDisplay.LinearGradientMode != value)
                {
                    valueDisplay.LinearGradientMode = value;
                    
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
		public override Channels.IInfoChannel SourceChannel
		{
			set
			{
				//disconnect old destination (if any) before continuing
				valueDisplay.DestinationChannel = null ;

				valueDisplay.SourceChannel = value ;

				//connect new destination if required
				if (valueDisplay.InputEnabled)
				{
					valueDisplay.DestinationChannel = value ;
				}

//				if (srcHdlrChannel != null)
//				{
//					srcHdlrChannel.ValueChanged -= srcHdlr ;
//					srcHdlrChannel = null ;
//				}


				base.SourceChannel = value ;

				// Next bit done after connecting property change event hander in base.SourceChannel.
				threshold_channel = value as KineticaRT.Channels.INumericThresholdChannel ;
				if (threshold_channel != null)
				{
                    OnSourcePropertyChanged(threshold_channel, KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs); // Called so that only one piece of code sets the actual colours.
				}
				else
				{
					if (this.InputEnabled)
					{
						valueDisplay.BackColor = inputBackColor ;
					}
					else
					{
						valueDisplay.BackColor = valueBackColor ;
					}
				}
              
				//				if (threshold_channel != null)
//				{
//					srcHdlrChannel = value ;
//					srcHdlrChannel.ValueChanged += srcHdlr ;
//					this.OnValueChanged(value, value.Value) ;
//				}
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
                return base.CustomDescription;
            }
            set
			{
				base.CustomDescription = value ;

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
		public override TextSources DescriptionSource
		{
            get
            {
                return base.DescriptionSource;
            }
            set
			{
				base.DescriptionSource = value ;
				Invalidate(descriptionRect) ;
			}
		}

		/// <summary>
		/// Gets or sets a Custom Units string
		/// </summary>
		[Category("Units")]
		[Description("Gets or sets a Custom Units string")]
		[DefaultValue("")]
		public override string CustomUnits
		{
            get
            {
                return base.CustomUnits;
            }
			set
			{
				base.CustomUnits = value ;

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
		public override bool UseCustomUnits
		{
            get
            {
                return base.UseCustomUnits;
            }
			set
			{
				base.UseCustomUnits = value ;
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
		public override string Format
		{
            get
            {
                return base.Format;
            }
			set
			{
				base.Format = value ;
				valueDisplay.Format = base.Format ;
			}
		}
        /// <summary>
        /// Gets or Sets the Color of the Value text
        /// </summary>
        [Category("Display")]
        [Description("Gets or Sets the Color of the Value text")]
        public override Color ForeColor //ft ajout/ pour optimisation en vitesse
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
                foreColorBrush.Color = value; //ft optimisation en vitesse
                OnPropertyChanged("ForeColor");
                UpdateLayout();//ft force a redessiner
            }
        }
        private bool ShouldSerializeForeColor() //ft ajout
        {
            return true;
        }

		/// <summary>
		/// Gets or Sets the Color of the Value text
		/// </summary>
		[Category("Display")]
		[Description("Gets or Sets the Color of the Value text")]
		public Color ValueColor
		{
			get
			{
				return valueDisplay.ForeColor ;
			}
			set
			{
				valueDisplay.ForeColor = value ;
				OnPropertyChanged("ValueColor") ;
			}
		}
		private bool ShouldSerializeValueColor()
		{
			//return valueDisplay.ForeColor != DefaultForeColor ;
            return true;
		}

		/// <summary>
		/// Gets or Sets the background Color of the Value text
		/// </summary>
		[Category("Display")]
		[Description("Gets or Sets the background Color of the Value text")]
		public Color ValueBackColor
		{
			get
			{
				return valueBackColor ;
			}
			set
			{
				valueDisplay.BackColor = value ;
				valueBackColor =  value ;
				OnPropertyChanged("ValueBackColor") ;
			}
		}
		private bool ShouldSerializeValueBackColor()
		{
			return valueBackColor != Color.Empty ;
		}

		/// <summary>
		/// Gets or Sets whether the rate at which the Control redraws
		/// </summary>
		[Category("Control")]
		[DefaultValue(KineticaRT.Controls.UpdateSpeeds.Slow)]
		[Description("Gets or Sets the rate at which the Control redraws")]
		public virtual KineticaRT.Controls.UpdateSpeeds UpdateSpeed
		{
			get
			{
                return updateSpeed;
			}
			set
			{
                updateSpeed = value;//ft
				valueDisplay.UpdateSpeed = value ;
			}
		}
        /// <summary>
        /// Gets or Sets the Tool Tip Text that appears when hovering over the control
        /// </summary>
        [Category("ErrorProvider")]
        [DefaultValue("")]
        [Description("Gets or Sets the Tool Tip Text that appears when hovering over the control")]
        public bool ErrorProviderEnabled //ft
        {
            get { return valueDisplay.ErrorProviderEnabled; }
            set { valueDisplay.ErrorProviderEnabled = value; }
        }

		/// <summary>
		/// Gets or sets the Percentage width devoted to the Units string
		/// </summary>
		[Category("Display")]
		[Description("Gets or sets the Percentage width devoted to the Units string")]
		[DefaultValue(15)]
		public int UnitsPercent
		{
			get
			{
				return unitsPercent ;
			}
			set
			{
				if (value != unitsPercent)
				{
					unitsPercent = value ;
					OnPropertyChanged("UnitsPercent") ;
					UpdateLayout() ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Percentage width devoted to the Value
		/// </summary>
		[Category("Display")]
		[Description("Gets or sets the Percentage width devoted to the Value")]
		[DefaultValue(25)]
		public int ValuePercent
		{
			get
			{
				return valuePercent ;
			}
			set
			{
				if (value != valuePercent)
				{
					valuePercent = value ;
					OnPropertyChanged("ValuePercent") ;
					UpdateLayout() ;
				}
			}
		}
     

		/// <summary>
		/// Gets or sets the order of Value, Units and Description sections
		/// </summary>
		[Category("Display")]
		[Description("Gets or sets the order of Value, Units and Description sections")]
		[DefaultValue(LayoutsDVU.DescriptionValueUnits)]
		public LayoutsDVU SectionLayout
		{
			get
			{
				return sectionLayout ;
			}
			set
			{
				if (value != sectionLayout)
				{
					sectionLayout = value ;
					UpdateLayout() ;
					OnPropertyChanged("SectionLayout") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether input is enabled
		/// </summary>
		[Category("Display")]
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

					if (value)
					{
						valueDisplay.BackColor = inputBackColor ;
						valueDisplay.DestinationChannel = SourceChannel ;
					}
					else
					{
						valueDisplay.BackColor = valueBackColor ;
						valueDisplay.DestinationChannel = null ;
					}

					OnPropertyChanged("InputEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether to only accept input that is numeric.
		/// Does not prevent text values being displayed.
		/// </summary>
		[Category("Display")]
		[Description("Gets or sets whether to only accept input that is numeric. Does not prevent text values being displayed.")]
		[DefaultValue(false)]
		public bool InputNumeric
		{
			get
			{
				return valueDisplay.NumericOnly ;
			}
			set
			{
				valueDisplay.NumericOnly = value ;
				OnPropertyChanged("InputNumeric") ;
			}
		}

		/// <summary>
		/// Gets or sets the background color of the input area when input is enabled
		/// </summary>
		[Category("Display")]
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

		
		
		#endregion

		#region Drawing
		/// <summary>
		/// Paint Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnPaint(PaintEventArgs e)
		{
            base.OnPaint(e) ;

            ////ft using (Brush brush = new SolidBrush(ForeColor))
            ////ft {

            //draw units and description text
            if (descriptionRect.Width > 0)
            {
                e.Graphics.DrawString(Description, Font, foreColorBrush, descriptionRect, stringFormat);
            }
            if (unitsRect.Width > 0)
            {
                e.Graphics.DrawString(Units, Font, foreColorBrush, unitsRect, stringFormat);
            }
            //ft}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the layout of value, description and units and causes a repaint
		/// </summary>
		protected override void UpdateLayout()
		{
			//Min sizes (for exception prevention)
			//Display: 4x0

			unitsRect		= new Rectangle(0, DisplayRectangle.Y, 0, DisplayRectangle.Height) ;
			descriptionRect = new Rectangle(0, DisplayRectangle.Y, 0, DisplayRectangle.Height) ;
			
			valueDisplay.Height	= DisplayRectangle.Height ;

			valueDisplay.Width		= Math.Max((int)(0.01 * valuePercent * DisplayRectangle.Width), 4) ;
			unitsRect.Width			= (int)(0.01 * unitsPercent * DisplayRectangle.Width) ;
			descriptionRect.Width	= DisplayRectangle.Width - valueDisplay.Width - unitsRect.Width ;
			
			switch (sectionLayout)
			{
				case LayoutsDVU.ValueDescriptionUnits:
				{
					valueDisplay.Left	= DisplayRectangle.Left ;
					descriptionRect.X	= valueDisplay.Right ;
					unitsRect.X			= descriptionRect.Right ;
				}
				break ;

				case LayoutsDVU.ValueUnitsDescription:
				{
					valueDisplay.Left	= DisplayRectangle.Left ;
					unitsRect.X			= valueDisplay.Right ;
					descriptionRect.X	= unitsRect.Right ;
				}
				break ;

				case LayoutsDVU.DescriptionUnitsValue:
				{
					descriptionRect.X	= DisplayRectangle.Left ;
					unitsRect.X			= descriptionRect.Right ;
					valueDisplay.Left	= unitsRect.Right ;
				}
				break ;

				case LayoutsDVU.UnitsDescriptionValue:
				{
					unitsRect.X			= DisplayRectangle.Left ;
					descriptionRect.X	= unitsRect.Right ;
					valueDisplay.Left	= descriptionRect.Right ;
				}
				break ;

				case LayoutsDVU.DescriptionValueUnits:
				{
					descriptionRect.X	= DisplayRectangle.Left ;
					valueDisplay.Left	= descriptionRect.Right ;
					unitsRect.X			= valueDisplay.Right ;
				}
				break ;

				case LayoutsDVU.UnitsValueDescription:
				{
					unitsRect.X			= DisplayRectangle.Left ;
					valueDisplay.Left	= unitsRect.Right ;
					descriptionRect.X	= valueDisplay.Right ;
				}
				break ;
			}

			Invalidate(DisplayRectangle) ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueDisplay.ToolTipText = NewText ;
			base.NewToolTip(NewText) ; 
		}

		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnSourcePropertyChanged(object Sender, PropertyChangedEventArgs e)
		{
			if (e == KineticaRT.Channels.Channel.ValuePropertyChangedEventArgs) return ;

			if (e == KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs)
			{
				if (threshold_channel != null)
				{
					if (threshold_channel.Color == Color.Empty)
					{
                        this.valueDisplay.BackColor = valueBackColor ;
                    }
					else
					{                       
						this.valueDisplay.BackColor = threshold_channel.Color ;
					}
                    
				}
				else
				{
					this.valueDisplay.BackColor = valueBackColor ;
				}

				return ;
			}

            if (e.PropertyName != "Value" && e.PropertyName != "ValueObject")
			{
				Invalidate(DisplayRectangle) ;
			}

			base.OnSourcePropertyChanged(Sender, e) ;
		}

        /// <summary>
        /// Mouse Leave Event
        /// </summary>
        /// <param name="e">Event Data</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this.valueDisplay.ErrorProviderUpdateEnabled=false;
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Mouse Enter Event
        /// </summary>
        /// <param name="e">Event Data</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.valueDisplay.ErrorProviderUpdateEnabled=false;
            base.OnMouseEnter(e);
        }

//		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value v)
//		{
//			if (v == null) return ;
//
//			if (threshold_channel != null)
//			{
//				double val = v.ToDouble() ;
//			
//				if (val < threshold_channel.LowLow)
//				{
//					this.valueDisplay.BackColor = threshold_channel.LowLowColor ;
//				}
//				else 
//					if (val < threshold_channel.Low)
//				{
//					this.valueDisplay.BackColor = threshold_channel.LowColor ;
//				}
//				else 
//					if (val > threshold_channel.HighHigh)
//				{
//					this.valueDisplay.BackColor = threshold_channel.HighHighColor ;
//				}
//				else
//					if (val > threshold_channel.High)
//				{
//					this.valueDisplay.BackColor = threshold_channel.HighColor ;
//				}
//				else 
//				{
//					this.valueDisplay.BackColor = threshold_channel.NormalColor ;
//				}
//			}
//		}

		#endregion

        public event EventHandler ValueChanged;//ft
        
        private void OnDisplayValueChanged(object sender, EventArgs e)//ft
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
            {
                ValueChanged(this, e);
            }
        }
	}
}

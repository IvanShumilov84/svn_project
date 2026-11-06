using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.ComponentModel;
//using System.Drawing;
using System.Data;
using System.Diagnostics;
using KineticaRT;
using System.Windows.Threading;
using System.Windows.Interop;

namespace CEL.WPF.Controls
{
    
 

    ///// <summary>
    ///// Interaction logic for DisplayStudio.xaml
    ///// </summary>
	/// <summary>
	/// KineticaRT Studio Display control.
	/// Displays and optionally edits a numeric, or text Value from a source channel,
	/// together with an optional description and units
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(Display.LicenseProvider))]
#endif
	[System.Drawing.ToolboxBitmap(typeof(Display))]
	public partial class DisplayStudio : NumericControl
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

		private CEL.WPF.Controls.Display valueDisplay;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		//ft private System.ComponentModel.Container components = null;

//		private KineticaRT.Channels.IChannel srcHdlrChannel ;
		private KineticaRT.Channels.INumericThresholdChannel threshold_channel ;
        private KineticaRT.Studio.Controls.LayoutsDVU sectionLayout;
		private int				unitsPercent ;
		private int				valuePercent ;
		private System.Windows.Media.Brush	inputBackColor ;
        private System.Windows.Media.Brush valueBackColor;

		private Rect		unitsRect ;
        private Rect        descriptionRect;
		//ft private System.Drawing.StringFormat	stringFormat ;
        private TextAlignment stringFormat;

		//private Channels.ValueChangedEventHandler	srcHdlr ;

		#region Initialisation and disposal
		/// <summary>
		/// Initialises a new instance of the Display class
		/// </summary>
		public DisplayStudio()
		{
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif

			// This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
			InitializeComponent2();

			//ft stringFormat = new System.Drawing.StringFormat() ;
            stringFormat = new TextAlignment();
            //ft stringFormat.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat = TextAlignment.Center;
            //ft stringFormat.Alignment = System.Drawing.StringAlignment.Near; //ft pas d equivalent
            

			unitsPercent			= 15 ;
			valuePercent			= 25 ;
            sectionLayout = KineticaRT.Studio.Controls.LayoutsDVU.DescriptionValueUnits;
			inputBackColor			= new SolidColorBrush(System.Windows.SystemColors.WindowColor) ;
			valueBackColor			= new SolidColorBrush(Colors.Transparent) ;

			//srcHdlr		= new Channels.ValueChangedEventHandler(OnValueChanged) ;

            valueDisplay.AllowDrop = false;
            base.AllowDrop = true;

            this.Height = Double.NaN;
            this.Width = Double.NaN;
            
            
			UpdateLayout() ;

            /////ft Changement de fonctionnement
            //this.ObjectSearch += new KineticaRT.ObjectSearchEventHandler(this.OnObjectSearch);
            //this.srcHdlr = new KineticaRT.Channels.ValueChangedEventHandler(this.Source_OnValueChanged);
            

		}
        //private void Source_OnValueChanged(KTC.IChannel sender, KTV.Value newVal)
        //{
        //    if ((this.logger != null) && (this.logger.CurrentMode != KTSL.Logger.Mode.Play) && (this.eventLogger != null) && (this.eventLogger.CurrentMode != KTSL.Logger.Mode.Play))
        //    {
        //        this.Value = newVal;
        //    }
        //}
       

        ///// <summary>
        ///// Clean up any resources being used.
        ///// </summary>
        //protected override void Dispose( bool disposing )
        //{
        //    if( disposing )
        //    {
        //        if( components != null )
        //            components.Dispose();
        //    }
        //    base.Dispose( disposing );
        //}
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent2()
		{
            if (this.valueDisplay == null)
                this.valueDisplay = new CEL.WPF.Controls.Display();
            else
                return;
			//ft this.SuspendLayout();
			// 
			// valueDisplay
			// 
            //V2 this.valueDisplay.BorderType = KineticaRT.Controls.BorderType.EditBox;
            this.valueDisplay.BorderStyle = KineticaRT.Controls.BorderStyles.EditBox;
            this.valueDisplay.InputEnabled = false;
			//ft this.valueDisplay.Location = new System.Drawing.Point(104, 0);
			this.valueDisplay.Name = "valueDisplay";
            //ft this.Width = 56;//ft this.valueDisplay.Size = new System.Drawing.Size(56, 24);
            //ft this.Height = 24;
            this.valueDisplay.Width = Double.NaN;
            this.valueDisplay.Height = Double.NaN;

            this.valueDisplay.TabIndex = 0;
			this.valueDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Fast ;
            this.valueDisplay.TextAlign = TextAlignment.Center;//ft System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Display
			// 

			//ft this.Controls.AddRange(new System.Windows.Forms.Control[] {  this.valueDisplay});
			this.Name = "Display";
            //ft this.Width = 200;//ft this.Size = new System.Drawing.Size(200, 24);
            //ft this.Height = 24;
            this.valueDisplay.Background = Brushes.Transparent;//ft doit toujours [etre transparent
            this.valueDisplay.Foreground = Brushes.White;//ft ajout
           //ft  this.valueDisplay.Content = "Test";
            //ft this.AddVisualChild(valueDisplay); //ft Nouveau  Sert a caller le redraw pour refresh?
    this.canva.Children.Add(valueDisplay);
			//ft this.ResumeLayout(false);

            //Grid.SetRow(this.canva,0);
            //Grid.SetColumn(this.canva,1);
            //this.canva.Children.Add( valueDisplay);
            
		}
		#endregion


		#region Properties
        /// <summary>
        /// Gets or Sets the style of the Control's Border
        /// </summary>
        [Category("Control")]
        [DefaultValue(KineticaRT.Controls.BorderStyles.EditBox)]
        [Description("Gets or Sets the style of the Control's Border")]
        public KineticaRT.Controls.BorderStyles BorderStyle
        {
            get { return valueDisplay.BorderStyle; }
            set { valueDisplay.BorderStyle = value; }
        }

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
		public virtual System.Windows.Media.Color BorderColor
		{
			get	{ return valueDisplay.BorderColor; }
			set	{ valueDisplay.BorderColor = value; }
		}
		private bool ShouldSerializeBorderColor()
		{
			return valueDisplay.BorderColor !=  Colors.LightSteelBlue ;
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

		/// <summary>
		/// Gets or sets the Source channel to which this object is connected
		/// </summary>
		[Category("General")]
		[Description("Gets or sets the Source channel to which this object is connected")]
		[DefaultValue(null)]
		public override KineticaRT.Channels.IInfoChannel SourceChannel
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
						valueDisplay.BackBrush = inputBackColor ;
					}
					else
					{
						valueDisplay.BackBrush = valueBackColor ;
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

				if (DescriptionSource == KineticaRT.Studio.Controls.TextSources.Custom)
				{
					//ft Invalidate(descriptionRect) ;
                    InvalidateVisual();
                    
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
                return base.DescriptionSource;
            }
            set
			{
				base.DescriptionSource = value ;
				//ft Invalidate(descriptionRect) ;
                InvalidateVisual();
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
					//ft Invalidate(unitsRect) ;
                    InvalidateVisual();
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
				//ft Invalidate(unitsRect) ;
                InvalidateVisual();
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
		public System.Windows.Media.Brush ValueColor
		{
			get
			{
				return valueDisplay.Foreground ;
			}
			set
			{
                valueDisplay.Foreground = value;
				OnPropertyChanged("ValueColor") ;
			}
		}
		private bool ShouldSerializeValueColor()
		{
			//ft return valueDisplay.Foreground != DefaultForeColor ;
            return true;
		}

		/// <summary>
		/// Gets or Sets the background Color of the Value text
		/// </summary>
		[Category("Display")]
		[Description("Gets or Sets the background Color of the Value text")]
		public System.Windows.Media.Brush ValueBackColor
		{
			get
			{
				return valueBackColor ;
			}
			set
			{
				valueDisplay.BackBrush = value ;
				valueBackColor =  value ;
				OnPropertyChanged("ValueBackColor") ;
			}
		}
		public bool ShouldSerializeValueBackColor()
		{
			return valueBackColor != new SolidColorBrush(Colors.Transparent) ;
		}

		/// <summary>
		/// Gets or Sets whether the rate at which the Control redraws
		/// </summary>
		[Category("Control")]
		[DefaultValue(KineticaRT.Controls.UpdateSpeeds.Slow)]
		[Description("Gets or Sets the rate at which the Control redraws")]
		public virtual KineticaRT.Controls.UpdateSpeeds UpdateSpeed
		{
			get{return valueDisplay.UpdateSpeed ;}
			set{valueDisplay.UpdateSpeed = value ;}
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
        [DefaultValue(KineticaRT.Studio.Controls.LayoutsDVU.DescriptionValueUnits)]
        public KineticaRT.Studio.Controls.LayoutsDVU SectionLayout
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
						valueDisplay.BackBrush = inputBackColor ;
						valueDisplay.DestinationChannel = SourceChannel ;
					}
					else
					{
                        valueDisplay.BackBrush = valueBackColor;
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
        public System.Windows.Media.Brush InputBackColor
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
                        valueDisplay.Background = inputBackColor;
					}
				}
			}
		}
		//private bool ShouldSerializeInputBackColor()
        public bool ShouldSerializeInputBackColor()
		{
			return inputBackColor != new SolidColorBrush(System.Windows.SystemColors.WindowColor) ;
		}

		
		
		#endregion

		#region Drawing
        ///// <summary>
        ///// Paint Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e) ;

        //    Brush		brush = new SolidBrush(ForeColor) ;

        //    //draw units and description text
        //    if (descriptionRect.Width > 0)
        //    {
        //        e.Graphics.DrawString(Description, Font, brush, descriptionRect, stringFormat) ;
        //    }
        //    if (unitsRect.Width > 0)
        //    {
        //        e.Graphics.DrawString(Units, Font, brush, unitsRect, stringFormat) ;
        //    }
        //    brush.Dispose() ;
        //}
         protected override void OnRender(DrawingContext drawingContext)
         {
           
             try
             {
                
             base.OnRender(drawingContext);

             this.valueDisplay.InvalidateVisual();

             //ft UpdateLayout(); //ft nouveau
            
            //System.Windows.Media.Brush brush = new SolidBrush(Foreground);
             Typeface currentTypeface = new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch);
            //draw units and description text
            if (descriptionRect.Width > 0)
            {
                //e.Graphics.DrawString(Description, Font, brush, descriptionRect, stringFormat);
                //System.Windows.Media.FontFamily courier = new System.Windows.Media.FontFamily("Courier New");
                
                FormattedText ft2 = new FormattedText(Description,
                                               System.Globalization.CultureInfo.CurrentCulture,
                                               System.Windows.FlowDirection.LeftToRight,
                                               currentTypeface,
                                               this.FontSize,
                                               Foreground);//Foreground
                
                Double fontHeight = ft2.Height;
                ft2.TextAlignment = stringFormat;
                if (stringFormat == TextAlignment.Center)
                {
                    drawingContext.DrawText(ft2, new System.Windows.Point(descriptionRect.X + descriptionRect.Width / 2, descriptionRect.Y + (descriptionRect.Height / 2) - (fontHeight / 2)));
                }
                else
                    drawingContext.DrawText(ft2, new System.Windows.Point(descriptionRect.X, descriptionRect.Y));
                //ft drawingContext.DrawRectangle(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Blue, 1.0), descriptionRect);
                //drawingContext.DrawRectangle(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Blue, 1.0), new Rect(10, 40, ft2.WidthIncludingTrailingWhitespace, ft2.Height));
            }
            if (unitsRect.Width > 0)
            {
                //e.Graphics.DrawString(Units, Font, brush, unitsRect, stringFormat);
                
                 FormattedText ft2 = new FormattedText(Units,
                                               System.Globalization.CultureInfo.CurrentCulture,
                                               System.Windows.FlowDirection.LeftToRight,
                                               currentTypeface,
                                               this.FontSize,
                                               Foreground);//Foreground
                 Double fontHeight = ft2.Height;
                 ft2.TextAlignment = stringFormat;
                 if (stringFormat == TextAlignment.Center)
                 {
                     drawingContext.DrawText(ft2, new System.Windows.Point(unitsRect.X + unitsRect.Width / 2, unitsRect.Y + (unitsRect.Height / 2) - (fontHeight / 2)));
                 }
                 else
                     drawingContext.DrawText(ft2, new System.Windows.Point(unitsRect.X, unitsRect.Y));
                 
                 
                //ft drawingContext.DrawRectangle(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Blue, 1.0), unitsRect);
                //drawingContext.DrawRectangle(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Blue, 1.0), new Rect(10, 40, ft2.WidthIncludingTrailingWhitespace, ft2.Height));
            }
             }
             catch (Exception ex)
             {
                 Console.WriteLine(ex.Message);
             }
        }

     


		#endregion

		#region Private Methods
         /// <summary>
         /// Updates the layout of value, description and units and causes a repaint
         /// </summary>
         protected override void UpdateLayout()
         {
             ////ft TODO

             ////Min sizes (for exception prevention)
             ////Display: 4x0

             Double height = 4.0;
             Double width = 4.0;

             if (!Double.IsNaN(this.Height))
             {
                 height = this.Height;
                 width = this.Width;
             }
             else if (this.ActualHeight != 0)
             {
                 height = this.ActualHeight;
                 width = this.ActualWidth;
             }

             //if (!Double.IsNaN(this.Width))

             //else if (this.ActualWidth != 0)


             Size newSize = new Size();
             newSize.Width = width;
             newSize.Height = height;

             UpdateLayout(newSize);

         }
        private void UpdateLayout(Size newSize)
        {

            //ft unitsRect = new Rect(0, DisplayRectangle.Y, 0, DisplayRectangle.Height);
            unitsRect = new Rect(0, 0, 0, newSize.Height);
            //ft descriptionRect = new Rect(0, DisplayRectangle.Y, 0, DisplayRectangle.Height);
            descriptionRect = new Rect(0, 0, 0, newSize.Height);

            //ft valueDisplay.Height = DisplayRectangle.Height;
            valueDisplay.Height = newSize.Height;

            //ft valueDisplay.Width = Math.Max((int)(0.01 * valuePercent * DisplayRectangle.Width), 4);
            valueDisplay.Width = Math.Max((int)(0.01 * valuePercent * newSize.Width), 4);
            //ft unitsRect.Width = (int)(0.01 * unitsPercent * DisplayRectangle.Width);
            unitsRect.Width = (int)(0.01 * unitsPercent * newSize.Width);
            //ft descriptionRect.Width = DisplayRectangle.Width - valueDisplay.Width - unitsRect.Width;
            descriptionRect.Width = (newSize.Width - valueDisplay.Width - unitsRect.Width);

            Thickness newThickness = new Thickness();
            switch (sectionLayout)
            {
                case KineticaRT.Studio.Controls.LayoutsDVU.ValueDescriptionUnits:
                    {
                        //ft label1.Margin = new Thickness(double this.Margin.Left, double top, double right, double bottom);

                        newThickness.Left = this.Margin.Left;
                        valueDisplay.Margin = newThickness;

                        descriptionRect.X = valueDisplay.Margin.Right;
                        unitsRect.X = descriptionRect.Right;
                        //valueDisplay.Left = DisplayRectangle.Left;
                        //descriptionRect.X = valueDisplay.Right;
                        //unitsRect.X = descriptionRect.Right;
                    }
                    break;

                case KineticaRT.Studio.Controls.LayoutsDVU.ValueUnitsDescription:
                    {
                        newThickness.Left = this.Margin.Left;
                        valueDisplay.Margin = newThickness;
                        unitsRect.X = valueDisplay.Width;
                        descriptionRect.X = unitsRect.Right;
                        //valueDisplay.Left = DisplayRectangle.Left;
                        //unitsRect.X = valueDisplay.Right;
                        //descriptionRect.X = unitsRect.Right;
                    }
                    break;

                case KineticaRT.Studio.Controls.LayoutsDVU.DescriptionUnitsValue:
                    {

                        descriptionRect.X = this.Margin.Left;
                        unitsRect.X = descriptionRect.Right;
                        newThickness.Left = unitsRect.Right;
                        valueDisplay.Margin = newThickness;
                        //descriptionRect.X = DisplayRectangle.Left;
                        //unitsRect.X = descriptionRect.Right;
                        //valueDisplay.Left = unitsRect.Right;
                    }
                    break;

                case KineticaRT.Studio.Controls.LayoutsDVU.UnitsDescriptionValue:
                    {

                        unitsRect.X = this.Margin.Left;
                        descriptionRect.X = unitsRect.Right;
                        newThickness.Left = descriptionRect.Right;
                        valueDisplay.Margin = newThickness;
                        //unitsRect.X = DisplayRectangle.Left;
                        //descriptionRect.X = unitsRect.Right;
                        //valueDisplay.Left = descriptionRect.Right;
                    }
                    break;

                case KineticaRT.Studio.Controls.LayoutsDVU.DescriptionValueUnits:
                    {
                        descriptionRect.X = 0;
                        //descriptionRect.Y = 0;
                        newThickness.Top = 0;
                        newThickness.Left = descriptionRect.Width;
                        valueDisplay.Margin = newThickness;
                        unitsRect.X = valueDisplay.Width + descriptionRect.Width;
                        //unitsRect.Y = 0;
                        //descriptionRect.X = DisplayRectangle.Left;
                        //valueDisplay.Left = descriptionRect.Right;
                        //unitsRect.X = valueDisplay.Right;

                    }
                    break;

                case KineticaRT.Studio.Controls.LayoutsDVU.UnitsValueDescription:
                    {
                        unitsRect.X = this.Margin.Left;
                        newThickness.Left = unitsRect.Right;
                        valueDisplay.Margin = newThickness;
                        descriptionRect.X = valueDisplay.Margin.Right;

                        //unitsRect.X = DisplayRectangle.Left;
                        //valueDisplay.Left = unitsRect.Right;
                        //descriptionRect.X = valueDisplay.Right;
                    }
                    break;
            }

            ////ft Invalidate(DisplayRectangle) ;
            InvalidateVisual();
            
           
        }

		

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////TESTTEST
        /// </summary>
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////TESTTEST
        /// </summary>
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////TESTTEST
        /// </summary>
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////TESTTEST
        /// </summary>
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////TESTTEST
        /// </summary>
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////TESTTEST
        /// </summary>
    
        //First we get the size of the control’s Canvas, which is done like this:
        //private Size getCanvasSize()
        //{
        //    if (this.Template == null)
        //        return new Size(0, 0);

        //    var canvas = (Canvas)Template.FindName("PART_ItemContainer", this);
        //    if (canvas == null)
        //        return new Size(0, 0);

        //    return new Size(canvas.ActualWidth, canvas.ActualHeight);
        //}
        //Note that we used the name for the Canvas that was set in the ControlTemplate and in the TemplatePart attribute.
        //Then for each item, we position it with this method:
        //private void positionItemHorizontally(ContentPresenter item, double canvasWidth)
        //{
        //    double itemPosition = getItemPosition(item.DataContext);
        //    var nominalPosition = itemPosition * canvasWidth;
        //    item.SetValue(Canvas.LeftProperty, nominalPosition);
        //}
        ////In order to get the item’s position value, we do the following:
        //private double getItemPosition(Object item)
        //{
        //    double itemPosition = 0;
        //    if (!string.IsNullOrEmpty(PositionPercentPath))
        //    {
        //        try
        //        {
        //            var positionProperty = item.GetType().GetProperty(PositionPercentPath);
        //            if (positionProperty != null)
        //            {
        //                try
        //                {
        //                    itemPosition = (double)positionProperty.GetValue(item, null);
        //                }
        //                catch (InvalidCastException)
        //                {
        //                }
        //            }
        //        }
        //        catch (System.Reflection.AmbiguousMatchException)
        //        {
        //        }
        //    }
        //    return itemPosition;
        //}

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////TESTTEST
        /// </summary>


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
					if (threshold_channel.Color == System.Drawing.Color.Empty)
					{
                         //ft this.valueDisplay.BackBrush = valueBackColor;
                         this.SetValueBackBrush(valueBackColor);
                    }
					else
					{
                        //ft this.valueDisplay.BackBrush = new SolidColorBrush((threshold_channel.Color).ToMediaColor());
                        //test this.valueDisplay.BackBrush = Brushes.Red;
                        //ft this.SetValueBackBrush(new SolidColorBrush(threshold_channel.Color.ToMediaColor()));
                        //ft |this.SetValueBackBrush(threshold_channel.Color);
                        this.SetValueBackBrush(threshold_channel.Color);
                        //ft this.valueDisplay.SetValueBackBrush(new SolidColorBrush(color));
                        //ft Bug.  Lorsque l on set le valueDisplay.Background, on perd le Text dans le rectangle.
                        //ft Le valueDisplay.Background doit etre Transparent pour que le valueDisplay.OnRender.DrawText fonctionne (voir http://www.codeproject.com/Articles/426169/Advanced-Text-Formatting-in-WPF)
                        //ft solution de contournement utiliser une autre prorpiete, et dessiner le background drawingcontext.DrawRectangle avant de caller le DratText
					}
				}
				else
				{
                    //ft this.valueDisplay.BackBrush = valueBackColor;
                    this.SetValueBackBrush(valueBackColor);
				}
               
				return ;
			}

			if (e.PropertyName != "Value")
			{
				//ft Invalidate(DisplayRectangle) ;
                InvalidateVisual();
			}

			base.OnSourcePropertyChanged(Sender, e) ;
        }
        
        private void SetValueBackBrush(Brush brush)
        {
            if (this.valueDisplay.Dispatcher.CheckAccess())
            {                
                this.valueDisplay.BackBrush = brush;
            }
            else
            {
                //InvokeRequired
                DispatcherOperation op = valueDisplay.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<Brush>(SetValueBackBrush), brush);
                DispatcherOperationStatus status = op.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = op.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("DisplayStudio.xaml.cs: Cannot update Threshold color");
                    }
                }
            }
        }
        //ft private delegate void SetValueBackBrushDelegate(Brush color)
        private void SetValueBackBrush(System.Drawing.Color color)
        {
            if (this.valueDisplay.Dispatcher.CheckAccess())
            {
                this.valueDisplay.BackBrush = new SolidColorBrush(color.ToMediaColor());
            }
            else
            {
                //InvokeRequired
                //ft valueDisplay.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new SetValueBackBrushDelegate(SetValueBackBrush), color);//plante
                //ftvalueDisplay.Dispatcher.BeginInvoke((System.Threading.ThreadStart)(delegate { SetValueBackBrush(color); }));//plante
                //ft Application.Current.Dispatcher.BeginInvoke((System.Threading.ThreadStart)(delegate { SetValueBackBrush(color); })); //plante
                //ft Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new SetValueBackBrushDelegate(SetValueBackBrush), color);//plante pas mais fait rien
                //fty Dispatcher.BeginInvoke(DispatcherPriority.Normal, new SetValueBackBrushDelegate(SetValueBackBrush), color); //plante
                //ft Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => { valueDisplay.BackBrush = color; }));// indique une erreur
                //ft Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => { SetValueBackBrush(color); }));
                // This will work as its using the dispatcher
                DispatcherOperation op = valueDisplay.Dispatcher.BeginInvoke(DispatcherPriority.Normal,new Action<System.Drawing.Color>(SetValueBackBrushColor),color);
                DispatcherOperationStatus status = op.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = op.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("DisplayStudio.xaml.cs: Cannot update Threshold color");
                    }
                }
            }
        }
        private void SetValueBackBrushColor(System.Drawing.Color color)
        {
            try
            {
                //On est dans le UI Thread
                Brush newColor = new SolidColorBrush(color.ToMediaColor());
                //Ici haut on a pas le choix de creer une autre var newColor pcq color a appartient au Thread d<update d<OPC, il faut en recreer une autre pour
                //affecter la Propriete BackBrush qui elle aussi appartient a un autre thread.
                this.valueDisplay.BackBrush = newColor;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
       
        private void canva_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (e.NewSize.IsEmpty) return;
                if (e.NewSize.Width == 0) return;
                UpdateLayout(e.NewSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
     
		#endregion
	}
}

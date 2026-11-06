//#define KrtStudio_license
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KineticaRT;

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// KineticaRT Studio Linear control.
	/// Displays a numeric Value from a source channel on a slider,
	/// together with an optional description and units
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(Linear.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Linear))]
	public class Linear : ScaledControl
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
							new KineticaRT.LicenceForm (type.FullName).ShowDialog () ;
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

		private KineticaRT.Controls.Slider valueSlider;
		private KineticaRT.Controls.Display valueDisplay;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int				scaleWidth ;
		private ScalePositions	scalePosition ;
		private Color			inputBackColor ;

		private Rectangle		unitsRect ;
		private Rectangle		descriptionRect ;
		private StringFormat	stringFormat ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Linear class
		/// </summary>
		public Linear()
		{
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif

			stringFormat = new StringFormat() ;
			stringFormat.LineAlignment	= StringAlignment.Center ;
			stringFormat.Alignment		= StringAlignment.Near ;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			scaleWidth				= 35 ;
			scalePosition			= ScalePositions.Left ;
			inputBackColor			= SystemColors.Window ;

			SetScalePosition() ;
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
			KineticaRT.Controls.Threshold threshold1 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold2 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold3 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold4 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.SliderNeedle sliderNeedle1 = new KineticaRT.Controls.SliderNeedle();
			this.valueSlider = new KineticaRT.Controls.Slider();
			this.valueDisplay = new KineticaRT.Controls.Display();
			this.SuspendLayout();
			// 
			// valueSlider
			// 
			this.valueSlider.BorderStyle = KineticaRT.Controls.BorderStyles.None;
			this.valueSlider.Location = new System.Drawing.Point(0, 32);
			this.valueSlider.Name = "valueSlider";
			sliderNeedle1.Centred = true;
			sliderNeedle1.Enabled = false;
			sliderNeedle1.FillStyle = KineticaRT.Controls.FillStyles.Segmented ;
			sliderNeedle1.FillFinish = 1;
			sliderNeedle1.FillStart = 0.4;
			sliderNeedle1.Finish = 1;
			sliderNeedle1.Start = 0.4;
			sliderNeedle1.Style = KineticaRT.Controls.SliderNeedleStyles.Line;
			sliderNeedle1.Width = 4;
			this.valueSlider.Needle = sliderNeedle1;
			this.valueSlider.Scale.MajorMarksRepeat = 20;
			this.valueSlider.Scale.MinorMarksDivisions = 4;
			this.valueSlider.Scale.Position = 0.4;
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
			this.valueSlider.Scale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																  threshold1,
																																  threshold2,
																																  threshold3,
																																  threshold4});
			this.valueSlider.Size = new System.Drawing.Size(88, 112);
			this.valueSlider.TabIndex = 0;
			this.valueSlider.UseSourceFormat = false;
			// 
			// valueDisplay
			// 
			this.valueDisplay.BorderStyle = KineticaRT.Controls.BorderStyles.EditBox;
			this.valueDisplay.InputEnabled = false;
			this.valueDisplay.Location = new System.Drawing.Point(0, 168);
			this.valueDisplay.Name = "valueDisplay";
			this.valueDisplay.NumericOnly = true;
			this.valueDisplay.Size = new System.Drawing.Size(88, 24);
			this.valueDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Slow ;
			this.valueDisplay.TabIndex = 1;
			this.valueDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Linear
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueDisplay,
																		  this.valueSlider});
			this.Name = "Linear";
			this.Size = new System.Drawing.Size(96, 192);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
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
				valueSlider.Needle.DestinationChannel = null ;

				valueDisplay.SourceChannel = value ;
				valueSlider.Needle.SourceChannel = value ;
				
				//connect new destination if required
				if (valueDisplay.InputEnabled)
				{
					valueDisplay.DestinationChannel = value ;
					valueSlider.Needle.DestinationChannel = value ;
				}

				base.SourceChannel = value ;
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
			set
			{
				base.Format = value ;
				valueDisplay.Format = base.Format ;
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
				return valueSlider.Needle.Color ;
			}
			set
			{
				valueSlider.Needle.Color = value ;
				OnPropertyChanged("NeedleColor") ;
			}
		}
		private bool ShouldSerializeNeedleColor()
		{
			return valueSlider.Needle.Color != Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or Sets the Color of the Fill
		/// </summary>
		[Category("Linear")]
		[Description("Gets or Sets the Color of the Fill")]
		public Color FillColor
		{
			get
			{
				return fillColor ;
			}
			set
			{
				if (thresholdChannel == null) valueSlider.Needle.FillColor = value ;
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
				return valueSlider.Scale.Color ;
			}
			set
			{
				valueSlider.Scale.Color = value ;
				OnPropertyChanged("ScaleColor") ;
			}
		}
		private bool ShouldSerializeScaleColor()
		{
			return valueSlider.Scale.Color != Color.Black ;
		}

		/// <summary>
		/// Gets or sets the orientation of the linear
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets the orientation of the linear")]
		[DefaultValue(Orientation.Vertical)]
		public Orientation Orientation
		{
			get
			{
				return valueSlider.Orientation ;
			}
			set
			{
				valueSlider.Orientation = value ;
				OnPropertyChanged("Orientation") ;
				UpdateLayout() ;
			}
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
				return valueSlider.Needle.FillEnabled ;
			}
			set
			{
				valueSlider.Needle.FillEnabled = value ;
				OnPropertyChanged("FillEnabled") ;
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
				return valueSlider.Needle.Visible ;
			}
			set
			{
				valueSlider.Needle.Visible = value ;
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
				return valueSlider.Needle.Style ;
			}
			set
			{
				valueSlider.Needle.Style = value ;
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
				return valueSlider.Needle.Width ;
			}
			set
			{
				valueSlider.Needle.Width = value ;
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
							valueSlider.Scale.LabelPosition = KineticaRT.Controls.ScaleMarkPositions.Inside ;
							valueSlider.Scale.MarkPosition	= KineticaRT.Controls.ScaleMarkPositions.Inside ;
							valueSlider.Scale.Enabled = true ;
						}
						break ;

						case ScalePositions.Right:
						{
							valueSlider.Scale.LabelPosition = KineticaRT.Controls.ScaleMarkPositions.Outside ;
							valueSlider.Scale.MarkPosition	= KineticaRT.Controls.ScaleMarkPositions.Outside ;
							valueSlider.Scale.Enabled = true ;
						}
						break ;

						case ScalePositions.Hidden:
						{
							valueSlider.Scale.Enabled = false ;
						}
						break ;
					}

					SetScalePosition() ;
				}
			}
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
					valueSlider.Needle.Enabled = value ;

					if (value)
					{
						valueDisplay.BackColor = inputBackColor ;
						valueDisplay.DestinationChannel = SourceChannel ;
						valueSlider.Needle.DestinationChannel = SourceChannel ;
					}
					else
					{
						valueDisplay.BackColor = BackColor ;
						valueDisplay.DestinationChannel = null ;
						valueSlider.Needle.DestinationChannel = null ;
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
		#endregion

		#region Drawing
		/// <summary>
		/// Paint Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e) ;

			Brush		brush = new SolidBrush(ForeColor) ;

			//draw units and description text
			if (descriptionRect.Width > 0)
			{
				e.Graphics.DrawString(Description, Font, brush, descriptionRect, stringFormat) ;
			}
			if (unitsRect.Width > 0)
			{
				e.Graphics.DrawString(Units, Font, brush, unitsRect, stringFormat) ;
			}
			brush.Dispose() ;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the sizes of all sections
		/// </summary>
		protected override void UpdateLayout()
		{
			//Min sizes (for exception prevention)
			//Display 4x0
			//Slider 4x2

			unitsRect = Rectangle.Empty ;
			descriptionRect = Rectangle.Empty ;

			unitsRect.X			= DisplayRectangle.Left ;
			descriptionRect.X	= unitsRect.X ;
			valueDisplay.Left	= unitsRect.X ;

			if (valueSlider.Orientation == Orientation.Vertical)
			{
				stringFormat.Alignment = StringAlignment.Center ;

				//desc, units and value are 1/12th total height
				//slider is 2/3 total height
				valueSlider.Left	= unitsRect.X ;

				unitsRect.Width			= DisplayRectangle.Width ;
				descriptionRect.Width	= unitsRect.Width ;
				valueSlider.Width		= Math.Max(unitsRect.Width, 4) ;
				valueDisplay.Width		= valueSlider.Width ;

				unitsRect.Height		= (int)(DisplayRectangle.Height / 12.0) ;
				descriptionRect.Height	= unitsRect.Height ;
				valueDisplay.Height		= unitsRect.Height ;
				valueSlider.Height		= Math.Max(DisplayRectangle.Height - (4 * unitsRect.Height), 2) ;

				descriptionRect.Y		= DisplayRectangle.Y ;
				unitsRect.Y				= descriptionRect.Bottom ;
				valueSlider.Top			= unitsRect.Bottom ;
				valueDisplay.Top		= valueSlider.Bottom + unitsRect.Height ;
			}
			else		//horizontal
			{
				stringFormat.Alignment = StringAlignment.Near ;

				//desc, units and value are 1/4 total height and 1/4 total width
				//slider is 3/4 total width
				unitsRect.Width			= DisplayRectangle.Width / 4 ;
				descriptionRect.Width	= unitsRect.Width ;
				valueDisplay.Width		= Math.Max(unitsRect.Width, 4) ;
				valueSlider.Width		= Math.Max(DisplayRectangle.Width - unitsRect.Width, 4) ;

				valueSlider.Left		= unitsRect.Right ;

				unitsRect.Height		= (int)(DisplayRectangle.Height / 4.0) ;
				descriptionRect.Height	= unitsRect.Height ;
				valueDisplay.Height		= unitsRect.Height ;
				valueSlider.Height		= Math.Max(DisplayRectangle.Height, 2) ;

				descriptionRect.Y		= DisplayRectangle.Y ;
				unitsRect.Y				= descriptionRect.Bottom ;
				valueDisplay.Top		= unitsRect.Bottom + unitsRect.Height ;
				valueSlider.Top			= DisplayRectangle.Y ;
			}

			Invalidate(DisplayRectangle) ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueDisplay.ToolTipText = NewText ;
			valueSlider.ToolTipText = NewText ;
			base.NewToolTip(NewText) ;
		}

		/// <summary>
		/// Set the range of scaled objects
		/// </summary>
		/// <param name="Min">New minimum value</param>
		/// <param name="Max">New Maximum value</param>
		protected override void NewRange(double Min, double Max)
		{
			base.NewRange(Min, Max) ;
			valueSlider.Scale.Minimum = Min ;
			valueSlider.Scale.Maximum = Max ;

			//need to set this as auto-adjust may have altered it
			NewDPs(DecimalPlaces) ;
		}

		/// <summary>
		/// Set the decimal places of scaled objects
		/// </summary>
		/// <param name="DPs">New decimal places</param>
		protected override void NewDPs(int DPs)
		{
			base.NewDPs(DPs) ;
			valueSlider.Scale.DecimalPlaces = DPs ;
		}

		/// <summary>
		/// Correctly (re)position the scale
		/// </summary>
		private void SetScalePosition()
		{
			//reposition the scale, and size the needle and fill to match
			double			percent ;
		    
			if (valueSlider.Orientation == Orientation.Vertical)
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
					valueSlider.Scale.Position = percent ;
					valueSlider.Needle.Start = percent ;
					valueSlider.Needle.Finish = 1 ;
					valueSlider.Needle.FillStart = percent ;
					valueSlider.Needle.FillFinish = 1 ;
				}
				break ;

				case ScalePositions.Right:
				{
					valueSlider.Scale.Position = 1 - percent ;
					valueSlider.Needle.Start = 0 ;
					valueSlider.Needle.Finish = 1 - percent ;
					valueSlider.Needle.FillStart = 0 ;
					valueSlider.Needle.FillFinish = 1 - percent ;
				}
				break ;

				case ScalePositions.Hidden:
				{
					valueSlider.Needle.Start = 0 ;
					valueSlider.Needle.Finish = 1;
					valueSlider.Needle.FillStart = 0 ;
					valueSlider.Needle.FillFinish = 1 ;
				}
				break ;
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
			SetScalePosition() ;
			base.OnResize(e) ;
		}
		#endregion

		#region Events
		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnSourcePropertyChanged(object Sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
				Invalidate(DisplayRectangle) ;
			}
			base.OnSourcePropertyChanged(Sender, e) ;
		}
		#endregion

		/// <summary>
		/// Gets the scale for the Scaled Control.
		/// </summary>
		protected override KineticaRT.Controls.Scale  ValueScale
		{
			get {return valueSlider.Scale;}
		}

		/// <summary>
		/// Sets the fill color for this object.
		/// Used for setting the LowLow threshold color when connected to threshold channels.
		/// </summary>
		/// <param name="color">The new color.</param>
		/// <param name="centrePoint">The new center point about which the fill is drawn.</param>
		protected override void SetFill (double centrePoint, System.Drawing.Color color)
		{
			valueSlider.Needle.CentrePoint = centrePoint ;
			if (color == System.Drawing.Color.Empty)
			{
				valueSlider.Needle.FillColor = fillColor ;
			}
			else
			{
				valueSlider.Needle.FillColor = color ;
			}
		}
	}
}

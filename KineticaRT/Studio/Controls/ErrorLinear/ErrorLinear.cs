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
	/// KineticaRT Studio Error Linear control.
	/// Displays the difference between numeric Values from two source channels on a slider,
	/// together with an optional description and units
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(ErrorLinear.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(ErrorLinear))]
	public class ErrorLinear : SetPointControl
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
						if ((key == null) || (key != "56BD5445F969"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "56BD5445F969")) key = "{56BD5445-F969-49fd-93A4-BA486E7E8EAC}" ; 
						context.SetSavedLicenseKey (typeof(ErrorLinear), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(ErrorLinear), null) ;
					if (key == "{56BD5445-F969-49fd-93A4-BA486E7E8EAC}") licence = new License ("Run") ;
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

		private KineticaRT.Controls.Slider		errorSlider;
		private KineticaRT.Controls.Display		errorDisplay;
		private Logic.Expressions.Expression	expression;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int				scaleWidth ;
		private ScalePositions	scalePosition ;

		private Rectangle		unitsRect ;
		private Rectangle		descriptionRect ;
		private StringFormat	stringFormat ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the ErrorLinear class
		/// </summary>
		public ErrorLinear()
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

			expression = new KineticaRT.Logic.Expressions.Expression() ;
			expression.Text = "Source - SetPointSource" ;
			expression.ObjectSearch += new ObjectSearchEventHandler(OnExpressionObjectSearch) ;
			errorSlider.Needle.SourceChannel = expression ;
			errorDisplay.SourceChannel = expression ;

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
			this.errorSlider = new KineticaRT.Controls.Slider();
			this.errorDisplay = new KineticaRT.Controls.Display();
			this.SuspendLayout();
			// 
			// errorSlider
			// 
            this.errorSlider.BorderType = KineticaRT.Controls.BorderType.None;
			this.errorSlider.Location = new System.Drawing.Point(0, 24);
			this.errorSlider.Name = "errorSlider";
			sliderNeedle1.Centred = true;
			sliderNeedle1.Enabled = false;
			sliderNeedle1.Style = KineticaRT.Controls.SliderNeedleStyles.Line;
			sliderNeedle1.Width = 4;
//			sliderNeedle1.FillColor = System.Drawing.Color.MediumBlue;
//			sliderNeedle1.FillEnabled = true;
//			sliderNeedle1.FillStyle = KineticaRT.Controls.FillStyles.Segmented ;
//			sliderNeedle1.FillStart = 0.9 ;
//			sliderNeedle1.FillFinish = 0.8 ;
			this.errorSlider.Needle = sliderNeedle1;
			this.errorSlider.Scale.MajorMarksRepeat = 20;
			this.errorSlider.Scale.MinorMarksDivisions = 4;
			this.errorSlider.Size = new System.Drawing.Size(96, 136);
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
			this.errorSlider.Scale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																	threshold1,
																																	threshold2,
																																	threshold3,
																																	threshold4});
			this.errorSlider.TabIndex = 3;
			this.errorSlider.UseSourceFormat = false ;
			// 
			// errorDisplay
			// 
            this.errorDisplay.BorderType = KineticaRT.Controls.BorderType.EditBox;
			this.errorDisplay.InputEnabled = false;
			this.errorDisplay.Location = new System.Drawing.Point(0, 176);
			this.errorDisplay.Name = "errorDisplay";
			this.errorDisplay.Orientation = KineticaRT.Controls.TextOrientations.Right;
			this.errorDisplay.Size = new System.Drawing.Size(96, 16);
			this.errorDisplay.TabIndex = 2;
			this.errorDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Slow ;
			this.errorDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.errorDisplay.UseSourceFormat = false ;
			// 
			// ErrorLinear
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.errorDisplay,
																		  this.errorSlider});
			this.Name = "ErrorLinear";
			this.Size = new System.Drawing.Size(96, 192);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
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
                return base.DescriptionSource ;
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
                return base.CustomUnits ;
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
                return base.UseCustomUnits ;
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
                return base.Format ;
            }
            set
			{
				base.Format = value ;
				
				//if possible and the format hasn't been overridden, 
				//set the display format to match the source format
				if (value == "")
				{
					if (SourceChannel is Channels.INumericChannel)
					{
						errorDisplay.Format = ((Channels.INumericChannel)SourceChannel).Format ;
					}
					else
					{
						errorDisplay.Format = "" ;
					}
				}
				else
				{
					errorDisplay.Format = base.Format ;
				}
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
				return errorSlider.Needle.Color ;
			}
			set
			{
				errorSlider.Needle.Color = value ;
				OnPropertyChanged("NeedleColor") ;
			}
		}
		private bool ShouldSerializeNeedleColor()
		{
			return errorSlider.Needle.Color != Color.SteelBlue ;
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
				return errorSlider.Needle.FillColor ;
			}
			set
			{
				errorSlider.Needle.FillColor = value ;
				OnPropertyChanged("FillColor") ;
			}
		}
		private bool ShouldSerializeFillColor()
		{
			return errorSlider.Needle.FillColor != Color.LightSteelBlue ;
		}

		/// <summary>
		/// Gets or Sets the Color of the Scale
		/// </summary>
		[Category("Linear")]
		[Description("Gets or Sets the Color of the Scale")]
		public Color ScaleColor
		{
			get
			{
				return errorSlider.Scale.Color ;
			}
			set
			{
				errorSlider.Scale.Color = value ;
				OnPropertyChanged("ScaleColor") ;
			}
		}
		private bool ShouldSerializeScaleColor()
		{
			return errorSlider.Scale.Color != Color.Black ;
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
				return errorSlider.Orientation ;
			}
			set
			{
				errorSlider.Orientation = value ;
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
				return errorSlider.Needle.FillEnabled ;
			}
			set
			{
				errorSlider.Needle.FillEnabled = value ;
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
				return errorSlider.Needle.Visible ;
			}
			set
			{
				errorSlider.Needle.Visible = value ;
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
				return errorSlider.Needle.Style ;
			}
			set
			{
				errorSlider.Needle.Style = value ;
				OnPropertyChanged("NeedleStyle") ;
			}
		}

		/// <summary>
		/// Gets or sets the width of the needle in pixels
		/// </summary>
		[Category("Linear")]
		[Description("Gets or sets the width of the needle in pixels")]
		[DefaultValue(4)]
		public int NeedleWidth
		{
			get
			{
				return errorSlider.Needle.Width ;
			}
			set
			{
				errorSlider.Needle.Width = value ;
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
							errorSlider.Scale.LabelPosition = KineticaRT.Controls.ScaleMarkPositions.Inside ;
							errorSlider.Scale.MarkPosition	= KineticaRT.Controls.ScaleMarkPositions.Inside ;
							errorSlider.Scale.Enabled = true ;
						}
						break ;

						case ScalePositions.Right:
						{
							errorSlider.Scale.LabelPosition = KineticaRT.Controls.ScaleMarkPositions.Outside ;
							errorSlider.Scale.MarkPosition	= KineticaRT.Controls.ScaleMarkPositions.Outside ;
							errorSlider.Scale.Enabled = true ;
						}
						break ;

						case ScalePositions.Hidden:
						{
							errorSlider.Scale.Enabled = false ;
						}
						break ;
					}

					SetScalePosition() ;
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
				base.SourceChannel = value ;

				//if possible and the format hasn't been overridden, 
				//set the display format to match the source format
				if (Format == "")
				{
					if (value is Channels.INumericChannel)
					{
						errorDisplay.Format = ((Channels.INumericChannel)value).Format ;
					}
					else
					{
						errorDisplay.Format = "" ;
					}
				}
				expression.Connect() ;
			}
		}

		/// <summary>
		/// Gets or sets the SetPoint channel to which this object is connected
		/// </summary>
		[Category("SetPoint")]
		[Description("Gets or sets the SetPoint channel to which this object is connected")]
		[DefaultValue(null)]
		public override Channels.IInfoChannel SetPointSourceChannel
		{
			set
			{
				base.SetPointSourceChannel = value ;
				expression.Connect() ;
			}
		}

		/// <summary>
		/// Gets the current Value
		/// </summary>
		[Category("General")]
		[Description("Gets the current Value")]
		public override Values.Value Value
		{
			get
			{
				return expression.Value ;
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
			//Displays 4x0
			//Slider 4x2

			unitsRect = Rectangle.Empty ;
			descriptionRect = Rectangle.Empty ;

			unitsRect.X			= DisplayRectangle.Left ;
			descriptionRect.X	= unitsRect.X ;
			errorDisplay.Left	= unitsRect.X ;

			if (errorSlider.Orientation == Orientation.Vertical)
			{
				stringFormat.Alignment = StringAlignment.Center ;

				//desc, units and value are 1/12th total height
				//slider is 2/3 total height
				errorSlider.Left	= unitsRect.X ;

				unitsRect.Width			= DisplayRectangle.Width ;
				descriptionRect.Width	= unitsRect.Width ;
				errorSlider.Width		= Math.Max(unitsRect.Width, 4) ;
				errorDisplay.Width		= errorSlider.Width ;

				unitsRect.Height		= (int)(DisplayRectangle.Height / 12.0) ;
				descriptionRect.Height	= unitsRect.Height ;
				errorDisplay.Height		= unitsRect.Height ;
				errorSlider.Height		= Math.Max(DisplayRectangle.Height - (4 * unitsRect.Height), 2) ;

				descriptionRect.Y		= DisplayRectangle.Y ;
				unitsRect.Y				= descriptionRect.Bottom ;
				errorSlider.Top			= unitsRect.Bottom ;
				errorDisplay.Top		= DisplayRectangle.Bottom - errorDisplay.Height ;
			}
			else		//horizontal
			{
				stringFormat.Alignment = StringAlignment.Near ;

				//desc, units and value are 1/4 total height and 1/4 total width
				//slider is 3/4 total width
				unitsRect.Width			= DisplayRectangle.Width / 4 ;
				descriptionRect.Width	= unitsRect.Width ;
				errorDisplay.Width		= Math.Max(unitsRect.Width, 4) ;
				errorSlider.Width		= Math.Max(DisplayRectangle.Width - unitsRect.Width, 4) ;

				errorSlider.Left		= unitsRect.Right ;

				unitsRect.Height		= (int)(DisplayRectangle.Height / 4.0) ;
				descriptionRect.Height	= unitsRect.Height ;
				errorDisplay.Height		= unitsRect.Height ;
				errorSlider.Height		= Math.Max(DisplayRectangle.Height, 2) ;

				descriptionRect.Y		= DisplayRectangle.Y ;
				unitsRect.Y				= descriptionRect.Bottom ;
				errorDisplay.Top		= DisplayRectangle.Bottom - errorDisplay.Height ;
				errorSlider.Top			= DisplayRectangle.Y ;
			}

			Invalidate(DisplayRectangle) ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			base.NewToolTip(NewText) ;
			errorDisplay.ToolTipText = NewText ;
			errorSlider.ToolTipText = NewText ;
		}

		/// <summary>
		/// Set the range of scaled objects
		/// </summary>
		/// <param name="Min">New minimum value</param>
		/// <param name="Max">New Maximum value</param>
		protected override void NewRange(double Min, double Max)
		{
			base.NewRange(Min, Max) ;
			errorSlider.Scale.Minimum = Min ;
			errorSlider.Scale.Maximum = Max ;

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
			errorSlider.Scale.DecimalPlaces = DPs ;
		}

		/// <summary>
		/// Correctly (re)position the scale
		/// </summary>
		private void SetScalePosition()
		{
			//reposition the scale, and size the needle and fill to match
			double			percent ;
		
			if (errorSlider.Orientation == Orientation.Vertical)
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
					errorSlider.Scale.Position = percent ;
					errorSlider.Needle.Start = percent ;
					errorSlider.Needle.Finish = 1 ;
					errorSlider.Needle.FillStart = percent ;
					errorSlider.Needle.FillFinish = 1 ;
				}
				break ;

				case ScalePositions.Right:
				{
					errorSlider.Scale.Position = 1 - percent ;
					errorSlider.Needle.Start = 0 ;
					errorSlider.Needle.Finish = 1 - percent ;
					errorSlider.Needle.FillStart = 0 ;
					errorSlider.Needle.FillFinish = 1 - percent ;
				}
				break ;

				case ScalePositions.Hidden:
				{
					errorSlider.Needle.Start = 0 ;
					errorSlider.Needle.Finish = 1;
					errorSlider.Needle.FillStart = 0 ;
					errorSlider.Needle.FillFinish = 1 ;
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
		private object OnExpressionObjectSearch(string Name)
		{
			if (Name == "Source")
			{
				return SourceChannel ;
			}
			else if (Name == "SetPointSource")
			{
				return SetPointSourceChannel ;
			}
			
			//else unknown name
			return null ;
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
			get {return errorSlider.Scale;}
		}

//		/// <summary>
//		/// Sets the fill color for this object.
//		/// Used for setting the LowLow threshold color when connected to threshold channels.
//		/// </summary>
//		/// <param name="color">The new color.</param>
//		protected override void SetFillColor (System.Drawing.Color color)
//		{
//			this.errorSlider.Needle.FillColor = color ;
//		}

	}
}

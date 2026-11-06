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
	/// KineticaRT Studio SetPoint Linear control.
	/// Displays numeric Values from two source channels on a slider,
	/// together with an optional description and units
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(SetPointLinear.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(SetPointLinear))]
	public class SetPointLinear : SetPointControl
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
						if ((key == null) || (key != "B4C5569F7558"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "B4C5569F7558")) key = "{B4C5569F-7558-40d3-8AE1-7F4C0A3AC64B}" ; 
						context.SetSavedLicenseKey (type, key) ;
						
					}

					if  (key == null) key = context.GetSavedLicenseKey (type, null) ;
					if (key == "{B4C5569F-7558-40d3-8AE1-7F4C0A3AC64B}") licence = new License ("Run") ;
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

		private KineticaRT.Controls.SliderMultiNeedle valueSlider;
		private KineticaRT.Controls.Display setPointDisplay;
		private KineticaRT.Controls.Display valueDisplay;

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
		/// Initialises a new instance of the SetPointLinear class
		/// </summary>
		public SetPointLinear()
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
			KineticaRT.Controls.SliderNeedle sliderNeedle2 = new KineticaRT.Controls.SliderNeedle();
			this.valueSlider = new KineticaRT.Controls.SliderMultiNeedle();
			this.setPointDisplay = new KineticaRT.Controls.Display();
			this.valueDisplay = new KineticaRT.Controls.Display();
			this.SuspendLayout();
			// 
			// valueSlider
			// 
            this.valueSlider.BorderType = KineticaRT.Controls.BorderType.None;
			this.valueSlider.Location = new System.Drawing.Point(0, 32);
			this.valueSlider.Name = "valueSlider";
			sliderNeedle1.Color = System.Drawing.Color.LightSteelBlue;
			sliderNeedle1.Enabled = false ;
			sliderNeedle1.Style = KineticaRT.Controls.SliderNeedleStyles.ReverseArrow;
			sliderNeedle2.Centred = true;
			sliderNeedle2.Enabled = false ;
			sliderNeedle2.Style = KineticaRT.Controls.SliderNeedleStyles.Line;
			sliderNeedle2.Width = 4;
			sliderNeedle2.FillEnabled = false;
			sliderNeedle2.FillStyle = KineticaRT.Controls.FillStyles.Segmented ;
			sliderNeedle2.FillStart = 0.9 ;
			sliderNeedle2.FillFinish = 0.8 ;
			this.valueSlider.Needles = new KineticaRT.Controls.SliderNeedleCollection(new KineticaRT.Controls.SliderNeedle[] {
																																 sliderNeedle1,
																																 sliderNeedle2});
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
			this.valueSlider.Size = new System.Drawing.Size(96, 128);
			this.valueSlider.TabIndex = 0;
			this.valueSlider.UseSourceFormat = false;
			// 
			// setPointDisplay
			// 
            this.setPointDisplay.BorderType = KineticaRT.Controls.BorderType.EditBox;
			this.setPointDisplay.Location = new System.Drawing.Point(0, 160);
			this.setPointDisplay.Name = "setPointDisplay";
			this.setPointDisplay.Size = new System.Drawing.Size(96, 16);
			this.setPointDisplay.TabIndex = 1;
			this.setPointDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Slow ;
			this.setPointDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// valueDisplay
			// 
            this.valueDisplay.BorderType = KineticaRT.Controls.BorderType.EditBox;
			this.valueDisplay.InputEnabled = false;
			this.valueDisplay.Location = new System.Drawing.Point(0, 176);
			this.valueDisplay.Name = "valueDisplay";
			this.valueDisplay.Size = new System.Drawing.Size(96, 16);
			this.valueDisplay.TabIndex = 2;
			this.valueDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Slow ;
			this.valueDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SetPointLinear
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueDisplay,
																		  this.setPointDisplay,
																		  this.valueSlider});
			this.Name = "SetPointLinear";
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
				valueDisplay.SourceChannel = value ;
				valueSlider.Needles[1].SourceChannel = value ;

				if (value != null)
				{
					string setpoint_source = value.Name+"Setpoint" ;
					Channels.IInfoChannel setpoint_source_channel = OnObjectSearch(setpoint_source) ;
					SetPointSourceChannel = setpoint_source_channel ;
					valueSlider.Needles[0].SourceChannel = setpoint_source_channel ;
				}
				else
				{
					valueSlider.Needles[0].SourceChannel = null ;
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
            get
            {
                return base.CustomDescription ;
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
				valueDisplay.Format = base.Format ;
				setPointDisplay.Format = base.Format ;
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
				return valueSlider.Needles[1].Color ;
			}
			set
			{
				valueSlider.Needles[1].Color = value ;
				OnPropertyChanged("NeedleColor") ;
			}
		}
		private bool ShouldSerializeNeedleColor()
		{
			return valueSlider.Needles[1].Color != Color.SteelBlue ;
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
				if (thresholdChannel == null) valueSlider.Needles[1].FillColor = value ;
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
				return valueSlider.Needles[1].FillEnabled ;
			}
			set
			{
				valueSlider.Needles[1].FillEnabled = value ;
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
				return valueSlider.Needles[1].Visible ;
			}
			set
			{
				valueSlider.Needles[1].Visible = value ;
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
				return valueSlider.Needles[1].Style ;
			}
			set
			{
				valueSlider.Needles[1].Style = value ;
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
				return valueSlider.Needles[1].Width ;
			}
			set
			{
				valueSlider.Needles[1].Width = value ;
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
							valueSlider.Needles[0].Style = KineticaRT.Controls.SliderNeedleStyles.ReverseArrow ;
							valueSlider.Needles[0].Visible = true ;
						}
						break ;

						case ScalePositions.Right:
						{
							valueSlider.Scale.LabelPosition = KineticaRT.Controls.ScaleMarkPositions.Outside ;
							valueSlider.Scale.MarkPosition	= KineticaRT.Controls.ScaleMarkPositions.Outside ;
							valueSlider.Scale.Enabled = true ;
							valueSlider.Needles[0].Style = KineticaRT.Controls.SliderNeedleStyles.Arrow ;
							valueSlider.Needles[0].Visible = true ;
						}
						break ;

						case ScalePositions.Hidden:
						{
							valueSlider.Scale.Enabled = false ;
							valueSlider.Needles[0].Visible = false ;
						}
						break ;
					}

					SetScalePosition() ;
				}
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
				setPointDisplay.SourceChannel = value ;
				valueSlider.Needles[0].SourceChannel = value ;

				base.SetPointSourceChannel = value ;
			}
		}

		/// <summary>
		/// Gets or Sets the Color of the SetPoint mark
		/// </summary>
		[Category("SetPoint")]
		[Description("Gets or Sets the Color of the SetPoint mark")]
		public Color SetPointColor
		{
			get
			{
				return valueSlider.Needles[0].Color ;
			}
			set
			{
				valueSlider.Needles[0].Color = value ;
				setPointDisplay.ForeColor = value ;
				OnPropertyChanged("SetPointColor") ;
			}
		}
		private bool ShouldSerializeSetPointColor()
		{
			return valueSlider.Needles[0].Color != Color.LightSteelBlue ;
		}

		/// <summary>
		/// Gets or sets whether the SetPoint mark is enabled
		/// </summary>
		[Category("SetPoint")]
		[Description("Gets or sets whether the SetPoint mark is enabled")]
		[DefaultValue(true)]
		public bool SetPointMarkEnabled
		{
			get
			{
				return valueSlider.Needles[0].Visible ;
			}
			set
			{
				valueSlider.Needles[0].Visible = value ;
				OnPropertyChanged("SetPointMarkEnabled") ;
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
			valueDisplay.Left	= unitsRect.X ;
			setPointDisplay.Left= unitsRect.X ;

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
				setPointDisplay.Width	= valueSlider.Width ;

				unitsRect.Height		= (int)(DisplayRectangle.Height / 12.0) ;
				descriptionRect.Height	= unitsRect.Height ;
				valueDisplay.Height		= unitsRect.Height ;
				setPointDisplay.Height	= unitsRect.Height ;
				valueSlider.Height		= Math.Max(DisplayRectangle.Height - (4 * unitsRect.Height), 2) ;

				descriptionRect.Y		= DisplayRectangle.Y ;
				unitsRect.Y				= descriptionRect.Bottom ;
				valueSlider.Top			= unitsRect.Bottom ;
				setPointDisplay.Top		= valueSlider.Bottom ;
				valueDisplay.Top		= setPointDisplay.Bottom ;
			}
			else		//horizontal
			{
				stringFormat.Alignment = StringAlignment.Near ;

				//desc, units and value are 1/4 total height and 1/4 total width
				//slider is 3/4 total width
				unitsRect.Width			= DisplayRectangle.Width / 4 ;
				descriptionRect.Width	= unitsRect.Width ;
				valueDisplay.Width		= Math.Max(unitsRect.Width, 4) ;
				setPointDisplay.Width	= valueDisplay.Width ;
				valueSlider.Width		= Math.Max(DisplayRectangle.Width - unitsRect.Width, 4) ;

				valueSlider.Left		= unitsRect.Right ;

				unitsRect.Height		= (int)(DisplayRectangle.Height / 4.0) ;
				descriptionRect.Height	= unitsRect.Height ;
				valueDisplay.Height		= unitsRect.Height ;
				setPointDisplay.Height	= unitsRect.Height ;
				valueSlider.Height		= Math.Max(DisplayRectangle.Height, 2) ;

				descriptionRect.Y		= DisplayRectangle.Y ;
				unitsRect.Y				= descriptionRect.Bottom ;
				setPointDisplay.Top		= unitsRect.Bottom ;
				valueDisplay.Top		= setPointDisplay.Bottom ;
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
					valueSlider.Needles[1].Start = percent ;
					valueSlider.Needles[1].Finish = 1 ;
					valueSlider.Needles[1].FillStart = percent + 0.1 ;
					valueSlider.Needles[1].FillFinish = 1 ;
					valueSlider.Needles[0].Start = percent ;
					valueSlider.Needles[0].Finish = percent + 0.1 ;
					valueSlider.Needles[0].FillStart = percent ;
					valueSlider.Needles[0].FillFinish = percent + 0.1 ;
				}
				break ;

				case ScalePositions.Right:
				{
					valueSlider.Scale.Position = 1 - percent ;
					valueSlider.Needles[1].Start = 0 ;
					valueSlider.Needles[1].Finish = 1 - percent ;
					valueSlider.Needles[1].FillStart = 0 ;
					valueSlider.Needles[1].FillFinish = 1 - percent - 0.1 ;
					valueSlider.Needles[0].Start = 1 - percent - 0.1 ;
					valueSlider.Needles[0].Finish = 1 - percent ;
					valueSlider.Needles[0].FillStart = 1 - percent - 0.1 ;
					valueSlider.Needles[0].FillFinish = 1 - percent ;
				}
				break ;

				case ScalePositions.Hidden:
				{
					valueSlider.Needles[1].Start = 0 ;
					valueSlider.Needles[1].Finish = 1;
					valueSlider.Needles[1].FillStart = 0 ;
					valueSlider.Needles[1].FillFinish = 1 ;
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
			valueSlider.Needles[1].CentrePoint = centrePoint ;
			if (color == System.Drawing.Color.Empty)
			{
				valueSlider.Needles[1].FillColor = fillColor ;
			}
			else
			{
				valueSlider.Needles[1].FillColor = color ;
			}
		}

	}
}

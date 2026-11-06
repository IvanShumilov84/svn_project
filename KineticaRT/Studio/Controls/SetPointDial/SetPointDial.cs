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
	/// KineticaRT Studio SetPoint Dial control.
	/// Displays numeric Values from two source channels on a dial,
	/// together with an optional description and units
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(SetPointDial.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(SetPointDial))]
	public class SetPointDial : SetPointControl
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
						if ((key == null) || (key != "B280B7A8AEAA"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "B280B7A8AEAA")) key = "{B280B7A8-AEAA-4199-8232-FAD27C6C0E2E}" ; 
						context.SetSavedLicenseKey (typeof(SetPointDial), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(SetPointDial), null) ;
					if (key == "{B280B7A8-AEAA-4199-8232-FAD27C6C0E2E}") licence = new License ("Run") ;
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

		private KineticaRT.Controls.DialMultiNeedle valueDial;
		private KineticaRT.Controls.Display setPointDisplay;
		private KineticaRT.Controls.Display valueDisplay;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the SetPointDial class
		/// </summary>
		public SetPointDial()
		{
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
			KineticaRT.Controls.DialNeedle dialNeedle1 = new KineticaRT.Controls.DialNeedle();
			KineticaRT.Controls.DialNeedle dialNeedle2 = new KineticaRT.Controls.DialNeedle();
			KineticaRT.Controls.Threshold threshold1 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold2 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold3 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold4 = new KineticaRT.Controls.Threshold();
			this.valueDial = new KineticaRT.Controls.DialMultiNeedle();
			this.setPointDisplay = new KineticaRT.Controls.Display();
			this.valueDisplay = new KineticaRT.Controls.Display();
			this.SuspendLayout();
			// 
			// valueDial
			// 
            this.valueDial.BorderType = KineticaRT.Controls.BorderType.None;
			this.valueDial.Enabled = true;
			this.valueDial.Name = "valueDial";
			dialNeedle1.Color = System.Drawing.Color.LightSteelBlue;
			dialNeedle1.Enabled = false;
			dialNeedle1.Length = 1;
			dialNeedle1.Style = KineticaRT.Controls.DialNeedleStyles.ThickLine;
			dialNeedle1.Width = 0;
			dialNeedle2.Enabled = false;
			dialNeedle2.FillColor = System.Drawing.Color.MediumBlue;
			dialNeedle2.FillEnabled = false;
			dialNeedle2.FillStyle = KineticaRT.Controls.FillStyles.Segmented ;
			dialNeedle2.FillStart = 0.9 ;
			dialNeedle2.FillFinish = 0.8 ;
			dialNeedle2.Length = 0.9;
			this.valueDial.Needles = new KineticaRT.Controls.DialNeedleCollection(new KineticaRT.Controls.DialNeedle[] {
																														   dialNeedle1,
																														   dialNeedle2});
			this.valueDial.Scale.MajorMarksRepeat = 20;
			this.valueDial.Scale.MinorMarksDivisions = 4;
			this.valueDial.Scale.Position = 0.9;
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
			this.valueDial.Scale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																  threshold1,
																																  threshold2,
																																  threshold3,
																																  threshold4});
			this.valueDial.TabIndex = 0;
			this.valueDial.UseSourceFormat = false;
			// 
			// setPointDisplay
			// 
            this.setPointDisplay.BorderType = KineticaRT.Controls.BorderType.EditBox;
			this.setPointDisplay.InputEnabled = false;
			this.setPointDisplay.Location = new System.Drawing.Point(48, 112);
			this.setPointDisplay.Name = "setPointDisplay";
			this.setPointDisplay.Size = new System.Drawing.Size(40, 16);
			this.setPointDisplay.TabIndex = 1;
			this.setPointDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Slow ;
			this.setPointDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// valueDisplay
			// 
            this.valueDisplay.BorderType = KineticaRT.Controls.BorderType.EditBox;
			this.valueDisplay.InputEnabled = false;
			this.valueDisplay.Location = new System.Drawing.Point(48, 128);
			this.valueDisplay.Name = "valueDisplay";
			this.valueDisplay.Size = new System.Drawing.Size(40, 16);
			this.valueDisplay.TabIndex = 2;
			this.valueDisplay.UpdateSpeed = KineticaRT.Controls.UpdateSpeeds.Slow ;
			this.valueDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SetPointDial
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueDisplay,
																		  this.setPointDisplay,
																		  this.valueDial});
			this.Name = "SetPointDial";
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
				valueDial.Needles[1].SourceChannel = value ;

				if (value != null)
				{
					string setpoint_source = value.Name+"Setpoint" ;
					Channels.IInfoChannel setpoint_source_channel = OnObjectSearch(setpoint_source) ;
					SetPointSourceChannel = setpoint_source_channel ;
					valueDial.Needles[0].SourceChannel = setpoint_source_channel ;
				}
				else
				{
					valueDial.Needles[0].SourceChannel = null ;
				}

				base.SourceChannel = value ;

				NewRange(Minimum, Maximum) ;
				NewText() ;
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
					valueDial.TopText = Description ;
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
				valueDial.TopText = Description ;
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
					valueDial.BottomText = Units ;
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
				valueDial.BottomText = Units ;
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
		[Category("Dial")]
		[Description("Gets or Sets the Color of the Needle")]
		public Color NeedleColor
		{
			get
			{
				return valueDial.Needles[1].Color ;
			}
			set
			{
				valueDial.Needles[1].Color = value ;
				OnPropertyChanged("NeedleColor") ;
			}
		}
		private bool ShouldSerializeNeedleColor()
		{
			return valueDial.Needles[1].Color != Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or Sets the Color of the Fill
		/// </summary>
		[Category("Dial")]
		[Description("Gets or Sets the Color of the Fill")]
		public Color FillColor
		{
			get
			{
				return fillColor ;
			}
			set
			{
				if (thresholdChannel == null) valueDial.Needles[1].FillColor = value ;
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
		[Category("Dial")]
		[Description("Gets or Sets the Color of the Scale")]
		public Color ScaleColor
		{
			get
			{
				return valueDial.Scale.Color ;
			}
			set
			{
				valueDial.Scale.Color = value ;
				OnPropertyChanged("ScaleColor") ;
			}
		}
		private bool ShouldSerializeScaleColor()
		{
			return valueDial.Scale.Color != Color.Black ;
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
				return valueDial.Needles[1].FillEnabled ;
			}
			set
			{
				valueDial.Needles[1].FillEnabled = value ;
				OnPropertyChanged("FillEnabled") ;
			}
		}

		/// <summary>
		/// Gets or sets whether the needle is enabled
		/// </summary>
		[Category("Dial")]
		[Description("Gets or sets whether the needle is enabled")]
		[DefaultValue(true)]
		public bool NeedleEnabled
		{
			get
			{
				return valueDial.Needles[1].Visible ;
			}
			set
			{
				valueDial.Needles[1].Visible = value ;
				OnPropertyChanged("NeedleEnabled") ;
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
				valueDial.Needles[0].SourceChannel = value ;

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
				return valueDial.Needles[0].Color ;
			}
			set
			{
				valueDial.Needles[0].Color = value ;
				setPointDisplay.ForeColor = value ;
				OnPropertyChanged("SetPointColor") ;
			}
		}
		private bool ShouldSerializeSetPointColor()
		{
			return valueDial.Needles[0].Color != Color.LightSteelBlue ;
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
				return valueDial.Needles[0].Visible ;
			}
			set
			{
				valueDial.Needles[0].Visible = value ;
				OnPropertyChanged("SetPointMarkEnabled") ;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the sizes of all sections
		/// </summary>
		protected override void UpdateLayout()
		{
			//Min sizes (for exception prevention)

			//Dial (min size 2x2)
			valueDial.Location	= DisplayRectangle.Location ;
			valueDial.Width		= Math.Max(DisplayRectangle.Width, 2) ;
			valueDial.Height	= Math.Max(DisplayRectangle.Height, 2) ;

			//Displays (min size 4x0)
			valueDisplay.Left	= DisplayRectangle.X + (int)(0.33 * DisplayRectangle.Width) ;
			valueDisplay.Width	= Math.Max((int)(0.34 * DisplayRectangle.Width), 4) ;
			valueDisplay.Top	= DisplayRectangle.Y + (int)(0.84 * DisplayRectangle.Height) ;
			valueDisplay.Height	= (int)(0.13 * DisplayRectangle.Height) ;

			setPointDisplay.Left	= valueDisplay.Left ;
			setPointDisplay.Width	= valueDisplay.Width ;
			setPointDisplay.Top		= valueDisplay.Top - valueDisplay.Height ;
			setPointDisplay.Height	= valueDisplay.Height ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueDisplay.ToolTipText = NewText ;
			valueDial.ToolTipText = NewText ;
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
			valueDial.Scale.Minimum = Min ;
			valueDial.Scale.Maximum = Max ;

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
			valueDial.Scale.DecimalPlaces = DPs ;
		}

		private void NewText()
		{
			valueDial.BottomText = Units ;
			valueDial.TopText = Description ;
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
			if ((DescriptionSource == TextSources.Description && e.PropertyName == "Description")||
				(DescriptionSource == TextSources.ToolTip && e.PropertyName == "ToolTip")			)
			{
				valueDial.TopText = Description ;
			}
			else if (!UseCustomUnits && e.PropertyName == "Units")
			{
				valueDial.BottomText = Units ;
			}
			base.OnSourcePropertyChanged(Sender, e) ;
		}
		#endregion

		/// <summary>
		/// Gets the scale for the Scaled Control.
		/// </summary>
		protected override KineticaRT.Controls.Scale  ValueScale
		{
			get {return valueDial.Scale;}
		}

		/// <summary>
		/// Sets the fill color for this object.
		/// Used for setting the LowLow threshold color when connected to threshold channels.
		/// </summary>
		/// <param name="color">The new color.</param>
		/// <param name="centrePoint">The new center point about which the fill is drawn.</param>
		protected override void SetFill (double centrePoint, System.Drawing.Color color)
		{
			valueDial.Needles[1].CentrePoint = centrePoint ;
			if (color == System.Drawing.Color.Empty)
			{
				valueDial.Needles[1].FillColor = fillColor ;
			}
			else
			{
				valueDial.Needles[1].FillColor = color ;
			}
		}

	}
}

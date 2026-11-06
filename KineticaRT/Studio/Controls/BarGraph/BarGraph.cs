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
	[LicenseProviderAttribute(typeof(BarGraph.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(BarGraph))]
	public class BarGraph : ScaledControl
	{
		#region License
#if KrtStudio_license
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
						if ((key == null) || (key != "D66207683286"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "D66207683286")) key = "{D6620768-3286-40b9-B497-27D4E5F0EACC}" ; 
						context.SetSavedLicenseKey (typeof(BarGraph), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(BarGraph), null) ;
					if (key == "{D6620768-3286-40b9-B497-27D4E5F0EACC}") licence = new License ("Run") ;
//					else if (allowExceptions && (context.UsageMode != LicenseUsageMode.Designtime)) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
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
		/// Possible layouts for Description and Units sections
		/// </summary>
		public enum Layouts
		{
			/// <summary>
			/// Description on Left, Units on Right
			/// </summary>
			DescriptionGapUnits,
			/// <summary>
			/// Description on Left, Units centred
			/// </summary>
			DescriptionUnitsGap,
			/// <summary>
			/// Description centred, Units on Right
			/// </summary>
			GapDescriptionUnits,
			/// <summary>
			/// Units centred, Description on Right
			/// </summary>
			GapUnitsDescription,
			/// <summary>
			/// Units on Left, Description centred
			/// </summary>
			UnitsDescriptionGap,
			/// <summary>
			/// Units on Left, Description on Right
			/// </summary>
			UnitsGapDescription,
		}

		private KineticaRT.Controls.BarGraph valueBarGraph;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int				scaleWidth ;
		private	Layouts			layout ;

		private Rectangle		unitsRect ;
		private Rectangle		descriptionRect ;
		private StringFormat	descriptionFormat ;
		private StringFormat	unitsFormat ;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the BarGraph class
		/// </summary>
		public BarGraph()
		{
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif

			descriptionFormat = new StringFormat() ;
			unitsFormat = new StringFormat() ;
			descriptionFormat.LineAlignment	= StringAlignment.Center ;
			unitsFormat.LineAlignment		= StringAlignment.Center ;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

//			//demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
//			demoLabel.Visible = demoMode ;

			scaleWidth	= 32 ;
			layout		= Layouts.DescriptionUnitsGap ;

			UpdateLayout() ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
//			if (license != null)	{license.Dispose();license = null;}

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
			this.valueBarGraph = new KineticaRT.Controls.BarGraph();
			this.SuspendLayout();
			// 
			// valueBarGraph
			// 
            this.valueBarGraph.BorderType = KineticaRT.Controls.BorderType.None;
			this.valueBarGraph.Location = new System.Drawing.Point(0, 24);
			this.valueBarGraph.Name = "valueBarGraph";
			this.valueBarGraph.Size = new System.Drawing.Size(296, 128);
			this.valueBarGraph.TabIndex = 0;
			this.valueBarGraph.Value = null;
			// 
			// BarGraph
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueBarGraph});
			this.Name = "BarGraph";
			this.Size = new System.Drawing.Size(300, 150);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the text formatting of the Value
		/// </summary>
		[Browsable(false)]
		public override string Format
		{
			get		{	return base.Format ;	}
			set		{	base.Format = value ;	}
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
				valueBarGraph.SourceChannel = value ;
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
		/// Gets or Sets the Color of the Bars
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets or Sets the Color of the Bars")]
		public Color BarColor
		{
			get
			{
				return valueBarGraph.BarColor ;
			}
			set
			{
				valueBarGraph.BarColor = value ;
				OnPropertyChanged("BarColor") ;
			}
		}
		private bool ShouldSerializeBarColor()
		{
			return valueBarGraph.BarColor != Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or Sets the Color of the Values
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets or Sets the Color of the Values")]
		public Color ValueColor
		{
			get
			{
				return valueBarGraph.NumberColor ;
			}
			set
			{
				valueBarGraph.NumberColor = value ;
				OnPropertyChanged("ValueColor") ;
			}
		}
		private bool ShouldSerializeValueColor()
		{
			return valueBarGraph.NumberColor != Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or Sets the Color of the Scales
		/// </summary>
		[Category("Scale")]
		[Description("Gets or Sets the Color of the Scales")]
		public Color ScaleColor
		{
			get
			{
				return valueBarGraph.LeftScale.Color ;
			}
			set
			{
				valueBarGraph.LeftScale.Color = value ;
				valueBarGraph.RightScale.Color = value ;
				OnPropertyChanged("ScaleColor") ;
			}
		}
		private bool ShouldSerializeScaleColor()
		{
			return valueBarGraph.LeftScale.Color != Color.Black ;
		}

		/// <summary>
		/// Gets or sets the width devoted to each scale (in pixels)
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the width devoted to each scale (in pixels)")]
		[DefaultValue(32)]
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
		/// Gets or sets the Layout of description and units
		/// </summary>
		[Category("BarGraph")]
		[Description("Gets or sets the Layout of description and units")]
		[DefaultValue(Layouts.DescriptionUnitsGap)]
		public Layouts InformationLayout
		{
			get
			{
				return layout ;
			}
			set
			{
				if (value != layout)
				{
					layout = value ;
					UpdateLayout() ;
					OnPropertyChanged("Layout") ;
				}
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

			lock (this)
			{
				Brush		brush = new SolidBrush(ForeColor) ;

				//draw units and description text
				if (descriptionRect.Width > 0)
				{
					e.Graphics.DrawString(Description, Font, brush, descriptionRect, descriptionFormat) ;
				}
				if (unitsRect.Width > 0)
				{
					e.Graphics.DrawString(Units, Font, brush, unitsRect, unitsFormat) ;
				}

				brush.Dispose() ;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the sizes of all sections
		/// </summary>
		protected override void UpdateLayout()
		{
			lock (this)
			{
				//Min sizes (for exception prevention)
				//Graph ??

				unitsRect = Rectangle.Empty ;
				descriptionRect = Rectangle.Empty ;

				descriptionRect.X		= DisplayRectangle.Top ;
				descriptionRect.Height	= Font.Height + 4 ;
				unitsRect.X				= descriptionRect.Top ;
				unitsRect.Height		= descriptionRect.Height ;
				unitsRect.Width			= DisplayRectangle.Width / 4 ;		//always 1/4 of space

				valueBarGraph.Left	= DisplayRectangle.Left ;
				valueBarGraph.Width = DisplayRectangle.Width ;
				valueBarGraph.Top	= descriptionRect.Bottom ;
				valueBarGraph.Height= DisplayRectangle.Height - descriptionRect.Height ;

				switch (layout)
				{
					case Layouts.DescriptionGapUnits:
					{
						descriptionRect.X		= DisplayRectangle.X ;
						descriptionRect.Width	= DisplayRectangle.Width - unitsRect.Width ;
						unitsRect.X				= descriptionRect.Right ;

						unitsFormat.Alignment		= StringAlignment.Far ;
						descriptionFormat.Alignment	= StringAlignment.Near ;
					}
					break ;

					case Layouts.DescriptionUnitsGap:
					{
						descriptionRect.X		= DisplayRectangle.X ;
						descriptionRect.Width	= (DisplayRectangle.Width - unitsRect.Width) / 2 ;
						unitsRect.X				= descriptionRect.Right ;

						unitsFormat.Alignment		= StringAlignment.Center ;
						descriptionFormat.Alignment	= StringAlignment.Near ;
					}
					break ;

					case Layouts.GapDescriptionUnits:
					{
						descriptionRect.Width	= DisplayRectangle.Width / 2 ;
						descriptionRect.X		= DisplayRectangle.X + unitsRect.Width ;
						unitsRect.X				= descriptionRect.Right ;

						unitsFormat.Alignment		= StringAlignment.Far ;
						descriptionFormat.Alignment	= StringAlignment.Center ;
					}
					break ;

					case Layouts.GapUnitsDescription:
					{
						descriptionRect.Width	= (DisplayRectangle.Width - unitsRect.Width) / 2 ;
						unitsRect.X				= DisplayRectangle.X + descriptionRect.Width ;
						descriptionRect.X		= unitsRect.Right ;

						unitsFormat.Alignment		= StringAlignment.Center ;
						descriptionFormat.Alignment	= StringAlignment.Far ;
					}
					break ;

					case Layouts.UnitsDescriptionGap:
					{
						descriptionRect.Width	= DisplayRectangle.Width / 2 ;
						unitsRect.X				= DisplayRectangle.X ;
						descriptionRect.X		= unitsRect.Right ;

						unitsFormat.Alignment		= StringAlignment.Near ;
						descriptionFormat.Alignment	= StringAlignment.Center ;
					}
					break ;

					case Layouts.UnitsGapDescription:
					{
						unitsRect.X				= DisplayRectangle.X ;
						descriptionRect.X		= unitsRect.Right ;
						descriptionRect.Width	= DisplayRectangle.Width - unitsRect.Width ;

						unitsFormat.Alignment		= StringAlignment.Near ;
						descriptionFormat.Alignment	= StringAlignment.Far ;
					}
					break ;
				}

				Invalidate(DisplayRectangle) ;
			}
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueBarGraph.ToolTipText = NewText ;
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
			valueBarGraph.LeftScale.Minimum = Min ;
			valueBarGraph.LeftScale.Maximum = Max ;

			valueBarGraph.RightScale.Minimum = Min ;
			valueBarGraph.RightScale.Maximum = Max ;

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
			valueBarGraph.LeftScale.DecimalPlaces = DPs ;
			valueBarGraph.RightScale.DecimalPlaces = DPs ;
		}

		/// <summary>
		/// Correctly (re)position the scales
		/// </summary>
		private void SetScalePosition()
		{
			//resize the scales
			valueBarGraph.ScaleWidth = ScaleWidth ;
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

		/// <summary>
		/// Font Changed event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnFontChanged(EventArgs e)
		{
			UpdateLayout() ;
			base.OnFontChanged(e) ;
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
	}
}

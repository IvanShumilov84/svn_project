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
	/// KineticaRT Studio Indicator control.
	/// Displays a boolean Value from a source channel,
	/// together with an optional description
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(Indicator.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Indicator))]
	public class Indicator : ControlBase
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
						if ((key == null) || (key != "76BE12E4E2C8"))
						{
							new KineticaRT.LicenceForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "76BE12E4E2C8")) key = "{76BE12E4-E2C8-42dc-9BC1-4599E802B48E}" ; 
						context.SetSavedLicenseKey (typeof(Indicator), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Indicator), null) ;
					if (key == "{76BE12E4-E2C8-42dc-9BC1-4599E802B48E}") licence = new License ("Run") ;
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
		/// Enumeration of possible positions for the Description
		/// </summary>
		public enum DescriptionPositions
		{
			/// <summary>
			/// To left of Lamp
			/// </summary>
			Left,
			/// <summary>
			/// To Right of Lamp
			/// </summary>
			Right,
			/// <summary>
			/// On the Lamp
			/// </summary>
			On,
		}

		private KineticaRT.Controls.Indicator valueIndicator;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Rectangle		descriptionRect ;
		private StringFormat	stringFormat ;

		private StringAlignment			descriptionAlignment ;
		private DescriptionPositions	descriptionPosition ;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Indicator class
		/// </summary>
		public Indicator()
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

			descriptionPosition	= DescriptionPositions.Left ;
			descriptionAlignment= StringAlignment.Near ;

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
			KineticaRT.Controls.State state1 = new KineticaRT.Controls.State();
			this.valueIndicator = new KineticaRT.Controls.Indicator();
			this.SuspendLayout();
			// 
			// valueIndicator
			// 
			this.valueIndicator.BackColor = System.Drawing.Color.SteelBlue;
			this.valueIndicator.BorderColor = System.Drawing.Color.SteelBlue;
			this.valueIndicator.Location = new System.Drawing.Point(120, 0);
			this.valueIndicator.Name = "valueIndicator";
			this.valueIndicator.Size = new System.Drawing.Size(24, 24);
			state1.BackColor = System.Drawing.SystemColors.Control;
			state1.BorderColor = System.Drawing.SystemColors.Control;
			this.valueIndicator.States = new KineticaRT.Controls.StateCollection(new KineticaRT.Controls.State[] {
																													 state1});
			this.valueIndicator.TabIndex = 0;
			this.valueIndicator.UseSourceFormat = false;
			// 
			// Indicator
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueIndicator});
			this.Name = "Indicator";
			this.Size = new System.Drawing.Size(150, 24);
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
				valueIndicator.SourceChannel = value ;
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
					if (descriptionPosition == DescriptionPositions.On)
					{
						valueIndicator.Text = Description ;
					}
					else
					{
						Invalidate(descriptionRect) ;
					}
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
				if (descriptionPosition == DescriptionPositions.On)
				{
					valueIndicator.Text = Description ;
				}
				else
				{
					Invalidate(descriptionRect) ;
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
		/// Gets or sets the shape of the lamp
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets the shape of the lamp")]
		[DefaultValue(true)]
		public KineticaRT.Controls.Shapes Shape
		{
			get
			{
				return valueIndicator.Shape ;
			}
			set
			{
				if (valueIndicator.Shape != value)
				{
					valueIndicator.Shape = value ;
					OnPropertyChanged("Shape") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the lamp is raised or flat
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets whether the lamp is raised or flat")]
		[DefaultValue(true)]
		public bool Raised
		{
			get
			{
				return (valueIndicator.BorderStyle == KineticaRT.Controls.BorderStyles.Raised) ;
			}
			set
			{
				KineticaRT.Controls.BorderStyles		new_style ;

				new_style = value ? KineticaRT.Controls.BorderStyles.Raised : KineticaRT.Controls.BorderStyles.None ;
				if (valueIndicator.BorderStyle != new_style)
				{
					valueIndicator.BorderStyle = new_style ;
					OnPropertyChanged("Raised") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Off (false) Color of the lamp
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or Sets the Off (false) Color of the lamp")]
		public Color OffColor
		{
			get
			{
				return valueIndicator.States[0].BackColor ;
			}
			set
			{
				valueIndicator.States[0].BackColor = value ;
				valueIndicator.States[0].BorderColor = value ;
				OnPropertyChanged("OffColor") ;
			}
		}
		private bool ShouldSerializeOffColor()
		{
			return valueIndicator.States[0].BackColor != SystemColors.Control ;
		}

		/// <summary>
		/// Gets or Sets the On (true) Color of the lamp
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or Sets the On (true) Color of the lamp")]
		public Color OnColor
		{
			get
			{
				return valueIndicator.BackColor ;
			}
			set
			{
				valueIndicator.BackColor = value ;
				valueIndicator.BorderColor = value ;
				OnPropertyChanged("OnColor") ;
			}
		}
		private bool ShouldSerializeOnColor()
		{
			return valueIndicator.BackColor != Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or sets where the description is positioned
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets where the description is positioned")]
		[DefaultValue(DescriptionPositions.Left)]
		public DescriptionPositions DescriptionPosition
		{
			get
			{
				return descriptionPosition ;
			}
			set
			{
				if (descriptionPosition != value)
				{
					descriptionPosition = value ;
					OnPropertyChanged("DescriptionPosition") ;
					
					if (value == DescriptionPositions.On)
					{
						valueIndicator.Text = Description ;
					}
					else
					{
						valueIndicator.Text = "" ;
					}

					UpdateLayout() ;
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
		/// Updates the layout of value, description and causes a repaint
		/// </summary>
		protected override void UpdateLayout()
		{
			valueIndicator.Height	= DisplayRectangle.Height ;
			valueIndicator.Top		= DisplayRectangle.Top ;

			switch (descriptionPosition)
			{
				case DescriptionPositions.Left:
				{
					valueIndicator.Width	= valueIndicator.Height ;

					descriptionRect			= new Rectangle(0, DisplayRectangle.Y, 0, DisplayRectangle.Height) ;
					descriptionRect.Width	= DisplayRectangle.Width - valueIndicator.Width ;

					descriptionRect.X		= DisplayRectangle.X ;
					valueIndicator.Left		= descriptionRect.Right ;
					
					Invalidate(DisplayRectangle) ;
				}
				break ;

				case DescriptionPositions.Right:
				{
					valueIndicator.Width	= valueIndicator.Height ;

					descriptionRect			= new Rectangle(0, DisplayRectangle.Y, 0, DisplayRectangle.Height) ;
					descriptionRect.Width	= DisplayRectangle.Width - valueIndicator.Width ;

					valueIndicator.Left		= DisplayRectangle.X ;
					descriptionRect.X		= valueIndicator.Right ;
					
					Invalidate(DisplayRectangle) ;
				}
				break ;

				default:		//lamp takes up all space
				{
					valueIndicator.Width	= DisplayRectangle.Width ;
					valueIndicator.Left		= DisplayRectangle.Left ;
					descriptionRect = Rectangle.Empty ;
				}
				break ;
			}
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueIndicator.ToolTipText = NewText ;
			base.NewToolTip(NewText) ;
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
				if (descriptionPosition == DescriptionPositions.On && (e.PropertyName == "Description" || e.PropertyName == "ToolTip"))
				{
					valueIndicator.Text = Description ;
				}
				else
				{
					Invalidate(DisplayRectangle) ;
				}
			}
			base.OnSourcePropertyChanged(Sender, e) ;
		}
		#endregion
	}
}

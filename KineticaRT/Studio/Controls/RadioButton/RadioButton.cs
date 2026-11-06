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
	/// KineticaRT Studio Radio Button control.
	/// Displays a boolean Value from a source channel,
	/// together with an optional description
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(RadioButton.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(RadioButton))]
	public class RadioButton : ControlBase, KineticaRT.Controls.IButton
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
						if ((key == null) || (key != "4EC609AE08AE"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "4EC609AE08AE")) key = "{4EC609AE-08AE-4d7b-9434-667CB0689B78}" ; 
						context.SetSavedLicenseKey (typeof(RadioButton), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(RadioButton), null) ;
					if (key == "{4EC609AE-08AE-4d7b-9434-667CB0689B78}") licence = new License ("Run") ;
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

		private KineticaRT.Controls.Button valueButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#region Initialisation and disposal
		/// <summary>
		/// Initialises a new instance of the RadioButton class
		/// </summary>
		public RadioButton()
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
			this.valueButton.BoxOnLeft = false;
			this.valueButton.Name = "valueButton";
			this.valueButton.Size = new System.Drawing.Size(150, 24);
			this.valueButton.Style = KineticaRT.Controls.ButtonStyles.RadioButton;
			this.valueButton.TabIndex = 0;
			this.valueButton.Text = "";
			this.valueButton.Type = KineticaRT.Controls.ButtonReturnTypes.Sticky;
			this.valueButton.UseSourceFormat = false;
			// 
			// RadioButton
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueButton});
			this.Name = "RadioButton";
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
				valueButton.SourceChannel = value ;
				valueButton.DestinationChannel = value ;
				base.SourceChannel = value ;
				valueButton.Text = Description ;
			}
		}

		/// <summary>
		/// Gets or sets whether the description is to the left of the radio button
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets whether the description is to the left of the radion button")]
		[DefaultValue(true)]
		public bool DescriptionOnLeft
		{
			get
			{
				return !valueButton.BoxOnLeft ;
			}
			set
			{
				valueButton.BoxOnLeft = !value ;
				OnPropertyChanged("DescriptionOnLeft") ;
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
					valueButton.Text = Description ;
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
				valueButton.Text = Description ;
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
		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the sizes of all sections
		/// </summary>
		protected override void UpdateLayout()
		{
			//NB - min sizes are there for exception prevention

			//Button (min size 2x2)
			valueButton.Location	= DisplayRectangle.Location ;
			valueButton.Width		= Math.Max(DisplayRectangle.Width, 2) ;
			valueButton.Height		= Math.Max(DisplayRectangle.Height, 2) ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueButton.ToolTipText = NewText ;
			base.NewToolTip(NewText) ;
		}

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
				valueButton.Text = Description ;
			}

			base.OnSourcePropertyChanged(Sender, e) ;
		}
		#endregion

		#region IButton
		/// <summary>Occurs when the value of the Value property changes.</summary>
		public event KineticaRT.Controls.ValueChangedEventHandler ValueChanged ;

		private void OnButtonValueChanged(object sender, Values.Value newValue)
		{
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
	}
}

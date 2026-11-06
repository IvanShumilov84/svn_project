using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// Button control which brings up a dialog allowing current channels to be selected
	/// </summary>
#if KrtStudio_license
	[LicenseProviderAttribute(typeof(ChannelSelector.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(ChannelSelector))]
	[DefaultProperty("Server")]
	public class ChannelSelector : System.Windows.Forms.Button, IConnectable
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
						if ((key == null) || (key != "7D05906ABE0E"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "7D05906ABE0E")) key = "{7D05906A-BE0E-4133-A0F2-537599F2FBC7}" ; 
						context.SetSavedLicenseKey (typeof(ChannelSelector), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(ChannelSelector), null) ;
					if (key == "{7D05906A-BE0E-4133-A0F2-537599F2FBC7}") licence = new License ("Run") ;
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

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ChannelSelectorForm			form ;
		private string						server ;

		#region Initialisation and Disposal
		/// <summary>
		/// Creates a new instance of the ChannelSelector class
		/// </summary>
		public ChannelSelector()
		{
#if KrtStudio_license
			//exception if no license
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
#endif

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			form = null ;
			server = "" ;
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
			// 
			// ChannelSelector
			// 
			this.Name = "ChannelSelector";
			this.Size = new System.Drawing.Size(120, 24);
			this.Text = "Channels...";
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the Collection from which available channels are determined
		/// </summary>
		[Description("Gets or sets the name of the Collection from which available channels are determined")]
		[Category("Channels")]
		[DefaultValue("")]
		public string Channels
		{
			get
			{
				return server ;
			}
			set
			{
				if (server != value)
				{
					server = value ;

					SetServer(OnObjectSearch(server) as ChannelCollection) ;

					OnPropertyChanged("Channels") ;
				}
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Defines the Collection from which available channels are determined
		/// </summary>
		/// <param name="Server">Collection containing available channels</param>
		/// <returns>True if successfully set, false if failed (eg if form showing)</returns>
		public bool SetServer(ChannelCollection Server)
		{
			if (!connected) return false ;

			//pass it on, but don't store a reference
			if (form == null)
			{
				form = new ChannelSelectorForm() ;
			}
			 return form.SetServer(server + ".", Server) ;
		}
		#endregion

		#region Events
		/// <summary>
		/// Click Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnClick(EventArgs e)
		{
			if (form == null)
			{
				form = new ChannelSelectorForm() ;
			}

			if (form.Visible)
			{
				form.Hide() ;
			}
			else
			{
				form.Show() ;
			}

			base.OnClick(e) ;
		}
		#endregion

		#region IConnectable
		/// <summary>
		/// Standard Event generated whenever a property (including the Value) is changed
		/// </summary>
		public event PropertyChangedEventHandler	PropertyChanged;
		/// <summary>
		/// The named property has changed: generate the PropertyChanged event
		/// </summary>
		/// <param name="propertyName">Name of property that has changed</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  //Invokes the delegates.
		}

		/// <summary>
		/// Event for requests to find an object by name.
		/// </summary>
		public event KineticaRT.ObjectSearchEventHandler ObjectSearch;   
		/// <summary>
		/// Fires an ObjectSearch event for the named object
		/// </summary>
		/// <summary>
		/// Fires an ObjectSearch event for the named object
		/// </summary>
		private object OnObjectSearch(string objectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName) ;  
		}

		/// <summary>
		/// Looks for the named object
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>Always returns null</returns>
		public virtual object FindObject(string objectName)
		{
			return null ;
		}

		/// <summary>
		/// Returns the named sibling object or null if it does not exist.
		/// </summary>
		public virtual object SearchObject (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

		private bool connected = false ;

		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public virtual void Connect ()
		{
			connected = true ;
			if (server != "")		SetServer(OnObjectSearch(server) as ChannelCollection) ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			SetServer(null) ;
			connected = false ;
		}
		#endregion
	}
}

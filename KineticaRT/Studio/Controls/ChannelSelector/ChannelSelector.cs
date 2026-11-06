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
	[ToolboxBitmap(typeof(ChannelSelector))]
	[DefaultProperty("Server")]
	public class ChannelSelector : System.Windows.Forms.Button, IConnectable
	{
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

		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public virtual void Connect ()
		{
			this.Connected = true ; //ft
			if (server != "")		SetServer(OnObjectSearch(server) as ChannelCollection) ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			SetServer(null) ;
			this.Connected = false ; //ft
		}
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool Connected //ft //ft
        {
            get { return connected; }
            set { connected = value; }
        }
		#endregion
	}
}

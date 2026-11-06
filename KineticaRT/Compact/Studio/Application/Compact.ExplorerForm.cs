using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32 ;

namespace KineticaRT.Compact.Studio
{
	/// <summary>
	/// Container form for a KineticaRT Explorer control
	/// </summary>
	public class ExplorerForm : System.Windows.Forms.Form
	{
        private KineticaRT.Compact.Studio.StudioExplorer explorer;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>Event generated to Save the given collection to file</summary>
		public event StudioExplorer.SaveCollectionEventHandler	SaveCollection ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises an new instance of the ExplorerForm class
		/// </summary>
		public ExplorerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			explorer.SaveCollection += new StudioExplorer.SaveCollectionEventHandler(OnSaveCollection) ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion (initialisation)

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExplorerForm));
			this.explorer = new KineticaRT.Compact.Studio.StudioExplorer();
			this.SuspendLayout();
			// 
			// explorer
			// 
			this.explorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.explorer.Location = new System.Drawing.Point(0, 0);
			this.explorer.Name = "explorer";
			this.explorer.Size = new System.Drawing.Size(504, 302);
			this.explorer.TabIndex = 0;
			// 
			// ExplorerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 302);
			this.Controls.Add(this.explorer);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExplorerForm";
			this.Text = "Explorer";
			this.SizeChanged += new System.EventHandler(this.ExplorerForm_SizeChanged);
			this.Load += new System.EventHandler(this.ExplorerForm_Load);
			this.Move += new System.EventHandler(this.ExplorerForm_Move);
			this.ResumeLayout(false);

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds the given object to the Root of the Explorer
		/// </summary>
		/// <param name="newObj">Object to be added</param>
		/// <param name="Name">Name of object, as it will be displayed in the Explorer</param>
		public void AddObject(string Name, object newObj)
		{
			explorer.AddRootObject(Name, newObj) ;
		}

		/// <summary>
		/// Removes all objects from the Explorer
		/// </summary>
		public void Clear()
		{
			explorer.Clear() ;
		}

		/// <summary>
		/// Removes the given object from the root level of the Tree, if it was there
		/// </summary>
		/// <param name="Object">Object to be removed</param>
		public void RemoveObject(object Object)
		{
			explorer.RemoveObject(Object) ;
		}

		/// <summary>
		/// Sets up the list of channels that can be added to the tree
		/// </summary>
		/// <param name="Root">XmlElement containing the addable channels</param>
		public void SetAddList(System.Xml.XmlElement Root)
		{
			explorer.SetAddList(Root) ;
		}

		/// <summary>
		/// Focuses on the given root object of the Tree, if it exists
		/// </summary>
		/// <param name="RootObject">Object at the root level upon which to focus</param>
		/// <returns>True if succeded, False if object not found</returns>
		public bool SelectObject(object RootObject)
		{
			return explorer.SelectObject(RootObject) ;
		}
		#endregion (public methods)

		/// <summary>
		/// Closing Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnClosing(CancelEventArgs e)
		{
			e.Cancel = true ;
			//this.WindowState = FormWindowState.Minimized ;
			base.OnClosing(e) ;
		}

		private void OnSaveCollection(ChannelCollection Collection)
		{
			if (SaveCollection != null)		SaveCollection(Collection) ;
		}

		private void ExplorerForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				RegistryKey key =  Registry.CurrentUser.OpenSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\"+GetType().ToString()) ;
				if (key != null)
				{
					WindowState = (FormWindowState) key.GetValue ("State") ;
					SetBounds ((int) key.GetValue ("Left"),(int) key.GetValue ("Top"),(int) key.GetValue ("Width"),(int) key.GetValue ("Height"),System.Windows.Forms.BoundsSpecified.All) ;
					if (!SystemInformation.VirtualScreen.IntersectsWith (RectangleToScreen(ClientRectangle)))this.Location = new System.Drawing.Point(10,10) ;
				}
			}
			catch {}
		}

		private void ExplorerForm_SizeChanged(object sender, System.EventArgs e)
		{
			SaveWindowSettings () ;
		}

		private void ExplorerForm_Move(object sender, System.EventArgs e)
		{
			SaveWindowSettings () ;
		}

		private void SaveWindowSettings () 
		{
			if (Created)
			{
				RegistryKey key =  Registry.CurrentUser.CreateSubKey(@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\"+GetType().ToString()) ;

				key.SetValue ("State", (int)WindowState) ;

				if (WindowState == System.Windows.Forms.FormWindowState.Normal)
				{
					key.SetValue ("Left", Left) ;
					key.SetValue ("Top", Top) ;
					key.SetValue ("Width", Size.Width) ;
					key.SetValue ("Height", Size.Height) ;
				}
			}
		}
	}
}

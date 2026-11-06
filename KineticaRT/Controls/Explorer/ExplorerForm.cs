using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32 ;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Basic container form for a KineticaRT Explorer control
	/// </summary>
	public class ExplorerForm : System.Windows.Forms.Form
	{
		private KineticaRT.Controls.Explorer ctlExplorer;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initialises an new instance of the ExplorerForm class
		/// </summary>
		public ExplorerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			RegistryKey key =  Registry.CurrentUser.CreateSubKey(@"Software\KineticaRT\Studio\"+this.GetType().ToString()) ;
			key.SetValue ("Left", Left) ;
			key.SetValue ("Top", Top) ;
			key.SetValue ("Width", Size.Width) ;
			key.SetValue ("Height", Size.Height) ;

			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ctlExplorer = new KineticaRT.Controls.Explorer();
			this.SuspendLayout();
			// 
			// ctlExplorer
			// 
			this.ctlExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlExplorer.Location = new System.Drawing.Point(0, 0);
			this.ctlExplorer.Name = "ctlExplorer";
			this.ctlExplorer.Size = new System.Drawing.Size(504, 302);
			this.ctlExplorer.TabIndex = 0;
			// 
			// ExplorerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 302);
			this.Controls.Add(this.ctlExplorer);
			this.Name = "ExplorerForm";
			this.Text = "Explorer";
			this.Load += new System.EventHandler(this.ExplorerForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Sets the given object to be the Root of the Explorer
		/// </summary>
		/// <param name="newObj">Object to set as the Root object</param>
		public void SelectObject(object newObj)
		{
			ctlExplorer.Clear() ;
			ctlExplorer.AddRootObject(newObj) ;
		}

		/// <summary>
		/// Closed Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnClosed(EventArgs e)
		{
			ctlExplorer.Dispose() ;
			base.OnClosed(e) ;
		}

		private void ExplorerForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				RegistryKey key =  Registry.CurrentUser.OpenSubKey (@"Software\KineticaRT\Studio\ExplorerForm") ;
				if (key != null)
				{
					Show () ;
					SetBounds ((int) key.GetValue ("Left"),(int) key.GetValue ("Top"),(int) key.GetValue ("Width"),(int) key.GetValue ("Height"),System.Windows.Forms.BoundsSpecified.All) ;
				}
			}
			catch {}
		}
	}
}

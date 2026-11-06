using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KineticaRT.Explorer
{
	/// <summary>
	/// Summary description for ExplorerForm.
	/// </summary>
	public class ExplorerForm : System.Windows.Forms.Form
	{
		private KineticaRT.Explorer.Explorer ctlExplorer;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

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
			this.ctlExplorer = new KineticaRT.Explorer.Explorer();
			this.SuspendLayout();
			// 
			// ctlExplorer
			// 
			this.ctlExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlExplorer.Name = "ctlExplorer";
			this.ctlExplorer.RootObject = null;
			this.ctlExplorer.Size = new System.Drawing.Size(504, 302);
			this.ctlExplorer.TabIndex = 0;
			// 
			// ExplorerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 302);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.ctlExplorer});
			this.Name = "ExplorerForm";
			this.Text = "Explorer";
			this.ResumeLayout(false);

		}
		#endregion

		public void SelectObject(object newObj)
		{
			ctlExplorer.RootObject = newObj ;
		}
	}
}

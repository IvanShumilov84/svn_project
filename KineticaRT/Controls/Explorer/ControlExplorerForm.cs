using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32 ;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Form that displays a Property Grid for te properties of a Control or set of controls.
	/// </summary>
	public class ControlExplorerForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initialises an new instance of the ControlExplorerForm class
		/// </summary>
		public ControlExplorerForm()
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
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.CommandsVisibleIfAvailable = true;
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.LargeButtons = false;
			this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(248, 302);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.Text = "propertyGrid1";
			this.propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// ControlExplorerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(248, 302);
			this.Controls.Add(this.propertyGrid1);
			this.Name = "ControlExplorerForm";
			this.Text = "Properties";
			this.SizeChanged += new System.EventHandler(this.ControlExplorerForm_SizeChanged);
			this.Load += new System.EventHandler(this.ControlExplorerForm_Load);
			this.Move += new System.EventHandler(this.ControlExplorerForm_Move);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Displays the given objects in the Property Grid
		/// </summary>
		/// <param name="newObjects">Objects to view the properties of</param>
		public void SelectObjects(object[] newObjects)
		{
			propertyGrid1.SelectedObjects = newObjects ;
		}

		private void ControlExplorerForm_Load(object sender, System.EventArgs e)
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

		private void ControlExplorerForm_SizeChanged(object sender, System.EventArgs e)
		{
			SaveWindowSettings () ;
		}

		private void ControlExplorerForm_Move(object sender, System.EventArgs e)
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

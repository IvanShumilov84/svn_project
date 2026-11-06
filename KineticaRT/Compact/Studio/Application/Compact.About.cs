using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KineticaRT.Compact.Studio
{
	/// <summary>
	/// About Dialog for KineticaRT.Studio
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label appName;
		private System.Windows.Forms.Label appVersion;
		private System.Windows.Forms.Button infoButton;
		private System.Windows.Forms.PictureBox banner;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initialises a new instance of the About class
		/// </summary>
		public About()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			appName.Text = System.IO.Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) ;
			appVersion.Text = "Version: " + System.Windows.Forms.Application.ProductVersion ;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
			this.OKButton = new System.Windows.Forms.Button();
			this.appName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.appVersion = new System.Windows.Forms.Label();
			this.infoButton = new System.Windows.Forms.Button();
			this.banner = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// OKButton
			// 
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKButton.Location = new System.Drawing.Point(384, 176);
			this.OKButton.Name = "OKButton";
			this.OKButton.TabIndex = 0;
			this.OKButton.Text = "&OK";
			// 
			// appName
			// 
			this.appName.Location = new System.Drawing.Point(8, 64);
			this.appName.Name = "appName";
			this.appName.Size = new System.Drawing.Size(448, 16);
			this.appName.TabIndex = 2;
			this.appName.Text = "App Name";
			this.appName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(448, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Copyright © 2004 KineticaRT Ltd.   All rights reserved.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// appVersion
			// 
			this.appVersion.Location = new System.Drawing.Point(8, 88);
			this.appVersion.Name = "appVersion";
			this.appVersion.Size = new System.Drawing.Size(448, 16);
			this.appVersion.TabIndex = 4;
			this.appVersion.Text = "App Version";
			this.appVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// infoButton
			// 
			this.infoButton.Location = new System.Drawing.Point(8, 176);
			this.infoButton.Name = "infoButton";
			this.infoButton.Size = new System.Drawing.Size(80, 23);
			this.infoButton.TabIndex = 5;
			this.infoButton.Text = "&System Info";
			this.infoButton.Click += new System.EventHandler(this.infoButton_Click);
			// 
			// banner
			// 
			this.banner.Dock = System.Windows.Forms.DockStyle.Top;
			this.banner.Image = ((System.Drawing.Image)(resources.GetObject("banner.Image")));
			this.banner.Location = new System.Drawing.Point(0, 0);
			this.banner.Name = "banner";
			this.banner.Size = new System.Drawing.Size(468, 56);
			this.banner.TabIndex = 6;
			this.banner.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 144);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(448, 24);
			this.label1.TabIndex = 7;
			this.label1.Text = "WARNING: This computer program is protected by copyright law and international tr" +
				"eaties. Refer to the license agreement for more information";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// About
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(468, 208);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.banner);
			this.Controls.Add(this.infoButton);
			this.Controls.Add(this.appVersion);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.appName);
			this.Controls.Add(this.OKButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.ResumeLayout(false);

		}
		#endregion

		private void infoButton_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("msinfo32.exe") ;
		}
	}
}

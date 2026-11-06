using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace KineticaRT.Studio
{
	/// <summary>
	/// Provides a splash screen window for KineticaRT Studio.
	/// </summary>
    /// <remarks>Used during application loading.</remarks>
    [ChangeRecord("2.0.0.0", "11 07 2006", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
    [Copyright("Copyright © KineticaRT Ltd 2006")]
    public class Splash : System.Windows.Forms.Form, ISplash
	{
        private static TraceSwitch studioSwitch = new TraceSwitch("Studio", "Studio Messages");
        
        private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initialises a new instance of the Splash class
		/// </summary>
		public Splash()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            try
            {
                string full_path = System.IO.Path.GetFullPath("Logo.gif");
                this.pictureBox1.Image = System.Drawing.Bitmap.FromFile(full_path);
                Size size = this.pictureBox1.Image.Size;
                if (size.Width > 304)
                {
                    this.Width = size.Width;
                    this.pictureBox1.Width = size.Width;
                }
                if (size.Height > 64)
                {
                    this.pictureBox1.Height = size.Height;
                    this.Height = this.Height + size.Height - 64;
                }
            }
            catch (System.IO.IOException e)
            {
                System.Diagnostics.Trace.WriteLineIf(studioSwitch.TraceInfo, "Logo.gif file not loaded, using default : " + e.Message);
            }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // lblProgress
            // 
            resources.ApplyResources(this.lblProgress, "lblProgress");
            this.lblProgress.Name = "lblProgress";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::KineticaRT.Studio.Properties.Resources.KineticaRT_Logo;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // Splash
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Splash";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Sets the top line of text.
		/// </summary>
        /// <remarks>Called by KineticaRT Studio.</remarks>
        /// <param name="Text">Text to set.</param>
		public void SetText(string Text)
		{
			lblTitle.Text = Text ;
			this.Refresh() ;
		}

		/// <summary>
		/// Sets the bottom line of text.
		/// </summary>
        /// <remarks>Called by KineticaRT Studio.</remarks>
		/// <param name="Text">Text to set.</param>
		public void SetProgress(string Text)
		{
			lblProgress.Text = Text ;
			this.Refresh() ;
		}

        /// <summary>
        /// Shows the Window.
        /// </summary>
        public new void Show()
        {
            base.Show();
        }

        /// <summary>
        /// Hides the window.
        /// </summary>
        public new void Hide()
        {
            base.Hide();
        }
    }
}

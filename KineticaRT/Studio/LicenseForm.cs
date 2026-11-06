using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace StudioDemonstration
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class licenseForm : System.Windows.Forms.Form
    {
		private KineticaRT.Controls.Button button1;
		private KineticaRT.Controls.Indicator indicator1;
		private KineticaRT.Controls.Slider slider1;
		private KineticaRT.Controls.Slider slider2;
		private KineticaRT.Studio.Controls.Linear linear1;
		private KineticaRT.Controls.Slider slider3;
		private KineticaRT.Controls.Frame frame1;
		private KineticaRT.Controls.Display display1;
        private KineticaRT.Controls.Dial dial1;
        private KineticaRT.Controls.DialMultiNeedle dialMultiNeedle1;
        private KineticaRT.Controls.Display display2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public licenseForm()
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
            KineticaRT.Controls.Scale scale1 = new KineticaRT.Controls.Scale();
            KineticaRT.Controls.Scale scale2 = new KineticaRT.Controls.Scale();
            this.button1 = new KineticaRT.Controls.Button();
            this.indicator1 = new KineticaRT.Controls.Indicator();
            this.slider1 = new KineticaRT.Controls.Slider();
            this.slider2 = new KineticaRT.Controls.Slider();
            this.linear1 = new KineticaRT.Studio.Controls.Linear();
            this.slider3 = new KineticaRT.Controls.Slider();
            this.frame1 = new KineticaRT.Controls.Frame();
            this.display1 = new KineticaRT.Controls.Display();
            this.dial1 = new KineticaRT.Controls.Dial();
            this.dialMultiNeedle1 = new KineticaRT.Controls.DialMultiNeedle();
            this.display2 = new KineticaRT.Controls.Display();
            ((System.ComponentModel.ISupportInitialize)(this.frame1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BorderType = KineticaRT.Controls.BorderType.None;
            this.button1.Location = new System.Drawing.Point(8, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 40);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            // 
            // indicator1
            // 
            this.indicator1.Location = new System.Drawing.Point(111, 8);
            this.indicator1.Name = "indicator1";
            this.indicator1.Size = new System.Drawing.Size(40, 40);
            this.indicator1.TabIndex = 4;
            this.indicator1.Text = "indicator1";
            // 
            // slider1
            // 
            this.slider1.Location = new System.Drawing.Point(264, 8);
            this.slider1.Name = "slider1";
            this.slider1.Size = new System.Drawing.Size(96, 200);
            this.slider1.TabIndex = 5;
            // 
            // slider2
            // 
            this.slider2.Location = new System.Drawing.Point(376, 8);
            this.slider2.Name = "slider2";
            this.slider2.Size = new System.Drawing.Size(72, 192);
            this.slider2.TabIndex = 6;
            // 
            // linear1
            // 
            // this.linear1.AllowDrop = true; SB removed because it is overriding the user values
            this.linear1.Location = new System.Drawing.Point(544, 32);
            this.linear1.Name = "linear1";
            this.linear1.NeedleWidth = 4;
            this.linear1.Size = new System.Drawing.Size(96, 192);
            this.linear1.TabIndex = 7;
            this.linear1.ValueFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // slider3
            // 
            this.slider3.Location = new System.Drawing.Point(448, 32);
            this.slider3.Name = "slider3";
            this.slider3.Size = new System.Drawing.Size(96, 200);
            this.slider3.TabIndex = 9;
            // 
            // frame1
            // 
            this.frame1.CornerRadius = 5;
            this.frame1.ForeColor = System.Drawing.Color.SteelBlue;
            this.frame1.HorizontalGrid = new KineticaRT.Controls.Grid(true, 8, 2);
            this.frame1.Location = new System.Drawing.Point(0, 0);
            this.frame1.Name = "frame1";
            // 
            // 
            // 
            this.frame1.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frame1.Panel.Location = new System.Drawing.Point(2, 2);
            this.frame1.Panel.Name = "PanelBase";
            this.frame1.Panel.Size = new System.Drawing.Size(268, 164);
            this.frame1.Panel.TabIndex = 0;
            this.frame1.Size = new System.Drawing.Size(272, 168);
            this.frame1.TabIndex = 0;
            this.frame1.VerticalGrid = new KineticaRT.Controls.Grid(true, 8, 2);
            // 
            // display1
            // 
            this.display1.Location = new System.Drawing.Point(24, 216);
            this.display1.Name = "display1";
            this.display1.Size = new System.Drawing.Size(150, 24);
            this.display1.TabIndex = 11;
            this.display1.Text = "display1";
            this.display1.Load += new System.EventHandler(this.display1_Load);
            // 
            // dial1
            // 
            this.dial1.Location = new System.Drawing.Point(24, 56);
            this.dial1.Name = "dial1";
            this.dial1.Scale = scale1;
            this.dial1.Size = new System.Drawing.Size(150, 150);
            this.dial1.TabIndex = 12;
            // 
            // dialMultiNeedle1
            // 
            this.dialMultiNeedle1.Location = new System.Drawing.Point(191, 56);
            this.dialMultiNeedle1.Name = "dialMultiNeedle1";
            this.dialMultiNeedle1.Scale = scale2;
            this.dialMultiNeedle1.Size = new System.Drawing.Size(150, 150);
            this.dialMultiNeedle1.TabIndex = 13;
            // 
            // display2
            // 
            this.display2.Location = new System.Drawing.Point(247, 230);
            this.display2.Name = "display2";
            this.display2.Size = new System.Drawing.Size(150, 24);
            this.display2.TabIndex = 14;
            this.display2.Text = "display2";
            // 
            // licenseForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(808, 266);
            this.Controls.Add(this.display2);
            this.Controls.Add(this.dialMultiNeedle1);
            this.Controls.Add(this.dial1);
            this.Controls.Add(this.display1);
            this.Controls.Add(this.slider3);
            this.Controls.Add(this.linear1);
            this.Controls.Add(this.slider2);
            this.Controls.Add(this.slider1);
            this.Controls.Add(this.indicator1);
            this.Controls.Add(this.button1);
            this.Name = "licenseForm";
            this.Text = "LicenseForm";
            this.Load += new System.EventHandler(this.licenseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.frame1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void licenseForm_Load(object sender, System.EventArgs e)
		{
		
		}

		private void display1_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}

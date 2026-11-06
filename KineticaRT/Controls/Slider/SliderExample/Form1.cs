using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace SliderExample
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private KineticaRT.Controls.Slider slider1;
		private KineticaRT.Controls.Slider slider2;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.slider1.Needle.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.slider1_PropertyChanged);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			KineticaRT.Controls.SliderNeedle sliderNeedle1 = new KineticaRT.Controls.SliderNeedle();
			KineticaRT.Controls.Threshold threshold1 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold2 = new KineticaRT.Controls.Threshold();
			this.slider1 = new KineticaRT.Controls.Slider();
			this.slider2 = new KineticaRT.Controls.Slider();
			this.SuspendLayout();
			// 
			// slider1
			// 
			this.slider1.Location = new System.Drawing.Point(8, 8);
			this.slider1.Name = "slider1";
			this.slider1.Scale.MajorMarksRepeat = 50;
			this.slider1.Scale.MajorMarksStart = -100;
			this.slider1.Scale.Minimum = -100;
			this.slider1.Scale.MinorMarksDivisions = 5;
			this.slider1.Size = new System.Drawing.Size(88, 288);
			this.slider1.TabIndex = 0;
			// 
			// slider2
			// 
			this.slider2.Location = new System.Drawing.Point(104, 8);
			this.slider2.Name = "slider2";
			sliderNeedle1.Color = System.Drawing.Color.Black;
			sliderNeedle1.FillColor = System.Drawing.Color.MediumBlue;
			sliderNeedle1.FillEnabled = true;
			sliderNeedle1.FillFinish = 0.8;
			sliderNeedle1.FillStart = 0.7;
			sliderNeedle1.FillStyle = KineticaRT.Controls.FillStyles.Segmented;
			sliderNeedle1.Style = KineticaRT.Controls.SliderNeedleStyles.Arrow;
			this.slider2.Needle = sliderNeedle1;
			this.slider2.Scale.MajorMarksRepeat = 25;
			this.slider2.Scale.MajorMarksStart = -100;
			this.slider2.Scale.Minimum = -100;
			this.slider2.Scale.MinorMarksDivisions = 5;
			threshold1.MarkColor = System.Drawing.Color.DarkSeaGreen;
			threshold1.MarkSize = 0.06;
			threshold1.Value = -80;
			threshold2.MarkSize = 0.06;
			threshold2.Value = 80;
			this.slider2.Scale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																threshold1,
																																threshold2});
			this.slider2.Size = new System.Drawing.Size(80, 288);
			this.slider2.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(192, 302);
			this.Controls.Add(this.slider2);
			this.Controls.Add(this.slider1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void slider1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Value")
			{
				slider2.Needle.Value = slider1.Needle.Value ;
			}
		}
	}
}

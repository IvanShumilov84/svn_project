using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace KineticaRT.Controls.InvisibleButton
{
    public class InvisibleButton: System.Windows.Forms.UserControl 
    {
        private Label label1;
    
        public InvisibleButton()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // InvisibleButton
            // 
            this.BackColor = System.Drawing.Color.LightSalmon;
            this.Controls.Add(this.label1);
            this.Name = "InvisibleButton";
            this.Size = new System.Drawing.Size(122, 48);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InvisibleButton_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InvisibleButton_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("MouseClick", Name);
        }
    }
}

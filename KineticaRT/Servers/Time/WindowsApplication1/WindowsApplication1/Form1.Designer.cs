namespace WindowsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            KineticaRT.Controls.XYStaticTrace xyStaticTrace2 = new KineticaRT.Controls.XYStaticTrace();
            this.xyGraph1 = new KineticaRT.Controls.XYGraph();
            this.SuspendLayout();
            // 
            // xyGraph1
            // 
            this.xyGraph1.Location = new System.Drawing.Point(43, 18);
            this.xyGraph1.Name = "xyGraph1";
            this.xyGraph1.Size = new System.Drawing.Size(237, 213);
            xyStaticTrace2.Color = System.Drawing.Color.Black;
            this.xyGraph1.StaticTraces = new KineticaRT.Controls.XYStaticTraceCollection(new KineticaRT.Controls.XYStaticTrace[] {
            xyStaticTrace2});
            this.xyGraph1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.xyGraph1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private KineticaRT.Controls.XYGraph xyGraph1;
    }
}


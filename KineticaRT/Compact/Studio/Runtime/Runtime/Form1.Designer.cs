namespace Runtime
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.Drawing.Graphics gr = this.CreateGraphics();
            //this.Font = new System.Drawing.Font("Tahoma", 5, System.Drawing.FontStyle.Regular);
            this.Font = new System.Drawing.Font("Tahoma", ((12 * 72) / gr.DpiX)+0.9F, System.Drawing.FontStyle.Regular);
            gr.Dispose();
            
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.connectTimer = new System.Windows.Forms.Timer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            //this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();


            this.tabControl1.Font = this.Font;

            
            // 
            // connectTimer
            // 
            this.connectTimer.Tick += new System.EventHandler(this.connectTimer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            //this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(255, 325);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(247, 296);
            this.tabPage1.Text = "Home";
            //// 
            //// tabPage2
            //// 
            //this.tabPage2.Location = new System.Drawing.Point(4, 25);
            //this.tabPage2.Name = "tabPage2";
            //this.tabPage2.Size = new System.Drawing.Size(247, 296);
            //this.tabPage2.Text = "tabPage2";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(255, 325);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.SteelBlue;
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer connectTimer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        //private System.Windows.Forms.TabPage tabPage2;
    }
}


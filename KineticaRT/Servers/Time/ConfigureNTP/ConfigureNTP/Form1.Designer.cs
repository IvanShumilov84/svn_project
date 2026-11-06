namespace ConfigureNTP
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
            this.configureServerButton = new System.Windows.Forms.Button();
            this.configureClientButton = new System.Windows.Forms.Button();
            this.serverNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.reSyncPeriodNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.reSyncPeriodNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // configureServerButton
            // 
            this.configureServerButton.Location = new System.Drawing.Point(456, 31);
            this.configureServerButton.Name = "configureServerButton";
            this.configureServerButton.Size = new System.Drawing.Size(106, 23);
            this.configureServerButton.TabIndex = 0;
            this.configureServerButton.Text = "Configure Server";
            this.configureServerButton.UseVisualStyleBackColor = true;
            this.configureServerButton.Visible = false;
            // 
            // configureClientButton
            // 
            this.configureClientButton.Location = new System.Drawing.Point(456, 5);
            this.configureClientButton.Name = "configureClientButton";
            this.configureClientButton.Size = new System.Drawing.Size(106, 23);
            this.configureClientButton.TabIndex = 1;
            this.configureClientButton.Text = "Configure Client";
            this.configureClientButton.UseVisualStyleBackColor = true;
            this.configureClientButton.Click += new System.EventHandler(this.configureClientButton_Click);
            // 
            // serverNameTextBox
            // 
            this.serverNameTextBox.Location = new System.Drawing.Point(99, 6);
            this.serverNameTextBox.Name = "serverNameTextBox";
            this.serverNameTextBox.Size = new System.Drawing.Size(350, 20);
            this.serverNameTextBox.TabIndex = 2;
            this.serverNameTextBox.Text = "ntp1.npl.co.uk";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server Name:";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(456, 91);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(106, 23);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "ReSync Period:";
            // 
            // reSyncPeriodNumericUpDown
            // 
            this.reSyncPeriodNumericUpDown.Location = new System.Drawing.Point(99, 36);
            this.reSyncPeriodNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.reSyncPeriodNumericUpDown.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.reSyncPeriodNumericUpDown.Name = "reSyncPeriodNumericUpDown";
            this.reSyncPeriodNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.reSyncPeriodNumericUpDown.TabIndex = 7;
            this.reSyncPeriodNumericUpDown.Value = new decimal(new int[] {
            900,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(225, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Seconds";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 126);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.reSyncPeriodNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverNameTextBox);
            this.Controls.Add(this.configureClientButton);
            this.Controls.Add(this.configureServerButton);
            this.Name = "Form1";
            this.Text = "Configure NTP Time";
            ((System.ComponentModel.ISupportInitialize)(this.reSyncPeriodNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button configureServerButton;
        private System.Windows.Forms.Button configureClientButton;
        private System.Windows.Forms.TextBox serverNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown reSyncPeriodNumericUpDown;
        private System.Windows.Forms.Label label3;
    }
}


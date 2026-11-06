using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KineticaRT.Compact.Studio.Application
{
	/// <summary>
	/// Summary description for EditWindowProperties.
	/// </summary>
	public class EditWindowProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.CheckBox showInTaskbarCheckBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox showControlsCheckBox;
		private System.Windows.Forms.CheckBox disableCloseCheckBox;
        private TextBox iconFileTextBox;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private Button button1;

        private string initialDirectory;

        
        
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor for class.
		/// </summary>
		public EditWindowProperties()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

            initialDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
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
            this.showControlsCheckBox = new System.Windows.Forms.CheckBox();
            this.showInTaskbarCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.disableCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.iconFileTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // showControlsCheckBox
            // 
            this.showControlsCheckBox.Location = new System.Drawing.Point(16, 40);
            this.showControlsCheckBox.Name = "showControlsCheckBox";
            this.showControlsCheckBox.Size = new System.Drawing.Size(144, 24);
            this.showControlsCheckBox.TabIndex = 1;
            this.showControlsCheckBox.Text = "Show Controls";
            // 
            // showInTaskbarCheckBox
            // 
            this.showInTaskbarCheckBox.Location = new System.Drawing.Point(16, 16);
            this.showInTaskbarCheckBox.Name = "showInTaskbarCheckBox";
            this.showInTaskbarCheckBox.Size = new System.Drawing.Size(144, 24);
            this.showInTaskbarCheckBox.TabIndex = 2;
            this.showInTaskbarCheckBox.Text = "Show In Taskbar";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(355, 16);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(64, 20);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(355, 40);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(64, 20);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "&Cancel";
            // 
            // disableCloseCheckBox
            // 
            this.disableCloseCheckBox.Location = new System.Drawing.Point(16, 64);
            this.disableCloseCheckBox.Name = "disableCloseCheckBox";
            this.disableCloseCheckBox.Size = new System.Drawing.Size(144, 24);
            this.disableCloseCheckBox.TabIndex = 6;
            this.disableCloseCheckBox.Text = "Disable Close Button";
            // 
            // iconFileTextBox
            // 
            this.iconFileTextBox.Location = new System.Drawing.Point(69, 94);
            this.iconFileTextBox.Name = "iconFileTextBox";
            this.iconFileTextBox.Size = new System.Drawing.Size(191, 20);
            this.iconFileTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Icon File:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EditWindowProperties
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(431, 137);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iconFileTextBox);
            this.Controls.Add(this.disableCloseCheckBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.showInTaskbarCheckBox);
            this.Controls.Add(this.showControlsCheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditWindowProperties";
            this.Text = "Edit Window Properties";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void okButton_Click(object sender, System.EventArgs e)
		{
			windowInfo.ShowInTaskbar = showInTaskbarCheckBox.Checked ;
			windowInfo.ShowControls  = this.showControlsCheckBox.Checked ;
			windowInfo.DisableClose = this.disableCloseCheckBox.Checked ;
            windowInfo.IconFile = this.iconFileTextBox.Text;

			this.DialogResult = DialogResult.OK ;
		}

		/// <summary>
		/// Gets or sets the Window to be edited
		/// </summary>
		public Project.WindowInfo WindowInfo
		{
			get
			{
				return windowInfo ;
			}
			set
			{
				windowInfo = value ;
				showInTaskbarCheckBox.Checked = windowInfo.ShowInTaskbar ;
				showControlsCheckBox.Checked = windowInfo.ShowControls ;
				disableCloseCheckBox.Checked = windowInfo.DisableClose ;
                SetTextBoxFileName(iconFileTextBox, windowInfo.IconFile);
			}
		}
		private Project.WindowInfo windowInfo ;

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = initialDirectory ;
            openFileDialog1.FileName = iconFileTextBox.Text ;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SetTextBoxFileName(iconFileTextBox, openFileDialog1.FileName);
            }
        }
    
        private void SetTextBoxFileName(TextBox textBox, string fileName)
        {
            try
            {
                if (System.IO.Path.GetDirectoryName(fileName) == initialDirectory)
                {
                    iconFileTextBox.Text = System.IO.Path.GetFileName(fileName);
                }
                else
                {
                    iconFileTextBox.Text = fileName;
                }
            }
            catch (System.ArgumentException)
            {
                //If fileName is not set (== "").
                iconFileTextBox.Text = "";
            }
        }

    }
}

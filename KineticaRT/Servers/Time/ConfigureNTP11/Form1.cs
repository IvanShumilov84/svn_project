using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;
using System.Security.Permissions;

namespace ConfigureNTP11
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown reSyncPeriodNumericUpDown;
		private System.Windows.Forms.TextBox serverNameTextBox;
		private System.Windows.Forms.Button configureClientButton;
		private System.Windows.Forms.Button exitButton;
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.reSyncPeriodNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.serverNameTextBox = new System.Windows.Forms.TextBox();
			this.configureClientButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.reSyncPeriodNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server Name:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "ReSync Period:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(224, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Seconds";
			// 
			// reSyncPeriodNumericUpDown
			// 
			this.reSyncPeriodNumericUpDown.Location = new System.Drawing.Point(96, 32);
			this.reSyncPeriodNumericUpDown.Maximum = new System.Decimal(new int[] {
																					  1000000,
																					  0,
																					  0,
																					  0});
			this.reSyncPeriodNumericUpDown.Name = "reSyncPeriodNumericUpDown";
			this.reSyncPeriodNumericUpDown.TabIndex = 3;
			this.reSyncPeriodNumericUpDown.Value = new System.Decimal(new int[] {
																					900,
																					0,
																					0,
																					0});
			// 
			// serverNameTextBox
			// 
			this.serverNameTextBox.Location = new System.Drawing.Point(96, 8);
			this.serverNameTextBox.Name = "serverNameTextBox";
			this.serverNameTextBox.Size = new System.Drawing.Size(408, 20);
			this.serverNameTextBox.TabIndex = 4;
			this.serverNameTextBox.Text = "ntp1.npl.co.uk";
			// 
			// configureClientButton
			// 
			this.configureClientButton.Location = new System.Drawing.Point(512, 8);
			this.configureClientButton.Name = "configureClientButton";
			this.configureClientButton.Size = new System.Drawing.Size(104, 23);
			this.configureClientButton.TabIndex = 5;
			this.configureClientButton.Text = "Configure Client";
			this.configureClientButton.Click += new System.EventHandler(this.configureClientButton_Click);
			// 
			// exitButton
			// 
			this.exitButton.Location = new System.Drawing.Point(512, 64);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(104, 23);
			this.exitButton.TabIndex = 6;
			this.exitButton.Text = "Exit";
			this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(624, 94);
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.configureClientButton);
			this.Controls.Add(this.serverNameTextBox);
			this.Controls.Add(this.reSyncPeriodNumericUpDown);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.reSyncPeriodNumericUpDown)).EndInit();
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

		private void exitButton_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void RegistrySetValue (string path, string key, object value)
		{
			RegistryKey target = Registry.LocalMachine.CreateSubKey(path);
			target.SetValue(key, value);

		}

		private void configureClientButton_Click(object sender, System.EventArgs e)
		{
			string path = "SYSTEM\\CurrentControlSet\\Services\\W32Time\\";
			RegistrySetValue(path+"Parameters", "Type", "NTP");
			RegistrySetValue(path + "Config", "AnnounceFlags", "5");
			RegistrySetValue(path + "TimeProviders\\NTPClient", "SpecialPollInterval", reSyncPeriodNumericUpDown.Value);
			RegistrySetValue(path + "TimeProviders\\NTPServer", "Enabled", 1);
			RegistrySetValue(path + "Parameters", "NtpServer", serverNameTextBox.Text);
		}
	}
}

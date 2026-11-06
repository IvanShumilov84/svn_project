using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ConfigureNTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void configureClientButton_Click(object sender, EventArgs e)
        {
            string path = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\W32Time\\";
            Registry.SetValue(path+"Parameters", "Type", "NTP");
            Registry.SetValue(path + "Config", "AnnounceFlags", "5");
            Registry.SetValue(path + "TimeProviders\\NTPClient", "SpecialPollInterval", reSyncPeriodNumericUpDown.Value);
            Registry.SetValue(path + "TimeProviders\\NTPServer", "Enabled", 1);
            Registry.SetValue(path + "Parameters", "NtpServer", serverNameTextBox.Text);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
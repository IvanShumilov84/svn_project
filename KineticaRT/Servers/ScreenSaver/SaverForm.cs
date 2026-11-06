using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KineticaRT.Servers.ScreenSaver
{
    [ChangeRecord("1.0", "09 03 2007", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT SubProject")]
    [Copyright("Copyright © KineticaRT Ltd 2007")]

    internal partial class SaverForm : Form
    {
        public SaverForm()
        {
            InitializeComponent();
        }

        private void SaverForm_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
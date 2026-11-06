using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int xStep = 10;
            int yStep = 1;

            pictureBox1.Location = new Point(pictureBox1.Location.X + xStep, pictureBox1.Location.Y + yStep);
            if (pictureBox1.Location.X + pictureBox1.Width > Width)
            {
                pictureBox1.Location = new Point(0, pictureBox1.Location.Y);
            }

            if (pictureBox1.Location.Y + pictureBox1.Height > Height)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X, 0);
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
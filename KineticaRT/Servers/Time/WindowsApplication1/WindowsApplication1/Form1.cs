using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xyGraph1.LeftScale.Maximum = 4;
            xyGraph1.LeftScale.Minimum = -4;
            xyGraph1.BottomScale.Maximum = 3;
            xyGraph1.BottomScale.Minimum = -3;
            xyGraph1.RightScale.Enabled = false;
            xyGraph1.TopScale.Enabled = false;
            PointF[] points = new PointF[2];
            points[0] = new Point(-1, -2);
            points[1] = new Point(1, 2);
            xyGraph1.StaticTraces[0].SetDataPoints(points);
        }
    }
}
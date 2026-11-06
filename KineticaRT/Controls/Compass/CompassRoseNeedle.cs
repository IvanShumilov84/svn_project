using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using KineticaRT;
using KineticaRT.Controls;


namespace KineticaRT.Controls.Compass
{
    /// <summary>
    /// Customised DialNeedle designed to be used on a compass to display a sixteen segment wind rose.
    /// </summary>
    [ChangeRecord("2.0.1.1", "09 05 2008", "N. Savidge", "Removed spiders web.")]
    [ChangeRecord("1.0", "30 06 2006", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
    [Copyright("Copyright © KineticaRT Ltd 2006-2008")]

    public class CompassRoseNeedle : KineticaRT.Controls.DialNeedle 
    {
        private Point[] shadedPoints;

        double[] bucket;

        /// <summary>
        /// Constructor for class
        /// </summary>
        public CompassRoseNeedle()
        {
            shadedPoints = new Point[3];
            this.length = 0.95;
            this.width = 0.07 ; 
        }


        /// <summary>
        /// Recalculates the position of the Needle
        /// </summary>
        /// <param name="DrawRect">Rectangle in which to position the Needle</param>
        /// <param name="objScale">Scale object related to the Control</param>
        /// <returns>True if Needle has changed angle since last redraw</returns>
        public override bool CalculatePosition(Rectangle DrawRect, KineticaRT.Controls.Scale objScale)
        {
            return true;
        }


        /// <summary>
        /// Gets or Sets the interval between grid lines.
        /// </summary>
        [Category("Needle")]
        [Description("Gets or Sets the interval between grid lines.")]
        [DefaultValue(10.0)]
        public double GridInterval
        {
            get
            {
                return grid_interval;
            }
            set
            {
                if (grid_interval != value)
                {
                    grid_interval = value;
                    OnPropertyChanged("GridInterval");
                }
            }
        }
        double grid_interval = 10;

        /// <summary>
        /// Determines if the scaling auto adjusts to the maximum value.
        /// </summary>
        [Category("Needle")]
        [Description("Determines if the scaling auto adjusts to the maximum value.")]
        [DefaultValue(true)]
        public bool AutoRange
        {
            get
            {
                return autoRange;
            }
            set
            {
                if (autoRange != value)
                {
                    autoRange = value;
                    OnPropertyChanged("AutoRange");
                }
            }
        }
        bool autoRange = true;

        /// <summary>
        /// Gets or Sets the current Value
        /// </summary>
        [Category("Needle")]
        [Description("Gets or Sets the current value")]
        public override object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;

                    KineticaRT.Values.DoubleArray double_array = value as KineticaRT.Values.DoubleArray;
                    if (double_array != null)
                    {
                        bucket = double_array.ToDoubleArray();
                        if (bucket.Length != 16)
                        {
                            bucket = null;
                        }
                    }
                    else
                    {
                        bucket = null;
                    }

                    OnPropertyChanged("Value");
                }
            }
        }
        private bool ShouldSerializeValue()
        {
            return false;
        }



        /// <summary>
        /// Draw the needle
        /// </summary>
        /// <param name="g">Graphics object with which to draw</param>
        /// <param name="DrawRect">Rectangle in which to draw</param>
        public override void Draw(Graphics g, Rectangle DrawRect)
        {
            if (g == null) throw new ArgumentNullException("g");

            base.SetUpScaling(DrawRect);

            if (visible && bucket != null)
            {
                double minimum;
                double maximum;

                if (autoRange)
                {
                    minimum = 0;
                    maximum = 0;
                    for (int index = 0; index < bucket.Length; index++)
                    {
                        if (bucket[index] > maximum)
                        {
                            maximum = bucket[index];
                        }
                    }
                }
                else
                {
                    minimum = this.minimum;
                    maximum = this.maximum;
                }

                if (maximum > minimum)
                {
                    SolidBrush brush;
                    brush = new SolidBrush(fillColor);
                    Pen pen = new Pen(color);
                    //Point pt1 = new Point();
                    //Point pt2 = new Point();

                    for (int index = 0; index < 16; index++)
                    {

                        // Draw segment (bar).
                        double rad_angle = 22.5 * index * Math.PI / 180;	//radians
                        double length = (bucket[index] - minimum) / (maximum - minimum) * this.length;
                        if (length <= 0.0) length = 0.0;

                        shadedPoints[0].X = ScaleX(0.5 + Math.Sin(rad_angle) * 0 * 0.5);// * width);
                        shadedPoints[0].Y = ScaleY(0.5 - Math.Cos(rad_angle) * 0 * 0.5);// * width);

                        shadedPoints[1].X = ScaleX(0.5 + Math.Sin(rad_angle - 11.25 * (Math.PI / 180)) * length * 0.99 * 0.5);
                        shadedPoints[1].Y = ScaleY(0.5 - Math.Cos(rad_angle - 11.25 * (Math.PI / 180)) * length * 0.99 * 0.5);

                        shadedPoints[2].X = ScaleX(0.5 + Math.Sin(rad_angle + 11.25 * (Math.PI / 180)) * length * 0.99 * 0.5);
                        shadedPoints[2].Y = ScaleY(0.5 - Math.Cos(rad_angle + 11.25 * (Math.PI / 180)) * length * 0.99 * 0.5);

                        if (fillEnabled)
                        {
                            g.FillPolygon(brush, shadedPoints);
                        }
                        g.DrawPolygon(pen, shadedPoints);

                        // NS 09 May 2008 - Needs auto grid interval otherwise uses too much processor if WindRun left to build up.
                        //// Grid lines (Spider web)
                        //for (double grid_length = minimum + grid_interval; grid_length < maximum && grid_length < bucket[index]; grid_length += grid_interval)
                        //{
                        //    length = (grid_length - minimum) / (maximum - minimum) * this.length;

                        //    pt1.X = ScaleX(0.5 + Math.Sin(rad_angle - 11.25 * (Math.PI / 180)) * length * 1 * 0.5);
                        //    pt1.Y = ScaleY(0.5 - Math.Cos(rad_angle - 11.25 * (Math.PI / 180)) * length * 1 * 0.5);

                        //    pt2.X = ScaleX(0.5 + Math.Sin(rad_angle + 11.25 * (Math.PI / 180)) * length * 1 * 0.5);
                        //    pt2.Y = ScaleY(0.5 - Math.Cos(rad_angle + 11.25 * (Math.PI / 180)) * length * 1 * 0.5);

                        //    g.DrawLine(pen, pt1, pt2);
                        //}
                    }
                    brush.Dispose();
                }
            }

            //done the drawing now, so update 'last_angle'
            lastAngle = currentAngle;
        }
    }
}

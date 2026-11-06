using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace KineticaRT.Controls.Compass
{
    /// <summary>
    /// Customised DialNeedle designed to be used on a compass for indicating direction.
    /// </summary>
    /// <remarks>Includes a segment indicating which section of the sixteen point compass the needle is located in.</remarks>
    [ChangeRecord("1.0", "30 06 2006", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
    [Copyright("Copyright © KineticaRT Ltd 2006")]

    public class CompassSegmentNeedle : KineticaRT.Controls.DialNeedle 
    {
        private Point[] shadedPoints;
        double segment_start;
        double segment_finish;
        Color segment_color;

        /// <summary>
        /// Constructor for class.
        /// </summary>
        public CompassSegmentNeedle()
        {
            shadedPoints = new Point[4];
            this.length = 0.95;
            this.width = 0.07 ; 
            segment_start = 0.90;
            segment_finish = 0.95;
            segment_color = Color.DarkRed;
        }

        /// <summary>
        /// Gets or sets the color of the 16 point compass seqment.
        /// </summary>
        [Category("Compass")]
        [Description("Gets or sets the color of the 16 point compass seqment")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.None)]
        //[DefaultValue(Color.DarkRed)]
        public Color SegmentColor
        {
            get
            {
                return segment_color;
            }
            set
            {
                if (segment_color != value)
                {
                    segment_color = value;
                    //AddToQueue(true);
                    OnPropertyChanged("SegmentColor");
                }
            }
        }
        private bool ShouldSerializeSegmentColor()
        {
            return color != Color.DarkRed;
        }
        /// <summary>
        /// Reset the SegmentColor to its default Value
        /// </summary>
        public void ResetSegmentColor()
        {
            color = Color.DarkRed;
        }

        /// <summary>
        /// Draw the custom needle.
        /// </summary>
        /// <param name="g">Graphics object with which to draw</param>
        /// <param name="DrawRect">Rectangle in which to draw</param>
        public override void Draw(Graphics g, Rectangle DrawRect)
        {
            SetUpScaling(DrawRect);

            // Draw the needle (if its visible).
            if (visible && krtValue is KineticaRT.Values.Double && !System.Double.IsNaN(krtValue.ToDouble()))
            {
                // Segment part.
                {
                    double current = currentAngle;
                    current = current - ((current + 11.25) % 22.5) + 11.25;
                    double rad_angle_start = (current - 11.25) * Math.PI / 180;	//radians
                    double rad_angle_stop = (current + 11.25) * Math.PI / 180;	//radians

                    segment_finish = length;
                    segment_start = segment_finish - 0.05;
                    
                    double length_start = segment_start ;
                    double length_stop = segment_finish;

                    shadedPoints[0].X = ScaleX(0.5 + Math.Sin(rad_angle_start) * 0.5 * length_stop);
                    shadedPoints[0].Y = ScaleY(0.5 - Math.Cos(rad_angle_start) * 0.5 * length_stop);

                    shadedPoints[1].X = ScaleX(0.5 + Math.Sin(rad_angle_start) * length_start * 0.5);
                    shadedPoints[1].Y = ScaleY(0.5 - Math.Cos(rad_angle_start) * length_start * 0.5);

                    shadedPoints[2].X = ScaleX(0.5 + Math.Sin(rad_angle_stop) * length_start * 0.5);
                    shadedPoints[2].Y = ScaleY(0.5 - Math.Cos(rad_angle_stop) * length_start * 0.5);

                    shadedPoints[3].X = ScaleX(0.5 + Math.Sin(rad_angle_stop) * 0.5 * length_stop);
                    shadedPoints[3].Y = ScaleY(0.5 - Math.Cos(rad_angle_stop) * 0.5 * length_stop);

                    {
                        Brush brush = new System.Drawing.SolidBrush (segment_color);
                        Rectangle rectangle = new Rectangle(ScaleX(0.5 - 0.5 * length_stop), ScaleY(0.5 - 0.5 * length_stop), ScaleX(length_stop) - 1, ScaleY(length_stop) - 1);

                        Rectangle inner_rectangle = rectangle;
                        GraphicsPath path;
                        System.Drawing.Region region;

                        Point[] pts = new Point[3];
                        System.Byte[] types = new Byte[3];
                        pts[0].X = ScaleX(0.5 + Math.Sin(rad_angle_start) * 0.5 * length_stop);
                        pts[0].Y = ScaleY(0.5 - Math.Cos(rad_angle_start) * 0.5 * length_stop);
                        types[0] = (byte)PathPointType.Start;
                        pts[1].X = ScaleX(0.5 + Math.Sin(rad_angle_stop) * length_stop);
                        pts[1].Y = ScaleY(0.5 - Math.Cos(rad_angle_stop) * length_stop);
                        types[1] = (byte)PathPointType.Line;
                        pts[2].X = ScaleX(0.5);
                        pts[2].Y = ScaleY(0.5);
                        types[2] = (byte)PathPointType.Line;

                        path = new GraphicsPath(pts, types);
                        region = new Region(path);
                        g.SetClip(region, CombineMode.Replace);
                        path = new GraphicsPath();
                        inner_rectangle.Inflate(-(int)ScaleX(segment_start), -(int)ScaleY(segment_start));
                        path.AddEllipse(inner_rectangle);
                        region = new Region(path);
                        g.SetClip(region, CombineMode.Exclude);
                        g.FillEllipse(brush, rectangle);
                        g.ResetClip () ;
                    }
                }

                // Needle part.
                {
                    double rad_angle = currentAngle * Math.PI / 180;	//radians

                    // Back.
                    shadedPoints[0].X = ScaleX(0.5 + Math.Sin(rad_angle) * width * 0.5);
                    shadedPoints[0].Y = ScaleY(0.5 - Math.Cos(rad_angle) * width * 0.5);

                    // Left Edge.
                    shadedPoints[1].X = ScaleX(0.5 + Math.Sin(rad_angle - (Math.PI / 2)) * width * 0.5);
                    shadedPoints[1].Y = ScaleY(0.5 - Math.Cos(rad_angle - (Math.PI / 2)) * width * 0.5);

                    // Tip.
                    shadedPoints[2].X = ScaleX(0.5 + Math.Sin(rad_angle) * 0.5 * length);
                    shadedPoints[2].Y = ScaleY(0.5 - Math.Cos(rad_angle) * 0.5 * length);

                    // Right Edge.
                    shadedPoints[3].X = ScaleX(0.5 + Math.Sin(rad_angle + (Math.PI / 2)) * width * 0.5);
                    shadedPoints[3].Y = ScaleY(0.5 - Math.Cos(rad_angle + (Math.PI / 2)) * width * 0.5);

                    if (shadedPoints[0].Equals(shadedPoints[2]))
                    {
                        //don't bother to draw: end and tip are the same point
                    }
                    else
                    {
                        Color top_clr;
                        Color bottom_clr;
                        //PathGradientBrush brush;
                        GraphicsPath path = new GraphicsPath();
                        //Color[] colors;

                        //CalculateSoftShading(out top_clr, out bottom_clr);
                        //bottom_clr = ControlPaint.Dark(color, (float)0.15);
                        //top_clr = ControlPaint.Light(color, (float)0.8);

                        top_clr = color;
                        bottom_clr = color;

                        //NB - this is not shaded according to angle atm
                        path.AddLines(shadedPoints);
                        //brush = new PathGradientBrush(path);
                        //colors = new Color[4];
                        //colors[0] = top_clr;
                        //colors[1] = color;
                        //colors[2] = bottom_clr;
                        //colors[3] = color;
                        //brush.SurroundColors = colors;

                        SolidBrush brush = new SolidBrush(fillColor);
                        Pen pen = new Pen(color);
                        g.FillPolygon(brush, shadedPoints);
                        g.DrawPolygon(pen, shadedPoints);

                        path.Dispose();
                        brush.Dispose();
                    }
                }
            }

            //done the drawing now, so update 'last_angle' drawn.
            lastAngle = currentAngle;
        }
    }
}



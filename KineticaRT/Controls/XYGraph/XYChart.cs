using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace KineticaRT.Controls
{
    /// <summary>
    /// Provides an XY Graph Control with a trace of type XYTrack.
    /// </summary>
    public class XYChart : KineticaRT.Controls.XYGraph 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public XYChart()
        {
            lock (this)
            {
                Traces[0] = new XYTrack();
                Traces[0].PersistTime = 60000;
            }
        }
    }

    /// <summary>
    /// XYTrace object designed for use on an XYChart.
    /// Displays the values over time of two source channels on an XY Plot, also allows the pen to be rotated based on a third parameter.
    /// </summary>
    public class XYTrack : KineticaRT.Controls.XYTrace
    {
        private float lastAngle ;
        private float currentAngle ;
        private Bitmap image;
        private Bitmap newImage;
        private Rectangle imageRectangle;
        private Rectangle inflatedImageRectangle;
        private Rectangle nextInflatedImageRectangle;
        private System.Drawing.Imaging.ImageAttributes attrs;
        private System.Drawing.Imaging.ColorMap[] map;

        /// <summary>
        /// Constructor.
        /// </summary>
        public XYTrack()
        {
            SetImage(new Bitmap(typeof(XYTrack), "Vessel.bmp"));
            attrs = new System.Drawing.Imaging.ImageAttributes();
            map = new System.Drawing.Imaging.ColorMap[1];
            map[0] = new System.Drawing.Imaging.ColorMap();
            map[0].OldColor = Color.Black;
        }

        /// <summary>
        /// Adds a new position to the track.
        /// </summary>
        /// <param name="X">Current X position.</param>
        /// <param name="Y">Current Y position.</param>
        /// <param name="angle">Current pen image angle.</param>
        public void AddPoint(double X, double Y, double angle)
        {
            lock (this)
            {
                this.XValue = X;
                this.YValue = Y;
                this.currentAngle = (float)angle;
            }
        }

        /// <summary>
        /// Sets the current image for the pen.
        /// </summary>
        /// <remarks>
        /// Parts of the image which are white are displayed as transparent.
        /// Parts of the image which are black are displayed in the color of the PenColor property.
        /// Other colors are not modified.
        /// </remarks>
        /// <param name="image">Bitmap to use for the image.</param>
        public void SetImage(Bitmap image)
        {
            lock (this)
            {
                newImage = image;
            }
        }

        /// <summary>
        /// Draws the pen image at the head of the trace.
        /// </summary>
        /// <param name="g">Graphics object to draw on.</param>
        /// <param name="style">Current PenStyle.</param>
        /// <param name="position">PointF specifying where to draw.</param>
        /// <param name="color">Current pen color.</param>
        /// <param name="erase">If true then the pen should be erased to color, not drawn.</param>
        protected override void DrawPen(System.Drawing.Graphics g, KineticaRT.Controls.PenStyles style, System.Drawing.PointF position, System.Drawing.Color color, bool erase)
        {
            float drawing_angle = erase ? lastAngle : currentAngle;

            g.RotateTransform(drawing_angle, MatrixOrder.Append);
            g.TranslateTransform(position.X, position.Y, MatrixOrder.Append);
            if (erase)
            {
                g.FillRectangle(new SolidBrush(color), inflatedImageRectangle);
            }
            else
            {
                if (newImage != null)
                {
                    image = newImage;
                    image.MakeTransparent(Color.White);
                    imageRectangle = new Rectangle(-image.Width / 2, -image.Height / 2, image.Width, image.Height);
                    nextInflatedImageRectangle = new Rectangle(imageRectangle.X - 2, imageRectangle.Y - 2, imageRectangle.Width + 4, imageRectangle.Height + 4);
                    newImage = null;
                }

                map[0].NewColor = color;
                attrs.SetRemapTable(map);
                g.DrawImage(image, imageRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attrs);
                lastAngle = drawing_angle;
                inflatedImageRectangle = nextInflatedImageRectangle;
            }
            g.TranslateTransform(-position.X, -position.Y, MatrixOrder.Append);
            g.RotateTransform(-drawing_angle, System.Drawing.Drawing2D.MatrixOrder.Append);
        }
    }
}

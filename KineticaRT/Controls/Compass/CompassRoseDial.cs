using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace KineticaRT.Controls.Compass
{
    /// <summary>
    /// Customised Dial designed to be used to show a wind rose.
    /// </summary>
    [ChangeRecord("1.0", "30 06 2006", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
    [Copyright("Copyright © KineticaRT Ltd 2006")]

    public class CompassRoseDial : KineticaRT.Controls.Dial
    {
        Image image;
        string imageFileName;

        /// <summary>
        /// Construct the object.
        /// </summary>
        public CompassRoseDial()
        {
            imageFileName = "";

            Resize += new EventHandler(MyDial_Resize);

            AllowDrop = true;

            Needle = new CompassRoseNeedle();
            Needle.Length = 0.95;
            scale.AutoAdjust = false;
            Scale.SweepAngle = 360; //Wont be drawing the scale but need the ZeroAngle etc. for the needle.
            scale.ZeroAngle = 0; // Top.
            scale.MajorMarksRepeat = 10;
            scale.LabelEvery = 3;
            //scale.LabelsText = compassPoints ;
            //scale.LabelNumeric = false;
            scale.Maximum = 360;
            scale.Position = 0.95;
        }
        //string[] compassPoints = { "N", "E", "S", "W" };

        /// <summary>
        /// Event handler for this dial resizing.
        /// </summary>
        private void MyDial_Resize(object sender, EventArgs e)
        {
            LoadImage();
        }

        /// <summary>
        /// Gets or sets the Filename for the background image.
        /// </summary>
        [Category("Compass")]
        [Description("Gets or sets the Filename for the background image")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [RefreshProperties(RefreshProperties.None)]
        [DefaultValue("")]
        [Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string ImageFileName
        {
            get
            {
                return imageFileName;
            }
            set
            {
                if (imageFileName != value)
                {
                    imageFileName = value;
                    LoadImage();
                    OnPropertyChanged("ImageFileName");
                }
            }
        }

        private void LoadImage()
        {
            try
            {
                // Bitmap is now wrong size - reload the background bitmap and resise to correct size.
                if (imageFileName.Length > 0)
                {
                    Bitmap new_bitmap = new System.Drawing.Bitmap(imageFileName);
                    image = new System.Drawing.Bitmap(new_bitmap, Width, Height);
                }
                else
                {
                    image = null;
                }
            }
            catch (System.ArgumentException)
            {
                image = null;
            }

            AddToQueue(true);
        }

        /// <summary>
        /// Custom Repaint function.
        /// </summary>
        /// <param name="FullRedraw">true indicates entire drawing area must be redrawn, false indicates time for an update.</param>
        /// <param name="g">The Graphics object to draw in.</param>
        public override void Repaint(bool FullRedraw, Graphics g)
        {
            if (g == null) throw new ArgumentNullException("g");

            CalculateDisplayRectangle();

            Rectangle rect = DisplayRectangle;

            //FullRedraw = true;

            if ((rect.Width > 10 && rect.Height > 10) && (needle.CalculatePosition(rect, scale) || FullRedraw || resized))
            {
                //needle has moved - redraw
                if (FullRedraw || resized || (needle.FillEnabled && needle.FillStyle != KineticaRT.Controls.FillStyles.Permanent))
                {
                    //need to redraw the whole lot from scratch - into the background bitmap
                    if (resized)
                    {
                        // Bitmap is the wrong size - create one the correct size.
                        if (bmpBackground != null) bmpBackground.Dispose();
                        bmpBackground = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                        resized = false;
                    }

                    Graphics gBmp = Graphics.FromImage(bmpBackground);

                    // Draw the (custom) background image
                    if (image != null)
                    {
                        gBmp.DrawImage(image, 0, 0);
                    }
                    else
                    {
                        gBmp.FillRectangle(backBrush, rect);
                    }

                    // Draw the fill.
                    SetDisplayRegion(gBmp);
                    gBmp.SmoothingMode = SmoothingMode.AntiAlias;
                    needle.DrawFill(gBmp, rect, scale, false, backBrush);

                    // Don't draw the scale on this dial, scale still exists since it contains the zero angle etc needed for drawing the dial.
                    if (scale != null) scale.Draw(gBmp, rect, KineticaRT.Controls.ScaleOrientations.Circular, this.Font, 1);

                    DrawText(gBmp);

                    // May want to draw a foreground image here, but its still behind the needle...

                    // Tidy up.
                    gBmp.Dispose();
                }

                // This bit done every update.
                Bitmap bmp = (Bitmap)bmpBackground.Clone();
                Graphics g2 = Graphics.FromImage(bmp);
                SetDisplayRegion(g2);
                g2.SmoothingMode = SmoothingMode.AntiAlias;

                needle.Draw(g2, rect);

                // Don't draw the scale on this dial, scale still exists since it contains the zero angle etc needed for drawing the dial.
                if (scale != null) scale.Draw(g2, rect, KineticaRT.Controls.ScaleOrientations.Circular, this.Font, 1);

                // May want to draw a foreground image here, in front of the needle...

                // Tidy up.
                g2.Dispose();
                g.DrawImage(bmp, 0, 0);
                bmp.Dispose();
            }
        }

        ///<summary>
        ///Gets or sets the Needle object
        ///</summary>
        [Category("Compass")]
        [Description("Gets or sets the Needle object")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [RefreshProperties(RefreshProperties.All)]
        public override DialNeedle Needle
        {
            get
            {
                return needle;
            }
            set
            {
                base.Needle = value;
            }
        }
        private bool ShouldSerializeNeedle()
        {
            return !needle.IsDefaultState();
        }
        /// <summary>
        /// Reset the Needle to its default settings
        /// </summary>
        public new void ResetNeedle()
        {
            needle = new CompassSegmentNeedle();
        }

    }

}


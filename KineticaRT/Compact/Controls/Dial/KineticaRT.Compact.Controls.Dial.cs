using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KineticaRT.Compact.Controls
{
    /// <summary>
    /// Enumeration of positions for labels and marks relative to the scale line
    /// </summary>
    public enum ScaleMarkPositions
    {
        /// <summary>
        /// Inside the line (circular scales); Left for vertical scales; Above for Horizontal scales
        /// </summary>
        Inside,
        /// <summary>
        /// Outside the line (circular scales); Right for vertical scales; Below for Horizontal scales
        /// </summary>
        Outside,
        /// <summary>
        /// On the line
        /// </summary>
        On,
    }


    public partial class Dial : KineticaRT.Compact.Controls.Control, KineticaRT.Compact.IConnectable, KineticaRT.Compact.IConnectable2, ISupportInitialize
    {
        private System.Windows.Forms.Timer timer;
        private bool updateNeeded = true;
        private KineticaRT.Compact.Values.Value value;
    
        public Dial()
        {
            InitializeComponent();

            //this.SetStyle(/*ControlStyles.DoubleBuffer |*/ ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();

            shadedPoints = new Point[4];
            timer = new Timer();
            timer.Interval = 50;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
            valueChangedHandler = new KineticaRT.Compact.Channels.ValueChangedEventHandler(sourceChannel_ValueChanged);
            propertyChangedHandler = new PropertyChangedEventHandler(sourceChannel_PropertyChanged);
        }

        #region ISupportInitialize
        private bool initialising;

        /// <summary>
        /// Signals the object that initialization is starting.
        /// </summary>
        public void BeginInit()
        {
            initialising = true;
        }

        /// <summary>
        /// Signals the object that initialization is complete.
        /// </summary>
        public void EndInit()
        {
            initialising = false;
            AutoAdjustMarks();
            AutoAdjustDps();
        }
        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.bmpOff = null;
        }

        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                fontSize = 0;
            }
        }
        private bool ShouldSerializeFont()
        {
            return false;
        }

        private float fontSize;

        public float FontSize
        {
            get
            {
                if (fontSize != 0) return fontSize;

                System.Drawing.Graphics gr = this.CreateGraphics();

                try
                {
                    return (base.Font.Size * gr.DpiX) / 72F;
                }
                finally
                {
                    gr.Dispose();
                }
            }
            set
            {
                fontSize = value;

                System.Drawing.Graphics gr = this.CreateGraphics();
                base.Font = new Font(base.Font.Name, ((value * 72F) / gr.DpiX), base.Font.Style);
                gr.Dispose();
            }
        }

        private Rectangle updateRectangle;

        void timer_Tick(object sender, EventArgs e)
        {
            if (updateNeeded)
            {
                updateNeeded = false;
                Rectangle temp_rectangle = updateRectangle ;
                temp_rectangle.Inflate(-1, -1);
                Invalidate(temp_rectangle);
                //Graphics g = CreateGraphics();
                //PaintEventArgs pea = new PaintEventArgs(g, ClientRectangle);
                //PaintDial(g);
                //g.Dispose();
            }
        }

        private string source;
        private KineticaRT.Compact.Channels.IChannel sourceChannel;
        public string Source
        {
            get
            {
                return source;
            }
            set
            {
                if (source != value)
                {
                    source = value;
                    Connect2();
                }
            }
        }
        private KineticaRT.Compact.Channels.ValueChangedEventHandler valueChangedHandler;
        private PropertyChangedEventHandler  propertyChangedHandler;
        
        /// <summary>
        /// Connects this object to others, Stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public override void Connect2()
        {
            shadedPoints = new Point[4];

            if (sourceChannel != null)
            {
                sourceChannel.ValueChanged -= valueChangedHandler;
                sourceChannel.PropertyChanged -= propertyChangedHandler;
            }
            sourceChannel = OnObjectSearch(source) as KineticaRT.Compact.Channels.IChannel;
            if (sourceChannel != null)
            {
                //validValue = false;
                sourceChannel.ValueChanged += valueChangedHandler;
                sourceChannel.PropertyChanged += propertyChangedHandler;
                value = sourceChannel.Value;

                SetAutoProperties();
            }
            else
            {
                //validValue = false;
                value = KineticaRT.Compact.Values.Values.NotConnected;
            }
        }

        private void SetAutoProperties ()
        {
            if (useSourceScaling)
            {
                KineticaRT.Compact.Channels.INumericChannel numeric = sourceChannel as KineticaRT.Compact.Channels.INumericChannel;
                if (numeric != null)
                {
                    Maximum = numeric.Maximum;
                    Minimum = numeric.Minimum;
                }
            }
        }

        void sourceChannel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Value")
            {
                SetAutoProperties();
            }
        }

        /// <summary>
        /// Gets or sets a KineticaRT.Compact.Values.Value which indicates the location of the needle.  This will normaly be of type KineticaRT.Compact.Values.Double.
        /// </summary>
        public KineticaRT.Compact.Values.Value Value
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
                    updateNeeded = true;
                }
            }
        }

        /// <summary>
        /// Gets or sets a double value which indicates the location of the needle.
        /// </summary>
        public double DoubleValue
        {
            get
            {
                if (this.value == null) return System.Double.NaN;
                return this.value.ToDouble();
            }
            set
            {
                if (this.value == null || this.value.ToDouble() != value)
                {
                    this.value = new KineticaRT.Compact.Values.Double(value);
                    updateNeeded = true;
                }
            }
        }

        void sourceChannel_ValueChanged(KineticaRT.Compact.Channels.IChannel sender, KineticaRT.Compact.Values.Value newVal)
        {
            if (newVal != value)
            {
                value = newVal;
                updateNeeded = true;
            }

//            //System.Globalization.NumberFormatInfo info = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
//            //info.NumberDecimalDigits = 2;
//            //double dbl = newVal.ToDouble();
//            //text = dbl.ToString("F2", info);
            
//            //text = System.Convert.ToString(newVal.ToDouble(), info);
//            //System.Diagnostics.Debug.WriteLine(sourceChannel.Value.ToString());
//            string new_value;
//            //string format_string;
//            //KineticaRT.Compact.Channels.INumericChannel numeric = sourceChannel as KineticaRT.Compact.Channels.INumericChannel;
//            if (/*numeric != null &&*/ string.IsNullOrEmpty(format))
//            {
//                //format_string = numeric.Format;
//                new_value = ((Channels.Channel)sourceChannel).ToString();
//            }
//            else
//            {
//                //format_string = format;
//                new_value = sourceChannel.Value.ToString(format, System.Globalization.CultureInfo.CurrentCulture);
//            }

////            if (string.IsNullOrEmpty(format_string))
//  //          {
//            new_value = ((Channels.Channel)sourceChannel).ToString();//format_string, null);

//                //new_value = sourceChannel.ToString(format_string);
//    //        }
//      //      else
//        //    {
//          //      new_value = sourceChannel.Value.ToString(format_string, System.Globalization.CultureInfo.CurrentCulture);
//            //}

//            if (new_value != value)
//            {
//                value = new_value;
//                //validValue = true;
//                updateNeeded = true;
//            }
        }

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        public override void Disconnect()
        {
            sourceChannel = null;
            value = KineticaRT.Compact.Values.Values.NotConnected;
            //validValue = false;
        }
        
    
        private Color errorColor = Color.Red;
        public Color ErrorColor
        {
            get
            {
                return errorColor;
            }
            set
            {
                if (errorColor != value)
                {
                    errorColor = value;
                    Invalidate(updateRectangle);
                }
            }
        }

        private string format;
        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
                Invalidate(updateRectangle);
            }
        }

        private Bitmap bmpOff ;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Graphics g = CreateGraphics(); // e.Graphics;
            //Graphics g = e.Graphics;
            //g.ResetClip();
            if (bmpOff  == null) bmpOff = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);

            // Create off-screen graphics
            Graphics g = Graphics.FromImage(bmpOff);

            //Brush brush = new SolidBrush(validValue ? value ? trueColor : BackColor : errorColor);
            //g.FillRectangle(brush, ClientRectangle);

            //if (e.ClipRectangle.Equals(ClientRectangle))
            {
                Brush brush = new SolidBrush(BackColor);
                g.FillRectangle(brush, ClientRectangle);
                brush.Dispose();
                Brush border_brush = new SolidBrush(borderColor);
                //Brush brush = new SolidBrush(validValue ? BackColor : errorColor);
                Rectangle rectangle = ClientRectangle;
                g.FillRectangle(border_brush, rectangle.X, rectangle.Y, borderWidth, rectangle.Height);
                g.FillRectangle(border_brush, rectangle.X + rectangle.Width - borderWidth, rectangle.Y, borderWidth, rectangle.Height);
                g.FillRectangle(border_brush, rectangle.X, rectangle.Y, rectangle.Width, borderWidth);
                g.FillRectangle(border_brush, rectangle.X, rectangle.Y + rectangle.Height - borderWidth, rectangle.Width, borderWidth);

                updateRectangle = ClientRectangle;
                updateRectangle.Inflate(-borderWidth, -borderWidth);
                //g.FillRectangle(brush, updateRectangle);

                DrawScale(g);
            }


            if (value == null || System.Double.IsNaN(value.ToDouble()))// == KineticaRT.Compact.Values.Values.NotConnected )
            {
                string description;
                KineticaRT.Compact.Channels.IChannel channel;
                channel = sourceChannel as KineticaRT.Compact.Channels.IChannel;
                if (channel != null)
                {
                    description = channel.ToString();
                }
                else
                {
                    description = "";
                }

                Brush text_brush = new SolidBrush(ForeColor);
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;
                drawFormat.LineAlignment = StringAlignment.Center;
                g.DrawString(description, base.Font, text_brush, updateRectangle, drawFormat);
            }
            else
            {
                DrawNeedle(g);
            }

            e.Graphics.DrawImage(bmpOff, 0, 0, this.ClientRectangle, GraphicsUnit.Pixel);

            g.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs paintg)
        {
            // Left empty, avoids undesirable flickering
        }

        private void CalcPoint(float length, ref System.Drawing.Point point)
        {
            float a = xScale * length * 0.5F;
            float b = yScale * length * 0.5F;
            float a2 = a * a;
            float b2 = b * b;
            float rd = (float)Math.Sqrt((b2 * a2) / (b2 * sin_theta2 + a2 * cos_theta2));
            //point = new PointF((float)(xOffset + xScale * 0.5 + rd * sin_theta), (float)(yOffset + yScale * 0.5 + rd * cos_theta)) ;
            point.X = (int)(xOffset + xScale * 0.5 + rd * sin_theta);
            point.Y = (int)(yOffset + yScale * 0.5 + rd * cos_theta);
        }
        
        private float sin_theta;
        private float cos_theta;
        private float sin_theta2;
        private float cos_theta2;
        
        int xScale ;
        int xOffset ;
        int yScale ;
        int yOffset ;

        private void DrawScale (Graphics g)
        {
            int scale_x_offset;
            int scale_y_offset;
            double ratio = 1.0;
            Rectangle drawRectangle = this.ClientRectangle;

            bool enabled = true ;
            int lineWidth = 1;
            bool anticlockwise = false;
            const double VERY_SMALL_DOUBLE = 0.0000000001;
            const float VERY_SMALL_FLOAT = 0.0000000001F;
            ScaleMarkPositions markPosition = ScaleMarkPositions.Outside;



            //calculate offset for scale radius proportion
            scale_x_offset = (int)(0.5 * (1 - position) * ratio * drawRectangle.Width);
            scale_y_offset = (int)(0.5 * (1 - position) * ratio * drawRectangle.Height);

            drawRectangle.Inflate(-scale_x_offset, -scale_y_offset);

            xScale = drawRectangle.Width;
            xOffset = drawRectangle.Left;
            yScale = drawRectangle.Height;
            yOffset = drawRectangle.Top;

            			//if area has no width or height, don't even try and draw
			if (xScale <= 0 || yScale <= 0)		return ;

            float val = (float)majorMarksStart;
            float range;
            float increment;
            float max = (float)maximum;
            float min = (float)minimum;
            //float				start_angle		= (float)zeroAngle - 90 ;	//Arc has x axis as origin
            float sweep_angle = (float)sweepAngle;
            int major_count;
            int label_count;
            Pen line_pen = new Pen(scaleColor, (float)lineWidth);

            if (!enabled) return;

            //			if (start_angle < 0)
            //			{
            //				start_angle += 360 ;
            //			}

            //Rectangle rect = new Rectangle(xOffset, yOffset, xScale, yScale);
            //double start = zeroAngle;
            //double sweep = sweep_angle;

            ////Cannot use start_angle and sweep_angle,
            ////as they don't take non-squareness into account
            //ScaleAngles(anticlockwise, ref start, ref sweep);


            major_count = 0;
            label_count = labelAfter;

            if (minimum > maximum)
            {
                //max and min are the wrong way around!
                min = (float)maximum;
                max = (float)minimum;
            }

            // Find the first minor mark.
            // we need to check that the start value is >= minimum value
            if (val < min)
            {
                val = min;
            }
            increment = System.Math.Abs((float)majorMarksRepeat / minorMarksDivisions);
            while (val >= (min + increment))
            {
                val -= increment;
                major_count++;
            }

            // Find the range.        
            range = (max - min);

            // Draw.                 
            if (range > 0)
            {
                bool started_major = false;
                Point start_point = new Point();
                Point end_point = new Point();
                int label_number = 0;
                Brush brush = new SolidBrush(scaleColor);

                //max += increment / 2;
                max += VERY_SMALL_FLOAT;		//skc 4/3/03
                for (; val <= max; val += increment)
                {
                    float fractional_value;
                    float display_angle;

                    fractional_value = (val - min) / range;
                    if (!anticlockwise)
                    {
                        display_angle = (float)zeroAngle + (sweep_angle * fractional_value);
                    }
                    else
                    {
                        display_angle = (float)zeroAngle - (sweep_angle * fractional_value);
                    }

                    //display_angle += 90 ;
                    if (display_angle >= 360)
                    {
                        display_angle -= 360;
                    }
                    else if (display_angle < 0)
                    {
                        display_angle += 360;
                    }

                    sin_theta = (float)Math.Sin(display_angle * Math.PI / 180);
                    cos_theta = (float)Math.Cos((display_angle + 180) * Math.PI / 180);
                    cos_theta2 = cos_theta * cos_theta;
                    sin_theta2 = sin_theta * sin_theta;

                    if ((major_count != 0) || (!started_major && val < (majorMarksStart - VERY_SMALL_DOUBLE)))
                    {
                        // Minor mark.
                        if (markPosition == ScaleMarkPositions.Inside)
                        {
                            CalcPoint(0.96F, ref start_point);
                            CalcPoint(1.00F, ref end_point);
                        }
                        else if (markPosition == ScaleMarkPositions.Outside)
                        {
                            CalcPoint(1.00F, ref start_point);
                            CalcPoint(1.04F, ref end_point);
                        }
                        else		//On line
                        {
                            CalcPoint(0.98F, ref start_point);
                            CalcPoint(1.02F, ref end_point);
                        }

                        g.DrawLine(line_pen, start_point.X, start_point.Y, end_point.X, end_point.Y);

                        if (major_count == 0)
                        {
                            major_count = minorMarksDivisions;
                        }
                    }
                    else
                    {
                        // Major mark
                        started_major = true;

                        if (markPosition == ScaleMarkPositions.Inside)
                        {
                            CalcPoint(0.90F, ref start_point);
                            CalcPoint(1.00F, ref end_point);
                        }
                        else if (markPosition == ScaleMarkPositions.Outside)
                        {
                            CalcPoint(1.10F, ref start_point);
                            CalcPoint(1.00F, ref end_point);
                        }
                        else		//On line
                        {
                            CalcPoint(0.95F, ref start_point);
                            CalcPoint(1.05F, ref end_point);
                        }

                        g.DrawLine(line_pen, start_point.X, start_point.Y, end_point.X, end_point.Y);

                        if ((label_count == 0) && labelsEnabled)
                        {
                            string label;
                            SizeF size;

                            label = GetLabel(val, label_number);
                            label_number++;

                            size = g.MeasureString(label, Font);

                            if (labelPosition == ScaleMarkPositions.Inside)
                            {
                                if (markPosition == ScaleMarkPositions.Outside)
                                {
                                    CalcPoint(0.95F, ref start_point);
                                }
                                else
                                {
                                    CalcPoint(0.89F, ref start_point);
                                }
                                start_point = InteriorAdjustment(size, display_angle, start_point);
                            }
                            else		//outside
                            {
                                if (markPosition == ScaleMarkPositions.Outside)
                                {
                                    CalcPoint(1.11F, ref start_point);
                                }
                                else
                                {
                                    CalcPoint(1.05F, ref start_point);
                                }
                                start_point = ExteriorAdjustment(size, display_angle, start_point);
                            }

                            g.DrawString(label, Font, brush, start_point.X, start_point.Y);
                            label_count = labelEvery;
                        }

                        label_count--;
                        major_count = minorMarksDivisions;
                    }

                    major_count--;
                }
                brush.Dispose();

                Rectangle rect = new Rectangle(xOffset, yOffset, xScale, yScale);
                //thresholds.Draw(g, ratio, rect, ScaleOrientations.Circular, position, this);
            }

            line_pen.Dispose();

        }

        		//converts angles for non-square objects
		/// <summary>
		/// Converts angles for non-square controls.
		/// </summary>
		/// <param name="Acw">True if the controlis anti-clockwise</param>
		/// <param name="StartAngle">Angle in square that is returned converted to an angle in the current Rectangle</param>
		/// <param name="SweepAngle">Angle in square that is returned converted to an angle in the current Rectangle</param>
		protected void ScaleAngles(bool Acw, ref double StartAngle, ref double SweepAngle)
		{
			double			end_angle ;
			double			scaled_start ;
			double			scaled_end ;
			double			scaled_sweep ;
			double			rad_angle ;
			//Point			pt				= new Point() ;
            double X;
            double Y;

			//find start point...
			rad_angle = StartAngle * Math.PI / 180 ;
			X = (xScale * 0.5 * Math.Sin(rad_angle)) ;
            Y = (-yScale * 0.5 * Math.Cos(rad_angle));

			//and convert that back to an angle
			scaled_start = Math.Atan((double)(Y / X)) * 180 / Math.PI ;
			//period of Tan is 180 deg, so may need to manually offset it
			if (StartAngle > 180)			scaled_start += 180 ;
			if (scaled_start < 0)			scaled_start += 360 ;

			//find end angle...
			if (SweepAngle == 0 || SweepAngle == 360)
			{
				//nothing to do - both 0 and 360 scale to themselves
				scaled_sweep = SweepAngle ;
			}
			else
			{
				//calculate the scaled sweep angle
				end_angle = (!Acw) ? (StartAngle + SweepAngle) : (StartAngle - SweepAngle) ;
				if (end_angle < 0)			end_angle += 360 ;
				if (end_angle >= 360)		end_angle -= 360 ;

				//...and end point...
				rad_angle	= end_angle * Math.PI / 180 ;
                X = (xScale * 0.5 * Math.Sin(rad_angle));
				Y = (- yScale * 0.5 * Math.Cos(rad_angle)) ;

				//...convert that back to an angle...
				scaled_end = Math.Atan((double)(Y / X)) * 180 / Math.PI ;
				//period of Tan is 180 deg, so may need to manually offset it
				if (end_angle > 180)		scaled_end += 180 ;
				if (scaled_end < 0)			scaled_end += 360 ;

				//and convert that to a sweep angle
				scaled_sweep = (!Acw) ? (360 - scaled_start + scaled_end) : (360 - scaled_end + scaled_start) ;
				if (scaled_sweep < 0)			scaled_sweep += 360 ;
				if (scaled_sweep >= 360)		scaled_sweep -= 360 ;
			}

			//return the values
			StartAngle = scaled_start ;
			SweepAngle = scaled_sweep ;
		}

        private bool labelNumeric = true ;
        private string[] labelsText = new string[0];
        private double scale = 1.0;
        private string numericLabelFormat = "G";

        // Gets label corresponding to value / nth string
        private string GetLabel(double value, int labelIndex)
        {
            string label;

            if (!labelNumeric)		//text labels
            {
                if (labelsText.Length > labelIndex)
                {
                    label = labelsText[labelIndex];
                }
                else
                {
                    label = "";
                }
            }
            else		//numeric labels
            {
                //				string		format ;
                //
                //				format = "N" ;
                //				format += decimalPlaces.ToString() ;

                label = (value * scale).ToString(numericLabelFormat);
            }

            return label;
        }

        //calculates an adjustment for scales labelled on the inside
        private Point InteriorAdjustment
        (
            SizeF size,		//text extent
            double angle,		//display angle (degrees)
            Point point		//point for adjusting
        )
        {
            Point adjusted = point;

            //x
            if (angle >= 200.0 && angle <= 340.0)
            {
                //no adjustment
            }
            else if (angle >= 20.0 && angle <= 160.0)
            {
                adjusted.X -= (int)size.Width;
            }
            else if (angle >= 160.0 && angle <= 200.0)
            {
                adjusted.X -= (int)(((200.0 - angle) * size.Width) / 40.0);
            }
            else
            {
                if (angle <= 20) angle += 360;

                adjusted.X -= (int)(((angle - 340.0) * size.Width) / 40.0);
            }

            //y
            if (angle >= 50.0 && angle <= 130.0)
            {
                adjusted.Y -= (int)(((angle - 50.0) * size.Height) / 80.0);
            }
            else if (angle >= 230.0 && angle <= 310.0)
            {
                adjusted.Y -= (int)(((310.0 - angle) * size.Height) / 80.0);
            }
            else if (angle >= 130.0 && angle <= 230.0)
            {
                adjusted.Y -= (int)size.Height;
            }
            else
            {
                //no adjustment
            }

            return adjusted;
        }

        //  calculates an adjustment for scales labelled on the outside
        private Point ExteriorAdjustment
        (
            SizeF size,		//text extent
            double angle,		//display angle (degrees)
            Point point		//point for adjusting
        )
        {
            Point adjusted = point;

            //x
            if (angle >= 225.0 && angle <= 315.0)
            {
                adjusted.X -= (int)size.Width;
            }
            else if (angle >= 45.0 && angle <= 135.0)
            {
                //no adjustment
            }
            else if (angle >= 135.0 && angle <= 225.0)
            {
                adjusted.X -= (int)(((angle - 135) * size.Width) / 90.0);
            }
            else
            {
                if (angle <= 45) angle += 360;

                adjusted.X -= (int)(((405 - angle) * size.Width) / 90.0);
            }

            //y
            if (angle >= 45.0 && angle <= 135.0)
            {
                adjusted.Y -= (int)(((135 - angle) * size.Height) / 90.0);
            }
            else if (angle >= 225.0 && angle <= 315.0)
            {
                adjusted.Y -= (int)(((angle - 225.0) * size.Height) / 90.0);
            }
            else if (angle >= 135.0 && angle <= 225.0)
            {
                //no adjustment
            }
            else
            {
                adjusted.Y -= (int)size.Height;
            }

            return adjusted;
        }

        
        private Point[] shadedPoints;
        private double lastAngle;

        /// <summary>
        /// Draw the needle
        /// </summary>
        /// <param name="g">Graphics object with which to draw</param>
        /// <param name="DrawRect">Rectangle in which to draw</param>
        public virtual void DrawNeedle(Graphics g)
        {
            bool needleVisible = true;

            if (needleVisible && value is Values.Double && !System.Double.IsNaN(value.ToDouble()))
            {
                displayRect = ClientRectangle;

                double currentValue = value.ToDouble();
                double range = maximum - minimum;
                double currentFraction = (currentValue - minimum) / range;
                double currentAngle = zeroAngle + currentFraction * sweepAngle;
                double width = 0.1;
                double length = 0.7;
                double rad_angle = currentAngle * Math.PI / 180;	//radians

                shadedPoints[0].X = ScaleX(0.5 + Math.Sin(rad_angle + Math.PI) * width * 0.5);
                shadedPoints[0].Y = ScaleY(0.5 - Math.Cos(rad_angle + Math.PI) * width * 0.5);

                shadedPoints[1].X = ScaleX(0.5 + Math.Sin(rad_angle - (Math.PI / 2)) * width * 0.5);
                shadedPoints[1].Y = ScaleY(0.5 - Math.Cos(rad_angle - (Math.PI / 2)) * width * 0.5);

                shadedPoints[2].X = ScaleX(0.5 + Math.Sin(rad_angle) * 0.5 * length);
                shadedPoints[2].Y = ScaleY(0.5 - Math.Cos(rad_angle) * 0.5 * length);

                shadedPoints[3].X = ScaleX(0.5 + Math.Sin(rad_angle + (Math.PI / 2)) * width * 0.5);
                shadedPoints[3].Y = ScaleY(0.5 - Math.Cos(rad_angle + (Math.PI / 2)) * width * 0.5);

                if (shadedPoints[0].Equals(shadedPoints[2]))
                {
                    //don't bother to draw: end and tip are the same point
                }
                else
                {
                    Brush brush;

                    brush = new SolidBrush(ForeColor);

                    g.FillPolygon(brush, shadedPoints);

                    brush.Dispose();
                }
                //else not a valid value		//draw unconnected symbol?

                //done the drawing now, so update 'last_angle'
                lastAngle = currentAngle;
            }
        }

        //Calculates MajorMarksStart, MajorMarksRepeat and MinorMarksDivisions
        //based on the current min and max values
        //returns true if changed something
        private bool AutoAdjustMarks()
        {
            if (initialising) return false ;
            bool changed = false;

            if (autoAdjust)
            {
                double range = maximum - minimum;

                if (range > 0.0)
                {
                    double interval;			//size of interval
                    int index;
                    double power_ten;			//a power of 10
                    double repeat;
                    double min_mult;
                    int minor;

                    // Calculate the smallest potential interval size
                    interval = range / 7;

                    // Calculate the smallest nice number not smaller than interval
                    repeat = FirstNiceNum(interval, out index, out power_ten);

                    while (repeat < interval)
                    {
                        repeat = NextNiceNum(ref index, ref power_ten);
                    }

                    //Produce the internal scale using the specified nice number
                    min_mult = Math.Ceiling(minimum / repeat);

                    // Perform some extra checking
                    if ((min_mult - 1.0) * repeat >= minimum)
                    {
                        min_mult--;
                    }

                    //minor marks: 5 for multiples of 5, 4 for 2, 1 for 1
                    switch (index)
                    {
                        default: minor = 2; break;			//1 * 10^n
                        case 1: minor = 4; break;			//2 * 10^n
                        case 2: minor = 5; break;			//5 * 10^n
                    }

                    //has anything changed?
                    if (majorMarksStart != (repeat * min_mult))
                    {
                        changed = true;
                        majorMarksStart = repeat * min_mult;
                    }

                    if (changed || majorMarksRepeat != repeat)
                    {
                        changed = true;
                        majorMarksRepeat = repeat;
                    }

                    if (changed || labelEvery != 1)
                    {
                        changed = true;
                        labelEvery = 1;
                    }

                    if (changed || minorMarksDivisions != minor)
                    {
                        changed = true;
                        minorMarksDivisions = minor;
                    }
                }
            }
            return changed;
        }

        //Calculates number of decimal places based on the current min and max
        //returns true if changed
        private bool AutoAdjustDps()
        {
            if (initialising) return false;
            bool changed = false;

            if (autoAdjust)
            {
                double range = maximum - minimum;

                if (range > 0.0)
                {
                    int log_range;
                    short places;

                    log_range = (int)Math.Log10(range);
                    places = (short)(2 - log_range);

                    if (range >= 1) places--;

                    if (places > 16)
                    {
                        places = 16;
                    }
                    else if (places < 0)
                    {
                        places = 0;
                    }

                    if (decimalPlaces != places)
                    {
                        changed = true;
                        DecimalPlaces = places;
                    }
                }
            }
            return changed;
        }

        // Calculate an initial value for the nice number
        // Index is into array
        // Returns nice number
        private double FirstNiceNum(double Interval, out int Index, out double PowerOfTen)
        {
            int exponent;

            // Calculate an initial power of 10
            exponent = (int)Math.Floor(Math.Log10(Interval));

            // Perform some extra checking
            PowerOfTen = Math.Pow(10.0, exponent);
            if (PowerOfTen * 10.0 <= Interval)
            {
                PowerOfTen *= 10.0;
            }

            // Initial index is always 0
            Index = 0;

            return PowerOfTen;
        }

        // Calculate the next nice number
        // Index is into Set
        // Returns NiceNumber
        private double NextNiceNum(ref int Index, ref double PowerOfTen)
        {
            //The set of potential multipliers for nice numbers
            double[] Set = new double[] { 1.0, 2.0, 5.0 };

            // Increment the index
            (Index)++;

            // If over max index, reset to 0 and increase the power of 10
            if (Index > 2)
            {
                Index = 0;
                PowerOfTen *= 10.0;
            }

            return (Set[Index] * PowerOfTen);
        }

        /// <summary>
        /// Converts a proportion to an actual value based on the scale's current range
        /// </summary>
        /// <param name="fraction">Proportion to convert (0-1)</param>
        /// <returns>Value to which the given proportion relates</returns>
        public double FractionToValue(double fraction)
        {
            return minimum + fraction * (maximum - minimum);
        }

        /// <summary>
        /// Converts a value in the scale's current range to a proportion of the way through that range
        /// </summary>
        /// <param name="Value">Value to convert</param>
        /// <returns>Fraction of the way along the range</returns>
        public double ValueToFraction(double Value)
        {
            if (maximum != minimum)
            {
                return (Value - minimum) / (maximum - minimum);
            }
            return 0;
        }

        /// <summary>
        /// Converts a value in the scale's current range to a proportion of the way through that range
        /// </summary>
        /// <param name="Value">Value to convert</param>
        /// <returns>Fraction of the way along the range</returns>
        public float ValueToFraction(float Value)
        {
            if (maximum != minimum)
            {
                return (Value - (float)minimum) / ((float)maximum - (float)minimum);
            }
            return 0;
        }



        private double zeroAngle = 210;
        ///<summary>
        ///Gets or Sets the zero (start) angle of the Scale (Circular scales only). Degrees clockwise from top.
        ///</summary>
        [Category("Scale")]
        [DefaultValue(210.0)]
        [Description("Gets or Sets the zero (start) angle of the Scale(Circular scales only). Degrees clockwise from top.")]
        public double ZeroAngle
        {
            get
            {
                return zeroAngle;
            }
            set
            {
                if (zeroAngle != value)
                {
                    zeroAngle = value;
                    while (zeroAngle > 360) zeroAngle -= 360;
                    while (zeroAngle < 0) zeroAngle += 360;

                    OnPropertyChanged("ZeroAngle");
                    updateNeeded = true;
                }
            }
        }

        private double sweepAngle = 300;
        ///<summary>
        ///Gets or Sets the Sweep angle of the Scale, in Degrees (circular scales only).
        ///</summary>
        [Category("Scale")]
        [DefaultValue(300.0)]
        [Description("Gets or Sets the sweep angle of the Scale, in Degrees (circular scales only).")]
        public double SweepAngle
        {
            get
            {
                return sweepAngle;
            }
            set
            {
                if (sweepAngle != value)
                {
                    sweepAngle = value;
                    while (sweepAngle > 360) sweepAngle -= 360;
                    while (sweepAngle < 0) sweepAngle += 360;

                    OnPropertyChanged("SweepAngle");
                    updateNeeded = true;
                }
            }
        }

        Color scaleColor = Color.DarkBlue;
        ///<summary>
        ///Gets or Sets the color of the Scale (lines and text)
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the color of the Scale (lines and text)")]
        public Color ScaleColor
        {
            get
            {
                return scaleColor;
            }
            set
            {
                if (scaleColor != value)
                {
                    scaleColor = value;
                    OnPropertyChanged("ScaleColor");
                    updateNeeded = true;
                }
            }
        }
        private bool ShouldSerializeColor()
        {
            return scaleColor != Color.DarkBlue;
        }
        
        int labelAfter = 0;
        ///<summary>
        ///Gets or Sets how many major marks occur before labels start
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets how many major marks occur before labels start")]
        [DefaultValue(0)]
        public int LabelAfter
        {
            get
            {
                return labelAfter;
            }
            set
            {
                if (labelAfter != value)
                {
                    labelAfter = value;
                    if (labelAfter < 0) labelAfter = 0;

                    OnPropertyChanged("LabelAfter");
                    updateNeeded = true;
                }
            }
        }

        double majorMarksStart = 0.0;
        ///<summary>
        ///Gets or Sets the value at which the first Major mark is drawn
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the value at which the first Major mark is drawn")]
        [DefaultValue(0.0)]
        public double MajorMarksStart
        {
            get
            {
                return majorMarksStart;
            }
            set
            {
                if (majorMarksStart != value)
                {
                    majorMarksStart = value;
                    OnPropertyChanged("MajorMarksStart");
                    updateNeeded = true;
                }
            }
        }

        double majorMarksRepeat = 20;
        ///<summary>
        ///Gets or Sets the Interval between major marks
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the interval between major marks")]
        [DefaultValue(20.0)]
        public double MajorMarksRepeat
        {
            get
            {
                return majorMarksRepeat;
            }
            set
            {
                if (majorMarksRepeat != value)
                {
                    majorMarksRepeat = value;
                    if (majorMarksRepeat < 0) majorMarksRepeat = 0;

                    OnPropertyChanged("MajorMarksRepeat");
                    updateNeeded = true;
                }
            }
        }

        int minorMarksDivisions = 4;
        ///<summary>
        ///Gets or Sets the number of divisions between major marks
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the number of divisions between major marks")]
        [DefaultValue(4)]
        public int MinorMarksDivisions
        {
            get
            {
                return minorMarksDivisions;
            }
            set
            {
                if (minorMarksDivisions != value)
                {
                    minorMarksDivisions = value;
                    if (minorMarksDivisions < 0) minorMarksDivisions = 0;

                    OnPropertyChanged("MinorMarksDivisions");
                    updateNeeded = true;
                }
            }
        }

        private double maximum = 100;
        ///<summary>
        ///Gets or Sets the Maximum value shown
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the Maximum value shown")]
        [DefaultValue(100.0)]
        public double Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                if (maximum != value)
                {
                    maximum = value;
                    AutoAdjustMarks();
                    AutoAdjustDps();
                    OnPropertyChanged("Maximum");
                    updateNeeded = true;
                }
            }
        }

        private double minimum = 0;
        ///<summary>
        ///Gets or Sets the Minimum Value shown
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the Minimum value shown")]
        [DefaultValue(0.0)]
        public double Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                if (minimum != value)
                {
                    minimum = value;
                    AutoAdjustMarks();
                    AutoAdjustDps();
                    OnPropertyChanged("Minimum");
                    updateNeeded = true;
                }
            }
        }

        private bool autoAdjust = true ;
        ///<summary>
        ///Gets or Sets whether automatic adjustment of MajorMarksStart, MajorMarksRepeat
        ///MinorMarksDivisions and DecimalPlaces occurs
        ///</summary>
        [Category("Scale")]
        [DefaultValue(true)]
        [Description("Gets or Sets whether automatic adjustment of MajorMarksStart, MajorMarksRepeat, MinorMarksDivisions and DecimalPlaces occurs")]
        public bool AutoAdjust
        {
            get
            {
                return autoAdjust;
            }
            set
            {
                if (autoAdjust != value)
                {
                    autoAdjust = value;
                    AutoAdjustMarks();
                    AutoAdjustDps();
                    OnPropertyChanged("AutoAdjust");
                    updateNeeded = true;
                }
            }
        }

        double position = 0.7;
        ///<summary>
        ///Gets or Sets the position of the Scale line as proportion of the parent Control (0..1)
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the position of the Scale line as proportion of the parent Control (0..1)")]
        //default value is variable
        public double Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position != value)
                {
                    position = value;
                    if (position > 1) position = 1;
                    if (position < 0) position = 0;

                    OnPropertyChanged("Position");
                    updateNeeded = true;
                }
            }
        }
        private bool ShouldSerializePosition()
        {
            return position != 0.7;
        }

        int labelEvery = 1;
        ///<summary>
        ///Gets or Sets the number of major marks between labels
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the number of major marks between labels")]
        [DefaultValue(1)]
        public int LabelEvery
        {
            get
            {
                return labelEvery;
            }
            set
            {
                if (labelEvery != value)
                {
                    labelEvery = value;
                    if (labelEvery < 1) labelEvery = 1;

                    OnPropertyChanged("LabelEvery");
                    updateNeeded = true;
                }
            }
        }

        private int decimalPlaces;
        ///<summary>
        ///Gets or Sets the number of decimal places on numeric labels
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the number of decimal places on numeric labels")]
        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get
            {
                return decimalPlaces;
            }
            set
            {
                if (decimalPlaces != value)
                {
                    decimalPlaces = value;
                    if (decimalPlaces < 0) decimalPlaces = 0;

                    numericLabelFormat = "N" + decimalPlaces.ToString();

                    OnPropertyChanged("DecimalPlaces");
                    updateNeeded = true;
                }
            }
        }

        ScaleMarkPositions labelPosition = ScaleMarkPositions.Outside;
        ///<summary>
        ///Gets or Sets the position of labels relative to the Scale line
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets the position of labels relative to the Scale line")]
        public ScaleMarkPositions LabelPosition
        {
            get
            {
                return labelPosition;
            }
            set
            {
                if (labelPosition != value)
                {
                    labelPosition = value;
                    OnPropertyChanged("LabelPosition");
                    updateNeeded = true;
                }
            }
        }
        private bool ShouldSerializeLabelPosition()
        {
            return labelPosition != ScaleMarkPositions.Outside;
        }

        bool labelsEnabled = true;
        ///<summary>
        ///Gets or Sets whether labels are shown
        ///</summary>
        [Category("Scale")]
        [Description("Gets or Sets whether labels are shown")]
        [DefaultValue(true)]
        public bool LabelsEnabled
        {
            get
            {
                return labelsEnabled;
            }
            set
            {
                if (labelsEnabled != value)
                {
                    labelsEnabled = value;
                    OnPropertyChanged("LabelsEnabled");
                    updateNeeded = true;
                }
            }
        }

        bool useSourceScaling = true;
        ///<summary>
        ///Gets or Sets whether the minimum and maximum are obtained from the source channel.
        ///</summary>
        [Category("Scale")]
        [Description("Whether the minimum and maximum are obtained from the source channel.")]
        [DefaultValue(true)]
        public bool UseSourceScaling
        {
            get
            {
                return useSourceScaling;
            }
            set
            {
                if (useSourceScaling != value)
                {
                    useSourceScaling = value;
                    OnPropertyChanged("UseSourceScaling");
                    updateNeeded = true;
                }
            }
        }

        //Font LabelFont = new Font("Tahoma", 5, FontStyle.Regular);

    }
}
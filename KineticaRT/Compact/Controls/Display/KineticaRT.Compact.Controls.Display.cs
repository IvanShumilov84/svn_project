using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KineticaRT.Compact.Controls
{
    public partial class Display : KineticaRT.Compact.Controls.Control, KineticaRT.Compact.IConnectable, KineticaRT.Compact.IConnectable2 
    {
        private System.Windows.Forms.Timer timer;
        bool updateNeeded = true;
        private string value;
        //private bool validValue;

        public Display()
        {
            InitializeComponent();

            //this.SetStyle(/*ControlStyles.DoubleBuffer |*/ ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.UpdateStyles();
            

            timer = new Timer();
            timer.Interval = 50;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
            valueChangedHandler = new KineticaRT.Compact.Channels.ValueChangedEventHandler(sourceChannel_ValueChanged);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Brush brush = new SolidBrush(validValue ? value ? trueColor : BackColor : errorColor);
            //e.Graphics.FillRectangle(brush, ClientRectangle);

            if (!e.ClipRectangle.Equals(updateRectangle))
            {
                Brush border_brush = new SolidBrush(borderColor);
                //Brush brush = new SolidBrush(validValue ? BackColor : errorColor);
                Rectangle rectangle = ClientRectangle;
                e.Graphics.FillRectangle(border_brush, rectangle.X, rectangle.Y, borderWidth, rectangle.Height);
                e.Graphics.FillRectangle(border_brush, rectangle.X + rectangle.Width - borderWidth, rectangle.Y, borderWidth, rectangle.Height);
                e.Graphics.FillRectangle(border_brush, rectangle.X, rectangle.Y, rectangle.Width, borderWidth);
                e.Graphics.FillRectangle(border_brush, rectangle.X, rectangle.Y + rectangle.Height - borderWidth, rectangle.Width, borderWidth);

                updateRectangle = ClientRectangle;
                updateRectangle.Inflate(-borderWidth, -borderWidth);
                //e.Graphics.FillRectangle(brush, updateRectangle);
            }
            
            string description;
            if (string.IsNullOrEmpty(value))
            {
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
            }
            else
            {
                description = this.value;
            }

            Brush text_brush = new SolidBrush(ForeColor);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(description, base.Font, text_brush, updateRectangle, drawFormat);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (updateNeeded)
            {
                updateNeeded = false;
                Invalidate(updateRectangle);
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
        /// <summary>
        /// Connects this object to others, Stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public override void Connect2()
        {
            if (sourceChannel != null)
            {
                sourceChannel.ValueChanged -= valueChangedHandler;
            }
            sourceChannel = OnObjectSearch(source) as KineticaRT.Compact.Channels.IChannel;
            if (sourceChannel != null)
            {
                //validValue = false;
                sourceChannel.ValueChanged += valueChangedHandler;
                value = sourceChannel.Value.ToString();
            }
            else
            {
                //validValue = false;
                value = KineticaRT.Compact.Values.Values.NotConnected.ToString();
            }
        }


        void sourceChannel_ValueChanged(KineticaRT.Compact.Channels.IChannel sender, KineticaRT.Compact.Values.Value newVal)
        {
            //System.Globalization.NumberFormatInfo info = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
            //info.NumberDecimalDigits = 2;
            //double dbl = newVal.ToDouble();
            //text = dbl.ToString("F2", info);
            
            //text = System.Convert.ToString(newVal.ToDouble(), info);
            //System.Diagnostics.Debug.WriteLine(sourceChannel.Value.ToString());
            string new_value;
            //string format_string;
            //KineticaRT.Compact.Channels.INumericChannel numeric = sourceChannel as KineticaRT.Compact.Channels.INumericChannel;
            if (/*numeric != null &&*/ string.IsNullOrEmpty(format))
            {
                //format_string = numeric.Format;
                new_value = ((Channels.Channel)sourceChannel).ToString();
            }
            else
            {
                //format_string = format;
                new_value = sourceChannel.Value.ToString(format, System.Globalization.CultureInfo.CurrentCulture);
            }

//            if (string.IsNullOrEmpty(format_string))
  //          {
            new_value = ((Channels.Channel)sourceChannel).ToString();//format_string, null);

                //new_value = sourceChannel.ToString(format_string);
    //        }
      //      else
        //    {
          //      new_value = sourceChannel.Value.ToString(format_string, System.Globalization.CultureInfo.CurrentCulture);
            //}

            if (new_value != value)
            {
                value = new_value;
                //validValue = true;
                updateNeeded = true;
            }
        }

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        public override void Disconnect()
        {
            sourceChannel = null;
            value = KineticaRT.Compact.Values.Values.NotConnected.ToString();
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

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace KineticaRT.Compact.Controls
{
    public partial class Indicator : KineticaRT.Compact.Controls.Control, KineticaRT.Compact.IConnectable, KineticaRT.Compact.IConnectable2 
    {
        private System.Windows.Forms.Timer timer;
        bool updateNeeded = true;
        private bool value;
        private bool validValue;

        public Indicator()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 50;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
            valueChangedHandler = new KineticaRT.Compact.Channels.ValueChangedEventHandler(sourceChannel_ValueChanged);
            updateNeeded = true;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Brush brush = new SolidBrush(validValue ? value ? trueColor : BackColor : errorColor);
            //e.Graphics.FillRectangle(brush, ClientRectangle);
            
            Brush border_brush = new SolidBrush(borderColor);
            Brush brush = new SolidBrush(validValue ? value ? trueColor : BackColor : errorColor);
            Brush text_brush = new SolidBrush(ForeColor);
            Rectangle rect = ClientRectangle;
            e.Graphics.FillRectangle(border_brush, rect);
            rect.Inflate(-borderWidth, -borderWidth);
            e.Graphics.FillRectangle(brush, rect);

            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;

            string description;
            if (string.IsNullOrEmpty(this.description))
            {
                KineticaRT.Compact.Channels.IInfoChannel info_channel;
                info_channel = sourceChannel as KineticaRT.Compact.Channels.IInfoChannel;
                if (info_channel != null)
                {
                    description = info_channel.Description;
                }
                else
                {
                    description = "";
                }
            }
            else
            {
                description = this.description;
            }

            e.Graphics.DrawString(description, base.Font, text_brush, rect, drawFormat);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (updateNeeded)
            {
                updateNeeded = false;
                Invalidate(ClientRectangle);
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
                updateNeeded = true;
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
                sourceChannel.ValueChanged += valueChangedHandler;
                try
                {
                    value = System.Convert.ToBoolean(sourceChannel.Value);
                    validValue = true ;
                }
                catch (System.FormatException)
                {
                    validValue = false;
                    value = false;
                }
            }
            else
            {
                validValue = true ;
            }
            updateNeeded = true;
        }


        void sourceChannel_ValueChanged(KineticaRT.Compact.Channels.IChannel sender, KineticaRT.Compact.Values.Value newVal)
        {
            //Debug.WriteLine("Update ------------------");
            value = sourceChannel.Value == KineticaRT.Compact.Values.Values.True;
            validValue = true;
            updateNeeded = true;
        }

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        public override void Disconnect()
        {
            sourceChannel = null;
            validValue = false;
            updateNeeded = true;
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    updateNeeded = true;
                }
            }
        }

        private Color trueColor = Color.Gold;
        public Color TrueColor
        {
            get
            {
                return trueColor;
            }
            set
            {
                if (trueColor != value)
                {
                    trueColor = value;
                    updateNeeded = true;
                }
            }
        }

        private Color errorColor = Color.Red ;
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
                    updateNeeded = true;
                }
            }
        }

 
    }
}

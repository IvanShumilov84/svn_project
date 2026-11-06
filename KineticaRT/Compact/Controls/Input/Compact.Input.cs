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
    public partial class Input : KineticaRT.Compact.Controls.Control, KineticaRT.Compact.IConnectable, KineticaRT.Compact.IConnectable2
    {
        private System.Windows.Forms.Timer timer;
        bool updateNeeded = true;
        private double value;

        public Input()
        {
            InitializeComponent();

            BorderStyle = BorderStyle.None;

            timer = new Timer();
            timer.Interval = 50;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
            valueChangedHandler = new KineticaRT.Compact.Channels.ValueChangedEventHandler(sourceChannel_ValueChanged);
            updateNeeded = true;

            Height = textBox1.Height;
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
                this.textBox1.Font = value;
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
                this.textBox1.Font = base.Font;
                gr.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Brush brush = new SolidBrush(BackColor);
            Rectangle rect = ClientRectangle;
            e.Graphics.FillRectangle(brush, rect);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (updateNeeded)
            {
                updateNeeded = false;
                if (!textBox1.Focused)
                {
                    textBox1.Text = value.ToString();
                }
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
                    value = System.Convert.ToDouble(sourceChannel.Value);
                }
                catch (System.FormatException)
                {
                    value = System.Double.NaN ;
                }
            }
            updateNeeded = true;
        }


        void sourceChannel_ValueChanged(KineticaRT.Compact.Channels.IChannel sender, KineticaRT.Compact.Values.Value newVal)
        {
            value = sourceChannel.Value.ToDouble() ;
            updateNeeded = true;
        }

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        public override void Disconnect()
        {
            sourceChannel = null;
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
                    updateNeeded = true;
                }
            }
        }
 
        #region HMI Events
        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            UpdateValue();
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            UpdateValue();
        }

        private void UpdateValue()
        {
            try
            {
                double new_value = System.Double.Parse(textBox1.Text);
                if (sourceChannel != null) sourceChannel.Value = new KineticaRT.Compact.Values.Double(new_value);
            }
            catch (System.FormatException)
            {
            }
            if (sourceChannel != null)
            {
                textBox1.Text = sourceChannel.Value.ToString();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                textBox1.Parent.Parent.SelectNextControl(textBox1, true, false, true, true);

                //textBox1.Parent.SelectNextControl(textBox1, true, false, true, true);
                //textBox1.Parent.Focus();
            }
        }

        private void Input_Resize(object sender, EventArgs e)
        {
            Height = textBox1.Height;
        }
        #endregion
    }
}

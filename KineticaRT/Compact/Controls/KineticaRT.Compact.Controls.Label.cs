using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KineticaRT.Compact.Controls
{
    public partial class Label : KineticaRT.Compact.Controls.Control, KineticaRT.Compact.IConnectable, KineticaRT.Compact.IConnectable2 
    {
        public Label()
        {
            InitializeComponent();

        }

        private Rectangle updateRectangle;

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


            Brush text_brush = new SolidBrush(ForeColor);
            //StringFormat drawFormat = new StringFormat();
            //drawFormat.Alignment = StringAlignment.Near;
            //drawFormat.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(value, base.Font, text_brush, updateRectangle, format);
        }


        private StringFormat format = new StringFormat();

        [Category("Label")]
        public StringAlignment ValueVerticalAlignment
        {
            get
            {
                return format.LineAlignment ;
            }
            set
            {
                format.LineAlignment = value;
                Invalidate();
            }
        }

        [Category("Label")]
        [Description("Gets or Sets the horizontal alignment of the text.")]
        public StringAlignment ValueAlignment
        {
            get
            {
                return format.Alignment;
            }
            set
            {
                format.Alignment = value;
                Invalidate();
            }
        }

        private string value = "Label:";


        [Category("Label")]
        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (value != this.value)
                {
                    this.value = value;
                    Invalidate();
                }
            }
        }

        private void Label_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

    }
}

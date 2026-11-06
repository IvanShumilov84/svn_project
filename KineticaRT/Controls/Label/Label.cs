using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace KineticaRT.Controls
{
    public partial class Label : KineticaRT.Controls.Control
    {
        public Label()//:base()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);//doit etre a false
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.Opaque, true);
            this.UpdateStyles();

        }
        public Boolean UseCompatibleTextRendering
        {
            get;
            set;
        }
        [Browsable(true), Category("Text"), Description("Gets or sets the text inside the control")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                //if (PropertyChanged != null)
                //{
                //    PropertyChanged(true);
                //}
                AddToQueue(true);
            }
        }
        #region Drawing


        /// <summary>
        /// Specifies the border style for a uControl.
        /// </summary>
        /// <remarks></remarks>
        public enum BorderStyles : int
        {
            /// <summary>
            /// No border.
            /// </summary>
            /// <remarks></remarks>
            None = 0,
            /// <summary>
            /// A single-line border.
            /// </summary>
            /// <remarks></remarks>
            FixedSingle = 1,
            /// <summary>
            /// A three-dimensional border.
            /// </summary>
            /// <remarks></remarks>
            Fixed3D = 2,
            /// <summary>
            /// A single-line border with options to change border color, adjust width and line style.
            /// </summary>
            /// <remarks></remarks>
            uControlStyle = 3
        }
      
        protected Graphics graphics;
        private Color m_BorderColor = Color.Black;
        private Single m_BorderWidth = 1;
        private BorderStyles m_BorderStyle;
        private System.Drawing.Drawing2D.DashStyle m_BorderDashStyle = DashStyle.Solid;
        private ContentAlignment m_TextAlign = ContentAlignment.MiddleCenter;
        private Image m_Image;
        

        /// <summary>
        /// Method that does all the drawing.
        /// </summary>
        /// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
        /// <param name="g">Graphics objects with which the drawing is done</param>
        public override void Repaint(bool FullRedraw, Graphics g)
        {
            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.GammaCorrected;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawControlBorder(ref g, this.ClientRectangle);
            DrawControlText(ref g, this.ClientRectangle);
            DrawControlImage(ref g, this.ClientRectangle);
         
        }
        /// <summary>
        /// Call this from OnPaint() to Draw Control's Border 
        /// </summary>
        /// <param name="g">Graphics Object Used to Paint</param>
        /// <param name="_ClientRect">The Rectangle that represents the client area of the control.</param>
        /// <remarks></remarks>
        protected void DrawControlBorder(ref Graphics g, Rectangle _ClientRect)
        {
            Pen ObjPen = new Pen(m_BorderColor, m_BorderWidth);

            _ClientRect.Width = _ClientRect.Width - 1;
            _ClientRect.Height = _ClientRect.Height - 1;

            switch (m_BorderStyle)
            {
                case BorderStyles.None:
                    return;

                    break;
                // DO NOTHING
                case BorderStyles.FixedSingle:
                    ObjPen.DashStyle = DashStyle.Solid;
                    ObjPen.Width = 1;
                    ObjPen.Color = Color.Black;
                    break;
                case BorderStyles.Fixed3D:
                    ObjPen.DashStyle = DashStyle.Solid;
                    ObjPen.Width = 2;
                    ObjPen.Color = Color.Gray;
                    g.DrawRectangle(ObjPen, _ClientRect.X, _ClientRect.Y, _ClientRect.Width - 1, _ClientRect.Height - 1);
                    break;
                case BorderStyles.uControlStyle:
                    ObjPen.DashStyle = m_BorderDashStyle;
                    break;
            }

            g.DrawRectangle(ObjPen, _ClientRect);
            ObjPen.Dispose();

        }
        /// <summary>
        /// Specifies alignment of <see cref="Text"></see> on the control. Placement and direction of text in relation to the control border 
        /// </summary>
        /// <value></value>
        /// <returns>The TextAlign As <see cref="System.Drawing.ContentAlignment"></see>The default is the value of the <see cref="TextAlign"></see> property.</returns>
        /// <remarks></remarks>
        [Browsable(true), Category("Text"), Description("Gets or sets the alignment of text inside the control")]
        public ContentAlignment TextAlign
        {
            get
            {
                return m_TextAlign;
            }
            set
            {
                m_TextAlign = value;
                AddToQueue(true);
            }
        }
        /// <summary>
        /// Call this from OnPaint() to Draw Control's Text
        /// </summary>
        /// <param name="g">Graphics Object Used to Paint</param>
        /// <remarks></remarks>
        protected void DrawControlText(ref Graphics g, Rectangle _ClientRect)
        {
            if (((m_Image != null)))
                _ClientRect.Width = _ClientRect.Width - 16;
            Rectangle _TextRect = GetTextRectangle(this.Text, this.Font, ref g, _ClientRect, this.TextAlign);
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), _TextRect);
        }

        /// <summary>
        /// Call this from OnPaint() to Draw Control's Image
        /// </summary>
        /// <param name="g">Graphics Object Used to Paint</param>
        /// <param name="_ClientRect">The Rectangle that represents the client area of the control.</param>
        /// <remarks></remarks>
        protected void DrawControlImage(ref Graphics g, Rectangle _ClientRect)
        {
            if (m_Image != null)
            {
                //g.DrawImage(m_Image, (_ClientRect.Width - 18), System.Convert.ToInt32((_ClientRect.Height / 2) - 8), this.Width, this.Height);
                Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
                //Rectangle rect = new Rectangle(0, 0, m_Image.Width, m_Image.Height);
                g.DrawImage(m_Image, rect);
            }
        }
        /// <summary>
        /// Get(s) the Text Rectangle to draw String, Call this from OnPaint() to Draw Control's Text
        /// </summary>
        /// <param name="_Text">Gets or sets the Text of the control, Represents Text as a series of Unicode characters.</param>
        /// <param name="_Font">System.Drawing.Font, Defines a particular format for text, including font face, size, and style attributes. </param>
        /// <param name="g">Graphics Object Used to Paint</param>
        /// <param name="_ClientRect">The Rectangle that represents the client area of the control.</param>
        /// <param name="_TextAlign">Specifies alignment of text on the control.</param>
        /// <returns></returns>
        /// <remarks>Public Enum System.Drawing.ContentAlignment As Integer</remarks>
        protected Rectangle GetTextRectangle(string _Text, Font _Font, ref Graphics g, Rectangle _ClientRect, ContentAlignment _TextAlign)
        {
            SizeF _TextSize = g.MeasureString(_Text, _Font);
            int _TextLeft = 0;
            _TextSize.Width = _TextSize.Width + 4;

            switch (_TextAlign)
            {
                case ContentAlignment.TopLeft:
                    return _ClientRect;
                case ContentAlignment.TopCenter:
                    _TextLeft = (_ClientRect.Width / 2) - (System.Convert.ToInt32((_TextSize.Width)) / 2);
                    return new Rectangle((_ClientRect.Left + _TextLeft), _ClientRect.Top, System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
                case ContentAlignment.TopRight:
                    _TextLeft = System.Convert.ToInt32(_ClientRect.Width - _TextSize.Width);
                    return new Rectangle((_ClientRect.Left + _TextLeft), _ClientRect.Top, System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
                case ContentAlignment.MiddleLeft:
                    return new Rectangle(_ClientRect.Left, System.Convert.ToInt32((_ClientRect.Height / 2)) - System.Convert.ToInt32((_TextSize.Height / 2)), System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
                case ContentAlignment.MiddleCenter:
                    _TextLeft = (_ClientRect.Width / 2) - (System.Convert.ToInt32((_TextSize.Width)) / 2);
                    return new Rectangle((_ClientRect.Left + _TextLeft), System.Convert.ToInt32((_ClientRect.Height / 2)) - System.Convert.ToInt32((_TextSize.Height / 2)), System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
                case ContentAlignment.MiddleRight:
                    _TextLeft = System.Convert.ToInt32(_ClientRect.Width - _TextSize.Width);
                    return new Rectangle((_ClientRect.Left + _TextLeft), System.Convert.ToInt32((_ClientRect.Height / 2)) - System.Convert.ToInt32((_TextSize.Height / 2)), System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
                case ContentAlignment.BottomLeft:
                    return new Rectangle(_ClientRect.Left, System.Convert.ToInt32((_ClientRect.Bottom - _TextSize.Height)), System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
                case ContentAlignment.BottomCenter:
                    _TextLeft = (_ClientRect.Width / 2) - (System.Convert.ToInt32((_TextSize.Width)) / 2);
                    return new Rectangle((_ClientRect.Left + _TextLeft), System.Convert.ToInt32((_ClientRect.Bottom - _TextSize.Height)), System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
                case ContentAlignment.BottomRight:
                    _TextLeft = System.Convert.ToInt32(_ClientRect.Width - _TextSize.Width);
                    return new Rectangle((_ClientRect.Left + _TextLeft), System.Convert.ToInt32((_ClientRect.Bottom - _TextSize.Height)), System.Convert.ToInt32((_TextSize.Width)), System.Convert.ToInt32((_TextSize.Height)));
            }
            return ClientRectangle;
        }
        /// <summary>
        /// Gets or sets the Image of the control, An abstract base class that provides functionality for the <see cref="Bitmap"></see> and Metafile descended classes.
        /// </summary>
        /// <value></value>
        /// <returns>The Image As <see cref="System.Drawing.Image"></see> of the control. The default is the value of the <see cref="iGreen.Controls.uControls.uLabelX.uLabelX.Image"></see> property.</returns>
        /// <remarks></remarks>
        [Browsable(true), Category("Image"), Description("Gets or sets the specified image / logo for the control.")]
        public Image Image
        {
            get { return m_Image; }
            set
            {
                m_Image = value;
                AddToQueue(true);
            }
        }
      
        /// <summary>
        /// Gets or sets the BorderDashStyle of the control, Specifies the border dash style of Control
        /// </summary>
        /// <value></value>
        /// <returns>The BorderDashStyle As <see cref="System.Drawing.Drawing2D.DashStyle"></see> of the control. The default is the value of the iGreen.Controls.uControls.uLabelX.BorderDashStyle property.</returns>
        /// <remarks></remarks>
        [Browsable(true), Category("Border"), Description("Gets or sets the specified border dash style for the style.")]
        public System.Drawing.Drawing2D.DashStyle BorderDashStyle
        {
            get { return m_BorderDashStyle; }
            set
            {
                m_BorderDashStyle = value;
                AddToQueue(true);
            }
        }
        #endregion
    
        //step 1
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }
        ////step 2
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do not allow the background to be painted 
        }
        //step 3
        //protected void InvalidateEx()
        //{
        //    if (Parent == null)
        //        return;
        //    Rectangle rc = new Rectangle(this.Location, this.Size);
        //    Parent.Invalidate(rc, true);
        //}

    }
}



using System;
using System.Drawing;
using System.ComponentModel;

using KineticaRT.Compact ;



namespace KineticaRT.Compact.Controls
{
    /// <summary>
    /// The KineticaRT Frame is a Control Container, which allows run-time editing
    /// of control layout and properties.
    /// </summary>
    [ChangeRecordAttribute("1.0", "17 02 2003", "S. Child", "Major Changes")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2003 KineticaRT Ltd.")]

    public partial class Frame : KineticaRT.Compact.Controls.FrameBase, ISupportInitialize
    {
        #region Variables
        //property variables
        private Color titleLeftColor;
        private Color titleRightColor;
        private Color titleUnderlineColor;
        private bool titleUnderlined;
        private string titleText;
        private string panelName;

        //other variables
        private StringFormat titleFormat;

        ///// <summary>
        ///// Required designer variable.
        ///// </summary>
        //private System.ComponentModel.Container components = null;
        #endregion

        #region Initialisation and Disposal
        /// <summary>
        /// Initialises a new instance of the Frame class
        /// </summary>
        public Frame()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            titleText = "KineticaRT.Controls.Frame";
            titleUnderlined = true;
            titleUnderlineColor = Color.SteelBlue;
            titleLeftColor = Color.White;
            titleRightColor = Color.LightSteelBlue;
            panelName = "";

            titleFormat = new StringFormat();
            titleFormat.LineAlignment = StringAlignment.Center;

            //ensure we are never without a panel, even though this may well
            //be overwritten by the setting of PanelName
            NewPanel(new PanelBase(), true);
        }

        ///// <summary>
        ///// Clean up any resources being used.
        ///// </summary>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        titleFormat.Dispose();

        //        if (components != null)
        //            components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        #endregion

        /// <summary>
        /// Gets or sets the Controls font.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the Controls FontSize.
        /// </summary>
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



        #region ISupportInitialize
        /// <summary>
        /// Initialization is complete
        /// </summary>
        public override void EndInit()
        {
            base.EndInit();

//            if (!this.DesignMode)
            {
                LoadPanel();
            }

            if (currentPanel == null)
            {
                NewPanel(new PanelBase(), true);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the PanelBase object in which the controls are held
        /// </summary>
        [Category("Frame")]
//        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual PanelBase Panel
        {
            get
            {
                return currentPanel;
            }
        }
        private bool ShouldSerializePanel()
        {
            return (panelName == "");
        }

        /// <summary>
        /// Gets or sets whether the Title bar is underlined
        /// </summary>
        [Category("Frame")]
        [DefaultValue(true)]
        [Description("Gets or sets whether the Title bar is underlined")]
        public bool TitleUnderlined
        {
            get
            {
                return titleUnderlined;
            }
            set
            {
                if (titleUnderlined != value)
                {
                    titleUnderlined = value;
                    needCalcTitle = true;
                    if (TitleEnabled) AddToQueue(true);
                    OnPropertyChanged("TitleUnderlined");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Text in the Title bar
        /// </summary>
        [Category("Frame")]
        [DefaultValue("KineticaRT.Controls.Frame")]
        [Description("Gets or sets the Text in the Title bar")]
        public string TitleText
        {
            get
            {
                return titleText;
            }
            set
            {
                if (titleText != value)
                {
                    titleText = value;
                    if (TitleEnabled) AddToQueue(false);
                    OnPropertyChanged("TitleText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Color at the left hand side of the title bar
        /// </summary>
        [Category("Frame")]
        [Description("Gets or sets the Color at the left hand side of the title bar")]
        public Color TitleLeftColor
        {
            get
            {
                return titleLeftColor;
            }
            set
            {
                if (titleLeftColor != value)
                {
                    titleLeftColor = value;
                    if (TitleEnabled) AddToQueue(false);
                    OnPropertyChanged("TitleLeftColor");
                }
            }
        }
        private bool ShouldSerializeTitleLeftColor()
        {
            return titleLeftColor != Color.White;
        }
        /// <summary>
        /// Reset the Color to its default value
        /// </summary>
        public void ResetTitleLeftColor()
        {
            titleLeftColor = Color.White;
        }

        /// <summary>
        /// Gets or sets the Color at the right hand side of the title bar
        /// </summary>
        [Category("Frame")]
        [Description("Gets or sets the Color at the right hand side of the title bar")]
        public Color TitleRightColor
        {
            get
            {
                return titleRightColor;
            }
            set
            {
                if (titleRightColor != value)
                {
                    titleRightColor = value;
                    if (TitleEnabled) AddToQueue(false);
                    OnPropertyChanged("TitleRightColor");
                }
            }
        }
        private bool ShouldSerializeTitleRightColor()
        {
            return titleRightColor != Color.LightSteelBlue;
        }
        /// <summary>
        /// Reset the Color to its default value
        /// </summary>
        public void ResetTitleRightColor()
        {
            titleRightColor = Color.LightSteelBlue;
        }

        /// <summary>
        /// Gets or sets the Color in which the title bar is underlined
        /// </summary>
        [Category("Frame")]
        [Description("Gets or sets the Color in which the title bar is underlined")]
        public Color TitleUnderlineColor
        {
            get
            {
                return titleUnderlineColor;
            }
            set
            {
                if (titleUnderlineColor != value)
                {
                    titleUnderlineColor = value;
                    if (TitleEnabled) AddToQueue(false);
                    OnPropertyChanged("TitleUnderlineColor");
                }
            }
        }
        private bool ShouldSerializeTitleUnderlineColor()
        {
            return titleUnderlineColor != Color.SteelBlue;
        }
        /// <summary>
        /// Reset the Color to its default value
        /// </summary>
        public void ResetTitleUnderlineColor()
        {
            titleUnderlineColor = Color.SteelBlue;
        }

        /// <summary>
        /// Gets or sets the name of the current Panel dll
        /// </summary>
        /// <remarks>
        /// Extensions other than '.dll' will be changed to '.dll'.
        /// If path is not specified, the file is relative to the path specified in the SystemDirectory property.
        /// If the specified file exists it will be loaded, and current controls
        /// will be unloaded. If the file does not exist the current controls will
        /// remain unchanged. When the frame is saved, current controls will be saved
        /// to the file specified here, and the object saved within the dll will be
        /// case sensitively named after the dll. Note that save will not be available if no 
        /// PanelName is specified.
        /// </remarks>
        [Category("Frame")]
        [DefaultValue("")]
        [Description("Name of Panel DLL to load")]
        //[Editor(typeof(KineticaRT.Controls.DllNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public virtual string PanelName
        {
            get
            {
                return panelName;
            }
            set
            {
                value = System.IO.Path.ChangeExtension(value, ".dll");

                panelName = value;

                if (/*!this.DesignMode && */ !initialising)
                {
                    LoadPanel();
                }

                OnPropertyChanged("PanelName");
            }
        }
        #endregion (properties)

        #region Drawing
        /// <summary>
        /// Calculates the vertical space needed for the title
        /// - includes the underline if required
        /// </summary>
        protected override void CalculateTitleHeight()
        {
            titleHeight = (int)this.Font.Size + 6;
            if (titleUnderlined) titleHeight += 2;
        }

        /// <summary>
        /// Draws the title bar
        /// </summary>
        /// <param name="g">Graphics object with which to draw</param>
        protected override void DrawTitle(Graphics g)
        {
            if (!TitleEnabled) return;

            Brush brush;
            Rectangle rect = titleRect;

            //underline?
            if (titleUnderlined)
            {
                brush = new SolidBrush(titleUnderlineColor);
                g.FillRectangle(brush, rect.X, rect.Bottom - 2, rect.Width, 2);
                brush.Dispose();
                rect.Height -= 2;
            }

//            brush = new LinearGradientBrush(rect, titleLeftColor, titleRightColor, LinearGradientMode.Horizontal);
            brush = new SolidBrush (titleLeftColor);

            g.FillRectangle(brush, rect);
            brush.Dispose();

            //sort out bezel here if required

            //add text
            brush = new SolidBrush(ForeColor);
            g.DrawString(titleText, this.Font, brush, rect, titleFormat);
            brush.Dispose();
        }
        #endregion (drawing)

        #region Public Methods
        ///// <summary>
        ///// Saves the Panel to the named dll. Does nothing if no PanelName
        ///// </summary>
        //public override void SavePanel()
        //{
        //    if (panelName.Length > 0 && currentPanel != null)
        //    {
        //        currentPanel.Save(Path.Combine(SystemDirectory, panelName), false);
        //    }
        //}

        ///// <summary>
        ///// Returns whether the currentPanel can be saved.
        ///// </summary>
        //public override bool CanSave()
        //{
        //    return (panelName.Length > 0 && currentPanel != null);
        //}
        #endregion (public methods)

        #region Other Methods
        /// <summary>
        /// Loads a new panel using the PanelName property
        /// </summary>
        /// <returns>True if succeeded and got a new Panel</returns>
        protected bool LoadPanel()
        {
            PanelBase new_panel = LoadPanelFromName(panelName, false);

            if (new_panel != null)
            {
                NewPanel(new_panel, true);
                return true;
            }
            return false;
        }
        #endregion (other methods)

        #region Events
        ///// <summary>
        ///// RightToLeft changed event
        ///// </summary>
        ///// <param name="e">Event data</param>
        //protected override void OnRightToLeftChanged(EventArgs e)
        //{
        //    if (RightToLeft == RightToLeft.Yes)
        //    {
        //        titleFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
        //    }
        //    else
        //    {
        //        titleFormat.FormatFlags = 0;
        //    }

        //    base.OnRightToLeftChanged(e);
        //}
        #endregion (events)
    }
}

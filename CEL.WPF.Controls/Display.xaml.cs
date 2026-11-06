using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing.Drawing2D;

using KineticaRT;
using KineticaRT.Channels;
using System.ComponentModel;
using System.Windows.Threading;

namespace CEL.WPF.Controls
{

   
 

    //public static class RectConversion
    //{
    //    public static System.Drawing.Rectangle
    //        ToRectangle(this Rect value)
    //    {
    //        System.Drawing.Rectangle result =
    //            new System.Drawing.Rectangle();
    //        result.X = (int)value.X;
    //        result.Y = (int)value.Y;
    //        result.Width = (int)value.Width;
    //        result.Height = (int)value.Height;
    //        return result;
    //    }

    //    public static Rect
    //        ToRect(this System.Drawing.Rectangle value)
    //    {
    //        Rect result = new Rect();
    //        result.X = value.X;
    //        result.Y = value.Y;
    //        result.Width = value.Width;
    //        result.Height = value.Height;
    //        return result;
    //    }
    //}
    /// <summary>
    /// Interaction logic for Display.xaml
    /// </summary>
    public partial class Display :  Controls.Control
    {

        #region Variables
        //objects
        //tb private System.Windows.Forms.TextBox textBox;
        private System.Windows.Controls.TextBox textBox2;

        //properties
        private string text;
        private object value;
        private KineticaRT.Channels.IChannel sourceChannel;
        private KineticaRT.Channels.IChannel destinationChannel;
        private string source;
        private string destination;
        //private System.Drawing.ContentAlignment textAlign;
        private TextAlignment textAlign;
        private bool spinButtonsEnabled;
        private double spinIncrement;
        private double minimum;
        private double maximum;
        private Color percentBarColor;
        private bool percentBarEnabled;
        private bool numericOnly;
        private KineticaRT.Controls.TextOrientations orientation; //TODO
        private bool useSourceFormat;
        private bool inputEnabled;
        private string format;
       

        //internal variables
        private KineticaRT.Channels.ValueChangedEventHandler srcHdlr;
        private PropertyChangedEventHandler propertyHdlr;

        private bool resizeBox;
        private bool editing;
        private bool textChanged;
        private string shortText;
        private KineticaRT.Values.Value krtValue;
        private Rect textRect;
        private System.Drawing.StringFormat textFormat;

        private Rect spinUpRect;
        private Rect spinDownRect;
        private bool spinning;
        private bool spinUpPressed;
        private bool spinDownPressed;
        private int spinIncrementCount;
        private System.Timers.Timer spinTimer;

        private bool barValid;
        private Rect barRect;
        private bool barDragging;

        private bool busySettingValue;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        //ft private System.ComponentModel.Container components = null;
        #endregion

      
        public Display()
        {
            InitializeComponent();
            //ft InitializeComponent2();//ft n<est plus necessaire


            value = new KineticaRT.Values.Text("");
            text = "";
            sourceChannel = null;
            destinationChannel = null;
            source = "";
            destination = "";
            textAlign = TextAlignment.Left;//ft System.Drawing.ContentAlignment.MiddleLeft;
            spinButtonsEnabled = false;
            spinIncrement = 1;
            minimum = System.Double.NegativeInfinity;
            maximum = System.Double.PositiveInfinity;

            System.Drawing.Color sdColor = System.Drawing.Color.Blue; //ft
            percentBarColor = sdColor.ToMediaColor(); //ft
            percentBarEnabled = false;
            numericOnly = false;
            orientation = KineticaRT.Controls.TextOrientations.Right;
            useSourceFormat = true;
            inputEnabled = true;
            format = "";
            backBrush = Brushes.Black;


            resizeBox = true;
            editing = false;
            textChanged = true;
            shortText = "";
            krtValue = new KineticaRT.Values.Text("");
            textFormat = new System.Drawing.StringFormat();
            textFormat.Trimming = System.Drawing.StringTrimming.None;
            textFormat.Alignment = System.Drawing.StringAlignment.Near;
            textFormat.LineAlignment = System.Drawing.StringAlignment.Center;

            spinUpRect = Rect.Empty; 
            spinDownRect = Rect.Empty;
            spinning = false;
            spinUpPressed = false;
            spinDownPressed = false;
            spinIncrementCount = 0;
            spinTimer = new System.Timers.Timer(slowPool.UpdateRate);

            barRect = Rect.Empty;
            barValid = false;
            barDragging = false;

            textRect = Rect.Empty;

            srcHdlr = new KineticaRT.Channels.ValueChangedEventHandler(OnValueChanged);
            propertyHdlr = new PropertyChangedEventHandler(OnSourcePropertyChanged);

            //			textBox.Visible = false ;
            //tb		//connect to events from text box
            //			textBox.GotFocus	+= new EventHandler(OnBoxGotFocus) ;
            //			textBox.LostFocus	+= new EventHandler(OnBoxLostFocus) ;
            //			textBox.KeyPress	+= new KeyPressEventHandler(OnBoxKeyPress) ;
            //			textBox.Validating	+= new CancelEventHandler(OnBoxValidating) ;

            //and timer
            spinTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnSpinTimerElapsed);

            //ft this.SetStyle(ControlStyles.Selectable, true);


            this.BorderType = KineticaRT.Controls.BorderType.Indented;
            //tb			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.textBox});
            this.Cursor = Cursors.IBeam;
            this.Name = "Display";
            //ft this.Size = new System.Drawing.Size(150, 24);
            //ft this.Width = 150;
            //ft this.Height = 24;
            
            this.Width = double.NaN; ;
            this.Height = double.NaN; ;
        }

        ///// <summary>
        ///// Required method for Designer support - do not modify 
        ///// the contents of this method with the code editor.
        ///// </summary>
        //private void InitializeComponent2()
        //{
        //    //tb this.textBox = new System.Windows.Forms.TextBox();
        //    //ft this.SuspendLayout();
        //    //tb			// 
        //    //			// textBox
        //    //			// 
        //    //			this.textBox.AutoSize = false;
        //    //			this.textBox.BackColor = System.Drawing.SystemColors.Control;
        //    //			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
        //    //			this.textBox.Name = "textBox";
        //    //			this.textBox.Size = new System.Drawing.Size(100, 20);
        //    //			this.textBox.TabIndex = 0;
        //    //			this.textBox.Text = "";
        //    //			this.textBox.WordWrap = false;
        //    // 
        //    // Display
        //    // 
        //    //ft this.BorderType = KineticaRT.Controls.BorderType.Indented;
        //    //tb			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.textBox});
        //    //ft this.Cursor = Cursors.IBeam;
        //    //ft this.Name = "Display";
        //    //ft this.Size = new System.Drawing.Size(150, 24);
        //    //ft this.Height = 24;
        //    //ft this.Width = 150;

        //    //ft this.ResumeLayout(false);

        //}
//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        protected override void Dispose(bool disposing)
//        {
//#if KRT_license
//            if (license != null) {license.Dispose();license = null;}
//#endif

//            if (disposing)
//            {
//                if (components != null)
//                    components.Dispose();
//            }
//            base.Dispose(disposing);
//        }



        #region Properties


        /// <summary>
        /// Gets or Sets the type of the Control's Border
        /// </summary>
        [Category("Control")]
        [DefaultValue(KineticaRT.Controls.BorderType.Indented)]
        [Description("Gets or Sets the typeof the Control's Border")]
        public override KineticaRT.Controls.BorderType BorderType
        {
            get { return base.BorderType; }
            set { base.BorderType = value; }
        }

        /// <summary>
        /// Gets or sets the Cursor that appears when the mouse passes over the control
        /// </summary>
        //[Category("Appearance")]
        //[Description("Gets or sets the Cursor that appears when the mouse passes over the control")]
        //public override Cursor Cursor
        //{
        //    get { return base.Cursor; }
        //    set { base.Cursor = value; }
        //}
        private bool ShouldSerializeCursor()
        {
            return (base.Cursor != (inputEnabled ? Cursors.IBeam : Cursors.Arrow));
        }

        /// <summary>
        /// Gets or sets the text corresponding to the currect Value
        /// </summary>
        [Category("Display")]
        [Browsable(true)]
        [DefaultValue("")]
        [Description("Gets or sets the text corresponding to the currect Value")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                SetText(value, true);
            }
        }
        //public static readonly DependencyProperty TextProperty =
        //    DependencyProperty.Register("Text",
        //    typeof(string),
        //    typeof(Display),
        //    new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.AffectsRender));


        /// <summary>
        /// Gets or sets the object that is the Source of the display's value
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets the object that is the Source of the display's value")]
        public KineticaRT.Channels.IChannel SourceChannel
        {
            get
            {
                return sourceChannel;
            }
            set
            {
                if (sourceChannel != value)
                {
                    //free old handler if any
                    if (sourceChannel != null)
                    {
                        sourceChannel.ValueChanged -= srcHdlr;
                        sourceChannel.PropertyChanged -= propertyHdlr;
                    }

                    //attach new sourceChannel
                    sourceChannel = value;
                    if (sourceChannel != null)
                    {
                        sourceChannel.ValueChanged += srcHdlr;
                        sourceChannel.PropertyChanged += propertyHdlr;

                        SetValue(sourceChannel.Value, true);
                        SetFormat();
                    }
                    OnPropertyChanged("SourceChannel");
                }
            }
        }
        private bool ShouldSerializeSourceChannel()
        {
            return false;
        }

        /// <summary>
        /// Gets or sets the object that is the Destination for the display's value
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets the object that is the Destination for the display's value")]
        public KineticaRT.Channels.IChannel DestinationChannel
        {
            get
            {
                return destinationChannel;
            }
            set
            {
                if (destinationChannel != value)
                {
                    destinationChannel = value;

                    if (destinationChannel != null) destinationChannel.Value = krtValue;

                    OnPropertyChanged("DestinationChannel");
                }
            }
        }
        private bool ShouldSerializeDestinationChannel()
        {
            return false;
        }

        /// <summary>
        /// Gets or sets the name of the object to be the Source
        /// </summary>
        [Category("Display")]
        [DefaultValue("")]
        [Description("Gets or sets the name of the object to be the Source")]
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
                    OnPropertyChanged("Source");
                }
                //try and connect to source
                SourceChannel = OnObjectSearch(source) as KineticaRT.Channels.IChannel;
            }
        }

        /// <summary>
        /// Gets or sets the name of object to be the Destination
        /// </summary>
        [Category("Display")]
        [DefaultValue("")]
        [Description("Gets or sets the name of object to be the Destination")]
        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                if (destination != value)
                {
                    destination = value;
                    OnPropertyChanged("Destination");
                }
                //try and connect to destination
                DestinationChannel = OnObjectSearch(destination) as KineticaRT.Channels.IChannel;
            }
        }

        /// <summary>
        /// Gets or sets the current Value
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets the current Value")]
        public object Value
        {
            get
            {
                return this.value;
            }
            set
            {
                //				if (this.value != value)
                //				{
                //					Values.Value		new_krt_value ;
                //
                //					this.value = value;
                //
                //					//is it already a KRT Value?
                //					new_krt_value = value as Values.Value ;
                //					//how about a standard double or string?
                //					if ((new_krt_value == null) && (value is System.Double)) new_krt_value = new Values.Double ((System.Double)value) ;
                //					if ((new_krt_value == null) && (value is System.String)) new_krt_value = new Values.Text ((System.String)value) ;
                //					//otherwise make it a string
                //					if (new_krt_value == null)
                //					{
                //						if (value != null)
                //						{
                //							new_krt_value = new Values.Text(value.ToString()) ;
                //						}
                //						else
                //						{
                //							new_krt_value = new Values.Text("") ;
                //						}
                //					}
                //
                //					if (new_krt_value.CompareTo (krtValue) != 0)
                //					{
                //						krtValue = new_krt_value ;
                //
                //						if (destinationChannel != null)		destinationChannel.Value = krtValue ;
                //
                //						//is this right??? what if the value wasn't set by the source?
                //						if (sourceChannel != null)
                //						{
                //							//make the source convert itself to a formatted string
                ////							SetText(sourceChannel.ToString(), false) ;
                //							SetText(sourceChannel.ToString(), true) ;
                //						}
                //						else if (krtValue != null)
                //						{
                //							SetText(krtValue.ToString(), false) ;
                //						}
                //						else
                //						{
                //							SetText("", false) ;
                //						}
                //
                //						AddToQueue(false) ;
                ////						OnValueChanged() ;
                //					}
                //					OnPropertyChanged("Value") ;
                //				}
                SetValue(value, false);
            }
        }
        private bool ShouldSerializeValue()
        {
            return false;		//save text string but not value object
        }

        /// <summary>
        /// Gets or sets the Horizontal and Vertical alignment of the text
        /// </summary>
        [Category("Display")]
        [DefaultValue(TextAlignment.Left)]
        [Description("Gets or sets the Horizontal and Vertical alignment of the text")]
        public TextAlignment TextAlign
        {
            get
            {
                return textAlign;
            }
            set
            {
                if (textAlign != value)
                {
                    textAlign = value;
                    switch (textAlign)
                    {
                        //ft case System.Drawing.ContentAlignment.BottomLeft:
                        case TextAlignment.Left:
                            {
                                //tb						textBox.TextAlign		= HorizontalAlignment.Left ;
                                textFormat.LineAlignment = System.Drawing.StringAlignment.Far;
                                textFormat.Alignment = System.Drawing.StringAlignment.Near;
                            }
                            break;

                        //ft case System.Drawing.ContentAlignment.MiddleLeft:
                        case TextAlignment.Center:
                            {
                                //tb						textBox.TextAlign		= HorizontalAlignment.Left ;
                                textFormat.LineAlignment = System.Drawing.StringAlignment.Center;
                                textFormat.Alignment = System.Drawing.StringAlignment.Near;
                            }
                            break;

                        //ft case System.Drawing.ContentAlignment.TopLeft:
                        case TextAlignment.Right:
                            {
                                //tb						textBox.TextAlign		= HorizontalAlignment.Left ;
                                textFormat.LineAlignment = System.Drawing.StringAlignment.Near;
                                textFormat.Alignment = System.Drawing.StringAlignment.Near;
                            }
                            break;

                        //case System.Drawing.ContentAlignment.BottomCenter:
                        //    {
                        //        //tb						textBox.TextAlign		= HorizontalAlignment.Center ;
                        //        textFormat.LineAlignment = System.Drawing.StringAlignment.Far;
                        //        textFormat.Alignment = System.Drawing.StringAlignment.Center;
                        //    }
                        //    break;

                        //case System.Drawing.ContentAlignment.MiddleCenter:
                        //    {
                        //        //tb						textBox.TextAlign		= HorizontalAlignment.Center ;
                        //        textFormat.LineAlignment = System.Drawing.StringAlignment.Center;
                        //        textFormat.Alignment = System.Drawing.StringAlignment.Center;
                        //    }
                        //    break;

                        //case System.Drawing.ContentAlignment.TopCenter:
                        //    {
                        //        //tb						textBox.TextAlign		= HorizontalAlignment.Center ;
                        //        textFormat.LineAlignment = System.Drawing.StringAlignment.Near;
                        //        textFormat.Alignment = System.Drawing.StringAlignment.Center;
                        //    }
                        //    break;

                        //case System.Drawing.ContentAlignment.BottomRight:
                        //    {
                        //        //tb						textBox.TextAlign		= HorizontalAlignment.Right ;
                        //        textFormat.LineAlignment = System.Drawing.StringAlignment.Far;
                        //        textFormat.Alignment = System.Drawing.StringAlignment.Far;
                        //    }
                        //    break;

                        //case System.Drawing.ContentAlignment.MiddleRight:
                        //    {
                        //        //tb						textBox.TextAlign		= HorizontalAlignment.Right ;
                        //        textFormat.LineAlignment = System.Drawing.StringAlignment.Center;
                        //        textFormat.Alignment = System.Drawing.StringAlignment.Far;
                        //    }
                        //    break;

                        //case System.Drawing.ContentAlignment.TopRight:
                        //    {
                        //        //tb						textBox.TextAlign		= HorizontalAlignment.Right ;
                        //        textFormat.LineAlignment = System.Drawing.StringAlignment.Near;
                        //        textFormat.Alignment = System.Drawing.StringAlignment.Far;
                        //    }
                        //    break;
                    }

                    resizeBox = true;
                    AddToQueue(false);
                    OnPropertyChanged("TextAlign");
                }
            }
        }
        ///// <summary>
        ///// Gets or sets the Horizontal and Vertical alignment of the text
        ///// </summary>
        //[Category("Display")]
        //[DefaultValue(System.Drawing.ContentAlignment.MiddleLeft)]
        //[Description("Gets or sets the Horizontal and Vertical alignment of the text")]
        //public System.Drawing.ContentAlignment TextAlign
        //{
        //    get
        //    {
        //        return textAlign;
        //    }
        //    set
        //    {
        //        if (textAlign != value)
        //        {
        //            textAlign = value;
        //            switch (textAlign)
        //            {
        //                case System.Drawing.ContentAlignment.BottomLeft:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Left ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Far;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Near;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.MiddleLeft:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Left ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Center;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Near;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.TopLeft:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Left ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Near;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Near;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.BottomCenter:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Center ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Far;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Center;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.MiddleCenter:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Center ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Center;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Center;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.TopCenter:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Center ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Near;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Center;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.BottomRight:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Right ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Far;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Far;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.MiddleRight:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Right ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Center;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Far;
        //                    }
        //                    break;

        //                case System.Drawing.ContentAlignment.TopRight:
        //                    {
        //                        //tb						textBox.TextAlign		= HorizontalAlignment.Right ;
        //                        textFormat.LineAlignment = System.Drawing.StringAlignment.Near;
        //                        textFormat.Alignment = System.Drawing.StringAlignment.Far;
        //                    }
        //                    break;
        //            }

        //            resizeBox = true;
        //            AddToQueue(false);
        //            OnPropertyChanged("TextAlign");
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets whether Spin Buttons are enabled
        /// </summary>
        /// <remarks>Spin buttons are used to increment and decrement numeric values</remarks>
        [Category("Display")]
        [DefaultValue(false)]
        [Description("Gets or sets whether Spin Buttons are enabled")]
        public bool SpinButtonsEnabled
        {
            get
            {
                return spinButtonsEnabled;
            }
            set
            {
                if (spinButtonsEnabled != value)
                {
                    spinButtonsEnabled = value;
                    resizeBox = true;
                    //ft SetWindowRegion();
                    AddToQueue(true);
                    OnPropertyChanged("SpinButtonsEnabled");
                }
            }
        }

        bool useSourceValue = true;
        /// <summary>
        /// Gets or sets whether to display text from the source channel or only from the value property.
        /// </summary>
        /// <remarks>whether to display text from the source channel or only from the value property.</remarks>
        [Category("Display")]
        [DefaultValue(true)]
        [Description("Gets or sets whether Spin Buttons are enabled")]
        public bool UseSourceValue
        {
            get
            {
                return useSourceValue;
            }
            set
            {
                if (useSourceValue != value)
                {
                    useSourceValue = value;
                    OnPropertyChanged("UseSourceValue");
                }
            }
        }

        /// <summary>
        /// Gets or sets the amount by which the Spin Buttons increment or decrement the value
        /// </summary>
        [Category("Display")]
        [DefaultValue(1.0)]
        [Description("Gets or sets the amount by which the Spin Buttons increment or decrement the value")]
        public double SpinIncrement
        {
            get
            {
                return spinIncrement;
            }
            set
            {
                if (spinIncrement != value)
                {
                    spinIncrement = value;
                    OnPropertyChanged("SpinIncrement");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Minimum numeric value
        /// </summary>
        /// <remarks>This value is only used to restrict user input. It does not restrict values from a Source channel</remarks>
        [Category("Display")]
        [DefaultValue(System.Double.NegativeInfinity)]
        [Description("Gets or sets the Minimum numeric value")]
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
                    AddToQueue(false);
                    OnPropertyChanged("Minimum");
                }
            }
        }

        /// <summary>
        /// Gets r sets the Maximum numeric value
        /// </summary>
        /// <remarks>This value is only used to restrict user input. It does not restrict values from a Source channel</remarks>
        [Category("Display")]
        [DefaultValue(System.Double.PositiveInfinity)]
        [Description("Gets or sets the Maximum numeric value")]
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
                    AddToQueue(false);
                    OnPropertyChanged("Maximum");
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the Percent Bar is enabled
        /// </summary>
        /// <remarks>
        /// The Percent bar is only displayed if the Minimum and Maximum value 
        /// are not infinite. In this case the bar will show the current value as
        /// a percentage of the full range allowed.
        /// </remarks>
        [Category("Display")]
        [DefaultValue(false)]
        [Description("Gets or sets whether the Percent Bar is enabled")]
        public bool PercentBarEnabled
        {
            get
            {
                return percentBarEnabled;
            }
            set
            {
                if (percentBarEnabled != value)
                {
                    percentBarEnabled = value;
                    resizeBox = true;
                    //ft SetWindowRegion();
                    AddToQueue(true);
                    OnPropertyChanged("PercentBarEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Color of the Percent bar
        /// </summary>
        /// <remarks>
        /// The Percent bar is only displayed if the Minimum and Maximum value 
        /// are not infinite. In this case the bar will show the current value as
        /// a percentage of the full range allowed.
        /// </remarks>
        [Category("Display")]
        [Description("Gets or sets the color of the Percent Bar")]
        public Color PercentBarColor
        {
            get
            {
                return percentBarColor;
            }
            set
            {
                if (percentBarColor != value)
                {
                    percentBarColor = value;
                    AddToQueue(false);
                    OnPropertyChanged("PercentBarColor");
                }
            }
        }
        private bool ShouldSerializePercentBarColor()
        {
            return (percentBarColor != Colors.Blue);
        }
        /// <summary>
        /// Reset the Color to its default Value
        /// </summary>
        public void ResetPercentBarColor()
        {
            percentBarColor = Colors.Blue;
        }

        /// <summary>
        /// Gets or sets whether to restrict user input to valid numeric values
        /// </summary>
        /// <remarks>Doesn't restrict values from Source channels</remarks>
        [Category("Display")]
        [DefaultValue(false)]
        [Description("Gets or sets whether to restrict user input to valid numeric values")]
        public bool NumericOnly
        {
            get
            {
                return numericOnly;
            }
            set
            {
                if (numericOnly != value)
                {
                    numericOnly = value;
                    OnPropertyChanged("NumericOnly");
                }
            }
        }

        /// <summary>
        /// Gets or sets the orientation of the Text
        /// </summary>
        [Category("Display")]
        [DefaultValue(KineticaRT.Controls.TextOrientations.Right)]
        [Description("Gets or sets the orientation of the Text")]
        public KineticaRT.Controls.TextOrientations Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                if (orientation != value)
                {
                    orientation = value;
                    AddToQueue(false);
                    OnPropertyChanged("Orientation");
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the Display gets its ToolTip, Minimum and Maximum
        /// from the its Source channel
        /// </summary>
        [Category("Display")]
        [DefaultValue(true)]
        [Description("Gets or sets whether the Display gets its ToolTip, Minimum and Maximum from its Source channel")]
        public bool UseSourceFormat
        {
            get
            {
                return useSourceFormat;
            }
            set
            {
                if (useSourceFormat != value)
                {
                    useSourceFormat = value;
                    if (useSourceFormat)
                    {
                        SetFormat();
                    }
                    OnPropertyChanged("UseSourceFormat");
                }
            }
        }

        /// <summary>
        /// Gets or sets whether user input is Enabled
        /// </summary>
        /// <remarks>This differs from Enabled by not preventing drag and drop or tooltips</remarks>
        [Category("Display")]
        [DefaultValue(true)]
        [Description("Gets or sets whether user input is Enabled")]
        public bool InputEnabled
        {
            get
            {
                return inputEnabled;
            }
            set
            {
                if (inputEnabled != value)
                {
                    inputEnabled = value;
                    //set correct cursor
                    this.Cursor = inputEnabled ? Cursors.IBeam : Cursors.Arrow;

                    OnPropertyChanged("InputEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the format string specifying the display format, leave blank to use the Source's formating.
        /// </summary>
        [Description("Format string specifying the display format, leave blank to use the Source's formating.  Eg. C,E,F3,G6,N")]
        [Category("Display")]
        [DefaultValue("")]
        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                if (format != value)
                {
                    format = value;
                    if (sourceChannel != null)
                    {
                        //make the source convert itself to a formatted string
                        if (format != "" && sourceChannel is KineticaRT.Channels.Channel)
                        {
                            SetText(((KineticaRT.Channels.Channel)sourceChannel).ToString(format, null), false);
                        }
                        else
                        {
                            SetText(sourceChannel.ToString(), false);
                        }
                        AddToQueue(false);
                    }
                    OnPropertyChanged("Format");
                }
            }
        }
        #endregion
        
		#region Drawing
        /// <summary>
        /// Method that does all the drawing.
        /// </summary>
        /// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
        /// <param name="g">Graphics objects with which the drawing is done</param>
        //ft public override void Repaint(bool FullRedraw, System.Drawing.Graphics g)
        public override void Repaint(bool FullRedraw, DrawingContext drawingContext)
        {
            CalculateRectangles();
            if (FullRedraw || resizeBox)
            {
                //ft Rectangle rect = ClientRectangle;
                //ft ici ca bug Rect rect = VisualTreeHelper.GetDescendantBounds(this);
                Rect rect = new Rect();
                rect.Width = this.Width;
                rect.Height = this.Height;


                needFullRedraw = false;
//ft a reactiver
                if (shape == Shapes.Rectangle || percentBarEnabled || spinButtonsEnabled)
                {
                    int border_width = BorderWidthPixels(rect);
                    DrawSquareBorder(drawingContext, rect, BorderType, borderColor, border_width);
                    //		rect.Inflate (-border_width,-border_width) ;
                    //		g.FillRectangle(backBrush, rect);
                }
                else
                {
                    //ft g.FillRectangle(backBrush, rect);
                    Pen pen = new Pen(backBrush, 1);
                    drawingContext.DrawRectangle(backBrush, pen, rect);


                    //ft test test Rectangle rectangle = new Rectangle();
                    //ft test test rectangle.Fill = backBrush;

                    DrawBorder(drawingContext);
                }
            }

            //ensure the remainder is drawn inside the border
            if (shape == Shapes.Rectangle || percentBarEnabled || spinButtonsEnabled)
            {
//ft TODO
                ////g.Clip = new Region(DisplayRectangle) ;
                ////				Region new_region = new Region(DisplayRectangle) ;
                ////ft Rect rect = ClientRectangle;
                //Rect rect = VisualTreeHelper.GetDescendantBounds(this);
                //int border_width = BorderWidthPixels(rect);
                //rect.Inflate(-border_width, -border_width);
                //Region new_region = new Region(rect);
                //new_region.Intersect(g.Clip);
                ////ft g.Clip = new_region;
                //drawingContext.Clip = new_region;
            }
            else
            {
//ft TODO
                ////ft GraphicsPath path = CalculateShapePath(DisplayRectangle, shape);
                //Path path = CalculateShapePath(VisualTreeHelper.GetDescendantBounds(this), shape);

                ////g.Clip = new Region(path) ;
                //Region new_region = new Region(path);
                //new_region.Intersect(g.Clip);
                //g.Clip = new_region;
                ////ft path.Dispose();
            }
//ft TODO

            //if (!FullRedraw || resizeBox)
            {
                //ft g.FillRectangle(backBrush, textRect);
                Pen pen = new Pen(backBrush, 1);
                drawingContext.DrawRectangle(backBrush, pen, textRect);
            }
           

         

            if (!editing)
            {
#if KRT_license
                //demo mode?
                if (demoMode)
                {
                    if (BackColor.GetBrightness() < 0.6)
                    {
                        DrawDemoTextColor(g, DisplayRectangle, Color.Orchid) ;
                    }
                    else
                    {
                        DrawDemoTextColor(g, DisplayRectangle, Color.DarkOrchid) ;
                    }
                }
#endif

                if (textChanged || resizeBox)
                {
                    //ft shortText = ReduceText(g);
                    shortText = ReduceText(drawingContext);
                    textChanged = false;
                }

                Rect text_rect = textRect;
                //ft Brush brush = new SolidBrush(ForeColor);
                Brush brush = Foreground;

                //set up transforms
                switch (orientation)
                {
                    case KineticaRT.Controls.TextOrientations.Down:
                        {
                            Rect original_text_rect = text_rect;

                            text_rect.X = original_text_rect.Y;
                            text_rect.Y = original_text_rect.X;
                            text_rect.Width = original_text_rect.Height;
                            text_rect.Height = original_text_rect.Width;

                            //ft g.RotateTransform(90);
                            RotateTransform rotateTransform = new RotateTransform(90);
                            drawingContext.PushTransform(rotateTransform);
                            
                            //ft g.TranslateTransform(0, -(original_text_rect.Right + original_text_rect.X));
                            TranslateTransform translateTransform = new TranslateTransform(0, -(original_text_rect.Right + original_text_rect.X));
                            drawingContext.PushTransform(translateTransform);
                        }
                        break;

                    case KineticaRT.Controls.TextOrientations.Up:
                        {
                            Rect original_text_rect = text_rect;

                            text_rect.X = original_text_rect.Y;
                            text_rect.Y = original_text_rect.X;
                            text_rect.Width = original_text_rect.Height;
                            text_rect.Height = original_text_rect.Width;

                            //ft g.RotateTransform(-90);
                            RotateTransform rotateTransform = new RotateTransform(-90);
                            drawingContext.PushTransform(rotateTransform);

                            //ft g.TranslateTransform(-VisualTreeHelper.GetDescendantBounds(this).Height, 0);
                            TranslateTransform translateTransform = new TranslateTransform(-VisualTreeHelper.GetDescendantBounds(this).Height, 0);
                            drawingContext.PushTransform(translateTransform);

                            //ft g.TranslateTransform(-VisualTreeHelper.GetDescendantBounds(this).Height, 0);
                            translateTransform = new TranslateTransform(-VisualTreeHelper.GetDescendantBounds(this).Height, 0);
                            drawingContext.PushTransform(translateTransform);
                            
                        }
                        break;

                    case KineticaRT.Controls.TextOrientations.Left:
                        {
                            //ft g.RotateTransform(180);
                            RotateTransform rotateTransform = new RotateTransform(180);
                            drawingContext.PushTransform(rotateTransform);

                            //ft g.TranslateTransform(-(text_rect.Right + text_rect.X), -(text_rect.Bottom + text_rect.Y));
                            TranslateTransform translateTransform = new TranslateTransform(-(text_rect.Right + text_rect.X), -(text_rect.Bottom + text_rect.Y));
                            drawingContext.PushTransform(translateTransform);
                        }
                        break;

                    case KineticaRT.Controls.TextOrientations.Right:
                        {
                        }
                        break;
                }

                ///ft TODO g.DrawString(shortText, this.Font, new SolidBrush(ForeColor), text_rect, textFormat);
                //ft brush.Dispose();
            }

            resizeBox = false;

            //ft DrawSpinButtons(g);
            DrawSpinButtons(drawingContext);

            //ft DrawPercentBar(g);
            DrawPercentBar(drawingContext);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
             try
             {
                
            base.OnRender(drawingContext);

            Repaint(true, drawingContext); //ft nouveau

            //System.Windows.Media.Brush brush = new SolidBrush(Foreground);
            Typeface currentTypeface = new Typeface(this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch);
            //draw units and description text
            if (textRect.Width > 0)
            {
                //e.Graphics.DrawString(Description, Font, brush, descriptionRect, stringFormat);
                //System.Windows.Media.FontFamily courier = new System.Windows.Media.FontFamily("Courier New");

                FormattedText ft2 = new FormattedText(Text,
                                               System.Globalization.CultureInfo.CurrentCulture,
                                               System.Windows.FlowDirection.LeftToRight,
                                               currentTypeface,
                                               this.FontSize,
                                               Foreground);//Foreground



                Double fontHeight = ft2.Height;
                ft2.TextAlignment = TextAlign;
                //double fontHeight = 0;
                if (TextAlign == TextAlignment.Center)
                {
                    drawingContext.DrawText(ft2, new System.Windows.Point(textRect.X + this.Width / 2, textRect.Y + (this.Height / 2) - (fontHeight / 2)));
                }


                //ft test de Background drawingContext.DrawRectangle(backBrush, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Yellow, 1.0), textRect);
                
                //this.Background = new SolidColorBrush(Colors.Blue);  ft ATTENTION le background doit toujours etre transparent!!!!! Il faut dessiner le background via DrawRectangle
                //drawingContext.DrawRectangle(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Blue, 1.0), new Rect(10, 40, ft2.WidthIncludingTrailingWhitespace, ft2.Height));

            }
            // Repaint(FullRedraw, drawingContext);
             }
              catch (Exception ex)
             {
                    Console.WriteLine(ex.Message);
             }
        
        }

        //Draws the percentage bar
        //ftprivate void DrawPercentBar(System.Drawing.Graphics g)
        private void DrawPercentBar(DrawingContext drawingContext)
        {
            if (!barValid) return;

            Rect rect = barRect;
            //ft Pen pen = new Pen(ControlPaint.Dark(borderColor), 1);
            //ft Pen pen = new Pen(borderColor.ToControlPaintDark(), 1);
            Pen pen = new Pen(ExtentionMethods.ToControlPaintDark(borderColor), 1);

            //ft g.FillRectangle(backBrush, barRect);
            drawingContext.DrawRectangle(backBrush,pen, barRect);
            //ft g.DrawLine(pen, barRect.Left, barRect.Top, barRect.Right - 1, barRect.Top);
            drawingContext.DrawLine(pen, new Point(barRect.Left, barRect.Top),new Point( barRect.Right - 1, barRect.Top));
            //ft pen.Dispose();

            //draw bar...
            if (krtValue is KineticaRT.Values.Double)
            {
                double val = krtValue.ToDouble();

                if (!System.Double.IsNaN(val) && !System.Double.IsInfinity(val))
                {
                    double fraction = (val - minimum) / (maximum - minimum);

                    if (fraction > 0)
                    {
                        Brush brush = new SolidColorBrush(percentBarColor);

                        if (fraction > 1) fraction = 1;

                        rect.Y++;
                        rect.Height--;
                        rect.Width = (int)(fraction * rect.Width);

                        //ft g.FillRectangle(brush, rect);
                        drawingContext.DrawRectangle(brush, pen, rect);
                        //ft brush.Dispose();
                    }
                }
            }
        }

        //Draws the spin buttons
        //ft private void DrawSpinButtons(System.Drawing.Graphics g)
        private void DrawSpinButtons(DrawingContext drawingContext)
        {
            if (spinButtonsEnabled)
            {
                //ft ControlPaint.DrawScrollButton(g, spinUpRect, ScrollButton.Up, spinUpPressed ? ButtonState.Pushed : ButtonState.Normal);
                //ft ControlPaint.DrawScrollButton(g, spinDownRect, ScrollButton.Down, spinDownPressed ? ButtonState.Pushed : ButtonState.Normal);
            }
        }

        private void CalculateRectangles()
        {
            CalculateDisplayRectangle();

            //ft Rect rect = DisplayRectangle;	//bar and buttons get subtracted
            //ft ici ca bug, la grandeur change tout le temps . Rect rect = VisualTreeHelper.GetDescendantBounds(this);
            Rect rect = new Rect();
            rect.Width = this.Width;
            rect.Height = this.Height;

            bool bar_now_valid = false;

            //the bar is drawn if enabled and finite limits defined
            bar_now_valid = percentBarEnabled && !System.Double.IsInfinity(minimum) && !System.Double.IsInfinity(maximum);
            if (bar_now_valid != barValid)
            {
                barValid = bar_now_valid;
                resizeBox = true;
            }

            if (barValid)
            {
                barRect = rect;

                barRect.Height = rect.Height / 2 > 4 ? 4 : rect.Height / 2;
                barRect.Y = rect.Bottom - barRect.Height;

                rect.Height -= barRect.Height;
            }

            if (spinButtonsEnabled)
            {
                spinUpRect.Height = rect.Height / 2;
                spinDownRect.Height = rect.Height - spinUpRect.Height;

                spinUpRect.Width = spinUpRect.Height;
                spinDownRect.Width = spinUpRect.Height;

                spinUpRect.Y = rect.Top;
                spinDownRect.Y = spinUpRect.Y + spinUpRect.Height;

                spinUpRect.X = rect.Right - spinUpRect.Width;
                spinDownRect.X = spinUpRect.X;

                rect.Width -= spinUpRect.Width;
            }

            //update textRect to only be bit text goes in
            textRect = rect;
        }

        //ft private string ReduceText(System.Drawing.Graphics g)
        private string ReduceText(DrawingContext drawingContext)
       
        {
            //ft SizeF size = g.MeasureString(text, this.Font);
            Size size = ExtentionMethods.MeasureTextSize(text, this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch, this.FontSize);

            

            //tb		if (size.Width > textBox.Width)
            if (size.Width > textRect.Width)
            {
                string temp;

                //need to reduce the length of the string
                if (this.Value.GetType() == typeof(KineticaRT.Values.Double))
                {
                    //numeric and too long
                    temp = "######";
                }
                else
                {
                    //return a cut-off string with '...' appended
                    //int			index	= text.Length - 1 ;

                    //temp = text + "..." ;

                    int index = 0;
                    do
                    {
                        //temp = temp.Remove(index, 1) ;
                        //index-- ;
                        index++;
                        temp = text.Substring(0, index) + "..."; ;
                        //ft size = g.MeasureString(temp, this.Font);
                        size = ExtentionMethods.MeasureTextSize(temp, this.FontFamily, this.FontStyle, this.FontWeight, this.FontStretch, this.FontSize);
                    }
                    //while (index > 0 && size.Width > textBox.Width) ;
                    //tb				while (index < text.Length && size.Width < textBox.Width) ;
                    while (index < text.Length && size.Width < textRect.Width);

                    index--;
                    temp = text.Substring(0, index);
                }

                //make tool tip show full string
                //base.SetToolTip(text) ;
                return temp;
            }

            //make tool tip show configured tool tip text
            //base.SetToolTip(toolTipText) ;
            return text;
        }
		#endregion

		#region Other Methods
        /// <summary>
        /// Sets the controls window to the relevant size and shape
        /// Call when shape changes, or control resizes
        /// </summary>
        //ft protected override void SetWindowRegion()
        protected void SetWindowRegion()
        {
//ft TODO
            //if (shape == Shapes.Rectangle || percentBarEnabled || spinButtonsEnabled)
            //{
            //    //must be square if bar or buttons enabled
            //    this.Region = null;
            //}
            //else
            //{
            //    GraphicsPath path = CalculateShapePath(ClientRectangle, shape);

            //    this.Region = new System.Drawing.Region(path);
            //    //ft path.Dispose();
            //}
        }

		//sets the current text
		private void SetText(string newText, bool setValue)
		{
			if (text != newText)
			{
				text = newText;
				textChanged = true ;

				if (setValue)
				{
					SetValue(TextToValue(text), false) ;
				}
				OnPropertyChanged("Text") ;
			}
		}

		//convert text to relevant value type
		private KineticaRT.Values.Value TextToValue(string Text)
		{
			string		temp ;
			temp = Text.ToLower() ;

			switch (temp)
			{
				case "true" :		return KineticaRT.Values.Values.True ;
				case "false" :		return KineticaRT.Values.Values.False ;
				case "pi"	:		return KineticaRT.Values.Values.Pi ;
				case "infinity"	:	return KineticaRT.Values.Values.PositiveInfinity ;
				case "-pi"	:		return KineticaRT.Values.Values.Pi.Multiply(KineticaRT.Values.Values.MinusOne) ;
				case "-infinity":	return KineticaRT.Values.Values.NegativeInfinity ;
			}

			double	dbl ;
			
			if (System.Double.TryParse (Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out dbl))
			{
				return new KineticaRT.Values.Double (dbl) ;
			}

			return new KineticaRT.Values.Text(Text) ;
		}

		private void IncrementValue()
		{
			if (krtValue is KineticaRT.Values.Double)
			{
				double		new_val		= krtValue.ToDouble() ;

				//only increments if value is a legitimate double
				if (!System.Double.IsNaN(new_val) && !System.Double.IsInfinity(new_val))
				{
					double		increment ;

					//should we be incrementing fast yet?
					if (spinIncrementCount >= 20)
					{
						increment = spinIncrement * 100 ;
					}
					else if (spinIncrementCount >= 10)
					{
						increment = spinIncrement * 10 ;
					}
					else
					{
						increment = spinIncrement ;
					}

					if (spinUpPressed)
					{
						new_val += increment ;
					}
					else if (spinDownPressed)
					{
						new_val -= increment ;
					}

					//keep it in range
					if (new_val < minimum)
					{
						new_val = minimum ;
					}
					else if (new_val > maximum)
					{
						new_val = maximum ;
					}

					SetValue(new KineticaRT.Values.Double(new_val), false) ;
					spinIncrementCount++ ;
				}
			}
		}

		// Calculates a new value from the position of the mouse on the percent bar
		private void SetValueFromBar(int XPos, int YPos)
		{
			double		fraction ;
			double		new_val ;

            fraction = 0;//ft TODO (XPos - barRect.Left) / (double)(barRect.Width);

			if (fraction < 0)
			{
				fraction = 0 ;
			}
			else if (fraction > 1)
			{
				fraction = 1 ;
			}

			new_val = minimum + fraction * (maximum - minimum) ;

			SetValue(new KineticaRT.Values.Double(new_val), false) ;
		}

		private void SetFormat()
		{
			if (useSourceFormat)
			{
				KineticaRT.Channels.IInfoChannel		ch ;

				ch = sourceChannel as KineticaRT.Channels.IInfoChannel ;
				if (ch != null)
				{
					KineticaRT.Channels.INumericChannel		num ;

					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;

					num = ch as KineticaRT.Channels.INumericChannel ;
					if (num != null)
					{
						Minimum	= num.Minimum ;
						Maximum	= num.Maximum ;
					}
				}
			}
		}
		
		private void SetValue(object newValue, bool fromSource)
		{
            if (!useSourceValue && fromSource) return;

			if (this.value != newValue)
			{
				KineticaRT.Values.Value		new_krt_value ;

                this.value = newValue;

				//is it already a KRT Value?
                new_krt_value = newValue as KineticaRT.Values.Value;
				//how about a standard double or string?
				if (new_krt_value == null)
				{
					try
					{
                        new_krt_value = new KineticaRT.Values.Double(System.Convert.ToDouble(newValue));
					}
					catch (System.Exception)
					{
					}
				}
				//otherwise make it a string
				if (new_krt_value == null)
				{
                    if (newValue != null)
					{
                        new_krt_value = new KineticaRT.Values.Text(newValue.ToString());
					}
					else
					{
                        new_krt_value = new KineticaRT.Values.Text("");
					}
				}

				if (new_krt_value.CompareTo (krtValue) != 0)
				{
					krtValue = new_krt_value ;

					if (!busySettingValue && (destinationChannel != null))		destinationChannel.Value = krtValue ;

					//is this right?? what if the value wasn't set by the source?
					if (sourceChannel != null)
					{
						if (fromSource)
						{
							//make the source convert itself to a formatted string
							if (format != "" && sourceChannel is KineticaRT.Channels.Channel)
							{
                                SetText(((KineticaRT.Channels.Channel)sourceChannel).ToString(format, null), false);
							}
							else
							{
								SetText(sourceChannel.ToString(), false) ;
							}
						}
						else
						{
                            if (useSourceValue)
                            {
                                //revert to source value
                                busySettingValue = true;
                                SetValue(sourceChannel.Value, true);
                                busySettingValue = false;
                            }
                            else
                            {
                                busySettingValue = true;
                                SetText(new_krt_value.ToString(), false);
                                busySettingValue = false;
                            }
						}
					}
					else if (krtValue != null)
					{
						SetText(krtValue.ToString(), false) ;
					}
					else
					{
						SetText("", false) ;
					}

					AddToQueue(false) ;

					//OnValueChanged() ;
				}
				OnPropertyChanged("Value") ;
			}
		}
		#endregion

		#region Events
        ///// <summary>
        ///// Resize Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected override void OnResize(EventArgs e)
        //{
        //    resizeBox = true ;
        //    base.OnResize(e) ;
        //}

//        /// <summary>
//        /// FontChanged Event
//        /// </summary>
//        /// <param name="e">Event Data</param>
//        protected override void OnFontChanged(EventArgs e)
//        {
////tb		textBox.Font = this.Font ;
//            resizeBox = true ;
//            base.OnFontChanged(e) ;
//        }

//tb	/// <summary>
//		/// BackColorChanged Event
//		/// </summary>
//		/// <param name="e">Event Data</param>
//		protected override void OnBackColorChanged(EventArgs e)
//		{
//			textBox.BackColor = BackColor ;
//			base.OnBackColorChanged(e) ;
//		}

//tb	/// <summary>
//		/// ForeColorChanged Event
//		/// </summary>
//		/// <param name="e">Event Data</param>
//		protected override void OnForeColorChanged(EventArgs e)
//		{
//			textBox.ForeColor = ForeColor ;
//			base.OnForeColorChanged(e) ;
//		}

        //ft done
        ///// <summary>
        ///// GotFocus Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected override void OnGotFocus(EventArgs e)
        //{
        //    if (inputEnabled && !spinning && !barDragging)
        //    {
        //        this.CreateTextBox();
        //        //tb			textBox.Visible = true ;
        //        textBox2.Focus();
        //    }
        //    base.OnGotFocus(e);
        //}
        /// <summary>
        /// GotFocus Event
        /// </summary>
        /// <param name="e">Event Data</param>
        private void canva_GotFocus(object sender, RoutedEventArgs e)
        {
            if (inputEnabled && !spinning && !barDragging)
            {
                this.CreateTextBox();
                //tb			textBox.Visible = true ;
                textBox2.Focus();
            }
            base.OnGotFocus(e);

        }
        ///// <summary>
        ///// RightToLeft changed event
        ///// </summary>
        ///// <param name="e">Event data</param>
        //protected override void OnRightToLeftChanged(EventArgs e)
        //{
        //    if (RightToLeft == RightToLeft.Yes)
        //    {
        //        textFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft ;
        //    }
        //    else
        //    {
        //        textFormat.FormatFlags = 0 ;
        //    }
			
        //    base.OnRightToLeftChanged(e) ;
        //}

        ///// <summary>
        ///// Mouse Down Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected override void OnMouseDown(MouseEventArgs e)
        //{
        //    lock (this)
        //    {
        //        if (e.Button == MouseButtons.Left && inputEnabled)
        //        {
        //            if (spinButtonsEnabled)
        //            {
        //                //have we clicked on the spin control?
        //                if (spinUpRect.Contains(e.X, e.Y))
        //                {
        //                    if (editing)			SendKeys.SendWait("{TAB}") ;

        //                    spinning = true ;
        //                    spinUpPressed = true ;
        //                    spinDownPressed = false ;
        //                }
        //                else if (spinDownRect.Contains(e.X, e.Y))
        //                {
        //                    if (editing)			SendKeys.SendWait("{TAB}") ;

        //                    spinning = true ;
        //                    spinDownPressed = true ;
        //                    spinUpPressed = false ;
        //                }
        //                else
        //                {
        //                    spinning = false ;
        //                    spinUpPressed = false ;
        //                    spinDownPressed = false ;
        //                }

        //                if (spinning)
        //                {
        //                    spinIncrementCount = 0 ;
        //                    IncrementValue () ;
        //                    this.Invalidate(spinUpPressed ? spinUpRect : spinDownRect) ;

        //                    //start timer for repetition: 200 ms interval
        //                    spinTimer.Start() ;
        //                }
        //                // else we're not on the spin control, so continue processing
        //            }

        //            if (!spinning)		//do nothing else if spinning has commenced
        //            {
        //                if (barValid)
        //                {
        //                    //have we clicked on the percent bar?
        //                    if (barRect.Contains(e.X, e.Y))
        //                    {
        //                        barDragging = true ;

        //                        if (editing)			SendKeys.SendWait("{TAB}") ;

        //                        SetValueFromBar(e.X, e.Y) ;
        //                    }
        //                    // else we're not on the percent bar, so continue processing
        //                }
        //            }
        //        }

        //        base.OnMouseDown(e) ;

        //        if (!spinning && !barDragging && inputEnabled)
        //        {
        //            //set focus on text box
        //            this.CreateTextBox() ;
        //            //tb2			textBox.Visible = true ;
        //            textBox2.Focus() ;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Mouse Up Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected override void OnMouseUp(MouseEventArgs e)
        //{
        //    lock (this)
        //    {
        //        if (spinning)
        //        {
        //            spinTimer.Stop() ;
        //            this.Invalidate(spinUpPressed ? spinUpRect : spinDownRect) ;

        //            spinning		= false ;
        //            spinUpPressed	= false ;
        //            spinDownPressed	= false ;
        //        }
        //        else if (barDragging)
        //        {
        //            barDragging = false ;
        //        }

        //        base.OnMouseUp(e) ;
        //    }
        //}

        ///// <summary>
        ///// Mouse Move Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    lock (this)
        //    {
        //        if (barDragging)
        //        {
        //            Cursor = Cursors.Default ;
        //            SetValueFromBar(e.X, e.Y) ;
        //        }
        //        else if ((spinButtonsEnabled && (spinDownRect.Contains(e.X, e.Y) || spinUpRect.Contains(e.X, e.Y)))
        //            || (barValid && barRect.Contains(e.X, e.Y))	)
        //        {
        //            Cursor = Cursors.Default ;
        //        }
        //        else if (inputEnabled)
        //        {
        //            Cursor = Cursors.IBeam ;
        //        }
        //        base.OnMouseMove(e) ;
        //    }
        //}
		#endregion

		#region Text Box Events
		private void OnBoxGotFocus(object sender, EventArgs e)
		{
			lock (this)
			{
				//tb		textBox.Text = text ;
				textBox2.Text = text ;
				editing = true ;
			}
		}

		private void OnBoxValidating(object sender, CancelEventArgs e)
		{
			lock (this)
			{
				e.Cancel = ValidateText() ;
			}
		}

		private void OnBoxLostFocus(object sender, EventArgs e)
		{
			lock (this)
			{
				ValidateText() ;

				editing = false ;
				//tb		textBox.Visible = false ;
				this.DestroyTextBox ();
			
				//do this in case text hasn't changed, but needs shortening
				AddToQueue(true) ;
			}
		}

		private bool ValidateText()
		{
			bool	cancel		= false ;

			if (editing)	//will be 'false' if escape was pressed
			{
				if (numericOnly)
				{
//tb				KineticaRT.Values.Double	new_val	= new KineticaRT.Values.Double(textBox.Text) ;
					KineticaRT.Values.Double	new_val	= new KineticaRT.Values.Double(textBox2.Text) ;
					double						new_dbl	= new_val.ToDouble() ;

					if (System.Double.IsInfinity(new_dbl) || System.Double.IsNaN(new_dbl) ||
						new_dbl > maximum || new_dbl < minimum)
					{
						//invalid value: retain focus
						cancel = true ;
					}
				}

				if (!cancel)
				{
//tb				SetText(textBox.Text, true) ;
					SetText(textBox2.Text, true) ;
				}
				else
				{
					//ErrorProvider		ep	= new ErrorProvider(this) ;
					//ep.SetError(textBox, "invalid value skc") ;

//tb				textBox.SelectAll() ;
					textBox2.SelectAll() ;
				}
			}

			return cancel ;
		}

        private void OnBoxKeyPress(object sender, KeyEventArgs e)
		{
//ft TODO

            //lock (this)
            //{
            //    if (e.KeyChar == (char)13)			//return
            //    {
            //        e.Handled = true ;
            //        //if text ok (=shouldn't cancel) move focus on
            //        if (!ValidateText())
            //        {
            //            //Simulate the tab key being pressed
            //            // - this can revert straight back to focussing on this
            //            //object if no other tabstops
            //            //(note that escape will clear focus without tabbing on)
            //            //					SendKeys.Send("{TAB}") ;
            //            this.Parent.Focus() ;
            //        }
            //    }
            //    else if (e.KeyChar == (char)9)		//tab
            //    {
            //        Parent.GetNextControl (Parent,true).Focus() ;
            //    }
            //    else if (e.KeyChar == (char)27)		//escape
            //    {
            //        //ignore new text
            //        e.Handled = true ;
            //        editing = false ;		//prevent validation
            //        //move focus on
            //        //tb			this.InvokeLostFocus(textBox, EventArgs.Empty) ;
            //        this.InvokeLostFocus(textBox2, EventArgs.Empty) ;
            //    }

            //    //			if (e.Handled)
            //    //			{
            //    //				//simulate the tab key being pressed
            //    //				//##this is flawed if this is the only TabStop
            //    //				//as focus comes straight back to the display and re-edits it
            //    //				SendKeys.Send("{TAB}") ;
            //    //			}

            //    //			//this doesn't seem ideal, but I can't find a better way atm
            //    //			if (e.Handled)
            //    //			{
            //    //				//this works iff there is another control to take the focus
            //    //				System.Windows.Forms.Control	ctrl = Parent.GetNextControl(this, true) ;
            //    //				if (ctrl != null)
            //    //				{
            //    //					ctrl.Focus() ;
            //    //				}
            //    //				else
            //    //				{
            //    //					//this works, but doesn't allow tabbing back in again
            //    //					textBox.Enabled = false ;
            //    //					textBox.Enabled = true ;
            //    //				}
            //    //			}
            //}
		}
		#endregion

		#region Timer events
		private void OnSpinTimerElapsed(object Sender, ElapsedEventArgs e)
		{
			if (spinning)
			{
				IncrementValue() ;
			}
			else
			{
				//just in case!
				spinTimer.Stop() ;
			}
		}
		#endregion

		#region Drag / Drop operations
		/// <summary>
		/// DragEnter event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnDragEnter (DragEventArgs e)
		{
//ft TODO

            ////check if allowed to drop things, and if drag object holds text
            //if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
            //{
            //    e.Effect = DragDropEffects.Copy;
            //}
            //else
            //{
            //    e.Effect = DragDropEffects.None;
            //}

            //base.OnDragEnter(e) ;
		}

        ///// <summary>NumericVariable
        ///// DragDrop event
        ///// </summary>
        ///// <param name="e">Event data</param>
        //protected override void OnDragDrop (DragEventArgs e)
        //{
        //    if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
        //    {
        //        Source = e.Data.GetData(DataFormats.Text).ToString() ;
        //    }

        //    base.OnDragDrop(e) ;
        //}
		#endregion

		#region Source event handling (value and property)
		//new value coming in
		private void OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value v)
		{
			lock (this)
			{
				busySettingValue = true ;
				//ft                 
                SetValue(sender.Value, true) ;
                //SetValue(v, true);//ft bug bug sender.Value contient l'ancienne valeur pas la nouvelle...
				busySettingValue = false ;
			}
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
				SetFormat() ;

				//also need to reformat the value
                if (sourceChannel != null && useSourceValue)
				{
					SetText(sourceChannel.ToString(), true) ;
				}
			}
		}
		#endregion

		#region Connection
        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public override void Connect()
        {
            if (source != "") SourceChannel = OnObjectSearch(source) as KineticaRT.Channels.IChannel;
            if (destination != "") DestinationChannel = OnObjectSearch(destination) as KineticaRT.Channels.IChannel;
        }

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        public override void Disconnect()
        {
            if (sourceChannel != null) SourceChannel = null;
            if (destinationChannel != null) DestinationChannel = null;
        }
		#endregion

	
		private void CreateTextBox ()
		{
			if (textBox2 != null) return ;
			
			textBox2 = new System.Windows.Controls.TextBox();

			// textBox
			//ft textBox2.AutoSize = false;
			//ft textBox2.BorderStyle = BorderStyle.None;
			//ft textBox2.Name = "textBox";
			//ft textBox2.Size = new System.Drawing.Size(100, 20);
            textBox2.Width = Double.NaN;
            textBox2.Height = Double.NaN;

			textBox2.TabIndex = 0;
			textBox2.Text = "";
            //ft textBox2.Font = Font ;
			//ft textBox2.WordWrap = false;
			textBox2.Background = this.Background ;
			textBox2.Foreground = this.Foreground ;
//			textBox2.AcceptsTab=true ;
            //ft fait plus bas canva.Children.Add(textBox2);

//ft TODO

            switch (textAlign)
            {
                //ft case System.Drawing.ContentAlignment.BottomLeft:
                case TextAlignment.Left:
                    {
                        textBox2.HorizontalAlignment = HorizontalAlignment.Left;
                    }
                    break;

                //case System.Drawing.ContentAlignment.MiddleLeft:
                //    {
                //        textBox2.HorizontalAlignment = HorizontalAlignment.Left;
                //    }
                //    break;

                //case System.Drawing.ContentAlignment.TopLeft:
                //    {
                //        textBox2.HorizontalAlignment = HorizontalAlignment.Left;
                //    }
                //    break;

                //ft case System.Drawing.ContentAlignment.BottomCenter:
                case TextAlignment.Center:
                    {
                        textBox2.HorizontalAlignment = HorizontalAlignment.Center;
                    }
                    break;

                //case System.Drawing.ContentAlignment.MiddleCenter:
                //    {
                //        textBox2.HorizontalAlignment = HorizontalAlignment.Center;
                //    }
                //    break;

                //case System.Drawing.ContentAlignment.TopCenter:
                //    {
                //        textBox2.HorizontalAlignment = HorizontalAlignment.Center;
                //    }
                //    break;

                //ft case System.Drawing.ContentAlignment.BottomRight:
                case  TextAlignment.Right:
                    {
                        textBox2.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    break;

                //case System.Drawing.ContentAlignment.MiddleRight:
                //    {
                //        textBox2.HorizontalAlignment = HorizontalAlignment.Right;
                //    }
                //    break;

                //case System.Drawing.ContentAlignment.TopRight:
                //    {
                //        textBox2.HorizontalAlignment = HorizontalAlignment.Right;
                //    }
                //    break;
               
            }

			//give it enough height for (hopefully) any characters
			//ft this.Font.GetHeight() ;
            
//tb		SizeF		size = g.MeasureString("Iy#", this.Font) ;
			//ft SizeF		size = this.CreateGraphics().MeasureString("Iy#", this.Font) ;
            double fontDpiSize = 96;
            double fontHeight = Math.Ceiling(fontDpiSize * this.FontFamily.LineSpacing);

            Thickness newThickness = new Thickness();
            newThickness.Left = textRect.X;
			textBox2.Margin = newThickness;
			textBox2.Width = textRect.Width ;
			//ft textBox2.Height = (int)(size.Height) ;
            textBox2.Height = (int)(fontHeight);
			//cannot be taller than the display is
			if (textBox2.Height > textRect.Height)
			{
				textBox2.Height = textRect.Height ;
			}

            switch (textFormat.LineAlignment)
            {
                case System.Drawing.StringAlignment.Center:
                    {
                        newThickness.Top = textRect.Top + (textRect.Height - textBox2.Height) / 2;
                        textBox2.Margin = newThickness;
                    }
                    break;

                case System.Drawing.StringAlignment.Near:		//top
                    {
                        newThickness.Top = textRect.Top;
                        textBox2.Margin = newThickness;
                    }
                    break;

                case System.Drawing.StringAlignment.Far:		//bottom
                    {
                        newThickness.Top =  textRect.Bottom - textBox2.Height;
                        textBox2.Margin =newThickness;
                    }
                    break;
            }

			//connect to events from text box
			textBox2.GotFocus	+= new RoutedEventHandler(OnBoxGotFocus) ;
			textBox2.LostFocus	+= new RoutedEventHandler(OnBoxLostFocus) ;
            //ft textBox2.KeyPress += new KeyEventArgs(OnBoxKeyPress);
			//ft textBox2.Validating	+= new CancelEventHandler(OnBoxValidating) ;

            this.AddChild(textBox2);//ft this.Controls.Add (textBox2);
		}

		private void DestroyTextBox ()
		{
			if (textBox2 == null) return ;

            textBox2.GotFocus -= new RoutedEventHandler(OnBoxGotFocus);
            textBox2.LostFocus -= new RoutedEventHandler(OnBoxLostFocus);
            //ft textBox2.KeyPress -= new KeyEventArgs(OnBoxKeyPress);
			//ft textBox2.Validating	-= new CancelEventHandler(OnBoxValidating) ;

			//ft this.Controls.Remove(textBox2) ;
           
			textBox2 = null ;
		}
	}
}

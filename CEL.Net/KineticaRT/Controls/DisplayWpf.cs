
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Media;
using KT = KineticaRT;
using KTC = KineticaRT.Channels;
using KTS = KineticaRT.Studio;

namespace CEL.KineticaRT.Controls
{
    public static class MyExtention
    {
        public static System.Windows.Media.Color ToMediaColor(this System.Drawing.Color originalColor)
        {
            return System.Windows.Media.Color.FromArgb(originalColor.A, originalColor.R, originalColor.G, originalColor.B);
        }
        //public static System.Drawing.Color ToDrawingColor(System.Windows.Media.SolidColorBrush br)
        //{
        //    return System.Drawing.Color.FromArgb(
        //        br.Color.A,
        //        br.Color.R,
        //        br.Color.G,
        //        br.Color.B);
        //}
        public static System.Drawing.Color ToDrawingColor(System.Windows.Media.Brush br)
        {
            SolidColorBrush solidBrush = (SolidColorBrush)br;
            return System.Drawing.Color.FromArgb(
                solidBrush.Color.A,
                solidBrush.Color.R,
                solidBrush.Color.G,
                solidBrush.Color.B);
        }
        public static System.Windows.Media.Brush ToMediaBrushColor(System.Drawing.Color drawingColor)
        {
       
            System.Windows.Media.Brush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B));
            return brush;
        }
    }


    public partial class DisplayWpf : System.Windows.Forms.Integration.ElementHost, KT.IConnectable //WindowsFormsIntegration
    {
        public CEL.WPF.Controls.DisplayStudio displayStudio;
        
        public DisplayWpf()
        {
            InitializeComponent();
            displayStudio = new CEL.WPF.Controls.DisplayStudio();
            //ft ici le ObjectSearch n<a pas encore ete sette.  voir PanelBase.cs OnControlAdded.  Il se fait juste avant le Connect, qui update les valeurs des proprietes
            //ft if (this.ObjectSearch != null)
                //ft this.displayStudio.ObjectSearch += this.ObjectSearch; //ft ajout pour mapper le  Search avec le Search du displayStudio
            this.Child = displayStudio;
            this.Name = "DisplayWpf";
        }
    //public partial class DisplayWpf : KTS.Controls.NumericControl, KT.IConnectable //WindowsFormsIntegration
    //{        
    //    public CEL.WPF.Controls.DisplayStudio displayStudio;
    //    public System.Windows.Forms.Integration.ElementHost elementHost;
    //    public DisplayWpf()
    //    {
    //        InitializeComponent();
    //        elementHost = new System.Windows.Forms.Integration.ElementHost();
    //        displayStudio = new CEL.WPF.Controls.DisplayStudio();
    //        elementHost.Child = displayStudio;
    //    }
        #region Events

        public event KT.ObjectSearchEventHandler ObjectSearch;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { }
            remove { }
        }

        #endregion

        public  void Connect()
        {
            if (this.ObjectSearch != null)
                this.displayStudio.ObjectSearch += this.ObjectSearch; //ft ajout pour mapper le  Search avec le Search du displayStudio
            displayStudio.Connect();
            this.Connected = true;//ft
            
        }

        public virtual void Disconnect()
        {
            if (this.ObjectSearch != null)
                this.displayStudio.ObjectSearch -= this.ObjectSearch; //ft ajout pour mapper le  Search avec le Search du displayStudio
            displayStudio.Disconnect();
            this.Connected = false; //ft
        }
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool Connected //ft //ft
        {
            get { return connected; }
            set { connected = value; }
        }
        public virtual object FindObject(string objectName)
        {
            return null;
        }
        protected KT.Channels.IInfoChannel OnObjectSearch(string objectName)
        {
            if (this.ObjectSearch == null)
            {
                return null;
            }
            
            return (this.ObjectSearch(objectName) as KT.Channels.IInfoChannel);
        }
        public virtual object SearchObject(string objectName)
        {
            return this.OnObjectSearch(objectName);
        }
        /// <summary>
        /// Gets or Sets the style of the Control's Border
        /// </summary>
        [Category("Control")]
        [Description("Gets or Sets the style of the Control's Border")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [System.ComponentModel.Browsable(false)]
        public KT.Controls.BorderStyles BorderStyle
        {
            get{return displayStudio.BorderStyle;}
            set { displayStudio.BorderStyle = value; }
        }

        //private bool ShouldSerializeBorderStyle()
        //{
        //    return false;
        //}

     
        ///// <summary>
        ///// Gets or Sets the style of the Control's Border
        ///// </summary>
        //[Category("Control")]
        //[DefaultValue(KineticaRT.Controls.BorderStyles.EditBox)]
        //[Description("Gets or Sets the style of the Control's Border")]
        //public new KineticaRT.Controls.BorderStyles BorderStyle
        //{
        //    get { return valueDisplay.BorderStyle; }
        //    set { valueDisplay.BorderStyle = value; }
        //}

        /// <summary>
        /// Gets or Sets the style of the Control's Border
        /// </summary>
        [Category("Control")]
        [DefaultValue(KT.Controls.BorderType.EditBox)]
        [Description("Gets or Sets the style of the Control's Border")]
        public virtual KT.Controls.BorderType BorderType
        {
            get { return this.displayStudio.BorderType; }
            set { this.displayStudio.BorderType = value; }
        }

        ///// <summary>
        ///// Gets or Sets the style of the Control's Border
        ///// </summary>
        //[Category("Control")]
        //[Description("Gets or Sets the Color of the Control's Border")]
        //public virtual System.Drawing.Color BorderColor
        //{
        //    get { return this.displayStudio.BorderColor.ToDrawingColor(); }
        //    set { this.displayStudio.BorderColor = value.ToMediaColor(); }
        //}


        //private bool ShouldSerializeBorderColor()
        //{
        //    return this.displayStudio.BorderColor != Color.LightSteelBlue;
        //}

        /// <summary>
        /// Gets or Sets the width of the Control's Border
        /// </summary>
        /// <remarks>
        /// Values between 0 and 0.5 represent a proportion of the width of the Control.
        /// Values greater than or equal to 1 represent the number of pixels
        /// </remarks>
        [Category("Control")]
        [DefaultValue(0.1)]
        [Description("Gets or Sets the width of the Control's Border: 0 - 0.5 as proprtion of width of control, or >= 1 as a number of pixels")]
        public virtual double BorderWidth
        {
            get { return this.displayStudio.BorderWidth; }
            set { this.displayStudio.BorderWidth = value; }
        }

        ///// <summary>
        ///// Gets or sets the name of the control.
        ///// </summary>
        //[Browsable(true)]
        //[Category("General")]
        //public new string Name
        //{
        //    get { return displayStudio.Name; }
        //    set { displayStudio.Name = value; }
        //}
        /// <summary>
        /// Gets or sets the name of Source channel whose value is displayed
        /// </summary>
        [Category("General")]
        [Description("Gets or sets the name of Source channel whose value is displayed")]
        [DefaultValue("")]
        public virtual string Source
        {
            get{return displayStudio.Source;}
            set{displayStudio.Source = value;}
        }
        /// <summary>
        /// Gets or sets the Source channel to which this object is connected
        /// </summary>
        [Category("General")]
        [Description("Gets or sets the Source channel to which this object is connected")]
        [DefaultValue(null)]
        public  KT.Channels.IInfoChannel SourceChannel
        {
            set{this.displayStudio.SourceChannel = value;}
        }

        /// <summary>
        /// Gets or sets a Custom Description
        /// </summary>
        [Category("Description")]
        [Description("Gets or sets a Custom Description")]
        [DefaultValue("")]
        public  string CustomDescription
        {
            get{return this.displayStudio.CustomDescription;}
            set{this.displayStudio.CustomDescription = value;}
        }

        /// <summary>
        /// Gets or sets where to get the Description from
        /// </summary>
        [Category("Description")]
        [Description("Gets or sets where to get the Description from")]
        [DefaultValue(KTS.Controls.TextSources.Description)]
        public  KTS.Controls.TextSources DescriptionSource
        {
            get{return this.displayStudio.DescriptionSource;}
            set{this.displayStudio.DescriptionSource = value;}
        }

        /// <summary>
        /// Gets or sets a Custom Units string
        /// </summary>
        [Category("Units")]
        [Description("Gets or sets a Custom Units string")]
        [DefaultValue("")]
        public  string CustomUnits
        {
            get{return this.displayStudio.CustomUnits;}
            set{this.displayStudio.CustomUnits = value;}
        }

        /// <summary>
        /// Gets or sets whether to use the Custom units string or the Source channel's value
        /// </summary>
        [Category("Units")]
        [Description("Gets or sets whether to use the Custom units string or the Source channel's value")]
        [DefaultValue(false)]
        public  bool UseCustomUnits
        {
            get{return this.displayStudio.UseCustomUnits;}
            set{this.displayStudio.UseCustomUnits = value;}
        }

        /// <summary>
        /// Gets or sets the text formatting of the Value
        /// If specified (ie non-blank string), overrides the format of the Source
        /// </summary>
        [Category("General")]
        [Description("If specified overrides Format of Source. Eg. C,E,F3,G6,N")]
        [DefaultValue("")]
        public  string Format
        {
            get{return this.displayStudio.Format;}
            set{this.displayStudio.Format = value;}
        }

        /// <summary>
        /// Gets or Sets the Color of the Value text
        /// </summary>
        [Category("Display")]
        [Description("Gets or Sets the Color of the Value text")]
        public System.Drawing.Color ValueForeColor
        {
            get { return MyExtention.ToDrawingColor( this.displayStudio.ValueColor); }
            set { this.displayStudio.ValueColor = MyExtention.ToMediaBrushColor(value); }
        }
        private bool ShouldSerializeValueColor()
        {
            //return valueDisplay.ForeColor != DefaultForeColor ;
            return true;
        }

        /// <summary>
        /// Gets or Sets the background Color of the Value text
        /// </summary>
        [Category("Display")]
        [Description("Gets or Sets the background Color of the Value text")]
        public System.Drawing.Color ValueBackColor
        {
            get { return MyExtention.ToDrawingColor(this.displayStudio.ValueBackColor); }//return this.displayStudio.ValueBackColor.ToDrawingColor(); }
            set { this.displayStudio.ValueBackColor = MyExtention.ToMediaBrushColor(value); }
        }
        private bool ShouldSerializeValueBackColor()
        {
            return this.displayStudio.ShouldSerializeValueBackColor();
        }

        /// <summary>
        /// Gets or Sets whether the rate at which the Control redraws
        /// </summary>
        [Category("Control")]
        [DefaultValue(KT.Controls.UpdateSpeeds.Slow)]
        [Description("Gets or Sets the rate at which the Control redraws")]
        public virtual KT.Controls.UpdateSpeeds UpdateSpeed
        {
            get{return this.displayStudio.UpdateSpeed;}
            set{this.displayStudio.UpdateSpeed = value;}
        }

        /// <summary>
        /// Gets or sets the Percentage width devoted to the Units string
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets the Percentage width devoted to the Units string")]
        [DefaultValue(15)]
        public int UnitsPercent
        {
            get{return this.displayStudio.UnitsPercent;}
            set{this.displayStudio.UnitsPercent = value;}
        }

        /// <summary>
        /// Gets or sets the Percentage width devoted to the Value
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets the Percentage width devoted to the Value")]
        [DefaultValue(25)]
        public int ValuePercent
        {
            get{return this.displayStudio.ValuePercent;}
            set{this.displayStudio.ValuePercent = value;}
        }


        /// <summary>
        /// Gets or sets the order of Value, Units and Description sections
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets the order of Value, Units and Description sections")]
        [DefaultValue(KTS.Controls.LayoutsDVU.DescriptionValueUnits)]
        public KTS.Controls.LayoutsDVU SectionLayout
        {
            get{return this.displayStudio.SectionLayout;}
            set{this.displayStudio.SectionLayout = value;}
        }

        /// <summary>
        /// Gets or sets whether input is enabled
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets whether input is enabled")]
        [DefaultValue(false)]
        public bool InputEnabled
        {
            get{return this.displayStudio.InputEnabled;}
            set{this.displayStudio.InputEnabled = value;}
        }

        /// <summary>
        /// Gets or sets whether to only accept input that is numeric.
        /// Does not prevent text values being displayed.
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets whether to only accept input that is numeric. Does not prevent text values being displayed.")]
        [DefaultValue(false)]
        public bool InputNumeric
        {
            get{return this.displayStudio.InputNumeric;}
            set{this.displayStudio.InputNumeric = value;}
        }

        /// <summary>
        /// Gets or sets the background color of the input area when input is enabled
        /// </summary>
        [Category("Display")]
        [Description("Gets or sets the background color of the input area when input is enabled")]
        public System.Drawing.Color InputBackColor
        {
            get { return MyExtention.ToDrawingColor(displayStudio.InputBackColor); }
            set { this.displayStudio.InputBackColor = MyExtention.ToMediaBrushColor(value); }
        }
        private bool ShouldSerializeInputBackColor()
        {
            return this.displayStudio.ShouldSerializeInputBackColor();
        }


//ft Nouveau
        /// <summary>
        /// Gets or Sets the background Color of the Value text
        /// </summary>
        [Category("Display")]
        [Description("Gets or Sets the background Color of the Value text")]
        public override System.Drawing.Color ForeColor
        {
            get { return MyExtention.ToDrawingColor(this.displayStudio.Foreground); }//return this.displayStudio.ValueBackColor.ToDrawingColor(); }
            set { this.displayStudio.Foreground = MyExtention.ToMediaBrushColor(value); }
        }
    
    }
}

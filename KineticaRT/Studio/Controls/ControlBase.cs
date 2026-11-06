using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KineticaRT;

namespace KineticaRT.Studio.Controls
{
	#region Declarations
	/// <summary>
	/// Enumeration of possible sources of text information
	/// </summary>
	public enum TextSources
	{
		/// <summary>
		/// Custom text
		/// </summary>
		Custom,
		/// <summary>
		/// Source channel Description property
		/// </summary>
		Description,
		/// <summary>
		/// Source Channel ToolTip property
		/// </summary>
		ToolTip,
	}

	/// <summary>
	/// Possible layouts for Description, Value and Units sections
	/// </summary>
	public enum LayoutsDVU
	{
		/// <summary>
		/// Description then Value then Units
		/// </summary>
		DescriptionValueUnits,
		/// <summary>
		/// Description then Units then Value
		/// </summary>
		DescriptionUnitsValue,
		/// <summary>
		/// Value then Description then Units
		/// </summary>
		ValueDescriptionUnits,
		/// <summary>
		/// Value then Units then Description
		/// </summary>
		ValueUnitsDescription,
		/// <summary>
		/// Units then Description then Value
		/// </summary>
		UnitsDescriptionValue,
		/// <summary>
		/// Units then Value then Description
		/// </summary>
		UnitsValueDescription,
	}
	#endregion

	/// <summary>
	/// Base class for KineticaRT.Studio controls
	/// </summary>
	[DefaultProperty("Source")]
    public class ControlBase : System.Windows.Forms.UserControl, IConnectable, ISource, IVisibleSource //ft
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string					customDescription ;
		private TextSources				descriptionSource ;
		private Channels.IInfoChannel	sourceChannel ;
		private string					source ;
		private string					customToolTip ;
		private TextSources				toolTipSource ;

		private	ToolTip					toolTip ;

		private PropertyChangedEventHandler	propertyHdlr ;

        private Channels.IChannel visibleSourceChannel; //ft
        private string visibleSource; //ft
        private Channels.ValueChangedEventHandler visibleSrcHdlr; //ft
        
 
		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the ControlBase class
		/// </summary>
		public ControlBase()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.ResizeRedraw = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//ft pour supporter les background transparent
            //ft test this.SetStyle(ControlStyles.Opaque, true);//ft pour supporter les background transparent ..incapable de faire marcher ca correctement
            this.UpdateStyles();


            // AllowDrop = true ;  SB removed because it is overriding the user values

            setVisibleCore = new SetVisibleCoreDelegate(this.SetVisibleCore); //ft
            visibleSrcHdlr = new Channels.ValueChangedEventHandler(OnVisibleValueChanged);//ft
            visibleSourceChannel = null;//ft
            visibleSource = "";//ft

            propertyHdlr			= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

			toolTip = new ToolTip() ;
            toolTip.ShowAlways = true;

			customDescription		= "" ;
			descriptionSource		= TextSources.Description ;
			source					= "" ;
			sourceChannel			= null ;
			customToolTip			= "" ;
			toolTipSource			= TextSources.ToolTip ;
			//don't call this here, as relevant objects in overriding controls
			//haven't been created yet
			//UpdateLayout() ;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                PropertyChanged = null;
                ObjectSearch = null;

                if (components != null)
				{
					components.Dispose();
				}
                toolTip.RemoveAll();
                toolTip.Dispose();
            }
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the control.
		/// </summary>
		[Browsable(true)]
		[Category("General")]
		public virtual new string Name //ft mis la propriété Virtual
		{
			get { return base.Name; }
			set { base.Name = value; }
		}

		/// <summary>
		/// Controls collection - inherited from System.Windows.Forms.Control
		/// </summary>
		[Browsable(false)]
		public new ControlCollection Controls
		{
			get { return base.Controls; }
		}
		private bool ShouldSerializeControls()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets a Custom Description
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets a Custom Description")]
		[DefaultValue("")]
		public virtual string CustomDescription
		{
			get
			{
				return customDescription ;
			}
			set
			{
				if (value != customDescription)
				{
					customDescription = value ;
					OnPropertyChanged("CustomDescription") ;
					
					if (descriptionSource == TextSources.Custom)
					{
						OnPropertyChanged("Description") ;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets where to get the Description from
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets where to get the Description from")]
		[DefaultValue(TextSources.Description)]
		public virtual TextSources DescriptionSource
		{
			get
			{
				return descriptionSource ;
			}
			set
			{
				if (descriptionSource != value)
				{
					descriptionSource = value ;
					OnPropertyChanged("DescriptionSource") ;
					OnPropertyChanged("Units") ;
				}
			}
		}

		/// <summary>
		/// Gets the Description displayed. Value depends on DescriptionSource property
		/// </summary>
		[Category("Description")]
		[Description("Gets the Description displayed. Value depends on DescriptionSource property")]
		public string Description
		{
			get
			{
				string			text	= "" ;

				if (descriptionSource == TextSources.Custom)
				{
					text = customDescription ;
				}
				else if (sourceChannel != null)
				{
					if (descriptionSource == TextSources.Description)
					{
						text = sourceChannel.Description ;
					}
					else
					{
						text = sourceChannel.ToolTip ;
					}
				}
				return text ;
			}
		}

		/// <summary>
		/// Gets or sets the custom Tool Tip
		/// </summary>
		[Category("ToolTip")]
		[Description("Gets or sets the custom Tool Tip")]
		[DefaultValue("")]
		public string CustomToolTip
		{
			get
			{
				return customToolTip ;
			}
			set
			{
				if (value != customToolTip)
				{
					customToolTip = value ;
					OnPropertyChanged("CustomToolTip") ;
					
					if (toolTipSource == TextSources.Custom)
					{
						NewToolTip(ToolTip) ;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets where the Tool Tip gets its value
		/// </summary>
		[Category("ToolTip")]
		[Description("Gets or sets where the Tool Tip gets its value")]
		[DefaultValue(TextSources.ToolTip)]
		public TextSources ToolTipSource
		{
			get
			{
				return toolTipSource ;
			}
			set
			{
				if (toolTipSource != value)
				{
					toolTipSource = value ;
					OnPropertyChanged("ToolTipSource") ;
					NewToolTip(ToolTip) ;
				}
			}
		}

		/// <summary>
		/// Gets the current Tool Tip
		/// </summary>
		[Category("ToolTip")]
		[Description("Gets the current Tool Tip")]
		public string ToolTip
		{
			get
			{
				string			text	= "" ;

				if (toolTipSource == TextSources.Custom)
				{
					text = customToolTip ;
				}
				else if (sourceChannel != null)
				{
					if (toolTipSource == TextSources.Description)
					{
						text = sourceChannel.Description ;
					}
					else
					{
						text = sourceChannel.ToolTip ;
					}
				}
				return text ;
			}
		}

		/// <summary>
		/// Gets the current Value
		/// </summary>
		[Category("General")]
		[Description("Gets the current Value")]
		public virtual Values.Value Value
		{
			get
			{
				return (sourceChannel != null) ? sourceChannel.Value : Values.Values.NotConnected ;
			}
		}

		/// <summary>
		/// Gets or sets the name of Source channel whose value is displayed
		/// </summary>
		[Category("General")]
		[Description("Gets or sets the name of Source channel whose value is displayed")]
		[DefaultValue("")]
		public string Source
		{
			get
			{
				return source ;
			}
			set
			{
				if (source != value)
				{
					source = value ;
					OnPropertyChanged("Source") ;
					SourceChannel = OnObjectSearch(source) ;

					//force a repaint, as description etc has likely changed
                    //Invalidate(DisplayRectangle) ;  ft deplace dans SourceChannel
				}
			}
		}

		/// <summary>
		/// Gets or sets the Source channel to which this object is connected
		/// </summary>
		[Category("General")]
		[Description("Gets or sets the Source channel to which this object is connected")]
		[DefaultValue(null)]
		public virtual Channels.IInfoChannel SourceChannel
		{
			get
			{
				return sourceChannel ;
			}
			set
			{
				if (sourceChannel != value)
				{
					if (sourceChannel != null)		sourceChannel.PropertyChanged -= propertyHdlr ;

					sourceChannel = value ;
					if (sourceChannel != null)		sourceChannel.PropertyChanged += propertyHdlr ;

					OnPropertyChanged("SourceChannel") ;

					OnPropertyChanged("Minimum") ;
					OnPropertyChanged("Maximum") ;
					OnPropertyChanged("Units") ;

					NewToolTip(ToolTip) ;

                    //force a repaint, as description etc has likely changed
                    Invalidate(DisplayRectangle);//ft 
				}
			}
		}
		#endregion

		#region Unused Properties - boolean handling
//		private bool UseCustomBooleanText
//		{
//			get
//			{
//				return useCustomBooleanText ;
//			}
//			set
//			{
//				if (value != useCustomBooleanText)
//				{
//					useCustomBooleanText = value ;
//					OnPropertyChanged("UseCustomBooleanText") ;
//					OnPropertyChanged("FalseText") ;
//					OnPropertyChanged("TrueText") ;
//				}
//			}
//		}
//
//		private string CustomFalseText
//		{
//			get
//			{
//				return customFalseText ;
//			}
//			set
//			{
//				if (value != customFalseText)
//				{
//					customFalseText = value ;
//					OnPropertyChanged("CustomFalseText") ;
//					
//					if (useCustomBooleanText)
//					{
//						OnPropertyChanged("FalseText") ;
//					}
//				}
//			}
//		}
//
//		private string CustomTrueText
//		{
//			get
//			{
//				return customTrueText ;
//			}
//			set
//			{
//				if (value != customTrueText)
//				{
//					customTrueText = value ;
//					OnPropertyChanged("CustomTrueText") ;
//					
//					if (useCustomBooleanText)
//					{
//						OnPropertyChanged("TrueText") ;
//					}
//				}
//			}
//		}
//
//		private string FalseText
//		{
//			get
//			{
//				string			text		= "False" ;
//
//				if (useCustomBooleanText)
//				{
//					text = customFalseText ;
//				}
//				else if (sourceChannel is Channels.BooleanChannel)
//				{
//					text = ((Channels.BooleanChannel)sourceChannel).FalseText ;
//				}
//				return text ;
//			}
//		}
//
//		private string TrueText
//		{
//			get
//			{
//				string			text		= "True" ;
//
//				if (useCustomBooleanText)
//				{
//					text = customTrueText ;
//				}
//				else if (sourceChannel is Channels.BooleanChannel)
//				{
//					text = ((Channels.BooleanChannel)sourceChannel).TrueText ;
//				}
//				return text ;
//			}
//		}

		#endregion

		#region Property Changed handling (in and out)
		/// <summary>
		/// Standard Event generated whenever a property (including the Value) is changed
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// The named property has changed: generate the PropertyChanged event
		/// </summary>
		/// <param name="propertyName">Name of property that has changed</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			//Ici c'est l'equivalent d'un RaiseEvent, par contre on doit verifier si un objet s'est attache au handler sur le PropertyChanged pour trigger l'event
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));  //Invokes the delegates.
		}

		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		protected virtual void OnSourcePropertyChanged(object Sender, PropertyChangedEventArgs e)
		{
			if ((toolTipSource == TextSources.Description && e.PropertyName == "Description") ||
				(toolTipSource == TextSources.ToolTip && e.PropertyName == "ToolTip")			)
			{
				NewToolTip(ToolTip) ;
			}
		}
		#endregion

		#region IConnectable
		/// <summary>
		/// Event for requests to find an object by name.
		/// </summary>
		public event KineticaRT.ObjectSearchEventHandler ObjectSearch;   
		/// <summary>
		/// Fires an ObjectSearch event for the named object
		/// </summary>
		/// <summary>
		/// Fires an ObjectSearch event for the named object
		/// </summary>
		protected Channels.IInfoChannel OnObjectSearch(string objectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName) as Channels.IInfoChannel ;  
		}

		/// <summary>
		/// Looks for the named object
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>Always returns null</returns>
		public virtual object FindObject(string objectName)
		{
			return null ;
		}

		/// <summary>
		/// Returns the named sibling object or null if it does not exist.
		/// </summary>
		public virtual object SearchObject (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

        private System.Drawing.Color originalBackColor;
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public virtual void Connect ()
		{
            if (originalBackColor == null || this.BackColor != Color.Fuchsia)
            {
                originalBackColor = this.BackColor;
            }
            else
            {
                this.BackColor = originalBackColor;
            }
            if (source != "")
            {
                SourceChannel = OnObjectSearch(source);

                if (this.sourceChannel == null && source.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }
            if (visibleSource != "")
            {
                VisibleSourceChannel = OnObjectSearch(visibleSource) as Channels.IChannel;
                if (this.visibleSourceChannel == null && this.visibleSource.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }
            this.Connected = true; //ft
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
            if (visibleSourceChannel != null) VisibleSourceChannel = null;
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
		#endregion

		#region Private Methods
		/// <summary>
		/// Override this method to update the layout of the controls various sections.
		/// Called when resized.
		/// </summary>
		protected virtual void UpdateLayout()
		{
		}
     
		/// <summary>
		/// A new tool tip is being set - override to ensure all sub-objects are set correctly
		/// </summary>
		protected virtual void NewToolTip(string NewText)
		{
			toolTip.SetToolTip(this, ToolTip) ;
			OnPropertyChanged("ToolTip") ;
		}
		#endregion

		#region Standard Events
		/// <summary>
		/// Rezise event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e) ;
			UpdateLayout() ;
         
		}
		#endregion

		#region Drag / Drop operations
		/// <summary>
		/// DragEnter event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnDragEnter (DragEventArgs e)
		{
			//check if allowed to drop things, and if drag object holds text
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}

			base.OnDragEnter(e) ;
		}

		/// <summary>
		/// DragDrop eventNumericVariable
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnDragDrop (DragEventArgs e)
		{
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				Source = e.Data.GetData(DataFormats.Text).ToString() ;
			}

			base.OnDragDrop(e) ;
		}
               
        /// <summary>
        /// Mouse Leave Event
        /// </summary>
        /// <param name="e">Event Data</param>protected override void OnMouseLeave(EventArgs e)
        protected override void OnMouseLeave(EventArgs e)    
        {
            toolTip.Active = false;            
            base.OnMouseLeave(e); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            toolTip.Active = true;
            base.OnMouseEnter(e);
        }
 

        ////ft test
        //protected override void OnBackColorChanged(EventArgs e)
        //{
        //    if (this.Parent != null)
        //    {
        //        Parent.Invalidate(this.Bounds, true);
        //    }
        //    base.OnBackColorChanged(e);
        //}

        //protected override void OnParentBackColorChanged(EventArgs e)
        //{
        //    this.Invalidate();
        //    base.OnParentBackColorChanged(e);
        //}
        //protected override CreateParams CreateParams //ft pour permettre d<avoir les controle en transparent mais cause plus de probleme...
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
        //        return cp;
        //    }
        //}
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);
        //    //e.Graphics.Clear(BackColor);
        //    if (this.BackColor != Color.Transparent)
        //    {
        //        e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
        //    }
        //}
		#endregion


        delegate void SetVisibleCoreDelegate(bool value);//ft
        private SetVisibleCoreDelegate setVisibleCore;//ft
        /// <summary>
        /// This member overrides Control.SetVisibleCore to make it thread safe.
        /// </summary>
        /// <remarks>The changes will be mode asynchronously.</remarks>
        /// <param name="value">true to make the control visible; otherwise, false.</param>
        protected override void SetVisibleCore(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(setVisibleCore, new object[] { value });
            }
            else
            {
                base.SetVisibleCore(value);
            }
        }
        /// <summary>
        /// Gets or sets the object that is the Source of the Button's InputEnabled value
        /// </summary>
        private Channels.IChannel VisibleSourceChannel //ft ajout
        {
            set
            {
                if (visibleSourceChannel != value)
                {
                    //free old handler if any
                    if (visibleSourceChannel != null)
                    {
                        visibleSourceChannel.ValueChanged -= visibleSrcHdlr;
                    }

                    //attach new source
                    visibleSourceChannel = value;
                    if (visibleSourceChannel != null)
                    {
                        visibleSourceChannel.ValueChanged += visibleSrcHdlr;
                        
                        this.Visible = (visibleSourceChannel.Value != null) && visibleSourceChannel.Value.ToDouble() != 0.0;
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the name of the object to be the Button's Visible Source.
        /// </summary>
        /// <remarks>If this is set to a valid channel then it overrides the value of Visible.</remarks>
        [DefaultValue("")]
        [Category("General")]
        [Description("Gets or sets the name of the object to be the control's Visible Source. ")]
        public string VisibleSource //ft ajout
        {
            get
            {
                return visibleSource;
            }
            set
            {
                if (visibleSource != value)
                {
                    visibleSource = value;
                    //try and connect to source
                    VisibleSourceChannel = OnObjectSearch(visibleSource) as Channels.IChannel;

                    OnPropertyChanged("VisibleSource");
                }
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newValue"></param>
        private void OnVisibleValueChanged(Channels.IChannel sender, KineticaRT.Values.Value newValue) //ft ajout
        {
            lock (this)
            {
                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Input Visible Value Change: " + newValue.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name);
                bool boolean = newValue.ToDouble() != 0.0;
                this.Visible = boolean;
            }
        }
	}
}

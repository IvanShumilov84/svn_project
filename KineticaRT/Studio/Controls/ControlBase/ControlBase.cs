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
	public class ControlBase : System.Windows.Forms.UserControl, IConnectable
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool					useCustomBooleanText ;
		private string					customFalseText ;
		private string					customTrueText ;
		private string					customDescription ;
		private TextSources				descriptionSource ;
		private Channels.InfoChannel				sourceChannel ;
		private string					source ;
		private string					customToolTip ;
		private TextSources				toolTipSource ;

		private	ToolTip					toolTip ;

		private PropertyChangedEventHandler	propertyHdlr ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the ControlBase class
		/// </summary>
		public ControlBase()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.ResizeRedraw = true ;
			this.SetStyle(ControlStyles.UserPaint, true) ;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;

			propertyHdlr			= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

			toolTip = new ToolTip() ;
			toolTip.ShowAlways = true ;

			useCustomBooleanText	= false ;
			customFalseText			= "False" ;
			customTrueText			= "True" ;
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
				if(components != null)
				{
					components.Dispose();
				}
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

		#region Properties (private unless proved needed)
		private bool UseCustomBooleanText
		{
			get
			{
				return useCustomBooleanText ;
			}
			set
			{
				if (value != useCustomBooleanText)
				{
					useCustomBooleanText = value ;
					OnPropertyChanged("UseCustomBooleanText") ;
					OnPropertyChanged("FalseText") ;
					OnPropertyChanged("TrueText") ;
				}
			}
		}

		private string CustomFalseText
		{
			get
			{
				return customFalseText ;
			}
			set
			{
				if (value != customFalseText)
				{
					customFalseText = value ;
					OnPropertyChanged("CustomFalseText") ;
					
					if (useCustomBooleanText)
					{
						OnPropertyChanged("FalseText") ;
					}
				}
			}
		}

		private string CustomTrueText
		{
			get
			{
				return customTrueText ;
			}
			set
			{
				if (value != customTrueText)
				{
					customTrueText = value ;
					OnPropertyChanged("CustomTrueText") ;
					
					if (useCustomBooleanText)
					{
						OnPropertyChanged("TrueText") ;
					}
				}
			}
		}

		private string FalseText
		{
			get
			{
				string			text		= "False" ;

				if (useCustomBooleanText)
				{
					text = customFalseText ;
				}
				else if (sourceChannel is Channels.BooleanChannel)
				{
					text = ((Channels.BooleanChannel)sourceChannel).FalseText ;
				}
				return text ;
			}
		}

		private string TrueText
		{
			get
			{
				string			text		= "True" ;

				if (useCustomBooleanText)
				{
					text = customTrueText ;
				}
				else if (sourceChannel is Channels.BooleanChannel)
				{
					text = ((Channels.BooleanChannel)sourceChannel).TrueText ;
				}
				return text ;
			}
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
		public Values.Value Value
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
					Invalidate(DisplayRectangle) ;
				}
			}
		}

		/// <summary>
		/// Source channel to which this object is connected
		/// </summary>
		protected virtual Channels.InfoChannel SourceChannel
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

					if (sourceChannel is Channels.BooleanChannel)
					{
						OnPropertyChanged("TrueText") ;
						OnPropertyChanged("FalseText") ;
					}
				}
			}
		}
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
		protected Channels.InfoChannel OnObjectSearch(string ObjectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(ObjectName) as Channels.InfoChannel ;  
		}

		/// <summary>
		/// Looks for the named object
		/// </summary>
		/// <param name="ObjectName">Name of Object to find</param>
		/// <returns>Always returns null</returns>
		public virtual object FindObject(string ObjectName)
		{
			return null ;
		}

		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public virtual void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
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
		/// DragDrop event
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
		#endregion
	}
}

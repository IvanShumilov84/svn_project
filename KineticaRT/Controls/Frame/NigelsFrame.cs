using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using KineticaRT;
//using System.CodeDom;
//using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Base class for customised KineticaRT Panels
	/// </summary>
	public class PanelBase : KineticaRT.Controls.Control
	{
		/// <summary>
		/// Load the Panel and initialise all the properties.
		/// </summary>
		public PanelBase ()
		{
			InitializePanel () ;
		}

		/// <summary>
		/// Saves the current properties, child controls etc to a code source file.
		/// </summary>
		/// <remarks>Normally called from "Save" on the panels context menu.</remarks>
		public void Save ()
		{
			// Code from Frame.SaveFile.
		}

		/// <summary>
		/// Initialize the panel.
		/// </summary>
		/// <remarks>This should be overriden in the derived classes to initialise the properties and create the child controls.</remarks>
		protected virtual void InitializePanel () 
		{
		}
	}
	
	/// <summary>
	/// Addable Class - information about objects that can be added to a Frame
	/// </summary>
	public class Addable : System.Object
	{
		private string			name ;
		private string			assembly ;
		private string			typename ;

		/// <summary>
		/// Initialise an Addable
		/// </summary>
		/// <param name="Name">Friendly name of control</param>
		/// <param name="Assembly">Name of Assembly in which the control is found</param>
		/// <param name="Typename">Typename of control</param>
		public Addable(string Name, string Assembly, string Typename)
		{
			name = Name ;
			assembly = Assembly ;
			typename = Typename ;
		}

		/// <summary>
		/// Friendly name of control
		/// </summary>
		public string Name
		{
			get { return name; }
		}
		/// <summary>
		/// Name of Assembly in which the control is found
		/// </summary>
		public string Assembly
		{
			get { return assembly; }
		}
		/// <summary>
		/// Typename of control
		/// </summary>
		public string Typename
		{
			get { return typename; }
		}
	}


	/// <summary>
	/// Information about a Grid
	/// </summary>
	[TypeConverter(typeof(Grid.GridTypeConverter))]
	public class Grid : System.Object
	{
		#region Type Converter
		/// <summary>
		/// This type converter extends the basic TypeConverter to allow conversion to an
		/// InstanceDescriptor, which allows the .NET Framework to generate source code for the object.
		/// </summary>
		internal class GridTypeConverter : ExpandableObjectConverter
		{
			/// <summary>
			/// Returns whether a desired conversion is possible
			/// </summary>
			/// <param name="context">Context</param>
			/// <param name="destinationType">Desired type</param>
			/// <returns>True if conversion is possible</returns>
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					return true;
				}

				// Always call the base to see if it can perform the conversion.
				return base.CanConvertTo(context, destinationType);
			}

			/// <summary>
			/// Performs the actual conversion to an InstanceDescriptor.
			/// Override to use other than the Empty Constructor
			/// </summary>
			/// <param name="context">Context</param>
			/// <param name="culture">Culture Info</param>
			/// <param name="value">Object to be converted</param>
			/// <param name="destinationType">Type to convert to</param>
			/// <returns>Converted object</returns>
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					Grid				grid		= (Grid) value;
					ConstructorInfo		ctor_info ;
					
					Type[]				types	= new Type[3]{typeof(bool), typeof(int), typeof(int)} ;

					ctor_info = typeof(Grid).GetConstructor(types);

					return new InstanceDescriptor(ctor_info, new object[]{grid.Enabled, grid.Size, grid.Spacing}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		private	int			size ;
		private int			spacing ;
		private bool		enabled ;

		/// <summary>
		/// Property Changed Event
		/// </summary>
		public event PropertyChangedEventHandler	PropertyChanged ;

		/// <summary>
		/// Creates a new instance of the Grid class
		/// </summary>
		public Grid()
		{
			size	= 8 ;
			spacing	= 2 ;
			enabled	= true ;
		}

		/// <summary>
		/// Creates a new instance of the grid class with the given properties
		/// </summary>
		/// <param name="Enabled">Value of Enabled property</param>
		/// <param name="Size">Value of Size property</param>
		/// <param name="Spacing">Value of Spacing property</param>
		public Grid (bool Enabled, int Size, int Spacing)
		{
			enabled = Enabled ;
			size	= Size ;
			spacing = Spacing ;
		}

		#region Properties
		/// <summary>
		/// Size of grid (pixels)
		/// </summary>
		[DefaultValue(8)]
		public int Size
		{
			get
			{
				return size ;
			}
			set
			{
				if (size != value)
				{
					size = value ;
					OnPropertyChanged("Size") ;
				}
			}
		}

		/// <summary>
		/// Spacing of objects on grid (pixels)
		/// - ie objects are (n * size) - spacing pixels
		/// </summary>
		[DefaultValue(2)]
		public int Spacing
		{
			get
			{
				return spacing ;
			}
			set
			{
				if (spacing != value)
				{
					spacing = value ;
					OnPropertyChanged("Spacing") ;
				}
			}
		}

		/// <summary>
		/// Whether grid is enabled
		/// </summary>
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return enabled ;
			}
			set
			{
				if (enabled != value)
				{
					enabled = value ;
					OnPropertyChanged("Enabled") ;
				}
			}
		}

		/// <summary>
		/// Number of pixels in one grid unit
		/// - ie Size if grid enabled, else 1
		/// </summary>
		public int Pixels
		{
			get
			{
				if (enabled)		return size ;
				return 1 ;
			}
		}

		/// <summary>
		/// Minimum number of pixels
		/// - ie (Size - Spacing) or 0 if Spacing > Size
		/// </summary>
		public int MinSize
		{
			get
			{
				if (size > spacing)
				{
					return size - spacing ;
				}
				return 0 ;
			}
		}
		#endregion

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) 
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));  //Invokes the delegates.
			}
		}
	}


	/// <summary>
	/// Frame: Container Control
	/// </summary>
//	[Serializable]
//	[DesignerSerializer(typeof(MyCodeDomSerializer), typeof(CodeDomSerializer))]
	public class Frame : KineticaRT.Controls.Control
	{
		#region Variables
		/// <summary>
		/// Handler for GetAddList event
		/// </summary>
		public delegate void GetAddListEventHandler(object sender, out ArrayList AddList);

		/// <summary>
		/// Event generated to retrieve AddList from parent Frame
		/// </summary>
		public event GetAddListEventHandler			GetAddList ;

		//property variables
		private	bool				titleEnabled ;
		private Color				titleLeftColor ;
		private Color				titleRightColor ;
		private Color				titleUnderlineColor ;
		private bool				titleUnderlined ;
		private string				titleText ;
		private Grid				horizontalGrid ;
		private Grid				verticalGrid ;
		private string				addListFile ;

		//other variables
		internal	ArrayList			addList ;
		private		EditForm			editForm ;
		private		bool				editMode ;
		private		Rectangle			titleRect ;
		private		int					titleHeight ;
		private		bool				needCalcTitleHeight ;
		private		StringFormat		titleFormat ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ObjectSearchEventHandler		searchHdlr ;
		private PropertyChangedEventHandler		gridHdlr ;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Frame class
		/// </summary>
		public Frame()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//set up menu
			MenuItem []		items ;
			items = new MenuItem [1] ;
			items[0] = new MenuItem("Enter &Edit Mode", new EventHandler(OnClickEditMode), Shortcut.CtrlE) ;
			this.ContextMenu = new ContextMenu(items) ;

			titleEnabled		= true ;
			titleUnderlined		= true ;
			titleUnderlineColor	= Color.SteelBlue ;
			titleLeftColor		= Color.White ;
			titleRightColor		= Color.LightSteelBlue ;
			ForeColor			= Color.SteelBlue ;
			titleText			= "KineticaRT.Controls.Frame" ;
			addListFile			= "" ;

			horizontalGrid		= new Grid() ;
			verticalGrid		= new Grid() ;

			titleRect			= Rectangle.Empty ;
			titleHeight			= 15 ;
			needCalcTitleHeight	= true ;
			titleFormat			= new StringFormat() ;
			titleFormat.LineAlignment = StringAlignment.Center ;

			addList = new ArrayList() ;

			editMode = false ;

//			//temporary...
//			Controls.Add(new Button()) ;
//			Controls.Add(new Button()) ;
//			Controls[0].Name = "btn 0" ;
//			Controls[0].BackColor = Color.AliceBlue ;
//			Controls[0].Location = new Point(50, 50) ;
//			Controls[1].Name = "btn 1" ;
//			Controls[1].BackColor = Color.Beige ;
//			Controls[1].Location = new Point(150, 50) ;
//			//...

			searchHdlr = new ObjectSearchEventHandler(OnChildObjectSearch);

			gridHdlr = new PropertyChangedEventHandler(OnGridChanged) ;
			horizontalGrid.PropertyChanged	+= gridHdlr ;
			verticalGrid.PropertyChanged	+= gridHdlr ;

			if (GetAddList != null)
			{
				ArrayList		newList ;
				GetAddList(this, out newList);
				addList.Clear() ;
				addList = newList ;
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				titleFormat.Dispose() ;

				if( components != null )
					components.Dispose();
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
			// 
			// Frame
			// 
			this.Name = "Frame";
			this.Size = new System.Drawing.Size(272, 168);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Frame Shape: must be a rectangle
		/// </summary>
		[Category("Control")]
		[Description("Frame Shape: must be a rectangle")]
		public override Shapes Shape
		{
			get { return Shapes.Rectangle; }
			set { shape = Shapes.Rectangle; }
		}

		//		[Browsable(true)]
		//		public new ControlCollection Controls
		//		{
		//			get { return base.Controls ; }
		//		}

		/// <summary>
		/// Name of file containing information about addable controls
		/// </summary>
		[Category("Frame")]
		[DefaultValue("")]
		[Description("Name of file containing information about addable controls")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string AddListFile
		{
			get
			{
				return addListFile ;
			}
			set
			{
				if (addListFile != value)
				{
					addListFile = value ;
					ReadAddListFile() ;
					OnPropertyChanged("AddListFile") ;
				}
			}
		}

		/// <summary>
		/// Title bar enabled
		/// </summary>
		[Category("Frame")]
		[DefaultValue(true)]
		[Description("Title bar enabled")]
		public bool TitleEnabled
		{
			get
			{
				return titleEnabled ;
			}
			set
			{
				if (titleEnabled != value)
				{
					titleEnabled = value ;
					needCalcTitleHeight = true ;
					AddToQueue(true) ;
					OnPropertyChanged("TitleEnabled") ;
				}
			}
		}

		/// <summary>
		/// Title bar is underlined
		/// </summary>
		[Category("Frame")]
		[DefaultValue(true)]
		[Description("Title bar is underlined")]
		public bool TitleUnderlined
		{
			get
			{
				return titleUnderlined ;
			}
			set
			{
				if (titleUnderlined != value)
				{
					titleUnderlined = value ;
					needCalcTitleHeight = true ;
					if (titleEnabled)			AddToQueue(true) ;
					OnPropertyChanged("TitleUnderlined") ;
				}
			}
		}

		/// <summary>
		/// Text in Title bar
		/// </summary>
		[Category("Frame")]
		[DefaultValue("KineticaRT.Controls.Frame")]
		[Description("Text in Title bar")]
		public string TitleText
		{
			get
			{
				return titleText ;
			}
			set
			{
				if (titleText != value)
				{
					titleText = value ;
					if (titleEnabled)			AddToQueue(false) ;
					OnPropertyChanged("TitleText") ;
				}
			}
		}

		/// <summary>
		/// Color at left of title bar
		/// </summary>
		[Category("Frame")]
		[Description("Color at left of title bar")]
		public Color TitleLeftColor
		{
			get
			{
				return titleLeftColor ;
			}
			set
			{
				if (titleLeftColor != value)
				{
					titleLeftColor = value ;
					if (titleEnabled)		AddToQueue(false) ;
					OnPropertyChanged("TitleLeftColor") ;
				}
			}
		}
		private bool ShouldSerializeTitleLeftColor()
		{
			return titleLeftColor != Color.White ;
		}
		/// <summary>
		/// Reset the Color to its default value
		/// </summary>
		public void ResetTitleLeftColor()
		{
			titleLeftColor = Color.White ;
		}

		/// <summary>
		/// Color at right of title bar
		/// </summary>
		[Category("Frame")]
		[Description("Color at right of title bar")]
		public Color TitleRightColor
		{
			get
			{
				return titleRightColor ;
			}
			set
			{
				if (titleRightColor != value)
				{
					titleRightColor = value ;
					if (titleEnabled)		AddToQueue(false) ;
					OnPropertyChanged("TitleRightColor") ;
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
			titleRightColor = Color.LightSteelBlue ;
		}

		/// <summary>
		/// Color of title bar underline
		/// </summary>
		[Category("Frame")]
		[Description("Color of title bar underline")]
		public Color TitleUnderlineColor
		{
			get
			{
				return titleUnderlineColor ;
			}
			set
			{
				if (titleUnderlineColor != value)
				{
					titleUnderlineColor = value ;
					if (titleEnabled)		AddToQueue(false) ;
					OnPropertyChanged("TitleUnderlineColor") ;
				}
			}
		}
		private bool ShouldSerializeTitleUnderlineColor()
		{
			return titleUnderlineColor != Color.SteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default value
		/// </summary>
		public void ResetTitleUnderlineColor()
		{
			titleUnderlineColor = Color.SteelBlue ;
		}

		/// <summary>
		/// Horizontal Grid
		/// </summary>
		[Category("Frame")]
		[Description("Horizontal Grid")]
		public Grid HorizontalGrid
		{
			get
			{
				return horizontalGrid ;
			}
			set
			{
				//ignore setting to null - invalid
				if (value != null)
				{
					horizontalGrid.PropertyChanged -= gridHdlr ;
					horizontalGrid = value ;
					horizontalGrid.PropertyChanged += gridHdlr ;

					//## TBD - handle new grid sizes like in OnGridChanged()
					OnPropertyChanged("HorizontalGrid") ;
				}
			}
		}

		/// <summary>
		/// Vertical Grid
		/// </summary>
		[Category("Frame")]
		[Description("Vertical Grid")]
		public Grid VerticalGrid
		{
			get
			{
				return verticalGrid ;
			}
			set
			{
				//ignore setting to null - invalid
				if (value != null)
				{
					verticalGrid.PropertyChanged -= gridHdlr ;
					verticalGrid = value ;
					verticalGrid.PropertyChanged += gridHdlr ;

					//## TBD - handle new grid sizes like in OnGridChanged()
					OnPropertyChanged("VerticalGrid") ;
				}
			}
		}
		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// Called from OnPaint and the Control's event pool Update
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		protected override void Repaint(bool FullRedraw, Graphics g)
		{
			base.Repaint(FullRedraw, g) ;

			CalculateDisplayRectangle() ;
			//offset title (if enabled)
			if (titleEnabled)
			{
				Rectangle		rect		= DisplayRectangle ;

				if (needCalcTitleHeight)
				{
					titleHeight = this.Font.Height + 6 ;
					if (titleUnderlined)		titleHeight += 2 ;

					needCalcTitleHeight = false ;
				}
				titleRect = rect ;
				titleRect.Height = titleHeight ;

				rect.Y		= titleRect.Bottom ;
				rect.Height -= titleRect.Height ;
				displayRect	= rect ;
			}

			//##only need to call this if display rectangle has changed
			PerformLayout() ;

			DrawTitle(g) ;
		}

		private void DrawTitle(Graphics g)
		{
			if (!titleEnabled)			return ;

			Brush			brush ;
			Rectangle		rect			= titleRect ;

			//underline?
			if (titleUnderlined)
			{
				brush = new SolidBrush(titleUnderlineColor) ;
				g.FillRectangle(brush, rect.X, rect.Bottom - 2, rect.Width, 2) ;
				brush.Dispose() ;
				rect.Height -= 2 ;
			}

			brush = new LinearGradientBrush(rect, titleLeftColor, titleRightColor, LinearGradientMode.Horizontal) ;

			g.FillRectangle(brush, rect) ;
			brush.Dispose() ;

			//## sort out bezel

			//add text
			brush = new SolidBrush(ForeColor) ;
			g.DrawString(titleText, this.Font, brush, rect, titleFormat) ;
			brush.Dispose() ;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a control from the add list
		/// </summary>
		/// <param name="Name">Name in add list</param>
		/// <returns>New Control (or null if failed</returns>
		public System.Windows.Forms.Control Add(string Name)
		{
			System.Windows.Forms.Control			new_ctrl	= null ;

			//find the index for this name
			for (int index = 0; index < addList.Count; index++)
			{
				if (((Addable)addList[index]).Name == Name)
				{
					//add a control
					new_ctrl = Add(index) ;
					break ;
				}
			}

			return new_ctrl ;
		}

		/// <summary>
		/// Adds a control from the add list by index
		/// </summary>
		/// <param name="Index">Zero-based index in add list</param>
		/// <returns>New Control (or null if failed</returns>
		public System.Windows.Forms.Control Add(int Index)
		{
			System.Windows.Forms.Control			new_ctrl	= null ;
			System.Runtime.Remoting.ObjectHandle	obj_ctrl ;
			Addable									addable		= addList[Index] as Addable ;

			obj_ctrl = Activator.CreateInstanceFrom (addable.Assembly, addable.Typename) ;
			new_ctrl = (System.Windows.Forms.Control)obj_ctrl.Unwrap() ;
			if (new_ctrl != null) 
			{
				//ensure name is unique
				while (FindControl(new_ctrl.Name) != null)
				{
					//## this is not sensible names, but will be unique
					new_ctrl.Name += "1" ;
				}

				this.Controls.Add(new_ctrl) ;
			}

			return new_ctrl ;
		}

		//returns null if can't find the matching object
		private System.Windows.Forms.Control FindControl(string CtrlName)
		{
			System.Windows.Forms.Control		ret_val		= null ;

			foreach (System.Windows.Forms.Control ctrl in Controls)
			{
				if (ctrl.Name == CtrlName)
				{
					ret_val = ctrl ;
					break ;
				}
			}
			return ret_val ;
		}
		
		//(can do this directly if call FindControl and set ctrl's Top etc)
		/// <summary>
		/// Moves the named control to the specified size and position
		/// </summary>
		/// <param name="CtrlName">Name of control to move</param>
		/// <param name="NewLeft">New Left position for control</param>
		/// <param name="NewTop">New Top position for control</param>
		/// <param name="NewWidth">New Width for control</param>
		/// <param name="NewHeight">New Height for control</param>
		public void MoveControl(string CtrlName, int NewLeft, int NewTop, int NewWidth, int NewHeight)
		{
			foreach (Control ctrl in Controls)
			{
				if (ctrl.Name == CtrlName)
				{
					ctrl.Top = NewTop ;
					ctrl.Left = NewLeft ;
					ctrl.Width = NewWidth ;
					ctrl.Height = NewHeight ;

					break ;
				}
			}
		}
		#endregion

		#region Other Methods
		private void ReadAddListFile()
		{
			if (addListFile != "")
			{
				XmlDocument		document	= new XmlDocument () ;
				XmlElement		root ;

				//ExpandEnvironmentStrings(add_list_file, file, MAX_PATH) ;

				try
				{
					document.Load (addListFile) ;

					//go through children to read various parts
					root = document.DocumentElement ;
					if (root != null)
					{
						//only load it if root is 'Addables'
						if (root.Name == "Addables")
						{
							//read new list
							ReadAddList(root, document) ;
						}
					}
				}
				catch(XmlException e)
				{
					Trace.WriteLine(e.Message, "Parse AddListFile Failed") ;
					return ;
				}
			}
		}

		private void ReadAddList(XmlElement Root, XmlDocument Doc)
		{
			XmlNode			child ;
			XmlElement		element ;
			string			name ;
			string			assembly ;
			string			type ;

			//got a valid XML file of expected type - clear old list
			addList.Clear() ;

			//go through all children of this node
			child = Root.FirstChild ;
			while (child != null)
			{
				if (child is XmlElement)
				{
					element = child as XmlElement ;

					//check type
					if (element.Name == "Add")
					{
						//get details...
						name = element.GetAttribute("Name") ;
						type = element.GetAttribute("Type") ;
						assembly = element.GetAttribute("Assembly") ;

						if (name != "" && type != "" && assembly != "")
						{
							//... and add to list
							addList.Add(new Addable(name, assembly, type)) ;
						}
					}
				}
				//else not an element - ignore it

				//go on to next node
				child = child.NextSibling ;
			}

			//##pass new list on to all child frames?
		}
		#endregion

		#region Events
		/// <summary>
		/// ControlAdded Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnControlAdded(ControlEventArgs e)
		{
			if (e.Control is KineticaRT.Controls.Control)
			{
				KineticaRT.Controls.Control		krt_ctrl	= e.Control as KineticaRT.Controls.Control ;
				krt_ctrl.ObjectSearch += searchHdlr ;
			}
			base.OnControlAdded(e) ;
		}

		/// <summary>
		/// ControlRemoved Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			if (e.Control is KineticaRT.Controls.Control)
			{
				KineticaRT.Controls.Control		krt_ctrl	= e.Control as KineticaRT.Controls.Control ;
				krt_ctrl.ObjectSearch += searchHdlr ;
			}
			base.OnControlRemoved(e) ;
		}

		/// <summary>
		/// Font changed event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnFontChanged(EventArgs e)
		{
			needCalcTitleHeight = true ;
			if (titleEnabled)			AddToQueue(true) ;

			base.OnFontChanged(e) ;
		}
		#endregion

		#region Mouse Events
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				//bring up a menu
				this.ContextMenu.Show(this, new Point(e.X, e.Y)) ;
			}

			base.OnMouseDown(e) ;
		}
		#endregion

		#region Menu Handling
		private void OnClickEditMode(object sender, EventArgs e)
		{
//edit ctrl...
//			if (!Contains(editCtrl))
//			{
//				this.Controls.Add(editCtrl) ;
//			}
//
//			editCtrl.Size = this.Size ;
//			editCtrl.BringToFront() ;

//edit form...
			editMode = true ;
			editForm = new EditForm(this) ;
			editForm.Closed			+= new EventHandler(OnEditClosed) ;
			editForm.Size = this.Size ;
			editForm.Location = this.PointToScreen(new Point(0,0)) ;
			editForm.ShowDialog(this) ;
		}

		/// <summary>
		/// Mouse Enter Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			if (editMode)
			{
				editForm.Size = this.Size ;
				editForm.Location = this.PointToScreen(new Point(0,0)) ;
				editForm.ShowDialog(this) ;
			}
			base.OnMouseEnter(e) ;
		}
		#endregion

		#region Other Events (editing & grid)
		private void OnEditClosed (object sender, EventArgs e)
		{
			editMode = false ;
			editForm.Dispose() ;
		}

		private void OnGridChanged(object sender, PropertyChangedEventArgs e)
		{
			//## TBD
		}
		#endregion

		#region Object Search
		/// <summary>
		/// Returns the named object or null if it does not exist.
		/// </summary>
		public virtual object FindObject (string objectName)
		{
			return FindControl(objectName) ;
		}

		/// <summary>
		/// Event handler for requests from the objects children to find another object for them by name.
		/// </summary>
		protected object OnChildObjectSearch (string objectName)
		{
			object found	= FindObject (objectName) ;

			//pass the event on if we haven't succeeded
			if (found == null) found = OnObjectSearch (objectName) ;
			return found ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			foreach (System.Windows.Forms.Control ctrl in this.Controls)
			{
				if (ctrl is KineticaRT.Controls.Control)
				{
					((KineticaRT.Controls.Control)ctrl).Connect() ;
				}
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			foreach (System.Windows.Forms.Control ctrl in this.Controls)
			{
				if (ctrl is KineticaRT.Controls.Control)
				{
					((KineticaRT.Controls.Control)ctrl).Disconnect() ;
				}
			}
		}
		#endregion

		#region XML Load and Save
		/// <summary>
		/// Load the named XML file
		/// </summary>
		/// <param name="File">XML File to load</param>
		public void LoadFile(string File)
		{
			XmlDocument		document	= new XmlDocument () ;
//			XmlElement		root ;
//			XmlElement		item ;
//
//			try
//			{
//				document.Load (File) ;
//			}
//			catch(XmlException e)
//			{
//				Trace.WriteLine(e.Message, "XML Parse Failed") ;
//				return ;
//			}
//
//			root = document.DocumentElement ;
//			item = (XmlElement)root.SelectSingleNode ("Frame") ;
//		
//			int		start = Environment.TickCount ;
//			int		end ;
//			LoadXML (item) ;
//			end = Environment.TickCount ;
//			//Trace.WriteLine(end - start, "Load time") ;
//			//System.Windows.Forms.MessageBox.Show((end - start).ToString()) ;
		}

		/// <summary>
		/// Save the named XML file
		/// </summary>
		/// <param name="Filename">File to save to</param>
		public void SaveFile(string Filename)
		{
//			XmlDocument		document	= new XmlDocument () ;
//			XmlElement		root ;
//			XmlElement		item ;
//
//			try
//			{
//				document.Load (File) ;
//			}
//			catch(XmlException e)
//			{
//				Trace.WriteLine(e.Message, "XML Parse Failed") ;
//				return ;
//			}
//
//			root = document.DocumentElement ;
//			item = (XmlElement)root.SelectSingleNode ("Frame") ;
//			
//			SaveXML (item) ;
//
//			document.Save(File) ;

			//Create our namespace, class and initialise function.
			CodeCompileUnit compileUnit = new CodeCompileUnit () ;
			CodeNamespace nameSpace = new CodeNamespace ("KineticaRT.Panel") ;
			CodeTypeDeclaration panel = new CodeTypeDeclaration("MyPanel");
			panel.BaseTypes.Add (typeof(KineticaRT.Controls.PanelBase)) ;
			panel.Comments.Add (new CodeCommentStatement ("Customized KineticaRT Panel", false)) ; 
			CodeMemberMethod initialise_panel = new CodeMemberMethod () ;
			initialise_panel.Comments.Add (new CodeCommentStatement ("Initialize the Panel", false)) ;
			initialise_panel.Name = "InitializePanel" ;
			initialise_panel.Attributes = MemberAttributes.Override | MemberAttributes.Family ;

			// Stick them all together.
			compileUnit.Namespaces.Add (nameSpace) ;
			nameSpace.Types.Add(panel);
			panel.Members.Add (initialise_panel) ;

			// Add all the initialisation code.
			// Start with our own properties.
			CodeAssignStatement assign = new CodeAssignStatement () ;
			assign.Left = new CodeVariableReferenceExpression("this.BackColor") ;
			assign.Right = new CodeVariableReferenceExpression("System.Drawing.Color.Fuchsia") ; // new CodePrimitiveExpression("5") ;
			initialise_panel.Statements.Add(assign) ;
			
			// Create a code generator.
			CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider ();
			ICodeGenerator generator = provider.CreateGenerator() ;
			CodeGeneratorOptions options = new CodeGeneratorOptions () ;
			options.BracingStyle = "C" ;
			options.BlankLinesBetweenMembers = false ;

			// Generate the code.
			generator.GenerateCodeFromCompileUnit (compileUnit, Console.Out, options) ;

			// Create a compiler.
			ICodeCompiler compiler = provider.CreateCompiler () ;
			CompilerParameters parameters = new CompilerParameters () ;
			parameters.ReferencedAssemblies.Add ("System.dll") ;
			parameters.ReferencedAssemblies.Add ("System.Drawing.dll") ;
			parameters.ReferencedAssemblies.Add ("System.Windows.Forms.dll") ;
			parameters.ReferencedAssemblies.Add ("KineticaRT.dll") ;
			parameters.ReferencedAssemblies.Add ("KineticaRT.Controls.dll") ;
			parameters.ReferencedAssemblies.Add ("KineticaRT.Controls.Frame.dll") ;

			CompilerResults results = compiler.CompileAssemblyFromDom (parameters, compileUnit) ;

			// Check for erorrs.
			if (results.Errors.HasErrors) 
			{
				string lcErrorMsg = "";

				// *** Create Error String
				lcErrorMsg = results.Errors.Count.ToString() + " Errors:";
				for (int x=0;x<results.Errors.Count;x++) 
				{
					lcErrorMsg = lcErrorMsg  + "\r\nLine: " + results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;		
				}

				MessageBox.Show(lcErrorMsg,"Compiler Demo",MessageBoxButtons.OK,MessageBoxIcon.Error);

				return;
			}

			// Save the dll file.
			System.IO.File.Copy (results.CompiledAssembly.Location, "MyPanel.dll", true) ;
			
			// Create a panel object.
			Assembly assembly = results.CompiledAssembly;
			object my_panel  = assembly.CreateInstance("KineticaRT.Panel.MyPanel");
			if (my_panel == null) 
			{
				MessageBox.Show("Couldn't load class.");
				return;
			}
		}

		/// <summary>
		/// Loads from XML
		/// </summary>
		/// <param name="element">XmlElement to load from</param>
		public override void LoadXML (XmlElement element)
		{
			//load self...
			base.LoadXML(element) ;

			// ...and load children (controls)
			XmlElement							controls_element ;

			controls_element = element.SelectSingleNode("Controls") as XmlElement ;
			if (controls_element != null)
			{
				XmlNodeList		ctrl_list ;

				// find all controls, create and load them
				ctrl_list = controls_element.SelectNodes("Control") ;
				if (ctrl_list != null)
				{
					foreach (XmlElement ctrl_element in ctrl_list)
					{
						System.Runtime.Remoting.ObjectHandle		obj_ctrl ;
						KineticaRT.Controls.Control						ctrl ;

						obj_ctrl = Activator.CreateInstanceFrom (ctrl_element.GetAttribute ("Assembly"), ctrl_element.GetAttribute ("TypeName")) ;
						ctrl = (KineticaRT.Controls.Control)obj_ctrl.Unwrap() ;
						if (ctrl != null) 
						{
							this.Controls.Add(ctrl) ;
							ctrl.LoadXML (ctrl_element) ;
						}
					}
				}
			}
		}

		/// <summary>
		/// Saves to XML
		/// </summary>
		/// <param name="element">XmlElement to save to</param>
		public override void SaveXML (XmlElement element)
		{
			//			int	x = Environment.TickCount ;

			//save self...
			base.SaveXML(element) ;

			XmlNode			ctrls	= element.SelectSingleNode("Controls") ;
			XmlDocument		doc		= element.OwnerDocument ;

			if (ctrls != null)
			{
				//clear any old control elements
				XmlNodeList list	= ctrls.SelectNodes("Control") ;

				foreach (XmlNode node in list)
				{
					ctrls.RemoveChild(node) ;
				}
			}
			else
			{
				ctrls = doc.CreateElement("Controls") ;
				element.AppendChild(ctrls) ;
			}

			//save the controls
			foreach (System.Windows.Forms.Control obj in this.Controls)
			{
				if (obj is KineticaRT.Controls.Control)
				{
					KineticaRT.Controls.Control	ctrl		= obj as KineticaRT.Controls.Control ;
					XmlElement					ctrl_node	= doc.CreateElement("Control") ;
					Type						ctrl_type	= ctrl.GetType() ;

					ctrls.AppendChild(ctrl_node) ;

					ctrl_node.SetAttribute("Assembly", ctrl_type.Module.Name) ;
					ctrl_node.SetAttribute("TypeName", ctrl_type.ToString()) ;
					ctrl.SaveXML(ctrl_node) ;
				}
			}

			//			Trace.WriteLine((Environment.TickCount - x).ToString()) ;
		}
		#endregion

		#region Serialization
		//		public void Serialize(string Filename)
		//		{
		//			System.IO.FileStream			stream ;
		//			System.Runtime.Serialization.Formatters.Soap.SoapFormatter formatter ;
		//			MyCodeDomSerializer				code_dom ;
		//			IDesignerSerializationManager	manager ;
		//			object							code_obj ;
		//				
		//			stream = new System.IO.FileStream(Filename, System.IO.FileMode.Create);
		//			formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
		//			code_dom = new MyCodeDomSerializer() ;
		//
		//			manager = code_dom as IDesignerSerializationManager ;
		//			code_obj = code_dom.Serialize(manager, this) ;
		//
		//			formatter.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.XsdString ;
		//			formatter.Serialize(stream, code_obj);
		//
		//			stream.Close () ;
		//		}
		#endregion
	}

	#region CodeDomSerializer (Unused)
//	[Serializable]
//	internal class MyCodeDomSerializer : CodeDomSerializer 
//	{
//		public override object Deserialize(IDesignerSerializationManager manager, object codeObject) 
//		{
//			// This is how we associate the component with the serializer.
//			CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.
//				GetSerializer(typeof(Frame).BaseType, typeof(CodeDomSerializer));
//
//			/* This is the simplest case, in which the class just calls the base class
//				to do the work. */
//			return baseClassSerializer.Deserialize(manager, codeObject);
//		}
// 
//		public override object Serialize(IDesignerSerializationManager manager, object value) 
//		{
//			/* Associate the component with the serializer in the same manner as with
//				Deserialize */
//			CodeDomSerializer baseClassSerializer = (CodeDomSerializer)manager.
//				GetSerializer(typeof(Frame).BaseType, typeof(CodeDomSerializer));
// 
//			object codeObject = baseClassSerializer.Serialize(manager, value);
// 
//			/* Anything could be in the codeObject.  This sample operates on a
//				CodeStatementCollection. */
//			if (codeObject is CodeStatementCollection) 
//			{
//				CodeStatementCollection statements = (CodeStatementCollection)codeObject;
// 
//				// The code statement collection is valid, so add a comment.
//				string commentText = "This comment was added to this object by a custom serializer.";
//				CodeCommentStatement comment = new CodeCommentStatement(commentText);
//				statements.Insert(0, comment);
//			}
//			return codeObject;
//		}
//	}
	#endregion
 }

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32 ;

using KineticaRT.Compact;
using KineticaRT.Compact.Diagnostics;

namespace KineticaRT.Compact.Controls
{
	#region CLASS: Addable
	/// <summary>
	/// Addable Class - information about objects that can be added to a Frame
	/// </summary>
	public class Addable : System.Object
	{
		private string			name ;
		private string			typename ;
		private Type			type ;

		/// <summary>
		/// Initialise an Addable
		/// </summary>
		/// <param name="Name">Friendly name of control</param>
		/// <param name="Typename">Typename of control</param>
		public Addable(string Name, string Typename)
		{
			name = Name ;
			typename = Typename ;
			type = Type.GetType(typename) ;
		}

		/// <summary>
		/// Gets the Friendly name of the control
		/// </summary>
		public string Name
		{
			get { return name; }
		}
		/// <summary>
		/// Gets the Typename of the control (including assembly definition)
		/// </summary>
		public string Typename
		{
			get { return typename; }
		}
		/// <summary>
		/// Gets the Type of the control
		/// </summary>
		public Type Type
		{
			get { return type; }
		}
	}
	#endregion

	#region CLASS: Grid
	/// <summary>
	/// Information about a Grid for positioning controls on a Frame
	/// </summary>
	public class Grid : System.Object
	{
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
		/// Creates a new instance of the grid class with the given Size and Spacing
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
		/// Gets or sets the Size of the grid in pixels
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
		/// Gets or sets the Space between objects on the grid in pixels
		/// </summary>
		/// <remarks>This means that the actual width or height of controls is [(n * Size) - Spacing] pixels</remarks>
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
		/// Gets or sets whether the grid is enabled
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
		/// Gets the number of pixels in one grid unit
		/// </summary>
		/// <remarks>This will be the Size if the grid is enabled, else 1</remarks>
		public int Pixels
		{
			get
			{
				if (enabled)		return size ;
				return 1 ;
			}
		}

		/// <summary>
		/// Gets the Minimum number of pixels that a control can be
		/// </summary>
		/// <remarks>This will be (Size - Spacing) or 0 if (Spacing > Size)</remarks>
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
	#endregion

	/// <summary>
	/// The KineticaRT Frame is a Control Container, which allows run-time editing
	/// of control layout and properties.
	/// </summary>
	[ToolboxBitmap(typeof(FrameBase))]
	public abstract class FrameBase : KineticaRT.Compact.Controls.Control, ISupportInitialize
	{
        ///// <summary>
        ///// Gets a value indicating if the application has been packed into a single executable file or not.
        ///// </summary>
        //static public bool Packed
        //{
        //    get
        //    {
        //        if (!packChecked)
        //        {
        //            packChecked = true;

        //            try
        //            {
        //                isPacked = null != Type.GetType("netz.NetzStarter, " + System.IO.Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName));
        //            }
        //            catch (System.IO.FileLoadException)
        //            {
        //                //System.Windows.Forms.MessageBox.Show (e.Message, AppDomain.CurrentDomain.FriendlyName + ": Invalid");
        //                //System.Windows.Forms.Application.Exit();
        //            }
        //        }

        //        return isPacked;
        //    }
        //}

        //static bool packChecked;
        //static bool isPacked;

#region License
#if KRT_license
		private License license = null;
		private		bool				demoMode ;
		// Do not move this class or its contents into another namespace - it would not be secure.
	
		////[LicenseProviderAttribute(typeof(xxx.LicenseProvider))]
		//private License license = null;
		////demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
		//if (license != null) {license.Dispose();license = null;}
		//This block: update key using GUIDGEN, and typeof(xxx)
		/// <summary>
		/// Implements a license provider for the class.
		/// </summary>
		public class LicenseProvider: System.ComponentModel.LicenseProvider
		{
			/// <summary>
			/// Gets a license for an instance or type of component, when given a context and whether the denial of a license throws an exception
			/// </summary>
			/// <param name="context">A LicenseContext that specifies where you can use the licensed object</param>
			/// <param name="type">A Type that represents the component requesting the license</param>
			/// <param name="instance">An object that is requesting the license</param>
			/// <param name="allowExceptions">true if a LicenseException should be thrown when the component cannot be granted a license; otherwise, false</param>
			/// <returns>A valid License</returns>
			public override System.ComponentModel.License GetLicense(LicenseContext context,Type type,object instance,bool allowExceptions)
			{
				License licence = null ;

				if (context != null)
				{
					string key = null ;

					if (context.UsageMode == LicenseUsageMode.Designtime)
					{
						Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\KineticaRT.Controls.Frame") ; 
						if (registry != null) key = registry.GetValue ("RegKey") as string ;
						if ((key == null) || (key != "4CD0571EE68D"))
						{
							registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\KineticaRT.Controls.Tab") ; 
							if (registry != null) key = registry.GetValue ("RegKey") as string ;
						}
						if ((key != null) && ((key == "4EF4AEDFD67B") || (key == "EA249366949B") || (key == "4CD0571EE68D"))) key = "{97492B27-F8A1-4eb8-9540-EDB7801A5E93}" ; 
						context.SetSavedLicenseKey (typeof(FrameBase), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(FrameBase), null) ;
					if (key == "{97492B27-F8A1-4eb8-9540-EDB7801A5E93}") licence = new License ("Run") ;
					else if (allowExceptions && (context.UsageMode != LicenseUsageMode.Designtime)) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
					else licence = new License ("None") ;
				}

				return licence ;
			}

			private class License:System.ComponentModel.License 
			{
				string key ;

				public License (string key)
				{
					this.key = key ;
				}

				public override string LicenseKey 
				{
					get
					{
						return key ;
					}
				}
		
				public override void Dispose()
				{
				}
			}
		}
#endif
		#endregion

		#region Variables
		/// <summary>
		/// Handler for GetAddList event
		/// </summary>
		public delegate void	GetAddListEventHandler(out ArrayList AddList, out ArrayList ComponentAddList, out string[] References);

		/// <summary>
		/// Event generated to retrieve AddList from parent Frame
		/// </summary>
		public	event GetAddListEventHandler	GetAddList ;

		//property variables
		private	bool				titleEnabled ;
		private Grid				horizontalGrid ;
		private Grid				verticalGrid ;
		private string				addListFile ;
		private string				systemDirectory ;

		//other variables
		internal	ArrayList			addList ;			//accessible to EditForm
		internal	ArrayList			componentAddList ;	//accessible to EditForm
		internal	string[]			referenceList ;		//used by Panel save & copy/paste
		//private		EditForm			editForm ;
//		private		bool				editMode ;
		private		bool				connected ;
//		private		ContextMenu			menu ;
		/// <summary>Vertical space required by Title</summary>
		protected	int					titleHeight ;
		/// <summary>Rectangle in which to draw the Title</summary>
		protected	Rectangle			titleRect ;
		/// <summary>True if the title / header bar needs recalculating</summary>
		protected	bool				needCalcTitle ;
		/// <summary>True if frame is being initialised (ISupportInitialize interface)</summary>
		protected	bool				initialising ;

		internal	PanelBase			currentPanel ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ObjectSearchEventHandler		searchHdlr ;
		private GetAddListEventHandler			addListHdlr ;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the FrameBase class
		/// </summary>
		public FrameBase()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license == null) || (license.LicenseKey != "Run");
#endif

            searchHdlr = new ObjectSearchEventHandler(OnChildObjectSearch);
            addListHdlr = new GetAddListEventHandler(OnGetAddList);

            //			bool editingEnabled = true ;
            //			try
            //			{
            //				RegistryKey key =  Registry.CurrentUser.OpenSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName) ;
            //				if (key != null)
            //				{
            //					editingEnabled = (((string)key.GetValue ("EnableEditMenu", true)).ToLower()=="true") ;
            //				}
            //			}
            //			catch {}
            //
            //			if (editingEnabled)
            //			{
            //set up menu
            //MenuItem[] items;
            //items = new MenuItem[1];
            //items[0] = new MenuItem("Enter &Edit Mode", new EventHandler(OnClickEditMode), Shortcut.CtrlE);
            //menu = new ContextMenu(items);
            //ContextMenu = menu;
            //menu.Popup += new EventHandler(OnMenuPopup);
            //			}

            titleEnabled = true;
            ForeColor = Color.SteelBlue;
            addListFile = "";
//            systemDirectory = Application.StartupPath;

            horizontalGrid = new Grid();
            verticalGrid = new Grid();

            titleRect = Rectangle.Empty;
            titleHeight = 15;
            needCalcTitle = true;

            addList = new ArrayList();
            componentAddList = new ArrayList();
            referenceList = null;

//            editMode = false;
            connected = false;
            //this.shape = KineticaRT.Controls.Shapes.RoundedRectangle ;
            this.cornerRadius = 5;
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
#if KRT_license
			if (license != null) {license.Dispose();license = null;}
#endif

			if( disposing )
			{
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
			// FrameBase
			// 
			this.Name = "FrameBase";
			this.Size = new System.Drawing.Size(272, 168);

		}
		#endregion

		#region ISupportInitialize
		/// <summary>
		/// Initialization is beginning
		/// </summary>
		public void BeginInit()
		{
			initialising = true ;
			this.SuspendLayout();
		}
	
		/// <summary>
		/// Initialization is complete
		/// </summary>
		public virtual void EndInit()
		{
			initialising = false ;

			ReadAddListFile() ;

//			if (ContextMenu == null)		ContextMenu = menu ;
			this.ResumeLayout(false);
			//CalculateDisplayRectangle() ;
		}
		#endregion

		#region Properties
#if KRT_license
		/// <summary>
		/// Gets whether the control is running in Demo Mode, which is the case if the control is unlicenced
		/// </summary>
		protected bool DemoMode
		{
			get { return demoMode; }
		}
#endif

        ///// <summary>
        ///// Gets or Sets the style of the Control's Border
        ///// </summary>
        //[Category("Control")]
        //[Description("Gets or Sets the style of the Control's Border")]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public new KineticaRT.Compact.Controls.BorderStyles BorderStyle
        //{
        //    get
        //    {
        //        return (KineticaRT.Controls.BorderStyles)this.borderType;
        //    }
        //    set
        //    {
        //        this.borderType = (KineticaRT.Controls.BorderType)value;
        //    }
        //}

        /// <summary>
		/// Gets or sets the shortcut menu associated with the control.
		/// </summary>
		[Category("Behavior")]
//		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override ContextMenu ContextMenu
		{
			get { return base.ContextMenu; }
			set { base.ContextMenu = value; }
		}

        ///// <summary>
        ///// Gets or sets the shape of the control.
        ///// </summary>
        ///// <remarks>Frame-based controls do not support shapes other than the standard rectangle.</remarks>
        //[Category("Control")]
        //[Description("FrameBase Shape: must be a rectangle")]
        //[DefaultValue(Shapes.Rectangle)]
        //[Browsable(true)]
        //public override Shapes Shape
        //{
        //    get
        //    {
        //        return shape ;
        //    }
			
        //    set
        //    {
        //        if (value == Shapes.Ellipse)
        //        {
        //            value = Shapes.RoundedRectangle ; 
        //        }
			
        //        if (shape != value)
        //        {
        //            lock(this)
        //            {
        //                shape = value ;

        //                //set the shape of the window region (must reset on 'resize' event)
        //                SetWindowRegion() ;

        //                //force a full redraw
        //                this.Invalidate() ;
        //                //						AddToQueue(true) ;
        //                OnPropertyChanged("Shape") ;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the corner radius of shaped controls.
        ///// </summary>
        ///// <remarks>Frame-based controls do not support shapes other than the standard rectangle, so this property is ignored.</remarks>
        //[Category("Control")]
        //[Description("FrameBase CornerRadius: Not used - must be 20")]
        //[DefaultValue(20)]
        //[Browsable(false)]
        //public override int CornerRadius
        //{
        //    get { return cornerRadius; }
        //    set { cornerRadius = 5; }
        //}

		/// <summary>
		/// Gets or sets the name of a file containing information about addable controls
		/// </summary>
		/// <remarks>
		/// If a full path is not specified, the file is relative to the path 
		/// specified in the SystemDirectory property
		/// </remarks>
		[Category("Frame")]
		[DefaultValue("")]
		[Description("Gets or sets the name of a file containing information about addable controls")]
		//[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
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
					if (!initialising)			ReadAddListFile() ;
					OnPropertyChanged("AddListFile") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Path to the System Directory, where Panel dlls and AddList files are sought.
		/// </summary>
		/// <remarks>Defaults to Application Startup path</remarks>
		[Category("Frame")]
		[Description("Gets or sets the Path to the System Directory, where Panel dlls and AddList files are sought.")]
		//[Editor(typeof(System.Windows.Forms.Design.FolderNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[Browsable(false)]
		public string SystemDirectory
		{
			get
			{
				return systemDirectory ;
			}
			set
			{
				if (systemDirectory != value)
				{
					systemDirectory = value ;
					//do we need to re-read the add list? - iff not full path
					if (!initialising && !Path.IsPathRooted(addListFile))
					{
						ReadAddListFile() ;
					}
					//Panels are not automatically re-read

					OnPropertyChanged("SystemDirectory") ;
				}
			}
		}
		private bool ShouldSerializeSystemDirectory()
		{
//			return (systemDirectory != Application.StartupPath) ;
            return true;
		}

		/// <summary>
		/// Gets or sets whether the Title bar is enabled
		/// </summary>
		[Category("Frame")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Title bar is enabled")]
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
					needCalcTitle = true ;
					AddToQueue(true) ;
					OnPropertyChanged("TitleEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Horizontal Grid settings
		/// </summary>
		[Category("Frame")]
		[Description("Gets or sets the Horizontal Grid settings")]
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
					horizontalGrid = value ;
					OnPropertyChanged("HorizontalGrid") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Vertical Grid settings
		/// </summary>
		[Category("Frame")]
		[Description("Gets or sets the Vertical Grid settings")]
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
					verticalGrid = value ;
					OnPropertyChanged("VerticalGrid") ;
				}
			}
		}

		#endregion (properties)

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// Called from OnPaint and the Control's event pool Update
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public /*override*/ void Repaint(bool FullRedraw, Graphics g)
		{
//            g.SmoothingMode = SmoothingMode.None;

//            if (FullRedraw)
//            {
//                CalculateDisplayRectangle() ;
//                DrawBorder(g) ;
//                needFullRedraw = false ;
//            }

//#if KRT_license
//            if (demoMode && !titleEnabled)
//            {
//                Rectangle		rect		= DisplayRectangle ;
//                rect.Y += Font.Height + 4 ;
//                rect.Height -= Font.Height + 4 ;
//                displayRect	= rect ;
//            }
//#endif
//            //offset title (if enabled)
//            if (titleEnabled)
//            {
//                Rectangle		rect		= DisplayRectangle ;

//                if (needCalcTitle)
//                {
//                    CalculateTitleHeight() ;

//                    needCalcTitle = false ;
//                }
//                titleRect = rect ;
//                titleRect.Height = titleHeight ;

//                rect.Y		= titleRect.Bottom ;
//                rect.Height -= titleRect.Height ;
//                displayRect	= rect ;
//            }

//            //##only need to call this if display rectangle has changed?
//            if (FullRedraw)
//            {
//                PerformLayout() ;
//            }

//            if (shape == Shapes.RoundedRectangle)
//            {
//                g.Clip = new Region(ClientRectangle) ;
//                Rectangle title_rect ;
//                title_rect = ClientRectangle ;
//                int border_width ;
//                if (borderType == BorderType.Flat || borderType == BorderType.Shaded || borderType == BorderType.Rounded || borderType == BorderType.RoundedWithInset || borderType == BorderType.ShadedWithInset)
//                {
//                    border_width = BorderWidthPixels(ClientRectangle);
//                }
//                else
//                {
//                    border_width = 0 ;
//                }
//                if (borderType == BorderType.Raised || borderType == BorderType.Indented || borderType == BorderType.EditBox || borderType == BorderType.RoundedWithInset || borderType == BorderType.ShadedWithInset)
//                {
//                    border_width += 2 ;
//                }
//                title_rect.Inflate (-border_width, -border_width) ;

//                GraphicsPath innerpath = CalculateShapePath(title_rect, shape) ;
//                //g.FillPath(System.Drawing.Brushes.DarkGoldenrod, innerpath) ;
//                //g.Clip = new Region(innerpath) ;
//                Region new_region = new Region(innerpath) ;
//                new_region.Intersect (g.Clip) ;
//                g.Clip = new_region ;
//                innerpath.Dispose() ;
//            }

//            DrawTitle(g) ;

//#if KRT_license
//            if (demoMode)
//            {
//                Rectangle			demo_rect ;
				
//                if (titleEnabled)
//                {
//                    demo_rect = titleRect ;
//                }
//                else
//                {
//                    demo_rect = displayRect ;
//                    demo_rect.Height = Font.Height + 4 ;
//                    demo_rect.Y = demo_rect.X ;
//                }
//                DrawDemoTextColor(g, demo_rect, Color.DarkOrchid) ;
//            }
//#endif
		}

		/// <summary>
		/// Calculates the vertical space needed for the title
		/// </summary>
		protected virtual void CalculateTitleHeight()
		{
            titleHeight = 16;//this.Font.Height + 6 ;
		}

		/// <summary>
		/// Draws the title bar
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		protected virtual void DrawTitle(Graphics g)
		{
			//done by override classes
		}
		#endregion (drawing)

		#region Public Methods
		/// <summary>
		/// Returns an object for which to show the properties
		/// </summary>
		/// <returns>Object to show the properties of</returns>
		public virtual object GetBasicPropertiesObject()
		{
			return currentPanel ;
		}

        ///// <summary>
        ///// Saves the current Panel to a dll.
        ///// Must be implemented by inherited classes.
        ///// </summary>
        //public abstract void SavePanel() ;

        ///// <summary>
        ///// Returns whether the currentPanel can be saved.
        ///// Must be implemented by inherited classes.
        ///// </summary>
        //public abstract bool CanSave() ;

		/// <summary>
		/// Adds a control from the add list
		/// </summary>
		/// <param name="Name">Name in add list</param>
		/// <returns>New Control (or null if failed)</returns>
		/// <remarks>Only controls identified in the add list can be added to a Frame</remarks>
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
		/// <returns>New Control (or null if failed)</returns>
		/// <remarks>Only controls identified in the add list can be added to a Frame</remarks>
		public System.Windows.Forms.Control Add(int Index)
		{
			System.Windows.Forms.Control			new_ctrl	= null ;

			if (currentPanel != null)
			{
				//				System.Runtime.Remoting.ObjectHandle	obj_ctrl ;
				//				string									assembly ;
				Addable				addable		= addList[Index] as Addable ;
				Type				ctrl_type ;
				//NS			LicenseContext		lic_context	= new PanelBase.PanelLicenseContext() ;
				
				//				//prepend the application path to the assembly dll
				//				assembly = Path.Combine(Application.StartupPath, addable.Assembly) ;
				//				obj_ctrl = Activator.CreateInstanceFrom (assembly, addable.Typename) ;
				//				new_ctrl = obj_ctrl.Unwrap() as System.Windows.Forms.Control ;

				ctrl_type = addable.Type ;
				if (ctrl_type == null)
				{
                    KineticaRT.Compact.Diagnostics.Trace.WriteLine("Invalid addable type");
					return null ;
				}

				//NS			new_ctrl = LicenseManager.CreateWithContext(ctrl_type, lic_context) as System.Windows.Forms.Control ;
				new_ctrl = System.Activator.CreateInstance (ctrl_type) as System.Windows.Forms.Control ;
				
				if (new_ctrl != null) 
				{
					//name the object (uniquely) according to its type
					new_ctrl.Name = currentPanel.GetUniqueName(new_ctrl.Name + "1") ;
					
					//position it inside the display rectangle
                    //new_ctrl.Left = DisplayRectangle.Left;
                    //new_ctrl.Top = DisplayRectangle.Top;
                    new_ctrl.Left = ClientRectangle.Left;
                    new_ctrl.Top = ClientRectangle.Top;

					currentPanel.Controls.Add(new_ctrl) ;
					//bring to front of z-order
					new_ctrl.BringToFront() ;
				}
			}

			return new_ctrl ;
		}

		/// <summary>
		/// Adds a component from the component add list
		/// </summary>
		/// <param name="Name">Name in add list</param>
		/// <returns>New Component (or null if failed)</returns>
		/// <remarks>Only Components identified in the component add list can be added to a Frame</remarks>
		public Component AddComponent(string Name)
		{
			//find the index for this name
			for (int index = 0; index < componentAddList.Count; index++)
			{
				if (((Addable)componentAddList[index]).Name == Name)
				{
					//add it
					return AddComponent(index) ;
				}
			}

			return null ;
		}

		/// <summary>
		/// Adds a component from the component add list by index
		/// </summary>
		/// <param name="Index">Zero-based index in add list</param>
		/// <returns>New Component (or null if failed)</returns>
		/// <remarks>Only Components identified in the component add list can be added to a Frame</remarks>
		public Component AddComponent(int Index)
		{
			if (currentPanel != null)
			{
				Addable				addable			= componentAddList[Index] as Addable ;

				return currentPanel.AddComponent(addable.Type) ;
			}

			return null ;
		}

		/// <summary>
		/// Defines an add list from a given XmlElement, in the same format as an
		/// AddList File.
		/// </summary>
		/// <remarks>
		/// If an AddListFile has been specified, this call does nothing.
		/// i.e. an AddList specified by file takes precedence over any other one.
		/// </remarks>
		/// <param name="Root">The root element of the Add List</param>
		/// <returns>True if accepted new add list, False if ignored it becuse the one specified by File takes precedence</returns>
		public virtual bool SetAddList(XmlElement Root)
		{
			if (addListFile == "")
			{
				ReadAddList(Root) ;
				ReadComponentList(Root) ;
				ReadReferenceList(Root) ;

				return true ;
			}
			return false ;
		}
		#endregion (public methods)

		#region Other Methods
		private void ReadAddListFile()
		{
			if (addListFile != "")
			{
				XmlDocument		document	= new XmlDocument () ;
				XmlElement		root ;
				string			file ;

				//if addListFile is not a full path, prepend the SystemDirectory
				file = Path.Combine(systemDirectory, addListFile) ;
				//ExpandEnvironmentStrings(add_list_file, file, MAX_PATH) ;

				if (!File.Exists(file))
				{
                    KineticaRT.Compact.Diagnostics.Trace.WriteLine(file + " does not exist");
					return ;
				}

				try
				{
					document.Load (file) ;

					//go through children to read various parts
					root = document.DocumentElement ;
					if (root != null)
					{
						//						//only load it if root is 'Addables'
						//						if (root.Name == "Addables")
					{
						//read new lists
						ReadAddList(root) ;
						ReadComponentList(root) ;
						ReadReferenceList(root) ;
					}
					}
				}
				catch(XmlException e)
				{
                    KineticaRT.Compact.Diagnostics.Trace.WriteLine("Parse AddListFile Failed: "+e.Message);
					return ;
				}
			}
		}

		private void ReadAddList(XmlElement Root)
		{
			XmlNodeList		addables ;
			string			name ;
			string			type ;

			//got a valid XML file of expected type - clear old list
			addList.Clear() ;

			//find all Add nodes
			addables = Root.SelectNodes("Add") ;

			//go through them
			foreach (XmlElement element in addables)
			{
				//get details...
				name = element.GetAttribute("Name") ;
				type = element.GetAttribute("Type") ;

				if (name != "" && type != "")
				{
					//... and add to list
                    try
                    {
                        addList.Add(new Addable(name, type));
                    }
                    catch (System.Exception e)
                    {
                        MessageBox.Show(e.Message, "Invalid Add item: "+name+" - "+type);
                    }
				}
			}

			//let things know (ie EditForm) that the add list has changed
			//so can update menu. atm assuming this will never happen after
			//edit mode is first entered
		}
	
		//addable components
		private void ReadComponentList(XmlElement Root)
		{
			XmlNodeList		addables ;
			string			name ;
			string			type ;

			//got a valid XML file of expected type - clear old list
			componentAddList.Clear() ;

			//find all Component nodes
			addables = Root.SelectNodes("Component") ;

			//go through them
			foreach (XmlElement element in addables)
			{
				//get details...
				name = element.GetAttribute("Name") ;
				type = element.GetAttribute("Type") ;

				if (name != "" && type != "")
				{
					//... and add to list
					componentAddList.Add(new Addable(name, type)) ;
				}
			}

			//? let things know (ie EditForm) that the add list has changed
			//so can update menu. atm assuming this will never happen after
			//edit mode is first entered
		}
		
		private void ReadReferenceList(XmlElement Root)
		{
			XmlNodeList		references ;
			int				index			= 0 ;

			//find all Reference nodes with an Assembly attribute
			references = Root.SelectNodes("Reference[@Assembly]") ;
			//(re)set list
			referenceList = new String[references.Count] ;

			//find all assemblies
			foreach (XmlElement element in references)
			{
				referenceList[index++] = element.GetAttribute("Assembly") ;
			}
		}

        //string temp_file;
        bool compact;  // Use compact framework.

		/// <summary>
		/// Loads a panel from the given DLL. Object type is 'KineticaRT.Panel.xxx'
		/// where xxx is Name without the path or extension
		/// </summary>
		/// <param name="newName">Dll name and path</param>
        /// <param name="compact">Uses the compact framework if true.</param>
        /// <returns>New Panel or null if failed</returns>
		protected PanelBase LoadPanelFromName(string newName, bool compact)
		{
			if (newName != "")
			{
                //MessageBox.Show(panels[currentPanelIndex].DllName, "Loading panel: ");

                this.compact = compact;
                Assembly assembly = null;

//                if (!Packed)
                {
                    //copy dll to temporary file
//                    temp_file = Path.GetTempFileName();
                    string file;

                    //if newName is not a full path, prepend the SystemDirectory
                    file = Path.Combine(systemDirectory, newName);

                    //does the dll exist?
                    if (!File.Exists(file))
                    {
                        //ensure got a reference list if at all possible
                        FireGetAddList();

                        //no - attempt to create it
//                        if (!PanelBase.CreateDllFromSource(Path.ChangeExtension(file, ".cs"), file, referenceList, compact))
                        {
                            //failed to create file - give up now
                            return null;
                        }
                    }

                    //try
                    //{
                    //    File.Copy(file, temp_file, true);
                    //}
                    //catch (System.Exception e)
                    //{
                    //    MessageBox.Show(e.Message, "Error copying panel: " + file);
                    //    return null;
                    //}

                    try
                    {
                        //assembly = Assembly.LoadFrom(temp_file);
                        assembly = Assembly.LoadFrom(file);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Panel Load edit file exception");
                    }
                }
                //else
                //{
                //    try
                //    {
                //        //assembly = Assembly.LoadFrom(file);
                //        assembly = Assembly.Load(newName);
                //    }
                //    catch (Exception e)
                //    {
                //        MessageBox.Show(e.Message, "Panel Load file exception");
                //    }
                //}

				try
				{
					if (assembly != null)
					{
						string		name		= Path.GetFileNameWithoutExtension(newName) ;
						object		my_panel ;

						try
						{
							my_panel = assembly.CreateInstance("KineticaRT.Panel." + name);

							if (my_panel is PanelBase)
							{
								return my_panel as PanelBase ;
							}
							else
							{
								MessageBox.Show("Assembly does not contain a valid Panel object") ;
							}
						}
						catch (Exception e)
						{
							if (e.InnerException != null)
							{
								MessageBox.Show(name + ": " + e.InnerException.Message, "Create Panel exception") ;
							}
							else
							{
								MessageBox.Show(e.Message, "Create Panel exception") ;
							}
						}
					}
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "PanelName exception") ;
				}
			}

			return null ;
		}

		//'ReplaceOld' means throw old one away (disconnect and dispose)
		//otherwise simply point to the new one
		/// <summary>
		/// Makes the given panel into the current Panel
		///'ReplaceOld' means throw old one away (disconnect and dispose)
		///otherwise simply point to the new one
		/// </summary>
		/// <param name="newPanel">New Panel to use</param>
		/// <param name="ReplaceOld">Whether to discard the old one or just use the new one</param>
		protected void NewPanel(PanelBase newPanel, bool ReplaceOld)
		{
			if (newPanel != null && !newPanel.Equals(currentPanel))
			{
				ReplacePanel(ReplaceOld ? currentPanel : null, newPanel) ;

				currentPanel = newPanel ;
				OnPropertyChanged("Panel") ;
			}
		}

		/// <summary>
		/// Replaces the oldPanel in the Controls collection with the newPanel
		/// </summary>
		/// <param name="oldPanel">Panel to be removed, or null</param>
		/// <param name="newPanel">Panel to be added</param>
		protected void ReplacePanel(PanelBase oldPanel, PanelBase newPanel)
		{
			if (newPanel != null && !newPanel.Equals(oldPanel))
			{
				//disconnect old panel if any
				if (oldPanel != null)
				{
					oldPanel.ObjectSearch -= searchHdlr ;
					oldPanel.GetAddList -= addListHdlr ;

					if (Controls.Contains(oldPanel))
					{
						Controls.Remove(oldPanel) ;
					}

					if (connected)	oldPanel.Disconnect() ;
					oldPanel.Dispose() ;
				}

				newPanel.Dock = DockStyle.Fill ;
				Controls.Add(newPanel) ;

				//if (newPanel.ObjectSearch == null && searchHdlr != null) 
				newPanel.ObjectSearch		-= searchHdlr ;
				newPanel.ObjectSearch		+= searchHdlr ;
				//if (newPanel.GetAddList == null && addListHdlr != null) 
				newPanel.GetAddList			-= addListHdlr ;
				newPanel.GetAddList			+= addListHdlr ;

				//NSCC if (connected)			newPanel.Connect() ;
			}
		}

		private void FireGetAddList()
		{
			//gets a new add list from parent and reference list if not got own list
			if (addList.Count == 0 && addListFile == "" && GetAddList != null)
			{
				ArrayList		newAdds ;
				ArrayList		newComponents ;
				string[]		newReferences ;
				
				GetAddList(out newAdds, out newComponents, out newReferences);

				addList.Clear() ;
				addList = newAdds ;

				componentAddList.Clear() ;
				componentAddList = newComponents ;

				referenceList = newReferences ;
			}
		}

		private void OnGetAddList(out ArrayList AddList, out ArrayList ComponentAddList, out string[] References)
		{
			//ensure got an add list and reference list of at all possible
			FireGetAddList() ;

			//return copies, not the original lists
			AddList			= (addList == null) ? new ArrayList() : (ArrayList)addList.Clone() ;
			ComponentAddList= (componentAddList == null) ? new ArrayList() : (ArrayList)componentAddList.Clone() ;
			References		= (referenceList == null) ? null : (string[])referenceList.Clone() ;
		}

		//		private System.ComponentModel.LicenseProvider GetLicenseProvider(Type objectType)
		//		{
		//			AttributeCollection		attrs	= TypeDescriptor.GetAttributes(objectType) ;
		//
		//			foreach (Attribute attr in attrs)
		//			{
		//				if (attr is LicenseProviderAttribute)
		//				{
		//					Type									lp_type ;
		//					System.ComponentModel.LicenseProvider	lp ;
		//					
		//					lp_type = ((LicenseProviderAttribute)attr).LicenseProvider ;
		//					lp = lp_type.Assembly.CreateInstance(lp_type.ToString()) as System.ComponentModel.LicenseProvider ;
		//
		//					return lp ;
		//				}
		//			}
		//			
		//			return null ;
		//		}
		#endregion (other methods)

		#region Events
        ///// <summary>
        ///// Font changed event
        ///// </summary>
        ///// <param name="e">Event data</param>
        //protected override void OnFontChanged(EventArgs e)
        //{
        //    needCalcTitle = true ;
        //    if (titleEnabled)			AddToQueue(true) ;

        //    base.OnFontChanged(e) ;
        //}

        ///// <summary>
        ///// Enter event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected override void OnEnter(EventArgs e)
        //{
        //    if (editMode && !editForm.Visible)
        //    {
        //        editForm.Visible = true ;
        //    }
        //    base.OnEnter(e) ;
        //}

        ///// <summary>
        ///// Leave Event
        ///// </summary>
        ///// <param name="e">Event data</param>
        //protected override void OnLeave(EventArgs e)
        //{
        //    if (editMode && editForm.Visible)
        //    {
        //        editForm.Visible = false ;
        //    }
        //    base.OnLeave(e) ;
        //}
		#endregion (events)

        //#region Edit mode Handling
        //private void OnClickEditMode(object sender, EventArgs e)
        //{
        //    //get new add list (unless already set)
        //    FireGetAddList() ;

        //    if (!editMode)
        //    {
        //        editMode = true ;
        //        editForm = new EditForm(this) ;
        //        editForm.Closed			+= new EventHandler(OnEditClosed) ;
        //        editForm.Size = this.Size ;
        //        editForm.Location = this.PointToScreen(new Point(0,0)) ;
        //    }
        //    editForm.Show() ;
        //}

        //private void OnEditClosed (object sender, EventArgs e)
        //{
        //    editMode = false ;
        //    this.Focus () ;
        //    //			editForm.Dispose() ;
			
        //    //			TopLevelControl.Select() ;
        //}

        //private void OnMenuPopup(object sender, EventArgs e)
        //{
        //    if (sender.Equals(menu))
        //    {
        //        bool editingEnabled = true ;
        //        try
        //        {
        //            RegistryKey key =  Registry.CurrentUser.OpenSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\User") ;
        //            if (key != null)
        //            {
        //                editingEnabled = (((string)key.GetValue ("EnableEditMenu", true)).ToLower()=="true") ;
        //            }
        //        }
        //        catch {}
	
        //        menu.MenuItems[0].Enabled = editingEnabled && !editMode ;
        //        menu.MenuItems[0].Visible = editingEnabled && !editMode ;
        //    }
        //}
        //#endregion (edit mode)

		#region Object Search
		/// <summary>
		/// Returns the named object or null if it does not exist.
		/// </summary>
		/// <param name="objectName">Name of object to find</param>
		/// <returns>Named object, or null if not found</returns>
		public override object FindObject (string objectName)
		{
			if (currentPanel != null)
			{
				return currentPanel.FindControl(objectName) ;
			}
			return null ;
		}

		/// <summary>
		/// Event handler for requests from the objects children to find another object for them by name.
		/// </summary>
		protected virtual object OnChildObjectSearch (string objectName)
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
        public override void Connect()
        {
            connected = true;
            if (currentPanel != null) currentPanel.Connect();
        }

        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public override void Connect2()
        {
            if (currentPanel != null) currentPanel.Connect2();
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			connected = false ;
			if (currentPanel != null)		currentPanel.Disconnect() ;
		}
		#endregion (connection)

        //#region Menu event stuff
        //public event KineticaRT.Studio.ContextMenuCreationEventHandler ContextMenuCreation;
        //private void OnContextMenuCreation(KineticaRT.Studio.ContextMenuCreationEventArgs e) { if (ContextMenuCreation != null) ContextMenuCreation(this, e); }
        //#endregion
    }
}

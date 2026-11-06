using System;
using System.Collections;
using System.ComponentModel;
//using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;

using KineticaRT.Compact ;

namespace KineticaRT.Compact.Controls
{
	/// <summary>
	/// Panel control. This is the part of the Frame that actually holds the contained controls.
	/// </summary>
	//[TypeConverterAttribute (typeof(ExpandableObjectTypeConverter))]
	public class PanelBase : System.Windows.Forms.UserControl, KineticaRT.Compact.IConnectable, KineticaRT.Compact.IConnectable2  
	{
		#region CLASS: PanelLicenseContext
//NS		/// <summary>
//		/// Designtime license context
//		/// </summary>
//		public class PanelLicenseContext : LicenseContext
//		{
//			/// <summary>
//			/// Gets the UsageMode of the License context
//			/// </summary>
//			public override LicenseUsageMode UsageMode
//			{
//				get
//				{
//					return LicenseUsageMode.Designtime ;
//				}
//			}
//		}
		#endregion

		#region Variables
		private	bool							connected ;
		private KineticaRT.Compact.ObjectSearchEventHandler searchHdlr ;
		private Frame.GetAddListEventHandler	addListHdlr ;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		protected System.ComponentModel.Container components = new Container() ;

		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the PanelBase class with the specified Container
		/// </summary>
		/// <param name="container">IContainer to which the PanelBase is added</param>
		public PanelBase(IContainer container)
		{
			if (container != null)		container.Add(this) ;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			connected			= false ;

			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch);
			addListHdlr = new Frame.GetAddListEventHandler(OnGetAddList) ;

			//this is for loading the panel's controls
			InitializePanel () ;

			Connect() ;
		}

//		/// <summary>
//		/// Mouse Down Event
//		/// </summary>
//		/// <param name="e">Event Data</param>
//		protected override void OnMouseDown(MouseEventArgs e)
//		{
//			//this.Select () ;
//			base.OnMouseDown (e) ;
//		}

		/// <summary>
		/// Initialises a new instance of the PanelBase class
		/// </summary>
		public PanelBase()
		{
            // This line only to remove compiler warning, PropertyChanged will always be null.
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""));

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			connected			= false ;

			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch);
			addListHdlr = new Frame.GetAddListEventHandler(OnGetAddList) ;

			//this is for loading the panel's controls
			InitializePanel () ;

			//this.AutoScroll = true ;
			//this.SetAutoScrollMargin(5, 5);
			// this.AutoScrollMinSize
			// this.AutoScrollPosition
		

			//NSCC Connect() ;
		}

		/// <summary>
		/// Initialises a new instance of the PanelBase class
		/// </summary>
		public PanelBase(string Name)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.Name = Name ;

			connected			= false ;

			searchHdlr = new ObjectSearchEventHandler(OnObjectSearch);
			addListHdlr = new Frame.GetAddListEventHandler(OnGetAddList) ;

			//this is for loading the panel's controls
			InitializePanel () ;
		}

		/// <summary>
		/// Initialize the panel.
		/// </summary>
		/// <remarks>This should be overriden in the derived classes to initialise the properties and create the child controls.</remarks>
		protected virtual void InitializePanel () 
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )				components.Dispose();
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
			// PanelBase
			// 
			this.Name = "PanelBase";
            //this.SetStyle(ControlStyles.UserPaint, true) ;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;
            //this.SetStyle(ControlStyles.Selectable, true) ;
		}
		#endregion

		//#region Properties
        ///// <summary>
        ///// Gets a collections of Components contained by the Panel
        ///// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public ComponentCollection Components
        //{
        //    get
        //    {
        //        return components.Components ;
        //    }
        //}
        //private bool ShouldSerializeComponents()
        //{
        //    return (components.Components != null) ;
        //}
        //#endregion

		#region Public Methods
		/// <summary>
		/// Returns a name that does not currently exist in the Controls collection.
		/// Returns BaseName if that does not exist, else a numeric increment of BaseName
		/// </summary>
		/// <param name="BaseName">Name to test and increment if required</param>
		/// <returns>Name that does not currently exist in the Controls Collection</returns>
		public string GetUniqueName(string BaseName)
		{
			//if the name is already unique, return that
			if (FindControl(BaseName) == null)		return BaseName ;

			//otherwise, find a unique one
			string		new_name ;
			string		name_base ;
			int			index			= 1 ;
			char []		numeric_chars	= new char[]{'0','1','2','3','4','5','6','7','8','9'} ;

			//strip any numeric suffix?
			name_base = BaseName.TrimEnd(numeric_chars) ;

			do
			{
				//increment name
				new_name = name_base + index ;
				index++ ;
			}
			while (FindControl(new_name) != null) ;

			return new_name ;
		}

		/// <summary>
		/// Finds the control with the specified name
		/// </summary>
		/// <param name="CtrlName">Name of control to find</param>
		/// <returns>Named Control or null if not found</returns>
		public System.Windows.Forms.Control FindControl(string CtrlName)
		{
			System.Windows.Forms.Control		ret_val		= null ;

			foreach (System.Windows.Forms.Control ctrl in Controls)
			{
//				if (ctrl.Name == CtrlName)
//				{
//					ret_val = ctrl ;
//					break ;
//				}
                if (CtrlName.StartsWith(ctrl.Name))
                {
                    if (ctrl.Name.Equals(CtrlName))
                    {
                        return ctrl;
                    }
                    if (CtrlName[ctrl.Name.Length] == '.')
                    {
                        string next_part;
                        next_part = CtrlName.Substring(ctrl.Name.Length + 1);

                        KineticaRT.Compact.Controls.FrameBase frame = ctrl as KineticaRT.Compact.Controls.FrameBase ;
                        if (frame != null)
                        {
                            ret_val = frame.FindObject(next_part) as System.Windows.Forms.Control ;
                            break ;
                        }

                        break;
                    }
				}
			}

			return ret_val ;
		}


		/// <summary>
		/// Adds a component of the specified type
		/// </summary>
		/// <param name="ComponentType">Type of component to add</param>
		/// <returns>New Component (or null if failed)</returns>
		public Component AddComponent(Type ComponentType)
		{
			Component			new_component		= null ;

			if (ComponentType != null)
			{
//NS			LicenseContext		lic_context	= new PanelLicenseContext() ;
//				new_component = LicenseManager.CreateWithContext(ComponentType, lic_context, new object[]{components}) as Component ;

/*NS*/			new_component = Activator.CreateInstance (ComponentType) as Component ;
				
				if (new_component != null)
				{
					components.Add (new_component, ComponentType.ToString()) ;

					//if we are connected, connect the new component
					if (connected && new_component is IConnectable)
					{
						((IConnectable)new_component).ObjectSearch += searchHdlr ;
						((IConnectable)new_component).Connect() ;
					}
				}
			}

			return new_component ;
		}
        #endregion

		#region Events
        ///// <summary>
        ///// ControlAdded Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected virtual void OnControlAdded(ControlEventArgs e)
        //{
        //    if (e.Control is IConnectable)
        //    {
                //IConnectable krt_ctrl = e.Control as IConnectable;
                //krt_ctrl.ObjectSearch += searchHdlr;

        //        KineticaRT.Controls.FrameBase framebase = krt_ctrl as FrameBase;
        //        if (framebase != null)
        //        {
        //            framebase.GetAddList += addListHdlr;
        //        }

        //        //if we are connected, connect the new control
        //        if (connected) krt_ctrl.Connect();
        //    }
        //    //base.OnControlAdded(e);
        //}

        ///// <summary>
        ///// ControlRemoved Event
        ///// </summary>
        ///// <param name="e">Event Data</param>
        //protected virtual void OnControlRemoved(ControlEventArgs e)
        //{
        //    if (e.Control is IConnectable)
        //    {
                //IConnectable krt_ctrl = e.Control as IConnectable;
                //krt_ctrl.ObjectSearch -= searchHdlr;

        //        if (krt_ctrl is Frame)
        //        {
        //            ((Frame)krt_ctrl).GetAddList -= addListHdlr;
        //        }
        //    }
        //    //base.OnControlRemoved(e);
        //}
		#endregion

		#region GetAddList
		/// <summary>
		/// Event generated to retrieve AddList
		/// </summary>
		public	event Frame.GetAddListEventHandler			GetAddList ;

		private void OnGetAddList(out ArrayList AddList, out ArrayList ComponentAddList, out string[] References)
		{
			if (GetAddList == null)
			{
				//don't return a null AddList - return an empty list
				AddList = new ArrayList() ;
				ComponentAddList = new ArrayList() ;
				//ok to return a null references list
				References = null ;
			}
			else
			{
				GetAddList(out AddList, out ComponentAddList, out References) ;
			}
		}

		private string [] GetReferencedAssemblies()
		{
			if (GetAddList != null)
			{
				ArrayList		add_list ;
				ArrayList		component_list ;
				string[]		references ;

				GetAddList(out add_list, out component_list, out references) ;

				return references ;
			}
			return null ;
		}
		#endregion

		#region IConnectable
		/// <summary>
		/// Event which fires when the value of a property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged ;   

		/// <summary>
		/// Event for requests to find an object by name.
		/// </summary>
        public event KineticaRT.Compact.ObjectSearchEventHandler ObjectSearch;   
		/// <summary>
		/// Fires an ObjectSearch event for the named object
		/// </summary>
		protected object OnObjectSearch(string objectName)
		{
			//first check ourself (controls and components)
			//object		obj		= FindObject(objectName) ;
			foreach (System.Windows.Forms.Control ctrl in Controls)
			{
				if (ctrl.Name.Equals(objectName))
				{
					return ctrl ;
				}
			}

			//## check components that have names

			//if (obj == null)
			//{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName);  
			//}
			//return obj ;
		}

		/// <summary>
		/// Looks for the named object amongst local controls and components
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>object or null if not found</returns>
		public virtual object FindObject(string objectName)
		{
			//return OnObjectSearch (objectName) ;
			foreach (System.Windows.Forms.Control ctrl in Controls)
			{
				//if (ctrl.Name.Equals(objectName))
                if (objectName.StartsWith(ctrl.Name))
                {
                    if (ctrl.Name.Equals(objectName))
                    {
                        return ctrl;
                    }
                    if (objectName[ctrl.Name.Length] == '.')
                    {
                        string next_part;
                        next_part = objectName.Substring(ctrl.Name.Length + 1);

                        KineticaRT.Compact.Controls.FrameBase frame = ctrl as KineticaRT.Compact.Controls.FrameBase;
                        if (frame != null)
                        {
                            return frame.FindObject(next_part);
                        }

                        return null;
                    }
				}
			}

			//## check components that have names

			return null ;
		}

		/// <summary>
		/// Looks for the named object amongst controls and components
		/// </summary>
		/// <param name="objectName">Name of Object to find</param>
		/// <returns>object or null if not found</returns>
		public virtual object SearchObject(string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public void Connect()
        {
            connected = true;
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                IConnectable connectable_control = ctrl as IConnectable;
                if (connectable_control != null)
                {
                    connectable_control.ObjectSearch += searchHdlr;
                    connectable_control.Connect();
                }
            }

            //foreach (Component component in components.Components)
            //{
            //    if (component is IConnectable)
            //    {
            //        ((IConnectable)component).ObjectSearch += searchHdlr;
            //        ((IConnectable)component).Connect();
            //        //## what if connected more than once?
            //    }
            //}
        }

        /// <summary>
        /// Connects this object to others, Stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public void Connect2()
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                if (ctrl is IConnectable2)
                {
                    ((IConnectable2)ctrl).Connect2();
                }
            }

            //foreach (Component component in components.Components)
            //{
            //    if (component is IConnectable2)
            //    {
            //        ((IConnectable2)component).Connect2();
            //    }
            //}
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public void Disconnect ()
		{
			foreach (System.Windows.Forms.Control ctrl in this.Controls)
			{
                IConnectable connectable_control = ctrl as IConnectable;
                if (connectable_control != null)
                {
                    connectable_control.Disconnect();
                    connectable_control.ObjectSearch -= searchHdlr;
				}
			}

            //foreach (Component component in components.Components)
            //{
            //    if (component is IConnectable)
            //    {
            //        ((IConnectable)component).Disconnect() ;
            //        //## what if connected more than once?
            //        if (connected)
            //        {
            //            ((IConnectable)component).ObjectSearch -= searchHdlr ;
            //        }
            //    }
            //}
			connected = false ;
		}
		#endregion

		#region Static Methods
        ///// <summary>
        ///// Creates a dll in the named file from the named source file
        ///// </summary>
        ///// <param name="SourceFilename">Source file</param>
        ///// <param name="DllFilename">File where dll is created</param>
        ///// <param name="References">Array of assembly names to reference in the dll</param>
        ///// <param name="compact">Uses the compact framework if true.</param>
        ///// <returns>True if succeeded</returns>
        ///// <remarks>This function is invoked automatically if a panel dll is requested that doesn't exist, but a .cs file of the same name does exist.</remarks>
        //public static bool CreateDllFromSource(string SourceFilename, string DllFilename, string [] References, bool compact)
        //{
        //    if (!File.Exists(SourceFilename))		return false ;

        //    // Create a compiler.
        //    CodeDomProvider		provider		= new Microsoft.CSharp.CSharpCodeProvider ();
        //    //ICodeCompiler		compiler		= provider.CreateCompiler () ;
        //    CompilerParameters	parameters		= new CompilerParameters () ;
        //    CompilerResults		results	;
        //    string				class_name		= Path.GetFileNameWithoutExtension(DllFilename) ;
        //    string				res_filename ;

        //    AddReferences(parameters, null, compact);

        //    parameters.MainClass = class_name ;
        //    parameters.OutputAssembly = DllFilename ;

        //    if (compact)
        //    {
        //        parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.dll");
        //        parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Data.dll");
        //        parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Drawing.dll");
        //        parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Windows.Forms.dll");
        //        parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Xml.dll");

        //        parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.dll"));
        //        parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
        //        parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));

        //    }
        //    else
        //    {
        //        parameters.ReferencedAssemblies.Add("System.dll");
        //        parameters.ReferencedAssemblies.Add("System.Drawing.dll");
        //        parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

        //        parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.dll"));
        //        parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
        //        parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));
        //    }

        //    //if there is a relevant resource file, embed it
        //    res_filename = "KineticaRT.Panel." + class_name + ".resources" ;
        //    res_filename = Path.Combine(Path.GetDirectoryName(DllFilename), res_filename) ;
        //    if (File.Exists(res_filename))
        //    {
        //        parameters.CompilerOptions = "/res:\"" + res_filename + "\"" ;
        //    }

        //    if (References != null)
        //    {
        //        foreach (string assembly in References)
        //        {
        //            parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, assembly)) ;
        //        }
        //    }

        //    //results = compiler.CompileAssemblyFromFile(parameters, SourceFilename);
        //    results = provider.CompileAssemblyFromFile(parameters, SourceFilename);

        //    // Check for erorrs.
        //    if (results.Errors.HasErrors) 
        //    {
        //        string lcErrorMsg = "";

        //        // *** Create Error String
        //        lcErrorMsg = results.Errors.Count.ToString() + " Errors:";
        //        for (int x=0;x<results.Errors.Count;x++) 
        //        {
        //            lcErrorMsg = lcErrorMsg  + "\r\nLine: " + results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;		
        //        }

        //        MessageBox.Show(SourceFilename + ": " + lcErrorMsg,"Panel Compiler",MessageBoxButtons.OK,MessageBoxIcon.Error);

        //        return false ;
        //    }
        //    return true ;
        //}
		#endregion
	}
}

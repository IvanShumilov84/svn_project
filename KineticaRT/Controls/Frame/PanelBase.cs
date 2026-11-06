#if !VS2012
    extern alias Compact2;
//extern alias Compact2;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Xml;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Panel control. This is the part of the Frame that actually holds the contained controls.
	/// </summary>
    [TypeConverterAttribute(typeof(ExpandableObjectTypeConverter))]
    public class PanelBase : System.Windows.Forms.UserControl, KineticaRT.IConnectable
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
#if !VS2012
    #if CompactEnabled
        private static bool compactEnabled =  true;
    #else
        private static bool compactEnabled = false;
    #endif
#endif
        private KineticaRT.ObjectSearchEventHandler searchHdlr;
#if !VS2012
        private Compact2.KineticaRT.Compact.ObjectSearchEventHandler compactSearchHdlr;
#endif
        private Frame.GetAddListEventHandler addListHdlr;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.Container components = new Container();

        #endregion

        #region Initialisation and Disposal
        /// <summary>
        /// Initialises a new instance of the PanelBase class with the specified Container
        /// </summary>
        /// <param name="container">IContainer to which the PanelBase is added</param>
        public PanelBase(IContainer container)
        {
            if (container != null) container.Add(this);

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.Connected = false;

            searchHdlr = new KineticaRT.ObjectSearchEventHandler(OnObjectSearch);
#if !VS2012
            if (compactEnabled) compactSearchHdlr = new Compact2.KineticaRT.Compact.ObjectSearchEventHandler(OnCompactObjectSearch);
#endif
            addListHdlr = new Frame.GetAddListEventHandler(OnGetAddList);

            //this is for loading the panel's controls
            InitializePanel();

            Connect();
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

            this.Connected = false;

            searchHdlr = new KineticaRT.ObjectSearchEventHandler(OnObjectSearch);
#if !VS2012
            if (compactEnabled) compactSearchHdlr = new Compact2.KineticaRT.Compact.ObjectSearchEventHandler(OnCompactObjectSearch);
#endif
            addListHdlr = new Frame.GetAddListEventHandler(OnGetAddList);

            //this is for loading the panel's controls
            InitializePanel();

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

            this.Name = Name;

            this.Connected = false;

            searchHdlr = new KineticaRT.ObjectSearchEventHandler(OnObjectSearch);
#if !VS2012
            if (compactEnabled) compactSearchHdlr = new Compact2.KineticaRT.Compact.ObjectSearchEventHandler(OnCompactObjectSearch);
#endif
            addListHdlr = new Frame.GetAddListEventHandler(OnGetAddList);

            //this is for loading the panel's controls
            InitializePanel();
        }

        /// <summary>
        /// Initialize the panel.
        /// </summary>
        /// <remarks>This should be overriden in the derived classes to initialise the properties and create the child controls.</remarks>
        protected virtual void InitializePanel()
        {
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
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
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//ft
            this.SetStyle(ControlStyles.Selectable, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//ft
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a collections of Components contained by the Panel
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ComponentCollection Components
        {
            get
            {
                return components.Components;
            }
        }
        private bool ShouldSerializeComponents()
        {
            return (components.Components != null);
        }
        #endregion

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
            if (FindControl(BaseName) == null) return BaseName;

            //otherwise, find a unique one
            string new_name;
            string name_base;
            int index = 1;
            char[] numeric_chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //strip any numeric suffix?
            name_base = BaseName.TrimEnd(numeric_chars);

            do
            {
                //increment name
                new_name = name_base + index;
                index++;
            }
            while (FindControl(new_name) != null);

            return new_name;
        }

        /// <summary>
        /// Finds the control with the specified name
        /// </summary>
        /// <param name="CtrlName">Name of control to find</param>
        /// <returns>Named Control or null if not found</returns>
        public System.Windows.Forms.Control FindControl(string CtrlName)
        {
            System.Windows.Forms.Control ret_val = null;

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

                        KineticaRT.Controls.FrameBase frame = ctrl as KineticaRT.Controls.FrameBase;
                        if (frame != null)
                        {
                            ret_val = frame.FindObject(next_part) as System.Windows.Forms.Control;
                            break;
                        }

                        break;
                    }
                }
            }

            return ret_val;
        }


        /// <summary>
        /// Adds a component of the specified type
        /// </summary>
        /// <param name="ComponentType">Type of component to add</param>
        /// <returns>New Component (or null if failed)</returns>
        public Component AddComponent(Type ComponentType)
        {
            Component new_component = null;

            if (ComponentType != null)
            {
                //NS			LicenseContext		lic_context	= new PanelLicenseContext() ;
                //				new_component = LicenseManager.CreateWithContext(ComponentType, lic_context, new object[]{components}) as Component ;

                /*NS*/
                new_component = Activator.CreateInstance(ComponentType) as Component;

                if (new_component != null)
                {
                    components.Add(new_component, ComponentType.ToString());

                    //if we are connected, connect the new component
                    if (this.Connected)
                    {
                        if (new_component is IConnectable)
                        {
                            ((KineticaRT.IConnectable)new_component).ObjectSearch += searchHdlr;
                            ((KineticaRT.IConnectable)new_component).Connect();
                        }
#if !VS2012
                        else if (compactEnabled && new_component is Compact2.KineticaRT.Compact.IConnectable)
                        {
                            ((Compact2.KineticaRT.Compact.IConnectable)new_component).ObjectSearch += compactSearchHdlr;
                            ((Compact2.KineticaRT.Compact.IConnectable)new_component).Connect();
                        }
#endif
                    }
                }
            }

            return new_component;
        }
        #endregion

        #region Events
        /// <summary>
        /// ControlAdded Event
        /// </summary>
        /// <param name="e">Event Data</param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is IConnectable)
            {
                IConnectable krt_ctrl = e.Control as IConnectable;
                krt_ctrl.ObjectSearch += searchHdlr;

                KineticaRT.Controls.FrameBase framebase = krt_ctrl as FrameBase;
                if (framebase != null)
                {
                    framebase.GetAddList += addListHdlr;
                }

                //if we are connected, connect the new control
                if (this.Connected) krt_ctrl.Connect();
            }
#if !VS2012
            else if (compactEnabled && e.Control is Compact2.KineticaRT.Compact.IConnectable)
            {
                Compact2.KineticaRT.Compact.IConnectable krt_ctrl = e.Control as Compact2.KineticaRT.Compact.IConnectable;
                krt_ctrl.ObjectSearch += compactSearchHdlr;

                //KineticaRT.Controls.FrameBase framebase = krt_ctrl as FrameBase ;
                //if (framebase != null)
                //{
                //    framebase.GetAddList += addListHdlr ;
                //}

                //if we are connected, connect the new control
                if (connected) krt_ctrl.Connect();
            }
#endif
            base.OnControlAdded(e);
        }

        /// <summary>
        /// ControlRemoved Event
        /// </summary>
        /// <param name="e">Event Data</param>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (e.Control is IConnectable)
            {
                IConnectable krt_ctrl = e.Control as IConnectable;
                if (this.Connected) krt_ctrl.Disconnect();
                krt_ctrl.ObjectSearch -= searchHdlr;

                if (krt_ctrl is Frame)
                {
                    ((Frame)krt_ctrl).GetAddList -= addListHdlr;
                }
            }
#if !VS2012
            else if (compactEnabled && e.Control is Compact2.KineticaRT.Compact.IConnectable)
            {
                Compact2.KineticaRT.Compact.IConnectable krt_ctrl = e.Control as Compact2.KineticaRT.Compact.IConnectable;
                if (connected) krt_ctrl.Disconnect();
                krt_ctrl.ObjectSearch -= compactSearchHdlr;

                //if (krt_ctrl is Frame)
                //{
                //    ((Frame)krt_ctrl).GetAddList -= addListHdlr;
                //}
            }
#endif

            base.OnControlRemoved(e);
        }
        #endregion

        #region GetAddList
        /// <summary>
        /// Event generated to retrieve AddList
        /// </summary>
        public event Frame.GetAddListEventHandler GetAddList;

        private void OnGetAddList(out ArrayList AddList, out ArrayList ComponentAddList, out string[] References)
        {
            if (GetAddList == null)
            {
                //don't return a null AddList - return an empty list
                AddList = new ArrayList();
                ComponentAddList = new ArrayList();
                //ok to return a null references list
                References = null;
            }
            else
            {
                GetAddList(out AddList, out ComponentAddList, out References);
            }
        }

        private string[] GetReferencedAssemblies()
        {
            if (GetAddList != null)
            {
                ArrayList add_list;
                ArrayList component_list;
                string[] references;

                GetAddList(out add_list, out component_list, out references);

                return references;
            }
            return null;
        }
        #endregion

        #region IConnectable
        /// <summary>
        /// Event which fires when the value of a property has changed.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Event for requests to find an object by name.
        /// </summary>
        public event KineticaRT.ObjectSearchEventHandler ObjectSearch;
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
                    return ctrl;
                }
            }

            //## check components that have names

            //if (obj == null)
            //{
            if (ObjectSearch == null) return null;
            return ObjectSearch(objectName);
            //}
            //return obj ;
        }
#if !VS2012
        /// <summary>
        /// Fires an ObjectSearch event for the named object
        /// </summary>
        protected object OnCompactObjectSearch(string objectName)
        {
            object non_compact_object = null;

            //first check ourself (controls and components)
            //object		obj		= FindObject(objectName) ;
            foreach (System.Windows.Forms.Control ctrl in Controls)
            {
                if (ctrl.Name.Equals(objectName))
                {
                    non_compact_object = ctrl;
                    break; ;
                }
            }

            //## check components that have names

            //if (obj == null)
            //{
            if (non_compact_object == null)
            {
                if (ObjectSearch != null)
                {
                    non_compact_object = ObjectSearch(objectName);
                }
            }

            if (non_compact_object is KineticaRT.Channels.IChannel)
            {
                CompactChannelProxy proxy = new CompactChannelProxy(non_compact_object as KineticaRT.Channels.IChannel);

                return (Compact2.KineticaRT.Compact.Channels.IChannel)proxy;
            }

            return non_compact_object;
            //}
            //return obj ;
        }
#endif
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

                        KineticaRT.Controls.FrameBase frame = ctrl as KineticaRT.Controls.FrameBase;
                        if (frame != null)
                        {
                            return frame.FindObject(next_part);
                        }

                        return null;
                    }
                }
            }

            //## check components that have names

            return null;
        }

        /// <summary>
        /// Looks for the named object amongst controls and components
        /// </summary>
        /// <param name="objectName">Name of Object to find</param>
        /// <returns>object or null if not found</returns>
        public virtual object SearchObject(string objectName)
        {
            return OnObjectSearch(objectName);
        }

        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public void Connect()
        {
            this.Connected = true;
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                IConnectable connectable_control = ctrl as IConnectable;
                if (connectable_control != null)
                {
                    connectable_control.Connect();
                }
#if !VS2012
                else if (compactEnabled)
                {
                    Compact2.KineticaRT.Compact.IConnectable compact_connectable_control = ctrl as Compact2.KineticaRT.Compact.IConnectable;
                    if (compact_connectable_control != null)
                    {
                        compact_connectable_control.Connect();
                    }
                }
#endif
            }

            foreach (Component component in components.Components)
            {
                if (component is IConnectable)
                {
                    ((IConnectable)component).ObjectSearch += searchHdlr;
                    ((IConnectable)component).Connect();
                    //## what if connected more than once?
                }
            }
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
#if !VS2012
                else if (compactEnabled)
                {
                    Compact2.KineticaRT.Compact.IConnectable2 compact_connectable_control = ctrl as Compact2.KineticaRT.Compact.IConnectable2;
                    if (compact_connectable_control != null)
                    {
                        compact_connectable_control.Connect2();
                    }
                }
#endif
            }

            foreach (Component component in components.Components)
            {
                if (component is IConnectable2)
                {
                    ((IConnectable2)component).Connect2();
                }
            }
        }

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        public void Disconnect()
        {
            foreach (System.Windows.Forms.Control ctrl in this.Controls)
            {
                if (ctrl is IConnectable)
                {
                    ((IConnectable)ctrl).Disconnect();
                }
            }

            foreach (Component component in components.Components)
            {
                if (component is IConnectable)
                {
                    ((IConnectable)component).Disconnect();
                    //## what if connected more than once?
                    if (this.Connected)
                    {
                        ((IConnectable)component).ObjectSearch -= searchHdlr;
                    }
                }
            }
            this.Connected = false; 
        }
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool Connected //ft
        {
            get { return connected; }
            set { connected = value; }
        }
        #endregion

        #region Saving
        private static void AddReferences(CompilerParameters parameters, string[] references, bool compact)
        {
            parameters.IncludeDebugInformation = false;
#if !VS2012
            if (compactEnabled && compact)
            {
                //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.dll");
                //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Data.dll");
                //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Drawing.dll");
                //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Windows.Forms.dll");
                //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Xml.dll");

                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Data.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Drawing.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Windows.Forms.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Xml.dll"));

                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.Controls.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.Controls.Frame.dll"));
            }

            else
            {
#endif
            
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

                parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));
#if !VS2012            
            }
#endif

            if (references != null)
            {
                foreach (string assembly in references)
                {
                    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, assembly));
                }
                
            }
            else //ft ajout du else
            {
                ReadReferencesFromProjectFile(parameters, references, compact);//ft Ajout pour loader les dependances a partir du .ksp
            }

        }

        private static void ReadReferencesFromProjectFile(CompilerParameters parameters, string[] references, bool compact)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            
            string projectFile = Path.ChangeExtension(System.Windows.Forms.Application.ExecutablePath, ".ksp");
            projectFile = Path.ChangeExtension(Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath), ".ksp");
            try
            {
                doc.Load(projectFile);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Invalid packed project file " + Path.GetFileName(projectFile));
            }
                        
            //open the project file as xml
            try
            {
                root = doc.DocumentElement;
                if (root.Name != "Project")
                {
                    MessageBox.Show("Root element must be 'Project'", "Invalid project file " + Path.GetFileName(projectFile));
                }
                else
                {
                    //XmlElement	channelAddList = root.SelectSingleNode("AddableChannels") as XmlElement;
                    XmlElement controlAddList = root.SelectSingleNode("AddableControls") as XmlElement;
                    if (controlAddList != null)
                    {
                        // Add user defined references.
                        XmlNodeList list = controlAddList.SelectNodes("Reference[@Assembly]");
                        foreach (XmlElement reference in list)
                        {
                            string dllname;
                            dllname = reference.GetAttribute("Assembly");
                            parameters.ReferencedAssemblies.Add(Path.Combine(System.Windows.Forms.Application.StartupPath, dllname));
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Invalid project file contents: " + Path.GetFileName(projectFile));
            }
        }


        /// <summary>
        /// Saves the current properties, child controls etc to a dll and corresponding C# source file.
        /// </summary>
        /// <remarks>
        /// Normally called from "Save" on the panel's context menu.
        /// Throws an ArgumentException if PanelName is an empty string
        /// </remarks>
        /// <param name="PanelName">Name (and path) of file to save to</param>
        /// <param name="compact">If true then the KineticaRT Compact version is used.</param>
        public void Save(string PanelName, bool compact)
        {
            SaveNonCompact(PanelName);
#if !VS2012
            if (compact) SaveCompact(PanelName);
#endif
        }
#if !VS2012
        private void SaveCompact(string PanelName)
        {
            if (PanelName == "")
            {
                throw (new ArgumentException("Cannot save to a blank name"));
            }

            string class_name = Path.GetFileNameWithoutExtension(PanelName);
            PanelName = Path.GetFullPath(Path.ChangeExtension(PanelName, ".cs"));
            PanelName = Path.GetDirectoryName(PanelName)+Path.DirectorySeparatorChar+"Compact." + Path.GetFileName (PanelName);

            //Create our namespace, class and initialise function.
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            CodeNamespace nameSpace = new CodeNamespace("KineticaRT.Panel");
            CodeTypeDeclaration panel = new CodeTypeDeclaration(class_name);
            CodeMemberMethod initialise_panel = new CodeMemberMethod();

            panel.BaseTypes.Add(typeof(Compact2.KineticaRT.Compact.Controls.PanelBase));
            panel.Comments.Add(new CodeCommentStatement("Customized KineticaRT Panel", false));
            initialise_panel.Comments.Add(new CodeCommentStatement("Initialize the Panel", false));
            initialise_panel.Name = "InitializePanel";
            initialise_panel.Attributes = MemberAttributes.Override | MemberAttributes.Family;

            // Stick them all together.
            compileUnit.Namespaces.Add(nameSpace);
            nameSpace.Types.Add(panel);
            panel.Members.Add(initialise_panel);

            // Add all the initialisation code.
            int variable_statement_index = 0;
            CodeMethodInvokeExpression suspend_method;
            CodeMethodReferenceExpression suspend_ref;
            CodeMethodInvokeExpression resume_method;
            CodeMethodReferenceExpression resume_ref;

            suspend_ref = new CodeMethodReferenceExpression();
            suspend_ref.MethodName = "SuspendLayout";
            suspend_ref.TargetObject = new CodeThisReferenceExpression();
            suspend_method = new CodeMethodInvokeExpression(suspend_ref, new CodeExpression[0]);

            resume_ref = new CodeMethodReferenceExpression();
            resume_ref.MethodName = "ResumeLayout";
            resume_ref.TargetObject = new CodeThisReferenceExpression();
            resume_method = new CodeMethodInvokeExpression(resume_ref, new CodeExpression[0]);

            System.Resources.ResourceWriter res_writer = null;
            string res_filename;

            res_filename = "KineticaRT.Compact.Panel." + class_name + ".resources";
            res_filename = Path.Combine(Path.GetDirectoryName(PanelName), res_filename);

            initialise_panel.Statements.Add(suspend_method);

            //create the object code...
            SaveObject(this, "this", initialise_panel, ref variable_statement_index, ref res_writer, res_filename, true);

            initialise_panel.Statements.Add(resume_method);

            //separate variable declarations from the rest
            initialise_panel.Statements.Insert(variable_statement_index, new CodeSnippetStatement(""));

            if (res_writer != null)
            {
                //we have used the resource file
                CodeStatement res_manager;
                CodeExpression mgr_initialiser;
                CodeExpression[] mgr_parameters;

                //save it...
                res_writer.Close();

                //create a resource manager variable to reload resources...
                mgr_parameters = new CodeExpression[1];
                mgr_parameters[0] = new CodeTypeOfExpression(class_name);
                mgr_initialiser = new CodeObjectCreateExpression(typeof(System.Resources.ResourceManager), mgr_parameters);
                res_manager = new CodeVariableDeclarationStatement(typeof(System.Resources.ResourceManager), "resources", mgr_initialiser);

                initialise_panel.Statements.Insert(0, res_manager);
            }

            // Create a code generator.
            CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            StreamWriter writer = new StreamWriter(PanelName);

            options.BracingStyle = "C";
            options.BlankLinesBetweenMembers = false;

            // Save the code to a .cs file.
            provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
            writer.Flush();
            writer.Close();

            // Create a compiler.
            CompilerParameters parameters = new CompilerParameters();
            parameters.OutputAssembly = System.IO.Path.ChangeExtension(PanelName, "dll");
            parameters.MainClass = class_name;

            AddReferences(parameters, GetReferencedAssemblies(), true);

            if (res_writer != null)
            {
                parameters.CompilerOptions = "/res:\"" + res_filename + "\"";
            }

            CompilerResults results = provider.CompileAssemblyFromDom(parameters, compileUnit);

            // Check for erorrs.
            if (results.Errors.HasErrors)
            {
                string lcErrorMsg = "";

                // *** Create Error String
                lcErrorMsg = results.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < results.Errors.Count; x++)
                {
                    lcErrorMsg = lcErrorMsg + "\r\nLine: " + results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                }

                MessageBox.Show(lcErrorMsg, "Panel Compiler (" + class_name + ")", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }
#endif
        private void SaveNonCompact (string PanelName)
        {
            if (PanelName == "")
            {
                throw (new ArgumentException("Cannot save to a blank name"));
            }

            //Create our namespace, class and initialise function.
            string class_name = Path.GetFileNameWithoutExtension(PanelName);
            PanelName = Path.GetFullPath(Path.ChangeExtension(PanelName, ".cs"));
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            CodeNamespace nameSpace = new CodeNamespace("KineticaRT.Panel");
            CodeTypeDeclaration panel = new CodeTypeDeclaration(class_name);
            CodeMemberMethod initialise_panel = new CodeMemberMethod();

            panel.BaseTypes.Add(typeof(KineticaRT.Controls.PanelBase));
            panel.Comments.Add(new CodeCommentStatement("Customized KineticaRT Panel", false));
            initialise_panel.Comments.Add(new CodeCommentStatement("Initialize the Panel", false));
            initialise_panel.Name = "InitializePanel";
            initialise_panel.Attributes = MemberAttributes.Override | MemberAttributes.Family;

            // Stick them all together.
            compileUnit.Namespaces.Add(nameSpace);
            nameSpace.Types.Add(panel);
            panel.Members.Add(initialise_panel);

            // Add all the initialisation code.
            int variable_statement_index = 0;
            CodeMethodInvokeExpression suspend_method;
            CodeMethodReferenceExpression suspend_ref;
            CodeMethodInvokeExpression resume_method;
            CodeMethodReferenceExpression resume_ref;

            suspend_ref = new CodeMethodReferenceExpression();
            suspend_ref.MethodName = "SuspendLayout";
            suspend_ref.TargetObject = new CodeThisReferenceExpression();
            suspend_method = new CodeMethodInvokeExpression(suspend_ref, new CodeExpression[0]);

            resume_ref = new CodeMethodReferenceExpression();
            resume_ref.MethodName = "ResumeLayout";
            resume_ref.TargetObject = new CodeThisReferenceExpression();
            resume_method = new CodeMethodInvokeExpression(resume_ref, new CodeExpression[0]);

            System.Resources.ResourceWriter res_writer = null;
            string res_filename;

            res_filename = "KineticaRT.Panel." + class_name + ".resources";
            res_filename = Path.Combine(Path.GetDirectoryName(PanelName), res_filename);

            initialise_panel.Statements.Add(suspend_method);

            //create the object code...
            SaveObject(this, "this", initialise_panel, ref variable_statement_index, ref res_writer, res_filename, false);

            initialise_panel.Statements.Add(resume_method);
            //separate variable declarations from the rest
            initialise_panel.Statements.Insert(variable_statement_index, new CodeSnippetStatement(""));

            //make sure we have our license context declared at the beginning
            //NS		CodeExpression		initialiser ;
            //			CodeStatement		context ;
            //			
            //			initialiser = new CodeObjectCreateExpression(new CodeTypeReference(typeof(PanelLicenseContext)), new CodeParameterDeclarationExpression[0]) ;
            //			context = new CodeVariableDeclarationStatement(typeof(LicenseContext), "context", initialiser) ;
            //			initialise_panel.Statements.Insert(0, context) ;

            if (res_writer != null)
            {
                //we have used the resource file
                CodeStatement res_manager;
                CodeExpression mgr_initialiser;
                CodeExpression[] mgr_parameters;

                //save it...
                res_writer.Close();

                //create a resource manager variable to reload resources...
                mgr_parameters = new CodeExpression[1];
                mgr_parameters[0] = new CodeTypeOfExpression(class_name);
                mgr_initialiser = new CodeObjectCreateExpression(typeof(System.Resources.ResourceManager), mgr_parameters);
                res_manager = new CodeVariableDeclarationStatement(typeof(System.Resources.ResourceManager), "resources", mgr_initialiser);

                initialise_panel.Statements.Insert(0, res_manager);
            }

            // Create a code generator.
            CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
            //05 ICodeGenerator			generator	= provider.CreateGenerator() ;
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            string filepath = Path.GetFullPath(Path.ChangeExtension(PanelName, ".cs"));
            bool isReadOnly = (File.Exists(filepath) && ((File.GetAttributes(filepath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly));
            if (!isReadOnly)
            {
                StreamWriter writer = new StreamWriter(filepath);
            
                options.BracingStyle = "C";
                options.BlankLinesBetweenMembers = false;

                // Save the code to a .cs file.
                //05 generator.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);
                //			generator.GenerateCodeFromCompileUnit (compileUnit, Console.Out, options) ;
                writer.Flush();
                writer.Close();

                // Create a compiler.
                //ICodeCompiler		compiler		= provider.CreateCompiler () ;
                CompilerParameters parameters = new CompilerParameters();
                parameters.OutputAssembly = System.IO.Path.ChangeExtension(PanelName, "dll"); 
                parameters.MainClass = class_name;

                AddReferences(parameters, GetReferencedAssemblies(), false);

                if (res_writer != null)
                {
                    parameters.CompilerOptions = "/res:\"" + res_filename + "\"";
                }

                CompilerResults results = provider.CompileAssemblyFromDom(parameters, compileUnit);
                //compiler.CompileAssemblyFromDom (parameters, compileUnit) ;

                // Check for erorrs.
                if (results.Errors.HasErrors)
                {
                    string lcErrorMsg = "";

                    // *** Create Error String
                    lcErrorMsg = results.Errors.Count.ToString() + " Errors:";
                    for (int x = 0; x < results.Errors.Count; x++)
                    {
                        lcErrorMsg = lcErrorMsg + "\r\nLine: " + results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                    }

                    MessageBox.Show(lcErrorMsg, "Panel Compiler (" + class_name + ")", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                //			// Save the dll file.
                //			File.Copy (results.CompiledAssembly.Location, PanelName, true) ;
            }
            else
            {
                DialogResult mesgRes = MessageBox.Show("The file is Read Only and cannot be saved." + Environment.NewLine + 
                                                       filepath + Environment.NewLine + Environment.NewLine +
                                                       "BE SURE THAT YOU HAVE THE MOST UPDATED VERSION" +  Environment.NewLine +
                                                       "BEFORE GET LOCK THE FILE!!!!" + Environment.NewLine +
                                                       "OTHERWISE YOU HAVE TO LEAVE KineticaRT TO DO IT" + Environment.NewLine +
                                                       "AND ALL YOUR PROGRESS WILL BE LOST." + Environment.NewLine + Environment.NewLine +
                                                       "You have the most updated version of this panel." + Environment.NewLine +
                                                       "You can save this panel safely." + Environment.NewLine + Environment.NewLine +
                                                       "Open containing folder?", "Save Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);            
                switch(mesgRes){
                    case DialogResult.Yes:
                        System.Diagnostics.Process.Start("explorer.exe", Directory.GetParent(filepath).FullName);
                        break;
                    
                    case DialogResult.No:
                        break;
                }
            
            }

        }

        //adds initialisation statements for the specified object to the
        //specified method
        internal void SaveObject(object Obj, string Name, CodeMemberMethod Method, ref int VariableStatementIndex, ref System.Resources.ResourceWriter ResWriter, string ResName, bool compact)
        {
            System.Reflection.PropertyInfo[] properties = Obj.GetType().GetProperties();
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(Obj);

            if (Obj is ISupportInitialize)
            {
                CodeMethodInvokeExpression init;
                CodeMethodReferenceExpression reference;

                reference = new CodeMethodReferenceExpression();
                reference.MethodName = "BeginInit";
                reference.TargetObject = new CodeCastExpression(typeof(ISupportInitialize), new CodeVariableReferenceExpression(Name));
                init = new CodeMethodInvokeExpression(reference, new CodeExpression[0]);

                Method.Statements.Add(init);
            }

            foreach (System.Reflection.PropertyInfo property in properties)
            {
                //save only those properties which are readable...

                /*if (property.Name == "Visible") //ft desactive
                {
                    System.Diagnostics.Trace.WriteLine("Visible");
                }*/

                if (property.CanRead)
                {
                    PropertyDescriptor desc = coll.Find(property.Name, false);

                    if (property.Name == "DataSource" /* || property.Name == "Items" */) //ft Items est necessaire pour pouvoir Sauvegarder des valerus statiques
                        continue; //ft Evite de serializer la propriete DataSource, sinon au moment de caller res_writer.Close() ca lance une exception Type 'System.Windows.Forms.ComboBox' in Assembly 'Sytem.Windows.Forms is not marked as serializable

                    //...think they need saving and are not hidden
                    if (desc != null &&
                        desc.SerializationVisibility != DesignerSerializationVisibility.Hidden &&
                        desc.ShouldSerializeValue(Obj))
                    {
                        object val = property.GetValue(Obj, null);

                        //is it a standard (writeable) property?
                        if (property.CanWrite)
                        {
                            //save an assign statement ('property = value ;')
                            CodeAssignStatement assign = new CodeAssignStatement();
                            string var_name = Name + "." + property.Name;

                            assign.Left = new CodeVariableReferenceExpression(var_name);
                            //assign.Right = PropertyToCode(property.PropertyType, val, var_name, Method, ref VariableStatementIndex, ref ResWriter, ResName);
                            if (val == null)
                            {
                                assign.Right = new CodeSnippetExpression("null");
                            }
                            else
                            {
                                assign.Right = PropertyToCode(val.GetType(), val, var_name, Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);
                            }

                            Method.Statements.Add(assign);
                        }
                        else if (desc.SerializationVisibility == DesignerSerializationVisibility.Content)
                        {
                            //save its constituent parts
                            if (val is ICollection)
                            {
                                SaveCollection(val as ICollection, Name + "." + property.Name, Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);
                            }
                            else
                            {
                                SaveObject(val, Name + "." + property.Name, Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);
                            }
                        }
                    }
                }
            }

            if (Obj is ISupportInitialize)
            {
                CodeMethodInvokeExpression init;
                CodeMethodReferenceExpression reference;

                reference = new CodeMethodReferenceExpression();
                reference.MethodName = "EndInit";
                reference.TargetObject = new CodeCastExpression(typeof(ISupportInitialize), new CodeVariableReferenceExpression(Name));
                init = new CodeMethodInvokeExpression(reference, new CodeExpression[0]);

                Method.Statements.Add(init);
            }
        }

        //returns a CodeExpression for the given object
        //If required returns a variable reference, and adds extra code statements
        //directly to the given Method
        //'Name' is a (theoretically) unique name used if the property has to be
        //saved to a resource.
        private CodeExpression PropertyToCode(Type PropertyType, object Value, string Name, CodeMemberMethod Method, ref int VariableStatementIndex, ref System.Resources.ResourceWriter ResWriter, string ResName, bool compact)
        {
            if (Value == null) return new CodeSnippetExpression("null");

            TypeConverter conv = TypeDescriptor.GetConverter(PropertyType);
            CodeExpression code;

            //have we got an InstanceDescriptor?
            if (conv.CanConvertTo(typeof(InstanceDescriptor)))
            {
                code = PropertyToDescribedCode(conv, PropertyType, Value, Name, Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);
            }
            else
            {
                //no Instance descriptor - assume definition is 'Complete'
                if (Value is string || Value.GetType().IsPrimitive)
                {
                    code = new CodePrimitiveExpression(Value);
                }
                else if (Value is Type)
                {
                    code = new CodeTypeOfExpression((Type)Value);
                }
                else if (Value is Array)
                {
                    CodeExpression[] initialisers;

                    initialisers = ArrayToInitialisers(Value as Array, Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);
                    code = new CodeArrayCreateExpression(Value.GetType(), initialisers);
                }
                else if (PropertyType.IsSerializable)
                {
                    //not a type we know how to deal with - save to resource file
                    CodeExpression res_expr;
                    CodeMethodReferenceExpression reference;
                    CodeExpression[] parameters;

                    reference = new CodeMethodReferenceExpression();
                    reference.MethodName = "GetObject";
                    reference.TargetObject = new CodeVariableReferenceExpression("resources");

                    parameters = new CodeExpression[1];
                    parameters[0] = new CodePrimitiveExpression(Name);

                    res_expr = new CodeMethodInvokeExpression(reference, parameters);

                    code = new CodeCastExpression(Value.GetType(), res_expr);

                    if (ResWriter == null)
                    {
                        ResWriter = new System.Resources.ResourceWriter(ResName);
                    }
                    ResWriter.AddResource(Name, Value);
                }
                else
                {
                    if (!PropertyType.IsInterface) Trace.WriteLine("Unserializable Type: " + Name + " (" + PropertyType.ToString() + ")", Value.GetType().ToString());
                    code = new CodeSnippetExpression("null");
                }
            }

            return code;
        }

        //returns a CodeExpression for the given object using an InstanceDescriptor
        //If required returns a variable reference, and adds extra code statements
        //directly to the given Method
        private CodeExpression PropertyToDescribedCode(TypeConverter Converter, Type PropertyType, object Value, string Name, CodeMemberMethod Method, ref int VariableStatementIndex, ref System.Resources.ResourceWriter ResWriter, string ResName, bool compact)
        {
            CodeExpression code;
            InstanceDescriptor inst_desc; ;

            inst_desc = (InstanceDescriptor)Converter.ConvertTo(Value, typeof(InstanceDescriptor));

            //##only doing 'not complete' for constructors

            if (!inst_desc.IsComplete &&
                inst_desc.MemberInfo.MemberType == MemberTypes.Constructor)
            {
                //definition is not complete - need to create a variable, 
                //set it up and return a reference to it
                CodeExpression[] parameters;
                int index = 0;
                CodeObjectCreateExpression initialiser;
                CodeVariableDeclarationStatement variable;
                string var_name;

                //code never going to be seen uncompiled, so unhelpful variable names are OK
                var_name = "var" + VariableStatementIndex.ToString();

                parameters = new CodeExpression[inst_desc.Arguments.Count];
                foreach (object arg in inst_desc.Arguments)
                {
                    //convert each parameter to code
                    parameters[index] = PropertyToCode(arg.GetType(), arg, var_name + "_" + index, Method, ref VariableStatementIndex, ref ResWriter, ResName,compact);
                    index++;
                }

                initialiser = new CodeObjectCreateExpression(PropertyType, parameters);
                variable = new CodeVariableDeclarationStatement(PropertyType, var_name, initialiser);
                //add the variable at the beginning of the method
                Method.Statements.Insert(VariableStatementIndex++, variable);

                //add other defining statements
                SaveObject(Value, var_name, Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);

                code = new CodeVariableReferenceExpression(var_name);
            }
            else	//definition is complete - simply return a single CodeExpression
            {
                Trace.WriteLineIf(!inst_desc.IsComplete, inst_desc.MemberInfo.MemberType.ToString(), "Not complete");

                switch (inst_desc.MemberInfo.MemberType)
                {
                    case MemberTypes.Constructor:
                        {
                            CodeExpression[] parameters;
                            int index = 0;

                            parameters = new CodeExpression[inst_desc.Arguments.Count];
                            foreach (object arg in inst_desc.Arguments)
                            {
                                //convert each parameter to code
                                parameters[index] = PropertyToCode(arg.GetType(), arg, Name + "_" + index, Method, ref VariableStatementIndex, ref ResWriter, ResName,compact);
                                index++;
                            }

                            code = new CodeObjectCreateExpression(PropertyType, parameters);
                        }
                        break;

                    case MemberTypes.Method:
                        {
                            CodeMethodReferenceExpression method;
                            CodeExpression[] parameters;
                            int index = 0;

                            parameters = new CodeExpression[inst_desc.Arguments.Count];
                            foreach (object arg in inst_desc.Arguments)
                            {
                                //convert each parameter to code
                                parameters[index] = PropertyToCode(arg.GetType(), arg, Name + "_" + index, Method, ref VariableStatementIndex, ref ResWriter, ResName,compact);
                                index++;
                            }

                            method = new CodeMethodReferenceExpression();
                            method.MethodName = inst_desc.MemberInfo.Name;
                            method.TargetObject = new CodeTypeReferenceExpression(inst_desc.MemberInfo.DeclaringType);

                            CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(method, parameters);

                            //do we need to cast the return value?
                            if (((MethodInfo)inst_desc.MemberInfo).ReturnType != PropertyType)
                            {
                                code = new CodeCastExpression(PropertyType, invoke);
                            }
                            else
                            {
                                code = invoke;
                            }
                        }
                        break;

                    case MemberTypes.Property:
                        {
                            code = new CodePropertyReferenceExpression(new CodeTypeReferenceExpression(inst_desc.MemberInfo.DeclaringType), Converter.ConvertToString(Value));
                        }
                        break;

                    case MemberTypes.Field:
                        {
                            string text = Converter.ConvertToString(Value);
                            if (text == "") text = "Empty";

                            code = new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(inst_desc.MemberInfo.DeclaringType), text);
                        }
                        break;

                    default:
                        {
                            Debug.WriteLine(inst_desc.MemberInfo.MemberType.ToString(), "Unhandled InstDesc");
                            code = new CodeSnippetExpression(Converter.ConvertToString(Value));
                        }
                        break;
                }
            }

            return code;
        }

        private CodeExpression[] ArrayToInitialisers(Array Value, CodeMemberMethod Method, ref int VariableStatementIndex, ref System.Resources.ResourceWriter ResWriter, string ResName, bool compact)
        {
            if (Value.Length == 0)
            {
                return new CodeSnippetExpression[0];
            }
            else
            {
                CodeExpression[] inits = new CodeExpression[Value.Length];
                int index = 0;

                foreach (object obj in Value)
                {
                    //##note that this does not guarantee unique names being passed in
                    // - this is OK as long as no array initialisers need saving to
                    // the resources file (which they probably won't
                    inits[index] = PropertyToCode(obj.GetType(), obj, "ArrInit" + index, Method, ref VariableStatementIndex, ref ResWriter, ResName,compact);
                    index++;
                }

                return inits;
            }
        }

        //adds initialisation statements for the specified Collection to the
        //specified method
        //CollName is eg 'this.Controls'
        //VariableStatementIndex is line index in method for next variable declaration
        //atm Can only save ControlCollection collections
        private void SaveCollection(ICollection Coll, string CollName, CodeMemberMethod Method, ref int VariableStatementIndex, ref System.Resources.ResourceWriter ResWriter, string ResName, bool compact)
        {
            if (Coll.Count > 0)
            {
                CodeVariableDeclarationStatement[] variables = new CodeVariableDeclarationStatement[Coll.Count];
                string[] names = new String[Coll.Count];
                int index = 0;
                //CodeExpression						create_method ;
                CodeMethodReferenceExpression create_ref = new CodeMethodReferenceExpression();
                CodeExpression[] create_params;
                CodeExpression initialiser;
                Type child_type;

                create_ref.MethodName = "CreateWithContext";
                create_ref.TargetObject = new CodeTypeReferenceExpression(typeof(LicenseManager));

                if (Coll is ControlCollection)
                {
                    create_params = new CodeExpression[2];
                    create_params[1] = new CodeVariableReferenceExpression("context");

                    foreach (System.Windows.Forms.Control ctrl in Coll)
                    {
                        //ensure names are unique by numbering them
                        names[index] = "ctrl" + VariableStatementIndex;

                        child_type = ctrl.GetType();

                        //create and initialise variable for object
                        //use license manager to get licenced controls
                        //NS					create_params[0]= new CodeTypeOfExpression(child_type) ;
                        //						create_method	= new CodeMethodInvokeExpression(create_ref, create_params) ;
                        //						initialiser		= new CodeCastExpression(child_type, create_method) ;
                        /*NS*/
                        initialiser = new CodeObjectCreateExpression(new CodeTypeReference(child_type), new CodeParameterDeclarationExpression[0]);
                        variables[index] = new CodeVariableDeclarationStatement(child_type, names[index], initialiser);

                        //add the variable at the beginning of the method
                        Method.Statements.Insert(VariableStatementIndex++, variables[index]);

                        //save object
                        SaveObject(ctrl, names[index], Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);
                        index++;
                    }

                    if (!compact)
                    {
                        //add objects to collection
                        CodeMethodReferenceExpression method;
                        CodeExpression[] parameters;
                        CodeExpression[] initialisers = new CodeExpression[Coll.Count];

                        index = 0;
                        foreach (string name in names)
                        {
                            initialisers[index] = new CodeVariableReferenceExpression(name);
                            index++;
                        }

                        parameters = new CodeExpression[1];
                        parameters[0] = new CodeArrayCreateExpression(typeof(System.Windows.Forms.Control), initialisers);

                        method = new CodeMethodReferenceExpression();
                        method.MethodName = "AddRange";
                        method.TargetObject = new CodeVariableReferenceExpression(CollName);

                        CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(method, parameters);
                        Method.Statements.Add(invoke);
                    }
                    else
                    {
                        //add objects to collection
                        index = 0;
                        foreach (string name in names)
                        {
                            CodeMethodReferenceExpression method;
                            CodeExpression[] parameters;

                            parameters = new CodeExpression[1];
                            parameters[0] = new CodeVariableReferenceExpression(name); 

                            method = new CodeMethodReferenceExpression();
                            method.MethodName = "Add";
                            method.TargetObject = new CodeVariableReferenceExpression(CollName);

                            CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(method, parameters);
                            Method.Statements.Add(invoke);
                        }
                    }
                }
                else if (Coll is ComponentCollection)
                {
                    create_params = new CodeExpression[3];
                    create_params[1] = new CodeVariableReferenceExpression("context");
                    //setup arguments 'new object[]{components}'
                    CodeExpression[] p = new CodeExpression[1];
                    p[0] = new CodeVariableReferenceExpression("components");
                    create_params[2] = new CodeArrayCreateExpression(typeof(object), p);

                    foreach (Component component in Coll)
                    {
                        //ensure names are unique by numbering them
                        names[index] = "component" + VariableStatementIndex;

                        child_type = component.GetType();

                        //create and initialise variable for object
                        //use license manager to get licenced controls
                        //NS						create_params[0]= new CodeTypeOfExpression(child_type) ;
                        //						create_method	= new CodeMethodInvokeExpression(create_ref, create_params) ;
                        //						initialiser		= new CodeCastExpression(child_type, create_method) ;
                        /*NS*/
                        initialiser = new CodeObjectCreateExpression(new CodeTypeReference(child_type), new CodeParameterDeclarationExpression[0]);
                        variables[index] = new CodeVariableDeclarationStatement(child_type, names[index], initialiser);

                        //add the variable at the beginning of the method
                        Method.Statements.Insert(VariableStatementIndex++, variables[index]);

                        //save object
                        SaveObject(component, names[index], Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);

                        //Add the component to the components list.
                        //components.Add (component0, "component0") ;
                        Method.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("components"), "Add", new CodeExpression[] { new CodeVariableReferenceExpression(names[index]), new CodePrimitiveExpression(names[index]) }));

                        index++;
                    }
                }
                else		//ICollection
                {
                    create_params = new CodeExpression[2];
                    create_params[1] = new CodeVariableReferenceExpression("context");

                    foreach (object obj in Coll)
                    {
                        //ensure names are unique by numbering them
                        names[index] = "obj" + VariableStatementIndex;

                        child_type = obj.GetType();

                        if (child_type == typeof(string))
                        {
                            variables[index] = new CodeVariableDeclarationStatement(child_type, names[index], new CodePrimitiveExpression(obj.ToString()));
                            Method.Statements.Insert(VariableStatementIndex++, variables[index]);

                        }
                        else
                        {
                            //create and initialise variable for object
                            //use license manager to get licenced controls
                            //NS					create_params[0]= new CodeTypeOfExpression(child_type) ;
                            //						create_method	= new CodeMethodInvokeExpression(create_ref, create_params) ;
                            //						initialiser		= new CodeCastExpression(child_type, create_method) ;
                            /*NS*/
                            initialiser = new CodeObjectCreateExpression(new CodeTypeReference(child_type), new CodeParameterDeclarationExpression[0]);
                            variables[index] = new CodeVariableDeclarationStatement(child_type, names[index], initialiser);

                            //add the variable at the beginning of the method
                            Method.Statements.Insert(VariableStatementIndex++, variables[index]);

                            //save object
                            SaveObject(obj, names[index], Method, ref VariableStatementIndex, ref ResWriter, ResName, compact);
                        }

                        index++;
                    }

                    //add objects to collection
                    CodeMethodReferenceExpression method;
                    CodeExpression[] parameters;
                    CodeExpression[] initialisers = new CodeExpression[Coll.Count];

                    index = 0;
                    foreach (string name in names)
                    {
                        initialisers[index] = new CodeVariableReferenceExpression(name);
                        index++;
                    }

                    Type elementType = Coll.GetType().GetMethod("AddRange").GetParameters()[0].ParameterType.GetElementType();

                    parameters = new CodeExpression[1];
                    if (null != elementType)
                    {
                        parameters[0] = new CodeArrayCreateExpression(elementType, initialisers);
                    }
                    else
                    {
                        parameters[0] = new CodeArrayCreateExpression(typeof(object), initialisers);
                    }

                    method = new CodeMethodReferenceExpression();
                    method.MethodName = "AddRange";
                    method.TargetObject = new CodeVariableReferenceExpression(CollName);

                    CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(method, parameters);
                    Method.Statements.Add(invoke);
                }
            }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a dll in the named file from the named source file
        /// </summary>
        /// <param name="SourceFilename">Source file</param>
        /// <param name="DllFilename">File where dll is created</param>
        /// <param name="References">Array of assembly names to reference in the dll</param>
        /// <param name="compact">Uses the compact framework if true.</param>
        /// <returns>True if succeeded</returns>
        /// <remarks>This function is invoked automatically if a panel dll is requested that doesn't exist, but a .cs file of the same name does exist.</remarks>
        public static bool CreateDllFromSource(string SourceFilename, string DllFilename, string[] References, bool compact)
        {
            if (!File.Exists(SourceFilename)) return false;

            // Create a compiler.
            CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
            //ICodeCompiler		compiler		= provider.CreateCompiler () ;
            CompilerParameters parameters = new CompilerParameters();
            CompilerResults results;
            string class_name = Path.GetFileNameWithoutExtension(DllFilename);
            string res_filename;

            AddReferences(parameters, References, compact); //ft Dans Addreferences on y ajoute aussi System.dll , System.Drawing... etc

            parameters.MainClass = class_name;
            parameters.OutputAssembly = DllFilename;

            //if (compactEnabled && compact)
            //{
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Data.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Drawing.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Windows.Forms.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Xml.dll");

            //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.dll"));
            //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
            //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));

            //}
            //else
            //{
            //    parameters.ReferencedAssemblies.Add("System.dll");
            //    parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            //    parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            //    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.dll"));
            //    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
            //    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));
            //}

            //if there is a relevant resource file, embed it
            res_filename = "KineticaRT.Panel." + class_name + ".resources";
            res_filename = Path.Combine(Path.GetDirectoryName(DllFilename), res_filename);
            if (File.Exists(res_filename))
            {
                parameters.CompilerOptions = "/res:\"" + res_filename + "\"";
            }

            //ft bout de code inutile pcq deja fait dans le call de AddReferences plus haut
            //ft if (References != null)
            //ft {
            //ft     foreach (string assembly in References)
            //ft     {
            //ft         parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, assembly));
            //ft     }
            //ft }

            //results = compiler.CompileAssemblyFromFile(parameters, SourceFilename);
            results = provider.CompileAssemblyFromFile(parameters, SourceFilename);

            // Check for erorrs.
            if (results.Errors.HasErrors)
            {
                string lcErrorMsg = "";

                // *** Create Error String
                lcErrorMsg = results.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < results.Errors.Count; x++)
                {
                    lcErrorMsg = lcErrorMsg + "\r\nLine: " + results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                }

                MessageBox.Show(SourceFilename + ": " + lcErrorMsg, "Panel Compiler", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }
        #endregion
#if !VS2012
        private class CompactChannelProxy : Compact2.KineticaRT.Compact.Channels.IChannel
        {
            private KineticaRT.Channels.IChannel channel;

            public CompactChannelProxy(KineticaRT.Channels.IChannel channel)
            {
                this.channel = channel;
                channel.ValueChanged += new KineticaRT.Channels.ValueChangedEventHandler(channel_ValueChanged);
            }

            void channel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
            {
                if (this.ValueChanged != null) this.ValueChanged(this, ConvertValue(newVal));
            }

            /// <summary>
            /// Gets or sets the channels name.  The name is used to identify the channel from other objects.
            /// </summary>
            /// <value>The objects name.</value>
            //[System.ComponentModel.Description("Name of channel.  This is used to identify the object from other objects.  E.G. in an Expression or in the Source property of a Dial."), System.ComponentModel.Category("Object")]
            public string Name
            {
                get
                {
                    return channel.Name;
                }
                set
                {
                    channel.Name = value;
                }
            }

            private Compact2.KineticaRT.Compact.Values.Value ConvertValue(KineticaRT.Values.Value value)
            {
                KineticaRT.Values.Double double_value = value as KineticaRT.Values.Double;
                if (double_value!=null) return new Compact2.KineticaRT.Compact.Values.Double(double_value.ToDouble());
                return new Compact2.KineticaRT.Compact.Values.Text(value.ToString());
            }

            /// <summary>Gets or sets the value of this channel.</summary>
            /// <value>A Value object representing the value of the channel.</value>
            public Compact2.KineticaRT.Compact.Values.Value Value
            {
                get
                {
                    return ConvertValue(channel.Value);
                }
                set
                {
                    channel.Value = new KineticaRT.Values.Text(value.ToString());
                }
            }

            /// <summary>
            /// Converts this Value to a System.String.
            /// </summary>
            /// <returns>The result.</returns>
            public string ToString(string format, IFormatProvider info)
            {
                return channel.ToString(format, info);
            }


            /// <summary>Occurs when the value of the Value property changes.</summary>
            public event Compact2.KineticaRT.Compact.Channels.ValueChangedEventHandler ValueChanged;

            /// <summary>
            /// Connects this object to others.
            /// </summary>
            public void Connect()
            {
                channel.Connect();
            }

            /// <summary>
            /// Disconnects this object from others.
            /// </summary>
            public void Disconnect()
            {
                channel.Disconnect();
            }

            /// <summary>
            /// Event handler for requests from the objects children to find another object for them by name.
            /// </summary>
            public event Compact2.KineticaRT.Compact.ObjectSearchEventHandler ObjectSearch
            {
                add { }
                remove { }
            }

            /// <summary>
            /// Returns the named Object or null if it does not exist.
            /// </summary>
            public object FindObject(string objectName)
            {
                return channel.FindObject(objectName);
            }

            /// <summary>
            /// Returns the named sibling object or null if it does not exist.
            /// </summary>
            public object SearchObject(string objectName)
            {
                return channel.SearchObject(objectName);
            }

            /// <summary>
            /// Event which fires when the value of the objects property has changed.
            /// </summary>
            public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
            {
                add { }
                remove { }
            }

        }
#endif
    }

    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (null == ienum) return seqType;
            return ienum.GetGenericArguments()[0];
        }
        private static Type FindIEnumerable(Type seqType)
        {
            if (null == seqType || seqType == typeof(string))
                return null;
            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());
            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }
            Type [] ifaces = seqType.GetInterfaces();
            if (null != ifaces && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (null != ienum) return ienum;
                }
            }
            if (null != seqType.BaseType && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }
            return null;
        }
    }
}

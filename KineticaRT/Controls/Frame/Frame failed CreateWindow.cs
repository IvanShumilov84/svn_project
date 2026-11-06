using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;
using KineticaRT;
using System.CodeDom;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

using System.Runtime.InteropServices;

public delegate int CallBack(int hwnd, uint msg, int wParam, int lParam);

[StructLayout(LayoutKind.Sequential)]
public struct WNDCLASS
{
	public uint			style;
	public CallBack		lpfnWndProc;
	public int			cbClsExtra;
	public int			cbWndExtra;
	public IntPtr		hInstance;
	public IntPtr		hIcon;
	public IntPtr		hCursor;
	public IntPtr		hbrBackground;
	public string		lpszMenuName;
	public string		lpszClassName;
}

namespace KineticaRT.Controls
{
	/// <summary>
	/// Frame: Container Control
	/// </summary>
//	[Serializable]
//	[DesignerSerializer(typeof(MyCodeDomSerializer), typeof(CodeDomSerializer))]
	public class Frame : KineticaRT.Controls.Control
	{
		private class Addable
		{
			public string			name ;
			public string			assembly ;
			public string			typename ;

			public Addable(string Name, string Assembly, string Typename)
			{
				name = Name ;
				assembly = Assembly ;
				typename = Typename ;
			}
		}

		#region Variables
		private ArrayList			addList ;
		private	UserControl			editCtrl ;

		//		private EditForm			editForm ;
		//		private bool				editMode ;

		private IntPtr				clicksWindow ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private ObjectSearchEventHandler		searchHdlr ;
		#endregion

		#region Initialisation and Disposal
		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		private static extern int RegisterClass(ref WNDCLASS WC) ;

		const int	CS_DBLCLKS          = 0x0008;
		const int	CS_PARENTDC         = 0x0080;

		/// <summary>
		/// Initialises a new instance of the Frame class
		/// </summary>
		public Frame()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			addList = new ArrayList() ;
			editCtrl = new KineticaRT.Controls.EditMode() ;

			//editMode = false ;
			
			clicksWindow = IntPtr.Zero ;

			WNDCLASS	wc;           
		
			wc.style         = CS_PARENTDC | CS_DBLCLKS ;	// Class style
			wc.lpfnWndProc   = new CallBack(ClicksProc);	// window proc
			wc.hInstance     = IntPtr.Zero;			//_Module.GetModuleInstance() ;		// Application that owns the class.
			wc.lpszClassName = "ClicksWindowClass";
			
			wc.cbClsExtra = 0 ;
			wc.cbWndExtra = 0 ;
			wc.hbrBackground = IntPtr.Zero ;
			wc.hCursor = IntPtr.Zero ;
			wc.hIcon = IntPtr.Zero ;
			wc.lpszMenuName = "" ;

			int ret = RegisterClass(ref wc) ;


			//temporary...
			addList.Add(new Addable("Dial", "KineticaRT.Controls.Dial.dll", "KineticaRT.Controls.Dial")) ;

			Controls.Add(new Button()) ;
			Controls.Add(new Button()) ;
			Controls[0].BackColor = Color.AliceBlue ;
			Controls[1].BackColor = Color.Beige ;
			//...

			searchHdlr = new ObjectSearchEventHandler(OnChildObjectSearch);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
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
			// Frame
			// 
			this.Name = "Frame";
			this.Size = new System.Drawing.Size(272, 168);

		}
		#endregion

		#region Properties
		//		[Browsable(true)]
		//		public new ControlCollection Controls
		//		{
		//			get { return base.Controls ; }
		//		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a control from the add list
		/// </summary>
		/// <param name="Name">Name in add list</param>
		/// <returns>Name of added object (or "" if failed)</returns>
		public string Add(string Name)
		{
			string		ctrl_name		= "" ;

			for (int index = 0; index < addList.Count; index++)
			{
				if (((Addable)addList[index]).name == Name)
				{
					ctrl_name = Add(index) ;
					break ;
				}
			}

			//keep transparent window topmost (if it is in use at the moment)
			if (Controls.Contains(editCtrl))
			{
				editCtrl.BringToFront() ;
			}

			return ctrl_name ;
		}

		/// <summary>
		/// Adds a control from the add list by index
		/// </summary>
		/// <param name="Index">Zero-based index in add list</param>
		/// <returns>Name of added object (or "" if failed)</returns>
		public string Add(int Index)
		{
			string		ctrl_name		= "" ;

			System.Runtime.Remoting.ObjectHandle		obj_ctrl ;
			KineticaRT.Controls.Control					ctrl ;
			Addable										addable		= addList[Index] as Addable ;

			obj_ctrl = Activator.CreateInstanceFrom (addable.assembly, addable.typename) ;
			ctrl = (KineticaRT.Controls.Control)obj_ctrl.Unwrap() ;
			if (ctrl != null) 
			{
				//## this doesn't prevent the multiple objects with the same name
				// - must create a unique name at this point
				ctrl_name = ctrl.Name ;

				this.Controls.Add(ctrl) ;
			}

			//keep transparent window topmost (if it is in use at the moment)
			if (Controls.Contains(editCtrl))
			{
				editCtrl.BringToFront() ;
			}

			return ctrl_name ;
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

		#region Control events
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
				if (this.ContextMenu == null)
				{
					MenuItem []		items ;
					items = new MenuItem [1] ;
					items[0] = new MenuItem("Enter Edit Mode", new EventHandler(OnClickEditMode)) ;
					this.ContextMenu = new ContextMenu(items) ;
				}

				this.ContextMenu.Show(this, new Point(e.X, e.Y)) ;
			}

			base.OnMouseDown(e) ;
		}
		#endregion

		#region Menu Handling
		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		private static extern IntPtr CreateWindowEx(
			ulong	dwExStyle,
			string	lpClassName,
			string	lpWindowName,
			ulong	dwStyle,
			int		X,
			int		Y,
			int		nWidth,
			int		nHeight,
			IntPtr	hWndParent,
			IntPtr	hMenu,
			IntPtr	hInstance,
			IntPtr	lpParam);

		[System.Runtime.InteropServices.DllImportAttribute("Kernel32.dll")]
		private static extern int GetLastError();

		const long	WS_EX_TRANSPARENT	= 0x00000020 ;
		const long	WS_CHILD            = 0x40000000 ;
		const long	WS_VISIBLE          = 0x10000000 ;

		private int ClicksProc(int hwnd, uint msg, int wparam, int lparam)
		{
			return 0 ;
		}

		private void OnClickEditMode(object sender, EventArgs e)
		{
			clicksWindow = CreateWindowEx(WS_EX_TRANSPARENT, "ClicksWindowClass", "Clicks", WS_CHILD | WS_VISIBLE, 0, 0, ClientRectangle.Width, ClientRectangle.Height, Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero) ;

			//int		err = GetLastError() ;
			//Debug.WriteLine(err.ToString()) ;

//edit ctrl...
//			if (!Contains(editCtrl))
//			{
//				this.Controls.Add(editCtrl) ;
//			}
//
//			editCtrl.Size = this.Size ;
//			editCtrl.BringToFront() ;

//edit form...
//			editMode = true ;
//			editForm = new EditForm() ;
//			editForm.MouseDown += new MouseEventHandler(OnEditMouseDown) ;
//			editForm.MouseMove += new MouseEventHandler(OnEditMouseMove) ;
//			editForm.MouseUp += new MouseEventHandler(OnEditMouseUp) ;
//			editForm.Closed += new EventHandler(OnEditClosed) ;
//			editForm.Size = this.Size ;
//			editForm.Location = this.PointToScreen(new Point(0,0)) ;
//			editForm.ShowDialog(this) ;
		}

//		protected override void OnMouseEnter(EventArgs e)
//		{
//			if (editMode)
//			{
//				editForm.ShowDialog(this) ;
//			}
//			base.OnMouseEnter(e) ;
//		}
		#endregion

		#region TEST
//		private System.Windows.Forms.Control		dragItem ;
//		private Point								startPoint ;		//mouse pos
//		private Point								referencePoint ;	//ctrl location
//
//		private void OnEditMouseDown(object sender, MouseEventArgs e)
//		{
//			if (e.Button == MouseButtons.Left)
//			{
//				//have we clicked on a control?
//				dragItem = PointToChild(e.X, e.Y) ;
//
//				if (dragItem != null)
//				{
//					//start a drag on that control
//					editForm.Capture = true ;
//					startPoint = new Point(e.X, e.Y) ;
//					referencePoint = dragItem.Location ;
//				}
//			}
//		}
//
//		private void OnEditMouseMove (object sender, MouseEventArgs e)
//		{
//			if (editForm.Capture && dragItem != null)
//			{
//				Point		diff		= new Point(startPoint.X - e.X, startPoint.Y - e.Y) ;
//				Point		new_pos		= new Point(referencePoint.X - diff.X, referencePoint.Y - diff.Y) ;
//
//				dragItem.Location = new_pos ;
//			}
//		}
//
//		private void OnEditMouseUp (object sender, MouseEventArgs e)
//		{
//			editForm.Capture = false ;
//		}
//
//		private void OnEditClosed (object sender, EventArgs e)
//		{
//			editMode = false ;
//		}
		#endregion

		#region Internal methods (edit mode handling)
		//find which control (not including editCtrl) is at the given point
		internal System.Windows.Forms.Control PointToChild(int X, int Y)
		{
			Rectangle		rect ;
			//NB: GetChildAtPoint cannot ignore editCtrl

			//			//search backwards to find topmost ctrl
			//			for (int index = Controls.Count - 1; index >= 0; index--)

			for (int index = 0; index < Controls.Count; index++)
			{
				if (Controls[index] != editCtrl)
				{
					rect = Controls[index].ClientRectangle ;
					rect.Location = Controls[index].Location ;
					if (rect.Contains(X, Y))
					{
						return Controls[index] ;
					}
				}
			}

			return null ;
		}

		internal MenuItem [] GetAddListItems(EventHandler Handler)
		{
			MenuItem []		items		= new MenuItem[addList.Count] ;

			for (int index = 0; index < addList.Count; index++)
			{
				items[index] = new MenuItem(((Addable)addList[index]).name, Handler) ;
			}

			return items ;
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
			XmlElement		root ;
			XmlElement		item ;

			try
			{
				document.Load (File) ;
			}
			catch(XmlException e)
			{
				Trace.WriteLine(e.Message, "XML Parse Failed") ;
				return ;
			}

			root = document.DocumentElement ;
			item = (XmlElement)root.SelectSingleNode ("Frame") ;
		
			int		start = Environment.TickCount ;
			int		end ;
			LoadXML (item) ;
			end = Environment.TickCount ;
			//Trace.WriteLine(end - start, "Load time") ;
			//System.Windows.Forms.MessageBox.Show((end - start).ToString()) ;
		}

		/// <summary>
		/// Save the named XML file
		/// </summary>
		/// <param name="File">File to save to</param>
		public void SaveFile(string File)
		{
			XmlDocument		document	= new XmlDocument () ;
			XmlElement		root ;
			XmlElement		item ;

			try
			{
				document.Load (File) ;
			}
			catch(XmlException e)
			{
				Trace.WriteLine(e.Message, "XML Parse Failed") ;
				return ;
			}

			root = document.DocumentElement ;
			item = (XmlElement)root.SelectSingleNode ("Frame") ;
			
			SaveXML (item) ;

			document.Save(File) ;
		}

		/// <summary>
		/// Loads from XML
		/// </summary>
		/// <param name="element">XmlElement to load from</param>
		public override void LoadXML (XmlElement element)
		{
			//load self...
			base.LoadXML(element) ;

			//...and load children (controls)
			XmlElement							controls_element ;

			controls_element = element.SelectSingleNode("Controls") as XmlElement ;
			if (controls_element != null)
			{
				XmlNodeList		ctrl_list ;

				//find all controls, create and load them
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

	#region CodeDomSerializer
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

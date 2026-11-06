using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;
using System.Xml;


namespace KineticaRT.Compact.Studio
{
	/// <summary>
	/// The KineticaRT.Studio.StudioExplorer class is an extension of the
	/// KineticaRT.Controls.Explorer, which allows adding and removing channels.
	/// </summary>
	public class StudioExplorer : KineticaRT.Controls.Explorer
	{
		#region Definitions - enums & events
		enum MenuItems
		{
			ShowProperties		= 0,
			ShowPropertiesBar	= 1,
			Connect				= 2,
			Disconnect			= 3,
			SaveBar				= 4,
			Save				= 5,
			AddBar				= 6,
			Add					= 7,
			CopyBar				= 8,
            Cut                 = 9,
			Copy				= 10,
			Paste				= 11,
			Delete				= 12,
			TopmostBar			= 13,
			Topmost				= 14,
            Count               = 15, // Only used to indicate the number of entries.
		}

		/// <summary>Event handler delegate for the SaveCollection event</summary>
		public delegate void SaveCollectionEventHandler (ChannelCollection Collection) ;
		
		/// <summary>Event generated to Save the given collection to file</summary>
		public event SaveCollectionEventHandler	SaveCollection ;
		#endregion

		#region Variables
		private System.ComponentModel.IContainer components = null;

		private	System.IO.Stream					copyStream ;
		#endregion (variables)

		#region Initialisation and disposal
		/// <summary>
		/// Initialises a new instance of the StudioExplorer class
		/// </summary>
		public StudioExplorer()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			//setup the context menu
            MenuItem[] items = new MenuItem[(int)MenuItems.Count];

			items[(int)MenuItems.ShowProperties] = new MenuItem("S&how Properties", new EventHandler(OnMenuShowProperties)) ;
			items[(int)MenuItems.ShowProperties].Checked = true ;
			items[(int)MenuItems.ShowPropertiesBar] = new MenuItem("-") ;
			items[(int)MenuItems.Connect]		= new MenuItem("C&onnect", new EventHandler(OnMenuConnect)) ;
			items[(int)MenuItems.Disconnect]	= new MenuItem("D&isconnect", new EventHandler(OnMenuDisconnect)) ;
			items[(int)MenuItems.SaveBar]		= new MenuItem("-") ;
			items[(int)MenuItems.Save]			= new MenuItem("&Save", new EventHandler(OnMenuSave), Shortcut.CtrlS) ;
			items[(int)MenuItems.AddBar]		= new MenuItem("-") ;
			items[(int)MenuItems.Add]			= new MenuItem("&Add") ;
			items[(int)MenuItems.CopyBar]		= new MenuItem("-") ;
            items[(int)MenuItems.Cut] = new MenuItem("&Cut", new EventHandler(OnMenuCut), Shortcut.CtrlX);
            items[(int)MenuItems.Copy] = new MenuItem("&Copy", new EventHandler(OnMenuCopy), Shortcut.CtrlC);
			items[(int)MenuItems.Paste]			= new MenuItem("&Paste", new EventHandler(OnMenuPaste), Shortcut.CtrlV) ;
			items[(int)MenuItems.Delete]		= new MenuItem("&Delete", new EventHandler(OnMenuDelete), Shortcut.Del) ;
			items[(int)MenuItems.TopmostBar]	= new MenuItem("-") ;
			items[(int)MenuItems.Topmost]		= new MenuItem("Always on &Top", new EventHandler(OnMenuTopmost)) ;

			items[(int)MenuItems.Add].Enabled = false ;
			
			treeView.ContextMenu = new ContextMenu(items) ;
			treeView.ContextMenu.Popup += new EventHandler(OnMenuPopup) ;
            treeView.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);

			copyStream = null ;
		}

        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnMenuPopup(sender,e);
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (copyStream != null)		copyStream.Close() ;

				if (components != null) 	components.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion (initialisation)

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Sets up the list of channels that can be added to the tree
		/// </summary>
		/// <param name="Root">XmlElement containing the addable channels</param>
		public void SetAddList(XmlElement Root)
		{
			//read the list straight into menu items
			MenuItem	add			= treeView.ContextMenu.MenuItems[(int)MenuItems.Add] ;

			//clear any old items
			add.MenuItems.Clear() ;

			if (Root != null)
			{
				AddMenuItem		new_item ;
				XmlNodeList		list		= Root.SelectNodes("Add[@Name and @Type]") ;
				EventHandler	add_hdlr	= new EventHandler(OnMenuAdd) ;

				foreach (XmlElement elt in list)
				{
					new_item = new AddMenuItem(elt.GetAttribute("Name"), add_hdlr, elt.GetAttribute("Type")) ;
					add.MenuItems.Add(new_item) ;
				}
			}

			add.Enabled = (add.MenuItems.Count > 0) ;
		}
		
		/// <summary>
		/// Focuses on the given root object of the Tree, if it exists
		/// </summary>
		/// <param name="RootObject">Object at the root level upon which to focus</param>
		/// <returns>True if succeded, False if object not found</returns>
		public bool SelectObject(object RootObject)
		{
			bool			ok		= false ;

			//search the root nodes
			foreach (ExtNode node in treeView.Nodes)
			{
				if (node.Object.Equals(RootObject))
				{
					node.Expand() ;
					treeView.SelectedNode = node ;
					ok = true ;
				}
				else		//collapse everything else
				{
					node.Collapse() ;
				}
			}

			if (ok)		treeView.SelectedNode.EnsureVisible() ;

			return ok ;
		}

		#endregion (public methods)

		#region Menu events
		/// <summary>
		/// Menu Popup event handler
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnMenuPopup(object sender, EventArgs e)
		{
			ExtNode		node			= treeView.SelectedNode as ExtNode ;

			base.OnMenuPopup(sender, e) ;

			if (node == null)
			{
				treeView.ContextMenu.MenuItems[(int)MenuItems.SaveBar].Visible = false ;
				treeView.ContextMenu.MenuItems[(int)MenuItems.Save].Visible = false ;

				treeView.ContextMenu.MenuItems[(int)MenuItems.Delete].Enabled = false ;
				treeView.ContextMenu.MenuItems[(int)MenuItems.Copy].Enabled = false ;
				treeView.ContextMenu.MenuItems[(int)MenuItems.Paste].Enabled = false ;
			}
			else
			{
				bool	saveable	=	(node.Object is ChannelCollection) &&
										(node.Parent == null) &&
										(SaveCollection != null) ;

				treeView.ContextMenu.MenuItems[(int)MenuItems.SaveBar].Visible = saveable ;
				treeView.ContextMenu.MenuItems[(int)MenuItems.Save].Visible = saveable ;

				treeView.ContextMenu.MenuItems[(int)MenuItems.Delete].Enabled = (node.Parent != null) ;
				treeView.ContextMenu.MenuItems[(int)MenuItems.Copy].Enabled = (node.Object.GetType().IsSerializable) ;
				treeView.ContextMenu.MenuItems[(int)MenuItems.Paste].Enabled = (copyStream != null) ;
			}
		}

		private void OnMenuSave(object sender, EventArgs e)
		{
			ExtNode			node		= treeView.SelectedNode as ExtNode ;

			//only fire the save event if anyone is listening, and
			//we are looking at a root collection
			if ((node != null) && (node.Parent == null) &&
				(node.Object is ChannelCollection) && (SaveCollection != null))
			{
				SaveCollection (node.Object as ChannelCollection) ;
			}
		}

		private void OnMenuAdd(object sender, EventArgs e)
		{
			AddMenuItem		item		= sender as AddMenuItem ;
			ExtNode			node		= treeView.SelectedNode as ExtNode ;

			if (node != null && item != null && item.AddType != null)
			{
				ChannelCollection	coll	= null ;

				if (node.Object is ChannelCollection)
				{
					coll = node.Object as ChannelCollection ;
				}
				else if (node.Parent != null)
				{
					node = (ExtNode)node.Parent ;
					coll = node.Object as ChannelCollection ;
				}

				if (coll != null)
				{
					try
					{
						KineticaRT.Object	new_obj = Activator.CreateInstance(item.AddType) as KineticaRT.Object ;

						AddObject(coll, node, new_obj) ;
					}
					catch (TargetInvocationException exc)
					{
						MessageBox.Show(exc.InnerException.Message, "Add object failed") ;
					}
					catch (Exception exc)
					{
						MessageBox.Show(exc.Message, "Add object failed") ;
					}
				}
			}
		}

        private void OnMenuCut(object sender, EventArgs e)
        {
            OnMenuCopy(sender, e);
            OnMenuDelete(sender, e);
        }

		private void OnMenuCopy(object sender, EventArgs e)
		{
			ExtNode		node		= treeView.SelectedNode as ExtNode ;
			Type		type		= node.Object.GetType() ;

			if (type.IsSerializable)
			{
				SoapFormatter		formatter	= new SoapFormatter();

				//close old memory stream and open a new one
				if (copyStream != null)			copyStream.Close() ;
				copyStream = new System.IO.MemoryStream() ;

				try
				{
					formatter.Serialize(copyStream, node.Object);
				}
				catch (Exception exc)
				{
					MessageBox.Show(exc.Message, "Copy Failed") ;
				}
			}
			else
			{
				MessageBox.Show(node.Text + " is node Serializable - cannot copy it", "Warning") ;
			}
		}

		private void OnMenuPaste(object sender, EventArgs e)
		{
			if (copyStream != null)
			{
				ExtNode				node	= treeView.SelectedNode as ExtNode ;
				ChannelCollection	coll	= null ;

				if (node.Object is ChannelCollection)
				{
					coll = node.Object as ChannelCollection ;
				}
				else if (node.Parent != null)
				{
					node = (ExtNode)node.Parent ;
					coll = node.Object as ChannelCollection ;
				}

				if (coll != null)
				{
					SoapFormatter		formatter	= new SoapFormatter();
					KineticaRT.Object	new_obj ;

					//find the start of the stream
					copyStream.Seek(0, System.IO.SeekOrigin.Begin) ;

					try
					{
						new_obj = formatter.Deserialize(copyStream) as KineticaRT.Object;

						AddObject(coll, node, new_obj) ;
					}
					catch (Exception exc)
					{
						MessageBox.Show(exc.Message, "Paste Failed") ;
					}
				}
				//else cannot paste - not at or in a valid collection
			}
		}

		private void OnMenuDelete(object sender, EventArgs e)
		{
			ExtNode		node		= treeView.SelectedNode as ExtNode ;

			if (node.Parent != null && ((ExtNode)node.Parent).IsCollection)
			{
				//we only understand ChannelCollections
				ChannelCollection	parent_coll	= ((ExtNode)node.Parent).Object as ChannelCollection ;

				if (parent_coll != null)
				{
					//remove from collection...
					parent_coll.Remove(node.Object as Object) ;

					//...and from tree
					node.Remove() ;
				}
			}
		}

		private void OnMenuTopmost(object sender, EventArgs e)
		{
			MenuItem		item	= (MenuItem)sender ;
			
			if (item.Checked)
			{
				ParentForm.TopMost = false ;
				item.Checked = false ;
			}
			else
			{
				ParentForm.TopMost = true ;
				item.Checked = true ;
			}
		}
		#endregion (menu events)

		#region Private methods
		private void AddObject(ChannelCollection Collection, ExtNode Node, KineticaRT.Object NewObject)
		{
			if (NewObject != null)
			{
				ExtNode		new_node ;
				string		base_name		= NewObject.Name ;
				int			index			= 1 ;
				string		name ;

				//generate unique name
				if (base_name == null || base_name == "")
				{
					//no basic name, so use the type
					base_name = NewObject.GetType().Name ;
				}
				else
				{
					//name may have an index on it already - trim that off
					base_name = base_name.TrimEnd(new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'}) ;
				}
				name = base_name ;

				while (Collection.FindObject(name) != null)
				{
					name = base_name + index ;
					index++ ;
				}

				//now try and add it (to the collection and the tree)
				try
				{
					Collection.Add(name, NewObject) ;

					new_node = new ExtNode(name, NewObject) ;
					Node.Nodes.Add(new_node) ;

					if (Node.IsExpanded)		new_node.Connect() ;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Add object failed") ;
				}
			}
		}
		#endregion (private methods)

		#region CLASS: AddMenuItem
		private class AddMenuItem : MenuItem
		{
			private	Type		addType		= null ;

			public AddMenuItem(string Text, EventHandler onClick, string TypeString) : base(Text, onClick)
			{
				addType = Type.GetType(TypeString) ;

				this.Enabled = (addType != null) ;
			}

			public Type AddType
			{
				get
				{
					return addType ;
				}
			}
		}
		#endregion (addmenuitem class)
	}
}


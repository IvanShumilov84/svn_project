using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace KineticaRT.Servers.Logging.Design
{
	/// <summary>
	/// Editor control for a <paramref name="Item"/>.
	/// </summary>
    [ChangeRecord("1.2.0.0", "10 03 2005", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2005-2006 KineticaRT Ltd.")]

    public class ItemEditorControl : System.Windows.Forms.UserControl
	{
		Logger logger ;
		private System.Windows.Forms.TreeView treeView;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initialises an instance of the class.
		/// </summary>
		public ItemEditorControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// treeView
			// 
			this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView.ImageIndex = -1;
			this.treeView.Location = new System.Drawing.Point(0, 0);
			this.treeView.Name = "treeView";
			this.treeView.SelectedImageIndex = -1;
			this.treeView.Size = new System.Drawing.Size(296, 192);
			this.treeView.TabIndex = 1;
			this.treeView.DoubleClick += new System.EventHandler(this.ItemEditorControl_DoubleClick);
			// 
			// ItemEditorControl
			// 
			this.Controls.Add(this.treeView);
			this.Name = "ItemEditorControl";
			this.Size = new System.Drawing.Size(296, 192);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Sets the <paramref name="Item"/> to be edited.
		/// </summary>
		public Logger Item
		{
			get 
			{
				return logger ;
			}

			set
			{
				logger = value as Logger ;
				if (logger != null)
				{
//					Group group = logger.SearchObject(logger.Group) as Group  ;
//					if (group != null)
//					{
//						Client client = group.SearchObject(group.Client) as Client ;
//						if (client != null)
//						{
//							string browseinfo = client.BrowseInfo ;
//							treeView.Nodes.Clear () ;
//
//							System.Xml.XmlDocument doc = new System.Xml.XmlDocument() ;
//
//							try
//							{
//								doc.LoadXml(browseinfo) ;
//								System.Xml.XmlNode list ;
//								
//								list = doc.SelectSingleNode ("Error") ;
//								if (list != null)
//								{
//									System.Xml.XmlElement elem = list as System.Xml.XmlElement ;
//									if (elem != null)
//									{
//										System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Servers.OpcClient.Strings", System.Reflection.Assembly.GetExecutingAssembly());
//										System.Windows.Forms.MessageBox.Show (elem.GetAttribute("Text"), rm.GetString("Browser Error"), MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly) ;
//									}
//								}
//
//
//								list = doc.SelectSingleNode ("List") ;
//								if (list != null) add_tree_items (list, treeView.Nodes) ;
//							}
//							catch (System.Xml.XmlException)
//							{
//							}
//						}
//					}
				}
			}
		}

		private void add_tree_items (System.Xml.XmlNode root, TreeNodeCollection rootNode)
		{
			// Branches
			System.Xml.XmlNodeList xml_items = root.SelectNodes("Branch") ;

			foreach (System.Xml.XmlNode xml_item in xml_items)
			{
				string name = xml_item.Attributes.GetNamedItem("Name").Value ;
				//string itemid = xml_item.Attributes.GetNamedItem("ItemID").Value ;
				TreeNode node = new TreeNode(name);
				//node.Tag = itemid ;
				node.Tag = name ;
				rootNode.Add (node) ;
				add_tree_items (xml_item, node.Nodes) ;

			}

			// Leaves.

			xml_items = root.SelectNodes("Leaf") ;

			foreach (System.Xml.XmlNode xml_item in xml_items)
			{
				string name = xml_item.Attributes.GetNamedItem("Name").Value ;
				string itemid = xml_item.Attributes.GetNamedItem("ItemID").Value ;
				TreeNode node = new TreeNode(name);
				node.Tag = itemid ;
				rootNode.Add (node) ;
			}
		}

		private void set_value ()
		{
//			logger.ItemID=treeView.SelectedNode.Tag as string  ;
//			logger.Description = treeView.SelectedNode.Text as string  ;
//			logger.ToolTip = logger.ItemID  ;
			//string description = treeView.SelectedNode.Text as string  ;
			//if (description.LastIndexOf(".") != -1)
			//{
			//	item.Description = description.Substring(description.LastIndexOf(".")+1) ;
			//}
			//else 
			//{
			//	item.Description = description ;
			//}
			//item.ToolTip = item.ItemID  ;
		}

		private void ItemEditorControl_DoubleClick(object sender, System.EventArgs e)
		{
			set_value () ;
			OnDone () ;
		}

		/// <summary>
		/// 
		/// </summary>
		public delegate void DoneEventHandler (object sender, EventArgs e);
		/// <summary>
		/// 
		/// </summary>
		public event DoneEventHandler Done;   
		/// <summary>
		/// 
		/// </summary>
		protected void OnDone(){if (Done != null) Done(this, EventArgs.Empty);}
	}

	/// <summary>
	/// UITypeEditor for a <paramref name="Item"/> .
	/// </summary>
    [ChangeRecord("1.2.0.0", "10 03 2005", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2005-2006 KineticaRT Ltd.")]

    public class ItemEditor : System.Drawing.Design.UITypeEditor 
	{
		private IWindowsFormsEditorService edSvc ;

		/// <summary>
		/// Edits the specified <paramref name="Item"/>.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			if (context == null) throw new System.ArgumentNullException ("context") ;
			//if (context != null && context.Instance != null && provider != null) 
			//Expression expression = context.Instance as Expression ;
			Logger logger = context.Instance as Logger ;
			if (logger != null && provider != null) 
			{
				edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (edSvc != null) 
				{
					//License license = null ;
					//bool designMode = LicenseManager.IsValid (typeof(KineticaRT.Logic.Expressions.Expression), this, out license) && (license.LicenseKey == "Design");
					//if (license != null) {license.Dispose();license = null;}
					//if (designMode)
					//{
					Design.ItemEditorControl	editor_control = new ItemEditorControl ();
					
					editor_control.Item = logger ; 
					editor_control.Done += new ItemEditorControl.DoneEventHandler (Done) ;
					edSvc.DropDownControl (editor_control) ;
					//}
				}
			}

			return value ;
		}

		/// <summary>
		/// Gets the style of the editor.
		/// </summary>
		/// <param name="context"></param>
		/// <returns>UITypeEditorEditStyle specifying the style.</returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			//if (context != null && context.Instance != null) 
			//{
			return UITypeEditorEditStyle.DropDown;
			//}
			//return base.GetEditStyle(context);
		}

		
		private void Done(object sender, EventArgs e) 
		{
			if (edSvc != null) 
			{
				edSvc.CloseDropDown();
			}
		}
	}
}

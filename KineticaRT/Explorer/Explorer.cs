using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;
using KineticaRT;

namespace KineticaRT.Explorer
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class Explorer : System.Windows.Forms.UserControl,
							IServiceProvider,
							IWindowsFormsEditorService,
							ITypeDescriptorContext
	{
		private System.Windows.Forms.TreeView		ctlTree;
		private object								objRoot;
		private System.Windows.Forms.PropertyGrid	ctlGrid;

		#region ITypeDescriptorContext
//		public IContainer Container 
//		{
//			get 
//			{
//				return components ;
//			}
//		}
		public object Instance 
		{
			get 
			{
				return this ;
			}
		}
		public PropertyDescriptor PropertyDescriptor 
		{
			get 
			{
				return null ;
			}
		}
		public void OnComponentChanged()
		{
		}
		public bool OnComponentChanging()
		{
			return true ;
		}
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Explorer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			ctlTree.AfterSelect += new TreeViewEventHandler(OnTreeAfterSelect) ;
			ctlTree.BeforeExpand += new TreeViewCancelEventHandler(OnTreeBeforeExpand) ;
			ctlTree.AfterCollapse += new TreeViewEventHandler(OnTreeAfterCollapse) ;
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

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ctlTree = new System.Windows.Forms.TreeView();
			this.ctlGrid = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// ctlTree
			// 
			this.ctlTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.ctlTree.ImageIndex = -1;
			this.ctlTree.Name = "ctlTree";
			this.ctlTree.SelectedImageIndex = -1;
			this.ctlTree.Size = new System.Drawing.Size(200, 272);
			this.ctlTree.TabIndex = 1;
			// 
			// ctlGrid
			// 
			this.ctlGrid.CommandsVisibleIfAvailable = true;
			this.ctlGrid.LargeButtons = false;
			this.ctlGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.ctlGrid.Location = new System.Drawing.Point(200, 0);
			this.ctlGrid.Name = "ctlGrid";
			this.ctlGrid.TabIndex = 2;
			this.ctlGrid.Text = "propertyGrid1";
			this.ctlGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.ctlGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.ctlGrid.Visible = false;
			// 
			// Explorer
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.ctlGrid,
																		  this.ctlTree});
			this.Name = "Explorer";
			this.Size = new System.Drawing.Size(448, 272);
			this.Load += new System.EventHandler(this.Explorer_Load);
			this.ResumeLayout(false);

		}
		#endregion

		//got a new node selected: fill in the right hand side
		private void OnTreeAfterSelect(object Sender, TreeViewEventArgs e)
		{
			ExtNode			node		= (ExtNode)e.Node ;
			object			obj			= node.Object ;

			if (this.Controls.Count > 2)
			{
				//remove any leftover editors
				foreach (Control ctl in Controls)
				{
					if (ctl != ctlTree && ctl != ctlGrid)
					{
						//should only ever be one at most.
						//and this will invalidate the enumerator anyway
						Controls.Remove(ctl) ;
						break ;
					}
				}
			}

			//what type should it be?
			if (node.SpecialEditor)
			{
				// Gets the attributes for the component.
				AttributeCollection attrs	= TypeDescriptor.GetAttributes(obj);
				EditorAttribute		ed_attr = (EditorAttribute)attrs[typeof(EditorAttribute)];

				if (ed_attr != null)
				{
					Type				ed_type	= Type.GetType(ed_attr.EditorTypeName) ;
					UITypeEditor		ed		= (UITypeEditor)Activator.CreateInstance(ed_type) ;

					//hide the property grid and show the special editor
					ctlGrid.Visible = false ;
					
					node.Object = ed.EditValue(this, (IServiceProvider) this, obj) ;
					return ;
				}

				//not found a special editor - don't bother to look again
				node.SpecialEditor = false ;
			}

			//not found a special editor, show the property grid
			ctlGrid.Location = new System.Drawing.Point(ctlTree.Width, 0);
			ctlGrid.Size = new System.Drawing.Size(ClientRectangle.Width - ctlTree.Width, ctlTree.Height);
			ctlGrid.Visible = true ;

			//and set its object
			ctlGrid.SelectedObject = obj ;
		}

		//about to expand a node - add the children of its children
		private void OnTreeBeforeExpand(object Sender, TreeViewCancelEventArgs e)
		{
			foreach (ExtNode node in e.Node.Nodes)
			{
				node.AddChildren() ;
			}
		}

		//just collapsed a node - remove the children of its children
		private void OnTreeAfterCollapse(object Sender, TreeViewEventArgs e)
		{
			foreach (ExtNode node in e.Node.Nodes)
			{
				node.RemoveChildren() ;
			}
		}
		
		protected override void OnResize(EventArgs e)
		{
			if (ctlGrid.Visible)
			{
				ctlGrid.Height		= ClientRectangle.Height ;
				ctlGrid.Width		= ClientRectangle.Width - ctlTree.Width ;
			}
			if (Controls.Count>2)
			{
				Controls[2].Height	= ClientRectangle.Height ;
				Controls[2].Width	= ClientRectangle.Width - ctlTree.Width ;
			}
			base.OnResize(e) ;
		}

		public object RootObject
		{
			get
			{
				return objRoot ;
			}
			set
			{
				if (objRoot != value)
				{
					objRoot = value ;

					//throw away old root object...
					while (ctlTree.Nodes.Count > 0)
					{
						ctlTree.Nodes.RemoveAt(0) ;
					}

					//and add new one
					ExtNode			root	= new ExtNode(objRoot.GetType().ToString(), objRoot) ;
					root.AddChildren() ;
					ctlTree.Nodes.Add(root) ;
				}
			}
		}

		/// <summary>
		/// Extension of TreeNode to contain an Object which the node represents
		/// </summary>
		private class ExtNode : System.Windows.Forms.TreeNode
		{
			private object			objObject ;
			private bool			bCollection ;
			private bool			bDictionary ;
			private bool			bSpecialEditor ;

			public ExtNode(string name, object Object)
			{
				objObject = Object ;

				//assume it has a special editor until proved otherwise
				bSpecialEditor = true ;

				try
				{
					ICollection		coll	= (ICollection)objObject ;

					//only get here if this is a collection
					bCollection = true ;
					this.BackColor = Color.PeachPuff ;

					//it is a collection, but is it dictionary type?
					try
					{
						IDictionary		dict	= (IDictionary)objObject ;
						bDictionary = true ;
					}
					catch
					{
						bDictionary = false ;
					}
				}
				catch
				{
					bCollection = false ;
					bDictionary = false ;
					this.BackColor = Color.PaleTurquoise ;
				}

				Text = name ;
			}

			public object Object
			{
				get
				{
					return objObject ;
				}
				set
				{
					objObject = value ;
				}
			}

			public bool IsCollection
			{
				get
				{
					return bCollection ;
				}
			}

			public bool SpecialEditor
			{
				get
				{
					return bSpecialEditor ;
				}
				set
				{
					bSpecialEditor = value ;
				}
			}

			//Adds nodes for any sub-objects / collections held by the object
			public void AddChildren()
			{
				//nothing can be done if got a null object
				if (objObject == null)		return ;

				if (!bCollection)
				{
					//add collections
					System.Reflection.PropertyInfo[]	properties ;
				
					properties = objObject.GetType().GetProperties () ;

					foreach (System.Reflection.PropertyInfo property in properties)
					{
						ICollection			coll ;
						object				val ;

						try
						{
							val = property.GetValue(objObject, null) ;
							coll = (ICollection)val ;
						
							//if get here, it means we have successfully found a property with an
							//ICollection interface
							Nodes.Add(new ExtNode(property.Name, val)) ;
						}
						catch
						{
						}
					}
				}
				else
				{
					//add items
					if (bDictionary)
					{
						foreach (DictionaryEntry entry in (IDictionary)objObject)
						{
							Nodes.Add(new ExtNode(entry.Key.ToString(), entry.Value)) ;
						}
					}
					else
					{
						ObjectCollection col = objObject as ObjectCollection ;
						if (col != null)
						{
							foreach (string name in col)
							{
								Nodes.Add(new ExtNode(name, col.GetObject (name))) ;
							}
						}
						else
						{
							foreach (object item in (ICollection)objObject)
							{
								Nodes.Add(new ExtNode(item.ToString(), item)) ;
							}
						}
					}
				}
			}

			//Removes child nodes
			public void RemoveChildren()
			{
				//make sure it is colapsed for next time
				if (IsExpanded)
				{
					Collapse() ;
				}

				while (Nodes.Count > 0)
				{
					Nodes.RemoveAt(0) ;
				}
			}
		}

		#region IServiceProvider
		public new object GetService(Type serviceType)
		{
			if (serviceType == typeof(IWindowsFormsEditorService))
			{
				return (IWindowsFormsEditorService)this ;
			}
			return null ;
		}
		#endregion

		#region IWindowsFormsEditorService
		public void CloseDropDown()
		{
			//nothing needs doing?
		}

		public void DropDownControl(Control control)
		{
			this.Controls.Add(control) ;
			control.Location = new System.Drawing.Point(ctlTree.Width, 0);
			OnResize (EventArgs.Empty) ;
		}

		public DialogResult ShowDialog(Form form)
		{
			return form.ShowDialog((IWin32Window)this) ;
		}
		#endregion

		private void Explorer_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}

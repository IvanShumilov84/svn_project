#if !VS2012
    extern alias KineticaRTCompact;
#endif

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
using KineticaRT.Channels;
using System.Linq;


namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Explorer control.
	/// Used for viewing and editing controls and components at runtime
	/// </summary>
	[ChangeRecord ("1.0", "17 02 2003", "S. Child", "Major Changes")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[ToolboxBitmap(typeof(Explorer))]
	public class Explorer : System.Windows.Forms.UserControl,
							IServiceProvider,
							IWindowsFormsEditorService,
							ITypeDescriptorContext
	{
		#region Variables

        //private ArrayList										rootObjects;
        private KineticaRT.Channels.IChannel valueChangedChannel;
        private KineticaRT.Channels.ValueChangedEventHandler valueChangedHandler ;
#if !VS2012
        private KineticaRTCompact.KineticaRT.Compact.Channels.IChannel valueChangedCompactChannel;
        private KineticaRTCompact.KineticaRT.Compact.Channels.ValueChangedEventHandler valueChangedCompactHandler;
#endif

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel typeStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel valueStatusBarPanel;
		private System.Windows.Forms.StatusBarPanel descriptionStatusBarPanel;
        private System.ComponentModel.IContainer components;
        private Splitter splitter;
        protected TreeView treeView;
        private Panel panel2;
        private TextBox txtSearch;
        private Splitter splitter1;
        private PropertyGrid propertyGrid;
        private Button btnPrevious;
        private Button btnNext;
        private CheckBox chkName;
        private CheckBox chkSource;
        private CheckBox chkDestination;
        private CheckBox chkExpression;
		private SetStatusBarPanelDelegate setStatusBarPanel ;

		#region Initialisation and disposal
		/// <summary>
		/// Initialises an new instance of the Explorer class
		/// </summary>
		public Explorer()
		{
			setStatusBarPanel = new SetStatusBarPanelDelegate(SetStatusBarPanel) ;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			treeView.AfterSelect += new TreeViewEventHandler(OnTreeAfterSelect) ;
			treeView.BeforeExpand += new TreeViewCancelEventHandler(OnTreeBeforeExpand) ;
			treeView.AfterCollapse += new TreeViewEventHandler(OnTreeAfterCollapse) ;
			treeView.MouseDown += new MouseEventHandler(OnTreeMouseDown) ;
			treeView.ItemDrag += new ItemDragEventHandler(OnTreeItemDrag) ;
            treeView.DragEnter +=new DragEventHandler(treeView_DragEnter);
            treeView.DragOver +=new DragEventHandler(treeView_DragOver);
            treeView.DragDrop += new DragEventHandler(treeView_DragDrop);



			MenuItem[]			items		= new MenuItem[4] ;

			items[0] = new MenuItem("Show Properties", new EventHandler(OnMenuShowProperties)) ;
			items[0].Checked = true ;
			items[1] = new MenuItem("-") ;
			items[2] = new MenuItem("C&onnect", new EventHandler(OnMenuConnect)) ;
			items[3] = new MenuItem("D&isconnect", new EventHandler(OnMenuDisconnect)) ;
			
			treeView.ContextMenu = new ContextMenu(items) ;
			treeView.ContextMenu.Popup += new EventHandler(OnMenuPopup) ;

		}

        void treeView_DragDrop(object sender, DragEventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void treeView_DragOver(object sender, DragEventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void treeView_DragEnter(object sender, DragEventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (valueChangedHandler != null)
			{
				valueChangedChannel.ValueChanged -= valueChangedHandler ;
				valueChangedHandler = null ;
				valueChangedChannel = null ;
			}

			// This added as tempory fix - not sure why or if it always works or what it is for!
			foreach (TreeNode node in treeView.Nodes)
			{
				ExtNode extnode = node as ExtNode ;
				if (extnode != null)
				{
					extnode.Disconnect () ;

					TreeNode node2 = node.FirstNode ;
					while (node2 != null)
					{
						ExtNode extnode2 = node2 as ExtNode ;
						extnode2.Disconnect () ;
						node2 = node2.NextNode ;
					}
				}
			}

			if( disposing )
			{
				if( components != null )	components.Dispose();
			}

			try 
			{
				base.Dispose( disposing );
			}
			catch
			{
				System.Diagnostics.Debug.WriteLine ("Explorer: Dispose failed") ;
			}
		}

		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.typeStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.valueStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.descriptionStatusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.splitter = new System.Windows.Forms.Splitter();
            this.treeView = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkName = new System.Windows.Forms.CheckBox();
            this.chkSource = new System.Windows.Forms.CheckBox();
            this.chkDestination = new System.Windows.Forms.CheckBox();
            this.chkExpression = new System.Windows.Forms.CheckBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.typeStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueStatusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionStatusBarPanel)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 200;
            this.toolTip.ReshowDelay = 100;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 625);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.typeStatusBarPanel,
            this.valueStatusBarPanel,
            this.descriptionStatusBarPanel});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(865, 24);
            this.statusBar1.SizingGrip = false;
            this.statusBar1.TabIndex = 4;
            this.statusBar1.Text = "statusBar1";
            // 
            // typeStatusBarPanel
            // 
            this.typeStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.typeStatusBarPanel.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
            this.typeStatusBarPanel.MinWidth = 239;
            this.typeStatusBarPanel.Name = "typeStatusBarPanel";
            this.typeStatusBarPanel.Text = "Type";
            this.typeStatusBarPanel.Width = 239;
            // 
            // valueStatusBarPanel
            // 
            this.valueStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.valueStatusBarPanel.Name = "valueStatusBarPanel";
            this.valueStatusBarPanel.Text = "Value";
            this.valueStatusBarPanel.Width = 616;
            // 
            // descriptionStatusBarPanel
            // 
            this.descriptionStatusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
            this.descriptionStatusBarPanel.Name = "descriptionStatusBarPanel";
            this.descriptionStatusBarPanel.Width = 10;
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(255, 0);
            this.splitter.MinExtra = 50;
            this.splitter.MinSize = 50;
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(4, 625);
            this.splitter.TabIndex = 3;
            this.splitter.TabStop = false;
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(255, 625);
            this.treeView.Sorted = true;
            this.treeView.TabIndex = 1;
            this.treeView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseMoveHandler);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkName);
            this.panel2.Controls.Add(this.chkSource);
            this.panel2.Controls.Add(this.chkDestination);
            this.panel2.Controls.Add(this.chkExpression);
            this.panel2.Controls.Add(this.btnPrevious);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(259, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(606, 29);
            this.panel2.TabIndex = 8;
            // 
            // chkName
            // 
            this.chkName.AutoSize = true;
            this.chkName.Checked = true;
            this.chkName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkName.Location = new System.Drawing.Point(287, 7);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(54, 17);
            this.chkName.TabIndex = 14;
            this.chkName.Text = "Name";
            this.chkName.UseVisualStyleBackColor = true;
            // 
            // chkSource
            // 
            this.chkSource.AutoSize = true;
            this.chkSource.Location = new System.Drawing.Point(360, 7);
            this.chkSource.Name = "chkSource";
            this.chkSource.Size = new System.Drawing.Size(60, 17);
            this.chkSource.TabIndex = 13;
            this.chkSource.Text = "Source";
            this.chkSource.UseVisualStyleBackColor = true;
            // 
            // chkDestination
            // 
            this.chkDestination.AutoSize = true;
            this.chkDestination.Location = new System.Drawing.Point(433, 7);
            this.chkDestination.Name = "chkDestination";
            this.chkDestination.Size = new System.Drawing.Size(79, 17);
            this.chkDestination.TabIndex = 16;
            this.chkDestination.Text = "Destination";
            this.chkDestination.UseVisualStyleBackColor = true;
            // 
            // chkExpression
            // 
            this.chkExpression.AutoSize = true;
            this.chkExpression.Location = new System.Drawing.Point(517, 7);
            this.chkExpression.Name = "chkExpression";
            this.chkExpression.Size = new System.Drawing.Size(77, 17);
            this.chkExpression.TabIndex = 15;
            this.chkExpression.Text = "Expression";
            this.chkExpression.UseVisualStyleBackColor = true;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Enabled = false;
            this.btnPrevious.Location = new System.Drawing.Point(6, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(39, 23);
            this.btnPrevious.TabIndex = 8;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(51, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(39, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(103, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(178, 20);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(259, 29);
            this.splitter1.MinExtra = 50;
            this.splitter1.MinSize = 50;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(606, 5);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid.Location = new System.Drawing.Point(259, 34);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(606, 591);
            this.propertyGrid.TabIndex = 10;
            this.propertyGrid.Visible = false;
            // 
            // Explorer
            // 
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.statusBar1);
            this.Name = "Explorer";
            this.Size = new System.Drawing.Size(865, 649);
            ((System.ComponentModel.ISupportInitialize)(this.typeStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueStatusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.descriptionStatusBarPanel)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region Properties - Unused
//		/// <summary>
//		/// Gets or sets the object that is at the root of the Explorer
//		/// </summary>
//		[Description("Gets or sets the object that is at the root of the Explorer")]
//		public object RootObject
//		{
//			get
//			{
//				return rootObject ;
//			}
//			set
//			{
//				if (rootObject != value)
//				{
//					rootObject = value ;
//
//					//throw away old root object...
//					while (treeView.Nodes.Count > 0)
//					{
//						treeView.Nodes.RemoveAt(0) ;
//					}
//
//					//and add new one
//					ExtNode			root	= new ExtNode(rootObject.GetType().ToString(), rootObject) ;
//					root.AddChildren() ;
//					treeView.Nodes.Add(root) ;
//				}
//			}
//		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds the given object to the root of the Explorer tree
		/// </summary>
		/// <param name="NewRootObject">Object to add</param>
		/// <remarks>The new object will appear named according to its Type</remarks>
		public void AddRootObject(object NewRootObject)
		{
			AddRootObject(NewRootObject.GetType().ToString(), NewRootObject) ;
		}

		/// <summary>
		/// Adds the given object to the root of the Explorer tree
		/// </summary>
		/// <param name="NewRootObject">Object to add</param>
		/// <param name="Name">Name of object as it will appear in the Tree</param>
		public void AddRootObject(string Name, object NewRootObject)
		{
			//search the root nodes
			foreach (ExtNode node in treeView.Nodes)
			{
				if (node.Object.Equals(NewRootObject))
				{
					return ;
				}
			}

			
			
//			if (!rootObjects.Contains(NewRootObject))
//			{
//				rootObjects.Add(NewRootObject) ;

			//and add new one
			ExtNode			root	= new ExtNode(Name, NewRootObject) ;
			root.AddChildren(Name) ;
			treeView.Nodes.Add(root) ;
//			}
		}

		/// <summary>
		/// Removes all objects from the Explorer
		/// </summary>
		public void Clear()
		{
			//throw away old root objects...
			while (treeView.Nodes.Count > 0)
			{
				treeView.Nodes.RemoveAt(0) ;
			}
		}
		/// <summary>
		/// Removes the given object from the root level of the Tree, if it was there
		/// </summary>
		/// <param name="Object">Object to be removed</param>
		public void RemoveObject(object Object)
		{
			//search the root nodes
			foreach (ExtNode node in treeView.Nodes)
			{
				if (node.Object.Equals(Object))
				{
					node.Collapse() ;
					node.Remove() ;
					break ;
				}
			}
		}
		#endregion

		#region Tree Events

		//got a new node selected: fill in the right hand side
		private void OnTreeAfterSelect(object Sender, TreeViewEventArgs e)
		{
			ExtNode			node		= (ExtNode)e.Node ;
			object			obj			= node.Object ;

            if (valueChangedHandler != null)
            {
                valueChangedChannel.ValueChanged -= valueChangedHandler;
                valueChangedHandler = null;
                valueChangedChannel = null;
            }
#if !VS2012
            if (valueChangedCompactHandler != null)
            {
                valueChangedCompactChannel.ValueChanged -= valueChangedCompactHandler;
                valueChangedCompactHandler = null;
                valueChangedCompactChannel = null;
            }
#endif

			if (obj != null)
			{
                lock (this)
                {
                    AttributeCollection attrs = TypeDescriptor.GetAttributes(obj);
                    System.ComponentModel.DescriptionAttribute description_attr = (System.ComponentModel.DescriptionAttribute)attrs[typeof(System.ComponentModel.DescriptionAttribute)];

                    descriptionStatusBarPanel.Text = (description_attr != null) ? description_attr.Description : "";
                    typeStatusBarPanel.Text = obj.GetType().ToString();
                    valueStatusBarPanel.Text = obj.ToString();
                    valueChangedChannel = obj as KineticaRT.Channels.IChannel;
                    if (valueChangedChannel != null)
                    {
                        valueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(channel_ValueChanged);
                        valueChangedChannel.ValueChanged += valueChangedHandler;
                        SetStatusBarPanel(valueStatusBarPanel, valueChangedChannel.ToString());
                    }
                    else
                    {
#if !VS2012
                        valueChangedCompactChannel = obj as KineticaRTCompact.KineticaRT.Compact.Channels.IChannel;
                        if (valueChangedCompactChannel != null)
                        {
                            valueChangedCompactHandler = new KineticaRTCompact.KineticaRT.Compact.Channels.ValueChangedEventHandler(channel_CompactValueChanged);
                            valueChangedCompactChannel.ValueChanged += valueChangedCompactHandler;
                        }
#endif
                    }
                }
			}
			else
			{
				this.typeStatusBarPanel.Text = "" ;
				this.valueStatusBarPanel.Text = "" ;
			}

            //if (this.Controls.Count > 6)//ft
            //{
            //    //remove any leftover editors
            //    foreach (Control ctl in Controls)
            //    {
            //        if (ctl != treeView && ctl != propertyGrid && ctl != splitter && ctl != splitter1  && ctl != txtSearch)
            //        {
            //            //should only ever be one at most.
            //            //and this will invalidate the enumerator anyway
            //            Controls.Remove(ctl) ;
            //            break ;
            //        }
            //    }
            //}

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
					propertyGrid.Visible = false ;
					
					node.Object = ed.EditValue(this, (IServiceProvider) this, obj) ;
					return ;
				}

				//not found a special editor - don't bother to look again
				node.SpecialEditor = false ;
			}

			//not found a special editor, show the property grid
//			propertyGrid.Location = new System.Drawing.Point(splitter.Right, 0);
//			propertyGrid.Size = new System.Drawing.Size(ClientRectangle.Width - treeView.Width, treeView.Height);

			lock (treeView)
			{
				//show property grid iff properties are available
				propertyGrid.Visible = treeView.ContextMenu.MenuItems[0].Checked ;
				propertyGrid.Dock = DockStyle.Fill ;

				//and set its object
				propertyGrid.SelectedObject = obj ;
			}
		}

		//about to expand a node - add the children of its children
		private void OnTreeBeforeExpand(object Sender, TreeViewCancelEventArgs e)
		{
			foreach (ExtNode node in e.Node.Nodes)
			{
				node.Connect() ;
				node.AddChildren() ;
			}
		}

		//just collapsed a node - remove the children of its children
		private void OnTreeAfterCollapse(object Sender, TreeViewEventArgs e)
		{
			foreach (ExtNode node in e.Node.Nodes)
			{
				node.Disconnect() ;
				node.RemoveChildren() ;
			}
		}
		

		//force node selection to happen on mouse down (not mouse up)
		//so that right click menu always applies to the correct object
		private void OnTreeMouseDown(object sender, MouseEventArgs e)
		{
			Point			pt		= new Point(e.X, e.Y) ;
			TreeNode		node	= treeView.TopNode ;

			//only bother checking visible nodes
			while (node != null)
			{
				if (node.Bounds.Contains(pt))
				{
					treeView.SelectedNode = node ;
					break ;
				}
				node = node.NextVisibleNode ;
			}
		}

		private void OnTreeItemDrag(object sender, ItemDragEventArgs e)
		{
			if (e.Item is ExtNode)
			{
				//put the name of the object in the clipboard
				ExtNode			node	= e.Item as ExtNode ;

				treeView.DoDragDrop(node.Path + node.Name, DragDropEffects.Copy) ;
			}
		}
		#endregion

		#region Menu events
		/// <summary>
		/// Menu Popup event handler
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected virtual void OnMenuPopup(object sender, EventArgs e)
		{
			ExtNode		node			= treeView.SelectedNode as ExtNode ;
			bool		can_connect		= (node != null && node.Object is IConnectable) ;

			treeView.ContextMenu.MenuItems[2].Enabled = can_connect ;
			treeView.ContextMenu.MenuItems[3].Enabled = can_connect ;
		}

		/// <summary>
		/// Show Properties has been selected from the menu
		/// </summary>
		/// <param name="sender">Menu item that generated the event</param>
		/// <param name="e">Event data</param>
		protected void OnMenuShowProperties(object sender, EventArgs e)
		{
			MenuItem		item	= (MenuItem)sender ;

			if (item.Checked)
			{
				item.Checked = false ;

				propertyGrid.Visible = false ;
				splitter.Visible = false ;
				//ft if (Controls.Count > 6)		Controls[6].Visible = false ;

				treeView.Dock = DockStyle.Fill ;
			}
			else
			{
				item.Checked = true ;
				
				treeView.Dock = DockStyle.Left ;
				splitter.Visible = true ;

				if (Controls.Count > 6)
				{
					Controls[6].Visible = true ;
				}
				else
				{
					propertyGrid.Visible = true ;
				}
			}
		}
			
		/// <summary>
		/// Connect has been selected from the menu
		/// </summary>
		/// <param name="sender">Menu item that generated the event</param>
		/// <param name="e">Event data</param>
		protected void OnMenuConnect(object sender, EventArgs e)
		{
			ExtNode		node		= treeView.SelectedNode as ExtNode ;

			if (node != null && node.Object is IConnectable)
			{
				((IConnectable)node.Object).Connect() ;
			}
		}

		/// <summary>
		/// Disconnect has been selected from the menu
		/// </summary>
		/// <param name="sender">Menu item that generated the event</param>
		/// <param name="e">Event data</param>
		protected void OnMenuDisconnect(object sender, EventArgs e)
		{
			ExtNode		node		= treeView.SelectedNode as ExtNode ;

			if (node != null && node.Object is IConnectable)
			{
				((IConnectable)node.Object).Disconnect() ;
			}
		}
		#endregion

		#region Other Events
		/// <summary>
		/// Resize Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnResize(EventArgs e)
		{
			if (treeView.ContextMenu != null && treeView.ContextMenu.MenuItems[0].Checked)
			{
//				if (propertyGrid.Visible)
//				{
//					propertyGrid.Height		= ClientRectangle.Height ;
//					propertyGrid.Width		= ClientRectangle.Width - treeView.Width ;
//				}
                //if (Controls.Count > 6)
                //{
                //    Controls[6].Height	= ClientRectangle.Height ;
                //    Controls[6].Width	= ClientRectangle.Width - treeView.Width ;
                //}
			}
			base.OnResize(e) ;
		}

		#endregion

		#region ITypeDescriptorContext Interface
		//		public IContainer Container 
		//		{
		//			get 
		//			{
		//				return components ;
		//			}
		//		}
		/// <summary>
		/// Gets the instance of the object that is connected with this TypeDescriptor request.
		/// Part of the ITypeDescriptorContext interface
		/// </summary>
		public object Instance 
		{
			get 
			{
				return this ;
			}
		}
		/// <summary>
		/// Gets the PropertyDescriptor that describes the given context item.
		/// Part of the ITypeDescriptorContext interface
		/// </summary>
		public PropertyDescriptor PropertyDescriptor 
		{
			get 
			{
				return null ;
			}
		}
		/// <summary>
		/// Raises the ComponentChanged event.
		/// Part of the ITypeDescriptorContext interface
		/// </summary>
		public void OnComponentChanged()
		{
		}
		/// <summary>
		/// Returns a value indicating whether this object can be changed.
		/// Part of the ITypeDescriptorContext interface
		/// </summary>
		/// <returns>true if this object can be changed; otherwise, false</returns>
		public bool OnComponentChanging()
		{
			return true ;
		}
		#endregion

		#region IServiceProvider Interface
		/// <summary>
		/// Gets the service object of the specified type.
		/// Part of the IServiceProvider interface
		/// </summary>
		/// <param name="serviceType">An object that specifies the type of service object to get</param>
		/// <returns>A service object of type serviceType or 'null' or if there is no service object of type serviceType</returns>
		public new object GetService(Type serviceType)
		{
			if (serviceType == typeof(IWindowsFormsEditorService))
			{
				return (IWindowsFormsEditorService)this ;
			}
			return null ;
		}
		#endregion

		#region IWindowsFormsEditorService Interface
		/// <summary>
		/// Closes any previously opened drop down control.
		/// Part of the IWindowsFormsEditorService interface
		/// </summary>
		public void CloseDropDown()
		{
			//nothing needs doing?
		}

		/// <summary>
		/// Opens the specified drop down list control.
		/// Part of the IWindowsFormsEditorService interface
		/// </summary>
		/// <param name="control">The drop down list Control to open</param>
		public void DropDownControl(Control control)
		{
			//only add the editor if properties are being shown
			if (treeView.ContextMenu.MenuItems[0].Checked)
			{
				this.Controls.Add(control) ;
				control.Location = new System.Drawing.Point(treeView.Width, 0);
				OnResize (EventArgs.Empty) ;
			}
		}

		/// <summary>
		/// Shows the specified Form.
		/// Part of the IWindowsFormsEditorService interface
		/// </summary>
		/// <param name="form">The Form to display</param>
		/// <returns>A DialogResult indicating the result code returned by the dialog box</returns>
		public DialogResult ShowDialog(Form form)
		{
			return form.ShowDialog((IWin32Window)this) ;

			//return DialogResult.OK ;
		}
		#endregion

		#region CLASS: Extended Tree Node Class
		/// <summary>
		/// Extension of TreeNode to contain an Object which the node represents
		/// </summary>
		protected class ExtNode : System.Windows.Forms.TreeNode
		{
			private object						objObject ;
			private bool						isCollection ;
			private bool						isDictionary ;
			private bool						isSpecialEditor ;
			private PropertyDescriptor			defaultProperty ;
			private string						objectName ;
			private string						objectPath ;
			private PropertyChangedEventHandler	propertyHandler ;
			//private string						propertyValue ;		//the bit in brackets
			private bool						connected ;			//..to property events

			/// <summary>
			/// Initialises a new instance of the ExtNode class
			/// </summary>
			/// <param name="name">Name of object held by the Node</param>
			/// <param name="Object">Object held by the Node</param>
			public ExtNode(string name, object Object)
			{
				connected = false ;
				objObject = Object ;
				

				//assume it has a special editor until proved otherwise
				isSpecialEditor = true ;

				if (objObject != null && objObject is ICollection)
//					 objObject.GetType().GetInterface("ICollection") != null)	  )
				{
					//this object (or its Type) supports the
					//ICollection interface, so it is a collection
					isCollection = true ;
					//this.BackColor = Color.PeachPuff ;
					this.ForeColor = Color.Blue ;

					//it is a collection, but is it dictionary type?
					isDictionary = (objObject is IDictionary) ;
				}
				else
				{
					isCollection = false ;
					isDictionary = false ;
					//this.BackColor = Color.PaleTurquoise ;
				}

				objectName = name ;
				objectPath = null ;
				defaultProperty = TypeDescriptor.GetDefaultProperty(objObject) ;

				Text = objectName ;
				if (defaultProperty != null)
				{
					SetText () ;

					//create the property changed event handler but don't connect yet
					propertyHandler = new PropertyChangedEventHandler(OnPropertyChanged) ;
				}
			}

			/// <summary>
			/// Initialises a new instance of the ExtNode class
			/// </summary>
			/// <param name="name">Name of object held by the Node</param>
			/// <param name="Object">Object held by the Node</param>
			/// <param name="path">Path to object held by the Node</param>
			public ExtNode(string path, string name, object Object) : this (name, Object)
			{
				objectPath = path ;
			}
			

			#region Properties
			/// <summary>
			/// Gets or sets the Object held by the Node
			/// </summary>
			public object Object
			{
				get
				{
					return objObject ;
				}
				set
				{
					if (objObject != value)
					{
						if (connected)
						{
							//can only be connected if object is IConnectable
							((IConnectable)objObject).PropertyChanged -= propertyHandler ;
						}

						objObject = value ;

						if (connected && objObject is IConnectable)
						{
							((IConnectable)objObject).PropertyChanged += propertyHandler ;
						}
						else
						{
							connected = false ;
						}
					}
				}
			}

			/// <summary>
			/// Gets whether the Object is a collectino (supports the ICollection interface)
			/// </summary>
			public bool IsCollection
			{
				get
				{
					return isCollection ;
				}
			}

			/// <summary>
			/// Gets or sets whether the Object has a special editor
			/// </summary>
			/// <remarks>Objects without special editors use the PropertyGrid</remarks>
			public bool SpecialEditor
			{
				get
				{
					return isSpecialEditor ;
				}
				set
				{
					isSpecialEditor = value ;
				}
			}

			/// <summary>
			/// Gets the name of the Object associated with this Node
			/// </summary>
			public new string Name
			{
				get
				{
					return objectName ;
				}
			}

			/// <summary>
			/// Gets the path of the Object associated with this Node
			/// </summary>
			public string Path
			{
				get
				{
					return objectPath ;
				}
			}
			#endregion

			#region Public Methods
			/// <summary>
			/// Adds nodes for any sub-objects / collections held by the object
			/// </summary>
			public void AddChildren()
			{
				AddChildren ("") ;
			}
			
			/// <summary>
			/// Adds nodes for any sub-objects / collections held by the object
			/// </summary>
			public void AddChildren(string path)
			{
				//nothing can be done if got a null object
				if (objObject == null)		return ;

				if (!isCollection)
				{
					//add collections
					System.Reflection.PropertyInfo[]	properties ;
				
					properties = objObject.GetType().GetProperties () ;

					foreach (System.Reflection.PropertyInfo property in properties)
					{
                        try
                        {
                            object val = property.GetValue(objObject, null);

                            if ((val != null && val is ICollection) ||
                                (property.PropertyType.GetInterface("ICollection") != null))
                            {
                                //this property (or its Type) support the
                                //ICollection interface, so it is a collection
                                Nodes.Add(new ExtNode(property.Name, val));
                            }
                        }
                        catch (System.Reflection.TargetParameterCountException)
                        {
                        }
					}
				}
				else		//collection
				{
					//add items
					if (isDictionary)
					{
						foreach (DictionaryEntry entry in (IDictionary)objObject)
						{
							Nodes.Add(new ExtNode(entry.Key.ToString(), entry.Value)) ;
						}
					}
					else
					{
						ChannelCollection colection = objObject as ChannelCollection ;
                        if (colection != null)
                        {
                            foreach (string name in colection)
                            {
                                Nodes.Add(new ExtNode(path + ".", name, colection.FindObject(name)));
                            }
                        }
                        else
                        {
#if !VS2012
#if Compact
                            KineticaRTCompact.KineticaRT.Compact.ChannelCollection compact_colection = objObject as KineticaRTCompact.KineticaRT.Compact.ChannelCollection;
                            if (compact_colection != null)
                            {
                                foreach (string name in compact_colection)
                                {
                                    Nodes.Add(new ExtNode(path + ".", name, compact_colection.FindObject(name)));
                                }
                            }
                            else
#endif
#endif
                            {
                                foreach (object item in (ICollection)objObject)
                                {
                                    Nodes.Add(new ExtNode(item.ToString(), item));
                                }
                            }
                        }
					}
				}
			}

			/// <summary>
			/// Removes child nodes from the tree
			/// </summary>
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

			/// <summary>
			/// Connects the PropertyChanged watch
			/// </summary>
			public void Connect()
			{
				if (defaultProperty != null && objObject is IConnectable)
				{
					((IConnectable)objObject).PropertyChanged += propertyHandler ;
					connected = true ;
//
//					//update the text in case we missed any updates
//					//whilst we were hidden
//					SetText(false) ;
				}
			}

			/// <summary>
			/// Disconnects the PropertyChanged watch
			/// </summary>
			public void Disconnect()
			{
				if (defaultProperty != null && objObject is KineticaRT.IConnectable)
				{
					connected = false ;
					((IConnectable)objObject).PropertyChanged -= propertyHandler ;
				}
			}
			#endregion

			#region Other methods
			private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
			{
				if (e.PropertyName == "Name")
				{
					string		new_name ;

					if (objObject is KineticaRT.Object)
					{
						new_name = ((KineticaRT.Object)objObject).Name ;
					}
					else
					{
						//find the name the hard way
						new_name = objObject.GetType().GetProperty("Name").GetValue(objObject, null).ToString() ;
					}

					//if this is an object in a ChannelCollection, 
					//tell it about the change of name
					if (this.Parent != null && ((ExtNode)this.Parent).Object is ChannelCollection)
					{
						((ChannelCollection)((ExtNode)this.Parent).Object).Rename(objectName, new_name) ;
					}

					objectName = new_name ;

					SetText () ;
				}
//				else
//				{
//					//update every property change, not just the default property,
//					//as other properties may well affect the format of the value
//					SetText(false) ;
//				}
			}

			//re-labels the node if the value has changed
			private void SetText()
			{
//				string			new_value		= objObject.ToString() ;
//
//				if (ForceUpdate || new_value != propertyValue)
//				{
//					propertyValue = new_value ;
//					Text = objectName + " (" + propertyValue + ")" ;
//				}

//				if (ForceUpdate)
//				{
				Text = objectName ;
//				}
			}
			#endregion
		}
		#endregion

		private TreeNode tipNode = null;
  
		private void MouseMoveHandler (object sender, MouseEventArgs e)
		{
			TreeNode node = treeView.GetNodeAt(e.X,e.Y);
			ExtNode extnode = node as ExtNode ;

            if ((node != null) && (node != tipNode) && (extnode.Object != null))
			{
				// moved to a new node so show the tip

                string new_tip = extnode.Object.GetType().ToString() + " (" + extnode.Object.ToString() + ")";
                if (extnode.Object is ISource)//ft
                {
                    ISource ctrl = extnode.Object as ISource; //ft
                    if (ctrl != null)
                    {
                        new_tip += "\n        Source: " + ctrl.Source ;
                    }
                }
                if (extnode.Object is IDestination)//ft
                {
                    IDestination ctrl = extnode.Object as IDestination; //ft
                    if (ctrl != null)
                    {
                        new_tip += "\nDestination: " + ctrl.Destination;
                    }
                }
                if (extnode.Object is KineticaRT.Channels.IExpressionChannel)//ft
                {
                    KineticaRT.Channels.IExpressionChannel ctrl = extnode.Object as KineticaRT.Channels.IExpressionChannel; //ft
                    if (ctrl != null)
                    {
                        new_tip += "\n Expression: " + ctrl.Text;
                    }
                }
                //if (extnode.Object is IInputEnabledSource)//ft
                //{
                //    IInputEnabledSource ctrl = extnode.Object as IInputEnabledSource; //ft
                //    if (ctrl != null)
                //        new_tip += "\n    Enabled: " + ctrl.InputEnabledSource;
                //}
                //if (extnode.Object is IResetStateSource)//ft
                //{
                //    IResetStateSource ctrl = extnode.Object as IResetStateSource; //ft
                //    if (ctrl != null)
                //        new_tip += "\n      Reset: " + ctrl.ButtonResetStateSource;
                //}

				//toolTip.SetToolTip( treeView ,new_tip);//ft
                //IWin32Window win = this;
                //toolTip.Show(new_tip, win, MousePosition);
                //toolTip.Show(new_tip, treeView, treeView.Width, /* treeView.Height + */ MousePosition.Y+30); 
                Point mousePos =  treeView.PointToClient(System.Windows.Forms.Control.MousePosition);
                toolTip.Show(new_tip, treeView, treeView.Width, mousePos.Y); 
				tipNode = node;
			}
			else
			{
				// not pointing at a node so clear the tip
				//toolTip.SetToolTip(treeView,"");
				//tipNode = null;
			}
		}
		delegate void SetStatusBarPanelDelegate (StatusBarPanel panel, string text) ;

		private void SetStatusBarPanel (StatusBarPanel panel, string text) 
		{
			if (this.InvokeRequired) 
			{
				this.BeginInvoke (setStatusBarPanel, new object[] {panel, text});
			}
			else 
			{
				panel.Text = text ;
			}
		}

        private void channel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            lock (this)
            {
                SetStatusBarPanel(valueStatusBarPanel, sender.ToString());
            }
        }
#if !VS2012
        private void channel_CompactValueChanged(KineticaRTCompact.KineticaRT.Compact.Channels.IChannel sender, KineticaRTCompact.KineticaRT.Compact.Values.Value newVal)
        {
            lock (this)
            {
                SetStatusBarPanel(valueStatusBarPanel, sender.ToString());
            }
        }
#endif
        private TreeNode nodeFound;
        bool found = false;
        public void FindNode(TreeNodeCollection nodeCollection, string TextToFind)
        {
            //found = false;
            foreach (TreeNode node in nodeCollection)
            {
                if (findNext == true && nodeFound != null)
                {
                    if (node != nodeFound)
                    {
                        FindNode(node.Nodes, TextToFind);
                    }
                    else
                    {
                        findNext = false; //reset flag
                        found = false;
                    }
                }
                else
                {
                    if (found)
                        continue;
                    bool textFound = false;
                    if (chkName.Checked == true)
                    {
                        if (node.Text.Contains(TextToFind))
                        {                           
                            textFound = true;
                        }
                    }
                    if (chkSource.Checked == true)
                    {
                        ExtNode extnode = node as ExtNode;

                        if ((node != null) && (node != tipNode) && (extnode.Object != null))
                        {
                            if (extnode.Object is ISource)//ft
                            {
                                ISource channel = extnode.Object as ISource;
                                if (channel.Source.Contains(TextToFind))
                                    textFound = true;
                            }
                        }
                    }
                    if (chkDestination.Checked == true)
                    {
                        ExtNode extnode = node as ExtNode;

                        if ((node != null) && (node != tipNode) && (extnode.Object != null))
                        {
                            if (extnode.Object is IDestination)//ft
                            {
                                IDestination channel = extnode.Object as IDestination;
                                if (channel.Destination.Contains(TextToFind))
                                    textFound = true;
                            }
                        }
                    }
                    if (chkExpression.Checked == true)
                    {
                        ExtNode extnode = node as ExtNode;

                        if ((node != null) && (node != tipNode) && (extnode.Object != null))
                        {
                            if (extnode.Object is IExpressionChannel)//ft
                            {
                                IExpressionChannel channel = extnode.Object as KineticaRT.Channels.IExpressionChannel;
                                if (channel.Text.Contains(TextToFind))
                                    textFound = true;
                            }
                        }
                    }

                    if ( textFound == true)
                    {
                        node.BackColor = Color.Yellow;
                        treeView.SelectedNode = node;
                        
                        TreeNode parentNode = node.Parent;
                        while (parentNode != null)
                        {
                            parentNode.Expand();
                            parentNode = parentNode.Parent;
                        }
                        found = true;
                        nodeFound = node;
                        break;
                    }
                    else
                    {
                        node.BackColor = Color.White;
                    }
                    FindNode(node.Nodes, TextToFind);
                }
            }
        }
        //private bool findPrevious = false;
        //private TreeNode previousNodeFound;
        //public void FindNodeReverse(TreeNodeCollection nodeCollection, string TextToFind)
        //{
        //    //foreach (TreeNode node in nodeCollection)
        //    foreach (TreeNode node in nodeCollection)
        //    {
        //        if (findPrevious == true && nodeFound != null)
        //        {
        //            if (node != nodeFound)
        //            {
        //                FindNodeReverse(node.Nodes, TextToFind);
        //            }
        //            else
        //            {
        //                findPrevious = false; //reset flag
        //                found = false;
        //            }
        //        }
        //        else
        //        {
        //            if (found)
        //                continue;
        //            if (node.Text.Contains(TextToFind))
        //            {
        //                treeView.SelectedNode = node;
        //                treeView.Focus();
        //                TreeNode parentNode = node.Parent;
        //                while (parentNode != null)
        //                {
        //                    parentNode.Expand();
        //                    parentNode = parentNode.Parent;
        //                }
        //                found = true;
        //                nodeFound = node;
        //                break;
        //            }
        //            FindNodeReverse(node.Nodes, TextToFind);
        //        }
        //    }
        //}

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Enter)
            {
                
            }
            nodeFound = null;
            FindNode(treeView.Nodes, txtSearch.Text);
            txtSearch.Focus();
            if (found == false)
                treeView.SelectedNode = null;
            found = false;
        }
        private bool findNext = false;
        private void btnNext_Click(object sender, EventArgs e)
        {
            findNext = true;
            FindNode(treeView.Nodes, txtSearch.Text);
            if (found == false)
            {
                MessageBox.Show("Item not found");
            }
            found = false;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //findPrevious = true;
            //FindNodeReverse(treeView.Nodes, txtSearch.Text);
        }
    }
}

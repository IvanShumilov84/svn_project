using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics ;
using System.ComponentModel.Design.Serialization;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Xml;
using System.IO;

namespace KineticaRT.Controls
{
	#region Declarations (enums, Copy base class)
	internal enum Corners
	{
		NorthWest	= 0,
		North		= 1,
		NorthEast	= 2,
		East		= 3,
		SouthEast	= 4,
		South		= 5,
		SouthWest	= 6,
		West		= 7,

		Invalid		= -1,
	}

	internal enum FrameMenu
	{
		ExitEditMode	= 0,
		ExitBar			= 1,
		Save			= 2,
		SaveBar			= 3,
		Undo			= 4,
		UndoBar			= 5,
		Add				= 6,
		AddComponent	= 7,
		AddBar			= 8,
		SelectAll		= 9,
		SelectBar		= 10,
		Align			= 11,
		MakeSameSize	= 12,
		Stack			= 13,
		Layout			= 14,
		FormatBar		= 15,
		Cut				= 16,
		Copy			= 17,
		Paste			= 18,
		Delete			= 19,
		DeleteBar		= 20,
		AdvProperties	= 21,
		Properties		= 22,
		Rename			= 23,
        GridEnable      = 24,
        DisconnectControl = 25, //ft
        ConnectControl  = 26, //ft
        Count
	}

	internal enum UndoTypes
	{
		Add		= 0,
		Delete	= 1,
		Move	= 2,
	}

	//same order as menu is set up
	internal enum Alignments
	{
		Left		= 0,
		Right		= 1,
		Top			= 2,
		Bottom		= 3,
		HCentre		= 4,
		VCentre		= 5,
	}

	//same order as menu is set up
	internal enum Sizings
	{
		Width		= 0,
		Height		= 1,
		Both		= 2,
	}

	//same order as menu is set up
	internal enum GridFormats
	{
		Snap		= 0,
		Size		= 1,
		Align		= 2,
	}

	/// <summary>
	/// Base Class used by Frame editor for copying and pasting controls
	/// </summary>
	public class ControlCopyBase : System.Object
	{
		/// <summary>
		/// Adds copies of controls to the specified panel
		/// </summary>
		/// <param name="Panel">Panel to which controls are added</param>
		public virtual void AddCopies(KineticaRT.Controls.PanelBase Panel) 
		{
		}

		/// <summary>
		/// Ensures the given control has a name that is unique within the given Panel's
		/// Controls collection
		/// </summary>
		/// <param name="Ctrl">Control to rename</param>
		/// <param name="Panel">Panel in which tocheck the name</param>
		protected void SetUniqueName(System.Windows.Forms.Control Ctrl, KineticaRT.Controls.PanelBase Panel)
		{
			Ctrl.Name = Panel.GetUniqueName(Ctrl.Name) ;
			//Debug.WriteLine(Ctrl.Name, "renamed") ;
		}
	}
	#endregion

	/// <summary>
	/// Form used as the run-time editor for a KineticaRT Frame
	/// </summary>
	public class EditForm : System.Windows.Forms.Form
	{
		#region CLASS: UndoInfo class
		private class UndoInfo : System.Object
		{
			public	UndoTypes			type ;
			public	ControlEditInfo []	cei ;
			public	Size []				sizes ;
			public	Point []			locations ;

			public UndoInfo(UndoTypes Type, int CtrlCount)
			{
				type = Type ;

				cei		= new ControlEditInfo[CtrlCount] ;
				if (type == UndoTypes.Move)
				{
					sizes		= new Size[CtrlCount] ;
					locations	= new Point[CtrlCount] ;
				}
				else
				{
					sizes = null ;
					locations = null ;
				}
			}
		}
		#endregion

		#region CLASS: ControlEditInfo class
		private class ControlEditInfo : System.Object
		{
			public	System.Windows.Forms.Control	ctrl ;
			public	bool							selected ;
			public	Rectangle []					handles ;

			private	const int		SIZE		= 6 ;		//handle size
			
			//Ctrl is linked control this is keeping info for
			//Selected is whether the control is currently selected
			public ControlEditInfo(System.Windows.Forms.Control	Ctrl, bool Selected)
			{
				ctrl = Ctrl ;
				selected = Selected ;

				handles = new Rectangle[8] ;
				for (int index = 0; index < 8; index++)
				{
					handles[index] = new Rectangle(0, 0, SIZE, SIZE) ;
				}

				//don't bother calculating the handles until they are needed
			}
            
			//X and Y offsets are panel relative to frame
			public void RecalculateHandles(int XOffset, int YOffset)
			{
				Rectangle	rect		= ctrl.ClientRectangle ;

				//offset rect to make handle calculation simpler
				//note that handles are drawn relative to Frame, and controls are
				//positioned relative to Panel
				rect.X = XOffset + ctrl.Location.X - SIZE ;
				rect.Y = YOffset + ctrl.Location.Y - SIZE ;
				rect.Width += SIZE ;
				rect.Height += SIZE ;

				//NB handles count clockwise from top left
				handles[0].X = handles[6].X = handles[7].X	= rect.X ;
				handles[1].X = handles[5].X					= rect.X + rect.Width / 2 ;
				handles[2].X = handles[3].X = handles[4].X	= rect.X + rect.Width ;

				handles[0].Y = handles[1].Y = handles[2].Y	= rect.Y ;
				handles[3].Y = handles[7].Y					= rect.Y + rect.Height / 2 ;
				handles[4].Y = handles[5].Y = handles[6].Y	= rect.Y + rect.Height ;
			}

			public Corners SelectedHandleContains(int X, int Y)
			{
				if (selected)
				{
					for (int index = 0; index < 8; index++)
					{
						if (handles[index].Contains(X, Y))
						{
							return (Corners)index ;
						}
					}
				}

				return Corners.Invalid ;
			}
		}
		#endregion

		#region Variables and declarations
//#if CompactEnabled
//        private static bool compactEnabled = true;
//#else
//        private static bool compactEnabled = false;
//#endif
        
        const int		MAX_UNDOS				= 10 ;
		//private System.ComponentModel.IContainer components;

		private Cursor[]	Directions	= new Cursor[8]	{	Cursors.SizeNWSE,
															Cursors.SizeNS,
															Cursors.SizeNESW,
															Cursors.SizeWE,
															Cursors.SizeNWSE,
															Cursors.SizeNS,
															Cursors.SizeNESW,
															Cursors.SizeWE		} ;
		private bool				menuShowing	;
		private ContextMenu			menu ;
		private bool				codeDelete ;
		private bool				codeAdd ;
		private bool				moving ;
		private Point				click ;					//in EditForm client coords
		private FrameBase			frameBase ;
		private ControlEditInfo		currentSelection ;
		private Corners				currentCorner ;
		private ControlEditInfo		dragObject ;			//object clicked on
		private Point				dragStartPoint ;		//mouse start pos (move or resize)
		private Point				dragReferencePoint ;	//dragObject start location
		private Size				dragReferenceSize ;		//dragObject start size
		private Region				handlesRgn ;
		private	SolidBrush			backBrush ;
		private ArrayList			ctrlInfo ;
		private ArrayList			undoInfo ;
		private bool				moveUndoAdded ;
		private bool				dragging ;
		private bool				almostDragging ;
		private bool				rubberBand ;
		private bool				almostRubberBand ;
		private Rectangle			bandRect ;				//in client coordinates
		private	bool				busy ;					//true to show hourglass cursor
		private bool				pasting ;
		private	ControlExplorerForm	editor ;

		private ControlEventHandler			addedHdlr ;			//}
		private ControlEventHandler			removedHdlr ;		//}frame
		private PropertyChangedEventHandler	propertyHdlr ;		//}
		private	EventHandler				relocatedHdlr ;		//frame and Owner
		private	EventHandler				disposedHdlr ;		//property editor
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new Instance of the EditForm class
		/// </summary>
		/// <param name="parent">FrameBase for which this form acts as editor. Must be a valid FrameBase object</param>
		public EditForm(FrameBase parent)
		{
			InitializeComponent();

			frameBase = parent ;
			Owner = frameBase.ParentForm ;

			editor = null ;

			InitialiseMenu() ;

			menuShowing			= false ;
			codeDelete			= false ;
			codeAdd				= false ;
			moveUndoAdded		= false ;
			moving				= false ;
			click				= Point.Empty ;
			currentSelection	= null ;
			backBrush			= new SolidBrush(BackColor) ;
			handlesRgn			= new Region(Rectangle.Empty) ;
			busy				= false ;
			pasting				= false ;

			dragging = almostDragging = rubberBand = almostRubberBand = false ;
			bandRect = Rectangle.Empty ;
			dragStartPoint = dragReferencePoint = Point.Empty ;
			dragReferenceSize = Size.Empty ;
			dragObject = null ;

			addedHdlr		= new ControlEventHandler(OnControlAdded) ;
			removedHdlr		= new ControlEventHandler(OnControlRemoved) ;
			propertyHdlr	= new PropertyChangedEventHandler(OnPropertyChanged) ;
			disposedHdlr	= new EventHandler(OnEditorDisposed) ;
			relocatedHdlr	= new EventHandler(OnRelocated) ;

			if (frameBase.currentPanel != null)
			{
				frameBase.currentPanel.ControlAdded		+= addedHdlr ;
				frameBase.currentPanel.ControlRemoved	+= removedHdlr ;
			}
			frameBase.PropertyChanged		+= propertyHdlr ;

			frameBase.Resize			+= relocatedHdlr ;
			frameBase.LocationChanged	+= relocatedHdlr ;
			Owner.LocationChanged		+= relocatedHdlr ;

			if (frameBase.currentPanel != null)
			{
				System.Windows.Forms.Control.ControlCollection	controls = frameBase.currentPanel.Controls ;

				ctrlInfo = new ArrayList(controls.Count) ;
				for (int index = 0; index < controls.Count; index++)
				{
					ctrlInfo.Add(new ControlEditInfo(controls[index], false)) ;
				}
			}
			else
			{
				ctrlInfo = new ArrayList() ;
			}

			undoInfo = new ArrayList(MAX_UNDOS) ;
		}

		private void InitialiseMenu()
		{
			MenuItem []		items ;
			MenuItem []		add_items			= null ;
			MenuItem []		component_add_items	= null ;
			MenuItem []		align_items			= new MenuItem[6] ;
			MenuItem []		size_items			= new MenuItem[3] ;
			MenuItem []		stack_items			= new MenuItem[2] ;
			MenuItem []		layout_items		= new MenuItem[6] ;

			if (frameBase.addList != null)
			{
				add_items = new MenuItem[frameBase.addList.Count] ;
				for (int index = 0; index < add_items.Length; index++)
				{
					add_items[index] = new MenuItem(((Addable)frameBase.addList[index]).Name, new EventHandler(OnClickAddable)) ;
				}
			}

			if (frameBase.componentAddList != null)
			{
				component_add_items = new MenuItem[frameBase.componentAddList.Count] ;
				for (int index = 0; index < component_add_items.Length; index++)
				{
					component_add_items[index] = new MenuItem(((Addable)frameBase.componentAddList[index]).Name, new EventHandler(OnClickAddableComponent)) ;
				}
			}

			align_items[0] = new MenuItem("&Left", new EventHandler(OnClickAlign)) ;
			align_items[1] = new MenuItem("&Right", new EventHandler(OnClickAlign)) ;
			align_items[2] = new MenuItem("&Top", new EventHandler(OnClickAlign)) ;
			align_items[3] = new MenuItem("&Bottom", new EventHandler(OnClickAlign)) ;
			align_items[4] = new MenuItem("&Horizontal Centre", new EventHandler(OnClickAlign)) ;
			align_items[5] = new MenuItem("&Vertical Centre", new EventHandler(OnClickAlign)) ;

			size_items[0] = new MenuItem("&Width", new EventHandler(OnClickSameSize)) ;
			size_items[1] = new MenuItem("&Height", new EventHandler(OnClickSameSize)) ;
			size_items[2] = new MenuItem("&Both", new EventHandler(OnClickSameSize)) ;

			stack_items[0] = new MenuItem("&Horizontal", new EventHandler(OnClickStack)) ;
			stack_items[1] = new MenuItem("&Vertical", new EventHandler(OnClickStack)) ;

			layout_items[0] = new MenuItem("&Snap to grid", new EventHandler(OnClickToGrid)) ;
			layout_items[1] = new MenuItem("Si&ze to grid", new EventHandler(OnClickToGrid)) ;
			layout_items[2] = new MenuItem("&Align to grid", new EventHandler(OnClickToGrid)) ;
			layout_items[3] = new MenuItem("-") ;
			layout_items[4] = new MenuItem("Bring to &Front", new EventHandler(OnClickZOrder)) ;
			layout_items[5] = new MenuItem("Send to &Back", new EventHandler(OnClickZOrder)) ;

			items = new MenuItem [(int)FrameMenu.Count] ;

			items[(int)FrameMenu.ExitEditMode]	= new MenuItem("Exit &Edit Mode", new EventHandler(OnClickEditMode), Shortcut.CtrlE) ;
			items[(int)FrameMenu.ExitBar]		= new MenuItem("-") ;
			items[(int)FrameMenu.Save]			= new MenuItem("&Save", new EventHandler(OnClickSave), Shortcut.CtrlS) ;
			items[(int)FrameMenu.SaveBar]		= new MenuItem("-") ;
			items[(int)FrameMenu.Undo]			= new MenuItem("&Undo", new EventHandler(OnClickUndo), Shortcut.CtrlZ) ;
			items[(int)FrameMenu.UndoBar]		= new MenuItem("-") ;
			items[(int)FrameMenu.Add]			= new MenuItem("A&dd", add_items) ;
			if (add_items == null)				items[(int)FrameMenu.Add].Enabled = false ;
			items[(int)FrameMenu.AddComponent]	= new MenuItem("Add Component", component_add_items) ;
			if (component_add_items == null)	items[(int)FrameMenu.AddComponent].Enabled = false ;
			items[(int)FrameMenu.AddBar]		= new MenuItem("-") ;
			items[(int)FrameMenu.SelectAll]		= new MenuItem("Select &All", new EventHandler(OnClickSelectAll), Shortcut.CtrlA) ;
			items[(int)FrameMenu.SelectBar]		= new MenuItem("-") ;
			items[(int)FrameMenu.Align]			= new MenuItem("Alig&n", align_items) ;
			items[(int)FrameMenu.MakeSameSize]	= new MenuItem("Make Same Si&ze", size_items) ;
			items[(int)FrameMenu.Stack]			= new MenuItem("S&tack", stack_items) ;
			items[(int)FrameMenu.Layout]		= new MenuItem("&Layout", layout_items) ;
			items[(int)FrameMenu.FormatBar]		= new MenuItem("-") ;
			items[(int)FrameMenu.Cut]			= new MenuItem("Cut", new EventHandler(OnClickCut), Shortcut.CtrlX) ;
			items[(int)FrameMenu.Copy]			= new MenuItem("Copy", new EventHandler(OnClickCopy), Shortcut.CtrlC) ;
			items[(int)FrameMenu.Paste]			= new MenuItem("Paste", new EventHandler(OnClickPaste), Shortcut.CtrlV) ;
			items[(int)FrameMenu.Delete]		= new MenuItem("Delete", new EventHandler(OnClickDelete), Shortcut.Del) ;
			items[(int)FrameMenu.DeleteBar]		= new MenuItem("-") ;
			items[(int)FrameMenu.AdvProperties]	= new MenuItem("Advanced P&roperties", new EventHandler(OnClickAdvancedProperties)) ;
			items[(int)FrameMenu.Properties]	= new MenuItem("&Properties", new EventHandler(OnClickProperties)) ;
			items[(int)FrameMenu.Rename]		= new MenuItem("&Rename", new EventHandler(OnClickRename)) ;
            items[(int)FrameMenu.GridEnable]    = new MenuItem("&Grid", new EventHandler(OnGridEnable), Shortcut.CtrlG);
            items[(int)FrameMenu.DisconnectControl] = new MenuItem("&Disconnect", new EventHandler(OnClickDisconnectControl));
            items[(int)FrameMenu.ConnectControl] = new MenuItem("&Connect", new EventHandler(OnClickConnectControl));
			menu = new ContextMenu(items) ;
			menu.Popup += new EventHandler(OnMenuPopup) ;

			ContextMenu = menu ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				backBrush.Dispose() ;
				if (editor != null)		editor.Dispose() ;

//				if(components != null)
//				{
//					components.Dispose();
//				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// EditForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(300, 300);
			this.ControlBox = false;
			this.Cursor = System.Windows.Forms.Cursors.Cross;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MinimumSize = new System.Drawing.Size(1, 1);
			this.Name = "EditForm";
			this.Opacity = 0.20000000298023224;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

		}
		#endregion

		#region Drawing
		/// <summary>
		/// Paint Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnPaint(PaintEventArgs e) 
		{
			base.OnPaint(e) ;

			DrawHandles(e.Graphics) ;
			DrawRubberBand(e.Graphics) ;
		}

		/// <summary>
		/// Back Color Changed Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnBackColorChanged(EventArgs e)
		{
			if (backBrush != null)			backBrush.Color = BackColor ;
			base.OnBackColorChanged(e) ;
		}

		private void DrawHandles()
		{
			Graphics		g = CreateGraphics() ;
			DrawHandles(g) ;
			g.Dispose() ;
		}
		private void DrawHandles(Graphics g)
		{
			//remove old handles (if any)
			if (!handlesRgn.IsEmpty(g))
			{
				g.FillRegion(backBrush, handlesRgn) ;
			}

			handlesRgn.MakeEmpty() ;

			if (currentSelection != null)
			{
				int			x_offset	= frameBase.DisplayRectangle.Left ;
				int			y_offset	= frameBase.DisplayRectangle.Top ;

				foreach (ControlEditInfo info in ctrlInfo)
				{
					if (info.selected)
					{
						//maybe not necessary to do this here? think about optimising calcs
						info.RecalculateHandles(x_offset, y_offset) ;

						if (info.Equals(currentSelection))
						{
							for (int index = 0; index < 8; index++)
							{
								ControlPaint.DrawGrabHandle(g, info.handles[index], true, true) ;
								handlesRgn.Union(info.handles[index]) ;
							}
						}
						else
						{
							for (int index = 0; index < 8; index++)
							{
								ControlPaint.DrawGrabHandle(g, info.handles[index], false, true) ;
								handlesRgn.Union(info.handles[index]) ;
							}
						}
					}
				}
			}
		}
		private void DrawRubberBand(Graphics g)
		{
			if (rubberBand)
			{
				ControlPaint.DrawReversibleFrame(RectangleToScreen(bandRect), BackColor, FrameStyle.Dashed) ;
			}
		}
		#endregion

		#region Other Methods
		//find which control is at the given point
		//returns ControlEditInfo object for control (or null)
		private ControlEditInfo PointToChild(int X, int Y)
		{
			Point							panel_pos	= new Point(X - frameBase.DisplayRectangle.Left, Y - frameBase.DisplayRectangle.Top) ;
			System.Windows.Forms.Control	ctrl ;
			
			if (frameBase.currentPanel != null)
			{
				ctrl = frameBase.currentPanel.GetChildAtPoint(panel_pos) ;

				//find the cei corresponding to this control
				return FindCEI(ctrl) ;
			}

			//no panel => no CEI
			return null ;

//			Point		panel_pos = new Point(X - frame.DisplayRectangle.Left, Y - frame.DisplayRectangle.Top) ;
//
//			foreach(ControlEditInfo info in ctrlInfo)
//			{
//				Rectangle		rect	= info.ctrl.ClientRectangle ;
//				
//				rect.Location = info.ctrl.Location ;
//
//				if (rect.Contains(panel_pos))
//				{
//					return info ;
//				}
//			}
//
//			return null ;
		}

		//find the cei corresponding to the given control
		//returns null if not found
		private ControlEditInfo FindCEI(System.Windows.Forms.Control Ctrl)
		{
			if (Ctrl == null)			return null ;

			foreach(ControlEditInfo info in ctrlInfo)
			{
				if (info.ctrl.Equals(Ctrl))
				{
					return info ;
				}
			}

			return null ;
		}

		private void SetSelections(ControlEditInfo newCEI, bool LeftBtn)
		{
			bool	ctrl_pressed		= ((Control.ModifierKeys & Keys.Control) == Keys.Control) ;
			bool	shift_pressed		= ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) ;
			bool	alt_pressed			= ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) ;
			bool	show_menu			= false ;
			bool	show_properties		= false ;

			if (LeftBtn)
			{
				if (!dragging)
				{
					if (newCEI == null)		//didn't click on a control
					{
						if (!ctrl_pressed && !shift_pressed)
						{
							ClearSelections() ;
						}
						//else leave selections as now
					}
					else if (shift_pressed && !ctrl_pressed && !alt_pressed)	//holding shift
					{
						//make chosen selection current, leave others
						if (newCEI != currentSelection)
						{
							currentSelection = newCEI ;
							currentSelection.selected = true ;
							moveUndoAdded = false ;
						}
					}
					else if (!shift_pressed && !ctrl_pressed && !alt_pressed)	//no btns held
					{
						//make chosen selection current, clear others
						ClearSelections() ;
						currentSelection = newCEI ;
						currentSelection.selected = true ;
					}
					else if (!shift_pressed && ctrl_pressed && !alt_pressed)	//holding ctrl
					{
						if (newCEI.selected)
						{
							//unselect this one, leave the others
							newCEI.selected = false ;
							moveUndoAdded = false ;

							//do we need to pick a new current selection?
							if (newCEI == currentSelection)
							{
								currentSelection = null ;

								foreach (ControlEditInfo cei in ctrlInfo)
								{
									if (cei.selected)
									{
										currentSelection = cei ;
										break ;
									}
								}
							}
						}
						else
						{
							//select this one (as current), leave the others
							currentSelection = newCEI ;
							currentSelection.selected = true ;
							moveUndoAdded = false ;
						}
					}
					//else invalid key combination - do nothing
				}
				//else dragging - don't change selections
			}
			else		//right btn
			{
				if (newCEI == null)		//didn't click on a control
				{
					//leave selections as now
				}
				else if (newCEI.selected)
				{
					//make chosen selection current, leave others
					if (newCEI != currentSelection)
					{
						currentSelection = newCEI ;
						currentSelection.selected = true ;
						moveUndoAdded = false ;
					}

					if (!shift_pressed && !ctrl_pressed && alt_pressed)		//holding alt
					{
						//bring up property pages
						show_properties = true ;
					}
					else if ((!shift_pressed || !ctrl_pressed) && !alt_pressed)
					{
						//bring up edit menu
						show_menu = true ;
					}
					//else invalid key combination - do nothing
				}
				else	//not a currently selected control
				{
					if (!shift_pressed && !ctrl_pressed && alt_pressed)		//holding alt
					{
						//make chosen selection current, clear others
						ClearSelections() ;
						currentSelection = newCEI ;
						currentSelection.selected = true ;

						//bring up property pages
						show_properties = true ;
					}
					else if (!shift_pressed && !ctrl_pressed && !alt_pressed)	//no btns held
					{
						//make chosen selection current, clear others
						ClearSelections() ;
						currentSelection = newCEI ;
						currentSelection.selected = true ;

						//bring up edit menu
						show_menu = true ;
					}
					else if ((!shift_pressed || !ctrl_pressed) && !alt_pressed)	//shift or ctrl held
					{
						//make chosen selection current, leave others
						currentSelection = newCEI ;
						currentSelection.selected = true ;
						moveUndoAdded = false ;

						//bring up edit menu
						show_menu = true ;
					}
					//else invalid key combination - do nothing
				}
			}

			DrawHandles() ;
			
			//do these after redrawing handles
			if (show_menu)	
			{
				menu.Show(this, PointToClient(Cursor.Position)) ;
			}
			else if (show_properties)
			{
				ShowProperties(false) ;
			}
		}

		private void ShowProperties(bool Advanced)
		{
			if (editor == null)
			{
				editor = new ControlExplorerForm() ;
				editor.Owner = frameBase.ParentForm ;
				editor.Disposed += disposedHdlr ;
			}

			if (currentSelection != null)
			{
				object []			ctrls ;
				int					count		= 0 ;
				int					index		= 1 ;

				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected)		count++ ;
				}

				ctrls = new object [count] ;
				ctrls[0] = currentSelection.ctrl ;
				
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected && !cei.Equals(currentSelection))
					{
						ctrls[index] = cei.ctrl ;
						index++ ;
					}
				}

				//show control's properties
				editor.Text = currentSelection.ctrl.Name + " Properties" ;
				editor.SelectObjects(ctrls) ;
			}
			else if (!Advanced)
			{
				//show properties for whatever the frame wants to
				object		obj		= frameBase.GetBasicPropertiesObject() ;

				editor.Text = "Panel Properties" ;
				editor.SelectObjects(new object[]{obj}) ;
			}
			else
			{
				//show frame's properties
				editor.Text = frameBase.GetType().Name + " Properties" ;
				editor.SelectObjects(new object[]{frameBase}) ;
			}
            //editor.Visible = false;
            editor.Show(); //Show(this); 
		}

		private void SelectAll(bool Redraw)
		{
			foreach (ControlEditInfo info in ctrlInfo)
			{
				info.selected = true ;
			}

			if (ctrlInfo.Count > 0)
			{
				currentSelection = (ControlEditInfo)ctrlInfo[0] ;
			}

			moveUndoAdded = false ;
			
			if (Redraw)			DrawHandles() ;
		}

		private void ClearSelections()
		{
			foreach (ControlEditInfo info in ctrlInfo)
			{
				info.selected = false ;
			}
			currentSelection = null ;
			moveUndoAdded = false ;
		}

		private void SetCursor(int X, int Y)
		{
			Corners			corner ;

			currentCorner = Corners.Invalid ;

			if (busy)		Cursor = Cursors.WaitCursor ;

			foreach (ControlEditInfo info in ctrlInfo)
			{
				corner = info.SelectedHandleContains(X, Y) ;
				if (corner != Corners.Invalid)
				{
					//set cursor to specified directional cursor
					currentCorner = corner ;
					Cursor = Directions[(int)corner] ;
					return ;
				}
			}
			//if get here, we are not pointing at a handle

			ControlEditInfo		cei = PointToChild(X, Y) ;
			string				new_tip ;

			if (cei != null)
			{
				Cursor = Cursors.SizeAll ;
				new_tip = cei.ctrl.Name + ": " + cei.ctrl.GetType().ToString ();

                if (cei.ctrl is ISource)//ft
                {
                    ISource ctrl = cei.ctrl as ISource; //ft
                    if (ctrl != null)
                        new_tip += "\n  Source: " + ctrl.Source;
                }
                if (cei.ctrl is IDestination)//ft
                {
                    IDestination ctrl = cei.ctrl as IDestination; //ft
                    if (ctrl != null)
                        new_tip += "\nDestination: " + ctrl.Destination;
                }
                if (cei.ctrl is IInputEnabledSource)//ft
                {
                    IInputEnabledSource ctrl = cei.ctrl as IInputEnabledSource; //ft
                    if (ctrl != null)
                        new_tip += "\nEnabled: " + ctrl.InputEnabledSource;
                }
                if (cei.ctrl is IResetStateSource)//ft
                {
                    IResetStateSource ctrl = cei.ctrl as IResetStateSource; //ft
                    if (ctrl != null)
                        new_tip += "\n    Reset: " + ctrl.ButtonResetStateSource;
                }


			}
			else		//cursor must be default (ie cross)
			{
				Cursor = Cursors.Cross ;
				new_tip = "" ;
			}

			//this just about works, but not well
			if (new_tip != toolTip1.GetToolTip(this))
			{
				toolTip1.SetToolTip(this, new_tip) ;
			}
		}
		private System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip () ;

        private bool gridActive = true;

		//assumes got a currentSelection
		private void DoMove(int X, int Y)
		{
			if (frameBase.currentPanel == null)		return ;

			int		v_move ;	
			int		h_move ;	
			int		new_left ;	
			int		new_top ;	
			int		max_left ;	
			int		max_right ;	
			int		max_up ;	
			int		max_down ;
            int     h_pixels = gridActive ? frameBase.HorizontalGrid.Pixels : 1;	
			int		v_pixels = gridActive ? frameBase.VerticalGrid.Pixels : 1 ;	
	
			//how far can we move?
			GetMaxMovement(out max_left, out max_right, out max_up, out max_down) ;

			//horizontal...
			h_move = X - dragStartPoint.X ;
			new_left = dragReferencePoint.X + h_move ;

			if (h_pixels > 1)
			{
                MoveToGridPosition(ref new_left, dragReferenceSize.Width, gridActive ? frameBase.HorizontalGrid.Pixels : 1, frameBase.currentPanel.DisplayRectangle.Left, frameBase.currentPanel.DisplayRectangle.Right);
			}

			//ensure not gone too far to right
			while ((new_left - currentSelection.ctrl.Left) > max_right) 
			{
				new_left -= h_pixels ;
			}

			//ensure not gone too far to left
			while ((currentSelection.ctrl.Left - new_left) > max_left)
			{
				new_left += h_pixels ;
			}

			//if it now goes too far to right, it is unable to move horizontally
			if ((new_left - currentSelection.ctrl.Left) > max_right)
			{
				h_move = 0 ;
			}
			else
			{
				h_move = new_left - currentSelection.ctrl.Left ;
			}

			//vertical...
			v_move = Y - dragStartPoint.Y ;
			new_top = dragReferencePoint.Y + v_move ;

			if (v_pixels > 1)
			{
                MoveToGridPosition(ref new_top, dragReferenceSize.Height, gridActive ? frameBase.VerticalGrid.Pixels : 1, frameBase.currentPanel.DisplayRectangle.Top, frameBase.currentPanel.DisplayRectangle.Bottom);
			}

			//ensure not gone too far down
			while ((new_top - currentSelection.ctrl.Top) > max_down) 
			{
				new_top -= v_pixels ;
			}

			//ensure not gone too far up
			while ((currentSelection.ctrl.Top - new_top) > max_up)
			{
				new_top += v_pixels ;
			}

			//if it now goes too far down, it is unable to move vertically
			if ((new_top - currentSelection.ctrl.Top) > max_down)
			{
				v_move = 0 ;
			}
			else
			{
				v_move = new_top - currentSelection.ctrl.Top ;
			}

			if (h_move != 0 || v_move != 0)
			{
				AddMoveUndo() ;

				//move all selected objects - including currentSelection
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected)
					{
						if (h_move != 0)		cei.ctrl.Left += h_move ;
						if (v_move != 0)		cei.ctrl.Top  += v_move ;
					}
				}

				DrawHandles() ;
			}
		}

		// Aligns left or top (= Start) to grid point on given Grid
		// Returns true if moved
		private bool MoveToGridPosition(ref int Start, int Size, int Pixels, int Min, int Max)
		{
			int		new_start ;
			int		new_end ;
			int		remainder ;

			//how far off the grid is it?
			remainder = (Start - Min) % Pixels ;

			//align it to the previous grid point
			new_start = Start - remainder ;

			//should it be the next grid point?
			if (remainder > Pixels / 2)
			{
				new_start += Pixels ;
			}

			//ensure its not off the frame
			new_end = new_start + Size ;
			while (new_end > Max)
			{
				//reduce it by a whole grid point
				new_start -= Pixels ;
				new_end = new_start + Size ;
			}

			if (new_start < Min)
			{
				new_start = Min ;
			}

			if (new_start != Start)
			{
				Start = new_start ;
				return true ;
			}

			Start = new_start ;
			return false ;
		}

		// Returns max movement in each direction to keep all selected controls within the frame
		private void GetMaxMovement(out int MaxLeft, out int MaxRight, out int MaxUp, out int MaxDown)
		{
			if (frameBase.currentPanel == null)
			{
				MaxLeft = MaxRight = MaxUp = MaxDown = 0 ;
				return ;
			}

			Rectangle		rect		= frameBase.currentPanel.DisplayRectangle ;

			MaxLeft	= MaxRight	= rect.Width ;
			MaxUp	= MaxDown	= rect.Height ;

			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected)
				{
					if (cei.ctrl.Dock != DockStyle.None)
					{
						//this control is locked where it is - nothing can move!
						MaxLeft = MaxRight = MaxUp = MaxDown = 0 ;
						break ;
					}

					if ((cei.ctrl.Left - rect.Left) < MaxLeft)
					{
						MaxLeft = cei.ctrl.Left - rect.Left ;
					}

					if ((rect.Right - cei.ctrl.Right) < MaxRight)
					{
						MaxRight = rect.Right - cei.ctrl.Right ;
					}

					if ((cei.ctrl.Top - rect.Top) < MaxUp)
					{
						MaxUp = cei.ctrl.Top - rect.Top ;
					}

					if ((rect.Bottom - cei.ctrl.Bottom) < MaxDown)
					{
						MaxDown = rect.Bottom - cei.ctrl.Bottom ;
					}
				}
			}
		}

		//assumes got a currentSelection
		private void DoResize(int X, int Y)
		{
			if (frameBase.currentPanel == null)			return ;

			bool		northerly			= false ;
			bool		southerly			= false ;
			bool		easterly			= false ;
			bool		westerly			= false ;
			int			max_west ;
			int			max_east ;
			int			max_width ;
			int			max_north ;
			int			max_south ;
			int			max_height ;
			int			new_top ;
			int			new_left ;
			int			new_height ;
			int			new_width ;
			int			width_move ;				//}
			int			height_move ;				//}how far the handle has been dragged
			int			left_move ;					//}
			int			top_move ;					//}
            int         h_pixels        = gridActive ? frameBase.HorizontalGrid.Pixels : 1;	
			int			v_pixels		= gridActive ? frameBase.VerticalGrid.Pixels : 1 ;	

			switch (currentCorner)
			{
				case Corners.North:			northerly = true ;					break ;
				case Corners.South:			southerly = true ;					break ;
				case Corners.East:			easterly = true ;					break ;
				case Corners.West:			westerly = true ;					break ;
				case Corners.NorthEast:		northerly = easterly = true ;		break ;
				case Corners.NorthWest:		northerly = westerly = true ;		break ;
				case Corners.SouthEast:		southerly = easterly = true ;		break ;
				case Corners.SouthWest:		southerly = westerly = true ;		break ;
			}

			left_move = X - dragStartPoint.X ;
			top_move = Y - dragStartPoint.Y ;

			GetMaxResizing(out max_west, out max_east, out max_width, out max_north, out max_south, out max_height) ;
	
			new_left		= currentSelection.ctrl.Location.X ;
			new_top			= currentSelection.ctrl.Location.Y ;
			new_width		= currentSelection.ctrl.Size.Width ;	
			new_height		= currentSelection.ctrl.Size.Height ;

			if (northerly)
			{
				//only go through any of this if movement is possible
				if (max_west > 0 || max_width > 0)
				{
					bool		ok			= true ;

					//reset top and height
					new_top		= dragReferencePoint.Y + top_move ; 
					new_height	= dragReferenceSize.Height - top_move ; 

					if (v_pixels > 1)
					{
						ok = StartToGrid(ref new_top, ref new_height, 0, frameBase.currentPanel.DisplayRectangle.Top, v_pixels) ;
					}

					if (!ok)
					{
						//resizing disallowed
						new_top		= currentSelection.ctrl.Location.Y ;
						new_height	= currentSelection.ctrl.Size.Height ;
					}
					else
					{
						//ensure not gone too far north
						while ((currentSelection.ctrl.Location.Y - new_top) > max_north)
						{
							new_top += v_pixels ;
							new_height -= v_pixels ;
						}

						//ensure not too short
						while ((new_top - currentSelection.ctrl.Location.Y) > max_height)
						{
							new_top -= v_pixels ;
							new_height += v_pixels ;
						}

						//if too tall now, cannot move
						if ((currentSelection.ctrl.Location.Y - new_top) > max_north)
						{
							new_top	= currentSelection.ctrl.Location.Y ;
							new_height = currentSelection.ctrl.Size.Height ;
						}
					}
				}
			}
			else if (southerly)
			{
				//only go through any of this if movement is possible
				if (max_south > 0 || max_height > 0)
				{
					//reset height
					new_height = dragReferenceSize.Height + top_move ; 

					if (v_pixels > 1)
					{
						SizeToGrid(new_top, ref new_height, 0, frameBase.currentPanel.DisplayRectangle.Bottom, frameBase.VerticalGrid) ;
					}

					//ensure not gone too far south
					while ((new_height - currentSelection.ctrl.Size.Height) > max_south) 
					{
						new_height -= v_pixels ;
					}

					//ensure not too short
					while ((currentSelection.ctrl.Size.Height - new_height) > max_height)
					{
						new_height += v_pixels ;
					}
				}
			}

			if (westerly)
			{
				//only go through any of this if movement is possible
				if (max_west > 0 || max_width > 0)
				{
					bool		ok			= true ;

					//reset left and width
					new_left	= dragReferencePoint.X + left_move ; 
					new_width	= dragReferenceSize.Width - left_move ; 

					if (h_pixels > 1)
					{
						ok = StartToGrid(ref new_left, ref new_width, 0, frameBase.currentPanel.DisplayRectangle.Left, h_pixels) ;
					}

					if (!ok)
					{
						//resizing disallowed
						new_left	= currentSelection.ctrl.Location.X ;
						new_width	= currentSelection.ctrl.Size.Width ;
					}
					else
					{
						//ensure not gone too far west
						while ((currentSelection.ctrl.Location.X - new_left) > max_west)
						{
							new_left ++ ;
							new_width -- ;
						}

						//ensure not too thin
						while ((new_left - currentSelection.ctrl.Location.X) > max_width)
						{
							new_left -- ;
							new_width ++ ;
						}

						//if too wide now, cannot move
						if ((currentSelection.ctrl.Location.X - new_left) > max_west)
						{
							new_left	= currentSelection.ctrl.Location.X ;
							new_width	= currentSelection.ctrl.Size.Width ;
						}
					}
				}
			}
			else if (easterly)
			{
				//only go through any of this if movement is possible
				if (max_east > 0 || max_width > 0)
				{
					//reset width
					new_width = dragReferenceSize.Width + left_move ; 

					if (h_pixels > 1)
					{
						SizeToGrid(new_left, ref new_width, 0, frameBase.currentPanel.DisplayRectangle.Right, frameBase.HorizontalGrid) ;
					}

					//ensure not gone too far east
					while ((new_width - currentSelection.ctrl.Size.Width) > max_east) 
					{
						new_width -= h_pixels ;
					}

					//ensure not too thin
					while ((currentSelection.ctrl.Size.Width - new_width) > max_width)
					{
						new_width += h_pixels ;
					}
				}
			}

			left_move = new_left - currentSelection.ctrl.Location.X ;
			width_move = new_width - currentSelection.ctrl.Size.Width ;

			top_move = new_top - currentSelection.ctrl.Location.Y ;
			height_move = new_height - currentSelection.ctrl.Size.Height ;

			if (left_move != 0 || width_move != 0 || top_move != 0 || height_move != 0)
			{
				AddMoveUndo() ;

				//resize all selected objects
				foreach (ControlEditInfo info in ctrlInfo)
				{
					if (info.selected)
					{
						if (left_move != 0)
						{
							info.ctrl.Left	+= left_move ;
							info.ctrl.Width	-= left_move ;
						}
						else if (width_move != 0)
						{
							info.ctrl.Width += width_move ;
						}

						if (top_move != 0)
						{
							info.ctrl.Top	+= top_move ;
							info.ctrl.Height-= top_move ;
						}
						else if (height_move != 0)
						{
                            info.ctrl.Height += height_move; //ft Cannot use a DependencyObject that belongs to a different thread than its parent Freezable.
						}
					}
				}

				DrawHandles() ;
			}
		}

		// Puts 'Start' onto the nearest grid line
		// Doesn't change the End of the control: returns FALSE if would be forced
		// to move End to get Start onto a grid point
		private bool StartToGrid(ref int Start, ref int Size, int MinSize, int MinStart, int GridPixels)
		{
			int		new_start ;		//left or top
			int		remainder ;
			int		end ;			//right or bottom - won't change
			int		new_size ;		//width or height

			end = Start + Size ;

			//how far off the grid is it?
			remainder = (Start - MinStart) % GridPixels ;

			//make it the previous grid position
			new_start = Start - remainder ;

			//should it be the next grid position?
			if (remainder > GridPixels / 2)
			{
				new_start += GridPixels ;
			}

			//ensure it is inside the frame
			while (new_start < MinStart)
			{
				new_start += GridPixels ;
			}

			new_size = end - new_start ;

			//is it now too small?
			while (new_size < MinSize)
			{
				//can we move the start up a bit?
				new_start -= GridPixels ;
				new_size += GridPixels ;

				if (new_start < MinStart)
				{
					//impossible to reconcile all conditions
					return false ;
				}
			}

			Start = new_start ;
			Size = new_size ;

			return true ;
		}

		// Makes 'Size' (width or height) n grid units. ('Start' is left or top)
		//returns true if resized
		private bool SizeToGrid(int Start, ref int Size, int MinSize, int MaxEnd, Grid Grid)
		{
			int		new_end ;
			int		new_size ;
			int		remainder ;

			//how far off the grid is it?
			remainder = Size % Grid.Pixels ;

			//make it the previous grid size
			new_size = Size - remainder ;

			//should it be the next grid size?
			if (remainder > Grid.Pixels / 2)
			{
				new_size += Grid.Pixels ;
			}

			new_size -= Grid.Spacing ;

			//ensure its not off the frame
			new_end = Start + new_size ;
			if (new_end > MaxEnd)
			{
				//reduce it by a whole grid size
				new_size -= Grid.Pixels ;
			}

			//..or too small
			if (new_size < MinSize)
			{
				new_size = MinSize ;
			}

			if (Size != new_size)
			{
				Size = new_size ;
				return true ;
			}

			return false ;
		}

		// Finds max resizing in each direction to keep all selected controls within the frame
		private void GetMaxResizing(out int MaxLeft, out int MaxRight, out int MaxWidth, out int MaxUp, out int MaxDown, out int MaxHeight)
		{
			if (frameBase.currentPanel == null)
			{
				MaxLeft = MaxRight = MaxWidth = MaxUp = MaxDown = MaxHeight = 0 ;
				return ;
			}

			Rectangle		rect		= frameBase.currentPanel.DisplayRectangle ;

			MaxLeft	= MaxRight	= MaxWidth	= rect.Width ;
			MaxUp	= MaxDown	= MaxHeight	= rect.Height ;

			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected)
				{
					switch (cei.ctrl.Dock)
					{
						case DockStyle.Fill:	//this ctrl cannot move, so none can
						{
							MaxLeft = MaxRight = MaxUp = MaxDown = MaxWidth = MaxHeight = 0 ;
						}
						return ;

						case DockStyle.Left:	//this ctrl can only move right or width
						{
							MaxLeft = MaxUp = MaxDown = MaxHeight = 0 ;
						}
						break ;

						case DockStyle.Right:	//this ctrl can only move left or width
						{
							MaxRight = MaxUp = MaxDown = MaxHeight = 0 ;
						}
						break ;

						case DockStyle.Top:		//this ctrl can only move down or height
						{
							MaxLeft = MaxRight = MaxUp = MaxWidth = 0 ;
						}
						break ;

						case DockStyle.Bottom:	//this ctrl can only move up or height
						{
							MaxLeft = MaxRight = MaxDown = MaxWidth = 0 ;
						}
						break ;

						default:				//this ctrl can move anywhere
						break ;
					}

					//now check the remaining movement
					if ((cei.ctrl.Left - rect.Left) < MaxLeft)
					{
						MaxLeft = cei.ctrl.Left - rect.Left ;
					}

					if ((cei.ctrl.Top - rect.Top) < MaxUp)
					{
						MaxUp = cei.ctrl.Top - rect.Top ;
					}

					if ((rect.Right - cei.ctrl.Right) < MaxRight)
					{
						MaxRight = rect.Right - cei.ctrl.Right ;
					}

					if (cei.ctrl.Width < MaxWidth)
					{
						MaxWidth = cei.ctrl.Width ;
					}

					if ((rect.Bottom - cei.ctrl.Bottom) < MaxDown)
					{
						MaxDown = rect.Bottom - cei.ctrl.Bottom ;
					}

					if (cei.ctrl.Height < MaxHeight)
					{
						MaxHeight = cei.ctrl.Height ;
					}
				}
			}
		}

		//Selects all controls within the band (or touched by the band if right button)
		private void BandSelections(Rectangle BandRect, MouseButtons Button)
		{
			Rectangle		band_rect		= BandRect ;
			Rectangle		ctrl_rect ;

			//are we holding the shift key? if not, clear current selections
			if (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift))
			{
				ClearSelections() ;
			}

			//ensure band rect is positive (width and height >= 0)
			if (band_rect.Width < 0)
			{
				band_rect.X += band_rect.Width ;
				band_rect.Width *= -1 ;
			}
			if (band_rect.Height < 0)
			{
				band_rect.Y += band_rect.Height ;
				band_rect.Height *= -1 ;
			}

			//convert to panel coordinates (not frame ones)
			band_rect.X -= frameBase.DisplayRectangle.X ;
			band_rect.Y -= frameBase.DisplayRectangle.Y ;

			//##?? go through the controls in reverse order so that
			//current selection (if there isn't one already) is the topmost selected control
			if (Button != MouseButtons.Right)
			{
				//fully in band...
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					ctrl_rect = cei.ctrl.ClientRectangle ;
					ctrl_rect.Location = cei.ctrl.Location ;

					if (band_rect.Contains(ctrl_rect))
					{
						cei.selected = true ;
						if (currentSelection == null)
						{
							currentSelection = cei ;
						}
					}
				}
			}
			else
			{
				//touched by band
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					ctrl_rect = cei.ctrl.ClientRectangle ;
					ctrl_rect.Location = cei.ctrl.Location ;

					if (ctrl_rect.Left <= band_rect.Right && ctrl_rect.Right >= band_rect.Left &&
						ctrl_rect.Top <= band_rect.Bottom && ctrl_rect.Bottom >= band_rect.Top	)
					{
						cei.selected = true ;
						if (currentSelection == null)
						{
							currentSelection = cei ;
						}
					}
				}
			}

			DrawHandles() ;
		}

		private bool MultipleItemsSelected()
		{
			int		count		= 0 ;

			if (currentSelection == null)		return false ;

			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected)
				{
					count++ ;

					if (count > 1)		return true ;
				}
			}

			return false ;
		}
		
		//adds initialisation statements to the specified method for selected controls
		//returns null if not used resources
		private System.Resources.ResourceWriter SaveSelectedControls(CodeMemberMethod Method, bool compact)
		{
			if (frameBase.currentPanel == null)			return null ;

			System.Resources.ResourceWriter		res_writer					= null ;

			if (currentSelection != null)
			{
				CodeVariableDeclarationStatement	variable ;
				string[]							names						= new String[ctrlInfo.Count] ;
				int									index						= 0 ;
//NS			int									variable_statement_index	= 1 ;
/*NS*/			int									variable_statement_index	= 0 ;
				int									count ;
				CodeMethodInvokeExpression			naming_expr ;
				CodeVariableReferenceExpression[]	naming_params ;
				string								res_filename ;
				ControlEditInfo						info ;
//NS			CodeExpression						create_method ;
//NS			CodeMethodReferenceExpression		create_ref		= new CodeMethodReferenceExpression() ;
//NS			CodeExpression[]					create_params ;
				CodeExpression						initialiser ;
//NS			CodeStatement						context ;
			
//NS			initialiser = new CodeObjectCreateExpression(new CodeTypeReference(typeof(PanelBase.PanelLicenseContext)), new CodeParameterDeclarationExpression[0]) ;
//				context = new CodeVariableDeclarationStatement(typeof(LicenseContext), "context", initialiser) ;
//				Method.Statements.Add(context) ;
				
				//name the resources as the class is named
				res_filename = "KineticaRT.PanelCopying.ControlCopies.resources" ;
//				res_filename = System.IO.Path.Combine(System.IO.Path.GetTempPath(), res_filename) ;
                res_filename = System.IO.Path.Combine(FrameBase.tempFolder.Path, res_filename);

				naming_params = new CodeVariableReferenceExpression[2] ;
				naming_params[0] = new CodeVariableReferenceExpression() ;
				naming_params[1] = new CodeVariableReferenceExpression("Panel") ;
				naming_expr = new CodeMethodInvokeExpression(null, "SetUniqueName", naming_params) ;

//NS			create_ref.MethodName	= "CreateWithContext" ;
//				create_ref.TargetObject = new CodeTypeReferenceExpression(typeof(LicenseManager)) ;
//
//				create_params	= new CodeExpression[2] ;
//				create_params[1]= new CodeVariableReferenceExpression("context") ;

				//go through in z-order
				foreach (System.Windows.Forms.Control ctrl in frameBase.currentPanel.Controls)
				{
					//find corresponding cei
					info = FindCEI(ctrl) ;
					if (info != null && info.selected)
					{
						names[index] = "ctrl" + variable_statement_index ;

						//create and initialise variable for object
//						initialiser = new CodeObjectCreateExpression(new CodeTypeReference(ctrl.GetType()), new CodeParameterDeclarationExpression[0]) ;
//						variable	= new CodeVariableDeclarationStatement(ctrl.GetType(), names[index], initialiser) ;
						//use license manager to get licenced controls
//NS					create_params[0]= new CodeTypeOfExpression(ctrl.GetType()) ;
//						create_method	= new CodeMethodInvokeExpression(create_ref, create_params) ;
//						initialiser		= new CodeCastExpression(ctrl.GetType(), create_method) ;
/*NS*/					initialiser		= new CodeObjectCreateExpression(new CodeTypeReference(ctrl.GetType()),  new CodeParameterDeclarationExpression[0]) ;

						variable		= new CodeVariableDeclarationStatement(ctrl.GetType(), names[index], initialiser) ;
						//add the variable at the beginning of the method
						Method.Statements.Insert(variable_statement_index++, variable) ;

						//save object
                        frameBase.currentPanel.SaveObject(ctrl, names[index], Method, ref variable_statement_index, ref res_writer, res_filename, compact);

						//ensure name is unique
						naming_params[0].VariableName = names[index] ;
						Method.Statements.Add(naming_expr) ;

						index++ ;
					}
				}

//				foreach (ControlEditInfo cei in ctrlInfo)
//				{
//					if (cei.selected && cei != currentSelection)
//					{
////						names[index] = cei.ctrl.Name ;
//						names[index] = "ctrl" + variable_statement_index ;
//
//						//create and initialise variable for object
//						initialiser = new CodeObjectCreateExpression(new CodeTypeReference(cei.ctrl.GetType()), new CodeParameterDeclarationExpression[0]) ;
//						variable	= new CodeVariableDeclarationStatement(cei.ctrl.GetType(), names[index], initialiser) ;
//						//add the variable at the beginning of the method
//						Method.Statements.Insert(variable_statement_index++, variable) ;
//
//						//save object
//						frame.panel.SaveObject(cei.ctrl, names[index], Method, ref variable_statement_index, ref res_writer, res_filename) ;
//
//						//ensure name is unique
//						naming_params[0].VariableName = names[index] ;
//						Method.Statements.Add(naming_expr) ;
//
//						index++ ;
//					}
//				}
//
//				//save current selection last so it remains current selection when pasted
//				names[index] = "ctrl" + variable_statement_index ;
//				initialiser = new CodeObjectCreateExpression(new CodeTypeReference(currentSelection.ctrl.GetType()), new CodeParameterDeclarationExpression[0]) ;
//				variable	= new CodeVariableDeclarationStatement(currentSelection.ctrl.GetType(), names[index], initialiser) ;
//				Method.Statements.Insert(variable_statement_index++, variable) ;
//				frame.panel.SaveObject(currentSelection.ctrl, names[index], Method, ref variable_statement_index, ref res_writer, res_filename) ;
//				naming_params[0].VariableName = names[index] ;
//				Method.Statements.Add(naming_expr) ;
//				index++ ;

				count = index ;

				//add objects to collection
				CodeMethodReferenceExpression	method ;
				CodeExpression[]				parameters ;
				CodeExpression[]				initialisers	= new CodeExpression[count] ;
				
				for(index = 0; index < count; index++)
				{
					initialisers[index] = new CodeVariableReferenceExpression(names[index]) ;
				}

				parameters = new CodeExpression[1] ;
				parameters[0] = new CodeArrayCreateExpression(typeof(System.Windows.Forms.Control), initialisers) ;

				method = new CodeMethodReferenceExpression() ;
				method.MethodName = "AddRange" ;
				method.TargetObject = new CodeVariableReferenceExpression("Panel.Controls") ;
						
				CodeMethodInvokeExpression invoke = new CodeMethodInvokeExpression(method, parameters) ;
				Method.Statements.Add(invoke) ;

				//do we have a resource file?
				if (res_writer != null)
				{
					//we have used the resource file
					CodeStatement					res_manager ;
					CodeExpression					mgr_initialiser ;
					CodeExpression[]				mgr_parameters ;

					//save it...
					res_writer.Close() ;

					//create a resource manager variable to reload resources...
					mgr_parameters = new CodeExpression[1] ;
					mgr_parameters[0] = new CodeTypeOfExpression("ControlCopies") ;
					mgr_initialiser = new CodeObjectCreateExpression(typeof(System.Resources.ResourceManager), mgr_parameters) ;
					res_manager = new CodeVariableDeclarationStatement(typeof(System.Resources.ResourceManager), "resources", mgr_initialiser) ;
				
					Method.Statements.Insert(0, res_manager) ;
				}
			}

			return res_writer  ;
		}

		//moves pasted controls (ie those selected at the moment)
		//with respect to where the mouse is currently pointing
		//- puts currentSelection (top left) as close as possible to MousePos
		private void PositionPastedControls(int XPos, int YPos)
		{
			int			max_left ;		//}
			int			max_right ;		//} how far we can go
			int			max_up ;		//}
			int			max_down ;		//}
			
			int			right_wish ;	//} how far we want to go
			int			down_wish ;		//} (-ve to move left or up)

			//how far can we move?
			GetMaxMovement(out max_left, out max_right, out max_up, out max_down) ;

			//how far do we want to move?
			right_wish	= XPos - currentSelection.ctrl.Location.X ;
			down_wish	= YPos - currentSelection.ctrl.Location.Y ;

			//constrain our wish to the possible movement
			if (right_wish >= 0)	//go right
			{
				if (right_wish > max_right)			right_wish = max_right ;
			}
			else					//go left
			{
				if (-right_wish > max_left)			right_wish = -max_left ;
			}

			if (down_wish >= 0)		//go down
			{
				if (down_wish > max_down)			down_wish = max_down ;
			}
			else					//go up
			{
				if (-down_wish > max_up)			down_wish = -max_up ;
			}

			//move all selected controls
			if (right_wish != 0 || down_wish != 0)
			{
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected)
					{
						if (right_wish != 0)	cei.ctrl.Left += right_wish ;
						if (cei.ctrl.Left + cei.ctrl.Width >= cei.ctrl.Parent.Bounds.Width) cei.ctrl.Left = cei.ctrl.Parent.Bounds.Width - cei.ctrl.Width ;
						if (cei.ctrl.Left < 0) cei.ctrl.Left = 0 ;
						if (down_wish != 0)		cei.ctrl.Top  += down_wish ;
						if (cei.ctrl.Top + cei.ctrl.Height >= cei.ctrl.Parent.Bounds.Height) cei.ctrl.Top = cei.ctrl.Parent.Bounds.Height - cei.ctrl.Height ;
						if (cei.ctrl.Top < 0) cei.ctrl.Top = 0 ;
					}
				}
			}
		}
		#endregion

		#region Formatting (via mouse and via keyboard)
		// Aligns selected objects to current selection
		private void Align (Alignments Alignment)
		{
			bool		multiple		= false ;

			if (currentSelection == null)			return ;

			//are multiple objects selected?
			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected && !cei.Equals(currentSelection))
				{
					//yes - we have at least one selection other than the main one
					multiple = true ;
					break ;
				}
			}

			//nothing to do if only one thing selected
			if (!multiple)			return ;

			//add a new 'undo' every alignment
			moveUndoAdded = false ;
			AddMoveUndo() ;

			switch (Alignment)
			{
				case Alignments.Top:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Top = currentSelection.ctrl.Top ;
						}
					}
				}
				break ;

				case Alignments.Left:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Left = currentSelection.ctrl.Left ;
						}
					}
				}
				break ;

				case Alignments.Bottom:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Top = currentSelection.ctrl.Bottom - cei.ctrl.Height ;
						}
					}
				}
				break ;

				case Alignments.Right:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Left = currentSelection.ctrl.Right - cei.ctrl.Width ;
						}
					}
				}
				break ;

				case Alignments.HCentre:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Left = currentSelection.ctrl.Left + 
									(currentSelection.ctrl.Width - cei.ctrl.Width) / 2 ;
						}
					}
				}
				break ;

				case Alignments.VCentre:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Top = currentSelection.ctrl.Top + 
									(currentSelection.ctrl.Height - cei.ctrl.Height) / 2 ;
						}
					}
				}
				break ;
			}

			DrawHandles() ;
		}

		// Makes selected objects same size as current selection
		private void MakeSameSize(Sizings Sizing)
		{
			bool		multiple		= false ;

			if (currentSelection == null)			return ;

			//are multiple objects selected?
			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected && !cei.Equals(currentSelection))
				{
					//yes - we have at least one selection other than the main one
					multiple = true ;
					break ;
				}
			}

			//nothing to do if only one thing selected
			if (!multiple)			return ;

			//add a new 'undo' every sizing
			moveUndoAdded = false ;
			AddMoveUndo() ;

			switch (Sizing)
			{
				case Sizings.Width:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Width = currentSelection.ctrl.Width ;
						}
					}
				}
				break ;

				case Sizings.Height:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Height = currentSelection.ctrl.Height ;
						}
					}
				}
				break ;

				case Sizings.Both:
				{
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected && !cei.Equals(currentSelection))
						{
							cei.ctrl.Width = currentSelection.ctrl.Width ;
							cei.ctrl.Height = currentSelection.ctrl.Height ;
						}
					}
				}
				break ;
			}

			DrawHandles() ;
		}

		// Stacks selection: currentSelection becomes top / left of stack,
		// others are sorted in order of cei
		// Leaves grid spacing in between controls
		private void Stack(bool Horizontal)
		{
			bool		multiple	= false ;
			int			size ;					//running total of widths or heights
			int			spacing ;

			if (currentSelection == null)			return ;

			//are multiple objects selected?
			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected && !cei.Equals(currentSelection))
				{
					//yes - we have at least one selection other than the main one
					multiple = true ;
					break ;
				}
			}

			//nothing to do if only one thing selected
			if (!multiple)			return ;

			//add a new 'undo' every stacking
			moveUndoAdded = false ;
			AddMoveUndo() ;

			if (Horizontal)
			{
				spacing = frameBase.HorizontalGrid.Spacing ;
				size = currentSelection.ctrl.Width + spacing ;

				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected && !cei.Equals(currentSelection))
					{
						cei.ctrl.Left = currentSelection.ctrl.Left + size ;
						size += cei.ctrl.Width + spacing ;
					}
				}
			}
			else
			{
				spacing = frameBase.VerticalGrid.Spacing ;
				size = currentSelection.ctrl.Height + spacing ;

				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected && !cei.Equals(currentSelection))
					{
						cei.ctrl.Top = currentSelection.ctrl.Top + size ;
						size += cei.ctrl.Height + spacing ;
					}
				}
			}

			DrawHandles() ;
		}

		// Stacks selection: currentSelection becomes top / left of stack,
		// others are sorted in order of cei
		// Leaves grid spacing in between controls
		private void Rename()
		{
			if (frameBase.currentPanel == null)			return ;
			if (currentSelection.ctrl == null)			return ;
			
//			//add a new 'undo' every stacking
//			moveUndoAdded = false ;
//			AddMoveUndo() ;

			RenameForm form = new RenameForm (currentSelection.ctrl.Name) ;
			System.Windows.Forms.DialogResult result = form.ShowDialog(this) ;
			
			if (result == System.Windows.Forms.DialogResult.Cancel) return ; 

			string new_name = form.NewName ;

			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected)
				{
					cei.ctrl.Name = frameBase.currentPanel.GetUniqueName(new_name) ;
				}
			}
		}

		// Sends selected controls(s) to front / back
		// If multiple selections, does in order of cei list; not undoable
		private void ZOrder(bool ToFront)
		{
			if (currentSelection == null)			return ;

			if (ToFront)
			{
				//##go through in reverse order?
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected)
					{
						cei.ctrl.BringToFront() ;
					}
				}
			}
			else		//to back
			{
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected)
					{
						cei.ctrl.SendToBack() ;
					}
				}
			}
		}

		private void ToGrid(GridFormats Format)
		{
			if (frameBase.currentPanel == null)			return ;

			int			new_pos ;

			if (currentSelection == null)			return ;

			//add a new 'undo' every formatting
			moveUndoAdded = false ;
			AddMoveUndo() ;

			switch (Format)
			{
				case GridFormats.Align:
				{
					//update positions
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected)
						{
							new_pos = cei.ctrl.Left ;
							if (MoveToGridPosition(ref new_pos, cei.ctrl.Width, frameBase.HorizontalGrid.Pixels, frameBase.currentPanel.DisplayRectangle.Left, frameBase.currentPanel.DisplayRectangle.Right))
							{
								cei.ctrl.Left = new_pos ;
							}

							new_pos = cei.ctrl.Top ;
							if (MoveToGridPosition(ref new_pos, cei.ctrl.Height, frameBase.VerticalGrid.Pixels, frameBase.currentPanel.DisplayRectangle.Top, frameBase.currentPanel.DisplayRectangle.Bottom))
							{
								cei.ctrl.Top = new_pos ;
							}
						}
					}
				}
				break ;

				case GridFormats.Size:
				{
					int		min_width		= frameBase.HorizontalGrid.MinSize ;
					int		min_height		= frameBase.VerticalGrid.MinSize ;
					int		max_width		= frameBase.currentPanel.DisplayRectangle.Width ;
					int		max_height		= frameBase.currentPanel.DisplayRectangle.Height ;

					//update positions
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected)
						{
							new_pos = cei.ctrl.Width ;
							if (SizeToGrid(cei.ctrl.Left, ref new_pos, min_width, max_width, frameBase.HorizontalGrid))
							{
								cei.ctrl.Width = new_pos ;
							}

							new_pos = cei.ctrl.Height ;
							if (SizeToGrid(cei.ctrl.Top, ref new_pos, min_height, max_height, frameBase.VerticalGrid))
							{
								cei.ctrl.Height = new_pos ;
							}
						}
					}
				}
				break ;

				//aligns and sizes to grid: more efficient to copy the code
				//into a single loop than call both the above loops
				case GridFormats.Snap:
				{
					int		min_width		= frameBase.HorizontalGrid.MinSize ;
					int		min_height		= frameBase.VerticalGrid.MinSize ;
					int		max_width		= frameBase.currentPanel.DisplayRectangle.Width ;
					int		max_height		= frameBase.currentPanel.DisplayRectangle.Height ;

					//update positions
					foreach (ControlEditInfo cei in ctrlInfo)
					{
						if (cei.selected)
						{
							new_pos = cei.ctrl.Left ;
							if (MoveToGridPosition(ref new_pos, cei.ctrl.Width, frameBase.HorizontalGrid.Pixels, frameBase.currentPanel.DisplayRectangle.Left, frameBase.currentPanel.DisplayRectangle.Right))
							{
								cei.ctrl.Left = new_pos ;
							}

							new_pos = cei.ctrl.Top ;
							if (MoveToGridPosition(ref new_pos, cei.ctrl.Height, frameBase.VerticalGrid.Pixels, frameBase.currentPanel.DisplayRectangle.Top, frameBase.currentPanel.DisplayRectangle.Bottom))
							{
								cei.ctrl.Top = new_pos ;
							}

							new_pos = cei.ctrl.Width ;
							if (SizeToGrid(cei.ctrl.Left, ref new_pos, min_width, max_width, frameBase.HorizontalGrid))
							{
								cei.ctrl.Width = new_pos ;
							}

							new_pos = cei.ctrl.Height ;
							if (SizeToGrid(cei.ctrl.Top, ref new_pos, min_height, max_height, frameBase.VerticalGrid))
							{
								cei.ctrl.Height = new_pos ;
							}
						}
					}
				}
				break ;
			}

			DrawHandles() ;
		}

		// Moves selected controls one grid unit in the direction specified (Keys.Up etc)
		// Uses grid size whether enabled or not
		private void KeyMove(Keys KeyCode)
		{
			if (frameBase.currentPanel == null)		return ;
			if (currentSelection == null)			return ;

			int			v_move			= 0 ;
			int			h_move			= 0 ;
			int			max_left ;					//max left movement
			int			max_right ;					//max right movement
			int			max_up ;					//max upward movement
			int			max_down ;					//max downward movement
			int			grid_unit ;

			grid_unit = (KeyCode == Keys.Up || KeyCode == Keys.Down) ? gridActive ? frameBase.VerticalGrid.Size : 1 : gridActive ? frameBase.HorizontalGrid.Size : 1 ;

			//how far can we move?
			GetMaxMovement(out max_left, out max_right, out max_up, out max_down) ;

			switch (KeyCode)
			{
				case Keys.Up:
				{
					int			new_top		= currentSelection.ctrl.Top ;
					
					//if not on the grid, move to it (in the correct direction)
					if (MoveToGridPosition(ref new_top, currentSelection.ctrl.Height, grid_unit, frameBase.currentPanel.DisplayRectangle.Top, frameBase.currentPanel.DisplayRectangle.Bottom))
					{
						v_move = new_top - currentSelection.ctrl.Top ;

						//have we gone the right way?
						if (v_move > 0)					v_move -= grid_unit ;

						//is it a do-able distance?
						if (v_move < (-max_up))			v_move = 0 ;
					}
					else if (max_up >= grid_unit)
					{
						//already on grid: move one grid unit
						v_move = - grid_unit ;
					}
				}
				break ;
		
				case Keys.Down:
				{
					int		new_top		= currentSelection.ctrl.Top ;

					if (MoveToGridPosition(ref new_top, currentSelection.ctrl.Height, grid_unit, frameBase.currentPanel.DisplayRectangle.Top, frameBase.currentPanel.DisplayRectangle.Bottom))
					{
						v_move = new_top - currentSelection.ctrl.Top ;

						//have we gone the right way?
						if (v_move < 0)				v_move += grid_unit ;

						//is it a do-able distance?
						if (v_move > max_down)		v_move = 0 ;
					}
					else if (max_down >= grid_unit)
					{
						//already on grid: move one grid unit
						v_move = grid_unit ;
					}
				}
				break ;

				case Keys.Left:
				{
					int		new_left		= currentSelection.ctrl.Left ;

					//if not on the grid, move to it (in the correct direction)
					if (MoveToGridPosition(ref new_left, currentSelection.ctrl.Width, grid_unit, frameBase.currentPanel.DisplayRectangle.Left, frameBase.currentPanel.DisplayRectangle.Right))
					{
						h_move = new_left - currentSelection.ctrl.Left ;

						//have we gone the right way?
						if (h_move > 0)					h_move -= grid_unit ;

						//is it a do-able distance?
						if (h_move < (-max_left))		h_move = 0 ;
					}
					else if (max_left >= grid_unit)
					{
						//already on grid: move one grid unit
						h_move = -grid_unit ;
					}
				}
				break ;

				case Keys.Right:
				{
					int		new_left		= currentSelection.ctrl.Left ;

					//if not on the grid, move to it (in the correct direction)
					if (MoveToGridPosition(ref new_left, currentSelection.ctrl.Width, grid_unit, frameBase.currentPanel.DisplayRectangle.Left, frameBase.currentPanel.DisplayRectangle.Right))
					{
						h_move = new_left - currentSelection.ctrl.Left ;

						//have we gone the right way?
						if (h_move < 0)					h_move += grid_unit ;

						//is it a do-able distance?
						if (h_move > max_right)			h_move = 0 ;
					}
					else if (max_right >= grid_unit)
					{
						//already on grid: move one grid unit
						h_move = grid_unit ;
					}
				}
				break ;
			}

			if (h_move != 0 || v_move != 0)
			{
				AddMoveUndo() ;

				//move all selected objects - including currentSelection
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected)
					{
						if (h_move != 0)			cei.ctrl.Left += h_move ;
						if (v_move != 0)			cei.ctrl.Top += v_move ;
					}
				}

				DrawHandles() ;
			}
		}

		// Resizes selected controls to grid size (minus spacing), then subsequently by 
		// one grid unit in the direction specified (Keys.Up etc)
		// Uses grid size whether enabled or not
		private void KeyResize(Keys KeyCode)
		{
			if (currentSelection == null)		return ;

			int			width_move		= 0 ;
			int			height_move		= 0 ;
			int			max_west ;
			int			max_east ;
			int			max_width ;
			int			max_north ;
			int			max_south ;
			int			max_height ;
			int			grid_unit ;
			
            grid_unit = (KeyCode == Keys.Up || KeyCode == Keys.Down) ? gridActive ? frameBase.VerticalGrid.Size : 1 : gridActive ? frameBase.HorizontalGrid.Size : 1;

			GetMaxResizing(out max_west, out max_east, out max_width, out max_north, out max_south, out max_height) ;

			switch (KeyCode)
			{
				case Keys.Up:
				{
					int		height		= currentSelection.ctrl.Height + frameBase.VerticalGrid.Spacing ;
			
					if ((height % grid_unit) != 0)
					{
						//not on grid
						height = (height / grid_unit) * grid_unit ;
					}
					else		//on (spaced) grid: shrink by one unit
					{
						//height = ((height / grid_unit) - 1) * grid_unit ;
						height -= grid_unit ;
					}

					height_move = height - currentSelection.ctrl.Height - frameBase.VerticalGrid.Spacing ;

					//check move is in correct direction and in range
					if (height_move < -max_height || height_move > 0)
					{
						height_move = 0 ;
					}
				}
				break ;

				case Keys.Down:
				{
					int		height		= currentSelection.ctrl.Height + frameBase.VerticalGrid.Spacing ;

					//put on to next grid point
					height = ((height / grid_unit) + 1) * grid_unit ;

					height_move = height - currentSelection.ctrl.Height - frameBase.VerticalGrid.Spacing ;

					//check move is in correct direction and in range
					if (height_move > max_south || height_move < 0)
					{
						height_move = 0 ;
					}
				}
				break ;
		
				case Keys.Left:
				{
					int		width		= currentSelection.ctrl.Width + frameBase.HorizontalGrid.Spacing ;
			
					if ((width % grid_unit) != 0)
					{
						//not on grid
						width = (width / grid_unit) * grid_unit ;
					}
					else		//on (spaced) grid
					{
						width = ((width / grid_unit) - 1) * grid_unit ;
					}

					width_move = width - currentSelection.ctrl.Width - frameBase.HorizontalGrid.Spacing ;

					//check move is in correct direction and in range
					if (width_move < -max_width || width_move > 0)
					{
						width_move = 0 ;
					}
				}
				break ;

				case Keys.Right:
				{
					int		width		= currentSelection.ctrl.Width + frameBase.HorizontalGrid.Spacing ;
			
					width = ((width / grid_unit) + 1) * grid_unit ;

					width_move = width - currentSelection.ctrl.Width - frameBase.HorizontalGrid.Spacing ;

					//check move is in correct direction and in range
					if (width_move > max_east || width_move < 0)
					{
						width_move = 0 ;
					}
				}
				break ;
			}

			if (width_move != 0 || height_move != 0)
			{
				AddMoveUndo() ;

				//resize all selected objects
				foreach (ControlEditInfo cei in ctrlInfo)
				{
					if (cei.selected)
					{
						if (width_move != 0)		cei.ctrl.Width += width_move ;
						if (height_move != 0)		cei.ctrl.Height += height_move ;
					}
				}

				DrawHandles() ;
			}
		}

		// Aligns selected controls lefts or tops etc
		private void KeyAlign(Keys KeyCode)
		{
			Alignments		alignment ;

			switch (KeyCode)
			{
				case Keys.Left:		alignment = Alignments.Left;		break;
				case Keys.Right:	alignment = Alignments.Right;		break;
				case Keys.Up:		alignment = Alignments.Top;			break;
				default:			alignment = Alignments.Bottom;		break ;
			}

			Align(alignment) ;
		}

		// Select next or previous control in cei list
		private void NextControl(bool Next, bool Tab)
		{
			if (currentSelection == null)		return ;

			bool			changed			= false ;
			int				current_index	= ctrlInfo.IndexOf(currentSelection) ;

			if (!Tab)
			{
				if (Next)		//go on to next control
				{
					if (current_index < ctrlInfo.Count - 1)
					{
						ClearSelections() ;
						currentSelection = (ControlEditInfo)ctrlInfo[current_index + 1] ;
						currentSelection.selected = true ;
						changed = true ;
					}
				}
				else			//go back to previous control
				{
					if (current_index > 0)
					{
						ClearSelections() ;
						currentSelection = (ControlEditInfo)ctrlInfo[current_index - 1] ;
						currentSelection.selected = true ;
						changed = true ;
					}
				}
			}
			else
			{
				if (ctrlInfo.Count > 1)
				{
					ClearSelections() ;

					if (Next)		//go on to next control - loop if reached end
					{
						if (current_index < ctrlInfo.Count - 1)
						{
							currentSelection = (ControlEditInfo)ctrlInfo[current_index + 1] ;
						}
						else
						{
							currentSelection = (ControlEditInfo)ctrlInfo[0] ;
						}
					}
					else			//go back to previous control - loop if reached end
					{
						if (current_index > 0)
						{
							currentSelection = (ControlEditInfo)ctrlInfo[current_index - 1] ;
						}
						else
						{
							currentSelection = (ControlEditInfo)ctrlInfo[ctrlInfo.Count - 1] ;
						}
					}

					currentSelection.selected = true ;
					changed = true ;
				}
			}

			if (changed)
			{
				DrawHandles() ;
			}
		}
		#endregion

		#region Undo
		private void AddMoveUndo()
		{
			if (!moveUndoAdded)
			{
				AddUndo(UndoTypes.Move) ;
			}
		}

		private void AddUndo(UndoTypes Type)
		{
			UndoInfo		info ;
			int				count		= 0 ;
			
			//should never get here if nothing selected, but just in case...
			if (currentSelection == null)		return ;

			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected)		count++ ;
			}

			//create it...
			info = new UndoInfo(Type, count) ;

			//set up the information in it (currentSelection first)...
			info.cei[0] = currentSelection ;
			if (Type == UndoTypes.Move)
			{
				info.sizes[0]		= currentSelection.ctrl.Size ;
				info.locations[0]	= currentSelection.ctrl.Location ;
				
				moveUndoAdded = true ;
			}
			else
			{
				//a different undo type means we can add a new move undo
				moveUndoAdded = false ;
			}
			count = 1 ;

			foreach (ControlEditInfo cei in ctrlInfo)
			{
				if (cei.selected && !cei.Equals(currentSelection))
				{
					info.cei[count] = cei ;
					if (Type == UndoTypes.Move)
					{
						info.sizes[count]		= cei.ctrl.Size ;
						info.locations[count]	= cei.ctrl.Location ;
					}
					count++ ;
				}
			}

			//add it to the list...
			undoInfo.Add(info) ;

			//remove the oldest one if required...
			if (undoInfo.Count > MAX_UNDOS)
			{
				undoInfo.RemoveAt(0) ;
			}
		}

		//undoes most recent change in list (if any)
		private void Undo()
		{
			if (frameBase.currentPanel == null)		return ;

			if (undoInfo.Count > 0)
			{
				UndoInfo	info		= (UndoInfo)undoInfo[undoInfo.Count - 1] ;

				//clear selections - will reset as required by undo
				ClearSelections() ;

				switch (info.type)
				{
					case UndoTypes.Add:
					{
						//remove all added controls (from frame and my ctrlInfo list)
						codeDelete = true ;
						for (int index = 0; index < info.cei.Length; index++)
						{
							frameBase.currentPanel.Controls.Remove(info.cei[index].ctrl) ;
							info.cei[index].ctrl.Dispose() ;
							ctrlInfo.Remove(info.cei[index]) ;
						}
						codeDelete = false ;
					}
					break ;

					case UndoTypes.Delete:
					{
						//add and select all deleted controls 
						//(to frame and my ctrlInfo list)
						codeAdd = true ;
						for (int index = 0; index < info.cei.Length; index++)
						{
							frameBase.currentPanel.Controls.Add(info.cei[index].ctrl) ;
							ctrlInfo.Add(info.cei[index]) ;
							info.cei[index].selected = true ;
						}
						currentSelection = info.cei[0] ;
						codeAdd = false ;
					}
					break ;

					case UndoTypes.Move:
					{
						//move all the controls back to where they started 
						//(size and position) and select them
						for (int index = 0; index < info.cei.Length; index++)
						{
							info.cei[index].ctrl.Size		= info.sizes[index] ;
							info.cei[index].ctrl.Location	= info.locations[index] ;
							info.cei[index].selected		= true ;
						}
						currentSelection = info.cei[0] ;
					}
					break ;
				}

				//remove undo from list, as it has now been undone
				moveUndoAdded = false ;
				//##have a 'dispose' to clear out info that should be cleared out?
				// - think if need to
				undoInfo.Remove(info) ;

				DrawHandles() ;
			}
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
				ContextMenu = null ;

				dragObject = PointToChild(e.X, e.Y) ;

				if (dragObject == null)
				{
					//clicked on background - allow rubber band to start
					almostRubberBand = true ;
					Capture = true ;

					bandRect.X = e.X ;
					bandRect.Y = e.Y ;
					bandRect.Width = 0 ;
					bandRect.Height = 0 ;
				}
				//else clicked on a control
			}
			else if (e.Button == MouseButtons.Left)
			{
				if (e.Clicks > 1)
				{
					//Double Click
					dragObject = PointToChild(e.X, e.Y) ;
					ShowProperties(false) ;
				}
				else if (currentCorner != Corners.Invalid && currentSelection != null)
				{
					//pointing at resize handle: start a resize
					dragging			= true ;		//skip the 'almost' stage
					moving				= false ;
					Capture				= true ;
					dragStartPoint		= new Point(e.X, e.Y) ;
					dragReferencePoint	= currentSelection.ctrl.Location ;
					dragReferenceSize	= currentSelection.ctrl.Size ;
				}
				else
				{
					//have we clicked on a control?
					dragObject = PointToChild(e.X, e.Y) ;

					if (dragObject != null)
					{
						//start a drag (move) on currently selected controls
						almostDragging		= true ;
						Capture				= true ;
						moving				= true ;
						dragStartPoint		= new Point(e.X, e.Y) ;
					}
					else	//clicked on background - allow rubber band to start
					{
						almostRubberBand = true ;
						Capture = true ;

						bandRect.X = e.X ;
						bandRect.Y = e.Y ;
						bandRect.Width = 0 ;
						bandRect.Height = 0 ;
					}
				}

				DrawHandles() ;
			}
			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (almostRubberBand)
			{
				//have we moved far enough to be an actual rubber band?
				if (Math.Abs(e.X - bandRect.X) > 4 || Math.Abs(e.Y - bandRect.Y) > 4)
				{
					almostRubberBand = false ;
					rubberBand = true ;
				}
			}

			if (rubberBand)
			{
				Graphics	g		= CreateGraphics() ;

				//are we holding the shift key? if not, clear current selections
				if (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift))
				{
					ClearSelections() ;
				}

				//undraw...
				DrawRubberBand(g) ;

				//reset band size
				bandRect.Width = e.X - bandRect.X ;
				bandRect.Height = e.Y - bandRect.Y ;
				
				//draw...
				DrawRubberBand(g) ;
				g.Dispose() ;
			}
			else
			{
				if (almostDragging)
				{
					//have we now moved far enough to count as a drag not just a click?
					if (Math.Abs(e.X - dragStartPoint.X) > 2 || Math.Abs(e.Y - dragStartPoint.Y) > 2)
					{
						almostDragging = false ;
						dragging = true ;

						//ensure clicked on object is selected and other selections are up to date
						if (!dragObject.selected)
						{
							if (!((Control.ModifierKeys & Keys.Shift) == Keys.Shift) &&
								!((Control.ModifierKeys & Keys.Control) == Keys.Control)	)
							{
								//only have new object selected
								ClearSelections() ;
								dragObject.selected = true ;
								currentSelection = dragObject ;
							}
							else
							{
								//add to selection
								dragObject.selected = true ;
								currentSelection = dragObject ;
								moveUndoAdded = false ;
							}
						}
						//else no change to selections

						dragReferencePoint	= currentSelection.ctrl.Location ;
						dragReferenceSize	= currentSelection.ctrl.Size ;
					}
				}

				if (dragging)
				{
					//a drag has begun - disallow rubber band
					if (almostRubberBand)
					{
						almostRubberBand = false ;
					}

					if (currentSelection != null)
					{
						if (Capture)
						{
							if (moving)
							{
								DoMove(e.X, e.Y) ;
							}
							else		//resizing
							{
								DoResize(e.X, e.Y) ;
							}
						}
					}
				}
			}

			if (!rubberBand && !dragging)
			{
				SetCursor(e.X, e.Y) ;
			}
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (almostRubberBand)			almostRubberBand = false ;
			if (almostDragging)				almostDragging = false ;

			if (rubberBand)
			{
				Graphics			g		= CreateGraphics() ;

				//undraw...
				DrawRubberBand(g) ;
				g.Dispose() ;

				rubberBand = false ;

				//select all that are fully inside band
				BandSelections(bandRect, e.Button) ;
			}
			else
			{
				SetSelections(dragObject, e.Button == MouseButtons.Left) ;

				//this may do this more often than actually required
				if (editor != null && editor.Visible)		ShowProperties(false) ;

				if (dragging)			dragging = false ;
				
				if (e.Button == MouseButtons.Right && dragObject == null)
				{
					//bring up a menu
					menu.Show(this, new Point(e.X, e.Y)) ;
				}
			}

            // NS04June2008 - removed
            ////force property editor to be on top
            //if (editor != null && editor.Visible)
            //{
            //    editor.BringToFront() ; 
            //}

			if (ContextMenu == null)		ContextMenu = menu ;

			Capture = false ;
			dragObject = null ;
		}
		
		/// <summary>
		/// Mouse Enter Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			menuShowing = false ;
			Capture = false ;
			base.OnMouseEnter(e) ;
		}

		/// <summary>
		/// Mouse Leave Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e) ;

			//ensure will get a MouseEnter once menu is finished with
			if (menuShowing)			this.Capture = true ;
		}
		#endregion

		#region Other Events
		/// <summary>
		/// Activated Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnActivated(EventArgs e)
		{
			Point			pt		= PointToClient(Cursor.Position) ;

			menuShowing = false ;
			
			if (!ClientRectangle.Contains(pt))
			{
				Capture = true ;
			}

			base.OnActivated(e) ;
		}

		/// <summary>
		/// Key Down event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
//				case Keys.Apps:		//right click menu key
//				{
//					menu.Show(this, this.PointToClient(Cursor.Position)) ;
//					e.Handled = true ;
//				}
//				break ;

				case Keys.Up:
				case Keys.Down:
				case Keys.Left:
				case Keys.Right:
				{
					//assume it will be handled (unless proved otherwise)
					e.Handled = true ;

					if (!e.Control && !e.Shift && !e.Alt)
					{
						KeyMove(e.KeyCode) ;
					}
					else if (e.Shift && !e.Control && !e.Alt)
					{
						KeyResize(e.KeyCode) ;
					}
					else if (e.Alt && !e.Control&& !e.Shift)
					{
						NextControl((e.KeyCode == Keys.Down || e.KeyCode == Keys.Right), false) ;
					}
					else if (e.Control && !e.Shift && !e.Alt)
					{
						KeyAlign(e.KeyCode) ;
					}
					else
					{
						//unhandled combination pressed
						e.Handled = false ;
					}
				}
				break ;

				case Keys.Escape:		//clear all selections (no modifiers)
				{
					if (!e.Control && !e.Shift && !e.Alt)
					{
						ClearSelections() ;
						DrawHandles() ;
						e.Handled = true ;
					}
				}
				break ;

				case Keys.Return:		//(with alt) show property pages
				{
					if (!e.Control && !e.Shift && e.Alt)
					{
						ShowProperties(false) ;
						e.Handled = true ;
					}
				}
				break ;

				case Keys.Tab:		//go on to next or back to previous control
				{
					if (!e.Control && !e.Alt)
					{
						NextControl(!e.Shift, true) ;
					}
				}
				break ;
			}

			base.OnKeyDown(e) ;
		}

		/// <summary>
		/// Closed event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnClosed(EventArgs e)
		{
			if (frameBase.currentPanel != null)
			{
				frameBase.currentPanel.ControlAdded		-= addedHdlr ;
				frameBase.currentPanel.ControlRemoved	-= removedHdlr ;
			}
			frameBase.PropertyChanged		-= propertyHdlr ;

			frameBase.Resize				-= relocatedHdlr ;
			frameBase.LocationChanged		-= relocatedHdlr ;
			Owner.LocationChanged			-= relocatedHdlr ;

			base.OnClosed(e) ;
		}

		private void OnRelocated(object sender, EventArgs e)
		{
			if (frameBase.Parent != null) // Required in case of orphanage when Frame deleted?
			{
				this.Location = frameBase.Parent.PointToScreen(frameBase.Location) ;
				this.Size = frameBase.Size ;
			}
		}

//		protected override void OnEnter(EventArgs e)
//		{
//			base.OnEnter(e) ;
//		}
//		
//		protected override void OnLeave(EventArgs e)
//		{
//			base.OnLeave(e) ;
//		}
//
//		protected override void OnGotFocus(EventArgs e)
//		{
//			base.OnGotFocus(e) ;
//		}
//
//		protected override void OnLostFocus(EventArgs e)
//		{
//			base.OnLostFocus(e) ;
//		}
		#endregion (events)

		#region Menu selection handling
		//menu is just about to be shown - sort out which bits need
		//to be hidden / disabled
		private void OnMenuPopup(object sender, EventArgs e)
		{
			bool	over_frame ;
			bool	multiple		= MultipleItemsSelected() ;
			bool	selection		= (currentSelection != null) ;

			click = this.PointToClient(Cursor.Position) ;
			over_frame = (PointToChild(click.X, click.Y) == null) ;
			
			menuShowing = true ;

			//show or hide bits specific to being over the frame
			menu.MenuItems[(int)FrameMenu.ExitEditMode].Visible = over_frame ;
			menu.MenuItems[(int)FrameMenu.ExitBar].Visible		= over_frame ;
			menu.MenuItems[(int)FrameMenu.Add].Visible			= over_frame ;
			menu.MenuItems[(int)FrameMenu.AddComponent].Visible	= over_frame ;
			menu.MenuItems[(int)FrameMenu.AddBar].Visible		= over_frame ;
			menu.MenuItems[(int)FrameMenu.SelectAll].Visible	= over_frame ;
			menu.MenuItems[(int)FrameMenu.SelectBar].Visible	= over_frame ;
			menu.MenuItems[(int)FrameMenu.Save].Visible			= over_frame ;
			menu.MenuItems[(int)FrameMenu.SaveBar].Visible		= over_frame ;
			menu.MenuItems[(int)FrameMenu.Rename].Visible		= !over_frame ;
            

			menu.MenuItems[(int)FrameMenu.AdvProperties].Visible= over_frame ;

			//disable/enable other bits
			if (over_frame)
			{
				//can only save if got name to save to
				menu.MenuItems[(int)FrameMenu.Save].Enabled = frameBase.CanSave() ;
			}

			//can only undo if got an undo
			menu.MenuItems[(int)FrameMenu.Undo].Enabled = (undoInfo.Count > 0) ;

			//can only paste if got relevant info in clipboard
			menu.MenuItems[(int)FrameMenu.Paste].Enabled = Clipboard.GetDataObject().GetDataPresent("FrameFormat") ;

			menu.MenuItems[(int)FrameMenu.Cut].Enabled = selection ;
			menu.MenuItems[(int)FrameMenu.Copy].Enabled = selection ;
			menu.MenuItems[(int)FrameMenu.Delete].Enabled = selection ;

            foreach (ControlEditInfo info in ctrlInfo)
            {
                if (info.selected)
                {
                    IConnectable ctrl = info.ctrl as IConnectable;
                    if (ctrl != null && ctrl.Connected)
                    {
                        menu.MenuItems[(int)FrameMenu.DisconnectControl].Visible = selection; //ft
                        menu.MenuItems[(int)FrameMenu.DisconnectControl].Enabled = selection; //ft
                        menu.MenuItems[(int)FrameMenu.ConnectControl].Visible = false; //ft
                        
                    }
                    else
                    {
                        menu.MenuItems[(int)FrameMenu.DisconnectControl].Visible = false; //ft
                        menu.MenuItems[(int)FrameMenu.ConnectControl].Visible = selection; //ft
                        menu.MenuItems[(int)FrameMenu.ConnectControl].Enabled = selection; //ft
                    }
                }
            }

            //formatting sub-items...
			foreach (MenuItem item in menu.MenuItems[(int)FrameMenu.Align].MenuItems)
			{
				item.Enabled = multiple ;
			}

			foreach (MenuItem item in menu.MenuItems[(int)FrameMenu.MakeSameSize].MenuItems)
			{
				item.Enabled = multiple ;
			}

			foreach (MenuItem item in menu.MenuItems[(int)FrameMenu.Stack].MenuItems)
			{
				item.Enabled = multiple ;
			}

			foreach (MenuItem item in menu.MenuItems[(int)FrameMenu.Layout].MenuItems)
			{
				item.Enabled = selection ;
			}
		}

		//'Exit Edit Mode'
		private void OnClickEditMode(object sender, EventArgs e)
		{
			menuShowing = false ;
			this.Close() ;
		}

		//'Properties'
		private void OnClickProperties(object sender, EventArgs e)
		{
			menuShowing = false ;

			ShowProperties(false) ;
		}

		//'Rename'
		private void OnClickRename(object sender, EventArgs e)
		{
			menuShowing = false ;

			Rename () ;
		}
			
		//'Advanced Properties'
		private void OnClickAdvancedProperties(object sender, EventArgs e)
		{
			menuShowing = false ;

			ShowProperties(true) ;
		}

        //'GridEnable'
        private void OnGridEnable(object sender, EventArgs e)
        {
            gridActive = ! gridActive;
        }

        private void OnClickDelete(object sender, EventArgs e)
		{
			menuShowing = false ;

			if (frameBase.currentPanel == null)		return ;

			//create a 'delete' undo
			AddUndo(UndoTypes.Delete) ;

			//remove all selected controls from frame and ctrlInfo
			//but don't dispose of them
			codeDelete = true ;
			for (int index = 0; index < ctrlInfo.Count;)
			{
				ControlEditInfo		info		= (ControlEditInfo)ctrlInfo[index] ;

				if (info.selected)
				{
					frameBase.currentPanel.Controls.Remove(info.ctrl) ;
					ctrlInfo.RemoveAt(index) ;
				}
				else
				{
					index++ ;
				}
			}
			currentSelection = null ;
			codeDelete = false ;

			DrawHandles() ;
		}

        private void OnClickCut(object sender, EventArgs e)
		{
			menuShowing = false ;

			OnClickCopy(sender, e) ;
			OnClickDelete(sender, e) ;
		}

		private void OnClickCopy(object sender, EventArgs e)
		{
			menuShowing = false ;
			if (currentSelection != null)
			{
				busy = true ;
				Cursor = Cursors.WaitCursor ;

				//Create our namespace, class and initialise function.
				CodeCompileUnit		compileUnit		= new CodeCompileUnit () ;
				CodeNamespace		nameSpace		= new CodeNamespace ("KineticaRT.PanelCopying") ;
				CodeTypeDeclaration panel			= new CodeTypeDeclaration("ControlCopies");
				CodeMemberMethod	add_copies		= new CodeMemberMethod () ;

				panel.BaseTypes.Add(typeof(ControlCopyBase)) ;
				add_copies.Name = "AddCopies" ;
				add_copies.Attributes = MemberAttributes.Public | MemberAttributes.Override ;
				add_copies.Parameters.Add(new CodeParameterDeclarationExpression(typeof(PanelBase), "Panel")) ;

				// Stick them all together.
				compileUnit.Namespaces.Add (nameSpace) ;
				nameSpace.Types.Add(panel);
				panel.Members.Add (add_copies) ;

				// Add all the initialisation code.
				System.Resources.ResourceWriter resources = SaveSelectedControls(add_copies, false) ;

				if (resources != null)
				{
					compileUnit.UserData.Add("Res", true) ;
				}

				//compilation and error checking will be done in Paste
				//put the results on the clipboard
				DataObject		data	= new DataObject("FrameFormat", compileUnit) ;
				Clipboard.SetDataObject(data) ;

				busy = false ;
				SetCursor(Cursor.Position.X, Cursor.Position.Y) ;
			}
		}

		private void OnClickPaste(object sender, EventArgs e)
		{
			Point		mouse_pos ;

			if (menuShowing)
			{
				mouse_pos = click ;
				menuShowing = false ;
			}
			else
			{
				mouse_pos = this.PointToClient(Cursor.Position) ;
			}

			if (frameBase.currentPanel == null)		return ;

			//paste from clipboard
			IDataObject			iobj = Clipboard.GetDataObject() ;

			if (iobj.GetDataPresent("FrameFormat", false))
			{
				object				data		= iobj.GetData("FrameFormat", false) ;
				CodeCompileUnit		unit		= data as CodeCompileUnit ;

				if (unit != null)
				{
					busy = true ;
					Cursor = Cursors.WaitCursor ;


					// Create a code generator.
					CodeDomProvider			provider	= new Microsoft.CSharp.CSharpCodeProvider ();
					//ICodeGenerator			generator	= provider.CreateGenerator() ;
					CodeGeneratorOptions	options		= new CodeGeneratorOptions () ;
					options.BracingStyle = "C" ;
					options.BlankLinesBetweenMembers = false ;

					// Generate the code.
                    //generator.GenerateCodeFromCompileUnit(unit, Console.Out, options);
                    provider.GenerateCodeFromCompileUnit(unit, Console.Out, options);

					// Create a compiler.
					//ICodeCompiler		compiler		= provider.CreateCompiler () ;
					CompilerParameters	parameters		= new CompilerParameters () ;
					string []			references		= frameBase.referenceList ;

                    //bool compact = true;
                    //if (compactEnabled && compact)
                    //{
                    //    ////parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\mscorlib.dll");
                    //    //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.dll");
                    //    //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Data.dll");
                    //    //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Drawing.dll");
                    //    //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Windows.Forms.dll");
                    //    //parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Xml.dll");

                    //    //parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.dll"));
                    //    //parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
                    //    //parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));

                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.dll"));
                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Data.dll"));
                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Drawing.dll"));
                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Windows.Forms.dll"));
                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, @"CompactFramework\System.Xml.dll"));

                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.dll"));
                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.Controls.dll"));
                    //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.Controls.Frame.dll"));
                    //}
                    //else
                    {
                        parameters.ReferencedAssemblies.Add("System.dll");
                        parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                        parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                     
                        parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.dll"));
                        parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
                        parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));
                    }

                    if (references != null)
                    {
                        foreach (string assembly in references)
                        {
                            parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, assembly));
                        }
                    }
                    else //ft ajout du else
                    {
                        ReadReferencesFromProjectFile(parameters, references, false);//ft Ajout pour loader les dependances a partir du .ksp
                    }
					if (unit.UserData.Contains("Res"))
					{
						//got resources - embed the file
						string		res_filename ;

						res_filename = "KineticaRT.PanelCopying.ControlCopies.resources" ;
                        //res_filename = System.IO.Path.Combine(System.IO.Path.GetTempPath(), res_filename);
                        res_filename = System.IO.Path.Combine(FrameBase.tempFolder.Path, res_filename);
						parameters.CompilerOptions = "/res:\"" + res_filename + "\"" ;
					}

					System.IO.StreamWriter			writer		= new System.IO.StreamWriter("Clipboard.cs") ;
                    //generator.GenerateCodeFromCompileUnit(unit, writer, options);
                    provider.GenerateCodeFromCompileUnit(unit, writer, options);
					writer.Flush() ;
					writer.Close() ;

                    //CompilerResults results = compiler.CompileAssemblyFromDom(parameters, unit);
                    CompilerResults results = provider.CompileAssemblyFromDom(parameters, unit);

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

						//clear clipboard - no point in trying to compile this again
						Clipboard.SetDataObject(new DataObject("FrameFormat", null), true) ;
					}
					else		//no errors
					{
						object temp_obj  = results.CompiledAssembly.CreateInstance("KineticaRT.PanelCopying.ControlCopies");
						if (temp_obj is ControlCopyBase)
						{
							pasting = true ;
							frameBase.SuspendLayout() ;
							ClearSelections() ;

							//add the controls...
							((ControlCopyBase)temp_obj).AddCopies(frameBase.currentPanel) ;

							//bring them to the front (they will be the n last controls)
							for(int index = ctrlInfo.Count - 1; index >= 0; index--)
							{
								ControlEditInfo			info		= (ControlEditInfo)ctrlInfo[index] ;
								if (info.selected)
								{
									info.ctrl.BringToFront() ;
								}
							}

							//move them wrt where the mouse currently is...
							PositionPastedControls(mouse_pos.X - frameBase.DisplayRectangle.X, mouse_pos.Y - frameBase.DisplayRectangle.Y) ;

							//keep an undo for adding them...
							AddUndo(UndoTypes.Add) ;

							pasting = false ;
							frameBase.ResumeLayout() ;
							DrawHandles() ;
						}
					}

					busy = false ;
					SetCursor(Cursor.Position.X, Cursor.Position.Y) ;
				}
			}
		}
        //ft Ajout
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
		private void OnClickAddable(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			menuShowing = false ;

			if (frameBase.currentPanel == null)		return ;

			if (item != null)		//should always exist
			{
				System.Windows.Forms.Control		ctrl = frameBase.Add(item.Index) ;

				if (ctrl != null)
				{
					//put the new control where we clicked...
					Point		location		= click ;
					Rectangle	display_rect	= frameBase.currentPanel.DisplayRectangle ;

					//revert location back to panel coordinates
					location.Offset(-frameBase.DisplayRectangle.X, -frameBase.DisplayRectangle.Y) ;
					if (location.X < 0)			location.X = 0 ;
					if (location.Y < 0)			location.Y = 0 ;
					
					//...or as close as possible whilst being 
					//within the frame's borders
					if ((location.X + ctrl.Width) > display_rect.Right)
					{
						location.X = Math.Max(display_rect.Right - ctrl.Width, display_rect.Left) ;
					}
					if ((location.Y + ctrl.Height) > display_rect.Bottom)
					{
						location.Y = Math.Max(display_rect.Bottom - ctrl.Height, display_rect.Top) ;
					}

					ctrl.Location = location ;
					DrawHandles() ;
				}
			}
		}

		private void OnClickAddableComponent(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			menuShowing = false ;

			if (item != null)		//should always exist
			{
				frameBase.AddComponent(item.Index) ;
			}
		}

		private void OnClickUndo(object sender, EventArgs e)
		{
			menuShowing = false ;

			Undo() ;
		}

		private void OnClickSelectAll(object sender, EventArgs e)
		{
			menuShowing = false ;
			SelectAll(true) ;
		}

		private void OnClickAlign(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			menuShowing = false ;

			if (item != null)		//should always exist
			{
				Align((Alignments)item.Index) ;
			}
		}

		private void OnClickSameSize(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			menuShowing = false ;

			if (item != null)		//should always exist
			{
				MakeSameSize((Sizings)item.Index) ;
			}
		}

		private void OnClickStack(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			menuShowing = false ;

			if (item != null)		//should always exist
			{
				Stack(item.Index == 0) ;
			}
		}

		private void OnClickToGrid(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			menuShowing = false ;

			if (item != null)		//should always exist
			{
				ToGrid((GridFormats)item.Index) ;
			}
		}

		private void OnClickZOrder(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			menuShowing = false ;

			if (item != null)		//should always exist
			{
				ZOrder(item.Text.EndsWith("Front")) ;
			}
		}

		private void OnClickSave(object sender, EventArgs e)
		{
//			if (frame.PanelName.Length > 0)
//			{
//				busy = true ;
//				Cursor = Cursors.WaitCursor ;
//
//				frame.panel.Save(System.IO.Path.Combine(frame.SystemDirectory, frame.PanelName)) ;
//				
//				busy = false ;
//				SetCursor(Cursor.Position.X, Cursor.Position.Y) ;
//			}
			busy = true ;
			Cursor = Cursors.WaitCursor ;

			frameBase.SavePanel() ;
			
			busy = false ;
			SetCursor(Cursor.Position.X, Cursor.Position.Y) ;
		}
		#endregion

		#region Frame, Panel, Editor Events
		private void OnPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Panel")
			{
				//panel has changed - connect to new one
				if (frameBase.currentPanel != null)
				{
					frameBase.currentPanel.ControlAdded	+= addedHdlr ;
					frameBase.currentPanel.ControlRemoved	+= removedHdlr ;

					//reset cei list
					ctrlInfo.Clear() ;
					for (int index = 0; index < frameBase.currentPanel.Controls.Count; index++)
					{
						ctrlInfo.Add(new ControlEditInfo(frameBase.currentPanel.Controls[index], false)) ;
					}
				}
				else	//just reset cei list
				{
					ctrlInfo.Clear() ;
				}

				//clear undos
				undoInfo.Clear() ;
			}
		}

		private void OnControlAdded(object sender, ControlEventArgs e)
		{
			if (pasting)
			{
				//add the new control to the list and select it
				currentSelection = new ControlEditInfo(e.Control, true) ;
				ctrlInfo.Add(currentSelection) ;
			}
			else if (!codeAdd)
			{
				ClearSelections() ;

				//add the new control to the list and select it
				currentSelection = new ControlEditInfo(e.Control, true) ;
				ctrlInfo.Add(currentSelection) ;
				AddUndo(UndoTypes.Add) ;

				DrawHandles() ;
			}
			//else - un-delete
		}

		private void OnControlRemoved(object sender, ControlEventArgs e)
		{
			if (!codeDelete)
			{
				//control has been removed other than via the edit mode
				//find it in the list, and remove it
				foreach (ControlEditInfo info in ctrlInfo)
				{
					if (info.ctrl == sender)
					{
						if (info == currentSelection)
						{
							//##need to actually look for another selected one and make that current
							//##or clear all selections at this point
							currentSelection = null ;
						}
						ctrlInfo.Remove(info) ;
						break ;
					}
				}
			}
		}

        private void OnClickDisconnectControl(object sender, EventArgs e)//ft ajout
        {

            if (frameBase.currentPanel == null) return;
                
            foreach (ControlEditInfo info in ctrlInfo)
            {
                if (info.selected)
                {
                    IConnectable ctrl = info.ctrl as IConnectable;
                    if (ctrl != null)
                        ctrl.Disconnect();
                    //##need to actually look for another selected one and make that current
                    //##or clear all selections at this point
                    currentSelection = null;
                }
            }
        }
        private void OnClickConnectControl(object sender, EventArgs e)//ft ajout
        {
            if (frameBase.currentPanel == null) return;

            foreach (ControlEditInfo info in ctrlInfo)
            {
                if (info.selected)
                {
                    IConnectable ctrl = info.ctrl as IConnectable;
                    if (ctrl != null)
                        ctrl.Connect();
                    //##need to actually look for another selected one and make that current
                    //##or clear all selections at this point
                    currentSelection = null;
                }
            }

        }
        private void OnEditorDisposed(object sender, EventArgs e)
		{
			editor = null ;
		}
		#endregion
	}
}
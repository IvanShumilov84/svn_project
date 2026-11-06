using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Frame EditMode transparent control
	/// </summary>
	internal class EditMode : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private System.Windows.Forms.Control		dragItem ;
		private Point								startPoint ;		//mouse pos
        private Point								referencePoint ;	//ctrl location

		public EditMode()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			dragItem = null ;
			startPoint = Point.Empty ;
			referencePoint = Point.Empty ;

			//SetStyle(ControlStyles.UserPaint, true) ;
			//SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;
			//SetStyle(ControlStyles.ResizeRedraw, true) ;
			//SetStyle(ControlStyles.UserMouse, true) ;
			//SetStyle(ControlStyles.SupportsTransparentBackColor, true) ;
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
			components = new System.ComponentModel.Container();
		}
		#endregion

		[System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
		private static extern long SetWindowLong(
			IntPtr hwnd,
			int index,
			long newLong);

		const int	GWL_EXSTYLE			= -20 ;
		const long	WS_EX_TRANSPARENT	= 0x00000020 ;

		protected override void OnHandleCreated(EventArgs e)
		{
			SetWindowLong(this.Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT) ;
			base.OnHandleCreated(e) ;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
//			Rectangle	rect = new Rectangle(80, 50, 70, 50) ;
//			e.Graphics.FillRectangle(new SolidBrush(Color.Blue), rect) ;

			//this seems to help the drawing problems, but not solve them
			//((UserControl)Parent).Invalidate(e.ClipRectangle) ;
			
			base.OnPaint(e) ;
		}

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

					items = new MenuItem [3] ;
					items[0] = new MenuItem("Exit Edit Mode", new EventHandler(OnClickEditMode)) ;
					items[1] = new MenuItem("-") ;

					if (Parent is Frame)
					{
						items[2] = new MenuItem("AddControl", ((Frame)Parent).GetAddListItems(new EventHandler(OnClickAddItem))) ;
					}
					else
					{
						items[2] = new MenuItem("Add Control") ;
						items[2].Enabled = false ;
					}

					this.ContextMenu = new ContextMenu(items) ;
				}

				this.ContextMenu.Show(this, new Point(e.X, e.Y)) ;
			}
			else if (e.Button == MouseButtons.Left)
			{
				//have we clicked on a control?
				Frame		frame	= Parent as Frame ;
				
				if (frame != null)
				{
					dragItem = frame.PointToChild(e.X, e.Y) ;

					if (dragItem != null)
					{
						//start a drag on that control
						Capture = true ;
						startPoint = new Point(e.X, e.Y) ;
						referencePoint = dragItem.Location ;
					}
				}
			}

			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (Capture && dragItem != null)
			{
				Point		diff		= new Point(startPoint.X - e.X, startPoint.Y - e.Y) ;
				Point		new_pos		= new Point(referencePoint.X - diff.X, referencePoint.Y - diff.Y) ;

				dragItem.Location = new_pos ;
			}
			base.OnMouseMove(e) ;
		}
		
		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Capture = false ;
			base.OnMouseUp(e) ;
		}

		private void OnClickEditMode(object sender, EventArgs e)
		{
			Parent.Controls.Remove(this) ;
		}

		private void OnClickAddItem(object sender, EventArgs e)
		{
			MenuItem		item = sender as MenuItem ;

			if (item != null)
			{
				Frame		frame	= Parent as Frame ;
				
				if (frame != null)
				{
					frame.Add(item.Index) ;
				}
			}
		}
	}
}

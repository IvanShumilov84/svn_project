using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using KineticaRT.Controls;
using System.Diagnostics;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Dial control
	/// </summary>
	[DefaultProperty("Needle")]
	[ToolboxBitmap(typeof(Dial))]
	public class Dial : KineticaRT.Controls.DialBase
	{
		#region Variables
		//property variables
		private	DialNeedle			needle ;

		//other variables
		private	const bool			demoMode = true ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Dial class
		/// </summary>
		public Dial()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			needle = new DialNeedle() ;

			//event handlers for sub-objects
			needle.PropertyChanged += propertyHdlr ;
			needle.ObjectSearch += searchHdlr ;
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
			// Dial
			// 
			this.Name = "Dial";

		}
		#endregion

		#region Properties
		///<summary>
		///Gets or sets the Needle object
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Needle object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public DialNeedle Needle
		{
			get 
			{
				return needle ;
			}
			set
			{
				if (needle != null)
				{
					needle.PropertyChanged -= propertyHdlr ;
					needle.ObjectSearch -= searchHdlr ;
				}

				needle = value ;
				if (needle != null)
				{
					needle.PropertyChanged += propertyHdlr ;
					needle.ObjectSearch += searchHdlr ;
				}

				OnPropertyChanged("Needle") ;
			}
		}
		private bool ShouldSerializeNeedle()
		{
			return !needle.IsDefaultState() ;
		}
		/// <summary>
		/// Reset the Needle to its default settings
		/// </summary>
		public void ResetNeedle()
		{
			needle = new DialNeedle() ;
		}

		/// <summary>
		/// Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum
		/// from the Source of the Needle
		/// </summary>
		[Category("Dial")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum from the Source of the Needle")]
		public override bool UseSourceFormat
		{
			set
			{
				if (useSourceFormat != value)
				{
					useSourceFormat = value ;
					if (useSourceFormat)
					{
						SetFormat(needle) ;
					}
					AddToQueue(false) ;
					OnPropertyChanged("UseSourceFormat") ;
				}
			}
		}

		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		protected override void Repaint (bool FullRedraw, Graphics g)
		{
			CalculateDisplayRectangle() ;

			Rectangle		rect		= DisplayRectangle ;

			if (needle.CalculatePosition(rect, scale) || FullRedraw || resized)
			{
				//needle has moved - redraw
				if (FullRedraw || resized || (needle.FillEnabled && needle.FillStyle != FillStyles.Permanent) )
				{
					//need to redraw the whole lot from scratch - into the background bitmap
					if (resized)
					{
						if (bmpBackground != null)		bmpBackground.Dispose() ;
						bmpBackground = new Bitmap(ClientRectangle.Width, ClientRectangle.Height) ;
						resized = false ;
					}

					Graphics	gBmp	= Graphics.FromImage(bmpBackground) ;

					//basic drawing (appearance etc)
					base.Repaint(true, gBmp) ;

					SetDisplayRegion(gBmp) ;
					gBmp.SmoothingMode = SmoothingMode.AntiAlias ;
					needle.DrawFill(gBmp, rect, scale, false, backBrush) ;

					scale.Draw(gBmp, rect, ScaleOrientations.Circular, this.Font, 1) ;
					DrawText(gBmp) ;
					gBmp.Dispose() ;
				}

				//copy the background bitmap and put the needle on top
//				g.DrawImage(bmpBackground, 0, 0) ;
//				g.SmoothingMode = SmoothingMode.AntiAlias ;
//				needle.Draw(g, rect) ;

				Bitmap		bmp = (Bitmap)bmpBackground.Clone() ;
				Graphics	g2	= Graphics.FromImage(bmp) ;
				
				SetDisplayRegion(g2) ;
				g2.SmoothingMode = SmoothingMode.AntiAlias ;
				needle.Draw(g2, rect) ;

				//draw 'Demo' if required
				if (demoMode)			DrawDemoText(g2, rect, BackColor) ;

				g2.Dispose() ;

				g.DrawImage(bmp, 0, 0) ;

				bmp.Dispose() ;
				g.Flush() ;
			}
		}
		#endregion

		#region Mouse Event handling
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			//disallow dragging if dial or needle is disabled
			if (base.Enabled && needle.Enabled)
			{
				double		new_frac		= PositionToFraction (e.X, e.Y) ;

				this.Capture = true ;
				needle.SetValueFromInput(new_frac, false, scale) ;
			}
			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (Enabled && Capture && needle.Enabled)
			{
				double		new_frac		= PositionToFraction (e.X, e.Y) ;
				needle.SetValueFromInput(new_frac, false, scale) ;
			}
			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (Enabled && Capture && needle.Enabled)
			{
				if (!needle.UpdateOnDrag)
				{
					//set new position
					double		new_frac		= PositionToFraction (e.X, e.Y) ;

					needle.SetValueFromInput(new_frac, true, scale) ;
				}

				if (needle.AutoReturn)
				{
					needle.Value = needle.DefaultValue ;
				}
			}
			if (Capture)		Capture = false ;

			base.OnMouseUp(e) ;
		}
		#endregion

		#region Drag / Drop operations
		/// <summary>
		/// DragEnter event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnDragEnter (DragEventArgs e)
		{
			//check if allowed to drop things, and if drag object holds text
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}

			base.OnDragEnter(e) ;
		}

		/// <summary>
		/// DragDrop event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnDragDrop (DragEventArgs e)
		{
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				needle.Source = e.Data.GetData(DataFormats.Text).ToString() ;
			}

			base.OnDragDrop(e) ;
		}
		#endregion

		#region Property changed event handling)
		/// <summary>
		/// Handles PropertyChanged events from sub-objects
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnChildPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SourceChannel")
			{
				SetFormat(needle) ;
			}
			base.OnChildPropertyChanged(sender, e) ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (needle != null)		needle.Connect() ;
			base.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (needle != null)		needle.Disconnect() ;
			base.Connect() ;
		}
		#endregion
	}
}

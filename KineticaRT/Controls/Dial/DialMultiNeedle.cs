using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Multiple Needle Dial control
	/// </summary>
	[DefaultProperty("Needles")]
	[ToolboxBitmap(typeof(DialMultiNeedle))]
	public class DialMultiNeedle : KineticaRT.Controls.DialBase
	{
		#region Variables
		//internal variables
		private int					captureIndex ;
		private	const bool			demoMode = false;

		//property variables
		private	DialNeedleCollection	needles ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the DialMultiNeedle class
		/// </summary>
		public DialMultiNeedle()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			needles = new DialNeedleCollection() ;

			captureIndex = -1 ;

			//event handlers for sub-objects
			needles.PropertyChanged += propertyHdlr ;
			needles.ObjectSearch += searchHdlr ;
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
			// DialMultiNeedle
			// 
			this.Name = "DialMultiNeedle";

		}
		#endregion

		#region Properties
		///<summary>
		///Gets or sets a Collection of Needle objects
		///</summary>
		[Category("Dial")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets a Collection of Needle objects")]
		public DialNeedleCollection Needles
		{
			get 
			{
				return needles ;
			}
			set
			{
				if (needles != null)
				{
					needles.PropertyChanged -= propertyHdlr ;
					needles.ObjectSearch -= searchHdlr ;
				}

				needles = value ;
				if (needles != null)
				{
					needles.PropertyChanged += propertyHdlr ;
					needles.ObjectSearch += searchHdlr ;
				}

				OnPropertyChanged("Needles") ;
			}
		}
		private bool ShouldSerializeNeedles()
		{
			return !needles.IsDefaultState() ;
		}
		/// <summary>
		/// Reset the Needle Collection to its default settings
		/// </summary>
		public void ResetNeedles()
		{
			needles = new DialNeedleCollection() ;
		}

		/// <summary>
		/// Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum
		/// from the Source of the first Needle in the collection
		/// </summary>
		[Category("Dial")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum from the Source of the first Needle in the collection")]
		public override bool UseSourceFormat
		{
            get
            {
                return useSourceFormat ;
            }

			set
			{
				if (useSourceFormat != value)
				{
					useSourceFormat = value ;
					if (useSourceFormat && needles.Count > 0)
					{
						SetFormat(needles[0]) ;
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
		public override void Repaint (bool FullRedraw, Graphics g)
		{
            //lock (this) //ft ajout 
            {
                CalculateDisplayRectangle();

                Rectangle rect = DisplayRectangle;
                bool draw_all;

                if (needles.CalculatePosition(rect, scale, out draw_all) || FullRedraw || resized)
                {
                    //needle has moved - redraw
                    if (FullRedraw || resized || draw_all)
                    {
                        //need to redraw the whole lot from scratch - into the background bitmap
                        if (resized)
                        {
                            if (bmpBackground != null) bmpBackground.Dispose();
                            bmpBackground = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                            resized = false;
                        }

                        Graphics gBmp = Graphics.FromImage(bmpBackground);

                        //basic drawing (appearance etc)
                        base.Repaint(true, gBmp);

                        SetDisplayRegion(gBmp);

                        gBmp.SmoothingMode = SmoothingMode.AntiAlias;
                        needles.DrawFill(gBmp, rect, scale, backColorBrush);

                        scale.Draw(gBmp, rect, ScaleOrientations.Circular, this.Font, 1);
                        DrawText(gBmp);
                        gBmp.Dispose();
                    }

                    Bitmap bmp = (Bitmap)bmpBackground.Clone();
                    Graphics g2 = Graphics.FromImage(bmp);
                    SetDisplayRegion(g2);
                    g2.SmoothingMode = SmoothingMode.AntiAlias;
                    needles.Draw(g2, rect);

                    //draw 'Demo' if required
                    //if (demoMode)		DrawDemoText(g2, rect, BackColor) ;

                    g2.Dispose();

                    g.DrawImage(bmp, 0, 0);

                    bmp.Dispose();
                    //GDIFlush g.Flush() ; 
                }
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
			if (base.Enabled && e.Button == MouseButtons.Left)
			{
				//work out if we've clicked on the needle
				double					new_frac ;
				double					min_frac ;
				double					max_frac ;

				new_frac = PositionToFractionRange (e.X, e.Y, true, out min_frac, out max_frac) ;
				if (needles.CheckForCapture(min_frac, max_frac, e.X, e.Y, scale, out captureIndex))
				{
					//we have captured one - set its value
					this.Capture = true ;
					needles[captureIndex].SetValueFromInput(new_frac, false, scale) ;
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
			if (Enabled && Capture && captureIndex >= 0)
			{
				double		new_frac		= PositionToFraction (e.X, e.Y) ;

				needles[captureIndex].SetValueFromInput(new_frac, false, scale) ;
			}
			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (Enabled && Capture && captureIndex >= 0)
			{
				if (!needles[captureIndex].UpdateOnDrag)
				{
					//set new position
					double		new_frac		= PositionToFraction (e.X, e.Y) ;

					needles[captureIndex].SetValueFromInput(new_frac, true, scale) ;
				}

				if (needles[captureIndex].AutoReturn)
				{
					needles[captureIndex].Value = needles[captureIndex].DefaultValue ;
				}
			}
			if (Capture)		Capture = false ;
			captureIndex = -1 ;

			base.OnMouseUp(e) ;
		}
		#endregion

		#region Drag / Drop operations
		/// <summary>
		/// DragOver event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnDragOver (DragEventArgs e)
		{
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text))
			{
				int			index ;
				Point		pt		= PointToClient(new Point(e.X, e.Y)) ;
				double		min ;
				double		max ;

				this.PositionToFractionRange(pt.X, pt.Y, true, out min, out max) ;

				if (needles.CheckForCapture(min, max, pt.X, pt.Y, scale, out index)	)
				{
					e.Effect = DragDropEffects.Copy;
				}
				else
				{
					e.Effect = DragDropEffects.None;
				}
			}				
			else
			{
				e.Effect = DragDropEffects.None;
			}

			base.OnDragOver(e) ;
		}

		/// <summary>
		/// DragDrop event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnDragDrop (DragEventArgs e)
		{
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text))
			{
				int			index ;
				Point		pt		= PointToClient(new Point(e.X, e.Y)) ;
				double		min ;
				double		max ;

				this.PositionToFractionRange(pt.X, pt.Y, true, out min, out max) ;

				if (needles.CheckForCapture(min, max, pt.X, pt.Y, scale, out index)	)
				{
					needles[index].Source = e.Data.GetData(DataFormats.Text).ToString() ;
				}
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
			if (e.PropertyName == "SourceChannel" && needles.Count > 0)
			{
				SetFormat(needles[0]) ;
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
			if (needles != null)		needles.Connect() ;
			base.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (needles != null)		needles.Disconnect() ;
			base.Connect() ;
		}
		#endregion
	}
}

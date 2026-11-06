using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Diagnostics ;
using System.Windows.Forms;
using KineticaRT;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT PictureButton Control. 
	/// Provides all the functionality of a standard KineticaRT Button,
	/// with the extra capability of Pictures.
	/// </summary>
	[DefaultProperty("Value")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Button.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(PictureButton))]
	public class PictureButton : KineticaRT.Controls.Button, IButton
	{
		#region Variables and Declarations
		private KineticaRT.Controls.Picture	picture ;
		private KineticaRT.Controls.Picture	downPicture ;
		private KineticaRT.Controls.Picture	disabledPicture ;
		private bool						downPictureEnabled;
		private bool						disabledPictureEnabled;
		private bool						pictureEnabled;

		//internal variables
		private ObjectSearchEventHandler			searchHdlr ;
		private MouseEventHandler					clickHdlr ;

		private bool					gotVisiblePicture ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Button class
		/// </summary>
		public PictureButton()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			searchHdlr	= new ObjectSearchEventHandler(OnObjectSearch) ;
			clickHdlr	= new MouseEventHandler(OnPictureMouseDown) ;

			downPicture.ObjectSearch	+= searchHdlr ;
			downPicture.MouseDown		+= clickHdlr ;

			disabledPicture.ObjectSearch+= searchHdlr ;
			disabledPicture.MouseDown	+= clickHdlr ;

			picture.ObjectSearch		+= searchHdlr ;
			picture.MouseDown			+= clickHdlr ;

			pictureEnabled			= false ;
			downPictureEnabled		= false ;
			disabledPictureEnabled	= false ;

			//now set the look
			SetLook() ;
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
			this.picture = new KineticaRT.Controls.Picture();
			this.downPicture = new KineticaRT.Controls.Picture();
			this.disabledPicture = new KineticaRT.Controls.Picture();
			this.SuspendLayout();
			// 
			// picture
			// 
            this.picture.BorderType = KineticaRT.Controls.BorderType.None;
			this.picture.Name = "picture";
			this.picture.Size = new System.Drawing.Size(2, 2);
			this.picture.TabIndex = 0;
			this.picture.VariableColors = new KineticaRT.Controls.VariableColorCollection(new KineticaRT.Controls.ReplaceableColor[0]);
			// 
			// downPicture
			// 
            this.downPicture.BorderType = KineticaRT.Controls.BorderType.None;
			this.downPicture.Name = "downPicture";
			this.downPicture.Size = new System.Drawing.Size(2, 2);
			this.downPicture.TabIndex = 1;
			this.downPicture.VariableColors = new KineticaRT.Controls.VariableColorCollection(new KineticaRT.Controls.ReplaceableColor[0]);
			// 
			// disabledPicture
			// 
            this.disabledPicture.BorderType = KineticaRT.Controls.BorderType.None;
			this.disabledPicture.Name = "disabledPicture";
			this.disabledPicture.Size = new System.Drawing.Size(2, 2);
			this.disabledPicture.TabIndex = 2;
			this.disabledPicture.VariableColors = new KineticaRT.Controls.VariableColorCollection(new KineticaRT.Controls.ReplaceableColor[0]);
			// 
			// PictureButton
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.disabledPicture,
																		  this.downPicture,
																		  this.picture});
			this.Name = "PictureButton";
			this.Size = new System.Drawing.Size(100, 40);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or Sets the type of the Control's Border
		/// </summary>
		[Category("Control")]
        [DefaultValue(BorderType.None)]
		[Description("Gets or Sets the type of the Control's Border")]
		public override BorderType BorderType
		{
			get { return base.BorderType ; }
			set 
			{
				base.BorderType = value ;
				resized = true ;
			}
		}

		/// <summary>
		/// Gets or Sets the width of the Control's Border
		/// </summary>
		/// <remarks>
		/// Values between 0 and 0.5 represent a proportion of the width of the Control.
		/// Values greater than or equal to 1 represent the number of pixels
		/// </remarks>
		[Category("Control")]
		[DefaultValue(0.1)]
		[Description("Gets or Sets the width of the Control's Border: 0 - 0.5 as proprtion of width of control, or >= 1 as a number of pixels")]
		public override double BorderWidth
		{
			get { return base.BorderWidth ; }
			set 
			{
				base.BorderWidth = value ;
				resized = true ;
			}
		}

		/// <summary>
		/// Gets the Picture shown in unpressed state
		/// </summary>
		[Category("Pictures")]
		[Description("Gets the Picture shown in unpressed state")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public KineticaRT.Controls.Picture Picture
		{
			get
			{
				return picture ;
			}
		}

		/// <summary>
		/// Gets the Picture shown in pressed state
		/// </summary>
		[Category("Pictures")]
		[Description("Gets the Picture shown in pressed state")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public KineticaRT.Controls.Picture DownPicture
		{
			get
			{
				return downPicture ;
			}
		}

		/// <summary>
		/// Gets the Picture shown when button is disabled
		/// </summary>
		[Category("Pictures")]
		[Description("Gets the Picture shown when button is disabled")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public KineticaRT.Controls.Picture DisabledPicture
		{
			get
			{
				return disabledPicture ;
			}
		}

		/// <summary>
		/// Gets or sets whether to show the unpressed state picture
		/// </summary>
		[Category("Pictures")]
		[DefaultValue(false)]
		[Description("Gets or sets whether to show the unpressed state picture")]
		public bool PictureEnabled
		{
			get
			{
				return pictureEnabled ;
			}
			set
			{
				if (pictureEnabled != value)
				{
					pictureEnabled = value ;
					SetLook() ;
					AddToQueue(false) ;
					OnPropertyChanged("PictureEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether to show the pressed state picture
		/// </summary>
		[Category("Pictures")]
		[DefaultValue(false)]
		[Description("Gets or sets whether to show the unpressed state picture")]
		public bool DownPictureEnabled
		{
			get
			{
				return downPictureEnabled ;
			}
			set
			{
				if (downPictureEnabled != value)
				{
					downPictureEnabled = value ;
					SetLook() ;
					AddToQueue(false) ;
					OnPropertyChanged("DownPictureEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether to show the disabled state picture
		/// </summary>
		[Category("Pictures")]
		[DefaultValue(false)]
		[Description("Gets or sets whether to show the disabled state picture")]
		public bool DisabledPictureEnabled
		{
			get
			{
				return disabledPictureEnabled ;
			}
			set
			{
				if (disabledPictureEnabled != value)
				{
					disabledPictureEnabled = value ;
					SetLook() ;
					AddToQueue(false) ;
					OnPropertyChanged("DisabledPictureEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Tool Tip Text that appears when hovering over the control
		/// </summary>
		/// <remarks>
		/// This Tool Tip is also applied to the Button's Picture objects.
		/// To override this behaviour and make the Pictures have a different tool tip,
		/// ensure that the Picture's ToolTipText property is set after this property.</remarks>
		[Category("Control")]
		[DefaultValue("")]
		[Description("Gets or Sets the Tool Tip Text that appears when hovering over the control")]
		public override string ToolTipText
		{
			set
			{
				base.ToolTipText = value ;

				picture.ToolTipText = value ;
				downPicture.ToolTipText = value ;
				disabledPicture.ToolTipText = value ;
			}
		}

		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
            //lock (this) //ft ajout 
            {
                BasicRepainting(FullRedraw, g);

                if (!gotVisiblePicture)
                {
                    DrawText(g);
                }
                else if (resized)
                {
                    resized = false;

                    Rectangle temp = boundsRect;

                    if (this.InvokeRequired)
                        this.BeginInvoke(new MethodInvoker(delegate() { RepaintUpdatePicture(temp); }));   // Avec Invoke on a un deadlock avec SetState
                    else
                        RepaintUpdatePicture(temp);

                }

                DrawBox(g);
            }
		}
        private void RepaintUpdatePicture(Rectangle temp)
        {
            //relocate pictures - draw where text is allowed
            picture.Size = new Size(temp.Width, temp.Height);
            picture.Location = new Point(temp.X, temp.Y);
            downPicture.Size = picture.Size;
            disabledPicture.Size = picture.Size;
            downPicture.Location = picture.Location;
            disabledPicture.Location = picture.Location;
        }
		#endregion

		#region Other methods
		/// <summary>
		/// Sets the Button's appearance and Picture visibility
		/// </summary>
		protected override void SetLook()
		{
			bool   got_picture = false ;

			//don't bother if not got pictures yet (which occurs at initialisation)
			if (picture == null || downPicture == null || disabledPicture == null)
			{
				return ;
			}

            if (!(inputEnabledValue || InputEnabled))
            {
                picture.Visible = false;
                downPicture.Visible = false;
                disabledPicture.Visible = true;
                got_picture = true;
            }
            else
            {
			    //deal with the pictures
			    switch (State)
			    {
				    case ButtonStates.Invalid:
				    {
					    picture.Visible = false ;
					    downPicture.Visible = false ;
					    disabledPicture.Visible = false ;
                        got_picture = false;
				    }
				    break ;

				    case ButtonStates.Pressed:
				    {
					    picture.Visible = false;
                        downPicture.Visible = true ;
					    disabledPicture.Visible = false;
                        got_picture = true;
                    }
				    break ;

				    case ButtonStates.Unpressed:
				    {
					    picture.Visible = true ;
					    downPicture.Visible = false;
                        disabledPicture.Visible = false;
                        got_picture = true;
                    }
				    break ;
			    }
            }

			//can't rely on Visible properties as before the form is shown
			// they will be false, and become true by first draw
//			gotVisiblePicture = picture.Visible || downPicture.Visible || disabledPicture.Visible ;
			gotVisiblePicture = got_picture ;

			//now call the base class to do the appearance
			base.SetLook() ;
		}
		#endregion

		#region Mouse event handling
		/// <summary>
		/// Handles a MouseDown event from a child Picture control.
		/// </summary>
		/// <param name="sender">Object that generated the Event</param>
		/// <param name="e">Event Data</param>
		private void OnPictureMouseDown(object sender, MouseEventArgs e)
		{
            if (System.Windows.Forms.MouseButtons.Left == e.Button)
            {
                HandleMouseDown(e);
            }
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect() ;

			if (picture != null)			picture.Connect() ;
			if (downPicture != null)		downPicture.Connect() ;
			if (disabledPicture != null)	disabledPicture.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect() ;

			if (picture != null)				picture.Disconnect() ;
			if (downPicture != null)			downPicture.Disconnect() ;
			if (disabledPicture != null)		disabledPicture.Disconnect() ;
		}
		#endregion
	}
}

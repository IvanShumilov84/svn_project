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
#if KRT_license
	[LicenseProviderAttribute(typeof(Dial.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Dial))]
	public class Dial : KineticaRT.Controls.DialBase
	{
		#region License
#if KRT_license
		private License license = null;

		// Do not move this class or its contents into another namespace - it would not be secure.
	
		//[LicenseProviderAttribute(typeof(xxx.LicenseProvider))]
		//private License license = null;
		//demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
		//if (license != null) {license.Dispose();license = null;}
		//This block: update key using GUIDGEN, and typeof(xxx)
		/// <summary>
		/// Implements a license provider for the class.
		/// </summary>
		public class LicenseProvider: System.ComponentModel.LicenseProvider
		{
			/// <summary>
			/// Gets a license for an instance or type of component, when given a context and whether the denial of a license throws an exception
			/// </summary>
			/// <param name="context">A LicenseContext that specifies where you can use the licensed object</param>
			/// <param name="type">A Type that represents the component requesting the license</param>
			/// <param name="instance">An object that is requesting the license</param>
			/// <param name="allowExceptions">true if a LicenseException should be thrown when the component cannot be granted a license; otherwise, false</param>
			/// <returns>A valid License</returns>
			public override System.ComponentModel.License GetLicense(LicenseContext context,Type type,object instance,bool allowExceptions)
			{
				License licence = null ;

				if (context != null)
				{
					string key = null ;

					if (context.UsageMode == LicenseUsageMode.Designtime)
					{
						Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
						if (registry != null) key = registry.GetValue ("RegKey") as string ;
						if ((key == null) || (key != "B125A8B15BAA" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "B125A8B15BAA" || key == "4CD0571EE68D")) key = "{B125A8B1-5BAA-476b-A44D-D94702C10D4A}" ; 
						context.SetSavedLicenseKey (typeof(Dial), key) ;
//						System.Windows.Forms.MessageBox.Show (typeof(Dial).ToString(), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Dial), null) ;
					if (key == "{B125A8B1-5BAA-476b-A44D-D94702C10D4A}") licence = new License ("Run") ;
					else if (allowExceptions && (context.UsageMode != LicenseUsageMode.Designtime)) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
					else licence = new License ("None") ;
				}

				return licence ;
			}

			private class License:System.ComponentModel.License 
			{
				string key ;

				public License (string key)
				{
					this.key = key ;
				}

				public override string LicenseKey 
				{
					get
					{
						return key ;
					}
				}
		
				public override void Dispose()
				{
				}
			}
		}

		private	bool				demoMode ;
#endif
		#endregion

		#region Variables
		//property variables
		/// <summary>
		/// The dials needle.
		/// </summary>
		protected	DialNeedle			needle ;

		//other variables
		//private	const bool			demoMode = false;

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

#if KRT_license
			demoMode = !LicenseManager.IsValid (typeof(Dial), this, out license) || (license.LicenseKey != "Run");
#endif

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
#if KRT_license
			if (license != null) {license.Dispose(); license = null;}
#endif

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
        ///Gets the Scale object
        ///</summary>
        [Category("Dial")]
        [Description("Gets the Scale object")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new KineticaRT.Controls.Scale Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }
        private bool ShouldSerializeScale()
        {
            //always save the scale
            return true;
        }
        /// <summary>
        /// Reset the Scale to its default settings
        /// </summary>
        public new void ResetScale()
        {
            scale = new Scale();
        }

        ///<summary>
		///Gets or sets the Needle object
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Needle object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[RefreshProperties(RefreshProperties.All)]
		public virtual DialNeedle Needle
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
        /// Gets or sets whether UseSourceFormat includes the use of TopText and BottomText.
        /// </summary>
        [Category("Dial")]
        [DefaultValue(true)]
        [Description("Whether UseSourceFormat includes the use of TopText and BottomText.")]
        public bool UseSourceForTopAndBottomText
        {
            get
            {
                return useSourceForTopAndBottomText;
            }
            set
            {
                if (useSourceForTopAndBottomText != value)
                {
                    useSourceForTopAndBottomText = value;
                    OnPropertyChanged("UseSourceForTopAndBottomText");
                }
            }
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
			get
			{
				return useSourceFormat ;
			}
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
		public override void Repaint (bool FullRedraw, Graphics g)
		{
			Rectangle		rect		= DisplayRectangle ;

			if ((rect.Width > 10 && rect.Height > 10) && (needle.CalculatePosition(rect, scale) || FullRedraw || resized || backColorChanged))
			{
				//needle has moved - redraw
                if (FullRedraw || resized || backColorChanged || (needle.FillEnabled && needle.FillStyle != FillStyles.Permanent))
				{
                    CalculateDisplayRectangle();//ft cette fonction doit etre calle pour setter correctement DisplayRectangle (displayRect)
                    rect = DisplayRectangle;//remet a jour les nouvelles dimensions
                    if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                    {
                        backColorChanged = false;
                        backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                    }
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
					needle.DrawFill(gBmp, rect, scale, false, backColorBrush) ;

					if (scale!=null) scale.Draw(gBmp, rect, ScaleOrientations.Circular, this.Font, 1) ;
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

#if KRT_license
				//draw 'Demo' if required
				if (demoMode)			DrawDemoText(g2, rect, BackColor) ;
#endif

				g2.Dispose() ;

				g.DrawImage(bmp, 0, 0) ;

				bmp.Dispose() ;
				//GDIFlush g.Flush() ; 
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
            base.Disconnect();
		}
		#endregion
	}
}

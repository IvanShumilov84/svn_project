using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

using KineticaRT.Controls;
using KineticaRT;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Slider Control
	/// </summary>
	[DefaultProperty("Needle")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Slider.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Slider))]
	public class Slider : KineticaRT.Controls.SliderBase
	{
		#region License
		#if KRT_license
		private	bool				demoMode ;
		private License license = null;

		// Do not move this class or its contents into another namespace - it would not be secure.
	
		////[LicenseProviderAttribute(typeof(xxx.LicenseProvider))]
		//private License license = null;
		////demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
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
						if ((key == null) || (key != "C80BDC9BEFEB" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "C80BDC9BEFEB" || key == "4CD0571EE68D")) key = "{C80BDC9B-EFEB-4b37-ABC1-9213AF9D37E7}" ; 
						context.SetSavedLicenseKey (typeof(Slider), key) ;
//						System.Windows.Forms.MessageBox.Show (typeof(Slider).ToString(), key) ;
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Slider), null) ;
					if (key == "{C80BDC9B-EFEB-4b37-ABC1-9213AF9D37E7}") licence = new License ("Run") ;
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
		#endif
		#endregion
		
		#region Variables

		//property variables
		private SliderNeedle				needle ;

		//other variables

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Slider class
		/// </summary>
		public Slider()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			#if KRT_license
			demoMode = !LicenseManager.IsValid (typeof(Slider), this, out license) || (license.LicenseKey != "Run");
			#endif
			
			needle = new SliderNeedle() ;

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
			if (license != null) {license.Dispose();license = null;}
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
			// Slider
			// 
			this.Name = "Slider";
			this.Size = new System.Drawing.Size(96, 200);
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
                Rectangle rect = DisplayRectangle;

                if (needle.CalculatePosition(rect, scale, orientation) || FullRedraw || resized || backColorChanged)//ft
                {
                    //needle has moved - redraw
                    if (FullRedraw || resized || backColorChanged || (needle.FillEnabled && needle.FillStyle != FillStyles.Permanent))
                    {

                        if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0) //ft si le le width ou heignt est 0 ca lance une exception
                        {
                            if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                            {
                                backColorChanged = false;
                                backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                            }
                            //need to redraw the whole lot from scratch - into the background bitmap
                            if (resized)
                            {
                                CalculateDisplayRectangle(); //ft cette fonction doit etre calle pour setter correctement DisplayRectangle (displayRect)
                                rect = DisplayRectangle;//remet a jour les nouvelles dimensions
                                if (bmpBackground != null) bmpBackground.Dispose();
                                bmpBackground = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                                resized = false;
                            }

                            Graphics gBmp = Graphics.FromImage(bmpBackground);

                            //basic drawing (appearance etc)
                            base.Repaint(true, gBmp);

                            SetDisplayRegion(gBmp);

                            offset = GetOffsetRatio(gBmp);
                            needle.DrawFill(gBmp, rect, scale, orientation, offset, gradientEnabled, gradientMode); //ft Dessine le rectangle plein du Bargraph

                            scale.Draw(gBmp, rect, orientation, this.Font, offset);
                            gBmp.Dispose();
                        }
                    }

                    Bitmap bmp = (Bitmap)bmpBackground.Clone();
                    Graphics g2 = Graphics.FromImage(bmp);

                    SetDisplayRegion(g2);
                    needle.Draw(g2, rect, scale, orientation, offset);

#if KRT_license
				if (demoMode)			DrawDemoText(g2, rect, BackColor) ;
#endif

                    g2.Dispose();

                    g.DrawImage(bmp, 0, 0);
                    bmp.Dispose();
                    //GDIFlush g.Flush() ; 
                }
            }
		}
		#endregion

		#region Properties
		///<summary>
		///Gets or sets the control's Needle object
		///</summary>
		[Category("Slider")]
		[Description("Gets or sets the control's Needle object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public virtual SliderNeedle Needle
		{
			get 
			{
				return needle ;
			}
			set
			{
				if (needle != null)
				{
					needle.PropertyChanged	-= propertyHdlr ;
					needle.ObjectSearch		-= searchHdlr ;
				}

				needle = value ;
				if (needle != null)
				{
					needle.PropertyChanged	+= propertyHdlr ;
					needle.ObjectSearch		+= searchHdlr ;
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
			needle = new SliderNeedle() ;
		}

		/// <summary>
		/// Gets or sets whether the Slider gets its ToolTip, Minimum and Maximum
		/// from the Source of its Needle
		/// </summary>
		[Category("Slider")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Slider gets its ToolTip, Minimum and Maximum from the Source of its Needle")]
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

		#region Mouse Event handling
		private void SetValueFromInput(int XPos, int YPos, bool drag_release)
		{
			double		frac			= PositionToFraction(XPos, YPos) ;

			needle.SetValueFromInput(frac, drag_release, scale) ;
		}

		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (base.Enabled && needle.Enabled)
			{
				this.Capture = true ;
				SetValueFromInput(e.X, e.Y, false) ;
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
				SetValueFromInput(e.X, e.Y, false) ;
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
				if (needle.UpdateOnDrag)
				{
					//set new position
					SetValueFromInput(e.X, e.Y, true) ;
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
			if (e == null) throw new ArgumentNullException("e") ;

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
			base.Disconnect() ;
		}
		#endregion
	}
}

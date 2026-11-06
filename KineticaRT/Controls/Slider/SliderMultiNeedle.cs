using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using KineticaRT.Controls;
using KineticaRT;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Slider Control with Multiple Needlde objects
	/// </summary>
	[DefaultProperty("Needles")]
#if KRT_license
	[LicenseProviderAttribute(typeof(SliderMultiNeedle.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(SliderMultiNeedle))]
	public class SliderMultiNeedle : KineticaRT.Controls.SliderBase
	{
		#region License
		#if KRT_license
		private License license = null;
		private	bool						demoMode ;

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
						if ((key == null) || (key != "55CCE43132F0" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "55CCE43132F0" || key == "4CD0571EE68D")) key = "{55CCE431-32F0-4f1d-B93C-F3F4DFC5A544}" ; 
						context.SetSavedLicenseKey (typeof(SliderMultiNeedle), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(SliderMultiNeedle), null) ;
					if (key == "{55CCE431-32F0-4f1d-B93C-F3F4DFC5A544}") licence = new License ("Run") ;
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
		//internal variables
		private int							captureIndex ;

		//property variables
		private SliderNeedleCollection		needles ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the SliderMultiNeedle class
		/// </summary>
		public SliderMultiNeedle()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			#if KRT_license
			demoMode = !LicenseManager.IsValid (typeof(Slider), this, out license) || (license.LicenseKey != "Run");
			#endif
			
			captureIndex = -1 ;

			needles = new SliderNeedleCollection() ;

			//event handlers for sub-objects
			needles.PropertyChanged += propertyHdlr ;
			needles.ObjectSearch += searchHdlr ;
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
			this.Name = "SliderMultiNeedle";
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
                CalculateDisplayRectangle();

                Rectangle rect = DisplayRectangle;
                bool draw_all;

                if (needles.CalculatePosition(rect, scale, orientation, out draw_all) || FullRedraw || resized)
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
                        offset = GetOffsetRatio(gBmp);
                        needles.DrawFill(gBmp, rect, scale, orientation, offset, gradientEnabled, gradientMode);

                        scale.Draw(gBmp, rect, orientation, this.Font, offset);
                        gBmp.Dispose();
                    }

                    Bitmap bmp = (Bitmap)bmpBackground.Clone();
                    Graphics g2 = Graphics.FromImage(bmp);

                    SetDisplayRegion(g2);
                    needles.Draw(g2, rect, scale, orientation, offset);
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
		///Gets or sets the Collection of Needle objects
		///</summary>
		[Category("Slider")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the Collection of Needle objects")]
		public SliderNeedleCollection Needles
		{
			get 
			{
				return needles ;
			}
			set
			{
				if (needles != null)
				{
					needles.PropertyChanged	-= propertyHdlr ;
					needles.ObjectSearch	-= searchHdlr ;
				}

				needles = value ;
				if (needles != null)
				{
					needles.PropertyChanged	+= propertyHdlr ;
					needles.ObjectSearch	+= searchHdlr ;
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
			needles = new SliderNeedleCollection() ;
		}

		/// <summary>
		/// Gets or sets whether the Slider gets its ToolTip, Minimum and Maximum
		/// from the Source of the first Needle in the collection
		/// </summary>
		[Category("Slider")]
		[DefaultValue(true)]
		[Description("If true, the Slider gets its ToolTip, Minimum and Maximum from the Source of the first Needle in the collection")]
		public override bool UseSourceFormat
		{
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

		#region Mouse Event handling
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (base.Enabled && e.Button == MouseButtons.Left)
			{
				//work out if we've clicked on a needle
				if (needles.CheckForCapture(e.X, e.Y, orientation, out captureIndex))
				{
					double					new_frac ;

					new_frac = PositionToFraction (e.X, e.Y) ;

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
				double		frac			= PositionToFraction(e.X, e.Y) ;
				
				needles[captureIndex].SetValueFromInput(frac, false, scale) ;
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
					double		frac			= PositionToFraction(e.X, e.Y) ;

					needles[captureIndex].SetValueFromInput(frac, true, scale) ;
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
			int			index ;
			Point		pt		= PointToClient(new Point(e.X, e.Y)) ;

			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text) &&
				needles.CheckForCapture(pt.X, pt.Y, orientation, out index)	)
			{
				e.Effect = DragDropEffects.Copy;
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
			int			index ;
			Point		pt		= PointToClient(new Point(e.X, e.Y)) ;

			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text) &&
				needles.CheckForCapture(pt.X, pt.Y, orientation, out index)	)
			{
				needles[index].Source = e.Data.GetData(DataFormats.Text).ToString() ;
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

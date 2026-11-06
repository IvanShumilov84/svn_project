using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using KineticaRT.Controls;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization ;
using System.Reflection;
using System.IO;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Picture Control
	/// Displays images from files, including animated GIFs
	/// </summary>
	//[TypeConverterAttribute (typeof(Picture.PictureTypeConverter))]
	[TypeConverterAttribute (typeof(ExpandableObjectTypeConverter))]
	[DefaultProperty("FileName")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Picture.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Picture))]
	public class Picture : KineticaRT.Controls.Control
	{
		#region License
#if KRT_license
		private License license = null;
		private	bool			demoMode ;
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
						if ((key == null) || (key != "17DDAC40A413" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "17DDAC40A413" || key == "4CD0571EE68D")) key = "{17DDAC40-A413-4e20-8C66-FC46109E61FA}" ; 
						context.SetSavedLicenseKey (typeof(Picture), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Picture), null) ;
					if (key == "{17DDAC40-A413-4e20-8C66-FC46109E61FA}") licence = new License ("Run") ;
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

		#region Type Converter (Unused)
		internal class PictureTypeConverter : ExpandableObjectConverter
		{
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))		return true ;

				return base.CanConvertTo(context, destinationType);
			}

			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					ConstructorInfo ctor	= value.GetType().GetConstructor(Type.EmptyTypes);

					return new InstanceDescriptor(ctor, null, false);
				}

				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Variables and Declarations
		//property variables
		private string					fileName ;
		private FillColor				fill ;
		private bool					play ;				//plays animatable images
		private VariableColorCollection	variableColors ;
		private bool					keepOriginalSize ;
		private ContentAlignment		alignment ;
		private Channels.IChannel		playSourceChannel ;		//source of 'play'
		private string					playSource ;

		//internal variables
		private	Bitmap			baseBmp ;
		private bool			newFrame ;
		private EventHandler	frameHandler ;
		private bool			playing ;			//combination of play and CanAnimate

		private Channels.ValueChangedEventHandler		playSrcHandler ;
		private ObjectSearchEventHandler				searchHdlr ;
		private PropertyChangedEventHandler				propertyHdlr ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Picture class
		/// </summary>
		public Picture()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#endif
			
			fileName			= "" ;
			fill				= new FillColor() ;
			variableColors		= new VariableColorCollection() ;
			play				= true ;
			keepOriginalSize	= false ;
			alignment			= ContentAlignment.MiddleCenter ;
			playSourceChannel	= null ;
			playSource			= "" ;

			baseBmp			= null ;
			newFrame		= false ;
			playing			= false ;

			frameHandler	= new EventHandler(OnFrameChanged) ;
			playSrcHandler	= new Channels.ValueChangedEventHandler(OnPlayChanged) ;
			propertyHdlr	= new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			searchHdlr		= new ObjectSearchEventHandler(OnObjectSearch) ;

			fill.PropertyChanged			+= propertyHdlr ;
			variableColors.PropertyChanged	+= propertyHdlr ;

			fill.ObjectSearch				+= searchHdlr ;
			variableColors.ObjectSearch		+= searchHdlr ;
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
				if (baseBmp != null)
				{
					baseBmp.Dispose() ;
				}

				if( components != null )
				{
					components.Dispose();
				}
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
			// Picture
			// 
			this.Name = "Picture";

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the image file to be displayed
		/// </summary>
		[Category("Picture")]
		[Description("Gets or sets the image file to be displayed")]
		[DefaultValue("")]
		[Editor(typeof(System.Windows.Forms.Design.FileNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string FileName
		{
			get
			{
				return fileName ;
			}
			set
			{
				if (fileName != value)
				{
					fileName = value ;

					OpenImage() ;
                    AddToQueue(true);

					OnPropertyChanged("FileName") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets a FillColor object
		/// </summary>
		/// <remarks>Replacement colors, including fill, are not available with animated images</remarks>
		[Category("Picture")]
		[Description("Replaceable Fill Color object")]
		public FillColor Fill
		{
			get
			{
				return fill ;
			}
			set
			{
				if (fill != null)
				{
					fill.PropertyChanged	-= propertyHdlr ;
					fill.ObjectSearch		-= searchHdlr ;
				}

				fill = value ;
				if (fill != null)
				{
					fill.PropertyChanged	+= propertyHdlr ;
					fill.ObjectSearch		+= searchHdlr ;
				}

				OpenImage() ;
				AddToQueue(true) ;
				OnPropertyChanged("Fill") ;
			}
		}
		private bool ShouldSerializeFill()
		{
			return !fill.IsDefaultState() ;
		}

		/// <summary>
		/// Gets or sets whether an animatable image is played
		/// </summary>
		/// <remarks>Replacement colors, including fill, are not available with animated images</remarks>
		[Category("Picture")]
		[Description("Gets or sets whether an animatable image is played")]
		[DefaultValue(true)]
		public bool Play
		{
			get
			{
				return play ;
			}
			set
			{
				if (play != value)
				{
					play = value ;
					if (play)
					{
						if (baseBmp != null && ImageAnimator.CanAnimate(baseBmp))
						{
							playing = true ;
							ImageAnimator.Animate(baseBmp, frameHandler) ;
						}
						else
						{
							playing = false ;
						}
					}
					else
					{
						playing = false ;
						if (baseBmp != null && ImageAnimator.CanAnimate(baseBmp))
						{
							ImageAnimator.StopAnimate(baseBmp, frameHandler) ;
//							baseBmp.SelectActiveFrame(FrameDimension.Page, defaultFrame) ;
						}
					}
					AddToQueue(false) ;
					OnPropertyChanged("Play") ;
				}
			}
		}

		///<summary>
		///Gets or sets the object that is the Source for the Play property
		///</summary>
		[Category("Picture")]
		[Description("Gets or sets the object that is the Source for the Play property")]
		public Channels.IChannel PlaySourceChannel
		{
			get
			{
				return playSourceChannel ;
			}
			set
			{
				if (playSourceChannel != value)
				{
                    lock (this)
                    {
                        //free old handler if any
                        if (playSourceChannel != null)
                        {
                            playSourceChannel.ValueChanged -= playSrcHandler;
                        }

                        //attach new source
                        playSourceChannel = value;
                        if (playSourceChannel != null)
                        {
                            playSourceChannel.ValueChanged += playSrcHandler;
                            Play = (playSourceChannel.Value != null) && (playSourceChannel.Value.ToDouble() != 0);
                        }
                    }
					OnPropertyChanged("PlaySourceChannel") ;
				}
			}
		}
		private bool ShouldSerializePlaySourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the object to be the PlaySource
		/// </summary>
		[Category("Picture")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the PlaySource")]
		public string PlaySource
		{
			get
			{
				return playSource ;
			}
			set
			{
				if (playSource != value)
				{
					playSource = value ;
					OnPropertyChanged("PlaySource") ;
				}
				//try and connect to source
				PlaySourceChannel = OnObjectSearch(playSource) as Channels.IChannel ;
			}
		}

		///<summary>
		///Gets or sets a collection of ReplaceableColor objects
		///</summary>
		/// <remarks>Replacement colors, including fill, are not available with animated images</remarks>
		[Category("Picture")]
		[Description("Gets or sets a collection of ReplaceableColor objects")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public VariableColorCollection VariableColors
		{
			get
			{
				return variableColors ;
			}
			set
			{
				if (variableColors != null)
				{
					variableColors.PropertyChanged	-= propertyHdlr ;
					variableColors.ObjectSearch		-= searchHdlr ;
				}

				variableColors = value ;
				if (variableColors != null)
				{
					variableColors.PropertyChanged	+= propertyHdlr ;
					variableColors.ObjectSearch		+= searchHdlr ;
				}

				OnPropertyChanged("VariableColors") ;
				AddToQueue(false) ;
			}
		}
		private bool ShouldSerializeVariableColors()
		{
			//always save if there are any items in the collection
			return variableColors.Count > 0 ;
		}

		/// <summary>
		/// Gets or sets whether the image remains its original size, rather than resizing to fit the control
		/// </summary>
		[Category("Picture")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the image remains its original size, rather than resizing to fit the control")]
		public bool KeepOriginalSize
		{
			get
			{
				return keepOriginalSize ;
			}
			set
			{
				if (keepOriginalSize != value)
				{
					keepOriginalSize = value ;
					AddToQueue(true) ;
					OnPropertyChanged("KeepOriginalSize") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Horizontal and Vertical Alignment of the image when KeepOriginalSize is true
		/// </summary>
		[Category("Picture")]
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Description("Gets or sets the Horizontal and Vertical Alignment of the image when KeepOriginalSize is true")]
		public ContentAlignment Alignment
		{
			get
			{
				return alignment ;
			}
			set
			{
				if (alignment != value)
				{
					alignment = value ;
					AddToQueue(false) ;
					OnPropertyChanged("Alignment") ;
				}
			}
		}
		#endregion

		#region Drawing
        /// <summary>
        /// OnPaintBackground
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)//ft ajout pour eviter un flicking de l image pcq cette fonction dessine le background en transparent, et il y avait un trop grand delais avant de repainter l<image via le AddToQueue
        {
            base.OnPaintBackground(e); //c<est cette ligne qui va painter le background transparent
            Repaint(true, e.Graphics); //ft contrairement au Control.Display, je call directement le Repaint, mais on a besoin du lock(lockObj) pour synchroniser les thread
        }
        private Object lockObj = new Object(); //ft oblige de remettre un lock pour synchroniser les thread du Repaint vs l<evenement OnPaintBackground 
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint (bool FullRedraw, Graphics g)
		{
            //ft ajout du lock this pcq j'ai retire le lock du OnPaint dans control.cs pour eviter des deadlock
            //entre OnPaint-Repaint execute par le main thread et le UpdateProc executé par Updater de Kineticart qui call aussi Repaint
            lock (lockObj) //ft oblige de remettre un lock pour synchroniser les thread du Repaint vs l<evenement OnPaintBackground
            {
                if (openImageNeeded)
                {
                    OpenImageActual();
                    openImageNeeded = false;
                }

                //lock(this)
                {
                    if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                    {
                        backColorChanged = false;
                        backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                    }
                    CalculateDisplayRectangle();

                    //basic drawing (appearance etc)
                    if (baseBmp == null)
                    {
                        base.Repaint(true, g);

                        SetDisplayRegion(g);
#if KRT_license
					if (demoMode)		DrawDemoText(g, DisplayRectangle, BackColor) ;
#endif
                    }
                    else
                    {
                        Bitmap bmp = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                        Graphics g2 = Graphics.FromImage(bmp);
                        Rectangle image_rect;

                        if (keepOriginalSize)
                        {
                            image_rect = new Rectangle(DisplayRectangle.Left, DisplayRectangle.Top, baseBmp.Width, baseBmp.Height);

                            switch (alignment)
                            {
                                case ContentAlignment.BottomCenter:
                                    {
                                        image_rect.X += (DisplayRectangle.Width - image_rect.Width) / 2;
                                        image_rect.Y = DisplayRectangle.Bottom - image_rect.Height;
                                    }
                                    break;

                                case ContentAlignment.BottomLeft:
                                    {
                                        image_rect.Y = DisplayRectangle.Bottom - image_rect.Height;
                                    }
                                    break;

                                case ContentAlignment.BottomRight:
                                    {
                                        image_rect.X = DisplayRectangle.Right - image_rect.Width;
                                        image_rect.Y = DisplayRectangle.Bottom - image_rect.Height;
                                    }
                                    break;

                                case ContentAlignment.MiddleCenter:
                                    {
                                        image_rect.X += (DisplayRectangle.Width - image_rect.Width) / 2;
                                        image_rect.Y += (DisplayRectangle.Height - image_rect.Height) / 2;
                                    }
                                    break;

                                case ContentAlignment.MiddleLeft:
                                    {
                                        image_rect.Y += (DisplayRectangle.Height - image_rect.Height) / 2;
                                    }
                                    break;

                                case ContentAlignment.MiddleRight:
                                    {
                                        image_rect.X = DisplayRectangle.Right - image_rect.Width;
                                        image_rect.Y += (DisplayRectangle.Height - image_rect.Height) / 2;
                                    }
                                    break;

                                case ContentAlignment.TopCenter:
                                    {
                                        image_rect.X += (DisplayRectangle.Width - image_rect.Width) / 2;
                                    }
                                    break;

                                case ContentAlignment.TopLeft:
                                    {
                                        //nothing to do
                                    }
                                    break;

                                case ContentAlignment.TopRight:
                                    {
                                        image_rect.X = DisplayRectangle.Right - image_rect.Width;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            image_rect = DisplayRectangle;
                        }

                        needFullRedraw = false;
                        g2.FillRectangle(backColorBrush, ClientRectangle);
                        DrawBorder(g2);

                        SetDisplayRegion(g2);
                        //have we got fill?
                        if (fill.Enabled)
                        {
                            Brush original_brush = new SolidBrush(fill.OriginalColor);

                            if (fill.Value > 0)
                            {
                                Rectangle rect = image_rect;
                                int offset = rect.Height - (int)(fill.Value * rect.Height);
                                Brush display_brush = new SolidBrush(fill.DisplayColor);

                                //draw a filled rectangle and an unfilled rectangle
                                //in the background
                                rect.Height = offset;
                                g2.FillRectangle(original_brush, rect);
                                rect.Height = image_rect.Height - offset;
                                rect.Y += (int)offset;

                                g2.FillRectangle(display_brush, rect);
                                display_brush.Dispose();
                            }
                            else
                            {
                                //draw the whole background
                                g2.FillRectangle(original_brush, image_rect);
                            }

                            original_brush.Dispose();
                        }

                        //are we animated?
                        if (playing && newFrame)
                        {
                            newFrame = false;
                            try
                            {
                                ImageAnimator.UpdateFrames();
                            }
                            catch (Exception e)
                            {
                                Trace.WriteLine(e.Message, "Animated Picture 'UpdateFrames' call failed");
                            }
                        }

                        if (!playing && variableColors.EnabledCount > 0)
                        {
                            //map variable colors
                            ImageAttributes attrs = new ImageAttributes();
                            ColorMap[] map = new ColorMap[variableColors.EnabledCount];
                            int index = 0;

                            foreach (ReplaceableColor clr in variableColors)
                            {
                                if (clr.Enabled)
                                {
                                    map[index] = new ColorMap();
                                    map[index].OldColor = clr.OriginalColor;
                                    map[index].NewColor = clr.DisplayColor;
                                    index++;
                                }
                            }

                            attrs.SetRemapTable(map);
                            g2.DrawImage(baseBmp, image_rect, 0, 0, baseBmp.Width, baseBmp.Height, GraphicsUnit.Pixel, attrs);
                        }
                        else
                        {
                            g2.DrawImage(baseBmp, image_rect);
                        }

#if KRT_license
					//demo mode?
					if (demoMode)
					{
						//write 'demo' in bottom right corner
						Rectangle		rect		= new Rectangle() ;

						rect.Width = 42 ;
						rect.Height = 16 ;
						rect.X = DisplayRectangle.Right - rect.Width ;
						rect.Y = DisplayRectangle.Bottom - rect.Height ;
						DrawDemoTextColor(g2, rect, Color.DarkOrchid) ;
					}
#endif

                        g2.Dispose();
                        
                        g.DrawImage(bmp, ClientRectangle);
                        bmp.Dispose();
                        
                    }
                    //GDIFlush g.Flush() ; 
                }
            }//ft fin lock 
		}
		#endregion

		#region Other methods
		//opens the image file and sets the fill color as transparent
		private void OpenImage()
		{
            openImageNeeded = true ;
        }
        bool openImageNeeded ;

        private void OpenImageActual () 
        {
			playing = false ;
			if (baseBmp != null)
			{
				if (ImageAnimator.CanAnimate(baseBmp))
				{
					ImageAnimator.StopAnimate(baseBmp, frameHandler) ;
				}
				baseBmp.Dispose() ;
				baseBmp = null ;
			}

			try
			{
				//this works for bmp, ico, jpg, gif...
				//string				path	= Path.Combine(System.Windows.Forms.Application.StartupPath, Filename) ;
                if (File.Exists(fileName)) //ft ajout pour eviter une exception
                {
                    baseBmp = (Bitmap)Image.FromFile(fileName);
                }
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.Message, "Failed to read file") ;
			}

			if (baseBmp != null)
			{
				if (play && ImageAnimator.CanAnimate(baseBmp))
				{
					playing = true ;
					ImageAnimator.Animate(baseBmp, frameHandler) ;
				}
				else if (fill.Enabled)
				{
					baseBmp.MakeTransparent(fill.OriginalColor) ;
				}
			}

			AddToQueue(true) ;
		}
		#endregion

		#region Property changed handling
		/// <summary>
		/// Handles PropertyChanged events from sub-objects
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (sender.Equals(fill) && (e.PropertyName == "Enabled" || e.PropertyName == "OriginalColor"))
			{
				//Need to re-open the image if original fill color or enabled has changed
				//(so transparent color for fill works)
				OpenImage() ;
			}

			AddToQueue(false) ;
		}

//		private void OnChildValueChanged (object sender, KineticaRT.Values.Value newVal)
//		{
//			if (fill.Enabled)
//			{
//				//changed value doesn't need a full redraw
//				AddToQueue(false) ;
//			}
//		}

		//new value coming in
		private void OnPlayChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			Play = (newValue.ToDouble() != 0) ;
		}

		private void OnFrameChanged(object o, EventArgs e) 
		{
			newFrame = true ;
			AddToQueue(false) ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
            lock (this)
            {
                if (playSource != "") PlaySourceChannel = OnObjectSearch(playSource) as Channels.IChannel;
                if (fill != null) fill.Connect();
                if (variableColors != null) variableColors.Connect();
            }
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
            lock (this)
            {
                if (playSourceChannel != null) PlaySourceChannel = null;
                if (fill != null) fill.Disconnect();
                if (variableColors != null) variableColors.Disconnect();
            }
		}
		#endregion
	}
}

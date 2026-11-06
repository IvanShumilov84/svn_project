using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Knob Control 
	/// </summary>
	[DefaultProperty("Value")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Knob.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Knob))]
	public class Knob : KineticaRT.Controls.Control
	{
		#region License
#if KRT_license
		private License license = null;
		private	bool					demoMode ;
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
						if ((key == null) || (key != "3FD7A7ADFF5A" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "3FD7A7ADFF5A" || key == "4CD0571EE68D")) key = "{3FD7A7AD-FF5A-4634-B662-DFFFF2D86EF3}" ; 
						context.SetSavedLicenseKey (typeof(Knob), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Knob), null) ;
					if (key == "{3FD7A7AD-FF5A-4634-B662-DFFFF2D86EF3}") licence = new License ("Run") ;
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
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//internal variables
		private	Bitmap					bmpBackground ;
		private double					currentAngle ;		//in degrees cw from top
		private double					lastAngle ;
		private bool					useTempValue ;
		private double					tempValue ;
		private bool					active ;
		private Values.Value			krtValue ;			//double or type mismatch
		private SolidBrush				knobBrush ;

		private Channels.ValueChangedEventHandler	srcHdlr ;
		private	PropertyChangedEventHandler			propertyHdlr ;

		//property variables
		private KineticaRT.Controls.Scale	scale ;
		private double						radius ;
		private object						value ;
		private Color						knobColor ;
		private Color						indicatorColor ;
		private	Channels.IChannel			sourceChannel ;
		private	Channels.IChannel			destinationChannel ;
		private string						source ;
		private string						destination ;
		private bool						updateOnDrag ;
		private	double						defaultValue ;
		private	bool						autoReturn ;
		private bool						useSourceFormat ;
		private bool						inputEnabled ;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Knob class
		/// </summary>
		public Knob()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#endif
			
			radius				= 0.6 ;
			value				= 0 ;
			knobColor			= Color.LightSteelBlue ;
			indicatorColor		= Color.SteelBlue ;
			sourceChannel		= null ;
			destinationChannel	= null ;
			source				= "" ;
			destination			= "" ;
			defaultValue		= 0 ;
			updateOnDrag		= true ;
			autoReturn			= false ;
			useSourceFormat		= true ;
			inputEnabled		= true ;

			//create scale properties with alternate defaults!
			scale = new KineticaRT.Controls.Scale(0.63,
								ScaleMarkPositions.Outside,
								ScaleMarkPositions.Outside) ;

			bmpBackground	= null ;
			lastAngle		= -1 ;
			currentAngle	= 0 ;
			useTempValue	= false ;
			tempValue		= 0 ;
			active			= false ;
			krtValue		= Values.Values.Zero ;
			knobBrush		= new SolidBrush(knobColor) ;

			srcHdlr = new Channels.ValueChangedEventHandler(OnValueChanged) ;
			propertyHdlr = new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

			//event handlers for sub-objects
			//(don't need to keep handlers, as objects never change)
			scale.PropertyChanged	+= new PropertyChangedEventHandler(OnChildPropertyChanged);
			scale.ObjectSearch		+= new ObjectSearchEventHandler(OnObjectSearch);
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
				if (bmpBackground != null)
				{
					bmpBackground.Dispose() ;
				}

				knobBrush.Dispose() ;

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
			// Knob
			// 
			this.Name = "Knob";

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the object that is the Source of the knob's value
		/// </summary>
		[Category("Knob")]
		[Description("Gets or sets the object that is the Source of the knob's value")]
		public Channels.IChannel SourceChannel
		{
			get
			{
				return sourceChannel;
			}
			set
			{
				if (sourceChannel != value)
				{
					//free old handler if any
					if (sourceChannel != null)		
					{
						sourceChannel.ValueChanged		-= srcHdlr ;
						sourceChannel.PropertyChanged	-= propertyHdlr ;
					}

					//attach new source
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		+= srcHdlr ;
						sourceChannel.PropertyChanged	+= propertyHdlr ;
						Value = sourceChannel.Value ;
					}
					
					SetFormat() ;

					OnPropertyChanged("SourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the object that is the Destination for the knob's value
		/// </summary>
		[Category("Knob")]
		[Description("Gets or sets the object that is the Destination for the knob's value")]
		public Channels.IChannel DestinationChannel
		{
			get
			{
				return destinationChannel;
			}
			set
			{
				if (destinationChannel != value)
				{
					destinationChannel = value ;

					if (destinationChannel != null)		destinationChannel.Value = krtValue ;

					OnPropertyChanged("DestinationChannel") ;
				}
			}
		}
		private bool ShouldSerializeDestinationChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the object to be the Source
		/// </summary>
		[Category("Knob")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Source")]
		public string Source
		{
			get
			{
				return source ;
			}
			set
			{
				if (source != value)
				{
					source = value ;
					OnPropertyChanged("Source") ;
				}
				//try and connect to source
				SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
			}
		}

		/// <summary>
		/// Gets or sets the name of the object to be the Destination
		/// </summary>
		[Category("Knob")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Destination")]
		public string Destination
		{
			get
			{
				return destination ;
			}
			set
			{
				if (destination != value)
				{
					destination = value ;
					OnPropertyChanged("Destination") ;
				}
				//try and connect to destination
				DestinationChannel = OnObjectSearch(destination) as Channels.IChannel ;
			}
		}
		
		/// <summary>
		/// Gets or sets the current Value
		/// </summary>
		[Category("Knob")]
		[Description("Gets or sets the current Value")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object Value
		{
			get
			{
				return this.value ;
			}
			set
			{
				if (this.value != value)
				{
					Values.Value		new_krt_value ;

					this.value = value;

					new_krt_value = value as Values.Double ;
					if ((new_krt_value == null) && (value is System.Double)) new_krt_value = new KineticaRT.Values.Double ((System.Double)value) ;
					if (new_krt_value == null) new_krt_value = Values.Values.TypeMismatch ;

					if (new_krt_value.CompareTo (krtValue) != 0)
					{
						krtValue = new_krt_value ;

						//set destination
						if (destinationChannel != null)	destinationChannel.Value = krtValue ;
						
						AddToQueue(true) ;
//						OnValueChanged() ;
					}
					OnPropertyChanged("Value") ;
				}
			}
		}

		///<summary>
		///Gets the Scale object
		///</summary>
		[Category("Knob")]
		[Description("Gets the Scale object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new KineticaRT.Controls.Scale Scale
		{
			get 
			{
				return scale ;
			}
		}
		private bool ShouldSerializeScale()
		{
			//always save the scale
			return true ;
		}
		/// <summary>
		/// Reset the Scale to its default settings
		/// </summary>
		public void ResetScale()
		{
			scale = new Scale() ;
		}

		/// <summary>
		/// Gets or sets whether the Value updates during a drag, or only when the drag is completed
		/// </summary>
		[Category("Knob")]
		[Description("Gets or sets whether the Value updates during a drag, or only when the drag is completed")]
		[DefaultValue(true)]
		public bool UpdateOnDrag
		{
			get
			{
				return updateOnDrag ;
			}
			set
			{
				if (updateOnDrag  != value)
				{
					updateOnDrag  = value;
					OnPropertyChanged("UpdateOnDrag") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the knob reverts to its DefaultValue after a drag
		/// </summary>
		[Category("Knob")]
		[Description("Gets or sets whether the knob reverts to its DefaultValue after a drag")]
		[DefaultValue(false)]
		public bool AutoReturn
		{
			get
			{
				return autoReturn ;
			}
			set
			{
				if (autoReturn != value)
				{
					autoReturn = value;
					OnPropertyChanged("AutoReturn") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Default Value
		/// </summary>
		/// <remarks>This is the value to which the knob will revert after a Drag if AutoReturn is true</remarks>
		[Category("Knob")]
		[Description("Gets or sets the Default Value")]
		[DefaultValue(0.0)]
		public double DefaultValue
		{
			get
			{
				return defaultValue ;
			}
			set
			{
				if (defaultValue != value)
				{
					defaultValue = value ;
					OnPropertyChanged("DefaultValue") ;

					//set value to new default
					Value = new KineticaRT.Values.Double(defaultValue) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Color of the Knob
		/// </summary>
		[Category("Knob")]
		[Description("Gets or sets the Color of the Knob")]
		public Color KnobColor
		{
			get
			{
				return knobColor;
			}
			set
			{
				if (knobColor != value)
				{
					knobColor = value;
					knobBrush.Color = knobColor ;
					AddToQueue(true) ;
					OnPropertyChanged("KnobColor") ;
				}
			}
		}
		private bool ShouldSerializeKnobColor()
		{
			return knobColor != Color.LightSteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetKnobColor()
		{
			knobColor = Color.LightSteelBlue ;
		}

		/// <summary>
		/// Gets or sets the Color of the Indicator mark
		/// </summary>
		[Category("Knob")]
		[Description("Gets or sets the Color of the Indicator mark")]
		public Color IndicatorColor
		{
			get
			{
				return indicatorColor;
			}
			set
			{
				if (indicatorColor != value)
				{
					indicatorColor = value;
					AddToQueue(true) ;
					OnPropertyChanged("IndicatorColor") ;
				}
			}
		}
		private bool ShouldSerializeIndicatorColor()
		{
			return indicatorColor != Color.SteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetIndicatorColor()
		{
			indicatorColor = Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or sets the radius of the Knob as a proportion of the radius of the Control (0..1)
		/// </summary>
		[Category("Knob")]
		[DefaultValue(0.6)]
		[Description("Gets or sets the radius of the Knob as a proportion of the radius of the control (0..1)")]
		public double Radius
		{
			get
			{
				return radius ;
			}
			set
			{
				if (radius != value)
				{
					radius = value ;
					AddToQueue(true) ;
					OnPropertyChanged("Radius") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Knob gets its ToolTip, Minimum and Maximum
		/// from its Source channel
		/// </summary>
		[Category("Knob")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Knob gets its ToolTip, Minimum and Maximum from its Source channel")]
		public bool UseSourceFormat
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
						SetFormat() ;
					}
					OnPropertyChanged("UseSourceFormat") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether user input is Enabled
		/// </summary>
		/// <remarks>This differs from Enabled by not preventing drag and drop or tooltips</remarks>
		[Category("Knob")]
		[DefaultValue(true)]
		[Description("Gets or sets whether user input is Enabled")]
		public bool InputEnabled
		{
			get
			{
				return inputEnabled ;
			}
			set
			{
				if (inputEnabled != value)
				{
					inputEnabled = value ;
					OnPropertyChanged("InputEnabled") ;
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

                if (CalculatePosition() || FullRedraw || resized || backColorChanged)
                {
                    //handle has moved - redraw
                    if (FullRedraw || resized || backColorChanged)
                    {
                        if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                        {
                            backColorChanged = false;
                            backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                        }
                        //need to redraw the whole lot from scratch - into the background bitmap
                        if (resized)
                        {
                            if (bmpBackground != null) bmpBackground.Dispose();
                            bmpBackground = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                            resized = false;
                            FullRedraw = true;
                        }

                        Graphics gBmp = Graphics.FromImage(bmpBackground);

                        //basic drawing (appearance etc)
                        base.Repaint(true, gBmp);

                        SetDisplayRegion(gBmp);

                        scale.Draw(gBmp, DisplayRectangle, ScaleOrientations.Circular, this.Font, 1);
                        gBmp.Dispose();
                    }

                    Bitmap bmp = (Bitmap)bmpBackground.Clone();
                    Graphics g2 = Graphics.FromImage(bmp);

                    SetDisplayRegion(g2);
                    g2.SmoothingMode = SmoothingMode.AntiAlias;
                    DrawKnob(g2, FullRedraw);

#if KRT_license
				if (demoMode)			DrawDemoText(g2, DisplayRectangle, knobColor) ;
#endif
                    g2.Dispose();

                    //copy the whole lot to the screen
                    g.DrawImage(bmp, 0, 0);
                    bmp.Dispose();
                    //GDIFlush g.Flush() ; 
                }
            }
		}

		//returns true if angle changed since last draw
		private bool CalculatePosition()
		{
			double	fraction ;
			bool	changed			= false ;

			if (useTempValue)
			{
				fraction = scale.ValueToFraction(tempValue) ;
			}
			else if (krtValue is Values.Double)
			{
				fraction = scale.ValueToFraction(krtValue.ToDouble()) ;
			}
			else	//error value
			{
				currentAngle = -1 ;
				return true ;
			}

			if (fraction > 1)
			{
				fraction = 1 ;
			}
			else if (fraction < 0)
			{
				fraction = 0 ;
			}

			if (!scale.AntiClockwise)
			{
				currentAngle = scale.ZeroAngle + (scale.SweepAngle * fraction) ;  
			}
			else
			{
				currentAngle = scale.ZeroAngle - (scale.SweepAngle * fraction) ;  
			}

			if (currentAngle < 0)
			{
				currentAngle += 360 ;
			}
			else if (currentAngle > 360)
			{
				currentAngle -= 360 ;
			}

			if (currentAngle != lastAngle)
			{
				//check if it differs by a whole pixel from last drawn angle (indicator mark)
				double		new_x ;
				double		new_y ;
				double		old_x ;
				double		old_y ;
				double		x_diff ;
				double		y_diff ;
				Rectangle	rect			= DisplayRectangle ;

				new_x	= rect.Width * (0.5 - Math.Sin(currentAngle * Math.PI / 180) * radius * 0.45) ;
				new_y	= rect.Height * (0.5 - Math.Cos(currentAngle * Math.PI / 180) * radius * 0.45) ;
				old_x	= rect.Width * (0.5 - Math.Sin(lastAngle * Math.PI / 180) * radius * 0.45) ;
				old_y	= rect.Height * (0.5 - Math.Cos(lastAngle * Math.PI / 180) * radius * 0.45) ;

				x_diff = Math.Abs(new_x - old_x) ;
				y_diff = Math.Abs(new_y - old_y) ;

				if (x_diff >= 1 || y_diff >= 1)
				{
					changed = true ;
				}
			}

			return changed ;
		}

		//draws base and handle
		private void DrawKnob(Graphics g, bool FullRedraw)
		{
			Rectangle		knob_rect	= new Rectangle() ;

			knob_rect.X		= ScaleX (0.5 - radius * 0.5) ;
			knob_rect.Y		= ScaleY (0.5 - radius * 0.5) ;
			knob_rect.Width = (int)(DisplayRectangle.Width * radius) + 1 ;
			knob_rect.Height= (int)(DisplayRectangle.Height * radius) + 1 ;

			//draw base
			if (FullRedraw)
			{
				GraphicsPath	outer_path ;
				Region			rgn ;

				g.FillEllipse(knobBrush, knob_rect) ;

				outer_path = CalculateShapePath(knob_rect, Shapes.Ellipse) ;
				rgn = new Region(outer_path) ;
				g.Clip = rgn ;
                DrawShapedBorder(g, knob_rect, BorderType.Raised, knobColor, Shapes.Ellipse, 2);
				rgn.Dispose() ;
			}
			else	//just do the inner circle
			{
				knob_rect.Inflate(-2, -2) ;
				g.FillEllipse(knobBrush, knob_rect) ;
			}

			//draw handle
			DrawHandle(g) ;

			//done the draw now - update the last angle
			lastAngle = currentAngle ;
		}

		private void DrawHandle(Graphics g)
		{
			if (krtValue is Values.Double)
			{
				Color				left ;
				Color				right ;
				double				handle_width	= 0.15 ;
				Pen					pen ;
				int					pen_width ;
				double				offset ;
				Point				left_start		= new Point() ;
				Point				right_start		= new Point() ;
				Point				left_end		= new Point() ;
				Point				right_end		= new Point() ;
				Point				ind_start		= new Point() ;
				Point				ind_end			= new Point() ;
				double				rad_angle		= (currentAngle - 90) * Math.PI / 180 ;

				pen_width = (int)Math.Min(DisplayRectangle.Width / 50.0, DisplayRectangle.Height / 50.0) ;

				CalculateSoftShadingLine(currentAngle, out right, out left) ;

				//left start, right end
				offset			= DisplayRectangle.Width * (Math.Cos(rad_angle + handle_width) * radius * 0.45) ;
				left_start.X	= ScaleX(0.5) + (int)offset ;
				right_end.X		= ScaleX(0.5) - (int)offset ;

				offset			= DisplayRectangle.Height * (Math.Sin(rad_angle + handle_width) * radius * 0.45) ;
				left_start.Y	= ScaleY(0.5) + (int)offset ;
				right_end.Y		= ScaleY(0.5) - (int)offset ;

				//right start, left end
				offset			= DisplayRectangle.Width * (Math.Cos(rad_angle - handle_width) * radius * 0.45) ;
				right_start.X	= ScaleX(0.5) + (int)offset ;
				left_end.X		= ScaleX(0.5) - (int)offset ;

				offset			= DisplayRectangle.Height * (Math.Sin(rad_angle - handle_width) * radius * 0.45) ;
				right_start.Y	= ScaleY(0.5) + (int)offset ;
				left_end.Y		= ScaleY(0.5) - (int)offset ;

				pen = new Pen(left, pen_width) ;
				g.DrawLine(pen, left_start, left_end) ;
				pen.Color = right ;
				g.DrawLine(pen, right_start, right_end) ;

				// Draw Indicator
				ind_start.X	= ScaleX (0.5 + Math.Cos(rad_angle) * radius * 0.45) ;
				ind_start.Y	= ScaleY (0.5 + Math.Sin(rad_angle) * radius * 0.45) ;
				ind_end.X	= ScaleX (0.5 + Math.Cos(rad_angle) * radius * 0.3) ;
				ind_end.Y	= ScaleY (0.5 + Math.Sin(rad_angle) * radius * 0.3) ;

				pen.Color = indicatorColor ;
				pen.Width = active ? (pen_width + 1) : pen_width / 2 ;
				g.DrawLine(pen, ind_start, ind_end) ;

				pen.Dispose() ;
			}
			//else invalid value - draw nothing

			//Draw 'unconnected' now - TBD
		}

		//  Calculates shading relative to the angle given
		private void CalculateSoftShadingLine(double Angle,	out Color cwSide, out Color	acwSide)
		{
			double		light ;
			double		l_min		= 0.1 ;
			double		l_change	= 0.4 ;
			double		dark ;
			double		d_min		= -0.3 ;
			double		d_change	= 0.8 ;

			if (Angle > 45 && Angle <= 135)
			{
				light	= l_min + l_change - l_change * (Angle - 45) / 90 ;
				dark	= d_min + d_change - d_change * (Angle - 45) / 90 ;

				acwSide = ControlPaint.Dark(knobColor, (float)dark) ;
				cwSide = ControlPaint.Light(knobColor, (float)light) ;
			}
			else if (Angle > 135 && Angle <= 225)
			{
				light	= l_min + l_change * (Angle - 135) / 90 ;
				dark	= d_min + d_change * (Angle - 135) / 90 ;

				cwSide = ControlPaint.Dark(knobColor, (float)dark) ;
				acwSide = ControlPaint.Light(knobColor, (float)light) ;
			}
			else if (Angle > 225 && Angle <= 315)
			{
				light	= l_min + l_change - l_change * (Angle - 225) / 90 ;
				dark	= d_min + d_change - d_change * (Angle - 225) / 90 ;

				acwSide = ControlPaint.Light(knobColor, (float)light) ;
				cwSide = ControlPaint.Dark(knobColor, (float)dark) ;
			}
			else
			{
				Angle = (Angle >= 315) ? Angle : Angle + 360 ;

				light	= l_min + l_change * (Angle - 315) / 90 ;
				dark	= d_min + d_change * (Angle - 315) / 90 ;

				cwSide = ControlPaint.Light(knobColor, (float)light) ;
				acwSide = ControlPaint.Dark(knobColor, (float)dark) ;
			}
		}
		#endregion

		#region Other methods
		/// <summary>
		/// Sets up the tool tip and scale range based on the Source channel
		/// </summary>
		private void SetFormat()
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel		ch ;

				ch = sourceChannel as Channels.IInfoChannel ;
				if (ch != null)
				{
					Channels.INumericChannel		num ;

					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;

					num = ch as Channels.INumericChannel ;
					if (num != null)
					{
						scale.Minimum	= num.Minimum ;
						scale.Maximum	= num.Maximum ;
					}
				}
			}
		}
		#endregion

		#region Mouse Event Handling
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (inputEnabled)
			{
				double		new_value		= PositionToValue(e.X, e.Y) ;
				
				Capture = true ;
				SetValueFromInput(new_value, false) ;
			}
			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (inputEnabled && Capture)
			{
				double		new_value		= PositionToValue(e.X, e.Y) ;

				SetValueFromInput(new_value, false) ;
			}
			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (inputEnabled && Capture)
			{
				if (updateOnDrag)
				{
					//set new position
					double		new_value		= PositionToValue(e.X, e.Y) ;

					SetValueFromInput(new_value, true) ;
				}

				if (autoReturn)
				{
					Value = new KineticaRT.Values.Double(defaultValue) ;
				}
			}
			if (Capture)		Capture = false ;

			base.OnMouseUp(e) ;
		}

		/// <summary>
		/// MouseEnter Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseEnter(EventArgs e)
		{
			if (!active)
			{
				active = true ;
				AddToQueue(true) ;
			}

			base.OnMouseEnter(e) ;
		}

		/// <summary>
		/// MouseLeave Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			if (active)
			{
				active = false ;
				AddToQueue(true) ;
			}

			base.OnMouseLeave(e) ;
		}

		private void SetValueFromInput(double newValue, bool Release)
		{
			if (Release)
			{
				useTempValue = false ;
			}

			if (updateOnDrag || (!updateOnDrag && Release) )
			{
				//set value
				Value = new KineticaRT.Values.Double(newValue) ;
			}
			else
			{
				tempValue = newValue ;
				useTempValue = true ;

				//force a redraw
				AddToQueue(false) ;
			}
		}

		//  calculates a value from a position. 
		private double PositionToValue(long XPos, long YPos)
		{
			double		theta ;				//real angle
			double		angle ;				//angle round from ZeroAngle
			double		fraction ;
			double		dx ;
			double		dy ;

			//convert coordinates to angle
			dx = XPos - ScaleX (0.5) ;
			dy = YPos - ScaleY (0.5) ;

			theta = Math.Atan2(dy, dx) * 180 / Math.PI ;
			theta += 90 ;		//I want to measure from vertical not x-axis
			if (theta < 0)		theta += 360 ;

			//...and if the knob was square this would be...
			theta = UnscaleAngle(theta) ;

			if (scale.AntiClockwise)
			{
				angle = theta - 360 + scale.ZeroAngle ;
			}
			else
			{
				angle = 360 + theta - scale.ZeroAngle ;
			}

			//ensure between 0 and 360 degrees
			if (angle >= 360)			angle -= 360 ;
			if (angle < 0)				angle += 360 ;

			//convert angle to fraction
			if ((!scale.AntiClockwise && (angle > scale.SweepAngle))	||
				(scale.AntiClockwise && angle < (360 - scale.SweepAngle)) )
			{
				//between sweep and zero (out of range)
				// - which is it closer to?
				if (!scale.AntiClockwise)
				{
					fraction = (angle > scale.SweepAngle + (360 - scale.SweepAngle)/2 ) ? 0 : 1 ;
				}
				else
				{
					fraction = (angle > (360 - scale.SweepAngle)/2 ) ? 1 : 0 ;
				}
			}
			else		//in range
			{
				if (!scale.AntiClockwise)
				{
					fraction		= angle / scale.SweepAngle ;
				}
				else
				{
					fraction		= (360 - angle) / scale.SweepAngle ;
				}
			}

			return scale.FractionToValue(fraction) ;
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
				Source = e.Data.GetData(DataFormats.Text).ToString() ;
			}

			base.OnDragDrop(e) ;
		}
		#endregion

		#region Source event handling (value and properties)
		//new value coming in
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value v)
		{
			//get most up to date value
			Value = sender.Value ;
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			SetFormat() ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
			if (destination != "")	DestinationChannel = OnObjectSearch(destination) as Channels.IChannel ;

			if (scale != null)		scale.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
			if (destinationChannel != null)		DestinationChannel = null ;

			if (scale != null)					scale.Disconnect() ;
		}
		#endregion
	}
}

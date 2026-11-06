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
	/// KineticaRT Surface control
	/// </summary>
	public class Surface : KineticaRT.Controls.Control
	{
		#region Variables
		//property variables
		private object					value ;
		private KineticaRT.Channel		sourceChannel ;
		private KineticaRT.Channel		destinationChannel ;

		//internal variables
		private Values.Value			krtValue ;			//double or type mismatch

		private Channel.ValueChangedEventHandler		srcHdlr ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		public Surface()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			krtValue = Values.Values.Zero ;

			this.value = 0 ;
			sourceChannel = null ;
			destinationChannel = null ;

			srcHdlr = new Channel.ValueChangedEventHandler(OnValueChanged);

			//default to elliptical
			Shape = KineticaRT.Controls.Shapes.Rectangle ;
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
			// Surface
			// 
			this.Name = "Surface";
			this.Size = new System.Drawing.Size(40, 40);
		}
		#endregion

		#region Properties
		[DefaultValue(Shapes.Ellipse)]
		public override Shapes Shape
		{
			get { return base.Shape; }
			set { base.Shape = value; }
		}

		/// <summary>
		/// Current Value
		/// </summary>
		[Category("Surface")]
		[Description("Current Value")]
		[DefaultValue(0)]
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					Values.Value		new_krt_value ;

					this.value = value ;

					try
					{
						//double		temp	= Convert.ToDouble(this.value) ;

						new_krt_value = (KineticaRT.Values.Value) this.value ; //new Values.Double(temp) ;
					}
					catch
					{
						//not a double
						new_krt_value = Values.Values.TypeMismatch ;
					}

					if (new_krt_value != krtValue)
					{
						krtValue = new_krt_value ;

						//set destination
						if (destinationChannel != null)		destinationChannel.Value = krtValue ;
							
						AddToQueue(false) ;
						OnValueChanged() ;
					}
					OnPropertyChanged("Value") ;
				}
			}
		}

		/// <summary>
		/// Object that is the Source of the Surface's value
		/// </summary>
		[Category("Surface")]
		[Description("Object that is the Source of the Surface's value")]
		[DefaultValue(null)]
		public KineticaRT.Channel SourceChannel
		{
			get
			{
				return sourceChannel;
			}
			set
			{
				if (sourceChannel != value)
				{
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged -= srcHdlr ;
					}
					
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged += srcHdlr ;
					}
					OnPropertyChanged("SourceChannel") ;
				}
			}
		}

		/// <summary>
		/// Object that is the Destination for the indocator's value
		/// </summary>
		[Category("Surface")]
		[Description("Object that is the Destination for of the Surface's value")]
		[DefaultValue(null)]
		public KineticaRT.Channel DestinationChannel
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

		#endregion

		#region Drawing
		[System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
		private static extern bool Rectangle(
			IntPtr hdc,
			int ulCornerX, int ulCornerY,
			int lrCornerX, int lrCornerY);

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
		struct TRIVERTEX 
		{
			[System.Runtime.InteropServices.FieldOffset(0)]public UInt32      x;
			[System.Runtime.InteropServices.FieldOffset(4)]public UInt32      y;
			[System.Runtime.InteropServices.FieldOffset(8)]public UInt16       Red;
			[System.Runtime.InteropServices.FieldOffset(10)]public UInt16       Green;
			[System.Runtime.InteropServices.FieldOffset(12)]public UInt16       Blue;
			[System.Runtime.InteropServices.FieldOffset(14)]public UInt16       Alpha;
		};

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
		struct GRADIENT_TRIANGLE 
		{
			[System.Runtime.InteropServices.FieldOffset(0)]public UInt32   Vertex1;
			[System.Runtime.InteropServices.FieldOffset(4)]public UInt32   Vertex2;
			[System.Runtime.InteropServices.FieldOffset(8)]public UInt32   Vertex3;
		}

		[System.Runtime.InteropServices.DllImportAttribute("Msimg32.dll")]
		private static extern bool GradientFill(
			IntPtr hdc,					// handle to DC
			ref TRIVERTEX[] Vertex,	// array of vertices
			UInt32 NumVertex,			// number of vertices
			ref GRADIENT_TRIANGLE[] Mesh,	// array of gradients
			UInt32 NumMesh,			// size of gradient array
			UInt32 Mode				// gradient fill mode
			);

		[System.Runtime.InteropServices.DllImportAttribute("Kernel32.dll")]
		private static extern UInt16 GetLastError();

		protected override void Repaint(bool FullRedraw, Graphics g)
		{
			Color		back ;

			if (krtValue is Values.Double)
			{
				back = BackColor ;
			}
			else
			{
				back = BackColor ;
			}

			//			base.repaint(FullRedraw, g) ;		//can't do this - need to use other colors
			CalculateDisplayRectangle() ;

			g.SmoothingMode = SmoothingMode.AntiAlias;

			//if (FullRedraw)
			{
				needFullRedraw = false ;

				g.FillRectangle(new SolidBrush(back), DisplayRectangle);
				DrawBorder(g, back) ;

				StringFormat	format	= new StringFormat() ;

				format.Alignment = StringAlignment.Center ;
				format.LineAlignment = StringAlignment.Center ;

				//g.DrawString (krtValue.ToString (), DefaultFont, Brushes.DarkGoldenrod, DisplayRectangle, format) ;

				DrawDemoText(g, DisplayRectangle, back) ;
			}

			KineticaRT.Values.DoubleArray dblary = krtValue as KineticaRT.Values.DoubleArray ;
		
			if (dblary != null)
			{
				double [] val = dblary.ToDoubleArray() ;

				//SolidBrush brush = new SolidBrush(Color.LightBlue);
				Pen pen = Pens.Black ;
				Point [] points = new Point [4] ;
				points[0].X = 0 ;
				points[0].Y = (int) (val[0] * 100) ;

				points[1].X = 100 ;
				points[1].Y = (int) (val[1] * 100) ;

				points[2].X = 100 ;
				points[2].Y = (int) (val[1] * 100 - 20) ;

				points[3].X = 0 ;
				points[3].Y = (int) (val[0] * 100 - 20) ;

				g.SmoothingMode = SmoothingMode.HighSpeed ;

				//for (int count = 0 ; count < 1000 ; count ++)
				{
					//g.FillPolygon (brush, points, FillMode.Alternate) ;
					g.DrawPolygon (pen, points) ;
				}
			}


//			{
//				IntPtr hdc = new IntPtr();
//				hdc = g.GetHdc();
//
//				TRIVERTEX[] vertex ;
//				GRADIENT_TRIANGLE[] mesh ;
//				vertex = new TRIVERTEX [4] ;
//				mesh = new GRADIENT_TRIANGLE [4] ;
//
//				vertex [0] .x       =  0;
//				vertex [0] .y       =  0;
//				vertex [0] .Red     =  0x0000;
//				vertex [0] .Green   =  0x0000;
//				vertex [0] .Blue    =  0x0000;
//				vertex [0] .Alpha   =  0x0000;
//
//				vertex [1] .x       =  100;
//				vertex [1] .y       =  0;
//				vertex [1] .Red     =  0x0000;
//				vertex [1] .Green   =  0x0000;
//				vertex [1] .Blue    =  0xff00;
//				vertex [1] .Alpha   =  0x0000;
//
//				vertex [2] .x       =  200;
//				vertex [2] .y       =  132; 
//				vertex [2] .Red     =  0x0000;
//				vertex [2] .Green   =  0xff00;
//				vertex [2] .Blue    =  0xff00;
//				vertex [2] .Alpha   =  0x0000;
//
//				vertex [3] .x       =  0;
//				vertex [3] .y       =  32;
//				vertex [3] .Red     =  0xff00;
//				vertex [3] .Green   =  0xff00;
//				vertex [3] .Blue    =  0xff00;
//				vertex [3] .Alpha   =  0x0000;
//
//				mesh[0].Vertex1   = 0 ;
//				mesh[0].Vertex2   = 1 ;
//				mesh[0].Vertex3   = 2 ;
//
//				mesh[1].Vertex1   = 0 ;
//				mesh[1].Vertex2   = 2 ;
//				mesh[1].Vertex3   = 3 ;
//
//				bool fred = GradientFill (hdc, ref vertex, 4, ref mesh, 2, 2) ;
//				UInt16 bill = GetLastError () ;
//
//				fred = Rectangle(hdc, 10, 70, 110, 120);
//				bill = GetLastError () ;
//
//				g.ReleaseHdc(hdc);
//
//			}

//			{
//				// Put the points of a polygon in an array.
//				Point[] points = {
//									 new Point(75, 0),
//									 new Point(100, 50),
////									 new Point(150, 50)	,
////									new Point(112, 75),
////									new Point(150, 150),
////									new Point(75, 100),
////									new Point(0, 150),
////									new Point(37, 75),
////									new Point(0, 50),
//									new Point(50, 50)};
//
//				// Use the array of points to construct a path.
//				GraphicsPath path = new GraphicsPath();
//				path.AddLines(points);
//
//				// Use the path to construct a path gradient brush.
//				PathGradientBrush pthGrBrush = new PathGradientBrush(path);
//
//				// Set the color at the center of the path to red.
//				pthGrBrush.CenterColor = Color.FromArgb(255, 255, 0, 0);
//				pthGrBrush.CenterPoint = new PointF(100, 50);
//
//				// Set the colors of the points in the array.
//				Color[] colors = {
//									Color.FromArgb(255, 255, 0, 0),
//									Color.FromArgb(255, 0, 255, 0),
////									Color.FromArgb(255, 0, 0, 255), 
////									Color.FromArgb(255, 255, 255, 255),
////									Color.FromArgb(255, 0, 0, 0),
////									Color.FromArgb(255, 0, 255, 0),
////									Color.FromArgb(255, 0, 0, 255),
////									Color.FromArgb(255, 255, 255, 255),
////									Color.FromArgb(255, 0, 0, 0),  
//									Color.FromArgb(255, 0, 0, 255)};
//
//				pthGrBrush.SurroundColors = colors;
//
//				// Fill the path with the path gradient brush.
//				//for (int index = 0 ; index < 1000 ; index++) g.FillPath(pthGrBrush, path);
//			}
		}

		#endregion

		#region New Value handling (in and out)
		private void OnValueChanged (Channel sender, KineticaRT.Values.Value newValue)
		{
			Value = newValue ;
		}

		public event ValueChangedEventHandler ValueChanged;   
		protected virtual void OnValueChanged()
		{
			if (ValueChanged != null) 
			{
				ValueChanged(this, krtValue);  //Invokes the delegates.
			}
		}
		#endregion
	}
}

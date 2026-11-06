using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Timers;
using KineticaRT;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT Display Control.
	/// Displays a string or numeric value as text
	/// </summary>
	[DefaultProperty("Source")]
	[ToolboxBitmap(typeof(Example))]
	public class Example : KineticaRT.Controls.Control
	{
		#region Variables

		//properties
		private object				value ;
		private	Channels.IChannel	sourceChannel ;
		private string				source ;
		private bool				useSourceFormat ;
		private bool				inputEnabled ;
		private string				format ;

		//internal variables
		private Channels.ValueChangedEventHandler	srcHdlr ;
		private PropertyChangedEventHandler			propertyHdlr ;

		private bool				resizeBox ;
		private Values.Value		krtValue ;
		private Rectangle			textRect ;
		private StringFormat		textFormat ;
		private string				text ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Display class
		/// </summary>
		public Example()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			value				= new KineticaRT.Values.Text("") ;
			sourceChannel		= null ;
			source				= "" ;
			useSourceFormat		= true ;
			inputEnabled		= true ;
			format				= "" ;
			text				= "-||-" ;

			resizeBox		= true ;
			krtValue		= new KineticaRT.Values.Text("") ;
			textFormat		= new StringFormat() ;
			textFormat.Trimming		= StringTrimming.None ;
			textFormat.Alignment	= StringAlignment.Near ;
			textFormat.LineAlignment= StringAlignment.Center ;

			textRect		= Rectangle.Empty ;

			srcHdlr		= new Channels.ValueChangedEventHandler(OnSourceValueChanged) ;
			propertyHdlr= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

			this.SetStyle(ControlStyles.Selectable, true) ;
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
			this.SuspendLayout();
			this.BorderStyle = KineticaRT.Controls.BorderStyles.Indented;
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Name = "Example";
			this.Size = new System.Drawing.Size(150, 24);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or Sets the style of the Control's Border
		/// </summary>
		[Category("Control")]
		[DefaultValue(BorderStyles.Indented)]
		[Description("Gets or Sets the style of the Control's Border")]
		public new BorderStyles BorderStyle
		{
			get	{ return base.BorderStyle; }
			set	{ base.BorderStyle = value; }
		}

		/// <summary>
		/// Gets or sets the Cursor that appears when the mouse passes over the control
		/// </summary>
		[Category("Appearance")]
		[Description("Gets or sets the Cursor that appears when the mouse passes over the control")]
		public override Cursor Cursor
		{
			get	{ return base.Cursor; }
			set	{ base.Cursor = value; }
		}
		private bool ShouldSerializeCursor()
		{
			return (base.Cursor != (inputEnabled ? Cursors.IBeam : Cursors.Default)) ;
		}

		/// <summary>
		/// Gets or sets the object that is the Source of the display's value
		/// </summary>
		[Category("Display")]
		[Description("Gets or sets the object that is the Source of the display's value")]
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

					//attach new sourceChannel
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		+= srcHdlr ;
						sourceChannel.PropertyChanged	+= propertyHdlr ;
						
						SetFormat() ;
					}

					OnSourceValueChanged (sourceChannel, sourceChannel != null ? sourceChannel .Value : KineticaRT.Values.Values.NotConnected) ;
					
					OnPropertyChanged("SourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
		}


		/// <summary>
		/// Gets or sets the name of the object to be the Source
		/// </summary>
		[Category("Display")]
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
		/// Gets the current Value
		/// </summary>
		[Category("Display")]
		[Description("Gets or sets the current Value")]
		public object Value
		{
			get
			{
				return this.value ;
			}
		}
		private bool ShouldSerializeValue()
		{
			return false ;		//save text string but not value object
		}

		/// <summary>
		/// Gets or sets whether the Display gets its ToolTip, Minimum and Maximum
		/// from the its Source channel
		/// </summary>
		[Category("Display")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Display gets its ToolTip, Minimum and Maximum from its Source channel")]
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

		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
			CalculateRectangles() ;
			if (FullRedraw || resizeBox)
			{
				Rectangle		rect		= ClientRectangle ;

				needFullRedraw = false ;

				if (shape == Shapes.Rectangle)
				{
					int border_width = BorderWidthPixels(rect);
					DrawSquareBorder(g, rect, borderType, borderColor, border_width) ;
				}
				else
				{
					g.FillRectangle(backBrush, rect);
					DrawBorder(g) ;
				}
			}

			//ensure the remainder is drawn inside the border
			if (shape == Shapes.Rectangle)
			{
				Rectangle		rect		= ClientRectangle ;
				int border_width = BorderWidthPixels(rect);
				rect.Inflate (-border_width,-border_width) ;
				Region new_region = new Region(rect) ;
				new_region.Intersect (g.Clip) ;
				g.Clip = new_region ;
			}
			else
			{
				GraphicsPath	path	= CalculateShapePath(DisplayRectangle, shape) ;
				
				//g.Clip = new Region(path) ;
				Region new_region = new Region(path) ;
				new_region.Intersect (g.Clip) ;
				g.Clip = new_region ;
				path.Dispose() ;
			}
			g.FillRectangle(backBrush, textRect); 

			Rectangle		text_rect	= textRect ;
			Brush			brush		= new SolidBrush(ForeColor) ;


			g.DrawString(text, this.Font, new SolidBrush(ForeColor), text_rect, textFormat) ;
			brush.Dispose() ;

			resizeBox = false ;
		}

		private void CalculateRectangles()
		{
			CalculateDisplayRectangle() ;

			textRect = DisplayRectangle ;	
		}

		#endregion

		#region Other Methods
		/// <summary>
		/// Sets the controls window to the relevant size and shape
		/// Call when shape changes, or control resizes
		/// </summary>
		protected override void SetWindowRegion()
		{
			if (shape == Shapes.Rectangle)
			{
				this.Region = null ;
			}
			else
			{
				GraphicsPath	path	= CalculateShapePath(ClientRectangle, shape) ;

				this.Region = new System.Drawing.Region(path) ;
				path.Dispose() ;
			}
		}

		/// <summary>
		/// Gets or sets the text format of the value from a Source.
		/// </summary>
		[Description("Eg. C,E,F3,G6,N")]
		[Category("Example")] 
		[DefaultValue("")]
		public string Format
		{
			get
			{
				return format ;
			}
			set
			{
				if (format != value)
				{
					format = value ;
					OnPropertyChanged("Format") ;
				}
			}
		}

		private void SetFormat()
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel		ch ;

				ch = sourceChannel as Channels.IInfoChannel ;
				if (ch != null)
				{
					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;

					Channels.INumericChannel num = this.sourceChannel as Channels.INumericChannel ;
					if (num != null)
					{
						format = num.Format ;
					}
				}
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Resize Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnResize(EventArgs e)
		{
			resizeBox = true ;
			base.OnResize(e) ;
		}

		/// <summary>
		/// FontChanged Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnFontChanged(EventArgs e)
		{
			//tb		textBox.Font = this.Font ;
			resizeBox = true ;
			base.OnFontChanged(e) ;
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

		/// <summary>NumericVariable
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

		#region Source event handling (value and property)
		//new value coming in
		private void OnSourceValueChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			string new_text ;

			this.value = newValue ;
			
			if (sourceChannel != null)
			{
				try
				{
					new_text = ((Channels.Channel)sourceChannel).ToString(format, null) ;
				}
				catch (System.FormatException)
				{
					new_text = "Invalid Format" ;
				}
			}
			else
			{
				new_text = "-||-" ;
			}

			if (new_text != text)
			{
				text = new_text ;
				this.AddToQueue (false) ;
			}
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Format")
			{
				SetFormat() ;
			}
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
		}
		#endregion
	}
}

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
	/// The KineticaRT Indicator control displays a boolean or numeric value, with
	/// different colors corresponding to different values.
	/// </summary>
    [ChangeRecord("2.0.0.1", "2007 07 06", "N. Savidge", "Allowed for Integers in the Value property.")]
    [ChangeRecord("1.2.1.0", "2005 09 26", "N. Savidge", "Added State.Text property")] 
	[ChangeRecord("1.2.0.0", "2003 03 13", "N. Savidge", "Strong Names")] 
	[ChangeRecord("1.0.0.0", "2003 01 01", "S. Childs",	"Original version")] 
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	[DefaultProperty("Value")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Indicator.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Indicator))]
	public class Indicator : KineticaRT.Controls.Control, ISource //ft
	{
		#region License
#if KRT_license
		private	bool					demoMode ;
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
						if ((key == null) || (key != "F0D324CFF67B" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "F0D324CFF67B" || key == "4CD0571EE68D")) key = "{F0D324CF-F67B-4d42-A893-6A11F9C7B137}" ; 
						context.SetSavedLicenseKey (typeof(Indicator), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Indicator), null) ;
					if (key == "{F0D324CF-F67B-4d42-A893-6A11F9C7B137}") licence = new License ("Run") ;
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
		private StateCollection			states ;
		private object					value ;
		private Channels.IChannel		sourceChannel ;
		private string					source ;
		private bool					useSourceFormat ;
		private string					text ;

		//internal variables
		private StringFormat			stringFormat ;
		private Color					lastBackColor ;
		private Color					lastBorderColor ;
		private Values.Value			krtValue ;			//double or type mismatch

		private Channels.ValueChangedEventHandler		srcHdlr ;
		private	PropertyChangedEventHandler				srcPropertyHdlr ;
		private PropertyChangedEventHandler				propertyHdlr ;
		private ObjectSearchEventHandler				searchHdlr ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Indicator class
		/// </summary>
		public Indicator()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#endif
			
			stringFormat = new StringFormat() ;
			stringFormat.LineAlignment	= StringAlignment.Center ;
			stringFormat.Alignment		= StringAlignment.Center ;

			lastBackColor	= Color.Empty ;
			lastBorderColor = Color.Empty ;
			krtValue		= Values.Values.Zero ;

			this.value			= 0 ;
			sourceChannel		= null ;
			source				= "" ;
			useSourceFormat		= true ;
			text				= "" ;

			srcHdlr = new Channels.ValueChangedEventHandler(OnValueChanged);
			srcPropertyHdlr = new PropertyChangedEventHandler(OnSourcePropertyChanged) ;

			//default to elliptical
			Shape = KineticaRT.Controls.Shapes.Ellipse ;
			
			states = new StateCollection() ;

			propertyHdlr= new PropertyChangedEventHandler(OnChildPropertyChanged) ;
			searchHdlr	= new ObjectSearchEventHandler(OnObjectSearch) ;

			states.PropertyChanged	+= propertyHdlr ;
			states.ObjectSearch		+= searchHdlr ;
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
			// Indicator
			// 
			this.Name = "Indicator";
			this.Size = new System.Drawing.Size(40, 40);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the Shape of the control
		/// </summary>
		[Category("Control")]
		[DefaultValue(Shapes.Ellipse)]
		[Description("Gets or sets the Shape of the control")]
		public override Shapes Shape
		{
			get { return base.Shape; }
			set { base.Shape = value; }
		}

		/// <summary>
		/// Gets the Background Color that corresponds to the current Value
		/// </summary>
		[Category("Indicator")]
		[Description("Gets the Background Color that corresponds to the current Value")]
		public Color CurrentBackColor
		{
			get
			{
				if (lastBackColor.IsEmpty)
				{
					return BackColor ;
				}
				return lastBackColor ;
			}
		}

		/// <summary>
		/// Gets the Border Color that corresponds to the current Value
		/// </summary>
		[Category("Indicator")]
		[Description("Gets the Border Color that corresponds to the current Value")]
		public Color CurrentBorderColor
		{
			get
			{
				if (lastBorderColor.IsEmpty)
				{
					return BorderColor ;
				}
				return lastBorderColor ;
			}
		}

		/// <summary>
		/// Gets or sets the current Value
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets the current Value")]
		//[DefaultValue(0)]
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
                    Values.Value new_krt_value;

                    this.value = value;

                    new_krt_value = value as Values.Double;
                    if (new_krt_value == null)
                    {
                        try
                        {
                            new_krt_value = new KineticaRT.Values.Double(System.Convert.ToDouble(value));
                        }
                        catch (System.Exception)
                        {
                        }
                    }
                    if (new_krt_value == null) new_krt_value = Values.Values.TypeMismatch;

					if (new_krt_value.CompareTo (krtValue) != 0)
					{
						krtValue = new_krt_value ;

//						//set destination
//						if (destinationChannel != null)	destinationChannel.Value = krtValue ;
						
						AddToQueue(false) ;
//						OnValueChanged() ;
					}
					OnPropertyChanged("Value") ;
				}
			}
		}
        private bool ShouldSerializeValue()
        {
            return false;
        }

		/// <summary>
		/// Gets or sets the object that is the Source of the indicator's value
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets the object that is the Source of the indicator's value")]
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
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		-= srcHdlr ;
						sourceChannel.PropertyChanged	-= srcPropertyHdlr ;
					}
					
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		+= srcHdlr ;
						sourceChannel.PropertyChanged	+= srcPropertyHdlr ;
						OnValueChanged (sourceChannel, sourceChannel.Value) ;

						SetFormat() ;
					}
					OnPropertyChanged("SourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
		}

//		/// <summary>
//		/// Object that is the Destination for the indicator's value
//		/// </summary>
//		[Category("Indicator")]
//		[Description("Object that is the Destination for of the indicator's value")]
//		public KineticaRT.Channel DestinationChannel
//		{
//			get
//			{
//				return destinationChannel;
//			}
//			set
//			{
//				if (destinationChannel != value)
//				{
//					destinationChannel = value ;
//
//					if (destinationChannel != null)		destinationChannel.Value = krtValue ;
//
//					OnPropertyChanged("DestinationChannel") ;
//				}
//			}
//		}
//		private bool ShouldSerializeDestinationChannel()
//		{
//			return false ;
//		}

		/// <summary>
		/// Gets or sets the name of the object to be the Source
		/// </summary>
		[Category("Indicator")]
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
				if (SourceChannel != null) OnValueChanged (SourceChannel, SourceChannel.Value) ;
			}
		}

//		/// <summary>
//		/// Name of object to be the Destination
//		/// </summary>
//		[Category("Indicator")]
//		[DefaultValue("")]
//		[Description("Name of object to be the Destination")]
//		public string Destination
//		{
//			get
//			{
//				return destination ;
//			}
//			set
//			{
//				if (destination != value)
//				{
//					destination = value ;
//					OnPropertyChanged("Destination") ;
//				}
//				//try and connect to destination
//				DestinationChannel = OnObjectSearch(destination) as KineticaRT.Channel ;
//			}
//		}

		///<summary>
		///Gets or sets a collection of State objects that define color/value correspondance
		///</summary>
		[Category("Indicator")]
		[Description("Gets or sets a collection of State objects that define color/value correspondance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public StateCollection States
		{
			get
			{
				return states ;
			}
			set
			{
				if (states != null)
				{
					states.PropertyChanged	-= propertyHdlr ;
					states.ObjectSearch		-= searchHdlr ;
				}
				
				states = value ;
				if (states != null)
				{
					states.PropertyChanged	+= propertyHdlr ;
					states.ObjectSearch		+= searchHdlr ;
				}

				OnPropertyChanged("States") ;
			}
		}
		private bool ShouldSerializeStates()
		{
			return !states.IsDefaultState() ;
		}

		/// <summary>
		/// Gets or sets whether the Indicator gets its ToolTip and Text from its Source channel
		/// </summary>
		[Category("Indicator")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Indicator gets its ToolTip and Text from its Source channel")]
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
		/// Gets or sets the Text displayed on the Indicator
		/// </summary>
		[Category("Indicator")]
		[DefaultValue("KineticaRT Button")]
		[Description("Gets or sets the Text displayed on the Indicator")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public override string Text
		{
			get
			{
				return text ;
			}
			set
			{
				if (text != value)
				{
					text = value ;
					AddToQueue(false) ;
					OnPropertyChanged("Text") ;
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
            //lock (this) //ft ajout 
            {

                Color back;
                Color border;
                string display_text;

                if (!(krtValue is Values.Double) || !states.FindCurrentValues(krtValue.ToDouble(), out back, out border, out display_text))
                {
                    if (!(krtValue is Values.Boolean))
                    {
                        //use default colours
                        back = BackColor;
                        border = borderColor;
                        display_text = text;
                    }
                    else
                    {
                        if (!states.FindCurrentValues(krtValue.ToDouble(), out back, out border, out display_text))
                        {
                            //use default colours
                            back = BackColor;
                            border = borderColor;
                            display_text = text;
                        }

                        if ((display_text.Length == 0) && this.SourceChannel is KineticaRT.Channels.IBooleanChannel)
                        {
                            KineticaRT.Channels.IBooleanChannel boolean_channel = this.SourceChannel as KineticaRT.Channels.IBooleanChannel;
                            KineticaRT.Values.Boolean boolean = krtValue as Values.Boolean;
                            display_text = boolean.ToBoolean() ? boolean_channel.TrueText : boolean_channel.FalseText;
                        }
                    }
                }

                //			base.repaint(FullRedraw, g) ;		//can't do this - need to use other colors
                CalculateDisplayRectangle();

                //g.SmoothingMode = SmoothingMode.AntiAlias;

                if (FullRedraw || lastBackColor != back || lastBorderColor != border)
                {
                    Brush brush = new SolidBrush(back);

                    needFullRedraw = false;
                    lastBackColor = back;
                    lastBorderColor = border;

                    if (DisplayRectangle.Width > 0 && DisplayRectangle.Height > 0) //ft si le le width ou heignt est 0 ca lance une exception
                    {
                        if (this.GradientEnabled)//ft Gradient support
                        {

                            Brush gradientBrush;
                            if (back == Color.White)
                            {
                                gradientBrush = new LinearGradientBrush(DisplayRectangle, back, this.DefaultGradientColor, this.LinearGradientMode);
                            }
                            else
                            {
                                gradientBrush = new LinearGradientBrush(DisplayRectangle, back, ControlPaint.Dark(back), this.LinearGradientMode);
                            }
                            g.FillRectangle(gradientBrush, DisplayRectangle);
                            gradientBrush.Dispose();
                        }
                        else
                        {
                            g.FillRectangle(brush, DisplayRectangle);
                        }
                        DrawBorder(g, border);

#if KRT_license
				if (demoMode)			DrawDemoText(g, DisplayRectangle, back) ;
#endif

                        brush.Dispose();
                    }
                }

                //draw text
                if (display_text.Length > 0 && display_text != " ")
                {
                    Brush brush = new SolidBrush(back);
                    g.FillRectangle(brush, DisplayRectangle);
                    brush.Dispose();
                    brush = new SolidBrush(ForeColor);
                    g.DrawString(display_text, Font, brush, DisplayRectangle, stringFormat);
                    brush.Dispose();
                }
            }
		}

		#endregion

		#region Other methods
		/// <summary>
		/// Sets up the tool tip based on the Source channel
		/// </summary>
		private void SetFormat()
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel		ch ;

				ch = sourceChannel as Channels.IInfoChannel ;
				if (ch != null)
				{
					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;
					Text		= (ch.Description != null) ? ch.Description : "" ;
				}
			}
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

		#region Source Event handling (value and property)
		//new value coming in
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			if (newValue is KineticaRT.Values.Double)
			{
				Value = newValue ;
			}
			else if (newValue is KineticaRT.Values.Boolean)
			{
				Value = newValue ;
			}
			else
			{
				if (newValue != null) Value = newValue.ToDouble() ;
			}
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			SetFormat() ;
		}
		#endregion

		#region Connection

        private System.Drawing.Color originalBackColor;
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
            if (originalBackColor == null || this.BackColor != Color.Fuchsia)
            {
                originalBackColor = this.BackColor;
            }
            else
            {
                this.BackColor = originalBackColor;
            }
            if (source != "")
            {
                SourceChannel = OnObjectSearch(source) as Channels.IChannel;
                if (this.sourceChannel == null) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }

//			if (destination != "")	DestinationChannel = OnObjectSearch(destination) as KineticaRT.Channel ;

			if (states != null)		states.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
//			if (destinationChannel != null)		DestinationChannel = null ;

			if (states != null)					states.Disconnect() ;
		}
		#endregion
	}
}

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
	[DefaultProperty("Text")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Display.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Display))]
	public class Display : KineticaRT.Controls.Control,ISource
	{
		#region License
#if KRT_license
		private License license = null;
		private	bool				demoMode ;
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
						if ((key == null) || (key != "53E372D93899" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "53E372D93899" || key == "4CD0571EE68D")) key = "{53E372D9-3899-4b2b-83DF-286F06ADACC2}" ; 
						context.SetSavedLicenseKey (typeof(Display), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Display), null) ;
					if (key == "{53E372D9-3899-4b2b-83DF-286F06ADACC2}") licence = new License ("Run") ;
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
		//objects
		//tb private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.TextBox textBox2;

		//properties
		private string				text ;
		private object				value ;
		private	Channels.IChannel	sourceChannel ;
		private	Channels.IChannel	destinationChannel ;
		private string				source ;
		private string				destination ;
		private ContentAlignment	textAlign ;
		private bool				spinButtonsEnabled ;
		private double				spinIncrement ;
		private double				minimum ;
		private double				maximum ;
        private Color 				percentBarColor;
		private bool				percentBarEnabled ;
		private bool				numericOnly ;
		private TextOrientations	orientation ;
		private bool				useSourceFormat ;
		private bool				inputEnabled ;
		private string				format ;
        private KineticaRT.Channels.INumericThresholdChannel threshold_channel; //ft 

		//internal variables
		private Channels.ValueChangedEventHandler	srcHdlr ;
		private PropertyChangedEventHandler			propertyHdlr ;
        private EventHandler       valueChangedHdlr; //ft

		private bool				resizeBox ;
		private bool				editing ;
		private bool				textChanged ;
		private string				shortText ;
		private Values.Value		krtValue ;
        private Rectangle 			textRect;
		private StringFormat		textFormat ;

        private Rectangle 			spinUpRect;
        private Rectangle 			spinDownRect;
		private bool				spinning ;
		private bool				spinUpPressed ;
		private bool				spinDownPressed ;
		private int					spinIncrementCount ;
		private System.Timers.Timer	spinTimer ;

		private bool				barValid ;
        private Rectangle 			barRect;
		private bool				barDragging ;

		private bool				busySettingValue ;
        private bool                enableErrorProviderUpdate;
        private Rectangle           iconRect;//ft

        private Color               foreColor;//ft test pour optimiser en vitesse
        private volatile bool       foreColorChanged = true; //ft flag utilise pour jouer sur l'objet brushForeColor afin d'�viter des CrossThread

        /// <summary>
        /// Bitmap in which the basic slider gets drawn
        /// </summary>
        protected Bitmap bmpBackground; //ft test


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Display class
		/// </summary>
		public Display()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (typeof(Display), this, out license) || (license.LicenseKey != "Run");
#endif

			value				= new KineticaRT.Values.Text("") ;
			text				= "" ;
			sourceChannel		= null ;
			destinationChannel 	= null ;
			source				= "" ;
			destination			= "" ;
			textAlign			= ContentAlignment.MiddleLeft ;
			spinButtonsEnabled	= false ;
			spinIncrement		= 1 ;
			minimum				= System.Double.NegativeInfinity ;
			maximum				= System.Double.PositiveInfinity ;
			percentBarColor		= Color.Blue ;
			percentBarEnabled	= false ;
			numericOnly			= false ;
			orientation			= TextOrientations.Right;
			useSourceFormat		= true ;
			inputEnabled		= true ;
			format				= "" ; 

			resizeBox		= true ;
			editing			= false ;
			textChanged		= true ;
			shortText		= "" ;
			krtValue		= new KineticaRT.Values.Text("") ;
			textFormat		= new StringFormat() ;
			textFormat.Trimming		= StringTrimming.None ;
			textFormat.Alignment	= StringAlignment.Near ;
			textFormat.LineAlignment= StringAlignment.Center ;

            spinUpRect 		= Rectangle.Empty;
            spinDownRect 	= Rectangle.Empty;
			spinning		= false ;
			spinUpPressed	= false ;
			spinDownPressed = false ;
			spinIncrementCount = 0 ;
			spinTimer		= new System.Timers.Timer(slowPool.UpdateRate) ;

            barRect 		= Rectangle.Empty;
			barValid		= false ;
			barDragging		= false ;

            textRect 		= Rectangle.Empty;
            iconRect        = Rectangle.Empty;

			srcHdlr		= new Channels.ValueChangedEventHandler(OnValueChanged) ;
			propertyHdlr= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;
            valueChangedHdlr = new EventHandler(OnDisplayValueChanged); //ft
//			textBox.Visible = false ; 
//tb		//connect to events from text box
//			textBox.GotFocus	+= new EventHandler(OnBoxGotFocus) ;
//			textBox.LostFocus	+= new EventHandler(OnBoxLostFocus) ;
//			textBox.KeyPress	+= new KeyPressEventHandler(OnBoxKeyPress) ;
//			textBox.Validating	+= new CancelEventHandler(OnBoxValidating) ;
            
			//and timer
			spinTimer.Elapsed += new ElapsedEventHandler(OnSpinTimerElapsed) ;

			this.SetStyle(ControlStyles.Selectable, true) ;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//ft 
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true); //ft pour empecher le flickering du control

            this.enableErrorProviderUpdate = false;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
#if KRT_license
			if (license != null) {license.Dispose();license = null;}
#endif
            if (bmpBackground != null)
            {
                bmpBackground.Dispose();
            }
			if( disposing )
			{

                srcHdlr = null;
                propertyHdlr = null;
                valueChangedHdlr = null;
                //inputEnabledSrcHdlr = null;

                //if (spinTimer != null)
                //{
                //    spinTimer.Elapsed -= spinTimerElapsedHdlr;
                //    spinTimer.Dispose();
                //    spinTimer = null;
                //    spinTimerElapsedHdlr = null;
                //}

                Source = String.Empty;
                Destination = String.Empty;
                DisplayValueChanged = null;

                if ( components != null )
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
			//tb this.textBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
//tb			// 
//			// textBox
//			// 
//			this.textBox.AutoSize = false;
//			this.textBox.BackColor = System.Drawing.SystemColors.Control;
//			this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
//			this.textBox.Name = "textBox";
//			this.textBox.Size = new System.Drawing.Size(100, 20);
//			this.textBox.TabIndex = 0;
//			this.textBox.Text = "";
//			this.textBox.WordWrap = false;
			// 
			// Display
			// 
            this.BorderType = KineticaRT.Controls.BorderType.Indented;
//tb			this.Controls.AddRange(new System.Windows.Forms.Control[] {this.textBox});
			this.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Name = "Display";
			this.Size = new System.Drawing.Size(150, 24);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or Sets the type of the Control's Border
		/// </summary>
		[Category("Control")]
        [DefaultValue(BorderType.Indented)]
		[Description("Gets or Sets the typeof the Control's Border")]
        public override BorderType BorderType
		{
            get { return base.BorderType; }
            set { base.BorderType = value; }
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
		/// Gets or sets the text corresponding to the currect Value
		/// </summary>
		[Category("Display")]
		[Browsable(true)]
		[DefaultValue("")]
		[Description("Gets or sets the text corresponding to the currect Value")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				return text ;
			}
			set
			{
				SetText(value, true) ;
			}
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
						
						SetValue(sourceChannel.Value, true) ;
						SetFormat() ;
					}
					OnPropertyChanged("SourceChannel") ;
                   
                    //ft ajout
                    // Next bit done after connecting property change event hander in base.SourceChannel.
                    threshold_channel = value as KineticaRT.Channels.INumericThresholdChannel;
                    if (threshold_channel != null)
                    {
                        OnSourcePropertyChanged(threshold_channel, KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs); // Called so that only one piece of code sets the actual colours.
                    }
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the object that is the Destination for the display's value
		/// </summary>
		[Category("Display")]
		[Description("Gets or sets the object that is the Destination for the display's value")]
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

                    //ft 20130220 ici le krtValue est vide et overwrite la valeur du channel de destination.
                    //ca cause un bug lorsque 2 HMI un master ouvert et un slave qui s ouvre ensuite. Le slave set toutes les valeurs a vide
                    //donc toutes les answers du user inputs du master sont flushe par le slave au moment ou le slave call le UserinputsForm.Connect, qui call les TextBox.Connect
                    
					//ft if (destinationChannel != null)		destinationChannel.Value = krtValue ;

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
		/// Gets or sets the name of object to be the Destination
		/// </summary>
		[Category("Display")]
		[DefaultValue("")]
		[Description("Gets or sets the name of object to be the Destination")]
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
		[Category("Display")]
		[Description("Gets or sets the current Value")]
		public object Value
		{
			get
			{
				return this.value ;
			}
			set
			{
//				if (this.value != value)
//				{
//					Values.Value		new_krt_value ;
//
//					this.value = value;
//
//					//is it already a KRT Value?
//					new_krt_value = value as Values.Value ;
//					//how about a standard double or string?
//					if ((new_krt_value == null) && (value is System.Double)) new_krt_value = new Values.Double ((System.Double)value) ;
//					if ((new_krt_value == null) && (value is System.String)) new_krt_value = new Values.Text ((System.String)value) ;
//					//otherwise make it a string
//					if (new_krt_value == null)
//					{
//						if (value != null)
//						{
//							new_krt_value = new Values.Text(value.ToString()) ;
//						}
//						else
//						{
//							new_krt_value = new Values.Text("") ;
//						}
//					}
//
//					if (new_krt_value.CompareTo (krtValue) != 0)
//					{
//						krtValue = new_krt_value ;
//
//						if (destinationChannel != null)		destinationChannel.Value = krtValue ;
//
//						//is this right??? what if the value wasn't set by the source?
//						if (sourceChannel != null)
//						{
//							//make the source convert itself to a formatted string
////							SetText(sourceChannel.ToString(), false) ;
//							SetText(sourceChannel.ToString(), true) ;
//						}
//						else if (krtValue != null)
//						{
//							SetText(krtValue.ToString(), false) ;
//						}
//						else
//						{
//							SetText("", false) ;
//						}
//
//						AddToQueue(false) ;
////						OnValueChanged() ;
//					}
//					OnPropertyChanged("Value") ;
//				}
				SetValue(value, false) ;
			}
		}
		private bool ShouldSerializeValue()
		{
			return false ;		//save text string but not value object
		}

		/// <summary>
		/// Gets or sets the Horizontal and Vertical alignment of the text
		/// </summary>
		[Category("Display")]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		[Description("Gets or sets the Horizontal and Vertical alignment of the text")]
		public ContentAlignment TextAlign
		{
			get
			{
				return textAlign ;
			}
			set
			{
				if (textAlign != value)
				{
					textAlign = value ;
					switch (textAlign)
					{
						case ContentAlignment.BottomLeft:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Left ;
							textFormat.LineAlignment= StringAlignment.Far ;
							textFormat.Alignment	= StringAlignment.Near ;
						}
						break ;

						case ContentAlignment.MiddleLeft:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Left ;
							textFormat.LineAlignment= StringAlignment.Center ;
							textFormat.Alignment	= StringAlignment.Near ;
						}
						break ;

						case ContentAlignment.TopLeft:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Left ;
							textFormat.LineAlignment= StringAlignment.Near ;
							textFormat.Alignment	= StringAlignment.Near ;
						}
						break ;

						case ContentAlignment.BottomCenter:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Center ;
							textFormat.LineAlignment= StringAlignment.Far ;
							textFormat.Alignment	= StringAlignment.Center ;
						}
						break ;

						case ContentAlignment.MiddleCenter:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Center ;
							textFormat.LineAlignment= StringAlignment.Center ;
							textFormat.Alignment	= StringAlignment.Center ;
						}
						break ;

						case ContentAlignment.TopCenter:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Center ;
							textFormat.LineAlignment= StringAlignment.Near ;
							textFormat.Alignment	= StringAlignment.Center ;
						}
						break ;

						case ContentAlignment.BottomRight:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Right ;
							textFormat.LineAlignment= StringAlignment.Far ;
							textFormat.Alignment	= StringAlignment.Far ;
						}
						break ;

						case ContentAlignment.MiddleRight:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Right ;
							textFormat.LineAlignment= StringAlignment.Center ;
							textFormat.Alignment	= StringAlignment.Far ;
						}
						break ;

						case ContentAlignment.TopRight:
						{
//tb						textBox.TextAlign		= HorizontalAlignment.Right ;
							textFormat.LineAlignment= StringAlignment.Near ;
							textFormat.Alignment	= StringAlignment.Far ;
						}
						break ;
					}

					resizeBox = true ;
					AddToQueue(false) ;
					OnPropertyChanged("TextAlign") ;
				}
			}
		}

        /// <summary>
        /// Gets or sets whether Spin Buttons are enabled
        /// </summary>
        /// <remarks>Spin buttons are used to increment and decrement numeric values</remarks>
        [Category("Display")]
        [DefaultValue(false)]
        [Description("Gets or sets whether Spin Buttons are enabled")]
        public bool SpinButtonsEnabled
        {
            get
            {
                return spinButtonsEnabled;
            }
            set
            {
                if (spinButtonsEnabled != value)
                {
                    spinButtonsEnabled = value;
                    resizeBox = true;
                    SetWindowRegion();
                    AddToQueue(true);
                    OnPropertyChanged("SpinButtonsEnabled");
                }
            }
        }

        bool useSourceValue = true;
        /// <summary>
        /// Gets or sets whether to display text from the source channel or only from the value property.
        /// </summary>
        /// <remarks>whether to display text from the source channel or only from the value property.</remarks>
        [Category("Display")]
        [DefaultValue(true)]
        [Description("Gets or sets whether Spin Buttons are enabled")]
        public bool UseSourceValue
        {
            get
            {
                return useSourceValue;
            }
            set
            {
                if (useSourceValue != value)
                {
                    useSourceValue = value;
                    OnPropertyChanged("UseSourceValue");
                }
            }
        }

        /// <summary>
		/// Gets or sets the amount by which the Spin Buttons increment or decrement the value
		/// </summary>
		[Category("Display")]
		[DefaultValue(1.0)]
		[Description("Gets or sets the amount by which the Spin Buttons increment or decrement the value")]
		public double SpinIncrement
		{
			get
			{
				return spinIncrement ;
			}
			set
			{
				if (spinIncrement != value)
				{
					spinIncrement = value ;
					OnPropertyChanged("SpinIncrement") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Minimum numeric value
		/// </summary>
		/// <remarks>This value is only used to restrict user input. It does not restrict values from a Source channel</remarks>
		[Category("Display")]
		[DefaultValue(System.Double.NegativeInfinity)]
		[Description("Gets or sets the Minimum numeric value")]
		public double Minimum
		{
			get
			{
				return minimum ;
			}
			set
			{
				if (minimum != value)
				{
					minimum = value ;
					AddToQueue(false) ;
					OnPropertyChanged("Minimum") ;
				}
			}
		}

		/// <summary>
		/// Gets r sets the Maximum numeric value
		/// </summary>
		/// <remarks>This value is only used to restrict user input. It does not restrict values from a Source channel</remarks>
		[Category("Display")]
		[DefaultValue(System.Double.PositiveInfinity)]
		[Description("Gets or sets the Maximum numeric value")]
		public double Maximum
		{
			get
			{
				return maximum ;
			}
			set
			{
				if (maximum != value)
				{
					maximum = value ;
					AddToQueue(false) ;
					OnPropertyChanged("Maximum") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Percent Bar is enabled
		/// </summary>
		/// <remarks>
		/// The Percent bar is only displayed if the Minimum and Maximum value 
		/// are not infinite. In this case the bar will show the current value as
		/// a percentage of the full range allowed.
		/// </remarks>
		[Category("Display")]
		[DefaultValue(false)]
		[Description("Gets or sets whether the Percent Bar is enabled")]
		public bool PercentBarEnabled
		{
			get
			{
				return percentBarEnabled ;
			}
			set
			{
				if (percentBarEnabled != value)
				{
					percentBarEnabled = value ;
					resizeBox = true ;
					SetWindowRegion() ;
					AddToQueue(true) ;
					OnPropertyChanged("PercentBarEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Color of the Percent bar
		/// </summary>
		/// <remarks>
		/// The Percent bar is only displayed if the Minimum and Maximum value 
		/// are not infinite. In this case the bar will show the current value as
		/// a percentage of the full range allowed.
		/// </remarks>
		[Category("Display")]
		[Description("Gets or sets the color of the Percent Bar")]
        public Color PercentBarColor
		{
			get
			{
				return percentBarColor ;
			}
			set
			{
				if (percentBarColor != value)
				{
					percentBarColor = value ;
					AddToQueue(false) ;
					OnPropertyChanged("PercentBarColor") ;
				}
			}
		}
		private bool ShouldSerializePercentBarColor()
		{
            return (percentBarColor != Color.Blue);
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetPercentBarColor()
		{
            percentBarColor = Color.Blue;
		}
        /// <summary>
        /// BackColor property.
        /// </summary>
        public override Color BackColor //ajout fix du frame j'ai deplace la propriete BackColor de Control.cs dans Display.cs pour optimiser les refresh... faire le Repaint une seule fois
        {
            get { return backColor; }//fix du frame j'ai deplace la propriete BackColor de Control.cs dans Display.cs pour optimiser les refresh 
            set
            {
                backColor = value;
                backColorChanged = true; //ft ceci est requis pour corriger un bug de synchronisation entre le changement de cette propriete et le thread qui call le Repaint.  Il arrivait que le backcolor ne changait pas
                AddToQueue(true);
            }
        }
        private Color backColor = Color.Empty;//ft
        private bool ShouldSerializeBackColor()//ft ajout
        {
            return true;
        }
        /// <summary>
        /// Gets or Sets the Color of the Value text
        /// </summary>
        [Category("Display")]
        [Description("Gets or Sets the Color of the Value text")]
        public override Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
                foreColorChanged = true;
                AddToQueue(false);
                OnPropertyChanged("ForeColor");
            }
        }
        private bool ShouldSerializeForeColor()
        {
            return foreColor != Color.Empty;
        }
		/// <summary>
		/// Gets or sets whether to restrict user input to valid numeric values
		/// </summary>
		/// <remarks>Doesn't restrict values from Source channels</remarks>
		[Category("Display")]
		[DefaultValue(false)]
		[Description("Gets or sets whether to restrict user input to valid numeric values")]
		public bool NumericOnly
		{
			get
			{
				return numericOnly ;
			}
			set
			{
				if (numericOnly != value)
				{
					numericOnly = value ;
					OnPropertyChanged("NumericOnly") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the orientation of the Text
		/// </summary>
		[Category("Display")]
		[DefaultValue(TextOrientations.Right)]
		[Description("Gets or sets the orientation of the Text")]
		public TextOrientations Orientation
		{
			get
			{
				return orientation ;
			}
			set
			{
				if (orientation != value)
				{
					orientation = value ;
					AddToQueue(false) ;
					OnPropertyChanged("Orientation") ;
				}
			}
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

		/// <summary>
		/// Gets or sets whether user input is Enabled
		/// </summary>
		/// <remarks>This differs from Enabled by not preventing drag and drop or tooltips</remarks>
		[Category("Display")]
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
					//set correct cursor
					this.Cursor = inputEnabled ? Cursors.IBeam : Cursors.Default ;

					OnPropertyChanged("InputEnabled") ;
				}
			}
		}

       
		/// <summary>
        /// Gets or sets the format string specifying the display format, leave blank to use the Source's formating.
		/// </summary>
		[Description("Format string specifying the display format, leave blank to use the Source's formating.  Eg. C,E,F3,G6,N")]
		[Category("Display")] 
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
					if (sourceChannel != null)
					{
						//make the source convert itself to a formatted string
						if (format != "" && sourceChannel is Channels.Channel)
						{
							SetText(((Channels.Channel)sourceChannel).ToString(format, null), false) ;
                            //ft SetText(((Channels.Channel)sourceChannel).Value.ToString(format, null), false);
						}
						else
						{
							SetText(sourceChannel.ToString(), false) ;
                            //ft SetText(sourceChannel.Value.ToString(), false);
						}
						AddToQueue(false) ;
					}
					OnPropertyChanged("Format") ;
				}
			}
		}
        private volatile bool iconEnabledChanged = false;
        private bool iconEnabled = false;
        private bool IconEnabled
        {
            get
            {  
                return iconEnabled;   
            }
            set 
            {
                if (value != iconEnabled)
                {
                    iconEnabled = value;
                    iconEnabledChanged = true;
                    AddToQueue(true);
                }
            }
        }
		#endregion

		#region Drawing

        //private static System.Drawing.Icon errorProviderIcon = new System.Drawing.Icon(typeof(KineticaRT.Controls.Control), "information.ico");
        private void DrawIcon(Graphics g, Rectangle Rect)
        {
            // Construct an Icon.
            //Icon icon1 = new Icon(SystemIcons.Exclamation, 10, 10);
            Icon icon1 = new Icon(typeof(KineticaRT.Controls.Control), "information.ico");

            // Call ToBitmap to convert it.
            Bitmap bmp = icon1.ToBitmap();
            iconRect = Rect;
            iconRect.X = Rect.Width - 20;
            iconRect.Width = 15;
            iconRect.Height = 15;
            iconRect.Y = (Rect.Height - 15) / 2;

            // Draw the bitmap.
            g.DrawImage(bmp, iconRect);
        }

        /// <summary>
        /// OnPaintBackground
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)//ft ajout pour eviter un flicking de l image pcq cette fonction dessine le background en transparent, et il y avait un trop grand delais avant de repainter l<image via le AddToQueue
        {
            base.OnPaintBackground(e); //c<est cette ligne qui va painter le background transparent
            AddToQueueImmediate();//ajoute a la queue pour dessin immediat
        }
        
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
            //ft lock(lockObj) //ft ici je n<ai pas utilise le lock pour des raisons de performances, mais je refresh via OnPaintBackGround et via  AddToQueueImmediate qui est tres rapide.
            //CalculateRectangles() ; //ft deplace dans le if ici bas
            if (FullRedraw || resizeBox || backColorChanged || iconEnabledChanged)
            {
                CalculateRectangles(); //ft attention cette fonction affecte la fonction CreateTextBox    
                System.Drawing.Rectangle rect = ClientRectangle;

                needFullRedraw = false;
                if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                {
                    backColorChanged = false;
                    backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                }

                if (resizeBox)
                {
                    if (bmpBackground != null) bmpBackground.Dispose();
                    bmpBackground = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                }
                Graphics gBmp = Graphics.FromImage(bmpBackground);

                //basic drawing (appearance etc) 
                //base.Repaint(true, gBmp); //ft ne pas calller
                if (this.GradientEnabled)//ft gradient support
                {
                    Color gradientColor;

                    if (this.BackColor == Color.White)
                    {
                        gradientColor = this.DefaultGradientColor;
                    }
                    else
                    {
                        gradientColor = ControlPaint.Dark(this.BackColor);
                    }
                    using (Brush gradientBrush = new LinearGradientBrush(textRect, this.BackColor, gradientColor, this.LinearGradientMode))
                    {
                        gBmp.FillRectangle(gradientBrush, textRect);//textRect
                    }
                }
                else
                {
                    gBmp.FillRectangle(backColorBrush, textRect);//textRect
                }

                //SetDisplayRegion(gBmp);

                if (shape == Shapes.Rectangle || percentBarEnabled || spinButtonsEnabled)
                {
                    int border_width = BorderWidthPixels(rect);
                    DrawSquareBorder(gBmp, rect, BorderType, borderColor, border_width);
                }
                else
                {
                    DrawBorder(gBmp);
                }


                if (errorProviderEnabled && iconEnabled)//ft draw l'icone du error provider
                {
                    DrawIcon(gBmp, rect);
                }
                iconEnabledChanged = false;

                gBmp.Dispose();
            }

            //ensure the remainder is drawn inside the border
            if (shape == Shapes.Rectangle || percentBarEnabled || spinButtonsEnabled)
            {
                //g.Clip = new Region(DisplayRectangle) ;
                //				Region new_region = new Region(DisplayRectangle) ;

                //ft ce bout de code n'a pas l'air de changer quelque chose  quand je le desactive jusqu'a new_region.Dispose
                ////Rectangle rect = ClientRectangle;
                ////int border_width = BorderWidthPixels(rect);
                ////rect.Inflate(-border_width, -border_width);
                ////Region new_region = new Region(rect);

                ////new_region.Intersect(g.Clip);
                ////Region old_region = g.Clip;
                ////g.Clip = new_region;
                ////old_region.Dispose();

                ////new_region.Dispose();

            }
            else
            {
                //ft pourquoi ce code ici bas?
                //using (GraphicsPath path = CalculateShapePath(DisplayRectangle, shape))
                //{
                //    //g.Clip = new Region(path) ;
                //    Region new_region = new Region(path);

                //    new_region.Intersect(g.Clip);
                //    Region old_region = g.Clip;
                //    g.Clip = new_region;
                //    old_region.Dispose();

                //    new_region.Dispose();

                //}
            }

            //else drawn the background already

            //tb			if (resizeBox)
            //			{
            //				//give it enough height for (hopefully) any characters
            //				this.Font.GetHeight() ;
            //				SizeF		size = g.MeasureString("Iy#", this.Font) ;
            //
            //				textBox.Left = textRect.X ;
            //				textBox.Width = textRect.Width ;
            //				textBox.Height = (int)(size.Height) ;
            //				//cannot be taller than the display is
            //				if (textBox.Height > textRect.Height)
            //				{
            //					textBox.Height = textRect.Height ;
            //				}
            //
            //				switch (textFormat.LineAlignment)
            //				{
            //					case StringAlignment.Center:
            //					{
            //						textBox.Top = textRect.Top + (textRect.Height - textBox.Height) / 2 ;
            //					}
            //					break ;
            //
            //					case StringAlignment.Near:		//top
            //					{
            //						textBox.Top = textRect.Top ;
            //					}
            //					break ;
            //
            //					case StringAlignment.Far:		//bottom
            //					{
            //						textBox.Top = textRect.Bottom - textBox.Height ;
            //					}
            //					break ;
            //				}
            //			}

            if (!editing)
            {
                using (Bitmap bmp = (Bitmap)bmpBackground.Clone())
                {
                    using (Graphics g2 = Graphics.FromImage(bmp))
                    {

                        SetDisplayRegion(g2);

#if KRT_license
			//demo mode?
			if (demoMode)
			{
				if (BackColor.GetBrightness() < 0.6)
				{
					DrawDemoTextColor(g, DisplayRectangle, Color.Orchid) ;
				}
				else
				{
					DrawDemoTextColor(g, DisplayRectangle, Color.DarkOrchid) ;
				}
			}
#endif

                        if (textChanged || resizeBox)
                        {
                            shortText = ReduceText(g2);
                            textChanged = false;
                        }


                        Rectangle text_rect = textRect;
                        if (foreColorChanged == true)//flag de type volatile pour eviter les Cross Thread Operation
                        {
                            //ft optimisation en vitesse c'est le seul endroit ou il faut jouer sur cet objet pour eviter les CrossThread
                            // le fait d'utiliser un flag volatile nous permet de synchroniseer les thread pour que le seul endroit qui
                            // joue sur cette variable soit situe dans ce Repaint UNIQUEMENT
                            foreColorChanged = false;
                            foreColorBrush.Color = foreColor;
                        }
                        //ft text optimiser en vitesse using (Brush brush = new SolidBrush(ForeColor))
                        {
                            //set up transforms
                            switch (orientation)
                            {
                                case TextOrientations.Down:
                                    {
                                        Rectangle original_text_rect = text_rect;

                                        text_rect.X = original_text_rect.Y;
                                        text_rect.Y = original_text_rect.X;
                                        text_rect.Width = original_text_rect.Height;
                                        text_rect.Height = original_text_rect.Width;

                                        g2.RotateTransform(90);
                                        g2.TranslateTransform(0, -(original_text_rect.Right + original_text_rect.X));
                                    }
                                    break;

                                case TextOrientations.Up:
                                    {
                                        Rectangle original_text_rect = text_rect;

                                        text_rect.X = original_text_rect.Y;
                                        text_rect.Y = original_text_rect.X;
                                        text_rect.Width = original_text_rect.Height;
                                        text_rect.Height = original_text_rect.Width;

                                        g2.RotateTransform(-90);
                                        g2.TranslateTransform(-ClientRectangle.Height, 0);
                                    }
                                    break;

                                case TextOrientations.Left:
                                    {
                                        g2.RotateTransform(180);
                                        g2.TranslateTransform(-(text_rect.Right + text_rect.X), -(text_rect.Bottom + text_rect.Y));
                                    }
                                    break;

                                case TextOrientations.Right:
                                    {
                                    }
                                    break;
                            }


                            g2.DrawString(shortText, this.Font, foreColorBrush, text_rect, textFormat);

#if KRT_license
                if (demoMode)			DrawDemoText(g2, rect, BackColor) ;
#endif


                            g.DrawImage(bmp, 0, 0);

                        }
                    }
                }
            }
            else  //on est en edit
            {
                g.FillRectangle(backColorBrush, textRect); //ft en editmode lorsque le backcolor etant gradiant ca paintait pas bien le background
            }


            DrawSpinButtons(g); //ft doit etre fait en edit mode
            DrawPercentBar(g);

            resizeBox = false;


            OnDisplayValueChanged(this, null); //ft test
            
		}

		//Draws the percentage bar
		private void DrawPercentBar(Graphics g)
		{
			if (!barValid)				return ;

            Rectangle rect = barRect;
            using (Pen pen = new Pen(ControlPaint.Dark(borderColor), 1))
            {

                g.FillRectangle(backColorBrush, barRect);
                g.DrawLine(pen, barRect.Left, barRect.Top, barRect.Right - 1, barRect.Top);
            }

			//draw bar...
			if (krtValue is Values.Double)
			{
				double		val		= krtValue.ToDouble() ;

				if (!System.Double.IsNaN(val) && ! System.Double.IsInfinity(val))
				{
					double		fraction		= (val - minimum) / (maximum - minimum) ;

					if (fraction > 0)
					{
                        using (Brush brush = new SolidBrush(percentBarColor))
                        {

                            if (fraction > 1) fraction = 1;

                            rect.Y++;
                            rect.Height--;
                            rect.Width = (int)(fraction * rect.Width);

                            g.FillRectangle(brush, rect);
                        }
					}
				}
			}
		}

		//Draws the spin buttons
		private void DrawSpinButtons(Graphics g)
		{
			if (spinButtonsEnabled)
			{
				ControlPaint.DrawScrollButton(g, spinUpRect, ScrollButton.Up, spinUpPressed ? ButtonState.Pushed : ButtonState.Normal) ;
				ControlPaint.DrawScrollButton(g, spinDownRect, ScrollButton.Down, spinDownPressed ? ButtonState.Pushed : ButtonState.Normal) ;
			}
		}

		private void CalculateRectangles()
		{
			CalculateDisplayRectangle() ;

            Rectangle rect = DisplayRectangle;	//bar and buttons get subtracted
			bool		bar_now_valid	= false ;

			//the bar is drawn if enabled and finite limits defined
			bar_now_valid = percentBarEnabled && !System.Double.IsInfinity(minimum) && !System.Double.IsInfinity(maximum) ;
			if (bar_now_valid != barValid)
			{
				barValid = bar_now_valid ;
				resizeBox = true ;
			}

			if (barValid)
			{
				barRect = rect ;

				barRect.Height	= rect.Height / 2 > 4 ? 4 : rect.Height / 2 ;
				barRect.Y		= rect.Bottom - barRect.Height ;

				rect.Height -= barRect.Height ;
			}

			if (spinButtonsEnabled)
			{
				spinUpRect.Height	= rect.Height / 2 ;
				spinDownRect.Height = rect.Height - spinUpRect.Height ;

				spinUpRect.Width	= spinUpRect.Height ;
				spinDownRect.Width	= spinUpRect.Height ;

				spinUpRect.Y		= rect.Top ;
				spinDownRect.Y		= spinUpRect.Y + spinUpRect.Height ;

				spinUpRect.X		= rect.Right - spinUpRect.Width ;
				spinDownRect.X		= spinUpRect.X ;

				rect.Width -= spinUpRect.Width ;
			}

			//update textRect to only be bit text goes in
			textRect = rect ;
		}

		private string ReduceText(Graphics g)
		{
            if (text == "") return "";//ft ajout pour eviter une exception
            SizeF		size = g.MeasureString(text, this.Font) ;

//tb		if (size.Width > textBox.Width)
			if (size.Width > textRect.Width)
			{
				string		temp ;

				//need to reduce the length of the string
				if (this.Value.GetType() == typeof(Values.Double))
				{
					//numeric and too long
					temp = "######" ;
				}
				else
				{
					//return a cut-off string with '...' appended
					//int			index	= text.Length - 1 ;

					//temp = text + "..." ;

					int index = 0 ;
					do
					{
						//temp = temp.Remove(index, 1) ;
						//index-- ;
						index ++ ;
						temp = text.Substring(0, index) + "..." ; ;
						size = g.MeasureString(temp, this.Font) ;
					}
					//while (index > 0 && size.Width > textBox.Width) ;
//tb				while (index < text.Length && size.Width < textBox.Width) ;
					while (index < text.Length && size.Width < textRect.Width) ;

					index -- ;
					temp = text.Substring(0, index) ;
				}

				//make tool tip show full string
				//base.SetToolTip(text) ;
				return temp ;
			}

			//make tool tip show configured tool tip text
			//base.SetToolTip(toolTipText) ;
			return text ;
		}
		#endregion

		#region Other Methods
		/// <summary>
		/// Sets the controls window to the relevant size and shape
		/// Call when shape changes, or control resizes
		/// </summary>
		protected override void SetWindowRegion()
		{
			if (shape == Shapes.Rectangle || percentBarEnabled || spinButtonsEnabled)
			{
				//must be square if bar or buttons enabled
				this.Region = null ;
			}
			else
			{
                using (GraphicsPath path = CalculateShapePath(ClientRectangle, shape))
                {
                    this.Region = new System.Drawing.Region(path);
                }
			}
		}

		//sets the current text
		private void SetText(string newText, bool setValue)
		{
			if (text != newText)//Dependemment de ou on provient en callant cette fonction, on peut arriver avec la newText format� selon le format F1 ex 56.1, et si le text est deja 56.1 faut pas trigger un value changed (AddToQueue) qui rafraichi le control c'est inutile
			{
				text = newText;
				textChanged = true ;

				if (setValue)
				{
					SetValue(TextToValue(text), false) ;
				}
				OnPropertyChanged("Text") ;
			}
		}

		//convert text to relevant value type
		private KineticaRT.Values.Value TextToValue(string Text)
		{
			string		temp ;
			temp = Text.ToLower() ;

			switch (temp)
			{
				case "true" :		return KineticaRT.Values.Values.True ;
				case "false" :		return KineticaRT.Values.Values.False ;
				case "pi"	:		return KineticaRT.Values.Values.Pi ;
				case "infinity"	:	return KineticaRT.Values.Values.PositiveInfinity ;
				case "-pi"	:		return KineticaRT.Values.Values.Pi.Multiply(KineticaRT.Values.Values.MinusOne) ;
				case "-infinity":	return KineticaRT.Values.Values.NegativeInfinity ;
			}

			double	dbl ;
			
			if (System.Double.TryParse (Text, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out dbl))
			{
				return new KineticaRT.Values.Double (dbl) ;
			}

			return new KineticaRT.Values.Text(Text) ;
		}

		private void IncrementValue()
		{
			if (krtValue is Values.Double)
			{
				double		new_val		= krtValue.ToDouble() ;

				//only increments if value is a legitimate double
				if (!System.Double.IsNaN(new_val) && !System.Double.IsInfinity(new_val))
				{
					double		increment ;

					//should we be incrementing fast yet?
					if (spinIncrementCount >= 20)
					{
						increment = spinIncrement * 100 ;
					}
					else if (spinIncrementCount >= 10)
					{
						increment = spinIncrement * 10 ;
					}
					else
					{
						increment = spinIncrement ;
					}

					if (spinUpPressed)
					{
						new_val += increment ;
					}
					else if (spinDownPressed)
					{
						new_val -= increment ;
					}

					//keep it in range
					if (new_val < minimum)
					{
						new_val = minimum ;
					}
					else if (new_val > maximum)
					{
						new_val = maximum ;
					}

					SetValue(new KineticaRT.Values.Double(new_val), false) ;
					spinIncrementCount++ ;
				}
			}
		}

		// Calculates a new value from the position of the mouse on the percent bar
		private void SetValueFromBar(int XPos, int YPos)
		{
			double		fraction ;
			double		new_val ;

			fraction = (XPos - barRect.Left) / (double)(barRect.Width) ;

			if (fraction < 0)
			{
				fraction = 0 ;
			}
			else if (fraction > 1)
			{
				fraction = 1 ;
			}

			new_val = minimum + fraction * (maximum - minimum) ;

			SetValue(new KineticaRT.Values.Double(new_val), false) ;
		}

		private void SetFormat()
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel		ch ;

				ch = sourceChannel as Channels.IInfoChannel ;
				if (ch != null)
				{
					Channels.INumericChannel		num ;
                    ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "";

					num = ch as Channels.INumericChannel ;
					if (num != null)
					{
						Minimum	= num.Minimum ;
						Maximum	= num.Maximum ;
					}
				}
			}
		}
		
		private void SetValue(object newValue, bool fromSource)
		{
            if (!useSourceValue && fromSource) return;

			if (this.value != newValue)
			{
				Values.Value		new_krt_value ;

                this.value = newValue;
                
				//is it already a KRT Value?
                new_krt_value = newValue as Values.Value;
				//how about a standard double or string?
				if (new_krt_value == null)
				{
					try
					{
                        new_krt_value = new KineticaRT.Values.Double(System.Convert.ToDouble(newValue));
					}
					catch (System.Exception)
					{
					}
				}
				//otherwise make it a string
				if (new_krt_value == null)
				{
                    if (newValue != null)
					{
                        new_krt_value = new Values.Text(newValue.ToString());
					}
					else
					{
						new_krt_value = new Values.Text("") ;
					}
				}

				if (new_krt_value.CompareTo (krtValue) != 0)
				{
					krtValue = new_krt_value ;

					if (!busySettingValue && (destinationChannel != null))		destinationChannel.Value = krtValue ;

					//is this right?? what if the value wasn't set by the source?
					if (sourceChannel != null)
					{
						if (fromSource)
						{
							//make the source convert itself to a formatted string
							if (format != "" && sourceChannel is Channels.Channel)
							{
								SetText(((Channels.Channel)sourceChannel).ToString(format, null), false) ;
                                //ft SetText(((Channels.Channel)sourceChannel).Value.ToString(format, null), false);
							}
							else
							{
								 SetText(sourceChannel.ToString(), false) ;
                                //ftSetText(sourceChannel.Value.ToString(), false);
							}
						}
						else
						{
                            if (useSourceValue)
                            {
                                //revert to source value
                                busySettingValue = true;
                                SetValue(sourceChannel.Value, true);
                                busySettingValue = false;
                            }
                            else
                            {
                                busySettingValue = true;
                                SetText(new_krt_value.ToString(), false);
                                busySettingValue = false;
                            }
						}
					}
					else if (krtValue != null)
					{
						SetText(krtValue.ToString(), false) ;
					}
					else
					{
						SetText("", false) ;
					}
                    if (textChanged) //ft ajout de cette condition pcq  Dependemment de ou on provient en callant cette fonction, on peut arriver avec la newText format� selon le format F1 ex 56.1, et si le text est deja 56.1 faut pas trigger un value changed (AddToQueue) qui rafraichi le control c'est inutile
                    {
                        AddToQueue(false);
                    }

                    if (textChanged) //ft ajout de cette condition pcq  Dependemment de ou on provient en callant cette fonction, on peut arriver avec la newText format� selon le format F1 ex 56.1, et si le text est deja 56.1 faut pas trigger un value changed (AddToQueue) qui rafraichi le control c'est inutile
                    {
                        AddToQueue(false);
                    }

                    					//OnValueChanged() ;
				}
				OnPropertyChanged("Value") ;
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

//tb	/// <summary>
//		/// BackColorChanged Event
//		/// </summary>
//		/// <param name="e">Event Data</param>
//		protected override void OnBackColorChanged(EventArgs e)
//		{
//			textBox.BackColor = BackColor ;
//			base.OnBackColorChanged(e) ;
//		}

//tb	/// <summary>
//		/// ForeColorChanged Event
//		/// </summary>
//		/// <param name="e">Event Data</param>
//		protected override void OnForeColorChanged(EventArgs e)
//		{
//			textBox.ForeColor = ForeColor ;
//			base.OnForeColorChanged(e) ;
//		}

		/// <summary>
		/// GotFocus Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnGotFocus(EventArgs e)
		{
			if (inputEnabled && !spinning && !barDragging)
			{
				this.CreateTextBox() ;
//tb			textBox.Visible = true ;
				textBox2.Focus() ;
			}
			base.OnGotFocus(e) ;
		}

		/// <summary>
		/// RightToLeft changed event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			if (RightToLeft == RightToLeft.Yes)
			{
				textFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft ;
			}
			else
			{
				textFormat.FormatFlags = 0 ;
			}
			
			base.OnRightToLeftChanged(e) ;
		}

		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			lock (this)
			{
				if (e.Button == MouseButtons.Left && inputEnabled)
				{
					if (spinButtonsEnabled)
					{
						//have we clicked on the spin control?
						if (spinUpRect.Contains(e.X, e.Y))
						{
							if (editing)			SendKeys.SendWait("{TAB}") ;

							spinning = true ;
							spinUpPressed = true ;
							spinDownPressed = false ;
						}
						else if (spinDownRect.Contains(e.X, e.Y))
						{
							if (editing)			SendKeys.SendWait("{TAB}") ;

							spinning = true ;
							spinDownPressed = true ;
							spinUpPressed = false ;
						}
						else
						{
							spinning = false ;
							spinUpPressed = false ;
							spinDownPressed = false ;
						}

						if (spinning)
						{
							spinIncrementCount = 0 ;
							IncrementValue () ;
							this.Invalidate(spinUpPressed ? spinUpRect : spinDownRect) ;

							//start timer for repetition: 200 ms interval
							spinTimer.Start() ;
						}
						// else we're not on the spin control, so continue processing
					}

					if (!spinning)		//do nothing else if spinning has commenced
					{
						if (barValid)
						{
							//have we clicked on the percent bar?
							if (barRect.Contains(e.X, e.Y))
							{
								barDragging = true ;

								if (editing)			SendKeys.SendWait("{TAB}") ;

								SetValueFromBar(e.X, e.Y) ;
							}
							// else we're not on the percent bar, so continue processing
						}
					}
				}

                if (e.Button == MouseButtons.Left)
                {
                    if (this.SourceChannel != null)
                        {
                            DoDragDrop(this.SourceChannel.Name, DragDropEffects.Copy | DragDropEffects.Move);
                        }
                }

				base.OnMouseDown(e) ;

				if (!spinning && !barDragging && inputEnabled)
				{
					//set focus on text box
					this.CreateTextBox() ;
					//tb2			textBox.Visible = true ;
					textBox2.Focus() ;
				}
			}
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			lock (this)
			{
				if (spinning)
				{
					spinTimer.Stop() ;
					this.Invalidate(spinUpPressed ? spinUpRect : spinDownRect) ;

					spinning		= false ;
					spinUpPressed	= false ;
					spinDownPressed	= false ;
				}
				else if (barDragging)
				{
					barDragging = false ;
				}

				base.OnMouseUp(e) ;
			}
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			lock (this)
			{
				if (barDragging)
				{
					Cursor = Cursors.Default ;
					SetValueFromBar(e.X, e.Y) ;
				}
				else if ((spinButtonsEnabled && (spinDownRect.Contains(e.X, e.Y) || spinUpRect.Contains(e.X, e.Y)))
					|| (barValid && barRect.Contains(e.X, e.Y))	)
				{
					Cursor = Cursors.Default ;
				}
				else if (inputEnabled)
				{
					Cursor = Cursors.IBeam ;
				}
                Point p = base.PointToClient(Cursor.Position);//ft
                bool enabled = this.iconRect.Contains(p);
                base.ToolTipEnabled = !enabled;//ft
                base.ToolTipErrorProviderEnabled = enabled;//ft
                this.ErrorProviderUpdateEnabled = enabled;
                if (enabled)
                {
                    base.ErrorProviderText = threshold_channel.ThresholdValuesMessage;
                }
                base.OnMouseMove(e) ;
			}
		}

        /// <summary>
        /// Mouse Enter Event
        /// </summary>
        /// <param name="e">Event Data</param>
        protected override void OnMouseEnter(EventArgs e) //mf
        {
            base.ToolTipEnabled = true;
            this.ErrorProviderUpdateEnabled=false;
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Mouse Leave Event
        /// </summary>
        /// <param name="e">Event Data</param>
        protected override void OnMouseLeave(EventArgs e) //mf
        {
            //Point p = base.PointToClient(Cursor.Position);

            //if (!this.iconRect.Contains(p))//textRect dans la version a Michael
            //{
            //    base.ToolTipEnabled = true;
            //    this.ErrorProviderUpdateEnabled=false;
            //}
            //if (!this.iconRect.Contains(p))//textRect dans la version a Michael
            //{
            //    base.ToolTipEnabled = true;
            //    this.ErrorProviderUpdateEnabled = false;
            //}
            //else
            //{
            //    //We have to disable ToolTip otherwise both ToolTip and ErrorProvider windows are shown
            //    base.ToolTipEnabled = false;
            //    base.ToolTipText = "";

            //    if (threshold_channel != null)
            //    {
            //        this.ErrorProviderText = threshold_channel.ThresholdValuesMessage; //Reload the latest limits

            //    }

            //    this.ErrorProviderUpdateEnabled=true;
            //}
            this.ErrorProviderUpdateEnabled = false;
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Use to control the ErrorProvider visibily during design time
        /// </summary>
        /// <returns></returns>
        public override bool ErrorProviderEnabled
        {
            get
            {
                return base.ErrorProviderEnabled;
            }
            set
            {            
                if (threshold_channel != null && value == true)
                {
                    base.errorProviderEnabled = value;
                    base.ErrorProviderText = threshold_channel.ThresholdValuesMessage;
                    this.IconEnabled = true;
                }
                else
                {
                    base.ErrorProviderText = string.Empty;
                    base.errorProviderEnabled = value;
                }
                OnPropertyChanged("ErrorProviderText");
                AddToQueue(true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ShouldSerializeErrorProviderEnabled() //ft ajout
        {
            return ErrorProviderEnabled;
        }
        /// <summary>
        /// ErrorProviderUpdateEnabled
        /// </summary>
        [Browsable(false)] //non visible dans le property grid
        public bool ErrorProviderUpdateEnabled
        {
            get { return enableErrorProviderUpdate; }
            set { enableErrorProviderUpdate = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ShouldSerializeErrorProviderUpdateEnabled() //ft ajout
        {
            return false;
        }
        /// <summary>
        /// IsErrorProviderUpdateEnable
        /// </summary>
        /// <returns></returns>
        //public bool IsErrorProviderUpdateEnable() //mf
        //{
        //    return enableErrorProviderUpdate;
        //}
        /// <summary>
        /// DisableErrorProviderUpdate
        /// </summary>
        //public void DisableErrorProviderUpdate() //mf
        //{
        //    enableErrorProviderUpdate = false;
        //}
        /// <summary>
        /// EnableErrorProviderUpdate
        /// </summary>
        //public void EnableErrorProviderUpdate() //mf
        //{
        //    enableErrorProviderUpdate = true;
        //}

		#endregion

		#region Text Box Events
		private void OnBoxGotFocus(object sender, EventArgs e)
		{
			lock (this)
			{
				//tb		textBox.Text = text ;
				textBox2.Text = text ;
				editing = true ;
			}
		}

		private void OnBoxValidating(object sender, CancelEventArgs e)
		{
			lock (this)
			{
				e.Cancel = ValidateText() ;
			}
		}

		private void OnBoxLostFocus(object sender, EventArgs e)
		{
			lock (this)
			{
				ValidateText() ;

				editing = false ;
				//tb		textBox.Visible = false ;
				this.DestroyTextBox ();
			
				//do this in case text hasn't changed, but needs shortening
				AddToQueue(true) ;
			}
		}

		private bool ValidateText()
		{
			bool	cancel		= false ;

			if (editing)	//will be 'false' if escape was pressed
			{
				if (numericOnly)
				{
//tb				KineticaRT.Values.Double	new_val	= new KineticaRT.Values.Double(textBox.Text) ;
					KineticaRT.Values.Double	new_val	= new KineticaRT.Values.Double(textBox2.Text) ;
					double						new_dbl	= new_val.ToDouble() ;

					if (System.Double.IsInfinity(new_dbl) || System.Double.IsNaN(new_dbl) ||
						new_dbl > maximum || new_dbl < minimum)
					{
						//invalid value: retain focus
						cancel = true ;
					}
				}

				if (!cancel)
				{
//tb				SetText(textBox.Text, true) ;
					SetText(textBox2.Text, true) ;
				}
				else
				{
					//ErrorProvider		ep	= new ErrorProvider(this) ;
					//ep.SetError(textBox, "invalid value skc") ;

//tb				textBox.SelectAll() ;
					textBox2.SelectAll() ;
				}
			}

			return cancel ;
		}

		private void OnBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			lock (this)
			{
				if (e.KeyChar == (char)13)			//return
				{
					e.Handled = true ;
					//if text ok (=shouldn't cancel) move focus on
					if (!ValidateText())
					{
						//Simulate the tab key being pressed
						// - this can revert straight back to focussing on this
						//object if no other tabstops
						//(note that escape will clear focus without tabbing on)
						//					SendKeys.Send("{TAB}") ;
						this.Parent.Focus() ;
					}
				}
				else if (e.KeyChar == (char)9)		//tab
				{
					Parent.GetNextControl (Parent,true).Focus() ;
				}
				else if (e.KeyChar == (char)27)		//escape
				{
					//ignore new text
					e.Handled = true ;
					editing = false ;		//prevent validation
					//move focus on
					//tb			this.InvokeLostFocus(textBox, EventArgs.Empty) ;
					this.InvokeLostFocus(textBox2, EventArgs.Empty) ;
				}

				//			if (e.Handled)
				//			{
				//				//simulate the tab key being pressed
				//				//##this is flawed if this is the only TabStop
				//				//as focus comes straight back to the display and re-edits it
				//				SendKeys.Send("{TAB}") ;
				//			}

				//			//this doesn't seem ideal, but I can't find a better way atm
				//			if (e.Handled)
				//			{
				//				//this works iff there is another control to take the focus
				//				System.Windows.Forms.Control	ctrl = Parent.GetNextControl(this, true) ;
				//				if (ctrl != null)
				//				{
				//					ctrl.Focus() ;
				//				}
				//				else
				//				{
				//					//this works, but doesn't allow tabbing back in again
				//					textBox.Enabled = false ;
				//					textBox.Enabled = true ;
				//				}
				//			}
			}
		}
		#endregion

		#region Timer events
		private void OnSpinTimerElapsed(object Sender, ElapsedEventArgs e)
		{
			if (spinning)
			{
				IncrementValue() ;
			}
			else
			{
				//just in case!
				spinTimer.Stop() ;
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
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value v)
		{
			//lock (this)  //ft pourquoi ce lock? ... ce lock lorsque activ� cause une baisse de performance du refresh du SineWave (thread fast)
			{
				busySettingValue = true ;
				//ft                 
                SetValue(sender.Value, true) ;

                //SetValue(v, true);//ft bug bug sender.Value contient l'ancienne valeur pas la nouvelle...
				busySettingValue = false ;
			}
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
                String messageCheck = string.Empty;
                //String errorProviderCurrentText = string.Empty;

                SetFormat();

				//also need to reformat the value
                if (sourceChannel != null && useSourceValue)
				{
					SetText(sourceChannel.ToString(), true) ;
                    //ft SetText(sourceChannel.Value.ToString(), true);
				}
                if (  e == KineticaRT.Channels.Channel.HighHighPropertyChangedEventArgs ||
                      e == KineticaRT.Channels.Channel.HighPropertyChangedEventArgs ||
                      e == KineticaRT.Channels.Channel.LowPropertyChangedEventArgs ||
                      e == KineticaRT.Channels.Channel.LowLowPropertyChangedEventArgs ||
                      e == KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs) //Ajout de ColorPropertyChangedEventArgs pcq dans la Propriete SourceChannel c'est ce qu'il prend pour s'initialiser
                {
                    if (threshold_channel != null)
                    {
                        
                        messageCheck = threshold_channel.ThresholdValuesMessage;
                        //errorProviderCurrentText = this.errorProviderText;

                        if (base.ErrorProviderEnabled == true)
                        {
                            //if (this.ErrorProviderUpdateEnabled ||
                            //     messageCheck == String.Empty ||
                            //    (errorProviderCurrentText == String.Empty & messageCheck != String.Empty ))
                            if (this.ErrorProviderUpdateEnabled || messageCheck != String.Empty)
                            {
                                this.ErrorProviderText = messageCheck;
                                IconEnabled = messageCheck != string.Empty;
                            }
                            else
                            {
                                IconEnabled = false;
                            }
                        }
                    }
                }
                if (e == KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs)
                {
                    if (threshold_channel != null)
                    {
                        if (threshold_channel.Color != Color.Empty)
                        {
                            this.BackColor = threshold_channel.Color;
                        }

                    }
                    return;
                }
			}
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

            if (destination != "")
            {
                DestinationChannel = OnObjectSearch(destination) as Channels.IChannel;
                if (this.destinationChannel == null) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }
            base.Connect();//ft ajout pour avoir le visibleSource fonctionnel
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
            base.Disconnect();
			if (sourceChannel != null)			SourceChannel = null ;
			if (destinationChannel != null)		DestinationChannel = null ;
		}
		#endregion

	
		private void CreateTextBox ()
		{
			if (textBox2 != null) return ;
            AddToQueue(true);
			textBox2 = new System.Windows.Forms.TextBox();
          
			// textBox
			textBox2.AutoSize = false;
			textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBox2.Name = "textBox";
			textBox2.Size = new System.Drawing.Size(100, 20);
			textBox2.TabIndex = 0;
			textBox2.Text = "";
			textBox2.Font = Font ;
			textBox2.WordWrap = false;
            
            //ft le Textbox supporte pas le Transparent alors tranforner le transparent en Black ...textBox2.SetStyle( ControlStyles.SupportsTransparentBackColor, true);//ft pour supporter les background transparent
            //if (BackColor == Color.Transparent)
            //    BackColor = Color.Black;
			textBox2.BackColor = BackColor ;
			textBox2.ForeColor = ForeColor ;
//			textBox2.AcceptsTab=true ;
			switch (textAlign)
			{
				case ContentAlignment.BottomLeft:
				{
					textBox2.TextAlign		= HorizontalAlignment.Left ;
				}
					break ;

				case ContentAlignment.MiddleLeft:
				{
					textBox2.TextAlign		= HorizontalAlignment.Left ;
				}
					break ;

				case ContentAlignment.TopLeft:
				{
					textBox2.TextAlign		= HorizontalAlignment.Left ;
				}
					break ;

				case ContentAlignment.BottomCenter:
				{
					textBox2.TextAlign		= HorizontalAlignment.Center ;
				}
					break ;

				case ContentAlignment.MiddleCenter:
				{
					textBox2.TextAlign		= HorizontalAlignment.Center ;
				}
					break ;

				case ContentAlignment.TopCenter:
				{
					textBox2.TextAlign		= HorizontalAlignment.Center ;
				}
					break ;

				case ContentAlignment.BottomRight:
				{
					textBox2.TextAlign		= HorizontalAlignment.Right ;
				}
					break ;

				case ContentAlignment.MiddleRight:
				{
					textBox2.TextAlign		= HorizontalAlignment.Right ;
				}
					break ;

				case ContentAlignment.TopRight:
				{
					textBox2.TextAlign		= HorizontalAlignment.Right ;
				}
					break ;
			}

			//give it enough height for (hopefully) any characters
			this.Font.GetHeight() ;
//tb		SizeF		size = g.MeasureString("Iy#", this.Font) ;
			SizeF		size = this.CreateGraphics().MeasureString("Iy#", this.Font) ;

			textBox2.Left = textRect.X ;
			textBox2.Width = textRect.Width ;
			textBox2.Height = (int)(size.Height) ;
			//cannot be taller than the display is
			if (textBox2.Height > textRect.Height)
			{
				textBox2.Height = textRect.Height ;
			}

			switch (textFormat.LineAlignment)
			{
				case StringAlignment.Center:
				{
					textBox2.Top = textRect.Top + (textRect.Height - textBox2.Height) / 2 ;
				}
					break ;

				case StringAlignment.Near:		//top
				{
					textBox2.Top = textRect.Top ;
				}
					break ;

				case StringAlignment.Far:		//bottom
				{
					textBox2.Top = textRect.Bottom - textBox2.Height ;
				}
					break ;
			}

			//connect to events from text box
			textBox2.GotFocus	+= new EventHandler(OnBoxGotFocus) ;
			textBox2.LostFocus	+= new EventHandler(OnBoxLostFocus) ;
			textBox2.KeyPress	+= new KeyPressEventHandler(OnBoxKeyPress) ;
			textBox2.Validating	+= new CancelEventHandler(OnBoxValidating) ;

			this.Controls.Add (textBox2);
		}

		private void DestroyTextBox ()
		{
			if (textBox2 == null) return ;

			textBox2.GotFocus	-= new EventHandler(OnBoxGotFocus) ;
			textBox2.LostFocus	-= new EventHandler(OnBoxLostFocus) ;
			textBox2.KeyPress	-= new KeyPressEventHandler(OnBoxKeyPress) ;
			textBox2.Validating	-= new CancelEventHandler(OnBoxValidating) ;

			this.Controls.Remove(textBox2) ;
			textBox2 = null ;
		}
        public event EventHandler DisplayValueChanged; //ft
        private void OnDisplayValueChanged(object sender, EventArgs e) //ft
        {
            EventHandler handler = DisplayValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
	}
}

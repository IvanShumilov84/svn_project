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
	#region Declarations (enums)
	/// <summary>
	/// Enumeration of possible button styles
	/// </summary>
	public enum ButtonStyles
	{
		/// <summary>
		/// Standard button style
		/// </summary>
		Button,
		/// <summary>
		/// Checkbox style
		/// </summary>
		CheckBox,
		/// <summary>
		/// Radio Button style
		/// </summary>
		RadioButton,
	}

	/// <summary>
	/// Enumeration of possible Button states
	/// </summary>
	public enum ButtonStates
	{
		/// <summary>
		/// Pressed / Checked state
		/// </summary>
		Pressed			= ButtonState.Checked,
		/// <summary>
		/// Unpressed / Unchecked state
		/// </summary>
		Unpressed		= ButtonState.Normal,
		/// <summary>
		/// Invalid state
		/// </summary>
		Invalid			= ButtonState.Inactive,
	}

	/// <summary>
	/// Enumeration of possible button return types
	/// </summary>
	public enum ButtonReturnTypes
	{
		/// <summary>
		/// Button stays pressed until clicked on again
		/// </summary>
		Normal,
		/// <summary>
		/// Button stays pressed until released by something like an Interlock.
		/// Sticky buttons cannot be released by clicking
		/// </summary>
		Sticky,
        /// <summary>
		/// Button is Pressed for only as long as it is held down during a click,
		/// then it reverts to Unpressed but doesn't reset the destination channel.
		/// </summary>
        AutoReturn,
        /// <summary>
        /// Button is Pressed for only as long as it is held down during a click,
        /// then it reverts to Unpressed
        /// </summary>
        AutoReset,
        /// <summary>
        /// Button is Pressed for as long as it is held down during a click,
        /// then it reverts to Unpressed after a 600ms.
        /// </summary>
        SlowAutoReset,
        /// <summary>
        /// Button is Pressed for as long as it is held down during a click,
        /// then it reverts to Unpressed after a 600ms.
        /// </summary>
        TimeDelayReset,
    }
	#endregion

	#region INTERFACE: IButton
	/// <summary>
	/// Interface used by the KineticaRT.Controls.Button.Interlock 
	/// to enable different types of button to be connected together.
	/// </summary>
	public interface IButton
	{
		/// <summary>Sets the value of the button to true/on/pressed or false/off/unpressed</summary>
		/// <param name="Pressed">True to set the Value to Pressed; False to set the Value to Unpressed</param>
		/// <remarks>This allows buttons to have different value types and still be able to be interlocked</remarks>
		void SetButtonState(bool Pressed) ;
		
		/// <summary>Gets the name of this button.</summary>
		/// <value>A string representing the name of the button.</value>
		string Name
		{
			get ;
		}

        // <summary>
        // InputEnabledSource
        // </summary>
        // //ft TODO pour le RadioButton et CheckBox ca serait une bonne implémentation a faire
        //string InputEnabledSource//ft 
        //{
        //    get;
        //    set;
        //}

		/// <summary>Occurs when the value of the Value property changes.</summary>
		event ValueChangedEventHandler ValueChanged ;   
	}
	#endregion

	/// <summary>
	/// KineticaRT Button Control. Also provides checkbox and radio button functionality.
	/// </summary>
    [ChangeRecord("2.0.0.2", "21 05 2008", "N. Savidge", "Added MouseSource property.")]
    [ChangeRecord("2.0.0.1", "?? ?? ??", "?? ????", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT")]
    [Copyright("Copyright © KineticaRT Ltd 2008")]
    
    [DefaultProperty("Value")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Button.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Button))]
    [Serializable()]
    public class Button : KineticaRT.Controls.Control, IButton, ISource, IDestination, IInputEnabledSource 
	{
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Controls.Button", "Controls.Button Messages");
		//ft private bool settingDestination = false ;

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
						if ((key == null) || (key != "29BA636DFC43" && key != "4CD0571EE68D"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "29BA636DFC43" || key == "4CD0571EE68D")) key = "{29BA636D-FC43-4710-BD8F-AAC640865A0F}" ; 
						context.SetSavedLicenseKey (typeof(Button), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Button), null) ;
					if (key == "{29BA636D-FC43-4710-BD8F-AAC640865A0F}") licence = new License ("Run") ;
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

		#region Variables and Declarations

        private Color gradientLeftColor ;//ft
        private Color gradientRightColor ;//ft

		private Channels.IChannel			sourceChannel ;
		private Channels.IChannel			inputEnabledSourceChannel ;
		private Channels.IChannel			destinationChannel ;
        private Channels.IChannel           timeDelayIntervalSourceChannel;
        //private bool						return_needed = false ;//ft pas necessaire??
		private string						source ;
		private string						inputEnabledSource ;
		private string						destination ;
        private string                      timeDelayIntervalSource;
        private int                         timeDelayInterval;
        private ButtonStyles style;
		private bool						boxOnLeft ;
		private string						text ;
		private TextOrientations			orientation ;
		private object						internalValue ;			//actual value
		private ButtonReturnTypes			type ;
		private bool						useSourceFormat ;
		private bool						inputEnabled ;
        private bool                        captureClick;
        /// <summary>
        /// Specifies if button accepts input
        /// </summary>
		protected bool						inputEnabledValue ;
        

		//internal variables
		private Channels.ValueChangedEventHandler	srcHdlr ;
		private Channels.ValueChangedEventHandler	inputEnabledSrcHdlr ;
        private Channels.ValueChangedEventHandler   timeDelayIntervalSrcHdlr;
        private PropertyChangedEventHandler propertyHdlr;
        private ValueChangedEventHandler         valueCaptureClickHdlr;

		/// <summary>
		/// Bounding rectangle for the Button Text (or picture)
		/// </summary>
		protected	Rectangle			boundsRect ;

		private	Rectangle				textRect ;
		private	Rectangle				boxRect ;
		private KineticaRT.Values.Value	krtValue ;			//value as KRT bool or 'type mismatch' //ft pas certain que certaine variable soit necessaire ?? devrait être retiré et remplace par this.value?
		private ButtonStates			state ;				//value as button state
		private ButtonStates			oldState ;
		private ButtonStates			newState ;
        private BorderType appearance;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Button class
		/// </summary>
		public Button()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

//			//demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
#if KRT_license
			demoMode = !LicenseManager.IsValid (typeof(Button), this, out license) || (license.LicenseKey != "Run");
#endif

			srcHdlr		= new Channels.ValueChangedEventHandler(source_OnValueChanged) ;
			inputEnabledSrcHdlr		= new Channels.ValueChangedEventHandler(OnInputEnabledValueChanged) ;
            timeDelayIntervalSrcHdlr = new Channels.ValueChangedEventHandler(OnTimeDelayIntervalValueChanged);
            propertyHdlr = new PropertyChangedEventHandler(OnSourcePropertyChanged);

			borderType				= BorderType.None ;
			style					= ButtonStyles.Button ;
			boxOnLeft				= true ;
			text					= "KineticaRT Button" ;
			orientation				= TextOrientations.Right;
			internalValue			= 0 ;
			sourceChannel			= null ;
			inputEnabledSourceChannel = null ;
			destinationChannel		= null ;
            timeDelayIntervalSourceChannel = null;
			source					= "" ;
			inputEnabledSource		= "" ;
			destination				= "" ;
            timeDelayIntervalSource = "" ;
            timeDelayInterval       = 100; ;
			type					= ButtonReturnTypes.Normal ;
			useSourceFormat			= true ;
			inputEnabled			= true ;
			inputEnabledValue		= false ;

			boxRect		= Rectangle.Empty ;
			textRect	= Rectangle.Empty ;
			boundsRect	= Rectangle.Empty ;
			krtValue	= KineticaRT.Values.Values.False ;
			state		= ButtonStates.Unpressed ;
			oldState	= ButtonStates.Unpressed ;
			newState	= ButtonStates.Unpressed ;
            appearance = BorderType.Raised;

			SetLook() ;
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
            this.SuspendLayout();
            // 
            // Button
            // 
            this.GradientEnabled = true;
            this.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.Name = "Button";
            this.Size = new System.Drawing.Size(100, 40);
            this.ResumeLayout(false);

		}
		#endregion

		#region Properties

        /// <summary>
        /// Gets or sets the Color at the left hand side of the control
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets the Color at the left hand side of the control")]
        public Color GradientLeftColor
        {
            get
            {
                return gradientLeftColor;
            }
            set
            {
                if (gradientLeftColor != value)
                {
                    gradientLeftColor = value;
                    if (GradientEnabled) AddToQueue(false);
                    OnPropertyChanged("GradientLeftColor");
                }
            }
        }
      

        /// <summary>
        /// Gets or sets the Color at the right hand side of the control
        /// </summary>
        [Category("Gradient")]
        [DefaultValue(typeof(Color),"Navy")]
        [Description("Gets or sets the Color at the right hand side of the control")]
        public Color GradientRightColor
        {
            get
            {
                return gradientRightColor;
            }
            set
            {
                if (gradientRightColor != value)
                {
                    gradientRightColor = value;
                    if (GradientEnabled) AddToQueue(false);
                    
                    OnPropertyChanged("GradientRightColor");
                }
            }
        }


		/// <summary>
		/// Gets or sets the style of the Button
		/// </summary>
		[Category("Button")]
		[DefaultValue(ButtonStyles.Button)]
		[Description("Gets or sets the style of the Button")]
		public ButtonStyles Style
		{
			get
			{
				return style ;
			}
			set
			{
				if (style != value)
				{
					style = value ;
					resized = true ;
					SetLook() ;
					AddToQueue(true) ;
					OnPropertyChanged("Style") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the box in Radio button and Checkbox styles is on the left
		/// </summary>
		[Category("Button")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the box in Radio button and Checkbox styles is on the left")]
		public bool BoxOnLeft
		{
			get
			{
				return boxOnLeft ;
			}
			set
			{
				if (boxOnLeft != value)
				{
					boxOnLeft = value ;
					AddToQueue(false) ;
					OnPropertyChanged("BoxOnLeft") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Text displayed by the button
		/// </summary>
		[Category("Button")]
		[DefaultValue("KineticaRT Button")]
		[Description("Gets or sets the Text displayed by the button")]
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

		/// <summary>
		/// Gets or sets the orientation of the Text
		/// </summary>
		[Category("Button")]
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
		/// Gets or sets the current value
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets the current value")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual object Value
		{
			get
			{
				return this.internalValue ;
			}
			set
			{
				bool fire_property_changed = false ;
				//ft bool fire_value_changed = false ;  

                //lock (this)//ft test pour desactiver le  LOCK  
				{

					//if (this.value != value)
					{
						Values.Value		new_krt_value	= KineticaRT.Values.Values.TypeMismatch ;

						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Value =: " + (value!=null?value.ToString():"null"), "Controls.Button: " + this.Parent.Name + ", " + this.Name) ;
						this.internalValue = value;

						//convert to boolean
						new_krt_value = value as Values.Boolean ;
						if (new_krt_value == null)
						{
							if (value is Values.Double)
							{
								new_krt_value = new Values.Boolean(((Values.Double)value).ToDouble() != 0) ;
							}
							else if (value is System.Boolean)
							{
								new_krt_value = new KineticaRT.Values.Boolean ((System.Boolean)value) ;
							}
							else if (value is Values.Text)
							{
								try
								{
									new_krt_value = new KineticaRT.Values.Boolean (System.Convert.ToBoolean(value)) ;
								}
								catch (System.FormatException)
								{
									new_krt_value = KineticaRT.Values.Values.False ;
								}
							}
							else if (value is System.String)
							{
								try
								{
									new_krt_value = new KineticaRT.Values.Boolean (System.Convert.ToBoolean(value)) ;
								}
								catch (System.FormatException)
								{
									new_krt_value = KineticaRT.Values.Values.False ;
								}
							}
						}
						//disallow invalid doubles
						if ((new_krt_value == null) && (value is System.Double) &&
							(!System.Double.IsInfinity((System.Double)value))	&&
							(!System.Double.IsNaN((System.Double)value))			)
						{
							new_krt_value = new KineticaRT.Values.Boolean ((System.Double)value != 0) ;
						}
						if (new_krt_value == null)
						{
							new_krt_value = Values.Values.TypeMismatch ;
						}

						//work out corresponding state
						if (new_krt_value is Values.Boolean)
						{
							bool		temp	= (new_krt_value.ToDouble() != 0) ;

							SetState((temp == false) ? ButtonStates.Unpressed : ButtonStates.Pressed) ;
						}
						else
						{
							SetState(ButtonStates.Invalid) ;
						}

						//ft la variable krtValue est desynchro...donc force refreseh de la destination      
                        //ft if (new_krt_value.CompareTo (krtValue) != 0)
						//{
							//ft krtValue = new_krt_value ;
                            krtValue = new_krt_value;
							//set destination
							if (destinationChannel != null)
							{
								//ft if (settingDestination == false)
								//ft{
									//ft settingDestination = true ;
									//ft destinationChannel.Value = krtValue ;
                                //ft testOnValueChanged(); //ft test pour les boutons autoreturn que le textcolor s affiche comme il faut
                                    destinationChannel.Value = new_krt_value;
									bool new_state = false ;
									try
									{
                                        new_state = System.Convert.ToBoolean(destinationChannel.Value);//ft 20130206 je pense qu'avec l'opc c'est trop rapide, et que la valeur du channel n'est pas encore changé et que le bouton semble Pressed, Unpressed et »Pressed encore
									}
									catch (System.FormatException)
									{
									}

                                    if (this.sourceChannel != this.destinationChannel)//ft lorsque la source = la destination le SetState sera dans le OnValueChanged plus bas
                                    {
                                        SetState(new_state ? ButtonStates.Pressed : ButtonStates.Unpressed);
                                    }
									//ft settingDestination = false ;
								//ft}
							}
							//fire Value Changed event for Interlocks
							//ft fire_value_changed = true ; // OnValueChanged() ;
						//}
						fire_property_changed = true ; // OnPropertyChanged("Value") ;
					}
				}
                if (this.sourceChannel != this.destinationChannel  || this.sourceChannel == null)
                {//ft lorsque la source = la destination le OnValueChanged sera  appelé dans le OnValueChanged(Channels.IChannel sender, KineticaRT.Values.Value newValue) plus bas.  Ceci evite d'appeler levenement 2 fois lorsque dans une sequence vb on attache un handler sur le control BOuton.ValueVhanged et non sur le channel du bouton
                    /*if (fire_value_changed)*/
                    OnValueChanged();
                }
				if (fire_property_changed) OnPropertyChanged("Value") ;
			}
		}

		/// <summary>
		/// Gets the state of the Button represented by the current Value
		/// </summary>
		[Category("Button")]
		[Description("Gets the state of the Button represented by the current Value")]
		public ButtonStates State
		{
			get
			{
				return state ;
			}
		}

        /// <summary>
		/// Gets or sets the object that is the Source of the Button's value
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets the object that is the Source of the Button's value")]
		public virtual Channels.IChannel SourceChannel
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

		/// <summary>
		/// Gets or sets the object that is the Source of the Button's InputEnabled value
		/// </summary>
		private Channels.IChannel InputEnabledSourceChannel
		{
			set
			{
				if (inputEnabledSourceChannel != value)
				{
					//free old handler if any
					if (inputEnabledSourceChannel != null)		
					{
						inputEnabledSourceChannel.ValueChanged		-= inputEnabledSrcHdlr ;
						inputEnabledValue = false ;
					}

					//attach new source
					inputEnabledSourceChannel = value ;
					if (inputEnabledSourceChannel != null)
					{
						inputEnabledSourceChannel.ValueChanged		+= inputEnabledSrcHdlr ;
						inputEnabledValue = (inputEnabledSourceChannel.Value != null) && inputEnabledSourceChannel.Value.ToDouble() != 0.0 ;
					}
				}
			}
		}
		private bool ShouldSerializeInputEnabledSourceChannel()
		{
			return false ;
		}

        /// <summary>
        /// Gets or sets the style of the Button
        /// </summary>
        [Category("Button")]
        [DefaultValue(100)]
        [Description("Gets or sets the time delay interval of the Button")]
        public int TimeDelayInterval
        {
            get
            {
                return timeDelayInterval;
            }
            set
            {
                if (timeDelayInterval != value)
                {
                    if (value <= 0)
                    {
                        value = 1;
                    }
                    timeDelayInterval = value;
                    OnPropertyChanged("TimeDelayInterval");
                }
            }
        }

        /// <summary>
        /// Gets or sets the object that is the Source of the Button's TimeDelayInterval value
        /// </summary>
        [Category("Button")]
        private Channels.IChannel TimeDelayIntervalSourceChannel
        {
            set
            {
                if (timeDelayIntervalSourceChannel != value)
                {
                    //free old handler if any
                    if (timeDelayIntervalSourceChannel != null)
                    {
                        timeDelayIntervalSourceChannel.ValueChanged -= timeDelayIntervalSrcHdlr;
                        if (timeDelayInterval <= 0)
                        {
                            timeDelayInterval = 1;
                        }
                    }

                    //attach new source
                    timeDelayIntervalSourceChannel = value;
                    if (timeDelayIntervalSourceChannel != null)
                    {
                        timeDelayIntervalSourceChannel.ValueChanged += timeDelayIntervalSrcHdlr;
                        timeDelayInterval = (int)timeDelayIntervalSourceChannel.Value.ToDouble() ;
                        if (timeDelayInterval <= 0)
                        {
                            timeDelayInterval = 1;
                        }
                    }
                }
            }
        }
        private bool ShouldSerializetimeDelayIntervalSourceChannel()
        {
            return false;
        }
        
        /// <summary>
		/// Gets or sets the object that is the Destination for the Button's value
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets the object that is the Destination for the Button's value")]
		public virtual Channels.IChannel DestinationChannel
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

					if ((destinationChannel != null) && (sourceChannel != destinationChannel ))		destinationChannel.Value = krtValue ;//TODO ft krtValue ou this.value ???

					OnPropertyChanged("DestinationChannel") ;
				}
			}
		}
		private bool ShouldSerializeDestinationChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the object to be the Button's Source
		/// </summary>
		[Category("Button")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Button's Source")]
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
		/// Gets or sets the name of the object to be the Button's InputEnabled Source.
		/// </summary>
		/// <remarks>If this is set to a valid channel then it overrides the value of InputEnabled.</remarks>
		[Category("Button")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Button's InputEnabled Source. If this is set to a valid channel then it overrides the value of InputEnabled")]
		public string InputEnabledSource
		{
			get
			{
				return inputEnabledSource ;
			}
			set
			{
				if (inputEnabledSource != value)
				{
					inputEnabledSource = value ;
					OnPropertyChanged("InputEnabledSource") ;
				}
				//try and connect to source
				InputEnabledSourceChannel = OnObjectSearch(inputEnabledSource) as Channels.IChannel ;
			}
		}

        /// <summary>
        /// Gets or sets the name of the object to be the Button's TimeDelayInterval Source.
        /// </summary>
        /// <remarks>If this is set to a valid channel then it overrides the value of timeDelayInterval.</remarks>
        [Category("Button")]
        [DefaultValue("")]
        [Description("Gets or sets the name of the object to be the Button's TimeDelayInterval Source. If this is set to a valid channel then it overrides the value of timeDelayInterval")]
        public string TimeDelayIntervalSource
        {
            get
            {
                return timeDelayIntervalSource;
            }
            set
            {
                if (timeDelayIntervalSource != value)
                {
                    timeDelayIntervalSource = value;
                    OnPropertyChanged("TimeDelayIntervalSource");
                }
                //try and connect to source
                TimeDelayIntervalSourceChannel = OnObjectSearch(timeDelayIntervalSource) as Channels.IChannel;
            }
        }
        
        /// <summary>
		/// Gets or sets the name of the object to be the Button's Destination
		/// </summary>
		[Category("Button")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the Button's Destination")]
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
		/// Gets or sets the return type
		/// </summary>
		[Category("Button")]
		[DefaultValue(ButtonReturnTypes.Normal)]
		[Description("Gets or sets the return type")]
		public ButtonReturnTypes Type
		{
			get
			{
				return type ;
			}
			set
			{
				if (type != value)
				{
					type = value ;
					OnPropertyChanged("Type") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Button gets its ToolTip and Text
		/// from the its Source channel
		/// </summary>
		[Category("Button")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Button gets its ToolTip and Text from its Source channel")]
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
		/// Gets or sets whether Input is Enabled
		/// </summary>
		/// <remarks>This differs from Enabled by not preventing drag and drop or tooltips</remarks>
		[Category("Button")]
		[DefaultValue(true)]
		[Description("Gets or sets whether Input is Enabled")]
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
					SetLook() ;
					AddToQueue(false) ;
					OnPropertyChanged("InputEnabled") ;
				}
			}
		}

        private string[] mouseSource;
        /// <summary>
        /// Array of strings containing the names of other controls which this button will treat as valid sources for mouse clicks which press this button.
        /// </summary>
        [Category("Button")]
        [Description("Array of strings containing the names of other controls which this button will treat as valid sources for mouse clicks which press this button.")]
        public string[] MouseSource
        {
            get { return mouseSource; }
            set
            {
                if (mouseSource != value)
                {
                    mouseSource = value;
                    OnPropertyChanged("MouseSource");

                    ConnectMouseSource();
                }
            }
        }


        /// <summary>
        /// CaptureClick
        /// </summary>
        /// <param name="valeur"></param>
        public void CaptureClick(bool valeur)
        {
            
                this.captureClick = valeur;
                if (CaptureClickValueChanged != null)
                {
                    CaptureClickValueChanged(valeur);
                }


        }
        //public bool CaptureClick
        //{
        //    get
        //    {
        //        return this.captureClick;
        //    }
        //    set
        //    {
        //        this.captureClick = value;
        //        if (CaptureClickValueChanged != null)
        //        {
        //            CaptureClickValueChanged(value);
        //        }

        //    }
        //}
		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
			//ft lock (this)
			{

                if (backColorChanged)//ft pour etre certain que le refresh se fera et que le flag sera resette seulement si c pacq il venait de la propritete backcolor
                {
                    backColorChanged = false;
                    backColorBrush.Color = BackColor;//ft ajoute ici pour eviter des crossthread on set le backBrush dans le thread qui fait le Repaint sinon ca va planter dans g =CreateGraphics de control.cs
                }
                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Repaint: " + this.state + ", " + FullRedraw.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name) ;
				BasicRepainting(FullRedraw, g) ;

				DrawText(g) ;
				DrawBox(g) ;
			}
		}

		/// <summary>
		/// Basic setup and background drawing
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		protected virtual void BasicRepainting(bool FullRedraw, Graphics g)
		{
			int			offset		= 0 ;

			if (style == ButtonStyles.Button)
			{
				if (state == ButtonStates.Pressed)
				{
					offset = 1 ;
				}
				else if (state == ButtonStates.Unpressed)
				{
					offset = -1 ;
				}
			}

			CalculateDisplayRectangle() ;
			//			base.repaint(true, g) ;

            ////ft
            if (this.GradientEnabled == true)
            {
                using( Brush brush = new LinearGradientBrush(ClientRectangle, gradientLeftColor, gradientRightColor, this.LinearGradientMode))//LinearGradientMode.Horizontal
                {
                    g.FillRectangle(brush, ClientRectangle);
                }

            }
            else
            {
                g.FillRectangle(backColorBrush, ClientRectangle);
            }


                ////ft TESTTTTTT
                //g.FillRectangle(backBrush, ClientRectangle);

            DrawBorder(g) ;
            

			//button part...
			SetDisplayRegion(g) ;
                if (shape == Shapes.Rectangle)
                {
                    DrawSquareBorder(g, DisplayRectangle, appearance, this.BorderType, BackColor, 2);
                }
                else
                {
                    GraphicsPath path = CalculateShapePath(DisplayRectangle, shape);
                    Region rgn = new Region(path);
                    Region old_rgn = g.Clip;

                    g.Clip = rgn;
                    DrawShapedBorder(g, DisplayRectangle, appearance, BackColor, shape, 2);
                    g.Clip = old_rgn;
                    rgn.Dispose();
                    path.Dispose();
                }
			//always do this: it is little work and textRect needs resetting anyway
			CalcBoxRect(offset) ;

#if KRT_license
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
		}

		//calculates rectangles for the button
		private void CalcBoxRect(int Offset)
		{
			boundsRect = DisplayRectangle ;

			if (style == ButtonStyles.Button)
			{
				//no checkbox/radio
				boxRect = Rectangle.Empty ;
			}
			else
			{
				//box is square, and as high as possible
				boxRect = DisplayRectangle ;
				boxRect.Width = boxRect.Height ;

				boundsRect.Width -= boxRect.Width ;

				if (boxOnLeft)
				{
					boundsRect.X = boxRect.Right ;
				}
				else
				{
					boxRect.X = boundsRect.Right ;
				}
			}

			//offset the text rectangle
			textRect = boundsRect ;
			textRect.Offset(Offset, Offset) ;
		}

		/// <summary>
		/// Draws the button text at the required orientation
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		protected void DrawText (Graphics g)
		{
			int		length		= text.Length ;

			if (length > 0)
			{
				Color				clr ;
				StringFormat		format = new StringFormat() ;
				Brush				brush ;
		
				if (!(inputEnabled || inputEnabledValue))
				{
					//shade color towards BackColor
					int	red		= (2 * BackColor.R + ForeColor.R) / 3 ;
					int	green	= (2 * BackColor.G + ForeColor.G) / 3 ;
					int	blue	= (2 * BackColor.B + ForeColor.B) / 3 ;

					clr = Color.FromArgb(red, green, blue) ;
				}
				else
				{
					clr = ForeColor ;
				}
				brush = new SolidBrush(clr) ;

				format.LineAlignment = StringAlignment.Center ;
				format.Alignment = StringAlignment.Center ;

				//set up string alignments and transforms
				switch (orientation)
				{
					case TextOrientations.Down:
					{
						Rectangle		original_text_rect = textRect ;

						textRect.X = original_text_rect.Y ;
						textRect.Y = original_text_rect.X ;
						textRect.Width = original_text_rect.Height ;
						textRect.Height = original_text_rect.Width ;

						g.RotateTransform(90) ;
						g.TranslateTransform(0, -(original_text_rect.Right + original_text_rect.X)) ;

						if (style != ButtonStyles.Button)
						{
							if (boxOnLeft)
							{
								format.LineAlignment = StringAlignment.Near ;
							}
							else
							{
								format.LineAlignment = StringAlignment.Far ;
							}
						}
					}
					break ;

					case TextOrientations.Up:
					{
						Rectangle		original_text_rect = textRect ;

						textRect.X = original_text_rect.Y ;
						textRect.Y = original_text_rect.X ;
						textRect.Width = original_text_rect.Height ;
						textRect.Height = original_text_rect.Width ;

						g.RotateTransform(-90) ;
						g.TranslateTransform(-ClientRectangle.Height, 0) ;

						if (style != ButtonStyles.Button)
						{
							if (boxOnLeft)
							{
								format.LineAlignment = StringAlignment.Far ;
							}
							else
							{
								format.LineAlignment = StringAlignment.Near ;
							}
						}
					}
					break ;

					case TextOrientations.Left:
					{
						g.RotateTransform(180) ;
						g.TranslateTransform(-(textRect.Right + textRect.X), -(textRect.Bottom + textRect.Y)) ;

						if (style != ButtonStyles.Button)
						{
							if (boxOnLeft)
							{
								format.Alignment = StringAlignment.Near ;
							}
							else
							{
								format.Alignment = StringAlignment.Far ;
							}
						}
					}
					break ;

					case TextOrientations.Right:
					{
						if (style != ButtonStyles.Button)
						{
							if (boxOnLeft)
							{
								format.Alignment = StringAlignment.Near ; //.Far ;
							}
							else
							{
								format.Alignment = StringAlignment.Near ;
							}
						}
					}
					break ;
				}

				g.DrawString(text, this.Font, brush, textRect, format) ;
				g.ResetTransform() ;
				brush.Dispose() ;

				//draw 'unconnected' here
			}
		}

		/// <summary>
		/// Draws the box for checkbox/radio button styles
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		protected void DrawBox(Graphics g)
		{
			if (style == ButtonStyles.Button)			return ;

			switch (style)
			{
				case ButtonStyles.CheckBox:
				{
					ControlPaint.DrawCheckBox(g, boxRect, (ButtonState)state) ;
				}
				break ;

				case ButtonStyles.RadioButton:
				{
					ControlPaint.DrawRadioButton(g, boxRect, (ButtonState)state) ;
				}
				break ;
			}
		}
		#endregion

		#region Other methods
		private void SetState(ButtonStates newState)
		{
			lock(this)
			{
				//if (newState != state)
				{
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "State Change: " + newState.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name) ;
					state = newState ;
					SetLook() ;
					AddToQueue(false) ;
				}
			}
		}

		/// <summary>
		/// Sets the Button's appearance
		/// </summary>
		protected virtual void SetLook()
		{
			lock (this)
			{
				BorderType		new_appearance		= BorderType.None ;
			
				if (style == ButtonStyles.Button)
				{
					if (state == ButtonStates.Pressed)
					{
                        new_appearance = BorderType.Indented;
					}
					else if (state == ButtonStates.Unpressed)
					{
                        new_appearance = BorderType.Raised;
					}
				}

				if (appearance != new_appearance)
				{
					appearance = new_appearance ;
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Appearance Change: " + appearance.ToString(), "Controls.Button: " + (this.Parent != null ? this.Parent.Name + ", " : "") + this.Name) ;
					AddToQueue(false) ;
				}
			}
		}

		/// <summary>
		/// Sets up the tool tip and text based on the Source channel
		/// </summary>
		private void SetFormat()
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel	ch		= sourceChannel as Channels.IInfoChannel ;

				if (ch != null)
				{
					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;
					Text		= (ch.Description != null) ? ch.Description : "" ;
				}
			}
		}
		#endregion

		#region Mouse event handling

        private System.Windows.Forms.Control [] mouseSourceControl;
        private MouseEventHandler mouseDownHandler;
        private MouseEventHandler mouseUpHandler;
        private void ConnectMouseSource()
        {
            if (mouseSource == null) return;

            lock (this)
            {
                DisconnectMouseSource();
                mouseSourceControl = new Control[mouseSource.Length];
                mouseDownHandler = new MouseEventHandler(Button_MouseDown);
                mouseUpHandler = new MouseEventHandler(Button_MouseUp);

                for (int index = 0; index < mouseSource.Length; index++)
                {
                    mouseSourceControl[index] = SearchObject(mouseSource[index]) as System.Windows.Forms.Control;
                    if (mouseSourceControl[index] != null)
                    {
                        mouseSourceControl[index].MouseDown += mouseDownHandler;
                        mouseSourceControl[index].MouseUp += mouseUpHandler;
                    }
                }
            }
        }
        private void DisconnectMouseSource()
        {
            if (mouseSourceControl == null) return;

            lock (this)
            {
                foreach (System.Windows.Forms.Control control in mouseSourceControl)
                {
                    if (control != null)
                    {
                        control.MouseDown -= mouseDownHandler;
                        control.MouseUp -= mouseUpHandler;
                    }
                }
                mouseSourceControl = null;
                mouseDownHandler = null;
                mouseUpHandler = null;
            }
        }

        void Button_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }
        void Button_MouseUp(object sender, MouseEventArgs e)
        {
            OnMouseUp(e);
        }

		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
            if (System.Windows.Forms.MouseButtons.Left == e.Button)
            {
                HandleMouseDown(e);
            }
			base.OnMouseDown(e) ;
		}

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

		/// <summary>
		/// Deals with a mouse down event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected void HandleMouseDown(MouseEventArgs e)
		{
			if (inputEnabled || inputEnabledValue)
			{
				switch (type)
				{
                    case ButtonReturnTypes.AutoReturn :
                    {
						this.Capture = true ;
                        this.CaptureClick ( true);
						this.Value = KineticaRT.Values.Values.True ;
					}
					break ;

					case ButtonReturnTypes.Sticky:
					{
						if (state != ButtonStates.Pressed)
						{
							this.Value = KineticaRT.Values.Values.True ;
						}
					}
					break ;

                    default:		//normal, AutoReset, SlowAutoReset et TimeDelayReset
					{
						this.Capture = true ;
                        this.CaptureClick ( true);

						lock (this)
						{
							//swap state - not value atm
							oldState = state ;
							newState = (state == ButtonStates.Unpressed) ? ButtonStates.Pressed : ButtonStates.Unpressed ;
							System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Mouse Down: " + oldState.ToString()+ ", " + newState.ToString()+ ", " + this.krtValue.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name) ;
							SetState(newState) ;
						}
					}
					break ;
				}
			}
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
            if (System.Windows.Forms.MouseButtons.Left == e.Button)
            {
                if ((inputEnabled || inputEnabledValue) && captureClick && type == ButtonReturnTypes.Normal)
                {
                    if ((sourceChannel != null) && !ClientRectangle.Contains(e.X, e.Y))
                    {
                        Value = sourceChannel.Value;
                        //					if (state != oldState)
                        //					{
                        //						//revert state to what it was
                        //						SetState(oldState) ;
                        //					}
                    }
                    else if (state != newState)
                    {
                        SetState(newState);
                    }
                }
            }
			base.OnMouseMove(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
            if (System.Windows.Forms.MouseButtons.Left == e.Button)
            {
                if ((inputEnabled || inputEnabledValue || type != ButtonReturnTypes.Normal) && captureClick)
                {
                    if (type == ButtonReturnTypes.Normal)
                    {
                        if (ClientRectangle.Contains(e.X, e.Y))
                        {
                            //set value to match state
                            this.Value = (state == ButtonStates.Unpressed) ? KineticaRT.Values.Values.False : KineticaRT.Values.Values.True;
                        }
                        else
                        {
                            if (sourceChannel != null)
                            {
                                this.Value = sourceChannel.Value;
                            }
                            else
                            {
                                SetState(oldState);
                            }
                        }
                    }
                    else if (type == ButtonReturnTypes.AutoReturn)
                    {
                        //return to 'unpressed'
                        if (destinationChannel == null || (destinationChannel != null && destinationChannel.Value != null && destinationChannel.Value.ToDouble() != 0))
                        {
                            if (sourceChannel != destinationChannel || sourceChannel == null)//base.Value Sera updaté par le feedback voir source_OnValueChanged le la source = la destination
                            {
                                this.Value = KineticaRT.Values.Values.False;
                            }
                        }
                        if (this.captureClick)
                        {
                            this.Capture = false;
                            this.CaptureClick(false);
                        }
                    }
                    else if ((type == ButtonReturnTypes.AutoReset) || (type == ButtonReturnTypes.SlowAutoReset) || (type == ButtonReturnTypes.TimeDelayReset))
                    {
                        //set value to match state
                        this.Value = (state == ButtonStates.Unpressed) ? KineticaRT.Values.Values.False : KineticaRT.Values.Values.True;
                        start_timer();
                    }
                }
            }
			base.OnMouseUp(e) ;
		}

        void start_timer()//ft 
        {
            // Attach to timer.
            lock (this)
            {
                if (this.Value == KineticaRT.Values.Values.True)
                {
                    if (reset_timer == null)
                    {
                        reset_timer = new System.Windows.Forms.Timer();
                        reset_timer.Tick += new EventHandler(reset_timer_Tick);
                    }
                    else
                    {
                        reset_timer.Stop();
                    }

                    switch (type)
                    {
                        case ButtonReturnTypes.SlowAutoReset:
                            reset_timer.Interval = 600;
                            break;

                        case ButtonReturnTypes.TimeDelayReset:
                            if (timeDelayInterval == 0)
                            {
                                reset_timer.Interval = 1;
                            }
                            else
                            {
                                reset_timer.Interval = timeDelayInterval;
                            }
                            break;

                        default:
                            reset_timer.Interval = 100;
                            break;
                    }
                    reset_timer.Start();
                }
                else
                {
                    if (reset_timer != null)
                    {
                        reset_timer.Dispose();
                        reset_timer = null;
                    }
                }
            }
        }
        void reset_timer_Tick(object sender, EventArgs e)
        {
            lock (this)
            {
                reset_timer.Dispose();
                reset_timer = null;
            }

            this.Value = KineticaRT.Values.Values.False;
        }

        System.Windows.Forms.Timer reset_timer; //ft
        

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
				Destination = e.Data.GetData(DataFormats.Text).ToString() ;
			}

			base.OnDragDrop(e) ;
		}
		#endregion

		#region New Value handling (in and out)
		/// <summary>Sets the value of the button to Pressed or Unpressed</summary>
		/// <param name="Pressed">True to set the Value to Pressed; False to set the Value to Unpressed</param>
		public void SetButtonState(bool Pressed)
		{
			this.Value = Pressed ? Values.Values.True : Values.Values.False ;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newValue"></param>
		private void OnInputEnabledValueChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			lock (this)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Input Enabled Value Change: " + newValue.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name) ;
				bool boolean = newValue.ToDouble () != 0.0 ;
				this.inputEnabledValue = boolean ;
                SetLook();
				AddToQueue(false) ;
			}
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="newValue"></param>
        private void OnTimeDelayIntervalValueChanged(Channels.IChannel sender, KineticaRT.Values.Value newValue)
        {
            lock (this)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Time Delay Interval Value Change: " + newValue.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name);
                this.timeDelayInterval = (int)newValue.ToDouble();
                if (this.timeDelayInterval <= 0)
                {
                    this.timeDelayInterval = 1;
                }
            }
        }

        ////new value coming in
        //private void source_OnValueChanged(Channels.IChannel sender, KineticaRT.Values.Value newValue)
        //{
        //    //System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Value Change: " + newValue.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name) ;
        //    if (return_needed)
        //    {
        //        return_needed = false;

        //        this.Value = KineticaRT.Values.Values.False;
        //    }
        //    else
        //    {
        //        if (this.sourceChannel != this.destinationChannel)
        //        {
        //            Value = newValue;
        //        }
        //        else
        //        {
        //            this.value = newValue;
        //            krtValue = newValue; //La propriété Value change aussei cette variable mais lorsque la source = la destination on veut pas passer par la propriété Value pour eviter des loop infinies

        //            bool new_state = false;  //ft mmmm ici pas sur encore... pour le state
        //            try
        //            {
        //                new_state = System.Convert.ToBoolean(newValue);
        //            }
        //            catch (System.FormatException)
        //            {
        //            }
        //            SetState(new_state ? ButtonStates.Pressed : ButtonStates.Unpressed);

        //            OnValueChanged();//test pour regler bug de changement couleur du text d'un bouton qui se fait resetter par un autre channel
        //        }
        //    }
        //}


        //new value coming in
        private void source_OnValueChanged(Channels.IChannel sender, KineticaRT.Values.Value newValue)
        {
            //System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Value Change: " + newValue.ToString(), "Controls.Button: " + this.Parent.Name + ", " + this.Name) ;
            //if (return_needed)
            //{
            //     return_needed = false;

            //    //ft override la nouvelle valeur
            //    newValue = KineticaRT.Values.Values.False;
            //}


            if (this.sourceChannel != this.destinationChannel)
            {
                this.Value = newValue;
            }
            else
            {
              

                this.internalValue = newValue;
                //le krtValue est envoyé lorsque dans une sequence vb on a un handler attache au control bouton au non au channel du bouton. et doit etre
                //synchro avec la variable this.value
                krtValue = newValue; //La propriété Value change aussei cette variable mais lorsque la source = la destination on veut pas passer par la propriété Value pour eviter des loop infinies

                bool new_state = false;  //ft mmmm ici pas sur encore... pour le state
                try
                {
                    new_state = System.Convert.ToBoolean(newValue);
                }
                catch (System.FormatException)
                {
                }
                SetState(new_state ? ButtonStates.Pressed : ButtonStates.Unpressed);

                OnValueChanged();//test pour regler bug de changement couleur du text d'un bouton qui se fait resetter par un autre channel

                if ((type == ButtonReturnTypes.AutoReturn) || (type == ButtonReturnTypes.AutoReset) || (type == ButtonReturnTypes.SlowAutoReset) || (type == ButtonReturnTypes.TimeDelayReset))
                {
                    if (newValue == KineticaRT.Values.Values.True)//ft besoin de remettre le channel a false
                    {
                        if (this.captureClick)//si l'usager a encore le doigt sur le bouton de la souris
                        {
                            //this.CaptureClickValueChanged += Button_CaptureClickValueChanged;
                            if (valueCaptureClickHdlr == null)
                            {
                                valueCaptureClickHdlr = new ValueChangedEventHandler(OnCaptureClickValueChanged);
                            }
                            this.CaptureClickValueChanged += valueCaptureClickHdlr;
                        }
                        else
                        {
                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke(new MethodInvoker(start_timer));//ft start le timer pour resetter fait dans un begin Invoke pcq sinon le Tick ne marche pas dans un WorkerThread.
                            }
                            else
                            {
                                start_timer();
                            }
                        }
                    }
                }
            }
        }

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			SetFormat() ;
		}

		/// <summary>
		/// Event generated when the Value changes
		/// </summary>
		public event KineticaRT.Controls.ValueChangedEventHandler ValueChanged;   
		/// <summary>
		/// Raises the ValueChanged event
		/// </summary>
		protected virtual void OnValueChanged()
		{
			if (ValueChanged != null) 
			{
				ValueChanged(this, krtValue);  //Invokes the delegates.
			}
		}
        /// <summary>
        /// Event generated when the Value changes
        /// </summary>
        private event ValueChangedEventHandler CaptureClickValueChanged;
        /// <summary>
        /// Raises the ValueChanged event
        /// </summary>
        private void OnCaptureClickValueChanged(bool value)
        {
            if (value == false) //l'utilisateur a relaché le bouton dans le mode autoreturn
            {
                this.Value = KineticaRT.Values.Values.False;
            }
            this.CaptureClickValueChanged -= valueCaptureClickHdlr;
        }
        delegate void ValueChangedEventHandler(bool Value);




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
            base.Connect() ;
           

            if (source != "")
            {
                SourceChannel = OnObjectSearch(source) as Channels.IChannel;
                if (this.sourceChannel == null && source.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }

            }
            if (inputEnabledSource != "")
            {
                InputEnabledSourceChannel = OnObjectSearch(inputEnabledSource) as Channels.IChannel;
                if (this.inputEnabledSourceChannel == null && this.inputEnabledSource.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }
            if (destination != "")
            {
                DestinationChannel = OnObjectSearch(destination) as Channels.IChannel;
                if (this.destinationChannel == null && destination.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }
            if (timeDelayIntervalSource != "")
            {
                TimeDelayIntervalSourceChannel = OnObjectSearch(timeDelayIntervalSource) as Channels.IChannel;
                if (this.timeDelayIntervalSourceChannel == null && this.timeDelayIntervalSource.Length > 0) //ft
                {
                    this.BackColor = Color.Fuchsia;
                }
            }

            ConnectMouseSource();
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect() ;

            DisconnectMouseSource();

            if (sourceChannel != null) SourceChannel = null;
			if (inputEnabledSourceChannel != null)	InputEnabledSourceChannel = null ;	
			if (destinationChannel != null)		DestinationChannel = null ;
            if (timeDelayIntervalSourceChannel != null) TimeDelayIntervalSourceChannel = null;
        }
		#endregion

        private void keyboard1_KeyDown(object sender, KeyEventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");

        }
	}
}

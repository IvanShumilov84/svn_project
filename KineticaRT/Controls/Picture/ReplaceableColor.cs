using System;
using System.ComponentModel;
using System.Drawing;
using KineticaRT;

namespace KineticaRT.Controls
{
	/// <summary>
	/// A ReplaceableColor is a color that is replaced by a diferent color when the image is drawn
	/// </summary>
	[TypeConverterAttribute (typeof(ExpandableObjectTypeConverter))]
	[DefaultProperty("OriginalColor")]
	public class ReplaceableColor : SubObjectBase
	{
		#region Variables
		private Color				originalColor ;
		private Color				displayColor ;
		private bool				enabled ;
		private Channels.IChannel	enabledSourceChannel ;	//source of 'enabled'
		private string				enabledSource ;

		private Channels.ValueChangedEventHandler		enabledSrcHandler ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the ReplaceableColor class
		/// </summary>
		public ReplaceableColor()
		{
			originalColor		= Color.White ;
			displayColor		= Color.Gray ;
			enabled				= false ;
			enabledSourceChannel= null ;
			enabledSource		= "" ;

			enabledSrcHandler = new Channels.ValueChangedEventHandler(OnEnabledChanged) ;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the Color to be replaced
		/// </summary>
		/// <remarks>Any parts of the image that are OriginalColor will be drawn in DisplayColor.</remarks>
		[Category("Color")]
		[Description("Gets or sets the Color to be replaced")]
		public Color OriginalColor
		{
			get
			{
				return originalColor ;
			}
			set
			{
				if (originalColor != value)
				{
					originalColor = value ;
					OnPropertyChanged("OriginalColor") ;
				}
			}
		}
		private bool ShouldSerializeOriginalColor()
		{
			return originalColor != Color.White ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetOriginalColor()
		{
			originalColor = Color.White ;
		}

		/// <summary>
		/// Gets or sets the replacement color
		/// </summary>
		/// <remarks>Any parts of the image that are OriginalColor will be drawn in DisplayColor.</remarks>
		[Category("Color")]
		[Description("Gets or sets the replacement color")]
		public Color DisplayColor
		{
			get
			{
				return displayColor ;
			}
			set
			{
				if (displayColor != value)
				{
					displayColor = value ;
					OnPropertyChanged("DisplayColor") ;
				}
			}
		}
		private bool ShouldSerializeDisplayColor()
		{
			return displayColor != Color.Gray ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetDisplayColor()
		{
			displayColor = Color.Gray ;
		}
	
		/// <summary>
		/// Gets or sets whether color replacement actually occurs
		/// </summary>
		[Category("Color")]
		[Description("Gets or sets whether color replacement actually occurs")]
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return enabled ;
			}
			set
			{
				 if (enabled != value)
				{
					enabled = value ;
					OnPropertyChanged("Enabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the object that is the Source of the Enabled property
		/// </summary>
		[Category("Color")]
		[Description("Gets or sets the object that is the Source of the Enabled property")]
		public Channels.IChannel EnabledSourceChannel
		{
			get
			{
				return enabledSourceChannel ;
			}
			set
			{
				if (enabledSourceChannel != value)
				{
					//free old handler if any
					if (enabledSourceChannel != null)		
					{
						enabledSourceChannel.ValueChanged -= enabledSrcHandler ;
					}

					//attach new source
					enabledSourceChannel = value ;
					if (enabledSourceChannel != null)
					{
						enabledSourceChannel.ValueChanged += enabledSrcHandler ;
						Enabled = (enabledSourceChannel.Value != null) && (enabledSourceChannel.Value.ToDouble() != 0) ;
					}
					OnPropertyChanged("EnabledSourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeEnabledSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or sets the name of the object to be the EnabledSource
		/// </summary>
		[Category("Color")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the object to be the EnabledSource")]
		public string EnabledSource
		{
			get
			{
				return enabledSource ;
			}
			set
			{
				if (enabledSource != value)
				{
					if (enabledSource == null)enabledSource = "" ;
					else enabledSource = value ;
					OnPropertyChanged("EnabledSource") ;
				}
				//try and connect to source
				EnabledSourceChannel = OnObjectSearch(enabledSource) as Channels.IChannel ;
			}
		}
		#endregion

		#region Value changed handling
		//new value coming in
		private void OnEnabledChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			Enabled = (newValue.ToDouble() != 0) ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (enabledSource != "")		EnabledSourceChannel = OnObjectSearch(enabledSource) as Channels.IChannel ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (enabledSourceChannel != null)			EnabledSourceChannel = null ;
		}
		#endregion
	}

	/// <summary>
	/// A FillColor is a ReplaceableColor wher the replacement is only made over a defined fraction of an image
	/// </summary>
	[TypeConverterAttribute (typeof(ExpandableObjectTypeConverter))]
	[DefaultProperty("Value")]
	public class FillColor : ReplaceableColor
	{
		#region Variables
		private double				value ;
		private Channels.IChannel	sourceChannel ;		//source of value
		private string				source ;
		
		private Channels.ValueChangedEventHandler		srcHandler ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the FillColor class
		/// </summary>
		public FillColor()
		{
			value		= 0 ;
			sourceChannel		= null ;
			source	= "" ;

			srcHandler = new Channels.ValueChangedEventHandler(OnValueChanged) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the Value specifying what over proportion of the image the color is replaced
		/// </summary>
		[Category("Color")]
		[Description("Gets or sets the Value specifying what over proportion of the image the color is replaced")]
		[DefaultValue(0.0)]
		public double Value
		{
			get
			{
				return this.value ;
			}
			set
			{
				if (this.value != value)
				{
					this.value = value ;
					OnPropertyChanged("Value") ;
					//OnValueChanged() ;
				}
			}
		}
 
		/// <summary>
		/// Gets or sets an object that is the source of the Value
		/// </summary>
		[Category("Color")]
		[Description("Gets or sets an object that is the source of the Value")]
		public Channels.IChannel SourceChannel
		{
			get
			{
				return sourceChannel ;
			}
			set
			{
				if (sourceChannel != value)
				{
					//free old handler if any
					if (sourceChannel != null)		
					{
						sourceChannel.ValueChanged -= srcHandler ;
					}

					//attach new source
					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged += srcHandler ;
						Value = sourceChannel.Value.ToDouble() ;
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
		/// Gets or sets the name of the object to be the Source
		/// </summary>
		[Category("Color")]
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
		#endregion

		#region New Value handling (in and out)
		//new value coming in
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			//this.Value = Convert.ToDouble(sender.Value) ;
			Value = newValue.ToDouble() ;
		}

//		public event KineticaRT.Controls.ValueChangedEventHandler ValueChanged;   
//		//new value going out
//		protected virtual void OnValueChanged()
//		{
//			if (ValueChanged != null) 
//			{
//				ValueChanged(this, new KineticaRT.Values.Double(this.value));  //Invokes the delegates.
//			}
//		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
			base.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
			base.Disconnect() ;
		}
		#endregion
	}
}

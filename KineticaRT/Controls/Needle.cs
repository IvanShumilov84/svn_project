using System;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.ComponentModel;
using KineticaRT ;
using System.Diagnostics;
using KineticaRT.Controls;
using System.Windows.Forms;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Enumeration of possible sources of min and max values for the Needle
	/// </summary>
	public enum NeedleRangeSources
	{
		/// <summary>
		/// The Needle's own Minimum and Maximum properties are used
		/// </summary>
		Self,
		/// <summary>
		/// If the Needle's Source is a KineticaRT.Channels.INumericChannel then its
		/// Minimum and Maximum are used, otherwise the Needle's own values are used
		/// </summary>
		Source,
		/// <summary>
		/// The range of the parent Control's Scale is used.
		/// </summary>
		Scale,
	}

	/// <summary>
	/// Provides the basic features of a Needle object, used by controls such as Dials and Sliders
	/// </summary>
	[TypeConverterAttribute(typeof(ExpandableObjectTypeConverter))]
	[DefaultProperty("Value")]
	public class Needle : KineticaRT.Controls.SubObject
	{
		#region Variables

        /// <summary>
        /// GradientEnabled
        /// </summary>
        protected bool gradientEnabled; //ft 
        /// <summary>
        /// GradientMode
        /// </summary>
        protected LinearGradientMode gradientMode;//ft

		/// <summary>
		/// True if needle is being dragged and UpdateOnDrag is false
		/// </summary>
		protected bool					useTempValue ;
		/// <summary>
		/// Value to use for drawing if useTempValue is true
		/// </summary>
		protected double				tempValue ;
		/// <summary>
		/// KineticaRT.Values.Value equivalent of Value property.
		/// Will be a Values.Double or a TypeMismatch
		/// </summary>
		protected Values.Value			krtValue ;

		/// <summary>
		/// Event Handler for Source value changes
		/// </summary>
		protected Channels.ValueChangedEventHandler	srcHdlr ;
		/// <summary>
		/// Event Handler for Source property changes
		/// </summary>
		protected PropertyChangedEventHandler		propertyHdlr ;

		/// <summary>
		/// Current value of SourceChannel property
		/// </summary>
		protected Channels.IChannel		sourceChannel ;
		/// <summary>
		/// Current value of Source property
		/// </summary>
		protected string				source ;
		/// <summary>
		/// Current value of DestinationChannel property
		/// </summary>
		protected Channels.IChannel		destinationChannel ;
		/// <summary>
		/// Current value of Destination property
		/// </summary>
		protected string				destination ;
		/// <summary>
		/// Current value of FillEnabled property
		/// </summary>
		protected bool					fillEnabled ;
		/// <summary>
		/// Current value of FillStyle property
		/// </summary>
		protected FillStyles			fillStyle ;
		/// <summary>
		/// Current value of FillColor property
		/// </summary>
		protected Color					fillColor ;
		/// <summary>
		/// Current value of FillStart property
		/// </summary>
		protected double				fillStart ;
		/// <summary>
		/// Current value of FillFinish property
		/// </summary>
		protected double				fillFinish ;
		/// <summary>
		/// Current value of UpdateOnDrag property
		/// </summary>
		protected bool					updateOnDrag ;
		/// <summary>
		/// Current value of DefaultValue property
		/// </summary>
		protected double				defaultValue ;
		/// <summary>
		/// Current value of AutoReturn property
		/// </summary>
		protected bool					autoReturn ;
		/// <summary>
		/// Current value of Value property
		/// </summary>
		protected object				value ;
		/// <summary>
		/// Current value of Color property
		/// </summary>
		protected Color					color ;
		/// <summary>
		/// Current value of Enabled (=draggable) property
		/// </summary>
		protected bool					enabled ;
		/// <summary>
		/// Current value of Visible property
		/// </summary>
		protected bool					visible ;
		/// <summary>
		/// Current value of Minimum property
		/// </summary>
		protected double				minimum ;
		/// <summary>
		/// Current value of Maximum property
		/// </summary>
		protected double				maximum ;
		/// <summary>
		/// Current value of RangeSource property
		/// </summary>
		protected NeedleRangeSources	rangeSource ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Needle class
		/// </summary>
		public Needle()
		{
			autoReturn		= false ;
			color			= Color.SteelBlue ;
			defaultValue	= 0.0 ;
			destination		= "" ;
			destinationChannel = null ;
			enabled			= true ;
			fillColor		= Color.LightSteelBlue ;
			fillEnabled		= false ;
			fillFinish		= 0.7 ;
			fillStart		= 0.3 ;
			fillStyle		= FillStyles.Solid ;
			minimum			= 0 ;
			maximum			= 100 ;
			rangeSource		= NeedleRangeSources.Scale ;
			source			= "" ;
			sourceChannel	= null ;
			updateOnDrag	= true ;
			value			= 0.0 ;
			visible			= true ;

			useTempValue= false ;
			tempValue	= 0 ;
			krtValue	= Values.Values.Zero ;
			srcHdlr		= new Channels.ValueChangedEventHandler(OnValueChanged) ;
			propertyHdlr= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or Sets the color of the Needle
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the color of the Needle")]
		public Color Color
		{
			get
			{
				return color;
			}
			set
			{
				if (color != value)
				{
					color = value;
					OnPropertyChanged("Color") ;
				}
			}
		}
		private bool ShouldSerializeColor()
		{
			return color != Color.SteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetColor()
		{
			color = Color.SteelBlue ;
		}

		/// <summary>
		/// Gets or Sets the color of the Fill
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the color of the Fill")]
		public Color FillColor
		{
			get
			{
				return fillColor;
			}
			set
			{
				if (fillColor != value)
				{
					fillColor = value;
					OnPropertyChanged("FillColor") ;
				}
			}
		}
		private bool ShouldSerializeFillColor()
		{
			return fillColor != Color.LightSteelBlue ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetFillColor()
		{
			fillColor = Color.LightSteelBlue ;
		}

        /// <summary>
		/// Gets or Sets the current Value
		/// </summary>
		[Category("Needle")]
		[Description("The current value")]
//		[DefaultValue(0.0)]
		public virtual object Value
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
					if (new_krt_value == null)
					{
						try
						{
							new_krt_value = new KineticaRT.Values.Double (System.Convert.ToDouble(value)) ;
						}
						catch (System.Exception)
						{
						}
					}
					if (new_krt_value == null) new_krt_value = Values.Values.TypeMismatch ;

					if (new_krt_value.CompareTo (krtValue) != 0)
					{
						krtValue = new_krt_value ;

						//set destination
						if (destinationChannel != null)	destinationChannel.Value = krtValue ;
						
//						OnValueChanged() ;
					}
					OnPropertyChanged("Value") ;
				}
			}
		}
		private bool ShouldSerializeValue()
		{
			return false ;
		}

		/// <summary>
		/// Gets or Sets the Channel that is the Source of the Needle's value
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Channel that is the Source of the Needle's value")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
                        Value = KineticaRT.Values.Values.NotConnected;
                    }

					lock (this)
					{
						//attach new source
						sourceChannel = value;
						if (sourceChannel != null)
						{
							sourceChannel.ValueChanged		+= srcHdlr ;
							sourceChannel.PropertyChanged	+= propertyHdlr ;
							Value = sourceChannel.Value ;
						}
						OnPropertyChanged("SourceChannel") ;
					}
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or Sets the Channel that is the Destination for the Needle's value
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Channel that is the destination for the Needle's value")]
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

					if (destinationChannel != null)	destinationChannel.Value = krtValue ;

					OnPropertyChanged("DestinationChannel") ;
				}
			}
		}
		private bool ShouldSerializeDestinationChannel()
		{
			return false ;
		}

		/// <summary>
		/// Gets or Sets the name of the Channel to be the Source
		/// </summary>
		[Category("Needle")]
		[DefaultValue("")]
		[Description("Gets or Sets the name of the Channel to be the Source")]
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
		/// Gets or Sets the name of the Channel to be the Destination
		/// </summary>
		[Category("Needle")]
		[DefaultValue("")]
		[Description("Gets or Sets the name of the Channel to be the Destination")]
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
		/// Gets or Sets whether the Needle is Visible
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets whether the Needle is Visible")]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return visible;
			}
			set
			{
				if (visible != value)
				{
					visible = value;
					OnPropertyChanged("Visible") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets whether the Needle is Enabled (draggable)
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets whether the Needle is Enabled (draggable)")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return enabled;
			}
			set
			{
				if (enabled != value)
				{
					enabled = value;
					OnPropertyChanged("Enabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Fill Style associated with the Needle
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Fill Style associated with the needle (solid, segmented or permanent)")]
		[DefaultValue(FillStyles.Solid)]
		public virtual FillStyles FillStyle
		{
			get
			{
				return fillStyle ;
			}
			set
			{
				if (fillStyle != value)
				{
					fillStyle = value;
					OnPropertyChanged("FillStyle") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets whether Fill is Enabled
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets whether Fill is Enabled")]
		[DefaultValue(false)]
		public bool FillEnabled
		{
			get
			{
				return fillEnabled ;
			}
			set
			{
				if (fillEnabled  != value)
				{
					fillEnabled  = value;
					OnPropertyChanged("FillEnabled") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Fill Start position as a proportion of the size of the control (0..1)
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Fill Start position as a proportion of the size of the control (0..1)")]
		[DefaultValue(0.3)]
		public double FillStart
		{
			get
			{
				return fillStart;
			}
			set
			{
				if (fillStart != value)
				{
					fillStart = value;
					OnPropertyChanged("FillStart") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Fill Finish position as a proportion of the size of the control (0..1)
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Fill Finish position as a proportion of the size of the control (0..1)")]
		[DefaultValue(0.7)]
		public double FillFinish
		{
			get
			{
				return fillFinish ;
			}
			set
			{
				if (fillFinish != value)
				{
					fillFinish = value;
					OnPropertyChanged("FillFinish") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets whether the Value updates as you drag, or only when released
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets whether the Value updates as you drag, or only when released")]
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
		/// Gets or Sets whether the Needle reverts to DefaultValue after dragging
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets whether the Needle reverts to DefaultValue after dragging")]
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
		/// Gets or Sets the Default Value of the Needle
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Default Value of the Needle")]
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
					//set value to new default
					Value = defaultValue ;

					OnPropertyChanged("DefaultValue") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the source of the minimum and maximum values displayed by the Needle
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the source of the minimum and maximum values displayed by the Needle")]
		[DefaultValue(NeedleRangeSources.Scale)]
		public NeedleRangeSources RangeSource
		{
			get
			{
				return rangeSource ;
			}
			set
			{
				if (rangeSource != value)
				{
					rangeSource = value;
					OnPropertyChanged("RangeSource") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Minimum value displayed by the Needle if RangeSource is NeedleRangeSources.Self
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Minimum value displayed by the Needle if RangeSource is NeedleRangeSources.Self")]
		[DefaultValue(0.0)]
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
					minimum = value;
					OnPropertyChanged("Minimum") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Maximum value displayed by the Needle if RangeSource is NeedleRangeSources.Self
		/// </summary>
		[Category("Needle")]
		[Description("Gets or Sets the Maximum value displayed by the Needle if RangeSource is NeedleRangeSources.Self")]
		[DefaultValue(100.0)]
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
					maximum = value;
					OnPropertyChanged("Maximum") ;
				}
			}
		}
		#endregion

		#region Other Methods
		/// <summary>
		/// Calculates a dark and a light version of the NeedleColor
		/// </summary>
		/// <param name="top_clr">(out) Light version of color</param>
		/// <param name="bottom_clr">(out) Dark version of color</param>
		protected void CalculateSoftShading(out Color top_clr, out Color bottom_clr)
		{
			bottom_clr = ControlPaint.Dark(color, (float)0.15) ;
			top_clr = ControlPaint.Light(color, (float)0.8) ;
		}
	
		/// <summary>
		/// Sets the Value of the needle based on a drag.
		/// If UpdateOnDrag is false, tempValue is updated not the actual Value property
		/// </summary>
		/// <param name="newFraction">Fraction representing value to be set</param>
		/// <param name="Release">True if this represents the end of a drag</param>
		/// <param name="Scale">Scale object to use to convert fraction to value, if RangeSource is NeedleRangeSources.Scale</param>
		public void SetValueFromInput(double newFraction, bool Release, Scale Scale)
		{
			double		new_value = FractionToValue(newFraction, Scale) ;

			if (Release)
			{
				useTempValue = false ;
			}

			if (updateOnDrag || (!updateOnDrag && Release) )
			{
				//set value
				Value = new_value ;
			}
			else
			{
				tempValue = new_value ;
				useTempValue = true ;

				//force a redraw
				this.OnPropertyChanged("TempValue") ;
			}
		}


		/// <summary>
		/// Converts the given value to a fraction based on the current RangeSource
		/// </summary>
		/// <param name="Value">Value to convert</param>
		/// <param name="objScale">Scale object to be used if with NeedleRangeSources.Scale</param>
		/// <returns>Fraction corresponding to given Value</returns>
		protected double ValueToFraction(double Value, Scale objScale)
		{
			if (rangeSource == NeedleRangeSources.Scale)
			{
				return (objScale != null) ? objScale.ValueToFraction(Value) : 0 ;
			}

			if (rangeSource == NeedleRangeSources.Source &&
				sourceChannel != null					 &&
				sourceChannel is Channels.INumericChannel		)
			{
				Channels.INumericChannel		num		= sourceChannel as Channels.INumericChannel ;

				if (num.Maximum != num.Minimum)
				{
					return (Value - num.Minimum) / (num.Maximum - num.Minimum) ;
				}
			}
			else
			{
				//default to 'Self'
				if (maximum != minimum)
				{
					return (Value - minimum) / (maximum - minimum) ;
				}
			}

			//return 0 if min == max (ie no range)
			return 0 ;
		}

		/// <summary>
		/// Converts the given fraction to a value based on the current RangeSource
		/// </summary>
		/// <param name="Fraction">Fraction to convert</param>
		/// <param name="objScale">Scale object to be used if with NeedleRangeSources.Scale</param>
		/// <returns>Value corresponding to given Fraction</returns>
		protected double FractionToValue(double Fraction, Scale objScale)
		{
            if (objScale == null) return 0.0;

			if (rangeSource == NeedleRangeSources.Scale)
			{
				return objScale.FractionToValue(Fraction) ;
			}

			if (rangeSource == NeedleRangeSources.Source &&
				sourceChannel != null					 &&
				sourceChannel is Channels.INumericChannel		)
			{
				Channels.INumericChannel		num		= sourceChannel as Channels.INumericChannel ;

				return num.Minimum + Fraction * (num.Maximum - num.Minimum) ;
			}
			else
			{
				//default to 'Self'
				return minimum + Fraction * (maximum - minimum) ;
			}
		}
		#endregion

		#region Source Change handling (value and properties)
		//new value coming in
		private void OnValueChanged (Channels.IChannel sender, KineticaRT.Values.Value newValue)
		{
			Value = newValue ;
		}

		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
				//force a reformatting if required: same as if source channel changed
				OnPropertyChanged("SourceChannel") ;
			}
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects the Needle to its Source and Destination
		/// </summary>
		public override void Connect ()
		{
			if (source != "")		SourceChannel = OnObjectSearch(source) as Channels.IChannel ;
			if (destination != "")	DestinationChannel = OnObjectSearch(destination) as Channels.IChannel ;
		}

		/// <summary>
		/// Disconnects the Needle from its Source and Destination
		/// </summary>
		public override void Disconnect ()
		{
			if (sourceChannel != null)			SourceChannel = null ;
			if (destinationChannel != null)		DestinationChannel = null ;
		}
		#endregion
    
    }
}

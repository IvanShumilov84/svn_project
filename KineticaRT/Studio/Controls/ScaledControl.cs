using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KineticaRT;

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// Enumeration of possible scale positions
	/// </summary>
	public enum ScalePositions
	{
		/// <summary>
		/// On the left of the control
		/// </summary>
		Left,
		/// <summary>
		/// On the right of the control
		/// </summary>
		Right,
		/// <summary>
		/// Scale is not shown
		/// </summary>
		Hidden,
	}

	/// <summary>
	/// Base class for KineticaRT.Studio controls containing a scale
	/// </summary>
	public class ScaledControl : NumericControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private	bool					useCustomRange ;
		private double					customMinimum ;
		private double					customMaximum ;
		private	bool					useCustomDecimalPlaces ;
		private int						customDecimalPlaces ;
		/// <summary>
		/// The threshold channel that this control is connected to or null if not connected to a threshold channel.
		/// </summary>
		protected KineticaRT.Channels.INumericThresholdChannel thresholdChannel = null ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the ScaledControl class
		/// </summary>
		public ScaledControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			useCustomRange			= false ;
			customMinimum			= 0 ;
			customMaximum			= 100 ;
			useCustomDecimalPlaces	= false ;
			customDecimalPlaces		= 0 ;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			// ScaledControl
			// 
			this.Name = "ScaledControl";

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the Source channel to which this object is connected
		/// </summary>
		[Category("General")]
		[Description("Gets or sets the Source channel to which this object is connected")]
		[DefaultValue(null)]
		public override Channels.IInfoChannel SourceChannel
		{
			set
			{
				base.SourceChannel = value ;
				NewRange(Minimum, Maximum) ;
				NewDPs(DecimalPlaces) ;

				thresholdChannel = SourceChannel as KineticaRT.Channels.INumericThresholdChannel ;
				if (thresholdChannel != null)
				{
					SetFill (thresholdChannel.Normal, thresholdChannel.NormalColor) ;
					ValueScale.Thresholds[0].Enabled = true ;
					ValueScale.Thresholds[0].Value = thresholdChannel.LowLow ;
					ValueScale.Thresholds[0].MarkColor = thresholdChannel.LowLowColor ;
					ValueScale.Thresholds[1].Enabled = true ;
					ValueScale.Thresholds[1].Value = thresholdChannel.Low ;
					ValueScale.Thresholds[1].MarkColor = thresholdChannel.LowColor ;
					ValueScale.Thresholds[2].Enabled = true ;
					ValueScale.Thresholds[2].Value = thresholdChannel.High ;
					ValueScale.Thresholds[2].MarkColor = thresholdChannel.HighColor ;
					ValueScale.Thresholds[3].Enabled = true ;
					ValueScale.Thresholds[3].Value = thresholdChannel.HighHigh ;
					ValueScale.Thresholds[3].MarkColor = thresholdChannel.HighHighColor ;
				}
				else
				{
					SetFill (0, System.Drawing.Color.Empty) ;
					ValueScale.Thresholds[0].Enabled = false ;
					ValueScale.Thresholds[1].Enabled = false ;
					ValueScale.Thresholds[2].Enabled = false ;
					ValueScale.Thresholds[3].Enabled = false ;
				}

				Invalidate(DisplayRectangle) ;
			}
		}

		/// <summary>
		/// Gets or sets whether to use the custom minimum and maximum, or those of the source channel
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets whether to use the custom minimum and maximum, or those of the source channel")]
		[DefaultValue(false)]
		public bool UseCustomRange
		{
			get
			{
				return useCustomRange ;
			}
			set
			{
				if (value != useCustomRange)
				{
					useCustomRange = value ;
					OnPropertyChanged("UseCustomRange") ;
					OnPropertyChanged("Minimum") ;
					OnPropertyChanged("Maximum") ;
					NewRange(Minimum, Maximum) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the custom Minimum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the custom Minimum value of the scale")]
		[DefaultValue(0.0)]
		public double CustomMinimum
		{
			get
			{
				return customMinimum ;
			}
			set
			{
				if (value != customMinimum)
				{
					customMinimum = value ;
					OnPropertyChanged("CustomMinimum") ;
					
					if (useCustomRange)
					{
						OnPropertyChanged("Minimum") ;
						NewRange(Minimum, Maximum) ;
					}
				}
			}
		}

		/// <summary>
		/// Gets the Minimum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets the Minimum value of the scale")]
		public double Minimum
		{
			get
			{
				double			min		= 0 ;

				if (useCustomRange)
				{
					min = customMinimum ;
				}
				else if (SourceChannel is Channels.INumericChannel)
				{
					min = ((Channels.INumericChannel)SourceChannel).Minimum ;
				}
				return min ;
			}
		}

		/// <summary>
		/// Gets or sets the custom Maximum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the custom Maximum value of the scale")]
		[DefaultValue(100.0)]
		public double CustomMaximum
		{
			get
			{
				return customMaximum ;
			}
			set
			{
				if (value != customMaximum)
				{
					customMaximum = value ;
					OnPropertyChanged("CustomMaximum") ;
					
					if (useCustomRange)
					{
						OnPropertyChanged("Maximum") ;
						NewRange(Minimum, Maximum) ;
					}
				}
			}
		}

		/// <summary>
		/// Gets the Maximum value of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets the Maximum value of the scale")]
		public double Maximum
		{
			get
			{
				double			max		= 100 ;

				if (useCustomRange)
				{
					max = customMaximum ;
				}
				else if (SourceChannel is Channels.INumericChannel)
				{
					max = ((Channels.INumericChannel)SourceChannel).Maximum ;
				}
				return max ;
			}
		}

		/// <summary>
		/// Gets or sets whether to use custom decimal places, or that of the source channel
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets whether to use custom decimal places, or that of the source channel")]
		[DefaultValue(false)]
		public bool UseCustomDecimalPlaces
		{
			get
			{
				return useCustomDecimalPlaces ;
			}
			set
			{
				if (value != useCustomDecimalPlaces)
				{
					useCustomDecimalPlaces = value ;
					OnPropertyChanged("UseCustomDecimalPlaces") ;
					OnPropertyChanged("DecimalPlaces") ;
					NewDPs(DecimalPlaces) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the custom Decimal Places of the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets or sets the custom Decimal Places of the scale")]
		[DefaultValue(0)]
		public int CustomDecimalPlaces
		{
			get
			{
				return customDecimalPlaces ;
			}
			set
			{
				if (value != customDecimalPlaces)
				{
					customDecimalPlaces = value ;
					OnPropertyChanged("CustomDecimalPlaces") ;
					
					if (useCustomDecimalPlaces)
					{
						OnPropertyChanged("DecimalPlaces") ;
						NewDPs(DecimalPlaces) ;
					}
				}
			}
		}

		/// <summary>
		/// Gets the number of Decimal Places displayed by the scale
		/// </summary>
		[Category("Scale")]
		[Description("Gets the number of Decimal Places displayed by the scale")]
		public int DecimalPlaces
		{
			get
			{
				int			dps		= 0 ;

				if (useCustomDecimalPlaces)
				{
					dps = customDecimalPlaces ;
				}
				else if (SourceChannel is Channels.INumericChannel)
				{
					dps = ((Channels.INumericChannel)SourceChannel).DecimalDigits ;
				}
				return dps ;
			}
		}
		#endregion

		#region Property Changed handling (in and out)
		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnSourcePropertyChanged(object Sender, PropertyChangedEventArgs e)
		{
			if (e == KineticaRT.Channels.Channel.ValuePropertyChangedEventArgs) return ;
			if (e == KineticaRT.Channels.Channel.ColorPropertyChangedEventArgs) return ;

			if (!useCustomRange && (e.PropertyName == "Minimum" || e.PropertyName == "Maximum"))
			{
				NewRange(Minimum, Maximum) ;
			}
			else if (!useCustomDecimalPlaces && e.PropertyName == "DecimalDigits")
			{
				NewDPs(DecimalPlaces) ;
			}
			else if (e == KineticaRT.Channels.Channel.LowLowPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[0].Value = thresholdChannel.LowLow ;
			}
			else if (e == KineticaRT.Channels.Channel.LowPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[1].Value = thresholdChannel.Low ;
			}
			else if (e == KineticaRT.Channels.Channel.NormalPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) SetFill (thresholdChannel.Normal, thresholdChannel.NormalColor) ;
			}
			else if (e == KineticaRT.Channels.Channel.HighPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[2].Value = thresholdChannel.High ;
			}			
			else if (e == KineticaRT.Channels.Channel.HighHighPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[3].Value = thresholdChannel.HighHigh ;
			}
			else if (e == KineticaRT.Channels.Channel.LowLowColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[0].MarkColor = thresholdChannel.LowLowColor ;
			}
			else if (e == KineticaRT.Channels.Channel.LowColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[1].MarkColor = thresholdChannel.LowColor ;
			}
			else if (e == KineticaRT.Channels.Channel.HighColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[2].MarkColor = thresholdChannel.HighColor ;
			}
			else if (e == KineticaRT.Channels.Channel.HighHighColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) ValueScale.Thresholds[3].MarkColor = thresholdChannel.HighHighColor ;
			}
			else if (e == KineticaRT.Channels.Channel.NormalColorPropertyChangedEventArgs)
			{
				if (thresholdChannel != null) SetFill (thresholdChannel.Normal, thresholdChannel.NormalColor) ;
			}
			else
			{
				base.OnSourcePropertyChanged(Sender, e) ;
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Override this to set the range of scaled objects
		/// </summary>
		/// <param name="Min">New minimum value</param>
		/// <param name="Max">New Maximum value</param>
		protected virtual void NewRange(double Min, double Max)
		{
		}

		/// <summary>
		/// Override this to set the decimal places of scaled objects
		/// </summary>
		/// <param name="DPs">New decimal places</param>
		protected virtual void NewDPs(int DPs)
		{
		}

		/// <summary>
		/// Gets the scale for the Scaled Control.
		/// </summary>
		protected virtual KineticaRT.Controls.Scale  ValueScale
		{
			get {return null;}
		}
		/// <summary>
		/// Sets the fill color.
		/// </summary>
		/// <param name="color">The new color</param>
		/// <param name="centrePoint">The new center point about which the fill is drawn.</param>
		protected virtual void SetFill (double centrePoint, System.Drawing.Color color)
		{
		}
		#endregion
	}
}

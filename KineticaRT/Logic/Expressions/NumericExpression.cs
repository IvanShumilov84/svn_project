using System;
using System.Runtime.Serialization ;
using KineticaRT;
using System.ComponentModel ;
using System.Diagnostics ;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel;
using KineticaRT.Channels;

namespace KineticaRT.Logic.Expressions
{
	/// <summary>
	/// KineticaRT Channel that evaluates expressions containing other Channels to produce a numeric result.
	/// </summary>
	/// <remarks>
	/// The difference between this and the Expression class is that this class has some extra properties such as Minimum, Maximum and Units.  The logic is the same.
	/// Expressions can include other channels such as expressions and driver channels. The value of the expression will be re-evaluated as and when the channels change.
	/// <p>Unlike other real-time systems which continually evaluate all expressions, a KineticaRT expression object will only use processor time
	/// when the inputs to the expression actually change and even then will only re-evaluate the parts of the expression which need re-evaluation.
	/// <br/>This makes it especially suitable for use with drivers with different update rates and digital inputs which rarely change.
	/// </p>
	/// <p>The expression object supports standard mathamatical operators and functions plus a selection of time based functions such as the RollingSD function.</p>
	/// <p>For a list of operators and functions supported, see <see cref="Expression.Text"/></p>
	/// </remarks>
	[ChangeRecord ("1.1", "13 03 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Description("Numeric Expression")]
	[Serializable]
    public class NumericExpression : Expression, ISerializable, INumericChannel, INumericExpressionChannel, IInfoChannel 
	{
		string	units = "" ;
		double	min = 0 ;
		double	max = 100 ;
		string	format = "G" ;
		int		decimalDigits = 2 ;
		System.Globalization.NumberFormatInfo info ;

		/// <summary>Initializes a new instance of the class.</summary>
		public NumericExpression()
		{
			info = (System.Globalization.NumberFormatInfo) System.Globalization.NumberFormatInfo.CurrentInfo.Clone () ;
		}

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData (info, context) ;
		}
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("Units", units) ;
			info.AddValue ("Min", min) ;
			info.AddValue ("Max", max) ;
			info.AddValue ("Format", format) ;
			info.AddValue ("DecimalDigits", decimalDigits) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected NumericExpression (SerializationInfo info, StreamingContext context):base(info, context)
		{
			this.info = (System.Globalization.NumberFormatInfo) System.Globalization.NumberFormatInfo.CurrentInfo.Clone () ;

			try		{ units	= (string)info.GetValue("Units", typeof(string)); }
			catch	{ }

			try		{ min = (double)info.GetValue("Min", typeof(double)); }
			catch	{ }

			try		{ max = (double)info.GetValue("Max", typeof(double)); }
			catch	{ }

			try		{ format = (string)info.GetValue("Format", typeof(string)); }
			catch	{ }

			try		{ decimalDigits = (int)info.GetValue("DecimalDigits", typeof(int)); }
			catch	{ }
		}
		#endregion

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString	()
		{
			try
			{
				return ((IFormattable)this.RawValue).ToString(format, info) ;
			}
			catch (System.FormatException e)
			{
				return e.Message ; 
			}
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the units string for a channel.
		/// </summary>
		/// <value>A string representing the units of the channel.</value>
		/// <remarks>This property is normally used to put a units label on display controls such as the Dial.</remarks>
		/// <example>"Km"</example>
		[System.ComponentModel.Description("Units of channel.  For example Kg or m²."),System.ComponentModel.Category("Numeric")] 
		[DefaultValue("")]
		public string Units
		{
			get
			{
				return units ;
			}
			set
			{
				if (units != value)
				{
					units = value ;
					OnPropertyChanged("Units") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the number of decimal digits to be displayed if displayed in number format or fixed point format.
		/// </summary>
		/// <value>The number of decimal digits to be displayed.</value>
		[System.ComponentModel.Description("Number of digits to display after the decimal point in fixed point or number formats."), System.ComponentModel.Category("Numeric")] 
		[DefaultValue(2)]
		public int DecimalDigits
		{
			get
			{
				return decimalDigits ;
			}
			set
			{
				if (decimalDigits != value)
				{
					decimalDigits = value ;
					info.NumberDecimalDigits = decimalDigits ;
					OnPropertyChanged("DecimalDigits") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the minimum value of channel.
		/// </summary>
		/// <value>The minimum value of the channel.</value>
		/// <remarks>
		/// This value is normally used for setting the minimum value on a display scale.
		/// <para>No limit checking is performed so the actual Value may be lower.</para>
		/// </remarks>
		[System.ComponentModel.Description("Minimum value of channel.  This is used to set the scales of display components such as the dial.  It will not limit the values accepted by this Channel."), System.ComponentModel.Category("Numeric")] 
		[DefaultValue(0)]
		public double Minimum
		{
			get
			{
				return min ;
			}
			set
			{
				if (min != value)
				{
					min = value ;
					OnPropertyChanged("Minimum") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the maximum value of channel.
		/// </summary>
		/// <value>The maximum balue of the channel.</value>
		/// <remarks>
		/// This value is normally used for setting the maximum value on a display scale.
		/// <para>No limit checking is performed so the actual Value may be higher.</para>
		/// </remarks>
		[System.ComponentModel.Description("Maximum value of channel.  This is used to set the scales of display components such as the dial.  It will not limit the values accepted by this Channel."), System.ComponentModel.Category("Numeric")] 
		[DefaultValue(100)]
		public double Maximum
		{
			get
			{
				return max ;
			}
			set
			{
				if (max != value)
				{
					max = value ;
					OnPropertyChanged("Maximum") ;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the format of the channel when viewed as text.
		/// </summary>
		/// <value>A string describing the display format of the channel.</value>
		/// <remarks>The following formats can be used:
		/// <ul>
		/// <li>"C" - Currency</li>
		/// <li>"D" - Decimal</li>
		/// <li>"E" - Exponential</li>
		/// <li>"F" - Fixed Point</li>
		/// <li>"G" - General</li>
		/// <li>"N" - Number</li>
		/// <li>"P" - Percentage</li>
		/// <li>"X" - Hexadecimal</li>
		/// <li>"0.00" - Zero padding</li>
		/// <li>"#.##" - Blank padding</li>
		/// </ul>
		/// </remarks>
		[System.ComponentModel.Description("Format of channel.  This is used to convert the value to text. Eg. C,E,F,G,N"), System.ComponentModel.Category("Numeric")] 
		[DefaultValue("G")]
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
		#endregion
	}
}

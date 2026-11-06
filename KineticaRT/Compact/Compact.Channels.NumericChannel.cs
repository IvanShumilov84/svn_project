using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;

namespace KineticaRT.Compact.Channels
{
	/// <summary>
	/// Base class for KineticaRT Channel objects containing numeric values.
	/// </summary>
	[ChangeRecord ("1.1", "13 03 2003", "N. Savidge", "Added serialization of decimalDigits")] 
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]

    public class NumericChannel : InfoChannel, INumericChannel, ISerializable, IExchangeChannel
	{
		string	units = "" ;
		double	min = 0 ;
		double	max = 100 ;
		string	format = "G" ;
		int		decimalDigits = 2 ;
		System.Globalization.NumberFormatInfo info ;

		/// <summary>Initializes a new instance of the class.</summary>
		public NumericChannel()
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
		public NumericChannel (SerializationInfo info, StreamingContext context):base(info, context)
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

        #region IExchangeChannel
        private void AddProperty(System.Text.StringBuilder sb, string name, string value)
        {
            sb.Append("<");
            sb.Append(name);
            sb.Append(">");
            sb.Append(value);
            sb.Append("</");
            sb.Append(name);
            sb.Append(">");
        }

        private string GetPropertyValue(string name, System.Xml.XmlNode node)
        {
            System.Xml.XmlNode child_node = node.SelectSingleNode(name);
            if (child_node != null)
            {
                return child_node.InnerText;
            }
            return null;
        }

        void IExchangeChannel.GetProperties(System.Text.StringBuilder sb)
        {
            AddProperty(sb, "Units", units.ToString());
            AddProperty(sb, "Minimum", min.ToString());
            AddProperty(sb, "Maximum", max.ToString());
            AddProperty(sb, "Format", format.ToString());
            AddProperty(sb, "DecimalDigits", decimalDigits.ToString());
        }

        void IExchangeChannel.SetProperties(System.Xml.XmlNode propertyNode)
        {
            units = GetPropertyValue("Units", propertyNode);
            Minimum = System.Convert.ToDouble(GetPropertyValue("Minimum", propertyNode));
            Maximum = System.Convert.ToDouble(GetPropertyValue("Maximum", propertyNode));
            format = GetPropertyValue("Format", propertyNode);
            decimalDigits = System.Convert.ToInt32(GetPropertyValue("DecimalDigits", propertyNode));
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
				return ((IFormattable)Value).ToString(format, info) ;
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

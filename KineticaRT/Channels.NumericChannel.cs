using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Channels
{
	/// <summary>
	/// Base class for KineticaRT Channel objects containing numeric values.
	/// </summary>
	[ChangeRecord ("1.1", "13 03 2003", "N. Savidge", "Added serialization of decimalDigits")] 
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
    public class NumericChannel : InfoChannel, INumericChannel, ISerializable, ILoggerChannel, ILoggerChannelSource, IExchangeChannel 
	{
		string	units = "" ;
        double min;
		double max = 100 ;
		string	format = "N" ;
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
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.GetObjectData (info, context) ;
			info.AddValue ("Units", units) ;
			info.AddValue ("Min", min) ;
			info.AddValue ("Max", max) ;
			info.AddValue ("Format", format) ;
			info.AddValue ("DecimalDigits", decimalDigits) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected NumericChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			this.info = (System.Globalization.NumberFormatInfo) System.Globalization.NumberFormatInfo.CurrentInfo.Clone () ;

			try		
			{
				units			= info.GetString ("Units"); 
				min				= KineticaRT.Convert.StringToDouble (info.GetString ("Min"));
                max             = KineticaRT.Convert.StringToDouble(info.GetString("Max")); 
				format			= info.GetString ("Format"); 
				decimalDigits	= info.GetInt32 ("DecimalDigits"); 
			}
			catch (System.Runtime.Serialization.SerializationException e)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, e.Message, this.GetType().ToString()) ;
			}

            this.info.NumberDecimalDigits = decimalDigits;
		}
		#endregion

        #region IExchangeChannel
        private static void AddProperty(System.Xml.XmlWriter xmlWriter, string name, string value)
        {
            xmlWriter.WriteElementString(name, value);
            //sb.Append("<");
            //sb.Append(name);
            //sb.Append(">");
            //sb.Append(value);
            //sb.Append("</");
            //sb.Append(name);
            //sb.Append(">");
        }

        private static string GetPropertyValue(string name, System.Xml.XmlNode node)
        {
            System.Xml.XmlNode child_node = node.SelectSingleNode(name);
            if (child_node != null)
            {
                return child_node.InnerText;
            }
            return null;
        }
        private string [] ExchangePropertyList = 
            {
                "Units",
                "Minimum",
                "Maximum",
                "Format",
                "DecimalDigits"
            } ;

        /// <summary>
        /// Returns true if the named property is one transmitted over the Exchange connection.
        /// </summary>
        /// <param name="name">Name of the property to check.</param>
        /// <returns></returns>
        public override bool IsExchangeProperty(string name)
        {
            if (base.IsExchangeProperty(name)) return true;

            foreach (string property in ExchangePropertyList)
            {
                if (property == name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the channels live properties to the specified StringBuilder in XML format.
        /// </summary>
        /// <param name="xmlWriter">The StringBuilder to add the properties to.</param>
        public override void GetProperties(System.Xml.XmlWriter xmlWriter)
        {
            if (xmlWriter == null) throw new ArgumentNullException("xmlWriter");
            base.GetProperties(xmlWriter);
            
            xmlWriter.WriteElementString("Units", units.ToString());
            xmlWriter.WriteElementString("Minimum", min.ToString());
            xmlWriter.WriteElementString("Maximum", max.ToString());
            xmlWriter.WriteElementString("Format", format.ToString());
            xmlWriter.WriteElementString("DecimalDigits", decimalDigits.ToString());

            //AddProperty(sb, "Units", units.ToString());
            //AddProperty(sb, "Minimum", min.ToString());
            //AddProperty(sb, "Maximum", max.ToString());
            //AddProperty(sb, "Format", format.ToString());
            //AddProperty(sb, "DecimalDigits", decimalDigits.ToString());
        }

        /// <summary>
        /// Sets the channels live properties to the values defined in the specified XmlNode.
        /// </summary>
        /// <param name="propertyNode">XmlNode containing the property values.</param>
        public override void SetProperties(System.Xml.XmlNode propertyNode)
        {
            if (propertyNode == null) throw new ArgumentNullException("propertyNode");
            base.SetProperties(propertyNode);

            units = GetPropertyValue("Units", propertyNode);
            min = System.Convert.ToDouble(GetPropertyValue("Minimum", propertyNode));
            max = System.Convert.ToDouble(GetPropertyValue("Maximum", propertyNode));
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
			if (Value == null) return "null" ;

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
					OnPropertyChanged(UnitsPropertyChangedEventArgs) ;
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
		public virtual double Minimum
		{
			get
			{
				return min ;
			}
			set
			{
				min = value ;
				OnPropertyChanged("Minimum") ;
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
        public virtual double Maximum
		{
			get
			{
				return max ;
			}
			set
			{
				max = value ;
				OnPropertyChanged("Maximum") ;
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
		/// <li>"O" - Octal</li>
		/// <li>"0.00" - Zero padding</li>
		/// <li>"#.##" - Blank padding</li>
		/// </ul>
		/// </remarks>
		[System.ComponentModel.Description("Format of channel.  This is used to convert the value to text. Eg. C - Currency, E - Exponential, F - Fixed Point, G - General, N - Number, P - Percentage, X - Hexadecimal, O - Octal, 0.00 - Zero padding, #.## - Blank padding"), System.ComponentModel.Category("Numeric")] 
		[DefaultValue("N")]
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

        /// <summary>
        /// Provide a mechanisim to control formatting.
        /// </summary>
        /// <value>Object control formatting.</value>
        [System.ComponentModel.Description("Info of channel"), System.ComponentModel.Category("Numeric")]
        public System.Globalization.NumberFormatInfo Info
        {
            get
            {
                return info;
            }
        }


		#endregion


    }
}

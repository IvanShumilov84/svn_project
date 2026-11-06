using System;

namespace KineticaRT.Compact.Values
{
	/// <summary>
	/// KineticaRT Value containing an array of double precision IEEE floating point values.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[System.ComponentModel.ImmutableObject(true)] 
	[System.ComponentModel.TypeConverter(typeof(DoubleArray.TypeConverter))]
	[Serializable]
	public class DoubleArray:Value, IConvertible, IFormattable
	{
		#region TypeConverter
		/// <summary>
		/// TypeConverter for KineticaRT.DoubleArray type.
		/// </summary>
		public class TypeConverter : KineticaRT.TypeConverter 
		{
			/// <summary>
			/// Overrides the CanConvertFrom method of TypeConverter.
			/// </summary>
			public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType) 
			{
				return (sourceType == typeof(string)) ;
			}

			/// <summary>
			/// Overrides the ConvertFrom method of TypeConverter.
			/// </summary>
			public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) 
			{
				if (value is string) 
				{
					return new DoubleArray ((string)value);
				}
				return null ;
			}

			/// <summary>
			/// Overrides the ConvertTo method of TypeConverter.
			/// </summary>
			public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) 
			{  
				if (destinationType == typeof(string)) 
				{
					return ((DoubleArray)value).ToString() ;
				}
				return "Type Mismatch" ;
			}
		}
		#endregion

		private double [] val ;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new class.</param>
		public DoubleArray (double [] value)
		{
			val = value ;
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new instance.</param>
		public DoubleArray (string value) 
		{
			val = new double [1] ;
//CF		if (System.Double.TryParse(value,System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo,out val [0]) == false)
			{
				val [0] = System.Double.NaN ;
			}
		}

		#region IConvertible
		/// <summary>Implements the IConvertible Interface.</summary>
		public bool		ToBoolean	(System.IFormatProvider provider){return System.Convert.ToBoolean	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public byte		ToByte		(System.IFormatProvider provider){return 0 ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public char		ToChar		(System.IFormatProvider provider){return System.Convert.ToChar		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public System.DateTime	ToDateTime	(System.IFormatProvider provider){return System.Convert.ToDateTime	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public decimal	ToDecimal	(System.IFormatProvider provider){return System.Convert.ToDecimal	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public double	ToDouble	(System.IFormatProvider provider){return System.Convert.ToDouble	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int16	ToInt16		(System.IFormatProvider provider){return 0 ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int32	ToInt32		(System.IFormatProvider provider){return System.Convert.ToInt32		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int64	ToInt64		(System.IFormatProvider provider){return System.Convert.ToInt64		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public sbyte	ToSByte		(System.IFormatProvider provider){return 0 ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Single	ToSingle	(System.IFormatProvider provider){return System.Convert.ToSingle	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public string	ToString	(System.IFormatProvider provider){return System.Convert.ToString	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public UInt16	ToUInt16	(System.IFormatProvider provider){return System.Convert.ToUInt16	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public UInt32	ToUInt32	(System.IFormatProvider provider){return System.Convert.ToUInt32	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public UInt64	ToUInt64	(System.IFormatProvider provider){return System.Convert.ToUInt64	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public object	ToType		(Type type, System.IFormatProvider provider){return System.Convert.ChangeType (val, type, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public TypeCode GetTypeCode	() {return System.Convert.GetTypeCode (val) ;}
		#endregion

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.Double[].
		/// </summary>
		/// <returns>The result.</returns>
		public virtual double[] ToDoubleArray ()
		{
			return val ;
		}

		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString ()
		{
			System.Text.StringBuilder str ;

			str = new System.Text.StringBuilder() ;
			str.Append ('{');
			if (val.Length > 0)
			{
				str.Append (val[0].ToString ());
				for (int index = 1 ; index < val.Length ; index++)
				{
					str.Append (", ");
					str.Append (val[index].ToString ());
				}
			}
			str.Append ('}') ;
			
			return str.ToString () ;
		}

		/// <summary>
		/// Converts this Value to a System.String using the specified format.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString (string format, IFormatProvider formatProvider)
		{
			System.Text.StringBuilder str ;

			str = new System.Text.StringBuilder() ;
			str.Append ('{');
			if (val.Length > 0)
			{
				str.Append (val[0].ToString (format, formatProvider));
				for (int index = 1 ; index < val.Length ; index++)
				{
					str.Append (", ");
					str.Append (val[index].ToString (format, formatProvider));
				}
			}
			str.Append ('}') ;
			
			return str.ToString () ;
		}
		#endregion
	}
}

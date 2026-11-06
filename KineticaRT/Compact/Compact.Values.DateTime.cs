using System;

namespace KineticaRT.Compact.Values
{
	/// <summary>
	/// KineticaRT Value containing a date and time value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[System.ComponentModel.TypeConverter(typeof(DateTime.TypeConverter))]
	[System.ComponentModel.ImmutableObject(true)] 
	[Serializable]
	public class DateTime:Value, IConvertible, IFormattable
	{
		#region TypeConverter
		/// <summary>
		/// TypeConverter for KineticaRT.DateTime type.
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
					return new DateTime ((string)value);
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
					return ((DateTime)value).ToString() ;
				}
				return "Type Mismatch" ;
			}
		}
		#endregion

		private System.DateTime val ;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new class.</param>
		public DateTime (System.DateTime value)
		{
			val = value ;
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new instance.</param>
		public DateTime (string value) 
		{
			val = System.Convert.ToDateTime (value) ;
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
		/// Converts this Value to a System.Double.
		/// </summary>
		/// <returns>The result.</returns>
		public override double ToDouble ()
		{
			return val.Second;
		}
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString	()
		{
			return val.ToString() ;
		}

		/// <summary>
		/// Converts this Value to a System.String using the specified format.
		/// </summary>
		/// <returns>The result.</returns>
		/// <remarks>The following formats can be used:
		/// <ul>
		/// <li>"d" - Short date</li>
		/// <li>"D" - Long date</li>
		/// <li>"f" - Long date, short time</li>
		/// <li>"F" - Long date, long time</li>
		/// <li>"g" - short date, short time</li>
		/// <li>"G" - short date, long time</li>
		/// <li>"t" - short time</li>
		/// <li>"T" - Long time</li>
		/// <li>"u" - Universal time (sortable)</li>
		/// <li>"U" - Universal time</li>
		/// <li>"dddd yyyy/MM/dd hh:mm:ss fff" - Custom</li>
		/// </ul>
		/// </remarks>
		public override string ToString (string format, IFormatProvider formatProvider)
		{
			return val.ToString (format, formatProvider) ;
		}
		#endregion

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values</summary>
		/// <returns>A 32-bit signed integer that indicates the relative order of the comparands. The return value has these meanings:
		///	<ul>
		///	<li>Less than zero - This instance is less than obj. </li>
		///	<li>Zero - This instance is equal to obj. </li>
		///	<li> Greater than zero - This instance is greater than obj. </li>
		///	</ul>
		/// </returns>
		public override int CompareTo (object obj)
		{
			DateTime it = obj as DateTime ;
			if (it == null) return 1 ;
			return this.val.CompareTo (it.val) ;
		}
	}
}

using System;

namespace KineticaRT.Values
{
	#region TypeConverter
	/// <summary>
	/// TypeConverter for KineticaRT.DateTime type.
	/// </summary>
	public class DateTimeTypeConverter : System.ComponentModel.TypeConverter 
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
			string string_value = value as string ;
			if (string_value != null) 
			{
				return new DateTime (string_value);
			}
			return null ;
		}

		/// <summary>
		/// Overrides the ConvertTo method of TypeConverter.
		/// </summary>
		public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) 
		{  
			DateTime date_time_value = value as DateTime ;

			if (date_time_value != null && destinationType == typeof(string)) 
			{
				if (culture == null) return new System.ArgumentNullException ("culture") ;
				return date_time_value.ToString(culture.DateTimeFormat) ;
			}
			return "Type Mismatch" ;
		}
	}
	#endregion

	/// <summary>
	/// KineticaRT Value containing a date and time value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]

    [System.Diagnostics.DebuggerDisplay("Value = {val}")] //ft Shows value in the debugger's watch window
	[System.ComponentModel.TypeConverter(typeof(DateTimeTypeConverter))]
	[System.ComponentModel.ImmutableObject(true)] 
	[Serializable]
	[CLSCompliantAttribute(false)]
	public class DateTime:KineticaRT.Values.Value, IConvertible, IFormattable
	{
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
			val = System.Convert.ToDateTime (value, System.Globalization.DateTimeFormatInfo.CurrentInfo) ;
		}

		#region IConvertible
		/// <summary>Implements the IConvertible Interface.</summary>
		public bool		ToBoolean	(System.IFormatProvider provider){return System.Convert.ToBoolean	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public byte		ToByte		(System.IFormatProvider provider){return System.Convert.ToByte		(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public char		ToChar		(System.IFormatProvider provider){return System.Convert.ToChar		(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public System.DateTime	ToDateTime	(System.IFormatProvider provider){return System.Convert.ToDateTime	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public decimal	ToDecimal	(System.IFormatProvider provider){return System.Convert.ToDecimal	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public double	ToDouble	(System.IFormatProvider provider){return val.Second ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int16	ToInt16		(System.IFormatProvider provider){return System.Convert.ToInt16		(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int32	ToInt32		(System.IFormatProvider provider){return System.Convert.ToInt32		(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int64	ToInt64		(System.IFormatProvider provider){return System.Convert.ToInt64		(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public sbyte	ToSByte		(System.IFormatProvider provider){return System.Convert.ToSByte		(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Single	ToSingle	(System.IFormatProvider provider){return System.Convert.ToSingle	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public string	ToString	(System.IFormatProvider provider){return System.Convert.ToString	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public UInt16	ToUInt16	(System.IFormatProvider provider){return System.Convert.ToUInt16	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public UInt32	ToUInt32	(System.IFormatProvider provider){return System.Convert.ToUInt32	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public UInt64	ToUInt64	(System.IFormatProvider provider){return System.Convert.ToUInt64	(val, provider) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public object	ToType		(Type conversionType, System.IFormatProvider provider){return System.Convert.ChangeType (val, conversionType, provider) ;}
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
			return val.ToString(System.Globalization.DateTimeFormatInfo.CurrentInfo) ;
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

		/// <summary>
		/// Equality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is equal to RHS otherwise false.</returns>
		public static bool operator ==(DateTime value1, DateTime value2)
		{
			if ((object)value1 == (object)value2) return true ;
			if ((object)value1 == null || (object)value2 == null) return false ;
			return (value1.val == value2.val);
		}

		/// <summary>
		/// Inequality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is not equal to RHS otherwise false.</returns>
		public static bool operator !=(DateTime value1, DateTime value2)
		{
			if ((object)value1 == (object)value2) return false ;
			if ((object)value1 == null || (object)value2 == null) return true ;
			return (value1.val != value2.val);
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			DateTime time_value = obj as DateTime ;
			if (time_value != null) return time_value == this ;
			Value value = obj as Value ;
			if (value != null) return this == value ;
			return false ;
		}

		/// <summary>
		/// Returns the hash code of this object.
		/// </summary>
		public override int GetHashCode ()
		{
			return val.GetHashCode() ;
		}

        #region Fuctions
        /// <summary>
        /// Adds this Value to another Value.
        /// </summary>
        /// <param name="value">The Value to add.</param>
        /// <returns>The result.</returns>
        public override Value Add(Value value)
        {
            if (value is KineticaRT.Values.TimeSpan)
            {
                TimeSpan ts = (KineticaRT.Values.TimeSpan) value;
                return new KineticaRT.Values.DateTime(this.val + ts.ToTimeSpan());
            }

            return Values.TypeMismatch ;
        }
        #endregion

    }
}

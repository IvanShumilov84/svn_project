using System;
using System.Collections.Generic;
using System.Text;

namespace KineticaRT.Values
{
	#region TypeConverter
	/// <summary>
    /// TypeConverter for KineticaRT.Int32Array type.
	/// </summary>
    public class Int32ArrayTypeConverter : System.ComponentModel.TypeConverter 
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
                return new Int32Array(string_value);
			}
			return null ;
		}

		/// <summary>
		/// Overrides the ConvertTo method of TypeConverter.
		/// </summary>
		public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) 
		{  
			if (culture == null) throw new System.ArgumentNullException ("culture") ;

			if (destinationType == typeof(string)) 
			{
				if (value == null) return "null" ;
                return ((Int32Array)value).ToString(culture.NumberFormat);
			}
			return "Type Mismatch" ;
		}
	}
	#endregion

	/// <summary>
    /// KineticaRT Value containing an array of Int32 precision IEEE floating point values.
	/// </summary>
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
	[Copyright ("Copyright 2009 KineticaRT Ltd.")]
    [ChangeRecord("2.0.1.2", "03 03 2009", "N. Savidge", "Original version")]

    [System.Diagnostics.DebuggerDisplay("Value = {val}")] //ft Shows value in the debugger's watch window
	[System.ComponentModel.ImmutableObject(true)]
    [System.ComponentModel.TypeConverter(typeof(Int32ArrayTypeConverter))]
	[Serializable]
	public class Int32Array:Value, IConvertible, IFormattable
	{
        private Int32[] val;

        /// <summary>Compares this instance to a specified object and returns an indication of their relative values</summary>
        /// <returns>A 32-bit signed integer that indicates the relative order of the comparands. The return value has these meanings:
        ///	<ul>
        ///	<li>Less than zero - This instance is less than obj. </li>
        ///	<li>Zero - This instance is equal to obj. </li>
        ///	<li> Greater than zero - This instance is greater than obj. </li>
        ///	</ul>
        /// </returns>
        public override int CompareTo(object obj)
        {
            Int32[] it = obj as Int32[];
            if (it == null) return 1 ;
            if (it.Length != val.Length) return 1 ;
            for (int index = 0 ; index < it.Length ; index ++)
            {
                if (it[index] != val[index]) return 1 ;
            }

            return 0 ;
        }

        /// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new class.</param>
        public Int32Array(Int32[] value)
		{
			val = value ;
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new instance.</param>
        public Int32Array(string value) 
		{
            val = new Int32[1];
            if (System.Int32.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out val[0]) == false)
			{
                val[0] = System.Int32.MinValue;
			}
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
		public double	ToDouble	(System.IFormatProvider provider){return System.Convert.ToDouble	(val, provider) ;}
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
        /// Converts this Value to a System.Int32[].
		/// </summary>
		/// <returns>The result.</returns>
        public virtual Int32[] ToInt32Array()
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
				str.Append (val[0].ToString (System.Globalization.NumberFormatInfo.CurrentInfo));
				for (int index = 1 ; index < val.Length ; index++)
				{
					str.Append (", ");
					str.Append (val[index].ToString (System.Globalization.NumberFormatInfo.CurrentInfo));
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


		/// <summary>
		/// Equality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is equal to RHS otherwise false.</returns>
        public static bool operator ==(Int32Array value1, Int32Array value2)
		{
			if ((object)value1 == (object)value2) return true ;
			if ((object)value1 == null || (object)value2 == null) return false ;
			return false ;
		}

		/// <summary>
		/// Inequality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is not equal to RHS otherwise false.</returns>
        public static bool operator !=(Int32Array value1, Int32Array value2)
		{
			if ((object)value1 == (object)value2) return false ;
			if ((object)value1 == null || (object)value2 == null) return true ;
			return true ;
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return false ;
		}

		/// <summary>
		/// Returns the hash code of this object.
		/// </summary>
		public override int GetHashCode ()
		{
			return this.val == null ? 0 : this.val.Length == 0 ? -1 : System.Convert.ToInt32(val[0]) ;
		}

		#endregion
	}
}

using System;

namespace KineticaRT.Values
{
	#region TypeConverter
	/// <summary>
	/// TypeConverter for KineticaRT.Boolean type.
	/// </summary>
	public class BooleanTypeConverter : System.ComponentModel.TypeConverter 
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
				return new Boolean (string_value);
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
				Boolean boolean_value = value as Boolean ;
				if (boolean_value == null) return "null" ;
				return boolean_value.ToString(culture.NumberFormat) ;
			}
			return "Type Mismatch" ;
		}
	}
	#endregion

       

	/// <summary>
	/// KineticaRT Value containing a boolean value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
    [System.Diagnostics.DebuggerDisplay("Value = {val}")] //ft Shows value in the debugger's watch window
	[System.ComponentModel.TypeConverter(typeof(BooleanTypeConverter))]
	[System.ComponentModel.ImmutableObject(true)] 
	[Serializable]
	[CLSCompliantAttribute(false)]
	public class Boolean:Value, IConvertible, IFormattable
	{
		private bool val ;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new class.</param>
		public Boolean (bool value)
		{
			val = value ;
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new instance.</param>
		public Boolean (string value) 
		{
            try
            {        
			    val = System.Convert.ToBoolean (value, System.Globalization.NumberFormatInfo.CurrentInfo) ;
            }
            catch
            {
                if ((value != null) && (value == "0"))
                    val = false;
                else if ((value != null) && (value == "1"))
                    val = true;
                else
                    throw;
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
		/// Converts this Value to a System.Double.
		/// </summary>
		/// <returns>The result.</returns>
		public override double ToDouble ()
		{
			return val ? 1 : 0 ;
		}
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString	()
		{
			return val ? "True" : "False" ;
		}

		/// <summary>
		/// Converts this Value to a System.Boolean.
		/// </summary>
		/// <returns>The result.</returns>
		public bool ToBoolean	()
		{
			return val ;
		}
		#endregion

		/// <summary>
		/// Equality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is equal to RHS otherwise false.</returns>
		public static bool operator ==(Boolean value1, Boolean value2)
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
		public static bool operator !=(Boolean value1, Boolean value2)
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
			Boolean boolean_value = obj as Boolean ;
            if ((object)boolean_value != null) return boolean_value == this;
			Value value = obj as Value ;
            if ((object)value != null) return this == value;
			return false ;
		}

		/// <summary>
		/// Returns the hash code of this object.
		/// </summary>
		public override int GetHashCode ()
		{
			return val.GetHashCode() ;
		}

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
			Boolean it = obj as Boolean ;
			if (it == null) return 1 ;
			return this.val.CompareTo (it.val) ;
		}
	}
}

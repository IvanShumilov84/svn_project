using System;

namespace KineticaRT.Values
{
	#region TypeConverter
	/// <summary>
	/// TypeConverter for KineticaRT.Integer type.
	/// </summary>
	public class IntegerTypeConverter : System.ComponentModel.TypeConverter 
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
			string string_value =  value as string ;
			if (string_value != null) 
			{
				return new Integer (string_value);
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
				Integer int_value = value as Integer ;
				if (int_value == null) return "null" ;
				return int_value.ToString(culture.NumberFormat) ;
			}
			return "Type Mismatch" ;
		}
	}
	#endregion

	/// <summary>
	/// KineticaRT Value containing an integer.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    [System.Diagnostics.DebuggerDisplay("Value = {val}")] //ft Shows value in the debugger's watch window
	[System.ComponentModel.ImmutableObject(true)] 
	[System.ComponentModel.TypeConverter(typeof(IntegerTypeConverter))]
	public class Integer:Value, IConvertible, IFormattable
	{
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
		
		private int val ;

		#region Constructors
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		public Integer () 
		{
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new class.</param>
		public Integer (int value) 
		{
			val = value ;
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new class.</param>
		public Integer (object value) 
		{
			if (value is int) val = (int)value ;
			else if (value is System.Int16 || value is System.Int64) val = System.Convert.ToInt32(value) ;
			else
			{
				try
				{
					val = System.Convert.ToInt32 (value,System.Globalization.NumberFormatInfo.CurrentInfo) ;
				}
				catch (System.FormatException)
				{
				}
				catch (System.OverflowException)
				{
				}
			}
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="newVal">The value of the new class.</param>
		public Integer (string newVal) 
		{
			//val = System.Convert.ToInteger (newVal) ;
			try
			{
				val = System.Int32.Parse(newVal,System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo) ;
			}
			catch (System.FormatException)
			{
			}
		}

//		/// <summary>
//		/// Initialises a new instance of the class.
//		/// </summary>
//		/// <param name="newVal">The value of the new class.</param>
//		public Integer (int newVal) 
//		{
//			val = newVal ;
//		}
		#endregion

		/// <summary>
		/// Equality operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS is equal to RHS otherwise false.</returns>
		public static bool operator ==(Integer value1, Integer value2)
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
		public static bool operator !=(Integer value1, Integer value2)
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
			Integer it = obj as Integer ;
			if (it != null)
			{
				return this.val.CompareTo (it.val) ;
			}
			try 
			{
				int it2 = System.Convert.ToInt32(obj, System.Globalization.NumberFormatInfo.CurrentInfo) ;
				return this.val.CompareTo (it2) ;
			}
			catch (System.FormatException)
			{
			}

			return 1 ;
		}

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.Integer.
		/// </summary>
		/// <returns>The result.</returns>
		public override double ToDouble ()
		{
			return val ;
		}

		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString ()
		{
			return val.ToString (System.Globalization.NumberFormatInfo.CurrentInfo) ;
		}

		/// <summary>
		/// Converts this Value to a System.String using the specified format.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString (string format, IFormatProvider formatProvider)
		{
			switch (format)
			{
				case "X" :
					try
					{
						return DecimalToBase (System.Convert.ToInt32(val), 16,1) ;
					}
					catch (System.Exception e)
					{
						return e.Message ;
					}

				case "O" :
					try
					{
						return DecimalToBase (System.Convert.ToInt32(val), 8,1) ;
					}
					catch (System.Exception e)
					{
						return e.Message ;
					}

				default:
					return val.ToString (format, formatProvider) ;
			}
		}
		
//		private string DecimalToBase(int iDec, int numbase, int zeroPadding)
//		{
//			string strBin = "";
//			int[] result = new int[32];
//			int MaxBit = 32;
//			for(; iDec > 0; iDec/=numbase)
//			{
//				int rem = iDec % numbase;
//				result[--MaxBit] = rem;
//			} 
//			for (int i=MaxBit;i<result.Length;i++)
//				if ((int)result.GetValue(i) >= base10)
//					strBin += cHexa[(int)result.GetValue(i)%base10];
//				else
//					strBin += result.GetValue(i);
//			strBin = strBin.TrimStart(new char[] {'0'});
//			int zeroPaddingRequired = zeroPadding - strBin.Length ; 
//			strBin = new string('0',zeroPaddingRequired) + strBin ; 
//			return strBin;
//		}

		private string DecimalToBase(int iDec, int numberBase, int zeroPadding)
		{
			if (numberBase > 16 || numberBase < 2) throw new System.ArgumentOutOfRangeException ("numberBase", numberBase, "numberBase - Range: 2..16") ;
			if (zeroPadding < 0) throw new System.ArgumentOutOfRangeException ("zeroPadding", zeroPadding, "zeroPadding should be positive") ;

			System.Text.StringBuilder str = new System.Text.StringBuilder() ;
			bool negative ;

			if (iDec < 0)
			{
				negative = true ;
				iDec *=-1 ;
			}
			else
			{
				negative = false ;
			}

			int MaxBit = 32;
			int max = MaxBit ;
			int[] result = new int[MaxBit];

			for(; iDec > 0; iDec/=numberBase)
			{
				int rem = iDec % numberBase;
				result[--max] = rem;
			} 

			if (iDec > 0) throw new System.ArgumentOutOfRangeException ("iDec", iDec, "iDec - Too large") ;

			for (int i=max;i<MaxBit;i++)
			{
				str.Append(cHexa[result[i]]);
			}
			int zeroPaddingRequired = zeroPadding - (MaxBit - max) ; 
			if (zeroPaddingRequired > 0) str.Insert (0, new string('0',zeroPaddingRequired)) ;
			if (negative) str.Insert (0, '-') ;
			return str.ToString();
		}

		[NonSerialized()]private char[] cHexa = new char[]{'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};

		//[NonSerialized()]private const int base10 = 10;
		//[NonSerialized()]private char[] cHexa = new char[]{'A','B','C','D','E','F'};
		//int[] iHexaNumeric = new int[] {10,11,12,13,14,15};
		//int[] iHexaIndices = new int[] {0,1,2,3,4,5};
		//const int asciiDiff = 48;

		#endregion

		#region Functions
		/// <summary>
		/// Adds this Value to another Value.
		/// </summary>
		/// <param name="value">The Value to add.</param>
		/// <returns>The result.</returns>
		public override Value Add (Value value)
		{
			if (value == null) throw new System.ArgumentNullException ("value") ;
			Integer rhs_value = value as Integer ;
			if (rhs_value != null)  return new Integer (val + rhs_value.val) ;
			Text rhs_value2 = value as Text ;
			if (rhs_value2 != null)
			{
				int rhs_val ;

				try
				{
					rhs_val = System.Convert.ToInt32 (rhs_value2.ToString(System.Globalization.NumberFormatInfo.CurrentInfo),System.Globalization.NumberFormatInfo.CurrentInfo) ;
				}
				catch (System.FormatException)
				{
					return Values.TypeMismatch ;
				}

				return new Integer (val + rhs_val) ;
			}

			try
			{
				double rhs_double = value.ToDouble () ;
				return new Integer (val + rhs_double) ;
			}
			catch (System.FormatException)
			{
				return Values.TypeMismatch ;
			}
		}

		/// <summary>
		/// Subtracts this Value from another Value.
		/// </summary>
		/// <param name="value">The Value to subtract.</param>
		/// <returns>The result.</returns>
		public override Value Subtract (Value value)
		{
			if (value == null) throw new System.ArgumentNullException ("value") ;
			Integer rhs_value = value as Integer ;
			if (rhs_value != null) return new Integer (val - rhs_value.val) ;
			try
			{
				double rhs_double = value.ToDouble () ;
				return new Integer (val - rhs_double) ;
			}
			catch (System.FormatException)
			{
				return Values.TypeMismatch ;
			}
			//return Values.OperationNotSupported ;
		}

		/// <summary>
		/// Multiplies this Value by another Value.
		/// </summary>
		/// <param name="value">The Value to multiply by.</param>
		/// <returns>The result.</returns>
		public override Value Multiply (Value value)
		{
			if (value == null) throw new System.ArgumentNullException ("value") ;
			Integer rhs_value = value as Integer ;
			if (rhs_value != null) return new Integer (val * rhs_value.val) ;
			try
			{
				double rhs_double = value.ToDouble () ;
				return new Integer (val * rhs_double) ;
			}
			catch (System.FormatException)
			{
				return Values.TypeMismatch ;
			}
			//return Values.OperationNotSupported ;
		}

		/// <summary>
		/// Divides this Value by another Value.
		/// </summary>
		/// <param name="value">The Value to divide by.</param>
		/// <returns>The result.</returns>
		public override Value Divide (Value value)
		{
			if (value == null) throw new System.ArgumentNullException ("value") ;
			Integer rhs_value = value as Integer ;
			if (rhs_value != null) return new Integer (val / rhs_value.val) ;
			try
			{
				double rhs_double = value.ToDouble () ;
				return new Integer (val / rhs_double) ;
			}
			catch (System.FormatException)
			{
				return Values.TypeMismatch ;
			}
			//return Values.OperationNotSupported ;
		}
		#endregion
	}
}

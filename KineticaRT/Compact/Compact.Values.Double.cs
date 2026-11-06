using System;

namespace KineticaRT.Compact.Values
{
	/// <summary>
	/// KineticaRT Value containing a double precision IEEE floating point value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	public class Double:Value, IConvertible, IFormattable, ISerializable
	{
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
		protected void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			info.AddValue ("val", val) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public Double (SerializationInfo info, StreamingContext context)
		{
			try		{ val = (double)info.GetValue("val", typeof(double)); }
			catch	{ }
		}
#endregion

#region TypeConverter
		/// <summary>
		/// TypeConverter for KineticaRT.Double type.
		/// </summary>
		public class TypeConverter : System.ComponentModel.TypeConverter 
		{
			/// <summary>
			/// Overrides the CanConvertFrom method of TypeConverter.
			/// </summary>
			public bool CanConvertFrom(Type sourceType) 
			{
				return (sourceType == typeof(string)) ;
			}

			/// <summary>
			/// Overrides the ConvertFrom method of TypeConverter.
			/// </summary>
			public object ConvertFrom(System.Globalization.CultureInfo culture, object value) 
			{
				if (value is string) 
				{
					return new Double ((string)value);
				}
				return null ;
			}

			/// <summary>
			/// Overrides the ConvertTo method of TypeConverter.
			/// </summary>
			public object ConvertTo(System.Globalization.CultureInfo culture, object value, Type destinationType) 
			{  
				if (destinationType == typeof(string)) 
				{
					return ((Double)value).ToString() ;
				}
				return "Type Mismatch" ;
			}
		}
		#endregion

		#region IConvertible
		/// <summary>Implements the IConvertible Interface.</summary>
		public bool		ToBoolean	(System.IFormatProvider provider){return System.Convert.ToBoolean	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public byte		ToByte		(System.IFormatProvider provider){return System.Convert.ToByte		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public char		ToChar		(System.IFormatProvider provider){return System.Convert.ToChar		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public System.DateTime	ToDateTime	(System.IFormatProvider provider){return System.Convert.ToDateTime	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public decimal	ToDecimal	(System.IFormatProvider provider){return System.Convert.ToDecimal	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public double	ToDouble	(System.IFormatProvider provider){return System.Convert.ToDouble	(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int16	ToInt16		(System.IFormatProvider provider){return System.Convert.ToInt16		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int32	ToInt32		(System.IFormatProvider provider){return System.Convert.ToInt32		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public Int64	ToInt64		(System.IFormatProvider provider){return System.Convert.ToInt64		(val) ;}
		/// <summary>Implements the IConvertible Interface.</summary>
		public sbyte	ToSByte		(System.IFormatProvider provider){return System.Convert.ToSByte		(val) ;}
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
		
		private double val ;

		#region Constructors
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		public Double () 
		{
			val = double.NaN ;
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="value">The value of the new class.</param>
		public Double (double value) 
		{
			val = value ;
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="newVal">The value of the new class.</param>
		public Double (string newVal) 
		{
			try
			{
				val = System.Double.Parse(newVal) ;
			}
			catch (System.Exception)
			{
				val = System.Double.NaN ;
			}
		}

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		/// <param name="newVal">The value of the new class.</param>
		public Double (float newVal) 
		{
			val = newVal ;
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
			Double it = obj as Double ;
			if (it == null) return 1 ;
			return this.val.CompareTo (it.val) ;
		}

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.Double.
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
			return val.ToString () ;
		}

		/// <summary>
		/// Converts this Value to a System.String using the specified format.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString (string format, IFormatProvider formatProvider)
		{
			return val.ToString (format, formatProvider) ;
		}
		#endregion

		#region Functions
		/// <summary>
		/// Adds this Value to another Value.
		/// </summary>
		/// <param name="rhs">The Value to add.</param>
		/// <returns>The result.</returns>
		public override Value Add (Value rhs)
		{
			if (rhs is Double) return new Double (val + ((Double)rhs).val) ;
			if (rhs is Text)
			{
//CF			double rhs_val ;
//CF			if (System.Double.TryParse(((Text)rhs).ToString(),System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo,out rhs_val) == false)
				{
					//return Values.TypeMismatch ;
                    return new KineticaRT.Compact.Values.Text("Type Mismatch (Add " + val.ToString() + "," + rhs.ToString() + ")");
				}

//CF			return new Double (val + rhs_val) ;
			}
			//return Values.TypeMismatch ;
            return new KineticaRT.Compact.Values.Text("Type Mismatch (Add " + val.ToString() + "," + rhs.ToString() + ")");
		}

		/// <summary>
		/// Subtracts this Value from another Value.
		/// </summary>
		/// <param name="rhs">The Value to subtract.</param>
		/// <returns>The result.</returns>
		public override Value Subtract (Value rhs)
		{
			if (rhs is Double) return new Double (val - ((Double)rhs).val) ;
			//return Values.TypeMismatch ;
            return new KineticaRT.Compact.Values.Text("Type Mismatch (Subtract " + val.ToString() + "," + rhs.ToString() + ")");
		}

		/// <summary>
		/// Multiplies this Value by another Value.
		/// </summary>
		/// <param name="rhs">The Value to multiply by.</param>
		/// <returns>The result.</returns>
		public override Value Multiply (Value rhs)
		{
			if (rhs is Double) return new Double (val * ((Double)rhs).val) ;
			//return Values.TypeMismatch ;
            return new KineticaRT.Compact.Values.Text("Type Mismatch (Multiply " + val.ToString() + "," + rhs.ToString() + ")");
	}

		/// <summary>
		/// Divides this Value by another Value.
		/// </summary>
		/// <param name="rhs">The Value to divide by.</param>
		/// <returns>The result.</returns>
		public override Value Divide (Value rhs)
		{
			if (rhs is Double) return new Double (val / ((Double)rhs).val) ;
			//return Values.TypeMismatch ;
            return new KineticaRT.Compact.Values.Text("Type Mismatch (Divide " + val.ToString() + "," + rhs.ToString() + ")");
		}
		#endregion
	}
}

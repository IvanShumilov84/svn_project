using System;

namespace KineticaRT.Compact.Values
{
	/// <summary>
	/// Implements the basic functionality common to all KineticaRT Value objects.
	/// </summary>
	/// <remarks>Use this type when you don't know what type of Value a channel will supply.</remarks>
	/// <seealso cref="Double"/>
	[ChangeRecord ("1.0", "17 02 2003", "N. Savidge", "Added operators (++, >, <) for use in the sequences.")] 
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class Value: IComparable
	{
		/// <summary>Compares this instance to a specified object and returns an indication of their relative values</summary>
		/// <returns>A 32-bit signed integer that indicates the relative order of the comparands. The return value has these meanings:
		///	<ul>
		///	<li>Less than zero - This instance is less than obj. </li>
		///	<li>Zero - This instance is equal to obj. </li>
		///	<li> Greater than zero - This instance is greater than obj. </li>
		///	</ul>
		/// </returns>
		public virtual int CompareTo (object obj)
		{
			throw new ArgumentException () ;
		}

		/// <summary>
		/// Return a value one greater than the original.
		/// </summary>
		/// <param name="value">The original value.</param>
		/// <returns>The new value</returns>
		public static Value operator ++(Value value)
		{
            return value.Add(KineticaRT.Compact.Values.Values.One);
		}

		/// <summary>
		/// Greater than operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS greater than RHS otherwise false.</returns>
		public static bool operator >(Value value1, Value value2)
		{
			return (value1.ToDouble () > value2.ToDouble() ? true : false);
		}

		/// <summary>
		/// Less than operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS less than RHS otherwise false.</returns>
		public static bool operator <(Value value1, Value value2)
		{
			return (value1.ToDouble () < value2.ToDouble() ? true : false);
		}

		/// <summary>
		/// Greater than operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS greater than RHS otherwise false.</returns>
		public static bool operator >(Value value1, double value2)
		{
			return (value1.ToDouble () > value2 ? true : false);
		}

		/// <summary>
		/// Less than operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS less than RHS otherwise false.</returns>
		public static bool operator <(Value value1, double value2)
		{
			return (value1.ToDouble () < value2 ? true : false);
		}

		#region Fuctions
		/// <summary>
		/// Adds this Value to another Value.
		/// </summary>
		/// <param name="rhs">The Value to add.</param>
		/// <returns>The result.</returns>
		public virtual Value Add (Value rhs)
		{
			//return Values.OperationNotSupported ;
			return new Text ("Operation Not Supported: " + this.GetType().ToString() + ".Add") ;
		}

		/// <summary>
		/// Subtracts this Value from another Value.
		/// </summary>
		/// <param name="rhs">The Value to subtract.</param>
		/// <returns>The result.</returns>
		public virtual Value Subtract (Value rhs)
		{
			//return Values.OperationNotSupported ;
			return new Text ("Operation Not Supported: " + this.GetType().ToString() + ".Subtract") ;
		}

		/// <summary>
		/// Multiplies this Value by another Value.
		/// </summary>
		/// <param name="rhs">The Value to multiply by.</param>
		/// <returns>The result.</returns>
		public virtual Value Multiply (Value rhs)
		{
			//return Values.OperationNotSupported ;
			return new Text ("Operation Not Supported: " + this.GetType().ToString() + ".Multiply") ;
		}

		/// <summary>
		/// Divides this Value by another Value.
		/// </summary>
		/// <param name="rhs">The Value to divide by.</param>
		/// <returns>The result.</returns>
		public virtual Value Divide (Value rhs)
		{
			//return Values.OperationNotSupported ;
			return new Text ("Operation Not Supported: " + this.GetType().ToString() + ".Divide") ;
		}
		#endregion

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.Double.
		/// </summary>
		/// <returns>The result.</returns>
		public virtual double ToDouble ()
		{
			return System.Double.NaN ;
		}
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString ()
		{
			return "???" ;
		}

		/// <summary>
		/// Converts this Value to a System.String using the specified format.
		/// </summary>
		/// <returns>The result.</returns>
		public virtual string ToString (string format, IFormatProvider formatProvider)
		{
			return "???" ;
		}
		#endregion
	}
}
using System;

namespace KineticaRT.Values
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

    [System.ComponentModel.ImmutableObject(true)] 
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
			System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Strings", System.Reflection.Assembly.GetExecutingAssembly());
			throw new System.NotImplementedException (rm.GetString("Values.Value.CompareTo()")) ;
		}

		/// <summary>
		/// Return a value one greater than the original.
		/// </summary>
		/// <param name="value">The original value.</param>
		/// <returns>The new value</returns>
		public static Value operator ++(Value value)
		{
			if (value == null) return KineticaRT.Values.Values.TypeMismatch ;
			return value.Add (KineticaRT.Values.Values.One) ;
		}
		/// <summary>
		/// Adds one to the specified value.
		/// </summary>
		/// <param name="value">The value to increment.</param>
		/// <returns>The new value</returns>
		public static Value Increment (Value value)
		{
			return value ++ ;
		}

        /// <summary>
        /// Return a value one greater than the original.
        /// </summary>
        /// <param name="value">The original value.</param>
        /// <returns>The new value</returns>
        public static Value operator --(Value value)
        {
            if (value == null) return KineticaRT.Values.Values.TypeMismatch;
            return value.Subtract(KineticaRT.Values.Values.One);
        }
        /// <summary>
        /// Adds one to the specified value.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>The new value</returns>
        public static Value Decrement(Value value)
        {
            return value--;
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS is equal to RHS otherwise false.</returns>
        public static bool operator ==(Value value1, Value value2)
        {
            if ((object)value1 == (object)value2) return true;
            if ((object)value1 == null || (object)value2 == null) return false;

            if (value1.GetType() == typeof(KineticaRT.Values.Boolean))
            {
                try
                {
                    bool val1 = System.Convert.ToBoolean(value1);
                    bool val2 = System.Convert.ToBoolean(value2);
                    return val1 == val2;
                }
                catch (System.FormatException)
                {
                    return false;
                }
            }
            else if (value1.GetType() == typeof(KineticaRT.Values.Double))
            {
                try
                {
                    double val1 = System.Convert.ToDouble(value1);
                    double val2 = System.Convert.ToDouble(value2);
                    return val1 == val2;
                }
                catch (System.FormatException)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS is equal to RHS otherwise false.</returns>
        public static bool operator ==(Value value1, double value2)
        {
            if ((object)value1 == null) return false;

            double val1 = value1.ToDouble ();
            return val1 == value2;
        }

        ///// <summary>
        ///// Inequality operator.
        ///// </summary>
        ///// <param name="value1">LHS</param>
        ///// <param name="value2">RHS</param>
        ///// <returns>true if LHS is not equal to RHS otherwise false.</returns>
        //public static bool operator !=(Value value1, Value value2)
        //{
        //    if ((object)value1 == (object)value2) return false ;
        //    if ((object)value1 == null || (object)value2 == null) return true ;

        //    try
        //    {
        //        if (value1.GetType() == typeof (KineticaRT.Values.Boolean))
        //        {
        //            bool val1 = System.Convert.ToBoolean(value1);
        //            bool val2 = System.Convert.ToBoolean(value2);
        //            return val1 != val2 ;
        //        }
        //    }
        //    catch (System.FormatException)
        //    {
        //    }
        //    return true ;
        //}

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS is not equal to RHS otherwise false.</returns>
        public static bool operator !=(Value value1, Value value2)
        {
            if ((object)value1 == (object)value2) return false;
            if ((object)value1 == null || (object)value2 == null) return true;

            try
            {
                if (value1.GetType() == typeof(KineticaRT.Values.Boolean))
                {
                    if (!(value2 is IConvertible)) return true;

                    bool val1 = ((KineticaRT.Values.Boolean)value1).ToBoolean();
                    bool val2 = System.Convert.ToBoolean(value2);
                    return val1 != val2;
                }
                else if (value1.GetType() == typeof(KineticaRT.Values.Double))
                {
                    double val1 = value1.ToDouble();
                    double val2 = value2.ToDouble();
                    return val1 != val2;
                }
            }
            catch (System.FormatException)
            {
            }
            catch (System.InvalidCastException)
            {
            }
            return true;
        }


        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS is not equal to RHS otherwise false.</returns>
        public static bool operator !=(Value value1, double value2)
        {
            if ((object)value1 == null) return true;

            double val1 = value1.ToDouble();
            return val1 != value2;
        }

        /// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			Value value = obj as Value ;
			if (value != null) return value == this ;
			return false ;
		}

		/// <summary>
		/// Returns the hash code of this object.
		/// </summary>
		public override int GetHashCode ()
		{
			return 0 ; // System.Convert.ToInt32 (this, System.Globalization.NumberFormatInfo.InvariantInfo) ;
		}

		/// <summary>
		/// Greater than operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS greater than RHS otherwise false.</returns>
		public static bool operator >(Value value1, Value value2)
		{
			if ((object)value1 == (object)value2) return false ;

            return value1.CompareTo(value2) > 0;

            //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Strings", System.Reflection.Assembly.GetExecutingAssembly());
            //throw new System.NotSupportedException (rm.GetString("> operator", System.Globalization.CultureInfo.CurrentUICulture)) ;
		}

		/// <summary>
		/// Less than operator.
		/// </summary>
		/// <param name="value1">LHS</param>
		/// <param name="value2">RHS</param>
		/// <returns>true if LHS less than RHS otherwise false.</returns>
		public static bool operator <(Value value1, Value value2)
		{
			if ((object)value1 == (object)value2) return false ;
            return value1.CompareTo(value2) < 0;

			//System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Strings", System.Reflection.Assembly.GetExecutingAssembly());
			//throw new System.NotSupportedException (rm.GetString("< operator", System.Globalization.CultureInfo.CurrentUICulture)) ;
		}

        /// <summary>
        /// Greater than operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS greater than RHS otherwise false.</returns>
        public static bool operator >(Value value1, double value2)
        {
            if ((object)value1 == (object)value2) return false;

            return value1.ToDouble() > value2;

            //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Strings", System.Reflection.Assembly.GetExecutingAssembly());
            //throw new System.NotSupportedException (rm.GetString("> operator", System.Globalization.CultureInfo.CurrentUICulture)) ;
        }

        /// <summary>
        /// Less than operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS less than RHS otherwise false.</returns>
        public static bool operator <(Value value1, double value2)
        {
            if ((object)value1 == (object)value2) return false;

            return value1.ToDouble() < value2;

            //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Strings", System.Reflection.Assembly.GetExecutingAssembly());
            //throw new System.NotSupportedException (rm.GetString("< operator", System.Globalization.CultureInfo.CurrentUICulture)) ;
        }

        /// <summary>
        /// Greater than operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS greater than RHS otherwise false.</returns>
        public static bool operator >(double value1, Value value2)
        {
            if ((object)value1 == (object)value2) return false;

            return value1 > value2.ToDouble();

            //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Strings", System.Reflection.Assembly.GetExecutingAssembly());
            //throw new System.NotSupportedException (rm.GetString("> operator", System.Globalization.CultureInfo.CurrentUICulture)) ;
        }

        /// <summary>
        /// Less than operator.
        /// </summary>
        /// <param name="value1">LHS</param>
        /// <param name="value2">RHS</param>
        /// <returns>true if LHS less than RHS otherwise false.</returns>
        public static bool operator <(double value1, Value value2)
        {
            if ((object)value1 == (object)value2) return false;

            return value1 < value2.ToDouble();

            //System.Resources.ResourceManager rm = new System.Resources.ResourceManager("KineticaRT.Strings", System.Reflection.Assembly.GetExecutingAssembly());
            //throw new System.NotSupportedException (rm.GetString("< operator", System.Globalization.CultureInfo.CurrentUICulture)) ;
        }

        #region Fuctions
		/// <summary>
		/// Adds this Value to another Value.
		/// </summary>
		/// <param name="value">The Value to add.</param>
		/// <returns>The result.</returns>
		public virtual Value Add (Value value)
		{
			return Values.TextNaN ;
		}

		/// <summary>
		/// Subtracts this Value from another Value.
		/// </summary>
		/// <param name="value">The Value to subtract.</param>
		/// <returns>The result.</returns>
		public virtual Value Subtract (Value value)
		{
            return Values.TextNaN;
		}

		/// <summary>
		/// Multiplies this Value by another Value.
		/// </summary>
		/// <param name="value">The Value to multiply by.</param>
		/// <returns>The result.</returns>
		public virtual Value Multiply (Value value)
		{
            return Values.TextNaN;
		}

		/// <summary>
		/// Divides this Value by another Value.
		/// </summary>
		/// <param name="value">The Value to divide by.</param>
		/// <returns>The result.</returns>
		public virtual Value Divide (Value value)
		{
            return Values.TextNaN;
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
        // <summary>
        // Converts this Value to a System.Double.
        // </summary>
        // <returns>The result.</returns>
        //public virtual long ToLong() //ft ajout 2012/05/09  'ft TODO: Ici .ToLong() non implémenté... doit Rajouter une classe/object Values.Long  (du meme genre que Values.Double)
        //{
        //    return System.Int64.MaxValue;
        //}
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
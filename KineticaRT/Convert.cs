using System;
using System.Collections.Generic;
using System.Text;

namespace KineticaRT
{
    /// <summary>
    /// Conversion functions usefull for use with KineticaRT components.
    /// </summary>
    public static class Convert
    {
        /// <summary>
        /// Converts a String value to a double value.
        /// Handles positive and negative infinity and NaN.
        /// </summary>
        /// <param name="str">The string to be conver6ted</param>
        /// <returns>The converted value</returns>
        static public double StringToDouble(string str)
        {
            if (str == null) return System.Double.NaN;

            try
            {
                if (str == "INF") return System.Double.PositiveInfinity;
                if (str == "-INF") return System.Double.NegativeInfinity;
                if (str == "infinity") return System.Double.PositiveInfinity;
                if (str == "-infinity") return System.Double.NegativeInfinity;

                return System.Convert.ToDouble(str, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (System.FormatException)
            {
                return System.Double.NaN;
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization ;
using System.Reflection;

namespace KineticaRT.Controls
{
	/// <summary>
	/// This type converter extends the basic TypeConverter to allow conversion to an
	/// InstanceDescriptor, which allows the .NET Framework to generate source code for the object.
	/// </summary>
	public class ObjectTypeConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert the object to the specified type
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <param name="destinationType">A Type that represents the type you want to convert to</param>
		/// <returns>True if conversion is possible; otherwise false</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}

			// Always call the base to see if it can perform the conversion.
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value parameter to</param>
		/// <returns>An Object that represents the converted value</returns>

		// NB: Override to use other than the Empty Constructor
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo ctor		= value.GetType().GetConstructor(Type.EmptyTypes);
				bool			complete	= false ;

				//if it is the basic sub object type...
				if (value is SubObjectBase)
				{
					//...check if it is in the default state and therefore
					//fully described by a simple constructor
					complete = ((SubObjectBase)value).IsDefaultState() ;
				}

				return new InstanceDescriptor(ctor, null, complete);
			}

			// Always call base, even if you can't convert.
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	/// <summary>
	/// This type converter extends the basic TypeConverter to allow conversion to an
	/// InstanceDescriptor, which allows the .NET Framework to generate source code for the object.
	/// </summary>
	public class ExpandableObjectTypeConverter : ExpandableObjectConverter
	{
		/// <summary>
		/// Returns whether this converter can convert the object to the specified type
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <param name="destinationType">A Type that represents the type you want to convert to</param>
		/// <returns>True if conversion is possible; otherwise false</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}

			// Always call the base to see if it can perform the conversion.
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		/// Converts the given value object to the specified type, using the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value parameter to</param>
		/// <returns>An Object that represents the converted value</returns>

		// NB: Override to use other than the Empty Constructor
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				ConstructorInfo ctor		= value.GetType().GetConstructor(Type.EmptyTypes);
				bool			complete	= false ;

				//if it is the basic sub object type...
				if (value is SubObjectBase)
				{
					//...check if it is in the default state and therefore
					//fully described by a simple constructor
					complete = ((SubObjectBase)value).IsDefaultState() ;
				}

				return new InstanceDescriptor(ctor, null, complete);
			}

			// Always call base, even if you can't convert.
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}

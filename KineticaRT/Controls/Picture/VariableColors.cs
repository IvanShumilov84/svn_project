using System;
using System.Drawing;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization ;
using System.Reflection;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Variable colors collection
	/// </summary>
	[TypeConverter(typeof(VariableColorCollection.ColorCollConverter))]
	public class VariableColorCollection : SubObjectCollection
	{
		#region Type Converter
		internal class ColorCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					VariableColorCollection		coll		= (VariableColorCollection) value;
					ReplaceableColor []			colors		= new ReplaceableColor [coll.Count] ;
					ConstructorInfo				ctor_info ;
					
					ctor_info = typeof(VariableColorCollection).GetConstructor(new Type[]{typeof(ReplaceableColor[])});

						//copy states into array
						for (int index = 0; index < coll.Count; index++)
						{
							colors[index] = coll[index] ;
						}

					return new InstanceDescriptor(ctor_info, new object[]{colors}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the VariableColorCollection class
		/// </summary>
		public VariableColorCollection()
		{
		}

		/// <summary>
		/// Initialises a new instance of the VariableColorCollection class containing the specified ReplacableColor objects
		/// </summary>
		/// <param name="colors">Array of ReplaceableColor objects to add to collection</param>
		public VariableColorCollection(ReplaceableColor [] colors)
		{
			AddRange(colors) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// ReplaceableColor object currently at the given index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ReplaceableColor this[int Index]
		{
			get
			{
				return (ReplaceableColor) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					List[Index] = value ;
				}
			}
		}

		/// <summary>
		/// Number of enabled items in the collection
		/// </summary>
		public int EnabledCount
		{
			get
			{
				int		count		= 0 ;

				foreach (ReplaceableColor clr in this)
				{
					if (clr.Enabled)
					{
						count ++ ;
					}
				}

				return count ;
			}
		}

		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new replaceableColor object with the specified colors
		/// </summary>
		/// <param name="OriginalColor">OriginalColor of new ReplaceableColor</param>
		/// <param name="DisplayColor">DisplayColor of new ReplaceableColor</param>
		public void AddColor(Color OriginalColor, Color DisplayColor)
		{
			ReplaceableColor		clr		= new ReplaceableColor() ;
			clr.OriginalColor = OriginalColor ;
			clr.DisplayColor = DisplayColor ;
			this.List.Add(clr) ;
		}

		/// <summary>
		/// Adds an array of ReplaceableColor objects to the collection
		/// </summary>
		/// <param name="newColors">Array of ReplaceableColors to add</param>
		public void AddRange(ReplaceableColor [] newColors)
		{
			for (int index = 0; index < newColors.Length; index++)
			{
				this.List.Add(newColors[index]) ;
			}
		}
		#endregion
	}
}

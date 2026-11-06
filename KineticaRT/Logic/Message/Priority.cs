using System;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Runtime.Serialization ;
using System.Reflection;
using KineticaRT.Controls;

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// Collection of Priority objects used by the Message Display controls
	/// </summary>
	[TypeConverter(typeof(PriorityCollection.PriorityCollConverter))]
	public class PriorityCollection : SubObjectCollection
	{
		#region Type Converter
		internal class PriorityCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					PriorityCollection		coll		= (PriorityCollection) value;
					Priority []				priorities	= new Priority[coll.Count] ;
					ConstructorInfo			ctor_info ;
					
					ctor_info = typeof(PriorityCollection).GetConstructor(new Type[]{typeof(Priority[])});

						//copy priorities into array
						for (int index = 0; index < coll.Count; index++)
						{
							priorities[index] = coll[index] ;
						}

					return new InstanceDescriptor(ctor_info, new object[]{priorities}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the PriorityCollection class containing a single Priority
		/// </summary>
		public PriorityCollection()
		{
			AddPriority() ;
		}

		/// <summary>
		/// Initialises a new instance of the PriorityCollection class containing the specified Priority objects
		/// </summary>
		/// <param name="priorities">Array of Priority objects to add to collection</param>
		public PriorityCollection(Priority [] priorities)
		{
			AddRange(priorities) ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Priority object currently at the given index position
		/// </summary>
		public Priority this[int Index]
		{
			get
			{
				return (Priority) List[Index] ;
			}
			set
			{
				if (value != List[Index])
				{
					List[Index] = value ;
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Adds a new Priority object to the collection
		/// </summary>
		public void AddPriority()
		{
			this.List.Insert(0, new Priority()) ;
		}

		/// <summary>
		/// Adds an array of Priority objects to the collection
		/// </summary>
		/// <param name="newPriorities">Array of Priorities to add</param>
		public void AddRange(Priority [] newPriorities)
		{
			for (int index = 0; index < newPriorities.Length; index++)
			{
				this.List.Insert(0, newPriorities[index]) ;
			}
		}

		//returns true if finds the priority
		/// <summary>
		/// Returns information about a specified priority number
		/// </summary>
		/// <param name="Priority">Priority number to find</param>
		/// <param name="BackColor">(out) BackColor associated with the Priority (or Color.Empty if not found)</param>
		/// <param name="TextColor">(out) TextColor associated with the Priority (or Color.Empty if not found)</param>
		/// <param name="Text">(out) Text associated with the Priority (or an empty strung if not found)</param>
		/// <returns>True if a Priority object that covers the Priority number has been found</returns>
		public bool GetPriorityInfo(PriorityCode Priority, out Color BackColor, out Color TextColor, out string Text)
		{
			foreach (Priority prty in this)
			{
				if (prty.IsInRange(Priority))
				{
					//found it!
					BackColor = prty.BackColor ;
					TextColor = prty.TextColor ;
					Text = prty.Text ;

					return true ;
				}
			}

			//not found it - set to default colors and number as text
			BackColor = Color.Empty ;
			TextColor = Color.Empty ;
			Text = Priority.ToString() ;

			return false ;
		}
		#endregion
	}

	/// <summary>
	/// Message Display Priority
	/// </summary>
	[TypeConverterAttribute (typeof(ObjectTypeConverter))]
	[DefaultProperty("Range")]
	public class Priority : SubObjectBase
	{
		#region Variables and Declarations
		//property variables
		private Color				backColor ;
		private Color				textColor ;
		private string				range ;
		private string				text ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Priority class
		/// </summary>
		public Priority()
		{
			backColor = Color.Empty ;
			textColor = Color.Empty ;
			range = "0-" ;
			text = "All" ;
		}
		#endregion

		#region Properties
		///<summary>
		///Message Back Color
		///</summary>
		[Category("Priority")]
		[Description("Message Back Color")]
		public Color BackColor
		{
			get
			{
				return backColor ;
			}
			set
			{
				if (backColor != value)
				{
					backColor = value ;
					OnPropertyChanged("BackColor") ;
				}
			}
		}
		private bool ShouldSerializeBackColor()
		{
			return backColor != Color.Empty ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetBackColor()
		{
			backColor = Color.Empty ;
		}

		///<summary>
		///Message Text Color
		///</summary>
		[Category("Priority")]
		[Description("Message Text Color")]
		public Color TextColor
		{
			get
			{
				return textColor ;
			}
			set
			{
				if (textColor != value)
				{
					textColor = value ;
					OnPropertyChanged("TextColor") ;
				}
			}
		}
		private bool ShouldSerializeTextColor()
		{
			return textColor != Color.Empty ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetTextColor()
		{
			textColor = Color.Empty ;
		}

		///<summary>
		///Gets or sets the string defining which Priority values are covered
		///<remarks>Strings are comma-separated inclusive ranges or single values</remarks>
		///</summary>
		///<example><ul>
		/// <li>"1-4,6,12" covers the numbers 1, 2, 3, 4, 6 and 12</li>
		/// <li>"0,6-" covers the numbers 0 and everything greater than or equal to 6</li>
		///</ul></example>
		[Category("Priority")]
		[Description("Priority Range")]
		[DefaultValue("0-")]
		public string Range
		{
			get
			{
				return range ;
			}
			set
			{
				if (range != value)
				{
					range = value ;
					OnPropertyChanged("Range") ;
				}
			}
		}

		///<summary>
		///Priority text
		///</summary>
		[Category("Priority")]
		[Description("Priority text")]
		[DefaultValue("All")]
		public string Text
		{
			get
			{
				return text ;
			}
			set
			{
				if (text != value)
				{
					text = value ;
					OnPropertyChanged("Text") ;
				}
			}
		}
		#endregion

		#region Static Methods
		/// <summary>
		/// Checks if the specified Priority number is within the specified Range string
		/// </summary>
		/// <param name="Priority">Priority number to check</param>
		/// <param name="Range">Range of priorities covered</param>
		/// <returns>True if in Range, else false</returns>
		/// <remarks>'Range' is comma-sepatated single values or hyphenated ranges</remarks>
		///<example><ul>
		/// <li>"1-4,6,12" covers the numbers 1, 2, 3, 4, 6 and 12</li>
		/// <li>"0,6-" covers the numbers 0 and everything greater than or equal to 6</li>
		///</ul></example>
        public static bool IsInRange(PriorityCode Priority, string Range)
		{
			string			expr ;
			int				comma ;
			int				dash ;
			string			section ;
            PriorityCode number1;
            PriorityCode number2;

			if (Range == null || Range == "")		return false ;

			expr = Range ;
			while (expr.Length > 0)
			{
				//find a (comma separated) section
				comma = expr.IndexOf(",") ;
				if (comma < 0)		comma = expr.Length ;

				section = expr.Substring(0, comma) ;
	
				//is this a range? (if so will include a '-')
				dash = section.IndexOf("-") ;

				try
				{
					if (dash > 0)		//range
					{
						number1 = (PriorityCode)System.Int32.Parse(section.Substring(0, dash)) ;
						
						if (dash < section.Length - 1)
						{
							number2 = (PriorityCode)System.Int32.Parse(section.Substring(dash+1)) ; ;
							if (Priority >= number1 && Priority <= number2)
							{
								return true ;
							}
						}
						else	//nothing after dash - range is to infinity
						{
							if (Priority >= number1)
							{
								return true ;
							}
						}
					}
					else	//single number
					{
						number1 = (PriorityCode) System.Int32.Parse(section) ;
						if (number1 == Priority)
						{
							return true ;
						}
					}
				}
				catch
				{
					//invalid number - skip this section and continue
				}

				if (comma < expr.Length)
				{
					//skip comma as well
					expr = expr.Remove(0, comma + 1) ;
				}
				else
				{
					expr = expr.Remove(0, comma) ;
				}
			}

			return false ;
		}
		#endregion

		#region Public methods
		/// <summary>
		/// Checks if the specified Priority number is within the range
		/// specified by this object's Range string.
		/// </summary>
		/// <param name="PriorityValue">Priority number to check</param>
		/// <returns>True if in Range, else false</returns>
        public bool IsInRange(PriorityCode PriorityValue)
		{
			return Priority.IsInRange(PriorityValue, range) ;
		}
		#endregion
	}
}

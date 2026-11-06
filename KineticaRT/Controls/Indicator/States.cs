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
	/// Collection of State objects used by the Indicator control
	/// </summary>
	[TypeConverter(typeof(StateCollection.StateCollConverter))]
	public class StateCollection : SubObjectCollection
	{
		#region Type Converter
		internal class StateCollConverter : ObjectTypeConverter
		{
			// This code performs the actual conversion to an InstanceDescriptor
			public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(InstanceDescriptor))
				{
					StateCollection		coll		= (StateCollection) value;
					State []			states		= new State [coll.Count] ;
					ConstructorInfo		ctor_info ;
					
					ctor_info = typeof(StateCollection).GetConstructor(new Type[]{typeof(State[])});

					//copy states into array
					for (int index = 0; index < coll.Count; index++)
					{
						states[index] = coll[index] ;
					}

					return new InstanceDescriptor(ctor_info, new object[]{states}) ;
				}

				// Always call base, even if you can't convert.
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the StateCollection class
		/// </summary>
		/// <remarks>The collection defaults to containing one State.</remarks>
		public StateCollection()
		{
			AddState(Color.Red) ;
		}

		/// <summary>
		/// Initialises a new instance of the StateCollection class containing the specified State objects
		/// </summary>
		/// <param name="states">Array of State objects to add to collection</param>
		public StateCollection(State [] states)
		{
			AddRange(states) ;
		}
		#endregion

		#region ISerializable (unused)
		//		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		//		{
		//			System.Windows.Forms.MessageBox.Show("got here") ;
		////			for (int index = 0 ; index < this.Count ; index ++)
		////			{
		////				info.AddValue("State" + index.ToString(), this.List[index]) ;
		////			}
		//			info.AddValue("StateCount", this.Count) ;
		//		}
		//		private StateCollection (SerializationInfo info, StreamingContext context)
		//		{
		//			int		count = info.GetInt32("StateCount") ;
		//
		//			System.Windows.Forms.MessageBox.Show("got to constructor " + count.ToString()) ;
		//
		//			while(this.Count < count)
		//			{
		////				System.Windows.Forms.MessageBox.Show("pre add " + this.Count.ToString()) ;
		//				AddState(Color.Aqua) ;
		////				System.Windows.Forms.MessageBox.Show("added " + this.Count.ToString()) ;
		//			}
		//
		////			foreach (SerializationEntry entry in info)
		////			{
		////				AddState(Color.Aqua) ;
		//////				BaseAdd(entry.Name, entry.Value);
		//////			((Object)(entry.Value)).ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
		////			}
		//		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the State object at the given zero-based index position
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public State this[int Index]
		{
			get
			{
				return (State) List[Index] ;
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
		/// Adds a new State object of the specified color to the collection
		/// </summary>
		/// <param name="StateColor">Color of new State</param>
		public void AddState(Color StateColor)
		{
			this.List.Add(new State(StateColor)) ;
		}

		/// <summary>
		/// Adds an array of State objects to the collection
		/// </summary>
		/// <param name="newStates">Array of States to add</param>
		public void AddRange(State [] newStates)
		{
			for (int index = 0; index < newStates.Length; index++)
			{
				this.List.Add(newStates[index]) ;
			}
		}

		/// <summary>
		/// Finds the colors that relate to the given Value
		/// </summary>
		/// <param name="Value">Value to find colors for</param>
		/// <param name="clrBack">BackColor of State that covers the given Value, or Color.Empty if not found</param>
		/// <param name="clrBorder">BorderColor of State that covers the given Value, or Color.Empty if not found</param>
		/// <returns>True if found a State that covers the given value, else false</returns>
		/// <remarks>
		/// If more than one State covers a given value, the colors defined by the first one found will be used.
		/// </remarks>
		public bool FindCurrentColors(double Value, out Color clrBack, out Color clrBorder)
		{
			foreach (State state in this)
			{
				if (state.FindCurrentColors(Value, out clrBack, out clrBorder))
				{
					//found it! stop here
					return true ;
				}
			}

			clrBack = Color.Empty ;
			clrBorder = Color.Empty ;
			return false ;
		}

		/// <summary>
		/// Finds the colors and text that relate to the given Value
		/// </summary>
		/// <param name="Value">Value to find colors for</param>
		/// <param name="clrBack">BackColor of State that covers the given Value, or Color.Empty if not found</param>
		/// <param name="clrBorder">BorderColor of State that covers the given Value, or Color.Empty if not found</param>
		/// <param name="text">Text returned if found, else string.Empty</param>
		/// <returns>True if found a State that covers the given value, else false</returns>
		/// <remarks>
		/// If more than one State covers a given value, the colors defined by the first one found will be used.
		/// </remarks>
		public bool FindCurrentValues(double Value, out Color clrBack, out Color clrBorder, out string text)
		{
			foreach (State state in this)
			{
				if (state.FindCurrentValues(Value, out clrBack, out clrBorder, out text))
				{
					//found it! stop here
					return true ;
				}
			}

			clrBack = Color.Empty ;
			clrBorder = Color.Empty ;
			text = string.Empty ;

			return false ;
		}

		#endregion
	}


	/// <summary>
	/// State object that defines a correspondance between a value or range of values
	/// and a color.
	/// </summary>
	[TypeConverterAttribute (typeof(ObjectTypeConverter))]
	[DefaultProperty("BackColor")]
	public class State : KineticaRT.Controls.SubObjectBase
	{
		#region Variables and Declarations
		//property variables
		private Color				backColor ;
		private Color				borderColor ;
		private bool				enabled ;
		private double				rangeMinimum ;
		private double				rangeMaximum ;
		private double				value ;
		private bool				singleValueType ;
		private string				text ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the State class
		/// </summary>
		public State()
		{
			Initialise(Color.Red) ;
		}

		/// <summary>
		/// Initialises a new instance of the State class with the specified color
		/// </summary>
		/// <param name="StateColor">Color of new State (Back and Border)</param>
		public State(Color StateColor)
		{
			Initialise(StateColor) ;
		}

		private void Initialise(Color Clr)
		{
			backColor		= Clr ;
			borderColor		= Clr ;
			enabled			= true ;
			rangeMinimum	= 0.0 ;
			rangeMaximum	= 100.0 ;
			this.value		= 0.0 ;
			singleValueType	= true ;
			text			= "" ;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the Text displayed on the Indicator
		/// </summary>
		[Category("State")]
		[DefaultValue("")]
		[Description("Gets or sets the Text displayed on the Indicator when this state is active")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
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
		///<summary>
		///Gets or sets the Back Color associated with this State's value(s)
		///</summary>
		[Category("State")]
		[Description("Gets or sets the Back Color associated with this State's value(s)")]
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
			return backColor != Color.Red ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetBackColor()
		{
			backColor = Color.Red ;
		}

		///<summary>
		///Gets or sets the Border Color associated with this State's value(s)
		///</summary>
		[Category("State")]
		[Description("Gets or sets the Border Color associated with this State's value(s)")]
		public Color BorderColor
		{
			get
			{
				return borderColor ;
			}
			set
			{
				if (borderColor != value)
				{
					borderColor = value ;
					OnPropertyChanged("BorderColor") ;
				}
			}
		}
		private bool ShouldSerializeBorderColor()
		{
			return borderColor != Color.Red ;
		}
		/// <summary>
		/// Reset the Color to its default Value
		/// </summary>
		public void ResetBorderColor()
		{
			borderColor = Color.Red ;
		}

		///<summary>
		///Gets or sets whether the State is enabled
		///</summary>
		///<remarks>Disabled States are ignored when a value/color correspondance is being sought</remarks>
		[Category("State")]
		[Description("Gets or sets whether the State is enabled")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return enabled ;
			}
			set
			{
				if (enabled != value)
				{
					enabled = value ;
					OnPropertyChanged("Enabled") ;
				}
			}
		}

		///<summary>
		///Gets or sets the minimum value included in a range
		///</summary>
		///<remarks>This applies to range-type States only. The minumum value is included in the range, but the maximum value is excluded.</remarks>
		[Category("State")]
		[Description("Gets or sets the minimum value included in a range")]
		[DefaultValue(0.0)]
		public double RangeMinimum
		{
			get
			{
				return rangeMinimum ;
			}
			set
			{
				if (rangeMinimum != value)
				{
					rangeMinimum = value ;
					OnPropertyChanged("RangeMinimum") ;
				}
			}
		}

		///<summary>
		///Gets or sets the maximum value in a range
		///</summary>
		///<remarks>This applies to range-type States only. The minumum value is included in the range, but the maximum value is excluded.</remarks>
		[Category("State")]
		[Description("Gets or sets the maximum value in a range")]
		[DefaultValue(100.0)]
		public double RangeMaximum
		{
			get
			{
				return rangeMaximum ;
			}
			set
			{
				if (rangeMaximum != value)
				{
					rangeMaximum = value ;
					OnPropertyChanged("RangeMaximum") ;
				}
			}
		}

		///<summary>
		///Gets or sets the exact value to match for a single value State.
		///</summary>
		///<remarks>This applies to single-value-type States only.</remarks>
		[Category("State")]
		[Description("Gets or sets the exact value to match for a single value State.")]
		[DefaultValue(0.0)]
		public double SingleValue
		{
			get
			{
				return this.value ;
			}
			set
			{
				if (this.value != value)
				{
					this.value = value ;
					OnPropertyChanged("SingleValue") ;
				}
			}
		}

		///<summary>
		///Gets or sets the type of state.
		///</summary>
		///<remarks>Single-value states use the SinglValue property, and range states use the RamgeMinimum and RangeMaximum properties.</remarks>
		[Category("State")]
		[Description("Gets or sets the type of state.")]
		[DefaultValue(true)]
		public bool SingleValueType
		{
			get
			{
				return singleValueType ;
			}
			set
			{
				if (singleValueType != value)
				{
					singleValueType = value ;
					OnPropertyChanged("SingleValueType") ;
				}
			}
		}
		#endregion

		#region Public methods
		/// <summary>
		/// If the specified value is covered by this State, returns the Back and Border Colors
		/// </summary>
		/// <param name="Value">Value to check</param>
		/// <param name="clrBack">BackColor returned if found, else Color.Empty</param>
		/// <param name="clrBorder">BorderColor returned if found, else Color.Empty</param>
		/// <returns>True if Value is covered by this state, else False</returns>
		public bool FindCurrentColors(double Value, out Color clrBack, out Color clrBorder)
		{
			if (enabled)
			{
				if (!singleValueType)
				{
					if (Value >= rangeMinimum && Value < rangeMaximum)
					{
						clrBack = backColor ;
						clrBorder = borderColor ;
						return true ;
					}
				}
				else		//single value
				{
					if (Value == this.value)
					{
						clrBack = backColor ;
						clrBorder = borderColor ;
						return true ;
					}
				}
			}

			//value not covered by this state
			clrBack = Color.Empty ;
			clrBorder = Color.Empty ;
			return false ;
		}

		/// <summary>
		/// If the specified value is covered by this State, returns the Text and the Back and Border Colors
		/// </summary>
		/// <param name="Value">Value to check</param>
		/// <param name="clrBack">BackColor returned if found, else Color.Empty</param>
		/// <param name="clrBorder">BorderColor returned if found, else Color.Empty</param>
		/// <param name="text">Text returned if found, else string.Empty</param>
		/// <returns>True if Value is covered by this state, else False</returns>
		public bool FindCurrentValues(double Value, out Color clrBack, out Color clrBorder, out string text)
		{
			if (enabled)
			{
				if (!singleValueType)
				{
					if (Value >= rangeMinimum && Value < rangeMaximum)
					{
						clrBack = backColor ;
						clrBorder = borderColor ;
						text = this.text ;
						return true ;
					}
				}
				else		//single value
				{
					if (Value == this.value)
					{
						clrBack = backColor ;
						clrBorder = borderColor ;
						text = this.text ;
						return true ;
					}
				}
			}

			//value not covered by this state
			clrBack = Color.Empty ;
			clrBorder = Color.Empty ;
			text = string.Empty ;
			return false ;
		}

		#endregion
	}
}

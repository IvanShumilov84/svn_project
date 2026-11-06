using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;

namespace KineticaRT.Compact.Channels
{
	/// <summary>
	/// Base class for KineticaRT Channel objects containing boolean (true/false) values.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
	public class BooleanChannel:InfoChannel, ISerializable, IBooleanChannel
	{
		string trueText = "True" ;
		string falseText = "False" ;

		/// Initialises a new instance of the class.
		public BooleanChannel()
		{
		}
		
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
		public new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("TrueText", trueText) ;
			info.AddValue ("FalseText", falseText) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public BooleanChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try		{ trueText	= (string)info.GetValue("TrueText", typeof(string)); }
			catch	{ }

			try		{ falseText	= (string)info.GetValue("FalseText", typeof(string)); }
			catch	{ }
		}
		#endregion

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString	()
		{
            if (Value.CompareTo(KineticaRT.Compact.Values.Values.False) == 0) return falseText;
			return trueText ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Text to display when value is true.
		/// </summary>
		/// <value>A string representing the channel when true.</value>
		[System.ComponentModel.Description("Text to display when value is true."), System.ComponentModel.Category("Boolean")] 
		[DefaultValue("True")]
		public string TrueText
		{
			get
			{
				return trueText ;
			}
			set
			{
				if (trueText != value)
				{
					trueText = value ;
					OnPropertyChanged("TrueText") ;
				}
			}
		}

		/// <summary>
		/// Text to display when value is false.
		/// </summary>
		/// <value>A string representing the channel when false.</value>
		[System.ComponentModel.Description("Text to display when value is false."), System.ComponentModel.Category("Boolean")] 
		[DefaultValue("False")]
		public string FalseText
		{
			get
			{
				return falseText ;
			}
			set
			{
				if (falseText != value)
				{
					falseText = value ;
					OnPropertyChanged("FalseText") ;
				}
			}
		}
		#endregion
	}
}
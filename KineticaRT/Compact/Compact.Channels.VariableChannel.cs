using System;
using System.Runtime.Serialization ;
using System.ComponentModel;
//using System.Windows.Forms;

namespace KineticaRT.Compact.Channels
{
	/// <summary>
	/// A Channel with a value.
	/// </summary>
	/// <remarks>Differs from a channel in that the value can be persisted.</remarks>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
	public class VariableChannel:InfoChannel, ISerializable 
	{
		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		public VariableChannel()
		{
		}

		#region ISerializable
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData (info, context) ;
		}
		/// <summary>Implements the ISerializable interface and returns the data needed to serialize the object.</summary>
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("Value", Value) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected VariableChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try
			{
                KineticaRT.Compact.Values.Value constant = new KineticaRT.Compact.Values.Value();
                Value = info.GetValue("Value", constant.GetType()) as KineticaRT.Compact.Values.Value;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
		}
		#endregion
	}
}

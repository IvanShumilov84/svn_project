using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;

namespace KineticaRT.Compact.Logic.Channels
{
	/// <summary>
	/// Channel containing a boolean (True/False) value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    public class BooleanVariable : KineticaRT.Compact.Channels.BooleanChannel, ISerializable
	{
		/// Initialises a new instance of the class.
		public BooleanVariable()
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
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("Value", Value) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public BooleanVariable (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try		{ Value = (Values.Boolean) info.GetValue ("Value", typeof(Values.Boolean)) ; }
			catch	{ }
		}
		#endregion
	}
}

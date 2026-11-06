using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;
using KineticaRT.Channels;

namespace KineticaRT.Logic.Channels
{
	/// <summary>
	/// Channel containing a numeric value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class NumericVariable: NumericChannel, ISerializable
	{
		/// Initialises a new instance of the class.
		public NumericVariable()
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
		protected NumericVariable (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try
			{
				Value = new KineticaRT.Values.Double ((double) info.GetValue ("Value", typeof(double))) ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
			catch (Exception)
			{
				//falls through here with a format exception if Value=""
			}
		}
		#endregion
	}
}

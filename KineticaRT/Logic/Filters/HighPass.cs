using System;
using System.Runtime.Serialization ;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace KineticaRT.Logic.Filters
{
	/// <summary>
	/// Performs a High pass first order linear filter on values it is set to or on values from a Source channel.
	/// </summary>
	[DefaultProperty("Value")]
	//[LicenseProviderAttribute(typeof(LowPass.LicenseProvider))]
	public class HighPass:LowPass, ISerializable
	{
		/// <summary>Initializes a new instance of the class.</summary>
		public HighPass()
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
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected HighPass (SerializationInfo info, StreamingContext context):base(info, context)
		{
		}
		#endregion

		/// <summary>
		/// Called to set the current value of the object to a new value.
		/// </summary>
		/// <param name="newVal">The new value of the object.</param>
		protected override void SetValue (double newVal)
		{
			System.Diagnostics.Trace.WriteLine("Set:"+newVal.ToString()) ;
			base.SetValue(currentValue - newVal) ;
		}

	}
}

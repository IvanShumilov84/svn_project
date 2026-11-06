using System;
using System.Runtime.Serialization ;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security.Permissions;

namespace KineticaRT.Channels
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
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.GetObjectData (info, context) ;
			info.AddValue ("Value", Value) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected VariableChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try
			{
				this.RawValue= info.GetValue ("Value", typeof (KineticaRT.Values.Value)) as KineticaRT.Values.Value ;
			}
			catch (System.Runtime.Serialization.SerializationException e)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, e.Message, this.GetType().ToString()) ;
			}
		}
		#endregion
	}
}

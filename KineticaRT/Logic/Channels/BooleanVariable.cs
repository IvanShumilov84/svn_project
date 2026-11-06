using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;
using System.Security.Permissions;
using KineticaRT.Channels;

namespace KineticaRT.Logic.Channels
{
	/// <summary>
	/// Channel containing a boolean (True/False) value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class BooleanVariable: BooleanChannel, ISerializable
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
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
            if (info == null) throw new ArgumentNullException("info");
            base.GetObjectData(info, context);
            info.AddValue("Value", Value);
        }
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected BooleanVariable (SerializationInfo info, StreamingContext context):base(info, context)
		{
            if (info == null) throw new ArgumentNullException("info");
            
            try
			{
				//Value = (bool) info.GetValue ("Value", typeof(bool)) ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False  ;
				string	s		= (string)info.GetValue("Value", typeof(string)) ;
				bool	b ;

				try
				{
					b = System.Boolean.Parse(s) ;
				}
				catch
				{
					try
					{
						b = (System.Double.Parse(s) == 0) ? false : true ;
					}
					catch (System.FormatException)
					{
						b = false ;
					}
				}

				Value = b ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False  ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
		}
		#endregion
	}
}

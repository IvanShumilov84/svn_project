using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;

namespace KineticaRT.Compact.Channels
{
	/// <summary>
	/// Base class for KineticaRT Channel objects containing text (string) values.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
	public class TextChannel:InfoChannel, ISerializable
	{
		/// Initialises a new instance of the class.
		public TextChannel()
		{
		}
		
		#region ISerializable
        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination for this serialization.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
        }

        /// <summary>Constructor for the ISerializable interface.</summary>
        protected TextChannel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion
	}
}
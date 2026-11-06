using System;
using System.Xml ;
using System.Xml.Serialization ;
using System.Runtime.Serialization ;

namespace KineticaRT.Compact
{
	/// <summary>
	/// Root object for a tree of KineticaRT Logic and Server Objects.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class Server:Collection
	{
		/// <summary>Initializes a new instance of the class.</summary>
		public Server()
		{
			Children = new ChannelCollection () ;
			Name = "Server" ;
		}

		#region ISerializable
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Server (SerializationInfo info, StreamingContext context):base(info, context)
		{
		}
		#endregion
	}
}

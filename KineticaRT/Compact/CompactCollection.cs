using System;
using System.Xml.Serialization ;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Serialization ;

namespace KineticaRT
{
	/// <summary>
	/// A standard collection object.
	/// An object that has child objects.
	/// </summary>
	/// <remarks>
	/// Most KineticaRT Logic and Server objects are based on this class.
	/// A collection of these objects can be held in an ObjectCollection class.
	/// </remarks>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable] 
	[DefaultProperty("Children")]
	public class Collection: Object, ISerializable
	{
		private ChannelCollection	objChildren ;

		#region Construction
		/// <summary>
		/// Constructor
		/// </summary>
		public Collection()
		{
		}

//		/// <summary>
//		/// Finalizer
//		/// </summary>
//		~Collection()
//		{
//			Debug.WriteLine ("Finalising " + GetType().FullName) ;
//		}
		#endregion

		#region ISerializable
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData (info, context) ;
		}
		/// <summary>Implements the ISerializable interface and returns the data needed to serialize the object.</summary>
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("Children", objChildren) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Collection (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try
			{
				objChildren = new ChannelCollection () ;
				objChildren = info.GetValue ("Children", objChildren.GetType()) as ChannelCollection ;
				if (objChildren != null) objChildren.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
			}
			catch
			{
				Debug.WriteLine("No 'Children' member in SerializationInfo") ;
			}
		}
		#endregion
		

		#region Children
		/// <summary>
		/// Gets or sets the objects Children Collection.  Gets null if there are no children.
		/// </summary>
		/// <value>This object children collection.</value>
		[System.ComponentModel.Description("The objects children collection."), System.ComponentModel.Category("Object")] 
		public virtual ChannelCollection Children
		{
			get	{return objChildren ;}
			set
			{
				if (objChildren != value)
				{
					objChildren = value ;
					if (objChildren != null) objChildren.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
					OnPropertyChanged ("Children") ;
				}
			}
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (objChildren != null) objChildren.Connect () ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (objChildren != null) objChildren.Disconnect () ;
		}
		#endregion

		#region ObjectSearch
		/// <summary>
		/// Returns the named object or null if it does not exist.
		/// </summary>
		public override object FindObject (string objectName)
		{
			if (objChildren == null) return null ;
			return objChildren.FindObject (objectName) ;
		}
		#endregion
	}
}

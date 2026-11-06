using System;
using System.Xml.Serialization ;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Serialization ;

namespace KineticaRT.Compact
{
	/// <summary>
	/// A standard object.
	/// </summary>
	/// <remarks>
	/// Most KineticaRT Logic and Server objects are based on this class.
	/// A collection of these objects can be held in an ChannelCollection class.
	/// </remarks>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Name")]
	public class Object: ISerializable, IConnectable
	{
		private string				objName ;

		#region Construction
		/// <summary>
		/// Constructor
		/// </summary>
		public Object()
		{
			Debug.WriteLine ("Constructing - " + GetType().FullName) ;
		}

#if DEBUG
		/// <summary>
		/// Finalizer
		/// </summary>
		~Object()
		{
			Debug.WriteLine ("Finalising " + GetType().FullName) ;
		}
#endif
		#endregion

		#region ISerializable
        //[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        /// <summary>
        /// Returns object property information for transmission via Exchange.
        /// </summary>
        /// <param name="info">Stores all the data needed to serialize or deserialize an object for transmission via KineticaRT Exchange</param>
        /// <param name="context">Describes the source and destination of a given serialized stream</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            info.AddValue("Name", objName);
        }
        /// <summary>Constructor for the ISerializable interface.</summary>
        protected Object(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            try
            {
                objName = info.GetString("Name");
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                objName = "Name not saved";
            }
        }
        #endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets the objects name.  The name is used to identify the object from other objects.
		/// </summary>
		/// <value>The objects name.</value>
		[System.ComponentModel.Description("Name of object.  This is used to identify the object from other objects.  E.G. in an Expression or in the Source property of a Dial."),System.ComponentModel.Category("Object")] 
		public virtual string Name
		{
			get	{return objName ;}
			set
			{
				if (objName != value)
				{
					objName = value ;
					OnPropertyChanged ("Name") ;
				}
			}
		}
		#endregion 

		#region PropertyChanged
		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   
		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public virtual void Connect ()
		{
		}

        /// <summary>
        /// Connects this object to others, stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public virtual void Connect2()
        {
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public virtual void Disconnect ()
		{
		}
		#endregion

		#region ObjectSearch
		/// <summary>
		/// Event handler for requests from the objects children to find another object for them by name.
		/// </summary>
		public event ObjectSearchEventHandler ObjectSearch;   
		/// <summary>
		/// Fires an object search event to the parent object.
		/// </summary>
		protected object OnObjectSearch(string ObjectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(ObjectName);  
		}

		/// <summary>
		/// Returns the named object or null if it does not exist.
		/// </summary>
		public virtual object FindObject (string objectName)
		{
			return null ;
		}

		/// <summary>
		/// Returns the named sibling object or null if it does not exist.
		/// </summary>
		public virtual object SearchObject (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

		/// <summary>
		/// Event handler for requests from the objects children to find another object for them by name.
		/// </summary>
		protected object Children_OnObjectSearch (string objectName)
		{
			object found = FindObject (objectName) ;
			if (found == null) found = OnObjectSearch (objectName) ;
			return found ;
		}
		#endregion
	}
}

using System;
using System.Xml.Serialization ;
using System.Xml;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Serialization ;
using System.Security.Permissions;

namespace KineticaRT
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
   
	//public class Object: ISerializable, IConnectable
    public class Object : ISerializable, IConnectable, INotifyPropertyChanged //ft Ajout de  INotifyPropertyChanged
	{
		internal static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Channels", "Channels");

		private string				objName ;

		#region Construction
		/// <summary>
		/// Constructor
		/// </summary>
		public Object()
		{
			//			Debug.WriteLine ("Constructing - " + GetType().FullName) ;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		public Object(string name)
		{
			this.objName = name ;
		}


//#if DEBUG
		/// <summary>
		/// Finalizer
		/// </summary>
//		~Object()
//		{
//			Debug.WriteLine ("Finalising " + GetType().FullName) ;
//		}
//#endif
		#endregion

		#region ISerializable
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			info.AddValue ("Name", objName) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Object (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try
			{
				objName = info.GetString ("Name") ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				objName = "Name not saved" ;
			}
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets the objects name.  The name is used to identify the object from other objects.
		/// </summary>
		/// <value>The objects name.</value>
		[System.ComponentModel.Description("Name of object.  This is used to identify the object from other objects.  E.G. in an Expression or in the Source property of a Dial."),System.ComponentModel.Category("Object")] 
		public string Name
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
		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
		{
			if (PropertyChanged != null) PropertyChanged(this, eventArgs);
		}
		#endregion

		#region Connection
        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public virtual void Connect()
        {
            this.Connected = true; //ft
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
            this.Connected = false; //ft
		}
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool Connected //ft //ft
        {
            get { return connected; }
            set { connected = value; }
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
		protected object OnObjectSearch(string objectName)
		{
            if (ObjectSearch==null || string.IsNullOrEmpty(objectName)) return null;
			return ObjectSearch(objectName);  
		}

		/// <summary>
		/// Returns the named child object or null if it does not exist.
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
		protected object ChildrenOnObjectSearch (string objectName)
		{
			object found = FindObject (objectName) ;
			if (found == null) found = OnObjectSearch (objectName) ;
			return found ;
		}
		#endregion
    }
}

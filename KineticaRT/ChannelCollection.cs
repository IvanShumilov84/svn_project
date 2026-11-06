using System;
using System.Xml ;
using System.Runtime.Serialization ;
using System.Security.Permissions;
using System.Diagnostics;

#if deadCEL

// Could do with adding a check for duplicate names being added to the list.
// Need some code to handle a channel being renamed.

namespace KineticaRT
{
	/// <summary>
	/// A Collection of Channels.
	/// </summary>
	/// <remarks>A tree of channels can be built up using this class.</remarks>
	[ChangeRecord (1.0, "17 02 2003", "N. Savidge", "Added IConnectable, Rename methods and renamed to ChannelCollection from ObjectCollection")] 
	[ChangeRecord (1.0, "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class ChannelCollection: System.Collections.Specialized.NameObjectCollectionBase, ISerializable, IConnectable
	{
		#region Construction
		/// <summary>Initializes a new instance of the class.</summary>
		public ChannelCollection ()
		{
		}
		#endregion

		#region ISerializable
		/// <summary>
		/// Gets the serialization data needed to reinstantiate this object.
		/// </summary>
		/// <param name="info">The SerializationInfo object to be populated with serialization data.</param>
		/// <param name="context">The destination context of the serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new System.ArgumentNullException ("info") ;

			base.GetObjectData (info, context) ;

			for (int index = 0 ; index < this.Count ; index ++)
			{
				info.AddValue (BaseGetKey(index), (Object)BaseGet (index)) ;
			}
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected ChannelCollection (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if (info == null) throw new System.ArgumentNullException ("info") ;

			foreach (SerializationEntry entry in info)
			{
				if (entry.Value is Object)
				{
					BaseAdd(entry.Name, entry.Value);
					if (info == null) throw new System.ArgumentNullException ("info") ;
					((Object)(entry.Value)).ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
				}
			}
		}
		#endregion

		#region Object
		/// <summary>
		/// Gets the Object with the specified name
		/// </summary>
		/// <param name="objectName">The name of the Object to get</param>
		/// <returns>The Object with the specifed name</returns>
		public object FindObject(string objectName)
		{
			return BaseGet (objectName) as object ;
		}

		/// <summary>
		/// Returns the named sibling object or null if it does not exist.
		/// </summary>
		public virtual object SearchObject (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

		/// <summary>
		/// Renames the specified Object.
		/// </summary>
		/// <param name="theObject">The Object to rename.</param>
		/// <param name="newName">The new name of the object.</param>
		public void Rename(Object theObject, string newName)
		{
			if (theObject == null) throw new System.ArgumentNullException ("theObject") ;

			BaseRemove(theObject.Name) ;
			theObject.Name = newName ;
			BaseAdd(newName, theObject);
		}	

		/// <summary>
		/// Renames the specified Object.
		/// </summary>
		/// <param name="oldName">The old name of the object.</param>
		/// <param name="newName">The new name of the object.</param>
		public void Rename(string oldName, string newName)
		{
			Object the_object ;
			the_object = BaseGet (oldName) as Object ;
			if (the_object != null)
			{
				BaseRemove(oldName) ;
				the_object.Name = newName ;
				BaseAdd(newName, the_object);
			}
		}	

		/// <summary>
		/// Adds the specified Object to the collection.
		/// </summary>
		/// <param name="newObject">The Object to add.</param>
		/// <remarks>The object is added using its current name.</remarks>
		public void Add(Object newObject)
		{
			if (newObject == null) throw new System.ArgumentNullException ("newObject") ;

			BaseAdd(newObject.Name, newObject);
			newObject.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
		}	

		/// <summary>
		/// Removes the specified Object from the collection.
		/// </summary>
		/// <param name="deleteObject">The Object to remove.</param>
		/// <remarks>The object is removed using its current name.</remarks>
		public void Remove(Object deleteObject)
		{
			if (deleteObject == null) throw new System.ArgumentNullException ("deleteObject") ;

			deleteObject.ObjectSearch -= new ObjectSearchEventHandler(Children_OnObjectSearch);
			BaseRemove(deleteObject.Name) ;
		}	

		/// <summary>
		/// Adds the specifed Object to the collection and names it.
		/// </summary>
		/// <param name="name">The name to give the Object</param>
		/// <param name="newObject">The Object to add.</param>
		public void Add(string name, Object newObject)
		{
			if (newObject == null) throw new System.ArgumentNullException ("newObject") ;

			newObject.Name = name ;
			BaseAdd(newObject.Name, newObject);
			newObject.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
		}	
		#endregion
	
		#region ObjectSearch
		/// <summary>Occurs when one of the children wants to find an Object by name.</summary>
		public event ObjectSearchEventHandler ObjectSearch;   
		/// <summary>Called when one of the children wants to find an Object by name.</summary>
		protected virtual object OnObjectSearch(string objectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName);  
		}

		private object Children_OnObjectSearch (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}
		#endregion
	
		/// <summary>
		/// Gets a string array containing the names of all the channels in the collection.
		/// </summary>
		public string [] GetNames ()
		{
			string [] names = new string [this.Count] ;

			for (int index = 0 ; index < this.Count ; index ++)
			{
				names [index] = ((Object)BaseGet (index)).Name ;
			}

			return names ;
		}

		#region Connection
		/// <summary>
		/// Calls the Connect method on all the child Objects.
		/// </summary>
		public virtual void Connect ()
		{
			for (int index = 0 ; index < this.Count ; index ++)
			{
				((Object)BaseGet (index)).Connect () ;
			}
		}

		/// <summary>
		/// Calls the Disconnect method on all the child Objects.
		/// </summary>
		public virtual void Disconnect ()
		{
			for (int index = 0 ; index < this.Count ; index ++)
			{
				((Object)BaseGet (index)).Disconnect () ;
			}
		}
		#endregion

		#region PropertyChanged
		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   
		/// <summary>
		/// Called to fire the PropertyChanged event.
		/// </summary>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}

#else

// Could do with adding a check for duplicate names being added to the list.
// Need some code to handle a channel being renamed.

namespace KineticaRT
{
	/// <summary>6
	/// A Collection of Channels.
	/// </summary>
	/// <remarks>A tree of channels can be built up using this class.</remarks>
	[ChangeRecord ("1.0", "17 02 2003", "N. Savidge", "Added IConnectable, Rename methods and renamed to ChannelCollection from ObjectCollection")] 
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class ChannelCollection: System.Collections.SortedList /*NameObjectCollectionBase*/, ISerializable, IConnectable
	{
        private static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.ChannelCollection", "KineticaRT.ChannelCollection");
            
        #region Construction
		/// <summary>Initializes a new instance of the class.</summary>
		public ChannelCollection ()
		{
		}
		#endregion

		#region ISerializable
		/// <summary>
		/// Gets the serialization data needed to reinstantiate this object.
		/// </summary>
		/// <param name="info">The SerializationInfo object to be populated with serialization data.</param>
		/// <param name="context">The destination context of the serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public virtual void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new System.ArgumentNullException ("info") ;

			//base.GetObjectData (info, context) ;

            info.AddValue("Enabled", enabled);
            info.AddValue("AlternateNames", alternateNames);

			for (int index = 0 ; index < this.Count ; index ++)
			{
				//info.AddValue (BaseGetKey(index), (Object)BaseGet (index)) ;
				info.AddValue ((string)GetKey(index), GetByIndex(index)) ;
			}
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected ChannelCollection (SerializationInfo info, StreamingContext context)//:base(info, context)
		{
			if (info == null) throw new System.ArgumentNullException ("info") ;

            try
            {
                enabled = info.GetBoolean("Enabled");
                alternateNames = info.GetValue("AlternateNames", typeof(string[])) as string [];
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, e.Message, this.GetType().ToString());
            }

			foreach (SerializationEntry entry in info)
			{
				if (entry.Value is Object)
				{
					//BaseAdd(entry.Name, entry.Value);
					base.Add(entry.Name, entry.Value);
					if (info == null) throw new System.ArgumentNullException ("info") ;
					((Object)(entry.Value)).ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
				}
			}
		}
		#endregion

		#region Object
		/// <summary>
		/// Gets the Object with the specified name
		/// </summary>
		/// <param name="objectName">The name of the Object to get</param>
		/// <returns>The Object with the specifed name</returns>
		public object FindObject(string objectName)
		{
			//return BaseGet (objectName) as object ;
			int index = IndexOfKey(objectName) ;
			if (index == -1) return null ;
			return this.GetByIndex(index) ;
		}

		/// <summary>
		/// Returns the named sibling object or null if it does not exist.
		/// </summary>
		public virtual object SearchObject (string objectName)
		{
			return OnObjectSearch (objectName) ;
		}

		/// <summary>
		/// Renames the specified Object.
		/// </summary>
		/// <param name="theObject">The Object to rename.</param>
		/// <param name="newName">The new name of the object.</param>
		public void Rename(Object theObject, string newName)
		{
			if (theObject == null) throw new System.ArgumentNullException ("theObject") ;

			//BaseRemove(theObject.Name) ;
			base.Remove(theObject.Name) ;
			theObject.Name = newName ;
			//BaseAdd(newName, theObject);
			base.Add(newName, theObject);
		}	

		/// <summary>
		/// Renames the specified Object.
		/// </summary>
		/// <param name="oldName">The old name of the object.</param>
		/// <param name="newName">The new name of the object.</param>
		public void Rename(string oldName, string newName)
		{
			Object the_object ;
//			the_object = BaseGet (oldName) as Object ;
			the_object = base.GetByIndex(base.IndexOfKey(oldName)) as Object;
			if (the_object != null)
			{
//				BaseRemove(oldName) ;
				base.Remove(oldName) ;
				the_object.Name = newName ;
//				BaseAdd(newName, the_object);
				base.Add(newName, the_object);
			}
		}	

		/// <summary>
		/// Adds the specified Object to the collection.
		/// </summary>
		/// <param name="newObject">The Object to add.</param>
		/// <remarks>The object is added using its current name.</remarks>
		public void Add(Object newObject)
		{
			if (newObject == null) throw new System.ArgumentNullException ("newObject") ;

//			BaseAdd(newObject.Name, newObject);
			base.Add(newObject.Name, newObject);
			newObject.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
		}	

		/// <summary>
		/// Removes the specified Object from the collection.
		/// </summary>
		/// <param name="deleteObject">The Object to remove.</param>
		/// <remarks>The object is removed using its current name.</remarks>
		public void Remove(Object deleteObject)
		{
			if (deleteObject == null) throw new System.ArgumentNullException ("deleteObject") ;

			deleteObject.ObjectSearch -= new ObjectSearchEventHandler(Children_OnObjectSearch);
			//BaseRemove(deleteObject.Name) ;
            IDisposable obj = deleteObject as IDisposable;
            if (obj != null) obj.Dispose();
            base.Remove(deleteObject.Name);
		}	

		/// <summary>
		/// Adds the specifed Object to the collection and names it.
		/// </summary>
		/// <param name="name">The name to give the Object</param>
		/// <param name="newObject">The Object to add.</param>
		public void Add(string name, Object newObject)
		{
			if (newObject == null) throw new System.ArgumentNullException ("newObject") ;

			newObject.Name = name ;
			//BaseAdd(newObject.Name, newObject);
			base.Add(newObject.Name, newObject);
			newObject.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
		}	
		#endregion
	
		#region ObjectSearch
		/// <summary>Occurs when one of the children wants to find an Object by name.</summary>
		public event ObjectSearchEventHandler ObjectSearch;   
		/// <summary>Called when one of the children wants to find an Object by name.</summary>
		protected virtual object OnObjectSearch(string objectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(objectName);  
		}

		private object Children_OnObjectSearch (string objectName)
		{
            int dot_position = objectName.IndexOf('.');
            if (dot_position >= 0)
            {
                if (dot_position==0)
                {
                    if (objectName.Length == 1) return null;
                    return FindObject(objectName.Substring(1));
                }
                else if (objectName.Substring(0, dot_position) == "Parent")
                {
                    if (dot_position == objectName.Length - 1) return null;
                    return OnObjectSearch(objectName.Substring(dot_position+1));
                }
            }
            else
            {
                if (objectName == "Parent")
                {
                    return this;
                }
            }
            int index = IndexOfKey(objectName);
            if (index == -1) return OnObjectSearch(objectName);
            return this.GetByIndex(index);
		}
		#endregion
	
		/// <summary>
		/// Gets a string array containing the names of all the channels in the collection.
		/// </summary>
		public string [] GetNames ()
		{
			string [] names = new string [this.Count] ;

			for (int index = 0 ; index < this.Count ; index ++)
			{
//				names [index] = ((Object)BaseGet (index)).Name ;
				names [index] = (string)base.GetKey(index) ;
			}

			return names ;
		}

		#region Connection
        /// <summary>
        /// Calls the Connect method on all the child Objects.
        /// </summary>
        public virtual void Connect()
        {
            for (int index = 0; index < this.Count; index++)
            {
                //((Object)BaseGet (index)).Connect () ;
                ((Object)base.GetByIndex(index)).Connect();
            }
            this.Connected = true; //ft
        }

        /// <summary>
        /// Calls the Connect2 method on all the child Objects.
        /// </summary>
        public virtual void Connect2()
        {
            for (int index = 0; index < this.Count; index++)
            {
                //((Object)BaseGet (index)).Connect () ;
                ((Object)base.GetByIndex(index)).Connect2();
            }
        }
 
        /// <summary>
		/// Calls the Disconnect method on all the child Objects.
		/// </summary>
		public virtual void Disconnect ()
		{
			for (int index = 0 ; index < this.Count ; index ++)
			{
				//((Object)BaseGet (index)).Disconnect () ;
				((Object)base.GetByIndex(index)).Disconnect () ;
			}
            this.Connected = false; //ft
		}
        
        private bool connected = false;
        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool Connected //ft
        {
            get { return connected; }
            set { connected = value; }
        }

		#endregion

		#region PropertyChanged
		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   
		/// <summary>
		/// Called to fire the PropertyChanged event.
		/// </summary>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
		#endregion

		// Provide the strongly typed member for ICollection.
		/// <summary>
		/// Copies all the elements of the current one-dimensional Array to the specified one-dimensional Array starting at the specified destination Array index. The index is specified as a 32-bit integer.
		/// </summary>
		/// <param name="array">The one-dimensional Array that is the destination of the elements copied from the current Array.</param>
		/// <param name="index">A 32-bit integer that represents the index in array at which copying begins.</param>
		public void CopyTo(KineticaRT.Channels.Channel [] array, int index)
		{
			((System.Collections.ICollection)this).CopyTo(array, index);
		}

        private string[] alternateNames;
        /// <summary>
        /// List of names which will also be recognised as names of the collection when objects are connecting to its channels.
        /// </summary>
        public string[] AlternateNames
        {
            get
            {
                return alternateNames;
            }
            set
            {
                if (alternateNames != value)
                {
                    alternateNames = value;
                    OnPropertyChanged("AlternateNames");
                }
            }
        }

        private bool enabled = true;
        /// <summary>
        /// When set true, channels in this collection can be found and connected to, when set false the channels will not be found but will still exist.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }
	}
}
#endif
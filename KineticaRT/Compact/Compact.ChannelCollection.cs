using System;
using System.Xml ;
using System.Runtime.Serialization ;

// Could do with adding a check for duplicate names being added to the list.
// Need some code to handle a channel being renamed.

namespace KineticaRT.Compact
{
	/// <summary>
	/// A Collection of Channels.
	/// </summary>
	/// <remarks>A tree of channels can be built up using this class.</remarks>
	[ChangeRecord ("1.0", "17 02 2003", "N. Savidge", "Added IConnectable, Rename methods and renamed to ChannelCollection from ObjectCollection")] 
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    public class ChannelCollection : System.Collections.SortedList/*.Specialized.NameObjectCollectionBase*/, ISerializable, IConnectable
	{
		#region Construction
		/// <summary>Initializes a new instance of the class.</summary>
		public ChannelCollection ()
		{
		}
		#endregion

		#region ISerializable
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			for (int index = 0 ; index < this.Count ; index ++)
			{
                info.AddValue((string)GetKey(index), GetByIndex(index));
            }
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected ChannelCollection (SerializationInfo info, StreamingContext context)
		{
//CF		foreach (SerializationEntry entry in info)
//			{
//				if (entry.Value is Object)
//				{
//					BaseAdd(entry.Name, entry.Value);
//					((Object)(entry.Value)).ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
//				}
//			}
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
//			return BaseGet (name) as object ;
            int index = IndexOfKey(objectName);
            if (index == -1) return null;
            return this.GetByIndex(index);
        }

		/// <summary>
		/// Renames the specified Object.
		/// </summary>
		/// <param name="theObject">The Object to rename.</param>
		/// <param name="newName">The new name of the object.</param>
		public void Rename(Object theObject, string newName)
		{
			//BaseRemove(theObject.Name) ;
			//theObject.Name = newName ;
			//BaseAdd(newName, theObject);
            
            if (theObject == null) throw new System.ArgumentNullException("theObject");

            //BaseRemove(theObject.Name) ;
            base.Remove(theObject.Name);
            theObject.Name = newName;
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
            //Object the_object ;
            //the_object = BaseGet (oldName) as Object ;
            //if (the_object != null)
            //{
            //    BaseRemove(oldName) ;
            //    the_object.Name = newName ;
            //    BaseAdd(newName, the_object);
            //}

            Object the_object;
            the_object = base.GetByIndex(base.IndexOfKey(oldName)) as Object;
            if (the_object != null)
            {
                base.Remove(oldName);
                the_object.Name = newName;
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
//			BaseAdd(newObject.Name, newObject);
//			newObject.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);

            if (newObject == null) throw new System.ArgumentNullException("newObject");
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
            //deleteObject.ObjectSearch -= new ObjectSearchEventHandler(Children_OnObjectSearch);
            //IDisposable obj = deleteObject as IDisposable ;
            //if (obj != null) obj.Dispose() ;
            //BaseRemove(deleteObject.Name) ;
            if (deleteObject == null) throw new System.ArgumentNullException("deleteObject");

            deleteObject.ObjectSearch -= new ObjectSearchEventHandler(Children_OnObjectSearch);
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
            //newObject.Name = name;
            //BaseAdd(newObject.Name, newObject);
            //newObject.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);

            if (newObject == null) throw new System.ArgumentNullException("newObject");

            newObject.Name = name;
            base.Add(newObject.Name, newObject);
            newObject.ObjectSearch += new ObjectSearchEventHandler(Children_OnObjectSearch);
		}	
		#endregion
	
		#region ObjectSearch
		/// <summary>Occurs when one of the children wants to find an Object by name.</summary>
		public event ObjectSearchEventHandler ObjectSearch;   
		/// <summary>Called when one of the children wants to find an Object by name.</summary>
		protected virtual object OnObjectSearch(string ObjectName)
		{
			if (ObjectSearch == null) return null ;
			return ObjectSearch(ObjectName);  
		}

		private object Children_OnObjectSearch (string objectName)
		{
//			return OnObjectSearch (objectName) ;

            int dot_position = objectName.IndexOf('.');
            if (dot_position >= 0)
            {
                if (dot_position == 0)
                {
                    if (objectName.Length == 1) return null;
                    return FindObject(objectName.Substring(1));
                }
                else if (objectName.Substring(0, dot_position) == "Parent")
                {
                    if (dot_position == objectName.Length - 1) return null;
                    return OnObjectSearch(objectName.Substring(dot_position + 1));
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
            //string [] names = new string [this.Count] ;

            //for (int index = 0 ; index < this.Count ; index ++)
            //{
            //    names [index] = ((Object)BaseGet (index)).Name ;
            //}

            //return names ;

            string[] names = new string[this.Count];

            for (int index = 0; index < this.Count; index++)
            {
                names[index] = (string)base.GetKey(index);
            }

            return names;
		}

		#region Connection
		/// <summary>
		/// Calls the Connect method on all the child Objects.
		/// </summary>
		public virtual void Connect ()
		{
            //for (int index = 0 ; index < this.Count ; index ++)
            //{
            //    ((Object)BaseGet (index)).Connect () ;
            //}

            for (int index = 0; index < this.Count; index++)
            {
                ((Object)base.GetByIndex(index)).Connect();
            }
		}

        /// <summary>
        /// Calls the Connect2 method on all the child Objects.
        /// </summary>
        public virtual void Connect2()
        {
            //for (int index = 0; index < this.Count; index++)
            //{
            //    ((Object)BaseGet (index)).Connect2 () ;
            //    //((Object)base.GetByIndex(index)).Connect2();
            //}

            for (int index = 0; index < this.Count; index++)
            {
                ((Object)base.GetByIndex(index)).Connect2();
            }
        }
        
		/// <summary>
		/// Calls the Disconnect method on all the child Objects.
		/// </summary>
		public virtual void Disconnect ()
		{
            //for (int index = 0 ; index < this.Count ; index ++)
            //{
            //    ((Object)BaseGet (index)).Disconnect () ;
            //}

            for (int index = 0; index < this.Count; index++)
            {
                ((Object)base.GetByIndex(index)).Disconnect();
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

	
		/// <summary>
		/// Deletes all child objects.
		/// </summary>
		public virtual void Empty ()
		{
            //for (int index = 0 ; index < this.Count ; index ++)
            //{
            //    IDisposable obj = BaseGet (index) as IDisposable ;
            //    if (obj != null) obj.Dispose() ;
            //}
            //this.BaseClear () ;


            for (int index = 0; index < this.Count; index++)
            {
                IDisposable obj = base.GetByIndex(index) as IDisposable;
                if (obj != null) obj.Dispose();
            }
            base.Clear();
        }
	}
}

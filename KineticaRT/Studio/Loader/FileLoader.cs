using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using KineticaRT;

namespace KineticaRT.Studio
{
	/// <summary>
	/// Loads objects from XML files in a specific directory
	/// Only loads KineticaRT.Object types
	/// </summary>
	public class FileLoader
	{
		#region CLASS: ObjectInfo
		private class ObjectInfo
		{
			public bool		persistent ;		//true if object is marked for persisting
			public string	name ;				//object name
			public object	value ;				//value to reinitialise to

			public ObjectInfo(string Name, bool Persistent, object Value)
			{
				name = Name ;
				persistent = Persistent ;
				value = Value ;
			}
		}
		#endregion

		#region Variables
		/// <summary>
		/// Progress event
		/// </summary>
		public event Loader.ProgressEventHandler		Progress ;

		private	string				fileName ;
		private	XmlElement			objects ;
		private	ArrayList			objectList ;
		private	DateTime			writeTime ;			//last file write time
		private	bool				forceReload ;
		private	bool				reinitialise ;
		private	int					dirIndex ;			//set unpon initialisation
		private	int					fileIndex ;			//set unpon initialisation
		private	int					nextObjIndex ;
		#endregion

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the DirectoryLoader class
		/// </summary>
		/// <param name="Name">Name of file (no path)</param>
		/// <param name="DirIndex">Integer that uniquely identifies the directory holding this</param>
		/// <param name="Index">Integer that uniquely identifies this object within its directory</param>
		public FileLoader(string Name, int DirIndex, int Index)
		{
			fileName	= Name ;
			objects		= null ;
			objectList	= new ArrayList() ;
			forceReload	= true ;
			reinitialise= true ;
			//writeTime initialises to 01/01/0001 00:00:00.0

			dirIndex = DirIndex ;
			fileIndex = Index ;

			nextObjIndex = 1 ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets the File name that this object loads (no path)
		/// </summary>
		public string FileName
		{
			get
			{
				return fileName ;
			}
		}
		#endregion

		#region Internal Methods
		/// <summary>
		/// Loads the file. Does not create the objects
		/// </summary>
		internal void Load(Collection Collection, Loader.Translation[] Translations, string DirPath)
		{
			string			full_path	= Path.Combine(DirPath, fileName) ;
			
			FireProgress("Loading file " + fileName) ;

			//don't bother if the file doesn't exist
			if (File.Exists(full_path) && Collection != null)
			{
				DateTime	last_write			= File.GetLastWriteTime(full_path) ;
				bool		full_load			= true ;

				//check if reloading:
				if (forceReload || last_write != writeTime)
				{
					Debug.WriteLine("Full (re)load " + fileName) ;

					//clear existing objects from this file
					Clear(Collection) ;
				}
				else
				{
					//just re-initialise values
					full_load = false ;
				}
				writeTime = last_write ;

				if (full_load)
				{
					XmlDocument				doc		= new XmlDocument() ;

					try
					{
						doc.Load(full_path) ;
					}
					catch (Exception e)
					{
						Debug.WriteLine(fileName, e.Message) ;
						return ;
					}
					
					if (doc.DocumentElement != null)
					{
						//look for an 'Objects' element directly below the root
						objects = doc.DocumentElement.SelectSingleNode("Objects") as XmlElement ;

						//don't load immediately - wait for other files to be cleared/set up
					}
				}
				else if (reinitialise)
				{
					Debug.WriteLine("Reinitialising " + fileName) ;

					//just reinitialise default properties
					foreach (ObjectInfo info in objectList)
					{
						SetDefaultValue(Collection.FindObject(info.name), info.value) ;
					}
				}
				else	//nothing to do
				{
					Debug.WriteLine("Not reinitialising " + fileName) ;
				}
			}
		}

		/// <summary>
		/// Actually creates the objects
		/// </summary>
		/// <param name="Collection">Collection in which to put the objects</param>
		/// <param name="Translations">How to translate old-style objects</param>
		/// <param name="PersistenceRoot">Root node of persistence file</param>
		internal void CreateObjects(Collection Collection, Loader.Translation[] Translations, XmlElement PersistenceRoot)
		{
			XmlElement				elt		= objects as XmlElement ;

			if (elt != null && Collection != null)
			{
				//parse its child elements and create the objects
				FireProgress("Creating objects for " + fileName) ;

				// read 'ForceReload' and 'DontReinitialise' attributes
				forceReload = StringToBoolean(elt.GetAttribute("ForceReload")) ;
				reinitialise = !StringToBoolean(elt.GetAttribute("DontReinitialise")) ;

				if (elt.ChildNodes.Count > 0)
				{
					XmlNode				child			= elt.FirstChild ;
					XmlElement			child_elt ;
					Formatter			formatter		= new KineticaRT.Studio.Formatter(Translations) ;
					object				new_obj ;

					//read the child elements
					while (child != null)
					{
						child_elt = child as XmlElement ;
						if (child_elt != null)
						{
							Stream			stream		= new System.IO.MemoryStream() ;
							StreamWriter	writer		= new StreamWriter(stream) ;

							//copy the xml to the stream and reset the stream pointer
							writer.Write(child_elt.OuterXml) ;
							writer.Flush() ;
							stream.Seek(0, SeekOrigin.Begin) ;

							//deserialize the xml using our special formatter
							new_obj = formatter.Deserialize(stream) ;
							if (new_obj is KineticaRT.Object)
							{
								KineticaRT.Object		obj		= new_obj as KineticaRT.Object ;

								//is the object nameless?
								if (obj.Name == null || obj.Name == "")
								{
									//generate a new name
									string		name = obj.GetType().ToString().Replace(".", "_") ;
									name += dirIndex + "_" + fileIndex + "_" + nextObjIndex++ ;
									Debug.WriteLine("generated name " + name) ;

									obj.Name = name ;
								}

								//does an object of this name already exist?
								if (Collection.FindObject(obj.Name) != null)
								{
									Debug.WriteLine("Object " + obj.Name + " already exists") ;
								}
								else
								{
									//add it to my list, storing initial value
									objectList.Add(new ObjectInfo(obj.Name, IsPersistent(child_elt), GetDefaultValue(obj))) ;
									Collection.Children.Add(obj.Name, obj) ;
								}
							}
							//else object creation failed or invalid (non-krt) type
						}
						//else - node not an element

						child = child.NextSibling ;
					}
				}

				//finished with the objects node - discard it
				objects = null ;
			}

			//update persisted values
			SetPersistedValues(Collection, PersistenceRoot) ;
		}

		/// <summary>
		/// Removes objects created by this file from Collection
		/// </summary>
		/// <param name="Collection">Collection containing objects</param>
		internal void Clear(Collection Collection)
		{
			Debug.WriteLine("Clear " + fileName) ;

			if (Collection != null)	//if no collection, simply clears local memory
			{
				Object			obj ;
				foreach (ObjectInfo info in objectList)
				{
					Debug.WriteLine("Remove " + info.name) ;
					obj = Collection.FindObject(info.name) as Object ;
					if (obj != null)		Collection.Children.Remove(obj) ;
				}
			}

			objectList.Clear() ;
		}

		// Returns true if anything was saved
		internal bool Persist(XmlElement Root, Collection Collection)
		{
			//no point bothering if no valid persistence file or collection
			if (Root == null || Collection == null)		return false ;

			bool			persisted		= false ;
			XmlDocument		doc				= Root.OwnerDocument ;

			if (objectList.Count > 0 && doc != null)
			{
				Object			obj ;
				XmlElement		elt ;
				object			value ;
				string			str_value ;
				string			str_type ;

				foreach(ObjectInfo info in objectList)
				{
					if (info.persistent)
					{
						Debug.WriteLine(info.name + " is persistent") ;

						//find the object
						obj = Collection.FindObject(info.name) as Object ;

						if (obj != null)
						{
							PropertyDescriptor		desc ;
						
							//nothing to do if no default property
							desc = TypeDescriptor.GetDefaultProperty(obj) ;
							if (desc != null)
							{
								//find or create the node for this object
								elt = GetPersistenceElement(doc, Root, info.name) ;
								if (elt != null)
								{
									value		= desc.GetValue(obj) ;
									str_value	= value.ToString() ;
									str_type	= value.GetType().AssemblyQualifiedName ;

									elt.SetAttribute("Value", str_value) ;
									elt.SetAttribute("Type", str_type) ;
									persisted = true ;
								}
							}
						}
					}
				}
			}

			return persisted ;
		}
		#endregion

		#region Other Methods
		//returns false by default (including if string is blank)
		private bool StringToBoolean(string Str)
		{
			bool		ret_val		= false ;

			//this does seem to work, but I'm not happy with it!
			//Str.ToLower() ;
			//ret_val = Str.Length > 0 && (Str.StartsWith("t") || Str.EndsWith("1")) ;

			if (Str.Length > 0)
			{
				try
				{
					ret_val = System.Boolean.Parse(Str) ;
				}
				catch
				{
					ret_val = (System.Double.Parse(Str) == 0) ? false : true ;
				}
			}

			return ret_val ;
		}

		//returns null if no default property, or if !reinitialise
		private object GetDefaultValue(object Object)
		{
			if (reinitialise)
			{
				PropertyDescriptor		desc	= TypeDescriptor.GetDefaultProperty(Object) ;

				if (desc != null)
				{
					return desc.GetValue(Object) ;
				}
			}
			return null ;
		}

		private void SetDefaultValue(object Object, object Value)
		{
			if (Object != null)
			{
				PropertyDescriptor		desc	= TypeDescriptor.GetDefaultProperty(Object) ;

				if (desc != null)
				{
					desc.SetValue(Object, Value) ;
				}
			}
		}

		private bool IsPersistent(XmlElement Elt)
		{
			return StringToBoolean(Elt.GetAttribute("Persist")) ;
		}

		private XmlElement GetPersistenceElement(XmlDocument Doc, XmlElement Root, string ObjectName)
		{
			XmlElement				elt ;
			string					pattern ;

			//does a node called this exist already?
			pattern = "Object[@Name=\"" + ObjectName + "\"]" ;
			elt = Root.SelectSingleNode(pattern) as XmlElement ;

			if (elt == null)
			{
				//no such element currently exists - create and add one
				elt = Doc.CreateElement("Object") ;
				if (elt != null)
				{
					Root.AppendChild(elt) ;
					elt.SetAttribute("Name", ObjectName) ;
				}
			}

			return elt ;
		}

		private void SetPersistedValues(Collection Collection, XmlElement PersistenceRoot)
		{
			if (Collection == null || PersistenceRoot == null)		return ;

			XmlNodeList			list ;
			string				name ;
			Object				obj ;
			PropertyDescriptor	desc ;

			list = PersistenceRoot.SelectNodes("Object[@Name]") ;
			foreach (XmlElement elt in list)
			{
				name = elt.GetAttribute("Name") ;

				//found a valid object - is it in this file?
				foreach (ObjectInfo info in objectList)
				{
					if (info.persistent && name == info.name)
					{
						Debug.WriteLine("Updating " + info.name) ;

						obj = Collection.FindObject(name) as Object ;
						if (obj != null)
						{
							//found it - update its default property
							desc = TypeDescriptor.GetDefaultProperty(obj) ;
							if (desc != null)
							{
								ConvertDefaultProperty(desc, elt.GetAttribute("Value"), elt.GetAttribute("Type"), obj) ;
								//Debug.WriteLine("Updated") ;
							}
						}
					}
				}
			}
		}

		private void ConvertDefaultProperty(PropertyDescriptor PropertyDesc, string Value, string Typename, Object Object)
		{
			//do the conversion
			if (PropertyDesc != null)
			{
				TypeConverter		converter		= null ;
				Type				type			= Type.GetType(Typename) ;

				if (type != null)
				{
					converter = TypeDescriptor.GetConverter(type) ;
				}

				if (converter == null)
				{
					converter = PropertyDesc.Converter ;
				}

				if (converter != null && converter.CanConvertFrom(typeof(string)))
				{
					try
					{
						object		o	= converter.ConvertFromString(Value) ;

						PropertyDesc.SetValue(Object, o) ;
					}
					catch (Exception e)
					{
						Debug.WriteLine(e.Message, "Set Property exception") ;
					}
				}
				else
				{
					Debug.WriteLine("Cannot convert property " + PropertyDesc.Name, Object.Name) ;
				}
			}
		}

		private void FireProgress(string Info)
		{
			if (Progress != null)		Progress(Info) ;
		}
		#endregion

		#region Unused (commented out)
//		private string GetObjectTypeName(XmlElement Elt, Loader.Translation[] Translations, out Loader.Translation Translator)
//		{
//			string		type_name ;
//
//			Translator = null ;
//
//			type_name = Elt.GetAttribute("DotNetType") ;
//
//			if (type_name.Length == 0)
//			{
//				string		prog_id ;
//
//				//find and translate prog_id
//				prog_id = Elt.GetAttribute("CreatePROGID") ;
//
//				foreach(Loader.Translation translation in Translations)
//				{
//					if (translation.progId == prog_id)
//					{
//						type_name = translation.typename ;
//										
//						Translator = translation ;
//						break ;
//					}
//				}
//
//				if (Translator == null)
//				{
//					Debug.WriteLine("No translator", prog_id) ;
//				}
//			}
//
//			return type_name ;
//		}
//
//		private void LoadObjectProperties(Object Obj, XmlElement Elt, Loader.Translation Translator)
//		{
//			Debug.WriteLine("Loading " + Obj.Name) ;
//
//			//			if (Obj is IKRT_XML)
//			//			{
//			//				//get object to load itself
//			//				((IKRT_XML)Obj).Load(Elt) ;
//			//			}
//			//			else		//perform the load myself, as it can't do it alone
//			{
//				// goes through all attributes of a node and tries to set the property of the same
//				// name on the given object - doesn't even try to load sub-objects
//				if (Elt.Attributes != null)
//				{
//					PropertyDescriptorCollection		properties ;
//					PropertyDescriptor					desc ;
//					TypeConverter						conv ;
//
//					properties = TypeDescriptor.GetProperties(Obj) ;
//
//					foreach (XmlAttribute attr in Elt.Attributes)
//					{
//						if (attr.Name != "Name" && attr.Name != "CreatePROGID" &&
//							attr.Name != "DotNetType")
//						{
//							conv = null ;
//
//							if (Translator != null && Translator.properties.Contains(attr.Name))
//							{
//								//find the descriptor for the translated property
//								Loader.PropertyInfo		info = Translator.properties[attr.Name] as Loader.PropertyInfo ;
//
//								desc = properties.Find(info.newName, false) ;
//
//								if (desc != null)
//								{
//									//is there a defined type to convert to?
//									if (info.type != null)
//									{
//										conv = TypeDescriptor.GetConverter(info.type) ;
//									}
//									else		//use the default converter for the property
//									{
//										conv = desc.Converter ;
//									}
//								}
//							}
//							else
//							{
//								desc = properties.Find(attr.Name, false) ;
//
//								if (desc != null)			conv = desc.Converter ;
//							}
//
//							//do the conversion
//							ConvertAndSetProperty(conv, desc, attr.Value, Obj) ;
//						}
//					}
//				}
//			}
//			//			Debug.WriteLine("Loaded " + Obj.Name) ;
//		}
//
//		private void ConvertAndSetProperty(TypeConverter Converter, PropertyDescriptor PropertyDesc, string Value, Object Object)
//		{
//			//do the conversion
//			if (PropertyDesc != null)
//			{
//				if (Converter != null && Converter.CanConvertFrom(typeof(string)))
//				{
//					try
//					{
//						object		o	= Converter.ConvertFromString(Value) ;
//
//						PropertyDesc.SetValue(Object, o) ;
//					}
//					catch (Exception e)
//					{
//						Debug.WriteLine(e.Message, "Set Property exception") ;
//					}
//				}
//				else
//				{
//					Debug.WriteLine("Cannot convert property " + PropertyDesc.Name, Object.Name) ;
//				}
//			}
//		}
//
//		//returns true if still needs to be deserialized
//		private bool CreateObjectInstance(string TypeName, XmlElement DataNode, Loader.Translation Translator, ref string Name, out Object Obj, Loader.Translation[] Translations)
//		{
//			//			System.Runtime.Remoting.ObjectHandle	obj_handle ;
//			Type			type		= Type.GetType(TypeName) ;
//			bool			need_load	= true ;
//
//			Obj = null ;
//
//			if (type != null)
//			{
//				Type	serializable	= type.GetInterface("ISerializable") ;
//
//				//				if (serializable != null)
//				//				{
//				//					object[]			args		= new object[2] ;
//				//
//				//					args[0] = FillSerializationInfo(type, DataNode, Translator, Translations) ;
//				//					args[1] = new StreamingContext(StreamingContextStates.File) ;
//				//
//				//					Obj = Activator.CreateInstance(type, args) as Object ;
//				//
//				//					need_load = false ;
//				//				}
//				//				else
//			{
//				Obj = Activator.CreateInstance(type) as Object ;
//			}
//
//				if (Obj != null) 
//				{
//					if (Name.Length == 0)
//					{
//						//no name element - generate one
//						Name = type.Name.Replace(".", "_") ;
//						Name += dirIndex + "_" + fileIndex + "_" + nextObjIndex ;
//
//						nextObjIndex++ ;
//						Debug.WriteLine("generated name " + Name) ;
//					}
//
//					Obj.Name = Name ;
//				}
//			}
//
//			return need_load ;
//		}
//
//		private SerializationInfo FillSerializationInfo(Type Type, XmlElement Data, Loader.Translation Translator, Loader.Translation[] Translations)
//		{
//			SerializationInfo	info ;
//
//			//create it...
//			info = new SerializationInfo(Type, new FormatterConverter()) ;
//
//			//fill in the values in it...
//			foreach (XmlAttribute attr in Data.Attributes)
//			{
//				if (attr.Name != "CreatePROGID" &&
//					attr.Name != "DotNetType")
//				{
//					if (Translator != null && Translator.properties.Contains(attr.Name))
//					{
//						//find the descriptor for the translated property
//						Loader.PropertyInfo		prop_info = Translator.properties[attr.Name] as Loader.PropertyInfo ;
//
//						info.AddValue(prop_info.newName, attr.Value, typeof(string)) ;
//					}
//					else
//					{
//						info.AddValue(attr.Name, attr.Value, typeof(string)) ;
//					}
//				}
//			}
//
//			//what about child nodes?
//			XmlElement				child				= Data.FirstChild as XmlElement ;
//			SerializationInfo		child_info ;
//			Loader.Translation		child_translator ;
//			string					child_typename ;
//			Type					child_type ;
//			string					child_name ;
//			
//			while (child != null)
//			{
//				child_translator = null ;
//				child_name = child.GetAttribute("Name") ;
//				if (child_name.Length > 0)
//				{
//					child_typename = GetObjectTypeName(child, Translations, out child_translator) ;
//
//					if (child_typename.Length > 0)
//					{
//						child_type = Type.GetType(child_typename) ;
//						if (child_type != null)
//						{
//							child_info = FillSerializationInfo(child_type, child, child_translator, Translations) ;
//							child_info.AddValue("Type", child_type, typeof(Type)) ;
//							info.AddValue(child_name, child_info, typeof(SerializationInfo)) ; 
//						}
//					}
//				}
//				child = child.NextSibling as XmlElement ;
//			}
//
//			return info ;
//		}
//
		#endregion
	}
}

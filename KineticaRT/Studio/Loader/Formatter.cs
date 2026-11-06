using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections;
using System.Xml;
using System.Diagnostics;

namespace KineticaRT.Studio
{
	/// <summary>
	/// Specialist seialization formatter for Deserializing KineticaRT Xml strings.
	/// </summary>
	public class Formatter: System.Runtime.Serialization.Formatter
	{
		private ISurrogateSelector		surrogateSelector	= null;
		private StreamingContext		context;
		private SerializationBinder		binder				= null;
		
		private Loader.Translation[]	translations		= null ;

		/// <summary>
		/// Creates a new instance of the Formatter class, with the given Translations
		/// </summary>
		internal Formatter(Loader.Translation[] Translations)
		{
			translations = Translations ;
		}

		
		#region Properties
		/// <summary>
		/// Binder property - ignored by this formatter.
		/// </summary>
		/// <remarks>
		/// As the Binder, Context and SurrogateSelector properties are ignored,
		/// this formatter does not honour Surrogates
		/// </remarks>
		public override SerializationBinder Binder 
		{
			get { return binder;}			
			set { binder = value;}
		}
		
		/// <summary>
		/// Context property - ignored by this formatter.
		/// </summary>
		/// <remarks>
		/// As the Binder, Context and SurrogateSelector properties are ignored,
		/// this formatter does not honour Surrogates
		/// </remarks>
		public override StreamingContext Context 
		{
			get { return context;}			
			set { context = value;}
		}
		
		/// <summary>
		/// SurrogateSelector property - ignored by this formatter.
		/// </summary>
		/// <remarks>
		/// As the Binder, Context and SurrogateSelector properties are ignored,
		/// this formatter does not honour Surrogates
		/// </remarks>
		public override ISurrogateSelector SurrogateSelector 
		{
			get { return surrogateSelector;}			
			set { surrogateSelector = value;}
		}
		#endregion

		#region Serialize - not implemented
		/// <summary>
		/// Serialize - not implemented
		/// </summary>
		public override void Serialize(Stream strm1, object oMain)
		{
		}

		/// <summary>
		/// WriteArray - not implemented
		/// </summary>
		protected override void WriteArray(object o1, String strStream, Type tp1)
		{
		}

		/// <summary>
		/// WriteBoolean - not implemented
		/// </summary>
		protected override void WriteBoolean(bool bl1, String strStream)
		{
		}

		/// <summary>
		/// WriteByte - not implemented
		/// </summary>
		protected override void WriteByte(byte bt1, String strStream)
		{
		}

		/// <summary>
		/// WriteChar - not implemented
		/// </summary>
		protected override void WriteChar(char ch1, String strStream)
		{
		}

		/// <summary>
		/// WriteDateTime - not implemented
		/// </summary>
		protected override void WriteDateTime(DateTime dt1, String strStream)
		{
		}

		/// <summary>
		/// WriteDecimal - not implemented
		/// </summary>
		protected override void WriteDecimal(decimal dcml1, String strStream)
		{
		}

		/// <summary>
		/// WriteDouble - not implemented
		/// </summary>
		protected override void WriteDouble(double dbl1, String strStream)
		{
		}

		/// <summary>
		/// WriteInt16 - not implemented
		/// </summary>
		protected override void WriteInt16(short sht1, String strStream)
		{
		}

		/// <summary>
		/// WriteInt32 - not implemented
		/// </summary>
		protected override void WriteInt32(int i1, String strStream)
		{
		}

		/// <summary>
		/// WriteInt64 - not implemented
		/// </summary>
		protected override void WriteInt64(long lng1, String strStream)
		{
		}

		/// <summary>
		/// WriteObjectRef - not implemented
		/// </summary>
		protected override void WriteObjectRef(object obj, String name, Type memberType)
		{
		}

		/// <summary>
		/// WriteSByte - not implemented
		/// </summary>
		protected override void WriteSByte(SByte sbt1, String strStream)
		{
		}

		/// <summary>
		/// WriteSingle - not implemented
		/// </summary>
		protected override void WriteSingle(float sgl1, String strStream)
		{
		}

		/// <summary>
		/// WriteTimeSpan - not implemented
		/// </summary>
		protected override void WriteTimeSpan(TimeSpan ts1, String strStream)
		{
		}

		/// <summary>
		/// WriteUInt16 - not implemented
		/// </summary>
		protected override void WriteUInt16(UInt16 ut1, String strStream)
		{
		}

		/// <summary>
		/// WriteUInt32 - not implemented
		/// </summary>
		protected override void WriteUInt32(UInt32 ut1, String strStream)
		{
		}

		/// <summary>
		/// WriteUInt64 - not implemented
		/// </summary>
		protected override void WriteUInt64(UInt64 ut1, String strStream)
		{
		}

		/// <summary>
		/// WriteValueType - not implemented
		/// </summary>
		protected override void WriteValueType(object obj, String strStream, Type tp1)
		{
		}
		#endregion

		#region Deserialize
		/// <summary>
		/// Deserializes the data on the provided stream and reconstitutes the graph of objects
		/// </summary>
		/// <remarks>
		/// 'strm1' should contain the xml for a single object and its children, in
		/// the KRT V5 format. The Translations provided when the Formatter was
		/// created will be used to work out which DotNet objects to create.
		/// Uses ObjectManager and FormatterServices to help.
		/// </remarks>
		/// <param name="strm1">The stream containing the data to deserialize</param>
		/// <returns>The top object of the deserialized graph</returns>
		public override object Deserialize(Stream strm1)
		{
			ObjectManager	object_manager ;
			XmlDocument		xmldocument			= new XmlDocument() ;
			StreamReader	reader				= new StreamReader(strm1) ;
			object			root_obj			= null ;
			XmlElement		root_elt ;
			int				next_id				= 1 ;
			string			name ;

			//create object manager
			object_manager = new ObjectManager(SurrogateSelector, new StreamingContext(StreamingContextStates.All));

			//load xml from stream - this assumes stream position is at start
			xmldocument.LoadXml(reader.ReadToEnd());
			root_elt = xmldocument.DocumentElement ;
	
			//the root element represents the root object
			root_obj = ParseObject(root_elt, object_manager, ref next_id, out name) ;

			//sort out references
			try
			{
				object_manager.DoFixups();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.InnerException.Message, "Oops") ;
			}
			
			return root_obj;
		}

		/*=================================ParseObject==================================
		**Action: The Deserializer has encountered an object and this method constructs it.
		**        The ObjectManager class has some methods that will greatly help in constructing
		**        Objects. If an object is encountered during deserialization that the Deserializer
		**        doesn't recognize yet, the following methods (in OM) could help resolve and finally 
		**        construct that object
		**        RecordFixup, RecordDelayedFixup or RecordArrayElementFixup 
		**        RegisterObject 
		**        DoFixups 
		**        The FormatterService class provide the following methods to help deserialziation
		**        GetUninitializedObject
		**        PopulateObjectMembers 
		**Returns: constructed Object
		**Arguments: XmlNode
		**Exceptions: 
		**Notes:   This method has some duplicate code in handling ISerializable objects and other
		**         objects. 
		==============================================================================*/
		private object ParseObject(XmlElement Element, ObjectManager Manager, ref int NextId, out string RootName)
		{
			object					new_obj				= null;
			Loader.Translation		obj_translator ;
			Type					obj_type ;

			RootName = Element.GetAttribute("Name") ;

			//find type
			obj_type = GetObjectType(Element, translations, out obj_translator) ;

			if (obj_type != null)
			{
				int				id				= NextId++ ;
				XmlNode			child			= Element.FirstChild ;
				object			child_object ;
				string			child_name ;
			
				//create blank object
				new_obj = FormatterServices.GetUninitializedObject(obj_type);
				
				//is it serializable?
				if(new_obj is ISerializable) 
				{
					SerializationInfo	info			= new SerializationInfo(obj_type, new FormatterConverter());

					//go through node attributes and add them to the serialization info
					foreach (XmlAttribute attr in Element.Attributes)
					{
						if (attr.Name != "CreatePROGID" && attr.Name != "DotNetType")
						{
							if (obj_translator != null && obj_translator.properties.Contains(attr.Name))
							{
								//find the descriptor for the translated property
								Loader.PropertyInfo		prop_info = obj_translator.properties[attr.Name] as Loader.PropertyInfo ;

								info.AddValue(prop_info.newName, attr.Value, typeof(string)) ;
							}
							else
							{
								info.AddValue(attr.Name, attr.Value, typeof(string)) ;
							}
						}
					}

					//now also do the children
					while (child != null)
					{
						if (child is XmlElement)
						{
							child_object = ParseObject(child as XmlElement, Manager, ref NextId, out child_name) ;

							if (child_object != null && child_name != "")
							{
								//object has been created - no need for delayed fixup
								info.AddValue(child_name, child_object) ;
							}
							else
							{
								//object hasn't be created, or is nameless.
								//Assume this means that it isn't a standard child 
								//object so add the actual xml string to the
								//SerializationInfo instead
								AddXmlToInfo(child, info) ;
							}
						}

						child = child.NextSibling ;
					}
					
					//register object, passing in serialization info
					//for initialisation
					Manager.RegisterObject(new_obj, id, info);
				}
				else		//not ISerializable (##not tested yet)
				{
					MemberInfo[]	members			= FormatterServices.GetSerializableMembers(obj_type);
					object[]		data			= new object[members.Length];
					int				member_index ;

					//go through node attributes and add them to the member info
					foreach (XmlAttribute attr in Element.Attributes)
					{
						if (attr.Name != "CreatePROGID" && attr.Name != "DotNetType")
						{
							if (obj_translator != null && obj_translator.properties.Contains(attr.Name))
							{
								//find the descriptor for the translated property
								Loader.PropertyInfo		prop_info = obj_translator.properties[attr.Name] as Loader.PropertyInfo ;

								member_index = FindMemberIndex(prop_info.newName, members);
							}
							else
							{
								member_index = FindMemberIndex(attr.Name, members);
							}

							data[member_index] = attr.Value;
						}
					}

					//now also do the children
					while (child != null)
					{
						if (child is XmlElement)
						{
							child_object = ParseObject(child as XmlElement, Manager, ref NextId, out child_name) ;

							//##what if no name?? - currently will get ignored
							if (child_object != null && child_name != "")
							{
								//object has been created - no need for delayed fixup
								member_index = FindMemberIndex(child_name, members) ;
								data[member_index] = child_object ;
							}
						}

						child = child.NextSibling ;
					}

					//initialise and register object
					FormatterServices.PopulateObjectMembers(new_obj, members, data);
					Manager.RegisterObject(new_obj, id);
				}
			}
			
			return new_obj;
		}
		
		//=======FindMemberIndex=======
		private int FindMemberIndex(String MemberName, MemberInfo[] Members)
		{
			//convert member name into index
			for(int index = 0; index < Members.Length; index++) 
			{
				if(Members[index].Name.Equals(MemberName))
				{
					return index;
				}
			}
			throw new ArgumentException("Member not found");
		}

		private Type GetObjectType(XmlElement Elt, Loader.Translation[] Translations, out Loader.Translation Translator)
		{
			string		type_name ;

			Translator = null ;

			type_name = Elt.GetAttribute("DotNetType") ;

			if (type_name.Length == 0 && translations != null)
			{
				string		prog_id ;

				//find and translate prog_id
				prog_id = Elt.GetAttribute("CreatePROGID") ;

				foreach(Loader.Translation translation in Translations)
				{
					if (translation.progId == prog_id)
					{
						type_name = translation.typename ;
										
						Translator = translation ;
						break ;
					}
				}

				if (Translator == null && prog_id != "")		Debug.WriteLine("No translator", prog_id) ;
			}

			//convert string to Type
			return (type_name != "") ? Type.GetType(type_name) : null ;
		}

		private void AddXmlToInfo(XmlNode Node, SerializationInfo Info)
		{
			string		name			= Node.Name ;
			int			index			= 1 ;

			do
			{
				try
				{
					Info.GetValue(name, typeof(string)) ;

					name = Node.Name + index++ ;
				}
				catch (System.Runtime.Serialization.SerializationException)
				{
					//if get here we have found a name not yet in the info so we can add it
					break ;
				}
			}
			while (true) ;
			
			Info.AddValue(name, Node.OuterXml) ;
		}
		#endregion
	}
}

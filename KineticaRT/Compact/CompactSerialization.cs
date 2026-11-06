using System;
using System.Reflection ;


//namespace System.Windows.Forms
//{
//    public class MessageBox
//    {
//        public enum DialogResult
//        {
//            OK = 0 ,
//        }

//        public static DialogResult Show(string text)
//        {
//            System.Diagnostics.Debug.WriteLine (text) ;
//            return DialogResult.OK ;
//        }
//    }
//}

namespace System.Runtime.Serialization
{
	public class SerializationException : SystemException
	{
	}
}

namespace System.ComponentModel
{
	public interface ITypeDescriptorContext : IServiceProvider 
	{
	}

	public sealed class TypeConverterAttribute : Attribute 
	{
		public TypeConverterAttribute (Type type)
		{
		}
	}

	public sealed class ImmutableObjectAttribute : Attribute
	{
		public ImmutableObjectAttribute (bool value)
		{
		}
	}

	//	public class TypeConverter
//	{
//	}
	
	/// <summary>
	/// Attribute for specifying the default value of something - for serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=false)]
	public class Description : Attribute
	{
		public Description(string description)
		{
		}
	}

	/// <summary>
	/// Attribute for specifying the default value of something - for serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=false)]
	public class Category : Attribute
	{
		public Category(string category)
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}

namespace KineticaRT.Compact
{
	public class TypeConverter:System.ComponentModel.TypeConverter 
	{
		public virtual bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType) 
		{
			return false ;  
		}

		public virtual object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) 
		{
			return null ;
		}

		public virtual object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType) 
		{  
			return "Type Mismatch" ;
		}
	}
	
	/// <summary>
	/// Interface providing for KineticaRT objects to be found and connected together.
	/// </summary>
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public interface ISerializable
	{
		void GetObjectData (SerializationInfo info, StreamingContext context) ;
	}

	/// <summary>
	/// Attribute for indicating that a class can be serialized.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=false)]
	public class SerializableAttribute : Attribute
	{
	}

	/// <summary>
	/// Attribute for specifying the default value of something - for serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=false)]
	public class DefaultProperty : Attribute
	{
		public DefaultProperty(string defaultProperty)
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}


	/// <summary>
	/// Summary description for Serialization.
	/// </summary>
	public class SerializationInfo
	{
		private System.Xml.XmlNode definitionNode ;

		public SerializationInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public SerializationInfo(System.Xml.XmlNode definitionNode)
		{
			this.definitionNode = definitionNode ;
		}

        public void AddValue(string name, KineticaRT.Compact.Values.Value value)
		{
		}
		public void AddValue(string name,string value)
		{
		}
		public void AddValue(string name,double value)
		{
		}
		public void AddValue(string name,Object value)
		{
		}
        //public void AddValue(string name,object value)
        //{
        //}

		public object GetValue(string name, Type type)
		{
			System.Xml.XmlNode property_node = definitionNode.FirstChild ;
			while (property_node != null)
			{
				if (property_node.Name == name)
				{
					System.Xml.XmlNode href_node = property_node.Attributes.GetNamedItem("href") ;
					if (href_node != null)
					{
						string id = href_node.Value.Substring (1);

						System.Xml.XmlNode value_node = definitionNode.NextSibling ;
						while ((value_node != null) && (value_node.Attributes.GetNamedItem("id").Value != id))
						{
							value_node = value_node.NextSibling ;
						}

						if (value_node != null)
						{
							string[] type_string = value_node.Name.Split(':') ;
							string assembly_name = value_node.Attributes.GetNamedItem("xmlns:"+type_string[0]).Value ;
							string full_type = assembly_name.Substring (0, assembly_name.LastIndexOf("/")) ;
							full_type = full_type.Substring(full_type.LastIndexOf ("/")+1) + "." + type_string[1] ;
							assembly_name = assembly_name.Substring (assembly_name.LastIndexOf("/")+1) ;
							assembly_name = assembly_name.Substring (0, assembly_name.IndexOf("%")) ;
							System.Reflection.Assembly assembly ;
							Type chan_type = null ;

							try
							{
								assembly = System.Reflection.Assembly.Load(assembly_name);
								chan_type = assembly.GetType (full_type) ;
							}
							catch (System.IO.IOException e)
							{
								System.Diagnostics.Debug.WriteLine (e.ToString (), "Loader") ;
							}

							if (chan_type != null)
							{
								Type [] types = new Type [] {typeof(SerializationInfo), typeof(StreamingContext)} ;
								ConstructorInfo constructor_info = chan_type.GetConstructor (types) ;
								if (constructor_info != null)
								{
									SerializationInfo si = new SerializationInfo(value_node) ;
									StreamingContext sc = new StreamingContext () ;
									object new_channel = constructor_info.Invoke (new System.Object [] {si, sc})  ;
									if (new_channel != null)
									{
										return new_channel ;
									}
									else
									{
										System.Windows.Forms.MessageBox.Show("Failed To construct: " + full_type) ;
									}
								}
								else
								{
									System.Windows.Forms.MessageBox.Show ("Failed To identify: " + full_type) ;
								}
							}
						}

						return "Not Implemented" ;
					}
					else
					{
						if (type == typeof(System.Double) && property_node.InnerText == "NaN")
						{
							return System.Double.NaN ;
						}

						return System.Convert.ChangeType (property_node.InnerText, type, null) ;
					}
				}

				property_node = property_node.NextSibling ;
			}

			return null ;
		}

		public string GetString(string name)
		{
			System.Xml.XmlNode property_node = definitionNode.FirstChild ;
			while (property_node != null)
			{
				string property_name = property_node.Name ;

				if (property_name == name)
				{
					if (property_node.FirstChild != null)
					{
						return property_node.InnerText ;
					}
					return "Not Implemented" ;
				}

				property_node = property_node.NextSibling ;
			}

			return "" ;
		}

	}

	/// <summary>
	/// Summary description for Serialization.
	/// </summary>
	public class StreamingContext
	{
		public StreamingContext()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}

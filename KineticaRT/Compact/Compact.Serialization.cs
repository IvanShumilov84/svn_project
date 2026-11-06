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
    /// <summary>
    /// Dummy - Does nothing.
    /// </summary>
	public class SerializationException : SystemException
	{
	}
}

namespace System.ComponentModel
{
    /// <summary>
    /// Dummy - Does nothing.
    /// </summary>
	public interface ITypeDescriptorContext : IServiceProvider 
	{
	}

    /// <summary>
    /// Dummy - Does nothing.
    /// </summary>
	public sealed class TypeConverterAttribute : Attribute 
	{
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="type">Dummy - Does nothing.</param>
		public TypeConverterAttribute (Type type)
		{
		}
	}

    /// <summary>
    /// Dummy - Does nothing.
    /// </summary>
	public sealed class ImmutableObjectAttribute : Attribute
	{
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="value">Dummy - Does nothing.</param>
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
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="description">Dummy - Does nothing.</param>
		public Description(string description)
		{
		}
	}

    //[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    //public class DesignerSerializationVisibility : Attribute
    //{
    //    public DesignerSerializationVisibility(DesignerSerializationVisibility visibility)
    //    {
    //    }
    //}

    /// <summary>
    /// Dummy - Does Nothing.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class Browsable : Attribute
    {
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="browsable">Dummy - Does nothing.</param>
        public Browsable(bool browsable)
        {
        }
    }

    
    /// <summary>
    /// Dummy - Does nothing.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class ToolboxBitmap : Attribute
    {
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="type">Dummy - Does nothing.</param>
        public ToolboxBitmap(Type type)
        {
        }
    }

    /// <summary>
	/// Attribute for specifying the default value of something - for serialization.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple=false)]
	public class Category : Attribute
	{
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="category">Dummy - Does nothing.</param>
		public Category(string category)
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}

namespace KineticaRT
{
    /// <summary>
    /// Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.
    /// </summary>
	public class TypeConverter:System.ComponentModel.TypeConverter 
	{
        /// <summary>
        /// Returns whether this converter can convert an object of one type to the type of this converter. 
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
        /// <param name="sourceType">A Type that represents the type you want to convert from.</param>
        /// <returns>true if this converter can perform the conversion; otherwise, false. </returns>
		public virtual bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, Type sourceType) 
		{
			return false ;  
		}

        /// <summary>
        /// Converts the given value to the type of this converter. 
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
        /// <param name="culture">A CultureInfo. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
        /// <param name="value">The Object to convert.</param>
        /// <returns>An Object that represents the converted value. </returns>
		public virtual object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) 
		{
			return null ;
		}

        /// <summary>
        /// Converts the given value object to the specified type.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo. If a null reference (Nothing in Visual Basic) is passed, the current culture is assumed.</param>
        /// <param name="value">The Object to convert.</param>
        /// <param name="destinationType">The Type to convert the value parameter to.</param>
        /// <returns>An Object that represents the converted value. </returns>
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
        /// <summary>
        /// Returns object property information for transmission via Exchange.
        /// </summary>
        /// <param name="info">Stores all the data needed to serialize or deserialize an object for transmission via KineticaRT Exchange</param>
        /// <param name="context">Describes the source and destination of a given serialized stream</param>
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
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="defaultProperty">Dummy - Does nothing.</param>
		public DefaultProperty(string defaultProperty)
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}


	/// <summary>
    /// Stores all the data needed to serialize or deserialize an object for transmission via KineticaRT Exchange.
	/// </summary>
	public class SerializationInfo
	{
		private System.Xml.XmlNode definitionNode ;

        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
		public SerializationInfo()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="definitionNode">Dummy - Does nothing.</param>
		public SerializationInfo(System.Xml.XmlNode definitionNode)
		{
			this.definitionNode = definitionNode ;
		}

        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="name">Dummy - Does nothing.</param>
        /// <param name="value">Dummy - Does nothing.</param>
		public void AddValue(string name,KineticaRT.Compact.Values.Value value)
		{
		}
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="name">Dummy - Does nothing.</param>
        /// <param name="value">Dummy - Does nothing.</param>
		public void AddValue(string name,string value)
		{
		}
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="name">Dummy - Does nothing.</param>
        /// <param name="value">Dummy - Does nothing.</param>
		public void AddValue(string name,double value)
		{
		}
        // Removed for compact version Net2.0
        //public void AddValue(string name,Object value)
        //{
        //}
        /// <summary>
        /// Dummy - Does nothing.
        /// </summary>
        /// <param name="name">Dummy - Does nothing.</param>
        /// <param name="value">Dummy - Does nothing.</param>
		public void AddValue(string name,object value)
		{
		}

        /// <summary>
        /// Retrieves a value from the SerializationInfo store. 
        /// </summary>
        /// <param name="name">The name associated with the value to retrieve.</param>
        /// <param name="type">The Type of the value to retrieve. If the stored value cannot be converted to this type, the system will throw a InvalidCastException. </param>
        /// <returns>The object of the specified Type associated with name. </returns>
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
										System.Windows.Forms.MessageBox.Show ("Failed To construct: " + full_type) ;
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

        /// <summary>
        /// Retrieves a String value from the SerializationInfo store. 
        /// </summary>
        /// <param name="name">The name associated with the value to retrieve.</param>
        /// <returns>The String associated with name. </returns>
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
    /// Describes the source and destination of a given serialized stream.
	/// </summary>
	public class StreamingContext
	{
        /// <summary>
        /// Class constructor.
        /// </summary>
		public StreamingContext()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}

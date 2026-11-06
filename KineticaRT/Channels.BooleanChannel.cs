using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Channels
{
	/// <summary>
	/// Base class for KineticaRT Channel objects containing boolean (true/false) values.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
    public class BooleanChannel : InfoChannel, ISerializable, IBooleanChannel, ILoggerChannel, ILoggerChannelSource, IExchangeChannel 
	{
		string trueText = "True" ;
		string falseText = "False" ;

		/// Initialises a new instance of the class.
		public BooleanChannel()
		{
		}
		
		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.GetObjectData (info, context) ;
			info.AddValue ("TrueText", trueText) ;
			info.AddValue ("FalseText", falseText) ;
		}

		/// <summary>
		/// Constructor for the ISerializable interface.
		/// </summary>
		protected BooleanChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try		
			{
				trueText	= info.GetString ("TrueText") ;
				falseText	= info.GetString ("FalseText") ;
			}
			catch (System.Runtime.Serialization.SerializationException e)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, e.Message, this.GetType().ToString()) ;
			}
		}
		#endregion

        #region IExchangeChannel
        private static string GetPropertyValue(string name, System.Xml.XmlNode node)
        {
            System.Xml.XmlNode child_node = node.SelectSingleNode(name);
            if (child_node != null)
            {
                return child_node.InnerText;
            }
            return null;
        }

        /// <summary>
        /// Adds the channels live properties to the specified StringBuilder in XML format.
        /// </summary>
        /// <param name="xmlWriter">The StringBuilder to add the properties to.</param>
        public override void GetProperties(System.Xml.XmlWriter xmlWriter)
        {
            if (xmlWriter == null) throw new ArgumentNullException("xmlWriter");
            base.GetProperties(xmlWriter);
            xmlWriter.WriteElementString("TrueText", trueText);
            xmlWriter.WriteElementString("FalseText", falseText);
        }

        /// <summary>
        /// Sets the channels live properties to the values defined in the specified XmlNode.
        /// </summary>
        /// <param name="propertyNode">XmlNode containing the property values.</param>
        public override void SetProperties(System.Xml.XmlNode propertyNode)
        {
            if (propertyNode == null) throw new ArgumentNullException("propertyNode");
            base.SetProperties(propertyNode);
            trueText = GetPropertyValue("TrueText", propertyNode);
            falseText = GetPropertyValue("FalseText", propertyNode);
        }
        #endregion




		#region Conversions
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString	()
		{
			if (Value == null) return "null" ;
			if ((object)Value == (object)KineticaRT.Values.Values.NotConnected) return Value.ToString () ;
			if (Value.CompareTo(KineticaRT.Values.Values.False)==0) return falseText ;
			return trueText ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Text to display when value is true.
		/// </summary>
		/// <value>A string representing the channel when true.</value>
		[System.ComponentModel.Description("Text to display when value is true."), System.ComponentModel.Category("Boolean")] 
		[DefaultValue("True")]
		public string TrueText
		{
			get
			{
				return trueText ;
			}
			set
			{
				if (trueText != value)
				{
					trueText = value ;
					OnPropertyChanged("TrueText") ;
				}
			}
		}

		/// <summary>
		/// Text to display when value is false.
		/// </summary>
		/// <value>A string representing the channel when false.</value>
		[System.ComponentModel.Description("Text to display when value is false."), System.ComponentModel.Category("Boolean")] 
		[DefaultValue("False")]
		public string FalseText
		{
			get
			{
				return falseText ;
			}
			set
			{
				if (falseText != value)
				{
					falseText = value ;
					OnPropertyChanged("FalseText") ;
				}
			}
		}
		#endregion
    }
}
using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Channels
{
	/// <summary>
	/// Base class for a KineticaRT.Logic.Channels Channel object.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
    [System.Diagnostics.DebuggerDisplay("Name = {Name}")] //ft Shows value in the debugger's watch window
    public abstract class InfoChannel : Channel, IInfoChannel, ISerializable, IExchangeChannel
	{
		string description = "" ;
		string toolTip = "" ;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		protected InfoChannel()
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
			//Add this to derived class if required: info.AddValue ("Value", Value) ;
			info.AddValue ("Description", description) ;
			info.AddValue ("ToolTip", toolTip) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected InfoChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if (info == null) throw new ArgumentNullException("info") ;

			try		
			{
				//Value	= info.GetValue("Value", typeof(KineticaRT.Values.Value)) as KineticaRT.Values.Value;
				description = info.GetString ("Description"); 
				toolTip = info.GetString ("ToolTip"); 
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

        private string[] ExchangePropertyList = 
            {
                "Description",
                "ToolTip",
            };

        /// <summary>
        /// Returns true if the named property is one transmitted over the Exchange connection.
        /// </summary>
        /// <param name="name">Name of the property to check.</param>
        /// <returns></returns>
        public virtual bool IsExchangeProperty(string name)
        {
            foreach (string property in ExchangePropertyList)
            {
                if (property == name)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Adds the channels live properties to the specified StringBuilder in XML format.
        /// </summary>
        /// <param name="xmlWriter">The StringBuilder to add the properties to.</param>
        public virtual void GetProperties(System.Xml.XmlWriter xmlWriter)
        {
            if (xmlWriter == null) throw new ArgumentNullException("xmlWriter");
            //base.GetProperties(xmlWriter);
            xmlWriter.WriteElementString("Description", description);
            xmlWriter.WriteElementString("ToolTip", toolTip);
        }

        /// <summary>
        /// Sets the channels live properties to the values defined in the specified XmlNode.
        /// </summary>
        /// <param name="propertyNode">XmlNode containing the property values.</param>
        public virtual void SetProperties(System.Xml.XmlNode propertyNode)
        {
            if (propertyNode == null) throw new ArgumentNullException("propertyNode");
            //base.SetProperties(propertyNode);
            description = GetPropertyValue("Description", propertyNode);
            toolTip = GetPropertyValue("ToolTip", propertyNode);
        }
        #endregion

		#region Properties
		/// <summary>
		/// Gets or sets the channel description.
		/// </summary>
		/// <value>A string describing this channel.</value>
		/// <remarks>This is used by classes such as the Dial to label the object on the screen.  It should be kept fairly short.</remarks>
		[System.ComponentModel.Description("Description of channel.  Keep this short so that it will fit onto the faces of display components, a longer description can be put in the ToolTip property"), System.ComponentModel.Category("Channel")] 
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return description ;
			}
			set
			{
				if (description != value)
				{
					description = value ;
					OnPropertyChanged (DescriptionPropertyChangedEventArgs) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the tooltip for channel.
		/// </summary>
		/// <value>A string for display on tooltips.</value>
		/// <remarks>This is used by classes such as the Dial to provide a full description when the mouse hovers over the display.  You can make this as long as you wish.</remarks>
		[System.ComponentModel.Description("Tooltip for channel. This can be as long as you want."),System.ComponentModel.Category("Channel")] 
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				return toolTip ;
			}
			set
			{
				if (toolTip != value)
				{
					toolTip = value ;
					OnPropertyChanged(ToolTipPropertyChangedEventArgs) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the objects name.  The name is used to identify the object from other objects.
		/// </summary>
		/// <value>The objects name.</value>
		[System.ComponentModel.Description("Name of object.  This is used to identify the object from other objects.  E.G. in an Expression or in the Source property of a Dial."),System.ComponentModel.Category("Object")] 
		public new string Name
		{
			get	{return ((Object)this).Name ;}
			set	{((Object)this).Name = value ;}
		}
		#endregion
	}
}
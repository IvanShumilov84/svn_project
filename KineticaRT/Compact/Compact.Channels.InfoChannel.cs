using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;

namespace KineticaRT.Compact.Channels
{
	/// <summary>
	/// Base class for a KineticaRT.Logic.Channels Channel object.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
	public abstract class InfoChannel:Channel, IInfoChannel, ISerializable
	{
		string description = "" ;
		string toolTip = "" ;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		public InfoChannel()
		{
		}
		
		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		//[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
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
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, e.Message, this.GetType().ToString()) ;
			}
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
					OnPropertyChanged("Description") ;
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
					OnPropertyChanged("ToolTip") ;
				}
			}
		}

		#endregion
	}
}
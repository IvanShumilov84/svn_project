using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Channels
{
	/// <summary>
	/// Base class for KineticaRT Channel objects containing text (string) values.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
    public class TextChannel : InfoChannel, ITextChannel, ISerializable, ILoggerChannel, ILoggerChannelSource //ft Ajout ITextChannel
	{
		/// Initialises a new instance of the class.
		public TextChannel()
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
		}

		/// <summary>Constructor for the ISerializable interface.</summary>
		protected TextChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
		}
		#endregion

        //#region ILoggerChannel
        ///// <summary>
        ///// Informs the channel that historical data (data collected while the application was not running) should be retrieved and returned to the logger.
        ///// </summary>
        //void ILoggerChannel.GetHistoricalData()
        //{
        //    // No historical data handling - indicate complete.
        //    if (HistoricalComplete != null)
        //    {
        //        HistoricalComplete(this, new KineticaRT.Channels.HistoricalCompleteEventArgs());
        //    }
        //}

        ///// <summary>Occurs when a historical value change is retrieved.</summary>
        //public event LogValueChangedEventHandler LogHistoricalValueChanged;

        ///// <summary>Occurs when the live value of the Value property changes.</summary>
        ///// <remarks>This differs to a live ValueChangedEvent in that it should only include real value changes and not include any values which are created by interpolattion or filtering for reasons such as smoothing display updates.</remarks>
        //public event LogValueChangedEventHandler LogValueChanged;

        ///// <summary>
        ///// Occurs when all historical data has been retrieved.
        ///// </summary>
        //public event KineticaRT.Channels.HistoricalCompleteEventHandler HistoricalComplete;

        //#endregion

        //#region ILoggerChannelSource
        //void ILoggerChannelSource.LogValueChanged(KineticaRT.Values.LogValue newVal)
        //{
        //    if (LogValueChanged != null)
        //    {
        //        LogValueChanged(this, newVal);
        //    }
        //}
        //#endregion

    }
}
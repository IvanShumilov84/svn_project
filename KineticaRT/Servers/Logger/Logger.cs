using System;
using System.Runtime.Serialization ;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Servers.Logging
{
	/// <summary>
	/// Provides access to standard OPC parameters for all OPC item classes.
	/// </summary>
    [ChangeRecord("1.2.0.0", "10 03 2005", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2005-2006 KineticaRT Ltd.")]

    public interface ILoggerChannel : KineticaRT.Channels.IInfoChannel 
	{
		/// <summary>
		/// Indicates the ordering of the channels in the data file.
		/// -x - place in header in reverse numerical order,
		/// 0  - don't use,
		/// +x - place in each record in numerical order.
		/// </summary>
		[System.ComponentModel.Description("Indicates the ordering of the channels in the data file."), System.ComponentModel.Category("Logger")] 
		[DefaultValue("1")]
		int Index
		{
			get ;
			set ;
		}

		/// <summary>Gets or sets the Enabled property.</summary>
		/// <value>true to enable or false to disable.</value>
		bool Enabled
		{
			get ;
			set ;
		}
		
		/// <summary>
		/// The name of the channel to be logged/replayed.
		/// </summary>
		/// <value>
		/// A string representing the name of the channel to be logged/replayed.
		/// </value>
		string Source
		{
			get ;
			set ;
		}

		/// <summary>
		/// The name of the Logger.CsvFile component which records/replays data from/to this channel.
		/// </summary>
		/// <value>
		/// A string representing the name of the logger component to use.
		/// </value>
		string Logger
		{
			get ; 
			set ;
		}

        /// <summary>
        /// Gets the low triggered value of the channel.
        /// </summary>
        /// <value>Boolean value indicating if the current value is below the LowTrigger value.</value>
        /// <remarks>
        /// This value is used for triggering the logging when the associated logger is in Record mode.
        /// <para>Return false if not required.</para>
        /// </remarks>
        bool LowTriggered
        {
            get;
        }

        /// <summary>
        /// Gets the High triggered value of the channel.
        /// </summary>
        /// <value>Boolean value indicating if the current value is above the HighTrigger value.</value>
        /// <remarks>
        /// This value is used for triggering the logging when the associated logger is in Record mode.
        /// <para>Return false if not required.</para>
        /// </remarks>
        bool HighTriggered
        {
            get;
        }

	}

	/// <summary>
	/// Base class for all logger classes.
	/// </summary>
    [ChangeRecord("1.2.0.0", "10 03 2005", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2005-2006 KineticaRT Ltd.")]

    [Description("Logger")]
	[Serializable]
	[DefaultProperty("FileName")]
	public class Logger:KineticaRT.Channels.Channel, ISerializable, IConnectable 
	{
		/// <summary>
		/// The current state of the recorder.
		/// </summary>
		public enum Mode
		{
			/// <summary>
			/// The recorder is currently stopped.
			/// </summary>
			Stop,
			/// <summary>
			/// The recorder is currently replaying data.
			/// </summary>
			Play,
			/// <summary>
			/// The recorder is currently recording data.
			/// </summary>
			Record,
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Logger()
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
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected Logger (SerializationInfo info, StreamingContext context):base(info, context)
		{
		}
		#endregion

		/// <summary>
		/// Adds a channel to the channel list for recording / playback.
		/// </summary>
		/// <param name="channel">The channel to add.</param>
		public virtual void AddChannel (ILoggerChannel channel)
		{
		}

		/// <summary>
		/// Removes a channel from the list of channels for recording / playback
		/// </summary>
		/// <param name="channel">The channel to remove.</param>
		public virtual void RemoveChannel (ILoggerChannel channel)
		{
		}

		/// <summary>
		/// The current Mode of this logger
		/// </summary>
		/// <value>
		/// A Mode representing the current state.
		/// </value>
		[System.ComponentModel.Description("The current Mode of the logger."), System.ComponentModel.Category("Logger")] 
		[DefaultValue(Mode.Stop)]
		public virtual Mode CurrentMode
		{
			get 
			{
				return Mode.Stop ;
			}
		}
	}
}

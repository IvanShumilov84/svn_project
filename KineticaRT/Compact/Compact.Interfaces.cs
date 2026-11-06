using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;


namespace KineticaRT.Compact
{
    /// <summary>
    /// Provides access to the project from channels etc.
    /// </summary>
    [Copyright("Copyright 2007 KineticaRT Ltd.")]
    public interface IProject
    {
        /// <summary>
        /// Reconnects the entire project.
        /// </summary>
        void Reconnect();
    }
    
    /// <summary>
	/// Interface providing for KineticaRT objects to be found and connected together.
	/// </summary>
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public interface IConnectable
	{
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		void Connect () ;

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		void Disconnect () ;

		/// <summary>
		/// Event handler for requests from the objects children to find another object for them by name.
		/// </summary>
		event ObjectSearchEventHandler ObjectSearch ;   

		/// <summary>
		/// Returns the named Object or null if it does not exist.
		/// </summary>
		object FindObject (string objectName) ;

		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		event System.ComponentModel.PropertyChangedEventHandler PropertyChanged ;   
	}

    /// <summary>
    /// Interface providing for KineticaRT objects to be found and connected together.
    /// </summary>
    [Copyright("Copyright 2006 KineticaRT Ltd.")]
    public interface IConnectable2 : IConnectable
    {
        /// <summary>
        /// Connects this object to others, Stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        void Connect2();
    }

}

namespace KineticaRT.Compact.Channels
{
	/// <summary>
	/// Interface providing for KineticaRT channel objects to be found, connected together, set and read.
	/// </summary>
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public interface IChannel: IConnectable
	{
		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		KineticaRT.Compact.Values.Value Value
		{
			get ;
			set ;
		}
		
		/// <summary>Occurs when the value of the Value property changes.</summary>
		event ValueChangedEventHandler ValueChanged ;   
	}

	/// <summary>
	/// Interface providing Description, Tooltip and Value properties for KineticaRT channel objects of various types.
	/// </summary>
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public interface IInfoChannel: IChannel
	{
		/// <summary>
		/// Gets or sets the channels name.  The name is used to identify the channel from other objects.
		/// </summary>
		/// <value>The objects name.</value>
		[System.ComponentModel.Description("Name of channel.  This is used to identify the object from other objects.  E.G. in an Expression or in the Source property of a Dial."),System.ComponentModel.Category("Object")] 
		string Name
		{
			get	;
			set ;
		}

		/// <summary>
		/// Gets or sets the channel description.
		/// </summary>
		/// <value>A string describing this channel.</value>
		/// <remarks>This is used by classes such as the Dial to label the object on the screen.  It should be kept fairly short.</remarks>
		[System.ComponentModel.Description("Description of channel.  Keep this short so that it will fit onto the faces of display components, a longer description can be put in the ToolTip property"), System.ComponentModel.Category("Channel")] 
		string Description
		{
			get ;
			set ;
		}

		/// <summary>
		/// Gets or sets the tooltip for channel.
		/// </summary>
		/// <value>A string for display on tooltips.</value>
		/// <remarks>This is used by classes such as the Dial to provide a full description when the mouse hovers over the display.  You can make this as long as you wish.</remarks>
		[System.ComponentModel.Description("Tooltip for channel. This can be as long as you want."),System.ComponentModel.Category("Channel")] 
		string ToolTip
		{
			get ;
			set ;
		}
	}

	/// <summary>
	/// Interface providing Units, DecimalDigits, Minimum, Maximum, Format, Description, Tooltip and Value properties for numeric KineticaRT channel objects of various types.
	/// </summary>
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public interface INumericChannel: IInfoChannel
	{
		/// <summary>
		/// Gets or sets the units string for a channel.
		/// </summary>
		/// <value>A string representing the units of the channel.</value>
		/// <remarks>This property is normally used to put a units label on display controls such as the Dial.</remarks>
		/// <example>"Km"</example>
		[System.ComponentModel.Description("Units of channel.  For example Kg or m²."),System.ComponentModel.Category("Numeric")] 
		string Units
		{
			get ;
			set ;
		}

		/// <summary>
		/// Gets or sets the number of decimal digits to be displayed if displayed in number format or fixed point format.
		/// </summary>
		/// <value>The number of decimal digits to be displayed.</value>
		[System.ComponentModel.Description("Number of digits to display after the decimal point in fixed point or number formats."), System.ComponentModel.Category("Numeric")] 
		int DecimalDigits
		{
			get ;
			set ;
		}

		/// <summary>
		/// Gets or sets the minimum value of channel.
		/// </summary>
		/// <value>The minimum value of the channel.</value>
		/// <remarks>
		/// This value is normally used for setting the minimum value on a display scale.
		/// <para>No limit checking is performed so the actual Value may be lower.</para>
		/// </remarks>
		[System.ComponentModel.Description("Minimum value of channel.  This is used to set the scales of display components such as the dial.  It will not limit the values accepted by this Channel."), System.ComponentModel.Category("Numeric")] 
		double Minimum
		{
			get ;
			set ;
		}

		/// <summary>
		/// Gets or sets the maximum value of channel.
		/// </summary>
		/// <value>The maximum balue of the channel.</value>
		/// <remarks>
		/// This value is normally used for setting the maximum value on a display scale.
		/// <para>No limit checking is performed so the actual Value may be higher.</para>
		/// </remarks>
		[System.ComponentModel.Description("Maximum value of channel.  This is used to set the scales of display components such as the dial.  It will not limit the values accepted by this Channel."), System.ComponentModel.Category("Numeric")] 
		double Maximum
		{
			get ;
			set ;
		}

		/// <summary>
		/// Gets or sets the format of the channel when viewed as text.
		/// </summary>
		/// <value>A string describing the display format of the channel.</value>
		/// <remarks>The following formats can be used:
		/// <ul>
		/// <li>"C" - Currency</li>
		/// <li>"D" - Decimal</li>
		/// <li>"E" - Exponential</li>
		/// <li>"F" - Fixed Point</li>
		/// <li>"G" - General</li>
		/// <li>"N" - Number</li>
		/// <li>"P" - Percentage</li>
		/// <li>"X" - Hexadecimal</li>
		/// <li>"0.00" - Zero padding</li>
		/// <li>"#.##" - Blank padding</li>
		/// </ul>
		/// </remarks>
		[System.ComponentModel.Description("Format of channel.  This is used to convert the value to text. Eg. C,E,F,G,N"), System.ComponentModel.Category("Numeric")] 
		string Format
		{
			get ;
			set ;
		}
    }

    /// <summary>
    /// Interface to be implemented on channels which allows a KineticaRT data Logger to collect and log buffered data including
    /// data collected while the application is not running and data collected at higher sampling rates than the normal update rate.
    /// </summary>
    [Copyright("Copyright 2006 KineticaRT Ltd.")]
    public interface ILoggerChannel : KineticaRT.Compact.Channels.IChannel
    {
        /// <summary>
        /// Informs the channel that historical data (data collected while the application was not running) should be retrieved and returned to the logger.
        /// </summary>
        void GetHistoricalData();

        /// <summary>Occurs when a historical value change is retrieved.</summary>
        event LogValueChangedEventHandler LogHistoricalValueChanged;

        /// <summary>Occurs when the live value of the Value property changes.</summary>
        /// <remarks>This differs to a live ValueChangedEvent in that it should only include real value changes and not include any values which are created by interpolattion or filtering for reasons such as smoothing display updates.</remarks>
        event LogValueChangedEventHandler LogValueChanged;

        /// <summary>
        /// Occurs when all historical data has been retrieved.
        /// </summary>
        event KineticaRT.Compact.Channels.HistoricalCompleteEventHandler HistoricalComplete;
    }


    /// <summary>
    /// Allows a source of logged data to communicate with a logger channel.
    /// </summary>
    [Copyright("Copyright 2007 KineticaRT Ltd.")]
    public interface ILoggerChannelSource
    {
        /// <summary>Called when the logged value of the Value property changes.</summary>
        void LogValueChanged(KineticaRT.Compact.Values.LogValue newVal);
        /// <summary>
        /// Gets or sets if live data is logged.
        /// </summary>
        bool LogLiveValues { get; set;}
    }

    /// <summary>
    /// Interface to be implemented on channels which allows a KineticaRT data Logger to play back data through the channel.
    /// </summary>
    [Copyright("Copyright 2006 KineticaRT Ltd.")]
    public interface IPlayerChannel : KineticaRT.Compact.Channels.IChannel
    {

        /// <summary>
        /// Places the channel into play mode.
        /// </summary>
        /// <remarks>
        /// There channel should return from play mode when there have been an equal number of calls to PlayStop as to PlayStart.
        /// </remarks>
        void PlayStart();

        /// <summary>
        /// Updates the channels current play value.
        /// </summary>
        /// <param name="newValue">The new play value of the channel.</param>
        void PlaySetValue(KineticaRT.Compact.Values.Value newValue);

        /// <summary>
        /// Returns the channel from play mode.
        /// </summary>
        /// <remarks>
        /// There channel should return from play mode when there have been an equal number of calls to PlayStop as to PlayStart.
        /// If more calls are made to PlayStop than PlayStart then the extra calls should be ignored.
        /// </remarks>
        void PlayStop();
    }
    


	/// <summary>
	/// Interface providing TrueText, FalseText and Value properties for boolean KineticaRT channel objects of various types.
	/// </summary>
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public interface IBooleanChannel: IInfoChannel
	{
		/// <summary>
		/// Text to display when value is true.
		/// </summary>
		/// <value>A string representing the channel when true.</value>
		[System.ComponentModel.Description("Text to display when value is true."), System.ComponentModel.Category("Boolean")] 
		string TrueText
		{
			get ;
			set ;
		}

		/// <summary>
		/// Text to display when value is false.
		/// </summary>
		/// <value>A string representing the channel when false.</value>
		[System.ComponentModel.Description("Text to display when value is false."), System.ComponentModel.Category("Boolean")] 
		string FalseText
		{
			get ;
			set ;
		}
	}

    /// <summary>
    /// Interface to be implemented on channels which exchanges live data with similar channels via a KineticaRT Exchange server.
    /// </summary>
    [Copyright("Copyright 2007 KineticaRT Ltd.")]
    public interface IExchangeChannel
    {
        /// <summary>
        /// Adds the channels live properties to the specified StringBuilder in XML format.
        /// </summary>
        /// <param name="sb">The StringBuilder to add the properties to.</param>
        void GetProperties(System.Text.StringBuilder sb);

        /// <summary>
        /// Sets the channels live properties to the values defined in the specified XmlNode.
        /// </summary>
        /// <param name="propertyNode">XmlNode containing the property values.</param>
        void SetProperties(System.Xml.XmlNode propertyNode);
    }
}



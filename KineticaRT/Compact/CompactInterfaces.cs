//using System.Runtime.Serialization;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;


namespace KineticaRT.Compact
{
    /// <summary>
    /// Interface for the application.
    /// </summary>
    [Copyright("Copyright 2005 KineticaRT Ltd.")]
    public interface IApplication
    {
        /// <summary>Gets the Name.</summary>
        /// <value>String.</value>
        string Name
        {
            get;
        }
    }

    /// <summary>
    /// Interface providing for KineticaRT objects to be enabled/disabled.
    /// </summary>
    [Copyright("Copyright 2005 KineticaRT Ltd.")]
    public interface IEnabled
    {
        /// <summary>Gets or sets the Enabled property.</summary>
        /// <value>true to enable or false to disable.</value>
        bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Event which fires when the value of the Enabled property has changed.
        /// </summary>
        event KineticaRT.Compact.EnabledEventHandler EnabledChanged;
    }

    /// <summary>
    /// Interface providing for KineticaRT objects to be found and connected together.
    /// </summary>
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public interface IConnectable
    {
        /// <summary>
        /// Connects this object to others.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Event handler for requests from the objects children to find another object for them by name.
        /// </summary>
        event ObjectSearchEventHandler ObjectSearch;

        /// <summary>
        /// Returns the named Object or null if it does not exist.
        /// </summary>
        object FindObject(string objectName);

        /// <summary>
        /// Returns the named sibling object or null if it does not exist.
        /// </summary>
        object SearchObject(string objectName);

        /// <summary>
        /// Event which fires when the value of the objects property has changed.
        /// </summary>
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
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
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public interface IChannel : IConnectable
    {
        /// <summary>
        /// Gets or sets the channels name.  The name is used to identify the channel from other objects.
        /// </summary>
        /// <value>The objects name.</value>
        //[System.ComponentModel.Description("Name of channel.  This is used to identify the object from other objects.  E.G. in an Expression or in the Source property of a Dial."), System.ComponentModel.Category("Object")]
        string Name
        {
            get;
            set;
        }

        /// <summary>Gets or sets the value of this channel.</summary>
        /// <value>A Value object representing the value of the channel.</value>
        KineticaRT.Compact.Values.Value Value
        {
            get;
            set;
        }

        /// <summary>
        /// Converts this Value to a System.String.
        /// </summary>
        /// <returns>The result.</returns>
        string ToString(string format, IFormatProvider info);


        /// <summary>Occurs when the value of the Value property changes.</summary>
        event ValueChangedEventHandler ValueChanged;
    }

    /// <summary>
    /// Interface providing Description, Tooltip and Value properties for KineticaRT channel objects of various types.
    /// </summary>
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public interface IInfoChannel : IChannel, ISerializable
    {
        /// <summary>
        /// Gets or sets the channel description.
        /// </summary>
        /// <value>A string describing this channel.</value>
        /// <remarks>This is used by classes such as the Dial to label the object on the screen.  It should be kept fairly short.</remarks>
        [System.ComponentModel.Description("Description of channel.  Keep this short so that it will fit onto the faces of display components, a longer description can be put in the ToolTip property"), System.ComponentModel.Category("Channel")]
        string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tooltip for channel.
        /// </summary>
        /// <value>A string for display on tooltips.</value>
        /// <remarks>This is used by classes such as the Dial to provide a full description when the mouse hovers over the display.  You can make this as long as you wish.</remarks>
        [System.ComponentModel.Description("Tooltip for channel. This can be as long as you want."), System.ComponentModel.Category("Channel")]
        string ToolTip
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Interface providing Units, DecimalDigits, Minimum, Maximum, Format, Description, Tooltip and Value properties for numeric KineticaRT channel objects of various types.
    /// </summary>
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public interface INumericChannel : IInfoChannel
    {
        /// <summary>
        /// Gets or sets the units string for a channel.
        /// </summary>
        /// <value>A string representing the units of the channel.</value>
        /// <remarks>This property is normally used to put a units label on display controls such as the Dial.</remarks>
        /// <example>"Km"</example>
        [System.ComponentModel.Description("Units of channel.  For example Kg or m²."), System.ComponentModel.Category("Numeric")]
        string Units
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of decimal digits to be displayed if displayed in number format or fixed point format.
        /// </summary>
        /// <value>The number of decimal digits to be displayed.</value>
        [System.ComponentModel.Description("Number of digits to display after the decimal point in fixed point or number formats."), System.ComponentModel.Category("Numeric")]
        int DecimalDigits
        {
            get;
            set;
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
            get;
            set;
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
            get;
            set;
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
            get;
            set;
        }
    }

    /// <summary>
    /// Interface providing Threshold, Units, DecimalDigits, Minimum, Maximum, Format, Description, Tooltip and Value properties for numeric KineticaRT channel objects of various types.
    /// </summary>
    [Copyright("Copyright 2004 KineticaRT Ltd.")]
    public interface INumericThresholdChannel : INumericChannel
    {
        /// <summary>
        /// Gets or sets the LowLow threshold value of the channel.
        /// </summary>
        /// <value>The LowLow threshold value of the channel.</value>
        /// <remarks>
        /// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
        /// <para>Set to System.Double.NegativeInfinity if not required.</para>
        /// </remarks>
        [System.ComponentModel.Description("The LowLow threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")]
        double LowLow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Low threshold value of the channel.
        /// </summary>
        /// <value>The Low threshold value of the channel.</value>
        /// <remarks>
        /// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
        /// <para>Set to System.Double.NegativeInfinity if not required.</para>
        /// </remarks>
        [System.ComponentModel.Description("The Low threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")]
        double Low
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Normal threshold value of the channel (Center Point).
        /// </summary>
        /// <value>The Normal threshold value of the channel.</value>
        /// <remarks>
        /// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
        /// <para>Set to System.Double.NegativeInfinity if not required.</para>
        /// </remarks>
        [System.ComponentModel.Description("The Normal threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")]
        double Normal
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the LowLow threshold value of the channel.
        /// </summary>
        /// <value>The High threshold value of the channel.</value>
        /// <remarks>
        /// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
        /// <para>Set to System.Double.NegativeInfinity if not required.</para>
        /// </remarks>
        [System.ComponentModel.Description("The High threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")]
        double High
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the HighHigh threshold value of the channel.
        /// </summary>
        /// <value>The HighHigh threshold value of the channel.</value>
        /// <remarks>
        /// This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages.
        /// <para>Set to System.Double.NegativeInfinity if not required.</para>
        /// </remarks>
        [System.ComponentModel.Description("The HighHigh threshold value of the channel.  This value is used for displaying threshold markers and warning indicators on display components and generating threshold messages."), System.ComponentModel.Category("Threshold")]
        double HighHigh
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the background color of the normal value
        /// </summary>
        [Description("Gets or sets the color of the normal value")]
        System.Drawing.Color NormalColor
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the background color of the value when below the LowLow Threshold.
        /// </summary>
        [Description("Gets or sets the background color of the value when below the LowLow Threshold.")]
        System.Drawing.Color LowLowColor
        {
            get;
            set;
        }
        /// <summary>
        /// Gets or sets the background color of the value when below the Low Threshold.
        /// </summary>
        [Description("Gets or sets the background color of the value when below the Low Threshold.")]
        System.Drawing.Color LowColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the background color of the value when above the High Threshold.
        /// </summary>
        [Description("Gets or sets the background color of the value when above the High Threshold.")]
        System.Drawing.Color HighColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the background color of the value when above the HighHigh Threshold.
        /// </summary>
        [Description("Gets or sets the background color of the value when above the HighHigh Threshold.")]
        System.Drawing.Color HighHighColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the color.
        /// </summary>
        [Description("Gets the color.")]
        System.Drawing.Color Color
        {
            get;
        }
    }


    /// <summary>
    /// Interface providing TrueText, FalseText and Value properties for boolean KineticaRT channel objects of various types.
    /// </summary>
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public interface IBooleanChannel : IInfoChannel
    {
        /// <summary>
        /// Text to display when value is true.
        /// </summary>
        /// <value>A string representing the channel when true.</value>
        [System.ComponentModel.Description("Text to display when value is true."), System.ComponentModel.Category("Boolean")]
        string TrueText
        {
            get;
            set;
        }

        /// <summary>
        /// Text to display when value is false.
        /// </summary>
        /// <value>A string representing the channel when false.</value>
        [System.ComponentModel.Description("Text to display when value is false."), System.ComponentModel.Category("Boolean")]
        string FalseText
        {
            get;
            set;
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

}


namespace KineticaRT.Compact.Studio
{
    /// <summary>
    /// Interface for user project methods.
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// Returns the named project file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>the contents of the project file.</returns>
        System.IO.Stream GetProjectFile(string fileName);
    }

}
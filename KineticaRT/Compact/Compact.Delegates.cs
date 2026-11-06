using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;

namespace KineticaRT.Compact
{
    /// <summary>
    /// Provides data for the Enabled event.
    /// </summary>
    public class EnabledEventArgs : EventArgs
    {
        private readonly bool enabled;

        /// <summary>
        /// Constructor for class
        /// </summary>
        /// <param name="enabled"></param>
        public EnabledEventArgs(bool enabled)
        {
            this.enabled = enabled;
        }

        /// <summary>
        /// Gets a value indicating if this is an enable or a disable event.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return enabled;
            }
        }

        /// <summary>
        /// An EnabledEventArgs indicating an enable.
        /// </summary>
        public static EnabledEventArgs EnabledEvent { get { return enabledEvent; } }
        private static EnabledEventArgs enabledEvent = new EnabledEventArgs(true);
        /// <summary>
        /// An EnabledEventArgs indicating a disable.
        /// </summary>
        public static EnabledEventArgs DisabledEvent { get { return disabledEvent; } }
        private static EnabledEventArgs disabledEvent = new EnabledEventArgs(false);
    }

    /// <summary>
	/// Represents the method that will handle the ObjectSearch event fired by an object to ask its parent object to find another object for it by name.
	/// </summary>
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public delegate object ObjectSearchEventHandler(string ObjectName);

    /// <summary>
    /// Represents the method that will handle the EnabledChanged event fired on the IEnabled Interface when the Enabled Property changes.
    /// </summary>
    [Copyright("Copyright 2005 KineticaRT Ltd.")]
    //	public delegate void EnabledChangedEventHandler(object sender, bool newValue);
    public delegate void EnabledEventHandler(object sender, EnabledEventArgs e);
}

namespace KineticaRT.Compact.Channels
{
    /// <summary>Represents the method that will handle the ValueChanged event raised when the Value property is changed on a Channel object.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="newVal">The new value.</param>
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public delegate void ValueChangedEventHandler(IChannel sender, KineticaRT.Compact.Values.Value newVal);

    /// <summary>Represents the method that will handle the LogValueChanged event raised when the LogValue property is changed on a ILogChannel object.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="newVal">The new value.</param>
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public delegate void LogValueChangedEventHandler(IChannel sender, KineticaRT.Compact.Values.LogValue newVal);

    /// <summary>Represents the method that will handle the LogValueChanged event raised when the LogValue property is changed on a ILogChannel object.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The new value.</param>
    [Copyright("Copyright 2003 KineticaRT Ltd.")]
    public delegate void HistoricalCompleteEventHandler(IChannel sender, HistoricalCompleteEventArgs e);

    /// <summary>
    /// Provides data for the HistoricalComplete event.
    /// </summary>
    public class HistoricalCompleteEventArgs : EventArgs
    {
    }
}

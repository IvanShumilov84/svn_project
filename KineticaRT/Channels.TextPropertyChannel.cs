using System.Runtime.Serialization;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;

namespace KineticaRT.Channels
{
    /// <summary>
    /// TextChannel which connects to a text property of another channel and will get and set that property.
    /// </summary>
    [ChangeRecord("1.0", "05 10 2006", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2006 KineticaRT Ltd.")]

    [Serializable]
    [DefaultProperty("ChannelName")]
    public class TextPropertyChannel : TextChannel, ISerializable
    {
        /// <summary>Initializes a new instance of the class.</summary>
        public TextPropertyChannel()
        {
        }

        #region ISerializable
        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination for this serialization.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
            info.AddValue("PropertyName", propertyName);
            info.AddValue("ChannelName", channelName);
        }
        /// <summary>Constructor for the ISerializable interface.</summary>
        protected TextPropertyChannel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

            try
            {
                propertyName = info.GetString("PropertyName");
                channelName = info.GetString("ChannelName");
            }
            catch (System.Runtime.Serialization.SerializationException e)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, e.Message, this.GetType().ToString());
            }
        }
        #endregion


        #region Properties
        /// <summary>
        /// Gets or sets the name of the property this channel connects to.
        /// </summary>
        /// <value>A string representing the name of the property.</value>
        [System.ComponentModel.Description("Name of property to connect to."), System.ComponentModel.Category("Property")]
        [DefaultValue("")]
        public string PropertyName
        {
            get
            {
                return propertyName;
            }
            set
            {
                if (propertyName != value)
                {
                    propertyName = value;
                    OnPropertyChanged("PropertyName");
                }
            }
        }
        private string propertyName = "";


        /// <summary>
        /// Gets or sets the name of the Chanel containing the Property to connect to.
        /// </summary>
        /// <value>A string describing the name of the channel.</value>
        [System.ComponentModel.Description("Name of the Channel containing the Property to connect to"), System.ComponentModel.Category("Property")]
        [DefaultValue("")]
        public string ChannelName
        {
            get
            {
                return channelName;
            }
            set
            {
                if (channelName != value)
                {
                    channelName = value;
                    OnPropertyChanged("ChannelName");
                }
            }
        }
        private string channelName = "";
        #endregion


        KineticaRT.Object linkChannel ;

        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public override void Connect()
        {
            
            lock (this)
            {
                if (linkChannel != null) linkChannel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler (OnPropertyChanged);

                if (!String.IsNullOrEmpty(channelName))
                {
                    linkChannel = OnObjectSearch(channelName) as KineticaRT.Object  ;

                    if (linkChannel != null)
                    {
                        linkChannel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler (OnPropertyChanged);
                        UpdateValue ();
                    }
                    else
                    {
                        Value = KineticaRT.Values.Values.NotConnected;
                    }
                }

                base.Connect();
            }
        }

        /// <summary>
        /// Disconnects this object from others.
        /// </summary>
        public override void Disconnect()
        {
            
            lock (this)
            {
                if (Connected)
                {
                    if (linkChannel != null) linkChannel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnPropertyChanged);
                    Value = KineticaRT.Values.Values.NotConnected;
                    linkChannel = null;
                    base.Disconnect();
                }
            }
        }

        private void UpdateValue ()
        {
            System.Reflection.PropertyInfo pi;
            //System.Reflection.IReflect reflection = linkChannel as System.Reflection.IReflect;
            //if (reflection == null) return;
            if (linkChannel == null) return;
            pi = linkChannel.GetType().GetProperty(propertyName);
            if (pi == null) return;
            object pv = pi.GetValue(linkChannel, null);
            base.Value = new KineticaRT.Values.Text (System.Convert.ToString (pv));
        }

        private void OnPropertyChanged(System.Object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == propertyName)
            {
                UpdateValue();

                //System.Reflection.PropertyInfo pi;
                //System.Reflection.IReflect reflection = linkChannel as System.Reflection.IReflect;
                //pi = linkChannel.GetType().GetProperty(propertyName);
                //object pv = pi.GetValue(linkChannel, null);
                //base.Value = new KineticaRT.Values.Boolean(System.Convert.ToBoolean(pv));
            }
        }

        /// <summary>Gets or sets the value of this channel.</summary>
        /// <value>A Value object representing the value of the channel.</value>
        [System.ComponentModel.Description("The value of this channel. "), System.ComponentModel.Category("Channel")]
        public override KineticaRT.Values.Value Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (linkChannel != null)
                {
                    KineticaRT.Values.Text val = value as KineticaRT.Values.Text ;

                    if (val != null)
                    {
                        System.Reflection.PropertyInfo pi;
                        //System.Reflection.IReflect reflection = linkChannel as System.Reflection.IReflect;
                        if (linkChannel == null) return;
                        pi = linkChannel.GetType().GetProperty(propertyName);
                        if (pi == null || pi.CanWrite==false) return;
                        try
                        {
                            pi.SetValue(linkChannel, val.ToString(), null);
                        }
                        catch (System.ArgumentException exception)
                        {
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Set '" + propertyName + "' : " + exception.Message, this.GetType().ToString());
                        }
                        //base.Value = new KineticaRT.Values.Boolean(System.Convert.ToBoolean(pv));
                    }
                }

                base.Value = value;
            }
        }
    }
}


using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace KineticaRT.Channels
{
    /// <summary>
    /// NumericChannel which connects to a numeric property of another channel and will get and set that property.
    /// </summary>
    [ChangeRecord("2.0.0.1", "31 10 2007", "N. Savidge", "Added SetValue to Connect.")]
    [ChangeRecord("1.0", "05 10 2006", "N. Savidge", "Original version.")]
    [Reference(ProjectNumber = "P9790", ProjectName = "KineticaRT")]
    [Copyright("Copyright 2006 KineticaRT Ltd.")]

    [Serializable]
    [DefaultProperty("ChannelName")]
    public class PropertyLink : Channel, ISerializable
    {
        /// <summary>Initializes a new instance of the class.</summary>
        public PropertyLink()
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
            info.AddValue("SourcePropertyName", sourcePropertyName);
            info.AddValue("SourceChannelName", sourceChannelName);
            info.AddValue("DestinationChannelName", destinationChannelName);
        }
        /// <summary>Constructor for the ISerializable interface.</summary>
        protected PropertyLink(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

            try
            {
                sourcePropertyName = info.GetString("SourcePropertyName");
                sourceChannelName = info.GetString("SourceChannelName");
                destinationChannelName = info.GetString("DestinationChannelName");
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
        public string SourcePropertyName
        {
            get
            {
                return sourcePropertyName;
            }
            set
            {
                if (sourcePropertyName != value)
                {
                    sourcePropertyName = value;
                    OnPropertyChanged("SourcePropertyName");
                }
            }
        }
        private string sourcePropertyName = "";


        /// <summary>
        /// Gets or sets the name of the Chanel containing the Property to connect to.
        /// </summary>
        /// <value>A string describing the name of the channel.</value>
        [System.ComponentModel.Description("Name of the Channel containing the Property to connect to"), System.ComponentModel.Category("Property")]
        [DefaultValue("")]
        public string SourceChannelName
        {
            get
            {
                return sourceChannelName;
            }
            set
            {
                if (sourceChannelName != value)
                {
                    sourceChannelName = value;
                    OnPropertyChanged("SourceChannelName");
                }
            }
        }
        private string sourceChannelName = "";

        /// <summary>
        /// Gets or sets the name of the Channel to copy the value of the source Channel.Property to.
        /// </summary>
        /// <value>A string describing the name of the channel.</value>
        [System.ComponentModel.Description("Name of the Channel to copy the value of the source Channel.Property to"), System.ComponentModel.Category("Property")]
        [DefaultValue("")]
        public string DestinationChannelName
        {
            get
            {
                return destinationChannelName;
            }
            set
            {
                if (destinationChannelName != value)
                {
                    destinationChannelName = value;
                    OnPropertyChanged("DestinationChannelName");
                }
            }
        }
        private string destinationChannelName = "";
        private KineticaRT.Channels.IChannel destinationChannel;

        #endregion


        KineticaRT.Object linkChannel ;

        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public override void Connect()
        {
            lock (this)
            {
                if (linkChannel != null) linkChannel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnPropertyChanged);

                if (!String.IsNullOrEmpty(sourceChannelName))
                {
                    linkChannel = OnObjectSearch(sourceChannelName) as KineticaRT.Object;

                    if (linkChannel != null)
                    {
                        linkChannel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(OnPropertyChanged);
                        //Value = linkChannel.Value;
                        SetValue();
                    }
                    else
                    {
                        Value = KineticaRT.Values.Values.NotConnected;
                    }
                }

                if (!String.IsNullOrEmpty(destinationChannelName))
                {
                    destinationChannel = OnObjectSearch(destinationChannelName) as KineticaRT.Channels.IChannel;
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
                if (this.Connected)
                {
                    if (linkChannel != null) linkChannel.PropertyChanged -= new System.ComponentModel.PropertyChangedEventHandler(OnPropertyChanged);
                    Value = KineticaRT.Values.Values.NotConnected;
                    linkChannel = null;
                    
                    destinationChannel = null;
                    base.Disconnect();
                }
            }
        }
        

        private void OnPropertyChanged(System.Object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == sourcePropertyName)
            {
                //System.Reflection.PropertyInfo pi;
                ////System.Reflection.IReflect reflection = linkChannel as System.Reflection.IReflect;
                //pi = linkChannel.GetType().GetProperty(sourcePropertyName);
                //KineticaRT.Values.Value pv = pi.GetValue(linkChannel, null) as KineticaRT.Values.Value;
                //base.Value = pv;
                //if (destinationChannel != null) destinationChannel.Value = pv;
                SetValue();
            }
        }

        private void SetValue()
        {
            System.Reflection.PropertyInfo pi;
            //System.Reflection.IReflect reflection = linkChannel as System.Reflection.IReflect;
            pi = linkChannel.GetType().GetProperty(sourcePropertyName);
            KineticaRT.Values.Value pv = pi.GetValue(linkChannel, null) as KineticaRT.Values.Value;
            base.Value = pv;
            if (destinationChannel != null) destinationChannel.Value = pv;
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
                    KineticaRT.Values.Double dblval = value as KineticaRT.Values.Double;

                    if (dblval != null)
                    {
                        System.Reflection.PropertyInfo pi;
                        //System.Reflection.IReflect reflection = linkChannel as System.Reflection.IReflect;
                        pi = linkChannel.GetType().GetProperty(sourcePropertyName);
                        pi.SetValue(linkChannel, dblval.ToDouble(), null);
                        //base.Value = new KineticaRT.Values.Boolean(System.Convert.ToBoolean(pv));
                    }
                }

                base.Value = value;
            }
        }

    }
}

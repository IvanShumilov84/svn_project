using System;
using System.Collections.Generic;
using System.Text;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Soap;
using System.Xml;
using System.ComponentModel;

using KineticaRT.Compact;
using KineticaRT.Compact.Diagnostics;

namespace KineticaRT.Compact.Servers.Exchange
{
    /// <summary>
    /// Stores data in a removable storage device.
    /// </summary>
    [ChangeRecord("1.0", "01 03 2006", "N. Savidge", "Original version")]
    [Reference(ProjectNumber = "P12/011/012", ProjectName = "KineticaRT SubProject")]
    [Copyright("Copyright © KineticaRT Ltd 2006")]

    [Serializable]
    public class Client : KineticaRT.Compact.Channels.TextChannel, ISerializable, IDisposable
    {
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Exchange", "Exchange Messages");

        #region Construction
        /// <summary>
        /// Initialises a new instance of the class.
        /// </summary>
        /// <remarks>To turn off Autoplay - GPEDIT.MSC / Computer Configuration / Administrative Templates / System / Turn off Autoplay</remarks>
        public Client()
        {
        }

        private Exchange.KineticaRTExchange server;
        private bool server_connected;

        #endregion

        #region ISerializable
        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination for this serialization.</param>
        //[System.Security.Permissions.SecurityPermissionAttribute(System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
            info.AddValue("MessageChannelName", messageChannelName);
            info.AddValue("ServerUrl", serverUrl);
            info.AddValue("SystemName", systemName);
            info.AddValue("Interval", interval.ToString());
        }

        /// <summary>Constructor for the ISerializable interface.</summary> 
        protected Client(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null) throw new ArgumentNullException("info");

            try
            {
                messageChannelName = info.GetString("MessageChannelName");
                serverUrl = info.GetString("ServerUrl");
                systemName = info.GetString("SystemName");
                interval = System.Convert.ToDouble(info.GetString("Interval"));
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Configuration is missing a property for " + GetType(), this.Name);
            }
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// Destructor for class
        /// </summary>
        ~Client()
        {
            Dispose(false);
        }

        /// <summary>
        /// Closes all resources used by this class, including the update thread.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose managed resources
                }
                // free native resources
            }
            disposed = true;
        }
        private bool disposed;

        /// <summary>
        /// Closes all resources used by this class, including the update thread.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the Channel to copy the messages to.
        /// </summary>
        /// <value>A string describing the name of the channel.</value>
        [System.ComponentModel.Description("Name of the Channel to copy the messages to"), System.ComponentModel.Category("Client")]
        [DefaultValue("")]
        public string MessageChannelName
        {
            get
            {
                return messageChannelName;
            }
            set
            {
                if (messageChannelName != value)
                {
                    messageChannelName = value;
                    if (connected && !String.IsNullOrEmpty(messageChannelName))
                    {
                        messageChannel = OnObjectSearch(messageChannelName) as KineticaRT.Compact.Channels.IChannel;
                    }
                    OnPropertyChanged("MessageChannelName");
                }
            }
        }
        private string messageChannelName = "";
        private KineticaRT.Compact.Channels.IChannel messageChannel;


        /// <summary>
        /// Gets or sets the URL of the server.
        /// </summary>
        /// <value>String containing the URL.</value>
        [System.ComponentModel.Description("The URL of the Server."), System.ComponentModel.Category("Client")]
        [DefaultValue("")]
        public string ServerUrl
        {
            get { return serverUrl; }

            set
            {
                if (value == null) value = "";

                if (serverUrl != value)
                {
                    serverUrl = value;
                    OnPropertyChanged("ServerUrl");
                }
            }
        }
        //string serverUrl = "http://localhost/KineticaRT/Service.asmx";
        string serverUrl = @"http://192.168.0.3/KineticaRT/Service.asmx";

        /// <summary>
        /// Gets or sets the name of the system to connect to.
        /// </summary>
        /// <value>String containing the Name.</value>
        [System.ComponentModel.Description("The name of the System to connect to."), System.ComponentModel.Category("Client")]
        [DefaultValue("")]
        public string SystemName
        {
            get { return systemName; }
            set
            {
                if (systemName != value)
                {
                    systemName = value;
                    OnPropertyChanged("SystemName");
                }
            }
        }
        string systemName = "";

        /// <summary>
        /// How long in seconds between scanning of the channels for new values.
        /// </summary>
        [System.ComponentModel.Description("Gets or sets how long in seconds between scanning of the channels for new values."), System.ComponentModel.Category("Client")]
        [DefaultValue(1)]
        public double Interval
        {
            get
            {
                return interval;
            }
            set
            {
                if (interval != value)
                {
                    interval = value;

                    ConnectUpdater();
                    OnPropertyChanged("Interval");
                }
            }
        }
        private double interval = 1;

        #endregion

        #region Logic
        private void SetStatus(string text)
        {
            Value = new KineticaRT.Compact.Values.Text(text);
            //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, text, this.Name);
        }

        #endregion

        #region Connection
        int client_id;

        /// <summary>
        /// Connects this channel to any other channels or devices needed.
        /// </summary>
        public override void Connect()
        {
            base.Connect();

            lock (this)
            {
                if (!connected)
                {
                    connected = true;

                    ConnectServer();

                    ConnectUpdater();
                }

                if (!String.IsNullOrEmpty(messageChannelName))
                {
                    messageChannel = OnObjectSearch(messageChannelName) as KineticaRT.Compact.Channels.IChannel;
                }

                ConnectChannels();  // Must be after connected = true;
            }
        }

        private void ConnectServer()
        {
            server = new Exchange.KineticaRTExchange();
            server.Proxy = new System.Net.WebProxy();
            if (!String.IsNullOrEmpty(serverUrl)) server.Url = serverUrl;
            server_connected = false;

            try
            {
                client_id = server.Connect(systemName);
                server_connected = true;
                reconnect_server = false;
            }
            catch (System.Net.WebException ex)
            {
                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Server connection NET problem: " + ex.Message, this.Name);
                System.Diagnostics.Debug.WriteLine("Server connection NET problem: " + ex.Message, this.Name);
                server = null;
            }
            catch (System.Web.Services.Protocols.SoapException)
            {
                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Server connection SOAP problem: " + e.Message, this.Name);
                server = null;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("Server not found: " + ex.Message, this.Name);
                server = null;
            }
            catch (System.UriFormatException ex)
            {
                System.Diagnostics.Debug.WriteLine("Server error: " + ex.Message, this.Name);
                server = null;
            }
        }

        private bool connected;
        private KineticaRT.Updater.UpdateEventHandler updateHandler;

        private bool reconnect_server;

        void updater_Update()
        {
            string update_data;
            StringBuilder sb = new StringBuilder();

            lock (this)
            {
                //caSystem.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, DateTime.Now.Millisecond.ToString() + "-----", this.Name);
                // Collect new data to send.

                if (ChannelList != null)
                {
                    bool go = false;
                    sb.Append("<Data>");
                    sb.Append("<Live>");

                    for (int index = 0; index < ChannelList.Length; index++)
                    {
                        Channel channel = ChannelList[index];
                        if (channel != null)
                        {
                            string value = channel.OutValue;
                            if (value != null)
                            {
                                go = true;
                                sb.Append("<Item I='" + index.ToString() + "' V='" + value + /*"' N='" + ChannelList[index].Name + */ "' />");
                            }
                        }
                    }

                    if (go)
                    {
                        sb.Append("</Live>");
                        sb.Append("</Data>");
                        update_data = sb.ToString();
                        //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, update_data, this.Name);
                    }
                    else
                    {
                        update_data = null;
                    }
                }
                else
                {
                    if (DateTime.Now - lastChannelListRequest > new TimeSpan(0, 0, 10))
                    {
                        if (reconnect_server)
                        {
                            ConnectServer();
                        }

                        if (server_connected)
                        {
                            try
                            {
                                string channel_list_data = server.GetChannelList(client_id);

                                if (string.IsNullOrEmpty(channel_list_data))
                                {
                                }
                                else if (channel_list_data == "<Disconnect />")
                                {
                                    // The server has probably been restarted.
                                    //server_connected = false;

                                    try
                                    {
                                        client_id = server.Connect(systemName);
                                    }
                                    catch(System.Exception)
                                    {
                                    }
                                }
                                else
                                {
                                    lastChannelListRequest = DateTime.Now;
                                    //sb.Append("<Data><ChannelListRequest /></Data>");

                                    ProcessChannelList(channel_list_data);
                                }
                            }
                            catch (System.Net.WebException ex)
                            {
                                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Server connection NET problem: " + ex.Message, this.Name);
                                System.Diagnostics.Debug.WriteLine("Server connection NET problem: " + ex.Message, this.Name);
                                //server = null;
                            }
                            catch (System.Web.Services.Protocols.SoapException)
                            {
                                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Server connection SOAP problem: " + e.Message, this.Name);
                                //server = null;
                            }
                            catch (System.IO.FileNotFoundException ex)
                            {
                                System.Diagnostics.Debug.WriteLine("Server not found: " + ex.Message, this.Name);
                                //server = null;
                            }
                            catch (System.UriFormatException ex)
                            {
                                System.Diagnostics.Debug.WriteLine("Server error: " + ex.Message, this.Name);
                                //server = null;
                            }
                            catch (System.Net.Sockets.SocketException ex)
                            {
                                System.Diagnostics.Debug.WriteLine("Server error: " + ex.Message, this.Name);
                            }
                        }
                        else
                        {
                            try
                            {
                                if (server != null) client_id = server.Connect(systemName);
                            }
                            catch (System.Exception)
                            {
                            }

                            return;
                        }
                    }
                    //else
                    //{
                    update_data = null;
                    //}
                    return;
                }
            }


            // Logged data.
            //            bool repeat = true;
            //            int repeat_count = 0;
            //          while (repeat)
            //            {
            //if (repeat_count > 20)
            //{
            //    break;
            //}
            //repeat_count++;

            string logged_data = null;
            try
            {
                if (!server_connected) return;
                int queueLength;
                logged_data = server.Update(client_id, update_data, out queueLength);
                update_data = null;
            }
            catch (System.Net.WebException e)
            {
                Trace.WriteLineIf(traceSwitch.TraceWarning, e.Message, this.Name);
                reconnect_server = true ;
            }
            catch (System.InvalidOperationException e)
            {
                Trace.WriteLineIf(traceSwitch.TraceWarning, e.Message, this.Name);
                reconnect_server = true;
            }
            catch (System.NullReferenceException e)
            {
                Trace.WriteLineIf(traceSwitch.TraceWarning, e.Message, this.Name);
                reconnect_server = true;
            }
            catch (System.UriFormatException e)
            {
                Trace.WriteLineIf(traceSwitch.TraceWarning, e.Message, this.Name);
                reconnect_server = true;
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                Trace.WriteLineIf(traceSwitch.TraceWarning, e.Message, this.Name);
                reconnect_server = true;
            }

            lock(this)
            {
                if (!String.IsNullOrEmpty(logged_data))
                {
                    lastLoggedData = DateTime.Now;

                    System.Xml.XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.LoadXml(logged_data);
                        System.Xml.XmlNode root = doc.DocumentElement;
                        if (root.Name == "Packet")
                        {
                            foreach (System.Xml.XmlNode root_node in root)
                            {
                                if (root_node.Name == "Data")
                                {

                                    foreach (System.Xml.XmlNode root_child_node in root_node)
                                    {
                                        switch (root_child_node.Name)
                                        {
                                            case "Message":
                                                if (messageChannel != null)
                                                {
                                                    System.Xml.XmlAttribute timestamp_attribute = root_child_node.Attributes["Time"];
                                                    System.Xml.XmlAttribute code_attribute = root_child_node.Attributes["Code"];
                                                    System.Xml.XmlAttribute channel_attribute = root_child_node.Attributes["Channel"];
                                                    System.Xml.XmlAttribute text_attribute = root_child_node.Attributes["Text"];
                                                    System.Xml.XmlAttribute source_attribute = root_child_node.Attributes["Source"];
                                                    System.Xml.XmlAttribute priority_attribute = root_child_node.Attributes["Priority"];
                                                    try
                                                    {
                                                        if ((timestamp_attribute != null) && (code_attribute != null) && (channel_attribute != null) && (text_attribute != null) && (source_attribute != null) && (priority_attribute != null))
                                                        {
                                                            Int64 time = System.Convert.ToInt64(timestamp_attribute.Value);

                                                            DateTime timestamp = DateTime.FromFileTime(time);
                                                            string code = code_attribute.Value;
                                                            string channel = channel_attribute.Value;
                                                            string text = text_attribute.Value; ;
                                                            string source = source_attribute.Value;
                                                            Int16 priority = System.Convert.ToInt16(priority_attribute.Value);

                                                            //KineticaRT.Compact.Logic.Message.Message new_fault = new KineticaRT.Logic.Message.Fault(timestamp, code, channel, text, source, (KineticaRT.Logic.Message.PriorityCode)priority);
                                                            KineticaRT.Compact.Values.Text new_fault = new KineticaRT.Compact.Values.Text(text);
                                                            messageChannel.Value = new_fault;
                                                        }
                                                    }
                                                    catch (System.InvalidCastException)
                                                    {
                                                        //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Invalid Message format: " + root_child_node.OuterXml, this.Name);
                                                    }
                                                    catch (System.FormatException)
                                                    {
                                                        //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Invalid format in message: " + root_child_node.OuterXml, this.Name);
                                                    }
                                                }
                                                break;
                                            case "LiveUpdate":
                                            case "Live":
                                                {
                                                    if (ChannelList != null)
                                                    {
                                                        foreach (System.Xml.XmlNode scan_child_node in root_child_node.ChildNodes)
                                                        {
                                                            System.Xml.XmlAttribute id_attribute = scan_child_node.Attributes["I"];
                                                            if (id_attribute != null)
                                                            {
                                                                if (id_attribute != null)
                                                                {
                                                                    Int32 id = System.Convert.ToInt32(id_attribute.Value);

                                                                    if (id >= 0 && id < ChannelList.Length)
                                                                    {
                                                                        Channel channel = ChannelList[id];
                                                                                                                                                
                                                                        if (channel != null)
                                                                        {
                                                                            if (channel.Target != null)
                                                                            {
                                                                                System.Xml.XmlAttribute value_attribute = scan_child_node.Attributes["V"];
                                                                                if (value_attribute != null)
                                                                                {
                                                                                    string value = value_attribute.Value;

                                                                                    ((Channel)ChannelList[id]).SetTarget(value);
                                                                                }
                                                                                else
                                                                                {
                                                                                    //XmlNode message_node = scan_child_node.SelectSingleNode("Message");
                                                                                    //if (message_node != null)
                                                                                    //{
                                                                                    //    string text = "";
                                                                                    //    string source = "Unknown";
                                                                                    //    int priority = 999;
                                                                                    //    DateTime time = DateTime.Now;
                                                                                    //    XmlNode text_node = message_node.SelectSingleNode("Text");
                                                                                    //    if (text_node != null) text = text_node.InnerText;
                                                                                    //    XmlNode source_node = message_node.SelectSingleNode("Source");
                                                                                    //    if (source_node != null) source = source_node.InnerText;
                                                                                    //    XmlNode priority_node = message_node.SelectSingleNode("Priority");
                                                                                    //    if (priority_node != null) priority = System.Convert.ToInt32(priority_node.InnerText);
                                                                                    //    XmlNode time_node = message_node.SelectSingleNode("Time");
                                                                                    //    if (time_node != null) time = System.Convert.ToDateTime(time_node.InnerText);

                                                                                    //    KineticaRT.Compact.Logic.Message.Message message;
                                                                                    //    message = new KineticaRT.Compact.Logic.Message.Message(time, text, source, priority);
                                                                                    //    ((Channel)ChannelList[id]).SetTarget(message);
                                                                                    //}
                                                                                    //else
                                                                                    {
                                                                                        XmlNode value_node = scan_child_node.SelectSingleNode("Value");
                                                                                        if (value_node != null)
                                                                                        {
                                                                                            string value = value_node.InnerText;

                                                                                            ((Channel)ChannelList[id]).SetTarget(value);
                                                                                        }
                                                                                    }
                                                                                }


                                                                                System.Xml.XmlNode properties = scan_child_node.SelectSingleNode("ExchangeProperties");

                                                                                if (properties != null)
                                                                                {
                                                                                    KineticaRT.Compact.Channels.IExchangeChannel exchange_channel = channel.target as KineticaRT.Compact.Channels.IExchangeChannel;
                                                                                    if (exchange_channel != null)
                                                                                    {
                                                                                        exchange_channel.SetProperties(properties);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                break;

                                                //{
                                                //    if (ChannelList != null)
                                                //    {
                                                //        foreach (System.Xml.XmlNode scan_child_node in root_child_node.ChildNodes)
                                                //        {
                                                //            System.Xml.XmlAttribute id_attribute = scan_child_node.Attributes["I"];
                                                //            if (id_attribute != null)
                                                //            {
                                                //                System.Xml.XmlAttribute value_attribute = scan_child_node.Attributes["V"];
                                                //                if (id_attribute != null)
                                                //                {
                                                //                    Int32 id = System.Convert.ToInt32(id_attribute.Value);
                                                //                    string value = value_attribute.Value;
                                                //                    if (id >= 0 && id < ChannelList.Length)
                                                //                    {
                                                //                        Channel channel = ChannelList[id];

                                                //                        //if (channel.Name == "HHbASCInput") System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, channel.Name + ": " + value, this.Name);
                                                //                        //if (channel.Name == "HHbPLCtoPCWatchdog") System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, DateTime.Now.Millisecond.ToString() + "  "+ channel.Name + ": " + value, this.Name);

                                                //                        if (channel != null)
                                                //                        {
                                                //                            if (channel.Target != null)
                                                //                            {
                                                //                                System.Xml.XmlNode properties_node = scan_child_node.SelectSingleNode ("ExchangeProperties");

                                                //                                if (properties_node != null)
                                                //                                {
                                                //                                    KineticaRT.Compact.Channels.IExchangeChannel exchange_channel = channel.target as KineticaRT.Compact.Channels.IExchangeChannel ;
                                                //                                    if (exchange_channel != null)
                                                //                                    {
                                                //                                        exchange_channel.SetProperties(properties_node);
                                                //                                    }
                                                //                                }


                                                //                                KineticaRT.Compact.Values.Value target_value;

                                                //                                switch (channel.Type)
                                                //                                {
                                                //                                    case "KineticaRT.Channels.BooleanChannel, KineticaRT": //ChannelType.Boolean:
                                                //                                    case "KineticaRT.Compact.Channels.BooleanChannel, KineticaRT.Compact":
                                                //                                        switch (value)
                                                //                                        {
                                                //                                            case "0":
                                                //                                            case "False":
                                                //                                                target_value = KineticaRT.Compact.Values.Values.False;
                                                //                                                break;

                                                //                                            default:
                                                //                                                target_value = KineticaRT.Compact.Values.Values.True;
                                                //                                                break;

                                                //                                        }
                                                //                                        break;

                                                //                                    //case "KineticaRT.Channels.BooleanChannel": // ChannelType.Int16:
                                                //                                    case "KineticaRT.Channels.NumericChannel, KineticaRT": // ChannelType.Single:
                                                //                                    case "KineticaRT.Compact.Channels.NumericChannel, KineticaRT.Compact":
                                                //                                        try
                                                //                                        {
                                                //                                            target_value = new KineticaRT.Compact.Values.Double(System.Convert.ToDouble(value));
                                                //                                        }
                                                //                                        catch (System.FormatException)
                                                //                                        {
                                                //                                            target_value = new KineticaRT.Compact.Values.Text(value);
                                                //                                        }
                                                //                                        break;

                                                //                                    default:
                                                //                                        target_value = new KineticaRT.Compact.Values.Text(value);
                                                //                                        break;
                                                //                                }

                                                //                                channel.SetTarget(target_value);
                                                //                            }
                                                //                        }
                                                //                    }
                                                //                }
                                                //            }
                                                //            else
                                                //            {
                                                //                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Missing Id: " + scan_child_node.OuterXml, this.Name);
                                                //            }
                                                //        }
                                                //    }
                                                //}
                                                //break;
                                            case "Scan":
                                                {
                                                    if (ChannelList != null)
                                                    {
                                                        System.Xml.XmlAttribute time_attribute = root_child_node.Attributes["T"];
                                                        if (time_attribute != null)
                                                        {
                                                            try
                                                            {
                                                                DateTime sample_time = DateTime.FromFileTimeUtc(System.Int64.Parse(time_attribute.Value));

                                                                //if (Name == "TestStand") System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, root_child_node.OuterXml, this.Name);
                                                                foreach (System.Xml.XmlNode scan_child_node in root_child_node.ChildNodes)
                                                                {
                                                                    System.Xml.XmlAttribute id_attribute = scan_child_node.Attributes["I"];
                                                                    System.Xml.XmlAttribute value_attribute = scan_child_node.Attributes["V"];
                                                                    if (id_attribute != null && value_attribute != null)
                                                                    {
                                                                        Int32 id = System.Convert.ToInt32(id_attribute.Value);
                                                                        Channel channel = ChannelList[id];
                                                                        //if (channel.Name != "HHnLogTimestamp" && channel.Name != "HHnScanCount" && channel.Name != "HHst115VAnalogIn" && channel.Name != "HHst240VAnalogIn" && channel.Name != "ASnLogTimestamp" && channel.Name != "ASnScanCount") System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "LogUD: " + channel.Name + "   " + scan_child_node.OuterXml, this.Name);

                                                                        if (channel.logTarget != null)
                                                                        {
                                                                            if (channel.Name == "TSstAT1AnalogIn" || channel.Name == "HHbASCInput" || channel.Name == "ASbCAFDInput")
                                                                            {
                                                                                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "LogUpdate: " + sample_time.ToLongTimeString() + "." + sample_time.Millisecond.ToString() + channel.Name + "   " + scan_child_node.OuterXml, this.Name);
                                                                                channel.logTarget.LogValueChanged(new KineticaRT.Compact.Values.LogValue(new KineticaRT.Compact.Values.Double(value_attribute.Value), sample_time));
                                                                            }
                                                                            else
                                                                            {
                                                                                channel.logTarget.LogValueChanged(new KineticaRT.Compact.Values.LogValue(new KineticaRT.Compact.Values.Double(value_attribute.Value), sample_time));
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Missing Id: " + scan_child_node.OuterXml, this.Name);
                                                                    }
                                                                }
                                                            }
                                                            catch (System.IndexOutOfRangeException)
                                                            {
                                                                // Abandon this transmission - data must not match channel list.
                                                            }
                                                        }
                                                    }
                                                }
                                                break;

                                            default:
                                                {
                                                    //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Unknown information recieved: " + root_child_node.Name, this.Name);
                                                }
                                                break;
                                        }
                                    }
                                }
                                //else
                                //{
                                //    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Unknown packet contents recieved.", this.Name);
                                //}
                            }
                        }
                        //else
                        //{
                        //    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Unknown root information recieved.", this.Name);
                        //}
                    }
                    catch (System.Xml.XmlException)
                    {
                        //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "XML Exception: " + e.Message + "\n" + logged_data, this.Name);
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Out of range exception: " + e.Message + "\n" + logged_data, this.Name);
                    }
                }
                else
                {
                    if (ChannelList != null && DateTime.Now - lastLoggedData > DataTimeout)
                    {
                        foreach (KineticaRT.Compact.Servers.Exchange.Client.Channel channel in ChannelList)
                        {
                            channel.SetTarget(KineticaRT.Compact.Values.Values.NotConnected);
                            if (channel.logTarget != null) channel.logTarget.LogValueChanged(new KineticaRT.Compact.Values.LogValue(KineticaRT.Compact.Values.Values.NotConnected, DateTime.Now));
                        }

                        ChannelList = null;
                    }
                }
            }
        }

        TimeSpan DataTimeout = new TimeSpan(0,0,30);
        DateTime lastLoggedData = DateTime.MinValue;
        DateTime lastChannelListRequest = DateTime.MinValue;
        private Channel[] ChannelList;

        private enum ChannelType
        {
            Boolean,
            Single,
            Int16,
            String,
            Other
        }

        class Channel
        {
            public string Name;
            public string Description;
            public string ToolTip;
            public string Units;
            public int DecimalDigits;
            public double Minimum = System.Double.NaN;
            public double Maximum = System.Double.NaN;
            public string Format;
            public string Type;
            public string Serial;
            
            public KineticaRT.Compact.Channels.IChannel Target
            {
                get
                {
                    return target;
                }

                set
                {
                    target=value ;
                    if (target != null)
                    {
                        logTarget = target as KineticaRT.Compact.Channels.ILoggerChannelSource ;
                        if (logTarget != null)
                        {
                            logTarget.LogLiveValues = false;
                        }
                        target.ValueChanged += new KineticaRT.Compact.Channels.ValueChangedEventHandler(target_ValueChanged);
                        outValue = target.Value ;
                    }
                }
            }

            public KineticaRT.Compact.Channels.IChannel target;

            public KineticaRT.Compact.Channels.ILoggerChannelSource LogTarget
            {
                get
                {
                    return logTarget;
                }
            }

            public KineticaRT.Compact.Channels.ILoggerChannelSource logTarget;


            public void SetTarget(string value)
            {
                if (value == "False")
                {
                    SetTarget(KineticaRT.Compact.Values.Values.False);
                }
                else if (value == "True")
                {
                    SetTarget(KineticaRT.Compact.Values.Values.True);
                }
                else
                {
                    KineticaRT.Compact.Values.Value new_value = new KineticaRT.Compact.Values.Double(value);
                    if (!System.Double.IsNaN(new_value.ToDouble()))
                    {
                        SetTarget(new_value);
                    }
                    else
                    {
                        SetTarget(new KineticaRT.Compact.Values.Text(value));
                    }
                }
            }
            public void SetTarget(KineticaRT.Compact.Values.Value value)
            {
                //Not needed on client - may have been set by another client.
                //if (Mode != ChannelIOMode.Write)
                {
                    lock (this)
                    {
                        ignore = true;
                        if (target != null) target.Value = value;
                        ignore = false;
                    }
                }
            }

            bool ignore = false;

            void target_ValueChanged(KineticaRT.Compact.Channels.IChannel sender, KineticaRT.Compact.Values.Value newVal)
            {
                if (!ignore)
                {
                    outValue = newVal;
                }
            }


            public string OutValue
            {
                get
                {
                    if (outValue != null)
                    {
                        string return_value = outValue.ToString();
                        outValue = null;
                        return return_value;
                    }
                    else 
                    {
                        return null; 
                    }
                }
            }

            private KineticaRT.Compact.Values.Value outValue;
            public KineticaRT.Compact.Values.Value Value;
        }

        private void ConnectChannels()
        {
            if (connected && ChannelList != null)
            {
                KineticaRT.Compact.ChannelCollection parent = this.SearchObject("Parent") as KineticaRT.Compact.ChannelCollection;
                if (parent == null) return;

                foreach (Channel channel in ChannelList)
                {
                    if (channel != null)
                    {
                        channel.Target = parent.FindObject(channel.Name) as KineticaRT.Compact.Channels.IChannel;

                        if (channel.target == null)
                        {
                            KineticaRT.Compact.Object new_channel;

                            if (!string.IsNullOrEmpty(channel.Serial))
                            {
                                new_channel = null;
                                //new_channel = DeserializeChannel(channel.Serial);
                            }
                            else
                            {
                                new_channel = CreateChannel(channel.Type);
                            }

                            if (new_channel != null)
                            {
                                new_channel.Name = channel.Name;

                                parent.Add(channel.Name, new_channel);
                                channel.Target = new_channel as Channels.IChannel;
                            }
                        }
                        else
                        {
                            channel.Value = channel.Target.Value;
                        }


                        if (channel.Target != null)
                        {
                            channel.Target.Value = channel.Value;
                            KineticaRT.Compact.Channels.IInfoChannel info_channel = channel.target as KineticaRT.Compact.Channels.IInfoChannel;

                            if (info_channel != null)
                            {
                                if (string.IsNullOrEmpty(info_channel.Description))
                                {
                                    if (!string.IsNullOrEmpty(channel.Description))
                                    {
                                        info_channel.Description = channel.Description;
                                    }
                                }

                                if (string.IsNullOrEmpty(info_channel.ToolTip))
                                {
                                    if (!string.IsNullOrEmpty(channel.Description))
                                    {
                                        info_channel.ToolTip = channel.Description;
                                    }
                                }
                            }

                            KineticaRT.Compact.Channels.INumericChannel numeric_channel = channel.target as KineticaRT.Compact.Channels.INumericChannel;
                            if (numeric_channel != null)
                            {
                                if (string.IsNullOrEmpty(numeric_channel.Units))
                                {
                                    if (!string.IsNullOrEmpty(channel.Units))
                                    {
                                        numeric_channel.Units = channel.Units;
                                    }
                                }

                                if (numeric_channel.DecimalDigits == 0)
                                {
                                    if (channel.DecimalDigits != 0)
                                    {
                                        numeric_channel.DecimalDigits = channel.DecimalDigits;
                                    }
                                }

                                if (numeric_channel.Minimum == System.Double.NaN)
                                {
                                    if (channel.Minimum != System.Double.NaN)
                                    {
                                        numeric_channel.Minimum = channel.Minimum;
                                    }
                                }

                                if (numeric_channel.Maximum == System.Double.NaN)
                                {
                                    if (channel.Maximum != System.Double.NaN)
                                    {
                                        numeric_channel.Maximum = channel.Maximum;
                                    }
                                }

                                if (string.IsNullOrEmpty(numeric_channel.Format))
                                {
                                    if (!string.IsNullOrEmpty(channel.Format))
                                    {
                                        numeric_channel.Format = channel.Format;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void DisconnectChannels()
        {
            if (connected && ChannelList != null)
            {
                foreach (Channel channel in ChannelList)
                {
                    if (channel != null && channel.Target != null)
                    {
                        channel.Target.Value = KineticaRT.Compact.Values.Values.NotConnected;
                        channel.Target = null;
                    }
                }
            }
        }

        /// <summary>
        /// Disconnects this channel from all other channels and devices.
        /// </summary>
        public override void Disconnect()
        {
            DisconnectUpdater();
            Disconnect2();

            base.Disconnect();
        }

        private void Disconnect2()
        {
            lock (this)
            {
                if (connected)
                {
                    if (server != null)
                    {
                        server.Disconnect(client_id);
                        server.Dispose();
                        server = null;
                        server_connected = false;
                    }

                    messageChannel = null;

                    DisconnectChannels(); // Must be before connected = false;

                    connected = false;
                }
            }
            base.Disconnect();
        }


        private void DisconnectUpdater()
        {
            if (updater != null)
            {
                updater.Update -= updateHandler;
                updater.Dispose();
                updater = null;
                updateHandler = null;
            }
        }

        private void ConnectUpdater()
        {
            DisconnectUpdater();
            updater = new KineticaRT.Updater("KineticaRT.Servers.Exchange:" + Name, (int)(interval * 1000));
            updateHandler = new KineticaRT.Updater.UpdateEventHandler(updater_Update);
            updater.Update += updateHandler;
        }

        private Updater updater;

        #endregion

        private object fred;                   

        private void ProcessChannelList(string channelListData)
        {
            if (channelListData == null) return;

            System.Xml.XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(channelListData);
                System.Xml.XmlNode root = doc.DocumentElement;

                //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "ChannelList: " + root.InnerXml, this.Name);

                ChannelList = new Channel[root.ChildNodes.Count];
                int channel_count = 0;

                foreach (System.Xml.XmlNode scan_child_node in root.ChildNodes)
                {
                    System.Xml.XmlAttribute name_attribute = scan_child_node.Attributes["Name"];
                    System.Xml.XmlAttribute index_attribute = scan_child_node.Attributes["Index"];
                    if ((name_attribute != null) && (index_attribute != null))
                    {
                        string name = name_attribute.Value;
                        int index = System.Convert.ToInt32(index_attribute.Value);

                        if (index >= 0 && index < ChannelList.Length)
                        {
                            ChannelList[index] = new Channel();
                            fred = ChannelList[index];
                            ChannelList[index].Name = name;
                            ChannelList[index].Value = KineticaRT.Compact.Values.Values.NotConnected;
                            System.Xml.XmlNode serial_node = scan_child_node.SelectSingleNode("Serialize");
                            if (serial_node != null)
                            {
                                ChannelList[index].Serial = serial_node.InnerXml;
                            }

                            System.Xml.XmlAttribute description_attribute = scan_child_node.Attributes["Description"];
                            if (description_attribute != null)
                            {
                                ChannelList[index].Description = description_attribute.Value;
                            }

                            System.Xml.XmlAttribute units_attribute = scan_child_node.Attributes["Units"];
                            if (units_attribute != null)
                            {
                                ChannelList[index].Units = units_attribute.Value;
                            }

                            System.Xml.XmlAttribute decimal_digits_attribute = scan_child_node.Attributes["DecimalDigits"];
                            if (decimal_digits_attribute != null)
                            {
                                ChannelList[index].DecimalDigits = System.Convert.ToInt32(decimal_digits_attribute.Value);
                            }

                            System.Xml.XmlAttribute minium_attribute = scan_child_node.Attributes["Minimum"];
                            if (minium_attribute != null)
                            {
                                ChannelList[index].Minimum = System.Convert.ToDouble(minium_attribute.Value);
                            }

                            System.Xml.XmlAttribute maximum_attribute = scan_child_node.Attributes["Maximum"];
                            if (maximum_attribute != null)
                            {
                                ChannelList[index].Maximum = System.Convert.ToDouble(maximum_attribute.Value);
                            }

                            System.Xml.XmlAttribute format_attribute = scan_child_node.Attributes["Format"];
                            if (format_attribute != null)
                            {
                                ChannelList[index].Units = format_attribute.Value;
                            }
                                                                                  
                            System.Xml.XmlAttribute type_attribute = scan_child_node.Attributes["Type"];
                            if (type_attribute != null)
                            {
                                switch (type_attribute.Value)
                                {
                                    case "Boolean":
                                        ChannelList[index].Type = "KineticaRT.Channels.BooleanChannel,KineticaRT";
                                        break;

                                    case "Single":
                                        ChannelList[index].Type = "KineticaRT.Channels.NumericChannel,KineticaRT";
                                        break;

                                    case "Int16":
                                        ChannelList[index].Type = "KineticaRT.Channels.NumericChannel,KineticaRT";
                                        break;

                                    case "String":
                                        ChannelList[index].Type = "KineticaRT.Channels.TextChannel,KineticaRT";
                                        break;

                                    default:
                                        ChannelList[index].Type = type_attribute.Value;
                                        break;
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(ChannelList[index].Serial))
                                {
                                    ChannelList[index].Type = "KineticaRT.Channels.TextChannel,KineticaRT";
                                }
                            }

                            channel_count++;
                        }
                        else
                        {
                            Trace.WriteLineIf(traceSwitch.TraceWarning, "Index too big: " + name, this.Name);
                        }
                    }
                }

                KineticaRT.Compact.IProject project = OnObjectSearch("Compact.Project") as KineticaRT.Compact.IProject;
                if (project != null)project.Reconnect();  // Sould call ConnectChannels();
            }
            catch (System.Xml.XmlException e)
            {
                Trace.WriteLineIf(traceSwitch.TraceWarning, "XML Exception: " + e.Message + "\n" + channelListData, this.Name);
            }
        }

        private KineticaRT.Compact.Object CreateChannel(string channelType)
        {
            string full_type = channelType;
            string assembly_name ;
            KineticaRT.Compact.Object new_channel = null;
            System.Reflection.Assembly assembly;
            Type chan_type = null;

            if (channelType.IndexOf('%') != -1)
            {
                assembly_name = channelType.Substring(channelType.LastIndexOf("/") + 1);
                assembly_name = assembly_name.Substring(0, assembly_name.IndexOf("%"));
            }
            else
            {
                Type type = Type.GetType(channelType);
                //assembly_name = channelType.Substring(channelType.LastIndexOf(",") + 1);
                if (type != null)
                {
                    new_channel = Activator.CreateInstance(type) as KineticaRT.Compact.Object;
                    return new_channel;
                }
                else
                {
                    return null;
                }
            }

            try
            {
                assembly = System.Reflection.Assembly.Load(assembly_name);
                chan_type = assembly.GetType(full_type);
            }
            catch (System.IO.IOException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString(), "Loader");
            }

            if (chan_type != null)
            {
                Type[] types = new Type[] { typeof(SerializationInfo), typeof(StreamingContext) };
                System.Reflection.ConstructorInfo constructor_info = chan_type.GetConstructor(types);
                if (constructor_info != null)
                {
                    new_channel = constructor_info.Invoke(new System.Object[] { }) as KineticaRT.Compact.Object;
                    if (new_channel == null)
                    {
                        Trace.WriteLineIf(traceSwitch.TraceWarning, "Construct error: " + assembly_name + ", " + full_type, Name);
                    }
                }
                else
                {
                    Trace.WriteLineIf(traceSwitch.TraceWarning, "No Constructor: " + assembly_name + ", " + full_type, Name);
                }
            }
            else
            {
                Trace.WriteLineIf(traceSwitch.TraceWarning, "Type not found: " + assembly_name + ", " + full_type, Name);
            }

            return new_channel ;
        }
    }
}

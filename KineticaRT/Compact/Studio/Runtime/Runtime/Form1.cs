using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Runtime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateProject();

            //KineticaRT.Compact.Controls.Display ctrl0 = new KineticaRT.Compact.Controls.Display();
            //ctrl0.BackColor = System.Drawing.Color.LightGreen;
            //ctrl0.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            //ctrl0.Location = new System.Drawing.Point(208, 88);
            //ctrl0.Name = "Display2";
            //ctrl0.Size = new System.Drawing.Size(80, 20);
            //ctrl0.TabIndex = 1;
            //this.Controls.Add(ctrl0);

            //KineticaRT.Compact.Controls.Indicator ctrl1 = new KineticaRT.Compact.Controls.Indicator();
            //ctrl1.BackColor = System.Drawing.Color.LightGreen;
            //ctrl1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            //ctrl1.Location = new System.Drawing.Point(8, 88);
            //ctrl1.Name = "Display2";
            //ctrl1.Size = new System.Drawing.Size(80, 20);
            //ctrl1.TabIndex = 1;
            //this.Controls.Add(ctrl1);

            //KineticaRT.Compact.Controls.Button ctrl2 = new KineticaRT.Compact.Controls.Button();
            //ctrl2.BackColor = System.Drawing.Color.LightGreen;
            //ctrl2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            //ctrl2.Location = new System.Drawing.Point(108, 88);
            //ctrl2.Name = "Display2";
            //ctrl2.Size = new System.Drawing.Size(80, 20);
            //ctrl2.TabIndex = 1;
            //this.Controls.Add(ctrl2);

            //string appPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            //appPath = System.IO.Path.GetDirectoryName(appPath);

            //string[] files = System.IO.Directory.GetFiles(appPath, "*.*");
            //if (files.Length>0)
            //{
            //    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            //    doc.Load(files[0]);
            //    System.Xml.XmlNodeList list = doc.DocumentElement.SelectNodes("Panel [@Name @Text]");
            //    foreach (System.Xml.XmlNode node in list)
            //    {
            //        System.Diagnostics.Debug.WriteLine(node.Attributes["File"].Value + node.Attributes["Text"].Value);
            //    }
            //}

            //System.Reflection.Assembly assembly;

            string channelType = "KineticaRT.Panel.Home, Compact.Home";
            KineticaRT.Compact.Controls.PanelBase new_panel = null;

            Type type = Type.GetType(channelType);
            if (type != null)
            {
                new_panel = Activator.CreateInstance(type) as KineticaRT.Compact.Controls.PanelBase;

                //foreach (Control control in new_panel.Controls)
                //{
                //    control.Font = this.Font;
                //}
            }


            home1 = new_panel;// new KineticaRT.Panel.Home();
            home1.Dock = System.Windows.Forms.DockStyle.Fill;
            home1.Name = "Home";
            home1.ObjectSearch += objectSearchEventHandler;

            this.tabControl1.TabPages[0].Controls.Add(home1);
            connectList.Add(home1);

            //display1 = new KineticaRT.Panel.Display();
            //display1.Dock = System.Windows.Forms.DockStyle.Fill;
            //display1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular);
            //display1.Location = new System.Drawing.Point(0, 0);
            //display1.Name = "display1";
            //display1.Size = new System.Drawing.Size(240, 294);
            //display1.TabIndex = 0;
            ////display1.Click += new System.EventHandler(this.display1_Click);
            //display1.ObjectSearch += objectSearchEventHandler;

            //this.tabControl1.TabPages.Add(new TabPage());
            //this.tabControl1.TabPages[1].Controls.Add(display1);
            //connectList.Add(display1);

            //indicator1 = new KineticaRT.Panel.Indicator ();
            //indicator1.Dock = System.Windows.Forms.DockStyle.Fill;
            //indicator1.Name = "Indicator";
            //indicator1.ObjectSearch += objectSearchEventHandler;

            //this.tabControl1.TabPages.Add(new TabPage());
            //this.tabControl1.TabPages[2].Controls.Add(indicator1);
            //connectList.Add(indicator1);

            //this.Controls.Add(this.display1);
            
            //display1.ObjectSearch += new KineticaRT.Compact.ObjectSearchEventHandler(display1_ObjectSearch);
            //project.Connect();
            //display1.Connect();
            //display1.Connect2();
            //display1.Show();
            connectNeeded = true;
            connectTimer.Enabled = true;

        }

        //private KineticaRT.Panel.Display display1;
        //private KineticaRT.Compact.Controls.PanelBase indicator1;
        private KineticaRT.Compact.Controls.PanelBase home1;
        private System.Collections.Generic.List<KineticaRT.Compact.IConnectable> connectList = new List<KineticaRT.Compact.IConnectable>();

        //object display1_ObjectSearch(string ObjectName)
        //{
        //    return project_ObjectSearch(ObjectName);
        //}

        //private void display1_Click(object sender, EventArgs e)
        //{
        //    project.Connect();
        //}

        private Project project;
        private void CreateProject()
        {
            project = new Project ();
            //project.ObjectSearch += objectSearchEventHandler;
            project.Connecting += new Project.ConnectEventHandler(project_Connecting);

            project.CreateChannels();

            string name = System.AppDomain.CurrentDomain.FriendlyName;
            Text = name;

            objectSearchEventHandler = new KineticaRT.Compact.ObjectSearchEventHandler(project_ObjectSearch);
        }

        bool connectNeeded;
        void project_Connecting()
        {
            connectNeeded = true;
            //connectTimer.Enabled = true;
            //display1.Connect();
            //display1.Connect2();
        }

        private object project_ObjectSearch(string ObjectName)
        {
            if (ObjectName == "Compact.Project") return project;
            return project.FindObject(ObjectName);
        }

        private KineticaRT.Compact.ObjectSearchEventHandler objectSearchEventHandler;

        class Project : KineticaRT.Compact.IProject
        {
            public void Connect()
            {
                Reconnect();
            }

            /// <summary>
            /// Reconnects the entire project.
            /// </summary>
            /// <remarks>Does not reconnect the Controls immediately as they must be connected from the main application thread.</remarks>
            public void Reconnect()
            {
                
                //collection.Connect();

                //controlConnectNeeded = true;
                OnConnecting();
            }
            //bool controlConnectNeeded;

            internal void InternalConnect()
            {
                collection.Connect();
                collection.Connect2();
            }

            public delegate void ConnectEventHandler();
            /// <summary>
            /// Event for requests to find an object by name.
            /// </summary>
            public event ConnectEventHandler Connecting;
            /// <summary>
            /// Fires an ObjectSearch event for the named object
            /// </summary>
            protected void OnConnecting()
            {
                if (Connecting == null) return;
                Connecting();
            }

            public object FindObject(string objectName)
            {
                return project_ObjectSearch (objectName);
            }

            private object project_ObjectSearch(string ObjectName)
            {
                if (ObjectName == "Compact.Project") return this;
                return collection.FindObject(ObjectName);
            }

            private KineticaRT.Compact.ObjectSearchEventHandler objectSearchEventHandler;


            private KineticaRT.Compact.Servers.Exchange.Client client;
            private KineticaRT.Compact.ChannelCollection collection;
            public void CreateChannels()
            {
                objectSearchEventHandler = new KineticaRT.Compact.ObjectSearchEventHandler(project_ObjectSearch);
                collection = new KineticaRT.Compact.ChannelCollection();
                collection.ObjectSearch += objectSearchEventHandler;
                client = new KineticaRT.Compact.Servers.Exchange.Client();
                collection.Add("_Client", client);


                string AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
 
                string name = System.AppDomain.CurrentDomain.FriendlyName;
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                doc.Load(AppPath+"\\"+name + ".config");
                System.Xml.XmlNode appSettings_node = doc.DocumentElement.SelectSingleNode("appSettings");

                System.Xml.XmlNode server_url_node = appSettings_node.SelectSingleNode("add [@key='ServerUrl']");
                System.Xml.XmlAttribute server_url_attribute = server_url_node.Attributes["value"];
                client.ServerUrl = server_url_attribute.Value;

                System.Xml.XmlNode system_name_node = appSettings_node.SelectSingleNode("add [@key='SystemName']");
                System.Xml.XmlAttribute system_name_attribute = system_name_node.Attributes["value"];
                client.SystemName = system_name_attribute.Value;




                //client.ServerUrl = System.Configuration.ConfigurationManager.AppSettings["ServerUrl"];
                //client.SystemName = System.Configuration.ConfigurationManager.AppSettings["SystemName"];
                client.Interval = 0.1;
            }
        }

        KineticaRT.Compact.Channels.INumericChannel tabChannel;
        KineticaRT.Compact.Channels.IChannel tabListChannel;

        private void connectTimer_Tick(object sender, EventArgs e)
        {
            if (connectNeeded)
            {
                connectNeeded = false;
                //connectTimer.Enabled = false;
                project.InternalConnect();

                foreach (KineticaRT.Compact.IConnectable item in connectList)
                {
                    item.Connect();
                }

                //tabChannel = project.FindObject("_Client.Tab") as KineticaRT.Compact.Channels.NumericChannel;
                tabChannel = project.FindObject("Tab") as KineticaRT.Compact.Channels.INumericChannel;
                if (tabChannel != null)
                {
                    tabChannel.ValueChanged += new KineticaRT.Compact.Channels.ValueChangedEventHandler(tabChannel_ValueChanged);
                }

                tabListChannel = project.FindObject("TabList") as KineticaRT.Compact.Channels.IChannel;
                if (tabListChannel != null)
                {
                    tabListChannel.ValueChanged += new KineticaRT.Compact.Channels.ValueChangedEventHandler(tabListChannel_ValueChanged);
                }

                foreach (KineticaRT.Compact.IConnectable item in connectList)
                {
                    KineticaRT.Compact.IConnectable2 connect2 = item as KineticaRT.Compact.IConnectable2;
                    if (connect2 != null) connect2.Connect2();
                }

//                display1.Connect2();
            }

            if (newTabIndex != -1)
            {
                if (newTabIndex >= tabControl1.TabPages.Count) newTabIndex = tabControl1.TabPages.Count - 1;
                if (newTabIndex < 0) newTabIndex = newTabIndex = 0;
                this.tabControl1.SelectedIndex = newTabIndex;
                newTabIndex = -1;
            }

            if (!string.IsNullOrEmpty(newTabList))
            {
                newTabIndex = this.tabControl1.SelectedIndex;
                try
                {
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    doc.LoadXml(newTabList);

                    foreach (object obj in this.tabControl1.TabPages)
                    {
                        KineticaRT.Compact.Controls.PanelBase panel = obj as KineticaRT.Compact.Controls.PanelBase;
                        if (panel != null)
                        {
                            panel.ObjectSearch -= objectSearchEventHandler;
                            connectList.Remove(panel);
                        }
                    }
                    tabControl1.TabPages.Clear();

                    System.Xml.XmlNodeList tabs = doc.DocumentElement.SelectNodes("Tab [@Name]");
                    foreach (System.Xml.XmlNode tab in tabs)
                    {
                        string name = tab.Attributes["Name"].Value;

                        System.Diagnostics.Debug.WriteLine(name);


                        string channelType = "KineticaRT.Panel." + name + ", Compact." + name;

                        Type type = Type.GetType(channelType);
                        if (type != null)
                        {
                            KineticaRT.Compact.Controls.PanelBase new_panel = Activator.CreateInstance(type) as KineticaRT.Compact.Controls.PanelBase;
                            new_panel.Dock = System.Windows.Forms.DockStyle.Fill;
                            new_panel.Name = name;
                            new_panel.ObjectSearch += objectSearchEventHandler;
                            new_panel.Visible = true;
                            //new_panel.Font = this.Font;
                            //foreach (Control control in new_panel.Controls)
                            //{
                            //    control.Font = this.Font;
                            //}

                            TabPage new_tabpage = new TabPage();
                            new_tabpage.Name = name;
                            new_tabpage.Text = name;
                            new_tabpage.Controls.Add(new_panel);
                            this.tabControl1.TabPages.Add(new_tabpage);
                            connectList.Add(new_panel);
                        }

                    }

                    currentTabList = newTabList;
                    newTabList = null;
                    connectNeeded = true;
                }
                catch (System.Xml.XmlException exception)
                {
                    System.Diagnostics.Debug.WriteLine(exception.Message);
                }
            }
        }

        Int32 newTabIndex;

        void tabChannel_ValueChanged(KineticaRT.Compact.Channels.IChannel sender, KineticaRT.Compact.Values.Value newVal)
        {
            Int32 value = (Int32)newVal.ToDouble();
            newTabIndex = value;
        }

        string newTabList;
        string currentTabList;

        void tabListChannel_ValueChanged(KineticaRT.Compact.Channels.IChannel sender, KineticaRT.Compact.Values.Value newVal)
        {
            string new_value = newVal.ToString();
            if (new_value != currentTabList)
            {
                newTabList = new_value;
            }
        }
    }
}
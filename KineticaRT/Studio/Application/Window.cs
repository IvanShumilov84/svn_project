using System;
using System.Drawing;
using System.Diagnostics;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml;

namespace KineticaRT.Studio
{
	/// <summary>
	/// KineticaRT Studio Application Window
	/// </summary>
	public class Window : System.Windows.Forms.Form
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Studio", "Window Messages");
        
        private KineticaRT.Controls.Tab tab;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
		/// Initialises a new instance of an Application Window
		/// </summary>
		public Window()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.ResizeRedraw = true ;
			this.SetStyle(ControlStyles.UserPaint, true) ;
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true) ;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//ft
            //Set Icon
            //Icon new_icon;
            //new_icon = Icon.ExtractAssociatedIcon("VitalLink.ico");
            //this.Icon = new_icon ;

			//connect tab to server
			tab.ObjectSearch += new ObjectSearchEventHandler(OnObjectSearch) ;
        //    tab.ContextMenuCreation += new ContextMenuCreationEventHandler(tab_ContextMenuCreation);

           // this.KeyPress += new KeyPressEventHandler(Window_KeyPress);
            
		}

        //void Window_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}


                //        MenuStrip menu_strip;
                //menu_strip = new MenuStrip();


                //ToolStripMenuItem windowMenu = new ToolStripMenuItem("Window");
                //ToolStripMenuItem windowNewMenu = new ToolStripMenuItem("New", null, new EventHandler(OnClickTest));
                //windowMenu.DropDownItems.Add(windowNewMenu);
                //((ToolStripDropDownMenu)(windowMenu.DropDown)).ShowImageMargin = false;
                //((ToolStripDropDownMenu)(windowMenu.DropDown)).ShowCheckMargin = true;

                //// Assign the ToolStripMenuItem that displays 
                //// the list of child forms.
                //menu_strip.MdiWindowListItem = windowMenu;

                //// Add the window ToolStripMenuItem to the MenuStrip.
                //menu_strip.Items.Add(windowMenu);

                //// Dock the MenuStrip to the top of the form.
                //menu_strip.Dock = DockStyle.Top;


                //MainMenuStrip = menu_strip;





        private void OnClickTest(object sender, EventArgs e)
        {
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceError, "MainTest");
        }

        //void tab_ContextMenuCreation(object sender, ContextMenuCreationEventArgs e)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

		/// <summary>
		/// Paint Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnPaint(PaintEventArgs e) 
		{
			base.OnPaint (e) ;
			//Repaint(true, e.Graphics) ;
			//AddToQueue(e.Graphics.Clip.Clone()) ;
			//e.Graphics.FillRectangle(System.Drawing.Brushes.Violet, 0, 0, 1000,2000) ;
			//e.Graphics.FillRectangle(System.Drawing.Brushes.Violet, e.ClipRectangle) ;
			//AddToQueueImmediate(e.ClipRectangle) ;
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Window));
            this.tab = new KineticaRT.Controls.Tab();
            ((System.ComponentModel.ISupportInitialize)(this.tab)).BeginInit();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.CornerRadius = 5;
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.ForeColor = System.Drawing.Color.SteelBlue;
            this.tab.HorizontalGrid = new KineticaRT.Controls.Grid(true, 8, 2);
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.Panels = new KineticaRT.Controls.PanelCollection(new KineticaRT.Controls.PanelInfo[0]);
            this.tab.Size = new System.Drawing.Size(720, 342);
            this.tab.TabIndex = 0;
            this.tab.VerticalGrid = new KineticaRT.Controls.Grid(true, 8, 2);
            // 
            // Window
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(720, 342);
            this.Controls.Add(this.tab);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "New Window";
            ((System.ComponentModel.ISupportInitialize)(this.tab)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Reads details of Panels and other information from the given Stream,
		/// and passes the specified AddList on to the Panels
		/// </summary>
		/// <param name="AddList">The root element of the Add List</param>
		/// <param name="Stream">Stream to be read</param>
		/// <remarks>Adds a single empty Panel if not given a stream</remarks>
		public void Setup(XmlElement AddList, Stream Stream)
		{
			if (AddList != null)
			{
				tab.SetAddList(AddList) ;
			}

			if (Stream != null)
			{
				XmlDocument			doc			= new XmlDocument() ;
				XmlElement			root ;
				string				temp_text ;

				//open the window file as xml
				try
				{
					doc.Load(Stream) ;
					root = doc.DocumentElement ;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Window - Invalid setup stream") ;
                    return;
				}

                try
                {
                    // Appearance
					//temp_text = root.GetAttribute("ShowInTaskbar") ;
					//ShowInTaskbar = temp_text.ToLower() == "true" ;

					temp_text = root.GetAttribute("BackColor") ;
					if (temp_text.Length > 0)	tab.BackColor = (System.Drawing.Color) new System.Drawing.ColorConverter().ConvertFromInvariantString (temp_text) ;

					temp_text = root.GetAttribute("Font") ;
					if (temp_text.Length > 0)	tab.Font = (System.Drawing.Font) new System.Drawing.FontConverter().ConvertFromInvariantString (temp_text) ;

					temp_text = root.GetAttribute("ForeColor") ;
					if (temp_text.Length > 0)	tab.ForeColor = (System.Drawing.Color) new System.Drawing.ColorConverter().ConvertFromInvariantString (temp_text) ;

					temp_text = root.GetAttribute("RightToLeft") ;
					if (temp_text.Length > 0)	tab.RightToLeft = (RightToLeft)System.Int32.Parse(temp_text) ;


					// Behavior
					temp_text = root.GetAttribute("AllowDrop") ;
					if (temp_text.Length > 0)	tab.AllowDrop = System.Boolean.Parse(temp_text) ;

					temp_text = root.GetAttribute("Enabled") ;
					if (temp_text.Length > 0)	tab.Enabled = System.Boolean.Parse(temp_text) ;

					temp_text = root.GetAttribute("TabIndex") ;
					if (temp_text.Length > 0)	tab.TabIndex = System.Int32.Parse(temp_text) ;

					temp_text = root.GetAttribute("TabStop") ;
					if (temp_text.Length > 0)	tab.TabStop = System.Boolean.Parse(temp_text) ;

					//temp_text = root.GetAttribute("Visible") ;
					//if (temp_text.Length > 0)	tab.Visible = System.Boolean.Parse(temp_text) ;


					// Control
					temp_text = root.GetAttribute("BorderColor") ;
					if (temp_text.Length > 0)	tab.BorderColor = (System.Drawing.Color) new System.Drawing.ColorConverter().ConvertFromInvariantString (temp_text) ;

					temp_text = root.GetAttribute("BorderStyle") ;
                    if (temp_text.Length > 0) tab.BorderType = (KineticaRT.Controls.BorderType)System.Int32.Parse(temp_text);

					temp_text = root.GetAttribute("BorderWidth") ;
					if (temp_text.Length > 0)	tab.BorderWidth = System.Double.Parse(temp_text,System.Globalization.NumberFormatInfo.InvariantInfo) ;

					temp_text = root.GetAttribute("Name") ;
					if (temp_text.Length > 0)	tab.Name = temp_text ;

					temp_text = root.GetAttribute("Shape") ;
					if (temp_text.Length > 0)	tab.Shape = (KineticaRT.Controls.Shapes)System.Int32.Parse(temp_text) ;

					temp_text = root.GetAttribute("ToolTipText") ;
					if (temp_text.Length > 0)	tab.ToolTipText = temp_text ;

					temp_text = root.GetAttribute("UpdateSpeed") ;
					if (temp_text.Length > 0)	tab.UpdateSpeed = (KineticaRT.Controls.UpdateSpeeds)System.Int32.Parse(temp_text) ;


					// Frame.
					tab.AddListFile = root.GetAttribute("AddListFile") ;
					temp_text = root.GetAttribute("HorizontalGrid.Enabled") ;
					if (temp_text.Length > 0)	tab.HorizontalGrid.Enabled = System.Boolean.Parse(temp_text) ;
					temp_text = root.GetAttribute("HorizontalGrid.Size") ;
					if (temp_text.Length > 0)	tab.HorizontalGrid.Size = System.Int32.Parse(temp_text) ;
					temp_text = root.GetAttribute("HorizontalGrid.Spacing") ;
					if (temp_text.Length > 0)	tab.HorizontalGrid.Spacing = System.Int32.Parse(temp_text) ;
					temp_text = root.GetAttribute("VerticalGrid.Enabled") ;
					if (temp_text.Length > 0)	tab.VerticalGrid.Enabled = System.Boolean.Parse(temp_text) ;
					temp_text = root.GetAttribute("VerticalGrid.Size") ;
					if (temp_text.Length > 0)	tab.VerticalGrid.Size = System.Int32.Parse(temp_text) ;
					temp_text = root.GetAttribute("VerticalGrid.Spacing") ;
					if (temp_text.Length > 0)	tab.VerticalGrid.Spacing = System.Int32.Parse(temp_text) ;
                    temp_text = root.GetAttribute("TitleEnabled");
                    if (temp_text.Length > 0) tab.TitleEnabled = System.Boolean.Parse(temp_text);
                    temp_text = root.GetAttribute("Compact");
                    if (temp_text.Length > 0) tab.Compact = System.Boolean.Parse(temp_text);


					//set the window title - use the file name if nothing else set
					temp_text = root.GetAttribute("Title") ;
					this.Text = temp_text.Length > 0 ? temp_text : "Window" ;

					//set the size and position
					temp_text = root.GetAttribute("WindowState") ;
					if (temp_text.Length > 0)	WindowState = (System.Windows.Forms.FormWindowState)System.Int32.Parse(temp_text) ;

					temp_text = root.GetAttribute("Left") ;
					if (temp_text.Length > 0)	theOriginalSettings.Left = System.Int32.Parse(temp_text) ;

					temp_text = root.GetAttribute("Top") ;
					if (temp_text.Length > 0)	theOriginalSettings.Top = System.Int32.Parse(temp_text) ;
					
					temp_text = root.GetAttribute("Width") ;
					if (temp_text.Length > 0)	theOriginalSettings.Width = System.Int32.Parse(temp_text) ;

					temp_text = root.GetAttribute("Height") ;
					if (temp_text.Length > 0)	theOriginalSettings.Height = System.Int32.Parse(temp_text) ;

					temp_text = root.GetAttribute("CurrentPanelIndex") ;
					if (temp_text.Length > 0)	tab.CurrentPanelIndex = System.Convert.ToInt32(temp_text) ;

					Left = theOriginalSettings.Left ;
					Top = theOriginalSettings.Top ;
					Width = theOriginalSettings.Width ;
					Height = theOriginalSettings.Height ;

					if (Left < SystemInformation.VirtualScreen.Left) Left = SystemInformation.WorkingArea.Left ;
					if (Left > SystemInformation.VirtualScreen.Right) Left = SystemInformation.WorkingArea.Left ;
					if (Top < SystemInformation.VirtualScreen.Top) Top = SystemInformation.WorkingArea.Top ;
					if (Top > SystemInformation.VirtualScreen.Bottom) Top = SystemInformation.WorkingArea.Top ;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Invalid window properties");
                }

				try
				{
					ReadPanels(root) ;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Invalid window panels") ;
				}

				try
				{
					ReadTest(root) ;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Invalid window test") ;
				}
			}
			else
			{
                //tab.Panels.AddPanel("Panel 1");
                tab.Panels.AddPanel(Text);
			}
		}

		private struct originalSettings
		{
			public int Left ;
			public int Top ;
			public int Width ;
			public int Height ;
		}

		originalSettings theOriginalSettings ;

		private void ReadTest(XmlElement Root)
		{
			XmlElement		test	= Root.SelectSingleNode("Test") as XmlElement ;
			if (test != null)
			{
				string			code	= test.InnerXml ;

                // Create a compiler.
				CodeDomProvider		provider		= new Microsoft.CSharp.CSharpCodeProvider ();
				//05 ICodeCompiler		compiler		= provider.CreateCompiler () ;
				CompilerParameters	parameters		= new CompilerParameters () ;
				CompilerResults		results	;
				string				class_name		= "test" ;

                //parameters.CompilerOptions = "/define:DEBUG;TRACE";
                parameters.CompilerOptions = "";//define:TRACE";
                parameters.IncludeDebugInformation = false;
				parameters.MainClass = class_name ;

				parameters.ReferencedAssemblies.Add ("System.dll") ;
				parameters.ReferencedAssemblies.Add ("System.Drawing.dll") ;
				parameters.ReferencedAssemblies.Add ("System.Windows.Forms.dll") ;
				parameters.ReferencedAssemblies.Add (Path.Combine(System.Windows.Forms.Application.StartupPath, "KineticaRT.dll")) ;
				parameters.ReferencedAssemblies.Add (Path.Combine(System.Windows.Forms.Application.StartupPath, "KineticaRT.Controls.dll")) ;
				parameters.ReferencedAssemblies.Add (Path.Combine(System.Windows.Forms.Application.StartupPath, "KineticaRT.Controls.Frame.dll")) ;
				parameters.ReferencedAssemblies.Add (Path.Combine(System.Windows.Forms.Application.StartupPath, "KineticaRT.Controls.Tab.dll")) ;
				parameters.ReferencedAssemblies.Add (Path.Combine(System.Windows.Forms.Application.StartupPath, "KineticaRT.Studio.exe")) ;

                //05 results = compiler.CompileAssemblyFromSource(parameters, code);
                results = provider.CompileAssemblyFromSource(parameters, code);

				// Check for erorrs.
				if (results.Errors.HasErrors) 
				{
					string lcErrorMsg = "";
					// *** Create Error String
					lcErrorMsg = results.Errors.Count.ToString() + " Errors:";
					for (int x=0;x<results.Errors.Count;x++) 
					{
						lcErrorMsg = lcErrorMsg  + "\r\nLine: " + results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;		
					}

					MessageBox.Show(lcErrorMsg,"Source Compiler",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				else
				{
					object temp_obj  = results.CompiledAssembly.CreateInstance("MyNamespace.test");

					if (temp_obj != null)
					{
						temp_obj.GetType().GetMethod("Setup").Invoke(temp_obj, new object[]{tab}) ;
					}
				}
			}
		}

		/// <summary>
		/// Saves details of Panels and other information to the given Stream.
		/// Does not save the panel dlls
		/// </summary>
		/// <param name="Stream">Stream to be saved to</param>
		public void SaveSetup(Stream Stream)
		{
			XmlDocument		doc		= new XmlDocument() ;
			XmlElement		root ;
			XmlElement		elt ;

			//create the window file
			try
			{
				root = doc.CreateElement("Window") ;
				doc.AppendChild(root) ;

				// Appearance
				root.SetAttribute("BackColor", new System.Drawing.ColorConverter().ConvertToInvariantString(tab.BackColor)) ;
				root.SetAttribute("Font", new System.Drawing.FontConverter().ConvertToInvariantString(tab.Font)) ;
				root.SetAttribute("ForeColor", new System.Drawing.ColorConverter().ConvertToInvariantString(tab.ForeColor)) ;
				root.SetAttribute("RightToLeft", ((int)tab.RightToLeft).ToString()) ;

				// Behavior
				if (tab.AllowDrop) root.SetAttribute("AllowDrop", "true") ;
				if (!tab.Enabled) root.SetAttribute("Enabled", "false") ;
				root.SetAttribute("TabIndex", tab.TabIndex.ToString()) ;
				if (!tab.TabStop) root.SetAttribute("TabStop", "false") ;
				//if (!tab.Visible) root.SetAttribute("Visible", "false") ;

				// Control
				root.SetAttribute("BorderColor", new System.Drawing.ColorConverter().ConvertToInvariantString(tab.BorderColor)) ;
				root.SetAttribute("BorderStyle", ((int)tab.BorderStyle).ToString()) ;
				root.SetAttribute("BorderWidth", ((double)tab.BorderWidth).ToString(System.Globalization.NumberFormatInfo.InvariantInfo)) ;
				root.SetAttribute("Name", tab.Name) ;
				root.SetAttribute("Shape", ((int)tab.Shape).ToString()) ;
				root.SetAttribute("ToolTipText", tab.ToolTipText) ;
				root.SetAttribute("UpdateSpeed", ((int)tab.UpdateSpeed).ToString()) ;

				// Frame.

				if (!tab.HorizontalGrid.Enabled) root.SetAttribute("HorizontalGrid.Enabled", "false") ;
				root.SetAttribute("HorizontalGrid.Size", ((int)tab.HorizontalGrid.Size).ToString()) ;
				root.SetAttribute("HorizontalGrid.Spacing", ((int)tab.HorizontalGrid.Spacing).ToString()) ;
				if (!tab.VerticalGrid.Enabled) root.SetAttribute("VerticalGrid.Enabled", "false") ;
				root.SetAttribute("VerticalGrid.Size", ((int)tab.VerticalGrid.Size).ToString()) ;
				root.SetAttribute("VerticalGrid.Spacing", ((int)tab.VerticalGrid.Spacing).ToString()) ;
                if (!tab.TitleEnabled) root.SetAttribute("TitleEnabled", "false");
                if (tab.Compact) root.SetAttribute("Compact", "true");

				root.SetAttribute("Title", Text) ;

				root.SetAttribute("WindowState", ((int)WindowState).ToString()) ;
				if (WindowState == System.Windows.Forms.FormWindowState.Normal)
				{
					root.SetAttribute("Left", Left.ToString()) ;
					root.SetAttribute("Top", Top.ToString()) ;
					root.SetAttribute("Width", Width.ToString()) ;
					root.SetAttribute("Height", Height.ToString()) ;
				}
				else
				{
					root.SetAttribute("Left", theOriginalSettings.Left.ToString()) ;
					root.SetAttribute("Top", theOriginalSettings.Top.ToString()) ;
					root.SetAttribute("Width", theOriginalSettings.Width.ToString()) ;
					root.SetAttribute("Height", theOriginalSettings.Height.ToString()) ;
				}

				root.SetAttribute("CurrentPanelIndex", tab.CurrentPanelIndex.ToString()) ;
				
				foreach (KineticaRT.Controls.PanelInfo info in tab.Panels)
				{
					elt = doc.CreateElement("Panel") ;
					root.AppendChild(elt) ;

					elt.SetAttribute("File", info.DllName) ;
					elt.SetAttribute("Text", info.Text) ;
				}

				doc.Save(Stream) ;
			}
			catch (Exception e)
			{
				MessageBox.Show("Failed to create save the Window: \n" + e.Message) ;
			}
		}

		/// <summary>
		/// Saves all the defined Panel dlls
		/// </summary>
		/// <returns>
		/// True if all panels are saved, or False if one or more
		/// panels does not have a defined dll file.
		/// </returns>
		public bool SavePanels()
		{
			bool		complete		= true ;

			foreach (KineticaRT.Controls.PanelInfo info in tab.Panels)
			{
				if (info.DllName.Length > 0)
				{
					info.Panel.Save(info.DllName,tab.Compact) ;
				}
				else
				{
					complete = false ;
				}
			}

			return complete ;
		}
		#endregion (public methods)

		#region Private methods
		private void ReadPanels(XmlElement Root)
		{
			XmlNodeList			list		= Root.SelectNodes("Panel[@File]") ;
			int					index		= 0 ;
			string				text ;

			foreach (XmlElement elt in list)
			{
                text = elt.GetAttribute("Text");
                try
                {
                    tab.Panels.AddPanel((text != "") ? text : "New Panel");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error adding panel: " + text);
                }

                try
                {
                    tab.Panels[index].DllName = elt.GetAttribute("File");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + " \r\n" + e.TargetSite + " \r\n" + e.StackTrace , "Error loading panel: " + text);
                }

                index++;
            }
		}
		#endregion

		#region Object Search
		/// <summary>
		/// Event for requests to find an object by name.
		/// </summary>
		public event KineticaRT.ObjectSearchEventHandler ObjectSearch;   

		private object OnObjectSearch(string Name)
		{
			//pass the request on as we can't find it ourselves
			if (ObjectSearch != null)
			{
				return ObjectSearch(Name) ;
			}
			return null ;
		}

		/// <summary>
		/// Gets the Object with the specified name
		/// </summary>
		/// <param name="name">The name of the Object to get</param>
		/// <returns>The Object with the specifed name</returns>
		public object FindObject(string name)
		{
			return tab.FindObject (name) ;
		}
		#endregion

		#region Connection
        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public void Connect()
        {
            tab.Connect();
        }

        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public void Connect2()
        {
            tab.Connect2();
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public void Disconnect ()
		{
			tab.Disconnect() ;
		}
		#endregion (connection)

		#region UNUSED: Combo navigation stuff
//		//Default is 'Tab'. Other recognised style is 'Combo'
//		private void SetPageNavigation(string Style)
//		{
//			if (Style == "Combo")
//			{
//				//show the combo box, hide the headers
//				tab.Height = ClientRectangle.Height - panelCombo.Height ;
//				panelCombo.Visible = true ;
//				//tab.TitleEnabled = false ;
//
//				//fill the combo box
//				foreach (Controls.PanelInfo info in tab.Panels)
//				{
//					panelCombo.Items.Add(info) ;
//				}
//				panelCombo.DisplayMember = "Text" ;
//				if (panelCombo.Items.Count > tab.CurrentPanel)
//				{
//					panelCombo.SelectedIndex = tab.CurrentPanel ;
//				}
//				panelCombo.SelectedIndexChanged += new EventHandler(OnComboItemChanged) ;
//
//				tab.PropertyChanged += new PropertyChangedEventHandler(OnTabPropertyChanged) ;
//				tab.Panels.PanelAdded += new Controls.PanelCollection.PanelChangedEventHandler(OnPanelAdded) ;
//				tab.Panels.PanelRemoved += new Controls.PanelCollection.PanelChangedEventHandler(OnPanelRemoved) ;
//				tab.Panels.PropertyChanged += new PropertyChangedEventHandler(OnPanelPropertyChanged) ;
//			}
//			else		//'Tab'
//			{
//				//nothing to do - default settings are correct
//
//				//NB: if ever allow this to be called after initial setup,
//				//must reset everything here
//			}
//		}
//
//		protected override void OnResize(EventArgs e)
//		{
//			tab.Height = (panelCombo.Visible) ? ClientRectangle.Height - panelCombo.Height : ClientRectangle.Height ;
//			base.OnResize(e) ;
//		}
//
//		private void OnComboItemChanged(object sender, EventArgs e)
//		{
//			tab.CurrentPanel = panelCombo.SelectedIndex ;
//		}
//
//		private void OnTabPropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//			if (e.PropertyName == "CurrentPanel")
//			{
//				if (panelCombo.Items.Count > tab.CurrentPanel)
//				{
//					panelCombo.SelectedIndex = tab.CurrentPanel ;
//				}
//			}
//			else if (e.PropertyName == "Panels")
//			{
//				tab.Panels.PanelAdded += new Controls.PanelCollection.PanelChangedEventHandler(OnPanelAdded) ;
//				tab.Panels.PanelRemoved += new Controls.PanelCollection.PanelChangedEventHandler(OnPanelRemoved) ;
//
//				panelCombo.Items.Clear() ;
//				foreach (Controls.PanelInfo info in tab.Panels)
//				{
//					panelCombo.Items.Add(info) ;
//				}
//			}
//		}
//
//		private void OnPanelAdded(object sender, string PanelName)
//		{
//			foreach (Controls.PanelInfo info in tab.Panels)
//			{
//				if (info.Name == PanelName)
//				{
//					if (!panelCombo.Items.Contains(info))
//					{
//						panelCombo.Items.Add(info) ;
//					}
//					break ;
//				}
//			}
//
//			//ensure current panel is still selected
//			if (panelCombo.Items.Count > tab.CurrentPanel)
//			{
//				panelCombo.SelectedIndex = tab.CurrentPanel ;
//			}
//		}
//
//		private void OnPanelRemoved(object sender, string PanelName)
//		{
//			foreach (Controls.PanelInfo info in tab.Panels)
//			{
//				if (info.Name == PanelName)
//				{
//					panelCombo.Items.Remove(info) ;
//					break ;
//				}
//			}
//			
//			//ensure current panel is still selected
//			if (panelCombo.Items.Count > tab.CurrentPanel)
//			{
//				panelCombo.SelectedIndex = tab.CurrentPanel ;
//			}
//		}
//
//		private void OnPanelPropertyChanged(object sender, PropertyChangedEventArgs e)
//		{
//			if (sender is Controls.PanelInfo && e.PropertyName == "Text")
//			{
//				int			index = panelCombo.Items.IndexOf(sender) ;
//
//				panelCombo.Items.RemoveAt(index) ;
//				panelCombo.Items.Insert(index, sender) ;
//
//				if (panelCombo.Items.Count > tab.CurrentPanel)
//				{
//					panelCombo.SelectedIndex = tab.CurrentPanel ;
//				}
//			}
//			else
//			{
//			}
//		}
		#endregion

		#region Properties
//		/// <summary>Gets or sets the name of the window.</summary>
//		/// <value>A string containing the name of the womdpw.</value>
//		public string Name
//		{
//			get
//			{
//				return name ;
//			}
//			set
//			{
//				if (name != value)
//				{
//					name = value ;
//					OnPropertyChanged("Name") ;
//				}
//			}
//		}
//		private string name = "" ;

//		private void SetVisible3 ()
//		{
//			base.Show() ;
//		}
		private void SetVisible ()
		{
			base.Visible = true ;
		}
		private void SetInvisible ()
		{
			base.Visible = false ;
		}
		/// <summary>
		/// Gets or sets the visibility of the window.
		/// </summary>
		public new bool Visible
		{
			get
			{
				return base.Visible ;
			}
			set
			{
				if (value)
				{
					if (this.IsHandleCreated) BeginInvoke (new System.Windows.Forms.MethodInvoker(this.SetVisible)) ;
				}
				else
				{
					if (this.IsHandleCreated) BeginInvoke (new System.Windows.Forms.MethodInvoker(this.SetInvisible)) ;
				}
			}
		}
			
			//else base.BeginInvoke (new System.Windows.Forms.MethodInvoker(this.SetVisible3)) ;
			//			if (this.InvokeRequired)
//			{
				//BeginInvoke (new System.Windows.Forms.MethodInvoker(this.SetVisible)) ;
//			}
//			else
//			{
				//base.Visible = true ;
//			}
//		public void SetInvisible ()
//		{
//			if (this.IsHandleCreated) BeginInvoke (new System.Windows.Forms.MethodInvoker(this.SetInvisible2)) ;
////			if (this.InvokeRequired)
////			{
//				//BeginInvoke (new System.Windows.Forms.MethodInvoker(this.SetInvisible)) ;
////			}
////			else
////			{
//				//base.Visible = false ;
////			}
//		}
		#endregion

        //#region Menu event stuff
        //public event ContextMenuCreationEventHandler ContextMenuCreation;
        //private void OnContextMenuCreation(ContextMenuCreationEventArgs e) { if (ContextMenuCreation != null) ContextMenuCreation(this, e); }
        //#endregion
        /// <summary>
        /// Gets or sets the windows context menu.
        /// </summary>
        public override ContextMenu ContextMenu
        {
            get
            {
                return tab.ContextMenu;
            }
            set
            {
                tab.ContextMenu = value;
            }
        }
    }
}

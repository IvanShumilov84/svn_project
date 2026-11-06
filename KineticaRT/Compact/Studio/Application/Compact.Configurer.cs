using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32 ;

namespace KineticaRT.Compact.Studio.Application
{
	/// <summary>
	/// KineticaRT Studio Application Configurer
	/// </summary>
	/// <remarks>
	/// Note that any dlls required for controls on panels must
	/// be in the application directory
	/// </remarks>
	public class Configurer : System.Windows.Forms.Form
	{
		#region Variables
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Project				proj ;

		private OpenFileDialog		openDialog;
		private MainMenu 			configMenu;
		private MenuItem 			fileMenu;
		private MenuItem 			fileMenuSave;
		private MenuItem			fileMenuBar1;
		private MenuItem 			fileMenuExit;
		private MenuItem 			projectMenu;
		private MenuItem 			projectMenuExplorer;
		private MenuItem 			projectMenuConfigurer;
		private MenuItem			projectMenuClosedown;
		private MenuItem 			projectMenuBar1;
		private MenuItem 			projectMenuSave;
		private MenuItem 			projectMenuBar2;
		private MenuItem 			projectMenuConnect;
		private MenuItem 			projectMenuDisconnect;
		private MenuItem 			projectMenuBar3;
		private MenuItem 			projectMenuTopmost;
		private MenuItem 			windowMenu;
		private MenuItem 			windowMenuAdd;
		private MenuItem 			windowMenuNew;
		private MenuItem 			windowMenuBar1;
		private MenuItem 			windowSubMenu;
		private MenuItem 			windowSubMenuRename;
		private MenuItem 			windowSubMenuProperties;
		private MenuItem 			windowSubMenuSelect;
		private MenuItem 			windowSubMenuBar1;
		private MenuItem 			windowSubMenuSave;
		private MenuItem 			windowSubMenuSaveAs;
		private MenuItem 			windowSubMenuBar2;
		private MenuItem 			windowSubMenuRemove;
		private MenuItem 			channelsMenu;
		private MenuItem 			channelsMenuAdd;
		private MenuItem 			channelsMenuNew;
		private MenuItem 			channelsMenuBar1;
		private MenuItem 			channelsSubMenu;
		private MenuItem 			channelsSubMenuRename;
		private MenuItem			channelsSubMenuEdit;
		private MenuItem 			channelsSubMenuBar1;
		private MenuItem 			channelsSubMenuSave;
		private MenuItem 			channelsSubMenuSaveAs;
		private MenuItem 			channelsSubMenuBar2;
		private MenuItem 			channelsSubMenuRemove;
        //private MenuItem 			sequenceMenu;
        //private MenuItem 			sequenceMenuAdd;
        //private MenuItem 			sequenceMenuNew;
        //private MenuItem 			sequenceMenuBar1;
		private MenuItem 			sequenceSubMenu;
		private MenuItem 			sequenceSubMenuRename;
		private MenuItem			sequenceSubMenuEdit;
		private MenuItem 			sequenceSubMenuBar1;
		private MenuItem 			sequenceSubMenuSave;
		private MenuItem 			sequenceSubMenuSaveAs;
		private MenuItem 			sequenceSubMenuBar2;
		private MenuItem 			sequenceSubMenuRemove;
		private MenuItem 			helpMenu;
		private MenuItem 			helpMenuHelp;
		private MenuItem 			helpMenuOnline;
		private MenuItem 			helpMenuBar1;
		private MenuItem 			helpMenuAbout;
		private MenuItem 			helpMenuOnlineStudio;
		private MenuItem 			helpMenuOnlineKineticaRT;
		private MenuItem 			helpMenuOnlineSupport;
		private ListView 			messageList;
		private ColumnHeader 		timeHeader;
		private System.Windows.Forms.MenuItem userMenu;
		private System.Windows.Forms.MenuItem userMenuEditing;
		private ColumnHeader 		messageHeader;
		private System.Windows.Forms.MenuItem userMenuConfigurer;
		private System.Windows.Forms.MenuItem userMenuExplorer;
		
		private bool				editingEnabled = true ;
		private bool				explorerEnabled = true ;
		private bool				configurerEnabled = true ;
		#endregion (variables)

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Configurer associated with the 
		/// specified Project
		/// </summary>
		/// <param name="Project">Project with which this is associated</param>
		public Configurer(Project Project)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			write = new WriteDelegate(Write) ;
			writeLine = new WriteLineDelegate(WriteLine) ;

			ListTraceListener		listener = new ListTraceListener() ;
			listener.SetDetination(this) ;
			Trace.Listeners.Add(listener) ;

			proj = Project ;
		}

		private ListViewItem	item		= null ;
		private WriteDelegate write ;
		delegate void WriteDelegate (string message) ;
		/// <summary>
		/// Writes a message to the listener, concatenated onto the previous message unless 'WriteLine' is called
		/// </summary>
		/// <param name="message">Message to write</param>
		public void Write(string message)
		{
			if (this.InvokeRequired) 
			{
				this.BeginInvoke (writeLine, new object[] {message});
			}
			else 
			{
				if (messageList != null)
				{
					if (item == null)
					{
						item	= new ListViewItem(DateTime.Now.ToString()) ;
						item.SubItems.Add(message) ;
						messageList.Items.Insert(0, item) ;
					}
					else	//just concatenate the text
					{
						item.SubItems[0].Text += message ;
					}
				}
			}
		}

		private WriteLineDelegate writeLine ;
		delegate void WriteLineDelegate (string message) ;
		/// <summary>
		/// Writes a complete message to the listener on a new line
		/// </summary>
		/// <param name="message">Message to write</param>
		public void WriteLine(string message)
		{
			if (this.InvokeRequired) 
			{
				this.BeginInvoke (writeLine, new object[] {message});
			}
			else 
			{
				if (messageList != null)
				{
					if (item == null)
					{
						item	= new ListViewItem(DateTime.Now.ToString()) ;
						item.SubItems.Add(message) ;
						messageList.Items.Insert(0, item) ;
					}
					else	//just concatenate the text
					{
						item.SubItems[0].Text += message ;
					}

					//clear the item so the next one goes on a new line
					item = null ;
				}
			}
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
		#endregion (initialisation)

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Configurer));
			this.configMenu = new System.Windows.Forms.MainMenu();
			this.fileMenu = new System.Windows.Forms.MenuItem();
			this.fileMenuSave = new System.Windows.Forms.MenuItem();
			this.fileMenuBar1 = new System.Windows.Forms.MenuItem();
			this.fileMenuExit = new System.Windows.Forms.MenuItem();
			this.projectMenu = new System.Windows.Forms.MenuItem();
			this.projectMenuConfigurer = new System.Windows.Forms.MenuItem();
			this.projectMenuExplorer = new System.Windows.Forms.MenuItem();
			this.projectMenuClosedown = new System.Windows.Forms.MenuItem();
			this.projectMenuBar1 = new System.Windows.Forms.MenuItem();
			this.projectMenuConnect = new System.Windows.Forms.MenuItem();
			this.projectMenuDisconnect = new System.Windows.Forms.MenuItem();
			this.projectMenuBar2 = new System.Windows.Forms.MenuItem();
			this.projectMenuSave = new System.Windows.Forms.MenuItem();
			this.projectMenuBar3 = new System.Windows.Forms.MenuItem();
			this.projectMenuTopmost = new System.Windows.Forms.MenuItem();
			this.windowMenu = new System.Windows.Forms.MenuItem();
			this.windowMenuAdd = new System.Windows.Forms.MenuItem();
			this.windowMenuNew = new System.Windows.Forms.MenuItem();
			this.windowMenuBar1 = new System.Windows.Forms.MenuItem();
			this.channelsMenu = new System.Windows.Forms.MenuItem();
			this.channelsMenuAdd = new System.Windows.Forms.MenuItem();
			this.channelsMenuNew = new System.Windows.Forms.MenuItem();
			this.channelsMenuBar1 = new System.Windows.Forms.MenuItem();
            //this.sequenceMenu = new System.Windows.Forms.MenuItem();
            //this.sequenceMenuAdd = new System.Windows.Forms.MenuItem();
            //this.sequenceMenuNew = new System.Windows.Forms.MenuItem();
            //this.sequenceMenuBar1 = new System.Windows.Forms.MenuItem();
			this.userMenu = new System.Windows.Forms.MenuItem();
			this.userMenuConfigurer = new System.Windows.Forms.MenuItem();
			this.userMenuEditing = new System.Windows.Forms.MenuItem();
			this.userMenuExplorer = new System.Windows.Forms.MenuItem();
			this.helpMenu = new System.Windows.Forms.MenuItem();
			this.helpMenuHelp = new System.Windows.Forms.MenuItem();
			this.helpMenuOnline = new System.Windows.Forms.MenuItem();
			this.helpMenuOnlineStudio = new System.Windows.Forms.MenuItem();
			this.helpMenuOnlineKineticaRT = new System.Windows.Forms.MenuItem();
			this.helpMenuOnlineSupport = new System.Windows.Forms.MenuItem();
			this.helpMenuBar1 = new System.Windows.Forms.MenuItem();
			this.helpMenuAbout = new System.Windows.Forms.MenuItem();
			this.windowSubMenu = new System.Windows.Forms.MenuItem();
			this.windowSubMenuRename = new System.Windows.Forms.MenuItem();
			this.windowSubMenuProperties = new System.Windows.Forms.MenuItem();
			this.windowSubMenuSelect = new System.Windows.Forms.MenuItem();
			this.windowSubMenuBar1 = new System.Windows.Forms.MenuItem();
			this.windowSubMenuSave = new System.Windows.Forms.MenuItem();
			this.windowSubMenuSaveAs = new System.Windows.Forms.MenuItem();
			this.windowSubMenuBar2 = new System.Windows.Forms.MenuItem();
			this.windowSubMenuRemove = new System.Windows.Forms.MenuItem();
			this.channelsSubMenu = new System.Windows.Forms.MenuItem();
			this.channelsSubMenuRename = new System.Windows.Forms.MenuItem();
			this.channelsSubMenuEdit = new System.Windows.Forms.MenuItem();
			this.channelsSubMenuBar1 = new System.Windows.Forms.MenuItem();
			this.channelsSubMenuSave = new System.Windows.Forms.MenuItem();
			this.channelsSubMenuSaveAs = new System.Windows.Forms.MenuItem();
			this.channelsSubMenuBar2 = new System.Windows.Forms.MenuItem();
			this.channelsSubMenuRemove = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenu = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenuRename = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenuEdit = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenuBar1 = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenuSave = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenuSaveAs = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenuBar2 = new System.Windows.Forms.MenuItem();
			this.sequenceSubMenuRemove = new System.Windows.Forms.MenuItem();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.messageList = new System.Windows.Forms.ListView();
			this.timeHeader = new System.Windows.Forms.ColumnHeader();
			this.messageHeader = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// configMenu
			// 
			this.configMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.fileMenu,
																					   this.projectMenu,
																					   this.windowMenu,
																					   this.channelsMenu,
//																					   this.sequenceMenu,
																					   this.userMenu,
																					   this.helpMenu});
			// 
			// fileMenu
			// 
			this.fileMenu.Index = 0;
			this.fileMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.fileMenuSave,
																					 this.fileMenuBar1,
																					 this.fileMenuExit});
			this.fileMenu.Text = "&File";
			// 
			// fileMenuSave
			// 
			this.fileMenuSave.Index = 0;
			this.fileMenuSave.Text = "&Save All";
			this.fileMenuSave.Click += new System.EventHandler(this.OnClickFileMenuSave);
			// 
			// fileMenuBar1
			// 
			this.fileMenuBar1.Index = 1;
			this.fileMenuBar1.Text = "-";
			// 
			// fileMenuExit
			// 
			this.fileMenuExit.Index = 2;
			this.fileMenuExit.Text = "&Exit";
			this.fileMenuExit.Click += new System.EventHandler(this.OnClickFileMenuExit);
			// 
			// projectMenu
			// 
			this.projectMenu.Index = 1;
			this.projectMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.projectMenuConfigurer,
																						this.projectMenuExplorer,
																						this.projectMenuClosedown,
																						this.projectMenuBar1,
																						this.projectMenuConnect,
																						this.projectMenuDisconnect,
																						this.projectMenuBar2,
																						this.projectMenuSave,
																						this.projectMenuBar3,
																						this.projectMenuTopmost});
			this.projectMenu.Text = "&Project";
			// 
			// projectMenuConfigurer
			// 
			this.projectMenuConfigurer.Index = 0;
			this.projectMenuConfigurer.Text = "Enable &Configurer";
			this.projectMenuConfigurer.Click += new System.EventHandler(this.OnClickProjectMenuConfigurer);
			// 
			// projectMenuExplorer
			// 
			this.projectMenuExplorer.Index = 1;
			this.projectMenuExplorer.Text = "Enable &Explorer";
			this.projectMenuExplorer.Click += new System.EventHandler(this.OnClickProjectMenuExplorer);
			// 
			// projectMenuClosedown
			// 
			this.projectMenuClosedown.Index = 2;
			this.projectMenuClosedown.Text = "Enable &Window Closedown";
			this.projectMenuClosedown.Click += new System.EventHandler(this.OnClickProjectMenuClosedown);
			// 
			// projectMenuBar1
			// 
			this.projectMenuBar1.Index = 3;
			this.projectMenuBar1.Text = "-";
			// 
			// projectMenuConnect
			// 
			this.projectMenuConnect.Index = 4;
			this.projectMenuConnect.Text = "C&onnect All";
			this.projectMenuConnect.Click += new System.EventHandler(this.OnClickProjectMenuConnect);
			// 
			// projectMenuDisconnect
			// 
			this.projectMenuDisconnect.Index = 5;
			this.projectMenuDisconnect.Text = "D&isconnect All";
			this.projectMenuDisconnect.Click += new System.EventHandler(this.OnClickProjectMenuDisconnect);
			// 
			// projectMenuBar2
			// 
			this.projectMenuBar2.Index = 6;
			this.projectMenuBar2.Text = "-";
			// 
			// projectMenuSave
			// 
			this.projectMenuSave.Index = 7;
			this.projectMenuSave.Text = "&Save Project File";
			this.projectMenuSave.Click += new System.EventHandler(this.OnClickProjectMenuSave);
			// 
			// projectMenuBar3
			// 
			this.projectMenuBar3.Index = 8;
			this.projectMenuBar3.Text = "-";
			// 
			// projectMenuTopmost
			// 
			this.projectMenuTopmost.Checked = true;
			this.projectMenuTopmost.Index = 9;
			this.projectMenuTopmost.Text = "Always On Top";
			this.projectMenuTopmost.Click += new System.EventHandler(this.OnClickProjectMenuTopmost);
			// 
			// windowMenu
			// 
			this.windowMenu.Index = 2;
			this.windowMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.windowMenuAdd,
																					   this.windowMenuNew,
																					   this.windowMenuBar1});
			this.windowMenu.Text = "&Window";
			// 
			// windowMenuAdd
			// 
			this.windowMenuAdd.Index = 0;
			this.windowMenuAdd.Text = "&Add Existing Window...";
			this.windowMenuAdd.Click += new System.EventHandler(this.OnClickWindowMenuAdd);
			// 
			// windowMenuNew
			// 
			this.windowMenuNew.Index = 1;
			this.windowMenuNew.Text = "Add &New Window";
			this.windowMenuNew.Click += new System.EventHandler(this.OnClickWindowMenuNew);
			// 
			// windowMenuBar1
			// 
			this.windowMenuBar1.Index = 2;
			this.windowMenuBar1.Text = "-";
			// 
			// channelsMenu
			// 
			this.channelsMenu.Index = 3;
			this.channelsMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.channelsMenuAdd,
																						 this.channelsMenuNew,
																						 this.channelsMenuBar1});
			this.channelsMenu.Text = "&Channels";
			// 
			// channelsMenuAdd
			// 
			this.channelsMenuAdd.Index = 0;
			this.channelsMenuAdd.Text = "&Add Existing Collection...";
			this.channelsMenuAdd.Click += new System.EventHandler(this.OnClickChannelsMenuAdd);
			// 
			// channelsMenuNew
			// 
			this.channelsMenuNew.Index = 1;
			this.channelsMenuNew.Text = "Add &New Collection";
			this.channelsMenuNew.Click += new System.EventHandler(this.OnClickChannelsMenuNew);
			// 
			// channelsMenuBar1
			// 
			this.channelsMenuBar1.Index = 2;
			this.channelsMenuBar1.Text = "-";
            //// 
            //// sequenceMenu
            //// 
            //this.sequenceMenu.Index = 4;
            //this.sequenceMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            //                                                                             this.sequenceMenuAdd,
            //                                                                             this.sequenceMenuNew,
            //                                                                             this.sequenceMenuBar1});
            //this.sequenceMenu.Text = "&Sequence";
            //// 
            //// sequenceMenuAdd
            //// 
            //this.sequenceMenuAdd.Index = 0;
            //this.sequenceMenuAdd.Text = "&Add Existing Sequence...";
            //this.sequenceMenuAdd.Click += new System.EventHandler(this.OnClickSequenceMenuAdd);
            //// 
            //// sequenceMenuNew
            //// 
            //this.sequenceMenuNew.Index = 1;
            //this.sequenceMenuNew.Text = "Add &New Sequence";
            //this.sequenceMenuNew.Click += new System.EventHandler(this.OnClickSequenceMenuNew);
            //// 
            //// sequenceMenuBar1
            //// 
            //this.sequenceMenuBar1.Index = 2;
            //this.sequenceMenuBar1.MdiList = true;
            //this.sequenceMenuBar1.Text = "-";
			// 
			// userMenu
			// 
			this.userMenu.Index = 4;
			this.userMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.userMenuConfigurer,
																					 this.userMenuEditing,
																					 this.userMenuExplorer});
			this.userMenu.Text = "User";
			// 
			// userMenuConfigurer
			// 
			this.userMenuConfigurer.Index = 0;
			this.userMenuConfigurer.Text = "Enable Configurer";
			this.userMenuConfigurer.Click += new System.EventHandler(this.userMenuConfigurer_Click);
			// 
			// userMenuEditing
			// 
			this.userMenuEditing.Index = 1;
			this.userMenuEditing.Text = "Enable Editing";
			this.userMenuEditing.Click += new System.EventHandler(this.userMenuEditing_Click);
			// 
			// userMenuExplorer
			// 
			this.userMenuExplorer.Index = 2;
			this.userMenuExplorer.Text = "Enable Explorer";
			this.userMenuExplorer.Click += new System.EventHandler(this.userMenuExplorer_Click);
			// 
			// helpMenu
			// 
			this.helpMenu.Index = 5;
			this.helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.helpMenuHelp,
																					 this.helpMenuOnline,
																					 this.helpMenuBar1,
																					 this.helpMenuAbout});
			this.helpMenu.Text = "&Help";
			this.helpMenu.Click += new System.EventHandler(this.OnClickHelpMenuAbout);
			// 
			// helpMenuHelp
			// 
			this.helpMenuHelp.Index = 0;
			this.helpMenuHelp.Text = "KineticaRT.Studio &Help";
			this.helpMenuHelp.Click += new System.EventHandler(this.OnClickHelpMenuHelp);
			// 
			// helpMenuOnline
			// 
			this.helpMenuOnline.Index = 1;
			this.helpMenuOnline.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.helpMenuOnlineStudio,
																						   this.helpMenuOnlineKineticaRT,
																						   this.helpMenuOnlineSupport});
			this.helpMenuOnline.Text = "KineticaRT &Online";
			// 
			// helpMenuOnlineStudio
			// 
			this.helpMenuOnlineStudio.Index = 0;
			this.helpMenuOnlineStudio.Text = "KineticaRT.Studio";
			this.helpMenuOnlineStudio.Click += new System.EventHandler(this.OnClickHelpMenuOnlineStudio);
			// 
			// helpMenuOnlineKineticaRT
			// 
			this.helpMenuOnlineKineticaRT.Index = 1;
			this.helpMenuOnlineKineticaRT.Text = "KineticaRT";
			this.helpMenuOnlineKineticaRT.Click += new System.EventHandler(this.OnClickHelpMenuOnlineKineticaRT);
			// 
			// helpMenuOnlineSupport
			// 
			this.helpMenuOnlineSupport.Index = 2;
			this.helpMenuOnlineSupport.Text = "Technical Support";
			this.helpMenuOnlineSupport.Click += new System.EventHandler(this.OnClickHelpMenuOnlineSupport);
			// 
			// helpMenuBar1
			// 
			this.helpMenuBar1.Index = 2;
			this.helpMenuBar1.Text = "-";
			// 
			// helpMenuAbout
			// 
			this.helpMenuAbout.Index = 3;
			this.helpMenuAbout.Text = "&About KineticaRT.Studio...";
			this.helpMenuAbout.Click += new System.EventHandler(this.OnClickHelpMenuAbout);
			// 
			// windowSubMenu
			// 
			this.windowSubMenu.Index = -1;
			this.windowSubMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.windowSubMenuRename,
																						  this.windowSubMenuProperties,
																						  this.windowSubMenuSelect,
																						  this.windowSubMenuBar1,
																						  this.windowSubMenuSave,
																						  this.windowSubMenuSaveAs,
																						  this.windowSubMenuBar2,
																						  this.windowSubMenuRemove});
			this.windowSubMenu.Text = "Sub Window";
			// 
			// windowSubMenuRename
			// 
			this.windowSubMenuRename.Index = 0;
			this.windowSubMenuRename.Text = "Re&name";
			this.windowSubMenuRename.Click += new System.EventHandler(this.OnClickWindowSubMenuRename);
			// 
			// windowSubMenuProperties
			// 
			this.windowSubMenuProperties.Index = 1;
			this.windowSubMenuProperties.Text = "Properties";
			this.windowSubMenuProperties.Click += new System.EventHandler(this.OnClickWindowSubMenuProperties);
			// 
			// windowSubMenuSelect
			// 
			this.windowSubMenuSelect.Index = 2;
			this.windowSubMenuSelect.Text = "Se&lect";
			this.windowSubMenuSelect.Click += new System.EventHandler(this.OnClickWindowSubMenuSelect);
			// 
			// windowSubMenuBar1
			// 
			this.windowSubMenuBar1.Index = 3;
			this.windowSubMenuBar1.Text = "-";
			// 
			// windowSubMenuSave
			// 
			this.windowSubMenuSave.Index = 4;
			this.windowSubMenuSave.Text = "&Save";
			this.windowSubMenuSave.Click += new System.EventHandler(this.OnClickWindowSubMenuSave);
			// 
			// windowSubMenuSaveAs
			// 
			this.windowSubMenuSaveAs.Index = 5;
			this.windowSubMenuSaveAs.Text = "Save &As...";
			this.windowSubMenuSaveAs.Click += new System.EventHandler(this.OnClickWindowSubMenuSaveAs);
			// 
			// windowSubMenuBar2
			// 
			this.windowSubMenuBar2.Index = 6;
			this.windowSubMenuBar2.Text = "-";
			// 
			// windowSubMenuRemove
			// 
			this.windowSubMenuRemove.Index = 7;
			this.windowSubMenuRemove.Text = "&Remove";
			this.windowSubMenuRemove.Click += new System.EventHandler(this.OnClickWindowSubMenuRemove);
			// 
			// channelsSubMenu
			// 
			this.channelsSubMenu.Index = -1;
			this.channelsSubMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.channelsSubMenuRename,
																							this.channelsSubMenuEdit,
																							this.channelsSubMenuBar1,
																							this.channelsSubMenuSave,
																							this.channelsSubMenuSaveAs,
																							this.channelsSubMenuBar2,
																							this.channelsSubMenuRemove});
			this.channelsSubMenu.Text = "Sub channels";
			// 
			// channelsSubMenuRename
			// 
			this.channelsSubMenuRename.Index = 0;
			this.channelsSubMenuRename.Text = "Re&name...";
			this.channelsSubMenuRename.Click += new System.EventHandler(this.OnClickChannelsSubMenuRename);
			// 
			// channelsSubMenuEdit
			// 
			this.channelsSubMenuEdit.Index = 1;
			this.channelsSubMenuEdit.Text = "&Edit";
			this.channelsSubMenuEdit.Click += new System.EventHandler(this.OnClickChannelsSubMenuEdit);
			// 
			// channelsSubMenuBar1
			// 
			this.channelsSubMenuBar1.Index = 2;
			this.channelsSubMenuBar1.Text = "-";
			// 
			// channelsSubMenuSave
			// 
			this.channelsSubMenuSave.Index = 3;
			this.channelsSubMenuSave.Text = "&Save";
			this.channelsSubMenuSave.Click += new System.EventHandler(this.OnClickChannelsSubMenuSave);
			// 
			// channelsSubMenuSaveAs
			// 
			this.channelsSubMenuSaveAs.Index = 4;
			this.channelsSubMenuSaveAs.Text = "Save &As...";
			this.channelsSubMenuSaveAs.Click += new System.EventHandler(this.OnClickChannelsSubMenuSaveAs);
			// 
			// channelsSubMenuBar2
			// 
			this.channelsSubMenuBar2.Index = 5;
			this.channelsSubMenuBar2.Text = "-";
			// 
			// channelsSubMenuRemove
			// 
			this.channelsSubMenuRemove.Index = 6;
			this.channelsSubMenuRemove.Text = "&Remove";
			this.channelsSubMenuRemove.Click += new System.EventHandler(this.OnClickChannelsSubMenuRemove);
            //// 
            //// sequenceSubMenu
            //// 
            //this.sequenceSubMenu.Index = -1;
            //this.sequenceSubMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            //                                                                                this.sequenceSubMenuRename,
            //                                                                                this.sequenceSubMenuEdit,
            //                                                                                this.sequenceSubMenuBar1,
            //                                                                                this.sequenceSubMenuSave,
            //                                                                                this.sequenceSubMenuSaveAs,
            //                                                                                this.sequenceSubMenuBar2,
            //                                                                                this.sequenceSubMenuRemove});
            //this.sequenceSubMenu.Text = "Sub sequence";
            //// 
            //// sequenceSubMenuRename
            //// 
            //this.sequenceSubMenuRename.Index = 0;
            //this.sequenceSubMenuRename.Text = "Re&name...";
            //this.sequenceSubMenuRename.Click += new System.EventHandler(this.OnClickSequenceSubMenuRename);
            //// 
            //// sequenceSubMenuEdit
            //// 
            //this.sequenceSubMenuEdit.Index = 1;
            //this.sequenceSubMenuEdit.Text = "&Edit";
            //this.sequenceSubMenuEdit.Click += new System.EventHandler(this.OnClickSequenceSubMenuEdit);
            //// 
            //// sequenceSubMenuBar1
            //// 
            //this.sequenceSubMenuBar1.Index = 2;
            //this.sequenceSubMenuBar1.Text = "-";
            //// 
            //// sequenceSubMenuSave
            //// 
            //this.sequenceSubMenuSave.Index = 3;
            //this.sequenceSubMenuSave.Text = "&Save";
            //this.sequenceSubMenuSave.Visible = false;
            //this.sequenceSubMenuSave.Click += new System.EventHandler(this.OnClickSequenceSubMenuSave);
            //// 
            //// sequenceSubMenuSaveAs
            //// 
            //this.sequenceSubMenuSaveAs.Index = 4;
            //this.sequenceSubMenuSaveAs.Text = "Save &As...";
            //this.sequenceSubMenuSaveAs.Visible = false;
            //this.sequenceSubMenuSaveAs.Click += new System.EventHandler(this.OnClickSequenceSubMenuSaveAs);
            //// 
            //// sequenceSubMenuBar2
            //// 
            //this.sequenceSubMenuBar2.Index = 5;
            //this.sequenceSubMenuBar2.Text = "-";
            //this.sequenceSubMenuBar2.Visible = false;
            //// 
            //// sequenceSubMenuRemove
            //// 
            //this.sequenceSubMenuRemove.Index = 6;
            //this.sequenceSubMenuRemove.Text = "&Remove";
            //this.sequenceSubMenuRemove.Click += new System.EventHandler(this.OnClickSequenceSubMenuRemove);
			// 
			// messageList
			// 
			this.messageList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.timeHeader,
																						  this.messageHeader});
			this.messageList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.messageList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.messageList.Location = new System.Drawing.Point(0, 0);
			this.messageList.MultiSelect = false;
			this.messageList.Name = "messageList";
			this.messageList.Size = new System.Drawing.Size(368, 33);
			this.messageList.TabIndex = 0;
			this.messageList.View = System.Windows.Forms.View.Details;
			// 
			// timeHeader
			// 
			this.timeHeader.Text = "Time";
			this.timeHeader.Width = 120;
			// 
			// messageHeader
			// 
			this.messageHeader.Text = "Message";
			this.messageHeader.Width = 240;
			// 
			// Configurer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 33);
			this.Controls.Add(this.messageList);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.configMenu;
			this.Name = "Configurer";
			this.Text = "KineticaRT.Studio";
			this.TopMost = true;
			this.SizeChanged += new System.EventHandler(this.OnSizeChanged);
			this.Load += new System.EventHandler(this.OnLoad);
			this.Move += new System.EventHandler(this.OnMove);
			this.ResumeLayout(false);

		}
		#endregion

		#region Internal Methods - called by Project
		/// <summary>
		/// Checks / unchecks the Enable Configurer menu item
		/// </summary>
		internal void SetConfigurerEnabled(bool Enabled)
		{
			projectMenuConfigurer.Checked = Enabled ;
		}

		/// <summary>
		/// Checks / unchecks the Enable Explorer menu item
		/// </summary>
		internal void SetExplorerEnabled(bool Enabled)
		{
			projectMenuExplorer.Checked = Enabled ;
		}

		/// <summary>
		/// Checks / unchecks the Enable Closedown menu item
		/// </summary>
		internal void SetClosedownEnabled(bool Enabled)
		{
			projectMenuClosedown.Checked = Enabled ;
		}
		/// <summary>
		/// Adds a menu item with the specified text that relates
		/// to a Window
		/// </summary>
		/// <param name="Text">Text of menu item to add</param>
		internal void AddWindowMenuItem(string Text)
		{
			MenuItem	new_item	= windowSubMenu.CloneMenu() ;

			new_item.Text = Text ;
			windowMenu.MenuItems.Add(new_item) ;
		}

		/// <summary>
		/// Adds a menu item with the specified text that relates
		/// to a ChannelCollection
		/// </summary>
		/// <param name="Text">Text of menu item to add</param>
		internal void AddChannelsMenuItem(string Text)
		{
			MenuItem	new_item	= channelsSubMenu.CloneMenu() ;

			new_item.Text = Text ;
			channelsMenu.MenuItems.Add(new_item) ;
		}

        ///// <summary>
        ///// Adds a menu item with the specified text that relates
        ///// to a Sequence
        ///// </summary>
        ///// <param name="Text">Text of menu item to add</param>
        //internal void AddSequenceMenuItem(string Text)
        //{
        //    MenuItem	new_item	= sequenceSubMenu.CloneMenu() ;

        //    new_item.Text = Text ;
        //    sequenceMenu.MenuItems.Add(new_item) ;
        //}

		/// <summary>
		/// Renames the menu item corresponding to the Window at the given Index
		/// </summary>
		/// <param name="Index">Index of Window</param>
		/// <param name="NewText">New Text to display</param>
		internal void RenameWindowMenuItem(int Index, string NewText)
		{
			//add 3 to the index to ignore non-window items
			windowMenu.MenuItems[Index+3].Text = NewText ;
		}

		/// <summary>
		/// Renames the menu item corresponding to the ChannelCollection at the given Index
		/// </summary>
		/// <param name="Index">Index of ChannelCollection</param>
		/// <param name="NewText">New Text to display</param>
		internal void RenameChannelsMenuItem(int Index, string NewText)
		{
			//add 3 to the index to ignore non-file items
			channelsMenu.MenuItems[Index+3].Text = NewText ;
		}

        ///// <summary>
        ///// Renames the menu item corresponding to the Sequence at the given Index
        ///// </summary>
        ///// <param name="Index">Index of Sequence</param>
        ///// <param name="NewText">New Text to display</param>
        //internal void RenameSequenceMenuItem(int Index, string NewText)
        //{
        //    //add 3 to the index to ignore non-file items
        //    sequenceMenu.MenuItems[Index+3].Text = NewText ;
        //}
		/// <summary>
		/// Removes the menu item corresponding to the Window at the given Index
		/// </summary>
		/// <param name="Index">Index of Window</param>
		internal void RemoveWindowMenuItem(int Index)
		{
			//add 3 to the index to ignore non-window items
			windowMenu.MenuItems.RemoveAt(Index+3) ;
		}

		/// <summary>
		/// Removes the menu item corresponding to the ChannelCollection at the given Index
		/// </summary>
		/// <param name="Index">Index of ChannelCollection</param>
		internal void RemoveChannelsMenuItem(int Index)
		{
			//add 3 to the index to ignore non-file items
			channelsMenu.MenuItems.RemoveAt(Index+3) ;
		}

        ///// <summary>
        ///// Removes the menu item corresponding to the Sequence at the given Index
        ///// </summary>
        ///// <param name="Index">Index of Sequence</param>
        //internal void RemoveSequenceMenuItem(int Index)
        //{
        //    //add 3 to the index to ignore non-file items
        //    sequenceMenu.MenuItems.RemoveAt(Index+3) ;
        //}
		#endregion (internal methods)

		#region File Menu event handling
		private void OnClickFileMenuSave(object sender, System.EventArgs e)
		{
			if (proj != null)			proj.SaveAll() ;
		}

		/// <summary>
		/// Closes the application.
		/// </summary>
        public void Exit()
        {
            this.Close();
        }

        public void DemonstrationExit()
        {
            System.Windows.Forms.MessageBox.Show(this, "This Demonstration is now closing down.\nYou may start the demonstration again if you want to continue.", "KineticaRT Studio Demonstration", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            this.Close();
        }

        private void OnClickFileMenuExit(object sender, System.EventArgs e)
		{
			Exit () ;
		}
		#endregion (file menu)

		#region Project Menu event handling
		private void OnClickProjectMenuExplorer(object sender, System.EventArgs e)
		{
			if (proj != null)	proj.EnableExplorer(!projectMenuExplorer.Checked) ;
		}

		private void OnClickProjectMenuConfigurer(object sender, System.EventArgs e)
		{
			if (proj != null)	proj.EnableConfigurer(!projectMenuConfigurer.Checked) ;
		}

		private void OnClickProjectMenuClosedown(object sender, System.EventArgs e)
		{
			if (proj != null)	proj.EnableClosedown(!projectMenuClosedown.Checked) ;
		}

		private void OnClickProjectMenuConnect(object sender, System.EventArgs e)
		{
			if (proj != null)		proj.Connect() ;
		}

		private void OnClickProjectMenuDisconnect(object sender, System.EventArgs e)
		{
			if (proj != null)		proj.Disconnect() ;
		}

		private void OnClickProjectMenuSave(object sender, System.EventArgs e)
		{
			if (proj != null)		proj.SaveProjectFile() ;
		}

		private void OnClickProjectMenuTopmost(object sender, System.EventArgs e)
		{
			this.TopMost = !this.TopMost ;
			projectMenuTopmost.Checked = this.TopMost ;
			OnPropertyChanged ("TopMost") ;
		}
		#endregion (project menu)

		#region Window Menu event handling
		private void OnClickWindowMenuAdd(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				//set up the dialog to open a window file
				openDialog.DefaultExt = "ksw" ;
				openDialog.Filter = "KineticaRT Studio Windows (*.ksw)|*.ksw|All Files (*.*)|*.*" ;
				openDialog.Title = "Open a Window" ;
				openDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath ;

				if (openDialog.ShowDialog(this) == DialogResult.OK)
				{
					//##NB - dialog does not enforce file to be in same directory
					//as project file, but the following assumes it is
                    proj.AddWindow(Path.GetFileName(openDialog.FileName), true, true, false);
				}
			}
		}

        private void OnClickWindowMenuNew(object sender, System.EventArgs e)
        {
            //if (proj != null)		proj.AddWindow("", true, true) ;
            if (proj == null) return;

            EditName edit = new EditName("Name of new Window", true);
            edit.NewName = "";

            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                proj.AddWindow(edit.NewName, true, true, true);
            }
        }

		private void OnClickWindowSubMenuRename(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				MenuItem		parent_item		= (MenuItem)((MenuItem)sender).Parent ;
				int				index			= parent_item.Index ;
				EditName		edit			= new EditName("New Window Name", false) ;

				//ignore the non-window menu items
				index -=3 ;

				edit.NewName	= parent_item.Text ;

				if (edit.ShowDialog(this) == DialogResult.OK && edit.NewName != parent_item.Text)
				{
					//name has actually changed
					proj.RenameWindow(index, edit.NewName) ;
				}
			}
		}

		private void OnClickWindowSubMenuProperties(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				MenuItem					parent_item		= (MenuItem)((MenuItem)sender).Parent ;
				int							index			= parent_item.Index ;
				EditWindowProperties		edit			= new EditWindowProperties () ;

				//ignore the non-window menu items
				index -=3 ;

				edit.WindowInfo = proj.GetWindowInfo (index) ;

				if (edit.ShowDialog(this) == DialogResult.OK)
				{
				}
			}
		}

		private void OnClickWindowSubMenuSelect(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int			index = ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-window menu items
				index -=3 ;

				proj.SelectWindow(index) ;
			}
		}

		private void OnClickWindowSubMenuSave(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int			index = ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-window menu items
				index -=3 ;
				proj.SaveWindow(index, false) ;
			}
		}

		private void OnClickWindowSubMenuSaveAs(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int			index = ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-window menu items
				index -=3 ;
				proj.SaveWindow(index, true) ;
			}
		}

		private void OnClickWindowSubMenuRemove(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int		index = ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-window menu items
				index -=3 ;

				proj.RemoveWindow(index) ;
			}
		}
		#endregion (window menu)

		#region Channels Menu event handling
		private void OnClickChannelsMenuAdd(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				//set up the dialog to open a channels file
				openDialog.DefaultExt = "ksc" ;
				openDialog.Filter = "KineticaRT.Studio Channels (*.ksc)|*.ksc|All Files (*.*)|*.*" ;
				openDialog.Title = "Open a Channels file" ;
				openDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath ;

				if (openDialog.ShowDialog(this) == DialogResult.OK)
				{
					//##NB - dialog does not enforce file to be in same directory
					//as project file, but the following assumes it is
					proj.AddChannelCollection(Path.GetFileName(openDialog.FileName), "", true) ;
				}
			}
		}

        private void OnClickChannelsMenuNew(object sender, System.EventArgs e)
        {
            if (proj == null) return;

            EditName edit = new EditName("Name of new collection", true);
            edit.NewName = "";

            if (edit.ShowDialog(this) == DialogResult.OK)
            {
                proj.CreateChannelCollection(edit.NewName, true);
            }
            //proj.AddChannelCollection("", "", true);
        }

		private void OnClickChannelsSubMenuRename(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				MenuItem	parent_item		= (MenuItem)((MenuItem)sender).Parent ;
				int			index			= parent_item.Index ;
                EditName edit = new EditName("New Channel Collection Name", true);

				//ignore the non-file menu items
				index -=3 ;

				edit.NewName	= parent_item.Text ;

				if (edit.ShowDialog(this) == DialogResult.OK && edit.NewName != parent_item.Text)
				{
					//name has actually changed
					proj.RenameChannels(index, edit.NewName) ;
				}
			}
		}

		private void OnClickChannelsSubMenuEdit(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int				index			= ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-file menu items
				index -=3 ;
				proj.EditChannels(index) ;
			}
		}

		private void OnClickChannelsSubMenuSave(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int				index = ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-file menu items
				index -=3 ;
				proj.SaveChannels(index, false) ;
			}
		}

		private void OnClickChannelsSubMenuSaveAs(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int				index = ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-file menu items
				index -=3 ;
				proj.SaveChannels(index, true) ;
			}
		}

		private void OnClickChannelsSubMenuRemove(object sender, System.EventArgs e)
		{
			if (proj != null)
			{
				int				index = ((MenuItem)((MenuItem)sender).Parent).Index ;

				//ignore the non-window menu items
				index -=3 ;
				proj.RemoveChannels(index) ;
			}
		}
		#endregion (channels menu)

        //#region Sequence Menu event handling
        //private void OnClickSequenceMenuAdd(object sender, System.EventArgs e)
        //{
        //    if (proj != null)
        //    {
        //        //set up the dialog to open a sequence file
        //        openDialog.DefaultExt = "cs" ;
        //        openDialog.Filter = "C# files (*.cs)|*.cs|Dll files (*.dll)|*.dll|All Files (*.*)|*.*" ;
        //        openDialog.Title = "Open a Sequence" ;
        //        openDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath ;

        //        if (openDialog.ShowDialog(this) == DialogResult.OK)
        //        {
        //            SequenceSetup		info	= new SequenceSetup(Path.GetFileNameWithoutExtension(openDialog.FileName)) ;

        //            if (info.ShowDialog(this) == DialogResult.OK)
        //            {
        //                //##NB - dialog does not enforce file to be in same directory
        //                //as project file, but the following assumes it is
        //                proj.AddSequence(info.SequenceFile, info.SequenceName, info.SequenceType, info.SequenceDefine, true, null) ;
        //            }
        //        }
        //    }
        //}

        //private void OnClickSequenceMenuNew(object sender, System.EventArgs e)
        //{
        //    if (proj != null)
        //    {
        //        SequenceSetup		setup		= new SequenceSetup() ;

        //        if (setup.ShowDialog(this) == DialogResult.OK)
        //        {
        //            proj.NewSequence(setup.SequenceName, setup.SequenceFile, setup.SequenceClass, setup.SequenceNamespace, setup.SequenceDefine) ;
        //        }
        //    }
        //}

        //private void OnClickSequenceSubMenuRename(object sender, System.EventArgs e)
        //{
        //    if (proj != null)
        //    {
        //        MenuItem	parent_item		= (MenuItem)((MenuItem)sender).Parent ;
        //        int			index			= parent_item.Index ;
        //        EditName edit = new EditName("New Sequence Name", true);

        //        //ignore the non-file menu items
        //        index -=3 ;

        //        edit.NewName	= parent_item.Text ;

        //        if (edit.ShowDialog(this) == DialogResult.OK && edit.NewName != parent_item.Text)
        //        {
        //            //name has actually changed
        //            proj.RenameSequence(index, edit.NewName) ;
        //        }
        //    }
        //}

        //private void OnClickSequenceSubMenuEdit(object sender, System.EventArgs e)
        //{
        //    if (proj != null)
        //    {
        //        int		index		= ((MenuItem)((MenuItem)sender).Parent).Index ;

        //        //ignore the non-file menu items
        //        index -= 3 ;
        //        proj.EditSequence(index) ;
        //    }
        //}

        //private void OnClickSequenceSubMenuSave(object sender, System.EventArgs e)
        //{
        //    MessageBox.Show("Not implemented") ;
        //}

        //private void OnClickSequenceSubMenuSaveAs(object sender, System.EventArgs e)
        //{
        //    MessageBox.Show("Not implemented") ;
        //}

        //private void OnClickSequenceSubMenuRemove(object sender, System.EventArgs e)
        //{
        //    if (proj != null)
        //    {
        //        int			index = ((MenuItem)((MenuItem)sender).Parent).Index ;

        //        //ignore the non-window menu items
        //        index -=3 ;

        //        proj.RemoveSequence(index) ;
        //    }
        //}
        //#endregion (sequence menu)

		#region User Menu event handling
		private void userMenuEditing_Click(object sender, System.EventArgs e)
		{
			editingEnabled = !userMenuEditing.Checked ;
			userMenuEditing.Checked = editingEnabled ;

			try
			{
				RegistryKey key =  Registry.CurrentUser.CreateSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\User") ;
				if (key != null) key.SetValue ("EnableEditMenu", editingEnabled) ;
			}
			catch {}
		}
		private void userMenuConfigurer_Click(object sender, System.EventArgs e)
		{
			configurerEnabled = !userMenuConfigurer.Checked ;
			userMenuConfigurer.Checked = configurerEnabled ;

			try
			{
				RegistryKey key =  Registry.CurrentUser.CreateSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\User") ;
				if (key != null) key.SetValue ("EnableConfigurer", configurerEnabled) ;
			}
			catch {}
		
			if (proj != null)	proj.EnableConfigurer(projectMenuConfigurer.Checked) ;
		}

		private void userMenuExplorer_Click(object sender, System.EventArgs e)
		{
			explorerEnabled = !userMenuExplorer.Checked ;
			userMenuExplorer.Checked = explorerEnabled ;

			try
			{
				RegistryKey key =  Registry.CurrentUser.CreateSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\User") ;
				if (key != null) key.SetValue ("EnableExplorer", explorerEnabled) ;
			}
			catch {}
	
			if (proj != null)	proj.EnableExplorer(projectMenuExplorer.Checked) ;
		}
		#endregion

		#region Help Menu
		private void OnClickHelpMenuHelp(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process process = null ;
			try
			{
				process = System.Diagnostics.Process.Start ("Manual.chm") ;
			}
			catch
			{
			}

			if (process == null)
			{
				MessageBox.Show(this, "KineticaRT Ltd apologise for the inconvenience, but there is at present no Help available", "KineticaRT.Studio Help") ;
			}
		}

		private void OnClickHelpMenuAbout(object sender, System.EventArgs e)
		{
			//show 'about' dialog
			About		about = new About() ;

			about.ShowDialog(this) ;
		}

		private void OnClickHelpMenuOnlineStudio(object sender, System.EventArgs e)
		{
			Process.Start("http://www.kineticart.co.uk/KineticaRTStudio.asp") ;
		}

		private void OnClickHelpMenuOnlineKineticaRT(object sender, System.EventArgs e)
		{
			Process.Start("http://www.kineticart.co.uk") ;
		}

		private void OnClickHelpMenuOnlineSupport(object sender, System.EventArgs e)
		{
			Process.Start("http://www.kineticart.co.uk/SupportRequest.asp") ;
		}
		#endregion (help menus)

		#region PropertyChanged
		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;   
		/// <summary>
		/// Event which fires when the value of the objects property has changed.
		/// </summary>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
		}
		#endregion

		/// <summary>
		/// Sets whether or not the Configurer window is on top of all windows or not.
		/// </summary>
		/// <param name="value">true=configurer on top of all other windows</param>
		public void SetTopMost (bool value)
		{
			this.projectMenuTopmost.Checked = value ;
			this.TopMost = value ;
		}
		private void OnLoad(object sender, System.EventArgs e)
		{
			try
			{
				RegistryKey key =  Registry.CurrentUser.OpenSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\"+GetType().ToString()) ;
				if (key != null)
				{
					WindowState = (FormWindowState) key.GetValue ("State") ;
					SetBounds ((int) key.GetValue ("Left"),(int) key.GetValue ("Top"),(int) key.GetValue ("Width"),(int) key.GetValue ("Height"),System.Windows.Forms.BoundsSpecified.All) ;
					if (!SystemInformation.VirtualScreen.IntersectsWith (RectangleToScreen(ClientRectangle)))this.Location = new System.Drawing.Point(10,10) ;
				}

				key =  Registry.CurrentUser.OpenSubKey (@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\User") ;
				if (key != null)
				{
					try
                    {
                        string value = key.GetValue("EnableEditMenu", true) as string ;
                        editingEnabled = (value == null) ? true : (value.ToLower()=="true") ;
                    } 
					catch{}
					try
                    {
                        string value = key.GetValue("EnableExplorer", true) as string;
                        explorerEnabled = (value == null) ? true : (value.ToLower() == "true");
                    }
					catch{}
					try
                    {
                        string value = key.GetValue("EnableConfigurer", true) as string;
                        configurerEnabled = (value == null) ? true : (value.ToLower() == "true");
                    }
					catch{}
				}
				userMenuEditing.Checked = editingEnabled ;
				userMenuExplorer.Checked = explorerEnabled ;
				userMenuConfigurer.Checked = configurerEnabled ;
			}
			catch {}
		}

		private void OnSizeChanged(object sender, System.EventArgs e)
		{
			SaveWindowSettings () ;
		}

		private void OnMove(object sender, System.EventArgs e)
		{
			SaveWindowSettings () ;
		}

		private void SaveWindowSettings () 
		{
			if (Created)
			{
				RegistryKey key =  Registry.CurrentUser.CreateSubKey(@"Software\KineticaRT\"+System.AppDomain.CurrentDomain.FriendlyName+@"\"+GetType().ToString()) ;

				key.SetValue ("State", (int)WindowState) ;

				if (WindowState == System.Windows.Forms.FormWindowState.Normal)
				{
					key.SetValue ("Left", Left) ;
					key.SetValue ("Top", Top) ;
					key.SetValue ("Width", Size.Width) ;
					key.SetValue ("Height", Size.Height) ;
				}
			}
		}
	}
}

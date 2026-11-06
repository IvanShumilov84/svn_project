using System;

namespace KineticaRT.Studio
{
	/// <summary>
	/// KineticaRT Studio Application
	/// </summary>
	/// <remarks>
	/// Note that any dlls required for controls on panels must
	/// be in the application directory
	/// </remarks>
	public class App
	{
		private KineticaRT.Studio.Application.Project				project ;

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Application
		/// </summary>
		public App()
		{
			//System.Resources.ResourceManager rm = new System.Resources.ResourceManager("StudioDemonstration.Strings", System.Reflection.Assembly.GetExecutingAssembly());
			//System.Windows.Forms.MessageBox.Show(rm.GetString("ApplicationName"));
			Initialise(false,string.Empty) ;
		}

		/// <summary>
		/// Initialises a new instance of the Application,
		/// forcing the Configurer window to be visible if requested
		/// </summary>
		public App(bool ShowCfg,string ProjectFileName) //ft
		{
			Initialise(ShowCfg,ProjectFileName) ; //ft 
		}

		private void Initialise(bool ShowCfg,string ProjectFileName)
		{
			project = new KineticaRT.Studio.Application.Project(ShowCfg,ProjectFileName) ;
		}
		#endregion (initialisation)

		#region Properties
		/// <summary>
		/// Gets whether the Application successfully loaded a project
		/// </summary>
		/// <remarks>Automatically shuts down if the project was not successfully loaded</remarks>
		public bool OK
		{
			get
			{
				return project.Ok ;
			}
		}

		/// <summary>
		/// Gets the Project asscociated with this Application
		/// </summary>
		public KineticaRT.Studio.Application.Project Project
		{
			get
			{
				return project ;
			}
		}
		#endregion (properties)

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string [] args) 
		{
			App app ;
            bool OneInstanceRunningMaximum = false;
            String AppName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            System.Threading.Mutex Mut = new System.Threading.Mutex(false, @"Global\" + AppName);
            System.Windows.Forms.DialogResult result;

            //if we have an argument, check if it is '/c...' (else ignore)
            if (args.Length > 0)
			{
				bool		force_cfg		= false ;
                string      project_file_name    = string.Empty;

				foreach (string arg in args)
				{
                    if (arg.ToLower().StartsWith("/c") && !(arg.ToLower().StartsWith("/config"))) //ft conflict with option /config
					{
						force_cfg = true ;
						//ft break ;
					}
                    if (arg.ToLower().StartsWith("/ksp="))//ft 
                    {
                        project_file_name = arg.Split('=')[1];
                    }
                    if (arg.ToLower().StartsWith("/oneinstance"))
                    {
                        // using {} not recommended, since Mutex has to be in scope for the lifetime of the app                                          
                        OneInstanceRunningMaximum = !Mut.WaitOne(0, false);
                    }
                    if (arg.ToLower().StartsWith("/oneinstanceopc"))
                    {
                        //Kill any ISAOPCSV.exe (OPC server) process in memory before launching KineticaRT
                        System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = "C:\\Windows\\system32\\taskkill.exe",
                            UseShellExecute = false,
                            Arguments = "/F /IM ISAOPCSV.exe /t",
                            CreateNoWindow = true
                        };
                    System.Diagnostics.Process proc = System.Diagnostics.Process.Start(startInfo);
                    }
                }
                if (OneInstanceRunningMaximum == false)
                    {
                        app = new App(force_cfg, project_file_name);//ft
                        if (app.OK)
                        {
                            System.Windows.Forms.Application.Run();
                        }
                    }
                else
                    {

                        result = System.Windows.Forms.MessageBox.Show("Another CELDAS200 instance is already running","CELDAS200", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    }
			}
			else	//no arguments
			{
                result = System.Windows.Forms.MessageBox.Show("No Arguments Detected associated with PrjMaster_HMI.exe : Please Configure", "CELDAS200", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
			}

		}
	}
}

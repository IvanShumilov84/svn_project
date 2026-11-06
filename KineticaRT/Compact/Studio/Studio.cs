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
		private KineticaRT.Compact.Studio.Application.Project				project ;

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Application
		/// </summary>
		public App()
		{
			//System.Resources.ResourceManager rm = new System.Resources.ResourceManager("StudioDemonstration.Strings", System.Reflection.Assembly.GetExecutingAssembly());
			//System.Windows.Forms.MessageBox.Show(rm.GetString("ApplicationName"));
			
			Initialise(false) ;
		}

		/// <summary>
		/// Initialises a new instance of the Application,
		/// forcing the Configurer window to be visible if requested
		/// </summary>
		public App(bool ShowCfg)
		{
			Initialise(ShowCfg) ;
		}

		private void Initialise(bool ShowCfg)
		{
			project = new KineticaRT.Compact.Studio.Application.Project(ShowCfg) ;
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
		public KineticaRT.Compact.Studio.Application.Project Project
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

			//if we have an argument, check if it is '/c...' (else ignore)
			if (args.Length > 0)
			{
				bool		force_cfg		= false ;
				
				foreach (string arg in args)
				{
					if (arg.ToLower().StartsWith("/c"))
					{
						force_cfg = true ;
						break ;
					}
				}

				app = new App (force_cfg) ;
			}
			else	//no arguments
			{
				app = new App () ;
			}

			if (app.OK)
			{
				System.Windows.Forms.Application.Run();
				//StudioDemonstration.Form1 form1 = new StudioDemonstration.Form1 () ;
				//form1.Show () ;
			}
		}
	}
}

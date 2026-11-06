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
	public class Application
	{
		private Project				project ;

		#region Initialisation
		/// <summary>
		/// Initialises a new instance of the Application
		/// </summary>
		public Application()
		{
			Initialise(false) ;
		}

		/// <summary>
		/// Initialises a new instance of the Application,
		/// forcing the Configurer window to be visible if requested
		/// </summary>
		public Application(bool ShowCfg)
		{
			Initialise(ShowCfg) ;
		}

		private void Initialise(bool ShowCfg)
		{
			project = new Project(ShowCfg) ;
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
				return project.OK ;
			}
		}

		/// <summary>
		/// Gets the Project asscociated with this Application
		/// </summary>
		public Project Project
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
			Application		app ;

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

				app = new Application(force_cfg) ;
			}
			else	//no arguments
			{
				app = new Application() ;
			}

			if (app.OK)
			{
				System.Windows.Forms.Application.Run();
			}
		}
	}
}

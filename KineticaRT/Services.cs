using System;

namespace KineticaRT
{
	/// <summary>
	/// Contains various KineticaRT system services.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public sealed class Services
	{
		/// <summary>
		/// Private constructor to avoid having to create.
		/// </summary>
		private Services ()
		{
		}

#if DEBUG
        static Updater fastUpdater = new Updater ("FastUpdate", 20) ;
#else
        static Updater fastUpdater = new Updater("FastUpdate", 20);
#endif

        static Updater twentyHertzUpdater = new Updater("TwentyHertzUpdater", 50);
        static Updater oneSecondUpdater = new Updater("OneSecondUpdate", 1000);
        static Updater tenSecondUpdater = new Updater("TenSecondUpdate", 10000);
        static Updater oneMinuteUpdater = new Updater("OneMinuteUpdate", 60000);

		/// <summary>Gets a KineticaRT system-defined object.</summary>
		/// <value>An Updater which updates at a fast rate.</value>
		public static Updater FastUpdater	{ get { return fastUpdater ;}}

        /// <summary>Gets a KineticaRT system-defined object.</summary>
        /// <value>An Updater which updates at 20 Hz.</value>
        public static Updater TwentyHertzUpdater { get { return twentyHertzUpdater; } }

        /// <summary>Gets a KineticaRT system-defined object.</summary>
        /// <value>An Updater which updates at 1 Hz.</value>
        public static Updater OneSecondUpdater { get { return oneSecondUpdater; } }

        /// <summary>Gets a KineticaRT system-defined object.</summary>
        /// <value>An Updater which updates at 1 minute intervals.</value>
        public static Updater OneMinuteUpdater { get { return oneMinuteUpdater; } }

        /// <summary>Gets a KineticaRT system-defined object.</summary>
        /// <value>An Updater which updates at 10 second intervals.</value>
        public static Updater TenSecondUpdater { get { return tenSecondUpdater; } }
    }
}

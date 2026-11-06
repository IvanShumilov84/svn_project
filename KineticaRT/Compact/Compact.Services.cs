using System;

namespace KineticaRT
{
	/// <summary>
	/// Contains various KineticaRT system services.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public class Services//: IDisposable
	{

		/// <summary>Gets a KineticaRT system-defined object.</summary>
		/// <value>An Updater which updates at a fast rate.</value>
		public static Updater FastUpdater	{ get { return fastUpdater ;}}
		static Updater fastUpdater = new Updater ("FastUpdate", 10) ;

		/// <summary>Gets a KineticaRT system-defined object.</summary>
		/// <value>An Updater which updates at 1 Hz.</value>
		public static Updater OneSecondUpdater	{ get { return oneSecondUpdater ;}}
		static Updater oneSecondUpdater = new Updater ("OneSecondUpdate", 1000) ;

        /// <summary>
        /// Class destructor.
        /// </summary>
		~Services ()
		{
			Dispose(false);
		}

		#region IDisposable
		private static bool				disposed = false;
        /// <summary>
        /// Releases resources used by the object.
        /// </summary>
        public static void Dispose()
		{
			Dispose(true);
			//GC.SuppressFinalize(this);
		}
        /// <summary>
        /// Releases resources used by the object.
        /// </summary>
        protected static void Dispose(bool disposing)
		{
			if(!disposed)
			{
				if(disposing)
				{
					fastUpdater.Dispose () ;
					oneSecondUpdater.Dispose () ;
				}
			}
			disposed = true;         
		}
		#endregion
	}
}

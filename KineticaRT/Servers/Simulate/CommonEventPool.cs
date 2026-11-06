using System;

namespace KineticaRT.Servers.Simulate
{
	/// <summary>
	/// Class containing the EventPool object shared by most simulate components.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	public class CommonEventPool
	{
		/// <summary>
		/// EventPool object shared by most simulate components.
		/// </summary>
		public static EventPool	Pool = new EventPool () ;
	}
}

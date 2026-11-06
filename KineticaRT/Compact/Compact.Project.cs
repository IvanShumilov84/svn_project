using System;
using System.Collections.Generic;
using System.Text;

namespace KineticaRT.Compact
{
    class Project : Object
    {
		/// <summary>Initializes a new instance of the class.</summary>
		public Project()
		{
		}

        /// <summary>
        /// Reconnects the entire project.
        /// </summary>
        public void Reconnect()
        {
            System.Diagnostics.Debug.WriteLine("KineticaRT.Compact.Project: Reconnect()");
        }
    }
}

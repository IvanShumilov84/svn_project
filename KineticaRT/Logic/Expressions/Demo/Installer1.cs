using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace KRT_Expression
{
	/// <summary>
	/// Summary description for Installer1.
	/// </summary>
	[RunInstaller(true)]
	public class Installer1 : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Installer1()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

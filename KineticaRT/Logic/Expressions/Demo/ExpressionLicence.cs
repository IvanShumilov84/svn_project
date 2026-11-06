using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace KineticaRT.Logic.Expressions
{
	/// <summary>
	/// Summary description for ExpressionLicence.
	/// </summary>
	//[LicenseProviderAttribute(typeof(Expression.LicenseProvider))]
	public class ExpressionLicence : System.ComponentModel.Component
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// ExpressionLicence
		/// </summary>
		/// <param name="container"></param>
		public ExpressionLicence(System.ComponentModel.IContainer container)
		{
			//System.Windows.Forms.MessageBox.Show ("Construct Lic:"+this.GetType()) ; 
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}

			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Initialises the class
		/// </summary>
		public ExpressionLicence()
		{
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

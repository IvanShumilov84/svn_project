using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace Compiler
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Compiler : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCompile;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox errorList;
		private System.Windows.Forms.TextBox txtSequence;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.OpenFileDialog openFile;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Compiler()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			openFile.InitialDirectory = Application.StartupPath ;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtSequence = new System.Windows.Forms.TextBox();
			this.btnCompile = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.errorList = new System.Windows.Forms.ListBox();
			this.txtType = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtSequence
			// 
			this.txtSequence.Location = new System.Drawing.Point(90, 8);
			this.txtSequence.Name = "txtSequence";
			this.txtSequence.Size = new System.Drawing.Size(181, 20);
			this.txtSequence.TabIndex = 0;
			this.txtSequence.Text = "";
			// 
			// btnCompile
			// 
			this.btnCompile.Location = new System.Drawing.Point(240, 64);
			this.btnCompile.Name = "btnCompile";
			this.btnCompile.Size = new System.Drawing.Size(109, 23);
			this.btnCompile.TabIndex = 1;
			this.btnCompile.Text = "Compile Sequence";
			this.btnCompile.Click += new System.EventHandler(this.btnCompile_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(280, 8);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(69, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "Browse...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 14);
			this.label1.TabIndex = 3;
			this.label1.Text = "Sequence File:";
			// 
			// openFile
			// 
			this.openFile.DefaultExt = "*.cs";
			this.openFile.Filter = "C# files (*.cs)|*.cs|All Files (*.*)|*.*";
			this.openFile.Title = "Select Sequence";
			// 
			// errorList
			// 
			this.errorList.HorizontalScrollbar = true;
			this.errorList.Location = new System.Drawing.Point(8, 96);
			this.errorList.Name = "errorList";
			this.errorList.Size = new System.Drawing.Size(336, 160);
			this.errorList.TabIndex = 4;
			// 
			// txtType
			// 
			this.txtType.Location = new System.Drawing.Point(90, 32);
			this.txtType.Name = "txtType";
			this.txtType.Size = new System.Drawing.Size(181, 20);
			this.txtType.TabIndex = 5;
			this.txtType.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 14);
			this.label2.TabIndex = 6;
			this.label2.Text = "Sequence Type:";
			// 
			// Compiler
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(354, 264);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.txtType,
																		  this.label2,
																		  this.errorList,
																		  this.txtSequence,
																		  this.label1,
																		  this.btnBrowse,
																		  this.btnCompile});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Compiler";
			this.Text = "Sequence Compiler";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Compiler());
		}

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			if (openFile.ShowDialog(this) == DialogResult.OK)
			{
				txtSequence.Text = openFile.FileName ;
			}
		}

		private void btnCompile_Click(object sender, System.EventArgs e)
		{
			if (!File.Exists(txtSequence.Text))
			{
				MessageBox.Show("Sequence File does not exist", "Error") ;
			}
			else if (txtType.Text.Length == 0)
			{
				MessageBox.Show("Please specify a sequence type (namespace and class)", "Error") ;
			}
			else
			{
				string		cs		= Path.ChangeExtension(txtSequence.Text, ".cs") ;
				string		dll		= Path.ChangeExtension(txtSequence.Text, ".dll") ;

				errorList.Items.Clear() ;
				CompileSequence(txtType.Text, dll, cs) ;
			}
		}

		private void CompileSequence(string Type, string DllFile, string CsFile)
		{
			// Create a compiler.
			CodeDomProvider		provider		= new Microsoft.CSharp.CSharpCodeProvider ();
			ICodeCompiler		compiler		= provider.CreateCompiler () ;
			CompilerParameters	parameters		= new CompilerParameters () ;
			CompilerResults		results	;

			parameters.MainClass = Type ;
			parameters.OutputAssembly = DllFile ;
			//enable trace messages
			parameters.CompilerOptions = "/d:TRACE" ;

			parameters.ReferencedAssemblies.Add ("System.dll") ;
			parameters.ReferencedAssemblies.Add (Path.Combine(System.Windows.Forms.Application.StartupPath, "KineticaRT.dll")) ;
			parameters.ReferencedAssemblies.Add (Path.Combine(System.Windows.Forms.Application.StartupPath, "KineticaRT.Sequence.dll")) ;

			results = compiler.CompileAssemblyFromFile(parameters, CsFile) ;

			// Check for erorrs.
			if (results.Errors.HasErrors) 
			{
				errorList.Items.Add(Path.GetFileNameWithoutExtension(DllFile) + " - " + results.Errors.Count.ToString() + " Errors:") ;
					
				foreach (CompilerError err in results.Errors)
				{
					errorList.Items.Add("Line: " + err.Line.ToString() + " - " + err.ErrorText) ;
				}
			}
			else
			{
				errorList.Items.Add("Compilation Successful - no errors") ;
			}
		}
	}
}

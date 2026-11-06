using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KineticaRT.Compact.Studio
{
	/// <summary>
	/// Dialog for defining the name, typename and file of a new
	/// or existing Sequence
	/// </summary>
	public class SequenceSetup : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtNamespace;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtClass;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button browse;
		private System.Windows.Forms.OpenFileDialog openFile;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool		createNew ;

		/// <summary>
		/// Initialises a new instance of the NewSequence class
		/// </summary>
		public SequenceSetup()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			createNew = true ;

			txtNamespace.Text = Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) ;
		}

		/// <summary>
		/// Initialises a new instance of the NewSequence class set up to
		/// open an existing sequence based on the specified string
		/// </summary>
		/// <param name="Base">String that provides the defaults for Name, File and Class</param>
		public SequenceSetup(string Base)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			createNew = false ;

			Text = "Open Sequence" ;

			txtNamespace.Text = Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) ;
			txtName.Text = Base ;
			txtFile.Text = Base + ".cs" ;
			txtClass.Text = Base ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SequenceSetup));
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.browse = new System.Windows.Forms.Button();
			this.txtClass = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtNamespace = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Name:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(48, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(128, 20);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.browse,
																					this.txtClass,
																					this.label4,
																					this.txtNamespace,
																					this.label3,
																					this.txtFile,
																					this.label2});
			this.groupBox1.Location = new System.Drawing.Point(4, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(288, 104);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			// 
			// browse
			// 
			this.browse.Location = new System.Drawing.Point(208, 16);
			this.browse.Name = "browse";
			this.browse.Size = new System.Drawing.Size(72, 23);
			this.browse.TabIndex = 2;
			this.browse.Text = "&Browse...";
			this.browse.Click += new System.EventHandler(this.browse_Click);
			// 
			// txtClass
			// 
			this.txtClass.Location = new System.Drawing.Point(72, 72);
			this.txtClass.Name = "txtClass";
			this.txtClass.Size = new System.Drawing.Size(128, 20);
			this.txtClass.TabIndex = 6;
			this.txtClass.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 23);
			this.label4.TabIndex = 5;
			this.label4.Text = "C&lass:";
			// 
			// txtNamespace
			// 
			this.txtNamespace.Location = new System.Drawing.Point(72, 48);
			this.txtNamespace.Name = "txtNamespace";
			this.txtNamespace.Size = new System.Drawing.Size(128, 20);
			this.txtNamespace.TabIndex = 4;
			this.txtNamespace.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 23);
			this.label3.TabIndex = 3;
			this.label3.Text = "Name&space:";
			// 
			// txtFile
			// 
			this.txtFile.Location = new System.Drawing.Point(72, 16);
			this.txtFile.Name = "txtFile";
			this.txtFile.Size = new System.Drawing.Size(128, 20);
			this.txtFile.TabIndex = 1;
			this.txtFile.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "&File:";
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(136, 144);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 3;
			this.okButton.Text = "&OK";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(216, 144);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "&Cancel";
			// 
			// openFile
			// 
			this.openFile.CheckFileExists = false;
			this.openFile.DefaultExt = "cs";
			this.openFile.Filter = "C# files (*.cs)|*.cs|All Files (*.*)|*.*";
			this.openFile.Title = "New Sequence";
			// 
			// SequenceSetup
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(296, 174);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cancelButton,
																		  this.okButton,
																		  this.groupBox1,
																		  this.txtName,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SequenceSetup";
			this.Text = "New Sequence";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>Gets the Filename, including '.cs' extension</summary>
		public string SequenceFile
		{
			get
			{
				return txtFile.Text ;
			}
		}

		/// <summary>Gets the Name</summary>
		public string SequenceName
		{
			get
			{
				return txtName.Text ;
			}
		}

		/// <summary>Gets the sequence Type (namespace and class)</summary>
		public string SequenceType
		{
			get
			{
				return txtNamespace.Text + "." + txtClass.Text ;
			}
		}

		/// <summary>Gets the sequence Namespace</summary>
		public string SequenceNamespace
		{
			get
			{
				return txtNamespace.Text ;
			}
		}

		/// <summary>Gets the sequence Define</summary>
		public string SequenceDefine
		{
			get
			{
				return "Debug;Trace" ;
			}
		}

		/// <summary>Gets the sequence Class</summary>
		public string SequenceClass
		{
			get
			{
				return txtClass.Text ;
			}
		}

		private void txtName_TextChanged(object sender, System.EventArgs e)
		{
			if (createNew)
			{
				string		current_name		= txtName.Text ;

				txtFile.Text = current_name + ".cs" ;
				txtClass.Text = current_name ;
			}
		}

		private void okButton_Click(object sender, System.EventArgs e)
		{
			if (txtName.Text == "")
			{
				MessageBox.Show("Please enter a name", "New Sequence") ;
				txtName.Focus() ;
			}
			else if (txtFile.Text == "")
			{
				MessageBox.Show("Please enter a filename", "New Sequence") ;
				txtFile.Focus() ;
			}
			else if (txtNamespace.Text == "")
			{
				MessageBox.Show("Please enter a namespace", "New Sequence") ;
				txtNamespace.Focus() ;
			}
			else if (txtClass.Text == "")
			{
				MessageBox.Show("Please enter a class name", "New Sequence") ;
				txtClass.Focus() ;
			}
			else
			{
				//file must be *.cs
				txtFile.Text = Path.ChangeExtension(txtFile.Text, ".cs") ;

				if (createNew)
				{
					//check file: must be *.cs and warn if already exists
					if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, txtFile.Text)))
					{
						if (MessageBox.Show("The file '" + txtFile.Text + "' already exists. Overwrite?", "Warning", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
						{
							txtFile.Focus() ;
							return ;
						}
						//else carry on...
					}
				}
				//else open existing

				DialogResult = DialogResult.OK ;
			}
		}

		private void browse_Click(object sender, System.EventArgs e)
		{
			openFile.InitialDirectory = System.Windows.Forms.Application.StartupPath ;
			openFile.FileName = txtFile.Text ;

			if (openFile.ShowDialog(this) == DialogResult.OK)
			{
				txtFile.Text = Path.GetFileName(openFile.FileName) ;
			}
		}
	}
}

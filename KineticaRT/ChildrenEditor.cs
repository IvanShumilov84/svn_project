using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace KineticaRT.Design
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class ChildrenEditorControl: System.Windows.Forms.UserControl
	{
		private ChannelCollection expression ;
		private System.Windows.Forms.Button button1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Initialises a new instance of the class.
		/// </summary>
		public ChildrenEditorControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitForm call
			this.BackColor = System.Drawing.SystemColors.Control;

			this.button1.Click += new System.EventHandler(this.button_Click);
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(64, 40);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(144, 104);
			this.button1.TabIndex = 0;
			this.button1.Text = "Add";
			// 
			// ChildrenEditorControl
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button1});
			this.Name = "ChildrenEditorControl";
			this.Size = new System.Drawing.Size(632, 232);
			//this.Leave += new System.EventHandler(this.ExpressionCtrl_Leave);
			this.ResumeLayout(false);

		}
		#endregion

		//private void ExpressionCtrl_Leave(object sender, System.EventArgs e)
		//{
		//	// expression.Text=ExpressionCtrl.Text ;
		//}

		private void Connect_button_Click(object sender, System.EventArgs e)
		{
			// expression.Text=ExpressionCtrl.Text ;
			// expression.Connect () ;
		}

		private void button_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Button button = sender as System.Windows.Forms.Button ;
			if (button != null)
			{
				//ExpressionCtrl.SelectedText = button.Tag as string ;
			}
		}

		/// <summary>
		/// Sets the expression to be edited.
		/// </summary>
		public ChannelCollection Expression
		{
			get
			{
				return expression ;
			}

			set
			{
				expression = value ;
				//ExpressionCtrl.Text = expression ;
			}
		}

		/// <summary>
		/// Represents the method that will handle the Done event raised when the editing is complete.
		/// </summary>
		public delegate void DoneEventHandler (object sender, EventArgs e);
		/// <summary>Occurs when the editing is complete.</summary>
		public event DoneEventHandler Done;   
		/// <summary>Called when the editing is complete.</summary>
		protected void OnDone(){if (Done != null) Done(this, EventArgs.Empty);}
	}

	//	//[LicenseProviderAttribute(typeof(Expression.LicenseProvider))]

	/// <summary>
	/// UITypeEditor for the expression class.
	/// </summary>
	public class ChildrenEditor : System.Drawing.Design.UITypeEditor 
	{
		private IWindowsFormsEditorService edSvc = null;

		/// <summary>
		/// Edits the value of the specified object.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value">The object to edit.</param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value) 
		{
			//if (context != null && context.Instance != null && provider != null) 
			//Expression expression = context.Instance as Expression ;
			ChannelCollection expression = value as ChannelCollection ;
			ChannelCollection return_value = expression ;

			if (expression != null && provider != null) 
			{
				edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

				if (edSvc != null) 
				{
					//License license = null ;
					//bool designMode = LicenseManager.IsValid (typeof(KineticaRT.Logic.Expressions.Expression), this, out license) && (license.LicenseKey == "Design");
					//if (license != null) {license.Dispose();license = null;}
					//if (designMode)
					//{
					Design.ChildrenEditorControl	editor_control = new ChildrenEditorControl ();
					
					editor_control.Expression = expression ; 
					editor_control.Done += new ChildrenEditorControl.DoneEventHandler (Done) ;
					edSvc.DropDownControl (editor_control) ;

					return_value = editor_control.Expression ;
					//}
				}
			}

			return return_value ;
		}

		/// <summary>
		/// Gets the editor style used by the EditValue method.
		/// </summary>
		/// <param name="context"></param>
		/// <returns>A UITypeEditorEditStyle value that indicates the style of editor used by EditValue. If the UITypeEditor does not support this method, then GetEditStyle will return None.</returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) 
		{
			//if (context != null && context.Instance != null) 
			//{
			return UITypeEditorEditStyle.DropDown;
			//}
			//return base.GetEditStyle(context);
		}

		
		private void Done(object sender, EventArgs e) 
		{
			if (edSvc != null) 
			{
				edSvc.CloseDropDown();
			}
		}
	}
}


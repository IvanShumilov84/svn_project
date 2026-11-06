using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace KineticaRT.Servers.OpcClient.Design
{
	/// <summary>
	/// Summary description for ChannelSelectorEditorControl.
	/// </summary>
	public class ChannelSelectorEditorControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ListView listView1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Displays a listbox for selection of channels.
		/// </summary>
		public ChannelSelectorEditorControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.listView1 = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(0, 0);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(352, 288);
			this.listView1.TabIndex = 0;
			// 
			// ChannelSelectorEditorControl
			// 
			this.Controls.Add(this.listView1);
			this.Name = "ChannelSelectorEditorControl";
			this.Size = new System.Drawing.Size(352, 288);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Sets the value to be edited.
		/// </summary>
		public object Value
		{
			get
			{
				return this.value ;
			}

			set
			{
				this.value = value ;
				//ExpressionCtrl.Text = expression ;
				//KineticaRT.Channels.IChannel channel = value as KineticaRT.Channels.IChannel ;
				KineticaRT.Object channel = value as KineticaRT.Object ;
				if (channel != null)
				{
					this.listView1.Items.Add (new System.Windows.Forms.ListViewItem(channel.Name)) ;

					KineticaRT.IApplication application = channel.SearchObject("Application") as KineticaRT.IApplication ;
					if (application != null)
					{
						
						this.listView1.Items.Add (new System.Windows.Forms.ListViewItem(application.Name)) ;
					}
				}
			}
		}
		object value ;

		/// <summary>
		/// Represents the method that will handle the Done event raised when the editing is complete.
		/// </summary>
		public delegate void DoneEventHandler (object sender, EventArgs e);
		/// <summary>Occurs when the editing is complete.</summary>
		public event DoneEventHandler Done;   
		/// <summary>Called when the editing is complete.</summary>
		protected void OnDone(){if (Done != null) Done(this, EventArgs.Empty);}
	}
}

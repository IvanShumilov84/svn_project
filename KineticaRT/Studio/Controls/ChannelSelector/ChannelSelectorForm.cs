using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using KineticaRT;

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// Summary description for ChannelSelectorForm.
	/// </summary>
	public class ChannelSelectorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDesription;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colDescription;
		private System.Windows.Forms.ListView listNames;

		private	bool		typingName ;

		#region Initialisation and disposal
		/// <summary>
		/// Creates a new instance of the ChannelSelectorForm class
		/// </summary>
		public ChannelSelectorForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			typingName = false ;

			listNames.SelectedIndexChanged += new EventHandler(OnListNewSelection) ;
			listNames.MouseDown += new MouseEventHandler(OnListMouseDown) ;
			txtName.KeyUp += new KeyEventHandler(OnNameKeyUp) ;
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
		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblDesription = new System.Windows.Forms.Label();
			this.listNames = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colDescription = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.Location = new System.Drawing.Point(2, 8);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(48, 24);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "Name:";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(64, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(224, 20);
			this.txtName.TabIndex = 3;
			this.txtName.Text = "";
			// 
			// txtDescription
			// 
			this.txtDescription.Enabled = false;
			this.txtDescription.Location = new System.Drawing.Point(64, 32);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ReadOnly = true;
			this.txtDescription.Size = new System.Drawing.Size(224, 20);
			this.txtDescription.TabIndex = 6;
			this.txtDescription.Text = "";
			// 
			// lblDesription
			// 
			this.lblDesription.Location = new System.Drawing.Point(2, 32);
			this.lblDesription.Name = "lblDesription";
			this.lblDesription.Size = new System.Drawing.Size(64, 24);
			this.lblDesription.TabIndex = 5;
			this.lblDesription.Text = "Description:";
			this.lblDesription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// listNames
			// 
			this.listNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.colName,
																						this.colDescription});
			this.listNames.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.listNames.HideSelection = false;
			this.listNames.Location = new System.Drawing.Point(0, 56);
			this.listNames.MultiSelect = false;
			this.listNames.Name = "listNames";
			this.listNames.Size = new System.Drawing.Size(288, 216);
			this.listNames.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listNames.TabIndex = 7;
			this.listNames.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 150;
			// 
			// colDescription
			// 
			this.colDescription.Text = "Description";
			this.colDescription.Width = 134;
			// 
			// ChannelSelectorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(290, 272);
			this.ControlBox = false;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listNames,
																		  this.txtDescription,
																		  this.lblDesription,
																		  this.txtName,
																		  this.lblName});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ChannelSelectorForm";
			this.Text = "Channel Selection";
			this.ResumeLayout(false);

		}
		#endregion

		#region Public methods
		/// <summary>
		/// Defines the Collection from which available channels are determined
		/// </summary>
		/// <param name="server">Collection containing available channels</param>
		/// <returns>True if successfully set, false if failed (eg if form showing)</returns>
		public bool SetServer (ChannelCollection server)
		{
			return SetServer ("", server) ;
		}

		/// <summary>
		/// Defines the Collection from which available channels are determined
		/// </summary>
		/// <param name="server">Collection containing available channels.</param>
		/// <param name="path">String containing the path to the channels.</param>
		/// <returns>True if successfully set, false if failed (eg if form showing)</returns>
		public bool SetServer (string path, ChannelCollection server)
		{
			//can't do it whilst visible
			if (this.Visible)			return false ;

			//clear old names and descriptions
			listNames.Items.Clear() ;

			if (path == "")
			{
				this.Text = "Channel Selection";
			}
			else
			{
				this.Text = "Channel Selection - " + path ;
			}

			if (server != null)
			{
				// This enumerator goes through each child and adds it to the list.
				foreach (System.Collections.DictionaryEntry child in server)
				{
					//Channels.IInfoChannel channel = server.FindObject(child) as Channels.IInfoChannel ;
					Channels.IInfoChannel channel = child.Value as Channels.IInfoChannel ;
					if (channel != null)
					{
						ListViewItem item = listNames.Items.Add(channel.Name) ;
						item.SubItems.Add(channel.Description) ;
						item.Tag = path + channel.Name ;
					}
				}
			}
			return true ;
		}
		#endregion

		#region Private Methods
		//returns null if no match found, else selects first match
		private ListViewItem FindListItem(string PartialName)
		{
			ListViewItem	match_item		= null ;

			if (listNames.Items.Count == 0)
			{
				return null ;
			}

			if (PartialName != "")
			{
				string		lower_case_name			= PartialName.ToLower() ;

				foreach (ListViewItem item in listNames.Items)
				{
					if (item.Text.ToLower().StartsWith(lower_case_name))
					{
						match_item = item ;
						break ;
					}
				}
			}

			if (match_item == null)
			{
				//clear current selection (if any)
				listNames.SelectedItems.Clear() ;
			}
			else
			{
				//select match_item
				if (!match_item.Selected)
				{
					listNames.SelectedItems.Clear() ;
					match_item.Selected = true ;
					match_item.Focused = true ;
					listNames.EnsureVisible(match_item.Index) ;
				}
			}

			return match_item ;
		}
		#endregion

		#region Events
		//new selection in list box - set text and description
		private void OnListNewSelection (object Sender, EventArgs e)
		{
			if (listNames.SelectedIndices.Count > 0)
			{
				txtDescription.Text = listNames.SelectedItems[0].SubItems[1].Text ;

				//set name unless currently tying in name box
				if (!typingName)
				{
					txtName.Text = listNames.SelectedItems[0].Text ;
				}
			}
			else
			{
				txtDescription.Text = "" ;
			}
		}

		//mouse down in list box - start drag
		private void OnListMouseDown(object Sender, MouseEventArgs e)
		{
			ListViewItem		item	= listNames.GetItemAt(e.X, e.Y) ;

			if (item != null)
			{
				if (!item.Selected)
				{
					listNames.SelectedItems.Clear() ;
					item.Selected = true ;
					item.Focused = true ;
				}

				//start a drag operation
				listNames.DoDragDrop(item.Tag, DragDropEffects.Copy) ;
			}
		}

		// typing in name box - find matching list item
		private void OnNameKeyUp (object sender, KeyEventArgs e)
		{
			typingName = true ;
			FindListItem(txtName.Text) ;
			typingName = false ;
		}
		#endregion
	}
}

using System;
using System.Windows.Forms;
using System.Diagnostics;

using KineticaRT.Compact.Studio.Application;

namespace KineticaRT.Compact.Studio
{
	/// <summary>
	/// TraceListener class which outputs timed messages to a list view.
	/// </summary>
	public class ListTraceListener : TraceListener
	{
		private Configurer		dest		= null ;
		//private ListViewItem	item		= null ;

		/// <summary>
		/// Initialises a new instance of the ListTraceListener class
		/// </summary>
		public ListTraceListener()
		{
			//nothing to do
		}

		/// <summary>
		/// Sets the list view control into which the Listener will write its messages
		/// </summary>
		/// <param name="dest">ListView control</param>
		public void SetDetination(Configurer dest)
		{
			this.dest = dest ;
		}

		/// <summary>
		/// Writes a message to the listener, concatenated onto the previous message unless 'WriteLine' is called
		/// </summary>
		/// <param name="message">Message to write</param>
		public override void Write(string message)
		{
			dest.Write (message) ;
//			if (list != null)
//			{
//				if (item == null)
//				{
//					item	= new ListViewItem(DateTime.Now.ToString()) ;
//					item.SubItems.Add(message) ;
//					list.Items.Insert(0, item) ;
//				}
//				else	//just concatenate the text
//				{
//					item.SubItems[0].Text += message ;
//				}
//			}
		}

		/// <summary>
		/// Writes a complete message to the listener on a new line
		/// </summary>
		/// <param name="message">Message to write</param>
		public override void WriteLine(string message)
		{
			dest.WriteLine (message) ;
//			if (list != null)
//			{
//				item	= new ListViewItem(DateTime.Now.ToString()) ;
//				
//				item.SubItems.Add(message) ;
//				list.Items.Insert(0, item) ;
//
//				//clear the item so the next one goes on a new line
//				item = null ;
//			}
		}
	}
}

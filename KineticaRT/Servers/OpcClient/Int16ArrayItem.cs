using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization ;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;

namespace KineticaRT.Servers.OpcClient
{
	/// <summary>
	/// An OPC Item which has a Integer Array value.
	/// Needs to be connected to a Group in order to work.
	/// </summary>
	[Description("OPC Int16ArrayItem")]
	[Serializable]
	public class Int16ArrayItem:KineticaRT.Channels.NumericChannel, ISerializable, IOPCItem
	{
		OpcItem opcItem ;
		string	itemID ;
		string	groupName ;
		Group	group ;
		private bool connected ;

		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.GetObjectData (info, context) ;
			info.AddValue ("Group", groupName) ;
			info.AddValue ("ItemID", itemID) ;
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected Int16ArrayItem (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.RawValue = KineticaRT.Values.Values.NotConnected ;

			try
			{
				groupName = info.GetString ("Group") ;
				itemID = info.GetString ("ItemID") ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
		}
		#endregion

		#region Construction
		/// <summary>
		/// Initialises an instance of the class.
		/// </summary>
		public Int16ArrayItem()
		{
			base.RawValue = KineticaRT.Values.Values.NotConnected ;
			itemID = "" ;
		}
		#endregion

		#region Connect
		/// <summary>
        /// Connects this object to its <see cref="Group"/>.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			if (!connected)
			{
				group = OnObjectSearch(groupName) as Group ;
				if (group == null)
				{
					System.Diagnostics.Trace.WriteLine (Name + ": Group not found-" + groupName, "Connect") ;
					this.Value = KineticaRT.Values.Values.NotConnected ;
					return ;
				}

				group.Connect () ;

				if (itemID.Length > 0)
				{
					opcItem	= group.CreateItem (itemID, KineticaRT.Values.Values.NotConnected) ;
					opcItem.ValueChanged += new OpcItem.ValueChangedEventHandler (Variable_OnValueChanged) ;
                    opcItem.Connect();//Nouvelle fonction ajoutée permettant d'accrocher le value changed avant d'avoir la mise a jour de la valeur par le serveur et de la propager dans HMI
				}

				connected = true ;
			}		
		}

		/// <summary>
        /// Disconnects this object from its <see cref="Group"/>.
		/// </summary>
		public override void Disconnect ()
		{
			if (connected)
			{
				if (opcItem          != null) opcItem.ValueChanged -= new OpcItem.ValueChangedEventHandler (Variable_OnValueChanged) ;
				opcItem.Disconnect () ;

				opcItem = null ;
				group = null ;
				base.RawValue = KineticaRT.Values.Values.NotConnected ;
				connected = false ;
			}
			base.Disconnect () ;
		}
		#endregion

		#region Properties
		/// <summary>
		/// ID of OPC Item.
		/// See OPC Server documentation for item ID's.
		/// </summary>
		[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[System.ComponentModel.Description("ID of OPC Item to connect to - See the OPC Server documentation for item ID's."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
		public string ItemID
		{
			get
			{
				return itemID ;
			}
			set
			{
				if (itemID != value)
				{
					itemID = value ;
					if (opcItem != null) opcItem.ItemID = itemID ;
					OnPropertyChanged("ItemID") ;
				}
			}
		}


		/// <summary>
		/// Name of Group.
		/// Set this before a Connect
		/// </summary>
		//[Editor(typeof(KineticaRT.Studio.Application.ChannelSelectorEditor), typeof(System.Drawing.Design.UITypeEditor))]
		public string Group
		{
			get
			{
				return groupName ;
			}
			set
			{
				if (groupName != value)
				{
					groupName = value ;
					OnPropertyChanged("Group") ;
				}
			}
		}

		
		/// <summary>
		/// Value of OPC item.
		/// Note that when set, the Value will not change until the OPC server reports that it has actually done so.
		/// </summary>
		public override KineticaRT.Values.Value Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				if (opcItem != null) 
				{
					if (value != null)
					{
						opcItem.Value =  value ;
					}
				}
			}
		}

		#endregion

		#region Delegates
		private void Variable_OnValueChanged (KineticaRT.Values.Value newVal)
		{
			base.Value = newVal ;
		}
		#endregion
	}
}

using System;
using System.Runtime.Serialization ;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Security.Permissions;
using KineticaRT.Channels;

namespace KineticaRT.Servers.OpcClient
{
	/// <summary>
	/// An OPC NumericThresholdItem.
	/// Needs to be connected to a Group in order to work.
	/// </summary>
	[Description("OPC NumericThresholdItem")]
	[Serializable]
	[DefaultProperty("ItemID")]
	public class NumericThresholdItem: NumericThresholdChannel, ISerializable, INumericThresholdChannel, IOPCItem
	{
		OpcItem opcItem ;
		OpcItem opcItem_LowLow	 ;
		OpcItem opcItem_Low		 ;
		OpcItem opcItem_High	 ;
		OpcItem opcItem_HighHigh ;
		string	itemID ;
		string	itemID_LowLow	;
		string	itemID_Low		;
		string	itemID_High		;
		string	itemID_HighHigh ;
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
			info.AddValue ("ItemID",			itemID) ;
			info.AddValue ("ItemID_LowLow",		itemID_LowLow	) ;
			info.AddValue ("ItemID_Low",		itemID_Low		) ;
			info.AddValue ("ItemID_High",		itemID_High		) ;
			info.AddValue ("ItemID_HighHigh",	itemID_HighHigh	) ;
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected NumericThresholdItem (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.RawValue = KineticaRT.Values.Values.NotConnected ;

			try
			{
				groupName = info.GetString ("Group") ;
				itemID			 = info.GetString ("ItemID") ;
				itemID_LowLow	 = info.GetString ("ItemID_LowLow") ;
				itemID_Low		 = info.GetString ("ItemID_Low") ;
				itemID_High		 = info.GetString ("ItemID_High") ;
				itemID_HighHigh	 = info.GetString ("ItemID_HighHigh") ;
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
		public NumericThresholdItem()
		{
			itemID = String.Empty ;
			itemID_LowLow = String.Empty	;
			itemID_Low = String.Empty		;
			itemID_High = String.Empty		;
			itemID_HighHigh = String.Empty ;
			groupName = String.Empty ;

			base.RawValue = KineticaRT.Values.Values.NotConnected ;
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

				if (this.itemID.Length > 0)
				{
                    //ft la ligne group.CreateItem et opcItem.ValueChanged doivent etre exécuté en même temps pcq sinon l'update
                    //du server d'initialisation peut être manqué et le channel demeure a NotConnected.
                    
                    opcItem	= group.CreateItem (itemID, KineticaRT.Values.Values.NotConnected) ;
					opcItem.ValueChanged += new OpcItem.ValueChangedEventHandler (Variable_OnValueChanged) ;
                    opcItem.Connect();//Nouvelle fonction ajoutée permettant d'accrocher le value changed avant d'avoir la mise a jour de la valeur par le serveur et de la propager dans HMI
				}

				if (this.itemID_LowLow.Length > 0)
				{
					opcItem_LowLow	= group.CreateItem (itemID_LowLow, KineticaRT.Values.Values.NegativeInfinity) ;
					opcItem_LowLow.ValueChanged += new OpcItem.ValueChangedEventHandler (Threshold_LowLow_OnValueChanged) ;
                    opcItem_LowLow.Connect();//Nouvelle fonction ajoutée permettant d'accrocher le value changed avant d'avoir la mise a jour de la valeur par le serveur et de la propager dans HMI
				}

				if (this.itemID_Low.Length > 0)
				{
					opcItem_Low	= group.CreateItem (itemID_Low, KineticaRT.Values.Values.NegativeInfinity) ;
					opcItem_Low.ValueChanged += new OpcItem.ValueChangedEventHandler (Threshold_Low_OnValueChanged) ;
                    opcItem_Low.Connect();//Nouvelle fonction ajoutée permettant d'accrocher le value changed avant d'avoir la mise a jour de la valeur par le serveur et de la propager dans HMI
				}

				if (this.itemID_High.Length > 0)
				{
					opcItem_High	= group.CreateItem (itemID_High, KineticaRT.Values.Values.NegativeInfinity) ;
					opcItem_High.ValueChanged += new OpcItem.ValueChangedEventHandler (Threshold_High_OnValueChanged) ;
                    opcItem_High.Connect();//Nouvelle fonction ajoutée permettant d'accrocher le value changed avant d'avoir la mise a jour de la valeur par le serveur et de la propager dans HMI
				}

				if (this.itemID_HighHigh.Length > 0)
				{
					opcItem_HighHigh	= group.CreateItem (itemID_HighHigh, KineticaRT.Values.Values.NegativeInfinity) ;
					opcItem_HighHigh.ValueChanged += new OpcItem.ValueChangedEventHandler (Threshold_HighHigh_OnValueChanged) ;
                    opcItem_HighHigh.Connect();//Nouvelle fonction ajoutée permettant d'accrocher le value changed avant d'avoir la mise a jour de la valeur par le serveur et de la propager dans HMI
				}

				connected = true ;
			}		
			

			
//!
//			if (group.OpcGroup == null)
//			{
//				group.Connect () ;
//			}
//			if (group.OpcGroup == null)
//			{
//				System.Diagnostics.Trace.WriteLine (Name + ": Group not connected-" + groupName, "Connect") ;
//				this.Value = KineticaRT.Values.Values.NotConnected ;
//				return ;
//			}
//
//			object temp = group.OpcGroup.Items ;
//			KRTLib_OPC.ItemCollectionClass items = temp as KRTLib_OPC.ItemCollectionClass ;
//			string name = this.Name  ;
//			items.Add (name) ;
//			opcItem = items.get_FindObject (ref name) as KRTLib_OPC.ItemClass ;
//			if (opcItem != null)
//			{
//				opcItem.ItemID = itemID ;
//				opcItem.OnChange += new KRTLib_OPC._IKRT_EventEvents_OnChangeEventHandler (Variable_OnValueChanged) ;
//			}
//			else
//			{
//				this.Value = KineticaRT.Values.Values.NotConnected ;
//			}
//
//			// Connect Threshold
//			if (this.itemID_LowLow.Length > 0)
//			{
//				string threshold_name = name+"LowLow" ;
//				items.Add (threshold_name) ;
//				opcItem_LowLow = items.get_FindObject (ref threshold_name) as KRTLib_OPC.ItemClass ;
//				if (opcItem_LowLow != null)
//				{
//					opcItem_LowLow.ItemID = itemID_LowLow ;
//					opcItem_LowLow.OnChange += new KRTLib_OPC._IKRT_EventEvents_OnChangeEventHandler (Threshold_LowLow_OnValueChanged) ;
//				}
//				else
//				{
//					this.LowLow = System.Double.NegativeInfinity ;
//				}
//			}
//
//			// Connect Threshold
//			if (this.itemID_Low.Length > 0)
//			{
//				string threshold_name = name+"Low" ;
//				items.Add (threshold_name) ;
//				opcItem_Low = items.get_FindObject (ref threshold_name) as KRTLib_OPC.ItemClass ;
//				if (opcItem_Low != null)
//				{
//					opcItem_Low.ItemID = itemID_Low ;
//					opcItem_Low.OnChange += new KRTLib_OPC._IKRT_EventEvents_OnChangeEventHandler (Threshold_Low_OnValueChanged) ;
//				}
//				else
//				{
//					this.Low = System.Double.NegativeInfinity ;
//				}
//			}
//
//			// Connect Threshold
//			if (this.itemID_High.Length > 0)
//			{
//				string threshold_name = name+"High" ;
//				items.Add (threshold_name) ;
//				opcItem_High = items.get_FindObject (ref threshold_name) as KRTLib_OPC.ItemClass ;
//				if (opcItem_High != null)
//				{
//					opcItem_High.ItemID = itemID_High ;
//					opcItem_High.OnChange += new KRTLib_OPC._IKRT_EventEvents_OnChangeEventHandler (Threshold_High_OnValueChanged) ;
//				}
//				else
//				{
//					this.High = System.Double.NegativeInfinity ;
//				}
//			}
//
//			// Connect Threshold
//			if (this.itemID_HighHigh.Length > 0)
//			{
//				string threshold_name = name+"HighHigh" ;
//				items.Add (threshold_name) ;
//				opcItem_HighHigh = items.get_FindObject (ref threshold_name) as KRTLib_OPC.ItemClass ;
//				if (opcItem_HighHigh != null)
//				{
//					opcItem_HighHigh.ItemID = itemID_HighHigh ;
//					opcItem_HighHigh.OnChange += new KRTLib_OPC._IKRT_EventEvents_OnChangeEventHandler (Threshold_HighHigh_OnValueChanged) ;
//				}
//				else
//				{
//					this.HighHigh = System.Double.NegativeInfinity ;
//				}
//			}

			connected = true ;
		}

		/// <summary>
		/// Disconnects this object from its <see cref="Group"/>.
		/// </summary>
		public override void Disconnect ()
		{
			if (connected)
			{
				if (opcItem != null)
				{
					opcItem.ValueChanged -= new OpcItem.ValueChangedEventHandler (Variable_OnValueChanged) ;
					opcItem.Disconnect () ;
					opcItem = null ;
				}
				if (opcItem_LowLow != null)
				{
					opcItem_LowLow.ValueChanged -= new OpcItem.ValueChangedEventHandler (Threshold_LowLow_OnValueChanged) ;
					opcItem_LowLow.Disconnect () ;
					opcItem_LowLow = null ;
					LowLow = System.Double.NegativeInfinity ;
				}
				if (opcItem_Low != null)
				{
					opcItem_Low.ValueChanged -= new OpcItem.ValueChangedEventHandler (Threshold_Low_OnValueChanged) ;
					opcItem_Low.Disconnect () ;
					opcItem_Low	= null ;
					Low	= System.Double.NegativeInfinity ;
				}
				if (opcItem_High != null)
				{
					opcItem_High.ValueChanged -= new OpcItem.ValueChangedEventHandler (Threshold_High_OnValueChanged) ;
					opcItem_High.Disconnect () ;
					opcItem_High = null ;
					High = System.Double.PositiveInfinity ;
				}
				if (opcItem_HighHigh != null)
				{
					opcItem_HighHigh.ValueChanged -= new OpcItem.ValueChangedEventHandler (Threshold_HighHigh_OnValueChanged) ;
					opcItem_HighHigh.Disconnect () ;
					opcItem_HighHigh = null ;
					HighHigh = System.Double.PositiveInfinity ;
				}
				group			 = null ;
				base.Value = KineticaRT.Values.Values.NotConnected ;
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
		/// ID of OPC Item for the LowLow threshold.
		/// See OPC Server documentation for item ID's.
		/// </summary>
		[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[System.ComponentModel.Description("ID of OPC Item to connect to - See the OPC Server documentation for item ID's."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
		public string ItemIdLowLow
		{
			get
			{
				return itemID_LowLow ;
			}
			set
			{
				if (itemID_LowLow != value)
				{
					itemID_LowLow = value ;
//!					if (opcItem_LowLow != null) opcItem_LowLow.ItemID = itemID_LowLow ;
					OnPropertyChanged("ItemID_LowLow") ;
				}
			}
		}

		/// <summary>
		/// ID of OPC Item for the Low threshold.
		/// See OPC Server documentation for item ID's.
		/// </summary>
		[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[System.ComponentModel.Description("ID of OPC Item to connect to - See the OPC Server documentation for item ID's."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
		public string ItemIdLow
		{
			get
			{
				return itemID_Low ;
			}
			set
			{
				if (itemID_Low != value)
				{
					itemID_Low = value ;
//!					if (opcItem_Low != null) opcItem_Low.ItemID = itemID_Low ;
					OnPropertyChanged("ItemID_Low") ;
				}
			}
		}

		/// <summary>
		/// ID of OPC Item for the High threshold.
		/// See OPC Server documentation for item ID's.
		/// </summary>
		[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[System.ComponentModel.Description("ID of OPC Item to connect to - See the OPC Server documentation for item ID's."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
		public string ItemIdHigh
		{
			get
			{
				return itemID_High ;
			}
			set
			{
				if (itemID_High != value)
				{
					itemID_High = value ;
//!					if (opcItem_High != null) opcItem_High.ItemID = itemID_High ;
					OnPropertyChanged("ItemID_High") ;
				}
			}
		}

		/// <summary>
		/// ID of OPC Item for the HighHigh threshold.
		/// See OPC Server documentation for item ID's.
		/// </summary>
		[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[System.ComponentModel.Description("ID of OPC Item to connect to - See the OPC Server documentation for item ID's."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
		public string ItemIdHighHigh
		{
			get
			{
				return itemID_HighHigh ;
			}
			set
			{
				if (itemID_HighHigh != value)
				{
					itemID_HighHigh = value ;
//!					if (opcItem_HighHigh != null) opcItem_HighHigh.ItemID = itemID_HighHigh ;
					OnPropertyChanged("ItemID_HighHigh") ;
				}
			}
		}


		/// <summary>
		/// Name of Group.
		/// Set this before a Connect
		/// </summary>
		[System.ComponentModel.Description("Name of Group object which this Item uses to specify Server, UpdateRate etc."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
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
//!				if (opcItem != null) opcItem.Value = value.ToDouble () ;
				//if (value != base.Value)
				//{
					if (opcItem != null) opcItem.Value = value ;
				//}
			}
		}

		#endregion

		#region Delegates
		private void Variable_OnValueChanged (KineticaRT.Values.Value newVal)
		{
			base.Value = newVal ;
		}

		private void Threshold_LowLow_OnValueChanged (KineticaRT.Values.Value newVal)
		{
			this.LowLow = newVal.ToDouble () ;
		}

		private void Threshold_Low_OnValueChanged (KineticaRT.Values.Value newVal)
		{
			this.Low = newVal.ToDouble () ;
		}

		private void Threshold_High_OnValueChanged (KineticaRT.Values.Value newVal)
		{
			this.High = newVal.ToDouble () ;
		}

		private void Threshold_HighHigh_OnValueChanged (KineticaRT.Values.Value newVal)
		{
			this.HighHigh = newVal.ToDouble () ;
		}
		#endregion

	}
}

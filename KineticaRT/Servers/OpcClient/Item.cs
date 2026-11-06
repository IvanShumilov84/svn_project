using System;
using System.Runtime.Serialization ;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics ;
using System.Security.Permissions;
 
namespace KineticaRT.Servers.OpcClient
{
	/// <summary>
	/// Provides access to standard OPC parameters for all OPC item classes.
	/// </summary>
	public interface IOPCItem:KineticaRT.Channels.IInfoChannel 
	{
		/// <summary>
		/// Name of Group.
		/// Set this before a Connect
		/// </summary>
		[System.ComponentModel.Description("Name of Group object which this Item uses to specify Server, UpdateRate etc."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
		//[Editor(typeof(KineticaRT.Studio.Application.ChannelSelectorEditor), typeof(System.Drawing.Design.UITypeEditor))]
		string Group
		{
			get ;
			set ;
		}

		/// <summary>
		/// ID of OPC Item.
		/// See OPC Server documentation for item ID's.
		/// </summary>
		//[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[System.ComponentModel.Description("ID of OPC Item to connect to - See the OPC Server documentation for item ID's."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
		string ItemID
		{
			get ;
			set ;
		}
	}

	/// <summary>
	/// An OPC Item.
	/// Needs to be connected to a Group in order to work.
	/// </summary>
	//[Editor(typeof(Design.ItemEditor), typeof(System.Drawing.Design.UITypeEditor))]
	[Description("OPC Item")]
	[Serializable]
	[DefaultProperty("ItemID")]
	public class Item:KineticaRT.Channels.NumericChannel, ISerializable, IOPCItem
	{
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.OpcClient", "OPC Client Messages");
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
			info.AddValue ("Scale", scale) ;
			info.AddValue ("Offset", offset) ;
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected Item (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			base.RawValue = KineticaRT.Values.Values.NotConnected ;

			try
			{
				groupName = info.GetString ("Group") ;
				itemID = info.GetString ("ItemID") ;
				scale = info.GetDouble ("Scale") ;
				offset = info.GetDouble ("Offset") ;
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
		public Item()
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
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Group not found-" + groupName, "Connect") ;
					this.Value = KineticaRT.Values.Values.NotConnected ;
					return ;
				}

				group.ConnectChanged +=new KineticaRT.Servers.OpcClient.Client.ConnectChangedEventHandler(group_ConnectChanged);

				//NS050906 group.Connect () ;

                               
                ConnectOpcItem () ;
                connected = true ;
			}		
		}

		private void ConnectOpcItem ()
		{
			//DisconnectOpcItem () ;
			if (opcItem == null)
			{
				if (this.itemID.Length > 0)
				{
                    //ft la ligne group.CreateItem et opcItem.ValueChanged doivent etre exécuté en même temps pcq sinon l'update
                    //du server d'initialisation peut être manqué et le channel demeure a NotConnected.
                    
                    opcItem = group.CreateItem(itemID, KineticaRT.Values.Values.NotConnected);
                    if (opcItem != null)// && opcItem.ServerHandle != -1) //TODO ft Ici dans les autres types d'opc channel, il n'y a pas cette condition "if"
                    {
                        opcItem.ValueChanged += new OpcItem.ValueChangedEventHandler(Variable_OnValueChanged);
                        opcItem.Connect();//Nouvelle fonction ajoutée permettant d'accrocher le value changed, avant d'avoir la mise a jour de la valeur par le serveur et de la propager dans HMI
                    }
                    
				}
			}
		}

		private void DisconnectOpcItem ()
		{
			if (opcItem != null)
			{
				opcItem.ValueChanged -= new OpcItem.ValueChangedEventHandler (Variable_OnValueChanged) ;
				opcItem.Disconnect () ;
				opcItem = null ;
			}
		}

		/// <summary>
        /// Disconnects this object from its <see cref="Group"/>.
		/// </summary>
		public override void Disconnect ()
		{
			if (connected)
			{
				DisconnectOpcItem () ;

				//NS050906 group.Disconnect () ;
				group.ConnectChanged -=new KineticaRT.Servers.OpcClient.Client.ConnectChangedEventHandler(group_ConnectChanged);
				group = null ;
				base.Value = KineticaRT.Values.Values.NotConnected ;
				connected = false ;
			}
			base.Disconnect () ;
		}

		private void group_ConnectChanged(object sender, KineticaRT.Servers.OpcClient.Client.ConnectChangedEventArgs e)
		{
			if (e.Connected)
			{
				ConnectOpcItem () ;
			}
			else
			{
				DisconnectOpcItem () ;
			}
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
		[System.ComponentModel.Description("Name of Group object which this Item uses to specify Server, UpdateRate etc."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("")]
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
				//if (value != base.Value)
				{
					if (opcItem != null)
					{
						if (value is KineticaRT.Values.Double)
						{
							opcItem.Value = new KineticaRT.Values.Double ((value.ToDouble() - offset) / scale) ;
						}
                        else if (value is KineticaRT.Values.Integer)
                        {
                            opcItem.Value = new KineticaRT.Values.Integer((value.ToDouble() - offset) / scale);
                        }
                        else if (value is KineticaRT.Values.UnsignedInteger)
                        {
                            opcItem.Value = new KineticaRT.Values.UnsignedInteger((value.ToDouble() - offset) / scale);
                        }
                        else
                        {
                            opcItem.Value = value;
                        }
					}
				}
			}
		}

//		internal void SetValue (KineticaRT.Values.Value newVal)
//		{
//			base.Value = newVal ;
//		}
//
		/// <summary>
		/// Scaling factor.
		/// </summary>
		[System.ComponentModel.Description("Scaling factor."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("1.0")]
		public double Scale
		{
			get
			{
				return scale ;
			}
			set
			{
				if (scale != value)
				{
					scale = value ;
					OnPropertyChanged("Scale") ;
				}
			}
		}

		/// <summary>
		/// Offset.
		/// </summary>
		[System.ComponentModel.Description("Offset."), System.ComponentModel.Category("OPC")] 
		[DefaultValue("0")]
		public double Offset
		{
			get
			{
				return offset ;
			}
			set
			{
				if (offset != value)
				{
					offset = value ;
					OnPropertyChanged("Offset") ;
				}
			}
		}

		
		#endregion


		double scale = 1.0 ;
		double offset = 0.0 ;

		#region Delegates
        //ft Nouvelle valeur en provenance du serveur OPC
		private void Variable_OnValueChanged (KineticaRT.Values.Value newVal)
		{
			if (newVal is KineticaRT.Values.Double)
			{
				base.Value = new KineticaRT.Values.Double (newVal.ToDouble() * scale + offset) ;
			}
            else if (newVal is KineticaRT.Values.Integer)
            {
                base.Value = new KineticaRT.Values.Integer(newVal.ToDouble() * scale + offset);
            }
            else if (newVal is KineticaRT.Values.UnsignedInteger)
            {
                base.Value = new KineticaRT.Values.UnsignedInteger(newVal.ToDouble() * scale + offset);
            }
            else if (newVal is KineticaRT.Values.Value)
            {
                base.Value = newVal;
            }
            else
            {
                KineticaRT.Values.Value new_krt_value;

                try
                {
                    System.Double new_val = System.Convert.ToDouble(newVal);
                    new_krt_value = new KineticaRT.Values.Double(new_val * scale + offset);
                }
                catch (System.FormatException)
                {
                    System.String new_val = newVal.ToString();
                    new_krt_value = new KineticaRT.Values.Text(new_val);
                }

                base.Value = new_krt_value;
            }
            //ft System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "KineticaRT.Servers.OpcClient.Item.Variable_OnValueChanged: " + this.Name.ToString() + " : " + newVal.ToString());//ft ajout pour debug
            //ft La ligne ici-haut plante en WPF lorsque le Threshold de couleur d<un channel est atteint et doit changer le Value-BackColor du DusplayStudioWPF via un callback ???
            //ft ERREUR Cannot use a DependencyObject that belongs to a different thread than its parent Freezable.
		}
		#endregion

	}
}

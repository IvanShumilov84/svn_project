using System;
using System.ComponentModel;
using System.Runtime.Serialization ;
using System.Diagnostics ;
using System.Security.Permissions;
using System.ComponentModel.Design;
using KineticaRT;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.ComponentModel;
using System.Drawing.Design ;

namespace KineticaRT.Servers.OpcClient
{
	/// <summary>
	/// An OPC Group.
	/// Needs to be connected to a Client in order to work.
	/// </summary>
	[Description("OPC Group")]
	[Serializable]
	public class Group:KineticaRT.Object, ISerializable 
	{
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.OpcClient", "OPC Client Messages");
		OpcGroup opcGroup ;
		Client client ;
		string clientName ;
		double updateRate = 1  ;
		double deadBand ;
		private bool connected ;
		internal void OnConnectChanged(object sender, Client.ConnectChangedEventArgs e){if (ConnectChanged != null) ConnectChanged(sender, e);}
		internal event Client.ConnectChangedEventHandler ConnectChanged;   


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
			info.AddValue ("Client", clientName) ;
			info.AddValue ("UpdateRate", updateRate) ;
			info.AddValue ("DeadBand", deadBand) ;
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected Group (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try
			{
				clientName = info.GetString ("Client") ;
				updateRate = info.GetDouble ("UpdateRate") ;
				deadBand = info.GetDouble ("DeadBand") ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}

			Initialise () ;
		}
		#endregion

		#region Construction
		/// <summary>
		/// Initialises an instance of the class.
		/// </summary>
		public Group()
		{
			Initialise () ;
		}

		private void Initialise () 
		{
			opcGroup = new OpcGroup () ;
		}
		#endregion

		#region Properties

		/// <summary>
		/// Name of Client.
		/// Set this before a Connect.
		/// </summary>
		//[Editor(typeof(KineticaRT.Studio.Application.ChannelSelectorEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Client
		{
			get
			{
				return clientName ;
			}
			set
			{
				if (clientName != value)
				{
					clientName = value ;
					OnPropertyChanged("Client") ;
				}
			}
		}
		
		/// <summary>
		/// Update rate.
		/// Rate at which updates are transmitted to/from the OPC Server in seconds. (0.1 = 10Hz)
		/// </summary>
		[System.ComponentModel.Description("Rate at which updates are transmitted to/from the OPC Server in seconds. (0.1 = 10Hz)")]
		[DefaultValue(1.0)]
		public double UpdateRate
		{
			get
			{
				return updateRate ;
			}
			set
			{
				if (updateRate != value)
				{
					updateRate = value ;
					if (opcGroup != null) opcGroup.UpdateRate = (int)(updateRate * 1000.0) ;
					OnPropertyChanged("UpdateRate") ;
				}
			}
		}

		/// <summary>
		/// Deadband.
		/// Percentage change required in order to trigger an update to/from the OPC Server.
		/// </summary>
		[DefaultValue(0.0)]
		public double DeadBand
		{
			get
			{
				return deadBand ;
			}
			set
			{
				if (deadBand != value)
				{
					deadBand = value ;
					if (opcGroup != null) opcGroup.DeadBand = (float) deadBand ;
					OnPropertyChanged("DeadBand") ;
				}
			}
		}
		#endregion

		#region Connection
		/// <summary>
        /// Connects this object to its <see cref="Client"/>.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			if (!connected)
			{
				connected = true ;

				client = OnObjectSearch(clientName) as Client ;
				if (client == null)
				{
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceWarning, Name + ": Client not found-" + clientName, "Connect") ;
					return ;
				}

				//NS050906 client.Connect () ;

				opcGroup.Name		= Name ;
				opcGroup.Client		= client ;
				opcGroup.UpdateRate = (int)(updateRate * 1000.0) ;
				opcGroup.DeadBand	= (float) deadBand ;
				opcGroup.Connect () ;

				OnConnectChanged(this, new Client.ConnectChangedEventArgs(true)) ;
			}
		}

		/// <summary>
        /// Disconnects this object from its <see cref="Client"/>.
		/// </summary>
		public override void Disconnect ()
		{
			if (connected)
			{
				opcGroup.Disconnect () ;
				//NS050906 if (client != null) client.Disconnect () ;
				connected = false ;
				OnConnectChanged(this, new Client.ConnectChangedEventArgs(false)) ;
			}
			base.Disconnect () ;
		}

		/// <summary>
		/// For internal use only.
		/// </summary>
		internal OpcItem CreateItem (string itemID, KineticaRT.Values.Value defaultValue)
		{
			//if (client == null || opcGroup == null) return null ;

			return new OpcItem (client, opcGroup, itemID, defaultValue) ;
		}

		#endregion
	}
}


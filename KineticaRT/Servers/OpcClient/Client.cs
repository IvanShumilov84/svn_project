using System;
using System.Runtime.Serialization ;
using KineticaRT_OPC ;
using System.ComponentModel ;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Diagnostics ;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace KineticaRT.Servers.OpcClient
{
	interface IOpcCallback
	{
		int UpdateRate
		{
			get ;
			set ;
		}
	}

	/// <summary>
	/// For internal use only.
	/// </summary>
	internal class OpcItem
	{
		private	string						itemID ;
		private	Client						client ;
		private	OpcGroup					group ;
		private	KineticaRT.Values.Value		defaultValue ;
		//internal KineticaRT.Values.Value	value ;
		internal KineticaRT.Values.Value	currentValue ;
		internal KineticaRT.Values.Value	newValue ;
		internal bool						Updated ;
		internal int						ServerHandle = -1 ;
		internal int						Index = -1 ;
		internal System.Type 				Type ;
		internal delegate void ValueChangedEventHandler(KineticaRT.Values.Value newVal);
		internal event ValueChangedEventHandler ValueChanged;   
		internal void OnValueChanged(KineticaRT.Values.Value newVal){if (ValueChanged != null) ValueChanged(newVal);}
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.OpcClient", "OpcClient Messages");

		/// <summary>
		/// For internal use only.
		/// </summary>
		internal OpcItem (Client client, OpcGroup group, string itemID, KineticaRT.Values.Value defaultValue)
		{
			this.defaultValue = defaultValue ;
			this.client = client ;
			this.group = group ;
			this.itemID = itemID ;
			//ft System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.AddItem", this.itemID) ;
			//ft group.AddItem (this) ;
			//ft System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.AddItem Done", this.itemID) ;

			group.ConnectChanged +=new KineticaRT.Servers.OpcClient.Client.ConnectChangedEventHandler(group_ConnectChanged);
		}
		/// <summary>
		/// For internal use only.
		/// </summary>
		public void Connect()
        {
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "group.AddItem", this.itemID);
            group.AddItem(this);//Cette fonction force le server a envoyer la valeur de L,object, et il faut que le handler soit accroché pour 
                                //updater le channel correctement avant le connect  voir Item.cs  (dans Connect)
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "group.AddItem Done", this.itemID);
        }

		/// <summary>
		/// Gets or sets the OPC ItemID for this item - See OPC Server manual for details of ItemIDs.
		/// </summary>
		public string ItemID
		{
			get
			{
				return itemID ;
			}
			set
			{
				if (itemID  != value)
				{
					group.RemoveItem (this) ;
					itemID  = value ;
					group.AddItem (this) ;
				}
			}
		}
			
		/// <summary>
		/// Gets or sets the value of the opc item.
		/// </summary>
		public KineticaRT.Values.Value Value
		{
			//				get
			//				{
			//					return this.value ;
			//				}
			set
			{
                if ((object)(value) == (object)(KineticaRT.Values.Values.NotConnected)) return;
                
                // NS 2.0.5.1 26 Sept 2011 - Removed check to see if the value has changed.
				lock (group) //ft j'ai pogné un dead lock ici 2015/11/06, il y a eu pwoer failure hier soir et on a perdu la DB sur le poste a Shab et on perdu le reseau.
				{
                    this.newValue = value;
                        
                    if (ServerHandle != -1)
					{
						if (Type != null)
						{
							System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Write: " + value.ToString() + " To: " + ServerHandle.ToString (System.Globalization.CultureInfo.CurrentCulture) + " Type: " + Type.ToString() , this.itemID) ;

                            //ft si je retire cette condition les valeurs des channel sont parfois skippés
							/*//ft 2012*/ if (!Updated)  
							{
								this.Updated = true ; 
                                group.UpdateCount ++ ;
                                System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Update+: " + group.UpdateCount.ToString(System.Globalization.CultureInfo.CurrentCulture), this.itemID) ;
							}
						}
						else
						{
							System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "Write: " + value.ToString() + " Failed: " + ServerHandle.ToString (System.Globalization.CultureInfo.CurrentCulture) + " Type: Undefined" , this.itemID) ;
						}
					}
				}
			}
		}
			
		/// <summary>
		/// Connects this Group to the OPC Server.
		/// </summary>
		public void Disconnect ()
		{
			lock (group)
			{
				if (group != null)
				{
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.RemoveItem", this.itemID) ;
					group.RemoveItem (this) ;
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.RemoveItem Doing", this.itemID) ;
					group.ConnectChanged -=new KineticaRT.Servers.OpcClient.Client.ConnectChangedEventHandler(group_ConnectChanged);
					group = null ;
					client = null ;
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.RemoveItem Done", this.itemID) ;
				}
			}
		}

		private void group_ConnectChanged(object sender, KineticaRT.Servers.OpcClient.Client.ConnectChangedEventArgs e)
		{
			if (e.Connected)
			{
				if (group != null)
				{
					group.AddItem (this) ;
				}
			}
			else
			{
//				Disconnect () ;
				if (group != null)
				{
					currentValue = KineticaRT.Values.Values.NotConnected ;
					OnValueChanged (KineticaRT.Values.Values.NotConnected) ;

					group.RemoveItem (this) ;
				}
			}
		}
	}

	/// <summary>
	/// For internal use only.
	/// </summary>
	[ComVisible(true)]	
	public class OpcGroup
	{
		/// <summary>
		/// For internal use only.
		/// </summary>
		public KineticaRT_OPC.Group opcGroup ; 

		/// <summary>
		/// For internal use only.
		/// </summary>
		private		Client				client ;
		private		OpcItem []			item ;
		private		int					itemCount ;
		private		int					updateRate ;
		private		float				deadBand ;
		private		bool				connected ;
		private		KineticaRT.Updater	updater ;
        KineticaRT.Updater.UpdateEventHandler updateHandler;

		internal	int					UpdateCount ;
		internal	int					Index ;
		private		int					statusCheckCount = 0 ;

		private		string				name ;

		//internal delegate void ConnectChangedEventHandler(object sender, ConnectChangedEventArgs e);
		internal void OnConnectChanged(object sender, Client.ConnectChangedEventArgs e){if (ConnectChanged != null) ConnectChanged(sender, e);}
		internal event Client.ConnectChangedEventHandler ConnectChanged;   

		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.OpcClient", "OpcClient Messages");

		/// <summary>
		/// For internal use only.
		/// </summary>
		internal OpcGroup ()
		{
			updateRate = 100 ;
			Index = -1 ;
			item = new OpcItem [10] ;
			updateQueue = new System.Collections.Generic.Queue<UpdateQueueItem>();
            //ft 2012 updateQueue = new System.Collections.Generic.Queue<UpdateQueueItem>();
            //updateQueue = new System.Collections.Generic.Queue<System.Array>();
            //updateDictionary = new System.Collections.Generic.Dictionary<System.Array,UpdateQueueItem>();


			//updater = new Updater("OPC Group: " + this.Client.Name, UpdateRate) ;
            // updateHandler = new KineticaRT.Updater.UpdateEventHandler(updater_Update);
            // updater = new Updater("OPC Group", updateRate);
            // updater.Update += updateHandler ;
			//!!! updater = null ;
			//client.ConnectChanged +=new KineticaRT.Servers.OpcClient.Client.ConnectChangedEventHandler(client_ConnectChanged);
		}

        /// <summary>
        /// Destructor for class.
        /// </summary>
        ~OpcGroup()
        {
            // updater.Update -= updateHandler;
        }

		private void client_ConnectChanged(object sender, KineticaRT.Servers.OpcClient.Client.ConnectChangedEventArgs e)
		{
			if (e.Connected)
			{
				ConnectOpcGroup () ;
			}
			else
			{
				DisconnectOpcGroup () ;
			}
		}
		//		private void client_ConnectChanged(object sender, KineticaRT.Servers.OpcClient.Client.ConnectChangedEventArgs e)
		//		{
		//			if (e.Connected)
		//			{
		//				Connect () ;
		//			}
		//			else
		//			{
		//				Disconnect () ;
		//			}
		//		}

        private class UpdateQueueItem
        {
            public System.Array id_array;
            public System.Array value_array;
            //ft public Boolean updated;
        }
		System.Collections.Generic.Queue<UpdateQueueItem> updateQueue;

        //private class UpdateQueueItem
        //{
        //    public System.Array id_array ;
        //    public System.Array value_array ;
        //}


        ////ft 2012 System.Collections.Generic.Queue<UpdateQueueItem> updateQueue;
        //System.Collections.Generic.Queue<System.Array> updateQueue;
        //System.Collections.Generic.Dictionary<System.Array,UpdateQueueItem> updateDictionary;

        ///// <summary>
        ///// For internal use only.
        ///// </summary>
        ///// <param name="v1"></param>
        ///// <param name="v2"></param>
        //public void Update(object v1, object v2)
        //{
        //    if (!connected) return;

        //    System.Array id_array = (System.Array)v1;
        //    System.Array value_array = (System.Array)v2;

        //    // Enqueue the new data.
        //    UpdateQueueItem new_item = new UpdateQueueItem();
        //    //new_item.id_array = (System.Array)id_array.Clone();
        //    //new_item.value_array = (System.Array)value_array.Clone();
        //    new_item.id_array = (System.Array)id_array;
        //    new_item.value_array = (System.Array)value_array;
        //    new_item.updated = true; //ft 2012

        //    //ft 20152 lock (updateQueue)
        //    lock (updateQueue)
        //    {
        //        try 
        //        {
        //            updateQueue.Enqueue(new_item.id_array);
        //            updateDictionary[new_item.id_array] = new_item;
        //        }
        //        catch
        //        {
        //            updateDictionary.Add(new_item.id_array, new_item);
        //        }

        //       //ft 2012 System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Update queued: " + id_array.Length.ToString(System.Globalization.CultureInfo.CurrentCulture) + ", " + updateQueue.Count, name);
        //    }
        //}

        /// <summary>
        /// For internal use only.
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public void Update(object v1, object v2)
        {
            if (!connected) return;

            System.Array id_array = (System.Array) v1 ;
            System.Array value_array = (System.Array) v2 ;

            // Enqueue the new data.
            UpdateQueueItem new_item = new UpdateQueueItem();
            //new_item.id_array = (System.Array)id_array.Clone();
            //new_item.value_array = (System.Array)value_array.Clone();
            new_item.id_array = (System.Array)id_array;
            new_item.value_array = (System.Array)value_array;

            lock (updateQueue)
            {
                updateQueue.Enqueue(new_item);

                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Update queued: " + id_array.Length.ToString(System.Globalization.CultureInfo.CurrentCulture) + ", " + updateQueue.Count, name);
            }
        }

        private void Update2()
        {
            //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Update unqueue?", name);

            // lock (this)  // Causes problem with lockup in OnValueChange call If it goes to an invoke on a control.
            {
                if (!connected) return;

                int count;
                lock (updateQueue)
                {
                    count = updateQueue.Count;
                }

                while (count > 0)
                {
                    UpdateQueueItem new_item;
                    //System.Array new_id_array;

                    lock (updateQueue)
                    {
                        count = updateQueue.Count;
                        if (count > 0)
                        {
							new_item = updateQueue.Dequeue();
                            //new_id_array = updateQueue.Dequeue();
                            //new_item = updateDictionary[new_id_array];
                            count--;
                            //if (new_item.updated )
                            //    updateDictionary[new_id_array].updated = false;
                            //else
                            //    continue;//passe au sample suivant
                        }
                        else
                        {
                            new_item = null;
                            System.Diagnostics.Trace.WriteLine("UpdateItem == NULL", name);
                        }
                    }
                    if (new_item == null)
                    {
                        System.Diagnostics.Trace.WriteLine("UpdateItem == NULL 2", name);
                        continue;
                    }

                 


                    System.Array id_array = new_item.id_array;
                    System.Array value_array = new_item.value_array;

                    //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Update unqueued: " + id_array.Length.ToString(System.Globalization.CultureInfo.CurrentCulture), name);

                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewValues: " + id_array.Length.ToString(System.Globalization.CultureInfo.CurrentCulture), name);

                    for (long index = 1; index <= id_array.Length; index++)
                    {
                        uint id = (uint)id_array.GetValue(index, 1);
                        OpcItem current_item = item[id];
                        //if (current_item.Type == null)//ft
                        if (current_item != null)
                        {
                            //ft Ici le lock est en référence avec le lock dans OpcGroup.AddItem voir lock (newItem).  AU moment d'enregistrer l'item sur le Serveur OPC via la ligne
                            // opcGroup.CreateItem(newItem.Index, newItem.ItemID, out type) et a ce moment le serveur reagit tout de suite et tombe dans cette fonction mais le MainThread
                            // n'a pas eu le temps de lire le type et de l'Affacter a item.Type ce qui fait que le type est null et de cette facon la condition ici bas skippé la valeur initiale du channel
                            // alors le lock permet d'Attendre que le main thread finisse l'affectation du type et ce thread attends le release pour bien faire le traitement.
                            
                            if (null == current_item.Type )
                            {
                                lock (current_item)
                                { 
                                    if(null == current_item.Type)
                                    {
                                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.Update2: current_item ou current_item.GetType() est null. Index:" + current_item.ItemID + index.ToString());
                                        return;
                                    }
                                }
                            }
                                
                            object value = value_array.GetValue(index, 1);

                            if (value != null) System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewRawValue: " + value.ToString() + " : " + value.GetType().ToString(), current_item.ItemID);

                            KineticaRT.Values.Value new_value;
                            if (value is double || value is float)
                            {
                                new_value = new KineticaRT.Values.Double(value);
                            }
                            else if (value == null)
                            {
                                //ft Il arrive que l'opcclient recoit des null, et envoie aux channels des empty mais le problème est que certain channels dont les TextChannel on des Get/Set
                                //et le empty va ecraser une bonne valeur via le Set.
                                //new_value = KineticaRT.Values.Values.Empty;  
                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Empty: ", current_item.ItemID);
                                continue;
                            }
                            else if (value is System.DBNull)
                            {
                                //ft Il arrive que l'opcclient recoit des null, et envoie aux channels des empty mais le problème est que certain channels dont les TextChannel on des Get/Set
                                //et le empty va ecraser une bonne valeur via le Set.
                                //new_value = KineticaRT.Values.Values.Empty;
                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "DB-Empty: ", current_item.ItemID);
                                continue;
                            }
                            else if (value is Int64 || value is Int32 || value is Int16 || value is System.SByte)
                            {
                                new_value = new KineticaRT.Values.Integer(value);
                            }
                            else if (value is UInt64 || value is UInt32 || value is UInt16 || value is System.Byte)
                            {
                                new_value = new KineticaRT.Values.UnsignedInteger(value);
                            }
                            else
                            {
                                string value_string = value as string;
                                if (value_string != null) new_value = new KineticaRT.Values.Text(value_string);
                                else if (value is bool) new_value = new KineticaRT.Values.Boolean((bool)value);
                                else
                                {
                                    double[] value_dblarray = value as double[];
                                    if (value_dblarray != null) new_value = new KineticaRT.Values.DoubleArray(value_dblarray);
                                    else
                                    {
                                        Int16[] value_int16 = value as Int16[];
                                        if (value_int16 != null)
                                        {
                                            System.Int16[] ia = new Int16[value_int16.Length];
                                            for (int index2 = 0; index2 < value_int16.Length; index2++)
                                            {
                                                ia[index2] = value_int16[index2];
                                            }
                                            new_value = new KineticaRT.Values.Int16Array(ia);
                                        }
                                        else
                                        {
                                            Int32[] value_int32 = value as Int32[];
                                            if (value_int32 != null)
                                            {
                                                System.Int32[] ia32 = new Int32[value_int32.Length];
                                                for (int index2 = 0; index2 < value_int32.Length; index2++)
                                                {
                                                    ia32[index2] = value_int32[index2];
                                                }
                                                new_value = new KineticaRT.Values.Int32Array(ia32);
                                            }
                                            else
                                            {
                                                string[] value_str = value as string[];
                                                if (value_str != null)
                                                {
                                                    string[] ia32 = new string[value_str.Length];
                                                    for (int index2 = 0; index2 < value_str.Length; index2++)
                                                    {
                                                        ia32[index2] = value_str[index2];
                                                    }
                                                    new_value = new KineticaRT.Values.StringArray(ia32);
                                                }
                                                else
                                                {
                                                    new_value = new KineticaRT.Values.Double(value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            //if (value is double) new_value = new KineticaRT.Values.Double (value) ;
                            //else if (value is float) new_value = new KineticaRT.Values.Double (value) ;
                            //else if (value is string) new_value = new KineticaRT.Values.Text ((string)value) ;
                            //else new_value = KineticaRT.Values.Values.TypeMismatch ;

                            //current_item.Type = value.GetType () ;
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewValue: " + new_value.ToString() + " : " + current_item.Type.ToString(), current_item.ItemID);
                            //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewValue2: " + new_value.ToString() + " : " + new_value.GetType().ToString(), current_item.ItemID);
                            current_item.currentValue = new_value;
                            current_item.OnValueChanged(new_value);
                               
                        }
                    }
                }

                //ft TODO a reactiver System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewValues Done", name);
            }
        }

        //private void Update2 ()
        //{
        //    //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Update unqueue?", name);

        //    // lock (this)  // Causes problem with lockup in OnValueChange call If it goes to an invoke on a control.
        //    {
        //        if (!connected) return;

        //        int count;
        //        lock (updateQueue)
        //        {
        //            count = updateQueue.Count;
        //        }

        //        while (count > 0)
        //        {
        //            UpdateQueueItem new_item;

        //            lock (updateQueue)
        //            {
        //                count = updateQueue.Count;
        //                if (count > 0)
        //                {
        //                    new_item = updateQueue.Dequeue();
        //                    count--;
        //                }
        //                else
        //                {
        //                    new_item = null ;
        //                    System.Diagnostics.Trace.WriteLine ("UpdateItem == NULL", name) ;
        //                }
        //            }
        //            if (new_item == null)
        //            {
        //                System.Diagnostics.Trace.WriteLine ("UpdateItem == NULL 2", name) ;
        //                continue;
        //            }

        //            System.Array id_array = new_item.id_array;
        //            System.Array value_array = new_item.value_array;

        //            //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Update unqueued: " + id_array.Length.ToString(System.Globalization.CultureInfo.CurrentCulture), name);

        //            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewValues: " + id_array.Length.ToString(System.Globalization.CultureInfo.CurrentCulture), name);

        //            for (long index = 1; index <= id_array.Length; index++)
        //            {
        //                uint id = (uint)id_array.GetValue(index, 1);
        //                OpcItem current_item = item[id];
        //                if ((current_item != null) && (current_item.Type != null))
        //                {
        //                    object value = value_array.GetValue(index, 1);

        //                    if (value != null) System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewRawValue: " + value.ToString() + " : " + value.GetType().ToString(), current_item.ItemID);

        //                    KineticaRT.Values.Value new_value;
        //                    if (value is double || value is float)
        //                    {
        //                        new_value = new KineticaRT.Values.Double(value);
        //                    }
        //                    else if (value == null)
        //                    {
        //                        //ft Il arrive que l'opcclient recoit des null, et envoie aux channels des empty mais le problème est que certain channels dont les TextChannel on des Get/Set
        //                        //et le empty va ecraser une bonne valeur via le Set.
        //                        //new_value = KineticaRT.Values.Values.Empty;  
        //                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Empty: ", current_item.ItemID);
        //                        continue;
        //                    }
        //                    else if (value is System.DBNull)
        //                    {
        //                        //ft Il arrive que l'opcclient recoit des null, et envoie aux channels des empty mais le problème est que certain channels dont les TextChannel on des Get/Set
        //                        //et le empty va ecraser une bonne valeur via le Set.
        //                        //new_value = KineticaRT.Values.Values.Empty;
        //                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "DB-Empty: ", current_item.ItemID);
        //                        continue;
        //                    }
        //                    else if (value is Int64 || value is Int32 || value is Int16 || value is System.SByte)
        //                    {
        //                        new_value = new KineticaRT.Values.Integer(value);
        //                    }
        //                    else if (value is UInt64 || value is UInt32 || value is UInt16 || value is System.Byte)
        //                    {
        //                        new_value = new KineticaRT.Values.UnsignedInteger(value);
        //                    }
        //                    else
        //                    {
        //                        string value_string = value as string;
        //                        if (value_string != null) new_value = new KineticaRT.Values.Text(value_string);
        //                        else if (value is bool) new_value = new KineticaRT.Values.Boolean((bool)value);
        //                        else
        //                        {
        //                            double[] value_dblarray = value as double[];
        //                            if (value_dblarray != null) new_value = new KineticaRT.Values.DoubleArray(value_dblarray);
        //                            else
        //                            {
        //                                Int16[] value_int16 = value as Int16[];
        //                                if (value_int16 != null)
        //                                {
        //                                    System.Int16[] ia = new Int16[value_int16.Length];
        //                                    for (int index2 = 0; index2 < value_int16.Length; index2++)
        //                                    {
        //                                        ia[index2] = value_int16[index2];
        //                                    }
        //                                    new_value = new KineticaRT.Values.Int16Array(ia);
        //                                }
        //                                else
        //                                {
        //                                    Int32[] value_int32 = value as Int32[];
        //                                    if (value_int32 != null)
        //                                    {
        //                                        System.Int32[] ia32 = new Int32[value_int32.Length];
        //                                        for (int index2 = 0; index2 < value_int32.Length; index2++)
        //                                        {
        //                                            ia32[index2] = value_int32[index2];
        //                                        }
        //                                        new_value = new KineticaRT.Values.Int32Array(ia32);
        //                                    }
        //                                    else
        //                                    {
        //                                        string[] value_str = value as string[];
        //                                        if (value_str != null)
        //                                        {
        //                                            string[] ia32 = new string[value_str.Length];
        //                                            for (int index2 = 0; index2 < value_str.Length; index2++)
        //                                            {
        //                                                ia32[index2] = value_str[index2];
        //                                            }
        //                                            new_value = new KineticaRT.Values.StringArray (ia32);
        //                                        }
        //                                        else
        //                                        {
        //                                            new_value = new KineticaRT.Values.Double(value);
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    //if (value is double) new_value = new KineticaRT.Values.Double (value) ;
        //                    //else if (value is float) new_value = new KineticaRT.Values.Double (value) ;
        //                    //else if (value is string) new_value = new KineticaRT.Values.Text ((string)value) ;
        //                    //else new_value = KineticaRT.Values.Values.TypeMismatch ;

        //                    //current_item.Type = value.GetType () ;
        //                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewValue: " + new_value.ToString() + " : " + current_item.Type.ToString(), current_item.ItemID);
        //                    current_item.currentValue = new_value;
        //                    current_item.OnValueChanged(new_value);
        //                }
        //            }
        //        }

        //        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.NewValues Done", name);
        //    }
        //}


		/// <summary>
		/// For internal use only.
		/// </summary>
		/// <param name="newItem"></param>
		internal void AddItem (OpcItem newItem)
		{
			lock (this)
			{
				if (opcGroup == null) return ;

				int new_index ;
				if (itemCount == item.Length)
				{
					while ((itemCount > 0) && (item [itemCount-1] == null)) itemCount-- ;
					if (itemCount == item.Length)
					{
						for (new_index = 0 ; (new_index < itemCount) && (item [new_index] != null) ; new_index ++) ;

						if (new_index == itemCount)
						{
							OpcItem [] new_item ;

							new_item = new OpcItem [itemCount * 2] ;//Ici on double le buffer qui contient les item OPC lorsqu'il sa dimension n'est pas suffisante
							item.CopyTo (new_item, 0) ;
							item = new_item ;
						}
					}
					else
					{
						new_index = itemCount ;
					}
				}
				else
				{
					new_index = itemCount ;
				}

				item [new_index] = newItem ;
				newItem.Index = new_index ;
				if (new_index == itemCount)
				{
					itemCount ++ ;
				}

				if (connected)
				{
					object type ;
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.CreateItem", name) ;

                    //ft Ici le lock est en référence avec le lock dans Update2 voir lock(current_item).  AU moment d'enregistrer l'item sur le Serveur OPC via la ligne
                    // opcGroup.CreateItem(newItem.Index, newItem.ItemID, out type) et a ce moment le serveur reagit tout de suite et tombe dans la fonction update2n mais le MainThread
                    // n'a pas eu le temps de lire le type de l'item OPC et de l'Affacter a item.Type ce qui fait que le type est null et de cette facon la condition if current_item.type != null 
                    // skippait la valeur initiale du channel alors le lock permet d'Attendre que le main thread finisse l'affectation du type et thread OPC attends le release pour bien faire le traitement dans update2.
                    lock (newItem)//ft
                    {
                        newItem.ServerHandle = opcGroup.CreateItem(newItem.Index, newItem.ItemID, out type);//ft enregistrement de l'item sur le COM d'OPC GROUP
                        if (newItem.ServerHandle == -1)
                        {
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "group.CreateItem Failed: " + newItem.ItemID, name);
                        }
                        else
                        {
                            if (type == null)
                            {
                                newItem.Type = typeof(string);
                            }
                            else
                            {
                                newItem.Type = type.GetType();
                            }

                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.CreateItem Done, " + newItem.ItemID + "Type: " + newItem.Type.ToString(), name);
                        }
                    }
				}
                else
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.CreateItem Item not connected, Type: " + newItem.Type.ToString(), name);
                }   
			}
		}

		/// <summary>
		/// For internal use only.
		/// </summary>
		/// <param name="newItem"></param>
		internal void RemoveItem (OpcItem newItem)
		{
			lock (this)
			{
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.RemoveItem: " + newItem.ItemID, name);
                if (newItem.ServerHandle != -1 && (item[newItem.Index] != null))
				{
					//System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.DeleteItem", name + "."+ newItem.ItemID) ;
					//opcGroup.DeleteItem (newItem.ServerHandle) ;
					//System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.DeleteItem Done", name + "."+ newItem.ItemID) ;
					deleteItemQueue.Enqueue (newItem.ServerHandle) ;
					item [newItem.Index].ServerHandle = -1 ;
					item [newItem.Index] = null ;
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.RemoveItem queued", name);
                }
			}
		}

		/// <summary>
		/// Rate at which items within this group will be updated.
		/// </summary>
		public Client Client
		{
			get
			{
				return client ;
			}
			set
			{
				if (client != value)
				{
					if (client != null) client.ConnectChanged -= new KineticaRT.Servers.OpcClient.Client.ConnectChangedEventHandler(client_ConnectChanged);
					client  = value ;
					client.ConnectChanged +=new KineticaRT.Servers.OpcClient.Client.ConnectChangedEventHandler(client_ConnectChanged);
					//client.OnConnectChanged () ;
				}
			}
		}

		/// <summary>
		/// Name of group - used for trace messages only.
		/// </summary>
		public string Name
		{
			get
			{
				return name ;
			}
			set
			{
				if (name != value)
				{
					name  = value ;
				}
			}
		}

		/// <summary>
		/// Rate at which items within this group will be updated.
		/// </summary>
		public int UpdateRate
		{
			get
			{
				return updateRate ;
			}
			set
			{
				if (updateRate  != value)
				{
					updateRate  = value ;
					lock (this)
					{
                        if (updater != null)
                        {
                            updater.Update -= updateHandler;
                            updater = new Updater("OPC Group", updateRate);
                            updater.Update += updateHandler;
                        }

                        if (opcGroup != null)
						{
							opcGroup.UpdateRate = updateRate ;
						}
					}
				}
			}
		}

		/// <summary>
		/// Deadband for items within this group.
		/// </summary>
		public float DeadBand
		{
			get
			{
				return deadBand ;
			}
			set
			{
				if (deadBand  != value)
				{
					deadBand  = value ;
					lock (this)
					{
						if (opcGroup != null)
						{
							opcGroup.DeadBand = deadBand ;
						}
					}
				}
			}
		}
			
		/// <summary>
		/// Connects this Group to the OPC Server.
		/// </summary>
		public void Connect ()
		{
			if (connected == true) return ;
			connected = true ;
			ConnectOpcGroup () ;
		}

		internal void ConnectOpcGroup () 
		{
			lock (this)
			{
				if (!connected) return ;
				if (!client.connected) return ;

				if (opcGroup == null)
				{
					if (client.opcClient != null)
					{
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.Create", name) ;
						opcGroup = client.opcClient.CreateGroup() ;
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.Create Done", name) ;
					}
				} 

				if (opcGroup != null)
				{
                    if (updateHandler == null)
                    {
                        updateHandler = new KineticaRT.Updater.UpdateEventHandler(updater_Update);
                        updater = new Updater("OPC Group", updateRate);
                        updater.Update += updateHandler;
                    }

//					OnConnectChanged(this, new Client.ConnectChangedEventArgs(true)) ;

					opcGroup.UpdateRate = UpdateRate ;
					opcGroup.DeadBand = this.deadBand ;
					opcGroup.Destination = this ;
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.Connect", name) ;
					opcGroup.Connected = 1 ;
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.Connect Done", name) ;

					OnConnectChanged(this, new Client.ConnectChangedEventArgs(true)) ;

					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.Connected Items Done", name) ;
					
//					updater = new Updater("OPC Group: " + this.Client.Name, UpdateRate) ;
//					updater.Update +=new KineticaRT.Updater.UpdateEventHandler(updater_Update);

//					for (int index = 0 ; index < itemCount ; index ++)
//					{
//						if ((item [index] != null) && (item [index].ServerHandle == -1))
//						{
//							object type ;
//							System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.CreateItem2", name) ;
//							item [index].ServerHandle = opcGroup.CreateItem (item [index].Index, item [index].ItemID, out type) ;
//							if (item[index].ServerHandle == -1)
//							{
//								System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "group.CreateItem Failed: " + item[index].ItemID, name);
//							}
//							else
//							{
//								if (type == null)
//								{
//									item [index].Type = typeof (string) ;
//								}
//								else
//								{
//									item [index].Type = type.GetType() ;
//								}
//								System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.CreateItem2 Done, Type: " + item [index].Type.ToString(), name) ;
//							}
//						}
//					}
				}
			}
		}

		/// <summary>
		/// Connects this Group to the OPC Server.
		/// </summary>
		public void Disconnect ()
		{
			if (connected == false) return ;
			connected = false ;
			DisconnectOpcGroup () ;
		}

		internal void DisconnectOpcGroup ()
		{
			lock (this)
			{
                if (updateHandler != null)
                {
                    updater.Update -= updateHandler;
                    updater.Dispose();
                    updater = null;
                    updateHandler = null;
                }

				if (opcGroup == null) return ;

				OnConnectChanged(this, new Client.ConnectChangedEventArgs(false)) ;

				//updater = null ;
				//System.Array items = new object [itemCount] ;
				//int delete_item_count = 0 ;

				for (int index = 0 ; index < itemCount ; index ++)
				{
					if ((item [index] != null) && (item [index].ServerHandle != -1))
					{
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.RemoveItem", name + item [index].ItemID) ;
						//items.SetValue (item[index].ServerHandle,delete_item_count++) ;
						RemoveItem (item [index]) ;
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.RemoveItem Done", name) ;
					}
				}

				//System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.DeleteItems: " + delete_item_count.ToString(System.Globalization.CultureInfo.CurrentCulture), name) ;
				//if (delete_item_count > 0)
				//{
				//	opcGroup.DeleteItems (delete_item_count, items) ;
				//}
				EmptyDeleteItemQueue () ;


				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.DeleteItems Done", name) ;
			}

			System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "group.Disconnect", name) ;
			opcGroup.Connected = 0 ;
			System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.Disconnect Done", name) ;
			opcGroup = null ;
		}

		private void EmptyDeleteItemQueue ()
		{
            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "group.EmptyDeleteItemQueue Start", name);
            if (deleteItemQueue.Count > 0)
			{
				System.Array items = new object [deleteItemQueue.Count] ;
				int delete_item_count = 0 ;
				while (deleteItemQueue.Count > 0)
				{
					items.SetValue (deleteItemQueue.Dequeue(), delete_item_count++) ;
				}

                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "group.EmptyDeleteItemQueue Emptying: " + items.Length, name);
                try
                {
                    opcGroup.DeleteItems(delete_item_count, items);
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "group.EmptyDeleteItemQueue Emptied: " + items.Length, name);
                }
                catch (System.Runtime.InteropServices.COMException e)
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.EmptyDeleteItemQueue Failed: " + e.Message, name);
                }
			}
		}
		System.Collections.Queue deleteItemQueue = new System.Collections.Queue () ;

		private void updater_Update()
		{
			if (!connected) return ;

			statusCheckCount ++ ;
			if (statusCheckCount > 100)
			{
				statusCheckCount = 0 ;
			
				bool disconnect = false ;

				lock (this)
				{
					try
					{
                        if (deleteItemQueue.Count > 0) EmptyDeleteItemQueue();
                        if (this.client.opcClient != null)
                        {
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "StatusCheck 1", name);
                            client.SetVendorInfo(this.client.opcClient.VendorInfo.ToString());
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "StatusCheck 2", name);
                        }
                        else
                        {
                            disconnect = true;
                        }
                    }
					catch
					{
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "StatusCheck Failed", name);
                        client.SetVendorInfo("Communications Error"); 
						//client.opcClient.Connected = 0 ;
						//client.opcClient.Connected = 1 ;
//						client.Disconnect () ;
//						connected = false ;
//						for (int index2 = 0 ; index2 < itemCount ; index2 ++)
//						{
//							item [index2].ServerHandle = -1 ;
//							item [index2].currentValue = KineticaRT.Values.Values.NotConnected ;
//							item [index2].OnValueChanged (KineticaRT.Values.Values.NotConnected) ;
//						}
//						opcGroup = null ;
//						client.opcClient = new KineticaRT_OPC.Client () ;
						disconnect = true ;
//						client.Connect() ;
//						Connect() ;
					}
				}

				if (disconnect)
				{
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Reconnect attempt 1", name);
                    client.Disconnect();
					client.Connect() ;
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Reconnect attempt 2", name);
                }
			}

           

            Update2();//ft process les éléments/événements recus de L'OPC

            TransmitChangesToOPC(); //ft process les éléments/événements à envoyer à L'OPC

            /*if (UpdateCount == 0) return ;
			System.Array items = new object [UpdateCount] ;
			System.Array values = new object [UpdateCount] ;

			System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.UpdateItem1", name) ;

			int index = 0 ;
			int item_count = 0 ;

            lock (this)
            {

                try
                {
                    item_count = UpdateCount;

                    for (index = 0; index < item.Length; index++)
                    {
                        if (item[index] != null)
                        {
                            if (item[index].Updated)
                            {
                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.UpdateItem2: " + UpdateCount.ToString(System.Globalization.CultureInfo.CurrentCulture) + ", " + index.ToString(System.Globalization.CultureInfo.CurrentCulture) + ", " + item[index].Updated.ToString(System.Globalization.CultureInfo.CurrentCulture), name);
                                UpdateCount--;
                                item[index].Updated = false;

                                object new_obj;

                                try
                                {
                                    if (item[index].newValue is KineticaRT.Values.StringArray)
                                    {
                                        KineticaRT.Values.StringArray new_val = (KineticaRT.Values.StringArray)item[index].newValue;

                                        new_obj = new_val.ToStringArray();
                                    }
                                    else if (item[index].newValue is KineticaRT.Values.Int16Array)
                                    {
                                        KineticaRT.Values.Int16Array new_val = (KineticaRT.Values.Int16Array)item[index].newValue;

                                        new_obj = new_val.ToInt16Array();
                                    }
                                    else if (item[index].newValue is KineticaRT.Values.Int32Array)
                                    {
                                        KineticaRT.Values.Int32Array new_val = (KineticaRT.Values.Int32Array)item[index].newValue;

                                        new_obj = new_val.ToInt32Array();
                                    }
                                    else if (item[index].newValue is KineticaRT.Values.DoubleArray)
                                    {
                                        KineticaRT.Values.DoubleArray new_val = (KineticaRT.Values.DoubleArray) item[index].newValue;

                                        new_obj = new_val.ToDoubleArray();
                                    }
                                    else if (item[index].Type == typeof(System.Boolean))
                                    {
                                        new_obj = item[index].newValue.ToDouble() != 0.0;
                                    }
                                    else
                                    {
                                        new_obj = System.Convert.ChangeType(item[index].newValue, item[index].Type, System.Globalization.CultureInfo.CurrentCulture);
                                    }
                                }
                                catch (System.FormatException e)
                                {
                                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Warning: (" + item[index].ServerHandle.ToString() + ") " + item[index].newValue.ToString() + ", Failed to convert to: " + item[index].Type.ToString() + ", " + e.Message, name);
                                    new_obj = 0;
                                    try
                                    {
                                        new_obj = System.Convert.ChangeType(item[index].newValue, item[index].Type, System.Globalization.CultureInfo.CurrentCulture);
                                    }
                                    catch (System.FormatException e2)
                                    {
                                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Warning2: (" + item[index].ServerHandle.ToString() + ") " + item[index].newValue.ToString() + ", Failed to convert to: " + item[index].Type.ToString() + ", " + e2.Message, name);
                                    }
                                }

                                items.SetValue(item[index].ServerHandle, UpdateCount);
                                values.SetValue(new_obj, UpdateCount);

                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "UpdateItem: " + items.GetValue(UpdateCount) + ", " + values.GetValue(UpdateCount) + " - " + values.GetValue(UpdateCount).GetType().ToString() + ", " + item[index].newValue.ToString() + " - " + item[index].newValue.GetType().ToString(), name);

                                if (UpdateCount == 0) break;
                            }
                        }
                    }

                    if (UpdateCount > 0)
                    {
                        item_count -= UpdateCount;
                        UpdateCount = 0;
                    }
                }
                catch (System.Exception e)
                {
                    if (index < item.Length)
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "UpdateItem Exception (" + item[index].ItemID + ") : " + e.Message, name);
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "UpdateItem Exception (??) : " + e.Message, name);
                    }

                    for (index = 0; index < item.Length; index++)
                    {
                        if (item[index] != null) item[index].Updated = false;
                    }

                    UpdateCount = 0;
                }
                //catch
                //{
                //    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "Non-CLS Exception", name) ;

                //    for (index = 0 ; index < item.Length ; index ++)
                //    {
                //        if (item [index] != null) item [index].Updated = false ;
                //    }

                //    UpdateCount = 0 ;
                //}
            }

			if (index == item.Length)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "UpdateItem: End of item", name) ;
			}
			else
			{
				try
				{
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.UpdateItem", name) ;
					opcGroup.UpdateItems (item_count, items, values) ;
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "group.UpdateItem Done", name) ;
				}
				catch
				{
					client.SetVendorInfo ("Communications Error") ; 
				}
			}*/
		}

        internal void TransmitChangesToOPC() //ft test pour caller cette fonction à partir du SetValue de l'opc item
        {

            int index = 0;
            int item_count = 0;
            System.Array items = null;
            System.Array values = null;

            lock (this)//ft process les éléments/événements à envoyer à L'OPC
            {//Le lock est deplacé au haut de la fonction pour proteger la variable UpdateCount qui se fait updaté dans l'OpcItem.Value causant une 
                //desynchro entre la grosseur de l'array items et values et la variable UpdateCount et finalement cause une exception IndexOutOfRange

                if (UpdateCount == 0) return;
                items = new object[UpdateCount];
                values = new object[UpdateCount];

                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.UpdateItem1", name);
                                         

                try
                {
                    item_count = UpdateCount;

                    for (index = 0; index < item.Length; index++)
                    {
                        if (item[index] != null)
                        {
                            if (item[index].Updated)
                            {
                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.UpdateItem2: " + UpdateCount.ToString(System.Globalization.CultureInfo.CurrentCulture) + ", " + index.ToString(System.Globalization.CultureInfo.CurrentCulture) + ", " + item[index].Updated.ToString(System.Globalization.CultureInfo.CurrentCulture), name);
                                UpdateCount--;
                                item[index].Updated = false;

                                object new_obj;

                                try
                                {
                                    if (item[index].newValue is KineticaRT.Values.StringArray)
                                    {
                                        KineticaRT.Values.StringArray new_val = (KineticaRT.Values.StringArray)item[index].newValue;

                                        new_obj = new_val.ToStringArray();
                                    }
                                    else if (item[index].newValue is KineticaRT.Values.Int16Array)
                                    {
                                        KineticaRT.Values.Int16Array new_val = (KineticaRT.Values.Int16Array)item[index].newValue;

                                        new_obj = new_val.ToInt16Array();
                                    }
                                    else if (item[index].newValue is KineticaRT.Values.Int32Array)
                                    {
                                        KineticaRT.Values.Int32Array new_val = (KineticaRT.Values.Int32Array)item[index].newValue;

                                        new_obj = new_val.ToInt32Array();
                                    }
                                    else if (item[index].newValue is KineticaRT.Values.DoubleArray)
                                    {
                                        KineticaRT.Values.DoubleArray new_val = (KineticaRT.Values.DoubleArray)item[index].newValue;

                                        new_obj = new_val.ToDoubleArray();
                                    }
                                    else if (item[index].Type == typeof(System.Boolean))
                                    {
                                        new_obj = item[index].newValue.ToDouble() != 0.0;
                                    }
                                    else
                                    {
                                        new_obj = System.Convert.ChangeType(item[index].newValue, item[index].Type, System.Globalization.CultureInfo.CurrentCulture);
                                    }
                                }
                                catch (System.FormatException e)
                                {
                                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Warning: (" + item[index].ServerHandle.ToString() + ") " + item[index].newValue.ToString() + ", Failed to convert to: " + item[index].Type.ToString() + ", " + e.Message, name);
                                    new_obj = 0;
                                    try
                                    {
                                        new_obj = System.Convert.ChangeType(item[index].newValue, item[index].Type, System.Globalization.CultureInfo.CurrentCulture);
                                    }
                                    catch (System.FormatException e2)
                                    {
                                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning, "Warning2: (" + item[index].ServerHandle.ToString() + ") " + item[index].newValue.ToString() + ", Failed to convert to: " + item[index].Type.ToString() + ", " + e2.Message, name);
                                    }
                                }
                                catch (System.Exception e)
                                {//ft Fix d'un bug d'update en batch, pcq en cas d'exception de type exception etait catche par l autre catch en dehors du FOR, et skippait le reste des elements a traiter
                                    //ex. S<il y a 25 element a Updater, et que l exception tombait sur le 1e, alors les 24 autres etait skippe a cause du pertit FOR dans le catch
                                    // plus bas qui execute le item[index].Updated = false;
                                    new_obj = 0;
                                    if (index < item.Length)
                                    {
                                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "UpdateItem Exception1 (" + item[index].ItemID + ") : " + e.Message, name);
                                    }
                                    else
                                    {
                                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "UpdateItem Exception1 (??) : " + e.Message, name);
                                    }
                                }

                                items.SetValue(item[index].ServerHandle, UpdateCount);
                                values.SetValue(new_obj, UpdateCount);

                                
                                

                                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "UpdateItem: " + items.GetValue(UpdateCount) + ", " + values.GetValue(UpdateCount) + " - " + values.GetValue(UpdateCount).GetType().ToString() + ", " + item[index].newValue.ToString() + " - " + item[index].newValue.GetType().ToString(), name);

                                if (UpdateCount == 0) break;
                            }
                        }
                    }

                    if (UpdateCount > 0)
                    {
                        item_count -= UpdateCount;
                        UpdateCount = 0;
                    }
                }
                catch (System.Exception e)
                {
                    if (index < item.Length)
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "UpdateItem Exception2 (" + item[index].ItemID + ") : " + e.Message, name);
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "UpdateItem Exception2 (??) : " + e.Message, name);
                    }

                    for (index = 0; index < item.Length; index++)
                    {
                        if (item[index] != null) item[index].Updated = false;
                    }

                    UpdateCount = 0;
                }
                //catch
                //{
                //    System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "Non-CLS Exception", name) ;

                //    for (index = 0 ; index < item.Length ; index ++)
                //    {
                //        if (item [index] != null) item [index].Updated = false ;
                //    }

                //    UpdateCount = 0 ;
                //}
            }

            if (index == item.Length)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "UpdateItem: End of item", name);
            }
            else
            {
                try
                {
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.UpdateItem", name);
                    opcGroup.UpdateItems(item_count, items, values);
                    System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "group.UpdateItem Done", name);
                }
                catch
                {
                    client.SetVendorInfo("Communications Error");
                }
            }
        }

	}

   
    
	
	/// <summary>
	/// KineticaRT OPC client.<br/>
	/// Connects to any OPC server to send and recieve data values.
	/// </summary>
	//[LicenseProviderAttribute(typeof(Client.LicenseProvider))]
	[Description("OPC Clent")]
	[Serializable]
    [Copyright("Copyright 2003-2010 KineticaRT Ltd.")]
    [ChangeRecord("2.0.1.6", "03 03 2009", "N. Savidge", "Added Int32ArrayItem")]
    [ChangeRecord("2.0.4.1", "06 12 2010", "N. Savidge", "Added Caching of browseInfo")]
    public class Client : Object, ISerializable
	{
		static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.OpcClient", "OPC Client Messages");
		internal KineticaRT_OPC.Client	opcClient ;		// The COM client.
		private string					machineName ;	// The PC to run the OPC Server on.
		private string					serverName ;	// Name of server
		internal bool					connected ;
		private string					vendorInfo = "" ;

		internal class ConnectChangedEventArgs:EventArgs
		{
			private bool newValue ;

			public ConnectChangedEventArgs (bool newValue) 
			{
				this.newValue = newValue ;
			}

			public bool Connected
			{
				get 
				{
					return newValue ;
				}
			}
		}
		internal delegate void ConnectChangedEventHandler(object sender, ConnectChangedEventArgs e);
		internal void OnConnectChanged(object sender, ConnectChangedEventArgs e){if (ConnectChanged != null) ConnectChanged(sender, e);}
		//internal void OnConnectChanged(){if (ConnectChanged != null) ConnectChanged(this, new Client.ConnectChangedEventArgs(this.connected));}
		internal event ConnectChangedEventHandler ConnectChanged;   


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
			info.AddValue ("Server", serverName) ;
			info.AddValue ("Machine", machineName) ;
		}
		/// <summary>
		/// Constructor for ISerializable interface.
		/// </summary>
		protected Client (SerializationInfo info, StreamingContext context):base(info, context)
		{
			if( info == null ) throw new ArgumentNullException("info") ;

			try
			{
				serverName = info.GetString ("Server") ;
				machineName = info.GetString ("Machine") ;
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
		public Client()
		{
//			License license = null;
//			license = LicenseManager.Validate(this.GetType(), this);
//			if (license != null) {license.Dispose();license = null;}
			Initialise () ;
		}

		private void Initialise ()
		{
			System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "client.Create", Server) ;
			try
			{
				opcClient = new KineticaRT_OPC.Client () ;
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "client.Create Done", Server) ;
			}
			catch (System.Exception e)
			{
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceError, "client.Create Failed: " + e.Message , Server) ;
			}
		}

		/// <summary>
		/// Destructor for class.
		/// </summary>
		~Client ()
		{
			System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "client.Destroy", Server) ;
			opcClient = null ;
			System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "client.Destroy Done", Server) ;
		}

		#endregion

		internal void SetVendorInfo (string value)
		{
			if (vendorInfo != value)
			{
				vendorInfo = value ;
				OnPropertyChanged("VendorInfo") ;
			}
		}

		#region properties

		/// <summary>
		/// Returns the list of items available from the connected OPC server for use in an item browser.
		/// </summary>
		public string BrowseInfo
		{
			get 
			{
				lock (this)
				{
					if (!this.connected)
					{
						System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "BrowseInfo: Not Connected", this.Server) ;
						return "" ;
					}

                    if (string.IsNullOrEmpty(browseInfo))
                    {
                        browseInfo = opcClient.BrowseInfo;
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "BrowseInfo: " + browseInfo, this.Server);
                    }

                    return browseInfo ;
				}
			}
		}

        string browseInfo = "";

		/// <summary>
		/// Name of Machine.
		/// Set this before a Connect.
		/// Use "" for local machine
		/// </summary>
		public string Machine
		{
			get
			{
				return machineName ;
			}
			set
			{
				if (machineName  != value)
				{
					machineName  = value ;
					OnPropertyChanged("Machine") ;
				}
			}
		}
		
		/// <summary>
		/// Name of OPC Server.
		/// Set this before doing a Connect.
		/// </summary>
		public string Server
		{
			get
			{
				return serverName ;
			}
			set
			{
				if (serverName != value)
				{
					serverName = value ;
					OnPropertyChanged("Server") ;
				}
			}
		}

		/// <summary>
		/// VendorInfo string from the OPC Server.
		/// Only set after Connection - If it is set then you are connected.
		/// </summary>
		public string VendorInfo
		{
			get
			{
				return vendorInfo ;
//				lock (this)
//				{
//					if (opcClient == null) return "Not Connected" ;
//
//					return opcClient.VendorInfo.ToString () ;
//				}
			}
		}


        /// <summary>
        /// Gets a boolean indicating if currently connected to the specifed server.
        /// Only set after Connection - If it is set then you are connected.
        /// </summary>
        public bool ServerConnected
        {
            get
            {
                lock (this)
                {
                    return (opcClient != null) && (this.opcClient.Connected != 0);
                }
            }
        }

		#endregion

		#region Connect
		/// <summary>
		/// Connect to other Objects.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				if (opcClient == null) return ;
				if (connected) return ;
				opcClient.Machine = machineName ;
				opcClient.Server = serverName ;
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "client.Connect", Server) ;
				opcClient.Connected = 1 ; //ft Cette ligne de code lance le process Isagraff OPC Da Server 3.0
                browseInfo = "";
				System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceVerbose, "client.Connect Done", Server) ;
				connected = true ;
				OnConnectChanged(this, new ConnectChangedEventArgs(true)) ;
                OnPropertyChanged("ServerConnected");
			}
		}

		/// <summary>
		/// Disconnect from other Objects.
		/// </summary>
		public override void Disconnect ()
		{
			lock (this)
			{
				if (connected)
				{
					System.Diagnostics.Trace.WriteLineIf (traceSwitch.TraceInfo, "client.Disconnect", Server) ;
                    try
                    {
                        try
                        {
                            opcClient.Connected = 0;
                            browseInfo = "";
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "client.Disconnect Done.", Server);
                        }
                        catch (System.InvalidCastException)
                        {
                            System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceWarning , "client.Disconnect Failed.", Server);
                        }

                    }
                    catch (System.Runtime.InteropServices.COMException e)
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "client.Disconnect Failed: " + e.Message, Server);
                    }
					connected = false ;
					OnConnectChanged(this, new ConnectChangedEventArgs(false)) ;
                    OnPropertyChanged("ServerConnected");
                }
			}
			base.Disconnect () ;
		}
		
		#endregion
	}
}

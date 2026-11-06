using System;
using System.ComponentModel;
using System.Runtime.Serialization ;
using KineticaRT;

namespace KineticaRT.Logic.Logger
{
	/// <summary>
	/// KineticaRT Logic Object which logs data.
	/// </summary>
	/// <remarks>
	/// This logger requires the following channels to exist before it can be used:<br/>
	/// DataRequirement (Text Input)<br/>
	/// Mode (0=Timed, 1=Continuous. Input)<br/>
	/// GetReady (Boolean Input)<br/>
	/// Ready (Boolean Output)<br/>
	/// Timing (Boolean Input)<br/>		
	/// Complete (Boolean Output)<br/>		
	/// SendResults (Boolean Input)<br/>	
	/// DataResults (Text Output)<br/>
	/// ResultsAvailable (Boolean Output)<br/>
	/// </remarks>
	[ChangeRecord (1.0, "22 04 2004", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="CF", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2004 KineticaRT Ltd.")]
	
	[Description("KineticaRT Logic Object which logs data.")]
	[Serializable]
	public class Logger:KineticaRT.Channels.InfoChannel, ISerializable
	{
		#region ISerializable
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		void ISerializable.GetObjectData (SerializationInfo info, StreamingContext context)
		{
			GetObjectData (info, context) ;
		}
		/// <summary>
		/// Populates a SerializationInfo with the data needed to serialize the target object
		/// </summary>
		/// <param name="info">The SerializationInfo to populate with data.</param>
		/// <param name="context">The destination for this serialization.</param>
		protected new void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
			info.AddValue ("SdLimitsFilename", sdLimitsFilename) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public Logger (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try		{ sdLimitsFilename	= (string)info.GetValue("SdLimitsFilename", typeof(string)); }
			catch	{ }

			Initialise() ;
		}
		#endregion

//		private KineticaRT.Channels.TextChannel		DataRequirementChannel ;
//		private KineticaRT.Channels.NumericChannel	ModeChannel ;
//		private KineticaRT.Channels.BooleanChannel	GetReadyChannel ;
//		private KineticaRT.Channels.BooleanChannel	ReadyChannel ;
//		private KineticaRT.Channels.BooleanChannel	TimingChannel ;
//		private KineticaRT.Channels.BooleanChannel	CompleteChannel ;
//		private KineticaRT.Channels.BooleanChannel	SendResultsChannel ;
//		private KineticaRT.Channels.TextChannel		DataResultsChannel ;
//		private KineticaRT.Channels.BooleanChannel	ResultsAvailableChannel ;
//		private KineticaRT.Channels.TextChannel		MessageChannel ;

//		private KineticaRT.Channels.ValueChangedEventHandler DataRequirementValueChangedHandler ;
//		private KineticaRT.Channels.ValueChangedEventHandler ModeValueChangedHandler ;
//		private KineticaRT.Channels.ValueChangedEventHandler GetReadyValueChangedHandler ;
//		private KineticaRT.Channels.ValueChangedEventHandler TimingValueChangedHandler ;
//		private KineticaRT.Channels.ValueChangedEventHandler SendResultsValueChangedHandler ;

		private enum LoggerState
		{
			egg ,
			hatched ,
			connection_error ,
			connected ,
			get_ready_received ,
			ready ,
			timing_received ,
			logging ,
			timing_dropped ,
			results_collected ,
			send_results_received ,
			results_available ,
			disconnected ,
		} ;

		private string [] loggerStateText =
		{
			"Egg" ,
			"Hatched" ,
			"Connection Error" ,
			"Connected" ,
			"GetReady Received" ,
			"Ready",
			"Timing Received" ,
			"Logging" ,
			"Timing Dropped" ,
			"Results Collected" ,
			"Send Results Received", 
			"Results Available" ,
			"Disconnected" ,
		} ;

//		private LoggerState loggerState ;
		
		private enum modes
		{
			timed,
			continuous,
		} ;

//		private struct Config
//		{
//			public double	baseScanRate ;			// Milliseconds.
//			public int		baseScanRateDivider ;
//		} ;
//
//		Config config ;
		
//		private struct Logging
//		{
//			public class ChannelInfo
//			{
//				public ChannelInfo ()
//				{
//					channelValueChangedHandler = new KineticaRT.Channels.ValueChangedEventHandler(Channel_ValueChanged) ;
//				}
//
//				public void Connect ()
//				{
//					Channel.ValueChanged += channelValueChangedHandler ;
//				}
//
//				public void Disconnect ()
//				{
//					Channel.ValueChanged -= channelValueChangedHandler ;
//				}
//
//				public enum ChannelType
//				{
//					Double,
//					Bool,
//				} ;
////				public string						Name ;
////				public string						SSName ;
////				public KineticaRT.Channels.IChannel Channel ;
//				public KineticaRT.Values.Value		Value ;
////				public ChannelType					Type ;
////				public float []						Floats ;
////				public bool []						Bools ;
////				public double						Sum ;
////				public double						SumSquares ;
////				public bool							Samples ;
////				public bool							SdLimitEnabled ;
////				public double						SdLimit ;
//
//
//				private KineticaRT.Channels.ValueChangedEventHandler channelValueChangedHandler ;
//
//				private void Channel_ValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
//				{
//					Value = newVal ;
//				}
//			} ;
//
//			public	System.Collections.ArrayList			Channel ;
//			public	int										NoOfSamples ;
//			public	int										MaxNoOfSamples ;
//			public	KineticaRT.Updater						Updater ;
//			public  KineticaRT.Updater.UpdateEventHandler	UpdaterUpdateEventHandler ;
//			public	bool									UpdaterUpdateEventHandlerConnected ;
//			public	int										BaseScanRateCounter ;
//			public	double									Next ;
//			public	double									Rate ;
//			public  int										DelayCount ;
//		} ;
//
//		Logging logging ;

		private struct DataRequirement
		{
			public string	Value ;
			public bool		Parsed ;
//			public bool		Valid ;
//			public string	SubSystem ;
//			public double	Period ;
//			public double	Rate ;
		} ;
		
		DataRequirement dataRequirement ;

		private modes		mode ;
		private string		sdLimitsFilename = "" ;

		/// <summary>
		/// Initialises an instanse of this class.
		/// </summary>
		public Logger()
		{
			Initialise () ;
		}

		private void Initialise ()
		{
//			logging.Updater = KineticaRT.Services.FastUpdater ;
//			logging.UpdaterUpdateEventHandler = new KineticaRT.Updater.UpdateEventHandler(updater_Update) ;
//			config.baseScanRate = 1 / logging.Updater.UpdateRate ;
//
//			DataRequirementValueChangedHandler	= new KineticaRT.Channels.ValueChangedEventHandler(DataRequirementValueChanged);
//			ModeValueChangedHandler				= new KineticaRT.Channels.ValueChangedEventHandler(ModeValueChanged);
//			GetReadyValueChangedHandler			= new KineticaRT.Channels.ValueChangedEventHandler(GetReadyValueChanged);
//			TimingValueChangedHandler			= new KineticaRT.Channels.ValueChangedEventHandler(TimingValueChanged);
//			SendResultsValueChangedHandler		= new KineticaRT.Channels.ValueChangedEventHandler(SendResultsValueChanged);
//			
//			SetState (LoggerState.hatched) ;
//			mode = modes.timed ;
			Value = new KineticaRT.Values.Text ("Created") ;
		}

		#region Connection

		/// <summary>
		/// Finds a single channel by name.
		/// Sends an error message if not found.
		/// </summary>
		/// <param name="name">Channel Name</param>
		/// <returns>The channel</returns>
		private KineticaRT.Channels.Channel FindChannel (string name)
		{
			KineticaRT.Channels.Channel channel ;

			channel = OnObjectSearch(name) as KineticaRT.Channels.Channel ;
			if (channel == null)
			{
				SendMessage (name + " channel not found") ;
			}

			return channel ;
		}

		/// <summary>
		/// Connects this object to the required channels"/>.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;
//			lock (this)
//			{
//				// Find all the necessary channels for controlling the logging.
//				MessageChannel = OnObjectSearch("Message") as KineticaRT.Channels.TextChannel ;
//
//				if (MessageChannel != null)
//				{
//					DataRequirementChannel	= FindChannel ("DataRequirement")	as KineticaRT.Channels.TextChannel		;
//					ModeChannel				= FindChannel ("Mode")				as KineticaRT.Channels.NumericChannel	;
//					GetReadyChannel			= FindChannel ("GetReady")			as KineticaRT.Channels.BooleanChannel	;
//					ReadyChannel			= FindChannel ("Ready")				as KineticaRT.Channels.BooleanChannel	;
//					TimingChannel			= FindChannel ("Timing")			as KineticaRT.Channels.BooleanChannel	;
//					CompleteChannel			= FindChannel ("Complete")			as KineticaRT.Channels.BooleanChannel	;
//					SendResultsChannel		= FindChannel ("SendResults")		as KineticaRT.Channels.BooleanChannel	;
//					DataResultsChannel		= FindChannel ("DataResults")		as KineticaRT.Channels.TextChannel		;
//					ResultsAvailableChannel	= FindChannel ("ResultsAvailable")	as KineticaRT.Channels.BooleanChannel	;
//				}
//
//				if 
//					(
//					DataRequirementChannel	!= null &&
//					ModeChannel				!= null &&
//					GetReadyChannel			!= null &&
//					ReadyChannel			!= null &&
//					TimingChannel			!= null &&
//					CompleteChannel			!= null &&
//					SendResultsChannel		!= null &&
//					DataResultsChannel		!= null &&
//					ResultsAvailableChannel	!= null 
//					)
//				{
//					SetState (LoggerState.connected) ;
//					Value = new KineticaRT.Values.Text ("Connected") ;
//				}
//				else
//				{
//					SetState (LoggerState.connection_error) ;
//					SendMessage ("Failed to find channel or channel of wrong type.") ;
//				}
//
//				if (loggerState == LoggerState.connected)
//				{
//					DataRequirementChannel.ValueChanged += DataRequirementValueChangedHandler ;
//					ModeChannel.ValueChanged			+= ModeValueChangedHandler ;
//					GetReadyChannel.ValueChanged		+= GetReadyValueChangedHandler ;
//					TimingChannel.ValueChanged			+= TimingValueChangedHandler ;
//					SendResultsChannel.ValueChanged		+= SendResultsValueChangedHandler ;
//				}
//			}
		}

		/// <summary>
		/// Disconnects this object from all channels.
		/// </summary>
		public override void Disconnect ()
		{
//			lock (this)
//			{
//				if (loggerState == LoggerState.connected)
//				{
//					DataRequirementChannel.ValueChanged -= DataRequirementValueChangedHandler ;
//					ModeChannel.ValueChanged			-= ModeValueChangedHandler ;
//					GetReadyChannel.ValueChanged		-= GetReadyValueChangedHandler ;
//					TimingChannel.ValueChanged			-= TimingValueChangedHandler ;
//					SendResultsChannel.ValueChanged		-= SendResultsValueChangedHandler ;
//				}
//
//				DataRequirementChannel		= null ;
//				ModeChannel					= null ;
//				GetReadyChannel				= null ;
//				ReadyChannel				= null ;
//				TimingChannel				= null ;
//				CompleteChannel				= null ;
//				SendResultsChannel			= null ;
//				DataResultsChannel			= null ;
//				ResultsAvailableChannel		= null ;
//				MessageChannel				= null ;
//
//				loggerState = LoggerState.disconnected ;
//				Value = new KineticaRT.Values.Text (loggerStateText[(int)loggerState]) ;
//			}

			base.Disconnect () ;
		}
		#endregion

		private void SendMessage (string message) 
		{
//			if (MessageChannel != null)
//			{
//				MessageChannel.Value = new KineticaRT.Values.Text (Name + ": " + message) ;
//			}
		}

		private void DataRequirementValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			lock (this)
			{
				dataRequirement.Value = newVal.ToString() ;
				dataRequirement.Parsed = false ;
			}
		}

		private void ModeValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			lock (this)
			{
				mode = (modes) newVal.ToDouble () ;
				if (mode != modes.continuous && mode != modes.timed)
				{
					mode = modes.timed ;
					SendMessage ("Mode set to invalid value; using timed mode.") ;
				}
			}
		}

		private void SetState (LoggerState newState)
		{
//			loggerState = newState ;
//			Value = new KineticaRT.Values.Text (loggerStateText[(int)loggerState]) ;
//
//			if (loggerState >= LoggerState.connected)
//			{
//				ReadyChannel.Value = loggerState == LoggerState.ready ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;
//				CompleteChannel.Value = loggerState == LoggerState.results_collected ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;
//				ResultsAvailableChannel.Value = loggerState == LoggerState.results_available ? KineticaRT.Values.Values.True : KineticaRT.Values.Values.False ;
//
//				if (loggerState != LoggerState.results_available)
//				{
//					DataResultsChannel.Value = KineticaRT.Values.Values.EmptyString ;
//				}
//			}
		}

//		private void CalculateRate ()
//		{
//			double ratio = config.baseScanRate / dataRequirement.Rate ;
//			int rate = (int) System.Math.Floor (ratio + 0.5) ;
//			if (rate < 1) rate = 1 ;
//			config.baseScanRateDivider = rate - 1 ;
//			logging.Rate = config.baseScanRate / rate ;
//
//			if (logging.Rate != dataRequirement.Rate)
//			{
//				SendMessage ("Can not acheive sample rate of " + dataRequirement.Rate + ". Using sample rate of " + logging.Rate.ToString() + ".") ;
//			}
//		}
//
//		private void GetReadyValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
//		{
//			if (newVal.CompareTo (KineticaRT.Values.Values.True) == 0)
//			{
//				SetState (LoggerState.get_ready_received) ;
//
//				lock (this)
//				{
//					//++ Stop logging.
//
//					if (!dataRequirement.Parsed)
//					{
//						// Parse data requirement.
//						dataRequirement.Valid = false ;
//
//						System.Xml.XmlDocument doc = new System.Xml.XmlDocument () ;
//						System.Xml.XmlElement docElement ;
//
//						// Open document.
//						try
//						{
//							doc.LoadXml (dataRequirement.Value) ;
//							docElement = doc.DocumentElement.FirstChild as System.Xml.XmlElement ;
//							dataRequirement.SubSystem = docElement.GetAttribute("SubSystem") ;
//						}
//						catch 
//						{
//							SendMessage ("Data requirement contains no valid XML") ;
//							return ;
//						}
//
//						System.Xml.XmlDocument sd_limits_doc = new System.Xml.XmlDocument () ;
//						System.Xml.XmlElement sd_limits_doc_element = null ;
//
//						// Open document.
//						try
//						{
//							if (sdLimitsFilename.Length > 0)
//							{
//								sd_limits_doc.LoadXml (dataRequirement.Value) ;
//								sd_limits_doc_element = sd_limits_doc.DocumentElement.FirstChild as System.Xml.XmlElement ;
//							}
//						}
//						catch 
//						{
//							SendMessage ("Standard Deviation Limits file contains no valid XML") ;
//							return ;
//						}
//
//
//						try
//						{
//							// Read logging parameters.
//							dataRequirement.Period = System.Convert.ToDouble (docElement.GetAttribute("Period")) ;
//							if (dataRequirement.Period < 1) dataRequirement.Period = 5 ;
//							dataRequirement.Period *= 1000 ;
//
//							dataRequirement.Rate = System.Convert.ToDouble(docElement.GetAttribute("Rate")) ;
//							if (dataRequirement.Rate < 1) dataRequirement.Rate = 100 ;
//							CalculateRate () ;
//
//							logging.MaxNoOfSamples = (int)(dataRequirement.Period / (1000/logging.Rate)) ;
//
//							// Create channel list.
//							logging.Channel = new System.Collections.ArrayList(32) ;
//
//							System.Xml.XmlElement channelsElement ;
//							channelsElement = docElement.FirstChild as System.Xml.XmlElement ;
//							while (channelsElement != null)
//							{
//								if (channelsElement.Name == "Channel")
//								{
//									string enabled_string = channelsElement.GetAttribute("Enabled") ;
//									if (enabled_string != "0" && enabled_string != "false")
//									{
//										string name = channelsElement.GetAttribute("Name") ;
//										string ssName = channelsElement.GetAttribute("SSName") ;
//										string samples_string = channelsElement.GetAttribute("Samples") ;
//										bool samples ;
//
//										if (ssName == "")
//										{
//											ssName = name ;
//										}
//
//										if (samples_string == "" || samples_string == "0" || samples_string == "false")
//										{
//											samples = false ;
//										}
//										else
//										{
//											samples = true ;
//										}
//											
//										KineticaRT.Channels.IChannel channel = OnObjectSearch(ssName) as KineticaRT.Channels.IChannel ;
//										if (channel == null)
//										{
//											SendMessage ("Channel not available - " + ssName + ".") ;
//										}
//										else
//										{
//											// Set up new channel.
//											Logging.ChannelInfo channelInfo = new Logging.ChannelInfo () ;
//
//											// Find SD Limit for this channel.
//											channelInfo.SdLimitEnabled = false ;
//											if (sd_limits_doc_element != null)
//											{
//												System.Xml.XmlElement limits_element ;
//												limits_element = sd_limits_doc_element.FirstChild as System.Xml.XmlElement ;
//												while (limits_element != null)
//												{
//													if (limits_element.Name == "Channel")
//													{
//														string sd_name = limits_element.GetAttribute("Name") ;
//
//														if (sd_name == name)
//														{
//															// Found it.
//															string limit_string = limits_element.GetAttribute("Limit") ;
//															try
//															{
//																channelInfo.SdLimit = System.Convert.ToDouble(limit_string) ;
//																channelInfo.SdLimitEnabled = true ;
//															}
//															catch (System.FormatException)
//															{
//															}
//
//															break ;
//														}
//													}
//
//													limits_element = limits_element.NextSibling  as System.Xml.XmlElement ;
//												}
//											}
//
//											// Set up other info.
//											channelInfo.Name	= name ;
//											channelInfo.SSName	= ssName ;
//											channelInfo.Channel = channel ;
//											channelInfo.Samples = samples ;
//											channelInfo.Value	= channel.Value ;
//
//											if (channel is KineticaRT.Channels.BooleanChannel)
//											{
//												channelInfo.Type = Logging.ChannelInfo.ChannelType.Bool ;
//												if (samples) channelInfo.Bools = new bool [logging.MaxNoOfSamples] ;
//											}
//											else
//											{
//												channelInfo.Type = Logging.ChannelInfo.ChannelType.Double ;
//												if (samples) channelInfo.Floats = new float [logging.MaxNoOfSamples] ;
//											}
//											channelInfo.Connect () ;
//											logging.Channel.Add(channelInfo) ;
//										}
//									}
//								}
//
//								channelsElement = channelsElement.NextSibling  as System.Xml.XmlElement ;
//							}
//
//							// Complete.
//							dataRequirement.Parsed = true ;
//						}
//						catch (System.Exception e)
//						{
//							SendMessage ("Data requirement parse failed: " + e.Message) ;
//							return ;
//						}
//					}
//					else
//					{
//						// Reset channels for another logging session.
//						foreach (Logging.ChannelInfo channel in logging.Channel)
//						{
//							channel.Sum = 0.0 ;
//							channel.SumSquares = 0.0 ;
//							channel.Value = channel.Channel.Value ;
//							channel.Connect () ;
//						}
//					}
//
//					// Initialise state variables.
//					logging.NoOfSamples = 0 ;
//					logging.DelayCount = 0 ;
//
//					if (!logging.UpdaterUpdateEventHandlerConnected)
//					{
//						logging.Updater.Update += logging.UpdaterUpdateEventHandler ;
//						logging.UpdaterUpdateEventHandlerConnected = true ;
//					}
//
//					SetState (LoggerState.ready) ;
//				}
//
//				sender.Value = KineticaRT.Values.Values.False ;
//			}
//		}
//
//		private void TimingValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
//		{
//			lock (this)
//			{
//				if (newVal.CompareTo (KineticaRT.Values.Values.True) == 0)
//				{
//					// True.
//					if (loggerState == LoggerState.ready)
//					{
//						// Start logging.
//						SetState (LoggerState.timing_received) ;
//
//						logging.BaseScanRateCounter = 0 ;
//						logging.Next = System.Environment.TickCount ;
//
//						SetState (LoggerState.logging) ;
//					}
//					else
//					{
//						// Ignore timing signal - not ready to log.
//					}
//				}
//				else
//				{
//					// False.
//					if (loggerState == LoggerState.logging)
//					{
//						// Finish logging.
//						SetState (LoggerState.timing_dropped) ;
//
//						StopLogging () ;
//
//						SetState (LoggerState.results_collected) ;
//					}
//					else
//					{
//						// Ignore timing signal - not logging.
//					}
//				}
//			}
//		}
//
//		private void StopLogging ()
//		{
//			if (logging.UpdaterUpdateEventHandlerConnected)
//			{
//				logging.Updater.Update -= logging.UpdaterUpdateEventHandler ;
//				logging.UpdaterUpdateEventHandlerConnected = false ;
//			}
//
//			foreach (Logging.ChannelInfo channel in logging.Channel)
//			{
//				channel.Disconnect () ;
//			}
//		}
//
//		private void SendResultsValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
//		{
//			if (newVal.CompareTo (KineticaRT.Values.Values.True) == 0)
//			{
//				lock (this)
//				{
//					if (loggerState == LoggerState.results_collected)
//					{
//						SetState (LoggerState.send_results_received) ;
//
//						//string results_string ;
//						System.Text.StringBuilder results_string ;
//
//						results_string = new System.Text.StringBuilder ("<Channels SubSystem=\"" + dataRequirement.SubSystem + "\">",1000) ;
//
//						results_string.Append ("<Diagnostics>" + logging.DelayCount + "</Diagnostics>") ;
//
//						foreach (Logging.ChannelInfo channel in logging.Channel)
//						{
//							double mean ;
//							double sd	;
//
//							if (logging.NoOfSamples == 0)
//							{
//								mean = 0.0 ;
//							}
//							else
//							{
//								mean = channel.Sum / logging.NoOfSamples ;
//							}
//
//							if (logging.NoOfSamples <= 1)
//							{
//								sd = 0 ;
//							}
//							else
//							{
//								double temp1 = (logging.NoOfSamples * channel.SumSquares) - (channel.Sum * channel.Sum) ;
//								double temp2 = logging.NoOfSamples * (logging.NoOfSamples - 1) ;
//								if (temp1 < 0) temp1 = 0 ;
//
//								sd = System.Math.Sqrt (temp1 / temp2) ;
//							}
//
//							string type ;
//							if (channel.Channel is KineticaRT.Channels.NumericChannel)
//							{
//								type = "Analog" ;
//							}
//							else if (channel.Channel is KineticaRT.Channels.BooleanChannel)
//							{
//								type = "Boolean" ;
//							}
//							else
//							{
//								type = "" ;
//							}
//
//							results_string.Append ("<Channel Name=\"" + channel.Name + "\" Type=\"" + type + "\" Rate=\"" + logging.Rate + "\"><D>" + mean + "</D><D>" + sd + "</D>") ;
//						
//							if (channel.Samples)
//							{
//								if (channel.Type == Logging.ChannelInfo.ChannelType.Bool)
//								{
//									for (int index = 0 ; index < logging.NoOfSamples ; index ++)
//									{
//										if (channel.Bools[index])
//										{
//											results_string.Append ("<D>1</D>") ;
//										}
//										else
//										{
//											results_string.Append ("<D>0</D>") ;
//										}
//									}
//								}
//								else
//								{
//									for (int index = 0 ; index < logging.NoOfSamples ; index ++)
//									{
//										results_string.Append ("<D>" + channel.Floats[index].ToString() + "</D>") ;
//									}
//								}
//							}
//						
//							results_string.Append ("</Channel>") ;
//						}
//					
//						results_string.Append ("</Channels>") ;
//					
//						DataResultsChannel.Value = new KineticaRT.Values.Text (results_string.ToString()) ;
//						SetState (LoggerState.results_available) ;
//					}
//
//					sender.Value = KineticaRT.Values.Values.False ;
//				}
//			}
//		}

//		private void updater_Update()
//		{
//			lock (this)
//			{
//				if (loggerState == LoggerState.logging)
//				{
//					if (logging.BaseScanRateCounter == 0)
//					{
//						double delay ;
//
//						do
//						{
//							// Take a sample.
//							foreach (Logging.ChannelInfo channel in logging.Channel)
//							{
//								if (channel.Type == Logging.ChannelInfo.ChannelType.Bool)
//								{
//									KineticaRT.Values.Boolean bool_value = channel.Value as KineticaRT.Values.Boolean ;
//									bool new_value = (bool_value != null) ? bool_value.ToBoolean () : false ;
//									double new_double_value = new_value ? 1.0 : 0.0 ;
//									channel.Sum += new_double_value ;
//									channel.SumSquares += new_double_value * new_double_value ;
//									if (channel.Samples)
//									{
//										channel.Bools [logging.NoOfSamples] = new_value ;
//									}
//								}
//								else
//								{
//									double new_value = channel.Value != null ? channel.Value.ToDouble () : 0.0 ;
//									channel.Sum += new_value ;
//									channel.SumSquares += new_value * new_value ;
//									if (channel.Samples)
//									{
//										channel.Floats[logging.NoOfSamples] = (float) new_value ;
//									}
//								}
//							}
//
//							logging.NoOfSamples ++ ;
//				
//							if (logging.NoOfSamples == logging.MaxNoOfSamples)
//							{
//								StopLogging () ;
//								SetState (LoggerState.results_collected) ;
//							}
//
//							delay = (System.Environment.TickCount - logging.Next) ;
//							logging.Next += 1000 / logging.Rate ;
//
//							if (delay > 1000/logging.Rate)
//							{
//								logging.DelayCount ++ ;
//							}
//						}
//						while ((loggerState == LoggerState.logging) && (delay > 1000/logging.Rate)) ;
//
//						logging.BaseScanRateCounter = config.baseScanRateDivider ;
//					}
//					else
//					{
//						logging.BaseScanRateCounter -- ;
//					}
//				}
//			}
//		}

		/// <summary>
		/// Name of file containing the Standard Deviation Limits.  Limits will not be checked if set to "".
		/// </summary>
		/// <value>String containg the name of the file containing the Standard Deviation Limits.</value>
		[System.ComponentModel.Description("String containg the name of the file containing the Standard Deviation Limits.."), System.ComponentModel.Category("Logger")] 
		[DefaultValue("")]
		public string SdLimitsFilename
		{
			get
			{
				return sdLimitsFilename ;
			}
			set
			{
				if (sdLimitsFilename != value)
				{
					sdLimitsFilename = value ;
					OnPropertyChanged("SdLimitsFilename") ;
				}
			}
		}
	}
}

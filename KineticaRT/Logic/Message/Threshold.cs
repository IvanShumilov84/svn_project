using System;
using KineticaRT;
using System.Diagnostics ;
using System.Runtime.Serialization ;
using System.IO ;
using System.Text ;
using System.ComponentModel;
using System.Drawing;

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// KineticaRT Channel that records when another channel passes a threshold value.<br/>
	/// </summary>
	/// <remarks>A message is created for each pass of the threshold value.  The message may be passed to other Message components for display, logging etc.</remarks>
	[Serializable]
	//[LicenseProviderAttribute(typeof(Threshold.LicenseProvider))]
	public class Threshold:KineticaRT.Channels.Channel, ISerializable 
	{
		#region License
//		// Do not move this class or its contents into another namespace - it would not be secure.
//	
//		////[LicenseProviderAttribute(typeof(Class.LicenseProvider))]
//		//private License license = null;
//		//license = LicenseManager.Validate(this.GetType(), this);
//		////demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
//		//if (license != null) {license.Dispose();license = null;}
//		/// <summary>
//		/// Implements a license provider for the class.
//		/// </summary>
//		public class LicenseProvider: System.ComponentModel.LicenseProvider
//		{
//			/// <summary>
//			/// Gets a license for an instance or type of component, when given a context and whether the denial of a license throws an exception
//			/// </summary>
//			/// <param name="context">A LicenseContext that specifies where you can use the licensed object.</param>
//			/// <param name="type">A Type that represents the component requesting the license.</param>
//			/// <param name="instance">An object that is requesting the license. </param>
//			/// <param name="allowExceptions">true if a LicenseException should be thrown when the component cannot be granted a license; otherwise, false.</param>
//			/// <returns>A valid License.</returns>
//			public override System.ComponentModel.License GetLicense(LicenseContext context,Type type,object instance,bool allowExceptions)
//			{
//				License licence = null ;
//
//				if (context != null)
//				{
//					string key = null ;
//
//					if (context.UsageMode == LicenseUsageMode.Designtime)
//					{
//						Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+"KineticaRT.Logic.Message") ; 
//						if (registry != null) key = registry.GetValue ("RegKey") as string ;
//						if ((key == null) || (key != "6653EB1F8A8D" && key != "4CD0571EE68D"))
//						{
//							new KineticaRT.LicenseForm ("KineticaRT.Logic.Message").ShowDialog () ;
//							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+"KineticaRT.Logic.Message") ; 
//							if (registry != null) key = registry2.GetValue ("RegKey") as string ;
//						}
//						if ((key != null) && (key == "6653EB1F8A8D" || key == "4CD0571EE68D")) key = "{6653EB1F-8A8D-4974-BE32-FA7872CA8C4C}" ; 
//						context.SetSavedLicenseKey (typeof(Threshold), key) ;
//						
//					}
//					if  (key == null) key = context.GetSavedLicenseKey (typeof(Threshold), null) ;
//					if (key == "{6653EB1F-8A8D-4974-BE32-FA7872CA8C4C}") licence = new License ("Run") ;
//					else if (allowExceptions && (context.UsageMode != LicenseUsageMode.Designtime)) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
//					else licence = new License ("None") ;
//				}
//
//				return licence ;
//			}
//
//			private class License:System.ComponentModel.License 
//			{
//				string key ;
//
//				public License (string key)
//				{
//					this.key = key ;
//				}
//
//				public override string LicenseKey 
//				{
//					get
//					{
//						return key ;
//					}
//				}
//		
//				public override void Dispose()
//				{
//				}
//			}
//		}
		#endregion

		enum states
		{
			under,
			equal,
			over
		} ;

		private string		source = "" ;
		private string		destination = "" ;
        private string      thresholdSource = "";
		private string		sourceText = "" ;
		private int			priority ;
        private states      state = states.equal;
		private string		text = "" ;
        private Color       textColor;
		private string		returnText = "" ;
        private Color       returnTextColor;
		private bool		connected = false ;
		private double		level = 90.0 ;

		private KineticaRT.Channels.IChannel source_object ;
		private KineticaRT.Channels.IChannel destination_object ;
        private KineticaRT.Channels.IChannel threshold_source_object;
        private KineticaRT.Channels.ValueChangedEventHandler source_value_changed_handler;
        private KineticaRT.Channels.ValueChangedEventHandler threshold_source_value_changed_handler; 

		#region Construction
		/// <summary>
		/// Initialises a new instance of the class
		/// </summary>
		public Threshold()
		{
//			License license = null;
//			license = LicenseManager.Validate(this.GetType(), this);
//			if (license != null) {license.Dispose();license = null;}
			
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
            threshold_source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(ThresholdSource_OnValueChanged);

		}
		#endregion

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
			info.AddValue ("Source", source) ;
			info.AddValue ("Destination", destination) ;
            info.AddValue ("ThresholdSource", thresholdSource);
			info.AddValue ("SourceText", sourceText) ;
			info.AddValue ("Text", text) ;
            info.AddValue ("TextColor", textColor.ToString());
			info.AddValue ("ReturnText", returnText) ;
            info.AddValue ("ReturnTextColor", returnTextColor.ToString());
            info.AddValue ("Level", level);
			info.AddValue ("Priority", priority) ;
        }
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Threshold (SerializationInfo info, StreamingContext context):base(info, context)
		{
			License license = null;
			license = LicenseManager.Validate(this.GetType(), this);
			if (license != null) {license.Dispose();license = null;}
			
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
            threshold_source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(ThresholdSource_OnValueChanged);

			try
			{
				source = info.GetString ("Source") ;
				destination = info.GetString ("Destination") ;
                thresholdSource = info.GetString("ThresholdSource");
				sourceText = info.GetString ("SourceText") ;
				text = info.GetString ("Text") ;
                textColor = Color.FromName(info.GetString("TextColor"));
				returnText = info.GetString ("ReturnText") ;
                returnTextColor = Color.FromName(info.GetString("ReturnTextColor"));
                level = (double)info.GetValue("Level", typeof(double));
				priority = info.GetInt32 ("Priority") ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				if (connected)
				{
					source_object.ValueChanged -= source_value_changed_handler ;
					destination_object = null ;
                    if (threshold_source_object != null)
                    {
                        threshold_source_object.ValueChanged -= threshold_source_value_changed_handler;
                        threshold_source_object = null;
                    }
					connected = false ;
				}

                if (threshold_source_object == null && thresholdSource != "") threshold_source_object = OnObjectSearch(thresholdSource) as KineticaRT.Channels.Channel;

                if (source_object == null) source_object = OnObjectSearch(source) as KineticaRT.Channels.Channel;
				
				if (source_object != null)
				{
					destination_object = this.FindObject (destination) as KineticaRT.Channels.Channel ;
					if (destination_object == null) destination_object = OnObjectSearch (destination) as KineticaRT.Channels.Channel ;

					source_object.ValueChanged += source_value_changed_handler ;
                    if (threshold_source_object != null)
                    {
                        level = threshold_source_object.Value.ToDouble();
                        threshold_source_object.ValueChanged += threshold_source_value_changed_handler;
                    }
                    connected = true;
				}
			}
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect () ;
			
			lock (this)
			{
				if (connected)
				{
					source_object.ValueChanged -= source_value_changed_handler ;
					source_object = null ;
					destination_object = null ;
                    destination_object = null;
                    if (threshold_source_object != null)
                    {
                        threshold_source_object.ValueChanged -= threshold_source_value_changed_handler;
                        threshold_source_object = null;
                    }
                    connected = false;
				}
			}
		}

		private void Source_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			lock (this)
			{
				//double val1 = newVal.ToDouble () ;
				//double val2 = System.Double.Parse (level) ;
				//val1.CompareTo (val2) ; //

				int result =  newVal.CompareTo (level) ;
				if (state == states.over)
				{
					if (result < 0)
					{
						state = states.under ;

						if (returnText.Length > 0)
						{
							Message message = new Message (returnText, sourceText, priority, returnTextColor) ;
							Value = message ;
							if (destination_object != null) destination_object.Value = message ;
						}
					}
				}
				else //if (state == under)
				{
					if (result > 0)
					{
						state = states.over ;

						if (text.Length > 0)
						{
							Message message = new Message (text, sourceText, priority, textColor) ;
							Value = message ;
							if (destination_object != null) destination_object.Value = message ;
						}
					}
				}
			}
		}

        private void ThresholdSource_OnValueChanged(KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
        {
            lock (this)
            {
                level = newVal.ToDouble();
            }
        }

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the channel to be recorded.
		/// </summary>
		/// <value>The name of the source channel.</value>
		public string Source
		{
			get
			{
				return source ;
			}
			set
			{
				if (source != value)
				{
					source = value ;
					OnPropertyChanged("Source") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the name of the destination channel.
		/// </summary>
		/// <value>The name of the destination <see cref="Channels.Channel"/>.</value>
		/// <remarks>
		/// Each message created will be placed in this channel.
		/// <para>This property may be left blank in which case the messages will only be available via the <see cref="Channels.Channel.Value"/> property.</para>
		/// </remarks> 
		public string Destination
		{
			get
			{
				return destination ;
			}
			set
			{
				if (destination != value)
				{
					destination = value ;
					OnPropertyChanged("Destination") ;
				}
			}
		}

        public string ThresholdSource
        {
            get
            {
                return thresholdSource;
            }
            set
            {
                if (thresholdSource != value)
                {
                    thresholdSource = value;
                    OnPropertyChanged("ThresholdSource");
                }
            }
        }
        /// <summary>
		/// Gets or sets the text for the <see cref="Message.Source"/> property of recorded messages.
		/// </summary>
		/// <value>The text for the <see cref="Message.Source"/> property of recorded messages.</value>
		public string SourceText
		{
			get
			{
				return sourceText ;
			}
			set
			{
				if (sourceText != value)
				{
					sourceText = value ;
					OnPropertyChanged("SourceText") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the value for the <see cref="Message.Priority"/> property of recorded messages.
		/// </summary>
		/// <value>The value for the <see cref="Message.Priority"/> property of recorded messages.</value>
		public int Priority
		{
			get
			{
				return priority ;
			}
			set
			{
				if (priority != value)
				{
					priority = value ;
					OnPropertyChanged("Priority") ;
				}
			}
		}
		#endregion

		/// <summary>
		/// Gets or sets the threshold value
		/// </summary>
		/// <value>The threshold value.</value>
		public double Level
		{
			get
			{
				return level ;
			}
			set
			{
				if (level != value)
				{
					level = value ;
					OnPropertyChanged("Level") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the message text
		/// </summary>
		/// <value>The message text.</value>
		public string Text
		{
			get
			{
				return text ;
			}
			set
			{
				if (text != value)
				{
					text = value ;
					OnPropertyChanged("Text") ;
				}
			}
		}

        /// <summary>
        /// Gets or sets the message text color
        /// </summary>
        /// <value>The message text color.</value>
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                if (textColor != value)
                {
                    textColor = value;
                    OnPropertyChanged("TextColor");
                }
            }
        }

        /// <summary>
		/// Gets or sets the text for the return message
		/// </summary>
		/// <value>The return message text.</value>
		public string ReturnText
		{
			get
			{
				return returnText ;
			}
			set
			{
				if (returnText != value)
				{
					returnText = value ;
					OnPropertyChanged("ReturnText") ;
				}
			}
		}

        /// <summary>
        /// Gets or sets the message returntext color
        /// </summary>
        /// <value>The message returntext color.</value>
        public Color ReturnTextColor
        {
            get
            {
                return returnTextColor;
            }
            set
            {
                if (returnTextColor != value)
                {
                    returnTextColor = value;
                    OnPropertyChanged("ReturnTextColor");
                }
            }
        }
    }
}

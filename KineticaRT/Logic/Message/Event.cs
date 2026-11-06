using System;
using KineticaRT;
using System.Diagnostics ;
using System.Runtime.Serialization ;
using System.IO ;
using System.Text ;
using System.Drawing;


namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// KineticaRT Channel that records when another channel changes state.<br/>
	/// </summary>
	/// <remarks>
	/// A message is created for each change of state value.  The message may be passed to other Message components for display, logging etc.
	/// </remarks>
	[Serializable]
	public class Event:KineticaRT.Channels.Channel, ISerializable 
	{
		enum states
		{
			under,
			over
		} ;

		string		source = "" ;
		string		destination = "" ;
		string		sourceText = "" ;
		int			priority ;
		states		state = states.under ;
		string		text = "" ;
        Color       textColor;
		string		returnText = "" ;
        Color       returnTextColor;
        private bool connected = false;

		private KineticaRT.Channels.IChannel source_object ;
		private KineticaRT.Channels.IChannel destination_object ;
		private KineticaRT.Channels.ValueChangedEventHandler source_value_changed_handler ; 

		#region Construction
		/// <summary>
		/// Initialises a new instance of the class
		/// </summary>
		public Event()
		{
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);
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
			info.AddValue ("SourceText", sourceText) ;
			info.AddValue ("Text", text) ;
            info.AddValue ("TextColor", textColor.ToString());
            info.AddValue ("ReturnText", returnText);
            info.AddValue ("ReturnTextColor", returnTextColor.ToString());
			info.AddValue ("Priority", priority) ;
        }
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Event (SerializationInfo info, StreamingContext context):base(info, context)
		{
			source_value_changed_handler = new KineticaRT.Channels.ValueChangedEventHandler(Source_OnValueChanged);

			try
			{
				source = info.GetString ("Source") ;
				destination = info.GetString ("Destination") ;
				sourceText = info.GetString ("SourceText") ;
				text = info.GetString ("Text") ;
                textColor = Color.FromName(info.GetString("TextColor"));
                returnText = info.GetString("ReturnText");
                returnTextColor = Color.FromName(info.GetString("ReturnTextColor"));
                priority = info.GetInt32("Priority");
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
					connected = false ;
				}

				if (source_object == null) source_object = OnObjectSearch (source) as KineticaRT.Channels.Channel ;
				
				if (source_object != null)
				{
					destination_object = this.FindObject (destination) as KineticaRT.Channels.Channel ;
					if (destination_object == null) destination_object = OnObjectSearch (destination) as KineticaRT.Channels.Channel ;

					source_object.ValueChanged += source_value_changed_handler ;
					connected = true ;
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
					connected = false ;
				}
			}
		}

		private void Source_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			lock (this)
			{
				bool result = newVal.CompareTo(KineticaRT.Values.Values.True) == 0 ;

				if (state == states.over)
				{
					if (!result)
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
				else 
				{
					if (result)
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
        /// Gets or sets the message text color
        /// </summary>
        /// <value>The message text color.</value>
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

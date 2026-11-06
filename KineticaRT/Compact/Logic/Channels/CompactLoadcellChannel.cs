using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;

namespace KineticaRT.Compact.Logic.Channels
{
	/// <summary>
	/// Channel containing a numeric value.
	/// </summary>
	[ChangeRecord ("1.0", "07 04 2006", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2006 KineticaRT Ltd.")]
	
	[Serializable]
    public class LoadcellChannel : KineticaRT.Compact.Channels.InfoChannel, ISerializable
	{
		bool connected ;
		string [] source ;
        KineticaRT.Compact.Channels.IChannel[] sourceChannel;
		double [] value ;
        KineticaRT.Compact.Channels.ValueChangedEventHandler[] valueChangedHandler;


		/// Initialises a new instance of the class.
		public LoadcellChannel()
		{
			Initialise () ;
		}

		private void Initialise ()
		{
			source = new string [6] ;
			sourceChannel = new KineticaRT.Compact.Channels.IChannel [6] ;
			value = new double [6] ;
			valueChangedHandler = new KineticaRT.Compact.Channels.ValueChangedEventHandler [6] ;

			valueChangedHandler [0] = new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged0) ;
            valueChangedHandler[1] = new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged1);
            valueChangedHandler[2] = new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged2);
            valueChangedHandler[3] = new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged3);
            valueChangedHandler[4] = new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged4);
            valueChangedHandler[5] = new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged5);
		}
		
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
			info.AddValue ("SourceFx", source[0]) ;
			info.AddValue ("SourceFy", source[1]) ;
			info.AddValue ("SourceFz", source[2]) ;
			info.AddValue ("SourceMx", source[3]) ;
			info.AddValue ("SourceMy", source[4]) ;
			info.AddValue ("SourceMz", source[5]) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public LoadcellChannel (SerializationInfo info, StreamingContext context):base(info, context)
		{
			Initialise () ;

			try		
			{ 
				source[0] = info.GetString ("SourceFx") ;
				source[1] = info.GetString ("SourceFy") ;
				source[2] = info.GetString ("SourceFz") ;
				source[3] = info.GetString ("SourceMx") ;
				source[4] = info.GetString ("SourceMy") ;
				source[5] = info.GetString ("SourceMz") ;
			}
			catch	{ }
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Loadcell")] 
		public string SourceFx
		{
			get
			{
				return source [0] ;
			}
			set
			{
				if (source [0] != value)
				{
					source [0] = value ;
					OnPropertyChanged("SourceFx") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Loadcell")] 
		public string SourceFy
		{
			get
			{
				return source [1] ;
			}
			set
			{
				if (source [1] != value)
				{
					source [1] = value ;
					OnPropertyChanged("SourceFy") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Loadcell")] 
		public string SourceFz
		{
			get
			{
				return source [2] ;
			}
			set
			{
				if (source [2] != value)
				{
					source [2] = value ;
					OnPropertyChanged("SourceFz") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Loadcell")] 
		public string SourceMx
		{
			get
			{
				return source [3] ;
			}
			set
			{
				if (source [3] != value)
				{
					source [3] = value ;
					OnPropertyChanged("SourceMx") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Loadcell")] 
		public string SourceMy
		{
			get
			{
				return source [4] ;
			}
			set
			{
				if (source [4] != value)
				{
					source [4] = value ;
					OnPropertyChanged("SourceMy") ;
				}
			}
		}


		/// <summary>
		/// Gets or sets the name of the source channel.
		/// </summary>
		/// <value>The name of the source channel.</value>
		/// <remarks>Leave the name blank (Empty string) if you don't want to use a source channel.
		/// <para>The channel will not connect to the source until the Connect method is called.</para>
		/// </remarks>
		[System.ComponentModel.Description("Name of source channel"),System.ComponentModel.Category("Loadcell")] 
		public string SourceMz
		{
			get
			{
				return source [5] ;
			}
			set
			{
				if (source [5] != value)
				{
					source [5] = value ;
					OnPropertyChanged("SourceMz") ;
				}
			}
		}


		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		[System.ComponentModel.Description("The value of this channel. "), System.ComponentModel.Category("Channel")] 
		public override Values.Value Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				base.Value = value ;
			}
		}
		#endregion

		#region Logic
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect () ;

			lock (this)
			{
				for (int index = 0 ; index < 6 ; index ++)
				{
					if (connected && sourceChannel[index] != null)sourceChannel[index].ValueChanged -= valueChangedHandler [index];
				
					if (source[index] != null && source[index].Length > 0)
					{
						if (connected && sourceChannel[index] != null) sourceChannel[index].ValueChanged -= valueChangedHandler [index];
				
						if (source[index] != null && source[index].Length > 0)
						{
							sourceChannel[index] = OnObjectSearch (source[index]) as KineticaRT.Compact.Channels.IChannel  ;
			
							if (sourceChannel[index] != null)
							{
								value[index] = sourceChannel[index].Value.ToDouble() ;
								sourceChannel[index].ValueChanged += valueChangedHandler [index];
							}
							else
							{
								try 
								{
									value[index] = System.Double.Parse (source[index],System.Globalization.CultureInfo.CurrentCulture) ;
								}
								catch (System.FormatException)
								{
									value[index] = System.Double.NaN ;
								}
							}
						}
					}
				}

				updateEventHandler = new KineticaRT.Updater.UpdateEventHandler(updater_Update) ;
				KineticaRT.Services.FastUpdater.Update += updateEventHandler ;

				connected = true ;
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
					for (int index = 0 ; index < 6 ; index ++)
					{
						if (sourceChannel[index] != null)
						{
							sourceChannel[index].ValueChanged -= valueChangedHandler [index];
							value[index] = 0.0 ;
						}
						sourceChannel[index] = null ;
					}

					KineticaRT.Services.FastUpdater.Update -= updateEventHandler ;
					updateEventHandler = null ;
					connected = false ;
				}
			}
		}

		private void SetValue () 
		{
			Value = new Values.DoubleArray (value) ;
		}

		private void updater_Update()
		{
			lock (this)
			{
				Value = new Values.DoubleArray (value) ;
			}
		}

		KineticaRT.Updater.UpdateEventHandler updateEventHandler ;


        private void This_OnValueChanged0(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			value[0] = newVal.ToDouble () ;
		}
        private void This_OnValueChanged1(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			value[1] = newVal.ToDouble () ;
		}
        private void This_OnValueChanged2(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			value[2] = newVal.ToDouble () ;
		}
        private void This_OnValueChanged3(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			value[3] = newVal.ToDouble () ;
		}
        private void This_OnValueChanged4(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			value[4] = newVal.ToDouble () ;
		}
        private void This_OnValueChanged5(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			value[5] = newVal.ToDouble () ;
		}
		#endregion
	}
}

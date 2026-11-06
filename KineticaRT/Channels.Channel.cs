using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Security.Permissions;
using KineticaRT;
using System.Windows.Forms;

namespace KineticaRT.Channels
{
	/// <summary>
	/// An Object with a value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
    public class Channel : Object, ISerializable, IChannel, IPlayerChannel, ILoggerChannel, ILoggerChannelSource
	{
		new internal static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Channels", "Channels");

        private System.Collections.Generic.Queue<KineticaRT.Values.Value> valuesQueue = new System.Collections.Generic.Queue<KineticaRT.Values.Value>();//ft
        private bool alreadyCalled;
		/// <summary>
		/// Current value.
		/// </summary>
		protected KineticaRT.Values.Value RawValue
		{
			get 
			{
				return objVal ;
			}
			set 
			{
				objVal = value ;
			}
		}
		private KineticaRT.Values.Value objVal = KineticaRT.Values.Values.NotConnected ;//= new KineticaRT.Values.Double (0.0);
		/// <summary>
		/// PropertyChangedEventArgs passed when the Value property changes.
		/// </summary>
		public static PropertyChangedEventArgs ValuePropertyChangedEventArgs { get { return valuePropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs valuePropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("Value");
		/// <summary>
		/// PropertyChangedEventArgs passed when the Description property changes.
		/// </summary>
		public static PropertyChangedEventArgs DescriptionPropertyChangedEventArgs { get { return descriptionPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs descriptionPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("Description");
		/// <summary>
		/// PropertyChangedEventArgs passed when the ToolTip property changes.
		/// </summary>
		public static PropertyChangedEventArgs ToolTipPropertyChangedEventArgs { get { return toolTipPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs toolTipPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("ToolTip");
		/// <summary>
		/// PropertyChangedEventArgs passed when the Units property changes.
		/// </summary>
		public static PropertyChangedEventArgs UnitsPropertyChangedEventArgs { get { return unitsPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs unitsPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("Units");
		/// <summary>
		/// PropertyChangedEventArgs passed when the LowLow property changes.
		/// </summary>
		public static PropertyChangedEventArgs LowLowPropertyChangedEventArgs { get { return lowLowPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs lowLowPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("LowLow");
		/// <summary>
		/// PropertyChangedEventArgs passed when the Low property changes.
		/// </summary>
		public static PropertyChangedEventArgs LowPropertyChangedEventArgs { get { return lowPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs lowPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("Low");
		/// <summary>
		/// PropertyChangedEventArgs passed when the Normal property changes.
		/// </summary>
		public static PropertyChangedEventArgs NormalPropertyChangedEventArgs { get { return normalPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs normalPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("Normal");
		/// <summary>
		/// PropertyChangedEventArgs passed when the High property changes.
		/// </summary>
		public static PropertyChangedEventArgs HighPropertyChangedEventArgs { get { return highPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs highPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("High");
		/// <summary>
		/// PropertyChangedEventArgs passed when the HighHigh property changes.
		/// </summary>
		public static PropertyChangedEventArgs HighHighPropertyChangedEventArgs { get { return highHighPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs highHighPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("HighHigh");
		/// <summary>
		/// PropertyChangedEventArgs passed when the NormalColor property changes.
		/// </summary>
		public static PropertyChangedEventArgs NormalColorPropertyChangedEventArgs { get { return normalColorPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs normalColorPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("NormalColor");
		/// <summary>
		/// PropertyChangedEventArgs passed when the LowLowColor property changes.
		/// </summary>
		public static PropertyChangedEventArgs LowLowColorPropertyChangedEventArgs { get { return lowLowColorPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs lowLowColorPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("LowLowColor");
		/// <summary>
		/// PropertyChangedEventArgs passed when the LowColor property changes.
		/// </summary>
		public static PropertyChangedEventArgs LowColorPropertyChangedEventArgs { get { return lowColorPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs lowColorPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("LowColor");
		/// <summary>
		/// PropertyChangedEventArgs passed when the HighColor property changes.
		/// </summary>
		public static PropertyChangedEventArgs HighColorPropertyChangedEventArgs { get { return highColorPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs highColorPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("HighColor");
		/// <summary>
		/// PropertyChangedEventArgs passed when the HighHighColor property changes.
		/// </summary>
		public static PropertyChangedEventArgs HighHighColorPropertyChangedEventArgs { get { return highHighColorPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs highHighColorPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("HighHighColor");
		/// <summary>
		/// PropertyChangedEventArgs passed when the Color property changes.
		/// </summary>
		public static PropertyChangedEventArgs ColorPropertyChangedEventArgs { get { return colorPropertyChangedEventArgs ;}}
		static private PropertyChangedEventArgs colorPropertyChangedEventArgs = new System.ComponentModel.PropertyChangedEventArgs("ThresholdColor");
		
		#region Conversions
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString ()
		{
			if (RawValue == null) return "null" ;
			return RawValue.ToString () ;
		}

		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public string ToString (string format, IFormatProvider info)
		{
			return RawValue.ToString (format, info) ;
		}
		#endregion

		#region Constructon
		/// <summary>Initializes a new instance of the class.</summary>
		public Channel()
		{
			//NS28.03.07 This should not be in a channel! This is not a double! RawValue= new KineticaRT.Values.Double () ;
		}
		#endregion

		#region ISerializable
		/// <summary>
		/// Gets the serialization data needed to reinstantiate this object.
		/// </summary>
		/// <param name="info">The SerializationInfo object to be populated with serialization data.</param>
		/// <param name="context">The destination context of the serialization.</param>
		[SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
		public override void GetObjectData (SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData (info, context) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected Channel (SerializationInfo info, StreamingContext context):base(info, context)
		{
		}
		#endregion

		#region Properties
		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		[System.ComponentModel.Description("Value of channel."),System.ComponentModel.Category("Channel")] 
		public virtual KineticaRT.Values.Value Value
		{
			get
			{
                if (playCount > 0)
                {
                    return playValue;
                }
                else
                {
                    return RawValue;
                }
			}
			set
			{
                if (((object)RawValue == null) && ((object)value == null)) return;

                if ((((object)RawValue == null) && ((object)value != null)) || (((object)RawValue != null) && ((object)value == null)))
                {
                    RawValue = value;
                    if (playCount <= 0)
                    {
                        OnValueChanged(value);
                        OnPropertyChanged(ValuePropertyChangedEventArgs);
                    }
                }
                else if ((RawValue.GetType() != value.GetType()))
                {
                    if ((object)RawValue != (object)value)
                    {
                        RawValue = value;
                        if (playCount <= 0)
                        {
                            OnValueChanged(value);
                            OnPropertyChanged(ValuePropertyChangedEventArgs);
                        }
                    }
                }
                else if (!RawValue.Equals(value))
                {
                    RawValue = value;
                    if (playCount <= 0)
                    {
                        OnValueChanged(value);
                        OnPropertyChanged(ValuePropertyChangedEventArgs);
                    }
                }
			}
		}


        /// <summary>Gets or sets the value of this channel.</summary>
        /// <value>A Value object representing the value of the channel.</value>
        [System.ComponentModel.Bindable(true)]//ft Test pour utiliser les channel et utiliser le DataBinding en WindowsForms standard
        public virtual object ValueObject
        {
            get
            {
                if (this is KineticaRT.Channels.IBooleanChannel)
                    return this.Value.ToDouble() >= 1;
                else if (this is KineticaRT.Channels.INumericChannel)
                    return this.Value.ToDouble();
                else if (this is KineticaRT.Channels.ITextChannel)
                    return this.Value.ToString();
                else
                    return this.Value.ToDouble();
            }

            set
            {
                if (value is KineticaRT.Values.Value)
                {
                    this.Value = (KineticaRT.Values.Value)value;
                    OnPropertyChanged("ValueObject");
                }
                else
                {
                    KineticaRT.Values.Value new_value;
                    if (value is double || value is float)
                    {
                        this.Value = new KineticaRT.Values.Double(value);
                        OnPropertyChanged("ValueObject");

                    }
                    else if (value == null)
                    {
                        //ft Il arrive que l'opcclient recoit des null, et envoie aux channels des empty mais le problème est que certain channels dont les TextChannel on des Get/Set
                        //et le empty va ecraser une bonne valeur via le Set.
                        //new_value = KineticaRT.Values.Values.Empty;  
                        //System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "Empty: ");

                    }
                    else if (value is System.DBNull)
                    {
                        //ft Il arrive que l'opcclient recoit des null, et envoie aux channels des empty mais le problème est que certain channels dont les TextChannel on des Get/Set
                        //et le empty va ecraser une bonne valeur via le Set.
                        //new_value = KineticaRT.Values.Values.Empty;
                        // System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceVerbose, "DB-Empty: ");

                    }
                    else if (value is Int64 || value is Int32 || value is Int16 || value is System.SByte)
                    {
                        this.Value = new KineticaRT.Values.Integer(value);
                        OnPropertyChanged("ValueObject");

                    }
                    else if (value is UInt64 || value is UInt32 || value is UInt16 || value is System.Byte)
                    {
                        this.Value = new KineticaRT.Values.UnsignedInteger(value);
                        OnPropertyChanged("ValueObject");
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
                        this.Value = new_value;
                        OnPropertyChanged("ValueObject");//ft utili/ pour notifier les Control Binde que la value a change (Active le data value changed en bidirectionnel)
                    }
                }
            }
        }
		#endregion

        #region IPlayer
        private Int32 playCount;
        private KineticaRT.Values.Value playValue;

        /// <summary>
        /// Updates the channels current play value.
        /// </summary>
        /// <param name="newValue">The new play value of the channel.</param>
        void IPlayerChannel.PlaySetValue(KineticaRT.Values.Value newValue)
        {
            playValue = newValue;

            if (playCount > 0)
            {
                OnValueChanged(playValue);
                OnPropertyChanged(ValuePropertyChangedEventArgs);
            }
        }

        /// <summary>
        /// Places the channel into play mode.
        /// </summary>
        /// <remarks>
        /// There channel should return from play mode when there have been an equal number of calls to PlayStop as to PlayStart.
        /// </remarks>
        void IPlayerChannel.PlayStart()
        {
            playCount++;
        }

        /// <summary>
        /// Returns the channel from play mode.
        /// </summary>
        /// <remarks>
        /// There channel should return from play mode when there have been an equal number of calls to PlayStop as to PlayStart.
        /// If more calls are made to PlayStop than PlayStart then the extra calls should be ignored.
        /// </remarks>
        void IPlayerChannel.PlayStop()
        {
            playCount--;
        }
        #endregion



		#region Value changed event stuff
		/// <summary>Occurs when the value of the Value property changes.</summary>
		public event ValueChangedEventHandler ValueChanged;   
		/// <summary>Called when the value of the Value property changes.</summary>
        // Protected to allow channels that may have their display format changed to update their displays without needing a value change.
		protected void OnValueChanged(KineticaRT.Values.Value newVal)
        {
            KineticaRT.Values.Value nextValue = null;
            if (ValueChanged != null)
            {
                if (alreadyCalled)
                {//la queue est utilisée pour regler un bug d entrée recursive (2fois) lorsque un bouton normal est clique et que la sequence a un handler et dans ce handler un reset la valeur du channel
                    lock (valuesQueue)
                    {
                        valuesQueue.Enqueue(newVal);
                        //En theorie on ne devrait jamais rentrer ici.. mais une exception qui se passe dans le ValueChanged() skip le traitement, et ne remet
                        //pas le flag a false et on reste en loop infinie et la queue augmente toujours

                        // Enlever le commentaire sur la ligne ci dessous afin dafficher le nombre delements dans la queue dans la console.
                        //System.Diagnostics.Debug.WriteLine(valuesQueue.Count.ToString());
                        if (valuesQueue.Count > 200)
                        {
                            alreadyCalled = false;
                            System.Diagnostics.Trace.WriteLine("Channel.Channel.cs Error: " + this.Name + " valuesQueue is too high ");
                            //MessageBox.Show("Channel.Channel Error: " + this.Name + " valuesQueue is too high ","Queue Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            valuesQueue.Clear();
                            ValueChanged(this, newVal);                            
                        }
                    }

                }               
                else 
                {
                    alreadyCalled = true;
                    try
                    {
                        ValueChanged(this, newVal);
                    }
                    catch(Exception exception)
                    {
                        System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceError, exception.Message + ": " + exception.StackTrace, System.Threading.Thread.CurrentThread.Name);
                    }
                    do
                    {
                        lock (valuesQueue)
                        {
                            if (alreadyCalled && valuesQueue.Count > 0)
                            {
                                nextValue = valuesQueue.Dequeue();
                            }
                            else
                            {
                                nextValue = null;
                            }
                        }
                        if (ValueChanged != null && nextValue != null)
                        {
                            // Empeche le queue d'augmenter si la code suivant leve une exception
                            try
                            {
                                ValueChanged(this, nextValue);
                            }
                            catch (Exception)
                            {
                                alreadyCalled = false;
                            }
                        }
                        else if (nextValue == null)
                        {
                            alreadyCalled = false;
                        }
                    } while (null != nextValue);
                }
            }
            
            if (LogValueChanged != null && logLiveValues)
            {
                LogValueChanged(this, new KineticaRT.Values.LogValue(newVal, DateTime.Now));
            }
           
        }
		#endregion

        //#region ILogger

        ///// <summary>
        ///// Starts collection of historical logged data.
        ///// </summary>
        //public void GetHistoricalData()
        //{
        //}

        ///// <summary>
        ///// Raised when collection of historical logged data has been completed.
        ///// </summary>
        ///// <param name="e"></param>
        //protected virtual void OnHistoricalComplete(KineticaRT.Channels.HistoricalCompleteEventArgs e)
        //{
        //    if (HistoricalComplete != null)
        //    {
        //        HistoricalComplete(this, e);
        //    }
        //}



        ///// <summary>Raised when collection of historical logged data has been completed.</summary>
        //public event KineticaRT.Channels.HistoricalCompleteEventHandler HistoricalComplete;
        ///// <summary>Occurs when the value of the LogValue property changes.</summary>
        //public event KineticaRT.Channels.LogValueChangedEventHandler LogHistoricalValueChanged;
        ///// <summary>Called when the value of the Value property changes.</summary>
        //private void OnLogHistoricalValueChanged(KineticaRT.Values.LogValue newVal) { if (LogHistoricalValueChanged != null) LogHistoricalValueChanged(this, newVal); }
        ///// <summary>Occurs when the value of the LogValue property changes.</summary>
        //public event KineticaRT.Channels.LogValueChangedEventHandler LogValueChanged;
        ///// <summary>Called when the value of the Value property changes.</summary>
        //private void OnLogValueChanged(KineticaRT.Values.LogValue newVal) { /*if (LogValueChanged != null)*/ LogValueChanged(this, newVal); }


        //#endregion
        #region ILoggerChannel
        /// <summary>
        /// Informs the channel that historical data (data collected while the application was not running) should be retrieved and returned to the logger.
        /// </summary>
        void ILoggerChannel.GetHistoricalData()
        {
            // No historical data handling - indicate complete.
            if (HistoricalComplete != null)
            {
                HistoricalComplete(this, new KineticaRT.Channels.HistoricalCompleteEventArgs());
            }
        }

        /// <summary>Occurs when a historical value change is retrieved.</summary>
        public event LogValueChangedEventHandler LogHistoricalValueChanged;

        /// <summary>Occurs when the live value of the Value property changes.</summary>
        /// <remarks>This differs to a live ValueChangedEvent in that it should only include real value changes and not include any values which are created by interpolattion or filtering for reasons such as smoothing display updates.</remarks>
        public event LogValueChangedEventHandler LogValueChanged;

        /// <summary>
        /// Raises logged value changed event
        /// </summary>
        /// <param name="logValue">Value for logging </param>
        /// <param name="timeStamp">Timestamp associated with monitored message containing log value</param>
        protected void OnLogValueChanged(int logValue, System.DateTime timeStamp)
        {
            //System.Diagnostics.Trace.WriteLineIf(Omnibox.traceSwitch.TraceInfo, timeStamp.ToString("HH:mm:ss'.'fff") + " " + TraceStrings.NewLoggedData + logValue.ToString(), this.Name);
            if (LogValueChanged != null)
            {
                LogValueChanged(this, new KineticaRT.Values.LogValue(new Values.Integer(logValue), timeStamp));
            }
        }

        /// <summary>
        /// Raises logged value changed event
        /// </summary>
        /// <param name="logValue">Value for logging </param>
        /// <param name="timeStamp">Timestamp associated with monitored message containing log value</param>
        protected void OnLogValueChanged(KineticaRT.Values.Value logValue, System.DateTime timeStamp)
        {
            //System.Diagnostics.Trace.WriteLineIf(Omnibox.traceSwitch.TraceInfo, timeStamp.ToString("HH:mm:ss'.'fff") + " " + TraceStrings.NewLoggedData + logValue.ToString(), this.Name);
            if (LogValueChanged != null)
            {
                LogValueChanged(this, new KineticaRT.Values.LogValue(new Values.Integer(logValue), timeStamp));
            }
        }

        /// <summary>
        /// Raises event when the channels logged vale changes
        /// </summary>
        /// <param name="logValue">Value to log</param>
        /// <param name="timeStamp">time stamp taken from monitired message</param>
        protected void OnLogValueChanged(string logValue, System.DateTime timeStamp)
        {
            if (LogValueChanged != null)
            {
                LogValueChanged(this, new KineticaRT.Values.LogValue(new Values.Text(logValue), System.DateTime.Now));
            }
        }

        /// <summary>
        /// Raises event when the channels logged vale changes
        /// </summary>
        /// <param name="logValue">Value to log</param>
        /// <param name="timeStamp">time stamp taken from monitired message</param>
        protected void OnLogValueChanged(double logValue, System.DateTime timeStamp)
        {
            if (LogValueChanged != null)
            {
                LogValueChanged(this, new KineticaRT.Values.LogValue(new Values.Double(logValue), System.DateTime.Now));
            }
        }


        /// <summary>
        /// Raises event when the channels logged vale changes
        /// </summary>
        /// <param name="logValue">Value to log</param>
        /// <param name="timeStamp">time stamp taken from monitired message</param>
        protected void OnLogValueChanged(bool logValue, System.DateTime timeStamp)
        {
            if (LogValueChanged != null)
            {
                LogValueChanged(this, new KineticaRT.Values.LogValue(new Values.Boolean(logValue), System.DateTime.Now));
            }
        }

        private bool logLiveValues = true;

        bool ILoggerChannelSource.LogLiveValues
        {
            get
            {
                return logLiveValues;
            }
            set
            {
                if (logLiveValues != value)
                {
                    logLiveValues = value;
                    OnPropertyChanged("LogLiveValues");
                }
            }
        }
        /// <summary>
        /// Gets or sets if live data is logged.
        /// </summary>
        [System.ComponentModel.Description("If set true then if live data is logged through the LogValueChanged event.")]
        [DefaultValue(true)]
        protected bool LogLiveValues
        {
            get
            {
                return logLiveValues;
            }
            set
            {
                if (logLiveValues != value)
                {
                    logLiveValues = value;
                    OnPropertyChanged("LogLiveValues");
                }
            }
        }

        /// <summary>
        /// Occurs when all historical data has been retrieved.
        /// </summary>
        public event KineticaRT.Channels.HistoricalCompleteEventHandler HistoricalComplete;

        /// <summary>Called when the value of the Value property changes.</summary>
        protected void OnLogHistoricalValueChanged(KineticaRT.Values.LogValue newVal) { if (LogHistoricalValueChanged != null) LogHistoricalValueChanged(this, newVal); }

        /// <summary>
        /// Raised when all historical data has been collected during startup.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnHistoricalComplete(KineticaRT.Channels.HistoricalCompleteEventArgs e)
        {
            if (HistoricalComplete != null)
            {
                HistoricalComplete(this, e);
            }
        }
        #endregion

        #region ILoggerChannelSource
        void ILoggerChannelSource.LogValueChanged(KineticaRT.Values.LogValue newVal)
        {
            if (LogValueChanged != null)
            {
                LogValueChanged(this, newVal);
            }
        }
        #endregion

    }
}

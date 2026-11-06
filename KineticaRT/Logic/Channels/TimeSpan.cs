using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Diagnostics;

namespace KineticaRT.Logic.Channels
{
	/// <summary>
	/// Channel which holds a time period value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	public class TimeSpan:KineticaRT.Channels.InfoChannel, System.Runtime.Serialization.ISerializable
	{
        static TraceSwitch traceSwitch = new TraceSwitch("KineticaRT.Servers.Log", "KineticaRT.Servers.Log Messages");
        //string format = "G";
		//System.Globalization.DateTimeFormatInfo info ;

		/// Initialises a new instance of the class.
		public TimeSpan()
		{
			Initialise() ;
            this.RawValue = new KineticaRT.Values.TimeSpan(new System.TimeSpan());
        }

		private void Initialise ()
		{
			//info = (System.Globalization.DateTimeFormatInfo) System.Globalization.DateTimeFormatInfo.CurrentInfo.Clone () ;
		}

		
		#region Properties
        /// <summary>
        /// Gets or sets the value to which this channel will be set on initialisation.
        /// Changing this property will also change the current value.
        /// </summary>
        /// <remarks>When the channel is saved, the value of this property will be stored and used to initialise the Value property when the channel is restored.</remarks>
        /// <value>The value to which this channel will be set at startup. Changing </value>
        [System.ComponentModel.Description("The value to which this channel will be set on initialisation."), System.ComponentModel.Category("Defaults")]
        public System.TimeSpan DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                if (defaultValue != value)
                {
                    defaultValue = value;
                    Value = new KineticaRT.Values.TimeSpan(value);
                    OnPropertyChanged("DefaultValue");
                }
            }
        }
        System.TimeSpan defaultValue;

        ///// <summary>
        ///// Gets or sets the format of the channel when viewed as text.
        ///// </summary>
        ///// <value>A string describing the display format of the channel.</value>
        ///// <remarks>The following formats can be used:
        ///// <ul>
        ///// <li>"d" - Short date</li>
        ///// <li>"D" - Long date</li>
        ///// <li>"f" - Long date, short time</li>
        ///// <li>"F" - Long date, long time</li>
        ///// <li>"g" - short date, short time</li>
        ///// <li>"G" - short date, long time</li>
        ///// <li>"t" - short time</li>
        ///// <li>"T" - Long time</li>
        ///// <li>"u" - Universal time (sortable)</li>
        ///// <li>"U" - Universal time</li>
        ///// <li>"dddd yyyy/MM/dd hh:mm:ss fff" - Custom</li>
        ///// </ul>
        ///// </remarks>
        //[System.ComponentModel.Description("Format of channel.  This is used to convert the value to text. Eg. d,D,f,F,g,G,t,T,u,U"), System.ComponentModel.Category("Numeric")] 
        //public string Format
        //{
        //    get
        //    {
        //        return format ;
        //    }
        //    set
        //    {
        //        if (format != value)
        //        {
        //            format = value ;
        //            OnPropertyChanged("Format") ;
        //        }
        //    }
        //}
		#endregion

		#region ISerializable
        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data.</param>
        /// <param name="context">The destination for this serialization.</param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null) throw new ArgumentNullException("info");

            //info.AddValue("Format", format);
            info.AddValue("DefaultValue", defaultValue);

            base.GetObjectData(info, context);
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		protected TimeSpan (System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info, context)
		{
            if (info == null) throw new ArgumentNullException("info");
            
            Initialise();
        
            try
            {
                //format = info.GetString("Format");

                defaultValue = (System.TimeSpan)info.GetValue("DefaultValue", typeof(System.TimeSpan));
                this.RawValue = new KineticaRT.Values.TimeSpan(defaultValue);
            }
            catch (System.Runtime.Serialization.SerializationException)
            {
                System.Diagnostics.Trace.WriteLineIf(traceSwitch.TraceInfo, "Configuration is missing a property for " + GetType(), this.Name);
            }
        }
		#endregion

		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString ()
		{
			try
			{
                if (RawValue != null)
				{
                    return ((IFormattable)RawValue).ToString();
				}
				else
				{
					return "null" ;
				}
			}
			catch (System.FormatException e)
			{
				return e.Message ; 
			}
		}
	}
}

using System.Runtime.Serialization ;
using System;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;

namespace KineticaRT.Compact.Channels
{
	/// <summary>
	/// An Object with a value.
	/// </summary>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
	[DefaultProperty("Value")]
	public class Channel : Object, ISerializable, IChannel
	{
        /// <summary>
        /// The value of this channel.
        /// </summary>
		protected KineticaRT.Compact.Values.Value objValue ; // = new KineticaRT.Values.Double (0.0);

		#region Conversions
		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public override string ToString ()
		{
			return objValue.ToString () ;
		}

		/// <summary>
		/// Converts this Value to a System.String.
		/// </summary>
		/// <returns>The result.</returns>
		public string ToString (string format, IFormatProvider info)
		{
			return objValue.ToString (format, info) ;
		}
		#endregion

		#region Constructon
		/// <summary>Initializes a new instance of the class.</summary>
		public Channel()
		{
			objValue = new KineticaRT.Compact.Values.Double () ;
		}
		#endregion

		#region ISerializable
        /// <summary>
        /// Gets the serialization data needed to reinstantiate this object.
        /// </summary>
        /// <param name="info">The SerializationInfo object to be populated with serialization data.</param>
        /// <param name="context">The destination context of the serialization.</param>
        //[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        /// <summary>Constructor for the ISerializable interface.</summary>
        protected Channel(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        #endregion

		#region Properties
		private bool setting ;

		/// <summary>Gets or sets the value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		[System.ComponentModel.Description("Value of channel."),System.ComponentModel.Category("Channel")] 
		public virtual KineticaRT.Compact.Values.Value Value
		{
			get
			{
				return objValue ;
			}
			set
			{
				if (setting == true)
				{
					objValue = value ;
				}
				else
				{
					setting = true ;

					try 
					{
						if ((objValue == null) && (value != null))
						{
							objValue = value ;
							OnValueChanged (value) ;
							OnPropertyChanged ("Value") ;
						}
						else if ((objValue != null) && (value == null))
						{
							objValue = value ;
							OnValueChanged (value) ;
							OnPropertyChanged ("Value") ;
						}
						else if ((objValue == null) && (value == null))
						{
							objValue = null ;
							OnValueChanged (value) ;
							OnPropertyChanged ("Value") ;
						}
						else if (!objValue.Equals (value))
						{
							objValue = value ;
							OnValueChanged (value) ;
							OnPropertyChanged ("Value") ;
						}
						if ((objValue != null) && !objValue.Equals(value))
						{
							OnValueChanged (objValue) ;
							OnPropertyChanged ("Value") ;
						}
					}
					catch (System.Exception e)
					{
						string caught = e.Message  + " Here" ;
					}

					setting = false ;
				}
			}
		}
		#endregion

		#region Value changed event stuff
		/// <summary>Occurs when the value of the Value property changes.</summary>
		public event ValueChangedEventHandler ValueChanged;   
		/// <summary>Called when the value of the Value property changes.</summary>
		private void OnValueChanged(KineticaRT.Compact.Values.Value newVal){if (ValueChanged != null) ValueChanged(this, newVal);}
		#endregion
	}
}

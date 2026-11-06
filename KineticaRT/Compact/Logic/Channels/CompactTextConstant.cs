using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;

namespace KineticaRT.Compact.Logic.Channels
{
	/// <summary>
	/// Constant channel containing a text (string) value.
	/// </summary>
	/// <remarks>Constants have no method for changing their 'Value' property. They are normally used to prevent users accidentally changing values by connecting a control such as a slider to them.  The value property may be configured using the 'Constant' property,</remarks>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    public class TextConstant : KineticaRT.Compact.Channels.TextChannel, ISerializable
	{
		/// Initialises a new instance of the class.
		public TextConstant()
		{
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
			info.AddValue ("Value", Value) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public TextConstant (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try
			{
				Value = new Values.Text((string) info.GetValue ("Value",			typeof(string))) ;
			}
			catch (Exception)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}
		}
		#endregion

		#region Properties
		/// <summary>Gets or sets the constant value of this channel.</summary>
		/// <value>A Value object representing the value of the channel.</value>
		[System.ComponentModel.Description("Value of channel."),System.ComponentModel.Category("Channel")] 
		public virtual Values.Value Constant
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

		/// <summary>Gets the value of this channel.</summary>
		/// <value>A Value object representing the result of evaluating the expression.</value>
		/// <remarks>The Value is not necessarily a Text value. It may be, for example, an error value or a Text value.</remarks>
		[System.ComponentModel.Description("The value of this channel. "), System.ComponentModel.Category("Channel")] 
		public override Values.Value Value
		{
			get
			{
				return base.Value ;
			}
		}
		#endregion
	}
}

using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;

using KineticaRT.Compact;

namespace KineticaRT.Compact.Logic.Channels
{
	/// <summary>
	/// Channel containing an expression which evaluates to a numeric value.<br/>
	/// </summary>
	/// <remarks>
	/// This class makes use of the <see cref="KineticaRT.Logic.Expressions.Expression"/> class which must be purchased separately.<br/>
	/// <para>If the expression evaluates to a type other than numeric then this class makes no attempt to convert it.  The Value property may for example contain error values or text values.</para>
	/// </remarks>
	/// <seealso cref="KineticaRT.Logic.Expressions.Expression"/>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    public class NumericExpression : KineticaRT.Compact.Channels.NumericChannel, ISerializable
	{
		Logic.Expressions.Expression  expression = new Expressions.Expression() ;

		/// Initialises a new instance of the class.
		public NumericExpression()
		{
			expression.ObjectSearch += new ObjectSearchEventHandler (this.OnObjectSearch);
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
			info.AddValue ("Text", expression.Text) ;
		}
		/// <summary>Constructor for the ISerializable interface.</summary>
		public NumericExpression (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try
			{
				expression.Text = (string) info.GetValue ("Text",			typeof(string)) ;
			}
			catch (Exception)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}

			expression.ObjectSearch += new ObjectSearchEventHandler (this.OnObjectSearch);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Expression text, see KineticaRT.Expression.<see cref="KineticaRT.Logic.Expressions.Expression.Text"/>.
		/// </summary>
		/// <value>The text of the expression to be evaluated.</value>
		[System.ComponentModel.Description("Expression text"),System.ComponentModel.Category("NumericExpression")] 
		//[Editor(typeof(KineticaRT.Logic.Expressions.Design.ExpressionEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return expression.Text ;
			}
			set
			{
				if (expression.Text != value)
				{
					expression.Text = value ;
					OnPropertyChanged("Text") ;
				}
			}
		}

		/// <summary>Gets the value of this channel.</summary>
		/// <value>A Value object representing the result of evaluating the expression.</value>
		/// <remarks>The Value is not necessarily a numeric value. It may be, for example, an error value or a Text value.</remarks>
		[System.ComponentModel.Description("The value of this channel. "), System.ComponentModel.Category("Channel")]
        public override KineticaRT.Compact.Values.Value Value
		{
			get
			{
				return base.Value ;
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
			expression.Connect () ;
			Value = expression.Value ;
			expression.ValueChanged += new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged);
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			expression.ValueChanged -= new KineticaRT.Compact.Channels.ValueChangedEventHandler(This_OnValueChanged);
			expression.Disconnect () ;
			base.Disconnect () ;
		}

        private void This_OnValueChanged(KineticaRT.Compact.Channels.IChannel sender, Values.Value newVal)
		{
			Value = newVal ;
		}
		#endregion
	}
}

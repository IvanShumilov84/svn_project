using System;
using System.Runtime.Serialization ;
using System.ComponentModel ;
using KineticaRT ;
using KineticaRT.Channels;

namespace KineticaRT.Logic.Channels
{
	/// <summary>
	/// Channel containing an expression which evaluates to a text (string) value.<br/>
	/// </summary>
	/// <remarks>
	/// This class makes use of the <see cref="KineticaRT.Logic.Expressions.Expression"/> class which must be purchased separately.
	/// <para>If the expression evaluates to a type other than text then this class makes no attempt to convert it.  The Value property may for example contain error values or boolean values.</para>
	/// </remarks>
	/// <seealso cref="KineticaRT.Logic.Expressions.Expression"/>
	[ChangeRecord ("1.0", "01 01 2003", "N. Savidge", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
	[Serializable]
    public class TextExpression : TextChannel, ITextExpressionChannel,ISerializable
	{
		KineticaRT.Logic.Expressions.Expression  expression = new KineticaRT.Logic.Expressions.Expression() ;

		/// Initialises a new instance of the class.
		public TextExpression()
		{
            Initialise();
        }
        
        private void Initialise ()
        {
            expression.ObjectSearch += new ObjectSearchEventHandler(this.OnObjectSearch);
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
		protected TextExpression (SerializationInfo info, StreamingContext context):base(info, context)
		{
			try
			{
				expression.Text = (string) info.GetValue ("Text",			typeof(string)) ;
			}
			catch (System.Runtime.Serialization.SerializationException)
			{
				//System.Windows.Forms.MessageBox.Show ("Configuration is missing a property for " + GetType()) ;
			}

            Initialise();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Expression text, see KineticaRT.Expression.<see cref="KineticaRT.Logic.Expressions.Expression.Text"/>.
		/// </summary>
		/// <value>The text of the expression to be evaluated.</value>
		[System.ComponentModel.Description("Expression text"),System.ComponentModel.Category("Expression")] 
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
		/// <remarks>The Value is not necessarily a Text value. It may be, for example, an error value or a Text value.</remarks>
		[System.ComponentModel.Description("The value of this channel. "), System.ComponentModel.Category("Channel")] 
		public override KineticaRT.Values.Value Value
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
            //ft Ici j'ai change l'ordre... pour eviter de perdre des Value qui ont change comme dans le NumericThresholdItem.Connect
            // il faut attacher le handler avant de faire le connect et en en pratique on a pas besoin de faire la ligne this.Value = this.expression.Value
            //parce que l'expression generea elle-meme la value et on tombera automatiquement dans la fonction Expression_OnValueCHanged
            expression.ValueChanged += new KineticaRT.Channels.ValueChangedEventHandler(Expression_OnValueChanged);
            base.Connect () ;
			expression.Connect () ;
			
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
            //ft Test changer l'ordre du disconnect et le retrait de l'handler pour s'assurer que L'expression retourne le Value NotConnected au moment du disconnect
			expression.Disconnect () ;
			base.Disconnect () ;
            expression.ValueChanged -= new KineticaRT.Channels.ValueChangedEventHandler(Expression_OnValueChanged);
		}

		private void Expression_OnValueChanged (KineticaRT.Channels.IChannel sender, KineticaRT.Values.Value newVal)
		{
			Value = newVal ;
		}
		#endregion
	}
}

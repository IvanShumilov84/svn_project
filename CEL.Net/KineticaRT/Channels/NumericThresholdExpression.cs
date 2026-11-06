/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 6/26/2006
 * Time: 8:43 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using KineticaRT;
using KineticaRT.Channels;
using KineticaRT.Logic.Expressions;
using KineticaRT.Logic.Expressions.Design;
using KineticaRT.Values;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
	
namespace CEL.KineticaRT.Channels
{
	[Serializable]
    public class NumericThresholdExpression : CEL.KineticaRT.Channels.NumericThresholdChannel, INumericExpressionChannel
	{
		#region "Fields"

		//ft private bool 						connected = false;
		private Expression 					expression;
		private ValueChangedEventHandler	srcHdlr;
	
		#endregion

		#region "Constructor"
		
		public NumericThresholdExpression() : base()
		{
			this.expression					= new Expression();
			this.expression.ObjectSearch   += new ObjectSearchEventHandler( this.OnObjectSearch );
			this.srcHdlr					= new ValueChangedEventHandler( this.Expression_OnValueChanged );
		}
		
		protected NumericThresholdExpression( SerializationInfo info, StreamingContext context ) : base( info, context )
		{
			if ( info == null )
			{
				throw new ArgumentNullException( "info" );
			}
			this.expression = new Expression();
			try
			{
				this.expression.Text			= info.GetString( "Text" );
			}
			catch ( SerializationException exception1 )
			{
				Trace.WriteLine( exception1.Message, base.GetType().ToString() );
			}
			this.expression.ObjectSearch   += new ObjectSearchEventHandler( this.OnObjectSearch );
			this.srcHdlr					= new ValueChangedEventHandler( this.Expression_OnValueChanged );
		}
		
		#endregion

		#region "Properties"
				
		[Editor(typeof(ExpressionEditor),typeof(UITypeEditor)),DefaultValue(""),Description("Expression Text"),Category("NumericExprression")]
		public string Text
		{
			get
			{
				return this.expression.Text;
			}
			set
			{
				if ( this.expression.Text != value )
				{
					if ( this.Connected )
					{	
						this.expression.ValueChanged -= this.srcHdlr;
						this.expression.Disconnect();
					}
					this.expression.Text = value;
					if ( this.Connected )
					{
                        this.expression.ValueChanged += this.srcHdlr;//Attacher le handler avant le connect
						this.expression.Connect();
						base.Value = this.expression.Value;//ft
					}
					this.OnPropertyChanged( "Text" );				
				}
			}
		}

		[Category("Channel"),Description("The value of this channel.")]
		public override Value Value
		{
			get
			{
				return base.Value;
			}
		}
		
		#endregion	

		#region "Methods"
	
		public override void Connect()
		{
			
			lock( this )
			{
				if ( this.Connected )
				{
					this.expression.ValueChanged -= this.srcHdlr;
				}
              
                //ft Ici j'ai change l'ordre... pour eviter de perdre des Value qui ont change comme dans le NumericThresholdItem.Connect
                // il faut attacher le handler avant de faire le connect et en en pratique on a pas besoin de faire la ligne this.Value = this.expression.Value
                //parce que l'expression generea elle-meme la value et on tombera automatiquement dans la fonction Expression_OnValueCHanged
                this.expression.ValueChanged += this.srcHdlr;
                this.expression.Connect();
				this.Value = this.expression.Value;//ft
                base.Value = this.expression.Value;//ft
                base.Connect();
			}
		}

		public override void Disconnect()
		{
			if ( this.Connected )
			{
				//ft this.expression.ValueChanged -= this.srcHdlr;
                //ft this.expression.Disconnect();
                //ft Test changer l'ordre du disconnect et le retrait de l'handler pour s'assurer que L'expression retourne le Value NotConnected au moment du disconnect
                this.expression.Disconnect();
                this.expression.ValueChanged -= this.srcHdlr;

                base.Disconnect();
			}
			
		}
		
		private void Expression_OnValueChanged( IChannel sender, Value newVal )
		{
			this.Value = newVal;
		}
		
		public override void GetObjectData( SerializationInfo info, StreamingContext context )
		{
			if ( info == null )
				throw new ArgumentNullException( "info" );
			base.GetObjectData( info, context );
			info.AddValue( "Text", this.expression.Text );
		}
				
		#endregion		
	}
}

/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 8/1/2006
 * Time: 9:12 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using KTC = KineticaRT.Channels;
using KTCTRL = KineticaRT.Controls;
using KTV = KineticaRT.Values;
using System;
using System.ComponentModel;

namespace CEL.KineticaRT.Controls
{
	/// <summary>
	/// Description of PictureButton.
	/// </summary>
	public class PictureButton : KTCTRL.PictureButton
	{
		#region Fields

		private KTC.IChannel					buttonResetStateSourceChannel;
		private string 							buttonResetStateSourceName = string.Empty;
		private KTC.ValueChangedEventHandler 	buttonResetStateSrcHdlr;
		private bool							buttonResetStateValue = false;
		
		#endregion
		
		#region Constructor
		
		public PictureButton() : base()
		{
			this.buttonResetStateSrcHdlr = new KTC.ValueChangedEventHandler( this.OnButtonResetStateValueChanged );
		}
		
		#endregion
		
		#region Properties
		
		[Category("Button"),DefaultValue(""),Description("Gets or sets the name of the channel that will resets this Button state.")]
		public string ButtonResetStateSource
		{
			get
			{
				return this.buttonResetStateSourceName;
			}
			set
			{
				if ( this.buttonResetStateSourceName != value )
				{
					this.buttonResetStateSourceName = value;
					this.ButtonResetStateSourceChannel = base.OnObjectSearch( this.buttonResetStateSourceName ) as KTC.IChannel;
					this.OnPropertyChanged( "ButtonResetStateSource" );
				}
			}
		}
		
		private KTC.IChannel ButtonResetStateSourceChannel
		{
			set
			{
				if ( this.buttonResetStateSourceChannel != value )
				{
					if ( this.buttonResetStateSourceChannel != null )
					{
						this.buttonResetStateSourceChannel.ValueChanged -= this.buttonResetStateSrcHdlr;
						this.ButtonResetStateValue = false;
					}
					this.buttonResetStateSourceChannel = value;
					if ( this.buttonResetStateSourceChannel != null )
					{
						this.ButtonResetStateValue = 
							( ( this.buttonResetStateSourceChannel.Value != null ) && 
							 ( this.buttonResetStateSourceChannel.Value.ToDouble() != 0 ) );
						this.buttonResetStateSourceChannel.ValueChanged += this.buttonResetStateSrcHdlr;
					}
				}
			}
		}
		
		private bool ButtonResetStateValue
		{
			get
			{
				return this.buttonResetStateValue;
			}
			set
			{
				if ( this.buttonResetStateValue != value )
				{
					this.buttonResetStateValue = value;
					if ( this.buttonResetStateValue == true )
					{
						this.SetButtonState( false );
					}
				}
			}
		}
		
		#endregion
		
		#region Methods
        private System.Drawing.Color originalBackColor;
		public override void Connect()
		{
            if (originalBackColor == null || this.BackColor != System.Drawing.Color.Fuchsia)
            {
                originalBackColor = this.BackColor;
            }
            else
            {
                this.BackColor = originalBackColor;
            }

            base.Connect();
                       

			if ( this.buttonResetStateSourceName != "" )
			{
				this.ButtonResetStateSourceChannel = base.OnObjectSearch( this.buttonResetStateSourceName ) as KTC.IChannel;
			}
            if (this.buttonResetStateSourceChannel == null) //ft
            {
                this.BackColor = System.Drawing.Color.Fuchsia;
            }
		}
		
		public override void Disconnect()
		{
			if ( this.buttonResetStateSourceChannel != null )
			{
				this.ButtonResetStateSourceChannel = null;
			}
		}

		private void OnButtonResetStateValueChanged( KTC.IChannel sender, KTV.Value newValue )
		{
			lock (this)
			{
				this.ButtonResetStateValue = ( newValue.ToDouble() != 0 );
			}
		}
			
		#endregion
	}
}

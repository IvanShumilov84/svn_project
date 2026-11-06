using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Diagnostics ;
using System.Windows.Forms;
using KineticaRT;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// KineticaRT EnabledButton Control. 
	/// Provides all the functionality of a standard KineticaRT Button,
	/// but connects to a channels Enabled property.
	/// </summary>
	[DefaultProperty("Value")]
#if KRT_license
	[LicenseProviderAttribute(typeof(Button.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Button))]
	public class EnabledButton : KineticaRT.Controls.Button, IButton
	{
		private KineticaRT.IEnabled sourceChannel ;
		private KineticaRT.EnabledEventHandler enabledHdlr ;
		private bool currentValue ;

		/// <summary>
		/// Constructor.
		/// </summary>
		public EnabledButton()
		{
			enabledHdlr	= new EnabledEventHandler(OnEnabled) ;
            // this.AllowDrop = true ; SB removed because it is overriding the user values
			this.Style = ButtonStyles.CheckBox ;
			this.Size = new Size(182, 14) ;
		}

		/// <summary>
		/// Gets or sets the object that is the Source of the Button's value
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets the object that is the Source of the Button's value")]
		[EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Channels.IChannel SourceChannel
		{
			get
			{
				return sourceChannel as KineticaRT.Channels.IChannel ;
			}
			set
			{
				KineticaRT.IEnabled new_channel = value as KineticaRT.IEnabled ;

				if (sourceChannel != new_channel)
				{
					//free old handler if any
					if (sourceChannel != null)		
					{
						sourceChannel.EnabledChanged -= enabledHdlr ;
					}

					//attach new source
					sourceChannel = new_channel ;
					if (sourceChannel != null)
					{
						KineticaRT.Channels.InfoChannel info_channel = value as KineticaRT.Channels.InfoChannel ;
						if (info_channel != null)
						{
							this.Text = info_channel.Description ;
						}
						else
						{
							this.Text = "" ;
						}
						sourceChannel.EnabledChanged += enabledHdlr ;
						 
						OnEnabled (sourceChannel, sourceChannel.Enabled ? KineticaRT.EnabledEventArgs.EnabledEvent : KineticaRT.EnabledEventArgs.DisabledEvent) ;
					}
					else
					{
						this.Text = "" ;
					}

					OnPropertyChanged("SourceChannel") ;
				}
			}
		}
        private bool ShouldSerializeSourceChannel()
        {
            return false;
        }

		/// <summary>
		/// Gets or sets the object that is the Destination for the Button's value
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets the object that is the Destination for the Button's value")]
		[EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
		public override  Channels.IChannel DestinationChannel
		{
			get
			{
				return null ;
			}
			set
			{
			}
		}

		/// <summary>
		/// The enabled property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">New value</param>
		protected virtual void OnEnabled(object Sender, EnabledEventArgs e)
		{
			currentValue = e.Enabled ;
			this.Value = e.Enabled ;
		}

	
		//public event EnabledChangedEventHandler EnabledChanged;   
		///// <summary>Called when the value of the Value property changes.</summary>
		//private void OnEnabledChanged(){if (EnabledChanged!= null) EnabledChanged(this, enabled);}

		/// <summary>
		/// Gets or sets the current value
		/// </summary>
		[Category("Button")]
		[Description("Gets or sets the current value")]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override object Value
		{
			get
			{
				return base.Value ;
			}
			set
			{
				base.Value = value ;

				bool new_value ;

				try
				{
					new_value = System.Convert.ToBoolean (value) ;
				}
				catch (System.FormatException)
				{
					new_value = false ;
				}

				if (sourceChannel != null) sourceChannel.Enabled = new_value ;
			}
		}
        private bool ShouldSerializeValue()
        {
            return false;
        }
    }
}

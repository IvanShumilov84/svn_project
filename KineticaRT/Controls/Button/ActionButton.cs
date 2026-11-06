
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
	public class ActionButton : KineticaRT.Controls.Button, IButton
	{
        private KineticaRT.Channels.IChannel sourceChannel;
		//private bool currentValue ;

		/// <summary>
		/// Constructor.
		/// </summary>
        public ActionButton()
		{
			// this.AllowDrop = true ;  SB removed because it is overriding the user values
			this.Style = ButtonStyles.Button ;
            this.Type = ButtonReturnTypes.AutoReturn;
			this.Size = new Size(182, 14) ;
		}

        private string methodName;
        /// <summary>
        /// Gets or sets the name of method to call when the button is pressed.
        /// </summary>
        [Category("Button")]
        [Description("The name of method to call when the button is pressed.")]
        [DefaultValue("")]
        public string MethodName
        {
            get
            {
                return methodName;
            }
            set
            {
                methodName = value;
                OnPropertyChanged("MethodName");
            }
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
                KineticaRT.Channels.IChannel new_channel = value as KineticaRT.Channels.IChannel;

				if (sourceChannel != new_channel)
				{
					//attach new source
					sourceChannel = new_channel ;
                    
                    if (UseSourceFormat)
                    {
                        if (sourceChannel != null)
                        {
                            KineticaRT.Channels.InfoChannel info_channel = value as KineticaRT.Channels.InfoChannel;
                            if (info_channel != null)
                            {
                                this.Text = info_channel.Description + "." + methodName ;
                            }
                            else
                            {
                                this.Text = "";
                            }
                        }
                        else
                        {
                            this.Text = "";
                        }
                    }
					
                    OnPropertyChanged("SourceChannel") ;
				}
			}
		}
        private bool ShouldSerializeSourceChannel()
        {
            return false;
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

				if (sourceChannel != null && !string.IsNullOrEmpty(methodName))
                {
                    if (new_value == false)
                    {
                        System.Reflection.MethodInfo mi;
                        mi = sourceChannel.GetType().GetMethod(methodName);
                        if (mi != null)
                        {
                            mi.Invoke(sourceChannel, null);
                        }
                    }
                }
			}
		}
        private bool ShouldSerializeValue()
        {
            return false;
        }
    }
}


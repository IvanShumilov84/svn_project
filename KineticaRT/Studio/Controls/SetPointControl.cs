using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KineticaRT;

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// Base class for KineticaRT.Studio controls with a Setpoint channel
	/// </summary>
	public class SetPointControl : ScaledControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Channels.IInfoChannel	setPointSourceChannel ;
		private string					setPointSource ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the SetPointControl class
		/// </summary>
		public SetPointControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			setPointSource			= "" ;
			setPointSourceChannel	= null ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// SetPointControl
			// 
			this.Name = "SetPointControl";

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the name of the SetPoint Source channel whose value is displayed
		/// </summary>
		[Category("SetPoint")]
		[Description("Gets or sets the name of the SetPoint Source channel whose value is displayed")]
		[DefaultValue("")]
		public string SetPointSource
		{
			get
			{
				return setPointSource ;
			}
			set
			{
				if (setPointSource != value)
				{
					setPointSource = value ;
					OnPropertyChanged("SetPointSource") ;
					SetPointSourceChannel = OnObjectSearch(setPointSource) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the SetPoint channel to which this object is connected
		/// </summary>
		[Category("SetPoint")]
		[Description("Gets or sets the SetPoint channel to which this object is connected")]
		[DefaultValue(null)]
		public virtual Channels.IInfoChannel SetPointSourceChannel
		{
			get
			{
				return setPointSourceChannel ;
			}
			set
			{
				if (setPointSourceChannel != value)
				{
					setPointSourceChannel = value ;
					OnPropertyChanged("SetPointSourceChannel") ;
				}
			}
		}

		/// <summary>
		/// Gets the current SetPoint Value
		/// </summary>
		[Category("SetPoint")]
		[Description("Gets the current SetPoint Value")]
		public Values.Value SetpointValue
		{
			get
			{
				return (setPointSourceChannel != null) ? setPointSourceChannel.Value : Values.Values.NotConnected ;
			}
		}
		#endregion

		#region IConnectable
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			base.Connect() ;
			if (setPointSource != "")				SetPointSourceChannel = OnObjectSearch(setPointSource) ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			base.Disconnect() ;
			if (setPointSourceChannel != null)		SetPointSourceChannel = null ;
		}
		#endregion
	}
}

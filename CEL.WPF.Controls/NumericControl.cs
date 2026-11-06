using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using KineticaRT;

namespace CEL.WPF.Controls
{
    /// <summary>
	/// Extends the ControlBase class by including Units and numeric Format
	/// </summary>
	[DefaultProperty("Source")]
	public class NumericControl : ControlBase
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
        //private System.ComponentModel.Container components = null;

		private string					customUnits ;
		private bool					useCustomUnits ;
		private string					format ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the NumericControl class
		/// </summary>
		public NumericControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			customUnits				= "" ;
			useCustomUnits			= false ;
			format					= "" ;
		}

        ///// <summary> 
        ///// Clean up any resources being used.
        ///// </summary>
        //protected override void Dispose( bool disposing )
        //{
        //    if( disposing )
        //    {
        //        if(components != null)
        //        {
        //            components.Dispose();
        //        }
        //    }
        //    base.Dispose( disposing );
        //}
		#endregion

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// NumericControl
			// 
			this.Name = "NumericControl";

		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets a Custom Units string
		/// </summary>
		[Category("Units")]
		[Description("Gets or sets a Custom Units string")]
		[DefaultValue("")]
		public virtual string CustomUnits
		{
			get
			{
				return customUnits ;
			}
			set
			{
				if (value != customUnits)
				{
					customUnits = value ;
					OnPropertyChanged("CustomUnits") ;
					
					if (useCustomUnits)
					{
						OnPropertyChanged("Units") ;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets whether to use the Custom units string or the Source channel's value
		/// </summary>
		[Category("Units")]
		[Description("Gets or sets whether to use the Custom units string or the Source channel's value")]
		[DefaultValue(false)]
		public virtual bool UseCustomUnits
		{
			get
			{
				return useCustomUnits ;
			}
			set
			{
				if (useCustomUnits != value)
				{
					useCustomUnits = value ;
					OnPropertyChanged("UseCustomUnits") ;
					OnPropertyChanged("Units") ;
				}
			}
		}

		/// <summary>
		/// Gets the Units string displayed. Value depends on UseCustomUnits property
		/// </summary>
		[Category("Units")]
		[Description("Gets the Units string displayed. Value depends on UseCustomUnits property")]
		public string Units
		{
			get
			{
				string			text	= "" ;

				if (useCustomUnits)
				{
					text = customUnits ;
				}
				else if (SourceChannel is KineticaRT.Channels.INumericChannel)
				{
                    text = ((KineticaRT.Channels.INumericChannel)SourceChannel).Units;
				}
				return text ;
			}
		}

		/// <summary>
		/// Gets or sets the text formatting of the Value
		/// If specified (ie non-blank string), overrides the format of the Source
		/// </summary>
		[Category("General")]
		[Description("If specified overrides Format of Source. Eg. C,E,F3,G6,N")]
		[DefaultValue("")]
		public virtual string Format
		{
			get
			{
				return format ;
			}
			set
			{
				if (format != value)
				{
					format = value ;
					OnPropertyChanged("Format") ;
				}
			}
		}
		#endregion
	}
}

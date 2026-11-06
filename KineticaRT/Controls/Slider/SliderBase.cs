using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using KineticaRT.Controls;
using KineticaRT;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Basic Features of a Slider Control
	/// </summary>
	public class SliderBase : KineticaRT.Controls.Control, IConnectable //ft ajout de IConnectable
	{
		#region Variables
        
        /// <summary>
        /// Gradient Enabled
        /// </summary>
        protected bool gradientEnabled; //ft 
        /// <summary>
        /// Gradient Mode
        /// </summary>
        protected LinearGradientMode gradientMode;//ft


		/// <summary>
		/// Bitmap in which the basic slider gets drawn
		/// </summary>
		protected	Bitmap					bmpBackground ;
		/// <summary>
		/// Offset used to keep Scale sensibly inside DisplayRectangle
		/// </summary>
		protected	float					offset ;
		/// <summary>
		/// Event handler for PropertyChanged events from child objects
		/// </summary>
		protected	PropertyChangedEventHandler	propertyHdlr ;
		/// <summary>
		/// Event handler for ObjectSearch events from child objects
		/// </summary>
		protected	ObjectSearchEventHandler	searchHdlr ;
		
		/// <summary>
		/// Current value of the Scale property
		/// </summary>
		protected KineticaRT.Controls.Scale	scale ;
		/// <summary>
		/// Current value of the Orientation property
		/// </summary>
		protected ScaleOrientations			orientation ;
		/// <summary>
		/// Currect value of the UseSourceFormat property
		/// </summary>
		protected	bool						useSourceFormat ;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the SliderBase class
		/// </summary>
		public SliderBase()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			offset			= 0 ;
			bmpBackground	= null ;

			orientation		= ScaleOrientations.Vertical ;
			scale			= new KineticaRT.Controls.Scale() ;
			useSourceFormat = true ;

			//event handlers for sub-objects
			propertyHdlr	= new PropertyChangedEventHandler(OnChildPropertyChanged);
			searchHdlr		= new ObjectSearchEventHandler(OnObjectSearch) ;

			scale.PropertyChanged += propertyHdlr ;
			scale.ObjectSearch += searchHdlr ;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (bmpBackground != null)
				{
					bmpBackground.Dispose() ;
				}

				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// SliderBase
			// 
			this.Name = "SliderBase";
			this.Size = new System.Drawing.Size(96, 200);

		}
		#endregion

		#region Drawing
		/// <summary>
		/// Calculates an offset for how much of the control's area should be used for the Scale
		/// based on font size (vertical scales only)
		/// </summary>
		/// <param name="g">Graphics object used for string measurement</param>
		/// <returns></returns>
		protected float GetOffsetRatio(Graphics g)
		{
			if (orientation == ScaleOrientations.Vertical)
			{
				//this is to stop the labels going off the top or bottom (vertical scales)
				SizeF				size ;

				size = g.MeasureString("A", this.Font) ;

				float		x ;
				x = (0.5F * size.Height) / DisplayRectangle.Height ;
//				return (DisplayRectangle.Top + 0.5 * size.Height) / (double)DisplayRectangle.Height ;
				return x ;
			}

			return 0.05F ;
		}
		#endregion

		#region Properties




		///<summary>
		///Gets the control's Scale object
		///</summary>
		[Category("Slider")]
		[Description("Gets the control's Scale object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new KineticaRT.Controls.Scale Scale
		{
			get 
			{
				return scale ;
			}
		}
		private bool ShouldSerializeScale()
		{
			//always save the scale
			return true ;
		}
		/// <summary>
		/// Reset the Scale to its default settings
		/// </summary>
		public void ResetScale()
		{
			scale = new Scale() ;
		}

		/// <summary>
		/// Gets or sets the Orientation of the Slider
		/// </summary>
		[Category("Slider")]
		[Description("Gets or sets the Orientation of the Slider")]
		[DefaultValue(Orientation.Vertical)]
		public virtual Orientation Orientation
		{
			get
			{
				if (orientation == ScaleOrientations.Horizontal)
				{
					return Orientation.Horizontal ;
				}
				return Orientation.Vertical ;
			}
			set
			{
				ScaleOrientations	new_value ;

				if (value == Orientation.Horizontal)
				{
					new_value = ScaleOrientations.Horizontal ;
				}
				else
				{
					new_value = ScaleOrientations.Vertical ;
				}
				
				if (orientation != new_value)
				{
					orientation = new_value;
					AddToQueue(true) ;
					OnPropertyChanged("Orientation") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the Slider gets its ToolTip, Minimum and Maximum
		/// from the Source of it's primary Needle
		/// </summary>
		[Category("Slider")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Slider gets its ToolTip, Minimum and Maximum from the Source of it's primary Needle")]
		public virtual bool UseSourceFormat
		{
			get
			{
				return useSourceFormat ;
			}
			set
			{
				//NB: set must be overridden to call SetFormat
				if (useSourceFormat != value)
				{
					useSourceFormat = value ;
//					if (useSourceFormat)
//					{
//						SetFormat() ;
//					}
					AddToQueue(false) ;
					OnPropertyChanged("UseSourceFormat") ;
				}
			}
		}

		#endregion

		#region Mouse Event handling
		/// <summary>
		/// Converts the given position into a fraction of the Slider's current range
		/// </summary>
		/// <param name="XPos">X position to convert</param>
		/// <param name="YPos">Y position to convert</param>
		/// <returns>Converted Fraction</returns>
		protected double PositionToFraction(int XPos, int YPos)
		{
			double		fraction ;
			Rectangle	rect			= DisplayRectangle ;

			//calculate fraction of display rectangle
			if (orientation == ScaleOrientations.Vertical)
			{
				if (!scale.AntiClockwise)
				{
					fraction = 1 - ((double)YPos / (double)rect.Height) ;
				}
				else
				{
					fraction = ((double)YPos / (double)rect.Height) ;
				}
			}
			else
			{
				if (scale.AntiClockwise)
				{
					fraction = 1 - ((double)XPos / (double)rect.Width) ;
				}
				else
				{
					fraction = ((double)XPos / (double)rect.Width) ;
				}
			}

			if (fraction > (1 - offset))
			{
				fraction = 1 - offset ;
			}
			else if (fraction < offset)
			{
				fraction = offset ;
			}

			//convert fraction to fraction of scale
			fraction = (fraction - offset) / (1 - 2 * offset) ;

			return fraction ;
		}
		#endregion

		#region Other methods
		/// <summary>
		/// Sets up the tool tip and scale range based on the given Needle's Source
		/// </summary>
		/// <param name="Needle">Needle whose Source information is used</param>
		protected void SetFormat(SliderNeedle Needle)
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel		ch ;

				ch = Needle.SourceChannel as Channels.IInfoChannel ;
				if (ch != null)
				{
					Channels.INumericChannel		num ;

					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;

					num = ch as Channels.INumericChannel ;
					if (num != null)
					{
						scale.Minimum	= num.Minimum ;
						scale.Maximum	= num.Maximum ;
					}
				}
			}
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (scale != null)		scale.Connect() ;
            base.Connect(); //ft
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (scale != null)		scale.Disconnect() ;
            base.Disconnect();  //ft
		}
     
		#endregion
	}
}


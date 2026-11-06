using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using KineticaRT.Controls;
using System.Diagnostics;

namespace KineticaRT.Controls
{
	/// <summary>
	/// Basic functionality of a Dial control
	/// </summary>
	[ChangeRecord ("1.0", "04 03 2003", "S. Child", "Original version")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	public class DialBase : KineticaRT.Controls.Control, IConnectable //ft ajout de IConnectable
	{
		#region Variables
		/// <summary>
		/// Bitmap in which the basic dial gets drawn
		/// </summary>
		protected	Bitmap						bmpBackground ;
		/// <summary>
		/// Event handler for PropertyChanged events from child objects
		/// </summary>
		protected	PropertyChangedEventHandler	propertyHdlr ;
		/// <summary>
		/// Event handler for ObjectSearch events from child objects
		/// </summary>
		protected	ObjectSearchEventHandler	searchHdlr ;

		/// <summary>
		/// Current value of Scale property
		/// </summary>
		protected	KineticaRT.Controls.Scale	scale ;
		/// <summary>
		/// Current value of TextPosition property
		/// </summary>
		protected	double						textPosition ;
		/// <summary>
		/// Current value of TopText property
		/// </summary>
		protected	string						topText ;
		/// <summary>
		/// Current value of BottomText property
		/// </summary>
		protected	string						bottomText ;
		/// <summary>
		/// Current value of the UseSourceFormat property
		/// </summary>
		protected	bool						useSourceFormat ;
        /// <summary>
        /// Current value of the UseSourceForTopAndBottomText property
        /// </summary>
        protected bool useSourceForTopAndBottomText = true;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the DialBase class
		/// </summary>
		public DialBase()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			scale = new KineticaRT.Controls.Scale() ;

			textPosition	= 0.3 ;
			topText			= "" ;
			bottomText		= "" ;
//skc			base.Enabled	= false ;
			useSourceFormat = true ;

			//event handlers for sub-objects
			propertyHdlr= new PropertyChangedEventHandler(OnChildPropertyChanged);
			searchHdlr	= new ObjectSearchEventHandler(OnObjectSearch);

			scale.PropertyChanged	+= propertyHdlr ;
			scale.ObjectSearch		+= searchHdlr ;
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

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// DialBase
			// 
			this.Name = "DialBase";

		}
		#endregion

		#region Properties
		///<summary>
		///Gets the Scale object
		///</summary>
		[Category("Dial")]
		[Description("Gets the Scale object")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public new KineticaRT.Controls.Scale Scale
		{
			get 
			{
				return scale ;
			}
            set
            {
                scale = value;
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

		///<summary>
		///Gets or sets the Text shown above the centre of the Dial
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Text shown above the centre of the Dial")]
		[DefaultValue("")]
		public virtual string TopText
		{
			get
			{
				return topText ;
			}
			set
			{
				if (topText != value)
				{
					topText = value ;
					AddToQueue(true) ;
					OnPropertyChanged("TopText") ;
				}
			}
		}

		///<summary>
		///Gets or sets the Text shown below the centre of the Dial
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the Text shown below the centre of the Dial")]
		[DefaultValue("")]
		public virtual string BottomText
		{
			get
			{
				return bottomText ;
			}
			set
			{
				if (bottomText != value)
				{
					bottomText = value ;
					AddToQueue(true) ;
					OnPropertyChanged("BottomText") ;
				}
			}
		}

		///<summary>
		///Gets or sets the position of text from the Dial's centre as a proportion of the Dial's radius (0..1)
		///</summary>
		[Category("Dial")]
		[Description("Gets or sets the position of text from the Dial's centre as a proportion of the Dial's radius (0..1)")]
		[DefaultValue(0.3)]
		public virtual double TextPosition
		{
			get
			{
				return textPosition ;
			}
			set
			{
				if (textPosition != value)
				{
					textPosition = value ;
					AddToQueue(true) ;
					OnPropertyChanged("TextPosition") ;
				}
			}
		}

//skc 4/3/03...
//		/// <summary>
//		/// Gets or sets whether user input (mouse dragging) is enabled
//		/// </summary>
//		[Category("Dial")]
//		[Description("Gets or sets whether user input (mouse dragging) is enabled")]
//		[DefaultValue(false)]
//		public new bool Enabled
//		{
//			get	{ return base.Enabled ;	}
//			set	{ base.Enabled = value ; }
//		}
//...skc

		/// <summary>
		/// Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum
		/// from the Source of it's primary Needle
		/// </summary>
		[Category("Dial")]
		[DefaultValue(true)]
		[Description("Gets or sets whether the Dial gets its ToolTip, TopText, BottomText, Minimum and Maximum from the Source of it's primary Needle")]
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

		#region Drawing
		/// <summary>
		/// Draw the Top and Bottom text onto the Dial
		/// </summary>
		/// <param name="g">Graphics object in which to draw</param>
		protected void DrawText(Graphics g)
		{
			if (topText.Length > 0 || bottomText.Length > 0)
			{
				Rectangle		rect	= DisplayRectangle ;
				SizeF			size	= new SizeF() ;
				StringFormat	format	= new StringFormat() ;
				Brush			brush	= new SolidBrush(ForeColor) ;

				//text is textPosition% away from centre
				format.Alignment = StringAlignment.Center ;
				format.LineAlignment = StringAlignment.Center ;

				size = g.MeasureString(topText, this.Font) ;
				rect.Y = ScaleY (0.5 - (0.5 * textPosition)) - (int)(size.Height / 2) ;
				rect.Height = (int)size.Height ;
				g.DrawString(topText, this.Font, brush, rect, format) ;

				size = g.MeasureString(topText, this.Font) ;
				rect.Y = ScaleY (0.5 + (0.5 * textPosition)) - (int)(size.Height / 2) ;
				rect.Height = (int)size.Height ;
				g.DrawString(bottomText, this.Font, brush, rect, format) ;
			}
		}
		#endregion

		#region Mouse Event handling
		/// <summary>
		/// Converts a Mouse Position into a fraction of the way round the Dial
		/// </summary>
		/// <param name="XPos">X co-ordinate of mouse position</param>
		/// <param name="YPos">Y co-ordinate of mouse position</param>
		/// <returns>Fraction equivalent to given position</returns>
		protected double PositionToFraction(long XPos, long YPos)
		{
			double		min ;
			double		max ;

			return PositionToFractionRange(XPos, YPos, false, out min, out max) ;
		}

		/// <summary>
		/// Converts a Mouse Position into a fraction of the way round the Dial
		/// Also returns a range within +/- 5 degrees of that fraction if requested
		/// </summary>
		/// <param name="XPos">X co-ordinate of mouse position</param>
		/// <param name="YPos">Y co-ordinate of mouse position</param>
		/// <param name="Range">If true, MinFrac and MaxFrac are calculated</param>
		/// <param name="MinFrac">Fraction at a position 5 degrees less than given point</param>
		/// <param name="MaxFrac">Fraction at a position 5 degrees more than given point</param>
		/// <returns>Value equivalent to given position</returns>
		protected double PositionToFractionRange(long XPos, long YPos, bool Range, out double MinFrac, out double MaxFrac)
		{
            if (scale == null)
            {
                MinFrac = 0;
                MaxFrac = 1;
                return 0.0;
            }

			double		theta ;				//real angle
			double		angle ;				//angle round from ZeroAngle
			double		min_fraction		= 0 ;
			double		max_fraction		= 1 ;
			double		fraction ;
			double		dx ;
			double		dy ;

			//convert coordinates to angle
			dx = XPos - ScaleX (0.5) ;
			dy = YPos - ScaleY (0.5) ;

			theta = Math.Atan2(dy, dx) * 180 / Math.PI ;
			theta += 90 ;		//I want to measure from vertical not x-axis
			if (theta < 0)		theta += 360 ;

			//...and if the dial was square this would be...
			theta = UnscaleAngle(theta) ;

			if (!scale.AntiClockwise)
			{
				angle = 360 + theta - scale.ZeroAngle ;
			}
			else
			{
				angle = scale.ZeroAngle - theta ;
			}

			//ensure between 0 and 360 degrees
			if (angle >= 360)			angle -= 360 ;
			if (angle < 0)				angle += 360 ;

			//convert angle to fraction
			if (angle > scale.SweepAngle)
			{
				//between sweep and zero (out of range)
				// - which is it closer to?
				fraction = (angle > scale.SweepAngle + (360 - scale.SweepAngle)/2 ) ? 0 : 1 ;

				if (Range)
				{
					//invalid fractions - ie no range
					min_fraction = 2 ;
					max_fraction = -1 ;
				}
			}
			else		//in range
			{
				fraction		= angle / scale.SweepAngle ;

				if (Range)
				{
					min_fraction	= (angle - 5) / scale.SweepAngle ;
					max_fraction	= (angle + 5) / scale.SweepAngle ;

					if (min_fraction < 0)		min_fraction = 0 ;
					if (max_fraction > 1)		max_fraction = 1 ;
				}
			}

			MinFrac = min_fraction ;
			MaxFrac = max_fraction ;

			return fraction ;
		}
		#endregion

		#region Other methods
		/// <summary>
		/// Sets up the text, tool tip and scale range based on the given Needle's Source
		/// </summary>
		/// <param name="Needle">Needle whose Source information is used</param>
		protected void SetFormat(DialNeedle Needle)
		{
			if (useSourceFormat)
			{
				Channels.IInfoChannel	ch ;

				ch = Needle.SourceChannel as Channels.IInfoChannel ;
				if (ch != null)
				{
					Channels.INumericChannel		num ;

                    if (useSourceForTopAndBottomText)
                    {
                        TopText = (ch.Description != null) ? ch.Description : "";
                    }
					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;

					num = ch as Channels.INumericChannel ;
					if (num != null)
					{
                        if (useSourceForTopAndBottomText)
                        {
                            BottomText = (num.Units != null) ? num.Units : "";
                        }
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
            this.Connected = true; //ft
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (scale != null)		scale.Disconnect() ;
            this.Connected = false; //ft
		}
		#endregion
	}
}

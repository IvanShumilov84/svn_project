using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using KineticaRT;
using System.Drawing.Drawing2D;

namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// KineticaRT Studio LampIndicator control.
	/// Displays a boolean Value from a source channel,
	/// together with an optional description
	/// </summary>
	public class LampIndicator : ControlBase
	{
		private KineticaRT.Controls.Indicator valueIndicator;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Rectangle		descriptionRect ;
		private StringFormat	stringFormat ;

		private bool				descriptionOnLeft ;
		private StringAlignment		descriptionAlignment ;

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the LampIndicator class
		/// </summary>
		public LampIndicator()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//ft 
            //ft this.SetStyle(ControlStyles.Opaque, true); //ft  test pour supporter les background transparent

			stringFormat = new StringFormat() ;
			stringFormat.LineAlignment	= StringAlignment.Center ;
			stringFormat.Alignment		= StringAlignment.Near ;

			descriptionOnLeft	= true ;
			descriptionAlignment= StringAlignment.Near ;

            // valueIndicator.AllowDrop = false; SB removed because it is overriding the user values
            // base.AllowDrop = true; SB removed because it is overriding the user values


			UpdateLayout() ;
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
			KineticaRT.Controls.State state1 = new KineticaRT.Controls.State();
			this.valueIndicator = new KineticaRT.Controls.Indicator();
			this.SuspendLayout();
			// 
			// valueIndicator
			// 
            this.valueIndicator.BackColor = System.Drawing.Color.SteelBlue;
			this.valueIndicator.BorderColor = System.Drawing.Color.SteelBlue;
			this.valueIndicator.Location = new System.Drawing.Point(120, 0);
			this.valueIndicator.Name = "valueIndicator";
			this.valueIndicator.Size = new System.Drawing.Size(24, 24);
			this.valueIndicator.UseSourceFormat = false ;
			this.valueIndicator.Text = " " ;
			state1.BackColor = System.Drawing.SystemColors.Control;
			state1.BorderColor = System.Drawing.SystemColors.Control;
			state1.Text = " " ;
			this.valueIndicator.States = new KineticaRT.Controls.StateCollection(new KineticaRT.Controls.State[] {
																													 state1});
			this.valueIndicator.TabIndex = 0;
			// 
			// LampIndicator
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.valueIndicator});
			this.Name = "LampIndicator";
			this.Size = new System.Drawing.Size(150, 24);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties



        //ft
        /// <summary>
        /// Gets or sets whether gradient is enabled
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets whether the gradient is enabled")]
        public bool GradientEnabled
        {
            get
            {
                return this.valueIndicator.GradientEnabled;
            }
            set
            {
                if (this.valueIndicator.GradientEnabled != value)
                {
                    this.valueIndicator.GradientEnabled = value;
                    
                    OnPropertyChanged("GradientEnabled");
                }
            }
        }
        //ft
        /// <summary>
        /// Gets or sets the default gradient color
        /// </summary>
        [Category("Gradient")]
        [Description("Gets or sets the default gradient color")]
        public Color DefaultGradientColor
        {
            get
            {

                return this.valueIndicator.DefaultGradientColor;
            }
            set
            {
                if (this.valueIndicator.DefaultGradientColor != value)
                {
                    this.valueIndicator.DefaultGradientColor = value;
                    
                    OnPropertyChanged("DefaultGradientColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the linear gradient mode
        /// </summary>
        [Category("Gradient")]
        [DefaultValue(typeof(Color), "Navy")]
        [Description("Gets or sets the linear gradient mode")]
        public LinearGradientMode LinearGradientMode
        {
            get
            {
                return this.valueIndicator.LinearGradientMode;
            }
            set
            {
                if (this.valueIndicator.LinearGradientMode != value)
                {
                    this.valueIndicator.LinearGradientMode = value;
                   

                    OnPropertyChanged("LinearGradientMode");
                }
            }
        }


		/// <summary>
		/// Source channel to which this object is connected
		/// </summary>
		public override Channels.IInfoChannel SourceChannel
		{
			set
			{
				valueIndicator.SourceChannel = value ;
				base.SourceChannel = value ;
			}
		}

		/// <summary>
		/// Gets or sets a Custom Description
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets a Custom Description")]
		[DefaultValue("")]
		public override string CustomDescription
		{
            get
            {
                return base.CustomDescription ;
            }
            set
			{
				base.CustomDescription = value ;

				if (DescriptionSource == TextSources.Custom)
				{
					Invalidate(descriptionRect) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets where to get the Description from
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets where to get the Description from")]
		[DefaultValue(TextSources.Description)]
		public override TextSources DescriptionSource
		{
            get
            {
                return base.DescriptionSource ;
            }
            set
			{
				base.DescriptionSource = value ;
				Invalidate(descriptionRect) ;
			}
		}

		/// <summary>
		/// Gets or sets the Alignment of the description
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets the Alignment of the description")]
		[DefaultValue(StringAlignment.Near)]
		public StringAlignment DescriptionAlignment
		{
			get
			{
				return descriptionAlignment ;
			}
			set
			{
				if (descriptionAlignment != value)
				{
					descriptionAlignment = value ;
					stringFormat.Alignment = value ;
					OnPropertyChanged("DescriptionAlignment") ;
					Invalidate(descriptionRect) ;
				}
			}
		}

		/// <summary>
		/// Gets or sets upon which side of the lamp the description appears
		/// </summary>
		[Category("Description")]
		[Description("Gets or sets upon which side of the lamp the description appears")]
		[DefaultValue(true)]
		public bool DescriptionOnLeft
		{
			get
			{
				return descriptionOnLeft ;
			}
			set
			{
				if (descriptionOnLeft != value)
				{
					descriptionOnLeft = value ;
					OnPropertyChanged("DescriptionOnLeft") ;
					UpdateLayout() ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the shape of the lamp
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets the shape of the lamp")]
		[DefaultValue(true)]
		public KineticaRT.Controls.Shapes Shape
		{
			get
			{
				return valueIndicator.Shape ;
			}
			set
			{
				if (valueIndicator.Shape != value)
				{
					valueIndicator.Shape = value ;
					OnPropertyChanged("Shape") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets whether the lamp is raised or flat
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets whether the lamp is raised or flat")]
		[DefaultValue(true)]
		public bool Raised
		{
			get
			{
                return (valueIndicator.BorderType == KineticaRT.Controls.BorderType.Raised);
			}
			set
			{
                KineticaRT.Controls.BorderType new_style;

                new_style = value ? KineticaRT.Controls.BorderType.Raised : KineticaRT.Controls.BorderType.None;
                if (valueIndicator.BorderType != new_style)
				{
                    valueIndicator.BorderType = new_style;
					OnPropertyChanged("Raised") ;
				}
			}
		}

		/// <summary>
		/// Gets or Sets the Off (false) Color of the lamp
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or Sets the Off (false) Color of the lamp")]
		public Color OffColor
		{
			get
			{
				return valueIndicator.States[0].BackColor ;
			}
			set
			{
				valueIndicator.States[0].BackColor = value ;
				valueIndicator.States[0].BorderColor = value ;
				OnPropertyChanged("OffColor") ;
			}
		}
		private bool ShouldSerializeOffColor()
		{
			return valueIndicator.States[0].BackColor != SystemColors.Control ;
		}

		/// <summary>
		/// Gets or Sets the On (true) Color of the lamp
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or Sets the On (true) Color of the lamp")]
		public Color OnColor
		{
			get
			{
				return valueIndicator.BackColor ;
			}
			set
			{
				valueIndicator.BackColor = value ;
				valueIndicator.BorderColor = value ;
				OnPropertyChanged("OnColor") ;
			}
		}
		private bool ShouldSerializeOnColor()
		{
			return valueIndicator.BackColor != Color.SteelBlue ;
		}

		#endregion

		#region Drawing
		/// <summary>
		/// Paint Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e) ;

            //ft test e.Graphics.FillRectangle(new SolidBrush(BackColor), ClientRectangle);
            //ft test e.Graphics.Clear(BackColor);
			//draw description text
			if (descriptionRect.Width > 0)
			{
                Brush brush = new SolidBrush(ForeColor);
                e.Graphics.DrawString(Description, Font, brush, descriptionRect, stringFormat);
                brush.Dispose();
			}
		}
        ////ft test  pour permettre d<avoir les controle en transparent mais cause plus de probleme...
        //protected override void OnBackColorChanged(EventArgs e)
        //{
        //    if (this.Parent != null)
        //    {
        //        Parent.Invalidate(this.Bounds, true);
        //    }
        //    base.OnBackColorChanged(e);
        //}

        //protected override void OnParentBackColorChanged(EventArgs e)
        //{
        //    this.Invalidate();
        //    base.OnParentBackColorChanged(e);
        //}
        //protected override CreateParams CreateParams //ft pour permettre d<avoir les controle en transparent mais cause plus de probleme...
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
        //        return cp;
        //    }
        //}

		#endregion

		#region Private Methods
		/// <summary>
		/// Updates the layout of value, description and causes a repaint
		/// </summary>
		protected override void UpdateLayout()
		{
			valueIndicator.Height	= DisplayRectangle.Height ;
			valueIndicator.Width	= valueIndicator.Height ;
			valueIndicator.Top		= DisplayRectangle.Top ;

			descriptionRect			= new Rectangle(0, DisplayRectangle.Y, 0, DisplayRectangle.Height) ;
			descriptionRect.Width	= DisplayRectangle.Width - valueIndicator.Width ;

			if (descriptionOnLeft)
			{
				descriptionRect.X		= DisplayRectangle.X ;
				valueIndicator.Left		= descriptionRect.Right ;
			}
			else
			{
				valueIndicator.Left		= DisplayRectangle.X ;
				descriptionRect.X		= valueIndicator.Right ;
			}

			Invalidate(DisplayRectangle) ;
		}

		/// <summary>
		/// A new tool tip is being set
		/// </summary>
		protected override void NewToolTip(string NewText)
		{
			valueIndicator.ToolTipText = NewText ;
			base.NewToolTip(NewText) ;
		}

     
		/// <summary>
		/// A property in the Source channel has changed
		/// </summary>
		/// <param name="Sender">Object generating the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnSourcePropertyChanged(object Sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName != "Value")
			{
				Invalidate(DisplayRectangle) ;
			}
			base.OnSourcePropertyChanged(Sender, e) ;
		}
		#endregion
	}
}

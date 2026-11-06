using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using KineticaRT.Controls;
using KineticaRT;


namespace KineticaRT.Studio.Controls
{
	/// <summary>
	/// Summary description for Indicator.
	/// </summary>
	public class Indicator : ControlBase
	{
		private KineticaRT.Controls.Indicator valueIndicator;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public enum TextSources
		{
			/// <summary>
			/// Custom text
			/// </summary>
			Custom,
			/// <summary>
			/// Source channel Description property
			/// </summary>
			Description,
			/// <summary>
			/// Source Channel ToolTip property
			/// </summary>
			TrueText,
			/// <summary>
			/// No text
			/// </summary>
			None,
			/// <summary>
			/// Source Value TrueText/FalseText property matching the current state
			/// </summary>
			CurrentStateText,
			/// <summary>
			/// Source Value TrueText/FalseText property not matching the current state
			/// </summary>
			FutureStateText,
		}

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the LampIndicator class
		/// </summary>
		public Indicator()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);//ft 
			Shape = KineticaRT.Controls.Shapes.RoundedRectangle ;
			textSource = TextSources.CurrentStateText;

            // valueIndicator.AllowDrop = false;  SB removed because it is overriding the user values
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
			this.valueIndicator.Dock = System.Windows.Forms.DockStyle.Fill;
			this.valueIndicator.Location = new System.Drawing.Point(0, 0);
			this.valueIndicator.Name = "valueIndicator";
			this.valueIndicator.Size = new System.Drawing.Size(100, 32);
			state1.BackColor = System.Drawing.SystemColors.Control;
			state1.BorderColor = System.Drawing.SystemColors.Control;
			this.valueIndicator.States = new KineticaRT.Controls.StateCollection(new KineticaRT.Controls.State[] {
																													 state1});
			this.valueIndicator.TabIndex = 0;
			this.valueIndicator.Text = "";
			this.valueIndicator.UseSourceFormat = false;
			// 
			// Indicator
			// 
			this.Controls.Add(this.valueIndicator);
			this.Name = "Indicator";
			this.Size = new System.Drawing.Size(100, 32);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties

		private TextSources			textSource ;

		/// <summary>
		/// Gets or sets where to get the button text from
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets where to get the button text from")]
		[DefaultValue(TextSources.CurrentStateText)]
		public TextSources TextSource
		{
			get
			{
				return textSource ;
			}
			set
			{
				if (textSource != value)
				{
					textSource = value ;
					OnPropertyChanged("TextSource") ;
					NewIndicatorText() ;
				}
			}
		}

		private string				customText ;

		/// <summary>
		/// Gets or sets Custom button text
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets Custom button text")]
		[DefaultValue("")]
		public string CustomText
		{
			get
			{
				return customText ;
			}
			set
			{
				if (value != customText)
				{
					customText = value ;
					OnPropertyChanged("CustomText") ;
					
					if (textSource == TextSources.Custom)
					{
						OnPropertyChanged("Text") ;
						NewIndicatorText() ;
					}
				}
			}
		}

		/// <summary>
		/// Gets the Text displayed on the button. Value depends on TextSource property
		/// </summary>
		[Category("Indicator")]
		[Description("Gets the Text displayed on the button. Value depends on TextSource property")]
		public override string Text
		{
			get
			{
				string			text		= " " ;

				if (textSource == TextSources.Custom)
				{
					text = customText ;
				}
				else if (textSource == TextSources.None)
				{
					//nothing to do
				}
				else if (textSource == TextSources.CurrentStateText)
				{
					Channels.IBooleanChannel boolean = SourceChannel as Channels.IBooleanChannel ;

					if (boolean != null)
					{
						if ((boolean.Value != null) && KineticaRT.Values.Values.True.Equals(boolean.Value))
						{
							text = ((Channels.IBooleanChannel)SourceChannel).TrueText ;
						}
						else
						{
							text = ((Channels.IBooleanChannel)SourceChannel).FalseText ;
						}
					}
				}
				else if (textSource == TextSources.FutureStateText)
				{
					Channels.IBooleanChannel boolean = SourceChannel as Channels.IBooleanChannel ;

					if (boolean != null)
					{
						if ((boolean.Value != null) && KineticaRT.Values.Values.True.Equals(boolean.Value))
						{
							text = ((Channels.IBooleanChannel)SourceChannel).FalseText ;
						}
						else
						{
							text = ((Channels.IBooleanChannel)SourceChannel).TrueText ;
						}
					}
				}
				else if (SourceChannel != null)
				{
					if (textSource == TextSources.Description)
					{
						text = SourceChannel.Description ;
					}
					else if (SourceChannel is Channels.IBooleanChannel)
					{
						text = ((Channels.IBooleanChannel)SourceChannel).TrueText ;
					}
				}
				return text ;
			}
		}

		private bool IsOn 
		{
			get
			{
				bool on = false;
				
				Channels.IBooleanChannel boolean = SourceChannel as Channels.IBooleanChannel ;

				if (boolean != null && (boolean.Value != null) && KineticaRT.Values.Values.True.Equals(boolean.Value))
				{
					on = true ;
				}

				return on ;
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
				NewIndicatorText() ;
			}
		}

		/// <summary>
		/// Gets or sets the shape of the lamp
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or sets the shape of the lamp")]
		[DefaultValue(KineticaRT.Controls.Shapes.RoundedRectangle)]
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
				return (valueIndicator.BorderStyle == KineticaRT.Controls.BorderStyles.Raised) ;
			}
			set
			{
				KineticaRT.Controls.BorderStyles		new_style ;

				new_style = value ? KineticaRT.Controls.BorderStyles.Raised : KineticaRT.Controls.BorderStyles.None ;
				if (valueIndicator.BorderStyle != new_style)
				{
					valueIndicator.BorderStyle = new_style ;
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

		Color textOnColor = Color.Black;
		Color textOffColor = Color.Black;

		/// <summary>
		/// Gets or Sets the Off (false) Color of the Text 
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or Sets the Off (false) Color of the lamp")]
		public Color TextOffColor
		{
			get
			{
				return textOffColor ;
			}
			set
			{
				textOffColor = value;
				NewIndicatorText();
				OnPropertyChanged("TextOffColor") ;
			}
		}
		private bool ShouldSerializeTextOffColor()
		{
			return textOffColor != Color.Black ;
		}

		/// <summary>
		/// Gets or Sets the On (true) Color of the Text 
		/// </summary>
		[Category("Indicator")]
		[Description("Gets or Sets the On (true) Color of the lamp")]
		public Color TextOnColor
		{
			get
			{
				return textOnColor ;
			}
			set
			{
				textOnColor = value ;
				NewIndicatorText();
				OnPropertyChanged("TextOnColor") ;
			}
		}
		private bool ShouldSerializeTextOnColor()
		{
			return textOnColor != Color.Black ;
		}

		#endregion


		#region Private Methods
		/// <summary>
		/// Updates the layout of value, description and causes a repaint
		/// </summary>
		protected override void UpdateLayout()
		{
			valueIndicator.Height	= DisplayRectangle.Height ;
			valueIndicator.Width	= valueIndicator.Width ;
			valueIndicator.Top		= DisplayRectangle.Top ;

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
			NewIndicatorText();

			if (e.PropertyName != "Value")
			{
				Invalidate(DisplayRectangle) ;
			}

			base.OnSourcePropertyChanged(Sender, e) ;
		}
		#endregion

	
		private void NewIndicatorText()
		{
			string new_text = Text ; 
			Color new_color = IsOn ? textOnColor : textOffColor;

			if (valueIndicator.Text != new_text) valueIndicator.Text = new_text;
			if (valueIndicator.States[0].Text != new_text) valueIndicator.States[0].Text = new_text;
			
			if (new_color != valueIndicator.ForeColor) valueIndicator.ForeColor = new_color;
		}
	
	}
}

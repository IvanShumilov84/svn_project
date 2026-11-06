#if !VS2012
    extern alias Compact2;
#endif

using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.CodeDom;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;
using KineticaRT;
using KineticaRT.Controls;

namespace KineticaRT.Controls
{
	/// <summary>
	/// The KineticaRT Tab Control combines the features of a KineticaRT Frame and a
	/// Tab control. Thus allowing run-time editing of multiple pages of controls.
	/// </summary>
	[ChangeRecord ("1.0", "17 02 2003", "S. Child", "Major Changes")] 
	[Reference (ProjectNumber="P9790", ProjectName="KineticaRT")]
	[Copyright ("Copyright 2003 KineticaRT Ltd.")]
	
#if KRT_license
	[LicenseProviderAttribute(typeof(Tab.LicenseProvider))]
#endif
	[ToolboxBitmap(typeof(Tab))]
	[DefaultProperty("Panels")]
	public class Tab : KineticaRT.Controls.FrameBase
	{
		#region CLASS: ScrollButton
		private class ScrollButton
		{
			public Rectangle		rect ;			//where to draw
			public ButtonState		state ;			//how to draw
			public bool				held ;			//true if MouseDown on button

			private	System.Windows.Forms.ScrollButton	type ;			//left or right

			public ScrollButton(System.Windows.Forms.ScrollButton Type)
			{
				type	= Type ;

				rect	= Rectangle.Empty ;
				state	= ButtonState.Normal ;
				held	= false ;
			}

			public void Draw(Graphics g, bool Active)
			{
				ControlPaint.DrawScrollButton(g, rect, type, Active ? state : ButtonState.Inactive) ;
			}
		}
		#endregion

		#region License
#if KRT_license
		private License license = null;
		private	bool				demoMode ;
		// Do not move this class or its contents into another namespace - it would not be secure.
	
		////[LicenseProviderAttribute(typeof(xxx.LicenseProvider))]
		//private License license = null;
		////demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license.LicenseKey != "Run");
		//if (license != null) {license.Dispose();license = null;}
		//This block: update key using GUIDGEN, and typeof(xxx)
		/// <summary>
		/// Implements a license provider for the class.
		/// </summary>
		public new class LicenseProvider: System.ComponentModel.LicenseProvider
		{
			/// <summary>
			/// Gets a license for an instance or type of component, when given a context and whether the denial of a license throws an exception
			/// </summary>
			/// <param name="context">A LicenseContext that specifies where you can use the licensed object</param>
			/// <param name="type">A Type that represents the component requesting the license</param>
			/// <param name="instance">An object that is requesting the license</param>
			/// <param name="allowExceptions">true if a LicenseException should be thrown when the component cannot be granted a license; otherwise, false</param>
			/// <returns>A valid License</returns>
			public override System.ComponentModel.License GetLicense(LicenseContext context,Type type,object instance,bool allowExceptions)
			{
				License licence = null ;

				if (context != null)
				{
					string key = null ;

					if (context.UsageMode == LicenseUsageMode.Designtime)
					{
						Microsoft.Win32.RegistryKey registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
						if (registry != null) key = registry.GetValue ("RegKey") as string ;
						if ((key == null) || (key != "EA249366949B"))
						{
							new KineticaRT.LicenseForm (type.FullName).ShowDialog () ;
							Microsoft.Win32.RegistryKey registry2 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey (@"SOFTWARE\KineticaRT\"+type.FullName) ; 
							if (registry2 != null) key = registry2.GetValue ("RegKey") as string ;
						}
						if ((key != null) && (key == "EA249366949B")) key = "{EA249366-949B-43e1-B623-C556B970A238}" ; 
						context.SetSavedLicenseKey (typeof(Tab), key) ;
						
					}
					if  (key == null) key = context.GetSavedLicenseKey (typeof(Tab), null) ;
					if (key == "{EA249366-949B-43e1-B623-C556B970A238}") licence = new License ("Run") ;
					else if (allowExceptions && (context.UsageMode != LicenseUsageMode.Designtime)) throw new LicenseException (type, instance, type.FullName+": Invalid Licence Key - Please contact www.KineticaRT.co.uk") ;
					else licence = new License ("None") ;
				}

				return licence ;
			}

			private class License:System.ComponentModel.License 
			{
				string key ;

				public License (string key)
				{
					this.key = key ;
				}

				public override string LicenseKey 
				{
					get
					{
						return key ;
					}
				}
		
				public override void Dispose()
				{
				}
			}
		}
#endif
		#endregion

		#region Variables
		const int		SCROLL_AMOUNT		= 32 ;		//how far each click scrolls the tabs

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Rectangle			headerBar ;
		private PanelCollection		panels ;
		private int					currentPanelIndex ;		//0-based index
		private StringFormat		textFormat ;
		private	int					headerRight ;		//headerBar - scroll btns if needed
		private bool				needScrollBtns ;
		private int					scrollOffset ;
		private ScrollButton		leftScrollBtn ;
		private ScrollButton		rightScrollBtn ;
		private	Timer				scrollTimer ;
		private int					requestedPanel ;	//used iff currentPanelIndex set in initialisation
        
		private	PropertyChangedEventHandler					propertyHdlr ;
		private EventHandler								colorHdlr ;
//		private PanelInfo.PanelRenamedEventHandler			renamedHdlr ;
		private PanelCollection.PanelChangedEventHandler	addedHdlr ;
		private PanelCollection.PanelChangedEventHandler	removedHdlr ;
		#endregion (variables)

		#region Initialisation and Disposal
		/// <summary>
		/// Initialises a new instance of the Tab class
		/// </summary>
		public Tab()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

#if KRT_license
			demoMode = !LicenseManager.IsValid (this.GetType(), this, out license) || (license == null) || (license.LicenseKey != "Run");
#endif

			requestedPanel = -1 ;

			panels = new PanelCollection() ;

			colorHdlr	= new EventHandler(OnChildColorChanged) ;
			propertyHdlr= new PropertyChangedEventHandler(OnChildPropertyChanged) ;
//			renamedHdlr	= new PanelInfo.PanelRenamedEventHandler(OnPanelRenamed) ;
			addedHdlr	= new PanelCollection.PanelChangedEventHandler(OnPanelAdded) ;
			removedHdlr	= new PanelCollection.PanelChangedEventHandler(OnPanelRemoved) ;
            srcHdlr = new Channels.ValueChangedEventHandler(OnValueChanged);
#if Compact
            compactSrcHdlr = new Compact2.KineticaRT.Compact.Channels.ValueChangedEventHandler(OnCompactValueChanged);
#endif 
			panels.PanelAdded		+= addedHdlr ;
			panels.PanelRemoved		+= removedHdlr ;
			panels.PropertyChanged	+= propertyHdlr ;
//			panels.PanelRenamed		+= renamedHdlr ;

			headerBar		= Rectangle.Empty ;
			textFormat		= new StringFormat() ;
			textFormat.LineAlignment	= StringAlignment.Center ;
			textFormat.Alignment		= StringAlignment.Center ;
			needScrollBtns	= false ;
			scrollOffset	= 0 ;
			headerRight		= 0 ;
			leftScrollBtn	= new ScrollButton(System.Windows.Forms.ScrollButton.Left) ;
			rightScrollBtn	= new ScrollButton(System.Windows.Forms.ScrollButton.Right) ;

			scrollTimer			= new Timer() ;
			scrollTimer.Interval= 600 ;
			scrollTimer.Tick	+= new EventHandler(OnScrollTimer) ;

			currentPanelIndex = -1 ;

			//default is to not have any panels
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
#if KRT_license
			if (license != null) {license.Dispose();license = null;}
#endif

			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		#endregion (initialise)

		#region ISupportInitialize
		/// <summary>
		/// Initialization is complete
		/// </summary>
		public override void EndInit()
		{
			base.EndInit() ;

			if (!this.DesignMode)
			{
//				LoadAllPanels(false) ;
				LoadAllPanels(true) ;
			}

			if (requestedPanel >= 0)
			{
				SetCurrentTab(requestedPanel) ;
			}
			else
			{
				SetCurrentTab(0) ;
			}
		}
		#endregion (isupportinitialise)

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Tab
			// 
			this.Name = "Tab";
		}
		#endregion

		#region Properties
        private bool compact;
        /// <summary>
        /// Set to true before creating any panels if the Tab is to be used on the Compact Framework.
        /// </summary>
        [Category("Tab")]
        [Description("Set true before creating any panels if the Tab is to be used on the Compact Framework.")]
        [DefaultValue(false)]
        public bool Compact
        {
            get
            {
                return compact;
            }
            set
            {
                if (compact != value)
                {
                    compact = value;
                    OnPropertyChanged("Compact");
                }
            }
        }

        private string source;
        /// <summary>
        /// Gets or sets the name of the channel that controls which tab is displayed.
        /// </summary>
        [Category("Tab")]
        [Description("Name of the channel that controls which tab is displayed.")]
        [DefaultValue("")]
        public string Source
        {
            get
            {
                return source; ;
            }
            set 
            {
                if (source != value)
                {
                    source = value ;
                    ConnectSource();
                    OnPropertyChanged("Source");
                }
            }
        }

		/// <summary>
		/// Gets or sets a Collection of PanelInfo objects
		/// </summary>
		[Category("Tab")]
		[Description("Panels Collection")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public PanelCollection Panels
		{
			get
			{
				return panels ;
			}
			set
			{
				if (!panels.Equals(value))
				{
					if (panels != null)
					{
						panels.PanelAdded		-= addedHdlr ;
						panels.PanelRemoved		-= removedHdlr ;
						panels.PropertyChanged	-= propertyHdlr ;
//						panels.PanelRenamed		-= renamedHdlr ;
					}

					//remove all old tabs
					//##this assumes no other controls in collection
					Controls.Clear() ;

					panels = value ;

					SuspendLayout() ;
					
					panels.PanelAdded		+= addedHdlr ;
					panels.PanelRemoved		+= removedHdlr ;
					panels.PropertyChanged	+= propertyHdlr ;
//					panels.PanelRenamed		+= renamedHdlr ;

					foreach (PanelInfo info in panels)
					{
						OnPanelAdded(info) ;//, info.Name) ;
					}
					currentPanelIndex = -1 ;
					SetCurrentTab(0) ;

					ResumeLayout() ;

					OnPropertyChanged("Panels") ;
				}
			}
		}

		/// <summary>
		/// Gets or sets the zero-based index of the current panel
		/// </summary>
		/// <remarks>'-1' if no panels exist</remarks>
		[Category("Tab")]
		[Description("Gets or sets the zero-based index of the current panel")]
		[DefaultValue(-1)]
		public int CurrentPanelIndex
		{
			get
			{
				return currentPanelIndex ;
			}
			set
			{
				if (value != currentPanelIndex)
				{
					if (!initialising)
					{
						SetCurrentTab(value) ;
					}
					else
					{
						requestedPanel = value ;
					}
				}
			}
		}
		#endregion (properties)

		#region Public Methods
		/// <summary>
		/// Returns an object for which to show the properties
		/// </summary>
		/// <returns>Object to show the properties of</returns>
		public override object GetBasicPropertiesObject()
		{
            if (currentPanelIndex >= panels.Count) currentPanelIndex = panels.Count - 1;
			return panels[currentPanelIndex] ;
		}

		/// <summary>
		/// Saves the current Panel to the relevant named dll. Does nothing if no dll name, or no current panel.
		/// </summary>
		public override void SavePanel()
		{
			if (currentPanelIndex >= 0 && currentPanelIndex < panels.Count)
			{
				PanelInfo			info		= panels[currentPanelIndex] ;

				if (info.DllName.Length > 0 && info.Panel != null)
				{
                    try
                    {
                        info.Panel.Save(System.IO.Path.Combine(SystemDirectory, info.DllName), compact);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());//ft
                    }
				}
			}
		}

		/// <summary>
		/// Returns whether the Current Panel can be saved.
		/// </summary>
		public override bool CanSave()
		{
			if (currentPanelIndex >= 0 && currentPanelIndex < panels.Count &&
				panels[currentPanelIndex].DllName.Length > 0 &&
				panels[currentPanelIndex].Panel != null					)
			{
				return true ;
			}
			return false ;
		}
		
		/// <summary>
		/// Finds the first panel with matching text and sets that as the current page
		/// </summary>
		/// <param name="Text">Header text to search for</param>
		public void SetCurrentPanelByText(string Text)
		{
			int			index		= 0 ;

			foreach (PanelInfo info in panels)
			{
				if (info.Text == Text)
				{
					//found it!
					SetCurrentTab(index) ;
					break ;
				}
				index++ ;
			}
		}
		#endregion (public methods)

		#region Drawing
        protected override void OnPaintBackground(PaintEventArgs e)//ft ajout test
        {
            base.OnPaintBackground(e);
            //AddToQueue(true);
            Repaint(true, e.Graphics);
        }
        private Object lockObj = new Object(); //ft oblige de remettre un lock pour synchroniser les thread du Repaint vs l<evenement OnPaintBackground 
		/// <summary>
		/// Method that does all the drawing.
		/// Called from OnPaint and the Control's event pool Update
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
            lock (lockObj)//lock(this) //ft ajout 
            {
                g.SmoothingMode = SmoothingMode.None;

                if (FullRedraw)
                {
                    CalculateDisplayRectangle();
                    //				g.FillRectangle(backBrush, ClientRectangle);

                    DrawBorder(g);
                    needFullRedraw = false;
                }

                //offset title (if enabled)
                if (TitleEnabled && panels.Count > 0)
                {
                    //if (resized || needCalcTitle)
                    {
                        Recalculate(g);
                        //needCalcTitle = false ;
                        resized = false;
                    }
                }

                //##only need to call this if display rectangle has changed?
                if (FullRedraw)
                {
                    if (!this.InvokeRequired)
                    {
                        PerformLayout();
                    }
                    else
                    {
                        InvokePerformLayout();
                    }
                }

                if (shape == Shapes.RoundedRectangle)
                {
                    g.Clip = new Region(ClientRectangle);
                    Rectangle title_rect;
                    title_rect = ClientRectangle;
                    int border_width;
                    if (borderType == BorderType.Flat || borderType == BorderType.Shaded || borderType == BorderType.Rounded || borderType == BorderType.RoundedWithInset || borderType == BorderType.ShadedWithInset)
                    {
                        border_width = BorderWidthPixels(ClientRectangle);
                    }
                    else
                    {
                        border_width = 0;
                    }
                    if (borderType == BorderType.Raised || borderType == BorderType.Indented || borderType == BorderType.EditBox || borderType == BorderType.RoundedWithInset || borderType == BorderType.ShadedWithInset)
                    {
                        border_width += 2;
                    }
                    title_rect.Inflate(-border_width, -border_width);

                    GraphicsPath innerpath = CalculateShapePath(title_rect, shape);
                    //g.FillPath(System.Drawing.Brushes.DarkGoldenrod, innerpath) ;
                    //g.Clip.Intersect(new Region(innerpath)) ;
                    Region innerregion = new Region(innerpath);
                    innerregion.Intersect(g.Clip);
                    g.Clip = innerregion;
                    innerpath.Dispose();
                }

                DrawTitle(g);

#if KRT_license
			if (demoMode)
			{
				Rectangle			demo_rect		= displayRect ;
				
				if (TitleEnabled && panels.Count > 0)
				{
					demo_rect.Y		= headerBar.Y ;
					demo_rect.Height= headerBar.Height ;
				}
				else
				{
					demo_rect.Y		= demo_rect.X ;
					demo_rect.Height= Font.Height + 4 ;
				}
				DrawDemoTextColor(g, demo_rect, Color.DarkOrchid) ;
			}
#endif
            }
		}

		/// <summary>
		/// Recalculates the DisplayRectangle based on the current ClientRectangle
		/// and Appearance.
		/// </summary>
		protected override void CalculateDisplayRectangle()
		{
			base.CalculateDisplayRectangle() ;

			if (TitleEnabled && panels.Count > 0)
			{
				Rectangle		rect		= displayRect ;
				
				//ignore header outlining
				rect.Inflate(-2, -2) ;

				//ignore header bar
				rect.Height -= Font.Height + 4 ;
				rect.Y += Font.Height + 4 ;

				displayRect = rect ;
			}
#if KRT_license
			else if (demoMode)
			{
				Rectangle		rect		= displayRect ;

				rect.Y += Font.Height + 4 ;
				rect.Height -= Font.Height + 4 ;
				displayRect	= rect ;
			}
#endif
		}

		private void Recalculate(Graphics g)
		{
			int			index			= 0 ;
			Rectangle	rect ;						//individual tab
			SizeF		size ;
			Rectangle	panel_rect ;

			//include 2 pixel outline that DisplayRectangle ignores
			rect = DisplayRectangle ;
			headerBar = new Rectangle(rect.X - 2, rect.Y, rect.Width + 4, Font.Height + 6) ;
			headerBar.Y -= headerBar.Height ;

			rect = headerBar ;

			foreach (PanelInfo info in panels)
			{
				if (index > 0)		rect.X	= rect.Right ;

				//calculate the width needed
				size = g.MeasureString(info.Text, Font) ;
				size.Width = size.Width + 8 ;		// 2 for each border and 2 each side as space
				
				rect.Width = (int)size.Width ;
				panels[index].rect = rect ;
				index++ ;
			}

			//do we need scroll buttons?
			panel_rect = panels[panels.Count - 1].rect ;
			if (panel_rect.Right > headerBar.Right)
			{
				needScrollBtns = true ;

				rightScrollBtn.rect.Height	= headerBar.Height - 2 ;
				rightScrollBtn.rect.Width	= rightScrollBtn.rect.Height ;
				rightScrollBtn.rect.Y		= headerBar.Y ;
				rightScrollBtn.rect.X		= headerBar.Right - rightScrollBtn.rect.Width ;

				leftScrollBtn.rect			= rightScrollBtn.rect ;
				leftScrollBtn.rect.X		-= leftScrollBtn.rect.Width ;

				headerRight = headerBar.Right - (2 * (rightScrollBtn.rect.Width)) ;
			}
			else
			{
				needScrollBtns = false ;
				headerRight = headerBar.Right ;
				scrollOffset = 0 ;
			}
		}

		/// <summary>
		/// Draws the tab headings
		/// </summary>
		/// <param name="g">Graphics object with which to draw</param>
		protected override void DrawTitle(Graphics g)
		{
			if (!TitleEnabled)			return ;

			Rectangle		tabs				= headerBar ;
			Region			old_clip			= g.Clip ;
			int				index				= 0 ;
			Color			back_color ;
			Rectangle		rect ;
			Rectangle		temp_rect ;
			SolidBrush		back_brush			= new SolidBrush(Color.Black) ;
			SolidBrush		fore_brush			= new SolidBrush(Color.Black) ;
			SolidBrush		top_brush			= new SolidBrush(Color.Black) ;
			SolidBrush		bottom_brush		= new SolidBrush(Color.Black) ;
			Color			current_color		= BackColor ;			//current tab's back color
			Rectangle		current_rect		= Rectangle.Empty ;		//current tab header rect

			tabs.Width = headerRight - headerBar.Left ;
		//	g.Clip = new Region(tabs) ;

			//ensure background of header bar is drawn
			g.FillRectangle(backColorBrush, headerBar) ;

			foreach (PanelInfo info in panels)
			{
				//if (info.Panel != null)
				//{
				//	back_color		= info.Panel.BackColor ;
				//	fore_brush.Color= info.Panel.ForeColor ;
				//}
				//else
				//{
                back_color = this.BackColor;
				fore_brush.Color = this.ForeColor ;
				//}
				back_brush.Color = back_color ;

				rect = info.rect ;
				rect.X += scrollOffset ;

                //if (rect.X < headerRight && rect.Right > headerBar.X)
                //{
                //    back_brush.Color = SystemColors.Desktop;
                //}

				if (index == currentPanelIndex)
				{
                    //back_brush.Color = ControlPaint.Light(back_color);
                    //fore_brush.Color = ControlPaint.Dark(ForeColor);
                    back_brush.Color = info.Panel.BackColor;
                    fore_brush.Color = info.Panel.ForeColor;

                    current_color = back_brush.Color;
                    current_rect = rect;
                }

				if (rect.Left < headerRight && rect.Right > headerBar.Left)
				{
                    if (index == currentPanelIndex)
                    {
                        g.FillRectangle(back_brush, rect);
                    }
                    else
                    {
                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                        new Point(rect.Left, rect.Top),
                        new Point(rect.Left, rect.Bottom * 2),
                        back_brush.Color,// this.BackColor;
                        ControlPaint.Dark(back_brush.Color));  // Opaque blue

                        g.FillRectangle(linGrBrush, rect);
                    }




					temp_rect = rect ;
                    top_brush.Color = ControlPaint.Light(back_brush.Color);
                    bottom_brush.Color = ControlPaint.Dark(back_brush.Color);

					//draw raising
					temp_rect.Height = 2 ;
					g.FillRectangle(top_brush, temp_rect) ;

					temp_rect = rect ;
					temp_rect.Width = 2 ;
					g.FillRectangle(top_brush, temp_rect) ;

					temp_rect.X = rect.Right - 2 ;
					g.FillRectangle(bottom_brush, temp_rect) ;

					//...and corner pixel
					g.FillRectangle(top_brush, rect.Right - 2, rect.Top, 1, 1) ;

					//text
                    g.DrawString(info.Text, this.Font, fore_brush, rect, textFormat);
				}
				//else off edge, dont bother to draw

				index++ ;
			}

			//frame the current tab
		//	g.Clip = old_clip ;

			rect = DisplayRectangle ;
			rect.Inflate(2, 2) ;

            // Base Line
            //DrawSquareBorder(g, rect, BorderType.Raised, current_color, 2);
            DrawSquareBorder(g, rect, BorderType.Raised, back_brush.Color, 2);

			//clear the part under the relevant heading, if it is visible
			if (current_rect.X < headerRight && current_rect.Right > headerBar.X)
			{
				rect.X		= (current_rect.X < headerBar.X) ? headerBar.X + 2 : current_rect.X /* + 2*/ ;
				rect.Width	= (current_rect.Right > headerRight) ? headerRight - rect.X : current_rect.Right - rect.X - 2 ;
				rect.Height = 2 ;

				//back_brush.Color = current_color ;
                back_brush.Color = current_color; // ControlPaint.Light(current_color);
                                
                // Active Base Line
                g.FillRectangle(back_brush, rect);
				if (rect.Right < headerRight)
				{
                    back_brush.Color = ControlPaint.Dark(current_color);
                    g.FillRectangle(back_brush, rect.Right, rect.Bottom - 2, 1, 1);
				}
			}
			// else it isn't visible

			//scroll buttons
			DrawScrollButtons(g) ;

			back_brush.Dispose() ;
			fore_brush.Dispose() ;
			top_brush.Dispose() ;
			bottom_brush.Dispose() ;
		}

		private void DrawScrollButtons(Graphics g)
		{
			if (TitleEnabled && needScrollBtns)
			{
				Rectangle			rect		= panels[panels.Count-1].rect ;
				leftScrollBtn.Draw(g, (scrollOffset < 0)) ;
				rightScrollBtn.Draw(g, ((scrollOffset + rect.Right) > headerRight)) ;
			}
		}

		#endregion (drawing)

		#region Other Methods
        private int nextTab;
        private void SetCurrentTab()
        {
            SetCurrentTab(nextTab);
        }

        private void SetCurrentTab(int index)
		{
            if (this.InvokeRequired)
            {
                nextTab = index;
                this.Invoke(new MethodInvoker(SetCurrentTab));
                return;
            }
			if (index < 0 || index >= panels.Count)			return ;

			if (index != currentPanelIndex)
			{
				//hide old current tab (if any)
				if (currentPanelIndex >= 0 && currentPanelIndex < panels.Count)
				{
					PanelBase		old_panel	= panels[currentPanelIndex].Panel ;//FindPanel(panels[currentPanel].Name) ;
                    if (old_panel != null)      old_panel.Hide() ;
				}

				//show new current tab
				currentPanelIndex = index ;
				//panels[currentPanel].PanelX.Show() ;
				//...and ensure frame is dealing with the correct one
				//NewPanel(panels[currentPanel].PanelX, false) ;
				PanelBase		new_panel		= panels[currentPanelIndex].Panel ;//FindPanel(panels[currentPanel].Name) ;

				if (new_panel != null)
				{
					NewPanel(new_panel, false) ;
					new_panel.Show() ;
				}

				OnPropertyChanged("CurrentPanel") ;

				AddToQueue(false) ;
			}
		}

//		private PanelBase FindPanel(string PanelName)
//		{
//			PanelBase		ret_val		= null ;
//
//			foreach (System.Windows.Forms.Control ctrl in Controls)
//			{
//				if (ctrl.Name == PanelName && ctrl is PanelBase)
//				{
//					ret_val = ctrl as PanelBase ;
//					break ;
//				}
//			}
//
//			return ret_val ;
//		}

//		private System.Windows.Forms.Control FindObject(string Name)
//		{
//			System.Windows.Forms.Control		ret_val		= null ;
//
//			foreach (System.Windows.Forms.Control ctrl in Controls)
//			{
//				if (ctrl.Name == Name)
//				{
//					ret_val = ctrl ;
//					break ;
//				}
//			}
//
//			return ret_val ;
//		}

		/// <summary>
		/// Loads the current panel using the current PanelInfo
		/// </summary>
		/// <returns>True if succeeded and got a new Panel</returns>
		private bool LoadPanel()
		{
			if (currentPanelIndex >= 0 && currentPanelIndex < panels.Count)
			{
                PanelBase new_panel = LoadPanelFromName(panels[currentPanelIndex].DllName, compact);

				if (new_panel != null)
				{
//					//ensure name matches expected name
//					new_panel.Name = panels[currentPanel].Name ;

					//replace current
					NewPanel(new_panel, true) ;
					new_panel.Show() ;

					panels[currentPanelIndex].Panel = new_panel ;
					
					needCalcTitle = true ;
					AddToQueue(false) ;

					return true ;
				}
			}
			return false ;
		}

		// Loads the panel specified by the given PanelInfo
		private bool LoadPanel(PanelInfo Info)
		{
			if (Info != null)
			{
				if (currentPanelIndex >= 0 && currentPanelIndex < panels.Count && Info.Equals(panels[currentPanelIndex]))
				{
					//we are loading the current panel
					return LoadPanel() ;
				}
				else		//load a hidden panel
				{
                    PanelBase new_panel = LoadPanelFromName(Info.DllName, compact);

					if (new_panel != null)
					{
//						//ensure name matches expected name
//						new_panel.Name = Info.Name ;

						//replace existing panel
						ReplacePanel(Info.Panel, new_panel) ;//FindPanel(Info.Name), new_panel) ;
						Info.Panel = new_panel ;

						//hide the new panel (not current)
						new_panel.Hide() ;

						needCalcTitle = true ;
						AddToQueue(false) ;

						return true ;
					}
				}
			}
			return false ;
		}

		/// Loads all the panels, including or excluding the current one
		private void LoadAllPanels(bool IncludeCurrent)
		{
			int				index		= 0 ;

			foreach (PanelInfo info in panels)
			{
				if (index != currentPanelIndex)
				{
                    PanelBase new_panel = LoadPanelFromName(info.DllName, compact);

					if (new_panel != null)
					{
//						//ensure name matches expected name
//						new_panel.Name = info.Name ;
						//replace the old panel
						ReplacePanel(info.Panel, new_panel) ;//FindPanel(info.Name), new_panel) ;
						info.Panel = new_panel ;
						//and hide the new one (it is not the current tab)
						new_panel.Hide() ;
					}
				}
				index++ ;
			}

			if (IncludeCurrent)			LoadPanel() ;
		
			needCalcTitle = true ;
			AddToQueue(false) ;
		}


		// Its time to scroll!
        private new void Scroll()
		{
			int		 new_offset		= scrollOffset ;

			if (leftScrollBtn.held)
			{
				//cannot scroll past the start
				new_offset += SCROLL_AMOUNT ;
				if (new_offset > 0)			new_offset = 0 ;
			}
			else if (rightScrollBtn.held)
			{
				Rectangle		panel_rect		= panels[panels.Count-1].rect ;
				//don't scroll further than need to
				if ( (scrollOffset + panel_rect.Right) > headerRight)
				{
					new_offset -= SCROLL_AMOUNT ;
				}
			}

			if (new_offset != scrollOffset)
			{
				scrollOffset = new_offset ;
				AddToQueue(false) ;
			}
		}

		#endregion (other methods)

		#region Mouse Events
		/// <summary>
		/// Mouse Down Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && TitleEnabled)
			{
				//have we clicked in a scroll button?
				if (needScrollBtns && (leftScrollBtn.rect.Contains(e.X, e.Y) || rightScrollBtn.rect.Contains(e.X, e.Y)) )
				{
					Capture = true ;
					if (leftScrollBtn.rect.Contains(e.X, e.Y))
					{
						leftScrollBtn.held	= true ;
						leftScrollBtn.state = ButtonState.Pushed ;
						Scroll() ;

						scrollTimer.Start() ;
					}
					else
					{
						rightScrollBtn.held	= true ;
						rightScrollBtn.state= ButtonState.Pushed ;
						Scroll() ;

						scrollTimer.Start() ;
					}
				}
				else
				{
					int			index		= 0 ;
					Rectangle	rect ;

					//have we clicked on a tab?
					foreach (PanelInfo info in panels)
					{
						rect = info.rect ;
						rect.X += scrollOffset ;

						if (rect.Contains(e.X, e.Y))
						{
							if (index != currentPanelIndex)
							{
								SetCurrentTab(index) ;
							}
							break ;
						}
						index++ ;
					}
				}
			}

			base.OnMouseDown(e) ;
		}

		/// <summary>
		/// Mouse Up Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			Capture = false ;

			if (leftScrollBtn.held)
			{
				scrollTimer.Stop() ;

				leftScrollBtn.held	= false ;
				
				if (leftScrollBtn.state != ButtonState.Normal)
				{
					leftScrollBtn.state = ButtonState.Normal ;
					AddToQueue(false) ;
				}
			}

			if (rightScrollBtn.held)
			{
				scrollTimer.Stop() ;
				
				rightScrollBtn.held = false ;
				if (rightScrollBtn.state != ButtonState.Normal)
				{
					rightScrollBtn.state= ButtonState.Normal ;
					AddToQueue(false) ;
				}
			}

			base.OnMouseUp(e) ;
		}

		/// <summary>
		/// Mouse Move Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			bool		changed			= false ;

			if (leftScrollBtn.held)
			{
				if (leftScrollBtn.rect.Contains(e.X, e.Y))
				{
					if (leftScrollBtn.state != ButtonState.Pushed)
					{
						leftScrollBtn.state = ButtonState.Pushed ;
						scrollTimer.Start() ;
						changed = true ;
					}
				}
				else if (leftScrollBtn.state != ButtonState.Normal)
				{
					leftScrollBtn.state = ButtonState.Normal ;
					scrollTimer.Stop() ;
					changed = true ;
				}
			}
			else if (rightScrollBtn.held)
			{
				if (rightScrollBtn.rect.Contains(e.X, e.Y))
				{
					if (rightScrollBtn.state != ButtonState.Pushed)
					{
						rightScrollBtn.state = ButtonState.Pushed ;
						scrollTimer.Start() ;
						changed = true ;
					}
				}
				else if (rightScrollBtn.state != ButtonState.Normal)
				{
					rightScrollBtn.state = ButtonState.Normal ;
					scrollTimer.Stop() ;
					changed = true ;
				}
			}

			if (changed)			AddToQueue(false) ;

			base.OnMouseMove(e) ;
		}
		#endregion (mouse events)

		#region Other Events
		//time to scroll!
		private void OnScrollTimer(object sender, EventArgs e)
		{
			if (leftScrollBtn.held || rightScrollBtn.held)
			{
				Scroll() ;
			}
			else		//no longer holding button - stop timer
			{
				scrollTimer.Stop() ;
			}
		}

		private void OnPanelAdded (object sender)//, string PanelName)
		{
			if (((PanelInfo)sender).Panel == null)
//			if (FindPanel(PanelName) == null)
			{
//				PanelBase		panel		= new PanelBase() ;
//
//				panel.Name = PanelName ;
//				Controls.Add(panel) ;
//				panel.Hide() ;
//				panel.Dock = DockStyle.Fill ;

				PanelBase		panel = ((PanelInfo)sender).Panel ;

				if (panel == null)
				{
//					panel = new PanelBase(((PanelInfo)sender).Name) ;
					panel = new PanelBase(this.Container) ;
					((PanelInfo)sender).Panel = panel ;
				}
				
				ReplacePanel(null, panel) ;
				panel.Hide() ;
			}

			if (currentPanelIndex < 0)		SetCurrentTab(0) ;

			//##make new additions current?
		}

		private void OnPanelRemoved (object sender)//, string PanelName)
		{
			PanelBase		panel = ((PanelInfo)sender).Panel ;//FindPanel(PanelName) ;

			if (panel != null)
			{
				Controls.Remove(panel) ;
			}

			//##ensure still got valid current page?
		}

//		private void OnPanelRenamed(object sender, string OldName, string NewName)
//		{
//			//find the panel with the old name, and rename it to the new name
//			PanelBase		panel	= FindPanel(OldName) ;
//
//			if (panel != null)
//			{
//				panel.Name = NewName ;
//			}
//		}

		/// <summary>
		/// ControlAdded event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnControlAdded(ControlEventArgs e)
		{
			e.Control.BackColorChanged += colorHdlr ;
			e.Control.ForeColorChanged += colorHdlr ;

			base.OnControlAdded(e) ;
		}

		/// <summary>
		/// ControlRemoved event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			e.Control.BackColorChanged -= colorHdlr ;
			e.Control.ForeColorChanged -= colorHdlr ;

			base.OnControlRemoved(e) ;
		}

		private void OnChildColorChanged(object sender, EventArgs e)
		{
			//force a redraw
			AddToQueue(false) ;
		}

		/// <summary>
		/// Handles PropertyChanged events from sub-objects
		/// </summary>
		/// <param name="sender">Object that generated the event</param>
		/// <param name="e">Event Data</param>
		protected override void OnChildPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			if (sender is PanelInfo && e.PropertyName == "Panel")
			{
//				//replace the old panel
//				PanelBase		panel	= FindPanel(((PanelInfo)sender).Name) ;
//				Controls.Remove(panel) ;
				Controls.Add(((PanelInfo)sender).Panel) ;
			}

			if (sender is PanelInfo && e.PropertyName == "DllName")
			{
				if (!this.DesignMode && !initialising)
				{
					LoadPanel(sender as PanelInfo) ;
				}
			}
			else
			{
				//assume all properties except value merit a full redraw
				if (e.PropertyName == "Value")
				{
					AddToQueue(false) ;
				}
				else
				{
					AddToQueue(true) ;
				}
			}
		}
		#endregion

		#region Connection
        /// <summary>
        /// Connects this object to others.
        /// </summary>
        public override void Connect()
        {
            panels.Connect();
        }

        /// <summary>
        /// Connects this object to others, stage 2.
        /// </summary>
        /// <remarks>This will be called after all objects have recieved a Connect call.</remarks>
        public override void Connect2()
        {
            ConnectSource();
            panels.Connect2();
        }

        private void OnValueChanged(Channels.IChannel sender, KineticaRT.Values.Value v)
        {
            SetCurrentTab(System.Convert.ToInt32(sourceChannel.Value.ToDouble()));
        }
#if Compact
        private void OnCompactValueChanged(Compact2.KineticaRT.Compact.Channels.IChannel sender, Compact2.KineticaRT.Compact.Values.Value v)
        {
            SetCurrentTab(System.Convert.ToInt32(compactSourceChannel.Value.ToDouble()));
        }
#endif
        private Channels.ValueChangedEventHandler srcHdlr;
        private Channels.IChannel sourceChannel;
#if Compact
        private Compact2.KineticaRT.Compact.Channels.ValueChangedEventHandler compactSrcHdlr;
        private Compact2.KineticaRT.Compact.Channels.IChannel compactSourceChannel;
#endif

        private void ConnectSource()
        {
    		//free old handler if any
			if (sourceChannel != null)		
			{
				sourceChannel.ValueChanged		-= srcHdlr ;
			}
#if Compact
            else if (compactSourceChannel != null)
            {
                compactSourceChannel.ValueChanged -= compactSrcHdlr;
            }
#endif

			//attach new sourceChannel
            object source_channel =  SearchObject(source) ;
            sourceChannel = source_channel as KineticaRT.Channels.IChannel;
            if (sourceChannel != null)
            {
                sourceChannel.ValueChanged += srcHdlr;

                try
                {
                    SetCurrentTab(System.Convert.ToInt32(sourceChannel.Value.ToDouble()));
                }
                catch (System.FormatException)
                {
                }
            }
#if Compact
            else
            {
                compactSourceChannel = source_channel as Compact2.KineticaRT.Compact.Channels.IChannel;
                if (compactSourceChannel != null)
                {
                    compactSourceChannel.ValueChanged += compactSrcHdlr;

                    try
                    {
                        SetCurrentTab(System.Convert.ToInt32(compactSourceChannel.Value.ToDouble()));
                    }
                    catch (System.FormatException)
                    {
                    }
                    catch (System.OverflowException)
                    {
                    }
                }
            }
#endif
        }

        /// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			panels.Disconnect() ;
		}
		#endregion (connection)

		#region Object Search
		/// <summary>
		/// Returns the named object or null if it does not exist.
		/// </summary>
		/// <param name="objectName">Name of object to find</param>
		/// <returns>Named object, or null if not found</returns>
		public override object FindObject (string objectName)
		{
			return panels.FindObject (objectName) ;
		}
		#endregion
	}
}

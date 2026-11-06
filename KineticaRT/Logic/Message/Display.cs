using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using KineticaRT.Controls;

namespace KineticaRT.Logic.Message
{
	/// <summary>
	/// List of messages from a source.
	/// </summary>
	[DefaultProperty("Value")]
	[ToolboxBitmap(typeof(Display))]
	public class Display : KineticaRT.Controls.Control, IComparer
	{
		#region Variables
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader dateColumn;
		private System.Windows.Forms.ColumnHeader timeColumn;
		private System.Windows.Forms.ColumnHeader sourceColumn;
		private System.Windows.Forms.ColumnHeader priorityColumn;
		private System.Windows.Forms.ColumnHeader messageColumn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		//properties
		private Channels.Channel		sourceChannel ;
		private string					source ;
		private Message					value ;
		private int						maxMessages ;

		private bool					gridlines ;
		private bool					headings ;
		private string[]				sourceFilter ;
        private KineticaRT.Logic.Message.PriorityCollection priorities;
		private bool					useSourceFormat ;

		//internal
		private	bool					demoMode ;
		private int						sortColumn ;
		private bool					sortReversed ;
		private bool					prtyChanged ;
		private int						demoMsgCount ;
		private ListViewItem			demoItem ;
		private bool					connected = false ;
		
		private Channels.ValueChangedEventHandler	srcHandler ;
		private PropertyChangedEventHandler			srcPropertyHandler ;
		private PropertyChangedEventHandler			propertyHandler ;
		private ObjectSearchEventHandler			searchHandler ;
		#endregion

		#region Initialisation and disposal
		/// <summary>
		/// Initialises a new instance of the Display class
		/// </summary>
		public Display()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			demoMode		= false ;

			sourceChannel	= null ;
			source			= "" ;
			value			= null ;
			maxMessages		= 100 ;
			gridlines		= true ;
			headings		= true ;
			sourceFilter	= null ;
			priorities		= new PriorityCollection() ;
			useSourceFormat	= true ;
            //AllowDrop		= true ; SB removed because it is overriding the user values

			srcHandler			= new Channels.ValueChangedEventHandler(OnNewMessage) ;
			srcPropertyHandler	= new PropertyChangedEventHandler(OnSourcePropertyChanged) ;
			propertyHandler		= new PropertyChangedEventHandler(OnPriorityChanged) ;
			searchHandler		= new ObjectSearchEventHandler(OnObjectSearch) ;

			priorities.PropertyChanged += propertyHandler ;
			priorities.ObjectSearch += searchHandler ;

			listView.ListViewItemSorter = this ;
			sortColumn = -1 ;
			sortReversed = false ;
			
			demoMsgCount = 0 ;
			demoItem = new ListViewItem("-") ;
			demoItem.SubItems.AddRange(new string[] {"-", "-", "-", "Demo"}) ;

			prtyChanged = false ;

			listView.ColumnClick += new ColumnClickEventHandler(OnColumnClicked) ;
			listView.MouseHover += new EventHandler(OnMouseHover) ;
            listView.ColumnWidthChanged += new ColumnWidthChangedEventHandler(OnColumnWidthChanged);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
                disposed = true;
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}
        private bool disposed = false;
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listView = new System.Windows.Forms.ListView();
			this.dateColumn = new System.Windows.Forms.ColumnHeader();
			this.timeColumn = new System.Windows.Forms.ColumnHeader();
			this.sourceColumn = new System.Windows.Forms.ColumnHeader();
			this.priorityColumn = new System.Windows.Forms.ColumnHeader();
			this.messageColumn = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listView
			// 
			this.listView.AllowColumnReorder = true;
			this.listView.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.listView.BackColor = System.Drawing.SystemColors.Control;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.dateColumn,
																					   this.timeColumn,
																					   this.sourceColumn,
																					   this.priorityColumn,
																					   this.messageColumn});
			this.listView.ForeColor = System.Drawing.SystemColors.ControlText;
			this.listView.FullRowSelect = true;
			this.listView.GridLines = true;
			this.listView.LabelWrap = false;
			this.listView.Location = new System.Drawing.Point(2, 8);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(340, 136);
			this.listView.TabIndex = 0;
			this.listView.View = System.Windows.Forms.View.Details;
			// 
			// dateColumn
			// 
			this.dateColumn.Text = "Date";
            this.dateColumn.Width = 75;
			// 
			// timeColumn
			// 
			this.timeColumn.Text = "Time";
            this.timeColumn.Width = 75;
			// 
			// sourceColumn
			// 
			this.sourceColumn.Text = "Source";
            this.sourceColumn.Width = 75;
			// 
			// priorityColumn
			// 
			this.priorityColumn.Text = "Priority";
            this.priorityColumn.Width = 75;
			// 
			// messageColumn
			// 
			this.messageColumn.Text = "Message";
            this.messageColumn.Width = 100;
			// 
			// Display
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.listView});
			this.Name = "Display";
			this.Size = new System.Drawing.Size(344, 150);
			this.ResumeLayout(false);

		}
		#endregion

		#region Properties
		/// <summary>
		/// Current Message
		/// </summary>
		[Category("Display")]
		[DefaultValue(null)]
		[Description("Current Message")]
		[Browsable (false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Message Value
		{
			get
			{
				return this.value ;
			}
			set
			{
                if (!IsHandleCreated && !disposed)
				{
                    if (InvokeRequired) return; // can't create the control if on the wrong thread.

					CreateControl () ;
					if (!IsHandleCreated) return ;
				}

				if (this.value != value)
				{
					bool		update			= false ;

					if (value != null)
					{
						//add the message if no source filter, or source matches
						if (SourceIsValid(value.Source))
						{
                            string prty_text = "All";
                            Color back_clr = Color.Empty;
                            Color text_clr = Color.Empty;

                            if (!value.TextColor.IsEmpty)
                            {
                                text_clr = value.TextColor;
                                back_clr = Color.Black;
                            }
                            else if ((priorities != null && priorities.Count > 0))
                            {
                                if (!priorities.GetPriorityInfo(value.Priority, out back_clr, out text_clr, out prty_text))
                                {
                                    return;
                                }
                            }
								
							ListViewItem		item ;

							//create the item...
							item = new ListViewItem(value.Timestamp.ToShortDateString()) ;

							if (back_clr != Color.Empty)			item.BackColor = back_clr ;
							if (text_clr != Color.Empty)			item.ForeColor = text_clr ;

							item.Tag = value.Priority ;
							item.SubItems.AddRange(new string[] {	value.Timestamp.ToLongTimeString(),
																	value.Source,
																	prty_text,
																	value.Text}) ;
									
							BeginInvoke (new InsertItemHandler (InsertItem), new object [] {item}) ;
							//InsertItem(item) ;

							if (demoMode)
							{
								demoMsgCount++ ;

								if (demoMsgCount >= 10)
								{
									BeginInvoke (new InsertItemHandler (InsertItem), new object [] {demoItem.Clone()}) ;
									//InsertItem((ListViewItem)demoItem.Clone()) ;
									demoMsgCount = 0 ;
								}
							}

                            update = true ;
						}
						//else unwanted source
					}
					else		//value is null
					{
						update = true ;
					}

					if (update)
					{
						//actually set the value - it has been checked
						this.value = value ;
						//OnValueChanged() ;
						OnPropertyChanged("Value") ;
					}
				}
			}
		}
		private bool ShouldSerializeValue()
		{
			return false ;
		}

		/// <summary>
		/// Source of messages
		/// </summary>
		[Category("Display")]
		[Description("Source of messages")]
		public KineticaRT.Channels.Channel SourceChannel
		{
			get
			{
				return sourceChannel ;
			}
			set
			{
				if (sourceChannel != value)
				{
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		-= srcHandler ;
						sourceChannel.PropertyChanged	-= srcPropertyHandler ;
					}

					sourceChannel = value ;
					if (sourceChannel != null)
					{
						sourceChannel.ValueChanged		+= srcHandler ;
						sourceChannel.PropertyChanged	+= srcPropertyHandler ;
						//NS Only display new values. OnNewMessage(sourceChannel, sourceChannel.Value) ;

						SetFormat() ;
					}
					OnPropertyChanged("SourceChannel") ;
				}
			}
		}
		private bool ShouldSerializeSourceChannel()
		{
			return false ;
		}

		/// <summary>
		/// Name of object to be the Source
		/// </summary>
		[Category("Display")]
		[DefaultValue("")]
		[Description("Name of object to be the Source")]
		public string Source
		{
			get
			{
				return source ;
			}
			set
			{
				if (source != value)
				{
					source = value ;
					OnPropertyChanged("Source") ;
				}
				//try and connect to source
				if (connected && source.Length > 0)
				{
					SourceChannel = OnObjectSearch(source) as KineticaRT.Channels.Channel ;
				}
			}
		}

		/// <summary>
		/// Maximum number of messages held in the list before old ones are replaced
		/// </summary>
		[Category("Display")]
		[DefaultValue(1000)]
		[Description("Maximum number of messages held in the list before old ones are replaced")]
		public int MaximumMessages
		{
			get
			{
				return maxMessages ;
			}
			set
			{
				if (maxMessages != value)
				{
					maxMessages = value ;
					OnPropertyChanged("MaximumMessages") ;
				}
			}
		}

		/// <summary>
		/// Gridlines enabled
		/// </summary>
		[Category("Display")]
		[DefaultValue(true)]
		[Description("Gridlines enabled")]
		public bool Gridlines
		{
			get
			{
				return gridlines ;
			}
			set
			{
				if (gridlines != value)
				{
					gridlines = value ;
					listView.GridLines = gridlines ;
					OnPropertyChanged("Gridlines") ;
				}
			}
		}

		/// <summary>
		/// Column headings enabled
		/// </summary>
		[Category("Display")]
		[DefaultValue(true)]
		[Description("Column headings enabled")]
		public bool Headings
		{
			get
			{
				return headings ;
			}
			set
			{
				if (headings != value)
				{
					headings = value ;
					if (!headings)
					{
						listView.HeaderStyle = ColumnHeaderStyle.None ;
					}
					else
					{
						listView.HeaderStyle = ColumnHeaderStyle.Clickable ;
					}
					OnPropertyChanged("Headings") ;
				}
			}
		}

		/// <summary>
		/// Array of valid Source strings. All Sources are accepted if this property is null
		/// </summary>
		[Category("Display")]
		[DefaultValue(null)]
		[Description("Array of valid Source strings. All Sources are accepted if this property is null")]
		public string[] SourceFilter
		{
			get
			{
				return sourceFilter ;
			}
			set
			{
				if (sourceFilter != value)
				{
					sourceFilter = value ;
					OnPropertyChanged("SourceFilter") ;
				}
			}
		}

		/// <summary>
		/// Tool Tip Text that appears when hovering over the control
		/// </summary>
		[Category("Control")]
		[DefaultValue("")]
		[Description("Tool Tip Text that appears when hovering over the control")]
		public override string ToolTipText
		{
			set
			{
				base.ToolTipText = value ;
				base.SetToolTip(listView) ;
			}
		}

		/// <summary>
		/// Border Style of the Control
		/// </summary>
		[Category("Control")]
        [DefaultValue(BorderType.Raised)]
		[Description("Border Style of the Control")]
        public override BorderType BorderType
		{
            get //ft
            {
                return base.BorderType; //ft
            }
            set
			{
                base.BorderType = value;
                CalculateDisplayRectangle();
                ResizeListView();
			}
		}

		/// <summary>
		/// Border width of the Control
		/// </summary>
		[Category("Control")]
		[DefaultValue(0.1)]
		[Description("Border width of the Control")]
		public override double BorderWidth
		{
            get //ft
            {
                return base.BorderWidth;//ft
            }
			set
			{
				base.BorderWidth = value ;
                CalculateDisplayRectangle();
                ResizeListView();
            }
		}
        /// <summary>
        /// Date column width of the Control
        /// </summary>
        [Category("Column")]
        [DefaultValue(75)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Date column width of the Control")]
        public int DateColumnWidth
        {
            get //ft
            {
                return this.dateColumn.Width;//ft
            }
            set//ft
            {
                if (value != this.dateColumn.Width)
                {
                    this.dateColumn.Width = value;
                    AdjustMessageColumn();
                    OnPropertyChanged("DateColumnWith");
                }
            }
        }
        /// <summary>
        /// Date column width of the Control
        /// </summary>
        [Category("Column")]
        [DefaultValue(75)]
        [Description("Time column width of the Control")]
        [RefreshProperties(RefreshProperties.All)]
        public int TimeColumnWidth
        {
            get //ft
            {
                return this.timeColumn.Width;//ft
            }
            set//ft
            {
                if (value != this.timeColumn.Width)
                {
                    this.timeColumn.Width = value;
                    AdjustMessageColumn();
                    OnPropertyChanged("TimeColumnWidth");
                }
            }
        }
        /// <summary>
        /// Date column width of the Control
        /// </summary>
        [Category("Column")]
        [DefaultValue(75)]
        [Description("Source column width of the Control")]
        [RefreshProperties(RefreshProperties.All)]
        public int SourceColumnWidth
        {
            get //ft
            {
                return this.sourceColumn.Width;//ft
            }
            set//ft
            {
                if (value != this.sourceColumn.Width)
                {
                    this.sourceColumn.Width = value;
                    AdjustMessageColumn();
                    OnPropertyChanged("SourceColumnWidth");
                }
            }
        }
        /// <summary>
        /// Message column width of the Control
        /// </summary>
        [Category("Column")]
        [DefaultValue(100)]
        [Description("Message column width of the Control")]
        [RefreshProperties(RefreshProperties.All)]
        public int MessageColumnWidth//ft
        {
            get //ft
            {
                return this.messageColumn.Width;//ft
            }
            set//ft
            {
                if (value != this.messageColumn.Width)
                {
                    this.messageColumn.Width = value;
                    OnPropertyChanged("MessageColumnWidth");
                }
            }
        }

        /// <summary>
        /// Priority column width of the Control
        /// </summary>
        [Category("Column")]
        [DefaultValue(75)]
        [Description("Priority column width of the Control")]
        [RefreshProperties(RefreshProperties.All)]
        public int PriorityColumnWidth//ft
        {
            get //ft
            {
                return this.priorityColumn.Width;//ft
            }
            set//ft
            {
                if (value != this.priorityColumn.Width)
                {
                    this.priorityColumn.Width = value;
                    AdjustMessageColumn();
                    OnPropertyChanged("PriorityColumnWidth");
                }
            }
        }

		/// <summary>
		/// Definition of priority colors and text
		/// </summary>
		[Category("Display")]
		//[DefaultValue(null)]
		[Description("Definition of priority colors and text")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public PriorityCollection Priorities
		{
			get
			{
				return priorities ;
			}
			set
			{
				if (priorities != null)
				{
					priorities.PropertyChanged -= propertyHandler ;
					priorities.ObjectSearch -= searchHandler ;
				}

				priorities = value ;
				if (priorities != null)
				{
					priorities.PropertyChanged += propertyHandler ;
					priorities.ObjectSearch += searchHandler ;
				}
				OnPropertyChanged("Priorities") ;
			}
		}
		private bool ShouldSerializePriorities()
		{
			return !priorities.IsDefaultState() ;
		}

		/// <summary>
		/// The Shape of the control (Rectangle, Rounded Rectangle, Ellipse)
		/// </summary>
		[Category("Control")]
		[DefaultValue(Shapes.Rectangle)]
		[Description("The Shape of the control (Rectangle, Rounded Rectangle, Ellipse)")]
		public override Shapes Shape
		{
			get { return base.Shape ; }
			set
			{
				base.Shape = value ;
                CalculateDisplayRectangle();
                ResizeListView();
            }
		}

		/// <summary>
		/// Corner radius (pixels) of the rounded rectangular shape
		/// </summary>
		[Category("Control")]
		[DefaultValue(20)]
		[Description("Corner radius (pixels) of the rounded rectangular shape")]
		public override int CornerRadius
		{
			get { return base.CornerRadius ; }
			set
			{
				base.CornerRadius = value ;
                CalculateDisplayRectangle();
                ResizeListView();
            }
		}

		/// <summary>
		/// If true, the Display gets its ToolTip from its Source channel
		/// </summary>
		[Category("Display")]
		[DefaultValue(true)]
		[Description("If true, the Display gets its ToolTip from its Source channel")]
		public bool UseSourceFormat
		{
			get
			{
				return useSourceFormat ;
			}
			set
			{
				if (useSourceFormat != value)
				{
					useSourceFormat = value ;
					if (useSourceFormat)
					{
						SetFormat() ;
					}
					OnPropertyChanged("UseSourceFormat") ;
				}
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clears all messages out of the Display
		/// </summary>
		public void Clear()
		{
			listView.Items.Clear() ;
		}
		#endregion

		#region Drawing
		/// <summary>
		/// Method that does all the drawing.
		/// </summary>
		/// <param name="FullRedraw">True if everything needs to be redrawn; false if only parts need redrawing</param>
		/// <param name="g">Graphics objects with which the drawing is done</param>
		public override void Repaint(bool FullRedraw, Graphics g)
		{
            //lock (this) //ft ajout 
            {
                CalculateDisplayRectangle();

                base.Repaint(FullRedraw, g);

                if (prtyChanged)
                {
                    UpdateListItems();
                    prtyChanged = false;
                }
            }
		}

        private void ResizeListView()
        {
                //resize list view
                listView.Location = new Point(DisplayRectangle.X, DisplayRectangle.Y);
                listView.Size = new Size(DisplayRectangle.Width, DisplayRectangle.Height);
                SetListViewRegion();

                AdjustMessageColumn();
        }
		#endregion

		#region Event Handling
		/// <summary>
		/// BackColorChanged Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnBackColorChanged(EventArgs e)
		{
			listView.BackColor = BackColor ;
			base.OnBackColorChanged(e) ;
		}

		/// <summary>
		/// ForeColorChanged Event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnForeColorChanged(EventArgs e)
		{
			listView.ForeColor = ForeColor ;
			base.OnForeColorChanged(e) ;
		}

		//clicked on a column header: do sorting
		private void OnColumnClicked (object Sender, ColumnClickEventArgs e)
		{
			if (sortColumn == e.Column)
			{
				sortReversed = !sortReversed ;
			}
			else
			{
				sortColumn = e.Column ;
				sortReversed = false ;
			}

			listView.Sort() ;
		}

        private void OnColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            ListView lvSender = (ListView)sender;
            if (lvSender.Columns[e.ColumnIndex] != this.messageColumn)
            {
                AdjustMessageColumn();
            }
        }

		private void OnMouseHover(object Sender, EventArgs e)
		{
		}
		#endregion

		#region Drag / Drop operations
		/// <summary>
		/// DragEnter event
		/// </summary>
		/// <param name="e">Event Data</param>
		protected override void OnDragEnter (DragEventArgs e)
		{
			//check if allowed to drop things, and if drag object holds text
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}

			base.OnDragEnter(e) ;
		}

		/// <summary>
		/// DragDrop event
		/// </summary>
		/// <param name="e">Event data</param>
		protected override void OnDragDrop (DragEventArgs e)
		{
			if (AllowDrop && e.Data.GetDataPresent(DataFormats.Text)) 
			{
				Source = e.Data.GetData(DataFormats.Text).ToString() ;
			}

			base.OnDragDrop(e) ;
		}
		#endregion

        /// <summary>
        /// Resize Event
        /// </summary>
        /// <param name="e">Event Data</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CalculateDisplayRectangle();
            ResizeListView();
        }


		#region Other methods

        private void AdjustMessageColumn()
        {
            int width;

            width = listView.Width - dateColumn.Width - timeColumn.Width
                        - sourceColumn.Width - priorityColumn.Width;
            //ensure message column is at least 100 pixels wide
            MessageColumnWidth = Math.Max(width, 100);
        }

		private delegate void InsertItemHandler(ListViewItem Item) ;

		private void InsertItem(ListViewItem Item)
		{
			ListViewItem		bottom_item ;


			if (!listView.IsHandleCreated)
			{
				try 
				{
					listView.CreateControl () ;
				}
				catch (System.ObjectDisposedException)
				{
				}

				if (!listView.IsHandleCreated) return ;
			}

			//...and insert it
			listView.BeginUpdate() ;

			if (listView.Items.Count > maxMessages - 1)
			{
				//remove oldest one (bottom of list)
				listView.Items.RemoveAt(listView.Items.Count - 1) ;
			}

			if (listView.TopItem != null && listView.TopItem.Index > 0)
			{
				int		y	= listView.Height ;
				//want to keep it at the top, even after adding the new item
				do
				{
					bottom_item = listView.GetItemAt(10, y) ;
					y-- ;
				}
				while (y >= 0 && bottom_item == null) ;

				if (bottom_item != null)
				{
					Rectangle		item_rect	= listView.GetItemRect(bottom_item.Index) ;

					if (item_rect.Bottom > (y + 2))
					{
						//bottom item isn't fully visible - pick the one before it
						//(we know there is one as the top item isn't the first)
						bottom_item = listView.Items[bottom_item.Index - 1] ;
					}
				}
			}
			else
			{
				bottom_item = null ;
			}

			//add message to start of list
			listView.Items.Insert(0, Item) ;

			if (bottom_item != null)
			{
				bottom_item.EnsureVisible() ;
			}

			listView.EndUpdate() ;
		}

		private void UpdateListItems()
		{
			listView.BeginUpdate() ;

			//go through all current messages and update text and colors
			foreach (ListViewItem item in listView.Items)
			{
				Color		back_clr ;
				Color		text_clr ;
				string		text ;
                PriorityCode priority;

				//we set the tag, so we know it is a long
				priority = (PriorityCode)System.Convert.ToInt32(item.Tag) ;

				priorities.GetPriorityInfo(priority, out back_clr, out text_clr, out text) ;
				if (back_clr != Color.Empty)
				{
					item.BackColor = back_clr ;
				}
				if (text_clr != Color.Empty)
				{
					item.ForeColor = text_clr ;
				}
				item.SubItems[4].Text = text ;
			}

			listView.EndUpdate() ;
		}

		// Sets the list view controls window to the relevant size and shape
		private void SetListViewRegion()
		{
			if (shape == Shapes.Rectangle)
			{
				listView.Region = null ;
			}
			else
			{
				Rectangle		rect	= new Rectangle(0, 0, DisplayRectangle.Width, DisplayRectangle.Height) ;
				GraphicsPath	path	= CalculateShapePath(rect, shape) ;

				listView.Region = new Region(path) ;
				path.Dispose() ;
			}
		}

		/// <summary>
		/// Sets up the tool tip based on the Source channel
		/// </summary>
		private void SetFormat()
		{
			if (useSourceFormat)
			{
				KineticaRT.Channels.InfoChannel		ch ;

				ch = sourceChannel as KineticaRT.Channels.InfoChannel ;
				if (ch != null)
				{
					ToolTipText = (ch.ToolTip != null) ? ch.ToolTip : "" ;
				}
			}
		}

		//returns true if no source filters, or string matches one of the filters
		private bool SourceIsValid(string Source)
		{
			if (sourceFilter == null || sourceFilter.Length == 0)		return true ;

			foreach (string src in sourceFilter)
			{
				if (src == Source)
				{
					return true ;
				}
			}

			//match not found
			return false ;
		}
		#endregion

		#region IComparer interface
		/// <summary>
		/// Compares two objects and returns a value indicating whether one is less than,
		/// equal to or greater than the other
		/// </summary>
		/// <param name="x">First object to compare</param>
		/// <param name="y">Second object to compare</param>
		/// <returns>
		/// Less than zero if x is less than y.
		/// Zero if x equals y.
		/// Greater han zero if x is greater than y.
		/// </returns>
		public int Compare (object x, object y)
		{
			ListViewItem		item_x	= (ListViewItem)x ;
			ListViewItem		item_y	= (ListViewItem)y ;

			switch (sortColumn)
			{
				case 0:		//date
				{
					//(temporarily?) retrieve date and time from strings
					DateTime			date_x	= DateTime.Parse(item_x.SubItems[0].Text) ;
					DateTime			date_y	= DateTime.Parse(item_y.SubItems[0].Text) ;

					if (!sortReversed)
					{
						return date_x.CompareTo(date_y) ;
					}
					else
					{
						return date_y.CompareTo(date_x) ;
					}
				}
				
				case 1:		//time
				{
					//(temporarily?) retrieve date and time from strings
					DateTime			time_x	= DateTime.Parse(item_x.SubItems[1].Text) ;
					DateTime			time_y	= DateTime.Parse(item_y.SubItems[1].Text) ;

					if (!sortReversed)
					{
						return time_x.CompareTo(time_y) ;
					}
					else
					{
						return time_y.CompareTo(time_x) ;
					}
				}

				case 2:		//source
				case 3:		//priority
				case 4:		//message
				{
					//comparison is strictly on strings
					if (!sortReversed)
					{
						return item_x.SubItems[sortColumn].Text.CompareTo(item_y.SubItems[sortColumn].Text) ;
					}
					else
					{
						return item_y.SubItems[sortColumn].Text.CompareTo(item_x.SubItems[sortColumn].Text) ;
					}
				}
			}

			return 0 ;
		}
		#endregion

		#region New Value / property changed handling (in and out)
		//new value coming in
		private void OnNewMessage(KineticaRT.Channels.IChannel Sender, KineticaRT.Values.Value v)
		{
			if (v == null || v is Message)
			{
				Value = (Message)v ;
			}
			else
			{
				//not a message - create one
				Value = new Message(v.ToString(), "?", 0) ;
			}
		}

		//property of source has changed
		private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "ToolTip")
			{
				SetFormat() ;
			}
		}

		//priority has changed
		private void OnPriorityChanged(object sender, PropertyChangedEventArgs e)
		{
			prtyChanged = true ;
			AddToQueue(false) ;
		}
		#endregion

		#region Connection
		/// <summary>
		/// Connects this object to others.
		/// </summary>
		public override void Connect ()
		{
			if (!connected)
			{
				if (source.Length > 0)
				{
					SourceChannel = OnObjectSearch(source) as KineticaRT.Channels.Channel ;
					if (SourceChannel != null)
					{
						connected = true ;
					}
				}
				//connected = true ;
			}

			if (priorities != null)		priorities.Connect() ;
		}

		/// <summary>
		/// Disconnects this object from others.
		/// </summary>
		public override void Disconnect ()
		{
			if (connected)
			{
				if (source.Length > 0)
				{
					SourceChannel = null ;
				}
				connected = false ;
			}

			if (priorities != null)		priorities.Disconnect() ;
		}
		#endregion
	}
}

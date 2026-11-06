/*
 * Created by SharpDevelop.
 * User: dannyl
 * Date: 6/2/2009
 * Time: 6:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using KT = KineticaRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using System.Runtime.CompilerServices;
using CEL.Data.BusinessObjects;
using ZedGraph;

namespace CEL.KineticaRT.Controls
{
    /// <summary>
    /// Description of ZedGraphControl.
    /// </summary>
    public class ZedGraphControl : System.Windows.Forms.UserControl, KT.IConnectable
    {
        private CEL.HMI.CELDAS CELProject;
        private global::ZedGraph.ZedGraphControl zedGraphControlBase;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button addAxisButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Button deletePreset;
        private System.Windows.Forms.Button savePreset;
        private System.Windows.Forms.Button newPreset;

        private System.Windows.Forms.ComboBox presetsComboBox;
        private System.Timers.Timer displayTimer;

        private System.Windows.Forms.Label presetLabel;
        private System.Windows.Forms.PropertyGrid properGrid;
        private System.Windows.Forms.Form properGridForm;
        private ToolStripMenuItem deleteAxisMenu;
        private ToolStripMenuItem propertiesMenu;
        private ErrorProvider errorProvider;
        private ZedGraph.IValuesToolTip tooltip;

        #region Fields

        private int tickStart = 0;
        private System.Threading.Timer timer;
        private IContainer components;
        private Tuple<int, object> graphObjectSelected;
        private GraphState state = GraphState.CanEdit;
        private bool isPresetLoading = false;
        private bool isGraphLoaded = false;
        private EngineGraphPresetCollection presetColl;
        private CheckBox chkHideYAxis;
        private ToolTip pointTooltip;
        private System.Windows.Forms.Label lblZoom;
        private CheckBox chkZoomYAxis;
        private CheckBox chkZoomXAxis;

        public event EventHandler Shown;
        private bool wasShown = false;

        #endregion

        #region Constructors

        private enum GraphState
        {
            CannotEdit,
            CanEdit,
        }
        
        public ZedGraphControl()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            // https://stackoverflow.com/questions/1166226/detecting-design-mode-from-a-controls-constructor
            bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);//ft
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);// pour supporter les background transparent
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);// pour supporter les background transparent
            this.UpdateStyles();
            
            this.tooltip = ValuesToolTip.Create(this.zedGraphControlBase, this.pointTooltip);

            ZedGraph.GraphPane graphPane;
            properGridForm = new System.Windows.Forms.Form();
            properGridForm.AutoSize = true;
            properGridForm.Text = "Properties";
            properGridForm.Dock = DockStyle.Fill;
            properGridForm.StartPosition = FormStartPosition.CenterScreen;

            properGrid = new System.Windows.Forms.PropertyGrid();
            properGrid.Dock = DockStyle.Fill;
            properGridForm.Controls.Add(properGrid);

            propertiesMenu = new ToolStripMenuItem("Properties");
            propertiesMenu.MouseUp += OnPropertiesMenu_MouseUp;
            deleteAxisMenu = new ToolStripMenuItem("Delete Axis");
            deleteAxisMenu.MouseUp += OnDeleteAxisMenu_MouseUp;

            errorProvider = new ErrorProvider();
            errorProvider.SetIconAlignment(this.savePreset, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(this.savePreset, 2);
            errorProvider.BlinkRate = 1000;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            base.AllowDrop = false;
            graphPane = this.zedGraphControlBase.GraphPane;
            graphPane.IsFontsScaled = false;
            graphPane.Title.Text = "No preset found!";

            this.presetsComboBox.DisplayMember = EngineGraphPresetMetadata.PropertyNames.PresetName;
            this.presetsComboBox.ValueMember = EngineGraphPresetMetadata.PropertyNames.Id;
            this.presetsComboBox.DataSource = presetColl;
            this.presetsComboBox.DropDown += new EventHandler(this.OnCbxPresetsDropDown);

            this.zedGraphControlBase.PrintDocument.DefaultPageSettings.Landscape = true;
            this.zedGraphControlBase.IsShowContextMenu = false;
            this.zedGraphControlBase.IsEnableSelection = true;
            this.zedGraphControlBase.IsAntiAlias = true;
            this.zedGraphControlBase.IsEnableHZoom = true;
            this.zedGraphControlBase.IsEnableVZoom = false;
            this.zedGraphControlBase.IsEnableVPan = true;
            this.zedGraphControlBase.IsEnableHPan = true;

            /// Work around do here <see cref="OnZedGraphMouseMove(ZedGraph.ZedGraphControl, MouseEventArgs)"/>
            // Temporary deactivation of this property because it's throwing exception when we mouseover at the end of a full rollingpoint list
            // The control want to show the tooltip of a point from the curve
            // But it's not working well when the list start rolling (it's start when the list is full around 400sec)
            // The exception is index out of range
            //this.zedGraphControlBase.IsShowPointValues = true;
            
            this.zedGraphControlBase.ZoomEvent += new ZedGraph.ZedGraphControl.ZoomEventHandler(this.OnZedGraphControlZoom);
            this.zedGraphControlBase.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.OnZedGraphContextMenuIsBuilding);
            this.zedGraphControlBase.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.OnZedGraphMouseDown);
            this.zedGraphControlBase.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.OnZedGraphMouseMove);

            this.zedGraphControlBase.SaveFileDialog.AddExtension = true;
            this.zedGraphControlBase.SaveFileDialog.DefaultExt = "jpg";
            this.zedGraphControlBase.SaveFileDialog.OverwritePrompt = true;
            this.zedGraphControlBase.SaveFileDialog.RestoreDirectory = true;
            
            this.zedGraphControlBase.AxisChange();
            this.zedGraphControlBase.Invalidate();
            
            this.chkHideYAxis.CheckedChanged += new EventHandler(OnHideYAxis_ChkHideYAxis);
            
            this.addAxisButton.Enabled = false;
            this.savePreset.Enabled = false;
            this.deletePreset.Enabled = false;
            this.chkHideYAxis.Enabled = false;
       
            if (!designMode)
                this.Shown += ZedGraphControl_Shown;

            //this.SetEngineModel("NO ENGINE MODEL");

            ZedGraph.ColorSymbolRotator.StaticInstance.NextColorIndex = 0;
        }
        
        #endregion

        #region Events

        /// <summary>
        /// This event must fire Shown event once
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (!wasShown)
            {
                wasShown = true;
                if (Shown != null)
                {
                    Shown(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Override background painting to not notify the parent on invalidate
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //DO NOT UNCOMMENT THE FOLLOWING LINE OTHERWISE IT WILL INDUCE FLICKERING ON PARENT CONTROL 
            //base.OnPaintBackground(e);
        }

        /// <summary>
        /// When this event is fire we expect that CELProject.EngineModel has been already set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZedGraphControl_Shown(object sender, EventArgs e)
        {
            CELProject = CEL.HMI.CELDAS.Instance;

            // EngineModel is set by the private value instead of the property to bypass the property setter
            this.EngineModel = CELProject.EngineModel;

            if (this.EngineModel != string.Empty && this.presetColl == null)
            {
                this.InitializeGraph();
            }

            isGraphLoaded = true;
        }

        private void OnCbxPresetsDropDown(object sender, EventArgs e)
        {
            try
            {
                if (this.EngineModel == string.Empty || this.presetColl == null) return;
                this.isPresetLoading = true;
                bool isLoaded = false;

                // Populate preset associate to ProjectId/EngineModel
                var loadedPresets = new EngineGraphPresetCollection();
                loadedPresets.Query.Where(loadedPresets.Query.EngineModel == this.EngineModel);
                loadedPresets.Query.Load();

                // Add handler on each item and child item to track changes
                if (loadedPresets.HasData)
                {
                    // Check if someone add a new preset
                    foreach (EngineGraphPreset loadedPresetItem in loadedPresets)
                    {
                        bool presetAlreadyExist = false;
                        GraphAxisCollection graphColl = loadedPresetItem.GraphAxisCollectionByGid;
                        graphColl.Filter = graphColl.AsQueryable().OrderBy(ax => ax.AxisTypeID).ThenBy(ax => ax.AxisIndex);
                        
                        foreach (EngineGraphPreset presetItem in this.presetsComboBox.Items)
                        {
                            if (presetItem.Id == loadedPresetItem.Id) presetAlreadyExist = true;
                        }

                        if (!presetAlreadyExist)
                        {
                            AddPresetHandlers(loadedPresetItem);
                            isLoaded = true;

                            this.presetColl.Add(loadedPresetItem);
                        }
                    }

                    // Remove removed presets (can be remove by all instances or test sessions of the same engine model)
                    var presetCountBeforeCleanup = this.presetColl.Count;
                    for (var presetIndex = 0; presetIndex < this.presetColl.Count; ++presetIndex)
                    {
                        EngineGraphPreset presetItem = this.presetColl[presetIndex];

                        bool presetStillExist = false;
                        foreach (EngineGraphPreset loadedPresetItem in loadedPresets)
                        {
                            if (presetItem.RowState == EntitySpaces.Interfaces.esDataRowState.Added || presetItem.Id == loadedPresetItem.Id) presetStillExist = true;
                        }

                        if (!presetStillExist)
                        {
                            presetItem.MarkAsDeleted();
                            RemovePresetHandlers(presetItem);
                        }
                    }

                    if (presetCountBeforeCleanup != this.presetColl.Count)
                    {
                        // Force event to let the graph updating with the new context 
                        //this.OnPresetsComboBoxSelectedIndexChanged(this.presetsComboBox, new System.EventArgs());
                        UpdateGraph();
                    }
                }

                if (isLoaded) UpdateGraph();

                this.isPresetLoading = false;
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnHideYAxis_ChkHideYAxis(object sender, EventArgs e)
        {
            try
            {
                if (this.zedGraphControlBase.GraphPane.YAxis.Title.Text == "Y Axis" ||
                    this.zedGraphControlBase.GraphPane.Y2Axis.Title.Text == string.Empty) return;

                var isAxisVisible = !((System.Windows.Forms.CheckBox)sender).Checked;

                foreach (ZedGraph.YAxis yaxis in this.zedGraphControlBase.GraphPane.YAxisList)
                {
                    if(!(this.zedGraphControlBase.GraphPane.YAxis.Title.Text == "Y Axis"))
                        yaxis.IsVisible = isAxisVisible;
                }

                foreach (ZedGraph.Y2Axis y2axis in this.zedGraphControlBase.GraphPane.Y2AxisList)
                {
                    if (!(this.zedGraphControlBase.GraphPane.Y2Axis.Title.Text == string.Empty))
                        y2axis.IsVisible = isAxisVisible;
                }

                this.addAxisButton.Enabled = isAxisVisible && state == GraphState.CanEdit && !this.displayTimer.Enabled;

                this.zedGraphControlBase.AxisChange();
                this.zedGraphControlBase.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public event KT.ObjectSearchEventHandler ObjectSearch;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { }
            remove { }
        }

        private void OnAddAxisButtonClick(object sender, System.EventArgs e)
        {
            try
            {
                if (this.zedGraphControlBase.GraphPane.YAxisList.Count == 1 & this.zedGraphControlBase.GraphPane.YAxis.Title.Text == "Y Axis")
                {
                    ZedGraph.YAxis newAxis = this.zedGraphControlBase.GraphPane.YAxis;
                    newAxis.Title.Text = "[Drag a display here]";
                    newAxis.Color = ZedGraph.ColorSymbolRotator.StaticNextColor;
                    this.SetAxisProperties(newAxis);

                    //this.zedGraphControlBase.GraphPane.YAxisList.Add(newAxis);
                    this.zedGraphControlBase.GraphPane.YAxis.MajorTic.IsOpposite = false;
                    this.zedGraphControlBase.GraphPane.YAxis.MinorTic.IsOpposite = false;
                }
                else if (this.zedGraphControlBase.GraphPane.Y2AxisList.Count == 1 &
                    (this.zedGraphControlBase.GraphPane.Y2Axis.Title.Text == string.Empty || this.zedGraphControlBase.GraphPane.Y2Axis.Title.Text == "[Drag a display here]"))
                {
                    ZedGraph.Y2Axis newAxis = this.zedGraphControlBase.GraphPane.Y2Axis;
                    newAxis.Title.Text = "[Drag a display here]";
                    newAxis.Color = ZedGraph.ColorSymbolRotator.StaticNextColor;
                    this.SetAxisProperties(newAxis);

                    //this.zedGraphControlBase.GraphPane.Y2AxisList.Add(newAxis);
                    this.zedGraphControlBase.GraphPane.Y2Axis.MajorTic.IsOpposite = false;
                    this.zedGraphControlBase.GraphPane.Y2Axis.MinorTic.IsOpposite = false;
                }
                else if (this.zedGraphControlBase.GraphPane.YAxisList.Count == this.zedGraphControlBase.GraphPane.Y2AxisList.Count ||
                         this.zedGraphControlBase.GraphPane.YAxisList.Count < this.zedGraphControlBase.GraphPane.Y2AxisList.Count)
                {
                    ZedGraph.YAxis newAxis = new ZedGraph.YAxis();
                    newAxis.Title.Text = "[Drag a display here]";
                    newAxis.Color = ZedGraph.ColorSymbolRotator.StaticNextColor;
                    this.SetAxisProperties(newAxis);

                    this.zedGraphControlBase.GraphPane.YAxisList.Add(newAxis);
                }
                else
                {
                    ZedGraph.Y2Axis newAxis;

                    newAxis = new ZedGraph.Y2Axis();
                    newAxis.Title.Text = "[Drag a display here]";
                    newAxis.Color = ZedGraph.ColorSymbolRotator.StaticNextColor;
                    this.SetAxisProperties(newAxis);

                    this.zedGraphControlBase.GraphPane.Y2AxisList.Add(newAxis);
                }

                this.zedGraphControlBase.AxisChange();
                this.zedGraphControlBase.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnRecordButtonClick(object sender, System.EventArgs e)
        {
            base.AllowDrop = false;

            this.addAxisButton.Enabled = false;
            this.presetsComboBox.Enabled = false;
            this.recordButton.Enabled = false;
            this.stopButton.Enabled = true;
            this.newPreset.Enabled = false;
            this.savePreset.Enabled = false;
            this.deletePreset.Enabled = false;

            //this.zedGraphControlBase.IsEnableZoom = false; //test
            this.zedGraphControlBase.IsShowContextMenu = false;
            this.zedGraphControlBase.IsShowHScrollBar = false;
            this.zedGraphControlBase.IsShowVScrollBar = false;

            this.zedGraphControlBase.ZoomOutAll(this.zedGraphControlBase.GraphPane);

            foreach (ZedGraph.LineItem curve in this.zedGraphControlBase.GraphPane.CurveList)
            {
                ZedGraph.RollingPointPairList list = new ZedGraph.RollingPointPairList(6000, true);
                curve.Points = list;
            }

            this.tickStart = Environment.TickCount;

            if (this.zedGraphControlBase.GraphPane.XAxis.Tag == null)
            {
                this.SetXAxisTrendDefaultScale();
                this.timer = new System.Threading.Timer(new TimerCallback(TimerTickTrend), null, 0, 50);
            }
            else
            {
                //this.SetXAxisXYDefaultScale();
                this.timer = new System.Threading.Timer(new TimerCallback(TimerTickXY), null, 0, 50);
            }
            this.displayTimer.Enabled = true;
        }

        private void OnStopButtonClick(object sender, System.EventArgs e)
        {
            this.StopRecording();
            this.state = GraphState.CannotEdit;
        }

        private void OnClearButtonClick(object sender, System.EventArgs e)
        {
            try
            {
                foreach (ZedGraph.CurveItem curve in this.zedGraphControlBase.GraphPane.CurveList)
                {
                    curve.Clear();
                }
                base.AllowDrop = true;
                this.addAxisButton.Enabled = !this.chkHideYAxis.Checked;
                this.clearButton.Enabled = false;
                this.presetsComboBox.Enabled = true;
                this.newPreset.Enabled = true;
                this.deletePreset.Enabled = true;

                if (this.errorProvider.GetError(savePreset) == string.Empty)
                {
                    this.savePreset.Enabled = false;
                }
                else
                {
                    this.savePreset.Enabled = true;
                }

                //this.zedGraphControlBase.IsEnableZoom = false; // test
                this.zedGraphControlBase.IsShowContextMenu = true;
                this.zedGraphControlBase.IsEnableSelection = true;
                this.zedGraphControlBase.IsShowHScrollBar = false;
                this.zedGraphControlBase.IsShowVScrollBar = false;

                this.state = GraphState.CanEdit;
                this.zedGraphControlBase.ZoomOutAll(this.zedGraphControlBase.GraphPane);
                this.zedGraphControlBase.AxisChange();
                this.zedGraphControlBase.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void OnNewPreset_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.EngineModel == "" || this.presetColl == null)
                {
                    MessageBox.Show("Set the engine model first!", "Warning", MessageBoxButtons.OK);
                    return;
                }

                bool isPresetNameNotValid = true;
                EngineGraphPreset newPreset = this.presetColl.AddNew();
                properGrid.SelectedObject = new PresetFilter(newPreset, this.zedGraphControlBase.GraphPane);
                newPreset.EngineModel = this.EngineModel;
                newPreset.PresetName = "Enter a preset name here";
                                
                do
                {
                    properGridForm.ShowDialog();

                    if ((newPreset.PresetName == null) || (newPreset.PresetName == string.Empty))
                    {
                        MessageBox.Show("Choose a preset name.", "Preset name not valid", MessageBoxButtons.OK);
                    }
                    else
                    {
                        int presetFound = 0;

                        foreach (EngineGraphPreset presetItem in this.presetColl)
                        {
                            if (string.Equals(presetItem.PresetName, newPreset.PresetName, StringComparison.OrdinalIgnoreCase))
                            {
                                presetFound++;
                            }
                        }

                        if (presetFound <= 1)
                        {
                            isPresetNameNotValid = false;
                        }
                        else
                        {
                            MessageBox.Show("Choose a preset name.", "Preset name already exist", MessageBoxButtons.OK);
                        }
                    }

                } while (isPresetNameNotValid);

                newPreset.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                this.presetsComboBox.SelectedItem = newPreset;

                this.state = GraphState.CanEdit;
                this.deletePreset.Enabled = true;
                this.addAxisButton.Enabled = true;
                this.chkHideYAxis.Enabled = true;

                base.AllowDrop = true;
            }
            catch (Exception ex)//ft ajout d'un catch parce que l'app etait kille completement
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnSavePreset_Click(object sender, EventArgs e)
        {
            EngineGraphPreset preset = null;

            try
            {
                preset = ((EngineGraphPreset)presetsComboBox.SelectedItem);
                RemovePresetHandlers(preset);
                preset.Save();
            }
            catch (Exception dataEx)
            {
                MessageBox.Show(dataEx.Message);
            }
            finally
            {
                if(preset != null)
                    AddPresetHandlers(preset);
                IsDataUnsaved();
            }
        }

        private void OnDeletePreset_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.presetsComboBox.SelectedItem != null)
                {
                    EngineGraphPreset preset = (EngineGraphPreset)this.presetsComboBox.SelectedItem;

                    RemovePresetHandlers(preset);
                    preset.MarkAsDeleted();
                    presetColl.Save();

                    // Force event to let the graph updating with the new context 
                    //this.OnPresetsComboBoxSelectedIndexChanged(this.presetsComboBox, new System.EventArgs());

                    UpdateGraph();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnPresetsComboBoxSelectedIndexChanged(object sender, System.EventArgs e)
        {
            UpdateGraph();
        }

        private void OnPresetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!this.isPresetLoading && this.presetColl.IsGraphDirty)
            {
                this.IsDataUnsaved();
            }

            this.zedGraphControlBase.AxisChange();
            this.zedGraphControlBase.Invalidate();
        }

        private void OnPropertiesMenu_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                EngineGraphPreset preset = ((EngineGraphPreset)this.presetsComboBox.SelectedItem);
                if (graphObjectSelected == null) return;

                EngineGraphPreset selectedPreset = null;
                GraphAxis selectedAxis = null;

                if (this.graphObjectSelected.Item2 is ZedGraph.GraphPane)
                {
                    selectedPreset = preset;
                }
                else if (this.graphObjectSelected.Item2 is ZedGraph.Legend)
                {
                    return;
                }
                else if (this.graphObjectSelected.Item2 is ZedGraph.XAxis)
                {
                    foreach (GraphAxis axisItem in preset.GraphAxisCollectionByGid)
                    {
                        if (axisItem.AxisTypeID == 1)
                        {
                            selectedAxis = axisItem;
                        }
                    }
                }
                else if (this.graphObjectSelected.Item2 is ZedGraph.X2Axis)
                {
                    foreach (GraphAxis axisItem in preset.GraphAxisCollectionByGid)
                    {
                        if (axisItem.AxisTypeID == 2)
                        {
                            selectedAxis = axisItem;
                        }
                    }
                }
                else if (this.graphObjectSelected.Item2 is ZedGraph.YAxis)
                {
                    foreach (GraphAxis axisItem in preset.GraphAxisCollectionByGid)
                    {
                        if (axisItem.AxisTypeID == 3 && axisItem.AxisIndex == graphObjectSelected.Item1)
                        {
                            selectedAxis = axisItem;
                        }
                    }
                }
                else if (this.graphObjectSelected.Item2 is ZedGraph.Y2Axis)
                {
                    foreach (GraphAxis axisItem in preset.GraphAxisCollectionByGid)
                    {
                        if (axisItem.AxisTypeID == 4 && axisItem.AxisIndex == graphObjectSelected.Item1)
                        {
                            selectedAxis = axisItem;
                        }
                    }
                }

                if (selectedPreset != null)
                {
                    properGrid.SelectedObject = new PresetFilter(selectedPreset, (ZedGraph.GraphPane)this.graphObjectSelected.Item2);
                }
                else
                {
                    if (selectedAxis == null)
                    {
                        MessageBox.Show("Drag a channel first!", "No channel has been set", MessageBoxButtons.OK);
                        return;
                    }
                    properGrid.SelectedObject = new AxisFilter((ZedGraph.Axis)this.graphObjectSelected.Item2, selectedAxis);
                }

                properGridForm.ShowDialog();
            }
            catch (Exception ex)//ft ajout d'un catch parce que l'app etait kille completement
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnDeleteAxisMenu_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (graphObjectSelected == null) return;

                if (this.graphObjectSelected.Item2 is ZedGraph.YAxis yAxis)
                {
                    this.zedGraphControlBase.GraphPane.YAxisList.Remove(yAxis);
                    if(yAxis.Tag != null)
                    {
                        var axisElements = (AxisElements)yAxis.Tag;

                        axisElements.AxisPreset.MarkAsDeleted();
                        foreach (var curve in axisElements.Curves)
                        {
                            this.zedGraphControlBase.GraphPane.CurveList.Remove(curve);
                        }
                    }

                    // Change axis index in preset as per a random axis has been deleted
                    for (int i = 0; i < this.zedGraphControlBase.GraphPane.YAxisList.Count; ++i)
                    {
                        if(this.zedGraphControlBase.GraphPane.YAxisList[i].Tag is AxisElements axisElements)
                        { 
                            axisElements.AxisPreset.AxisIndex = i;

                            foreach (var curve in axisElements.Curves)
                            {
                                curve.YAxisIndex = i;
                            }
                        }
                    }

                    // Prevent that GraphPane contains 0 axis.
                    if (this.zedGraphControlBase.GraphPane.YAxisList.Count == 0)
                    {
                        this.zedGraphControlBase.GraphPane.AddYAxis("[Drag a display here]");
                        SetAxisProperties(this.zedGraphControlBase.GraphPane.YAxisList[0]);
                    }
                }
                else if (this.graphObjectSelected.Item2 is ZedGraph.Y2Axis y2Axis)
                {
                    this.zedGraphControlBase.GraphPane.Y2AxisList.Remove(y2Axis);
                    if(y2Axis.Tag != null)
                    {
                        var axisElements = (AxisElements)y2Axis.Tag;

                        axisElements.AxisPreset.MarkAsDeleted();
                        foreach (var curve in ((AxisElements)y2Axis.Tag).Curves)
                        {
                            this.zedGraphControlBase.GraphPane.CurveList.Remove(curve);
                        }
                    }

                    // Change axis index in preset as per a random axis has been deleted
                    for (int i = 0; i < this.zedGraphControlBase.GraphPane.Y2AxisList.Count; ++i)
                    {
                        if (this.zedGraphControlBase.GraphPane.Y2AxisList[i].Tag is AxisElements axisElements)
                        {
                            axisElements.AxisPreset.AxisIndex = i;

                            foreach (var curve in axisElements.Curves)
                            {
                                curve.YAxisIndex = i;
                            }
                        }
                    }

                    // Prevent that GraphPane contains 0 axis.
                    if (this.zedGraphControlBase.GraphPane.Y2AxisList.Count == 0)
                    {
                        this.zedGraphControlBase.GraphPane.AddY2Axis("[Drag a display here]");
                        SetAxisProperties(this.zedGraphControlBase.GraphPane.Y2AxisList[0]);
                        this.zedGraphControlBase.GraphPane.Y2AxisList[0].IsVisible = false;
                    }
                }

                this.zedGraphControlBase.AxisChange();
                this.zedGraphControlBase.Invalidate();
            }
            catch (Exception ex)//ft ajout d'un catch parce que l'app etait kille completement
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnZedGraphControlZoom(ZedGraph.ZedGraphControl sender,
                                    ZedGraph.ZoomState oldState,
                                    ZedGraph.ZoomState newState)
        {
            if (sender.GraphPane.XAxis.Scale.MagAuto)
            {
                sender.GraphPane.XAxis.Scale.Mag = 0;
            }
            foreach (ZedGraph.YAxis axis in sender.GraphPane.YAxisList)
            {
                if (axis.Scale.MagAuto)
                {
                    axis.Scale.Mag = 0;
                }
            }
            foreach (ZedGraph.Y2Axis axis in sender.GraphPane.Y2AxisList)
            {
                if (axis.Scale.MagAuto)
                {
                    axis.Scale.Mag = 0;
                }
            }
        }

        private void OnZedGraphContextMenuIsBuilding(ZedGraph.ZedGraphControl sender,
                                                     System.Windows.Forms.ContextMenuStrip menuStrip,
                                                     System.Drawing.Point mousePt,
                                                     ZedGraph.ZedGraphControl.ContextMenuObjectState objState)
        {
            try
            {
                Graphics g = this.CreateGraphics();


                if (this.state == GraphState.CanEdit)
                {
                    menuStrip.Items.Clear();
                    menuStrip.Items.Add(deleteAxisMenu);
                    menuStrip.Items.Add(propertiesMenu);

                    deleteAxisMenu.Visible = false;
                    propertiesMenu.Visible = false;

                    deleteAxisMenu.Enabled = false;
                    propertiesMenu.Enabled = false;

                    if (this.zedGraphControlBase.MasterPane.FindNearestPaneObject(new Point(mousePt.X, mousePt.Y), g, out ZedGraph.GraphPane paneObj, out object nearestObject, out int index))
                    {

                        this.graphObjectSelected = new Tuple<int, object>(index, nearestObject);

                        if (nearestObject is ZedGraph.Axis)
                        {
                            deleteAxisMenu.Enabled = true;
                            deleteAxisMenu.Visible = true;
                        }

                        if (!(nearestObject is ZedGraph.Legend))
                        {
                            propertiesMenu.Enabled = true;
                            propertiesMenu.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            try
            {
                if (this.AllowDrop && e.Data.GetDataPresent(DataFormats.Text))
                {
                    Point clientCoord = this.PointToClient(new Point(e.X, e.Y));
                    PointF mousePosition = new PointF(clientCoord.X, clientCoord.Y);
                    Graphics g = this.CreateGraphics();

                    if (this.zedGraphControlBase.GraphPane.FindNearestObject(mousePosition, g, out object nearestObject, out int index))
                    {
                        if (nearestObject is ZedGraph.XAxis)
                        {
                            this.OnDragDropXAxis(e, (ZedGraph.Axis)nearestObject, index);
                        }
                        else if (nearestObject is ZedGraph.YAxis)
                        {
                            this.OnDragDropYAxis(e, (ZedGraph.Axis)nearestObject, index);
                        }
                        else if (nearestObject is ZedGraph.Y2Axis)
                        {
                            this.OnDragDropYAxis(e, (ZedGraph.Axis)nearestObject, index);
                        }
                    }
                }
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
            base.OnDragDrop(e);
        }

        private void OnDragDropXAxis(DragEventArgs e, ZedGraph.Axis axis, int index)
        {
            try
            {
                KT.Channels.IInfoChannel channel;

                channel = this.OnObjectSearch(e.Data.GetData(DataFormats.Text).ToString());
                if (channel != null)
                {
                    this.SetXAxisChannelSource(channel, axis, null, null);
                    this.zedGraphControlBase.AxisChange();
                    this.zedGraphControlBase.Invalidate();

                    IsDataUnsaved();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnDragDropYAxis(DragEventArgs e, ZedGraph.Axis axis, int index)
        {
            try
            {
                KT.Channels.IInfoChannel channel;

                channel = this.OnObjectSearch(e.Data.GetData(DataFormats.Text).ToString());
                if (channel != null)
                {
                    this.SetYAxisChannelSource(channel, null, null, axis, index);
                    this.zedGraphControlBase.AxisChange();
                    this.zedGraphControlBase.Invalidate();
                    this.recordButton.Enabled = true;

                    IsDataUnsaved();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            try
            {
                e.Effect = DragDropEffects.None;
                KT.Channels.IInfoChannel channel;

                channel = this.OnObjectSearch(e.Data.GetData(DataFormats.Text).ToString());
                if (channel != null)
                {
                    if (this.AllowDrop && e.Data.GetDataPresent(DataFormats.Text))
                    {
                        e.Effect = DragDropEffects.Copy;
                        base.OnDragEnter(e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            try
            {
                KT.Channels.IInfoChannel channel;
                e.Effect = DragDropEffects.None;

                channel = this.OnObjectSearch(e.Data.GetData(DataFormats.Text).ToString());
                if (channel != null)
                {
                    if (this.AllowDrop && e.Data.GetDataPresent(DataFormats.Text))
                    {
                        Point clientCoord = this.PointToClient(new Point(e.X, e.Y));
                        PointF mousePosition = new PointF(clientCoord.X, clientCoord.Y);
                        Graphics g = this.CreateGraphics();

                        if (this.zedGraphControlBase.GraphPane.FindNearestObject(mousePosition, g, out object nearestObject, out int index))
                        {
                            if (nearestObject is ZedGraph.Axis axis)
                            {
                                if (nearestObject is ZedGraph.YAxis || nearestObject is ZedGraph.Y2Axis)
                                {
                                    if (axis.Title.Text == "[Drag a display here]")
                                        e.Effect = DragDropEffects.Copy;
                                }
                                else
                                {
                                    e.Effect = DragDropEffects.Copy;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }

            if (e.Effect == DragDropEffects.Copy)
                base.OnDragEnter(e);
        }
       
        protected KT.Channels.IInfoChannel OnObjectSearch(string objectName)
        {
            if (this.ObjectSearch == null)
            {
                return null;
            }
            return (this.ObjectSearch(objectName) as KT.Channels.IInfoChannel);
        }

        void OnDisplayTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.Visible)
            {
                lock (this.zedGraphControlBase)
                {
                    this.zedGraphControlBase.AxisChange();
                    this.zedGraphControlBase.Invalidate();
                }
            }
        }

        private bool OnZedGraphMouseDown(ZedGraph.ZedGraphControl sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (!(presetColl.Count > 0)) return false;

                Object nearestObject;
                Graphics g = this.CreateGraphics();
                ZedGraph.GraphPane paneObj;

                int index;

                if (e.Button == System.Windows.Forms.MouseButtons.Right && this.state == GraphState.CanEdit)
                {
                    this.zedGraphControlBase.IsShowContextMenu = false;

                    if (this.zedGraphControlBase.MasterPane.FindNearestPaneObject(new PointF(e.X, e.Y), g, out paneObj, out nearestObject, out index))
                    {
                        if (!(nearestObject is ZedGraph.Legend))
                        {
                            this.zedGraphControlBase.IsShowContextMenu = true;
                            return true;
                        }
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && this.state == GraphState.CannotEdit)
                {
                    this.zedGraphControlBase.IsShowContextMenu = true;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (this.zedGraphControlBase.MasterPane.FindNearestPaneObject(new PointF(e.X, e.Y), g, out paneObj, out nearestObject, out index))
                    {
                        if (nearestObject is ZedGraph.Axis)
                        {
                            //DoDragDrop(nearestObject, DragDropEffects.Copy | DragDropEffects.Move);
                            //return true;
                        }
                    }
                }

            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        /// <summary>
        /// This event is a work around the catch a case that is not handle is ZedGraphControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool OnZedGraphMouseMove(ZedGraph.ZedGraphControl sender, System.Windows.Forms.MouseEventArgs e)
        {
            bool retVal = false;

            try
            {
                // IsShowPointValues is set to true from the menu item bypass this logic
                if (!this.zedGraphControlBase.IsShowPointValues)
                {
                    // Methods taken from ZedGraph source code
                    HandlePointValues(e.Location);
                }
                else
                {                    
                    this.zedGraphControlBase.IsShowPointValues = false;
                    retVal = true;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                // DO NOTHING HERE
                // The error is not critical and it's happening only when we try to display the tooltip from the start or the end of the rolling point list
            }

            return retVal;
        }

        #endregion

        #region Properties

        [Browsable(true)]
        public new bool AllowDrop
        {
            get
            {
                return base.AllowDrop;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public new ControlCollection Controls
        {
            get
            {
                return base.Controls;
            }
        }

        [Category("Control"), Browsable(true)]
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public SaveFileDialog SaveFileDialog
        {
            get { return this.zedGraphControlBase.SaveFileDialog; }
            set { this.zedGraphControlBase.SaveFileDialog = value; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(true)]
        public string EngineModel { get; private set; } = string.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool NeedUpdateAgain { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private bool IsUpdating { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Stop the recording and clear all presets
        /// </summary>
        public void Stop()
        {
            // On arrête les graphiques en cours d'enregistrement
            this.OnStopButtonClick(this, new EventArgs());
            this.OnClearButtonClick(this, new EventArgs());

            this.ClearPresets();
        }

        private void AddPresetHandlers(EngineGraphPreset preset)
        {
            try
            {
                foreach (GraphAxis axisItem in preset.GraphAxisCollectionByGid)
                {                    
                    foreach (GraphParameter paramItem in axisItem.GraphParameterCollectionByAxisID)
                    {
                        paramItem.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                    }
                    axisItem.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                }
                preset.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ZedGraphControl.AddPresetHandlers:" + ex.ToString());
            }
        }

        private void RemovePresetHandlers(EngineGraphPreset preset)
        {
            try
            {
                // Remove all handlers associated with the preset.                
                foreach (GraphAxis axisItem in preset.GraphAxisCollectionByGid)
                {
                    foreach (GraphParameter paramItem in axisItem.GraphParameterCollectionByAxisID)
                    {
                        paramItem.PropertyChanged -= new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                    }
                    axisItem.PropertyChanged -= new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                }
                preset.PropertyChanged -= new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ZedGraphControl.RemovePresetHandlers:" + ex.ToString());
            }
        }

        [Obsolete("This method is obsolete. You can delete it the ZedGraphControl is able to use CELProject!", false)]
        public void SetEngineModel(string engineModel)
        {
            this.EngineModel = engineModel;

            if (isGraphLoaded == true)
                InitializeGraph();
        }

        private void InitializeGraph()
        {
            if (!this.LoadPreset())
            {
                this.savePreset.Enabled = false;
                this.deletePreset.Enabled = false;
                this.addAxisButton.Enabled = false;
                base.AllowDrop = false;
                return;
            }

            this.zedGraphControlBase.IsShowContextMenu = true;
            this.deletePreset.Enabled = true;
            this.state = GraphState.CanEdit;
        }

        void KT.IConnectable.Connect()
        {
            //return (this.Connected = true) == true; //ft
        }

        void KT.IConnectable.Disconnect()
        {
            //return (this.Connected = false) == false; //ft
        }

        /// <summary>
        /// Connected
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        private bool connected = false;
        bool KT.IConnectable.Connected //ft
        {
            get { return connected; }
            set { connected = value; }
        }

        private void DoResetGraph()
        {
            try
            {
                ZedGraph.GraphPane graphPane = this.zedGraphControlBase.GraphPane;

                this.DoStopRecording();

                graphPane.CurveList.Clear();

                graphPane.Title.Text = "No preset found!";
                graphPane.XAxis.Tag = null;
                graphPane.XAxis.Title.Text = "X Axis";
                graphPane.XAxis.Color = Color.Black;

                graphPane.YAxis.Title.Text = "[Drag a display here]";
                graphPane.YAxis.MajorTic.Color = Color.Black;
                graphPane.YAxis.MinorTic.Color = Color.Black;
                graphPane.YAxis.MajorTic.IsOpposite = false;
                graphPane.YAxis.MinorTic.IsOpposite = false;
                graphPane.YAxis.MajorTic.IsOpposite = false;
                graphPane.YAxis.MinorTic.IsOpposite = false;
                graphPane.YAxis.Tag = null;

                graphPane.Y2Axis.IsVisible = false;
                graphPane.Y2Axis.Title.Text = string.Empty;
                graphPane.Y2Axis.MajorTic.Color = Color.Black;
                graphPane.Y2Axis.MinorTic.Color = Color.Black;
                graphPane.Y2Axis.MajorTic.IsOpposite = false;
                graphPane.Y2Axis.MinorTic.IsOpposite = false;
                graphPane.Y2Axis.MajorTic.IsOpposite = false;
                graphPane.Y2Axis.MinorTic.IsOpposite = false;
                graphPane.Y2Axis.Tag = null;

                for (int index = graphPane.YAxisList.Count - 1; index > 0; index--)
                {
                    graphPane.YAxisList.RemoveAt(index);
                }

                for (int index = graphPane.Y2AxisList.Count - 1; index > 0; index--)
                {
                    graphPane.Y2AxisList.RemoveAt(index);
                }

                this.RestoreScale(graphPane);

                base.AllowDrop = true;
                this.addAxisButton.Enabled = false;
                this.chkHideYAxis.Enabled = true;
                this.clearButton.Enabled = false;
                this.presetsComboBox.Enabled = true;
                //this.recordButton.Enabled				= false;
                this.stopButton.Enabled = false;

                //this.zedGraphControlBase.IsEnableZoom = false; //test
                this.zedGraphControlBase.IsShowContextMenu = true;
                this.zedGraphControlBase.IsShowHScrollBar = false;
                this.zedGraphControlBase.IsShowVScrollBar = false;

                ZedGraph.ColorSymbolRotator.StaticInstance.NextColorIndex = 0;
                ZedGraph.ColorSymbolRotator.StaticInstance.NextSymbolIndex = 0;
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool LoadPreset()
        {
            this.isPresetLoading = true;
            bool isLoaded = false;
            try
            {
                this.presetColl = new EngineGraphPresetCollection();
                this.presetColl.Query.Where(this.presetColl.Query.EngineModel == this.EngineModel);
                this.presetColl.Query.OrderBy(this.presetColl.Query.PresetName.Ascending);
                
                //GraphAxisQuery o1 = enginePresetQuery.Prefetch<GraphAxisQuery>(EngineGraphPreset.Prefetch_GraphAxisCollectionByGid);
                //EngineGraphPresetQuery engineGraph1 = o1.GetQuery<EngineGraphPresetQuery>();
                //o1.OrderBy(o1.AxisTypeID.Ascending, o1.AxisIndex.Ascending);
                //o1.Where(engineGraph1.EngineModel == this.EngineModel);

                //GraphParameterQuery gp = enginePresetQuery.Prefetch<GraphParameterQuery>(EngineGraphPreset.Prefetch_GraphAxisCollectionByGid, GraphAxis.Prefetch_GraphParameterCollectionByAxisID );
                //EngineGraphPresetQuery engineGraph2 = gp.GetQuery<EngineGraphPresetQuery>();
                //GraphAxisQuery o2 = gp.GetQuery<GraphAxisQuery>();
                //gp.Where(engineGraph2.EngineModel == this.EngineModel);

                //AxisTypeQuery axt = enginePresetQuery.Prefetch<AxisTypeQuery>
                //   (
                //    CEL.Data.BusinessObjects.AxisType.Prefetch_AxisTypeCollectionByTypeID);
                //axt.Prefetch(EngineGraphPreset.Prefetch_GraphAxisCollectionByGid);

                //EngineGraphPresetQuery engineGraph3 = axt.GetQuery<EngineGraphPresetQuery>();
                //GraphAxisQuery o3 = axt.GetQuery<GraphAxisQuery>();
                //AxisTypeQuery atype = axt.GetQuery<AxisTypeQuery>();
                ////axt.Where(engineGraph3.EngineModel == this.EngineModel);

                // Populate preset associate to ProjectId/EngineModel

                this.presetColl.Query.Load();

                // Add handler on each item and child item to track changes
                if (presetColl.HasData)
                {
                    foreach (EngineGraphPreset presetItem in presetColl)
                    {
                        GraphAxisCollection graphColl = presetItem.GraphAxisCollectionByGid;
                        graphColl.Filter = graphColl.AsQueryable().OrderBy(ax => ax.AxisTypeID).ThenBy(ax => ax.AxisIndex);
                        AddPresetHandlers(presetItem);
                    }

                    isLoaded = true;
                }

                this.presetsComboBox.DisplayMember = EngineGraphPresetMetadata.PropertyNames.PresetName;
                this.presetsComboBox.ValueMember = EngineGraphPresetMetadata.PropertyNames.Id;                
                this.presetsComboBox.DataSource = presetColl;

                if (isLoaded) UpdateGraph();

                this.isPresetLoading = false;
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
            return isLoaded;
        }

        private void DoStopRecording()
        {
            if (this.timer != null)
            {
                this.timer.Dispose();
                this.timer = null;
            }

            this.displayTimer.Enabled = false;
        }

        public virtual object FindObject(string objectName)
        {
            return null;
        }

        public void ResetGraph()
        {
            this.DoResetGraph();
            //this.zedGraphControlBase.AxisChange();
            this.zedGraphControlBase.Invalidate();
        }

        private void RestoreScale(ZedGraph.GraphPane primaryPane)
        {
            this.SetXAxisTrendDefaultScale();

            using (Graphics g = this.CreateGraphics())
            {
                foreach (ZedGraph.YAxis axis in primaryPane.YAxisList)
                    axis.ResetAutoScale(primaryPane, g);
                foreach (ZedGraph.Y2Axis axis in primaryPane.Y2AxisList)
                    axis.ResetAutoScale(primaryPane, g);
            }
        }

        object KT.IConnectable.SearchObject(string objectName)
        {
            return this.OnObjectSearch(objectName);
        }

        public void ClearPresets()
        {
            if (isGraphLoaded)
            {
                try
                {
                    // Add handler on each item and child item to track changes
                    if (this.presetColl.HasData)
                    {
                        foreach (EngineGraphPreset presetItem in this.presetColl)
                        {
                            foreach (GraphAxis axisItem in presetItem.GraphAxisCollectionByGid)
                            {
                                foreach (GraphParameter paramItem in axisItem.GraphParameterCollectionByAxisID)
                                {
                                    paramItem.PropertyChanged -= new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                                }
                                axisItem.PropertyChanged -= new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                            }
                            presetItem.PropertyChanged -= new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                        }

                        this.EngineModel = string.Empty;
                        ResetGraph();

                        this.presetColl.Dispose();
                        this.presetColl = null;
                    }
                }
                catch (Exception ex)//ft ajout d'un catch
                {
                    MessageBox.Show(ex.ToString());
                }

                this.isGraphLoaded = false;
            }

            // La prochaine fois que la fenêtre va s'afficher, les presets seront s'initialisées.
            this.wasShown = false;
        }

        private void SetXAxisChannelSource(KT.Channels.IInfoChannel channel, ZedGraph.Axis axis, GraphAxis axisPersistent, GraphParameter grapParamPersistent)
        {
            try
            {
                EngineGraphPreset presetItem = (EngineGraphPreset)presetsComboBox.SelectedItem;
                GraphAxis axisPreset = axisPersistent;
                bool dropOnXAxis = axisPersistent == null && grapParamPersistent == null;

                string axisTitle;
                KT.Channels.INumericChannel numChannel;

                if (axisPreset == null)
                {
                    axisTitle = channel.Description;

                    if (presetItem.GraphAxisCollectionByGid != null)
                    {
                        foreach (GraphAxis axisItem in presetItem.GraphAxisCollectionByGid)
                        {
                            if (axisItem.AxisTypeID.HasValue ||
                                axisItem.AxisTypeID.Value == ((axis is ZedGraph.X2Axis) ? 2 : 1))
                            {
                                axisPreset = axisItem;
                                break;
                            }
                        }
                    }

                    if (axisPreset == null)
                    {
                        axisPreset = presetItem.GraphAxisCollectionByGid.AddNew();
                    }

                    axisPreset.AxisTitle = channel.Description;
                    axisPreset.AxisTypeID = (axis is ZedGraph.X2Axis) ? 2 : 1;
                    axisPreset.Color = axis.Color == null ? Color.Black.Name : axis.Color.Name;

                    GraphParameter graphParam = grapParamPersistent;
                    if (graphParam == null)
                    {
                        if (axisPreset.GraphParameterCollectionByAxisID != null)
                        {
                            //graphParam = axisPreset.GraphParameterCollectionByAxisID.AddNew();

                            foreach (GraphParameter paramItem in axisPreset.GraphParameterCollectionByAxisID)
                            {
                                if (paramItem.AxisID == axisPreset.Id)
                                {
                                    graphParam = paramItem;
                                    break;
                                }
                            }
                        }

                        if (graphParam == null)
                        {
                            graphParam = axisPreset.GraphParameterCollectionByAxisID.AddNew();
                        }

                        graphParam.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                        graphParam.ChannelName = channel.Name;
                    }

                    numChannel = ((KT.Channels.INumericChannel)channel);

                    if (numChannel != null)
                    {
                        if (numChannel.Units.Trim() != "")
                        {
                            axisPreset.Unit = numChannel.Units.Trim();
                        }
                        if (numChannel.Minimum != numChannel.Maximum)
                        {
                            axisPreset.ScaleMin = (int)numChannel.Minimum;
                            axisPreset.ScaleMax = (int)numChannel.Maximum;

                            axisPreset.ScaleMinAuto = false;
                            axisPreset.ScaleMaxAuto = false;

                            //this.zedGraphControlBase.GraphPane.XAxis.Scale.Min = numChannel.Minimum;
                            //this.zedGraphControlBase.GraphPane.XAxis.Scale.Max = numChannel.Maximum;
                        }
                        else
                        {
                            axisPreset.ScaleMin = 0; axisPreset.ScaleMax = 0;
                            axisPreset.ScaleMinAuto = true;
                            axisPreset.ScaleMaxAuto = true;
                        }
                    }

                    axisPreset.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                }
                                                
                if (dropOnXAxis)
                {
                    this.zedGraphControlBase.GraphPane.XAxis.Tag = channel;
                    this.zedGraphControlBase.GraphPane.XAxis.Title.Text = axisPreset.AxisTitle + ",(" + axisPreset.Unit + ")";
                    this.zedGraphControlBase.GraphPane.XAxis.Color = Color.Black;
                }
                else
                {                    
                    if (channel != null)
                    {
                        this.zedGraphControlBase.GraphPane.XAxis.Tag = channel;
                        this.zedGraphControlBase.GraphPane.XAxis.Title.Text = axisPreset.AxisTitle + ",(" + axisPreset.Unit + ")";
                        this.zedGraphControlBase.GraphPane.XAxis.Color = (axisPreset.Color == null) || (axisPreset.Color == string.Empty) ? Color.Black : Color.FromName(axisPreset.Color.TrimEnd());
                    }
                    else
                    {
                        if (grapParamPersistent.ChannelName != "Time (s)")
                        {
                            this.zedGraphControlBase.GraphPane.XAxis.Title.Text = grapParamPersistent.ChannelName + " NOT FOUND";
                            this.zedGraphControlBase.GraphPane.XAxis.Color = Color.Fuchsia;
                        }
                        else
                        {
                            this.zedGraphControlBase.GraphPane.XAxis.Title.Text = "Time (s)";
                            this.zedGraphControlBase.GraphPane.XAxis.Color = (axisPreset.Color == null) || (axisPreset.Color == string.Empty) ? Color.Black : Color.FromName(axisPreset.Color.TrimEnd()); ;
                        }
                    }
                }

                this.zedGraphControlBase.GraphPane.XAxis.Type = ZedGraph.AxisType.Linear;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.Mag = 0;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.Min = axisPreset.ScaleMin.HasValue ? axisPreset.ScaleMin.Value : 0;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.Max = axisPreset.ScaleMax.HasValue ? axisPreset.ScaleMax.Value : 30;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.MinAuto = axisPreset.ScaleMinAuto.HasValue ? axisPreset.ScaleMinAuto.Value : true;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.MaxAuto = axisPreset.ScaleMaxAuto.HasValue ? axisPreset.ScaleMaxAuto.Value : true;
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetXAxisTrendDefaultScale()
        {
            try
            {

                EngineGraphPreset preset = (EngineGraphPreset)presetsComboBox.SelectedItem;
                if (preset == null) return;

                GraphAxis defaultAxis = null;

                foreach (GraphAxis axisItem in preset.GraphAxisCollectionByGid)
                {
                    if (axisItem.AxisTypeID == 1)
                    {
                        defaultAxis = axisItem;

                        if (defaultAxis.Color == null)
                            defaultAxis.Color = Color.Black.Name;
                        break;
                    }
                }

                if (defaultAxis == null)
                {
                    defaultAxis = preset.GraphAxisCollectionByGid.AddNew();

                    defaultAxis.AxisTitle = "Time (s)";
                    defaultAxis.AxisIndex = 0;
                    defaultAxis.ScaleMin = 0;
                    defaultAxis.ScaleMax = 30;
                    defaultAxis.ScaleMinAuto = false;
                    defaultAxis.ScaleMaxAuto = false;
                    defaultAxis.AxisTypeID = 1;
                    defaultAxis.Color = Color.Black.Name;

                    GraphParameter graphParam = defaultAxis.GraphParameterCollectionByAxisID.AddNew();
                    graphParam.ChannelName = "Time (s)";
                    graphParam.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);

                    defaultAxis.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                }

                this.zedGraphControlBase.GraphPane.XAxis.Title.Text = defaultAxis.AxisTitle;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.Min = (double)defaultAxis.ScaleMin;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.MinAuto = (bool)defaultAxis.ScaleMinAuto;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.Max = (double)defaultAxis.ScaleMax;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.MaxAuto = (bool)defaultAxis.ScaleMaxAuto;
                this.zedGraphControlBase.GraphPane.XAxis.Color = Color.FromName(defaultAxis.Color);
                this.zedGraphControlBase.AxisChange();
                this.zedGraphControlBase.Invalidate();
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetXAxisXYDefaultScale()
        {
            KT.Channels.INumericChannel numChannel;

            numChannel = ((KT.Channels.INumericChannel)this.zedGraphControlBase.GraphPane.XAxis.Tag);

            if (numChannel != null)
            {
                if (numChannel.Minimum != numChannel.Maximum)
                {
                    this.zedGraphControlBase.GraphPane.XAxis.Scale.Min = numChannel.Minimum;
                    this.zedGraphControlBase.GraphPane.XAxis.Scale.Max = numChannel.Maximum;
                }
                else
                {
                    // Force delta between channel otherwise all will be hidden
                    this.zedGraphControlBase.GraphPane.XAxis.Scale.Min = numChannel.Minimum;
                    this.zedGraphControlBase.GraphPane.XAxis.Scale.Max = numChannel.Minimum + 100;
                }
            }
            else
            {
                this.zedGraphControlBase.GraphPane.XAxis.Scale.MinAuto = true;
                this.zedGraphControlBase.GraphPane.XAxis.Scale.MaxAuto = true;
            }
        }

        private void SetYAxisChannelSource(KT.Channels.IInfoChannel channel, GraphAxis axisPersistent, GraphParameter grapParamPersistent, ZedGraph.Axis axis, int index)
        {
            try
            {
                EngineGraphPreset presetItem = (EngineGraphPreset)presetsComboBox.SelectedItem;
                GraphAxis axisPreset = axisPersistent;
                Color newColor = ZedGraph.ColorSymbolRotator.StaticNextColor;

                ZedGraph.LineItem newCurve;
                string axisTitle;                    
                 
                if (axisPreset == null)
                {
                    KT.Channels.INumericChannel numChannel;

                    axisTitle = channel.Description;

                    if (presetItem.GraphAxisCollectionByGid != null)
                    {
                        // No not allow the user to add the same channel
                        foreach (GraphAxis checkedAxis in presetItem.GraphAxisCollectionByGid)
                        {
                            foreach (GraphParameter checkedParameter in checkedAxis.GraphParameterCollectionByAxisID)
                            {
                                if (channel.Name == checkedParameter.ChannelName)
                                {
                                    MessageBox.Show("Channel reference already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }

                        foreach (GraphAxis axisItem in presetItem.GraphAxisCollectionByGid)
                        {
                            if (axisItem.AxisTitle == channel.Description)
                            {
                                axisPreset = axisItem;
                                break;
                            }
                        }
                    }

                    if (axisPreset == null)
                    {
                        axisPreset = presetItem.GraphAxisCollectionByGid.AddNew();
                    }

                    axisPreset.AxisTitle = channel.Description;
                    axisPreset.AxisIndex = index;
                    axisPreset.AxisTypeID = (axis is ZedGraph.Y2Axis) ? 4 : 3;
                    axisPreset.Color = axis.Color.Name;

                    GraphParameter graphParam = grapParamPersistent;
                    if (graphParam == null)
                    {
                        if (axisPreset.GraphParameterCollectionByAxisID != null)
                        {
                            foreach (GraphParameter paramItem in axisPreset.GraphParameterCollectionByAxisID)
                            {
                                if (paramItem.AxisID == axisPreset.Id)
                                {
                                    graphParam = paramItem;
                                    break;
                                }
                            }
                        }

                        if (graphParam == null)
                        {
                            graphParam = axisPreset.GraphParameterCollectionByAxisID.AddNew();
                        }
                        graphParam.ChannelName = channel.Name;
                        graphParam.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                    }

                    numChannel = ((KT.Channels.INumericChannel)channel);

                    if (numChannel != null)
                    {
                        if (numChannel.Units.Trim() != "")
                        {
                            axisPreset.Unit = numChannel.Units.Trim();
                        }
                        if (numChannel.Minimum != numChannel.Maximum)
                        {
                            axisPreset.ScaleMin = (int)numChannel.Minimum;
                            axisPreset.ScaleMax = (int)numChannel.Maximum;
                            axisPreset.ScaleMinAuto = false;
                            axisPreset.ScaleMaxAuto = false;
                        }
                        else
                        {
                            axisPreset.ScaleMin = 0; axisPreset.ScaleMax = 0;
                            axisPreset.ScaleMinAuto = true;
                            axisPreset.ScaleMaxAuto = true;

                        }
                    }

                    axisPreset.PropertyChanged += new PropertyChangedEventHandler(this.OnPresetPropertyChanged);
                }

                // Set color for old preset
                if (!(axisPreset.Color != null & axisPreset.Color != string.Empty))
                {
                    if (newColor == this.zedGraphControlBase.BackColor)
                        newColor = ZedGraph.ColorSymbolRotator.StaticNextColor;

                    //Color newColor = ZedGraph.ColorSymbolRotator.StaticNextColor;
                    axisPreset.Color = newColor.Name;
                }

                axis.Type = ZedGraph.AxisType.Linear;
                axis.Scale.Mag = 0;
                axis.Scale.Min = (double)axisPreset.ScaleMin;
                axis.Scale.Max = (double)axisPreset.ScaleMax;
                axis.Scale.MinAuto = (bool)axisPreset.ScaleMinAuto;
                axis.Scale.MaxAuto = (bool)axisPreset.ScaleMaxAuto;

                if (channel != null)
                {
                    axis.Color = Color.FromName(axisPreset.Color.Trim()); // The Trim is important because SQL adding trailing spaces for nchar type
                    axis.Title.Text = axisPreset.AxisTitle + ",(" + axisPreset.Unit + ")";
                    newCurve = new ZedGraph.LineItem(channel.Description);
                    newCurve.Color = axis.Color;
                    newCurve.IsY2Axis = (axis is ZedGraph.Y2Axis);
                    newCurve.YAxisIndex = (int)axisPreset.AxisIndex;
                    newCurve.Symbol.Type = ZedGraph.SymbolType.None;
                    newCurve.Tag = channel;

                    //foreach (ZedGraph.CurveItem curveitem in this.zedGraphControlBase.GraphPane.CurveList)
                    //{
                    //    if (curveitem.Label.Text == channel.Description)
                    //    {
                    //        curveitem.Color = axis.Color;
                    //        return;
                    //    }
                    //}
                    axis.Tag = new AxisElements(axisPreset, new List<CurveItem> { newCurve });
                    this.zedGraphControlBase.GraphPane.CurveList.Add(newCurve);
                }
                else
                {
                    axis.Color = Color.Fuchsia;
                    axis.Title.Text = axisPreset.AxisTitle + " NOT FOUND";
                    newCurve = new ZedGraph.LineItem(grapParamPersistent.ChannelName + " NOT FOUND");
                    newCurve.Color = Color.Fuchsia;
                    newCurve.IsY2Axis = (axis is ZedGraph.Y2Axis);
                    newCurve.YAxisIndex = (int)axisPreset.AxisIndex;
                    newCurve.Symbol.Type = ZedGraph.SymbolType.None;
                    newCurve.Tag = null;

                    //foreach (ZedGraph.CurveItem curveitem in this.zedGraphControlBase.GraphPane.CurveList)
                    //{
                    //    if (curveitem.Label.Text == axis.Title.Text)
                    //    {
                    //        curveitem.Color = axis.Color;
                    //        return;
                    //    }
                    //}
                    axis.Tag = new AxisElements(axisPreset, new List<CurveItem> { newCurve });
                    this.zedGraphControlBase.GraphPane.CurveList.Add(newCurve);
                }
                
                this.zedGraphControlBase.AxisChange();
                this.zedGraphControlBase.Invalidate();
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetAxisProperties(ZedGraph.Axis axis)
        {
            axis.IsVisible = true;            
            axis.MajorTic.Color = axis.Color;
            axis.MajorTic.IsOpposite = false;
            axis.MinorTic.Color = axis.Color;
            axis.MinorTic.IsOpposite = false;
            axis.Scale.FontSpec.FontColor = axis.Color;
            axis.Title.FontSpec.FontColor = axis.Color;
        }

        private void StopRecording()
        {
            this.DoStopRecording();

            this.zedGraphControlBase.IsEnableZoom = true;
            this.zedGraphControlBase.IsShowContextMenu = true;
            this.zedGraphControlBase.IsShowHScrollBar = true;
            this.zedGraphControlBase.IsShowVScrollBar = true;

            this.zedGraphControlBase.AxisChange();
            this.zedGraphControlBase.Invalidate();

            this.recordButton.Enabled = true;
            this.clearButton.Enabled = true;
            this.stopButton.Enabled = false;
        }

        private void TimerTickTrend(Object obj)
        {
            try
            {
                lock (this.zedGraphControlBase)
                {
                    double time = (Environment.TickCount - tickStart) / 1000.0;

                    foreach (ZedGraph.LineItem curve in this.zedGraphControlBase.GraphPane.CurveList)
                    {
                        ZedGraph.IPointListEdit list = curve.Points as ZedGraph.IPointListEdit;

                        if (list == null)
                            continue;

                        if(curve.Tag != null)
                            list.Add(time, ((KT.Channels.IInfoChannel)curve.Tag).Value.ToDouble());
                    }

                    ZedGraph.Scale xScale = this.zedGraphControlBase.GraphPane.XAxis.Scale;
                    if (time > xScale.Max - xScale.MajorStep)
                    {
                        xScale.Max = time + xScale.MajorStep;
                        xScale.Min = xScale.Max - 30.0;
                    }
                }
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TimerTickXY(Object obj)
        {
            try
            {
                lock (this.zedGraphControlBase)
                {
                    double xValue = ((KT.Channels.IInfoChannel)this.zedGraphControlBase.GraphPane.XAxis.Tag).Value.ToDouble();

                    foreach (ZedGraph.LineItem curve in this.zedGraphControlBase.GraphPane.CurveList)
                    {
                        ZedGraph.IPointListEdit list = curve.Points as ZedGraph.IPointListEdit;

                        if (list == null)
                            continue;

                        list.Add(xValue, ((KT.Channels.IInfoChannel)curve.Tag).Value.ToDouble());
                    }
                }
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool IsDataUnsaved()
        {
            try
            {                
                if (!this.isPresetLoading)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    
                    foreach (EngineGraphPreset item in this.presetColl)
                    {
                        if (item.RowState != EntitySpaces.Interfaces.esDataRowState.Unchanged)
                        {
                            sb.AppendLine(item.PresetName);
                        }
                        else
                        {
                            if (item.GraphAxisCollectionByGid.IsDirty)
                            {
                                sb.AppendLine(item.PresetName);
                            }
                            else
                            {
                                foreach (GraphAxis axisItem in item.GraphAxisCollectionByGid)
                                {
                                    if (axisItem.GraphParameterCollectionByAxisID.IsDirty)
                                    {
                                        sb.AppendLine(item.PresetName);
                                    }
                                }

                            }
                        }
                    }

                    if (sb.Length > 0)
                    {
                        this.errorProvider.SetError(this.savePreset, "Preset not saved: " + Environment.NewLine + sb.ToString());
                        this.savePreset.Enabled = true;
                        return true;
                    }
                    else
                    {
                        this.errorProvider.SetError(this.savePreset, "");
                        this.savePreset.Enabled = false;
                    }
                }
            }
            catch (Exception ex)//ft ajout d'un catch
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void UpdateGraph()
        {
            EngineGraphPreset preset;
            GraphAxisCollection graphAxisColl;

            int yCounter = 0;
            int y2Counter = 0;
            bool presetUpdated = false;

            if (this.presetsComboBox.SelectedIndex != -1)
            {
                chkHideYAxis.Checked = false;
                addAxisButton.Enabled = true;

                preset = (EngineGraphPreset)this.presetsComboBox.SelectedItem;
                graphAxisColl = preset.GraphAxisCollectionByGid;

                // Do not enable recording button during recording
                if (!this.displayTimer.Enabled)
                    this.ResetGraph();

                this.zedGraphControlBase.GraphPane.Title.Text = preset.PresetName;
                this.zedGraphControlBase.SaveFileDialog.FileName = preset.PresetName;


                foreach (GraphAxis axisItem in graphAxisColl)
                {
                    GraphParameterCollection graphParamColl = axisItem.GraphParameterCollectionByAxisID;
                    KT.Channels.IInfoChannel channel;

                    foreach (GraphParameter graphParamItem in graphParamColl)
                    {
                        channel = this.OnObjectSearch(graphParamItem.ChannelName);
                        //if (channel != null)
                        {
                            CEL.Data.BusinessObjects.AxisType axisType = axisItem.UpToAxisTypeByAxisTypeID;

                            switch (axisType.Type)
                            {
                                case "XAxis":
                                    {
                                        ZedGraph.XAxis xAxis = this.zedGraphControlBase.GraphPane.XAxis;
                                        this.SetXAxisChannelSource(channel, xAxis, axisItem, graphParamItem);
                                        this.SetAxisProperties(xAxis);
                                        break;
                                    }
                                case "X2Axis":
                                    {
                                        // TODO: Support persistensy with X2Axis
                                        throw new NotSupportedException();
                                    }
                                case "YAxis":
                                    {
                                        ZedGraph.YAxis axis;
                                        int axisYIndex = (int)axisItem.AxisIndex;

                                        if (yCounter == 0)
                                        {
                                            axis = this.zedGraphControlBase.GraphPane.YAxis;
                                            //axis.Tag = axisItem;                                            
                                        }
                                        else
                                        {
                                            axis = new ZedGraph.YAxis();
                                            //axis.Tag = axisItem;

                                            this.zedGraphControlBase.GraphPane.YAxisList.Add(axis);
                                        }

                                        this.SetYAxisChannelSource(channel, axisItem, graphParamItem, axis, axisYIndex);
                                        this.SetAxisProperties(axis);
                                        yCounter++;

                                        if ((yCounter > 0) & this.zedGraphControlBase.GraphPane.YAxisList.Count != yCounter)
                                        {
                                            { }
                                        }

                                        break;
                                    }
                                case "Y2Axis":
                                    {
                                        ZedGraph.Y2Axis axis;
                                        int axisY2Index = (int)axisItem.AxisIndex;

                                        if (y2Counter == 0)
                                        {
                                            axis = this.zedGraphControlBase.GraphPane.Y2Axis;
                                            //axis.Tag = axisItem;
                                            axis.IsVisible = true;

                                            this.zedGraphControlBase.GraphPane.Y2Axis.MajorTic.IsOpposite = false;
                                            this.zedGraphControlBase.GraphPane.Y2Axis.MinorTic.IsOpposite = false;
                                        }
                                        else
                                        {
                                            axis = new ZedGraph.Y2Axis();
                                            //axis.Tag = axisItem;
                                            this.zedGraphControlBase.GraphPane.Y2AxisList.Add(axis);
                                        }

                                        //// Check if the axis index as change
                                        //if (axisY2Index != y2Counter)
                                        //    axisY2Index = y2Counter;

                                        this.SetYAxisChannelSource(channel, axisItem, graphParamItem, axis, axisY2Index);
                                        this.SetAxisProperties(axis);
                                        y2Counter++;

                                        if ((y2Counter > 0) & this.zedGraphControlBase.GraphPane.Y2AxisList.Count != y2Counter)
                                        {
                                            { }
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        throw new NotSupportedException();
                                    }
                            }
                        }
                    }
                }

                // Do not enable recording button during recording
                if (!this.displayTimer.Enabled)
                    this.recordButton.Enabled = true;
                presetUpdated = true;

                //
                if (((yCounter > 0) & this.zedGraphControlBase.GraphPane.YAxisList.Count != yCounter) ||
                    ((y2Counter > 0) & this.zedGraphControlBase.GraphPane.Y2AxisList.Count != y2Counter))
                {
                    { }
                }
            }
            else
            {
                //this.zedGraphControlBase.SaveFileDialog.FileName = "";
                this.DoResetGraph();

                this.zedGraphControlBase.GraphPane.YAxis.Title.Text = "Y Axis";
                this.zedGraphControlBase.IsShowContextMenu = false;
                this.deletePreset.Enabled = false;

            }

            if (presetUpdated & (
                ((yCounter > 0) & this.zedGraphControlBase.GraphPane.YAxisList.Count != yCounter) ||
                ((y2Counter > 0) & this.zedGraphControlBase.GraphPane.Y2AxisList.Count != y2Counter)))
            {
                { }
                presetUpdated = false;
            }

            IsDataUnsaved();
            this.zedGraphControlBase.AxisChange();
            this.zedGraphControlBase.Invalidate();
        }

        #endregion

        #region Methods extracted from ZedGraph source code it's is temporary until the library we be fixed

        /// <summary>
        /// This method is needed to show point value tool tip 
        /// </summary>
        /// <param name="mousePt"></param>
        /// <returns></returns>
        /// <remarks>The method name will fire exception only if we try to show the tooltip from the rolling point boundaries</remarks>
        private Point HandlePointValues(Point mousePt)
        {
            int iPt;
            GraphPane pane;
            object nearestObj;
            ZedGraph.ZedGraphControl zedGraphControl = this.zedGraphControlBase;

            using (Graphics g = this.CreateGraphics())
            {

                if (zedGraphControl.MasterPane.FindNearestPaneObject(mousePt,
                    g, out pane, out nearestObj, out iPt))
                {
                    if (nearestObj is CurveItem && iPt >= 0)
                    {
                        CurveItem curve = (CurveItem)nearestObj;
                        {

                            if (curve is PieItem)
                            {
                                this.SetToolTip(((PieItem)curve).Value.ToString("G"), mousePt);
                            }
                            else
                            {
                                PointPair pt = curve.Points[iPt];

                                if (pt.Tag is string)
                                    this.SetToolTip((string)pt.Tag, mousePt);
                                else
                                {
                                    double xVal, yVal, lowVal;
                                    ValueHandler valueHandler = new ValueHandler(pane, false);
                                    if ((curve is BarItem || curve is ErrorBarItem || curve is HiLowBarItem)
                                            && pane.BarSettings.Base != BarBase.X)
                                        valueHandler.GetValues(curve, iPt, out yVal, out lowVal, out xVal);
                                    else
                                        valueHandler.GetValues(curve, iPt, out xVal, out lowVal, out yVal);

                                    string xStr = MakeValueLabel(curve.GetXAxis(pane), xVal, iPt,
                                        curve.IsOverrideOrdinal);
                                    string yStr = MakeValueLabel(curve.GetYAxis(pane), yVal, iPt,
                                        curve.IsOverrideOrdinal);

                                    this.SetToolTip("( " + xStr + ", " + yStr + " )", mousePt);
                                }
                            }

                            this.EnableToolTip();
                        }
                    }
                    else
                        this.DisableToolTip();
                }
                else
                    this.DisableToolTip();
            }
            return mousePt;
        }

        /// <summary>
        /// Enables the tool tip.
        /// </summary>
        private void EnableToolTip()
        {
            this.tooltip.Enable();
        }

        /// <summary>
        /// Sets the tool tip.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="point">The point.</param>
        private void SetToolTip(string caption, Point point)
        {
            this.tooltip.Set(caption, point);
        }

        /// <summary>
        /// Disables the tool tip.
        /// </summary>
        private void DisableToolTip()
        {
            this.tooltip.Disable();
        }

        /// <summary>
        /// Make a string label that corresponds to a user scale value.
        /// </summary>
        /// <param name="axis">The axis from which to obtain the scale value.  This determines
        /// if it's a date value, linear, log, etc.</param>
        /// <param name="val">The value to be made into a label</param>
        /// <param name="iPt">The ordinal position of the value</param>
        /// <param name="isOverrideOrdinal">true to override the ordinal settings of the axis,
        /// and prefer the actual value instead.</param>
        /// <returns>The string label.</returns>
        protected string MakeValueLabel(Axis axis, double val, int iPt, bool isOverrideOrdinal)
        {
            if (axis != null)
            {
                if (axis.Scale.IsDate || axis.Scale.Type == ZedGraph.AxisType.DateAsOrdinal)
                {
                    return XDate.ToString(val, "G");
                }
                else if (axis.Scale.IsText && axis.Scale.TextLabels != null)
                {
                    int i = iPt;
                    if (isOverrideOrdinal)
                        i = (int)(val - 0.5);

                    if (i >= 0 && i < axis.Scale.TextLabels.Length)
                        return axis.Scale.TextLabels[i];
                    else
                        return (i + 1).ToString();
                }
                else if (axis.Scale.IsAnyOrdinal && axis.Scale.Type != ZedGraph.AxisType.LinearAsOrdinal
                                && !isOverrideOrdinal)
                {
                    return iPt.ToString("G");
                }
                else
                    return val.ToString("G");
            }
            else
                return "";
        }
        #endregion

        private class PresetFilter
        {
            private EngineGraphPreset _engineGraphPreset;
            private ZedGraph.GraphPane _graphPane;

            public PresetFilter(EngineGraphPreset engPreset, ZedGraph.GraphPane graphPane)
            {
                this._engineGraphPreset = engPreset;
                this._graphPane = graphPane;
            }

            public string PresetName
            {
                get
                {
                    return this._engineGraphPreset.PresetName;
                }
                set
                {
                    if (value != _engineGraphPreset.PresetName)
                    {
                        _engineGraphPreset.PresetName = value;
                        _graphPane.Title.Text = value;
                    }
                }
            }
        }

        private class AxisFilter
        {
            private GraphAxis _axisPreset;
            private AxisElements _axisElements;            
            private ZedGraph.Axis _graphAxis;

            public AxisFilter(ZedGraph.Axis graphAxis, GraphAxis axisPreset)
            {
                _graphAxis = graphAxis;
                _axisPreset = axisPreset;

                if(graphAxis.Tag is AxisElements axisElements)
                { 
                    _axisElements = axisElements;
                }
            }

            [Category("Axis")]
            public string AxisTitle
            {
                get
                {
                    return _axisPreset.AxisTitle;
                }
                set
                {
                    if (value != _axisPreset.AxisTitle)
                    {
                        var unitIndex = _graphAxis.Title.Text.IndexOf(',');

                        _axisPreset.AxisTitle = value;

                        if(unitIndex != -1)
                        { 
                            _graphAxis.Title.Text = value + _graphAxis.Title.Text.Substring(unitIndex);
                        }
                        else
                        {
                            _graphAxis.Title.Text = value;
                        }
                    }
                }
            }

            [Category("Axis")]
            public Color AxisColor
            {
                get
                {
                    return Color.FromName(_axisPreset.Color.Trim());
                }
                set
                {
                    if (value != Color.FromName(_axisPreset.Color.Trim()))
                    {
                        _axisPreset.Color = value.Name;

                        _graphAxis.MajorTic.Color = value;
                        _graphAxis.MajorTic.IsOpposite = false;
                        _graphAxis.MinorTic.Color = value;
                        _graphAxis.MajorTic.IsOpposite = false;
                        _graphAxis.Scale.FontSpec.FontColor = value;
                        _graphAxis.Title.FontSpec.FontColor = value;

                        _graphAxis.Color = value;

                        // This will change when we will have multiple curve for one axis
                        if (_graphAxis is YAxis || _graphAxis is Y2Axis)
                        {
                            foreach (var curve in _axisElements.Curves)
                            {
                                curve.Color = value;
                            }
                        }
                    }
                }
            }

            [Category("Axis Scale")]
            public int Min
            {
                get
                {
                    return (int)_axisPreset.ScaleMin;
                }
                set
                {
                    if (value != _axisPreset.ScaleMin)
                    {
                        _axisPreset.ScaleMin = value;
                        _graphAxis.Scale.Min = value;
                    }
                }
            }

            [Category("Axis Scale")]
            public int Max
            {
                get
                {
                    return (int)_axisPreset.ScaleMax;
                }
                set
                {
                    if (value != _axisPreset.ScaleMax)
                    {
                        _axisPreset.ScaleMax = value;
                        _graphAxis.Scale.Max = value;
                    }
                }
            }

            [Category("Axis Scale")]
            public bool MinAuto
            {
                get
                {
                    return (bool)_axisPreset.ScaleMinAuto;
                }
                set
                {
                    if (value != _axisPreset.ScaleMinAuto)
                    {
                        _axisPreset.ScaleMinAuto = value;
                        _graphAxis.Scale.MinAuto = value;
                    }
                }
            }

            [Category("Axis Scale")]
            public bool MaxAuto
            {
                get
                {
                    return (bool)_axisPreset.ScaleMaxAuto;
                }
                set
                {
                    if (value != _axisPreset.ScaleMaxAuto)
                    {
                        _axisPreset.ScaleMaxAuto = value;
                        _graphAxis.Scale.MaxAuto = value;
                    }
                }
            }
        }

        private class AxisElements
        {
            public GraphAxis AxisPreset { get; }
            public List<CurveItem> Curves { get; }

            public AxisElements(GraphAxis graphAxis, List<CurveItem> curves)
            {
                AxisPreset = graphAxis;
                Curves = curves;
            }
        }

        #region Windows Forms Designer generated code
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.recordButton = new System.Windows.Forms.Button();
            this.presetLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.displayTimer = new System.Timers.Timer();
            this.presetsComboBox = new System.Windows.Forms.ComboBox();
            this.zedGraphControlBase = new ZedGraph.ZedGraphControl();
            this.addAxisButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.deletePreset = new System.Windows.Forms.Button();
            this.savePreset = new System.Windows.Forms.Button();
            this.newPreset = new System.Windows.Forms.Button();
            this.chkHideYAxis = new System.Windows.Forms.CheckBox();
            this.pointTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.chkZoomXAxis = new System.Windows.Forms.CheckBox();
            this.chkZoomYAxis = new System.Windows.Forms.CheckBox();
            this.lblZoom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // recordButton
            // 
            this.recordButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.recordButton.BackColor = System.Drawing.Color.Blue;
            this.recordButton.Enabled = false;
            this.recordButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recordButton.ForeColor = System.Drawing.Color.White;
            this.recordButton.Location = new System.Drawing.Point(128, 346);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(120, 32);
            this.recordButton.TabIndex = 2;
            this.recordButton.Text = "Record";
            this.recordButton.UseVisualStyleBackColor = false;
            this.recordButton.Click += new System.EventHandler(this.OnRecordButtonClick);
            // 
            // presetLabel
            // 
            this.presetLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.presetLabel.BackColor = System.Drawing.Color.Black;
            this.presetLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.presetLabel.ForeColor = System.Drawing.Color.White;
            this.presetLabel.Location = new System.Drawing.Point(648, 361);
            this.presetLabel.Name = "presetLabel";
            this.presetLabel.Size = new System.Drawing.Size(72, 32);
            this.presetLabel.TabIndex = 6;
            this.presetLabel.Text = "Presets";
            this.presetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearButton.BackColor = System.Drawing.Color.Blue;
            this.clearButton.Enabled = false;
            this.clearButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearButton.ForeColor = System.Drawing.Color.White;
            this.clearButton.Location = new System.Drawing.Point(384, 346);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(120, 32);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear Data";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.OnClearButtonClick);
            // 
            // displayTimer
            // 
            this.displayTimer.SynchronizingObject = this;
            this.displayTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnDisplayTimerElapsed);
            // 
            // presetsComboBox
            // 
            this.presetsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.presetsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.presetsComboBox.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.presetsComboBox.Location = new System.Drawing.Point(725, 346);
            this.presetsComboBox.Name = "presetsComboBox";
            this.presetsComboBox.Size = new System.Drawing.Size(272, 30);
            this.presetsComboBox.TabIndex = 7;
            this.presetsComboBox.SelectedIndexChanged += new System.EventHandler(this.OnPresetsComboBoxSelectedIndexChanged);
            // 
            // zedGraphControlBase
            // 
            this.zedGraphControlBase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControlBase.IsAutoScrollRange = true;
            this.zedGraphControlBase.IsPrintKeepAspectRatio = false;
            this.zedGraphControlBase.IsPrintScaleAll = false;
            this.zedGraphControlBase.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControlBase.Name = "zedGraphControlBase";
            this.zedGraphControlBase.ScrollGrace = 0D;
            this.zedGraphControlBase.ScrollMaxX = 0D;
            this.zedGraphControlBase.ScrollMaxY = 0D;
            this.zedGraphControlBase.ScrollMaxY2 = 0D;
            this.zedGraphControlBase.ScrollMinX = 0D;
            this.zedGraphControlBase.ScrollMinY = 0D;
            this.zedGraphControlBase.ScrollMinY2 = 0D;
            this.zedGraphControlBase.Size = new System.Drawing.Size(1000, 341);
            this.zedGraphControlBase.TabIndex = 0;
            this.zedGraphControlBase.UseExtendedPrintDialog = true;
            // 
            // addAxisButton
            // 
            this.addAxisButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addAxisButton.BackColor = System.Drawing.Color.Blue;
            this.addAxisButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addAxisButton.ForeColor = System.Drawing.Color.White;
            this.addAxisButton.Location = new System.Drawing.Point(0, 346);
            this.addAxisButton.Name = "addAxisButton";
            this.addAxisButton.Size = new System.Drawing.Size(120, 32);
            this.addAxisButton.TabIndex = 1;
            this.addAxisButton.Text = "Add Axis";
            this.addAxisButton.UseVisualStyleBackColor = false;
            this.addAxisButton.Click += new System.EventHandler(this.OnAddAxisButtonClick);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stopButton.BackColor = System.Drawing.Color.Blue;
            this.stopButton.Enabled = false;
            this.stopButton.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stopButton.ForeColor = System.Drawing.Color.White;
            this.stopButton.Location = new System.Drawing.Point(256, 346);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(120, 32);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = false;
            this.stopButton.Click += new System.EventHandler(this.OnStopButtonClick);
            // 
            // deletePreset
            // 
            this.deletePreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deletePreset.BackColor = System.Drawing.Color.Blue;
            this.deletePreset.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deletePreset.ForeColor = System.Drawing.Color.White;
            this.deletePreset.Location = new System.Drawing.Point(801, 382);
            this.deletePreset.Name = "deletePreset";
            this.deletePreset.Size = new System.Drawing.Size(70, 32);
            this.deletePreset.TabIndex = 13;
            this.deletePreset.Text = "Delete";
            this.deletePreset.UseVisualStyleBackColor = false;
            this.deletePreset.Click += new System.EventHandler(this.OnDeletePreset_Click);
            // 
            // savePreset
            // 
            this.savePreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.savePreset.BackColor = System.Drawing.Color.Blue;
            this.savePreset.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.savePreset.ForeColor = System.Drawing.Color.White;
            this.savePreset.Location = new System.Drawing.Point(877, 382);
            this.savePreset.Name = "savePreset";
            this.savePreset.Size = new System.Drawing.Size(70, 32);
            this.savePreset.TabIndex = 12;
            this.savePreset.Text = "Save";
            this.savePreset.UseVisualStyleBackColor = false;
            this.savePreset.Click += new System.EventHandler(this.OnSavePreset_Click);
            // 
            // newPreset
            // 
            this.newPreset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.newPreset.BackColor = System.Drawing.Color.Blue;
            this.newPreset.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newPreset.ForeColor = System.Drawing.Color.White;
            this.newPreset.Location = new System.Drawing.Point(725, 382);
            this.newPreset.Name = "newPreset";
            this.newPreset.Size = new System.Drawing.Size(70, 32);
            this.newPreset.TabIndex = 11;
            this.newPreset.Text = "New";
            this.newPreset.UseVisualStyleBackColor = false;
            this.newPreset.Click += new System.EventHandler(this.OnNewPreset_Click);
            // 
            // chkHideYAxis
            // 
            this.chkHideYAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkHideYAxis.AutoSize = true;
            this.chkHideYAxis.BackColor = System.Drawing.Color.Black;
            this.chkHideYAxis.ForeColor = System.Drawing.Color.White;
            this.chkHideYAxis.Location = new System.Drawing.Point(3, 390);
            this.chkHideYAxis.Name = "chkHideYAxis";
            this.chkHideYAxis.Size = new System.Drawing.Size(84, 17);
            this.chkHideYAxis.TabIndex = 15;
            this.chkHideYAxis.Text = "Hide Y Axes";
            this.chkHideYAxis.UseVisualStyleBackColor = false;
            // 
            // pointTooltip
            // 
            this.pointTooltip.AutoPopDelay = 5000;
            this.pointTooltip.InitialDelay = 100;
            this.pointTooltip.ReshowDelay = 0;
            // 
            // chkZoomXAxis
            // 
            this.chkZoomXAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkZoomXAxis.AutoSize = true;
            this.chkZoomXAxis.BackColor = System.Drawing.Color.Black;
            this.chkZoomXAxis.Checked = true;
            this.chkZoomXAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkZoomXAxis.ForeColor = System.Drawing.Color.White;
            this.chkZoomXAxis.Location = new System.Drawing.Point(349, 390);
            this.chkZoomXAxis.Name = "chkZoomXAxis";
            this.chkZoomXAxis.Size = new System.Drawing.Size(55, 17);
            this.chkZoomXAxis.TabIndex = 16;
            this.chkZoomXAxis.Text = "X Axis";
            this.chkZoomXAxis.UseVisualStyleBackColor = false;
            this.chkZoomXAxis.CheckedChanged += new System.EventHandler(this.chkZoomAxes_CheckedChanged);
            // 
            // chkZoomYAxis
            // 
            this.chkZoomYAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkZoomYAxis.AutoSize = true;
            this.chkZoomYAxis.BackColor = System.Drawing.Color.Black;
            this.chkZoomYAxis.ForeColor = System.Drawing.Color.White;
            this.chkZoomYAxis.Location = new System.Drawing.Point(426, 390);
            this.chkZoomYAxis.Name = "chkZoomYAxis";
            this.chkZoomYAxis.Size = new System.Drawing.Size(55, 17);
            this.chkZoomYAxis.TabIndex = 17;
            this.chkZoomYAxis.Text = "Y Axis";
            this.chkZoomYAxis.UseVisualStyleBackColor = false;
            this.chkZoomYAxis.CheckedChanged += new System.EventHandler(this.chkZoomAxes_CheckedChanged);
            // 
            // lblZoom
            // 
            this.lblZoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblZoom.BackColor = System.Drawing.Color.Black;
            this.lblZoom.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoom.ForeColor = System.Drawing.Color.White;
            this.lblZoom.Location = new System.Drawing.Point(283, 382);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(55, 20);
            this.lblZoom.TabIndex = 18;
            this.lblZoom.Text = "Zoom";
            this.lblZoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ZedGraphControl
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.chkZoomYAxis);
            this.Controls.Add(this.chkZoomXAxis);
            this.Controls.Add(this.chkHideYAxis);
            this.Controls.Add(this.deletePreset);
            this.Controls.Add(this.savePreset);
            this.Controls.Add(this.newPreset);
            this.Controls.Add(this.presetsComboBox);
            this.Controls.Add(this.presetLabel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.recordButton);
            this.Controls.Add(this.addAxisButton);
            this.Controls.Add(this.zedGraphControlBase);
            this.DoubleBuffered = true;
            this.Name = "ZedGraphControl";
            this.Size = new System.Drawing.Size(1000, 418);
            ((System.ComponentModel.ISupportInitialize)(this.displayTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void chkZoomAxes_CheckedChanged(object sender, EventArgs e)
        {
            if(sender is CheckBox checkBox)
            { 
                if(sender.Equals(chkZoomXAxis))
                    this.zedGraphControlBase.IsEnableHZoom = checkBox.Checked;
                else
                    this.zedGraphControlBase.IsEnableVZoom = checkBox.Checked;
            }
        }
    }
}

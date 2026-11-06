/*******************************************************************
 * NAME:		XYGraphDemo.cs
 * 
 * COPYRIGHT:	(c) 2003, KineticaRT Ltd. All rights reserved.
 *
 *				This software is protected by Copyright and the
 *				information contained herein is confidential.
 *				This software may not be copied and the
 *				information contained herein may not be used or
 *				disclosed except with the written permission of
 *				and in the manner permitted by the Authors, 
 *				KineticaRT Ltd.
 * 
 * ABSTRACT:    Demonstration of the KineticaRT XY Graph
 * 
 * NOTES:		None
 * 
 * MODIFICATION RECORD:
 * 
 * Date			Version		Author		Details
 * 18-Dec-2002  V0.1a		R Thorne	Original Version
 *******************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace XYGraphDemonstration
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class XYGraphDemo : KineticaRT.Demonstrations.DemoForm
	{
		private System.ComponentModel.IContainer components;
		private static ApplicationContext context;
		private System.Windows.Forms.TabPage tabProperties;
		private System.Windows.Forms.GroupBox groupStdXYGraphInfo;
		private AxSHDocVw.AxWebBrowser browseStdXYGraphInfo;
		private System.Windows.Forms.GroupBox groupXYGraphProperties;
		private System.Windows.Forms.PropertyGrid propertiesGridStdXYGraph;
		private System.Windows.Forms.Button cmdShowProperties;
		private KineticaRT.Controls.XYGraph xyGraphProperties;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.Button cmdShowStdXYGraphInfo;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabXYGraph;
		private System.Windows.Forms.TabPage tabTraces;
		private System.Windows.Forms.CheckBox chkVerticalGridEnabled;
		private System.Windows.Forms.CheckBox chkHorizontalGridEnabled;
		private System.Windows.Forms.CheckBox chkenableThresholds;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton optBorderStyleRaised;
		private System.Windows.Forms.RadioButton optBorderStyleRoundedwithInset;
		private System.Windows.Forms.RadioButton optBorderStyleRounded;
		private System.Windows.Forms.RadioButton optBorderStyleShadedwithInset;
		private System.Windows.Forms.RadioButton optBorderStyleShaded;
		private System.Windows.Forms.RadioButton optBorderStyleFlat;
		private System.Windows.Forms.RadioButton optBorderStyleEditBox;
		private System.Windows.Forms.RadioButton optBorderStyleIndented;
		private System.Windows.Forms.RadioButton optBorderStyleNone;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton optShapeEllipse;
		private System.Windows.Forms.RadioButton optShapeRoundedRectangle;
		private System.Windows.Forms.RadioButton optShapeRectangle;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.CheckBox chkTraceLineVisible;
		private System.Windows.Forms.RadioButton radioPenStyleCross;
		private System.Windows.Forms.RadioButton radioPenStyleSquare;
		private System.Windows.Forms.RadioButton radioPenStyleMan;
		private System.Windows.Forms.RadioButton radioPenStyleOpenCircle;
		private System.Windows.Forms.RadioButton radioPenStyleCircle;
		private System.Windows.Forms.RadioButton radioPenStylePlus;
		private System.Windows.Forms.RadioButton radioPenStyleDot;
		private System.Windows.Forms.CheckBox chkFadeEnabled;
		private System.Windows.Forms.CheckBox chkShowMarks;
		private System.Windows.Forms.RadioButton radioMarkStyleDiamond;
		private System.Windows.Forms.RadioButton radioMarkStyleCross;
		private System.Windows.Forms.RadioButton radioMarkStyleSquare;
		private System.Windows.Forms.RadioButton radioMarkStyleMan;
		private System.Windows.Forms.RadioButton radioMarkStyleOpenCircle;
		private System.Windows.Forms.RadioButton radioMarkStyleCircle;
		private System.Windows.Forms.RadioButton radioMarkStylePlus;
		private System.Windows.Forms.RadioButton radioMarkStyleDot;
		private System.Windows.Forms.RadioButton radioPenStyleDiamond;
		private System.Windows.Forms.TrackBar barXYGraphPersistence;
		private System.Windows.Forms.Label lblXYGraphPersistence;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private KineticaRT.Controls.SliderMultiNeedle sliderPropertiesInput;
		private System.Windows.Forms.CheckBox chkPropertiesManualInput;
		private System.Windows.Forms.TabPage tabMultiTrace;
		private System.Windows.Forms.GroupBox groupMultipleTracesInfo;
		private AxSHDocVw.AxWebBrowser browseMultipleTracesInfo;
		private KineticaRT.Controls.XYGraph xyGraphMultipleTraces;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TrackBar trackInput2YPhase;
		private System.Windows.Forms.TrackBar trackInput2YPeriod;
		private System.Windows.Forms.TrackBar trackInput2XPhase;
		private System.Windows.Forms.TrackBar trackInput2XPeriod;
		private System.Windows.Forms.TrackBar trackInput1YPhase;
		private System.Windows.Forms.TrackBar trackInput1YPeriod;
		private System.Windows.Forms.TrackBar trackInput1XPhase;
		private System.Windows.Forms.TrackBar trackInput1XPeriod;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Button cmdShowHideMultipleTracesInfo;
		private System.Windows.Forms.Button cmdAddMark;
		private static SplashForm splashForm;


		public XYGraphDemo()
		{
			KineticaRT.Servers.Simulate.SineWave sineWave;
			KineticaRT.Channels.Channel channel;

			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			configuration = new KineticaRT.Server();
			configuration.Children = new KineticaRT.ObjectCollection();

			sineWave = new KineticaRT.Servers.Simulate.SineWave();
			sineWave.Maximum = 100;
			sineWave.Period = 5;
			configuration.Children.Add("PropertiesXAuto", sineWave);

			sineWave = new KineticaRT.Servers.Simulate.SineWave();
			sineWave.Maximum = 100;
			sineWave.Period = 10;
			configuration.Children.Add("PropertiesYAuto", sineWave);

			channel = new KineticaRT.Channels.Channel();
			configuration.Children.Add("PropertiesX", channel);
			sliderPropertiesInput.Needles[0].DestinationChannel = channel;
			xyGraphProperties.Traces[0].XSourceChannel = channel;

			channel = new KineticaRT.Channels.Channel();
			configuration.Children.Add("PropertiesY", channel);
			sliderPropertiesInput.Needles[1].DestinationChannel = channel;
			xyGraphProperties.Traces[0].YSourceChannel = channel;

			sineWave = new KineticaRT.Servers.Simulate.SineWave();			
			sineWave.Maximum = 100;
			sineWave.Period = 1;
			sineWave.Phase = 1;
			configuration.Children.Add("Input1X", sineWave);
			xyGraphMultipleTraces.Traces[0].XSource = "Input1X";

			sineWave = new KineticaRT.Servers.Simulate.SineWave();			
			sineWave.Maximum = 100;
			sineWave.Period = 1;
			sineWave.Phase = 1;
			configuration.Children.Add("Input1Y", sineWave);
			xyGraphMultipleTraces.Traces[0].YSource = "Input1Y";

			sineWave = new KineticaRT.Servers.Simulate.SineWave();			
			sineWave.Maximum = 100;
			sineWave.Period = 1;
			sineWave.Phase = 1;
			configuration.Children.Add("Input2X", sineWave);
			xyGraphMultipleTraces.Traces[1].XSource = "Input2X";

			sineWave = new KineticaRT.Servers.Simulate.SineWave();			
			sineWave.Maximum = 100;
			sineWave.Period = 1;
			sineWave.Phase = 1;
			configuration.Children.Add("Input2Y", sineWave);
			xyGraphMultipleTraces.Traces[1].YSource = "Input2Y";

			// Configure the shape radio buttons
			setShape(optShapeEllipse,xyGraphProperties);
			setShape(optShapeRectangle,xyGraphProperties);
			setShape(optShapeRoundedRectangle,xyGraphProperties);

			// Configure the border style radio buttons
			setBorderStyle(optBorderStyleNone,xyGraphProperties);
			setBorderStyle(optBorderStyleFlat,xyGraphProperties);
			setBorderStyle(optBorderStyleRaised,xyGraphProperties);
			setBorderStyle(optBorderStyleIndented,xyGraphProperties);
			setBorderStyle(optBorderStyleEditBox,xyGraphProperties);
			setBorderStyle(optBorderStyleRaised,xyGraphProperties);
			setBorderStyle(optBorderStyleShaded,xyGraphProperties);
			setBorderStyle(optBorderStyleShadedwithInset,xyGraphProperties);
			setBorderStyle(optBorderStyleRounded,xyGraphProperties);
			setBorderStyle(optBorderStyleRoundedwithInset,xyGraphProperties);	

			chkHorizontalGridEnabled_CheckedChanged(null, null);
			chkVerticalGridEnabled_CheckedChanged(null, null);
			chkenableThresholds_CheckedChanged(null, null);
			chkTraceLineVisible_CheckedChanged(null, null);
			chkFadeEnabled_CheckedChanged(null, null);
			chkShowMarks_CheckedChanged(null, null);
			chkPropertiesManualInput_CheckedChanged(null, null);
			
			setPenStyle(radioPenStyleDot, null);
			setPenStyle(radioPenStylePlus, null);
			setPenStyle(radioPenStyleCircle, null);
			setPenStyle(radioPenStyleOpenCircle, null);
			setPenStyle(radioPenStyleCross, null);
			setPenStyle(radioPenStyleMan, null);
			setPenStyle(radioPenStyleSquare, null);
			setPenStyle(radioPenStyleDiamond, null);

			setMarkStyle(radioMarkStyleDot, null);
			setMarkStyle(radioMarkStylePlus, null);
			setMarkStyle(radioMarkStyleCircle, null);
			setMarkStyle(radioMarkStyleOpenCircle, null);
			setMarkStyle(radioMarkStyleCross, null);
			setMarkStyle(radioMarkStyleMan, null);
			setMarkStyle(radioMarkStyleSquare, null);
			setMarkStyle(radioMarkStyleDiamond, null);

			barXYGraphPersistence_Scroll(null, null);

			setPhase(trackInput1XPhase, null);
			setPhase(trackInput1YPhase, null);
			setPhase(trackInput2XPhase, null);
			setPhase(trackInput2YPhase, null);

			setPeriod(trackInput1XPeriod, null);
			setPeriod(trackInput1YPeriod, null);
			setPeriod(trackInput2XPeriod, null);
			setPeriod(trackInput2YPeriod, null);

			configuration.Connect();
			xyGraphProperties.Connect();
			xyGraphMultipleTraces.Connect();


			// configure the information panels
			object zero = 0;
			object emptyString = "";	
			browseStdXYGraphInfo.Navigate(Application.StartupPath + "\\StandardXYGraphInfo.htm",ref zero, ref emptyString, ref emptyString, ref emptyString);			
			browseMultipleTracesInfo.Navigate(Application.StartupPath + "\\MultipleTraces.htm",ref zero, ref emptyString, ref emptyString, ref emptyString);			

			Demo_Resize(null, null);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{			
			this.components = new System.ComponentModel.Container();
			KineticaRT.Controls.SliderNeedle sliderNeedle1 = new KineticaRT.Controls.SliderNeedle();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(XYGraphDemo));
			KineticaRT.Controls.Threshold threshold1 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold2 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold3 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold4 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold5 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold6 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold7 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold8 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold9 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold10 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold11 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.Threshold threshold12 = new KineticaRT.Controls.Threshold();
			KineticaRT.Controls.XYTrace xyTrace1 = new KineticaRT.Controls.XYTrace();
			KineticaRT.Controls.XYTrace xyTrace2 = new KineticaRT.Controls.XYTrace();
			KineticaRT.Controls.XYTrace xyTrace3 = new KineticaRT.Controls.XYTrace();
			this.tabProperties = new System.Windows.Forms.TabPage();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.chkPropertiesManualInput = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.sliderPropertiesInput = new KineticaRT.Controls.SliderMultiNeedle();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabXYGraph = new System.Windows.Forms.TabPage();
			this.chkVerticalGridEnabled = new System.Windows.Forms.CheckBox();
			this.chkHorizontalGridEnabled = new System.Windows.Forms.CheckBox();
			this.chkenableThresholds = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.optBorderStyleRaised = new System.Windows.Forms.RadioButton();
			this.optBorderStyleRoundedwithInset = new System.Windows.Forms.RadioButton();
			this.optBorderStyleRounded = new System.Windows.Forms.RadioButton();
			this.optBorderStyleShadedwithInset = new System.Windows.Forms.RadioButton();
			this.optBorderStyleShaded = new System.Windows.Forms.RadioButton();
			this.optBorderStyleFlat = new System.Windows.Forms.RadioButton();
			this.optBorderStyleEditBox = new System.Windows.Forms.RadioButton();
			this.optBorderStyleIndented = new System.Windows.Forms.RadioButton();
			this.optBorderStyleNone = new System.Windows.Forms.RadioButton();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.optShapeEllipse = new System.Windows.Forms.RadioButton();
			this.optShapeRoundedRectangle = new System.Windows.Forms.RadioButton();
			this.optShapeRectangle = new System.Windows.Forms.RadioButton();
			this.tabTraces = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.lblXYGraphPersistence = new System.Windows.Forms.Label();
			this.barXYGraphPersistence = new System.Windows.Forms.TrackBar();
			this.chkShowMarks = new System.Windows.Forms.CheckBox();
			this.chkFadeEnabled = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioMarkStyleDiamond = new System.Windows.Forms.RadioButton();
			this.radioMarkStyleCross = new System.Windows.Forms.RadioButton();
			this.radioMarkStyleSquare = new System.Windows.Forms.RadioButton();
			this.radioMarkStyleMan = new System.Windows.Forms.RadioButton();
			this.radioMarkStyleOpenCircle = new System.Windows.Forms.RadioButton();
			this.radioMarkStyleCircle = new System.Windows.Forms.RadioButton();
			this.radioMarkStylePlus = new System.Windows.Forms.RadioButton();
			this.radioMarkStyleDot = new System.Windows.Forms.RadioButton();
			this.chkTraceLineVisible = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.radioPenStyleDiamond = new System.Windows.Forms.RadioButton();
			this.radioPenStyleCross = new System.Windows.Forms.RadioButton();
			this.radioPenStyleSquare = new System.Windows.Forms.RadioButton();
			this.radioPenStyleMan = new System.Windows.Forms.RadioButton();
			this.radioPenStyleOpenCircle = new System.Windows.Forms.RadioButton();
			this.radioPenStyleCircle = new System.Windows.Forms.RadioButton();
			this.radioPenStylePlus = new System.Windows.Forms.RadioButton();
			this.radioPenStyleDot = new System.Windows.Forms.RadioButton();
			this.cmdAddMark = new System.Windows.Forms.Button();
			this.groupXYGraphProperties = new System.Windows.Forms.GroupBox();
			this.propertiesGridStdXYGraph = new System.Windows.Forms.PropertyGrid();
			this.xyGraphProperties = new KineticaRT.Controls.XYGraph();
			this.cmdShowStdXYGraphInfo = new System.Windows.Forms.Button();
			this.cmdShowProperties = new System.Windows.Forms.Button();
			this.groupStdXYGraphInfo = new System.Windows.Forms.GroupBox();
			this.browseStdXYGraphInfo = new AxSHDocVw.AxWebBrowser();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.cmdShowHideMultipleTracesInfo = new System.Windows.Forms.Button();
			this.trackInput2YPhase = new System.Windows.Forms.TrackBar();
			this.trackInput2YPeriod = new System.Windows.Forms.TrackBar();
			this.trackInput2XPhase = new System.Windows.Forms.TrackBar();
			this.trackInput2XPeriod = new System.Windows.Forms.TrackBar();
			this.trackInput1YPhase = new System.Windows.Forms.TrackBar();
			this.trackInput1YPeriod = new System.Windows.Forms.TrackBar();
			this.trackInput1XPhase = new System.Windows.Forms.TrackBar();
			this.trackInput1XPeriod = new System.Windows.Forms.TrackBar();
			this.tabMultiTrace = new System.Windows.Forms.TabPage();
			this.xyGraphMultipleTraces = new KineticaRT.Controls.XYGraph();
			this.label16 = new System.Windows.Forms.Label();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.groupMultipleTracesInfo = new System.Windows.Forms.GroupBox();
			this.browseMultipleTracesInfo = new AxSHDocVw.AxWebBrowser();
			this.tabControl.SuspendLayout();
			this.tabProperties.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabXYGraph.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabTraces.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.barXYGraphPersistence)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupXYGraphProperties.SuspendLayout();
			this.groupStdXYGraphInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.browseStdXYGraphInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2YPhase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2YPeriod)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2XPhase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2XPeriod)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1YPhase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1YPeriod)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1XPhase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1XPeriod)).BeginInit();
			this.tabMultiTrace.SuspendLayout();
			this.groupBox8.SuspendLayout();
			this.groupBox7.SuspendLayout();
			this.groupMultipleTracesInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.browseMultipleTracesInfo)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdClose
			// 
			this.cmdClose.Location = new System.Drawing.Point(656, 504);
			this.cmdClose.Visible = true;
			// 
			// tabControl
			// 
			this.tabControl.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.tabProperties,
																					 this.tabMultiTrace});
			this.tabControl.ItemSize = new System.Drawing.Size(59, 18);
			this.tabControl.Size = new System.Drawing.Size(736, 488);
			this.tabControl.Visible = true;
			// 
			// tabProperties
			// 
			this.tabProperties.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.label15,
																						this.groupBox6,
																						this.tabControl1,
																						this.groupXYGraphProperties,
																						this.xyGraphProperties,
																						this.cmdShowStdXYGraphInfo,
																						this.cmdShowProperties,
																						this.groupStdXYGraphInfo});
			this.tabProperties.Location = new System.Drawing.Point(4, 22);
			this.tabProperties.Name = "tabProperties";
			this.tabProperties.Size = new System.Drawing.Size(728, 462);
			this.tabProperties.TabIndex = 0;
			this.tabProperties.Text = "Properties";
			// 
			// label15
			// 
			this.label15.BackColor = System.Drawing.Color.Transparent;
			this.label15.ForeColor = System.Drawing.Color.Black;
			this.label15.Location = new System.Drawing.Point(8, 8);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(360, 24);
			this.label15.TabIndex = 55;
			this.label15.Text = "An XY Graph has many properties and can be connected to other KineticaRT objects." +
				"";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.chkPropertiesManualInput,
																					this.label2,
																					this.label1,
																					this.sliderPropertiesInput});
			this.groupBox6.Location = new System.Drawing.Point(272, 224);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(80, 168);
			this.groupBox6.TabIndex = 54;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Inputs";
			// 
			// chkPropertiesManualInput
			// 
			this.chkPropertiesManualInput.Location = new System.Drawing.Point(8, 144);
			this.chkPropertiesManualInput.Name = "chkPropertiesManualInput";
			this.chkPropertiesManualInput.Size = new System.Drawing.Size(64, 16);
			this.chkPropertiesManualInput.TabIndex = 3;
			this.chkPropertiesManualInput.Text = "Manual";
			this.toolTip.SetToolTip(this.chkPropertiesManualInput, "Set if the XY Graph is driven manually / automatically");
			this.chkPropertiesManualInput.CheckedChanged += new System.EventHandler(this.chkPropertiesManualInput_CheckedChanged);
			// 
			// label2
			// 
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(48, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Y";
			// 
			// label1
			// 
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "X";
			// 
			// sliderPropertiesInput
			// 
			this.sliderPropertiesInput.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.sliderPropertiesInput.Location = new System.Drawing.Point(16, 32);
			this.sliderPropertiesInput.Name = "sliderPropertiesInput";
			sliderNeedle1.Color = System.Drawing.Color.Tomato;
			this.sliderPropertiesInput.Needles = new KineticaRT.Controls.SliderNeedleCollection(new KineticaRT.Controls.SliderNeedle[] {
																																		   sliderNeedle1,
																																		   new KineticaRT.Controls.SliderNeedle()});
			this.sliderPropertiesInput.Scale.MajorMarksRepeat = 20;
			this.sliderPropertiesInput.Size = new System.Drawing.Size(48, 104);
			this.sliderPropertiesInput.TabIndex = 0;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					  this.tabXYGraph,
																					  this.tabTraces});
			this.tabControl1.Location = new System.Drawing.Point(8, 224);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(256, 232);
			this.tabControl1.TabIndex = 53;
			// 
			// tabXYGraph
			// 
			this.tabXYGraph.Controls.AddRange(new System.Windows.Forms.Control[] {
																					 this.chkVerticalGridEnabled,
																					 this.chkHorizontalGridEnabled,
																					 this.chkenableThresholds,
																					 this.groupBox2,
																					 this.groupBox3});
			this.tabXYGraph.Location = new System.Drawing.Point(4, 22);
			this.tabXYGraph.Name = "tabXYGraph";
			this.tabXYGraph.Size = new System.Drawing.Size(248, 206);
			this.tabXYGraph.TabIndex = 0;
			this.tabXYGraph.Text = "XY Graph";
			this.tabXYGraph.ToolTipText = "Set xy graph properties";
			// 
			// chkVerticalGridEnabled
			// 
			this.chkVerticalGridEnabled.BackColor = System.Drawing.Color.Transparent;
			this.chkVerticalGridEnabled.Checked = true;
			this.chkVerticalGridEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkVerticalGridEnabled.ForeColor = System.Drawing.Color.Black;
			this.chkVerticalGridEnabled.Location = new System.Drawing.Point(8, 24);
			this.chkVerticalGridEnabled.Name = "chkVerticalGridEnabled";
			this.chkVerticalGridEnabled.Size = new System.Drawing.Size(160, 32);
			this.chkVerticalGridEnabled.TabIndex = 55;
			this.chkVerticalGridEnabled.Text = "Vertical Grid Enabled";
			this.toolTip.SetToolTip(this.chkVerticalGridEnabled, "Set if the xy graph\'s vertical grid is enabled");
			this.chkVerticalGridEnabled.CheckedChanged += new System.EventHandler(this.chkVerticalGridEnabled_CheckedChanged);
			// 
			// chkHorizontalGridEnabled
			// 
			this.chkHorizontalGridEnabled.BackColor = System.Drawing.Color.Transparent;
			this.chkHorizontalGridEnabled.Checked = true;
			this.chkHorizontalGridEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHorizontalGridEnabled.ForeColor = System.Drawing.Color.Black;
			this.chkHorizontalGridEnabled.Location = new System.Drawing.Point(88, 4);
			this.chkHorizontalGridEnabled.Name = "chkHorizontalGridEnabled";
			this.chkHorizontalGridEnabled.Size = new System.Drawing.Size(144, 32);
			this.chkHorizontalGridEnabled.TabIndex = 54;
			this.chkHorizontalGridEnabled.Text = "Horizontal Grid Enabled";
			this.toolTip.SetToolTip(this.chkHorizontalGridEnabled, "Set if the xy graph\'s horizontal grid is enabled");
			this.chkHorizontalGridEnabled.CheckedChanged += new System.EventHandler(this.chkHorizontalGridEnabled_CheckedChanged);
			// 
			// chkenableThresholds
			// 
			this.chkenableThresholds.BackColor = System.Drawing.Color.Transparent;
			this.chkenableThresholds.ForeColor = System.Drawing.Color.Black;
			this.chkenableThresholds.Location = new System.Drawing.Point(8, 8);
			this.chkenableThresholds.Name = "chkenableThresholds";
			this.chkenableThresholds.Size = new System.Drawing.Size(80, 24);
			this.chkenableThresholds.TabIndex = 51;
			this.chkenableThresholds.Text = "Thresholds";
			this.toolTip.SetToolTip(this.chkenableThresholds, "Set the xy graph\'s threshold marks on/off");
			this.chkenableThresholds.CheckedChanged += new System.EventHandler(this.chkenableThresholds_CheckedChanged);
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.Transparent;
			this.groupBox2.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.optBorderStyleRaised,
																					this.optBorderStyleRoundedwithInset,
																					this.optBorderStyleRounded,
																					this.optBorderStyleShadedwithInset,
																					this.optBorderStyleShaded,
																					this.optBorderStyleFlat,
																					this.optBorderStyleEditBox,
																					this.optBorderStyleIndented,
																					this.optBorderStyleNone});
			this.groupBox2.ForeColor = System.Drawing.Color.Black;
			this.groupBox2.Location = new System.Drawing.Point(8, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(232, 88);
			this.groupBox2.TabIndex = 53;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Border Style";
			this.toolTip.SetToolTip(this.groupBox2, "Set the xy graph\'s border style");
			// 
			// optBorderStyleRaised
			// 
			this.optBorderStyleRaised.Checked = true;
			this.optBorderStyleRaised.Location = new System.Drawing.Point(80, 38);
			this.optBorderStyleRaised.Name = "optBorderStyleRaised";
			this.optBorderStyleRaised.Size = new System.Drawing.Size(64, 24);
			this.optBorderStyleRaised.TabIndex = 4;
			this.optBorderStyleRaised.TabStop = true;
			this.optBorderStyleRaised.Tag = "1";
			this.optBorderStyleRaised.Text = "Raised";
			this.toolTip.SetToolTip(this.optBorderStyleRaised, "Set the xy graph\'s border style to raised");
			this.optBorderStyleRaised.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleRoundedwithInset
			// 
			this.optBorderStyleRoundedwithInset.Location = new System.Drawing.Point(152, 58);
			this.optBorderStyleRoundedwithInset.Name = "optBorderStyleRoundedwithInset";
			this.optBorderStyleRoundedwithInset.Size = new System.Drawing.Size(72, 32);
			this.optBorderStyleRoundedwithInset.TabIndex = 8;
			this.optBorderStyleRoundedwithInset.Tag = "2";
			this.optBorderStyleRoundedwithInset.Text = "Rounded with Inset";
			this.toolTip.SetToolTip(this.optBorderStyleRoundedwithInset, "Set the xy graph\'s border style to rounded with inset");
			this.optBorderStyleRoundedwithInset.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleRounded
			// 
			this.optBorderStyleRounded.Location = new System.Drawing.Point(152, 38);
			this.optBorderStyleRounded.Name = "optBorderStyleRounded";
			this.optBorderStyleRounded.Size = new System.Drawing.Size(72, 24);
			this.optBorderStyleRounded.TabIndex = 7;
			this.optBorderStyleRounded.Tag = "2";
			this.optBorderStyleRounded.Text = "Rounded";
			this.toolTip.SetToolTip(this.optBorderStyleRounded, "Set the xy graph\'s border style to rounded");
			this.optBorderStyleRounded.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleShadedwithInset
			// 
			this.optBorderStyleShadedwithInset.Location = new System.Drawing.Point(152, 12);
			this.optBorderStyleShadedwithInset.Name = "optBorderStyleShadedwithInset";
			this.optBorderStyleShadedwithInset.Size = new System.Drawing.Size(72, 32);
			this.optBorderStyleShadedwithInset.TabIndex = 6;
			this.optBorderStyleShadedwithInset.Tag = "2";
			this.optBorderStyleShadedwithInset.Text = "Shaded with Inset";
			this.toolTip.SetToolTip(this.optBorderStyleShadedwithInset, "Set the xy graph\'s border style to shaded with inset");
			this.optBorderStyleShadedwithInset.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleShaded
			// 
			this.optBorderStyleShaded.Location = new System.Drawing.Point(80, 62);
			this.optBorderStyleShaded.Name = "optBorderStyleShaded";
			this.optBorderStyleShaded.Size = new System.Drawing.Size(72, 24);
			this.optBorderStyleShaded.TabIndex = 5;
			this.optBorderStyleShaded.Tag = "2";
			this.optBorderStyleShaded.Text = "Shaded";
			this.toolTip.SetToolTip(this.optBorderStyleShaded, "Set the xy graph\'s border style to shaded");
			this.optBorderStyleShaded.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleFlat
			// 
			this.optBorderStyleFlat.Location = new System.Drawing.Point(8, 38);
			this.optBorderStyleFlat.Name = "optBorderStyleFlat";
			this.optBorderStyleFlat.Size = new System.Drawing.Size(56, 24);
			this.optBorderStyleFlat.TabIndex = 1;
			this.optBorderStyleFlat.Tag = "2";
			this.optBorderStyleFlat.Text = "Flat";
			this.toolTip.SetToolTip(this.optBorderStyleFlat, "Set the xy graph\'s border style to flat");
			this.optBorderStyleFlat.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleEditBox
			// 
			this.optBorderStyleEditBox.Location = new System.Drawing.Point(80, 16);
			this.optBorderStyleEditBox.Name = "optBorderStyleEditBox";
			this.optBorderStyleEditBox.Size = new System.Drawing.Size(72, 24);
			this.optBorderStyleEditBox.TabIndex = 3;
			this.optBorderStyleEditBox.Tag = "2";
			this.optBorderStyleEditBox.Text = "Edit Box";
			this.toolTip.SetToolTip(this.optBorderStyleEditBox, "Set the xy graph\'s border style to edit box");
			this.optBorderStyleEditBox.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleIndented
			// 
			this.optBorderStyleIndented.Location = new System.Drawing.Point(8, 62);
			this.optBorderStyleIndented.Name = "optBorderStyleIndented";
			this.optBorderStyleIndented.Size = new System.Drawing.Size(72, 24);
			this.optBorderStyleIndented.TabIndex = 2;
			this.optBorderStyleIndented.Tag = "2";
			this.optBorderStyleIndented.Text = "Indented";
			this.toolTip.SetToolTip(this.optBorderStyleIndented, "Set the xy graph\'s border style to indented");
			this.optBorderStyleIndented.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// optBorderStyleNone
			// 
			this.optBorderStyleNone.Location = new System.Drawing.Point(8, 16);
			this.optBorderStyleNone.Name = "optBorderStyleNone";
			this.optBorderStyleNone.Size = new System.Drawing.Size(56, 24);
			this.optBorderStyleNone.TabIndex = 0;
			this.optBorderStyleNone.Tag = "0";
			this.optBorderStyleNone.Text = "None";
			this.toolTip.SetToolTip(this.optBorderStyleNone, "Set the xy graph\'s border style to none");
			this.optBorderStyleNone.CheckedChanged += new System.EventHandler(this.setBorderStyle);
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.Color.Transparent;
			this.groupBox3.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.optShapeEllipse,
																					this.optShapeRoundedRectangle,
																					this.optShapeRectangle});
			this.groupBox3.ForeColor = System.Drawing.Color.Black;
			this.groupBox3.Location = new System.Drawing.Point(8, 56);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(232, 48);
			this.groupBox3.TabIndex = 52;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Shape";
			this.toolTip.SetToolTip(this.groupBox3, "Set the xy graph\'s shape");
			// 
			// optShapeEllipse
			// 
			this.optShapeEllipse.Location = new System.Drawing.Point(160, 16);
			this.optShapeEllipse.Name = "optShapeEllipse";
			this.optShapeEllipse.Size = new System.Drawing.Size(64, 24);
			this.optShapeEllipse.TabIndex = 2;
			this.optShapeEllipse.Tag = "0";
			this.optShapeEllipse.Text = "Ellipse";
			this.toolTip.SetToolTip(this.optShapeEllipse, "Set the xy graph\'s shape to ellipse");
			this.optShapeEllipse.CheckedChanged += new System.EventHandler(this.setShape);
			// 
			// optShapeRoundedRectangle
			// 
			this.optShapeRoundedRectangle.Location = new System.Drawing.Point(80, 12);
			this.optShapeRoundedRectangle.Name = "optShapeRoundedRectangle";
			this.optShapeRoundedRectangle.Size = new System.Drawing.Size(80, 32);
			this.optShapeRoundedRectangle.TabIndex = 1;
			this.optShapeRoundedRectangle.Tag = "2";
			this.optShapeRoundedRectangle.Text = "Rounded Rectangle";
			this.toolTip.SetToolTip(this.optShapeRoundedRectangle, "Set the xy graph\'s shape to rounded rectangle");
			this.optShapeRoundedRectangle.CheckedChanged += new System.EventHandler(this.setShape);
			// 
			// optShapeRectangle
			// 
			this.optShapeRectangle.Checked = true;
			this.optShapeRectangle.Location = new System.Drawing.Point(8, 16);
			this.optShapeRectangle.Name = "optShapeRectangle";
			this.optShapeRectangle.Size = new System.Drawing.Size(80, 24);
			this.optShapeRectangle.TabIndex = 0;
			this.optShapeRectangle.TabStop = true;
			this.optShapeRectangle.Tag = "1";
			this.optShapeRectangle.Text = "Rectangle";
			this.toolTip.SetToolTip(this.optShapeRectangle, "Set the xy graph\'s shape to rectangle");
			this.optShapeRectangle.CheckedChanged += new System.EventHandler(this.setShape);
			// 
			// tabTraces
			// 
			this.tabTraces.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.groupBox5,
																					this.chkShowMarks,
																					this.chkFadeEnabled,
																					this.groupBox1,
																					this.chkTraceLineVisible,
																					this.groupBox4,
																					this.cmdAddMark});
			this.tabTraces.Location = new System.Drawing.Point(4, 22);
			this.tabTraces.Name = "tabTraces";
			this.tabTraces.Size = new System.Drawing.Size(248, 206);
			this.tabTraces.TabIndex = 1;
			this.tabTraces.Text = "Trace";
			this.tabTraces.ToolTipText = "Set trace properties";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.lblXYGraphPersistence,
																					this.barXYGraphPersistence});
			this.groupBox5.Location = new System.Drawing.Point(8, 136);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(232, 64);
			this.groupBox5.TabIndex = 58;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Persist Time";
			// 
			// lblXYGraphPersistence
			// 
			this.lblXYGraphPersistence.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblXYGraphPersistence.Location = new System.Drawing.Point(120, 16);
			this.lblXYGraphPersistence.Name = "lblXYGraphPersistence";
			this.lblXYGraphPersistence.Size = new System.Drawing.Size(104, 24);
			this.lblXYGraphPersistence.TabIndex = 29;
			this.lblXYGraphPersistence.Text = "1 Second";
			this.toolTip.SetToolTip(this.lblXYGraphPersistence, "Trace persistance time");
			// 
			// barXYGraphPersistence
			// 
			this.barXYGraphPersistence.Location = new System.Drawing.Point(8, 16);
			this.barXYGraphPersistence.Maximum = 11;
			this.barXYGraphPersistence.Name = "barXYGraphPersistence";
			this.barXYGraphPersistence.Size = new System.Drawing.Size(112, 45);
			this.barXYGraphPersistence.TabIndex = 28;
			this.toolTip.SetToolTip(this.barXYGraphPersistence, "Set trace persistance time");
			this.barXYGraphPersistence.Value = 9;
			this.barXYGraphPersistence.Scroll += new System.EventHandler(this.barXYGraphPersistence_Scroll);
			// 
			// chkShowMarks
			// 
			this.chkShowMarks.Checked = true;
			this.chkShowMarks.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkShowMarks.Location = new System.Drawing.Point(88, 104);
			this.chkShowMarks.Name = "chkShowMarks";
			this.chkShowMarks.Size = new System.Drawing.Size(88, 16);
			this.chkShowMarks.TabIndex = 57;
			this.chkShowMarks.Text = "Show Marks";
			this.toolTip.SetToolTip(this.chkShowMarks, "Show trace marks");
			// 
			// chkFadeEnabled
			// 
			this.chkFadeEnabled.Checked = true;
			this.chkFadeEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFadeEnabled.Location = new System.Drawing.Point(8, 104);
			this.chkFadeEnabled.Name = "chkFadeEnabled";
			this.chkFadeEnabled.Size = new System.Drawing.Size(56, 16);
			this.chkFadeEnabled.TabIndex = 56;
			this.chkFadeEnabled.Text = "Fade";
			this.toolTip.SetToolTip(this.chkFadeEnabled, "Set trace fade on / off");
			this.chkFadeEnabled.CheckedChanged += new System.EventHandler(this.chkFadeEnabled_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.radioMarkStyleDiamond,
																					this.radioMarkStyleCross,
																					this.radioMarkStyleSquare,
																					this.radioMarkStyleMan,
																					this.radioMarkStyleOpenCircle,
																					this.radioMarkStyleCircle,
																					this.radioMarkStylePlus,
																					this.radioMarkStyleDot});
			this.groupBox1.Location = new System.Drawing.Point(96, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(88, 96);
			this.groupBox1.TabIndex = 55;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Mark Style";
			this.toolTip.SetToolTip(this.groupBox1, "Set Mark Style");
			// 
			// radioMarkStyleDiamond
			// 
			this.radioMarkStyleDiamond.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStyleDiamond.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStyleDiamond.Image")));
			this.radioMarkStyleDiamond.Location = new System.Drawing.Point(56, 64);
			this.radioMarkStyleDiamond.Name = "radioMarkStyleDiamond";
			this.radioMarkStyleDiamond.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStyleDiamond.TabIndex = 10;
			this.toolTip.SetToolTip(this.radioMarkStyleDiamond, "Set Mark Style to a Diamond");
			this.radioMarkStyleDiamond.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// radioMarkStyleCross
			// 
			this.radioMarkStyleCross.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStyleCross.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStyleCross.Image")));
			this.radioMarkStyleCross.Location = new System.Drawing.Point(32, 40);
			this.radioMarkStyleCross.Name = "radioMarkStyleCross";
			this.radioMarkStyleCross.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStyleCross.TabIndex = 9;
			this.toolTip.SetToolTip(this.radioMarkStyleCross, "Set Mark Style to a Cross");
			this.radioMarkStyleCross.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// radioMarkStyleSquare
			// 
			this.radioMarkStyleSquare.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStyleSquare.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStyleSquare.Image")));
			this.radioMarkStyleSquare.Location = new System.Drawing.Point(8, 64);
			this.radioMarkStyleSquare.Name = "radioMarkStyleSquare";
			this.radioMarkStyleSquare.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStyleSquare.TabIndex = 8;
			this.toolTip.SetToolTip(this.radioMarkStyleSquare, "Set Mark Style to a Square");
			this.radioMarkStyleSquare.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// radioMarkStyleMan
			// 
			this.radioMarkStyleMan.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStyleMan.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStyleMan.Image")));
			this.radioMarkStyleMan.Location = new System.Drawing.Point(56, 40);
			this.radioMarkStyleMan.Name = "radioMarkStyleMan";
			this.radioMarkStyleMan.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStyleMan.TabIndex = 7;
			this.toolTip.SetToolTip(this.radioMarkStyleMan, "Set Mark Style to a Man");
			this.radioMarkStyleMan.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// radioMarkStyleOpenCircle
			// 
			this.radioMarkStyleOpenCircle.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStyleOpenCircle.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStyleOpenCircle.Image")));
			this.radioMarkStyleOpenCircle.Location = new System.Drawing.Point(8, 40);
			this.radioMarkStyleOpenCircle.Name = "radioMarkStyleOpenCircle";
			this.radioMarkStyleOpenCircle.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStyleOpenCircle.TabIndex = 6;
			this.toolTip.SetToolTip(this.radioMarkStyleOpenCircle, "Set Mark Style to a Open Circle");
			this.radioMarkStyleOpenCircle.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// radioMarkStyleCircle
			// 
			this.radioMarkStyleCircle.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStyleCircle.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStyleCircle.Image")));
			this.radioMarkStyleCircle.Location = new System.Drawing.Point(56, 16);
			this.radioMarkStyleCircle.Name = "radioMarkStyleCircle";
			this.radioMarkStyleCircle.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStyleCircle.TabIndex = 5;
			this.toolTip.SetToolTip(this.radioMarkStyleCircle, "Set Mark Style to a Circle");
			this.radioMarkStyleCircle.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// radioMarkStylePlus
			// 
			this.radioMarkStylePlus.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStylePlus.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStylePlus.Image")));
			this.radioMarkStylePlus.Location = new System.Drawing.Point(32, 16);
			this.radioMarkStylePlus.Name = "radioMarkStylePlus";
			this.radioMarkStylePlus.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStylePlus.TabIndex = 4;
			this.toolTip.SetToolTip(this.radioMarkStylePlus, "Set Mark Style to a Plus");
			this.radioMarkStylePlus.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// radioMarkStyleDot
			// 
			this.radioMarkStyleDot.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioMarkStyleDot.Checked = true;
			this.radioMarkStyleDot.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioMarkStyleDot.Image")));
			this.radioMarkStyleDot.Location = new System.Drawing.Point(8, 16);
			this.radioMarkStyleDot.Name = "radioMarkStyleDot";
			this.radioMarkStyleDot.Size = new System.Drawing.Size(24, 24);
			this.radioMarkStyleDot.TabIndex = 3;
			this.radioMarkStyleDot.TabStop = true;
			this.toolTip.SetToolTip(this.radioMarkStyleDot, "Set Mark Style to a dot");
			this.radioMarkStyleDot.CheckedChanged += new System.EventHandler(this.setMarkStyle);
			// 
			// chkTraceLineVisible
			// 
			this.chkTraceLineVisible.Checked = true;
			this.chkTraceLineVisible.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkTraceLineVisible.Location = new System.Drawing.Point(8, 120);
			this.chkTraceLineVisible.Name = "chkTraceLineVisible";
			this.chkTraceLineVisible.Size = new System.Drawing.Size(88, 16);
			this.chkTraceLineVisible.TabIndex = 54;
			this.chkTraceLineVisible.Text = "Line Visible";
			this.toolTip.SetToolTip(this.chkTraceLineVisible, "Set the trace line visible / invisible");
			this.chkTraceLineVisible.CheckedChanged += new System.EventHandler(this.chkTraceLineVisible_CheckedChanged);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.radioPenStyleDiamond,
																					this.radioPenStyleCross,
																					this.radioPenStyleSquare,
																					this.radioPenStyleMan,
																					this.radioPenStyleOpenCircle,
																					this.radioPenStyleCircle,
																					this.radioPenStylePlus,
																					this.radioPenStyleDot});
			this.groupBox4.Location = new System.Drawing.Point(8, 8);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(88, 96);
			this.groupBox4.TabIndex = 53;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Pen Style";
			this.toolTip.SetToolTip(this.groupBox4, "Set Pen Style");
			// 
			// radioPenStyleDiamond
			// 
			this.radioPenStyleDiamond.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStyleDiamond.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStyleDiamond.Image")));
			this.radioPenStyleDiamond.Location = new System.Drawing.Point(56, 64);
			this.radioPenStyleDiamond.Name = "radioPenStyleDiamond";
			this.radioPenStyleDiamond.Size = new System.Drawing.Size(24, 24);
			this.radioPenStyleDiamond.TabIndex = 10;
			this.toolTip.SetToolTip(this.radioPenStyleDiamond, "Set Pen Style to a Diamond");
			this.radioPenStyleDiamond.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// radioPenStyleCross
			// 
			this.radioPenStyleCross.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStyleCross.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStyleCross.Image")));
			this.radioPenStyleCross.Location = new System.Drawing.Point(32, 40);
			this.radioPenStyleCross.Name = "radioPenStyleCross";
			this.radioPenStyleCross.Size = new System.Drawing.Size(24, 24);
			this.radioPenStyleCross.TabIndex = 9;
			this.toolTip.SetToolTip(this.radioPenStyleCross, "Set Pen Style to a Cross");
			this.radioPenStyleCross.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// radioPenStyleSquare
			// 
			this.radioPenStyleSquare.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStyleSquare.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStyleSquare.Image")));
			this.radioPenStyleSquare.Location = new System.Drawing.Point(8, 64);
			this.radioPenStyleSquare.Name = "radioPenStyleSquare";
			this.radioPenStyleSquare.Size = new System.Drawing.Size(24, 24);
			this.radioPenStyleSquare.TabIndex = 8;
			this.toolTip.SetToolTip(this.radioPenStyleSquare, "Set Pen Style to a Square");
			this.radioPenStyleSquare.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// radioPenStyleMan
			// 
			this.radioPenStyleMan.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStyleMan.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStyleMan.Image")));
			this.radioPenStyleMan.Location = new System.Drawing.Point(56, 40);
			this.radioPenStyleMan.Name = "radioPenStyleMan";
			this.radioPenStyleMan.Size = new System.Drawing.Size(24, 24);
			this.radioPenStyleMan.TabIndex = 7;
			this.toolTip.SetToolTip(this.radioPenStyleMan, "Set Pen Style to a Man");
			this.radioPenStyleMan.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// radioPenStyleOpenCircle
			// 
			this.radioPenStyleOpenCircle.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStyleOpenCircle.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStyleOpenCircle.Image")));
			this.radioPenStyleOpenCircle.Location = new System.Drawing.Point(8, 40);
			this.radioPenStyleOpenCircle.Name = "radioPenStyleOpenCircle";
			this.radioPenStyleOpenCircle.Size = new System.Drawing.Size(24, 24);
			this.radioPenStyleOpenCircle.TabIndex = 6;
			this.toolTip.SetToolTip(this.radioPenStyleOpenCircle, "Set Pen Style to a Open Circle");
			this.radioPenStyleOpenCircle.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// radioPenStyleCircle
			// 
			this.radioPenStyleCircle.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStyleCircle.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStyleCircle.Image")));
			this.radioPenStyleCircle.Location = new System.Drawing.Point(56, 16);
			this.radioPenStyleCircle.Name = "radioPenStyleCircle";
			this.radioPenStyleCircle.Size = new System.Drawing.Size(24, 24);
			this.radioPenStyleCircle.TabIndex = 5;
			this.toolTip.SetToolTip(this.radioPenStyleCircle, "Set Pen Style to a Circle");
			this.radioPenStyleCircle.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// radioPenStylePlus
			// 
			this.radioPenStylePlus.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStylePlus.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStylePlus.Image")));
			this.radioPenStylePlus.Location = new System.Drawing.Point(32, 16);
			this.radioPenStylePlus.Name = "radioPenStylePlus";
			this.radioPenStylePlus.Size = new System.Drawing.Size(24, 24);
			this.radioPenStylePlus.TabIndex = 4;
			this.toolTip.SetToolTip(this.radioPenStylePlus, "Set Pen Style to a Cross");
			this.radioPenStylePlus.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// radioPenStyleDot
			// 
			this.radioPenStyleDot.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioPenStyleDot.Checked = true;
			this.radioPenStyleDot.Image = ((System.Drawing.Bitmap)(resources.GetObject("radioPenStyleDot.Image")));
			this.radioPenStyleDot.Location = new System.Drawing.Point(8, 16);
			this.radioPenStyleDot.Name = "radioPenStyleDot";
			this.radioPenStyleDot.Size = new System.Drawing.Size(24, 24);
			this.radioPenStyleDot.TabIndex = 3;
			this.radioPenStyleDot.TabStop = true;
			this.toolTip.SetToolTip(this.radioPenStyleDot, "Set Pen Style to a Dot");
			this.radioPenStyleDot.CheckedChanged += new System.EventHandler(this.setPenStyle);
			// 
			// cmdAddMark
			// 
			this.cmdAddMark.Location = new System.Drawing.Point(192, 72);
			this.cmdAddMark.Name = "cmdAddMark";
			this.cmdAddMark.Size = new System.Drawing.Size(48, 32);
			this.cmdAddMark.TabIndex = 56;
			this.cmdAddMark.Text = "Set Mark";
			this.toolTip.SetToolTip(this.cmdAddMark, "Add a mark to the XY Graph trace");
			this.cmdAddMark.Click += new System.EventHandler(this.cmdAddMark_Click);
			// 
			// groupXYGraphProperties
			// 
			this.groupXYGraphProperties.Controls.AddRange(new System.Windows.Forms.Control[] {
																								 this.propertiesGridStdXYGraph});
			this.groupXYGraphProperties.Location = new System.Drawing.Point(376, 16);
			this.groupXYGraphProperties.Name = "groupXYGraphProperties";
			this.groupXYGraphProperties.Size = new System.Drawing.Size(280, 320);
			this.groupXYGraphProperties.TabIndex = 42;
			this.groupXYGraphProperties.TabStop = false;
			this.groupXYGraphProperties.Tag = "";
			this.groupXYGraphProperties.Text = "Control Properties";
			this.groupXYGraphProperties.Visible = false;
			// 
			// propertiesGridStdXYGraph
			// 
			this.propertiesGridStdXYGraph.CommandsVisibleIfAvailable = true;
			this.propertiesGridStdXYGraph.HelpVisible = false;
			this.propertiesGridStdXYGraph.LargeButtons = false;
			this.propertiesGridStdXYGraph.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertiesGridStdXYGraph.Location = new System.Drawing.Point(8, 16);
			this.propertiesGridStdXYGraph.Name = "propertiesGridStdXYGraph";
			this.propertiesGridStdXYGraph.SelectedObject = this.xyGraphProperties;
			this.propertiesGridStdXYGraph.Size = new System.Drawing.Size(248, 248);
			this.propertiesGridStdXYGraph.TabIndex = 0;
			this.propertiesGridStdXYGraph.Text = "StdDialPropertyGrid";
			this.propertiesGridStdXYGraph.ToolbarVisible = false;
			this.propertiesGridStdXYGraph.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertiesGridStdXYGraph.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// xyGraphProperties
			// 
			this.xyGraphProperties.BorderColor = System.Drawing.SystemColors.ControlDark;
			threshold1.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(91)), ((System.Byte)(242)), ((System.Byte)(96)));
			threshold1.Value = 25;
			threshold2.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(148)), ((System.Byte)(91)));
			threshold2.Value = 50;
			threshold3.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(91)), ((System.Byte)(91)));
			threshold3.Value = 75;
			this.xyGraphProperties.BottomScale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																				threshold1,
																																				threshold2,
																																				threshold3});
			this.xyGraphProperties.HorizontalGridColor = System.Drawing.Color.Teal;
			threshold4.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(91)), ((System.Byte)(242)), ((System.Byte)(96)));
			threshold4.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside;
			threshold4.Value = 25;
			threshold5.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(148)), ((System.Byte)(91)));
			threshold5.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside;
			threshold5.Value = 50;
			threshold6.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(91)), ((System.Byte)(91)));
			threshold6.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside;
			threshold6.Value = 75;
			this.xyGraphProperties.LeftScale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																			  threshold4,
																																			  threshold5,
																																			  threshold6});
			this.xyGraphProperties.Location = new System.Drawing.Point(8, 40);
			this.xyGraphProperties.Name = "xyGraphProperties";
			threshold7.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(91)), ((System.Byte)(242)), ((System.Byte)(96)));
			threshold7.Value = 25;
			threshold8.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(148)), ((System.Byte)(91)));
			threshold8.Value = 50;
			threshold9.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(91)), ((System.Byte)(91)));
			threshold9.Value = 75;
			this.xyGraphProperties.RightScale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																			   threshold7,
																																			   threshold8,
																																			   threshold9});
			this.xyGraphProperties.Shape = KineticaRT.Controls.Shapes.RoundedRectangle;
			this.xyGraphProperties.Size = new System.Drawing.Size(336, 176);
			this.xyGraphProperties.TabIndex = 45;
			threshold10.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(91)), ((System.Byte)(242)), ((System.Byte)(96)));
			threshold10.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside;
			threshold10.Value = 25;
			threshold11.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(148)), ((System.Byte)(91)));
			threshold11.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside;
			threshold11.Value = 50;
			threshold12.MarkColor = System.Drawing.Color.FromArgb(((System.Byte)(242)), ((System.Byte)(91)), ((System.Byte)(91)));
			threshold12.MarkPosition = KineticaRT.Controls.ScaleMarkPositions.Inside;
			threshold12.Value = 75;
			this.xyGraphProperties.TopScale.Thresholds = new KineticaRT.Controls.ThresholdCollection(new KineticaRT.Controls.Threshold[] {
																																			 threshold10,
																																			 threshold11,
																																			 threshold12});
			xyTrace1.Width = 3;
			this.xyGraphProperties.Traces = new KineticaRT.Controls.XYTraceCollection(new KineticaRT.Controls.XYTrace[] {
																															xyTrace1});
			this.xyGraphProperties.ObjectSearch += new KineticaRT.ObjectSearchEventHandler(this.configuration_ObjectSearch);
			this.xyGraphProperties.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.xyGraphProperties_PropertyChanged);
			// 
			// cmdShowStdXYGraphInfo
			// 
			this.cmdShowStdXYGraphInfo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdShowStdXYGraphInfo.Location = new System.Drawing.Point(272, 432);
			this.cmdShowStdXYGraphInfo.Name = "cmdShowStdXYGraphInfo";
			this.cmdShowStdXYGraphInfo.Size = new System.Drawing.Size(88, 24);
			this.cmdShowStdXYGraphInfo.TabIndex = 44;
			this.cmdShowStdXYGraphInfo.Text = "Information <<";
			this.toolTip.SetToolTip(this.cmdShowStdXYGraphInfo, "View information about the XY Graph Properties Demostration");
			this.cmdShowStdXYGraphInfo.Click += new System.EventHandler(this.cmdShowStdXYGraphInfo_Click);
			// 
			// cmdShowProperties
			// 
			this.cmdShowProperties.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdShowProperties.Location = new System.Drawing.Point(272, 400);
			this.cmdShowProperties.Name = "cmdShowProperties";
			this.cmdShowProperties.Size = new System.Drawing.Size(88, 24);
			this.cmdShowProperties.TabIndex = 43;
			this.cmdShowProperties.Text = "Properties >>";
			this.cmdShowProperties.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip.SetToolTip(this.cmdShowProperties, "View and modify the xy graph\'s properties");
			this.cmdShowProperties.Click += new System.EventHandler(this.cmdShowProperties_Click);
			// 
			// groupStdXYGraphInfo
			// 
			this.groupStdXYGraphInfo.Controls.AddRange(new System.Windows.Forms.Control[] {
																							  this.browseStdXYGraphInfo});
			this.groupStdXYGraphInfo.Location = new System.Drawing.Point(440, 8);
			this.groupStdXYGraphInfo.Name = "groupStdXYGraphInfo";
			this.groupStdXYGraphInfo.Size = new System.Drawing.Size(272, 280);
			this.groupStdXYGraphInfo.TabIndex = 41;
			this.groupStdXYGraphInfo.TabStop = false;
			this.groupStdXYGraphInfo.Text = "Information";
			// 
			// browseStdXYGraphInfo
			// 
			this.browseStdXYGraphInfo.ContainingControl = this;
			this.browseStdXYGraphInfo.Enabled = true;
			this.browseStdXYGraphInfo.Location = new System.Drawing.Point(8, 16);
			this.browseStdXYGraphInfo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("browseStdXYGraphInfo.OcxState")));
			this.browseStdXYGraphInfo.Size = new System.Drawing.Size(300, 150);
			this.browseStdXYGraphInfo.TabIndex = 0;
			// 
			// cmdShowHideMultipleTracesInfo
			// 
			this.cmdShowHideMultipleTracesInfo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdShowHideMultipleTracesInfo.Location = new System.Drawing.Point(272, 432);
			this.cmdShowHideMultipleTracesInfo.Name = "cmdShowHideMultipleTracesInfo";
			this.cmdShowHideMultipleTracesInfo.Size = new System.Drawing.Size(88, 24);
			this.cmdShowHideMultipleTracesInfo.TabIndex = 49;
			this.cmdShowHideMultipleTracesInfo.Text = "Information <<";
			this.toolTip.SetToolTip(this.cmdShowHideMultipleTracesInfo, "View information about the XY Graph Multiple Trace Demonstration");
			this.cmdShowHideMultipleTracesInfo.Click += new System.EventHandler(this.cmdShowMultipleTracesInfo_Click);
			// 
			// trackInput2YPhase
			// 
			this.trackInput2YPhase.Location = new System.Drawing.Point(48, 160);
			this.trackInput2YPhase.Name = "trackInput2YPhase";
			this.trackInput2YPhase.Size = new System.Drawing.Size(112, 45);
			this.trackInput2YPhase.TabIndex = 80;
			this.trackInput2YPhase.Tag = "Input2Y";
			this.toolTip.SetToolTip(this.trackInput2YPhase, "Set Trace 2\'s Y channel phase");
			this.trackInput2YPhase.Value = 6;
			this.trackInput2YPhase.Scroll += new System.EventHandler(this.setPhase);
			// 
			// trackInput2YPeriod
			// 
			this.trackInput2YPeriod.Location = new System.Drawing.Point(48, 120);
			this.trackInput2YPeriod.Name = "trackInput2YPeriod";
			this.trackInput2YPeriod.Size = new System.Drawing.Size(112, 45);
			this.trackInput2YPeriod.TabIndex = 76;
			this.trackInput2YPeriod.Tag = "Input2Y";
			this.toolTip.SetToolTip(this.trackInput2YPeriod, "Set Trace 2\'s Y channel period");
			this.trackInput2YPeriod.Value = 2;
			this.trackInput2YPeriod.Scroll += new System.EventHandler(this.setPeriod);
			// 
			// trackInput2XPhase
			// 
			this.trackInput2XPhase.Location = new System.Drawing.Point(48, 64);
			this.trackInput2XPhase.Name = "trackInput2XPhase";
			this.trackInput2XPhase.Size = new System.Drawing.Size(112, 45);
			this.trackInput2XPhase.TabIndex = 75;
			this.trackInput2XPhase.Tag = "Input2X";
			this.toolTip.SetToolTip(this.trackInput2XPhase, "Set Trace 2\'s X channel phase");
			this.trackInput2XPhase.Value = 4;
			this.trackInput2XPhase.Scroll += new System.EventHandler(this.setPhase);
			// 
			// trackInput2XPeriod
			// 
			this.trackInput2XPeriod.Location = new System.Drawing.Point(48, 32);
			this.trackInput2XPeriod.Name = "trackInput2XPeriod";
			this.trackInput2XPeriod.Size = new System.Drawing.Size(112, 45);
			this.trackInput2XPeriod.TabIndex = 66;
			this.trackInput2XPeriod.Tag = "Input2X";
			this.toolTip.SetToolTip(this.trackInput2XPeriod, "Set Trace 2\'s X channel period");
			this.trackInput2XPeriod.Value = 5;
			this.trackInput2XPeriod.Scroll += new System.EventHandler(this.setPeriod);
			// 
			// trackInput1YPhase
			// 
			this.trackInput1YPhase.Location = new System.Drawing.Point(48, 160);
			this.trackInput1YPhase.Name = "trackInput1YPhase";
			this.trackInput1YPhase.Size = new System.Drawing.Size(112, 45);
			this.trackInput1YPhase.TabIndex = 80;
			this.trackInput1YPhase.Tag = "Input1Y";
			this.toolTip.SetToolTip(this.trackInput1YPhase, "Set Trace 1\'s Y channel phase");
			this.trackInput1YPhase.Value = 8;
			this.trackInput1YPhase.Scroll += new System.EventHandler(this.setPhase);
			// 
			// trackInput1YPeriod
			// 
			this.trackInput1YPeriod.Location = new System.Drawing.Point(48, 120);
			this.trackInput1YPeriod.Name = "trackInput1YPeriod";
			this.trackInput1YPeriod.Size = new System.Drawing.Size(112, 45);
			this.trackInput1YPeriod.TabIndex = 76;
			this.trackInput1YPeriod.Tag = "Input1Y";
			this.toolTip.SetToolTip(this.trackInput1YPeriod, "Set Trace 1\'s Y channel period");
			this.trackInput1YPeriod.Value = 6;
			this.trackInput1YPeriod.Scroll += new System.EventHandler(this.setPeriod);
			// 
			// trackInput1XPhase
			// 
			this.trackInput1XPhase.Location = new System.Drawing.Point(48, 64);
			this.trackInput1XPhase.Name = "trackInput1XPhase";
			this.trackInput1XPhase.Size = new System.Drawing.Size(112, 45);
			this.trackInput1XPhase.TabIndex = 75;
			this.trackInput1XPhase.Tag = "Input1X";
			this.toolTip.SetToolTip(this.trackInput1XPhase, "Set Trace 1\'s X phase");
			this.trackInput1XPhase.Value = 2;
			this.trackInput1XPhase.Scroll += new System.EventHandler(this.setPhase);
			// 
			// trackInput1XPeriod
			// 
			this.trackInput1XPeriod.Location = new System.Drawing.Point(48, 32);
			this.trackInput1XPeriod.Name = "trackInput1XPeriod";
			this.trackInput1XPeriod.Size = new System.Drawing.Size(112, 45);
			this.trackInput1XPeriod.TabIndex = 66;
			this.trackInput1XPeriod.Tag = "Input1X";
			this.toolTip.SetToolTip(this.trackInput1XPeriod, "Set Trace 1\'s X channel period");
			this.trackInput1XPeriod.Value = 8;
			this.trackInput1XPeriod.Scroll += new System.EventHandler(this.setPeriod);
			// 
			// tabMultiTrace
			// 
			this.tabMultiTrace.Controls.AddRange(new System.Windows.Forms.Control[] {
																						this.xyGraphMultipleTraces,
																						this.label16,
																						this.groupBox8,
																						this.groupBox7,
																						this.cmdShowHideMultipleTracesInfo,
																						this.groupMultipleTracesInfo});
			this.tabMultiTrace.Location = new System.Drawing.Point(4, 22);
			this.tabMultiTrace.Name = "tabMultiTrace";
			this.tabMultiTrace.Size = new System.Drawing.Size(728, 462);
			this.tabMultiTrace.TabIndex = 1;
			this.tabMultiTrace.Text = "Multiple Traces";
			// 
			// xyGraphMultipleTraces
			// 
			this.xyGraphMultipleTraces.BorderColor = System.Drawing.SystemColors.ControlDark;
			this.xyGraphMultipleTraces.HorizontalGridColor = System.Drawing.Color.Teal;
			this.xyGraphMultipleTraces.Location = new System.Drawing.Point(8, 24);
			this.xyGraphMultipleTraces.Name = "xyGraphMultipleTraces";
			this.xyGraphMultipleTraces.Shape = KineticaRT.Controls.Shapes.RoundedRectangle;
			this.xyGraphMultipleTraces.Size = new System.Drawing.Size(352, 192);
			this.xyGraphMultipleTraces.TabIndex = 46;
			xyTrace2.Width = 3;
			xyTrace3.LineColor = System.Drawing.Color.Blue;
			xyTrace3.MarkStyle = KineticaRT.Controls.PenStyles.Circle;
			xyTrace3.PenColor = System.Drawing.Color.Blue;
			xyTrace3.Width = 3;
			this.xyGraphMultipleTraces.Traces = new KineticaRT.Controls.XYTraceCollection(new KineticaRT.Controls.XYTrace[] {
																																xyTrace2,
																																xyTrace3});
			this.xyGraphMultipleTraces.ObjectSearch += new KineticaRT.ObjectSearchEventHandler(this.configuration_ObjectSearch);
			// 
			// label16
			// 
			this.label16.BackColor = System.Drawing.Color.Transparent;
			this.label16.ForeColor = System.Drawing.Color.Black;
			this.label16.Location = new System.Drawing.Point(8, 8);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(360, 16);
			this.label16.TabIndex = 67;
			this.label16.Text = "The XY Graph can show multiple traces.";
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.trackInput2YPhase,
																					this.label9,
																					this.label10,
																					this.label11,
																					this.trackInput2YPeriod,
																					this.trackInput2XPhase,
																					this.label12,
																					this.label13,
																					this.label14,
																					this.trackInput2XPeriod});
			this.groupBox8.ForeColor = System.Drawing.Color.Blue;
			this.groupBox8.Location = new System.Drawing.Point(192, 216);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(168, 208);
			this.groupBox8.TabIndex = 66;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Trace 2";
			// 
			// label9
			// 
			this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label9.Location = new System.Drawing.Point(8, 168);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(40, 16);
			this.label9.TabIndex = 79;
			this.label9.Text = "Phase";
			// 
			// label10
			// 
			this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label10.Location = new System.Drawing.Point(8, 120);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(40, 16);
			this.label10.TabIndex = 78;
			this.label10.Text = "Period";
			// 
			// label11
			// 
			this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label11.Location = new System.Drawing.Point(8, 104);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(152, 16);
			this.label11.TabIndex = 77;
			this.label11.Text = "Y ";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label12
			// 
			this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label12.Location = new System.Drawing.Point(8, 72);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(40, 16);
			this.label12.TabIndex = 74;
			this.label12.Text = "Phase";
			// 
			// label13
			// 
			this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label13.Location = new System.Drawing.Point(8, 32);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(40, 16);
			this.label13.TabIndex = 73;
			this.label13.Text = "Period";
			// 
			// label14
			// 
			this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label14.Location = new System.Drawing.Point(8, 16);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(144, 16);
			this.label14.TabIndex = 68;
			this.label14.Text = "X ";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.AddRange(new System.Windows.Forms.Control[] {
																					this.trackInput1YPhase,
																					this.label6,
																					this.label7,
																					this.label8,
																					this.trackInput1YPeriod,
																					this.trackInput1XPhase,
																					this.label4,
																					this.label3,
																					this.label5,
																					this.trackInput1XPeriod});
			this.groupBox7.ForeColor = System.Drawing.Color.Red;
			this.groupBox7.Location = new System.Drawing.Point(16, 216);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(168, 208);
			this.groupBox7.TabIndex = 65;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Trace 1";
			// 
			// label6
			// 
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Location = new System.Drawing.Point(8, 160);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(40, 16);
			this.label6.TabIndex = 79;
			this.label6.Text = "Phase";
			// 
			// label7
			// 
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Location = new System.Drawing.Point(8, 120);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 16);
			this.label7.TabIndex = 78;
			this.label7.Text = "Period";
			// 
			// label8
			// 
			this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label8.Location = new System.Drawing.Point(8, 104);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(152, 16);
			this.label8.TabIndex = 77;
			this.label8.Text = "Y ";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Location = new System.Drawing.Point(8, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 74;
			this.label4.Text = "Phase";
			// 
			// label3
			// 
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(8, 32);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(40, 16);
			this.label3.TabIndex = 73;
			this.label3.Text = "Period";
			// 
			// label5
			// 
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Location = new System.Drawing.Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(144, 16);
			this.label5.TabIndex = 68;
			this.label5.Text = "X ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupMultipleTracesInfo
			// 
			this.groupMultipleTracesInfo.Controls.AddRange(new System.Windows.Forms.Control[] {
																								  this.browseMultipleTracesInfo});
			this.groupMultipleTracesInfo.Location = new System.Drawing.Point(376, 8);
			this.groupMultipleTracesInfo.Name = "groupMultipleTracesInfo";
			this.groupMultipleTracesInfo.Size = new System.Drawing.Size(272, 280);
			this.groupMultipleTracesInfo.TabIndex = 48;
			this.groupMultipleTracesInfo.TabStop = false;
			this.groupMultipleTracesInfo.Text = "Information";
			// 
			// browseMultipleTracesInfo
			// 
			this.browseMultipleTracesInfo.ContainingControl = this;
			this.browseMultipleTracesInfo.Enabled = true;
			this.browseMultipleTracesInfo.Location = new System.Drawing.Point(8, 16);
			this.browseMultipleTracesInfo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("browseMultipleTracesInfo.OcxState")));
			this.browseMultipleTracesInfo.Size = new System.Drawing.Size(300, 150);
			this.browseMultipleTracesInfo.TabIndex = 0;
			// 
			// XYGraphDemo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(760, 539);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.tabControl,
																		  this.cmdClose});
			this.MinimumSize = new System.Drawing.Size(402, 568);
			this.Name = "XYGraphDemo";
			this.Text = "KineticaRT XY Graph Component Demonstration";
			this.Resize += new System.EventHandler(this.Demo_Resize);
			this.tabControl.ResumeLayout(false);
			this.tabProperties.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabXYGraph.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabTraces.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.barXYGraphPersistence)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupXYGraphProperties.ResumeLayout(false);
			this.groupStdXYGraphInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.browseStdXYGraphInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2YPhase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2YPeriod)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2XPhase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput2XPeriod)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1YPhase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1YPeriod)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1XPhase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackInput1XPeriod)).EndInit();
			this.tabMultiTrace.ResumeLayout(false);
			this.groupBox8.ResumeLayout(false);
			this.groupBox7.ResumeLayout(false);
			this.groupMultipleTracesInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.browseMultipleTracesInfo)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			context = new ApplicationContext();

			//Assign the Idle event the handler so that you can close the splash form and show the main form after the application is run
			Application.Idle += new EventHandler(AppIdle);

			//Create splash form 
			splashForm = new SplashForm ();

			//Show splash form. 

			splashForm.Size = new System.Drawing.Size(1,1);									
			splashForm.Show();
			splashForm.Size = splashForm.Controls[0].Size; 

			// centre the form
			Screen screen = Screen.FromControl(splashForm);
			splashForm.Left = screen.WorkingArea.Width / 2 - (splashForm.Width / 2);
			splashForm.Top = screen.WorkingArea.Height / 2 - (splashForm.Height / 2);

			//Run the application passing the context
			Application.Run(context);
		}

		/***********************************************************************
		 * 
		 * NAME:		AppIdle
		 * 
		 * ABSTRACT:	Loads the demo and handles the display of the 
		 *				splash screen
		 * 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/
		
		private static void AppIdle(object sender, EventArgs e)	
		{
			//only run if the MainForm has not been set in the context
			if(context.MainForm == null)
			{
				//Remove the event handler because we only want this code to fire once. This saves performance because this event is called alot
				Application.Idle -= new EventHandler(AppIdle);

				int startTime = Environment.TickCount;

				//Set the main form				
				context.MainForm = new XYGraphDemo();				

				context.MainForm.Show();

				// display splash screen for a least 2 seconds
				int SplashScreenDisplayTime = 2000 - (Environment.TickCount - startTime);
				
				if (SplashScreenDisplayTime > 0) 
				{
					System.Threading.Thread.Sleep(SplashScreenDisplayTime);
				}				
				
				//Close the splash form
				splashForm.Close();

				//release the reference to the splashform
				splashForm = null;
			}
		} 

		/***********************************************************************
		 * 
		 * NAME:		Demo_Resize
		 * 
		 * ABSTRACT:	As the demo resizes size the controls within the form
		 *				appropriately	 
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		protected override void Demo_Resize(object sender, System.EventArgs e)
		{						
			// if the information area isn't displayed don't allow the form to be
			// resized
			if (bDisplayInfo == false)
			{
				this.Height = this.MinimumSize.Height;				
			}
			tabControl.Width = this.Width - 25;		
			tabControl.Height = this.Height - 55 - cmdClose.Height;
			cmdClose.Left = tabControl.Left + tabControl.Width - cmdClose.Width;
			cmdClose.Top = tabControl.Top + tabControl.Height + 10;
			
			if (tabControl.Width > groupStdXYGraphInfo.Left) 
			{
				groupStdXYGraphInfo.Top = 8;
				groupStdXYGraphInfo.Left  = 368;
				groupStdXYGraphInfo.Width = tabControl.Width - groupStdXYGraphInfo.Left - 15;
				groupStdXYGraphInfo.Height = tabControl.Height - groupStdXYGraphInfo.Top - 35;				
				browseStdXYGraphInfo.Width = groupStdXYGraphInfo.Width - 15;
				browseStdXYGraphInfo.Height = groupStdXYGraphInfo.Height - 25;

				// std xy graph properties
				MatchControlAspect(groupXYGraphProperties, groupStdXYGraphInfo);
				MatchControlAspect(propertiesGridStdXYGraph, browseStdXYGraphInfo);					

				// multiple traces info
				MatchControlAspect(groupMultipleTracesInfo, groupStdXYGraphInfo);
				MatchControlAspect(browseMultipleTracesInfo, browseStdXYGraphInfo);					

			}
		}

		/***********************************************************************
		 * 
		 * NAME:		chkenableThresholds_CheckedChanged
		 * 
		 * ABSTRACT:	Enable / Disbale the xy graph/s thresholds
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void chkenableThresholds_CheckedChanged(object sender, System.EventArgs e)
		{
			enableThresholds(chkenableThresholds.Checked, xyGraphProperties.LeftScale);
			enableThresholds(chkenableThresholds.Checked, xyGraphProperties.RightScale);
			enableThresholds(chkenableThresholds.Checked, xyGraphProperties.TopScale);
			enableThresholds(chkenableThresholds.Checked, xyGraphProperties.BottomScale);
		}

		/***********************************************************************
		 * 
		 * NAME:		xyGraphProperties_PropertyChanged
		 * 
		 * ABSTRACT:	Monitors any changes made to the standard xy graph and make
		 *				sure that the radio buttons reflect these changes
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void xyGraphProperties_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{				
				case "BorderStyle":
					switch (xyGraphProperties.BorderStyle) 
					{
						case KineticaRT.Controls.BorderStyles.EditBox:
							optBorderStyleEditBox.Checked = true;
							break;
						case KineticaRT.Controls.BorderStyles.Flat:
							optBorderStyleFlat.Checked = true;
							break;		
						case KineticaRT.Controls.BorderStyles.Indented:
							optBorderStyleIndented.Checked = true;
							break;
						case KineticaRT.Controls.BorderStyles.None:
							optBorderStyleNone.Checked = true;
							break;
						case KineticaRT.Controls.BorderStyles.Raised:
							optBorderStyleRaised.Checked = true;
							break;
						case KineticaRT.Controls.BorderStyles.Rounded:
							optBorderStyleRounded.Checked = true;
							break;
						case KineticaRT.Controls.BorderStyles.RoundedWithInset:
							optBorderStyleRoundedwithInset.Checked = true;
							break;
						case KineticaRT.Controls.BorderStyles.Shaded:
							optBorderStyleShaded.Checked = true;
							break;
						case KineticaRT.Controls.BorderStyles.ShadedWithInset:
							optBorderStyleShadedwithInset.Checked = true;
							break;
					}					
					break;
				case "Shape":
					switch (xyGraphProperties.Shape) 
					{
						case KineticaRT.Controls.Shapes.Ellipse:
							optShapeEllipse.Checked = true;
							break;
						case KineticaRT.Controls.Shapes.Rectangle:
							optShapeRectangle.Checked = true;
							break;
						case KineticaRT.Controls.Shapes.RoundedRectangle:
							optShapeRoundedRectangle.Checked = true;
							break;		
					}
					break;	
				case "Source":
					xyGraphProperties.Connect();
					break;
				case "HorizontalGridEnabled":
					chkHorizontalGridEnabled.Checked = xyGraphProperties.HorizontalGridEnabled;
					break;
				case "VerticalGridEnabled":
					chkVerticalGridEnabled.Checked = xyGraphProperties.VerticalGridEnabled;
					break;
				case "PenStyle":
					switch (xyGraphProperties.Traces[0].PenStyle) 
					{
						case KineticaRT.Controls.PenStyles.Dot:
							radioPenStyleDot.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Circle:
							radioPenStyleCircle.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Cross:
							radioPenStyleCross.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Diamond:
							radioPenStyleDiamond.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Man:
							radioPenStyleMan.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.OpenCircle:
							radioPenStyleOpenCircle.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Plus:
							radioPenStylePlus.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Square:
							radioPenStyleSquare.Checked = true;
							break;
					}
					break;
				case "MarkStyle":
					switch (xyGraphProperties.Traces[0].MarkStyle) 
					{
						case KineticaRT.Controls.PenStyles.Dot:
							radioMarkStyleDot.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Circle:
							radioMarkStyleCircle.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Cross:
							radioMarkStyleCross.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Diamond:
							radioMarkStyleDiamond.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Man:
							radioMarkStyleMan.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.OpenCircle:
							radioMarkStyleOpenCircle.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Plus:
							radioMarkStylePlus.Checked = true;
							break;
						case KineticaRT.Controls.PenStyles.Square:
							radioMarkStyleSquare.Checked = true;
							break;
					}
					break;
				case "LineVisible":
					chkTraceLineVisible.Checked = xyGraphProperties.Traces[0].LineVisible;
					break;
				case "Fade":
					chkFadeEnabled.Checked = xyGraphProperties.Traces[0].Fade;
					break;
				case "ShowMarks":
					chkShowMarks.Checked = xyGraphProperties.Traces[0].ShowMarks;
					break;
			}

		}

		/***********************************************************************
		 * 
		 * NAME:		chkHorizontalGridEnabled_CheckedChanged
		 * 
		 * ABSTRACT:	Sets the horizontal grid on the xy graph on / off
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void chkHorizontalGridEnabled_CheckedChanged(object sender, System.EventArgs e)
		{
			xyGraphProperties.HorizontalGridEnabled = chkHorizontalGridEnabled.Checked;
		}

		/***********************************************************************
		 * 
		 * NAME:		chkVerticalGridEnabled_CheckedChanged
		 * 
		 * ABSTRACT:	Sets the vertical grid on the xy graph on / off
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void chkVerticalGridEnabled_CheckedChanged(object sender, System.EventArgs e)
		{
			xyGraphProperties.VerticalGridEnabled = chkVerticalGridEnabled.Checked;
		}

		/***********************************************************************
		 * 
		 * NAME:		setBorderStyle
		 * 
		 * ABSTRACT:	Sets the border style of the standard xy graph	 		 
		 * 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void setBorderStyle(object sender, System.EventArgs e)
		{
			setBorderStyle(sender, xyGraphProperties);
		}

		/***********************************************************************
		 * 
		 * NAME:		setShape
		 * 
		 * ABSTRACT:	Sets the shape of the standard xy graph depending
		 *				on the value of the selected radio button's
		 *				tag property
		 * 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void setShape(object sender, System.EventArgs e)
		{
			setShape(sender, xyGraphProperties);
		}

		/***********************************************************************
		 * 
		 * NAME:		cmdShowProperties_Click
		 * 
		 * ABSTRACT:	Hide / displays the trend properties area
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void cmdShowProperties_Click(object sender, System.EventArgs e)
		{
			if (cmdShowProperties.Text == "Properties >>") 
			{
				groupStdXYGraphInfo.Visible = false;
				groupXYGraphProperties.Visible = true;
				groupXYGraphProperties.Tag = "Visible";
				cmdShowProperties.Text = "Properties <<";
				cmdShowStdXYGraphInfo.Text = "Information >>";
				HideShowInforArea(true);						
			}
			else 
			{
				HideShowInforArea(false);				
			}		
		}

		/***********************************************************************
		 * 
		 * NAME:		cmdShowStdXYGraphInfo_Click
		 * 
		 * ABSTRACT:	Hide / displays the standard xy graph information area
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void cmdShowStdXYGraphInfo_Click(object sender, System.EventArgs e)
		{
			if (cmdShowStdXYGraphInfo.Text == "Information >>") 
			{
				groupStdXYGraphInfo.Visible = true;
				groupXYGraphProperties.Visible = false;
				groupXYGraphProperties.Tag = "";
				cmdShowProperties.Text = "Properties >>";
				cmdShowStdXYGraphInfo.Text = "Information <<";
				HideShowInforArea(true);						
			}
			else 			{
				HideShowInforArea(false);				
			}
		}

		/***********************************************************************
		 * 
		 * NAME:		setPenStyle
		 * 
		 * ABSTRACT:	Sets the pen style of the xy graph trace
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void setPenStyle(object sender, System.EventArgs e) 
		{
			RadioButton radioButton = (RadioButton)sender;			
			if (radioButton.Checked) 
			{
				if (sender == radioPenStyleDot) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.Dot;
				}
				else if (sender == radioPenStylePlus) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.Plus;
				}
				else if (sender == radioPenStyleCircle) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.Circle;
				}
				else if (sender == radioPenStyleOpenCircle) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.OpenCircle;
				}
				else if (sender == radioPenStyleCross) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.Cross;
				}
				else if (sender == radioPenStyleMan) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.Man;
				}
				else if (sender == radioPenStyleSquare) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.Square;
				}
				else if (sender == radioPenStyleDiamond) 
				{
					xyGraphProperties.Traces[0].PenStyle= KineticaRT.Controls.PenStyles.Diamond;
				}
			}
		}

		/***********************************************************************
		 * 
		 * NAME:		setMarkStyle
		 * 
		 * ABSTRACT:	Sets the mark style of the xy graph trace
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void setMarkStyle(object sender, System.EventArgs e) 
		{
			RadioButton radioButton = (RadioButton)sender;			
			if (radioButton.Checked) 
			{
				if (sender == radioMarkStyleDot) 
				{
					xyGraphProperties.Traces[0].MarkStyle = KineticaRT.Controls.PenStyles.Dot;
				}
				else if (sender == radioMarkStylePlus) 
				{
					xyGraphProperties.Traces[0].MarkStyle= KineticaRT.Controls.PenStyles.Plus;
				}
				else if (sender == radioMarkStyleCircle) 
				{
					xyGraphProperties.Traces[0].MarkStyle= KineticaRT.Controls.PenStyles.Circle;
				}
				else if (sender == radioMarkStyleOpenCircle) 
				{
					xyGraphProperties.Traces[0].MarkStyle= KineticaRT.Controls.PenStyles.OpenCircle;
				}
				else if (sender == radioMarkStyleCross) 
				{
					xyGraphProperties.Traces[0].MarkStyle= KineticaRT.Controls.PenStyles.Cross;
				}
				else if (sender == radioMarkStyleMan) 
				{
					xyGraphProperties.Traces[0].MarkStyle= KineticaRT.Controls.PenStyles.Man;
				}
				else if (sender == radioMarkStyleSquare) 
				{
					xyGraphProperties.Traces[0].MarkStyle= KineticaRT.Controls.PenStyles.Square;
				}
				else if (sender == radioMarkStyleDiamond) 
				{
					xyGraphProperties.Traces[0].MarkStyle= KineticaRT.Controls.PenStyles.Diamond;
				}
			}
		}

		/***********************************************************************
		 * 
		 * NAME:		chkTraceLineVisible_CheckedChanged
		 * 
		 * ABSTRACT:	Sets the xy graph trace's line is visible
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void chkTraceLineVisible_CheckedChanged(object sender, System.EventArgs e)
		{
			xyGraphProperties.Traces[0].LineVisible = chkTraceLineVisible.Checked;
		}

		/***********************************************************************
		 * 
		 * NAME:		chkFadeEnabled_CheckedChanged
		 * 
		 * ABSTRACT:	Sets the xy graph trace's fade to enabled / disabled
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void chkFadeEnabled_CheckedChanged(object sender, System.EventArgs e)
		{
			xyGraphProperties.Traces[0].Fade = chkFadeEnabled.Checked;
		}

		/***********************************************************************
		 * 
		 * NAME:		chkShowMarks_CheckedChanged
		 * 
		 * ABSTRACT:	Sets the xy graph trace's marks are enabled / disabled
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void chkShowMarks_CheckedChanged(object sender, System.EventArgs e)
		{
			xyGraphProperties.Traces[0].ShowMarks = chkShowMarks.Checked;
		}

		/***********************************************************************
		 * 
		 * NAME:		barLowPassFilterTime_Scroll
		 * 
		 * ABSTRACT:	Configure the time period of the low pass filter based
		 *				on the scroll bar
		 *		 		 		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void barXYGraphPersistence_Scroll(object sender, System.EventArgs e)
		{			
			switch (barXYGraphPersistence.Value) 
			{
				case 0: // 100 milliseconds
					lblXYGraphPersistence.Text = "100 milliseconds";
					xyGraphProperties.Traces[0].PersistTime = 100;
					break;
				case 1: // 200 milliseconds
					lblXYGraphPersistence.Text = "200 milliseconds";
					xyGraphProperties.Traces[0].PersistTime = 200;
					break;
				case 2: // 350 milliseconds
					lblXYGraphPersistence.Text = "350 milliseconds";
					xyGraphProperties.Traces[0].PersistTime = 350;
					break;
				case 3: // 500 milliseconds
					lblXYGraphPersistence.Text = "500 milliseconds";
					xyGraphProperties.Traces[0].PersistTime = 500;
					break;
				case 4: // 750 milliseconds
					lblXYGraphPersistence.Text = "750 milliseconds";
					xyGraphProperties.Traces[0].PersistTime = 750;
					break;
				case 5: // 1 Second
					lblXYGraphPersistence.Text = "1 second";
					xyGraphProperties.Traces[0].PersistTime = 1000;
					break;
				case 6: // 1.5 seconds
					xyGraphProperties.Traces[0].PersistTime = 1500;
					lblXYGraphPersistence.Text = "1.5 seconds";
					break;
				case 7: // 2 seconds
					xyGraphProperties.Traces[0].PersistTime = 2000;
					lblXYGraphPersistence.Text = "2 seconds";
					break;
				case 8: // 3.5 seconds
					xyGraphProperties.Traces[0].PersistTime = 3500;
					lblXYGraphPersistence.Text = "3.5 seconds";
					break;
				case 9: // 5 seconds
					xyGraphProperties.Traces[0].PersistTime = 5000;
					lblXYGraphPersistence.Text = "5 seconds";
					break;
				case 10: // 7.5 seconds
					xyGraphProperties.Traces[0].PersistTime = 7500;
					lblXYGraphPersistence.Text = "7.5 seconds";
					break;
				case 11: // 10 seconds
					xyGraphProperties.Traces[0].PersistTime = 10000;
					lblXYGraphPersistence.Text = "10 seconds";
					break;
			}
		}

		/***********************************************************************
		 * 
		 * NAME:		chkPropertiesManualInput_CheckedChanged
		 * 
		 * ABSTRACT:	Set if the properties xy graph is manually / automatically
		 *				driven
		 *		 		 		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/		

		private void chkPropertiesManualInput_CheckedChanged(object sender, System.EventArgs e)
		{

			KineticaRT.Channels.Channel channel;			

			if (chkPropertiesManualInput.Checked) 
			{
				sliderPropertiesInput.Needles[0].SourceChannel = null;
				sliderPropertiesInput.Needles[1].SourceChannel = null;
				sliderPropertiesInput.Needles[0].Style = KineticaRT.Controls.SliderNeedleStyles.Handle;
				sliderPropertiesInput.Needles[1].Style = KineticaRT.Controls.SliderNeedleStyles.Handle;
			}
			else 
			{
				channel = (KineticaRT.Channels.Channel)configuration.Children.GetObject("PropertiesXAuto");
				sliderPropertiesInput.Needles[0].SourceChannel = channel;
				channel = (KineticaRT.Channels.Channel)configuration.Children.GetObject("PropertiesYAuto");
				sliderPropertiesInput.Needles[1].SourceChannel = channel;
				sliderPropertiesInput.Needles[0].Style = KineticaRT.Controls.SliderNeedleStyles.Arrow;
				sliderPropertiesInput.Needles[1].Style = KineticaRT.Controls.SliderNeedleStyles.Arrow;
			}
			
		}

		/***********************************************************************
		 * 
		 * NAME:		setPhase
		 * 
		 * ABSTRACT:	Set the sinewave's phase for the specifc trace's channel		 
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void setPhase(object sender, System.EventArgs e)
		{
			KineticaRT.Servers.Simulate.SineWave sineWave;
			System.Windows.Forms.TrackBar trackBar;
			trackBar = (System.Windows.Forms.TrackBar)sender;
			sineWave = (KineticaRT.Servers.Simulate.SineWave)configuration.Children.GetObject(trackBar.Tag.ToString());			
			
			double trackBarValue = (double)trackBar.Value;
			sineWave.Phase = trackBarValue / 10;
			configuration.Connect();
			xyGraphMultipleTraces.Connect();
		}

		/***********************************************************************
		 * 
		 * NAME:		setPeriod
		 * 
		 * ABSTRACT:	Set the sinewave's period for the specifc trace's channel		 
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void setPeriod(object sender, System.EventArgs e)
		{
			KineticaRT.Servers.Simulate.SineWave sineWave;
			System.Windows.Forms.TrackBar trackBar;		
			trackBar = (System.Windows.Forms.TrackBar)sender;
			sineWave = (KineticaRT.Servers.Simulate.SineWave)configuration.Children.GetObject(trackBar.Tag.ToString());			
	
			sineWave.Period = trackBar.Value;		
			configuration.Connect();
			xyGraphMultipleTraces.Connect();
		}


		/***********************************************************************
		 * 
		 * NAME:		cmdShowDragDropInfo_Click
		 * 
		 * ABSTRACT:	Hide / displays the button connection / drop and drag
		 *				information area
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void cmdShowMultipleTracesInfo_Click(object sender, System.EventArgs e)
		{
			Button button = (Button)sender;
			HideShowInforArea(button.Text == "Information >>", button);
		}

		/***********************************************************************
		 * 
		 * NAME:		UpdateInformationButtonLabels
		 * 
		 * ABSTRACT:	Update the information buttons displays with information >>
		 *				or information << depeding on the current state
		 *
		 * ARGUMENTS:	Label - What to set the information button labels to
		 *				button - Button pressed to modify the information area
		 *		 
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		protected override void UpdateInformationButtonLabels(string label, Button button)
		{					
			if (label == "Information <<")
			{
				if ((button != null) && (button.Text.StartsWith("Properties")))
				{
					// if have pressed a property butten to display the properties
					// then set the appropriate information button to information >>
					if ((label == "Information <<") && (button.Text == "Properties <<"))
					{
						if (button == cmdShowProperties)
						{					
							cmdShowStdXYGraphInfo.Text = "Information >>";
						}
					}
				}
				// if have pressed a information button to redisplay the information 
				// area then make sure the correct, info / property button is shown 
				// depending if the property group tag is set to 'Visible' or not

				if (groupXYGraphProperties.Tag.ToString() == "Visible") 
				{
					cmdShowProperties.Text = "Properties <<";
				}
				else 
				{
					cmdShowStdXYGraphInfo.Text = label;
				}
			}
			else 			
			{
				// If the info area is hidden set the properties and information 
				// button labels to >>
				cmdShowProperties.Text = "Properties >>";					
				cmdShowStdXYGraphInfo.Text = label;				
			}								
			cmdShowHideMultipleTracesInfo.Text = label;			
		}

		/***********************************************************************
		 * 
		 * NAME:		cmdAddMark_Click
		 * 
		 * ABSTRACT:	Add a mark to the XY Graph
		 *
		 * RETURNS:		void
		 * 
		 * NOTES:		None
		 * 
		 **********************************************************************/

		private void cmdAddMark_Click(object sender, System.EventArgs e)
		{
			xyGraphProperties.Traces[0].SetMark();
		}
	}
}

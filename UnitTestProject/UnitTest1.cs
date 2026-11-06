using System;
using System.Windows;

using EntitySpaces.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TestStack.White;
using TestStack.White.AutomationElementSearch;
using TestStack.White.Factory;
using TestStack.White.Drawing;
using TestStack.White.Sessions;
using TestStack.White.Configuration;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.TabItems;

using System.Diagnostics;
using System.Threading;
using System.Windows.Automation; //MS UI Automation test

using CEL.HMI;
using CEL.Library;

using KineticaRT.Sequence;
using KineticaRT.Servers.OpcClient;
using KineticaRT.Values;


namespace UnitTestProject
{

    [TestClass]
    public class UnitTest1 : KineticaRT.Sequence.Sequence
    {

//        private CELDAS m_CELProject;
        private readonly string _path = @"C:\My Projects\14105 TEI\HMI\PrjMaster_HMI.exe";
        private readonly string projectPassword = "14105";
        private KineticaRT.Studio.Application.Project project;
        private System.Collections.SortedList mNameList =  new System.Collections.SortedList();
        private Client mClient = new Client();
        private Group mGroup = new Group();
        private Item[] mDOUT = new Item[500];
        private Item[] mDIN = new Item[500];



        [TestMethod]
        public void ShouldWelcomeScreen()
        {
            string[] arguments = {"C:/My Projects/14105 TEI/HMI/PrjMaster_HMI.exe",
                                 "/master",
                                 "/simulation",
                                 "/password=14105",
                                 "/ksp=C:\\My Projects\\14105 TEI\\CustomPrj\\Config\\Prj14105_HMI.ksp",
                                 "/config=C:\\My Projects\\14105 TEI\\CustomPrj\\Config\\Prj14105_HMI.exe.config"};
            using (WhiteWrapper wrapper = new WhiteWrapper(_path, "/master /simulation /debug /password=14105 /ksp=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.ksp\"  /config=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.exe.config\""))
            {
                Window window = wrapper.GetWindowWithTimeout("Welcome Screen", 20);
                Assert.IsTrue(window.DisplayState == DisplayState.Restored);
            }
        }
                
        [TestMethod]
        public void ShouldUpdateLabelWhenButtonIsClicked()
        {
            CoreAppXmlConfiguration.Instance.BusyTimeout = 10000;
            using (WhiteWrapper wrapper = new WhiteWrapper(_path, "/master /simulation /debug /password=14105 /ksp=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.ksp\"  /config=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.exe.config\""))
            {
                Window windowWelcomeScreen;
                //                for(int i=0; i<10;i++)
                {
                    windowWelcomeScreen = wrapper.GetWindowWithTimeout("Welcome Screen", 20);

                    Label label = windowWelcomeScreen.Get<Label>("RevisionNumberTxt");
                    Assert.IsNotNull(label);
                    Assert.AreEqual("Software Revision N/C", label.Text);

                    label = windowWelcomeScreen.Get<Label>("ProjectNumberTxt");
                    Assert.IsNotNull(label);
                    Assert.AreEqual("Project 14105", label.Text);

                    label = windowWelcomeScreen.Get<Label>("lblX64Edition");
                    Assert.IsNotNull(label);
                    Assert.AreEqual("x64 Edition", label.Text);

                    MyStudioButton btnOpenTest = windowWelcomeScreen.Get<MyStudioButton>("btnOpenTest");
                    Assert.IsNotNull(btnOpenTest);
                    while (!btnOpenTest.Enabled)
                        Thread.Sleep(1000);

                    wrapper.WaitForStatusWithTimeout("IDLING", 20);

                    btnOpenTest.Click();

                    Window childWindowTestAndPassword = windowWelcomeScreen.ModalWindow("Test and Password");
                    Assert.IsNotNull(childWindowTestAndPassword);
                    TextBox txtPassword = childWindowTestAndPassword.Get<TextBox>("Password_TextBox");
                    Assert.IsNotNull(txtPassword);
                    txtPassword.Text = projectPassword;

                    ComboBox engineList = childWindowTestAndPassword.Get<ComboBox>("EngineModel_List");
//                    ComboBox engineList = window.gGetControl("EngineModel_List");
                    Assert.IsNotNull(engineList);
                    engineList.Select("T700-401C");

                    MyStudioButton btnOkPassord = childWindowTestAndPassword.Get<MyStudioButton>("btnOkPassword");
                    Assert.IsNotNull(btnOkPassord);
                    btnOkPassord.Click();

                    Window childWindowUserInputs = wrapper.GetChildWindow(windowWelcomeScreen, "User Inputs");
                    Assert.IsNotNull(childWindowUserInputs);

                    MyStudioButton btnOkUserInputs = childWindowUserInputs.Get<MyStudioButton>("btnOkUserInputs");
                    Assert.IsNotNull(btnOkUserInputs);
                    btnOkUserInputs.Click();

                    //Send ENTER to popup window
                    Window window = wrapper.GetWindowWithTimeout("CEL POPUP WINDOW", 20);
                    Assert.IsNotNull(window);
                    window.LogStructure();

                    Button btnOK = window.Get<Button>("2");//AutomationId du bouton OK est 2
                    Assert.IsNotNull(btnOK);
                    btnOK.Click();

                    wrapper.WaitForStatusWithTimeout("RUNNING", 20);

                    window = wrapper.GetWindowWithTimeout("T700 Critical", 20);

                    MyStudioButton btnCloseTest = wrapper.GetControl<MyStudioButton>(window, "Button4");
                    Assert.IsNotNull(btnCloseTest);
                    btnCloseTest.Click();

                    wrapper.WaitForStatusWithTimeout("IDLING", 20);
                }
            }
        }

        [TestMethod]
        public void TestHMI14105Facility()
        {
            CoreAppXmlConfiguration.Instance.BusyTimeout = 10000;

            BuildAndConnectCLient();
            BuildAndConnectGroup();
            BuildAndConnectDout();
            BuildAndConnectDin();
            Item lIndCOOLING_TOWER_FAN = BuildAndConnectItem("COOLING_TOWER_FAN_IND", "COOLING_TOWER_FAN_IND");


/*            for (int i = 0; i < 10; i++)
            {
                mDIN[303].Value = Values.True;
                System.Threading.Thread.Sleep(20000);
                //                    Assert.AreEqual(mDIN[303].Value, Values.True);
                mDIN[304].Value = Values.True;
                System.Threading.Thread.Sleep(10000);
                //                   Assert.AreEqual(mDIN[304].Value, Values.False);
                mDIN[303].Value = Values.True;
                System.Threading.Thread.Sleep(10000);
                //                    Assert.AreEqual(mDIN[303].Value, Values.False);
                mDIN[304].Value = Values.True;
                System.Threading.Thread.Sleep(10000);
                //                    Assert.AreEqual(mDIN[304].Value, Values.True);
            }
*/

            // Start CELDAS
            using (WhiteWrapper wrapper = new WhiteWrapper(_path, "/master /simulation /debug /password=14105 /ksp=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.ksp\"  /config=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.exe.config\""))
            {
                Window wind;
                Window wWelcomeScreen;
                Window wUserInput;
                Window wCELPopup;

//                KineticaRT.Studio.Application.Project itsKineticaRTProject = (KineticaRT.Studio.Application.Project)GetObject("Project");

                // Wait for CELDAS to load
                wrapper.WaitForStatusWithTimeout("IDLING", 20);

                // Get welcome screen handle
                wWelcomeScreen = wrapper.GetWindowWithTimeout("Welcome Screen", 20);


                // Verify the project number
                Label label = wrapper.GetControl<Label>(wWelcomeScreen, "ProjectNumberTxt");
                Assert.IsNotNull(label);
                Assert.AreEqual("Project 14105", label.Text);

                label = wrapper.GetControl<Label>(wWelcomeScreen, "RevisionNumberTxt");
                Assert.IsNotNull(label);
                Assert.AreEqual("Software Revision N/C", label.Text);

                // Select the test button
                MyStudioButton btnOpenTest = wrapper.GetControl<MyStudioButton>(wWelcomeScreen, "btnOpenTest");
                Assert.IsNotNull(btnOpenTest);
                while (!btnOpenTest.Enabled)
                    Thread.Sleep(1000);
                btnOpenTest.Click();

                Window childWindow = wrapper.GetChildWindow(wWelcomeScreen, "Test and Password");
                Assert.IsNotNull(childWindow);
                TextBox txtPassword = wrapper.GetControl<TextBox>(childWindow, "Password_TextBox");
                Assert.IsNotNull(txtPassword);
                txtPassword.Text = projectPassword;

                ComboBox engineList = wrapper.GetControl<ComboBox>(childWindow, "EngineModel_List");
                Assert.IsNotNull(engineList);
                engineList.Select("T700-701D");

                MyStudioButton btnOkPassord = wrapper.GetControl<MyStudioButton>(childWindow, "btnOkPassword");
                Assert.IsNotNull(btnOkPassord);
                btnOkPassord.Click();

                wUserInput = wrapper.GetChildWindow(wWelcomeScreen, "User Inputs");
                Assert.IsNotNull(wUserInput);

                MyStudioButton btnOkUserInputs = wrapper.GetControl<MyStudioButton>(wUserInput, "btnOkUserInputs");
                Assert.IsNotNull(btnOkUserInputs);
                btnOkUserInputs.Click();

                wCELPopup = wrapper.GetWindowWithTimeout("CEL POPUP WINDOW", 20);

                Button btnOK = wCELPopup.Get<Button>("2");//AutomationId du bouton OK est 2
                Assert.IsNotNull(btnOK);
                btnOK.Click();

                wrapper.WaitForStatusWithTimeout("RUNNING", 20);

                Window criticalWindow = wrapper.GetWindowWithTimeout("T700 Critical", 20);
                criticalWindow.Focus();

                MyStudioDisplay display2 = wrapper.GetControl<MyStudioDisplay>(criticalWindow, "Display2");
                Assert.IsNotNull(display2);
                Assert.AreEqual("T700-701D", display2.Name);

//                MyStudioButton btnDiagnosticOpen = wrapper.GetControl<MyStudioButton>(criticalWindow, "btnDIAGNOSTIC_ENGINE_OPEN_WINDOW");
//                Assert.IsNotNull(btnDiagnosticOpen);
//                btnDiagnosticOpen.Click();

                System.Boolean aa;

                System.Threading.Thread.Sleep(20000);
                aa = mDIN[303].Value == 0.0;
                System.Threading.Thread.Sleep(20000);
                aa = mDIN[304].Value == 0.0;
                System.Threading.Thread.Sleep(20000);

                aa = mDIN[303].Value == 0.0;
                System.Threading.Thread.Sleep(20000);
                aa = mDIN[304].Value == 0.0;
                System.Threading.Thread.Sleep(20000);

                // OPen Facility window
                MyStudioButton btnFCSOpen = wrapper.GetControl<MyStudioButton>(criticalWindow, "btnFACILITY_OPEN_WINDOW");
                Assert.IsNotNull(btnFCSOpen);
                btnFCSOpen.Click();


/*                MyStudioButton btnAntiIce = wrapper.GetControl<MyStudioButton>(criticalWindow, "btnANTI_ICE");
                Assert.IsNotNull(btnAntiIce);
                bool f = btnAntiIce.Enabled;
                string b = mdout104.Value.ToString();
                btnAntiIce.Click();
                System.Threading.Thread.Sleep(500);
                b = mdout104.Value.ToString();
                btnAntiIce.Click();
                System.Threading.Thread.Sleep(500);
                b = mdout104.Value.ToString();
*/

                Window fcsWindow = wrapper.GetChildWindow(criticalWindow, "FCS");
                fcsWindow.Focus();

                // Test the Cooling tower fan button and its DOUT 302
/*                MyStudioButton btnCOOLING_TOWER_FAN = wrapper.GetControl<MyStudioButton>(fcsWindow, "btnCOOLING_TOWER_FAN");
                Assert.IsNotNull(btnCOOLING_TOWER_FAN);
                Assert.AreEqual(mDOUT[302].Value, Values.False);
                btnCOOLING_TOWER_FAN.Click();
                System.Threading.Thread.Sleep(500); // To give time to the new dout value to go to QNX and back.
                Assert.AreEqual(mDOUT[302].Value, Values.True);
                btnCOOLING_TOWER_FAN.Click();
                System.Threading.Thread.Sleep(500);
                Assert.AreEqual(mDOUT[302].Value, Values.False);
*/
                Item a = lIndCOOLING_TOWER_FAN;
                while (mDIN[201].Value == Values.False)
                    System.Console.WriteLine("DIN 200 est false");

                for (int i = 0; i < 10; i++)
                {
                    mDIN[303].Value = Values.True;
                    System.Threading.Thread.Sleep(20000);
//                    Assert.AreEqual(mDIN[303].Value, Values.True);
                    mDIN[304].Value = Values.False;
                    System.Threading.Thread.Sleep(10000);
 //                   Assert.AreEqual(mDIN[304].Value, Values.False);
                    mDIN[303].Value = Values.False;
                    System.Threading.Thread.Sleep(10000);
//                    Assert.AreEqual(mDIN[303].Value, Values.False);
                    mDIN[304].Value = Values.True;
                    System.Threading.Thread.Sleep(10000);
//                    Assert.AreEqual(mDIN[304].Value, Values.True);
                }


                // Test the Water supply pump button and dout 203
/*                MyStudioButton btnDYNO_WATER_SUPPLY_PUMP = wrapper.GetControl<MyStudioButton>(fcsWindow, "btnDYNO_WATER_SUPPLY_PUMP");
                Assert.IsNotNull(btnDYNO_WATER_SUPPLY_PUMP);
                Assert.AreEqual(mDOUT[203].Value, Values.False);
                btnDYNO_WATER_SUPPLY_PUMP.Click();
                System.Threading.Thread.Sleep(500); // To give time to the new dout value to go to QNX and back.
                Assert.AreEqual(mDOUT[203].Value, Values.True);
                btnDYNO_WATER_SUPPLY_PUMP.Click();
                System.Threading.Thread.Sleep(500);
                Assert.AreEqual(mDOUT[203].Value, Values.False);
*/
                // Test the Cooling tower pump button and DOUT 302
                MyStudioButton btnCOOLING_TOWER_PUMP = wrapper.GetControl<MyStudioButton>(fcsWindow, "btnCOOLING_TOWER_PUMP");
                Assert.IsNotNull(btnCOOLING_TOWER_PUMP);
                Assert.AreEqual(mDOUT[301].Value, Values.False);
                btnCOOLING_TOWER_PUMP.Click();
                System.Threading.Thread.Sleep(1000); // To give time to the new dout value to go to QNX and back.
                Assert.AreEqual(mDOUT[301].Value, Values.True);
                btnCOOLING_TOWER_PUMP.Click();
                System.Threading.Thread.Sleep(1000);
                Assert.AreEqual(mDOUT[301].Value, Values.False);

                // Test the Louver exaust button and DOUT 206
                MyStudioButton btnLOUVER_EXHAUST = wrapper.GetControl<MyStudioButton>(fcsWindow, "btnLOUVER_EXHAUST");
                Assert.IsNotNull(btnLOUVER_EXHAUST);
                Assert.AreEqual(mDOUT[206].Value, Values.False);
                btnLOUVER_EXHAUST.Click();
                System.Threading.Thread.Sleep(1000); // To give time to the new dout value to go to QNX and back.
                Assert.AreEqual(mDOUT[206].Value, Values.True);
                btnLOUVER_EXHAUST.Click();
                System.Threading.Thread.Sleep(1000);
                Assert.AreEqual(mDOUT[206].Value, Values.False);

                // Test the intake heater button and DOUT 210
/*                MyStudioButton btnINTAKE_HEATER_BTN = wrapper.GetControl<MyStudioButton>(fcsWindow, "btnINTAKE_HEATER_BTN");
                Assert.IsNotNull(btnINTAKE_HEATER_BTN);
                Assert.AreEqual(mDOUT[210].Value, Values.False);
                btnINTAKE_HEATER_BTN.Click();
                System.Threading.Thread.Sleep(500); // To give time to the new dout value to go to QNX and back.
                Assert.AreEqual(mDOUT[210].Value, Values.True);
                btnINTAKE_HEATER_BTN.Click();
                System.Threading.Thread.Sleep(500);
                Assert.AreEqual(mDOUT[210].Value, Values.False);
*/
                // Test the louver intake button and DOUT 207
                MyStudioButton btnLOUVER_INTAKE = wrapper.GetControl<MyStudioButton>(fcsWindow, "btnLOUVER_INTAKE");
                Assert.IsNotNull(btnLOUVER_INTAKE);
                Assert.AreEqual(mDOUT[207].Value, Values.False);
                btnLOUVER_INTAKE.Click();
                System.Threading.Thread.Sleep(1000); // To give time to the new dout value to go to QNX and back.
                Assert.AreEqual(mDOUT[207].Value, Values.True);
                btnLOUVER_INTAKE.Click();
                System.Threading.Thread.Sleep(1000);
                Assert.AreEqual(mDOUT[207].Value, Values.False);
                
                


                
                Window systemCheckWindow = wrapper.GetWindowWithTimeout("System Check", 20);

                systemCheckWindow.Focus();
/*
                Panel DASPanel = systemCheckWindow.Get<Panel>("DAS_Channels_Tab");

                System.Windows.Point loc = DASPanel.Location;
                loc.X += 10;
                loc.Y += 10;
                TestStack.White.InputDevices.Mouse.Instance.Location = loc;
                TestStack.White.InputDevices.Mouse.Instance.Click();
*/
//                MyStudioButton btnDin204 = wrapper.GetControl<MyStudioButton>(systemCheckWindow, "btnDIN34"); // Small Door
//                MyStudioButton btnDin205 = wrapper.GetControl<MyStudioButton>(systemCheckWindow, "btnDIN35"); // Large Door 1
//                MyStudioButton btnDin206 = wrapper.GetControl<MyStudioButton>(systemCheckWindow, "btnDIN36"); // Large Door 2
 //               Button buttonDin204 = systemCheckWindow.Get<Button>(SearchCriteria.ByText("DIN204")); // Small Door
/*
                var btnDin204 = systemCheckWindow.Get(SearchCriteria.ByText("DIN204")); // Small Door
                btnDin204.Click();
                System.Threading.Thread.Sleep(500);
                double a = mDIN[204].Value.ToDouble();
               
                btnDin204.Click();
                System.Threading.Thread.Sleep(500);
                a = mDIN[204].Value.ToDouble();
*/
///                MyStudioButton btnDin205 = systemCheckWindow.Get<MyStudioButton>(SearchCriteria.ByText("DIN205")); // Large Door 1
///                MyStudioButton btnDin206 = systemCheckWindow.Get<MyStudioButton>(SearchCriteria.ByText("DIN206")); // Large Door 2

 ///               MyLampIndicator din204Light = systemCheckWindow.Get<MyLampIndicator>("LampIndicator34");
///                MyLampIndicator din205Light = systemCheckWindow.Get<MyLampIndicator>("LampIndicator35");
///                MyLampIndicator din206Light = systemCheckWindow.Get<MyLampIndicator>("LampIndicator36");

 //               MyStudioButton nameDin204 = systemCheckWindow.Get<MyStudioButton>(SearchCriteria.ByText("DIN204")); // Small Door
                //               MyStudioButton name2Din204 = systemCheckWindow.Get<MyStudioButton>("Din204"); LampIndicator34
//                String name = btnDin204.Name;

//                Window diagnoticWindow = wrapper.GetChildWindow(criticalWindow, "Diagnostic");
//                MyLampIndicator indDoorStatus = wrapper.GetControl<MyLampIndicator>(diagnoticWindow, "indDoorStatus");
//                MyLampIndicator indDoorStatus_01 = wrapper.GetControl<MyLampIndicator>(diagnoticWindow, "indDoorStatus_01");
//                MyLampIndicator indDoorStatus_03 = wrapper.GetControl<MyLampIndicator>(diagnoticWindow, "indDoorStatus_03");


                MyStudioButton btnCloseTest = wrapper.GetControl<MyStudioButton>(systemCheckWindow, "Close_Button");
                Assert.IsNotNull(btnCloseTest);
                btnCloseTest.Click();

                wrapper.WaitForStatusWithTimeout("IDLING", 20);

//                MyStudioButton btnClose = wrapper.GetControl<MyStudioButton>(wWelcomeScreen, "Close");
                Button btnClose = wWelcomeScreen.Get<Button>("Close");//AutomationId du bouton OK est 2

                btnClose.Click();

 //               wrapper.SaveCache();

                System.Threading.Thread.Sleep(4000);

                
            }
        }

        [TestMethod]
        public void TestHMI14105()
        {
            CoreAppXmlConfiguration.Instance.BusyTimeout = 10000;
            using (WhiteWrapper wrapper = new WhiteWrapper(_path, "/master /simulation /debug /password=14105 /ksp=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.ksp\"  /config=\"C:/My Projects/14105 TEI/CustomPrj/Config/Prj14105_HMI.exe.config\""))
            {
                Window window;
                KineticaRT.Studio.Application.Project itsKineticaRTProject = (KineticaRT.Studio.Application.Project)GetObject("Project");

                CELDAS CELProject = CELDAS.Instance;
                HMIChannelCreation HMIChannelCreation = new HMIChannelCreation(ref CELProject);

                string[] engines = new string[] { "T700-401C" }; //, "T700-700", "T700-701C", "T700-701D"};

                wrapper.WaitForStatusWithTimeout("IDLING", 20);

                foreach (string engine in engines)
                {
                    window = wrapper.GetWindowWithTimeout("Welcome Screen", 20);

                    Label label = wrapper.GetControl<Label>(window, "ProjectNumberTxt");
                    Assert.IsNotNull(label);
                    Assert.AreEqual("Project 14105", label.Text);

                    label = wrapper.GetControl<Label>(window, "RevisionNumberTxt");
                    Assert.IsNotNull(label);
                    Assert.AreEqual("Software Revision N/C", label.Text);

                    MyStudioButton btnOpenTest = wrapper.GetControl<MyStudioButton>(window, "btnOpenTest");
                    Assert.IsNotNull(btnOpenTest);
                    while (!btnOpenTest.Enabled)
                        Thread.Sleep(1000);
                    btnOpenTest.Click();

                    Window childWindow = wrapper.GetChildWindow(window, "Test and Password");
                    Assert.IsNotNull(window);
                    TextBox txtPassword = wrapper.GetControl<TextBox>(childWindow, "Password_TextBox");
                    Assert.IsNotNull(txtPassword);
                    txtPassword.Text = projectPassword;

                    ComboBox engineList = wrapper.GetControl<ComboBox>(childWindow, "EngineModel_List");
                    Assert.IsNotNull(engineList);
                    engineList.Select(engine);

                    MyStudioButton btnOkPassord = wrapper.GetControl<MyStudioButton>(childWindow, "btnOkPassword");
                    Assert.IsNotNull(btnOkPassord);
                    btnOkPassord.Click();

                    childWindow = wrapper.GetChildWindow(window, "User Inputs");
                    Assert.IsNotNull(window);

                    MyStudioButton btnOkUserInputs = wrapper.GetControl<MyStudioButton>(childWindow, "btnOkUserInputs");
                    Assert.IsNotNull(btnOkUserInputs);
                    btnOkUserInputs.Click();

                    window = wrapper.GetWindowWithTimeout("CEL POPUP WINDOW", 20);

                    Button btnOK = window.Get<Button>("2");//AutomationId du bouton OK est 2
                    Assert.IsNotNull(btnOK);
                    btnOK.Click();

                    wrapper.WaitForStatusWithTimeout("RUNNING", 20);

                    window = wrapper.GetWindowWithTimeout("T700 Critical", 20);

                    MyStudioDisplay display2 = wrapper.GetControl<MyStudioDisplay>(window, "Display2");
                    Assert.IsNotNull(display2);
                    Assert.AreEqual(engine, display2.Name);

                    MyStudioButton btnCloseTest = wrapper.GetControl<MyStudioButton>(window, "Button4");
                    Assert.IsNotNull(btnCloseTest);
                    btnCloseTest.Click();

                    wrapper.WaitForStatusWithTimeout("IDLING", 20);
                }
            }
        }

        [TestMethod]
        public void TestDIN()
        {
//            CoreAppXmlConfiguration.Instance.BusyTimeout = 10000;

            BuildAndConnectCLient();
            BuildAndConnectGroup();
            BuildAndConnectDout();
            BuildAndConnectDin();
            ConnectAll();
//            Item lIndCOOLING_TOWER_FAN = BuildAndConnectItem("COOLING_TOWER_FAN_IND", "COOLING_TOWER_FAN_IND");

            // Start CELDAS
//                while (mDIN[201].Value == Values.False)
 //                   System.Console.WriteLine("DIN 200 est false");

                for (int i = 0; i < 10; i++)
                {
                    mDOUT[101].Value = Values.True;
                    System.Threading.Thread.Sleep(20000);
                    //                    Assert.AreEqual(mDIN[303].Value, Values.True);
                    mDOUT[102].Value = Values.True;
                    System.Threading.Thread.Sleep(10000);
                    //                   Assert.AreEqual(mDIN[304].Value, Values.False);
                    mDOUT[101].Value = Values.False;
                    System.Threading.Thread.Sleep(10000);
                    //                    Assert.AreEqual(mDIN[303].Value, Values.False);
                    mDOUT[102].Value = Values.False;
                    System.Threading.Thread.Sleep(10000);
                    //                    Assert.AreEqual(mDIN[304].Value, Values.True);
                }
        }


        private object Children_OnObjectSearch(string objectName)
        {
            int dot_position = objectName.IndexOf('.');
            if (dot_position >= 0)
            {
                if (dot_position == 0)
                {
                    if (objectName.Length == 1) return null;
                    return FindObject(objectName.Substring(1));
                }
                else if (objectName.Substring(0, dot_position) == "Parent")
                {
                    if (dot_position == objectName.Length - 1) return null;
                    return OnObjectSearch(objectName.Substring(dot_position + 1));
                }
            }
            else
            {
                if (objectName == "Parent")
                {
                    return this;
                }
            }
            int index = mNameList.IndexOfKey(objectName);
            if (index == -1) return OnObjectSearch(objectName);
            return mNameList.GetByIndex(index);
        }

        private void BuildAndConnectCLient()
        {
            mClient.Server = "ISaGraf.StandAlone.OPC.DA30.3";
            mClient.Name = "Client_IsaGRAF";
            mClient.Machine = "";
            mNameList.Add(mClient.Name, mClient);

            ((KineticaRT.Object)(mClient)).ObjectSearch += new KineticaRT.ObjectSearchEventHandler(Children_OnObjectSearch);
//            mClient.Connect();
        }

        private void ConnectAll()
        {
            mClient.Connect();
            mGroup.Connect();
            for (int i = 101; i < 402; i++)
            {
                mDOUT[i].Connect();
            }
            for (int i = 101; i < 340; i++)
            {
                mDIN[i].Connect();
            }
        }

        private void BuildAndConnectGroup()
        {
            mGroup.Client = "Client_IsaGRAF";
            mGroup.UpdateRate = 0.1;
            mGroup.DeadBand = 0.0;
            mGroup.Name = "Group_Fast";
            mNameList.Add(mGroup.Name, mGroup);
            ((KineticaRT.Object)(mGroup)).ObjectSearch += new KineticaRT.ObjectSearchEventHandler(Children_OnObjectSearch);
//            mGroup.Connect();
        }

        private Item BuildAndConnectItem(string pItemId, string pName)
        {
            Item lItem = new Item();
            lItem.Group = "Group_Fast";
            lItem.ItemID = pItemId;
            lItem.Name = pName;
            lItem.Scale = 1.0;
            lItem.Offset = 0.0;
            mNameList.Add(lItem.Name, lItem);
            ((KineticaRT.Object)(lItem)).ObjectSearch += new KineticaRT.ObjectSearchEventHandler(Children_OnObjectSearch);
            lItem.Connect();

            return lItem;
        }

        private void BuildAndConnectDout()
        {
            for (int i = 101; i < 402; i++)
            {
                mDOUT[i] = new Item();
                mDOUT[i].Group = "Group_Fast";
                mDOUT[i].ItemID = "dacn1.dacn1_user_interface.dacn1_serial_iomgr_dout[" + i + "]";
                mDOUT[i].Name = "dacn1_serial_iomgr_dout[" + i + "]";
                mDOUT[i].Value = new KineticaRT.Values.Boolean(true);
                ((KineticaRT.Object)(mDOUT[i])).ObjectSearch += new KineticaRT.ObjectSearchEventHandler(Children_OnObjectSearch);
//                mDOUT[i].Connect();
            }
        }

        private void BuildAndConnectDin()
        {
            for (int i = 101; i < 340; i++)
            {
                mDIN[i] = new Item();
                mDIN[i].Group = "Group_Fast";
                mDIN[i].ItemID = "dacn1.dacn1_user_interface.dacn1_serial_iomgr_din[" + i + "]";
                mDIN[i].Name = "dacn1_serial_iomgr_din[" + i + "]";
                ((KineticaRT.Object)(mDIN[i])).ObjectSearch += new KineticaRT.ObjectSearchEventHandler(Children_OnObjectSearch);
//                mDIN[i].Connect();
            }
        }
    }
}


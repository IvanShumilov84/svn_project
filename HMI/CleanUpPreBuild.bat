echo off
cd "C:\My Projects\CELDAS 200\HMI"
echo CleanUpPreBuild.bat in %cd% at %time%
del /F *.pdb
del /F clipboard.cs
del /F temp.out
del /F CELDAS.dll
del /F OpenTest_sequence.dll
del /F FlowMeter_sequence.dll
del /F ConfigurationSettings.dll
del /F EmissionInfo.dll
del /F Flowmeter_Sequence.dll
del /F MessagePanel.dll
del /F Test_Password.dll
del /F TestProgressHorizontalPanel.dll
del /F TestProgressVerticalPanel.dll
del /F UserInputs.dll
del /F WelcomeScreen_Panel.dll

del /F APS500\*.dll
del /F APS1000\*.dll
del /F APS2000\*.dll
del /F APS2X00\*.dll
del /F APS32X0\*.dll
del /F APS5000A\*.dll
del /F ARCHIVE\*.dll
del /F ARINC\*.dll
del /F ATAR_09C\*.dll
del /F CANBUS\*.dll
del /F Common\*.dll
del /F DAS_CALIBRATION\*.dll
del /F Diagnostic\*.dll
del /F FCS\*.dll
del /F GTCP85_98\*.dll
del /F GTCP85_129\*.dll
del /F GTCP131_9A\*.dll
del /F GTCP131_9B\*.dll
del /F GTCP331_200\*.dll
del /F GTCP331_350\*.dll
del /F GTCP331_500\*.dll
del /F PT6C\*.dll
del /F PT6A\*.dll
del /F PT6T\*.dll
del /F PW100\*.dll
del /F PW150\*.dll
del /F PW200\*.dll
del /F PW210\*.dll
del /F PW308C_ANC\*.dll
del /F PW901A\*.dll
del /F PW980A\*.dll
del /F PWC\*.dll
del /F Statistics\*.dll
del /F SYSTEM_CHECK\*.dll
del /F TFE731\*.dll
del /F Throttle\*.dll
del /F TSCP700\*.dll
del /F TPE331\*.dll

del /F APS500\*.pdb
del /F APS1000\*.pdb
del /F APS2000\*.pdb
del /F APS2X00\*.pdb
del /F APS32X0\*.pdb
del /F APS5000A\*.pdb
del /F ARCHIVE\*.pdb
del /F ARINC\*.pdb
del /F ATAR_09C\*.pdb
del /F CANBUS\*.pdb
del /F Common\*.pdb
del /F DAS_CALIBRATION\*.pdb
del /F Diagnostic\*.pdb
del /F FCS\*.pdb
del /F GTCP85_98\*.pdb
del /F GTCP85_129\*.pdb
del /F GTCP131_9A\*.pdb
del /F GTCP131_9B\*.pdb
del /F GTCP331_200\*.pdb
del /F GTCP331_350\*.pdb
del /F GTCP331_500\*.pdb
del /F PT6C\*.pdb
del /F PT6A\*.pdb
del /F PW100\*.pdb
del /F PW150\*.pdb
del /F PW200\*.pdb
del /F PW210\*.pdb
del /F PW308C_ANC\*.pdb
del /F PW901A\*.pdb
del /F PW980A\*.pdb
del /F PWC\*.pdb
del /F Statistics\*.pdb
del /F SYSTEM_CHECK\*.pdb
del /F TFE731\*.pdb
del /F Throttle\*.pdb
del /F TSCP700\*.pdb

echo This fonction has been hard coded in the Build Events
echo Pre-build has cleaned DLL files before
echo on
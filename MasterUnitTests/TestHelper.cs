//Jean-Philip Dauray - 2019-03-21
//Importé de 18942
using PrjMaster_HMI.StudioDemonstration;
using KineticaRT.Servers.OpcClient;
using System;
using System.Collections.Generic;

namespace MasterUnitTests
{
    public static class TestHelper
    {
        public static List<object> GetAllEnumValues(Type enumType)
        {
            var content = new List<object>();
            var enumUnderlyingType = Enum.GetUnderlyingType(enumType);
            var enumValues = Enum.GetValues(enumType);

            for (int i = 0; i < enumValues.Length; i++)
            {
                // Retrieve the value of the ith enum item.
                content.Add(enumValues.GetValue(i));
            }
            return content;
        }

        public static List<Engine> GetAllEngineEnumValuesWithoutNonEngine(Type enumType)
        {
            var content = new List<Engine>();
            var enumUnderlyingType = Enum.GetUnderlyingType(enumType);
            var enumValues = Enum.GetValues(enumType);

            for (int i = 0; i < enumValues.Length; i++)
            {
                // Retrieve the value of the ith enum item.
                content.Add((Engine)enumValues.GetValue(i));
            }

            content.Remove(Engine.EngineNotConnected);
            content.Remove(Engine.DASCALIBRATION);
            content.Remove(Engine.SYSTEMCHECK);

            return content;
        }

        public static SmallEngineMedraFileGeneratorSpy BuildSmallEngineMedraFileGeneratorSpy(string sessionId = "999", bool usingMixerNozzle = false)
        {
            var medraFileGenerator = new SmallEngineMedraFileGeneratorSpy(sessionId, usingMixerNozzle);

            medraFileGenerator.T1_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[] { };
            medraFileGenerator.T7_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[,] { };
            medraFileGenerator.T8_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[,] { };
            medraFileGenerator.T11_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[,] { };
            medraFileGenerator.Ps1dot2_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[] { };
            medraFileGenerator.P1dot2_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[] { };
            medraFileGenerator.P7_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[,] { };
            medraFileGenerator.P8_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[,] { };
            medraFileGenerator.P11_DELETE = new KineticaRT.Servers.OpcClient.BooleanItem[,] { };

            medraFileGenerator.AircraftModel = "";
            medraFileGenerator.AircraftSerial = "2";
            medraFileGenerator.Comment = "Comment";
            medraFileGenerator.Customer = "";
            medraFileGenerator.EngineName = "TFE731-2-1C";
            medraFileGenerator.EngineSerialNumber = "020123";
            medraFileGenerator.EnteredBy = "EnteredBy";
            medraFileGenerator.NozzleCode = 0;
            medraFileGenerator.RecordedBy = "RecordedBy";
            medraFileGenerator.WorkOderNumber = "";

            medraFileGenerator.FuelLHV = 0;
            medraFileGenerator.SpecificGravityAs60F = 0.0;
            medraFileGenerator.DryBulbTemperature = 0.0;
            medraFileGenerator.HighPressureTurbineNozzleArea = 0.0;
            medraFileGenerator.LowPressureTurbineNozzleArea = 0.0;
            medraFileGenerator.SGSettingFactor = 0;
            medraFileGenerator.FlatRatingTemp = 0.0;
            medraFileGenerator.FlatRatingN1 = 0.0;
            medraFileGenerator.ThrustCorrectionFactor = 0.0;
            medraFileGenerator.TimeSinceNew = 0;
            medraFileGenerator.CyclesSinceNew = 0;
            medraFileGenerator.N1CompensatorDashNumber = 0;

            return medraFileGenerator;
        }

        public static BigEngineMedraFileGeneratorSpy BuildBigEngineMedraFileGeneratorSpy(string sessionId = "999", bool usingMixerNozzle = false)
        {
            var medraFileGenerator = new BigEngineMedraFileGeneratorSpy(sessionId, usingMixerNozzle);

            medraFileGenerator.AircraftModel = "";
            medraFileGenerator.AircraftSerial = "60";
            medraFileGenerator.Comment = "Comment";
            medraFileGenerator.Customer = "";
            medraFileGenerator.EngineName = "TFE731-60-1C";
            medraFileGenerator.EngineSerialNumber = "600123";
            medraFileGenerator.EnteredBy = "EnteredBy";
            medraFileGenerator.NozzleCode = 1;
            medraFileGenerator.RecordedBy = "RecordedBy";
            medraFileGenerator.WorkOderNumber = "";

            medraFileGenerator.FuelLHV = 18500;
            medraFileGenerator.SpecificGravityAs60F = 0.81;
            medraFileGenerator.DryBulbTemperature = 25.0;
            medraFileGenerator.HighPressureTurbineNozzleArea = 15;
            medraFileGenerator.LowPressureTurbineNozzleArea = 25;
            medraFileGenerator.SGSettingFactor = 5;
            medraFileGenerator.FlatRatingTemp = 900;
            medraFileGenerator.FlatRatingN1 = 17000;
            medraFileGenerator.ThrustCorrectionFactor = 0.0;
            medraFileGenerator.TimeSinceNew = 5;
            medraFileGenerator.CyclesSinceNew = 20;
            medraFileGenerator.N1CompensatorDashNumber = 0;

            return medraFileGenerator;
        }

        public static MedraFileGeneratorSpy BuildMedraFileGeneratorSpy(string sessionId = "999", bool usingMixerNozzle = false, bool asSample = false)
        {
            return new MedraFileGeneratorSpy(sessionId, usingMixerNozzle, asSample);
        }

        public static DummyBooleanItem[,] BuildDummyBooleanItemArray(int size, int size2, KineticaRT.Values.Boolean value)
        {
            var generatedObject = new DummyBooleanItem[size, size2];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    generatedObject[i, j] = new DummyBooleanItem();
                    generatedObject[i, j].CustomRawValue = value;
                }
            }

            return generatedObject;
        }

        public static DummyBooleanItem[] BuildDummyBooleanItemArray(int size, KineticaRT.Values.Boolean value)
        {
            var generatedObject = new DummyBooleanItem[size];

            for (int i = 0; i < size; i++)
            {
                generatedObject[i] = new DummyBooleanItem();
                generatedObject[i].CustomRawValue = value;
            }

            return generatedObject;
        }

    }

    public class DummyBooleanItem : BooleanItem
    {
        public override void Connect()
        {
            //Do nothing to avoid overriding the value with "NOT CONNECTED"
        }

        public KineticaRT.Values.Value CustomRawValue
        {
            //Rend visible le RawValue pour le setter dans les tests afin de bypass le "NOT CONNECTED"
            get
            {
                return RawValue;
            }
            set
            {
                RawValue = value;
            }
        }
    }

    public class SmallEngineMedraFileGeneratorSpy : SmallEngineMedraFileGenerator
    {
        public SmallEngineMedraFileGeneratorSpy(string sessionId, bool usingMixerNozzle)
        : base(sessionId, usingMixerNozzle)
        {

        }

        public new string GenerateMasterNumberRecord()
        {
            return base.GenerateMasterNumberRecord();
        }

        public new string ConvertIntegerToEFourteenFormat(int value)
        {
            return base.ConvertIntegerToEFourteenFormat(value);
        }

        public new string ConvertDoubleToEFourteenFormat(double value)
        {
            return base.ConvertDoubleToEFourteenFormat(value);
        }

        public void SetRunValues(double values)
        {
            //Common
            PBAR = values;
            N1 = 3550 * values;
            N2 = 4500 * values;
            Fn = 1100 * values;
            WF = 500 * values;
            TFuel = 70 * values;
            Ps1dot2_1 = 73 * values;
            Ps1dot2_2 = 73 * values;
            Ps1dot2_3 = 73 * values;
            Ps1dot2_4 = 73 * values;
            Ps1dot2_5 = 73 * values;
            Ps1dot2_6 = 73 * values;

            //Specific
            T1_1 = 70 * values;
            T1_2 = 70 * values;
            T1_3 = 70 * values;
            T1_4 = 70 * values;
            T1_5 = 70 * values;
            T1_6 = 70 * values;
            T1_7 = 70 * values;
            T1_8 = 70 * values;
            T1_9 = 70 * values;
            T1_10 = 70 * values;

            if (_UsingMixerNozzle)
                T5 = 80 * values;

            P3 = 80 * values;
            T3 = 80 * values;
            Ps235 = 70 * values;
            Ts235 = 70 * values;
            P1dot2_1 = 70 * values;
            P1dot2_2 = 70 * values;
            P1dot2_3 = 70 * values;
            P1dot2_4 = 70 * values;
            P1dot2_5 = 70 * values;
            P1dot2_6 = 70 * values;

            if (_UsingMixerNozzle)
            {
                P8_1_1 = 91 * values;
                P8_1_2 = 91 * values;
                P8_1_3 = 91 * values;
                P8_1_4 = 91 * values;
                P8_1_5 = 91 * values;
                P8_2_1 = 91 * values;
                P8_2_2 = 91 * values;
                P8_2_3 = 91 * values;
                P8_2_4 = 91 * values;
                P8_2_5 = 91 * values;
                P8_3_1 = 91 * values;
                P8_3_2 = 91 * values;
                P8_3_3 = 91 * values;
                P8_3_4 = 91 * values;
                P8_3_5 = 91 * values;
                P8_4_1 = 91 * values;
                P8_4_2 = 91 * values;
                P8_4_3 = 91 * values;
                P8_4_4 = 91 * values;
                P8_4_5 = 91 * values;
                P8_5_1 = 91 * values;
                P8_5_2 = 91 * values;
                P8_5_3 = 91 * values;
                P8_5_4 = 91 * values;
                P8_5_5 = 91 * values;

                T8_1_1 = 160 * values;
                T8_1_2 = 160 * values;
                T8_1_3 = 160 * values;
                T8_1_4 = 160 * values;
                T8_1_5 = 160 * values;
                T8_2_1 = 160 * values;
                T8_2_2 = 160 * values;
                T8_2_3 = 160 * values;
                T8_2_4 = 160 * values;
                T8_2_5 = 160 * values;
                T8_3_1 = 160 * values;
                T8_3_2 = 160 * values;
                T8_3_3 = 160 * values;
                T8_3_4 = 160 * values;
                T8_3_5 = 160 * values;
                T8_4_1 = 160 * values;
                T8_4_2 = 160 * values;
                T8_4_3 = 160 * values;
                T8_4_4 = 160 * values;
                T8_4_5 = 160 * values;
                T8_5_1 = 160 * values;
                T8_5_2 = 160 * values;
                T8_5_3 = 160 * values;
                T8_5_4 = 160 * values;
                T8_5_5 = 160 * values;
            }
            else
            {
                P7_1_1 = 90 * values;
                P7_1_2 = 90 * values;
                P7_1_3 = 90 * values;
                P7_1_4 = 90 * values;
                P7_1_5 = 90 * values;
                P7_2_1 = 90 * values;
                P7_2_2 = 90 * values;
                P7_2_3 = 90 * values;
                P7_2_4 = 90 * values;
                P7_2_5 = 90 * values;
                P7_3_1 = 90 * values;
                P7_3_2 = 90 * values;
                P7_3_3 = 90 * values;
                P7_3_4 = 90 * values;
                P7_3_5 = 90 * values;
                P7_4_1 = 90 * values;
                P7_4_2 = 90 * values;
                P7_4_3 = 90 * values;
                P7_4_4 = 90 * values;
                P7_4_5 = 90 * values;
                P7_5_1 = 90 * values;
                P7_5_2 = 90 * values;
                P7_5_3 = 90 * values;
                P7_5_4 = 90 * values;
                P7_5_5 = 90 * values;

                T7_1_1 = 150 * values;
                T7_1_2 = 150 * values;
                T7_1_3 = 150 * values;
                T7_1_4 = 150 * values;
                T7_1_5 = 150 * values;
                T7_2_1 = 150 * values;
                T7_2_2 = 150 * values;
                T7_2_3 = 150 * values;
                T7_2_4 = 150 * values;
                T7_2_5 = 150 * values;
                T7_3_1 = 150 * values;
                T7_3_2 = 150 * values;
                T7_3_3 = 150 * values;
                T7_3_4 = 150 * values;
                T7_3_5 = 150 * values;
                T7_4_1 = 150 * values;
                T7_4_2 = 150 * values;
                T7_4_3 = 150 * values;
                T7_4_4 = 150 * values;
                T7_4_5 = 150 * values;
                T7_5_1 = 150 * values;
                T7_5_2 = 150 * values;
                T7_5_3 = 150 * values;
                T7_5_4 = 150 * values;
                T7_5_5 = 150 * values;
            }

            P11_1_1 = 100 * values;
            P11_1_2 = 100 * values;
            P11_1_3 = 100 * values;
            P11_1_4 = 100 * values;
            P11_1_5 = 100 * values;
            P11_1_6 = 100 * values;
            P11_2_1 = 100 * values;
            P11_2_2 = 100 * values;
            P11_2_3 = 100 * values;
            P11_2_4 = 100 * values;
            P11_2_5 = 100 * values;
            P11_2_6 = 100 * values;
            P11_3_1 = 100 * values;
            P11_3_2 = 100 * values;
            P11_3_3 = 100 * values;
            P11_3_4 = 100 * values;
            P11_3_5 = 100 * values;
            P11_3_6 = 100 * values;
            P11_4_1 = 100 * values;
            P11_4_2 = 100 * values;
            P11_4_3 = 100 * values;
            P11_4_4 = 100 * values;
            P11_4_5 = 100 * values;
            P11_4_6 = 100 * values;

            T11_1_1 = 221 * values;
            T11_1_2 = 221 * values;
            T11_1_3 = 221 * values;
            T11_1_4 = 221 * values;
            T11_1_5 = 221 * values;
            T11_1_6 = 221 * values;
            T11_2_1 = 221 * values;
            T11_2_2 = 221 * values;
            T11_2_3 = 221 * values;
            T11_2_4 = 221 * values;
            T11_2_5 = 221 * values;
            T11_2_6 = 221 * values;
            T11_3_1 = 221 * values;
            T11_3_2 = 221 * values;
            T11_3_3 = 221 * values;
            T11_3_4 = 221 * values;
            T11_3_5 = 221 * values;
            T11_3_6 = 221 * values;
            T11_4_1 = 221 * values;
            T11_4_2 = 221 * values;
            T11_4_3 = 221 * values;
            T11_4_4 = 221 * values;
            T11_4_5 = 221 * values;
            T11_4_6 = 221 * values;
        }

    }

    public class BigEngineMedraFileGeneratorSpy : BigEngineMedraFileGenerator
    {

        public BigEngineMedraFileGeneratorSpy(string sessionId, bool usingMixerNozzle)
        : base(sessionId, usingMixerNozzle)
        {

        }

        public new string GenerateMasterNumberRecord()
        {
            return base.GenerateMasterNumberRecord();
        }

        public new string ConvertIntegerToEFourteenFormat(int value)
        {
            return base.ConvertIntegerToEFourteenFormat(value);
        }

        public new string ConvertDoubleToEFourteenFormat(double value)
        {
            return base.ConvertDoubleToEFourteenFormat(value);
        }

        public void SetRunValues(double values)
        {
            //Common
            PBAR = 10 + values;
            N1 = 7500 + 2700 * values;
            N2 = 22050 + 2000 * values;
            Fn = 1300 * (values - 1);
            WF = 0.5 * values;
            TFuel = 30 * values;
            Ps1dot2_1 = 0.3 * values;//83 * (values - 1);
            Ps1dot2_2 = 0.3 * values;//83 * (values - 1);
            Ps1dot2_3 = 0.3 * values;//83 * (values - 1);
            Ps1dot2_4 = 0.3 * values;//83 * (values - 1);
            Ps1dot2_5 = 0.3 * values;//83 * (values - 1);
            Ps1dot2_6 = 0.3 * values;//83 * (values - 1);

            //Specific
            Tt1dot2_1 = 30 * values;
            Tt1dot2_2 = 30 * values;
            Tt1dot2_3 = 30 * values;
            Tt1dot2_4 = 30 * values;
            Tt1dot2_5 = 30 * values;
            Tt1dot2_6 = 30 * values;
            Tt1dot2_7 = 30 * values;
            Tt1dot2_8 = 30 * values;
            Tt1dot2_9 = 30 * values;
            Tt1dot2_10 = 30 * values;

            T45 = 550 + 300 * values;
            Pt30 = 40 * values;
            Tt30 = 250 + 150 * values;
            Ps245 = 18 * (values - 1);
            Tt245 = 120 * values;
            Ps1dot2_7 = 0.3 * values;//83 * (values - 1);
            Ps1dot2_8 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_1 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_2 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_3 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_4 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_5 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_6 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_7 = 0.3 * values;//83 * (values - 1);
            Pt1dot2_8 = 0.3 * values;//83 * (values - 1);

            if (_UsingMixerNozzle)
            {
                Pt60_1_1 = (values - 1) + 0.12;
                Pt60_1_2 = (values - 1) + 0.12;
                Pt60_1_3 = (values - 1) + 0.12;
                Pt60_1_4 = (values - 1) + 0.12;
                Pt60_1_5 = (values - 1) + 0.12;
                Pt60_2_1 = (values - 1) + 0.12;
                Pt60_2_2 = (values - 1) + 0.12;
                Pt60_2_3 = (values - 1) + 0.12;
                Pt60_2_4 = (values - 1) + 0.12;
                Pt60_2_5 = (values - 1) + 0.12;
                Pt60_3_1 = (values - 1) + 0.12;
                Pt60_3_2 = (values - 1) + 0.12;
                Pt60_3_3 = (values - 1) + 0.12;
                Pt60_3_4 = (values - 1) + 0.12;
                Pt60_3_5 = (values - 1) + 0.12;
                Pt60_4_1 = (values - 1) + 0.12;
                Pt60_4_2 = (values - 1) + 0.12;
                Pt60_4_3 = (values - 1) + 0.12;
                Pt60_4_4 = (values - 1) + 0.12;
                Pt60_4_5 = (values - 1) + 0.12;
                Pt60_5_1 = (values - 1) + 0.12;
                Pt60_5_2 = (values - 1) + 0.12;
                Pt60_5_3 = (values - 1) + 0.12;
                Pt60_5_4 = (values - 1) + 0.12;
                Pt60_5_5 = (values - 1) + 0.12;
                Pt60_6_1 = (values - 1) + 0.12;
                Pt60_6_2 = (values - 1) + 0.12;
                Pt60_6_3 = (values - 1) + 0.12;
                Pt60_6_4 = (values - 1) + 0.12;
                Pt60_6_5 = (values - 1) + 0.12;

                Tt60_1_1 = 700 + 40 * values;
                Tt60_1_2 = 700 + 40 * values;
                Tt60_1_3 = 700 + 40 * values;
                Tt60_1_4 = 700 + 40 * values;
                Tt60_1_5 = 700 + 40 * values;
                Tt60_2_1 = 700 + 40 * values;
                Tt60_2_2 = 700 + 40 * values;
                Tt60_2_3 = 700 + 40 * values;
                Tt60_2_4 = 700 + 40 * values;
                Tt60_2_5 = 700 + 40 * values;
                Tt60_3_1 = 700 + 40 * values;
                Tt60_3_2 = 700 + 40 * values;
                Tt60_3_3 = 700 + 40 * values;
                Tt60_3_4 = 700 + 40 * values;
                Tt60_3_5 = 700 + 40 * values;
                Tt60_4_1 = 700 + 40 * values;
                Tt60_4_2 = 700 + 40 * values;
                Tt60_4_3 = 700 + 40 * values;
                Tt60_4_4 = 700 + 40 * values;
                Tt60_4_5 = 700 + 40 * values;
                Tt60_5_1 = 700 + 40 * values;
                Tt60_5_2 = 700 + 40 * values;
                Tt60_5_3 = 700 + 40 * values;
                Tt60_5_4 = 700 + 40 * values;
                Tt60_5_5 = 700 + 40 * values;
                Tt60_6_1 = 700 + 40 * values;
                Tt60_6_2 = 700 + 40 * values;
                Tt60_6_3 = 700 + 40 * values;
                Tt60_6_4 = 700 + 40 * values;
                Tt60_6_5 = 700 + 40 * values;
            }
            else
            {
                Pt55_1_1 = (values - 1) + 0.12;
                Pt55_1_2 = (values - 1) + 0.12;
                Pt55_1_3 = (values - 1) + 0.12;
                Pt55_1_4 = (values - 1) + 0.12;
                Pt55_1_5 = (values - 1) + 0.12;
                Pt55_2_1 = (values - 1) + 0.12;
                Pt55_2_2 = (values - 1) + 0.12;
                Pt55_2_3 = (values - 1) + 0.12;
                Pt55_2_4 = (values - 1) + 0.12;
                Pt55_2_5 = (values - 1) + 0.12;
                Pt55_3_1 = (values - 1) + 0.12;
                Pt55_3_2 = (values - 1) + 0.12;
                Pt55_3_3 = (values - 1) + 0.12;
                Pt55_3_4 = (values - 1) + 0.12;
                Pt55_3_5 = (values - 1) + 0.12;
                Pt55_4_1 = (values - 1) + 0.12;
                Pt55_4_2 = (values - 1) + 0.12;
                Pt55_4_3 = (values - 1) + 0.12;
                Pt55_4_4 = (values - 1) + 0.12;
                Pt55_4_5 = (values - 1) + 0.12;
                Pt55_5_1 = (values - 1) + 0.12;
                Pt55_5_2 = (values - 1) + 0.12;
                Pt55_5_3 = (values - 1) + 0.12;
                Pt55_5_4 = (values - 1) + 0.12;
                Pt55_5_5 = (values - 1) + 0.12;
                Pt55_6_1 = (values - 1) + 0.12;
                Pt55_6_2 = (values - 1) + 0.12;
                Pt55_6_3 = (values - 1) + 0.12;
                Pt55_6_4 = (values - 1) + 0.12;
                Pt55_6_5 = (values - 1) + 0.12;

                Tt55_1_1 = 700 + 40 * values;
                Tt55_1_2 = 700 + 40 * values;
                Tt55_1_3 = 700 + 40 * values;
                Tt55_1_4 = 700 + 40 * values;
                Tt55_1_5 = 700 + 40 * values;
                Tt55_2_1 = 700 + 40 * values;
                Tt55_2_2 = 700 + 40 * values;
                Tt55_2_3 = 700 + 40 * values;
                Tt55_2_4 = 700 + 40 * values;
                Tt55_2_5 = 700 + 40 * values;
                Tt55_3_1 = 700 + 40 * values;
                Tt55_3_2 = 700 + 40 * values;
                Tt55_3_3 = 700 + 40 * values;
                Tt55_3_4 = 700 + 40 * values;
                Tt55_3_5 = 700 + 40 * values;
                Tt55_4_1 = 700 + 40 * values;
                Tt55_4_2 = 700 + 40 * values;
                Tt55_4_3 = 700 + 40 * values;
                Tt55_4_4 = 700 + 40 * values;
                Tt55_4_5 = 700 + 40 * values;
                Tt55_5_1 = 700 + 40 * values;
                Tt55_5_2 = 700 + 40 * values;
                Tt55_5_3 = 700 + 40 * values;
                Tt55_5_4 = 700 + 40 * values;
                Tt55_5_5 = 700 + 40 * values;
                Tt55_6_1 = 700 + 40 * values;
                Tt55_6_2 = 700 + 40 * values;
                Tt55_6_3 = 700 + 40 * values;
                Tt55_6_4 = 700 + 40 * values;
                Tt55_6_5 = 700 + 40 * values;
            }

            P155_1_1 = (values - 1) + 0.05;
            P155_1_2 = (values - 1) + 0.05;
            P155_1_3 = (values - 1) + 0.05;
            P155_1_4 = (values - 1) + 0.05;
            P155_1_5 = (values - 1) + 0.05;
            P155_1_6 = (values - 1) + 0.05;
            P155_2_1 = (values - 1) + 0.05;
            P155_2_2 = (values - 1) + 0.05;
            P155_2_3 = (values - 1) + 0.05;
            P155_2_4 = (values - 1) + 0.05;
            P155_2_5 = (values - 1) + 0.05;
            P155_2_6 = (values - 1) + 0.05;
            P155_3_1 = (values - 1) + 0.05;
            P155_3_2 = (values - 1) + 0.05;
            P155_3_3 = (values - 1) + 0.05;
            P155_3_4 = (values - 1) + 0.05;
            P155_3_5 = (values - 1) + 0.05;
            P155_3_6 = (values - 1) + 0.05;
            P155_4_1 = (values - 1) + 0.05;
            P155_4_2 = (values - 1) + 0.05;
            P155_4_3 = (values - 1) + 0.05;
            P155_4_4 = (values - 1) + 0.05;
            P155_4_5 = (values - 1) + 0.05;
            P155_4_6 = (values - 1) + 0.05;

            T155_1_1 = 10 + 20 * values;
            T155_1_2 = 10 + 20 * values;
            T155_1_3 = 10 + 20 * values;
            T155_1_4 = 10 + 20 * values;
            T155_1_5 = 10 + 20 * values;
            T155_1_6 = 10 + 20 * values;
            T155_2_1 = 10 + 20 * values;
            T155_2_2 = 10 + 20 * values;
            T155_2_3 = 10 + 20 * values;
            T155_2_4 = 10 + 20 * values;
            T155_2_5 = 10 + 20 * values;
            T155_2_6 = 10 + 20 * values;
            T155_3_1 = 10 + 20 * values;
            T155_3_2 = 10 + 20 * values;
            T155_3_3 = 10 + 20 * values;
            T155_3_4 = 10 + 20 * values;
            T155_3_5 = 10 + 20 * values;
            T155_3_6 = 10 + 20 * values;
            T155_4_1 = 10 + 20 * values;
            T155_4_2 = 10 + 20 * values;
            T155_4_3 = 10 + 20 * values;
            T155_4_4 = 10 + 20 * values;
            T155_4_5 = 10 + 20 * values;
            T155_4_6 = 10 + 20 * values;
        }

    }

    public class MedraFileGeneratorSpy : MedraFileGenerator
    {
        public MedraFileGeneratorSpy(string path, bool usingMixerNozzle, bool asSample)
        : base(path, usingMixerNozzle, asSample)
        {

        }

        public new string ConvertIntegerToEFourteenFormat(int value)
        {
            return base.ConvertIntegerToEFourteenFormat(value);
        }

        public new string ConvertDoubleToEFourteenFormat(double value)
        {
            return base.ConvertDoubleToEFourteenFormat(value);
        }

        public new string ConvertToEFourteenFormat(object value)
        {
            return base.ConvertToEFourteenFormat(value);
        }

        public override int CountDeletedParameters()
        {
            return 0;
        }

    }
}

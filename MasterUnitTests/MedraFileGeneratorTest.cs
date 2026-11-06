//Jean-Philip Dauray - 2019-03-21
//Importé de 18942
using PrjMaster_HMI.StudioDemonstration;
using NUnit.Framework;
using KineticaRT.Values;

namespace MasterUnitTests
{
    [TestFixture]
    public class MedraFileGeneratorTest
    {
        [TestCase(0, "+.00000000E+00")]
        [TestCase(1, "+.10000000E+01")]
        [TestCase(-1, "-.10000000E+01")]
        [TestCase(20000, "+.20000000E+05")]
        [TestCase(-20000, "-.20000000E+05")]
        public void ConvertIntegerToEFourteenFormat(int value, string expected)
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildMedraFileGeneratorSpy();

            //Execute
            var result = medraFileGenerator.ConvertIntegerToEFourteenFormat(value);

            //Verify
            Assert.AreEqual(expected, result);
        }

        [TestCase(20000.0, "+.20000000E+05")]
        [TestCase(-20000.0, "-.20000000E+05")]
        [TestCase(0.0000004, "+.40000000E-06")]
        [TestCase(-0.0000004, "-.40000000E-06")]
        [TestCase(123456.123456, "+.12345612E+06")]
        [TestCase(-123456.123456, "-.12345612E+06")]
        public void ConvertDoubleToEFourteenFormat(double value, string expected)
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildMedraFileGeneratorSpy();

            //Execute
            var result = medraFileGenerator.ConvertDoubleToEFourteenFormat(value);

            //Verify
            Assert.AreEqual(expected, result);
        }

        [TestCase(0, "+.00000000E+00")]
        [TestCase(1, "+.10000000E+01")]
        [TestCase(-1, "-.10000000E+01")]
        [TestCase(20000, "+.20000000E+05")]
        [TestCase(-20000, "-.20000000E+05")]
        [TestCase(20000.0, "+.20000000E+05")]
        [TestCase(-20000.0, "-.20000000E+05")]
        [TestCase(0.0000004, "+.40000000E-06")]
        [TestCase(-0.0000004, "-.40000000E-06")]
        [TestCase(123456.123456, "+.12345612E+06")]
        [TestCase(-123456.123456, "-.12345612E+06")]
        [TestCase("20000.0", "+.20000000E+05")]
        [TestCase("+.20000000E+05", "+.20000000E+05")]
        [TestCase("qwerty", "qwerty        ")]
        public void ConvertToEFourteenFormat(object value, string expected)
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildMedraFileGeneratorSpy();

            //Execute
            var result = medraFileGenerator.ConvertToEFourteenFormat(value);

            //Verify
            Assert.AreEqual(expected, result);
        }

        //public void ConvertFieldToProperties()
        //{
        //    var reader = new System.IO.StreamReader("c:/MEDRA/input.txt");
        //    var writer = new System.IO.StreamWriter("c:/MEDRA/output.txt");
        //    while (!reader.EndOfStream)
        //    {
        //        var propertyAsText = reader.ReadLine();
        //
        //        if (!string.IsNullOrWhiteSpace(propertyAsText))
        //        {
        //            var content = propertyAsText.Split(' ');
        //            writer.WriteLine("Private _" + content[2] + " As " + content[4]);
        //            writer.WriteLine("Public Property " + content[2] + " As " + content[4]);
        //            writer.WriteLine("  Get");
        //            writer.WriteLine("      Return _" + content[2]);
        //            writer.WriteLine("  End Get");
        //            writer.WriteLine("  Set(ByVal value As " + content[4] + ")");
        //            writer.WriteLine("      _" + content[2] + " = value");
        //            writer.WriteLine("  End Set");
        //            writer.WriteLine("End Property");
        //        }
        //        writer.WriteLine("");
        //
        //    }
        //    reader.Close();
        //    writer.Close();
        //}


        [Test]
        public void MappedIsDeleted_MultiDimension_AllFalse()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T8_DELETE = TestHelper.BuildDummyBooleanItemArray(5, 5, Values.False);
            medraFileGenerator.T8_DELETE = ref_T8_DELETE;
            var mappedData = new Mapped("T8_1_1", 0, "", 0);

            //Execute
            var result = mappedData.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result);
        }

        [Test]
        public void MappedIsDeleted_MultiDimension_AllTrue()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T8_DELETE = TestHelper.BuildDummyBooleanItemArray(5, 5, Values.True);
            medraFileGenerator.T8_DELETE = ref_T8_DELETE;
            var mappedData = new Mapped("T8_1_1", 0, "", 0);

            //Execute
            var result = mappedData.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsTrue(result);
        }

        [Test]
        public void MappedIsDeleted_MultiDimension_Mixed()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T8_DELETE = TestHelper.BuildDummyBooleanItemArray(5, 5, Values.False);
            medraFileGenerator.T8_DELETE = ref_T8_DELETE;
            var mappedData1 = new Mapped("T8_1_1", 0, "", 0);
            var mappedData2 = new Mapped("T8_5_4", 0, "", 0);

            //Execute
            ref_T8_DELETE[0, 0].CustomRawValue = Values.False;
            ref_T8_DELETE[4, 3].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_SingleDimension_AllFalse()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T1_DELETE = TestHelper.BuildDummyBooleanItemArray(8, Values.False);
            medraFileGenerator.T1_DELETE = ref_T1_DELETE;
            var mappedData = new Mapped("T1_5", 0, "", 0);

            //Execute
            var result = mappedData.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result);
        }

        [Test]
        public void MappedIsDeleted_SingleDimension_AllTrue()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T1_DELETE = TestHelper.BuildDummyBooleanItemArray(8, Values.True);
            medraFileGenerator.T1_DELETE = ref_T1_DELETE;
            var mappedData = new Mapped("T1_5", 0, "", 0);

            //Execute
            var result = mappedData.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsTrue(result);
        }

        [Test]
        public void MappedIsDeleted_SingleDimension_Mixed()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T1_DELETE = TestHelper.BuildDummyBooleanItemArray(8, Values.False);
            medraFileGenerator.T1_DELETE = ref_T1_DELETE;
            var mappedData1 = new Mapped("T1_4", 0, "", 0);
            var mappedData2 = new Mapped("T1_5", 0, "", 0);

            //Execute
            ref_T1_DELETE[3].CustomRawValue = Values.False;
            ref_T1_DELETE[4].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_T1()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T1_DELETE = TestHelper.BuildDummyBooleanItemArray(10, Values.False);
            medraFileGenerator.T1_DELETE = ref_T1_DELETE;
            var mappedData1 = new Mapped("T1_9", 0, "", 0);
            var mappedData2 = new Mapped("T1_10", 0, "", 0);

            //Execute
            ref_T1_DELETE[8].CustomRawValue = Values.False;
            ref_T1_DELETE[9].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_T7()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy(usingMixerNozzle: true);
            var ref_T7_DELETE = TestHelper.BuildDummyBooleanItemArray(5, 5, Values.False);
            medraFileGenerator.T7_DELETE = ref_T7_DELETE;
            var mappedData1 = new Mapped("T7_5_4", 0, "", 0);
            var mappedData2 = new Mapped("T7_5_5", 0, "", 0);

            //Execute
            ref_T7_DELETE[4, 3].CustomRawValue = Values.False;
            ref_T7_DELETE[4, 4].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_T8()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T8_DELETE = TestHelper.BuildDummyBooleanItemArray(5, 5, Values.False);
            medraFileGenerator.T8_DELETE = ref_T8_DELETE;
            var mappedData1 = new Mapped("T8_5_4", 0, "", 0);
            var mappedData2 = new Mapped("T8_5_5", 0, "", 0);

            //Execute
            ref_T8_DELETE[4, 3].CustomRawValue = Values.False;
            ref_T8_DELETE[4, 4].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_T11()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildSmallEngineMedraFileGeneratorSpy();
            var ref_T11_DELETE = TestHelper.BuildDummyBooleanItemArray(4, 6, Values.False);
            medraFileGenerator.T11_DELETE = ref_T11_DELETE;
            var mappedData1 = new Mapped("T11_3_6", 0, "", 0);
            var mappedData2 = new Mapped("T11_4_6", 0, "", 0);

            //Execute
            ref_T11_DELETE[2, 5].CustomRawValue = Values.False;
            ref_T11_DELETE[3, 5].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_Tt1dot2()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildBigEngineMedraFileGeneratorSpy();
            var ref_Tt1_DELETE = TestHelper.BuildDummyBooleanItemArray(10, Values.False);
            medraFileGenerator.Tt1dot2_DELETE = ref_Tt1_DELETE;
            var mappedData1 = new Mapped("Tt1dot2_9", 0, "", 0);
            var mappedData2 = new Mapped("Tt1dot2_10", 0, "", 0);

            //Execute
            ref_Tt1_DELETE[8].CustomRawValue = Values.False;
            ref_Tt1_DELETE[9].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_Tt60()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildBigEngineMedraFileGeneratorSpy();
            var ref_Tt60_DELETE = TestHelper.BuildDummyBooleanItemArray(6, 5, Values.False);
            medraFileGenerator.Tt60_DELETE = ref_Tt60_DELETE;
            var mappedData1 = new Mapped("Tt60_6_4", 0, "", 0);
            var mappedData2 = new Mapped("Tt60_6_5", 0, "", 0);

            //Execute
            ref_Tt60_DELETE[5, 3].CustomRawValue = Values.False;
            ref_Tt60_DELETE[5, 4].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_Tt55()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildBigEngineMedraFileGeneratorSpy(usingMixerNozzle: true);
            var ref_Tt55_DELETE = TestHelper.BuildDummyBooleanItemArray(6, 5, Values.False);
            medraFileGenerator.Tt55_DELETE = ref_Tt55_DELETE;
            var mappedData1 = new Mapped("Tt55_6_4", 0, "", 0);
            var mappedData2 = new Mapped("Tt55_6_5", 0, "", 0);

            //Execute
            ref_Tt55_DELETE[5, 3].CustomRawValue = Values.False;
            ref_Tt55_DELETE[5, 4].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }

        [Test]
        public void MappedIsDeleted_T155()
        {
            //Setup
            var medraFileGenerator = TestHelper.BuildBigEngineMedraFileGeneratorSpy();
            var ref_T155_DELETE = TestHelper.BuildDummyBooleanItemArray(4, 6, Values.False);
            medraFileGenerator.T155_DELETE = ref_T155_DELETE;
            var mappedData1 = new Mapped("T155_4_5", 0, "", 0);
            var mappedData2 = new Mapped("T155_4_6", 0, "", 0);

            //Execute
            ref_T155_DELETE[3, 4].CustomRawValue = Values.False;
            ref_T155_DELETE[3, 5].CustomRawValue = Values.True;
            var result1 = mappedData1.IsDeleted(medraFileGenerator);
            var result2 = mappedData2.IsDeleted(medraFileGenerator);

            //Verify
            Assert.IsFalse(result1);
            Assert.IsTrue(result2);
        }


    }
}

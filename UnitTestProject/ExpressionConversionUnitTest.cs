using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CEL.Library;
using System.Diagnostics;

namespace UnitTestProject
{
    [TestClass]
    public class ExpressionConversionUnitTest
    {

        [TestMethod]
        public void ConvertExpression_Power()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("POWER_KW", "KW", "W", ref newExpression);
            Assert.IsTrue(newExpression == "((POWER_KW)×(1000))");

            expressionUnitConversion.ProcessUnitConversion("POWER_W", "W", "KW", ref newExpression);
            Assert.IsTrue(newExpression == "((POWER_W)/(1000))");

        }

        [TestMethod]
        public void ConvertExpression_Frequency()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("FREQ_KHZ", "KHZ", "HZ", ref newExpression);
            Assert.IsTrue(newExpression == "((FREQ_KHZ)×(1000))");

            expressionUnitConversion.ProcessUnitConversion("FREQ_KHZ", "HZ", "KHZ", ref newExpression);
            Assert.IsTrue(newExpression == "((FREQ_KHZ)/(1000))");

            expressionUnitConversion.ProcessUnitConversion("SPEED_RPM", "RPM", "HZ", ref newExpression);
            Assert.IsTrue(newExpression == "((SPEED_RPM)×(1.0/60))");

            expressionUnitConversion.ProcessUnitConversion("SPEED_HZ", "HZ", "RPM", ref newExpression);
            Assert.IsTrue(newExpression == "((SPEED_HZ)/(1.0/60))");

        }
        [TestMethod]
        public void ConvertExpression_Flow()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("FLOW_LPH", "LPH", "LPM", ref newExpression);
            Assert.IsTrue(newExpression == "((FLOW_LPH)×(1.0/60.0))"); //=="((FLOW_LPH)/(60.0))"

            expressionUnitConversion.ProcessUnitConversion("FLOW_LPM", "LPM", "LPH", ref newExpression);
            Assert.IsTrue(newExpression == "((FLOW_LPM)×(60.0))"); //== "((FLOW_LPH)/(1.0/60.0))"
        }   


        
        [TestMethod]
        public void PSI_PSI_WITH_BARO_PSIA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("P1_PSIA", "PSIA", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "(P1_PSIA-P_BARO_PSIA)");

            expressionUnitConversion.ProcessUnitConversion("P1_PSIG", "PSIG", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "(P1_PSIG)");

            expressionUnitConversion.ProcessUnitConversion("P1_PSI", "PSI", "PSI", ref newExpression);
            Assert.IsTrue(newExpression == "(P1_PSI)");

            expressionUnitConversion.ProcessUnitConversion("P1_PSIA", "PSIA", "PSIA", ref newExpression);
            Assert.IsTrue(newExpression == "(P1_PSIA)");

            expressionUnitConversion.ProcessUnitConversion("P1_PSID", "PSID", "PSID", ref newExpression);
            Assert.IsTrue(newExpression == "(P1_PSID)");

            expressionUnitConversion.ProcessUnitConversion("P1_PSID", "PSID", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_PSID)×(1.0))");

        }
        [TestMethod]
        public void PSI_PSI_WITH_BARO_KPAA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_KPAA";
            string baroUnit = "KPAA";
            string newExpression = "";
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("P_ORIFICE_PSIG", "PSIG", "PSIA", ref newExpression);
            Assert.IsTrue(newExpression == "((P_ORIFICE_PSIG)+((P_BARO_KPAA)/(6.89475729)))");

            expressionUnitConversion.ProcessUnitConversion("P_ORIFICE_PSIA", "PSIA", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "(P_ORIFICE_PSIA-((P_BARO_KPAA)/(6.89475729)))");

        }

        [TestMethod]
        public void BAR_PSI_WITH_BARO_PSIA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);

            expressionUnitConversion.ProcessUnitConversion("P1_BARA", "BARA", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "(((P1_BARA)×(1.0/0.0689475729))-P_BARO_PSIA)");
                 
            
            expressionUnitConversion.ProcessUnitConversion("P1_PSIG", "PSIG", "BARA", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_PSIG+P_BARO_PSIA)×(0.0689475729))");
        }
        [TestMethod]
        public void BAR_PSI_WITH_BARO_BARA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_BARA";
            string baroUnit = "BARA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);

            expressionUnitConversion.ProcessUnitConversion("P1_BARA", "BARA", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_BARA-P_BARO_BARA)×(1.0/0.0689475729))");

                    

        
        }
        [TestMethod]
        public void BAR_PSI_WITH_BARO_KPAA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_KPAA";
            string baroUnit = "KPAA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);

            expressionUnitConversion.ProcessUnitConversion("P1_BARA", "BARA", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_BARA-((P_BARO_KPAA)×(1.0 / 100.0)))×(1.0/0.0689475729))");


         

        } 
        [TestMethod]
        public void INH2O_PSI_WITH_BARO_PSIA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("P6_PSIA", "PSIA", "INH2OG", ref newExpression);
            Assert.IsTrue(newExpression == "((P6_PSIA-P_BARO_PSIA)/(1.0/27.680672603))");

            expressionUnitConversion.ProcessUnitConversion("P6_INH2OG", "INH2OG", "PSIA", ref newExpression);
            Assert.IsTrue(newExpression == "(((P6_INH2OG)×(1.0/27.680672603))+P_BARO_PSIA)");
        }
         [TestMethod]
        public void INHG_PSI_WITH_BARO_PSIA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("P1_PSID", "PSID", "INHGA", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_PSID+P_BARO_PSIA)×(2.03602046))");

        }
        [TestMethod]
        public void KPA_PSI_WITH_BARO_PSIA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);
            

            expressionUnitConversion.ProcessUnitConversion("P1_PSIG", "PSIG", "KPAA", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_PSIG+P_BARO_PSIA)×(6.89475729))");
            expressionUnitConversion.ProcessUnitConversion("P1_KPAA", "KPAA", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "(((P1_KPAA)/(6.89475729))-P_BARO_PSIA)");


            expressionUnitConversion.ProcessUnitConversion("P1_PSIA", "PSIA", "KPAA", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_PSIA)×(6.89475729))");
            expressionUnitConversion.ProcessUnitConversion("P1_KPAA", "KPAA", "PSIA", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_KPAA)/(6.89475729))");
        }
      

        [TestMethod]
        public void KPA_BAR_WITH_BARO_PSIA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_PSIA";
            string baroUnit = "PSIA";
            string newExpression = string.Empty;
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("P1_KPAG", "KPAG", "BARA", ref newExpression);
            Assert.IsTrue(newExpression == "(((P1_KPAG)×(1.0 / 100.0))+((P_BARO_PSIA)/(1.0/0.0689475729)))");
            expressionUnitConversion.ProcessUnitConversion("P1_BARA", "BARA", "KPAG", ref newExpression);
            Assert.IsTrue(newExpression == "((P1_BARA-((P_BARO_PSIA)/(1.0/0.0689475729)))/(1.0 / 100.0))");
        }

        [TestMethod]
        public void KPA_PSI_WITH_BARO_KPAA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_KPAA";
            string baroUnit= "KPAA";
            string newExpression= "";
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("P_INLET_PSI", "PSI", "KPAA", ref newExpression);
            Assert.IsTrue(newExpression == "(((P_INLET_PSI)×(6.89475729))+P_BARO_KPAA)");
            expressionUnitConversion.ProcessUnitConversion("P_INLET_KPAA", "KPAA", "PSI", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_KPAA-P_BARO_KPAA)/(6.89475729))");

            expressionUnitConversion.ProcessUnitConversion("P_INLET_PSIG", "PSIG", "KPAA", ref newExpression);
            Assert.IsTrue(newExpression == "(((P_INLET_PSIG)×(6.89475729))+P_BARO_KPAA)");
            expressionUnitConversion.ProcessUnitConversion("P_INLET_KPAA", "KPAA", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_KPAA-P_BARO_KPAA)/(6.89475729))");

            String newExpressionPSIG = newExpression; //Test d une double conversion
            expressionUnitConversion.ProcessUnitConversion(newExpressionPSIG, "PSIG", "BARG", ref newExpression);
            Assert.IsTrue(newExpression == "((((P_INLET_KPAA-P_BARO_KPAA)/(6.89475729)))/(1.0/0.0689475729))");
        }

        [TestMethod]
        public void INH2O_PSI_WITH_BARO_KPAA()
        {
            ExpressionUnitConversion expressionUnitConversion = new ExpressionUnitConversion();
            string baroChannel = "P_BARO_KPAA";
            string baroUnit = "KPAA";
            string newExpression = "";
            expressionUnitConversion.SetBaroChannel(baroChannel, baroUnit);


            expressionUnitConversion.ProcessUnitConversion("P_INLET_PSI", "PSI", "INH2O", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_PSI)×(27.680672603))");
            expressionUnitConversion.ProcessUnitConversion("P_INLET_INH2O", "INH2O", "PSI", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_INH2O)/(27.680672603))");

            expressionUnitConversion.ProcessUnitConversion("P_INLET_PSI", "PSI", "INH2OG", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_PSI)×(27.680672603))");
            expressionUnitConversion.ProcessUnitConversion("P_INLET_INH2OG", "INH2OG", "PSI", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_INH2OG)/(27.680672603))");


            expressionUnitConversion.ProcessUnitConversion("P_INLET_PSIG", "PSIG", "INH2O", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_PSIG)×(27.680672603))");
            expressionUnitConversion.ProcessUnitConversion("P_INLET_INH2O", "INH2O", "PSIG", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_INH2O)/(27.680672603))");


            expressionUnitConversion.ProcessUnitConversion("P_INLET_PSID", "PSID", "INH2O", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_PSID)×(27.680672603))");
            expressionUnitConversion.ProcessUnitConversion("P_INLET_INH2O", "INH2O", "PSID", ref newExpression);
            Assert.IsTrue(newExpression == "((P_INLET_INH2O)/(27.680672603))");


            bool error = expressionUnitConversion.ProcessUnitConversion("P_INLET_PSIG", "PSIG", "INH2OA", ref newExpression);
            Assert.IsTrue(error );
        }

      
    }
}

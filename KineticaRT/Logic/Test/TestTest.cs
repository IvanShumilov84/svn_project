//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Test
//{
//    //<Test>
//    class TestTest : Test
//    {
//        //  <TestProfile Name="DryMotorProcedure">
//        class Profile_DryMotorProcedure:Profile
//        {
//            Step[] steps = { new Step_0(), new Step_1(), new Step_2(), new Step_3(), new Step_4() };

//            //    <Step Name="0">
//            class Step_0 : Step
//            {
//                //      <Output Name="O_PLA" Value="16" />
//                void Execute ()
//                {
//                    //      Output ("O_PLA", "16"); 
//                    //Output("O_PLA", "16");
//                    //      <Output Name="O_SECEN" Value="1" />
//                    //Output ("O_SECEN", "1");
//                    //      <Condition Name="I_PLA" Value="16" />
//                    //      <Condition Name="I_SECEN" Value="1" />
////                    Wait("I_PLA==16 && I_SECEN==1");
//                }
//            }
//            //    </Step>

//            //    <Step Name="1">
//            class Step_1 : Step
//            {
//                //      <Action Name="Message" Text="Perform MCD VLV Leak Check..."/>
//                //      <Condition Expression="AckButton" />
//            }
//            //    </Step>

//            //    <Step Name="2">
//            class Step_2 : Step
//            {
//                //      <StartCondition Name="PLAIDLTMR" Value="300" />
//                //      <Action Name="Record S-S Data" />
//                //      <Action Name="VerifyInputs" />
//            }
//            //    </Step>

//            //    <Step Name="3">
//            class Step_3 : Step
//            {
//                //      <Output Name="SECEN" Value="0" />
//                //      <Action Name="ResetPLAIDLTMR" />
//                //      <Action Name="Message" Text="Confirm SEC light on."/>
//                //      <Condition Expression="ACK_OK_Pressed" />
//            }
//            //    </Step>

//            //    <Step Name="4">
//            class Step_4 : Step
//            {
//                //      <StartCondition Name="PLAIDLTMR" Value="30" />
//                //      <Action Name="Record S-S Data" />
//                //      <Action Name="Message" Text="Perform SEC idle leak check."/>
//                //      <Condition Expression="A8 &lt; 354 &amp; I_SQ &amp; AckButton" />
//            }
//            //    </Step>
//        }
//    }
//}

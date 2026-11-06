using System;
using System.Collections.Generic;
using System.Text;

namespace KineticaRT
{
//    class DiagnosticsLog
  //  {
        //private System.Text.StringBuilder diagnostics;
        //private string newline = "\r\n";
        //private bool writeDiagnostics;
        //private int diagnosticsWritten;
        //private string xfileName = "Diagnostics.txt";
        //private string previousFileName = "Diagnostics.previous.txt";
        //private string previousPreviousFileName = "Diagnostics.previous.previous.txt";
        //private string previousPreviousPreviousFileName = "Diagnostics.previous.previous.previous.txt";

        //private void WriteDiagnostics(bool flush)
        //{
        //    writeDiagnostics = true;
        //    if (flush || diagnostics.Length > 10000)
        //    {
        //        if (writeDiagnostics || (flush && diagnosticsWritten < 3))
        //        {
        //            try
        //            {
        //                System.IO.File.Delete(Name + previousPreviousPreviousFileName);
        //            }
        //            catch (System.IO.IOException) { }

        //            try
        //            {
        //                System.IO.File.Move(Name + previousPreviousFileName, Name + previousPreviousPreviousFileName);
        //            }
        //            catch (System.IO.IOException) { }

        //            try
        //            {
        //                System.IO.File.Move(Name + previousFileName, Name + previousPreviousFileName);
        //            }
        //            catch (System.IO.IOException) { }

        //            try
        //            {
        //                System.IO.File.Move(Name + xfileName, Name + previousFileName);
        //            }
        //            catch (System.IO.IOException) { }

        //            System.IO.TextWriter file = new System.IO.StreamWriter(Name + xfileName);
        //            file.Write(diagnostics.ToString());
        //            file.Close();

        //            diagnostics.Length = 0;
        //            writeDiagnostics = false;
        //            diagnosticsWritten++;
        //        }
        //        else
        //        {
        //            // Remove all but the last 1MByte.
        //            diagnostics = new System.Text.StringBuilder(diagnostics.ToString(), diagnostics.Length - 1000000, 1000000, 10010000);
        //        }
        //    }
        //}


        //private void Initialise()
        //{
            //            diagnostics = new System.Text.StringBuilder(10010000);
        //}

        //diagnostics.Append(this.Name + ": " + DateTime.Now.ToLongTimeString() + "." + DateTime.Now.Millisecond + 
        //    "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ :~)");
        //diagnostics.Append(newline);
        //writeDiagnostics = true;
        //WriteDiagnostics(false);


		//public override void Disconnect ()
		//{
        //                WriteDiagnostics(true);
        //}
    //}
}

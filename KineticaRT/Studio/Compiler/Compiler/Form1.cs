using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.CodeDom.Compiler;

namespace Compiler
{
    public partial class Form1 : Form
    {

#if CompactEnabled
        private static bool compactEnabled =  true;
#else
        private static bool compactEnabled = false;
#endif

        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string[] cs = System.IO.Directory.GetFiles(Application.StartupPath,"*.cs");
            string[] dll = System.IO.Directory.GetFiles(Application.StartupPath, "*.dll");
            System.Collections.Generic.List<string> references = new List<string>();
            System.Collections.Generic.List<string> excludes = new List<string>() ;
            excludes.Add("BallardAPI");
            excludes.Add("BallrdAPIExtended");
            excludes.Add("OPaC.uxTheme.Win32");
            excludes.Add("AxisVideo");

            foreach (string dll_file in dll)
            {
                if (!excludes.Contains(Path.GetFileNameWithoutExtension(dll_file)))
                {
                    references.Add(dll_file);
                }
            }

            string[] reference = references.ToArray();
            

            foreach (string cs_file in cs)
            {
                // Try to compile it.
                listBox1.Items.Add(System.IO.Path.GetFileNameWithoutExtension(cs_file));
                if (!CreateDllFromSource(cs_file, Path.ChangeExtension(cs_file, "dll"), reference, false)) break;
            }

        }


        /// <summary>
        /// Creates a dll in the named file from the named source file
        /// </summary>
        /// <param name="SourceFilename">Source file</param>
        /// <param name="DllFilename">File where dll is created</param>
        /// <param name="References">Array of assembly names to reference in the dll</param>
        /// <param name="compact">Uses the compact framework if true.</param>
        /// <returns>True if succeeded</returns>
        /// <remarks>This function is invoked automatically if a panel dll is requested that doesn't exist, but a .cs file of the same name does exist.</remarks>
        public static bool CreateDllFromSource(string SourceFilename, string DllFilename, string[] References, bool compact)
        {
            if (!File.Exists(SourceFilename)) return false;

            // Create a compiler.
            CodeDomProvider provider = new Microsoft.CSharp.CSharpCodeProvider();
            //ICodeCompiler		compiler		= provider.CreateCompiler () ;
            CompilerParameters parameters = new CompilerParameters();
            CompilerResults results;
            string class_name = Path.GetFileNameWithoutExtension(DllFilename);
            string res_filename;

            AddReferences(parameters, References, compact);

            parameters.MainClass = class_name;
            parameters.OutputAssembly = DllFilename;

            //if (compactEnabled && compact)
            //{
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Data.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Drawing.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Windows.Forms.dll");
            //    parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Xml.dll");

            //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.dll"));
            //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
            //    parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));

            //}
            //else
            //{
            //    parameters.ReferencedAssemblies.Add("System.dll");
            //    parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            //    parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            //    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.dll"));
            //    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
            //    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));
            //}

            //if there is a relevant resource file, embed it
            res_filename = "KineticaRT.Panel." + class_name + ".resources";
            res_filename = Path.Combine(Path.GetDirectoryName(DllFilename), res_filename);
            if (File.Exists(res_filename))
            {
                parameters.CompilerOptions = "/res:\"" + res_filename + "\"";
            }

            if (References != null)
            {
                foreach (string assembly in References)
                {
                    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, assembly));
                }
            }

            //results = compiler.CompileAssemblyFromFile(parameters, SourceFilename);
            results = provider.CompileAssemblyFromFile(parameters, SourceFilename);

            // Check for erorrs.
            if (results.Errors.HasErrors)
            {
                string lcErrorMsg = "";

                // *** Create Error String
                lcErrorMsg = results.Errors.Count.ToString() + " Errors:";
                for (int x = 0; x < results.Errors.Count; x++)
                {
                    lcErrorMsg = lcErrorMsg + "\r\nLine: " + results.Errors[x].Line.ToString() + " - " + results.Errors[x].ErrorText;
                }

                return MessageBox.Show(SourceFilename + ": " + lcErrorMsg, "Panel Compiler", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.OK;
            }
            return true;
        }

        private static void AddReferences(CompilerParameters parameters, string[] references, bool compact)
        {
            parameters.IncludeDebugInformation = false;

            if (compactEnabled && compact)
            {
                parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.dll");
                parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Data.dll");
                parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Drawing.dll");
                parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add(@"F:\Program Files (x86)\Microsoft Visual Studio 8\SmartDevices\SDK\CompactFramework\2.0\v2.0\WindowsCE\System.Xml.dll");

                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.Controls.dll"));
                parameters.ReferencedAssemblies.Add(System.IO.Path.Combine(Application.StartupPath, "KineticaRT.Compact.Controls.Frame.dll"));

            }
            else
            {
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

                parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.dll"));
                parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, "KineticaRT.Controls.Frame.dll"));
            }

            //if (references == null)
            //{
            //    GetReferencedAssemblies();
            //}

            if (references != null)
            {
                foreach (string assembly in references)
                {
                    parameters.ReferencedAssemblies.Add(Path.Combine(Application.StartupPath, assembly));
                }
            }

        }

    }
}
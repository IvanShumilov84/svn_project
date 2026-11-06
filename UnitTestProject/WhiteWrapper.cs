using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TestStack.White;
using TestStack.White.AutomationElementSearch;
using TestStack.White.Configuration;
using TestStack.White.Factory;
using TestStack.White.Sessions;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Custom;
using TestStack.White.UIItems.Actions;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.Finders;

using KineticaRT;
using KineticaRT.Controls;
using KineticaRT.Channels;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading;

//Used For Custom control
using System.Windows.Automation;

//using System.Windows.Automation; //MS UI Automation test

namespace UnitTestProject
{
    class WhiteWrapper : IDisposable
    {

        private readonly Application _host = null;
        //ft private readonly Window _mainWindow = null;
//        public Window _mainWindow = null;
        private MyStudioDisplay dspStatus = null;
        private TextBox status = null;

        public WhiteWrapper(string path, string exeArguments)
        {
            System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo();
            processStartInfo.Arguments = exeArguments;
            processStartInfo.WorkingDirectory = Path.GetDirectoryName(path);
            processStartInfo.FileName = path;
            _host = Application.Launch(processStartInfo);

            System.Threading.Thread.Sleep(5000);//permettre au HMI de compiler et de demarrer
            Window win = GetWindowWithTimeout("Welcome Screen", 20);
            dspStatus = GetControlByAutomationId<MyStudioDisplay>(win, "dspStatus");
            Assert.IsNotNull(dspStatus);
            status = new TextBox(dspStatus.AutomationElement, dspStatus.ActionListener);
            List<Window> windows = _host.GetWindows();

        }

        public WhiteWrapper(string path, string exeArguments, string mainWindowTitle)
            : this(path, exeArguments)
        {
            GetWindow(mainWindowTitle);
        }

        public void Dispose()
        {
            if (_host != null)
                _host.Kill();
        }

        public void SaveCache()
        {
            if (_host != null)
                _host.ApplicationSession.Save();
        }

        public Window GetWindow(string title)
        {
            try
            {
                return _host.GetWindow(title, InitializeOption.NoCache.AndIdentifiedBy(title));
            }
            catch
            {
                return null;
            }
        }

        public Window GetWindowWithTimeout(string title, int timeout)
        {
            Window window = _host.GetWindow(title, InitializeOption.NoCache.AndIdentifiedBy(title));
            try
            {
                for (int I = 0; I < timeout && window == null; I++)
                {
                    System.Threading.Thread.Sleep(1000);
                    window = GetWindow(title);
                }
            }
            catch
            {
                window = null;
            }
            Assert.IsNotNull(window);
            return window;
        }

        public Window GetChildWindow(Window window, string title)
        {
            try
            {
                return window.ModalWindow(title);
            }
            catch
            {
                return null;
            }

        }
 
        public TControl GetControl<TControl>(Window window, string controlName) where TControl : UIItem
        {
            try
            {
                return window.Get<TControl>(controlName);
            }
            catch
            {
                return null;
            }
        }

        public TControl GetControlByAutomationId<TControl>(Window window, string controlName) where TControl : UIItem
        {
            try
            {
                return window.Get<TControl>(SearchCriteria.ByAutomationId(controlName));
            }
            catch
            {
                return null;
            }
        }

        public TControl GetControlByAutomationId<TControl>(Panel panel, string controlName) where TControl : UIItem
        {
            try
            {
                return panel.Get<TControl>(SearchCriteria.ByAutomationId(controlName));
            }
            catch
            {
                return null;
            }
        }
        
        public void WaitForStatusWithTimeout(string statusName, int timeout)
        {
            for (int loop = 0; loop < timeout && status.Name != statusName; loop++)
            {
                Thread.Sleep(1000);
            }
            Assert.AreEqual(statusName, status.Name);
        }
    }

    //Specify the ControlType which corresponds to the top level of Custom UI Item.
    //White needs this when finding this item inside the window.
    [ControlTypeMapping(CustomUIItemType.Pane)]
    public class MyStudioButton : CustomUIItem
    {
        // Implement these two constructors. The order of parameters should be same.
        public MyStudioButton(AutomationElement automationElement, ActionListener actionListener)
            : base(automationElement, actionListener)
        {
        }

        //Empty constructor is mandatory with protected or public access modifier.
        protected MyStudioButton() { }

       /// <summary>
        /// Specifies if button accepts input
        /// </summary>
        protected bool inputEnabledValue;

    }

    //Specify the ControlType which corresponds to the top level of Custom UI Item.
    //White needs this when finding this item inside the window.
    [ControlTypeMapping(CustomUIItemType.Pane)]
    public class MyLampIndicator : CustomUIItem 
    {
        // Implement these two constructors. The order of parameters should be same.
        public MyLampIndicator(AutomationElement automationElement, ActionListener actionListener)
            : base(automationElement, actionListener)
        {
        }

        //Empty constructor is mandatory with protected or public access modifier.
        protected MyLampIndicator() { }
    }

    //Specify the ControlType which corresponds to the top level of Custom UI Item.
    //White needs this when finding this item inside the window.
    [ControlTypeMapping(CustomUIItemType.Pane)]
    public class MyStudioDisplay : CustomUIItem
    {

        // Implement these two constructors. The order of parameters should be same.
        public MyStudioDisplay(AutomationElement automationElement, ActionListener actionListener)
            : base(automationElement, actionListener)
        {
        }

        //Empty constructor is mandatory with protected or public access modifier.
        protected MyStudioDisplay() { }

        //public virtual string Source
        //{
        //    get
        //    {
        //        return (string)GetProperty("Source");
        //    }
        //}
        ////Sample method
        //public virtual void EnterDate(DateTime dateTime)
        //{
        //    //Base class, i.e. CustomUIItem has property called Container. Use this find the items within this.
        //    //Can also use SearchCriteria for find items
        //    Container.Get<TextBox>("day").Text = dateTime.Day.ToString();
        //    Container.Get<TextBox>("month").Text = dateTime.Month.ToString();
        //    Container.Get<TextBox>("year").Text = dateTime.Year.ToString();
        //}

    }
//    KineticaRT.Controls.Frame()
    [ControlTypeMapping(CustomUIItemType.Pane)]
    public class MyStudioFrame : CustomUIItem
    {

        // Implement these two constructors. The order of parameters should be same.
        public MyStudioFrame(AutomationElement automationElement, ActionListener actionListener)
            : base(automationElement, actionListener)
        {
        }

        //Empty constructor is mandatory with protected or public access modifier.
        protected MyStudioFrame() { }

        //public virtual string Source
        //{
        //    get
        //    {
        //        return (string)GetProperty("Source");
        //    }
        //}
        ////Sample method
        //public virtual void EnterDate(DateTime dateTime)
        //{
        //    //Base class, i.e. CustomUIItem has property called Container. Use this find the items within this.
        //    //Can also use SearchCriteria for find items
        //    Container.Get<TextBox>("day").Text = dateTime.Day.ToString();
        //    Container.Get<TextBox>("month").Text = dateTime.Month.ToString();
        //    Container.Get<TextBox>("year").Text = dateTime.Year.ToString();
        //}

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using InsulationCutFileGeneratorMVC.MVC_Model;
using InsulationCutFileGeneratorMVC.MVC_View;
using InsulationCutFileGeneratorMVC.MVC_Controller;

namespace InsulationCutFileGeneratorMVC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataEntryView view = new DataEntryView
            {
                Visible = false
            };
            IList dataEntries = new ArrayList();
            DataEntryController controller = new DataEntryController(view, dataEntries);
            controller.LoadView();
            view.ShowDialog();
        }
    }
}

using InsulationCutFileGeneratorMVC.MVC_Controller;
using InsulationCutFileGeneratorMVC.MVC_View;
using System;
using System.Collections;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
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
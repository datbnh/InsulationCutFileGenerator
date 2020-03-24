using InsulationCutFileGeneratorMVC.MVC_Controller;
using InsulationCutFileGeneratorMVC.MVC_View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            DataEntryView view = new DataEntryView();
            view.Parent = tabControl1.TabPages[0];
            view.Dock = DockStyle.Top;

            IList dataEntries = new ArrayList();
            DataEntryController controller = new DataEntryController(view, dataEntries);
            controller.LoadView();

            FormSettings settingsForm = new FormSettings();
            settingsForm.Parent = tabControl1.TabPages[1];

            AutoSize = true;
        }


    }
}

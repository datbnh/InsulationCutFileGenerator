using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC.MVC_View
{
    public partial class CodePreviewWindow : Form
    {
        public string PreviewText
        {
            get => richTextBox1.Text; set
            {
                richTextBox1.Text = value;
                label1.Text = richTextBox1.TextLength.ToString() + " bytes.";
            }
        }

        public CodePreviewWindow()
        {
            InitializeComponent();
        }
    }
}
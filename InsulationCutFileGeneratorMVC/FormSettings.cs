using InsulationCutFileGeneratorMVC.MVC_View;
using System;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC
{
    public partial class FormSettings : UserControl
    {
        public FormSettings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadUIToSettings();
            Settings.SaveSettingsToRegistry();
            label2.Visible = true;
            timer1.Enabled = true;
            timer1.Start();
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadSettingsToUI();
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Are you sure you want to reset all settings to their values?" +
                Environment.NewLine +
                "This cannot be undone.",
                "Reset Settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                == DialogResult.No)
                return;
            Settings.Instance = Settings.DefaultSettings;
            LoadSettingsToUI();
            Settings.SaveSettingsToRegistry();
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            textBox1.Enabled = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            textBox2.Enabled = checkBox4.Checked;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            LoadSettingsToUI();
            button1.Enabled = false;
            button2.Enabled = false;
        }
        private void LoadSettingsToUI()
        {
            checkBox1.Checked = Settings.Instance.IsSingleEntry;
            checkBox2.Checked = Settings.Instance.IsUseFeMaleInstead;
            switch (Settings.Instance.PittsburgSixMmValidationMode)
            {
                case PittsburghSixMmValidationMode.Ignore:
                    radioButton1.Checked = true;
                    break;

                case PittsburghSixMmValidationMode.Warning:
                    radioButton2.Checked = true;
                    break;

                case PittsburghSixMmValidationMode.Enforcing:
                    radioButton3.Checked = true;
                    break;

                default:
                    break;
            }
            checkBox3.Checked = Settings.Instance.UsePredefinedFileName;
            checkBox4.Checked = Settings.Instance.UsePredefinedPath;
            textBox1.Text = Settings.Instance.PredefinedFileName;
            textBox2.Text = Settings.Instance.PredefinedPath;
        }

        private void LoadUIToSettings()
        {
            Settings.Instance.IsSingleEntry = checkBox1.Checked;
            Settings.Instance.IsUseFeMaleInstead = checkBox2.Checked;
            Settings.Instance.UsePredefinedFileName = checkBox3.Checked;
            Settings.Instance.UsePredefinedPath = checkBox4.Checked;
            Settings.Instance.PredefinedFileName = textBox1.Text;
            Settings.Instance.PredefinedPath = textBox2.Text;
            if (radioButton1.Checked)
                Settings.Instance.PittsburgSixMmValidationMode = PittsburghSixMmValidationMode.Ignore;
            else if (radioButton2.Checked)
                Settings.Instance.PittsburgSixMmValidationMode = PittsburghSixMmValidationMode.Warning;
            else
                Settings.Instance.PittsburgSixMmValidationMode = PittsburghSixMmValidationMode.Enforcing;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Visible = false;
            timer1.Stop();
        }

        private void checkBox1_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible == true)
            {
                if (FormPasswordReader.VerifyPassword(Settings.Instance.PasswordHash))
                    this.Enabled = true;
                else
                    this.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormPasswordSetter passwordReader = new FormPasswordSetter(Settings.Instance.PasswordHash);
            passwordReader.ShowDialog();
        }
    }
}
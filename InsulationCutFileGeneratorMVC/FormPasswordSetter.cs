using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC.MVC_View
{
    public partial class FormPasswordSetter : Form
    {
        private string oldPasswordHash;
        public FormPasswordSetter(string oldPasswordHash)
        {
            InitializeComponent();
            this.oldPasswordHash = oldPasswordHash;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(oldPasswordHash) 
                || (Settings.GetPasswordHash(textBox1.Text) == oldPasswordHash))
            {
                ConfirmAndSaveNewPassword();
            } else
            {
                label4.Visible = true;
            }

        }

        private void ConfirmAndSaveNewPassword()
        {
            if (textBox2.Text.Equals(textBox3.Text))
            {
                Settings.SavePassword(textBox2.Text);
                this.Close();
            }
        }

        private void FormPasswordSetter_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(oldPasswordHash))
            {
                textBox1.Visible = false;
                label1.Visible = false;
                button1.Text = "Set";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Equals(textBox2.Text))
            {
                if (string.IsNullOrEmpty(textBox3.Text))
                {
                    label5.Text = "Password cannot be empty.";
                    label5.Visible = true;
                    button1.Enabled = false;
                } else
                {
                    label5.Visible = false;
                    button1.Enabled = true;
                }
            } else
            {
                label5.Text = "Those passwords do not match.";
                label5.Visible = true;
                button1.Enabled = false;
            }
        }
    }
}

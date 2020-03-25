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

namespace InsulationCutFileGeneratorMVC
{
    public partial class FormPasswordReader : Form
    {
        private string passwordHash;

        public bool IsPasswordMatched = false;

        public FormPasswordReader()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/212510/what-is-the-easiest-way-to-encrypt-a-password-when-i-save-it-to-the-registry
            var hash = Settings.GetPasswordHash(textBox1.Text);

            if (hash == passwordHash)
            {
                IsPasswordMatched = true;
                this.Close();
            }
            else
            {
                SystemSounds.Beep.Play();
                label2.Visible = true;
            }
        }

        public static bool VerifyPassword(string passwordHash)
        {
            var form = new FormPasswordReader();
            form.verifyPassword(passwordHash);
            return form.IsPasswordMatched;
        }

        private void verifyPassword(string passwordHash)
        {
            this.passwordHash = passwordHash;
            this.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
        }
    }
}

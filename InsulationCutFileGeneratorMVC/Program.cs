using InsulationCutFileGeneratorMVC.MVC_View;
using System;
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

            if (DateTime.Now >= new DateTime(2020, 5, 1))
            {
                MessageBox.Show("You are running Beta version of this software."
                    + Environment.NewLine + Environment.NewLine
                    + "This version has expired. " +
                    " Please update to new version to continue.",
                    "Beta Version Has Expired", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(Settings.Instance.PasswordHash))
            {
                MessageBox.Show("Admin password is not set."
                    + Environment.NewLine + Environment.NewLine
                    + "Set a new password in the next step to" +
                    " prevent unauthorised changes in software's settings.",
                    "Admid Password Is Not Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormPasswordSetter passwordReader = new FormPasswordSetter(null);

                passwordReader.ShowDialog();

                if (!string.IsNullOrEmpty(Settings.Instance.PasswordHash))
                {
                    MessageBox.Show("Admin password is now set."
                    + Environment.NewLine + Environment.NewLine
                    + "Please launch the program again.",
                    "Password Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Application.Run(new FormMain());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC.Helpers
{
    public partial class TimedMessageBox : Form
    {
        private readonly int interval;
        private int currentTime = 0;
        public TimedMessageBox(string path, string entryId, int interval)
        {
            InitializeComponent();
            Text = "Cut File Exported Successfully";
            linkLabel1.Text = path;
            label3.Text = "for Entry " + entryId + ".";
            this.interval = interval;
            label1.Text = "Closing in " + (interval - currentTime) + " second" + (interval - currentTime == 1 ? "" : "s") + "...";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentTime++;
            label1.Text = "Closing in " + (interval - currentTime) + " second" + (interval - currentTime == 1 ? "" : "s") + "...";
            if (currentTime >= interval)
                this.Close();
        }

        private void TimedMessageBox_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(linkLabel1.Text);
            label4.Visible = true;
        }
    }
}

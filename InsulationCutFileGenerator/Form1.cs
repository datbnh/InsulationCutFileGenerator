using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsulationCutFileGenerator
{
    public partial class Form1 : Form
    {
        private List<EntryViewModel> dataEntries = new List<EntryViewModel>();
        public Form1()
        {
            InitializeComponent();
            AddEntry();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GenerateFiles();
        }

        private void GenerateFiles()
        {
            foreach (var entry in dataEntries)
            {
                if (!entry.DataEntry.Validate())
                {
                    MessageBox.Show("Entry " + entry.Index + " is in valid:\r\n" + entry.DataEntry.GetFullDescription(), 
                        "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var folderPath = Path.GetDirectoryName(saveFileDialog1.FileName);
                var time = DateTime.Now.ToString("ddMMHHMM");
                bool isError = false;
                try
                {
                    foreach (var item in dataEntries)
                    {
                        var fileName = string.Format("{0}-{1}_{2}x{3}x{4}", time, item.Index, item.DataEntry.ShortEdge, item.DataEntry.LongEdge, item.DataEntry.Quantity);
                        item.DataEntry.GenerateGCode(Path.Combine(folderPath, fileName) + ".txt");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while generating file: " + saveFileDialog1.FileName +
                        "\r\n" + ex.Message, "Error Generating File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isError = isError | true;
                }

                if (!isError)
                    MessageBox.Show("CUT file(s) generated successfully.", "File Generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            AddEntry();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            RemoveEntry();
        }

        private void AddEntry()
        {

            if (dataEntries.Count > 0)
            {
                var newDataEntry = dataEntries.Last().Clone(dataEntries.Count + 1);
                newDataEntry.IsHeaderVisible = false;
                newDataEntry.Dock = DockStyle.Fill;
                dataEntries.Add(newDataEntry);
            }
            else
            {
                dataEntries.Add(new EntryViewModel(1, 0, 0, 0, 1, ""));
            }
            flowLayoutPanel1.Controls.Add(dataEntries.Last());
            UpadateEntryCount();
        }

        private void RemoveEntry()
        {
            var idx = dataEntries.Count - 1;
            if (idx >= 0)
            {
                dataEntries.RemoveAt(idx);
                flowLayoutPanel1.Controls.RemoveAt(idx);
            }
            UpadateEntryCount();
        }

        private void UpadateEntryCount()
        {
            label1.Text = (dataEntries.Count == 1 ? "Entry: " : "Entries: ") + dataEntries.Count.ToString();
            button1.Text = dataEntries.Count == 1 ? "Generate File" : "Generate Files";
        }
    }
}

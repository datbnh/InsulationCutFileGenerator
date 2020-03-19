using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace InsulationCutFileGenerator
{
    public partial class Form1 : Form
    {
        private List<DuctEntry> dataEntries = new List<DuctEntry>();

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
                if (!entry.Control.Validate())
                {
                    MessageBox.Show("Entry " + entry.Index + " is in valid:\r\n" + entry.Data.GetFullDescription(),
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
                        var fileName = string.Format("{0}-{1}_{2}x{3}x{4}", time, item.Index, item.Data.FemaleSize, item.Data.MaleSize, item.Data.Quantity);
                        var filePath = Path.Combine(folderPath, fileName) + ".txt";
                        item.Control.ExportCutFile(filePath);
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
            var newDataEntry = new DuctEntry(1);
            if (dataEntries.Count > 0)
            {
                dataEntries.Last().Clone(dataEntries.Count + 1);
            }
            newDataEntry.View.IsHeaderVisible = false;
            newDataEntry.View.Dock = DockStyle.Fill;
            dataEntries.Add(newDataEntry);
            flowLayoutPanel1.Controls.Add(dataEntries.Last().View);
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
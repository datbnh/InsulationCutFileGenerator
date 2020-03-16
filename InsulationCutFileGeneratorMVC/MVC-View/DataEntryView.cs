using InsulationCutFileGeneratorMVC.Helpers;
using System;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC
{
    public partial class DataEntryView : Form
    {
        public DataEntryViewMode Mode;

        private DataEntryController controller;

        private bool isDataChanged = false;
        private int lastSelectedRowIndex = -1;

        public DataEntryView()
        {
            InitializeComponent();
            InitializeInsulationTypeComboBox();
            InitializeInsulationThicknessComboBox();
        }

        public string DuctId { get { return textBox3.Text; } set { textBox3.Text = value; } }

        public string EntryId { get { return textBox1.Text; } set { textBox1.Text = value; } }

        public InsulationThickness InsulationThickness
        {
            get => (InsulationThickness)(((ComboBoxItem)textBox7.SelectedItem).Value);
            set => SelectInsulationThickness(value);
        }

        public InsulationType InsulationType
        {
            get => (InsulationType)(((ComboBoxItem)textBox6.SelectedItem).Value);
            set => SelectInsulationType(value);
        }

        public bool IsDataChanged { get { return isDataChanged; } set { isDataChanged = value; if (value) Text = "*"; else Text = ""; } }

        public string JobName { get { return textBox2.Text; } set { textBox2.Text = value; } }

        public int PittsburghSize
        {
            get => (int)numericUpDown1.Value;
            set => numericUpDown1.Value = value < numericUpDown1.Minimum ?
                numericUpDown1.Minimum : value > numericUpDown1.Maximum ? numericUpDown1.Maximum : value;
        }

        public int SixMmSize
        {
            get => (int)numericUpDown2.Value;
            set => numericUpDown2.Value = value < numericUpDown2.Minimum ?
                numericUpDown2.Minimum : value > numericUpDown2.Maximum ? numericUpDown2.Maximum : value;
        }

        public int Quantity
        {
            get => (int)numericUpDown3.Value;
            set => numericUpDown3.Value = value < numericUpDown3.Minimum ?
                numericUpDown3.Minimum : value > numericUpDown3.Maximum ? numericUpDown3.Maximum : value;
        }


        #region Events raised back to controller
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            controller.CancelModifyingSelectedEntry();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            controller.ClearCurrentEntry();
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            controller.DuplicateSelectedEntry();
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            controller.BeginModifyingSelectedEntry();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            controller.CreateNewEntry();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            controller.AddOrUpdateCurrentEntry();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            controller.RemoveCurrentEntry();
        }

        private void listDataEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Mode == DataEntryViewMode.New || Mode == DataEntryViewMode.Edit) && IsDataChanged)
                if (MessageBox.Show("Discard changes and continue?", "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.No)
                    return;
            if (dataEntriesListView.SelectedItems.Count <= 0)
                return;
            //Console.WriteLine(lastSelectedRowIndex + " > " + listDataEntries.SelectedIndices[0]);
            if (dataEntriesListView.SelectedIndices[0] == lastSelectedRowIndex)
                return;
            controller.SelectedEntryChanged(dataEntriesListView.SelectedItems[0].Text);
            lastSelectedRowIndex = dataEntriesListView.SelectedIndices[0];
        }



        #endregion Events raised back to controller

        #region View implementation
        private void InitializeInsulationThicknessComboBox()
        {
            var insulationThicknesseses = (InsulationThickness[])Enum.GetValues(typeof(InsulationThickness));
            textBox7.Items.Clear();
            for (int i = 0; i < insulationThicknesseses.Length; i++)
            {

                var item = insulationThicknesseses[i];
                textBox7.Items.Add(
                    new ComboBoxItem(i, (string.IsNullOrEmpty(item.GetId()) ? "" : "" + item.GetId() + " | ")
                    + item.GetDescription(), item));
            }
            textBox7.SelectedIndex = 0;
        }

        private void InitializeInsulationTypeComboBox()
        {
            var insulationTypes = (InsulationType[])Enum.GetValues(typeof(InsulationType));
            textBox6.Items.Clear();
            for (int i = 0; i < insulationTypes.Length; i++)
            {
                var item = insulationTypes[i];
                textBox6.Items.Add(
                    new ComboBoxItem(i, (string.IsNullOrEmpty(item.GetId()) ? "" : "" + item.GetId() + " | ")
                    + item.GetDescription(), item));
            }
            textBox6.SelectedIndex = 0;
        }

        private void SelectInsulationThickness(InsulationThickness value)
        {
            for (int i = 0; i < textBox7.Items.Count; i++)
            {
                var item = textBox7.Items[i];
                if (((InsulationThickness)(((ComboBoxItem)item).Value)).GetId().Equals(value.GetId()))
                {
                    textBox7.SelectedItem = item;
                    break;
                }
            }
        }
        private void SelectInsulationType(InsulationType value)
        {
            for (int i = 0; i < textBox6.Items.Count; i++)
            {
                var item = textBox6.Items[i];
                if (((InsulationType)(((ComboBoxItem)item).Value)).GetId().Equals(value.GetId()))
                {
                    textBox6.SelectedItem = item;
                    break;
                }
            }
        }

        public void SetController(DataEntryController controller)
        {
            this.controller = controller;
        }


        public void SetMode(DataEntryViewMode mode)
        {
            this.Mode = mode;
            switch (this.Mode)
            {
                case DataEntryViewMode.New:
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    numericUpDown1.ReadOnly = false;
                    numericUpDown2.ReadOnly = false;
                    textBox6.Enabled = !false;
                    textBox7.Enabled = !false;
                    buttonModify.Enabled = false;
                    buttonDuplicate.Enabled = false;
                    buttonSave.Text = "Add";
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonClear.Enabled = true;
                    buttonRemove.Enabled = false;
                    groupBox1.Text = "New Entry";
                    break;

                case DataEntryViewMode.View:
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    numericUpDown1.ReadOnly = true;
                    numericUpDown2.ReadOnly = true;
                    textBox6.Enabled = !true;
                    textBox7.Enabled = !true;
                    buttonModify.Enabled = true;
                    buttonDuplicate.Enabled = true;
                    buttonSave.Text = "Update";
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonClear.Enabled = false;
                    buttonRemove.Enabled = true;
                    groupBox1.Text = "Displaying Entry [" + textBox1.Text + "]";
                    break;

                case DataEntryViewMode.Edit:
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = false;
                    textBox3.ReadOnly = false;
                    numericUpDown1.ReadOnly = false;
                    numericUpDown2.ReadOnly = false;
                    textBox6.Enabled = !false;
                    textBox7.Enabled = !false;
                    buttonModify.Enabled = false;
                    buttonDuplicate.Enabled = false;
                    buttonSave.Text = "Update";
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonClear.Enabled = true;
                    buttonRemove.Enabled = true;
                    groupBox1.Text = "Modifying Entry [" + textBox1.Text + "]";
                    break;

                default:
                    break;
            }
        }

       

        internal void SetSelectedEntry(DataEntry currentEntry)
        {
            foreach (ListViewItem row in dataEntriesListView.Items)
            {
                row.Selected = row.Text.Equals(currentEntry.Id);
            }
        }
        public string GetSelectedEntryId()
        {
            if (dataEntriesListView.SelectedItems.Count > 0)
                return dataEntriesListView.SelectedItems[0].Text;
            return string.Empty;
        }

        internal void AddEntryToListView(DataEntry currentEntry)
        {
            ListViewItem item;
            item = dataEntriesListView.Items.Add(currentEntry.Id);
            item.SubItems.Add(currentEntry.JobName);
            item.SubItems.Add(currentEntry.DuctId);
            item.SubItems.Add(currentEntry.PittsburghSize.ToString());
            item.SubItems.Add(currentEntry.SixMmSize.ToString());
            item.SubItems.Add(currentEntry.InsulationType.GetId());
            item.SubItems.Add(currentEntry.InsulationThickness.GetId());
            item.SubItems.Add(currentEntry.Quantity.ToString());
        }

        internal void UpdateEntryInListVew(DataEntry currentEntry)
        {
            ListViewItem rowToUpdate = null;

            foreach (ListViewItem row in dataEntriesListView.Items)
            {
                if (row.Text.Equals(currentEntry.Id))
                {
                    rowToUpdate = row;
                }
            }

            if (rowToUpdate != null)
            {
                rowToUpdate.Text = currentEntry.Id;
                rowToUpdate.SubItems[1].Text = currentEntry.JobName;
                rowToUpdate.SubItems[2].Text = currentEntry.DuctId;
                rowToUpdate.SubItems[3].Text = currentEntry.PittsburghSize.ToString();
                rowToUpdate.SubItems[4].Text = currentEntry.SixMmSize.ToString();
                rowToUpdate.SubItems[5].Text = currentEntry.InsulationType.GetId();
                rowToUpdate.SubItems[6].Text = currentEntry.InsulationThickness.GetId();
                rowToUpdate.SubItems[7].Text = currentEntry.Quantity.ToString();
            }
        }

        internal void RemoveEntryFromListView(DataEntry entry)
        {
            for (int i = 0; i < dataEntriesListView.Items.Count; i++)
            {
                if (dataEntriesListView.Items[i].Text.Equals(entry.Id))
                {
                    dataEntriesListView.Items.RemoveAt(i);
                    dataEntriesListView.Focus();
                    break;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            IsDataChanged = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            IsDataChanged = true;
        }

        private void textBox4_ValueChanged(object sender, EventArgs e)
        {
            IsDataChanged = true;
        }

        private void textBox5_ValueChanged(object sender, EventArgs e)
        {
            IsDataChanged = true;
        }
        private void textBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDataChanged = true;
        }


        private void textBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsDataChanged = true;
        }
        
        #endregion View implementation


    }
}
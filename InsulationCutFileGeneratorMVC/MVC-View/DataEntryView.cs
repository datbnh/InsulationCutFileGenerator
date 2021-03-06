﻿using InsulationCutFileGeneratorMVC.Core.DataEntryValidator;
using InsulationCutFileGeneratorMVC.Helpers;
using InsulationCutFileGeneratorMVC.MVC_Controller;
using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace InsulationCutFileGeneratorMVC.MVC_View
{
    public partial class DataEntryView : UserControl, IDataEntryView
    {
        private readonly CodePreviewWindow codePreviewWindow;

        private DataEntryViewMode mode;

        private DataEntryController controller;

        private bool isDataChanged = false;
        private int lastSelectedRowIndex = -1;

        private readonly string captionTextFormatter
            = "{0}{1} | Insulation Cut File Generator";

        internal void ExportSelectedEntry()
        {
            controller.ExportSelectedEntry();
        }

        public DataEntryView()
        {
            InitializeComponent();
            InitializeInsulationTypeComboBox();
            InitializeInsulationThicknessComboBox();
            codePreviewWindow = new CodePreviewWindow(this);
            codePreviewWindow.Hide();
        }

        public string DuctId { get { return textBoxDuctId.Text; } set { textBoxDuctId.Text = value; } }

        public string EntryId { get { return textBoxEntryId.Text; } set { textBoxEntryId.Text = value; } }

        public InsulationThickness InsulationThickness
        {
            get => (InsulationThickness)(((ComboBoxItem)comboBoxInsulationThickness.SelectedItem).Value);
            set => SelectInsulationThickness(value);
        }

        public InsulationType InsulationType
        {
            get => (InsulationType)(((ComboBoxItem)comboBoxInsulationType.SelectedItem).Value);
            set => SelectInsulationType(value);
        }

        public bool IsDataChanged { get { return isDataChanged; } set { isDataChanged = value; UpdateCaptionText(); } }

        public string JobName { get { return textBoxJobName.Text; } set { textBoxJobName.Text = value; } }

        public int PittsburghSize
        {
            get => (int)numericUpDownPittsburghSize.Value;
            set => numericUpDownPittsburghSize.Value = value < numericUpDownPittsburghSize.Minimum ?
                numericUpDownPittsburghSize.Minimum : value > numericUpDownPittsburghSize.Maximum ? numericUpDownPittsburghSize.Maximum : value;
        }

        public int SixMmSize
        {
            get => (int)numericUpDownSixMmSize.Value;
            set => numericUpDownSixMmSize.Value = value < numericUpDownSixMmSize.Minimum ?
                numericUpDownSixMmSize.Minimum : value > numericUpDownSixMmSize.Maximum ? numericUpDownSixMmSize.Maximum : value;
        }

        public int Quantity
        {
            get => (int)numericUpDownQuantity.Value;
            set => numericUpDownQuantity.Value = value < numericUpDownQuantity.Minimum ?
                numericUpDownQuantity.Minimum : value > numericUpDownQuantity.Maximum ? numericUpDownQuantity.Maximum : value;
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

        internal void ShowValidationInfo(DataEntryValidationResult result)
        {
            if (result.IsValid)
            {
                textBox4.Clear();
                textBox4.AppendText(result.Description);
            }
            else
            {
                textBox4.Clear();
                textBox4.AppendText("Invalid entry: ", Color.DarkRed, FontStyle.Bold);
                textBox4.AppendText(result.Description);
            }
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
            if (dataEntriesListView.SelectedItems.Count <= 0)
                return;
            if (dataEntriesListView.SelectedIndices[0] == lastSelectedRowIndex)
                return;
            if ((mode == DataEntryViewMode.New || mode == DataEntryViewMode.Edit) && IsDataChanged)
                if (MessageBox.Show("Discard changes and continue?", "Discard Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    == DialogResult.No)
                    return;

            controller.SelectEntry(dataEntriesListView.SelectedItems[0].Text);
            lastSelectedRowIndex = dataEntriesListView.SelectedIndices[0];
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            controller.PreviewGCode();
        }

        #endregion Events raised back to controller

        #region View implementation

        private void InitializeInsulationThicknessComboBox()
        {
            var insulationThicknesseses = (InsulationThickness[])Enum.GetValues(typeof(InsulationThickness));
            comboBoxInsulationThickness.Items.Clear();
            for (int i = 0; i < insulationThicknesseses.Length; i++)
            {
                var item = insulationThicknesseses[i];
                comboBoxInsulationThickness.Items.Add(
                    new ComboBoxItem(i, (string.IsNullOrEmpty(item.GetId()) ? "" : "" + item.GetId() + " | ")
                    + item.GetDescription(), item));
            }
            comboBoxInsulationThickness.SelectedIndex = 0;
        }

        internal void PreviewGCode(string text)
        {
            codePreviewWindow.PreviewText = text;
            codePreviewWindow.Text = (
                "Previewing Entry "
                + textBoxEntryId.Text
                + " ["
                + (string.IsNullOrEmpty(controller.CurrentEntry.JobName) ? "" : (controller.CurrentEntry.JobName + "-"))
                + (string.IsNullOrEmpty(controller.CurrentEntry.DuctId) ? "" : (controller.CurrentEntry.DuctId + ": "))
                + controller.CurrentEntry.PittsburghSize
                + "×"
                + controller.CurrentEntry.SixMmSize
                + " "
                + controller.CurrentEntry.InsulationType.GetId()
                + controller.CurrentEntry.InsulationThickness.GetId()
                + "×"
                + controller.CurrentEntry.Quantity + " off"
                + "]").Trim();

            codePreviewWindow.ShowDialog();
        }

        private void InitializeInsulationTypeComboBox()
        {
            var insulationTypes = (InsulationType[])Enum.GetValues(typeof(InsulationType));
            comboBoxInsulationType.Items.Clear();
            for (int i = 0; i < insulationTypes.Length; i++)
            {
                var item = insulationTypes[i];
                comboBoxInsulationType.Items.Add(
                    new ComboBoxItem(i, (string.IsNullOrEmpty(item.GetId()) ? "" : "" + item.GetId() + " | ")
                    + item.GetDescription(), item));
            }
            comboBoxInsulationType.SelectedIndex = 0;
        }

        private void SelectInsulationThickness(InsulationThickness value)
        {
            for (int i = 0; i < comboBoxInsulationThickness.Items.Count; i++)
            {
                var item = comboBoxInsulationThickness.Items[i];
                if (((InsulationThickness)(((ComboBoxItem)item).Value)).GetId().Equals(value.GetId()))
                {
                    comboBoxInsulationThickness.SelectedItem = item;
                    break;
                }
            }
        }

        private void SelectInsulationType(InsulationType value)
        {
            for (int i = 0; i < comboBoxInsulationType.Items.Count; i++)
            {
                var item = comboBoxInsulationType.Items[i];
                if (((InsulationType)(((ComboBoxItem)item).Value)).GetId().Equals(value.GetId()))
                {
                    comboBoxInsulationType.SelectedItem = item;
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
            this.mode = mode;
            switch (this.mode)
            {
                case DataEntryViewMode.New:
                    textBoxEntryId.ReadOnly = true;
                    textBoxJobName.ReadOnly = false;
                    textBoxDuctId.ReadOnly = false;
                    comboBoxInsulationType.Enabled = !false;
                    comboBoxInsulationThickness.Enabled = !false;
                    EnableNumericUpDowns();
                    buttonModify.Enabled = false;
                    buttonDuplicate.Enabled = false;
                    if (Settings.Instance.IsSingleEntry) {
                        buttonSave.Text = "Apply";
                        buttonModify.Text = "New Entry";
                    } 
                    else
                    {
                        buttonSave.Text = "➕" + Environment.NewLine + "Add";
                        buttonModify.Text = "Modify";
                    }
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonClear.Enabled = true;
                    buttonRemove.Enabled = false;
                    groupBox1.Text = "New Entry";
                    textBox4.Text = "";
                    buttonPreview.Enabled = false;
                    button2.Enabled = false;
                    break;

                case DataEntryViewMode.View:
                    textBoxEntryId.ReadOnly = true;
                    textBoxJobName.ReadOnly = true;
                    textBoxDuctId.ReadOnly = true;
                    comboBoxInsulationType.Enabled = !true;
                    comboBoxInsulationThickness.Enabled = !true;
                    DisableNumericUpDowns();
                    buttonModify.Enabled = true;
                    buttonDuplicate.Enabled = true;
                    if (Settings.Instance.IsSingleEntry)
                    {
                        buttonSave.Text = "Apply";
                        buttonModify.Text = "New Entry";
                    }
                    else
                    {
                        buttonSave.Text = "➕" + Environment.NewLine + "Add";
                        buttonModify.Text = "Modify";
                    }
                    //buttonSave.Text = "✔" + Environment.NewLine + "Update";
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonClear.Enabled = false;
                    buttonRemove.Enabled = true;
                    groupBox1.Text = "Displaying Entry [" + textBoxEntryId.Text + "]";
                    buttonPreview.Enabled = true;
                    button2.Enabled = true;
                    break;

                case DataEntryViewMode.Edit:
                    textBoxEntryId.ReadOnly = true;
                    textBoxJobName.ReadOnly = false;
                    textBoxDuctId.ReadOnly = false;
                    comboBoxInsulationType.Enabled = !false;
                    comboBoxInsulationThickness.Enabled = !false;
                    EnableNumericUpDowns();
                    buttonModify.Enabled = false;
                    buttonDuplicate.Enabled = false;
                    if (Settings.Instance.IsSingleEntry)
                    {
                        buttonSave.Text = "Apply";
                        buttonModify.Text = "New Entry";
                    }
                    else
                    {
                        //buttonSave.Text = "➕" + Environment.NewLine + "Add";
                        buttonSave.Text = "✔" + Environment.NewLine + "Update";
                        buttonModify.Text = "Modify";
                    }
                    buttonSave.Enabled = true;
                    buttonCancel.Enabled = true;
                    buttonClear.Enabled = true;
                    buttonRemove.Enabled = true;
                    groupBox1.Text = "Modifying Entry [" + textBoxEntryId.Text + "]";
                    textBox4.Text = "";
                    buttonPreview.Enabled = false;
                    button2.Enabled = false;
                    break;

                default:
                    textBoxEntryId.ReadOnly = true;
                    textBoxJobName.ReadOnly = true;
                    textBoxDuctId.ReadOnly = true;
                    comboBoxInsulationType.Enabled = !true;
                    comboBoxInsulationThickness.Enabled = !true;
                    DisableNumericUpDowns();
                    buttonModify.Enabled = true;
                    buttonDuplicate.Enabled = true;
                    buttonSave.Text = "Add/Update";
                    buttonSave.Enabled = false;
                    buttonCancel.Enabled = false;
                    buttonClear.Enabled = false;
                    buttonRemove.Enabled = false;
                    groupBox1.Text = "ERROR: Undefined View Mode";
                    buttonPreview.Enabled = false;
                    button2.Enabled = false;
                    break;
            }

            UpdateCaptionText();
        }

        private void UpdateCaptionText()
        {
            Text = string.Format(captionTextFormatter, 
                ("Entry [" + textBoxEntryId.Text + "]"), 
                isDataChanged ? "*" : "");
        }

        public void SelectEntry(DataEntry currentEntry)
        {
            foreach (ListViewItem row in dataEntriesListView.Items)
            {
                row.Selected = row.Text.Equals(currentEntry.Id);
                return;
            }
        }

        public string GetSelectedEntryId()
        {
            if (dataEntriesListView.SelectedItems.Count > 0)
                return dataEntriesListView.SelectedItems[0].Text;
            return string.Empty;
        }

        public void AddEntryToListView(DataEntry currentEntry)
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

        public void UpdateEntryInListVew(DataEntry currentEntry)
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

        public void RemoveEntryFromListView(DataEntry entry)
        {
            var numberOfEntries = dataEntriesListView.Items.Count;
            var indexOfRemovedEntry = 0;
            for (var i = 0; i < numberOfEntries; i++)
            {
                if (dataEntriesListView.Items[i].Text.Equals(entry.Id))
                {
                    dataEntriesListView.Items.RemoveAt(i);
                    dataEntriesListView.Focus();
                    indexOfRemovedEntry = i;
                    numberOfEntries--;
                    break;
                }
            }

            if (numberOfEntries == 0)
                controller.CreateNewEntry();
            else if (numberOfEntries == 1)
                controller.SelectEntry(dataEntriesListView.Items[0].Text);
            else if (indexOfRemovedEntry == numberOfEntries) // now, for sure, numberOfEntries > 1
                controller.SelectEntry(dataEntriesListView.Items[numberOfEntries - 1].Text); // select the last entry
            else
                controller.SelectEntry(dataEntriesListView.Items[indexOfRemovedEntry].Text); // select the entry next to the removed entry
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

        private void DisableNumericUpDowns()
        {
            numericUpDownPittsburghSize.ReadOnly = true;
            numericUpDownSixMmSize.ReadOnly = true;
            numericUpDownQuantity.ReadOnly = true;
            numericUpDownPittsburghSize.Increment = 0;
            numericUpDownSixMmSize.Increment = 0;
            numericUpDownQuantity.Increment = 0;
        }

        private void EnableNumericUpDowns()
        {
            numericUpDownPittsburghSize.ReadOnly = false;
            numericUpDownSixMmSize.ReadOnly = false;
            numericUpDownQuantity.ReadOnly = false;
            numericUpDownPittsburghSize.Increment = 50;
            numericUpDownSixMmSize.Increment = 50;
            numericUpDownQuantity.Increment = 1;
        }

        #endregion View implementation

        private void button2_Click(object sender, EventArgs e)
        {
            ExportSelectedEntry();
        }

        private void DataEntryView_Load(object sender, EventArgs e)
        {
            UpdateUIFromSettings();
        }

        private void UpdateUIFromSettings()
        {
            if (Settings.Instance.IsUseFeMaleInstead)
            {
                label4.Text = "Female Size";
                label5.Text = "Male Size";
            }
            else
            {
                label4.Text = "Pittsburgh Size";
                label5.Text = "Six-mm Size";
            }

            if (Settings.Instance.IsSingleEntry) {
                buttonNew.Visible = false;
                buttonRemove.Visible = false;
                buttonCancel.Visible = false;
                buttonDuplicate.Visible = false;
                //buttonModify.Visible = false;
                dataEntriesListView.Visible = false;
                buttonModify.Text = "New";
            } 
            else
            {
                buttonNew.Visible = true;
                buttonRemove.Visible = true;
                buttonCancel.Visible = true;
                buttonDuplicate.Visible = true;
                //buttonModify.Visible = true;
                dataEntriesListView.Visible = true;
                buttonModify.Text = "Modify";
            }
        }

        private void DataEntryView_VisibleChanged(object sender, EventArgs e)
        {
            UpdateUIFromSettings();
        }
    }
}
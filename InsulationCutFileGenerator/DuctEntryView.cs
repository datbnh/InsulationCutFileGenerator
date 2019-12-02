using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsulationCutFileGenerator
{
    public partial class DuctEntryView : UserControl
    {
        internal DuctEntryView() : this(-1, 0, 0, 0, 0, "") {}
        public int Index { private set; get; }

        public DuctEntryView(int index, DuctEntryData data, DuctEntryControl control)
        {
            Index = index;
            DataEntry = data;
            Control = control;
        }


        private DuctEntryView(int index, int insulationIdx, int sizeA, int sizeB, int qty, string itemNumber)
        {
            InitializeComponent();
            InitInsulationComboBox();
            Index = index;
            labelIndex.Text = index.ToString();
            comboBox1.SelectedIndex = insulationIdx;
            numericUpDownSizeA.Value = Math.Max(sizeA, numericUpDownSizeA.Minimum);
            numericUpDownSizeB.Value = Math.Max(sizeB, numericUpDownSizeB.Minimum);
            numericUpDownQty.Value = Math.Max(qty, numericUpDownQty.Minimum);
            textBox1.Text = itemNumber;

            DataEntry.SetInsulation((Insulation)comboBox1.SelectedItem);
            DataEntry.SetSize((int)numericUpDownSizeA.Value, (int)numericUpDownSizeB.Value);
            DataEntry.SetQuatity((int)numericUpDownQty.Value);
            DataEntry.SetItemNumber(textBox1.Text);

            ValidateEntry();
        }

        public DuctEntryView Clone(int idx)
        {
            return new DuctEntryView(idx, comboBox1.SelectedIndex, DataEntry.FemaleSize, DataEntry.MaleSize, 0, "");
        }

        private void InitInsulationComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(BuiltInInsulationTypes.PopulateAllBuiltInTypes().ToArray());
        }

        private bool ValidateEntry()
        {
            if (!Control.Validate())
            {
                BackColor = Color.MistyRose;
                labelDescription.Text = DataEntry.Description;
                return false;
            }

            if (DataEntry.Quantity < 1)
                BackColor = Color.LightGray;
            else
                BackColor = Color.White;

            labelDescription.Text = DataEntry.Description;
            return true;
        }

        internal DuctEntryData DataEntry { get; private set; }
        internal DuctEntryControl Control { get; private set; }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataEntry == null)
                return;
            DataEntry.SetInsulation((Insulation)comboBox1.SelectedItem);
            ValidateEntry();
        }

        private void NumericUpDownSizeA_ValueChanged(object sender, EventArgs e)
        {
            if (DataEntry == null)
                return;
            DataEntry.SetSize((int)numericUpDownSizeA.Value, (int)numericUpDownSizeB.Value);
            ValidateEntry();
        }

        private void NumericUpDownSizeB_ValueChanged(object sender, EventArgs e)
        {
            if (DataEntry == null)
                return;
            DataEntry.SetSize((int)numericUpDownSizeA.Value, (int)numericUpDownSizeB.Value);
            ValidateEntry();
        }

        private void NumericUpDownQty_ValueChanged(object sender, EventArgs e)
        {
            if (DataEntry == null)
                return;
            DataEntry.SetQuatity((int)numericUpDownQty.Value);
            ValidateEntry();
        }

        public bool IsHeaderVisible
        {
            set
            {
                if (!value)
                {
                    tableLayoutPanel1.RowStyles[0].SizeType = SizeType.Absolute;
                    tableLayoutPanel1.RowStyles[0].Height = 0;
                }
                else
                {
                    tableLayoutPanel1.RowStyles[0].SizeType = SizeType.AutoSize;
                }
            }

            get
            {
                return (tableLayoutPanel1.RowStyles[0].Height != 0);
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            DataEntry.SetItemNumber(textBox1.Text);
        }
    }
}

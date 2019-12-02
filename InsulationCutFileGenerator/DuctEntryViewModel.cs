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
    public partial class EntryViewModel : UserControl
    {
        internal EntryViewModel() : this(-1, 0, 0, 0, 0, "") {}
        public int Index { private set; get; }

        public EntryViewModel(int index, int insulationIdx, int sizeA, int sizeB, int qty, string itemNumber)
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

            DataEntry = new DuctEntryData((Insulation)comboBox1.SelectedItem,
                (int)numericUpDownSizeA.Value, (int)numericUpDownSizeB.Value, (int)numericUpDownQty.Value, textBox1.Text);
            ValidateEntry();
        }

        public EntryViewModel Clone(int idx)
        {
            return new EntryViewModel(idx, comboBox1.SelectedIndex, DataEntry.ShortEdge, DataEntry.LongEdge, 0, "");
        }

        private void InitInsulationComboBox()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(BuiltInInsulationTypes.PopulateAllBuiltInTypes().ToArray());
        }

        private bool ValidateEntry()
        {
            if (!DataEntry.Validate())
            {
                BackColor = Color.MistyRose;
                labelDescription.Text = DataEntry.GetFullDescription();
                return false;
            }

            if (DataEntry.Quantity < 1)
                BackColor = Color.LightGray;
            else
                BackColor = Color.White;

            labelDescription.Text = DataEntry.GetFullDescription();
            return true;
        }

        internal DuctEntryData DataEntry { get; private set; }

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

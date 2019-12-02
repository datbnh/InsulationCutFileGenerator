using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGenerator
{
    public class DuctEntryData
    {
        public string Description { get; internal set; }
        private bool IsForceLongSizeAsMaleSize;

        public DuctEntryData(Insulation insulation, int femaleSize, int maleSize, int quantity, string itemNumber, bool isForceLongEdgeAsPittsburgMale = true)
        {
            IsForceLongSizeAsMaleSize = isForceLongEdgeAsPittsburgMale;
            Insulation = insulation;
            Quantity = quantity;
            ItemNumber = itemNumber;
            SetSize(femaleSize, maleSize);
        }

        internal void SetSize(int femaleSize, int maleSize)
        {
            if (IsForceLongSizeAsMaleSize)
            {
                FemaleSize = Math.Min(femaleSize, maleSize);
                MaleSize = Math.Max(femaleSize, maleSize);
            } else
            {
                FemaleSize = femaleSize;
                MaleSize = maleSize;
            }
        }

        internal void SetInsulation(Insulation value)
        {
            Insulation = value;
        }

        internal void SetQuatity(int value)
        {
            Quantity = value;
        }

        internal void SetItemNumber(string itemNumber)
        {
            ItemNumber = itemNumber;
        }

        public Insulation Insulation { get; private set; }
        public int FemaleSize { get; private set; }
        public int MaleSize { get; private set; }
        public int Quantity { get; private set; }
        public string ItemNumber { get; private set; }

        public string GetFullDescription()
        {
            return Description;
        }
    }
}

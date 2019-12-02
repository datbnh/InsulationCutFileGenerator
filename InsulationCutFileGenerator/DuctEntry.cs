using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGenerator
{
    class DuctEntryData
    {
        private string fullDescription;

        public DuctEntryData(Insulation insulation, int sizeA, int sizeB, int quantity, string itemNumber)
        {
            ShortEdge = Math.Min(sizeA, sizeB);
            LongEdge = Math.Max(sizeA, sizeB);
            Insulation = insulation;
            Quantity = quantity;
            ItemNumber = itemNumber;
        }

        public int GetInsulationShortEdge()
        {
            return ShortEdge - 2 * Insulation.Thickness + Insulation.ShortEdgeTotalOverlapping;
        }

        public int GetInsulationLongEdge()
        {
            return LongEdge - Insulation.LongEdgeTotalSeparation;
        }

        public int GetInsulationTotalLaggingSize()
        {
            return 2 * (ShortEdge + LongEdge) + Insulation.ShortEdgeTotalOverlapping;
        }

        internal void SetSize(int sizeA, int sizeB)
        {
            ShortEdge = Math.Min(sizeA, sizeB);
            LongEdge = Math.Max(sizeA, sizeB);
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
        public int ShortEdge { get; private set; }
        public int LongEdge { get; private set; }
        public int Quantity { get; private set; }
        public string ItemNumber { get; private set; }

        public int GetShortInsulationSize()
        {
            if (Insulation.InsulatingMode == InsulatingMode.Lagging)
                throw new Exception("Short insulation size is not available for lagging mode.");
            return ShortEdge - 2 * Insulation.Thickness + Insulation.ShortEdgeTotalOverlapping;
        }

        public int GetLongInsulationSize()
        {
            if (Insulation.InsulatingMode == InsulatingMode.Lagging)
                throw new Exception("Long insulation size is not available for lagging mode.");
            return LongEdge - Insulation.LongEdgeTotalSeparation;
        }

        public int GetLaggingSize()
        {
            if (!(Insulation.InsulatingMode == InsulatingMode.Lagging))
                throw new Exception("Lagging size is not available for non-lagging mode.");
            return ShortEdge + LongEdge + Insulation.ShortEdgeTotalOverlapping;
        }

        public string GetFullDescription()
        {
            return fullDescription;
        }

        private int GetTotalInsulationLength()
        {
            if (Insulation.InsulatingMode == InsulatingMode.Lagging)
                return GetLaggingSize() * Quantity;
            else
                return (GetShortInsulationSize() + GetLongInsulationSize()) * 2 * Quantity;
        }

        public bool Validate()
        {
            if (Insulation == null)
            {
                fullDescription = "Invalid insulation type.";
                return false;
            }
            if (Insulation.InsulatingMode == InsulatingMode.Lagging)
            {
                if (GetLaggingSize() > 0)
                    fullDescription = string.Format("Total size: {3} mm × {0} mm. Qty: {1}. Total insulation required: {2:0.000} m.",
                        GetLaggingSize(), Quantity, GetTotalInsulationLength() / 1000f, Insulation.FULL_SIZE);
                else
                    fullDescription = "Invalid configuration.";

                return GetLaggingSize() > 0;
            }
            else
            {
                if (GetShortInsulationSize() <= 0)
                    fullDescription = "Short size is too small.";
                else if (GetLongInsulationSize() <= 0)
                    fullDescription = "Long size is too small.";
                else
                    fullDescription = string.Format("Short size: {4} mm × {0} mm, Long side: 1400 mm × {1} mm. Qty: {2}. Total insulation required: {3:0.000} m.",
                        GetShortInsulationSize(), GetLongInsulationSize(), Quantity, GetTotalInsulationLength() / 1000f, Insulation.FULL_SIZE);

                return (GetShortInsulationSize() > 0) && (GetLongInsulationSize() > 0);
            }
        }

        private string cutFilePath;
        private int cutFileLineBlockCounter;

        private void InitCutFile(string filePath)
        {
            cutFilePath = filePath;
            cutFileLineBlockCounter = 0;
            using (StreamWriter writer = new StreamWriter(cutFilePath, false))
            {
                writer.WriteLine(GCodeGenerator.Init());
            }
        }

        private void FinaliseCutFile()
        {
            if (string.IsNullOrEmpty(cutFilePath))
                throw new Exception("Cut file must be initialised first.");
            using (StreamWriter writer = new StreamWriter(cutFilePath, true))
            {
                writer.WriteLine(GCodeGenerator.Stop());
            }
        }

        private void AppendRipCutAtXToCutFile(long xMm, string text, bool isCutDirectionFromFullSizeToZero=false)
        {
            if (string.IsNullOrEmpty(cutFilePath))
                throw new Exception("Cut file must be initialised first.");
            using (StreamWriter writer = new StreamWriter(cutFilePath, true))
            {
                writer.WriteLine(GCodeGenerator.LineBlock(++cutFileLineBlockCounter));
                writer.WriteLine(GCodeGenerator.AddText(text));
                writer.WriteLine(GCodeGenerator.MoveTo(xMm, isCutDirectionFromFullSizeToZero ? 1400 : 0));
                writer.WriteLine(GCodeGenerator.KnifeDown());
                writer.WriteLine(GCodeGenerator.MoveTo(xMm, isCutDirectionFromFullSizeToZero ? 0 : 1400));
                writer.WriteLine(GCodeGenerator.KnifeUp());
            }
        }

        public void GenerateGCode(string filePath)
        {
            var X = 0;
            InitCutFile(filePath);

            // cut first line
            AppendRipCutAtXToCutFile(0, "RIP CUT BEFORE");

            for (var qtyCount = 1; qtyCount <= Quantity; qtyCount++)
            {
                X += GetShortInsulationSize();
                AppendRipCutAtXToCutFile(X, string.Format("1/{0}{1:0}",
                    string.IsNullOrEmpty(ItemNumber) ? "" : ItemNumber + "/", qtyCount), true);
                X += GetShortInsulationSize();
                AppendRipCutAtXToCutFile(X, string.Format("2/{0}{1:0}",
                    string.IsNullOrEmpty(ItemNumber) ? "" : ItemNumber + "/", qtyCount));
                X += GetLongInsulationSize();
                AppendRipCutAtXToCutFile(X, string.Format("3/{0}{1:0}",
                    string.IsNullOrEmpty(ItemNumber) ? "" : ItemNumber + "/", qtyCount), true);
                X += GetLongInsulationSize();
                AppendRipCutAtXToCutFile(X, string.Format("4/{0}{1:0}",
                    string.IsNullOrEmpty(ItemNumber) ? "" : ItemNumber + "/", qtyCount));
            }

            FinaliseCutFile();
        }
    }
}

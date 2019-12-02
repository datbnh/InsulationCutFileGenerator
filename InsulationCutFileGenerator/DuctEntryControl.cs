using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGenerator
{
    public class DuctEntryControl
    {
        private int cutFileLineBlockCounter;

        private readonly DuctEntryData DuctEntry;
        private string OutputFilePath;

        public DuctEntryControl(DuctEntryData ductEntry)
        {
            DuctEntry = ductEntry;
        }

        private void InitCutFile(string outputFilePath)
        {
            OutputFilePath = outputFilePath;
            cutFileLineBlockCounter = 0;
            using (StreamWriter writer = new StreamWriter(OutputFilePath, false))
            {
                writer.WriteLine(GCode.Init());
            }
        }

        private void FinaliseCutFile()
        {
            if (string.IsNullOrEmpty(OutputFilePath))
                throw new Exception("Cut file must be initialised first.");
            using (StreamWriter writer = new StreamWriter(OutputFilePath, true))
            {
                writer.WriteLine(GCode.Stop());
            }
        }

        private void AppendRipCutAtXToCutFile(long xMm, string text, bool isCutDirectionFromFullSizeToZero = false)
        {
            if (string.IsNullOrEmpty(OutputFilePath))
                throw new Exception("Cut file must be initialised first.");
            using (StreamWriter writer = new StreamWriter(OutputFilePath, true))
            {
                writer.WriteLine(GCode.LineBlock(++cutFileLineBlockCounter));
                writer.WriteLine(GCode.AddText(text));
                writer.WriteLine(GCode.MoveTo(xMm, isCutDirectionFromFullSizeToZero ? Insulation.FULL_SIZE : 0));
                writer.WriteLine(GCode.KnifeDown());
                writer.WriteLine(GCode.MoveTo(xMm, isCutDirectionFromFullSizeToZero ? 0 : Insulation.FULL_SIZE));
                writer.WriteLine(GCode.KnifeUp());
            }
        }

        public void ExportCutFile(string outputFilePath)
        {
            if (DuctEntry.Insulation.InsulatingMode != InsulatingMode.Lagging)
                ExportCutFileForInternalInsulation(outputFilePath);
            else
                ExportCutFileForLaggingInsulation(outputFilePath);
            OutputFilePath = null;
        }

        private void ExportCutFileForInternalInsulation(string outputFilePath)
        {
            var X = 0;
            InitCutFile(outputFilePath);

            // cut first line
            AppendRipCutAtXToCutFile(0, "RIP CUT BEFORE");

            for (var qtyCount = 1; qtyCount <= DuctEntry.Quantity; qtyCount++)
            {
                X += GetInsulationSizeForFemaleSide();
                AppendRipCutAtXToCutFile(X, string.Format("1/{0}{1:0}",
                    string.IsNullOrEmpty(DuctEntry.ItemNumber) ? "" : DuctEntry.ItemNumber + "/", qtyCount), true);
                X += GetInsulationSizeForFemaleSide();
                AppendRipCutAtXToCutFile(X, string.Format("2/{0}{1:0}",
                    string.IsNullOrEmpty(DuctEntry.ItemNumber) ? "" : DuctEntry.ItemNumber + "/", qtyCount));
                X += GetInsulationSizeForMaleSide();
                AppendRipCutAtXToCutFile(X, string.Format("3/{0}{1:0}",
                    string.IsNullOrEmpty(DuctEntry.ItemNumber) ? "" : DuctEntry.ItemNumber + "/", qtyCount), true);
                X += GetInsulationSizeForMaleSide();
                AppendRipCutAtXToCutFile(X, string.Format("4/{0}{1:0}",
                    string.IsNullOrEmpty(DuctEntry.ItemNumber) ? "" : DuctEntry.ItemNumber + "/", qtyCount));
            }

            FinaliseCutFile();
        }

        

        private void ExportCutFileForLaggingInsulation(string outputFilePath)
        {
            throw new NotImplementedException();
        }

        public int GetInsulationShortEdge()
        {
            return DuctEntry.FemaleSize - 2 * DuctEntry.Insulation.Thickness + DuctEntry.Insulation.ShortEdgeTotalOverlapping;
        }

        public int GetInsulationLongEdge()
        {
            return DuctEntry.MaleSize - DuctEntry.Insulation.LongEdgeTotalSeparation;
        }

        public int GetInsulationTotalLaggingSize()
        {
            return 2 * (DuctEntry.FemaleSize + DuctEntry.MaleSize) + DuctEntry.Insulation.ShortEdgeTotalOverlapping;
        }

        public int GetInsulationSizeForFemaleSide()
        {
            if (DuctEntry.Insulation.InsulatingMode == InsulatingMode.Lagging)
                throw new Exception(nameof(GetInsulationSizeForFemaleSide) + " is not available for lagging mode.");
            return DuctEntry.FemaleSize - 2 * DuctEntry.Insulation.Thickness + DuctEntry.Insulation.ShortEdgeTotalOverlapping;
        }

        public int GetInsulationSizeForMaleSide()
        {
            if (DuctEntry.Insulation.InsulatingMode == InsulatingMode.Lagging)
                throw new Exception(nameof(GetInsulationSizeForMaleSide) + " is not available for lagging mode.");
            return DuctEntry.MaleSize - DuctEntry.Insulation.LongEdgeTotalSeparation;
        }

        public int GetLaggingSize()
        {
            if (!(DuctEntry.Insulation.InsulatingMode == InsulatingMode.Lagging))
                throw new Exception(nameof(GetLaggingSize) + " is not available for non-lagging mode.");
            return DuctEntry.FemaleSize + DuctEntry.MaleSize + DuctEntry.Insulation.ShortEdgeTotalOverlapping;
        }

        private int GetTotalInsulationLength()
        {
            if (DuctEntry.Insulation.InsulatingMode == InsulatingMode.Lagging)
                return GetLaggingSize() * DuctEntry.Quantity;
            else
                return (GetInsulationSizeForFemaleSide() + GetInsulationSizeForMaleSide()) * 2 * DuctEntry.Quantity;
        }

        public bool Validate()
        {
            if (DuctEntry.Insulation == null)
            {
                DuctEntry.Description = "Invalid insulation type.";
                return false;
            }
            if (DuctEntry.Insulation.InsulatingMode == InsulatingMode.Lagging)
            {
                if (GetLaggingSize() > 0)
                    DuctEntry.Description = string.Format("Total size: {3} mm × {0} mm. Qty: {1}. Total insulation required: {2:0.000} m.",
                        GetLaggingSize(), DuctEntry.Quantity, GetTotalInsulationLength() / 1000f, Insulation.FULL_SIZE);
                else
                    DuctEntry.Description = "Invalid configuration.";

                return GetLaggingSize() > 0;
            }
            else
            {
                if (GetInsulationSizeForFemaleSide() <= 0)
                    DuctEntry.Description = "Short size is too small.";
                else if (GetInsulationSizeForMaleSide() <= 0)
                    DuctEntry.Description = "Long size is too small.";
                else
                    DuctEntry.Description = string.Format("Short size: {4} mm × {0} mm, Long side: 1400 mm × {1} mm. Qty: {2}. Total insulation required: {3:0.000} m.",
                        GetInsulationSizeForFemaleSide(), GetInsulationSizeForMaleSide(), DuctEntry.Quantity, GetTotalInsulationLength() / 1000f, Insulation.FULL_SIZE);

                return (GetInsulationSizeForFemaleSide() > 0) && (GetInsulationSizeForMaleSide() > 0);
            }
        }
    }
}

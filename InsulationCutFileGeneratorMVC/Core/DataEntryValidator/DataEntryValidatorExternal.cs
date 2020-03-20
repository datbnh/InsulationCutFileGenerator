using InsulationCutFileGeneratorMVC.MVC_Model;
using System;

namespace InsulationCutFileGeneratorMVC.Core.DataEntryValidator
{
    internal class DataEntryValidatorExternal : IDataEntryValidator
    {
        public InsulationType InsulationType => InsulationType.External;

        public DataEntryValidationResult Validate(DataEntry entry)
        {
            var laggingLength = GetLaggingLength(entry);
            if (entry.InsulationType == InsulationType.Undefined)
                return new DataEntryValidationResult(false,
                    "Insulation type is undefined.");
            if (entry.InsulationThickness == InsulationThickness.Undefined)
                return new DataEntryValidationResult(false,
                    "Undefined insulation thickness.");
            if (laggingLength <= 0)
                return new DataEntryValidationResult(false,
                    "Specified duct size is too small.");
            return new DataEntryValidationResult(
                true,
                string.Format("Lagging sheet (ea.):{4} {3} mm × {0} mm.{4}" +
                    "Quantity:{4} {1}.{4}Total insulation required:{4} {2:0.0} m.",
                    laggingLength,
                    entry.Quantity,
                    GetTotalInsulationLength(entry) / 1000f,
                    DataEntry.DUCT_FULL_LENGTH,
                    Environment.NewLine));
        }

        public static int GetTotalInsulationLength(DataEntry entry)
        {
            return GetLaggingLength(entry) * entry.Quantity;
        }

        public static int GetLaggingLength(DataEntry entry)
        {
            return entry.PittsburghSize + entry.SixMmSize + entry.InsulationThickness.GetExternalTotalAdjustment();
        }
    }
}
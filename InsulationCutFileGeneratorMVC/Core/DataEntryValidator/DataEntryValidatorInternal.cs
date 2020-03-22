using InsulationCutFileGeneratorMVC.MVC_Model;
using System;

namespace InsulationCutFileGeneratorMVC.Core.DataEntryValidator
{
    internal class DataEntryValidatorInternal : IDataEntryValidator
    {
        public InsulationType InsulationType => InsulationType.Internal;

        public DataEntryValidationResult Validate(DataEntry entry)
        {
            var insulationPittsburghSize = GetInsulationPittsburgSize(entry);
            var insulationSixMmSize = GetInsulationSixMmSize(entry);
            if (entry.InsulationType == InsulationType.Undefined)
                return new DataEntryValidationResult(false,
                    "Undefined insulation type.");
            if (entry.InsulationThickness == InsulationThickness.Undefined)
                return new DataEntryValidationResult(false,
                    "Undefined insulation thickness.");
            if (insulationPittsburghSize <= 0 || insulationSixMmSize <= 0)
                return new DataEntryValidationResult(false,
                    "Specified duct size is too small with selected insulation thickness.");
            return new DataEntryValidationResult(
                true,
                string.Format("Pittsburgh sheet (ea.):{5} {4} mm × {0} mm.{5}" +
                    "Six-mm sheet (ea.):{5} {4} mm × {1} mm.{5}" +
                    "Quantity:{5} {2}.{5}Total insulation required:{5} {3:0.0} m.",
                    insulationPittsburghSize,
                    insulationSixMmSize,
                    entry.Quantity,
                    GetTotalInsulationLength(entry) / 1000f,
                    DataEntry.DUCT_FULL_LENGTH,
                    Environment.NewLine));
        }

        public static int GetTotalInsulationLength(DataEntry entry)
        {
            return (GetInsulationPittsburgSize(entry) + GetInsulationSixMmSize(entry))
                * 2 * entry.Quantity;
        }

        public static int GetInsulationPittsburgSize(DataEntry entry)
        {
            return entry.PittsburghSize
                - 2 * entry.InsulationThickness.GetActualThickness()
                + entry.InsulationThickness.GetInternalPittsburghAdjustment();
        }

        public static int GetInsulationSixMmSize(DataEntry entry)
        {
            return entry.SixMmSize
                - 2 * entry.InsulationThickness.GetActualThickness()
                + entry.InsulationThickness.GetInternalSixMmAdjustment();
        }
    }
}
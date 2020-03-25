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
                    "Undefined insulation type.");
            if (entry.InsulationThickness == InsulationThickness.Undefined)
                return new DataEntryValidationResult(false,
                    "Undefined insulation thickness.");
            var sizeWarningFlag = false;
            if (entry.PittsburghSize > entry.SixMmSize)
            {
                switch (Settings.Instance.PittsburgSixMmValidationMode)
                {
                    case PittsburghSixMmValidationMode.Ignore:
                        break;
                    case PittsburghSixMmValidationMode.Warning:
                        sizeWarningFlag = true;
                        break;
                    case PittsburghSixMmValidationMode.Enforcing:
                        return new DataEntryValidationResult(false,
                            "Pitssburgh size cannot be larger than Six-mm size.");
                    default:
                        break;
                }
            }
            if (laggingLength <= 0)
                return new DataEntryValidationResult(false,
                    "Specified duct size is too small.");
            return new DataEntryValidationResult(
                true,
                string.Format("Lagging sheet (ea.):{4} {3} mm × {0} mm.{4}" +
                    "Quantity:{4} {1}.{4}Total insulation required:{4} {2:0.0} m.{5}",
                    laggingLength,
                    entry.Quantity,
                    GetTotalInsulationLength(entry) / 1000f,
                    DataEntry.DUCT_FULL_LENGTH,
                    Environment.NewLine,
                    sizeWarningFlag ? Environment.NewLine + Environment.NewLine + "***WARNING: Pittsburgh size should not larger than Six-mm size.***" : ""
                    ));
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
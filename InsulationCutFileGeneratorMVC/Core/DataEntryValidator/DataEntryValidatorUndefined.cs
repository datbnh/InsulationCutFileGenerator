using InsulationCutFileGeneratorMVC.MVC_Model;

namespace InsulationCutFileGeneratorMVC.Core
{
    public class DataEntryValidatorUndefined : IDataEntryValidator
    {
        public InsulationType InsulationType => InsulationType.Undefined;

        public DataEntryValidationResult Validate(DataEntry entry)
        {
            return new DataEntryValidationResult(false, "Undefined insulation type.");
        }
    }
}
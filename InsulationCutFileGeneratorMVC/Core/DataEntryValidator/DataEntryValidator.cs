using InsulationCutFileGeneratorMVC.MVC_Model;

namespace InsulationCutFileGeneratorMVC.Core.DataEntryValidator
{
    public static class DataEntryValidator
    {
        public static DataEntryValidationResult Validate(this DataEntry entry)
        {
            return DataEntryValidatorFactory.Factory.GetInstance(entry.InsulationType).Validate(entry);
        }
    }
}
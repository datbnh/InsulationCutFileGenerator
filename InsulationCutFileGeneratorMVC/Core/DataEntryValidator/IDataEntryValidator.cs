using InsulationCutFileGeneratorMVC.MVC_Model;

namespace InsulationCutFileGeneratorMVC.Core
{
    public interface IDataEntryValidator
    {
        InsulationType InsulationType { get; }
        DataEntryValidationResult Validate(DataEntry entry);
    }
}

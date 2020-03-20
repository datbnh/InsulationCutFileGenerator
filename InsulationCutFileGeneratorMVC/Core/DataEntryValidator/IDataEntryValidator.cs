using InsulationCutFileGeneratorMVC.MVC_Model;

namespace InsulationCutFileGeneratorMVC.Core.DataEntryValidator
{
    internal interface IDataEntryValidator
    {
        InsulationType InsulationType { get; }

        DataEntryValidationResult Validate(DataEntry entry);
    }
}
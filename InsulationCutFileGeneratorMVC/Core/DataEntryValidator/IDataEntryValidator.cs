namespace InsulationCutFileGeneratorMVC.Core
{
    public interface IDataEntryValidator
    {
        InsulationType InsulationType { get; }
        DataEntryValidationResult Validate(DataEntry entry);
    }
}

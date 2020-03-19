namespace InsulationCutFileGeneratorMVC.Core
{
    public class DataEntryValidationResult
    {
        public string Description;
        public bool IsValid;

        public DataEntryValidationResult(bool isValid, string description)
        {
            IsValid = isValid;
            Description = description;
        }
    }
}
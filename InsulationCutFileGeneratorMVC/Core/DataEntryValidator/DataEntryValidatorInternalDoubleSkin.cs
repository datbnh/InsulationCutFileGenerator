using System;

namespace InsulationCutFileGeneratorMVC.Core
{
    public class DataEntryValidatorInternalDoubleSkin : IDataEntryValidator
    {
        public InsulationType InsulationType => InsulationType.InternalDoubleSkin;

        public DataEntryValidationResult Validate(DataEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}

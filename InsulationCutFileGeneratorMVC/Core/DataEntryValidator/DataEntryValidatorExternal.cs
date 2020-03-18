using System;

namespace InsulationCutFileGeneratorMVC.Core
{
    public class DataEntryValidatorExternal : IDataEntryValidator
    {
        public InsulationType InsulationType => InsulationType.External;

        public DataEntryValidationResult Validate(DataEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}

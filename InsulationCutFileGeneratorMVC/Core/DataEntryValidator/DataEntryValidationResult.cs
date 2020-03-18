using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

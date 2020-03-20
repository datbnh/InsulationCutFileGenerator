using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

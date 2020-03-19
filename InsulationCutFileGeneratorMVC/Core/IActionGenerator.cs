using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsulationCutFileGeneratorMVC.MVC_Model;

namespace InsulationCutFileGeneratorMVC.Core
{
    public interface IActionGenerator
    {
        Action[] GenerateActionSequence();
        DataEntryValidationResult ValidateEntry(DataEntry entry);
    }
}

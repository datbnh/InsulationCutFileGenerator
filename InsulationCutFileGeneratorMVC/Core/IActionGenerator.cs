using InsulationCutFileGeneratorMVC.MVC_Model;
using System;

namespace InsulationCutFileGeneratorMVC.Core
{
    public interface IActionGenerator
    {
        Action[] GenerateActionSequence();

        DataEntryValidationResult ValidateEntry(DataEntry entry);
    }
}
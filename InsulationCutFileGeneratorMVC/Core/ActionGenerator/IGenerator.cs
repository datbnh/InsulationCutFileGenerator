using InsulationCutFileGeneratorMVC.MVC_Model;
using System.Collections.Generic;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{
    public interface IGenerator
    {
        InsulationType InsulationType { get; }

        List<KeyValuePair<Action, object[]>> GenerateActionSequence(DataEntry entry);
    }
}
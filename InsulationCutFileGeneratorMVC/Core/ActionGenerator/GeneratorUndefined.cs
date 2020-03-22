using InsulationCutFileGeneratorMVC.MVC_Model;
using System.Collections.Generic;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{
    public class GeneratorUndefined : IGenerator
    {
        public InsulationType InsulationType => InsulationType.Undefined;

        public List<KeyValuePair<Action, object[]>> GenerateActionSequence(DataEntry entry)
        {
            return null;
        }
    }
}
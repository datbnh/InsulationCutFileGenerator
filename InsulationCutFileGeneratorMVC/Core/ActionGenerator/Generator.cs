using InsulationCutFileGeneratorMVC.MVC_Model;
using System.Collections.Generic;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{
    public static class Generator
    {
        public static List<KeyValuePair<Action, object[]>>
            GenerateActionSequence(this DataEntry entry)
        {
            return GeneratorFactory.Factory.GetInstance(entry.InsulationType).GenerateActionSequence(entry);
        }
    }
}
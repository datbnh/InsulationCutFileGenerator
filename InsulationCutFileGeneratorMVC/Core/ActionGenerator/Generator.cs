using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

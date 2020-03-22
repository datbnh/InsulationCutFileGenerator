using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

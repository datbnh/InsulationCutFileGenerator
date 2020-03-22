using InsulationCutFileGeneratorMVC.Core.DataEntryValidator;
using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{
    public class GeneratorExternal : IGenerator
    {
        public InsulationType InsulationType => InsulationType.External;

        public List<KeyValuePair<Action, object[]>> GenerateActionSequence(DataEntry entry)
        {

            if (!entry.Validate().IsValid)
                throw new ArgumentException("Invalid data entry.");

            List<KeyValuePair<Action, object[]>> output = new List<KeyValuePair<Action, object[]>>
            {
                new KeyValuePair<Action, object[]>
                (Action.Initialize, null),

                new KeyValuePair<Action, object[]>
                (Action.RipCutBackward, new object[] { 0, "RIP CUT BEFORE" })
            };

            var laggingLength = DataEntryValidatorExternal.GetLaggingLength(entry);
            int currentX = 0;
            for (int quantityCount = 0; quantityCount < entry.Quantity; quantityCount++)
            {
                string text = string.Format("{0}{1:0}",
                    string.IsNullOrEmpty(entry.DuctId) ?
                    "" : entry.DuctId + "/", quantityCount + 1);

                output.Add(new KeyValuePair<Action, object[]>
                    (Action.LCut, new object[] { currentX, currentX + laggingLength, text }));

                currentX += laggingLength;
            }

            output.Add(new KeyValuePair<Action, object[]>(Action.End, null));

            return output;
        }
    }
}

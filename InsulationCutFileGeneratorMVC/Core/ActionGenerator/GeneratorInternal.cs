﻿using InsulationCutFileGeneratorMVC.Core.DataEntryValidator;
using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{

    public class GeneratorInternal
    {
        public InsulationType InsulationType => InsulationType.Internal;

        public List<KeyValuePair<Action, object[]>> GenerateActionSequence(DataEntry entry)
        {

            if (!entry.Validate().IsValid)
                throw new ArgumentException("Invalid data entry.");

            List<KeyValuePair<Action, object[]>> output = new List<KeyValuePair<Action, object[]>>();
            output.Add(new KeyValuePair<Action, object[]>
                (Action.Initialize, null));

            output.Add(new KeyValuePair<Action, object[]>
                (Action.RipCutBackward, new object[] { 0, "RIP CUT BEFORE" }));

            var sixMmStep = DataEntryValidatorInternal.GetInsulationSixMmSize(entry);
            var pittsburghStep = DataEntryValidatorInternal.GetInsulationPittsburgSize(entry);
            var currentX = 0;
            for (int quantityCount = 0; quantityCount < entry.Quantity; quantityCount++)
            {
                for (int i = 0; i < 2; i++)
                {
                    string text = string.Format("{0}{1:0}",
                        string.IsNullOrEmpty(entry.DuctId) ?
                        "" : entry.DuctId + "/", quantityCount + 1);

                    currentX += sixMmStep;
                    output.Add(new KeyValuePair<Action, object[]>
                        (Action.RipCutForward, new object[] { currentX, "M/" + text }));
                    currentX += pittsburghStep;
                    output.Add(new KeyValuePair<Action, object[]>
                        (Action.RipCutBackward, new object[] { currentX, "F/" + text }));
                }
            }

            output.Add(new KeyValuePair<Action, object[]>(Action.End, null));

            return output;
        }

        
    }
}
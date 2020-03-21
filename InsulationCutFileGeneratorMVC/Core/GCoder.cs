using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Action = InsulationCutFileGeneratorMVC.Core.ActionGenerator.Action;
using InsulationCutFileGeneratorMVC.Core.ActionGenerator;

namespace InsulationCutFileGeneratorMVC.Core
{
    public static class GCoder
    {
        public static int GlobalLineBlockCounter { get; private set; } = 1;

        public static string GenerateGCode(List<KeyValuePair<Action, object[]>> actions)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < actions.Count; i++)
                sb.AppendLine(actions[i].ToGCode());
            return sb.ToString().Trim();
        }

        public static string ToGCode(this KeyValuePair<Action, object[]> action)
        {
            string output = "";
            switch (action.Key)
            {
                case ActionGenerator.Action.Initialize:
                    output = "H210*" + Environment.NewLine + "G71*";
                    break;
                case ActionGenerator.Action.End:
                    output = "M0*";
                    break;
                case ActionGenerator.Action.AddText:
                    output = string.Format("M31*{0}*", action.Value[0]);
                    break;
                case ActionGenerator.Action.BeginLineBlock:
                    output = string.Format("N{0:0}*", GlobalLineBlockCounter++);
                    break;
                case ActionGenerator.Action.KnifeUp:
                    output = "M15*";
                    break;
                case ActionGenerator.Action.KnifeDown:
                    output = "M14*";
                    break;
                case ActionGenerator.Action.MoveTo:
                    output = string.Format("X{0:0}Y{1:0}*",
                        (int)action.Value[0] * 10, (int)action.Value[1] * 10);
                    break;
                //case ActionGenerator.Action.HorizontalCutForward:
                //case ActionGenerator.Action.HorizontalCutBackward:
                case ActionGenerator.Action.RipCutForward:
                case ActionGenerator.Action.RipCutBackward:
                case ActionGenerator.Action.LCut:
                    output = DecomposeActionAndGetGCode(action);
                    break;
                default:
                    break;
            }
            return output;
        }
        public static void ResetGlobalLineBlockCounter() => GlobalLineBlockCounter = 1;
        private static string DecomposeActionAndGetGCode(KeyValuePair<Action, object[]> action)
        {
            var decomposedActions = ActionDecomposer.Decompose(action);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < decomposedActions.Count; i++)
                sb.AppendLine(decomposedActions[i].ToGCode());
            return sb.ToString().Trim();
        }
    }
}

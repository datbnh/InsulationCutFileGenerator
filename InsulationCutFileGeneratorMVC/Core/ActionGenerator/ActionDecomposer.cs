using InsulationCutFileGeneratorMVC.MVC_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{
    public static class ActionDecomposer
    {
        //public static string GetText(this KeyValuePair<Action, object[]> action)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(action.Key.ToString());
        //    if (action.Value != null)
        //        for (int i = 0; i < action.Value.Length; i++)
        //        {
        //            sb.Append(" " + action.Value[i].ToString());
        //        }
        //    return sb.ToString();
        //}

        //public static string GetText(this List<KeyValuePair<Action, object[]>> actions)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < actions.Count; i++)
        //    {
        //        var currentDecomposedActions = actions[i].Decompose();
        //        for (int j = 0; j < currentDecomposedActions.Count; j++)
        //        {
        //            sb.AppendLine(currentDecomposedActions[j].GetText());
        //        }
        //    }
        //    return sb.ToString();
        //}

        public static List<KeyValuePair<Action, object[]>> Decompose(this KeyValuePair<Action, object[]> action)
        {
            var output = new List<KeyValuePair<Action, object[]>>();
            switch (action.Key)
            {
                //case Action.HorizontalCutForward:
                //    output.AddRange(DecomposeHorizontalCutForward(action));
                //    break;
                //case Action.HorizontalCutBackward:
                //    output.AddRange(DecomposeHorizontalCutBackward(action));
                //    break;
                case Action.RipCutForward:
                    output.AddRange(DecomposeRipCutForward(action));
                    break;
                case Action.RipCutBackward:
                    output.AddRange(DecomposeRipCutBackward(action));
                    break;
                case Action.LCut:
                    output.AddRange(DecomposeLCut(action));
                    break;
                case Action.Initialize:
                case Action.End:
                case Action.AddText:
                case Action.BeginLineBlock:
                case Action.KnifeUp:
                case Action.KnifeDown:
                case Action.MoveTo:
                default:
                    output.Add(action);
                    break;
            }

            return output;
        }

        private static List<KeyValuePair<Action, object[]>> DecomposeLCut(KeyValuePair<Action, object[]> action)
        {
            var output = new List<KeyValuePair<Action, object[]>>();
            if (action.Value == null || action.Value.Length < 3)
                throw new ArgumentException("Invalid LCut action argument: this action must take at least two arguments.");
            var x1 = (int)(action.Value[0]);
            var x2 = (int)(action.Value[1]);
            var text = action.Value.Length >= 3 ? action.Value[2].ToString() : "";
            output.Add(new KeyValuePair<Action, object[]>(Action.BeginLineBlock, null));
            if (!string.IsNullOrEmpty(text))
                output.Add(new KeyValuePair<Action, object[]>(Action.AddText, new object[] { text }));
            output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x1, 0 }));
            output.Add(new KeyValuePair<Action, object[]>(Action.KnifeDown, null));
            output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x2, 0 }));
            output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x2, DataEntry.DUCT_FULL_LENGTH }));
            output.Add(new KeyValuePair<Action, object[]>(Action.KnifeUp, null));
            return output;
        }

        private static List<KeyValuePair<Action, object[]>> DecomposeRipCutBackward(KeyValuePair<Action, object[]> action)
        {
            var output = new List<KeyValuePair<Action, object[]>>();
            if (action.Value == null)
                throw new ArgumentException("Invalid RipCutBackward action argument: this action must take at least one argument.");
            var x = (int)(action.Value[0]);
            var text = action.Value.Length >= 2 ? action.Value[1].ToString() : "";
            output.Add(new KeyValuePair<Action, object[]>(Action.BeginLineBlock, null));
            if (!string.IsNullOrEmpty(text))
                output.Add(new KeyValuePair<Action, object[]>(Action.AddText, new object[] { text }));
            output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x, DataEntry.DUCT_FULL_LENGTH }));
            output.Add(new KeyValuePair<Action, object[]>(Action.KnifeDown, null));
            output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x, 0 }));
            output.Add(new KeyValuePair<Action, object[]>(Action.KnifeUp, null));
            return output;
        }

        private static List<KeyValuePair<Action, object[]>> DecomposeRipCutForward(KeyValuePair<Action, object[]> action)
        {
            var output = new List<KeyValuePair<Action, object[]>>();
            if (action.Value == null)
                throw new ArgumentException("Invalid RipCutForward action argument: this action must take at least one argument.");
            var x = (int)(action.Value[0]);
            var text = action.Value.Length >= 2 ? action.Value[1].ToString() : "";
            output.Add(new KeyValuePair<Action, object[]>(Action.BeginLineBlock, null));
            if (!string.IsNullOrEmpty(text))
                output.Add(new KeyValuePair<Action, object[]>(Action.AddText, new object[] { text }));
            output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x, 0 }));
            output.Add(new KeyValuePair<Action, object[]>(Action.KnifeDown, null));
            output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x, DataEntry.DUCT_FULL_LENGTH }));
            output.Add(new KeyValuePair<Action, object[]>(Action.KnifeUp, null));
            return output;
        }

        //private static List<KeyValuePair<Action, object[]>> DecomposeHorizontalCutBackward(KeyValuePair<Action, object[]> action)
        //{
        //    var output = new List<KeyValuePair<Action, object[]>>();
        //    if (action.Value == null || action.Value.Length < 3)
        //        throw new ArgumentException("Invalid HorizontalCutBackward action argument: " +
        //            "this action must take at least three arguments.");
        //    var x = (int)(action.Value[0]);
        //    var y = (int)(action.Value[1]);
        //    var length = (int)(action.Value[2]);
        //    var text = action.Value.Length >= 4 ? action.Value[3].ToString() : "";
        //    output.Add(new KeyValuePair<Action, object[]>(Action.BeginLineBlock, null));
        //    if (!string.IsNullOrEmpty(text))
        //        output.Add(new KeyValuePair<Action, object[]>(Action.AddText, new object[] { text }));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x, y }));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.KnifeDown, null));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x + length, y }));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.KnifeUp, null));
        //    return output;
        //}

        //private static List<KeyValuePair<Action, object[]>> DecomposeHorizontalCutForward(KeyValuePair<Action, object[]> action)
        //{
        //    var output = new List<KeyValuePair<Action, object[]>>();
        //    if (action.Value == null || action.Value.Length < 3)
        //        throw new ArgumentException("Invalid HorizontalCutForward action argument: this action must take at least three arguments.");
        //    var x = (int)(action.Value[0]);
        //    var y = (int)(action.Value[1]);
        //    var length = (int)(action.Value[2]);
        //    var text = action.Value.Length >= 4 ? action.Value[3].ToString() : "";
        //    output.Add(new KeyValuePair<Action, object[]>(Action.BeginLineBlock, null));
        //    if (!string.IsNullOrEmpty(text))
        //        output.Add(new KeyValuePair<Action, object[]>(Action.AddText, new object[] { text }));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x, y }));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.KnifeDown, null));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.MoveTo, new object[] { x - length, y }));
        //    output.Add(new KeyValuePair<Action, object[]>(Action.KnifeUp, null));
        //    return output;
        //}
    }
}

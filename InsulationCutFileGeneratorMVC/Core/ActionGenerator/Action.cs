using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Core.ActionGenerator
{
    public enum Action
    {
        [Description("INIT")]
        Initialize,
        [Description("STOP")]
        End,
        [Description("TEXT")]
        AddText,
        [Description("LINE")]
        BeginLineBlock,
        [Description("KNIFE_UP")]
        KnifeUp,
        [Description("KNIFE_DN")]
        KnifeDown,
        [Description("MOVE_TO")]
        MoveTo,
        // compound actions
        HorizontalCutForward,
        HorizontalCutBackward,
        RipCutForward,
        RipCutBackward,
    }
}

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
        [Description("LINE_BLOCK")]
        BeginLineBlock,
        [Description("KNIFE_UP")]
        KnifeUp,
        [Description("KNIFE_DOWN")]
        KnifeDown,
        [Description("MOVE_TO")]
        MoveTo,
        // compound actions
        ///// <summary>
        ///// MoveTo(arg0, arg1),
        ///// KnifeDown,
        ///// then MoveTo(arg0 + arg2, arg1),
        ///// then KnifeUp.
        ///// Text provided through arg3.
        ///// </summary>
        //HorizontalCutForward,
        ///// <summary>
        ///// MoveTo(arg0, arg1),
        ///// KnifeDown,
        ///// then MoveTo(arg0 - arg2, arg1),
        ///// then KnifeUp.
        ///// Text provided through arg3.
        ///// </summary>
        //HorizontalCutBackward,
        ///// <summary>
        ///// MoveTo(arg0, 0),
        ///// KnifeDown,
        ///// then MoveTo(arg0, DataEntry.DUCT_FULL_LENGTH),
        ///// then KnifeUp.
        ///// Text provided through arg1.
        ///// </summary>
        RipCutForward,
        /// <summary>
        /// MoveTo(arg0, DataEntry.DUCT_FULL_LENGTH),
        /// KnifeDown,
        /// then MoveTo(arg0, 0),
        /// then KnifeUp.
        /// Text provided through arg1.
        /// </summary>
        RipCutBackward,
        /// <summary>
        /// MoveTo(arg0, 0),
        /// KnifeDown,
        /// then MoveTo(arg1, 0),
        /// then MoveTo(arg1, DataEntry.DUCT_FULL_LENGTH),
        /// then KnifeUp.
        /// Text provided through arg2. 
        /// </summary>
        LCut,
    }
}

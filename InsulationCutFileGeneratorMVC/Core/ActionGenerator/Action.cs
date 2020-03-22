using System.ComponentModel;

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
        /// <summary>
        /// Compound action. Requires at least 1 arg: {int arg0, string arg1}.
        /// <para>
        /// Decomposed action sequence:
        /// ❶ MoveTo(arg0, 0)
        /// ➤ ❷ KnifeDown
        /// ➤ ❸ MoveTo(arg0, DataEntry.DUCT_FULL_LENGTH)
        /// ➤ ❹ KnifeUp.
        /// </para>
        /// Text can be provided through arg1.
        /// </summary>
        RipCutForward,

        /// <summary>
        /// Compound action. Requires at least 1 arg: {int arg0, string arg1}.
        /// <para>
        /// Decomposed action sequence:
        /// ❶ MoveTo(arg0, DataEntry.DUCT_FULL_LENGTH)
        /// ➤ ❷ KnifeDown
        /// ➤ ❸ MoveTo(arg0, 0)
        /// ➤ ❹ KnifeUp.
        /// </para>
        /// Text can provided through arg1.
        /// </summary>
        RipCutBackward,

        /// <summary>
        /// Compound action. Requires at least 2 args: {int arg0, int arg1, string arg2}.
        /// <para>
        /// Decomposed action sequence:
        /// ❶ MoveTo(arg0, 0)
        /// ➤ ❷ KnifeDown
        /// ➤ ❸ MoveTo(arg1, 0)
        /// ➤ ❹ MoveTo(arg1, DataEntry.DUCT_FULL_LENGTH)
        /// ➤ ❺ KnifeUp.
        /// </para>
        /// <para>
        /// Text can be provided through arg2.
        /// </para>
        /// </summary>
        LCut,
    }
}
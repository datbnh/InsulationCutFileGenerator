using System.ComponentModel;

namespace InsulationCutFileGenerator
{
    public enum InsulatingMode
    {
        [Description("Internal")]
        Internal,

        [Description("Lagging")]
        Lagging,

        [Description("Perforated/Double Skin")]
        Perforated
    }
}
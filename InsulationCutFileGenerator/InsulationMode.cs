using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

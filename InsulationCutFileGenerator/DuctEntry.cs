using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGenerator
{
    public class DuctEntry
    {
        public DuctEntryView View { get; private set; }
        public DuctEntryControl Control { get; private set; }
        public DuctEntryData Data { get; private set; }

        public int Index { get; private set; }

        public DuctEntry(int index)
        {
            Index = index;
            Data = new DuctEntryData(BuiltInInsulationTypes.Internal25, 0,0,0,"");
            Control = new DuctEntryControl(Data);
            View = new DuctEntryView(0, Data, Control);
        }

        internal DuctEntry Clone(int v)
        {
            throw new NotImplementedException();
        }
    }
}

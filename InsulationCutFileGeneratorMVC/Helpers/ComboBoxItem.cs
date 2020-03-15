using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Helpers
{
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public int Index { get; private set; }

        public ComboBoxItem(int index, string text, object value)
        {
            Text = text;
            Value = value;
            Index = index;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}

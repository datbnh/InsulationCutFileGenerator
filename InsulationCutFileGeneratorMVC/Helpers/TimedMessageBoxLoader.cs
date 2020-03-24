using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGeneratorMVC.Helpers
{
    public static class TimedMessageBoxLoader
    {
        public static void ShowTimedMessageBox(string filePath, string entryId, int interval)
        {
            TimedMessageBox timedMessageBox = new TimedMessageBox(filePath, entryId, interval);
            timedMessageBox.Show();
        }
    }
}

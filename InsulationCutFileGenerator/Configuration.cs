using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGenerator
{
    public class Configuration
    {
        public bool IsSingleEntry;
        public bool IsUsingPredefinedFolder;
        public bool IsUsingPredefinedFileName;
        public bool IsForceLongSideAsPittsburgMaleSide;
        public string OutputPredefinedFolder;
        public string OutputPredefinedFileName;

        public static Configuration Default
        {
            get => new Configuration
            {
                IsForceLongSideAsPittsburgMaleSide = true,
                IsSingleEntry = true,
                IsUsingPredefinedFileName = true,
                IsUsingPredefinedFolder = true,
                OutputPredefinedFileName = "STRAIGHT.CUT",
                OutputPredefinedFolder = @".\",
            };
        }
    }
}

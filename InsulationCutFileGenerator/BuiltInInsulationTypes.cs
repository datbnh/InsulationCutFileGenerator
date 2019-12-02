using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGenerator
{
    public static class BuiltInInsulationTypes
    {
        public static Insulation Internal25 { get => new Insulation(InsulatingMode.Internal, 25, 30, "+", "Internal 25 mm"); } // 50 - 20
        public static Insulation Internal38 { get => new Insulation(InsulatingMode.Internal, 38, 36, "@", "Internal 38 mm"); } // 76 - 40
        public static Insulation Internal50 { get => new Insulation(InsulatingMode.Internal, 50, 50, "++", "Internal 50 mm"); } // 100 - 50
        public static Insulation Internal75 { get => new Insulation(InsulatingMode.Internal, 75, 80, "#", "Internal 75 mm"); } // 150 - 70
        public static Insulation Internal100 { get => new Insulation(InsulatingMode.Internal, 100, 60, "##", "Internal 100 mm"); } // 200 - 140

        public static Insulation Lagging25 { get => new Insulation(InsulatingMode.Internal, 25, 120, "L+", "Lagging 25 mm"); }
        public static Insulation Lagging38 { get => new Insulation(InsulatingMode.Internal, 38, 140, "L@", "Lagging 38 mm"); }
        public static Insulation Lagging50 { get => new Insulation(InsulatingMode.Internal, 50, 170, "L++", "Lagging 50 mm"); }
        public static Insulation Lagging75 { get => new Insulation(InsulatingMode.Internal, 75, 230, "L#", "Lagging 75 mm"); }
        //public static InsulationType Lagging100 { get => new InsulationType(100, 5, "L##"); }

        public static Insulation Perf25 { get => new Insulation(InsulatingMode.Lagging, 25, 0, "P+", "Perf. 25 mm"); } // 50
        public static Insulation Perf38 { get => new Insulation(InsulatingMode.Lagging, 38, 0, "P@", "Perf. 38 mm"); } // 76
        public static Insulation Perf50 { get => new Insulation(InsulatingMode.Lagging, 50, 0, "P++", "Perf. 50 mm"); } // 100
        public static Insulation Perf75 { get => new Insulation(InsulatingMode.Lagging, 75, 0, "P#", "Perf. 75 mm"); } // 150
        public static Insulation Perf100 { get => new Insulation(InsulatingMode.Lagging, 100, 0, "P##", "Perf. 100 mm"); } // 200

        public static List<Insulation> PopulateAllBuiltInTypes()
        {
            return new List<Insulation>
            {
                Internal25,
                Internal38,
                Internal50,
                Internal75,
                Internal100,

                Lagging25,
                Lagging38,
                Lagging50,
                Lagging75,

                Perf25,
                Perf38,
                Perf50,
                Perf75,
                Perf100
            };
        }
    }
}

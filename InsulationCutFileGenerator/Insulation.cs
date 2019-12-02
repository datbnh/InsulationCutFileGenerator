using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsulationCutFileGenerator
{
    public class Insulation
    {
        public static readonly int FULL_SIZE = 1400;

        internal Insulation(InsulatingMode insulatingMode, int thickness, int shortEdgeTotalOverlaing, string id, string descrpition)
        {
            InsulatingMode = insulatingMode;
            Thickness = thickness;
            ShortEdgeTotalOverlapping = shortEdgeTotalOverlaing;
            LongEdgeTotalSeparation = longEdgeTotalSepartion;
            Id = id;
            Description = descrpition;
        }

        internal Insulation(int thickness, int pittsburgOverlapping, int sixMmSeparation, string id)
        {
            Thickness = thickness;
            ShortEdgeTotalOverlapping = pittsburgOverlapping;
            Id = id;
            LongEdgeTotalSeparation = sixMmSeparation;
        }

        /// <summary>
        /// Insulation thickness.
        /// </summary>
        public int Thickness { get; private set; }
        /// <summary>
        /// Two-sided overlapping for internal insulation OR overall overlapping for lagging insulation.
        /// </summary>
        public int ShortEdgeTotalOverlapping { get; private set; }
        public string Id { get; private set; }
        /// <summary>
        /// Two-sided separation for internal insulation.
        /// </summary>
        public int LongEdgeTotalSeparation { get; private set; }

        public string Description { get; private set; }

        internal const int longEdgeTotalSepartion = 15;
        //public bool IsLagging { get => Id.ToUpper().Contains("L"); }

        public InsulatingMode InsulatingMode { get; private set; }

        public override string ToString()
        {
            return Description + " [" + Id + "]";
        }
    }
}

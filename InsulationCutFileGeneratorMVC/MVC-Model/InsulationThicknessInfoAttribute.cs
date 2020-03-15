using System;

namespace InsulationCutFileGeneratorMVC
{
    internal class InsulationThicknessInfoAttribute : Attribute
    {
        public string Id { get; private set; }
        public string Description { get; private set; }
        public int InternalPittsburghAdjustment { get; private set; }
        public int InternalSixMmAdjustment { get; private set; }
        public int ExternalTotalAdjustment { get; private set; }
        public int DoubleSkinPittsburgAdjustment { get; private set; }
        public int DoubleSkinSixMmAdjustment { get; private set; }

        public InsulationThicknessInfoAttribute(string id, string description, int internalPittsburghAdjustment,
            int internalSixMmAdjustment, int externalTotalAdjustment, int doubleSkinPittsburgAdjustment, int doubleSkinSixMmAdjustment)
        {
            Id = id;
            Description = description;
            InternalPittsburghAdjustment = internalPittsburghAdjustment;
            InternalSixMmAdjustment = internalSixMmAdjustment;
            ExternalTotalAdjustment = externalTotalAdjustment;
            DoubleSkinPittsburgAdjustment = doubleSkinPittsburgAdjustment;
            DoubleSkinSixMmAdjustment = doubleSkinSixMmAdjustment;
        }
    }
}
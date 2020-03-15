using InsulationCutFileGeneratorMVC.Helpers;

namespace InsulationCutFileGeneratorMVC
{
    public enum InsulationThickness
    {
        [InsulationThicknessInfo("", "Undefined", 0, 0, 0, 0, 0)]
        Undefined = 0,
        [InsulationThicknessInfo("+", "25", -15, 30, 120, -15, -15)]
        _25 = 25,
        [InsulationThicknessInfo("@", "38", -15, 40, 140, -15, -15)]
        _38 = 38,
        [InsulationThicknessInfo("++", "50", -15, 50, 170, -15, -15)]
        _50 = 50,
        [InsulationThicknessInfo("#", "68/75", -15, 60, 230, -15, -15)]
        _68 = 68,
        [InsulationThicknessInfo("##", "100", -15, 80, 375, -15, -15)]
        _100 = 100,
    }

    public static class InsulationThicknessExtension
    {
        public static string GetId(this InsulationThickness t)
            => t.GetAttribute<InsulationThicknessInfoAttribute>().Id;
        public static string GetDescription(this InsulationThickness t)
            => t.GetAttribute<InsulationThicknessInfoAttribute>().Description;
        public static int GetInternalPittsburghAdjustment(this InsulationThickness t)
            => t.GetAttribute<InsulationThicknessInfoAttribute>().InternalPittsburghAdjustment;
        public static int GetInternalSixMmAdjustment(this InsulationThickness t)
            => t.GetAttribute<InsulationThicknessInfoAttribute>().InternalSixMmAdjustment;
        public static int GetExternalTotalAdjustment(this InsulationThickness t)
            => t.GetAttribute<InsulationThicknessInfoAttribute>().ExternalTotalAdjustment;
        public static int GetDoubleSkinPittsburgAdjustment(this InsulationThickness t)
            => t.GetAttribute<InsulationThicknessInfoAttribute>().DoubleSkinPittsburgAdjustment;
        public static int GetDoubleSkinSixMmAdjustment(this InsulationThickness t)
            => t.GetAttribute<InsulationThicknessInfoAttribute>().DoubleSkinSixMmAdjustment;
    }
}
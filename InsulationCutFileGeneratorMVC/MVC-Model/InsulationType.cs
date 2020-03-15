using InsulationCutFileGeneratorMVC.Helpers;
using System.ComponentModel;

namespace InsulationCutFileGeneratorMVC
{
    public enum InsulationType
    {
        [InsulationTypeInfo("", "Undefined")]
        Undefined,
        [InsulationTypeInfo("A", "Internal")]
        Internal,
        [InsulationTypeInfo("L", "Lagging")]
        External,
        [InsulationTypeInfo("P", "Internal for double-skin ducts")]
        InternalDoubleSkin
    }

    public static class InsulationTypeExtensions
    {
        public static string GetId(this InsulationType t)
            => t.GetAttribute<InsulationTypeInfoAttribute>().Id;
        public static string GetDescription(this InsulationType t)
            => t.GetAttribute<InsulationTypeInfoAttribute>().Description;
    }
}
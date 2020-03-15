using System;

namespace InsulationCutFileGeneratorMVC
{
    internal class InsulationTypeInfoAttribute : Attribute
    {
        public InsulationTypeInfoAttribute(string id, string description)
        {
            Id = id;
            Description = description;
        }
        public string Id { get; private set; }
        public string Description { get; private set; }
    }

}
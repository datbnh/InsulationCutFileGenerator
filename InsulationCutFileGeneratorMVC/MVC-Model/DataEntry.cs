namespace InsulationCutFileGeneratorMVC
{
    public class DataEntry
    {
        internal DataEntry(string id)
        {
            Id = id;
            InsulationThickness = InsulationThickness.Undefined;
            InsulationType = InsulationType.Undefined;
        }

        internal DataEntry(string id, string jobName, string ductId)
        {
            Id = id;
            JobName = jobName;
            DuctId = ductId;
            InsulationThickness = InsulationThickness.Undefined;
            InsulationType = InsulationType.Undefined;
        }

        private DataEntry() { }

        public string Id { get; set; }

        public string JobName { get; set; }

        public string DuctId { get; set; }

        public InsulationThickness InsulationThickness { get; set; }
        public InsulationType InsulationType { get; set; }

        public int PittsburghSize { set; get; }
        public int SixMmSize { set; get; }
        public int Quantity { get; set; }
    }
}
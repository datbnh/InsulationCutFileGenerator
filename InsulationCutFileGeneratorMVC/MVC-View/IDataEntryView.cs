using InsulationCutFileGeneratorMVC.MVC_Model;
using InsulationCutFileGeneratorMVC.MVC_Controller;

namespace InsulationCutFileGeneratorMVC.MVC_View
{
    public interface IDataEntryView
    {
        string DuctId { get; set; }
        string EntryId { get; set; }
        InsulationThickness InsulationThickness { get; set; }
        InsulationType InsulationType { get; set; }
        bool IsDataChanged { get; set; }
        string JobName { get; set; }
        int PittsburghSize { get; set; }
        int Quantity { get; set; }
        int SixMmSize { get; set; }

        void AddEntryToListView(DataEntry currentEntry);
        string GetSelectedEntryId();
        void RemoveEntryFromListView(DataEntry entry);
        void SelectEntry(DataEntry currentEntry);
        void SetController(DataEntryController controller);
        void SetMode(DataEntryViewMode mode);
        void UpdateEntryInListVew(DataEntry currentEntry);
    }
}
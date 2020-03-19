namespace InsulationCutFileGeneratorMVC.MVC_Controller
{
    public interface IDataEntryController
    {
        void AddOrUpdateCurrentEntry();
        void BeginModifyingSelectedEntry();
        void CancelModifyingSelectedEntry();
        void ClearCurrentEntry();
        void CreateNewEntry();
        void DuplicateSelectedEntry();
        void LoadView();
        void RemoveCurrentEntry();
        void SelectEntry(string id);
    }
}
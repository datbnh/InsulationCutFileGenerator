using System.Collections;

namespace InsulationCutFileGeneratorMVC
{
    public class DataEntryController
    {
        private DataEntry currentEntry;
        private readonly IList dataEntries;
        private string lastSelectedId = "1";
        private int nextId = 1;
        private DataEntryView view;

        public DataEntryController(DataEntryView view, IList dataEntries)
        {
            this.view = view;
            this.dataEntries = dataEntries;
            view.SetController(this);
        }

        internal void AddOrUpdateCurrentEntry()
        {
            UpdateViewToCurrentEntry();
            if (!dataEntries.Contains(currentEntry)) // update existing entry
            {
                dataEntries.Add(currentEntry);
                view.AddEntryToListView(currentEntry);
                nextId++;
            }
            else // add new entry
            {
                view.UpdateEntryInListVew(currentEntry);
            }
            view.SetMode(DataEntryViewMode.View);
            view.SetSelectedEntry(currentEntry);
        }

        internal void BeginModifyingSelectedEntry()
        {
            view.SetMode(DataEntryViewMode.Edit);
        }

        internal void CancelModifyingSelectedEntry()
        {
            SelectEntry(lastSelectedId);
        }

        internal void ClearCurrentEntry()
        {
            view.EntryId = currentEntry.Id;
            view.JobName = "";
            view.DuctId = "";
            view.PittsburghSize = 0;
            view.SixMmSize = 0;
            view.InsulationType = InsulationType.Internal;
            view.InsulationThickness = InsulationThickness._50;
            view.IsDataChanged = true;
        }

        internal void CreateNewEntry()
        {
            currentEntry = new DataEntry(nextId.ToString());
            UpdateViewFromCurrentEntry();
            view.SetMode(DataEntryViewMode.New);
        }

        internal void DuplicateSelectedEntry()
        {
            lastSelectedId = currentEntry.Id; // save current id in case user cancels action
            currentEntry = new DataEntry(nextId.ToString())
            {
                JobName = view.JobName,
                DuctId = view.DuctId,
                PittsburghSize = view.PittsburghSize,
                SixMmSize = view.SixMmSize,
                InsulationType = view.InsulationType,
                InsulationThickness = view.InsulationThickness,
                Quantity = view.Quantity,
            };
            UpdateViewFromCurrentEntry();
            view.SetMode(DataEntryViewMode.New);
        }

        internal void LoadView()
        {
            CreateNewEntry(); // prompt to creat new entry
        }

        internal void SelectedEntryChanged(string id)
        {
            SelectEntry(id);
        }

        private void SelectEntry(string id)
        {
            foreach (DataEntry entry in dataEntries)
            {
                if (entry.Id == id)
                {
                    currentEntry = entry;
                    UpdateViewFromCurrentEntry();
                    view.SetSelectedEntry(entry);
                    view.SetMode(DataEntryViewMode.View);
                    break;
                }
            }
        }

        private void UpdateViewFromCurrentEntry()
        {
            view.EntryId = currentEntry.Id;
            view.JobName = currentEntry.JobName;
            view.DuctId = currentEntry.DuctId;
            view.PittsburghSize = currentEntry.PittsburghSize;
            view.SixMmSize = currentEntry.SixMmSize;
            view.InsulationType = currentEntry.InsulationType;
            view.InsulationThickness = currentEntry.InsulationThickness;
            view.Quantity = currentEntry.Quantity;
            view.IsDataChanged = false;
        }

        private void UpdateViewToCurrentEntry()
        {
            currentEntry.Id = view.EntryId;
            currentEntry.JobName = view.JobName;
            currentEntry.DuctId = view.DuctId;
            currentEntry.PittsburghSize = view.PittsburghSize;
            currentEntry.SixMmSize = view.SixMmSize;
            currentEntry.InsulationType = view.InsulationType;
            currentEntry.InsulationThickness = view.InsulationThickness;
            currentEntry.Quantity = view.Quantity;
        }
    }
}
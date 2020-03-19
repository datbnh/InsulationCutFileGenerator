using InsulationCutFileGeneratorMVC.Core;
using InsulationCutFileGeneratorMVC.MVC_Model;
using InsulationCutFileGeneratorMVC.MVC_View;
using System.Collections;

namespace InsulationCutFileGeneratorMVC.MVC_Controller
{
    public class DataEntryController : IDataEntryController
    {
        private DataEntry currentEntry;
        private readonly IList dataEntries;
        private string lastSelectedId = "1";
        private int nextId = 1;
        private readonly DataEntryView view;

        private Core.DataEntryValidatorFactory validatorFactory;
        private Core.IDataEntryValidator validator;

        public DataEntryController(DataEntryView view, IList dataEntries)
        {
            this.view = view;
            this.dataEntries = dataEntries;
            view.SetController(this);
            validatorFactory = new Core.DataEntryValidatorFactory();
        }

        public void AddOrUpdateCurrentEntry()
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
            view.SelectEntry(currentEntry);
            view.ShowValidationInfo(currentEntry);
        }

        public void BeginModifyingSelectedEntry()
        {
            view.SetMode(DataEntryViewMode.Edit);
        }

        public void CancelModifyingSelectedEntry()
        {
            SelectEntry(lastSelectedId);
        }

        public void ClearCurrentEntry()
        {
            view.EntryId = currentEntry.Id;
            view.JobName = "";
            view.DuctId = "";
            view.PittsburghSize = 0;
            view.SixMmSize = 0;
            view.InsulationType = InsulationType.Undefined;
            view.InsulationThickness = InsulationThickness.Undefined;
            view.Quantity = 1;
            view.IsDataChanged = true;
        }

        public void CreateNewEntry()
        {
            currentEntry = new DataEntry(nextId.ToString());
            UpdateViewFromCurrentEntry();
            view.SetMode(DataEntryViewMode.New);
        }

        public void DuplicateSelectedEntry()
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

        public void LoadView()
        {
            CreateNewEntry(); // prompt to creat new entry
        }

        public void SelectEntry(string id)
        {
            foreach (DataEntry entry in dataEntries)
            {
                if (entry.Id == id)
                {
                    currentEntry = entry;
                    UpdateViewFromCurrentEntry();
                    view.SelectEntry(entry);
                    view.SetMode(DataEntryViewMode.View);
                    view.ShowValidationInfo(entry);
                    break;
                }
            }
        }

        public DataEntryValidationResult Validate(DataEntry entry)
        {
            validator = validatorFactory.GetInstance(entry.InsulationType);
            return validator.Validate(entry);
        }

        public void RemoveCurrentEntry()
        {
            for (int i = 0; i < dataEntries.Count; i++)
            {
                var entry = (DataEntry)dataEntries[i];
                if (entry.Id.Equals(view.GetSelectedEntryId()))
                {
                    view.RemoveEntryFromListView(entry);
                    dataEntries.RemoveAt(i);
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
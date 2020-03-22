using InsulationCutFileGeneratorMVC.Core;
using InsulationCutFileGeneratorMVC.Core.ActionGenerator;
using InsulationCutFileGeneratorMVC.Core.DataEntryValidator;
using InsulationCutFileGeneratorMVC.MVC_Model;
using InsulationCutFileGeneratorMVC.MVC_View;
using System;
using System.Collections;
using System.Media;

namespace InsulationCutFileGeneratorMVC.MVC_Controller
{
    public class DataEntryController : IDataEntryController
    {
        public DataEntry CurrentEntry { get; private set; }
        private readonly IList dataEntries;
        private string lastSelectedId = "1";
        private int nextId = 1;
        private readonly DataEntryView view;

        public DataEntryController(DataEntryView view, IList dataEntries)
        {
            this.view = view;
            this.dataEntries = dataEntries;
            view.SetController(this);
        }

        public void AddOrUpdateCurrentEntry()
        {
            UpdateViewToCurrentEntry();
            var result = Validate(CurrentEntry);
            view.ShowValidationInfo(result);
            if (!result.IsValid)
            {
                SystemSounds.Exclamation.Play();
                return; // only valid entry will be accepted.
            }
            if (!dataEntries.Contains(CurrentEntry)) // update existing entry
            {
                dataEntries.Add(CurrentEntry);
                view.AddEntryToListView(CurrentEntry);
                nextId++;
            }
            else // add new entry
            {
                view.UpdateEntryInListVew(CurrentEntry);
            }
            view.SetMode(DataEntryViewMode.View);
            view.SelectEntry(CurrentEntry);
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
            view.EntryId = CurrentEntry.Id;
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
            CurrentEntry = new DataEntry(nextId.ToString());
            UpdateViewFromCurrentEntry();
            view.SetMode(DataEntryViewMode.New);
        }

        public void DuplicateSelectedEntry()
        {
            lastSelectedId = CurrentEntry.Id; // save current id in case user cancels action
            CurrentEntry = new DataEntry(nextId.ToString())
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
                    CurrentEntry = entry;
                    UpdateViewFromCurrentEntry();
                    view.SelectEntry(entry);
                    view.SetMode(DataEntryViewMode.View);
                    view.ShowValidationInfo(Validate(entry));
                    break;
                }
            }
        }

        public DataEntryValidationResult Validate(DataEntry entry)
        {
            return entry.Validate();
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
            view.EntryId = CurrentEntry.Id;
            view.JobName = CurrentEntry.JobName;
            view.DuctId = CurrentEntry.DuctId;
            view.PittsburghSize = CurrentEntry.PittsburghSize;
            view.SixMmSize = CurrentEntry.SixMmSize;
            view.InsulationType = CurrentEntry.InsulationType;
            view.InsulationThickness = CurrentEntry.InsulationThickness;
            view.Quantity = CurrentEntry.Quantity;
            view.IsDataChanged = false;
        }

        private void UpdateViewToCurrentEntry()
        {
            CurrentEntry.Id = view.EntryId;
            CurrentEntry.JobName = view.JobName;
            CurrentEntry.DuctId = view.DuctId;
            CurrentEntry.PittsburghSize = view.PittsburghSize;
            CurrentEntry.SixMmSize = view.SixMmSize;
            CurrentEntry.InsulationType = view.InsulationType;
            CurrentEntry.InsulationThickness = view.InsulationThickness;
            CurrentEntry.Quantity = view.Quantity;
        }

        internal void PreviewGCode()
        {
            GCoder.ResetGlobalLineBlockCounter();
            var text = GCoder.GenerateGCode(CurrentEntry.GenerateActionSequence());
            view.PreviewGCode(text);
        }
    }
}
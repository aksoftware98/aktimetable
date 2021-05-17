using AK_Timetable.Models;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AK_Timetable.Components
{
    public partial class WorkItemsList : ComponentBase
    {

        [Parameter]
        public EventCallback<WorkItem> WorkItemSelect { get; set; }

        private WorkItem _selectedModel = null;
        private List<WorkItem> _workItems = new List<WorkItem>();
        private bool _isFromShown = false; 

        private void ToggleForm()
        {
            _selectedModel = null;
            _isFromShown = !_isFromShown;
        }

        private void OnFormSubmitted(WorkItem workItem)
        {
            // On new added
            if (_selectedModel == null)
                _workItems.Add(workItem);
            else // On existing deleted 
            {
                _selectedModel.Color = workItem.Color;
                _selectedModel.Description = workItem.Description;
                _selectedModel.Name = workItem.Name;
                _selectedModel.Severity = workItem.Severity;
            }
        }

        protected override void OnInitialized()
        {
            try
            {
                var content = File.ReadAllText("C:\\Users\\dell\\Desktop\\May table.json");
                _workItems = JsonConvert.DeserializeObject<List<WorkItem>>(content);
            }
            catch (Exception)
            {
                _workItems = new List<WorkItem>(); 
            }
            
        }


        private async Task SaveAsync()
        {
            var options = new SaveDialogOptions()
            {
                Title = "Choose a location",
                Filters = new[]
                {
                    new FileFilter { Name = "JSON", Extensions = new[] { "json" } }
                }
            };

            var mainWindow = Electron.WindowManager.BrowserWindows.First();
            var result = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
            if (result != null)
            {
                File.WriteAllText(result, JsonConvert.SerializeObject(_workItems));
            }

        }

        private void SelectWorkItem(WorkItem workItem)
        {
            _selectedModel = workItem;
            WorkItemSelect.InvokeAsync(workItem);
        }

    }
}

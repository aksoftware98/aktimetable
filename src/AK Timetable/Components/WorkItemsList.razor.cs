using AK_Timetable.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AK_Timetable.Components
{
    public partial class WorkItemsList : ComponentBase
    {

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



    }
}

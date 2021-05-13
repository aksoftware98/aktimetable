using AK_Timetable.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Components
{
    public partial class WorkItemForm
    {
        
        [Parameter]
        public EventCallback<WorkItem> OnFormSubmitted { get; set; }

        [Parameter]
        public WorkItem Model { get; set; }

        private bool _isEdit = false;

        protected override void OnParametersSet()
        {
            if (Model == null)
            {
                Model = new WorkItem();
                _isEdit = false;
            }
            else
            {
                _isEdit = true;
            }
            base.OnParametersSet();
        }

        private void SubmitForm()
        {
            OnFormSubmitted.InvokeAsync(Model);
            Model = null;
        }

    }
}

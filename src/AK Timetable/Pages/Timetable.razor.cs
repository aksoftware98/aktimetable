using AK_Timetable.Components;
using AK_Timetable.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Pages
{
    [Authorize]
    public partial class Timetable : ComponentBase
    {

        [Inject]
        public GraphServiceClient Graph { get; set; }

        private TimetableBlock _selectedBlock;
        private AKTimetable _timetable;
        private string _name = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            var result = await Graph.Me.Request().GetAsync();
            _name = result.DisplayName;
        }

        private void OnSelectWorkItem(WorkItem workItem)
        {
            
            _timetable.SetWorkItem(workItem);
        }

    }
}

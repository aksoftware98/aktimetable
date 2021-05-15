using AK_Timetable.Components;
using AK_Timetable.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Pages
{
    public partial class Timetable : ComponentBase
    {


        private TimetableBlock _selectedBlock;
        private AKTimetable _timetable;

        private void OnSelectWorkItem(WorkItem workItem)
        {
            _timetable.SetWorkItem(workItem);
        }

    }
}

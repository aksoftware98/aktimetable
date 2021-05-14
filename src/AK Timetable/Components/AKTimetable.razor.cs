using AK_Timetable.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Components
{
    public partial class AKTimetable : ComponentBase
    {
        
        [Parameter]
        public Timetable Timetable { get; set; }



    }
}

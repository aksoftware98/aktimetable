using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AK_Timetable.Models
{
    public class Timetable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Day { get; set; }

        public List<TimetableBlock> Blocks { get; set; }
    }
}

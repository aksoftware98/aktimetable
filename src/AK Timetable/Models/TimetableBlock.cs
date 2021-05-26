using System;

namespace AK_Timetable.Models
{
    public class TimetableBlock
    {
        public string Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Note { get; set; }

        public WorkItem WorkItem { get; set; }
        public int Sequence { get; set; }
        public string Name => WorkItem?.Name;
    }
}

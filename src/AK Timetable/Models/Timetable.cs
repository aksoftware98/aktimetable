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

        public Timetable()
        {
            Blocks = new List<TimetableBlock>();
            InitializeTimetable();
        }

        private void InitializeTimetable()
        {
            var datetime = DateTime.Today;
            for (int i = 0; i < 96; i++)
            {
                int mins = i * 15;
                var startDate = datetime.AddMinutes(mins);
                var endDate = startDate.AddMinutes(15);
                Blocks.Add(new TimetableBlock
                {
                    StartDate = startDate,
                    EndDate = endDate,
                });
            }
        }
    }
}

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
        private DateTime _day;
        public DateTime Day
        {
            get => _day;
            set
            {
                _day = value;
                ChangeDay();
            }
        }

        public List<TimetableBlock> Blocks { get; set; }

        public Timetable()
        {
            Blocks = new List<TimetableBlock>();
            Day = DateTime.Now.Date;
            ChangeDay(); 
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
                    Number = i + 1,
                });
            }
        }
        private void ChangeDay()
        {
            if (Blocks != null)
                foreach (var item in Blocks)
                {
                    var startDate = new DateTime(_day.Year, _day.Month, _day.Day, item.StartDate.Hour, item.StartDate.Minute, item.StartDate.Second, DateTimeKind.Local);
                    var endDate = new DateTime(_day.Year, _day.Month, _day.Day, item.EndDate.Hour, item.EndDate.Minute, item.EndDate.Second, DateTimeKind.Local);
                    item.StartDate = startDate;
                    item.EndDate = endDate;
                }
        }
    }
}

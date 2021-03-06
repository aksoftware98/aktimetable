using System;
using System.ComponentModel.DataAnnotations;

namespace AK_Timetable.Models
{
    public class WorkItem
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public Severity Severity { get; set; }

        public WorkItem()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}

﻿using System;

namespace AK_Timetable.Models
{
    public class TimetableBlock
    {
        public string Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Note { get; set; }
    }
}
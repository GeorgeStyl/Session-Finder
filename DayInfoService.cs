using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NodeJSClient
{
    public class DayEntry
    {
        public int Day { get; set; }
        public string Name { get; set; }
        public string RoomID { get; set; }
        public string Availability { get; set; } // yes, no, maybe
        public string Time { get; set; }         // e.g. "14:00"
    }

    public class DayInfoService
    {
        private readonly Dictionary<int, DayEntry> _dayData;

        public DayInfoService()
        {
            _dayData = new Dictionary<int, DayEntry>();
            List<DayEntry> entries = new List<DayEntry>();

            entries.Add(new DayEntry
            {
                Day = 1,
                Name = "Alice",
                RoomID = "A101",
                Availability = "yes",
                Time = "09:00"
            });

            entries.Add(new DayEntry
            {
                Day = 2,
                Name = "Bob",
                RoomID = "B202",
                Availability = "maybe",
                Time = "10:30"
            });

            entries.Add(new DayEntry
            {
                Day = 3,
                Name = "Carol",
                RoomID = "C303",
                Availability = "no",
                Time = "11:00"
            });

        }


        public DayEntry GetEntryForDay(int day)
        {
            return _dayData.TryGetValue(day, out var entry) ? entry : null;
        }
    }
}

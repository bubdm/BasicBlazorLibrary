using System;
namespace BasicBlazorLibrary.Components.CalendarPopups
{
    internal class DateSpot
    {
        public int Row { get; set; }
        public int Column { get; set; } //its one based now.
        public DateTime Date { get; set; } //this is needed too now.
    }
}
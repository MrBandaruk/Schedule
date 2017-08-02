using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAC.Dto
{
    public class CalendarDtoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Additional { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CalendarViewDtoItem
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }

    public class CalendarDtoModel
    {
        public List<CalendarDtoItem> Events { get; set; }
    }

    public class CalendarViewDto
    {
        public List<CalendarViewDtoItem> Events { get; set; }
    }
}

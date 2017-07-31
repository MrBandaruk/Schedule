using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAC.Dto
{
    public class CalendarDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Additional { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

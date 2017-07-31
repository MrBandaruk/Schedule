using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.BLL.Model
{
    public class CalendarModelItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Additional { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        
    }

    public class CalendarViewModelItem
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }

    }

    public class CalendarModel
    {
        public List<CalendarModelItem> Events { get; set; }
    }

    public class CalendarViewModel
    {
        public List<CalendarViewModelItem> Events { get; set; }
    }
}

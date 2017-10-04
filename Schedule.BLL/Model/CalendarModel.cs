using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.BLL.Model
{
    public class CalendarModelItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Additional")] //TODO: придумать, что делать с пустым Additional
        [StringLength(300)]
        public string Additional { get; set; }

        [Display(Name = "Stard Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }        
    }

    public class CalendarViewModelItem
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string color { get; set; }

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

using System.Data.Entity;

namespace Schedule.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageLink { get; set; }
        public string Body { get; set; }
    }

  
}
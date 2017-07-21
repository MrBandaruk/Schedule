using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.BLL.Model
{
    public class NewsViewModelItem
    {
        public int Id { get; set; }
        public string ShortTitle { get; set; }
        public string FullTitle { get; set; }
        public string ShortArticle { get; set; }
        public string FullArticle { get; set; }

        public virtual ICollection<NewsImageModelItem> NewsImages { get; set; }

    }

    public class NewsImageModelItem
    {
        public int Id { get; set; }
        public byte[] ImageItem { get; set; }
        public int NewsId { get; set; }

    }

    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public class NewsViewModel
    {
        public List<NewsViewModelItem> News { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}

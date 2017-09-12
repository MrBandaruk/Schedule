using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Dto
{
    public class NewsDtoItem
    {
        public int Id { get; set; }
        public string ShortTitle { get; set; }
        public string FullTitle { get; set; }
        public string ShortArticle { get; set; }
        public string FullArticle { get; set; }

        public virtual ICollection<NewsImageDto> NewsImages { get; set; }
    }

    public class NewsImageDto
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

    public class NewsDtoModel
    {
        public List<NewsDtoItem> News { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class DataTablesDtoModel
    {
        public List<NewsDtoItem> News { get; set; }
        public int iTotalRecords { get; set; }
    }

}

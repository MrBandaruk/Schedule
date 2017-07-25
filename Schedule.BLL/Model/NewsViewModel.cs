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

    public class DataTablesPanelModel
    {
        public List<NewsViewModelItem> News { get; set; }
        public int iTotalRecords { get; set; }
    }

    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    public class jQueryDataTableParamModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DAL.Dto;

namespace Schedule.DAL.Providers.GetAllNews
{
    public class SortForPanel 
    {        
        private int start, length, column, totalItems = new DataBaseDataContext().FinalNews.Count();
        private string sortType;
        NewsDbProviderHelpers helper = new NewsDbProviderHelpers();

        public int TotalItems
        {
            get
            {
                return totalItems;
            }
        }

        public SortForPanel(int start, int length, int sortCol, string sortType)
        {
            this.start = start;
            this.length = length;
            this.column = sortCol;
            this.sortType = sortType;
        }

        public DataTablesDtoModel Sort()
        {
            switch (column)
            {
                case 1:
                    return SortById();

                case 2:
                    return SortByTitle();

                case 3:
                    return SortByArticle();

                default:
                    return SortById();
            }
        }

        public DataTablesDtoModel SortById()
        {
            using (var db = new DataBaseDataContext())
            {
                if (sortType == "asc")
                {
                    return helper.MapDbToDto(db.FinalNews.OrderBy(x => x.Id).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
                else
                {
                    return helper.MapDbToDto(db.FinalNews.OrderByDescending(x => x.Id).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
            }
        }

        public DataTablesDtoModel SortByTitle()
        {
            using (var db = new DataBaseDataContext())
            {
                if (sortType == "asc")
                {
                    return helper.MapDbToDto(db.FinalNews.OrderBy(x => x.FullTitle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
                else
                {
                    return helper.MapDbToDto(db.FinalNews.OrderByDescending(x => x.FullTitle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
            }
        }

        public DataTablesDtoModel SortByArticle()
        {
            using (var db = new DataBaseDataContext())
            {
                if (sortType == "asc")
                {
                    return helper.MapDbToDto(db.FinalNews.OrderBy(x => x.FullArticle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
                else
                {
                    return helper.MapDbToDto(db.FinalNews.OrderByDescending(x => x.FullArticle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
            }
        }

    }
}

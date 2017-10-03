using Schedule.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Providers.GetAllNews
{
    public class SortForPanelWithSearch
    {
        private int start, length, column, totalItems = new DataBaseDataContext().FinalNews.Count();
        private string sortType, searchStr;
        NewsDbProviderHelpers helper = new NewsDbProviderHelpers();

        public int TotalItems
        {
            get
            {
                return totalItems;
            }
        }

        public SortForPanelWithSearch(int start, int length, int sortCol, string sortType, string searchStr)
        {
            this.start = start;
            this.length = length;
            this.column = sortCol;
            this.sortType = sortType;
            this.searchStr = searchStr;
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
                    return helper.MapDbToDto(db.FinalNews.Where(n => n.ShortTitle.Contains(searchStr) ||
                           n.FullTitle.Contains(searchStr) || n.ShortArticle.Contains(searchStr) ||
                           n.FullArticle.Contains(searchStr)).OrderBy(x => x.Id).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
                else
                {
                    return helper.MapDbToDto(db.FinalNews.Where(n => n.ShortTitle.Contains(searchStr) ||
                           n.FullTitle.Contains(searchStr) || n.ShortArticle.Contains(searchStr) ||
                           n.FullArticle.Contains(searchStr)).OrderByDescending(x => x.Id).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
            }
        }

        public DataTablesDtoModel SortByTitle()
        {
            using (var db = new DataBaseDataContext())
            {
                if (sortType == "asc")
                {
                    return helper.MapDbToDto(db.FinalNews.Where(n => n.ShortTitle.Contains(searchStr) ||
                           n.FullTitle.Contains(searchStr) || n.ShortArticle.Contains(searchStr) ||
                           n.FullArticle.Contains(searchStr)).OrderBy(x => x.FullTitle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
                else
                {
                    return helper.MapDbToDto(db.FinalNews.Where(n => n.ShortTitle.Contains(searchStr) ||
                           n.FullTitle.Contains(searchStr) || n.ShortArticle.Contains(searchStr) ||
                           n.FullArticle.Contains(searchStr)).OrderByDescending(x => x.FullTitle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
            }
        }

        public DataTablesDtoModel SortByArticle()
        {
            using (var db = new DataBaseDataContext())
            {
                if (sortType == "asc")
                {
                    return helper.MapDbToDto(db.FinalNews.Where(n => n.ShortTitle.Contains(searchStr) ||
                           n.FullTitle.Contains(searchStr) || n.ShortArticle.Contains(searchStr) ||
                           n.FullArticle.Contains(searchStr)).OrderBy(x => x.FullArticle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
                else
                {
                    return helper.MapDbToDto(db.FinalNews.Where(n => n.ShortTitle.Contains(searchStr) ||
                           n.FullTitle.Contains(searchStr) || n.ShortArticle.Contains(searchStr) ||
                           n.FullArticle.Contains(searchStr)).OrderByDescending(x => x.FullArticle).Skip(start).Take(helper.GetAllForTable(totalItems, length, start)).ToList(), totalItems);
                }
            }
        }
    }
}

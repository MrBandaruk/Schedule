using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DAL.Dto;
using System.Data.Entity;

namespace Schedule.DAL.Providers.GetAllNews
{
    public class SortForIndexWithSearch : ISortData
    {
        private int pageSize;
        private int page;
        private string searchString;
        private string sortOrder;
        private int totalItems = new DataBaseDataContext().FinalNews.Count();
        public int totalItemsWithSearch
        {
            get
            {
                return new DataBaseDataContext().FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                                n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                                n.FullArticle.Contains(searchString)).Count();
            }
        }

        public SortForIndexWithSearch(string searchStr, string srtOrder, int pgSize, int pgNumber)
        {
            pageSize = pgSize;
            page = pgNumber;
            searchString = searchStr;
            sortOrder = srtOrder;
        }


        public IQueryable<FinalNews> Sort()
        {
            switch (sortOrder)
            {
                case "A-Z":
                    return ByAZ();

                case "Z-A":
                    return ByZA();

                case "Old":
                    return ByOld();

                case "New":
                    return ByNew();

                default:
                    return ByAZ();
            }
        }


        public IQueryable<FinalNews> ByAZ()
        {
            using (var db = new DataBaseDataContext())
            {
                return db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                              n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                              n.FullArticle.Contains(searchString)).OrderBy(x => x.ShortTitle).Skip((page - 1) * pageSize)
                              .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
            }

        }

        public IQueryable<FinalNews> ByZA()
        {
            using (var db = new DataBaseDataContext())
            {

                return db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                                    n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                                    n.FullArticle.Contains(searchString)).OrderByDescending(x => x.ShortTitle).Skip((page - 1) * pageSize)
                                    .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
            }

        }

        public IQueryable<FinalNews> ByOld()
        {
            using (var db = new DataBaseDataContext())
            {

                return db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                                    n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                                    n.FullArticle.Contains(searchString)).OrderBy(x => x.Id).Skip((page - 1) * pageSize)
                                    .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
            }

        }

        public IQueryable<FinalNews> ByNew()
        {
            using (var db = new DataBaseDataContext())
            {

                return db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                                    n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                                    n.FullArticle.Contains(searchString)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
                                    .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
            }

        }

        private int All(int totalItems, int pageSize, int page)
        {
            int total = totalItems - ((page - 1) * pageSize);
            if (total >= pageSize)
            {
                return pageSize;
            }
            return total;
        }
    }
}

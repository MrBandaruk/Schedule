using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Providers.GetAllNews
{
    public class SortForIndex : ISortData
    {
        private string sortOrder;
        private int pageSize;
        private int page;
        private int totalItems = new DataBaseDataContext().FinalNews.Count();

        public int TotalItems
        {
            get
            {
                return totalItems;
            }
        }

        public SortForIndex(string srtOrder, int pgSize, int pgNumber)
        {
            sortOrder = srtOrder;
            pageSize = pgSize;
            page = pgNumber;
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
                return db.FinalNews.OrderBy(x => x.ShortTitle).Skip((page - 1) * pageSize)
                                    .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
            }
        }

        public IQueryable<FinalNews> ByNew()
        {
            var db = new DataBaseDataContext();
            
                return db.FinalNews.OrderByDescending(x => x.ShortTitle).Skip((page - 1) * pageSize)
                                    .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
            
        }

        public IQueryable<FinalNews> ByOld()
        {
            using (var db = new DataBaseDataContext())
            {
                return db.FinalNews.OrderBy(x => x.Id).Skip((page - 1) * pageSize)
                                    .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
            }
        }

        public IQueryable<FinalNews> ByZA()
        {
            using (var db = new DataBaseDataContext())
            {
                return db.FinalNews.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
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

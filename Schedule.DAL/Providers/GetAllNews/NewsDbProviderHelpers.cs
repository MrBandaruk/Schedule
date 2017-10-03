using Schedule.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Providers.GetAllNews
{
    public class NewsDbProviderHelpers
    {
        NewsDbProvider dbProvider = new NewsDbProvider();



        public List<NewsDtoItem> MakeDtoList(IQueryable<FinalNews> dbItems)
        {
            List<NewsDtoItem> items = new List<NewsDtoItem>();

            foreach (var item in dbItems)
            {
                items.Add(dbProvider.MapDbToDto(item));
            }

            return items;
        }

        //count how many items get from db
        public int GetAll(int totalItems, int pageSize, int page)
        {
            int total = totalItems - ((page - 1) * pageSize);
            if (total >= pageSize)
            {
                return pageSize;
            }
            return total;
        }

        //count how many items get from db for DataTable
        public int GetAllForTable(int totalItems, int lenth, int start)
        {
            if (lenth == -1)
            {
                lenth = totalItems;
            }
            int total = totalItems - start;
            if (total >= lenth)
            {
                return lenth;
            }
            return total;
        }

        public DataTablesDtoModel MapDbToDto(List<FinalNews> dbItem, int allItems)
        {
            if (dbItem != null)
            {
                return new DataTablesDtoModel
                {
                    News = dbItem.ConvertAll(x => new NewsDtoItem() {
                        Id = x.Id,
                        ShortTitle = x.ShortTitle,
                        FullTitle = x.FullTitle,
                        ShortArticle = x.ShortArticle,
                        FullArticle = x.FullArticle }),
                    iTotalRecords = allItems
                };
            }

            return null;
        }
    }
}

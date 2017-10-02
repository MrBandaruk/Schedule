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
    }
}

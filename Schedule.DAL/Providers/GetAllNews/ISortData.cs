using Schedule.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL.Providers.GetAllNews
{
    public interface ISortData
    {
        List<NewsDtoItem> ByAZ();
        List<NewsDtoItem> ByZA();
        List<NewsDtoItem> ByOld();
        List<NewsDtoItem> ByNew();
    }
}
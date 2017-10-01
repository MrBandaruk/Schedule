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
        IQueryable<FinalNews> ByAZ();
        IQueryable<FinalNews> ByZA();
        IQueryable<FinalNews> ByOld();
        IQueryable<FinalNews> ByNew();
    }
}
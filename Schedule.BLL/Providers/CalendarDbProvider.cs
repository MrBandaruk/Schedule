using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.BLL.Model;
using Schedule.DAC.Dto;

namespace Schedule.BLL.Providers
{
    public class CalendarDbProvider
    {
        public DAC.CalendarDbProvider dbProv = new DAC.CalendarDbProvider();

        #region CRUD

        #region Create

        public void Add(CalendarModelItem dbItem)
        {
            dbProv.Add(MapDtoToDb(dbItem));
        }

        #endregion


        #region Read

        public CalendarViewModel GetAll()
        {
            return MapDtoToModel(dbProv.GetAll());
        }

        #endregion


        #region Update


        #endregion


        #region Delete


        #endregion


        #endregion


        #region Helpers

        private CalendarDtoItem MapDtoToDb(CalendarModelItem dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarDtoItem
                {
                    Id = dbItem.Id,
                    Title = dbItem.Title,
                    Additional = dbItem.Additional,
                    StartDate = dbItem.StartDate,
                    EndDate = dbItem.EndDate,
                };

            }

            return null;
        }


        private CalendarViewModel MapDtoToModel(CalendarViewDto dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarViewModel
                {
                    Events = dbItem.Events.ConvertAll(x => new CalendarViewModelItem() { id = x.id, title = x.title, start = x.start})
                };
            }

            return null;
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DAC.Dto;
using Schedule.DAC;

namespace Schedule.DAC
{
    public class CalendarDbProvider
    {
        #region CRUD

        #region Create

        public void Add(CalendarDtoItem eventItem)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = MapDtoToDb(eventItem);

                db.Events.InsertOnSubmit(item);
                db.SubmitChanges();
            }
        }

        #endregion


        #region Read

        public CalendarViewDto GetAll()
        {
            
            using (var db = new DataBaseDataContext())
            {
                var dbItems = db.Events.Select(x => x).ToList();

                return MapDbToDto(dbItems);

            }

        }


        public CalendarDtoItem GetById(int id)
        {
            using (var db = new DataBaseDataContext())
            {
                var dbItem = db.Events.Where(x => x.Id == id).FirstOrDefault();

                return MapDbToDto(dbItem);
            }
        }

        #endregion


        #region Update



        #endregion


        #region Delete

        public void Delete(int id)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = db.Events.Where(i => i.Id == id).FirstOrDefault();
                db.Events.DeleteOnSubmit(item);

                db.SubmitChanges();
            }
        }

        #endregion

        #endregion


        #region Helpers

        private Event MapDtoToDb(CalendarDtoItem dbItem)
        {
            if (dbItem != null)
            {
                return new Event
                {
                    Id = dbItem.Id,
                    Title = dbItem.Title,
                    Additional = dbItem.Additional,
                    StartDate = dbItem.StartDate,
                    EndDate = dbItem.EndDate
                };
            }

            return null;
        }


        private CalendarViewDto MapDbToDto(List<Event> dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarViewDto
                {
                    Events = dbItem.ConvertAll(x => new CalendarViewDtoItem() { id = x.Id, title = x.Title, start = x.StartDate.ToUniversalTime()})
                };
            }

            return null;
        }


        private CalendarDtoItem MapDbToDto(Event dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarDtoItem
                {
                    Id = dbItem.Id,
                    Title = dbItem.Title,
                    Additional = dbItem.Additional,
                    StartDate = dbItem.StartDate,
                    EndDate = dbItem.EndDate
                };
            }

            return null;
        }

        #endregion
    }
}
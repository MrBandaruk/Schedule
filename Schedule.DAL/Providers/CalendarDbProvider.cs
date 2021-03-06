﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.DAL.Dto;
using Schedule.DAL;

namespace Schedule.DAL
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
                db.FinalEvent.InsertOnSubmit(item);
                db.SubmitChanges();
            }
        }

        #endregion

       

        #region Read

        public CalendarViewDto GetAll()
        {
            
            using (var db = new DataBaseDataContext())
            {
                var dbItems = db.FinalEvent.Select(x => x).ToList();

                return MapDbToDto(dbItems);

            }

        }


        public CalendarDtoItem GetById(int id)
        {
            using (var db = new DataBaseDataContext())
            {
                var dbItem = db.FinalEvent.Where(x => x.Id == id).FirstOrDefault();

                return MapDbToDto(dbItem);
            }
        }

        #endregion


        #region Update

        public void Update(CalendarDtoItem eventItem)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = db.FinalEvent.Where(x => x.Id == eventItem.Id).FirstOrDefault();

                if (item != null)
                {
                    item.Title = eventItem.Title;
                    item.Additional = eventItem.Additional;
                    item.StartDate = eventItem.StartDate;
                    item.EndDate = eventItem.EndDate;
                    item.Color = eventItem.Color;

                    db.SubmitChanges();
                }
            }
        }


        public void Update(CalendarViewDtoItem eventItem)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = db.FinalEvent.Where(x => x.Id == eventItem.id).FirstOrDefault();

                if (item != null)
                {
                    item.Title = eventItem.title;                    
                    item.StartDate = eventItem.start;
                    item.EndDate = eventItem.end;
                    item.Color = eventItem.color;

                    db.SubmitChanges();
                }
            }
        }

        #endregion


        #region Delete

        public void Delete(int id)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = db.FinalEvent.Where(i => i.Id == id).FirstOrDefault();
                db.FinalEvent.DeleteOnSubmit(item);

                db.SubmitChanges();
            }
        }

        #endregion

        #endregion


        #region Helpers

        private FinalEvent MapDtoToDb(CalendarDtoItem dbItem)
        {
            if (dbItem != null)
            {
                return new FinalEvent
                {
                    Id = dbItem.Id,
                    Title = dbItem.Title,
                    Additional = dbItem.Additional,
                    StartDate = dbItem.StartDate,
                    EndDate = dbItem.EndDate,
                    Color = dbItem.Color
                };
            }

            return null;
        }


        private CalendarViewDto MapDbToDto(List<FinalEvent> dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarViewDto
                {
                    Events = dbItem.ConvertAll(x => new CalendarViewDtoItem() { id = x.Id, title = x.Title, start = x.StartDate.ToUniversalTime(), end = x.EndDate.ToUniversalTime(), color = x.Color})
                };
            }

            return null;
        }


        private CalendarDtoItem MapDbToDto(FinalEvent dbItem)
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
                    Color = dbItem.Color
                };
            }

            return null;
        }

        #endregion
    }
}

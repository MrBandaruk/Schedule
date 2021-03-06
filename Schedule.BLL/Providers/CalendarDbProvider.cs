﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.BLL.Model;
using Schedule.DAL.Dto;
using System.Globalization;

namespace Schedule.BLL.Providers
{
    public class CalendarDbProvider
    {
        public DAL.CalendarDbProvider dbProv = new DAL.CalendarDbProvider();

        #region CRUD

        #region Create

        public void Add(CalendarModelItem dbItem)
        {
            //dbItem.StartDate = DateTime.ParseExact(dbItem.StartDate.ToString("MM.dd.yyyy HH:mm:ss"), "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //dbItem.EndDate = DateTime.ParseExact(dbItem.EndDate.ToString("MM.dd.yyyy HH:mm:ss"), "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            switch (dbItem.Color)
            {
                case "Стандартный":
                    dbItem.Color = "#465775";
                    break;

                case "Зеленый":
                    dbItem.Color = "green";
                    break;

                case "Оранжевый":
                    dbItem.Color = "orange";
                    break;

                case "Красный":
                    dbItem.Color = "red";
                    break;

                default:
                    dbItem.Color = "#465775";
                    break;
            }

            dbProv.Add(MapDtoToDb(dbItem));
        }

        #endregion


        #region Read

        public CalendarViewModel GetAll()
        {
            return MapDtoToModel(dbProv.GetAll());
        }


        public CalendarModelItem GetById(int id)
        {
            return MapDtoToModelItem(dbProv.GetById(id));
        }

        #endregion


        #region Update

        public void Update(CalendarModelItem dbItem)
        {
            switch (dbItem.Color)
            {
                case "Стандартный":
                    dbItem.Color = "#465775";
                    break;

                case "Зеленый":
                    dbItem.Color = "green";
                    break;

                case "Желтый":
                    dbItem.Color = "yellow";
                    break;

                case "Красный":
                    dbItem.Color = "red";
                    break;

                default:
                    dbItem.Color = "#465775";
                    break;
            }

            dbProv.Update(MapDtoToDb(dbItem));
        }

        public void Update(CalendarViewModelItem dbItem)
        {
            dbProv.Update(MapDtoToDb(dbItem));
        }

        #endregion


        #region Delete

        public void Delete(int id)
        {
            dbProv.Delete(id);
        } 

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
                    Color = dbItem.Color
                };

            }

            return null;
        }


        private CalendarViewDtoItem MapDtoToDb(CalendarViewModelItem dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarViewDtoItem
                {
                    id = dbItem.id,
                    title = dbItem.title,
                    start = dbItem.start,
                    end = dbItem.end,
                    color = dbItem.color
                };

            }

            return null;
        }


        private CalendarModelItem MapDtoToModelItem(CalendarDtoItem dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarModelItem
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


        private CalendarViewModel MapDtoToModel(CalendarViewDto dbItem)
        {
            if (dbItem != null)
            {
                return new CalendarViewModel
                {
                    Events = dbItem.Events.ConvertAll(x => new CalendarViewModelItem() { id = x.id, title = x.title, start = x.start, end = x.end, color = x.color})
                };
            }

            return null;
        }

        #endregion
    }
}

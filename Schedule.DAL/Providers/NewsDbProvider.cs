﻿using Schedule.DAL.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Schedule.DAL.Providers.GetAllNews;

namespace Schedule.DAL
{
    public class NewsDbProvider
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region CRUD

        #region Create
        public void Add(NewsDtoItem newsItem)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = MapDtoToDb(newsItem);

                if (newsItem.NewsImages != null)
                {
                    var images = newsItem.NewsImages.Select(MapNewsImage).ToList();
                    item.FinalNewsImages.AddRange(images);
                }

                db.FinalNews.InsertOnSubmit(item);
                db.SubmitChanges();
            }
        }

        #endregion


        #region Read

        #region OldGetAll
        ////data for news Index
        //public NewsDtoModel GetAll(string sortOrder, string searchString, int pageSize, int page)
        //{
        //    List<NewsDtoItem> result = new List<NewsDtoItem>();
        //    IQueryable<FinalNews> dbItems;
        //    PageInfo pageInfo;


        //    using (var db = new DataBaseDataContext())
        //    {
        //        var totalItems = db.FinalNews.Count();
        //        if (!string.IsNullOrEmpty(searchString))
        //        {

        //            switch (sortOrder)
        //            {
        //                case "A-Z":
        //                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
        //                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
        //                            n.FullArticle.Contains(searchString)).OrderBy(x => x.ShortTitle).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                case "Z-A":
        //                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
        //                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
        //                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.ShortTitle).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                case "Old":
        //                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
        //                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
        //                            n.FullArticle.Contains(searchString)).OrderBy(x => x.Id).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                case "New":
        //                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
        //                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
        //                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                default:
        //                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
        //                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
        //                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;
        //            }

        //            pageInfo = new PageInfo
        //            {
        //                PageNumber = page,
        //                PageSize = pageSize,
        //                TotalItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
        //                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
        //                            n.FullArticle.Contains(searchString)).Count()
        //            };

        //        }
        //        else
        //        {

        //            switch (sortOrder)
        //            {
        //                case "A-Z":
        //                    dbItems = db.FinalNews.OrderBy(x => x.ShortTitle).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                case "Z-A":
        //                    dbItems = db.FinalNews.OrderByDescending(x => x.ShortTitle).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                case "Old":
        //                    dbItems = db.FinalNews.OrderBy(x => x.Id).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                case "New":
        //                    dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;

        //                default:
        //                    dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize)
        //                            .Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
        //                    break;
        //            }

        //            pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalItems };

        //        }

        //        foreach (var dbItem in dbItems.ToList())
        //        {
        //            result.Add(MapDbToDto(dbItem));
        //        }



        //        return (MapDtoItemsToModel(result, pageInfo));

        //    }

        //}

        ////data for dataTables jquery
        //public DataTablesDtoModel GetAllData(int start, int length, int sortCol, string sortType, string search)
        //{
        //    IQueryable<FinalNews> dbItems;            
        //    using (var db = new DataBaseDataContext())
        //    {
        //        var allItems = db.FinalNews.Count();

        //        if (!string.IsNullOrEmpty(search))
        //        {
        //            switch (sortType)
        //            {
        //                case "asc":
        //                    switch (sortCol)
        //                    {
        //                        case 0:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 1:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderBy(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 2:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderBy(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        default:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                    }
        //                    break;


        //                case "desc":
        //                    switch (sortCol)
        //                    {
        //                        case 0:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 1:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 2:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        default:
        //                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
        //                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
        //                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                    }
        //                    break;

        //                default:
        //                    dbItems = null;
        //                    break;
        //            }

        //            allItems = dbItems.Count();


        //        }
        //        else
        //        {
        //            switch (sortType)
        //            {
        //                case "asc":
        //                    switch (sortCol)
        //                    {
        //                        case 0:
        //                            dbItems = db.FinalNews.OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 1:
        //                            dbItems = db.FinalNews.OrderBy(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 2:
        //                            dbItems = db.FinalNews.OrderBy(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        default:
        //                            dbItems = db.FinalNews.OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;
        //                    }
        //                    break;

        //                case "desc":
        //                    switch (sortCol)
        //                    {
        //                        case 0:
        //                            dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 1:
        //                            dbItems = db.FinalNews.OrderByDescending(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        case 2:
        //                            dbItems = db.FinalNews.OrderByDescending(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;

        //                        default:
        //                            dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
        //                            break;
        //                    }
        //                    break;


        //                default:
        //                    dbItems = null;
        //                    break;
        //            }
        //        }               
        //        return MapDbToDto(dbItems.ToList(), allItems);
        //    }
        //}
        #endregion

        //get all news for News/Idex
        public NewsDtoModel GetAll(string sortOrder, string searchString, int pageSize, int page)
        {
            List<NewsDtoItem> items = new List<NewsDtoItem>();           
            PageInfo pageInfo = new PageInfo();

            if (!string.IsNullOrEmpty(searchString))
            {
                SortForIndexWithSearch sort = new SortForIndexWithSearch(searchString, sortOrder, pageSize, page);
                items = sort.Sort();
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = sort.totalItemsWithSearch };
            }
            else
            {
                SortForIndex sort = new SortForIndex(sortOrder, pageSize, page);
                items = sort.Sort();
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = sort.TotalItems };
            }


            return (MapDtoItemsToModel(items, pageInfo));
        }

        //get all news for News/Panel
        public DataTablesDtoModel GetAllData(int start, int length, int sortCol, string sortType, string search)
        {

            if (!string.IsNullOrEmpty(search))
            {
                SortForPanelWithSearch sort = new SortForPanelWithSearch(start, length, sortCol, sortType, search);
                return sort.Sort();
            }
            else
            {
                SortForPanel sort = new SortForPanel(start, length, sortCol, sortType);
                return sort.Sort();
            }

        }


        public List<NewsDtoItem> GetAll()
        {
            List<NewsDtoItem> result = new List<NewsDtoItem>();
            using (var db = new DataBaseDataContext())
            {
                var dbItems = db.FinalNews.Select(x => x).Include(n => n.FinalNewsImages).ToList();

                foreach (var dbItem in dbItems)
                {
                    result.Add(MapDbToDto(dbItem));
                }

            }

            return result;
        }

        public NewsDtoItem GetById(int id)
        {
            using (var db = new DataBaseDataContext())
            {
                var dbItem = db.FinalNews.Where(x => x.Id == id).Include(n => n.FinalNewsImages).FirstOrDefault();

                return MapDbToDto(dbItem);
            }
        }

        public NewsDtoItem GetLast()
        {
            using (var db = new DataBaseDataContext())
            {
                var lastItem = db.FinalNews.Select(x => x).Include(n => n.FinalNewsImages).OrderByDescending(x => x.Id).First();

                return MapDbToDto(lastItem);
            }
        }


        public NewsImageDto GetImageById(int id)
        {
            using (var db = new DataBaseDataContext())
            {
                var dbItem = db.FinalNewsImages.FirstOrDefault(x => x.Id == id);

                return MapDbToDto(dbItem);

            }

            
        }

        public List<NewsImageDto> GetImagesByNewsId(int id)
        {
            List<NewsImageDto> result = new List<NewsImageDto>();
            using (var db = new DataBaseDataContext())
            {

                var allItems = db.FinalNewsImages.Where(x => x.NewsId == id).ToList();
               
                foreach (var img in allItems)
                {
                    result.Add(MapDbToDto(img));
                }
               
            }

            return result;
        }


        //get 3 last news for index
        public List<NewsDtoItem> GetThreeLast()
        {
            List<NewsDtoItem> res = new List<NewsDtoItem>();

            using(var db = new DataBaseDataContext())
            {
                var items = db.FinalNews.Skip(Math.Max(0, db.FinalNews.Count() - 3)).Select(x => x).Include(n => n.FinalNewsImages).OrderByDescending(x => x.Id);

                foreach (var item in items)
                {
                    res.Add(MapDbToDto(item));
                }
            }

            return res;
        }

        #endregion


        #region Update

        public void Edit(NewsDtoItem item)
        {
            using (var db = new DataBaseDataContext())
            {
                var editignItem = db.FinalNews.Where(i => i.Id == item.Id).Include(x => x.FinalNewsImages).FirstOrDefault();

                if (editignItem != null)
                {
                    editignItem.ShortTitle = item.ShortTitle;
                    editignItem.FullTitle = item.FullTitle;
                    editignItem.ShortArticle = item.ShortArticle;
                    editignItem.FullArticle = item.FullArticle;

                    if (item.NewsImages != null)
                    {
                        
                        var images = item.NewsImages.Select(x => x).ToList();
                        foreach (var img in images)
                        {
                            editignItem.FinalNewsImages.Add(MapNewsImage(img));
                        }
                    }

                    db.SubmitChanges();
                };
            }
        }

        #endregion


        #region Delete

        public void Delete(int id)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = db.FinalNews.FirstOrDefault(i => i.Id == id);
                var image = db.FinalNewsImages.Where(i => i.NewsId == id);
                db.FinalNewsImages.DeleteAllOnSubmit(image);
                db.FinalNews.DeleteOnSubmit(item);

                db.SubmitChanges();
            }
        }

        #endregion


        #endregion


        #region Helpers


        public FinalNews MapDtoToDb(NewsDtoItem dbItem)
        {
            if (dbItem != null)
            {
                return new FinalNews
                {
                    Id = dbItem.Id,
                    ShortTitle = dbItem.ShortTitle,
                    FullTitle = dbItem.FullTitle,
                    ShortArticle = dbItem.ShortArticle,
                    FullArticle = dbItem.FullArticle
                };
            }

            return null;
        }


        private FinalNewsImages MapNewsImage(NewsImageDto x)
        {
            return new FinalNewsImages() { Id = x.Id, ImageItem = x.ImageItem, NewsId = x.NewsId };
        }


        public NewsDtoItem MapDbToDto(FinalNews dbItem)
        {
            if (dbItem != null)
            {
                return new NewsDtoItem
                {
                    Id = dbItem.Id,
                    ShortTitle = dbItem.ShortTitle,
                    FullTitle = dbItem.FullTitle,
                    ShortArticle = dbItem.ShortArticle,
                    FullArticle = dbItem.FullArticle,
                    NewsImages = dbItem.FinalNewsImages.Select(x => 
                    new NewsImageDto() {
                        Id = x.Id,
                        ImageItem = x.ImageItem.ToArray(),
                        NewsId = dbItem.Id }).ToList()
                };
            }

            return null;
        }


        private DataTablesDtoModel MapDbToDto(List<FinalNews> dbItem, int allItems)
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


        private NewsImageDto MapDbToDto(FinalNewsImages dbItem)
        {
            if (dbItem != null)
            {
                return new NewsImageDto
                {
                    Id = dbItem.Id,
                    ImageItem = dbItem.ImageItem.ToArray(),
                    NewsId = dbItem.NewsId
                };
            }

            return null;
        }

        private NewsImageDto MapDbIdToDto(FinalNewsImages dbItem)
        {
            if (dbItem != null)
            {
                return new NewsImageDto
                {
                    Id = dbItem.Id
                };
            }

            return null;
        }


        private NewsDtoModel MapDtoItemsToModel(List<NewsDtoItem> dbItems, PageInfo pageInfo)
        {
            if (dbItems != null)
            {
                return new NewsDtoModel
                {
                    News = dbItems,
                    PageInfo = pageInfo
                };
            }

            return null;
        }

        private int DTAll(int totalItems, int lenth, int start)
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

        #endregion
    }
}

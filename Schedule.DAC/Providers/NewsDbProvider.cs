using Schedule.DAC.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAC
{
    public class NewsDbProvider
    {
        #region CRUD

        #region Create

        public void Add(NewsDtoItem newsItem)
        {
            using (var db = new DataBaseDataContext())
            {
                var item = MapDtoToDb(newsItem);

                var images = newsItem.NewsImages.Select(MapNewsImage).ToList();

                db.FinalNews.InsertOnSubmit(item);
                item.FinalNewsImages.AddRange(images);
                db.SubmitChanges();
            }
        }

        #endregion


        #region Read

        
        public NewsDtoModel GetAll(string sortOrder, string searchString, int pageSize, int page)
        {
            List<NewsDtoItem> result = new List<NewsDtoItem>();
            IQueryable<FinalNew> dbItems;
            PageInfo pageInfo;
            

            using (var db = new DataBaseDataContext())
            {
                var totalItems = db.FinalNews.Count();
                if (!string.IsNullOrEmpty(searchString))
                {
                    //dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                    //n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                    //n.FullArticle.Contains(searchString)).Include(n => n.FinalNewsImages);

                    switch (sortOrder)
                    {
                        case "A-Z":
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderBy(x => x.ShortTitle).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        case "Z-A":
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.ShortTitle).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        case "Old":
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        case "New":
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        default:
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;
                    }

                    pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).Count() };

                }
                else
                {

                    switch (sortOrder)
                    {
                        case "A-Z":
                            dbItems = db.FinalNews.OrderBy(x => x.ShortTitle).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        case "Z-A":
                            dbItems = db.FinalNews.OrderByDescending(x => x.ShortTitle).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        case "Old":
                            dbItems = db.FinalNews.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        case "New":
                            dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;

                        default:
                            dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(All(totalItems, pageSize, page)).Include(n => n.FinalNewsImages);
                            break;
                    }

                    pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = totalItems };

                }

                foreach (var dbItem in dbItems.ToList())
                {
                    result.Add(MapDbToDto(dbItem));
                }



                return (MapDtoItemsToModel(result, pageInfo));

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

        
        //data for dataTables jquery
        public DataTablesDtoModel GetAllData(int start, int length, int sortCol, string sortType, string search)
        {
            IQueryable<FinalNew> dbItems;            
            using (var db = new DataBaseDataContext())
            {
                var allItems = db.FinalNews.Count();

                if (!string.IsNullOrEmpty(search))
                {
                    switch (sortType)
                    {
                        case "asc":
                            switch (sortCol)
                            {
                                case 0:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 1:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderBy(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 2:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderBy(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                default:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                            }
                            break;


                        case "desc":
                            switch (sortCol)
                            {
                                case 0:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 1:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 2:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                default:
                                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(search) ||
                                    n.FullTitle.Contains(search) || n.ShortArticle.Contains(search) ||
                                    n.FullArticle.Contains(search)).OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                            }
                            break;

                        default:
                            dbItems = null;
                            break;
                    }

                    allItems = dbItems.Count();

                    
                }
                else
                {
                    switch (sortType)
                    {
                        case "asc":
                            switch (sortCol)
                            {
                                case 0:
                                    dbItems = db.FinalNews.OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 1:
                                    dbItems = db.FinalNews.OrderBy(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 2:
                                    dbItems = db.FinalNews.OrderBy(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                default:
                                    dbItems = db.FinalNews.OrderBy(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;
                            }
                            break;

                        case "desc":
                            switch (sortCol)
                            {
                                case 0:
                                    dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 1:
                                    dbItems = db.FinalNews.OrderByDescending(x => x.FullTitle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                case 2:
                                    dbItems = db.FinalNews.OrderByDescending(x => x.FullArticle).Skip(start).Take(DTAll(allItems, length, start));
                                    break;

                                default:
                                    dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip(start).Take(DTAll(allItems, length, start));
                                    break;
                            }
                            break;


                        default:
                            dbItems = null;
                            break;
                    }
                }               
                return MapDbToDto(dbItems.ToList(), allItems);
            }
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
                var dbItem = db.FinalNewsImages.Where(x => x.Id == id).FirstOrDefault();

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

        public List<NewsImageDto> GetImagesIdByNewsId(int id)
        {
            List<NewsImageDto> result = new List<NewsImageDto>();
            using (var db = new DataBaseDataContext())
            {

                var allItems = db.FinalNewsImages.Where(x => x.NewsId == id).ToList();

                foreach (var img in allItems)
                {
                    result.Add(MapDbIdToDto(img));
                }

            }

            return result;
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
                var item = db.FinalNews.Where(i => i.Id == id).FirstOrDefault();
                var image = db.FinalNewsImages.Where(i => i.NewsId == id);
                db.FinalNewsImages.DeleteAllOnSubmit(image);
                db.FinalNews.DeleteOnSubmit(item);

                db.SubmitChanges();
            }
        }

        #endregion


        #endregion


        #region Helpers

        private FinalNew MapDtoToDb(NewsDtoItem dbItem)
        {
            if (dbItem != null)
            {
                return new FinalNew
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


        private FinalNewsImage MapNewsImage(NewsImageDto x)
        {
            return new FinalNewsImage() { Id = x.Id, ImageItem = x.ImageItem, NewsId = x.NewsId };
        }


        private NewsDtoItem MapDbToDto(FinalNew dbItem)
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
                    NewsImages = dbItem.FinalNewsImages.Select(x => new NewsImageDto() { Id = x.Id, ImageItem = x.ImageItem.ToArray(), NewsId = dbItem.Id }).ToList()
                };
            }

            return null;
        }


        private DataTablesDtoModel MapDbToDto(List<FinalNew> dbItem, int allItems)
        {
            if (dbItem != null)
            {
                return new DataTablesDtoModel
                {
                    News = dbItem.ConvertAll(x => new NewsDtoItem() { Id = x.Id, ShortTitle = x.ShortTitle, FullTitle = x.FullTitle, ShortArticle = x.ShortArticle, FullArticle = x.FullArticle }),
                    iTotalRecords = allItems
                };
            }

            return null;
        }


        private NewsImageDto MapDbToDto(FinalNewsImage dbItem)
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

        private NewsImageDto MapDbIdToDto(FinalNewsImage dbItem)
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

        private int All(int totalItems, int pageSize, int page)
        {
            int total = totalItems - ((page - 1) * pageSize);
            if (total >= pageSize)
            {
                return pageSize;
            }
            return total;
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

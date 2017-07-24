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

            using (var db = new DataBaseDataContext())
            {
                int allItems = db.FinalNews.Count();
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = allItems };

                if (!string.IsNullOrEmpty(searchString))
                {
                    dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                    n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                    n.FullArticle.Contains(searchString)).Include(n => n.FinalNewsImages);

                    switch (sortOrder)
                    {
                        case "title":
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderBy(x => x.ShortTitle).Include(n => n.FinalNewsImages);
                            break;

                        case "title_desc":
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.ShortTitle).Include(n => n.FinalNewsImages);
                            break;

                        case "id_desc":
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderBy(x => x.Id).Include(n => n.FinalNewsImages);
                            break;

                        default:
                            dbItems = db.FinalNews.Where(n => n.ShortTitle.Contains(searchString) ||
                            n.FullTitle.Contains(searchString) || n.ShortArticle.Contains(searchString) ||
                            n.FullArticle.Contains(searchString)).OrderByDescending(x => x.Id).Include(n => n.FinalNewsImages);
                            break;
                    }

                }
                else
                {

                    switch (sortOrder)
                    {
                        case "title":
                            dbItems = db.FinalNews.OrderBy(x => x.ShortTitle).Skip((page - 1) * pageSize).Take(pageSize).Include(n => n.FinalNewsImages);
                            break;

                        case "title_desc":
                            dbItems = db.FinalNews.OrderByDescending(x => x.ShortTitle).Skip((page - 1) * pageSize).Take(pageSize).Include(n => n.FinalNewsImages);
                            break;

                        case "id_desc":
                            dbItems = db.FinalNews.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(n => n.FinalNewsImages);
                            break;

                        default:
                            dbItems = db.FinalNews.OrderByDescending(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize).Include(n => n.FinalNewsImages);
                            break;
                    }
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

        #endregion


        #region Update

        public void Edit(NewsDtoItem item)
        {
            using (var db = new DataBaseDataContext())
            {
                var editignItem = db.FinalNews.Where(i => i.Id == item.Id).FirstOrDefault();
                if (editignItem != null)
                {
                    editignItem.ShortTitle = item.ShortTitle;
                    editignItem.FullTitle = item.FullTitle;
                    editignItem.ShortArticle = item.ShortArticle;
                    editignItem.FullArticle = item.FullArticle;
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

        #endregion
    }
}

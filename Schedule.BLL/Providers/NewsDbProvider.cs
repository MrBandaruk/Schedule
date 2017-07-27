using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schedule.BLL.Model;
using Schedule.DAC.Dto;

namespace Schedule.BLL.Providers
{
    public class NewsDbProvider
    {
        public DAC.NewsDbProvider dbProv = new DAC.NewsDbProvider();

        #region CRUD

        #region Create

        public void Add(NewsViewModelItem dbItem)
        {
            dbProv.Add(MapDtoToDb(dbItem));
        }

        #endregion


        #region Read

        public NewsViewModel GetAll(string sortOrder, string searchString, int pageSize, int page)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                var foundNews = dbProv.GetAll(sortOrder, searchString, pageSize, page);
                Schedule.BLL.Model.PageInfo foundPageInfo = new Model.PageInfo { PageNumber = foundNews.PageInfo.PageNumber, PageSize = foundNews.PageInfo.PageSize, TotalItems = foundNews.PageInfo.TotalItems };
                return MapViewModel(foundNews, foundPageInfo);
            }

            var dbNews = dbProv.GetAll(sortOrder, searchString, pageSize, page);
            Schedule.BLL.Model.PageInfo pageInfo = new Model.PageInfo { PageNumber = dbNews.PageInfo.PageNumber, PageSize = dbNews.PageInfo.PageSize, TotalItems = dbNews.PageInfo.TotalItems };


            return MapViewModel(dbNews, pageInfo);
        }

        public DataTablesPanelModel GetAllData(int start, int length, int sortcol, string sortType, string search)
        {
            var dbItems = dbProv.GetAllData(start, length, sortcol, sortType, search);
            return MapViewModel(dbItems);
        }

        public NewsViewModelItem GetById(int id)
        {
            var dbItem = dbProv.GetById(id);
            return MapModelItem(dbItem);
        }

        public NewsViewModelItem GetLast()
        {
            return MapModelItem(dbProv.GetLast());
        }

        public List<NewsViewModelItem> GetAll()
        {
            List<NewsViewModelItem> result = new List<NewsViewModelItem>();

            var dbItems = dbProv.GetAll();

            foreach (var dbItem in dbItems)
            {
                result.Add(MapModelItem(dbItem));
            }

            return result;
        }

        public List<NewsImageModelItem> GetImageByNewsId(int id)
        {
            List<NewsImageModelItem> result = new List<NewsImageModelItem>();

            var dbItems = dbProv.GetImagesByNewsId(id);

            foreach (var dbItem in dbItems)
            {
                result.Add(MapModelItem(dbItem));
            }

            return result;
        }

        public NewsImageModelItem GetImageById(int id)
        {
            return MapModelItem(dbProv.GetImageById(id));
        }

        #endregion


        #region Update

        public void Edit(NewsViewModelItem dbItem)
        {
            dbProv.Edit(MapDtoToDb(dbItem));
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

        private NewsDtoItem MapDtoToDb(NewsViewModelItem dbItem)
        {
            if (dbItem != null)
            {
                var result = new NewsDtoItem
                {
                    Id = dbItem.Id,
                    ShortTitle = dbItem.ShortTitle,
                    FullTitle = dbItem.FullTitle,
                    ShortArticle = dbItem.ShortArticle,
                    FullArticle = dbItem.FullArticle,
                };

                
                if (dbItem.NewsImages != null)
                {
                    result.NewsImages = dbItem.NewsImages.Select(MapNewsImageDto).ToList();
                }
                
                
                return result;
            }

            return null;
        }


        private NewsImageDto MapNewsImageDto(NewsImageModelItem x)
        {
            return new NewsImageDto() { Id = x.Id, ImageItem = x.ImageItem, NewsId = x.NewsId };
        }


        private NewsViewModel MapViewModel(NewsDtoModel dbItem, Model.PageInfo pageInfo)
        {
            List<NewsViewModelItem> convert = new List<NewsViewModelItem>();

            if (dbItem != null)
            {
                convert = dbItem.News.ConvertAll(x => new NewsViewModelItem { Id = x.Id, ShortTitle = x.ShortTitle, FullTitle = x.FullTitle, ShortArticle = x.ShortArticle, FullArticle = x.FullArticle, NewsImages = x.NewsImages.Select(i => new NewsImageModelItem { Id = i.Id, ImageItem = i.ImageItem, NewsId = i.NewsId }).ToList() });

                return new NewsViewModel
                {
                    News = convert,
                    PageInfo = pageInfo
                };
            }

            return null;
        }


        private DataTablesPanelModel MapViewModel(DataTablesDtoModel dbItem)
        {
            if (dbItem != null)
            {
                return new DataTablesPanelModel
                {
                    News = dbItem.News.ConvertAll(x => new NewsViewModelItem() { Id = x.Id, ShortTitle = x.ShortTitle, FullTitle = x.FullTitle, ShortArticle = x.ShortArticle, FullArticle = x.FullArticle}),
                    iTotalRecords = dbItem.iTotalRecords
                };
            }

            return null;
        }


        private NewsViewModelItem MapModelItem(NewsDtoItem dbItem)
        {
            if (dbItem != null)
            {
                return new NewsViewModelItem
                {
                    Id = dbItem.Id,
                    ShortTitle = dbItem.ShortTitle,
                    FullTitle = dbItem.FullTitle,
                    ShortArticle = dbItem.ShortArticle,
                    FullArticle = dbItem.FullArticle,

                    NewsImages = dbItem.NewsImages.Select(MapNewsImageDto).ToList()
                };
            }

            return null;
        }



        private NewsImageModelItem MapNewsImageDto(NewsImageDto x)
        {
            return new NewsImageModelItem() { Id = x.Id, ImageItem = x.ImageItem, NewsId = x.NewsId };
        }

        private NewsImageModelItem MapModelItem (NewsImageDto dbItem)
        {
            if (dbItem != null)
            {
                return new NewsImageModelItem
                {
                    Id = dbItem.Id,
                    ImageItem = dbItem.ImageItem,
                    NewsId = dbItem.NewsId
                };
            }

            return null;
        }

        #endregion
    }
}

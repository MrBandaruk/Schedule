using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using log4net;

namespace Schedule.Controllers
{
    public class NewsController : Controller
    {
        public BLL.Providers.NewsDbProvider newsDbProv = new BLL.Providers.NewsDbProvider();

        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            //log.Error("Test error log");
            return View();
        }


        [HttpPost]
        public ActionResult Create(BLL.Model.NewsViewModelItem item)
        {
            
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var image = Request.Files[i];

                

                if (image.ContentLength != 0)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(image.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(image.ContentLength);
                        item.NewsImages.Add(new BLL.Model.NewsImageModelItem { NewsId = item.Id, ImageItem = imageData });
                    }
                } else
                {
                    item.NewsImages = null;
                }


            }

            newsDbProv.Add(item);

            return RedirectToAction("Edit", item.Id);
        }

        #endregion


        #region Read

        public ActionResult Index(string sortOrder, string searchString, int? pageSize, int? page)
        {
            var model = GetAll(sortOrder, searchString, pageSize, page);
            return View(model);
        }


        public ActionResult Panel()
        {
            return View();
        }


        public ActionResult dataTablesData(BLL.Model.jQueryDataTableParamModel param)
        {
            var model = newsDbProv.GetAllData(param.iDisplayStart, param.iDisplayLength, param.iSortCol_0, param.sSortDir_0, param.sSearch);

            var result = model.News.ConvertAll(x => new
            {
                x.Id,
                x.FullTitle,
                x.FullArticle,
                x.ShortTitle,
                x.ShortArticle
            });

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = model.iTotalRecords,
                iTotalDisplayRecords = model.iTotalRecords,
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Article(int id)
        {
            return View(newsDbProv.GetById(id));
        }


        public ActionResult GetImagesByNewsId(int id)
        {
            var images = newsDbProv.GetImageByNewsId(id);
            return File(images.FirstOrDefault().ImageItem, "image/jpeg");
        }

        public ActionResult GetImagesIdByNewsId(int id)
        {
            var images = newsDbProv.GetImagesIdByNewsId(id);
            return Json(new { success = true, images }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetImageById(int id)
        {
            return File(newsDbProv.GetImageById(id).ImageItem, "image/jpeg");
        }

        #endregion


        #region Update

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(newsDbProv.GetLast());
            }

            return View(newsDbProv.GetById((int)id));
        }

        [HttpPost]
        public ActionResult Edit(BLL.Model.NewsViewModelItem item)
        {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var image = Request.Files[i];


                    if (image.ContentLength != 0)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(image.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(image.ContentLength);
                            item.NewsImages.Add(new BLL.Model.NewsImageModelItem { NewsId = item.Id, ImageItem = imageData });
                        }
                    } else
                    {
                        item.NewsImages = null;
                    }

                }



            newsDbProv.Edit(item);

            return RedirectToAction("Panel");
        }

        #endregion


        #region Delete
        [HttpGet]
        public ActionResult Delete(int id)
        {
            newsDbProv.Delete(id);

            return RedirectToAction("Panel");
        }

        [HttpGet]
        public ActionResult DeleteMany(List<int> id)
        {
            foreach (var i in id)
            {
                newsDbProv.Delete(i);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion




        #region Helpers

        public BLL.Model.NewsViewModel GetAll(string sortOrder, string searchString, int? pageSize, int? page)
        {
            if (String.IsNullOrEmpty(sortOrder) && ViewBag.sortParam == null)
            {
                sortOrder = ViewBag.SortOrder = "New";
            }
            else if (ViewBag.sortParam != null)
            {
                sortOrder = ViewBag.SortOrder;
            }
            else
            {
                ViewBag.SortOrder = sortOrder;
            }


            ViewBag.SearchString = String.IsNullOrEmpty(searchString) ? "" : searchString;


            if (pageSize == null)
            {
                pageSize = 3;
            }

            if (page == null)
            {
                page = 1;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                var foundNews = newsDbProv.GetAll(sortOrder, searchString, (int)pageSize, (int)page);
                return foundNews;
            }


            var news = newsDbProv.GetAll(sortOrder, searchString, (int)pageSize, (int)page);


            return news;
        }




        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Schedule.Controllers
{
    public class NewsController : Controller
    {
        public BLL.Providers.NewsDbProvider newsDbProv = new BLL.Providers.NewsDbProvider();


        #region News

       
        #region Create

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(BLL.Model.NewsViewModelItem item)
        {
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var image = Request.Files[i];


                if (image != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(image.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(image.ContentLength);
                        item.NewsImages.Add(new BLL.Model.NewsImageModelItem { NewsId = item.Id, ImageItem = imageData });
                    }
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

        /*
        public ActionResult Panel(string sortOrder, string searchString, int? pageSize, int? page)
        {
            var model = GetAll(sortOrder, searchString, pageSize, page);
            return View(model);
        }
        */

        public ActionResult Panel()
        {
            //var model = newsDbProv.GetAll();
            return View();
        }

        
        public ActionResult dataTablesData (BLL.Model.jQueryDataTableParamModel param)
        {
            var model = newsDbProv.GetAllData(param.iDisplayStart, param.iDisplayLength, param.iSortCol_0, param.sSortDir_0, param.sSearch);

            var result = model.News.ConvertAll(x => new {
                x.Id,
                x.FullTitle,
                x.FullArticle            
            });

            return Json(new {
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
            newsDbProv.Edit(item);

            return RedirectToAction("Panel");
        }

        #endregion


        #region Delete

        public ActionResult Delete(int id)
        {
            newsDbProv.Delete(id);

            return RedirectToAction("Panel");
        }

        #endregion


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

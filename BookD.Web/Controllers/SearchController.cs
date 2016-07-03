using All.Core;
using All.Repo;
using BookD.Bill;
using BookD.Core;
using Db.DbModel;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookD.Web.Controllers
{
    public class SearchController : BookControllerBase
    {
        [HttpGet]
        public ActionResult Index(string initKey, string type, string siteType)
        {
            var obj = WebRepoFactory.CreateRepo<BookUserMsg>().GetAll().ToList();
            ViewData["initKey"] = initKey != null ? initKey : "";
            ViewData["type"] = type != null ? type : "";
            ViewData["siteType"] = siteType != null ? siteType : "";
            ViewData["searchType"] = Enum.GetNames(typeof(BookSearchType));
            return View();
        }
        [HttpPost]
        public ActionResult SearchBook(string key, string type, RequestPageBase pageModel, ResourceFromSite siteType)
        {
            var service = new SearchModelManager();
            var rst = service.SearchBook(key, (BookSearchType)Enum.Parse(typeof(BookSearchType), type), pageModel.PageSize, pageModel.PageIndex, siteType);
            ViewBag.Pages = rst;
            return View("SearchPagePar");
        }

        [HttpGet]
        public ActionResult UserResourceDetail()
        {
            if (User==null)
            {
                return RedirectToAction("login", "home");
            }
            var service = new SearchModelManager();
            ViewData["books"] = service.GetRemindBook(User);
            return View();
        }
    }
}

using BookD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookD.Web
{
    public class ProductsController : BookControllerBase
    {
        [HttpGet]
        public ActionResult Products()
        {
            return View();
        }
    }
}

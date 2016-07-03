using BookD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookD.Web.Controllers
{
    public class ErrorController : BookControllerBase
    {
        [HttpGet]
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            return View();
        }
    }
}

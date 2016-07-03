using All.Core;
using All.Repo;
using Db.DbModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace BookD.Web
{
    public class UpdateController : BaseController
    {
        [HttpPost]
        public object CheckUpdateWpfClient()
        {
            var repo = WebRepoFactory.CreateRepo<UpdateInfo>();
            var obj = repo.GetAll().Where(r => r.AppName == AppName.小丁wpf客户端.ToString()).OrderByDescending(r => r.CreatedOn).FirstOrDefault();
            if (obj != null)
            {
                return new
                {
                    AppName = obj.AppName,
                    AppVersion = obj.AppVersion,
                    Desc = obj.Desc
                };
            }
            return null;
        }
        [HttpPost]
        public HttpResponseMessage DownUpdateWpfClient()
        {
            var file = new FileStream(HttpContext.Current.Server.MapPath("~/updateFile/update.zip"), FileMode.Open);
            var content = new byte[file.Length];
            file.Read(content, 0, (int)file.Length);
            file.Close();
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(content);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-zip-compressed");
            return result;
        }
    }
}

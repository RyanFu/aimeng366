﻿using All.Core;
using All.Repo;
using Db.DbModel;
using Db.EF.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Entity;

namespace BookD.Web.webapis
{
    public class MsgController : BaseController
    {
        [HttpPost]
        [TokenAuthorize]
        public SResult<List<object>, string> GetMsg()
        {
            var userContext = WebRepoFactory.CreateRepo<User>();
            var user = userContext.GetAll().FirstOrDefault(r => r.UserCode == this.Code);
            var rst = new SResult<List<object>, string>();
            if (user != null)
            {
                var msgRepo = WebRepoFactory.CreateRepo<BookUserMsg>();
                var msgs = msgRepo.GetAll().Include(r => r.Book).Include(r => r.Book.ResourceIndex).Where(r => r.UserMsg.ToUserId == user.Id && r.UserMsg.IsRead == false).OrderByDescending(r => r.UserMsg.CreatedOn).Take(100).ToList();

                if (msgs.Count() > 0)
                {
                    rst.RState = RState.OK;
                    rst.Resualt = new List<object>();
                    rst.Resualt.AddRange(msgs.Select(r => new
                    {
                        Name = r.Book.ResourceIndex.Name,
                        LastChapter = r.LastChapter,
                        LastChapterUrl = r.LastChapterUrl,
                        AuthorName = r.Book.AuthorName,
                        Description = r.Book.Description,
                        ImgUrl = r.Book.ImgUrl,
                        Url = r.Book.ResourceIndex.Url,
                        ResourceFromsite = r.Book.ResourceIndex.ResourceFromsite.ToString()
                    }));
                    rst.Resual2 = string.Join(",", msgs.Select(x => x.UserMsgId).ToArray());
                    msgRepo.Save();
                    return rst;
                }
            }

            rst.RState = RState.OK;
            return rst;
        }


        [HttpPost]
        [TokenAuthorize]
        public SResult<List<object>> SetMsg(Input<string> msgIds)
        {
            var userContext = WebRepoFactory.CreateRepo<User>();
            var user = userContext.GetAll().FirstOrDefault(r => r.UserCode == this.Code);
            var rst = new SResult<List<object>>();
            var msgRepo = WebRepoFactory.CreateRepo<BookUserMsg>();
            var mids = msgIds.InputPara.Split(',').Select(r => Convert.ToInt32(r));
            var lstMsg = msgRepo.Where(r => mids.Contains(r.UserMsgId)).ToList();
            //变更阅读状态
            foreach (var item in lstMsg)
            {
                item.UserMsg.IsRead = true;
            }
            msgRepo.Save();
            rst.RState = RState.OK;
            return rst;
        }
    }
}
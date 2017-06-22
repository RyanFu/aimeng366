using All.Core;
using All.Helper;
using Db.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using BookD.Core;
using Db.EF.DbModel;
using Db.DbModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using All.Manager;
using All.Repo;

namespace BookD.Bill
{
    /// <summary>
    /// 小说搜索管理类
    /// </summary>
    public class SearchModelManager : ManagerBase<Book>
    {
        public static HttpHelper BookHttpHelper = new HttpHelper();

        /// <summary>
        /// 搜索 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<SearchBook> SearchBook(string key, BookSearchType type, int pageSize, int pageIndex, ResourceFromSite siteType)
        {
            var bookCache = MemoryCacheProvider.Cache.Get(key + siteType + pageIndex) as List<SearchBook>;
            if (bookCache != null)
            {
                return bookCache;
            }
            var rst = new List<SearchBook>();
            switch (siteType)
            {
                case ResourceFromSite.起点:
                    var qRst = SearchQiDian(key, type, pageSize, pageIndex);
                    if (qRst != null)
                    {
                        foreach (var item in qRst)
                        {
                            var temp = new SearchBook()
                            {
                                ResourceId = item.bookid,
                                AuthorName = item.authorname,
                                BookName = item.bookname,
                                BookUrl = item.bookurl,
                                Description = item.description,
                                LastChapter = item.lastchaptername,
                                ImgUrl = item.coverurl
                            };
                            var vip = "http://vipreader.qidian.com/BookReader/vip,";
                            var putong = "http://read.qidian.com/BookReader/";

                            if (item.lastvipchapter == null || item.lastvipchapter == "0" || item.lastvipchapter == "")
                            {
                                temp.LastChapterUrl = putong + item.bookid + "," + item.lastchapter + ".aspx";
                            }
                            else
                            {
                                temp.LastChapterUrl = vip + item.bookid + "," + item.lastchapter + ".aspx";
                            }
                            MemoryCacheProvider.Cache.Add(item.bookid + ResourceFromSite.起点.ToString(), temp, DateTimeOffset.Now.AddHours(1));
                            rst.Add(temp);
                        }
                    }
                    break;
                case ResourceFromSite.十七K:
                    break;
                default:
                    break;
            }
            MemoryCacheProvider.Cache.Add(key + siteType + pageIndex, rst, DateTimeOffset.Now.AddHours(1));
            return rst;
        }
        /// <summary>
        /// 检索起点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        private List<QiDianResponseBook> SearchQiDian(string key, BookSearchType type, int pageSize, int pageIndex)
        {
            var encodeKey = System.Web.HttpUtility.HtmlEncode(key);
            var searchType = "";
            if (type == BookSearchType.书名)
            {
                searchType = "bookname";
            }
            else if (type == BookSearchType.作者)
            {
                searchType = "authorname";
            }
            var url = "http://sosu.qidian.com/ajax/search.ashx?method=Search&keyword=" + encodeKey + "&range=&ranker=&n=" + pageSize + "&start=" + pageSize * pageIndex + "&internalsiteid=&categoryid=&action_status=&authortagid=&sign_status=&pricetypeid=&rpid=10&groupbyparams=&impression=&roleinfo=&searchtype=" + searchType + "&timespan=&noec";

            string responseString = BookHttpHelper.GetHtml(url, HttpRefer.QiDian);
            return GetBooksOfJaon(responseString);
        }
        /// <summary>
        /// 从json中获取book
        /// </summary>
        /// <returns></returns>
        private List<QiDianResponseBook> GetBooksOfJaon(string json)
        {
            var temp = json.Skip(json.IndexOf("books") + 7);
            var newString = new string(temp.ToArray());
            var newTemp = newString.Take(newString.IndexOf("],") + 1);
            var rstString = new string(newTemp.ToArray());
            var rst = JsonConvert.DeserializeObject<List<QiDianResponseBook>>(rstString);
            return rst;
        }

        public ValidationResult AddRemindBook(string resourceId, ResourceFromSite site, User user)
        {
            if (site==null||user==null||resourceId==null)
            {
                return new ValidationResult("服务器错误,请刷新后重试");
            }
            using (var scope = new System.Transactions.TransactionScope())
            {
                var userRoleContext = WebRepoFactory.CreateRepo<UserResourceIndex>(Repo.dbContext);
                try
                {
                    var data = MemoryCacheProvider.Cache.Get(resourceId + site.ToString()) as SearchBook;

                    var modelcontext = WebRepoFactory.CreateRepo<ResourceIndex>(userRoleContext.dbContext);

                    var resource = modelcontext.GetAll().FirstOrDefault(r => r.ResourceId == resourceId && r.ResourceFromsite == site);

                    if (resource == null)
                    {
                        resource = new ResourceIndex()
                        {
                            Name = data.BookName,
                            ResourceFromsite = site,
                            ResourceId = resourceId,
                            ResourceType = ResourceType.小说,
                            SubscribeNum = 1,
                            Url = data.BookUrl,
                            CreatedBy = user.UserName,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = user.UserName,
                            ModifiedOn = DateTime.Now,
                        };
                        Repo.Insert(new Book()
                        {
                            AuthorName = " " + data.AuthorName,
                            ImgUrl = data.ImgUrl,
                            ResourceIndex = resource,
                            Description = data.Description.Trim(),
                            LastChapter = data.LastChapter,
                            LastChapterUrl = data.LastChapterUrl
                        });
                        userRoleContext.Insert(new UserResourceIndex()
                        {
                            ResourceIndex = resource,
                            UserId = user.Id
                        });
                    }
                    else
                    {
                        if (userRoleContext.GetAll().Any(r => r.ResourceIndexId == resource.Id && r.UserId == user.Id))
                        {
                            return new ValidationResult("用户已经订阅个信息");
                        }
                        resource.SubscribeNum += 1;
                        userRoleContext.Insert(new UserResourceIndex()
                        {
                            ResourceIndex = resource,
                            UserId = user.Id
                        });
                    }
                    userRoleContext.Save();
                    scope.Complete();
                }
                catch (Exception e)
                {
                    LogHelper.error(e.Message);
                    return new ValidationResult(e.Message);
                }
            }
            return null;
        }

        public List<Book> GetRemindBook(User user)
        {
            var urContext = WebRepoFactory.CreateRepo<UserResourceIndex>(Repo.dbContext);
            var ids = urContext.Where(r => r.UserId == user.Id).Select(r => r.ResourceIndex.Id);
            var books = Repo.Where(r => ids.Contains(r.ResourceIndexId)).Include(r => r.ResourceIndex);
            return books.ToList();
        }
    }
}

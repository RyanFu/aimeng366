using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace All.Helper
{
    public class HttpHelper
    {
        public String userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322; .NET4.0C; .NET4.0E)";
        public String accept = "*/*";
        public String contentType = "application/x-www-form-urlencoded; charset=UTF-8";
        public Dictionary<string, string> Cookies = new Dictionary<string, string>();

        /// <summary>
        /// 请求http，获得响应
        /// </summary>
        /// <param name="url">http地址</param>
        /// <param name="data">要发送的数据</param>
        /// <returns></returns>
        public HttpWebResponse GetResponse(string url, string refer, byte[] data = null, int timeout = 30000)
        {
            Uri ui = new Uri(url);
            var domain = ui.Host;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.UserAgent = userAgent;
            request.Accept = accept;
            request.ContentType = contentType;
            request.Referer = refer;
            CookieContainer cookieContainer = new CookieContainer();
            foreach (var item in Cookies)
            {
                var cookie = new Cookie();
                cookie.Path = "/";
                cookie.Expires = DateTime.MaxValue;
                cookie.Name = item.Key;
                cookie.Value = item.Value;
                cookie.Domain = domain;
                cookieContainer.Add(cookie);
            }

            request.CookieContainer = cookieContainer;
            request.ProtocolVersion = HttpVersion.Version11;
            if (data != null)
            {
                request.Method = "POST";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                request.Method = "GET";
            }
            request.Timeout = timeout;
            var rst = request.Headers.ToString();
            return (HttpWebResponse)request.GetResponse();
        }
        /// <summary>
        /// 从响应获得字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetHtml(string url, string refer, byte[] data = null, int timeout = 30000)
        {
            try
            {
                using (var response = GetResponse(url, refer, data, timeout))
                {
                    ProcessCookies(response.Cookies);

                    if (response.Headers.Get("Location") != null)
                    {
                        return GetHtml(response.Headers.Get("Location"), refer, data, timeout);
                    }
                    else if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (var sr = new StreamReader(stream, Encoding.UTF8))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public string GetHtml(HttpWebResponse response)
        {
            ProcessCookies(response.Cookies);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 从响应获得图像
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Image GetImage(string url, string refer)
        {
            using (var response = GetResponse(url, refer))
            {
                ProcessCookies(response.Cookies);

                using (var stream = response.GetResponseStream())
                {
                    return Image.FromStream(stream);
                }
            }
        }


        /// <summary>
        /// 异步请求http，自己判断是否需要获取响应
        /// </summary>
        /// <param name="url">http地址</param>
        /// <param name="data">要发送的数据</param>
        /// <returns></returns>
        public Task<WebResponse> GetResponseAsync(string url, string refer, byte[] data = null, int timeout = 30000)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
            {
                return GetResponse(url, refer, data, timeout);
            });
        }
        /// <summary>
        /// 添加Cookies
        /// </summary>
        /// <param name="cookies"></param>
        public void AddCookies(Dictionary<string, string> cookies)
        {
            if (cookies != null)
            {
                foreach (var item in cookies)
                {
                    var cookie = new Cookie();
                    cookie.Path = "/";
                    cookie.Expires = DateTime.MaxValue;
                    cookie.Name = item.Key.Trim();
                    cookie.Value = item.Value.Trim();
                    if (Cookies.ContainsKey(cookie.Name))
                    {
                        if (cookie.Value.Trim() != null || cookie.Value != "")
                        {
                            Cookies[cookie.Name] = cookie.Value;
                        }
                    }
                    else
                    {
                        Cookies.Add(cookie.Name, cookie.Value);
                    }
                }
            }
        }
        /// <summary>
        /// 获取指定键的cookies值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetCookie(string key)
        {
            if (!Cookies.ContainsKey(key))
            {
                return string.Empty;
            }
            return Cookies[key];
        }
        /// <summary>
        /// 处理响应的cookies
        /// </summary>
        /// <param name="cookies"></param>
        private void ProcessCookies(CookieCollection _cookies)
        {
            foreach (Cookie cookie in _cookies)
            {
                if (Cookies.ContainsKey(cookie.Name))
                {
                    if (cookie.Value.Trim() != null || cookie.Value != "")
                    {
                        Cookies[cookie.Name] = cookie.Value;
                    }
                }
                else
                {
                    Cookies.Add(cookie.Name, cookie.Value);
                }
            }
        }


    }
}

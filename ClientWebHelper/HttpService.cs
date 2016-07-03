using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using All.ConfigHelper;
using All.Core;

namespace ClientWebHelper
{
    public class HttpService
    {
        HttpClient _client;
        
        private static string UserCode;
        private static string Pwd;

        public HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new HttpClient(new SecurityMsgHandler());
                    _client.MaxResponseContentBufferSize = int.MaxValue;
                }
                return _client;
            }
        }

        /// <summary>
        /// 登录系统获取访问口令（Token）
        /// </summary>
        /// <returns></returns>
        public async Task<string> Login(string userCode, string pwd)
        {
            if (string.IsNullOrEmpty(userCode)||string.IsNullOrEmpty(pwd))
            {
                return null;
            }
            UserCode = userCode;
            Pwd = pwd;
            var loginInput = new Input<string, string>();
            loginInput.InputPara = userCode;
            loginInput.InputPara2 = pwd;

            Client.Timeout = TimeSpan.FromSeconds(30);
            //每次请求的时候再构建URL，确保URL是最新的
            var url = ConfigHelper.GetStringConfig(AllConfigKeys.ServiceUrl);
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            Client.BaseAddress = new Uri(url + "api/");

            var inputJson = JsonConvert.SerializeObject(loginInput);
            var content = new StringContent(inputJson);
            //必须请求JSON格式
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var requestMsg = new HttpRequestMessage(HttpMethod.Post, "authorize/gtoken");
            requestMsg.Content = content;
            HttpResponseMessage responseMsg;
            try
            {
                responseMsg = Client.SendAsync(requestMsg).GetAwaiter().GetResult();
                responseMsg.EnsureSuccessStatusCode();
                var retJson = await responseMsg.Content.ReadAsStringAsync();
                var rst = JsonConvert.DeserializeObject<SResult>(retJson);
                if (rst.RState == RState.VersionOld)
                {
                    //写入最新版本信息(此处版本信息写在Error属性中)
                    ConfigHelper.SetStringConfig(AllConfigKeys.UserVersion, rst.Value);
                    //跳转到升级页面

                }
                if (rst.RState != RState.OK)
                {
                    return string.IsNullOrEmpty(rst.Value) ? "登录失败" : rst.Value;
                }
                ConfigHelper.SetStringConfig(AllConfigKeys.UserCode, userCode);
                //将登录成功后的Token写入配置
                ConfigHelper.SetStringConfig(AllConfigKeys.UserToken, rst.Value);
                return null;
            }
            catch (System.Net.WebException ex)
            {
                if ((ex.Status == System.Net.WebExceptionStatus.ConnectFailure || ex.Status == System.Net.WebExceptionStatus.Timeout))
                {
                    //网络未连接或远程服务器已关闭或者连接超时，尝试更新一下新的URL地址，不成功就算了

                }

                string msg = WebExceptionMsg(ex.Status);
                return msg;
            }
            catch (System.Threading.Tasks.TaskCanceledException ex)
            {
                string msg = "连接超时或已取消";
                return msg;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return msg;
            }
        }
        /// <summary>
        /// 使用缓存中的密码，重登录
        /// </summary>
        /// <returns></returns>
        public Task<string> ReLogin()
        {
            return Login(UserCode, Pwd);
        }
        /// <summary>
        /// 调用远程API（要求验证的API），(注意返回值可能为空)
        /// </summary>
        /// <typeparam name="T1">输入参数类型</typeparam>
        /// <typeparam name="TResualt">输出参数类型</typeparam>
        /// <param name="api">API接口地址，从DroidApi常量中取</param>
        /// <param name="input">输入参数</param>
        /// <param name="timeOutSeconds">执行超时时间默认为5分钟，单位为秒</param>
        /// <returns></returns>
        public Task<TResualt> NutriFunc<T1, TResualt>(string api, T1 input, int timeOutSeconds = 30, bool dealError = true)
            where T1 : class
            where TResualt : SResult, new()
        {
            return NutriFunc<TResualt>(api, input, timeOutSeconds, dealError);
        }

        /// <summary>
        /// 调用远程API（要求验证的API）(注意返回值可能为空)
        /// </summary>
        /// <typeparam name="TResualt"></typeparam>
        /// <param name="api"></param>
        /// <param name="input">输入参数，可以为空或者是一个对象（不能是简单类型）</param>
        /// <param name="timeOutSeconds"></param>
        /// <returns></returns>
        public async Task<TResualt> NutriFunc<TResualt>(string api, object input, int timeOutSeconds = 30, bool dealError = true)
            where TResualt : SResult, new()
        {
            //是否在用户验证失败减肥药已重提交
            bool reTryed = false;


            Client.Timeout = TimeSpan.FromSeconds(timeOutSeconds);
            //每次请求的时候再构建URL，确保URL是最新的
            var url = ConfigHelper.GetStringConfig(AllConfigKeys.ServiceUrl);
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            Client.BaseAddress = new Uri(url + "api/");

        Start:
            HttpContent content = null;
            if (input != null)
            {
                var inputJson = JsonConvert.SerializeObject(input);
                content = new StringContent(inputJson);
                //必须请求JSON格式
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            }
            var requestMsg = new HttpRequestMessage(HttpMethod.Post, api);
            requestMsg.Content = content;
            HttpResponseMessage responseMsg;
            try
            {
                responseMsg = Client.SendAsync(requestMsg).GetAwaiter().GetResult();
                if (responseMsg.StatusCode == System.Net.HttpStatusCode.Unauthorized && !reTryed)
                {
                    //重新获取Token
                    //如果获取成功重新请求数据，否则提示错误并返回null;
                    var loginRst = await ReLogin();
                    if (string.IsNullOrEmpty(loginRst))
                    {
                        reTryed = true;
                        goto Start;
                    }
                    else
                    {
                        AutoDealError(loginRst, false);
                        return null;
                    }
                }
                responseMsg.EnsureSuccessStatusCode();
                var retJson = await responseMsg.Content.ReadAsStringAsync();
                var rst = JsonConvert.DeserializeObject<TResualt>(retJson);
                if (rst==null)
                {
                    return null;
                }
                if (rst.RState == RState.OK)
                {
                    //只在OK时返回数据，否则都返回null
                    return rst;
                }
                if (rst.RState == RState.Error)
                {
                    if (dealError)
                    {
                        AutoDealError(rst.Value + ",请重试!", false);
                        return null;
                    }
                    else
                    {
                        return rst;
                    }

                }
                if (rst.RState == RState.VersionOld)
                {
                    //写入最新版本信息(此处版本信息写在Error属性中)
                    //ConfigHelper.SetStringConfig(ConfigHelper.Version, rst.RState);
                    //跳转到升级页面

                }
                else if (rst.RState == RState.PasswordError)
                {
                    //不清空本地密码

                    //跳转到登录页面


                }
                else if (rst.RState == RState.LockDevice)
                {
                    //锁死设备
                }
                return null;
            }
            catch (System.Net.WebException ex)
            {

                if (ex.Status == System.Net.WebExceptionStatus.ConnectFailure || ex.Status == System.Net.WebExceptionStatus.Timeout)
                {
                    //网络未连接或远程服务器已关闭或者连接超时，尝试更新一下新的URL地址，不成功就算了


                }

                string msg = WebExceptionMsg(ex.Status);
                if (dealError)
                {
                    AutoDealError(msg + ",请重试!", false);
                    return null;
                }
                else
                {
                    var resualt = new TResualt();
                    return resualt;
                }
            }
            catch (System.Threading.Tasks.TaskCanceledException ex)
            {
                string msg = "连接超时或已取消";
                if (dealError)
                {
                    AutoDealError(msg, false);
                    return null;
                }
                else
                {
                    var resualt = new TResualt();
                    resualt.Value = msg;
                    return resualt;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (dealError)
                {
                    AutoDealError(ex.GetType().FullName + ":" + ex.Message, false);
                    return null;
                }
                else
                {
                    var resualt = new TResualt();

                    return resualt;
                }
            }
        }


        /// <summary>
        /// 自动处理错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="showLong"></param>
        public static void AutoDealError(string msg, bool showLong)
        {

        }
        public static string WebExceptionMsg(System.Net.WebExceptionStatus status)
        {
            string msg = "";
            switch (status)
            {
                case System.Net.WebExceptionStatus.ConnectFailure:
                    msg = "网络未连接或远程服务器已关闭";
                    break;
                case System.Net.WebExceptionStatus.ConnectionClosed:
                    msg = "网络连接已关闭";
                    break;
                case System.Net.WebExceptionStatus.MessageLengthLimitExceeded:
                    msg = "数据量超出限制";
                    break;
                case System.Net.WebExceptionStatus.ReceiveFailure:
                    msg = "网络接收错误";
                    break;
                case System.Net.WebExceptionStatus.SendFailure:
                    msg = "网络发送错误";
                    break;
                case System.Net.WebExceptionStatus.Timeout:
                    msg = "网络超时";
                    break;
                default:
                    msg = "未知网络错误";
                    break;
            }
            return msg;
        }

        /// <summary>
        /// 下载任意网络地址图片，不缓存
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<byte[]> DownWebPic(Uri url)
        {
            System.Net.WebClient webClient = new System.Net.WebClient();

            try
            {
                var webImage = webClient.DownloadDataTaskAsync(url);
                return webImage;
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// 取消正在进行中的调用
        /// </summary>
        public void Cancel()
        {
            try
            {
                Client.CancelPendingRequests();
            }
            catch (Exception ex)
            {

            }

        }
    }
    /// <summary>
    /// 用于访问监管平台API
    /// </summary>
    public class SecurityMsgHandler : HttpClientHandler
    {
        public SecurityMsgHandler()
        {
            //默认支持压缩，如果连接出现问题请改回这个设置
            this.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //测试期间暂时用DeviceId，替代DeviceNo;
            request.Headers.Add("UserCode", ConfigHelper.GetStringConfig(AllConfigKeys.UserCode));

            request.Headers.Add("UserToken", ConfigHelper.GetStringConfig(AllConfigKeys.UserToken));
            //当前版本号
            request.Headers.Add("UserVersion", ConfigHelper.GetStringConfig(AllConfigKeys.UserVersion));
            //客户端类型
            request.Headers.Add("UserClientType", ((int)AllConfigKeys.UserClientType).ToString());

            return base.SendAsync(request, cancellationToken);
        }
    }
}

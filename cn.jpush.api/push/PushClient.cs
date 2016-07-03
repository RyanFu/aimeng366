using cn.jpush.api.common;
using cn.jpush.api.util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json;

namespace cn.jpush.api.push
{
    internal class PushClient : BaseHttpClient
    {
        //private const String HOST_NAME_SSL = "https://api.jpush.cn";
        private const String HOST_NAME = "https://api.jpush.cn";
        private const String PUSH_PATH = "/v3/push";

        private String appKey;
        private String masterSecret;
        private bool enableSSL = false;
        private long timeToLive;
        private bool apnsProduction = false;
        private HashSet<DeviceEnum> devices = new HashSet<DeviceEnum>();

        public PushClient(String masterSecret, String appKey, long timeToLive, HashSet<DeviceEnum> devices, bool apnsProduction)
        {
            this.appKey = appKey;
            this.masterSecret = masterSecret;
            this.timeToLive = timeToLive;
            this.devices = devices;
        }

        public MessageResult sendNotification(String notificationContent, NotificationParams notParams, Dictionary<String, Object> extras)
        {
            if (extras != null)
            {
                notParams.NotyfyMsgContent.n_extras = extras;
            }
            notParams.NotyfyMsgContent.n_content = notificationContent;

            return sendMessage(notParams, MsgTypeEnum.NOTIFICATIFY);
        }

        public MessageResult sendCustomMessage(String msgTitle, String msgContent, CustomMessageParams cParams, Dictionary<String, Object> extras)
        {
            if (msgTitle != null)
            {
                cParams.CustomMsgContent.title = msgTitle;
            }
            if (extras != null)
            {
                cParams.CustomMsgContent.extras = extras;
            }
            cParams.CustomMsgContent.message = msgContent;

            return sendMessage(cParams, MsgTypeEnum.COUSTOM_MESSAGE);
        }


        private MessageResult sendMessage(MessageParams msgParams, MsgTypeEnum msgType)
        {
            msgParams.ApnsProduction = this.apnsProduction ? 1 : 0;
            msgParams.AppKey = this.appKey;
            msgParams.MasterSecret = this.masterSecret;

            if (msgParams.TimeToLive == MessageParams.NO_TIME_TO_LIVE)
            {
                msgParams.TimeToLive = this.timeToLive;
            }
            if (this.devices != null)
            {
                foreach (DeviceEnum device in this.devices)
                {
                    msgParams.addPlatform(device);
                }
            }
            return sendPush(msgParams, msgType);
        }

        private MessageResult sendPush(MessageParams msgParams, MsgTypeEnum msgType)
        {
            //String url = enableSSL ? HOST_NAME_SSL : HOST_NAME;
            String url = HOST_NAME;
            url += PUSH_PATH;
            String pamrams = prase(msgParams, msgType);
            //Console.WriteLine("begin post"); 
            //String auth = Base64.getBase64Encode(this.appKey + ":" + this.masterSecret);
            String auth = Convert.ToBase64String(new ASCIIEncoding().GetBytes(this.appKey + ":" + this.masterSecret));
            ResponseResult result = sendPost(url, auth, pamrams);
            //Console.WriteLine("end post");

            MessageResult messResult = new MessageResult();
            if (result.responseCode == System.Net.HttpStatusCode.OK)
            {
                //Console.WriteLine("responseContent===" + result.responseContent);
                messResult = (MessageResult)JsonTool.JsonToObject(result.responseContent, messResult);
                String content = result.responseContent;
            }
            messResult.ResponseResult = result;

            return messResult;

        }

        private String prase(MessageParams message, MsgTypeEnum msgType)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            StringBuilder sb = new StringBuilder();
            message.setMsgContent();
            //start 自定义参数
            Dictionary<String, Object> innerExtras = new Dictionary<String, Object>();
            innerExtras.Add("time_to_live", 60);
            String innerString = JsonTool.DictionaryToJson(innerExtras);
            //end 自定义参数
            string platformValue = "\"all\"";
            if (message.ReceiverType == ReceiverTypeEnum.ALIAS)
            {
                platformValue = "{\"alias\" : [\"" + message.ReceiverValue + "\"]}";
            }
            else if (message.ReceiverType == ReceiverTypeEnum.TAG)
            {
                platformValue = "{\"tag\" : [\"" + message.ReceiverValue + "\"]}";
            }

            sb.Append("{\"platform\": \"all\","); //推送平台设置

            sb.Append("\"audience\" :" + platformValue + ",");//推送设备指定
            if (msgType == MsgTypeEnum.NOTIFICATIFY) //通知
            { 
                sb.Append("\"notification\" : {\"alert\" : \"" +  message.MsgContent+ "\",\"android\" : {},\"ios\" : {        \"extras\" : { \"newsid\" : 321}}},");//通知内容体。
            }
            else if (msgType == MsgTypeEnum.COUSTOM_MESSAGE)//消息
            {
                sb.Append("\"message\" : {");
                if (!string.IsNullOrEmpty(message.Title))
                {
                    sb.Append("\"title\" : \"" + message.Title.Trim() + "\",");
                }
                sb.Append("\"msg_content\" : \"" + message.MsgContent.Trim() + "\"},\"extras\" : { \"ToUser\" : \"小董\", \"ToUser\" : \"小戴俊\"},");  // 消息内容体 HttpUtility.UrlEncode(message.MsgContent.Trim() , Encoding.GetEncoding("utf-8"))
            }
            if (!string.IsNullOrEmpty(innerString))
                sb.Append(" \"options\" :" + innerString + "}"); 

            Debug.Print(sb.ToString()); 
            return sb.ToString();
            //return HttpUtility.UrlEncode(sb.ToString(), Encoding.GetEncoding("utf-8"));
        }

    }

    enum MsgTypeEnum
    {
        NOTIFICATIFY = 1,
        COUSTOM_MESSAGE = 2
    }
}

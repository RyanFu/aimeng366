using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace All.Core
{
    /// <summary>
    /// 邮件辅助类
    /// </summary>
    public class EMailHelper
    {
        /// <summary>
        /// C#发送邮件函数
        /// </summary>
        /// <param name="sto">接受者邮箱</param>
        /// <param name="stoer">收件人</param>
        /// <param name="sSubject">主题</param>
        /// <param name="sBody">内容</param>
        /// <param name="sfile">附件</param>

        /// <returns></returns>
        public bool Sendmail(string sto, string stoer, string sSubject, string sBody, string sfile)
        {
            ////设置from和to地址
            MailAddress from = new MailAddress(SysSet.OfficialQQEmailAddress, "艾梦366官方邮件");
            MailAddress to = new MailAddress(sto, stoer);

            ////创建一个MailMessage对象
            MailMessage oMail = new MailMessage(from, to);

            //// 添加附件
            if (sfile != "")
            {
                oMail.Attachments.Add(new Attachment(sfile));
            }
            ////邮件标题
            oMail.Subject = sSubject;
            ////邮件内容
            oMail.Body = sBody;
            ////邮件格式
            oMail.IsBodyHtml = false;
            ////邮件采用的编码
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            ////设置邮件的优先级为高
            oMail.Priority = MailPriority.High;
            ////发送邮件
            SmtpClient client = new SmtpClient();
            ////client.UseDefaultCredentials = false; 
            client.Host = "smtp.qq.com";
            client.Credentials = new NetworkCredential(SysSet.OfficialQQ, SysSet.OfficialQQPwd);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(oMail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ////释放资源
                oMail.Dispose();
            }
        }
    }
}

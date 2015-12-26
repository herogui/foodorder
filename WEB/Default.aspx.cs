using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Text;
using System.Web.Services;
public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    [WebMethod]
    public static bool SendMail(string  user,string content)
    {
        bool isOk = true;
        try
            {                
                string senderServerIp = "smtp.126.com";

                string fromMailAddress = "herogui@126.com";//duliufang888@126.com
                string toMailAddress = "505536350@qq.com";
                string subjectInfo = "Test sending e_mail";
                string bodyInfo = "Hello Eric, This is my first testing e_mail";
                string mailUsername = "herogui";
                string mailPassword = "618314guibao"; //发送邮箱的密码（）
                string mailPort = "25";   

                CSendMail email = new CSendMail(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
                ////email.AddAttachments(attachPath);
                email.Send();

           //MailSender mail = new MailSender(toMailAddress,fromMailAddress,"kdkdkddk","dkdkdkdk","herogui

              
            }
            catch (Exception ex)
            {
                isOk = false;
            }
        return isOk;
    }

    /// <summary> 

    /// 发送邮件程序 

    /// </summary> 

    /// <param name="from">发送人邮件地址</param> 

    /// <param name="fromname">发送人显示名称</param> 

    /// <param name="to">发送给谁（邮件地址）</param> 
      
    /// <param name="subject">标题</param> 

    /// <param name="body">内容</param> 

    /// <param name="username">邮件登录名</param> 

    /// <param name="password">邮件密码</param> 

    /// <param name="server">邮件服务器</param> 

    /// <param name="fujian">附件</param> 

    /// <returns>send ok</returns> 

    /// 调用方法 SendMail("abc@126.com", "某某人", "cba@126.com", "你好", "我测试下邮件", "邮箱登录名", "邮箱密码", "smtp.126.com", ""); 

    private string SendMail(string from, string fromname, string to, string subject, string body, string username, string password, string server, string fujian)
    {

        try
        {

            //邮件发送类 

            MailMessage mail = new MailMessage();

            //是谁发送的邮件 

            mail.From = new MailAddress(from, fromname);

            //发送给谁 

            mail.To.Add(to);

            //标题 

            mail.Subject = subject;

            //内容编码 

            mail.BodyEncoding = Encoding.Default;

            //发送优先级 

            mail.Priority = MailPriority.High;

            //邮件内容 

            mail.Body = body;

            //是否HTML形式发送 

            mail.IsBodyHtml = true;

            //附件 

            if (fujian.Length > 0)
            {

                mail.Attachments.Add(new Attachment(fujian));

            }

            //邮件服务器和端口 

            SmtpClient smtp = new SmtpClient(server, 25);

            smtp.UseDefaultCredentials = true;

            //指定发送方式 

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            //指定登录名和密码 

            smtp.Credentials = new System.Net.NetworkCredential(username, password);

            //超时时间 

            smtp.Timeout = 10000;

            smtp.Send(mail);

            return "send ok";

        }

        catch (Exception exp)
        {

            return exp.Message;

        }

    }

    class MailSender
    {
        private readonly MailMessage mail;
        private readonly string password;//发件人密码 
        private readonly string user;

        /// <summary>  
        /// 处审核后类的实例  
        /// </summary>  
        /// <param name="to">收件人地址</param>  
        /// <param name="from">发件人地址</param>  
        /// <param name="body">邮件正文</param>  
        /// <param name="subject">邮件的主题</param>  
        /// <param name="sendAccount">发件人账号</param>
        /// <param name="sendPassword">发件人密码</param>  
        public MailSender(string to, string from, string body, string subject, string sendAccount, string sendPassword)
        {
            mail = new MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Priority = MailPriority.Normal;
            this.password = sendPassword;
            this.user = sendAccount;
        }
        /// <summary>  
        /// 添加附件  
        /// </summary>  
        public void Attachments(string path)
        {
            string[] pathes = path.Split(',');
            for (int i = 0; i < pathes.Length; i++)
            {
                var data = new Attachment(pathes[i], MediaTypeNames.Application.Octet);//实例化附件  
                var disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(pathes[i]);//获取附件的创建日期  
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(pathes[i]);//获取附件的修改日期  
                disposition.ReadDate = System.IO.File.GetLastAccessTime(pathes[i]);//获取附件的读取日期  
                mail.Attachments.Add(data);//添加到附件中  
            }
        }
        /// <summary>  
        /// 异步发送邮件  
        /// </summary>  
        /// <param name="CompletedMethod"></param>  
        public void SendAsync(SendCompletedEventHandler CompletedMethod)
        {
            if (mail != null)
            {
                var smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(user, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mail.From.Host;
                smtpClient.SendCompleted += CompletedMethod;//注册异步发送邮件完成时的事件  
                smtpClient.SendAsync(mail, mail.Body);
            }
        }
        /// <summary>  
        /// 发送邮件  
        /// </summary>  
        public void Send()
        {
            if (mail != null)
            {
                var smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(user, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mail.From.Host;
                smtpClient.Send(mail);
            }
        }
    }

    public class CSendMail
    {
        private MailMessage mMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private SmtpClient mSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
        private int mSenderPort;   //发送邮件所用的端口号（htmp协议默认为25）
        private string mSenderServerHost;    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
        private string mSenderPassword;    //发件箱的密码
        private string mSenderUsername;   //发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        private bool mEnableSsl;    //是否对邮件内容进行socket层加密传输
        private bool mEnablePwdAuthentication;  //是否对发件人邮箱进行密码验证

        ///<summary>
        /// 构造函数
        ///</summary>
        ///<param name="server">发件箱的邮件服务器地址</param>
        ///<param name="toMail">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        ///<param name="fromMail">发件人地址</param>
        ///<param name="subject">邮件标题</param>
        ///<param name="emailBody">邮件内容（可以以html格式进行设计）</param>
        ///<param name="username">发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）</param>
        ///<param name="password">发件人邮箱密码</param>
        ///<param name="port">发送邮件所用的端口号（htmp协议默认为25）</param>
        ///<param name="sslEnable">true表示对邮件内容进行socket层加密传输，false表示不加密</param>
        ///<param name="pwdCheckEnable">true表示对发件人邮箱进行密码验证，false表示不对发件人邮箱进行密码验证</param>
        public CSendMail(string server, string toMail, string fromMail, string subject, string emailBody, string username, string password, string port, bool sslEnable, bool pwdCheckEnable)
        {
            try
            {
                mMailMessage = new MailMessage();
                mMailMessage.To.Add(toMail);
                mMailMessage.From = new MailAddress(fromMail);
                mMailMessage.Subject = subject;
                mMailMessage.Body = emailBody;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mMailMessage.Priority = MailPriority.Normal;
                this.mSenderServerHost = server;
                this.mSenderUsername = username;
                this.mSenderPassword = password;
                this.mSenderPort = Convert.ToInt32(port);
                this.mEnableSsl = sslEnable;
                this.mEnablePwdAuthentication = pwdCheckEnable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mMailMessage.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 邮件的发送
        ///</summary>
        public void Send()
        {
            try
            {
                if (mMailMessage != null)
                {
                    mSmtpClient = new SmtpClient();
                    //mSmtpClient.Host = "smtp." + mMailMessage.From.Host;
                    mSmtpClient.Host = this.mSenderServerHost;
                    mSmtpClient.Port = this.mSenderPort;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.EnableSsl = this.mEnableSsl;
                    if (this.mEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //NTLM: Secure Password Authentication in Microsoft Outlook Express
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");
                    }
                    else
                    {
                        mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                    }
                    mSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);
                 }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
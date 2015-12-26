
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
using DAL;

public partial class Fendian : System.Web.UI.Page
{
    public string shop = "";
    public string user = "";

    /// <summary>
    /// 限定的时间范围
    /// </summary>
    public string limitTime = "";

    public string fudingTme1 = System.Configuration.ConfigurationSettings.AppSettings["fudingTme1"];
    public string fudingTme2 = System.Configuration.ConfigurationSettings.AppSettings["fudingTme2"];

    public string liufangTme1 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme1"];
    public string liufangTme2 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme2"];
    public string liufangTme3 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme3"];
    public string liufangTme4 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme4"];  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["user"] != null)
            {
                user = Request.QueryString["user"].ToString();
            }
            else//微信用户
            {
               // user = getUser(tel);  根据手机号获取用户名称
            }

            if (Request.QueryString["shop"] != null)
            {
                shop = Request.QueryString["shop"].ToString();
                if (shop.Equals("福鼎")) limitTime = fudingTme1 + "-" + fudingTme2;
                else if (shop.Equals("杜六房")) limitTime = liufangTme1 + "-" + liufangTme2 + "或" + liufangTme3 + "-" + liufangTme4;
            }
            else//根据微信用户获取店铺
            {
                // shop = getShop(user);  根据手机号获取用户名称
            }
        }
    }

    [WebMethod]
    public static bool SendMail(string shop, string user, string content)
    {
        bool isOk = false;
        try
        {
            string senderServerIp = "smtp.126.com";

            string fromMailAddress = "herogui@126.com";
            string toMailAddress = "505536350@qq.com";
            string subjectInfo = "分店:" + shop+"_发送人:" + user + "_" + DateTime.Now.ToLongDateString();

            string title = " <div>日期：" + DateTime.Now.ToLongDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;分店:" + shop + "</div>";            
            string bodyInfo = title+content;//正文
            string mailUsername = "herogui";
            string mailPassword = "618314guibao"; //发送邮箱的密
            string mailPort = "25";

            CSendMail email = new CSendMail(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
            email.Send();
            isOk = true;
        }
        catch (Exception ex)
        {
            isOk = false;
        }
        return isOk;
    }

 
}

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
using System.Data;

public partial class SendOrderDuliu : System.Web.UI.Page
{
    public string shop = "";
    public string user = "";
    public string myuserid = "";
    public static string userId = "";

    /// <summary>
    /// 限定的时间范围
    /// </summary>
    public string limitTime = "";

    public string liufangTme1 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme1"];
    public string liufangTme2 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme2"];
    public string liufangTme3 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme3"];
    public string liufangTme4 = System.Configuration.ConfigurationSettings.AppSettings["liufangTme4"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            if (Request.QueryString["userId"] != null)
            {
                userId = Request.QueryString["userId"].ToString();
                myuserid = userId;
            }


            if (Request.QueryString["user"] != null)
            {
                user = Request.QueryString["user"].ToString();
            }
            else//微信用户  根据手机号获取用户名称
            {
                string weixingId = "QQW122"; //这里改微信用户的ID为
                DbHelp db = new DbHelp();
                DataSet ds = db.Query("select * from tb_user where weixingId='" + weixingId + "'");
                if (null != ds && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        user = dt.Rows[0]["username"].ToString();// 用户名称
                        shop = dt.Rows[0]["shop"].ToString();// 店名     
                    }
                }
            }

            if (Request.QueryString["shop"] != null)
            {
                shop = Request.QueryString["shop"].ToString();
            }

           limitTime = liufangTme1 + "-" + liufangTme2 + "或" + liufangTme3 + "-" + liufangTme4;
        }
    }

    [WebMethod]
    public static string getDishes()
    {
        string str = "";
        DbHelp db = new DbHelp();
        DataSet ds = db.Query("select * from tb_dishesDuliu  where IsAction = '1' ");
        if (null != ds && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            int num = 0;
            foreach (DataRow dr in dt.Rows)
            {
                str += dr["Name"].ToString() + "@" + dr["Unit"].ToString();
                if (num < dt.Rows.Count - 1) str += ",";
            }
        }


        return str;
    }

    /// <summary>
    /// 保存预定
    /// </summary>
    /// <param name="shop"></param>
    /// <param name="user"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool SaveOrder(string shop, string user, string content)
    {
        bool isOk = false;

        Dictionary<string, string> dataMap = new Dictionary<string, string>();
        dataMap.Add("id", Guid.NewGuid().ToString());
        dataMap.Add("UserId", userId);
        dataMap.Add("OrderDate", DateTime.Now.ToString());
        string[] strs = content.Split(';');
        foreach (string kv in strs)
        {
            string[] strs2 = kv.Split('@');
            string name = strs2[0];
            if (name.Length < 1) continue;

            string num = strs2[1];
            dataMap.Add(PinYinConverter.Get(name), num);
        }
        DbHelp db = new DbHelp();
        db.InsertData("tb_orderDuliu", dataMap);

        return isOk;
    }

    /// <summary>
    /// 发邮件
    /// </summary>
    /// <param name="shop"></param>
    /// <param name="user"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool SendMail(string shop, string user, string content)
    {
        bool isOk = false;
        try
        {
            //string senderServerIp = "smtp.126.com";

            string fromMailAddress = System.Configuration.ConfigurationSettings.AppSettings["fromMail"];

            string subjectInfo = "分店:" + shop + "_" + user + "_杜六房订菜" + DateTime.Now.ToLongDateString();

            string title = " <div>" + DateTime.Now.ToLongDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + shop +"_" + user + "_杜六房订菜</div>";
            string bodyInfo = title + content;//正文
            string mailUsername = System.Configuration.ConfigurationSettings.AppSettings["username"];
            string mailPassword = System.Configuration.ConfigurationSettings.AppSettings["password"];//发送邮箱的密
            string mailPort = "25";

            //获取接收的邮箱
            DbHelp db = new DbHelp();
            DataSet ds = db.Query("select Email from tb_email  ");
            if (null != ds && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    string toMailAddress = dr["Email"].ToString();
                    CSendMail email = new CSendMail("", toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
                    email.Send();
                }
            }

            isOk = true;
        }
        catch (Exception ex)
        {
            isOk = false;
        }
        return isOk;
    }
}
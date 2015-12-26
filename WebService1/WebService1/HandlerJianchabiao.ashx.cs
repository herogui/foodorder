using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlHelp;

namespace WebService1
{
    /// <summary>
    /// HandlerJianchabiao 的摘要说明
    /// </summary>
    public class HandlerJianchabiao : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string checkDate = context.Request.QueryString["checkDate"].ToString();
            string factoryID = context.Request.QueryString["factoryID"].ToString();
            string userID = context.Request.QueryString["userID"].ToString();
            string zenrenID = context.Request.QueryString["zenrenID"].ToString();
            string jianchajilu = context.Request.QueryString["jianchajilu"].ToString();
            string cunzaiwenti = context.Request.QueryString["cunzaiwenti"].ToString();
            string fangkui = context.Request.QueryString["fangkui"].ToString();

            Dictionary<string, string> dataMap = new Dictionary<string, string>();
            dataMap.Add("guid", Guid.NewGuid().ToString());
            dataMap.Add("checkDate", checkDate);
            dataMap.Add("factoryID", factoryID);
            dataMap.Add("userID", userID);
            dataMap.Add("zenrenID", zenrenID);
            dataMap.Add("jianchajilu", jianchajilu);
            dataMap.Add("cunzaiwenti", cunzaiwenti);
            dataMap.Add("fangkui", fangkui);

            DbHelp db = new DbHelp();

            context.Response.Write(db.InsertData("[sde].[sde].sdjianchabiao", dataMap).ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
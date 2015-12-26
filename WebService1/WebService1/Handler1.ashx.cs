using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService1
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Service1 server = new Service1();
            string ischeck = context.Request.QueryString["ischeck"].ToString();
            string userid = context.Request.QueryString["userid"].ToString();
            string factoryId = context.Request.QueryString["factoryId"].ToString();
           
            context.Response.Write(server.InsertTask(ischeck, userid, factoryId));

           
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService1
{
    /// <summary>
    /// Handler2 的摘要说明
    /// </summary>
    public class Handler2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/plain";
            Service1 server = new Service1();
            string list = context.Request.QueryString["list"].ToString();
            context.Response.Write(server.InsertTaskList(list));
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService1
{
    /// <summary>
    /// HandlerDel 的摘要说明
    /// </summary>
    public class HandlerDel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            Service1 server = new Service1();
           
            string userid = context.Request.QueryString["userid"].ToString();
            server.deleteTaskByUser(userid);
            context.Response.Write("Hello World");
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
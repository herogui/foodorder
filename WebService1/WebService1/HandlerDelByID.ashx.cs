using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService1
{
    /// <summary>
    /// HandlerDelByID 的摘要说明
    /// </summary>
    public class HandlerDelByID : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.QueryString["id"].ToString();
            Service1 server = new Service1();
             int num =  server.deleteTask(id);
             context.Response.Write(num.ToString());
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
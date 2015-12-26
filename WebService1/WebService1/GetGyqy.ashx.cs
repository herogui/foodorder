using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlHelp;
using System.Data;

namespace WebService1
{
    /// <summary>
    /// GetGyqy 的摘要说明
    /// </summary>
    public class GetGyqy : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userid = context.Request.QueryString["userid"].ToString();
            DbHelp db = new DbHelp();
            DataTable dt = db.GetDataTable("select * from [sde].[sde].sdtask where userid = '" + userid + "'");
            string str = "";
            if (null == dt || dt.Rows.Count < 1)
                context.Response.Write("");
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    str += dr["userid"].ToString()+",";
                    str += dr["factoryId"].ToString() + ",";
                    str += dr["ischeck"].ToString() + ";";
                }
                context.Response.Write(str);
            }
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
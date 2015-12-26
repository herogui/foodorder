using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqlHelp;
using System.Data;

namespace WebService1
{
    /// <summary>
    /// HandlerExist 的摘要说明
    /// </summary>
    public class HandlerExist : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
          
            DbHelp db = new DbHelp();
            DataTable dt1 = db.GetDataTable("select a1,a3 from sde.sde.SDFACTORY");
            DataTable dt2 = db.GetDataTable("SELECT factoryId  FROM [sde].[sde].[sdtask]");

            List<string> listAllId = new List<string>();
            List<string> listAllName = new List<string>(); 
            foreach (DataRow dr in dt1.Rows)
            {
                listAllId.Add(dr["a1"].ToString().Replace(".000000",""));
                listAllName.Add(dr["a3"].ToString());
            }

            List<string> listAdd = new List<string>();
            foreach (DataRow dr in dt2.Rows)
            {
                listAdd.Add(dr["factoryId"].ToString());
            }

            for (int i = listAllId.Count - 1; i > -1; i--)
            {
                if (isExist(listAdd, listAllId[i]))
                {
                    listAllId.RemoveAt(i);
                    listAllName.RemoveAt(i);
                }
            }

            Random rd = new Random();
            int num = rd.Next(listAllId.Count-1);

            string str = "";
            str += listAllId[num] + "," + listAllName[num] + "," + listAllId.Count + "," + listAllName.Count + "," + listAdd.Count;
          

            context.Response.Write(str);
        }

        bool isExist(List<string> listAdd,string fid)
        {
            bool ise = false;
            foreach (string kv in listAdd)
            {
                if (fid.Contains(kv))
                {                    
                    ise = true;
                    break;
                }
            }
            return ise;
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
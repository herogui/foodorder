using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using SqlHelp;

namespace WebService1
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string InsertTask(string ischeck, string userid, string factoryId)
        {
            Dictionary<string, string> dataMap = new Dictionary<string, string>();
            dataMap.Add("id", Guid.NewGuid().ToString());
            dataMap.Add("ischeck", ischeck);
            dataMap.Add("userid", userid);
            dataMap.Add("factoryId", factoryId);
            DbHelp db = new DbHelp();
            

            return db.InsertData("[sde].[sde].sdtask", dataMap).ToString();
        }

        [WebMethod]
        public string InsertTaskList(string sqllist)
        {
            string[] strs = sqllist.Split('@');
            foreach (string kv in strs)
            {
                try
                {
                    string[] strs2 = kv.Split('$');
                    InsertTask(strs2[0], strs2[1], strs2[2]);
                }
                catch (Exception e)
                { 
                }
            }

            return "";
        }

        [WebMethod]
        public string UpdateTask(string id, string ischeck, string userid, string factoryId)
        {
            Dictionary<string, string> dataMap = new Dictionary<string, string>();
            dataMap.Add("id", Guid.NewGuid().ToString());
            if(ischeck.Length>0)
            dataMap.Add("ischeck", ischeck);
            if (userid.Length > 0)
            dataMap.Add("userid", userid);
            if (factoryId.Length > 0)
            dataMap.Add("factoryId", factoryId);
            DbHelp db = new DbHelp();

            return db.UpdateDataWkt("[sde].[sde].sdtask", dataMap,"where id ='"+id+"'").ToString();
        }

        [WebMethod]
        public int deleteTask(string factoryId)
        {
            DbHelp db = new DbHelp();

            return db.ExecuteNonQuery("delete [sde].[sde].sdtask where factoryId = '" + factoryId + "'");
        }

        [WebMethod]
        public int deleteTaskByUser(string userid)
        {
            DbHelp db = new DbHelp();
            return db.ExecuteNonQuery("delete [sde].[sde].sdtask where userid = '" + userid + "'");
        }
    }
}
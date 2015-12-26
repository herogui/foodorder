using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DbHelp
    {
        string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["dbcfg"].ConnectionString;//可配置

        public DbHelp()
        {
           
        }      

        /// <summary>
        /// 插入一条语句
        /// </summary>
        /// <param name="tabName">表名</param>
        /// <param name="dataMap">数据(列名，值)</param>
        /// <returns></returns>
        public int InsertData(string tabName, Dictionary<string, string> dataMap)
        {

            int num = 0;

            StringBuilder strBld = new StringBuilder();

            try
            {
                strBld.Append("insert into  ");
                strBld.Append(tabName);
                strBld.Append("    (");

                foreach (string kv in dataMap.Keys)
                {
                    strBld.Append(kv);
                    strBld.Append(",");
                }
                strBld.Remove(strBld.Length - 1, 1);
                strBld.Append(")");

                strBld.Append("   values   ");

                strBld.Append("    (");
                foreach (string kv in dataMap.Keys)
                {
                    strBld.Append("'");
                    strBld.Append(dataMap[kv]);
                    strBld.Append("'");
                    strBld.Append(",");
                }
                strBld.Remove(strBld.Length - 1, 1);
                strBld.Append(")");

                num = ExecuteNonQuery(strBld.ToString());

                
            }
            catch (Exception exp)
            {
                
            }
            finally
            {
                strBld = null;
            }

            return num;
        }

        /// <summary>
        /// 更新一条语句
        /// </summary>
        /// <param name="tabName">表名</param>
        /// <param name="dataMap">数据(列名，值)</param>
        /// <returns></returns>
        public  bool UpdateDataWkt(string tabName, Dictionary<string, string> dataMap,string where)
        {
            bool isOk = false;

            StringBuilder strBld = new StringBuilder();

            try
            {
                strBld.Append("update   ");
                strBld.Append(tabName);
                strBld.Append("   set  ");

                int num0 = 0;
                foreach (string kv in dataMap.Keys)
                {
                    strBld.Append(kv);
                    strBld.Append(" = ");
                    if (dataMap[kv].IndexOf("STGeomFromText") < 0)
                    {
                        strBld.Append("'");
                    }
                    strBld.Append(dataMap[kv]);
                    if (dataMap[kv].IndexOf("STGeomFromText") < 0)
                    {
                        strBld.Append("'");
                    }
                    if (num0 < dataMap.Count - 1)
                    strBld.Append(",");

                    num0++;
                }

                strBld.Append("  " + where);
               

                int num = ExecuteNonQuery(strBld.ToString());

                if (num > 0) isOk = true;
            }
            catch (Exception exp)
            {

            }
            finally
            {
                strBld = null;
            }

            return isOk;
        }
          
        /// <summary>
        /// 插入一条语句
        /// </summary>
        /// <param name="tabName">表名</param>
        /// <param name="dataMap">数据(列名，值)</param>
        /// <returns></returns>
        public  bool InsertDataWkt(string tabName, Dictionary<string, string> dataMap)
        {
            bool isOk = false;

            StringBuilder strBld = new StringBuilder();

            try
            {
                strBld.Append("insert into  ");
                strBld.Append(tabName);
                strBld.Append("    (");

                foreach (string kv in dataMap.Keys)
                {
                    strBld.Append(kv);
                    strBld.Append(",");
                }
                strBld.Remove(strBld.Length - 1, 1);
                strBld.Append(")");

                strBld.Append("   values   ");

                strBld.Append("    (");
                foreach (string kv in dataMap.Keys)
                {
                    if (dataMap[kv].IndexOf("STGeomFromText") < 0)
                    {
                        strBld.Append("'");
                    }
                    strBld.Append(dataMap[kv]);
                    if (dataMap[kv].IndexOf("STGeomFromText") < 0)
                    {
                        strBld.Append("'");
                    }
                    strBld.Append(",");
                }
                strBld.Remove(strBld.Length - 1, 1);
                strBld.Append(")");

                int num = ExecuteNonQuery(strBld.ToString());

                if (num > 0) isOk = true;
            }
            catch (Exception exp)
            {

            }
            finally
            {
                strBld = null;
            }

            return isOk;
        }



        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public  DataSet Query(string SQLString)
        {          
             using (SqlConnection conn = new SqlConnection(connStr))
            {
                DataSet ds = new DataSet();
                try
                {
                    conn.Open();
                    SqlDataAdapter command = new SqlDataAdapter(SQLString, conn);
                    command.Fill(ds, "ds");
                }
                catch (SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                return ds;
            }
        }

        public  int ExecuteNonQuery(string cmdText)
        {
           SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = cmdText;
                int val = cmd.ExecuteNonQuery();
                conn.Close();
                return val;
            }
        }

        public  void UpdateDs(DataTable dt, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter command = new SqlDataAdapter(strSql, conn);
                    SqlCommandBuilder cmd = new SqlCommandBuilder(command);
                    command.Update(dt);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }

        }

        public DataTable GetDataTable(string sql)
        {
            DataSet ds = this.Query(sql);
            if (null != ds)
            {
                return ds.Tables[0];
            }
            else return null;
        }

        public  void InsertDs(DataTable dt, string strSql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(strSql, conn);
                    SqlCommandBuilder commBuilder = new SqlCommandBuilder(adapter);

                    adapter.InsertCommand = commBuilder.GetInsertCommand();

                    adapter.Update(dt);
                    dt.AcceptChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }
}

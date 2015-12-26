using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using System.Reflection;
using System.Data;
using System.Collections;

namespace DAL
{
    public class SQLHelpe
    {
        private static SqlConnection con;
        static SQLHelpe helpe;

        string constr = System.Configuration.ConfigurationManager.ConnectionStrings["dbcfg"].ConnectionString;//可配置
        public static SQLHelpe IniHelpe()
        {
            if (helpe == null)
            {
                helpe = new SQLHelpe();
            }
            return helpe;
        }

        private SQLHelpe()
        {
            con = new SqlConnection(constr);
        }
        public IList<T> Query<T>(string sql)
        {
            IList<T> list = new List<T>();
            using (con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                Type t = typeof(T);
                PropertyInfo[] ps = t.GetProperties();
                string[] tpty = new string[ps.Length];
                for (int i = 0; i < ps.Length; i++)
                {
                    tpty[i] = ps[i].Name;
                }
                string[] rety = null;
                string[] fields = null;
                bool read = false;
                string shouzimu;
                string tempName;
                while (reader.Read())
                {
                    //获取相交的属性名称
                    if (!read)
                    {
                        read = true;
                        rety = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            tempName = reader.GetName(i);
                            shouzimu = tempName[0].ToString();
                            shouzimu = shouzimu.ToUpper();
                            tempName = tempName.Remove(0, 1);
                            tempName = tempName.Insert(0, shouzimu);
                            rety[i] = tempName;
                            //rety[i] = reader.GetName(i);
                        }
                        fields = tpty.Intersect(rety).ToArray();
                        //重新获取对象和所查结果的公共属性,这样处理不是很合理.对对象的约束减少了.
                        List<PropertyInfo> templist = new List<PropertyInfo>();
                        for (int i = 0; i < fields.Length; i++)
                        {
                            templist.Add(t.GetProperty(fields[i]));
                        }
                        ps = templist.ToArray<PropertyInfo>();


                    }
                    T m = (T)Activator.CreateInstance(t);
                    foreach (PropertyInfo p in ps)
                    {
                        if (!(reader[p.Name]  is System.DBNull))
                        {
                            p.SetValue(m, reader[p.Name], null);
                        }
                        
                    }

                    //T m = (T)Activator.CreateInstance(t);
                    //foreach (PropertyInfo p in ps)
                    //{
                    //    Object[] myAttributes = p.GetCustomAttributes(true);
                    //    if (myAttributes.Length > 0)
                    //    {
                    //        Annotation a = myAttributes[0] as Annotation;
                    //        if (a.IsField)
                    //        {
                    //            p.SetValue(m, reader[p.Name], null);

                    //        }
                    //    }
                    //    else
                    //    {

                    //        p.SetValue(m, reader[p.Name], null);

                    //    }


                    list.Add(m);
                }
                reader.Close();
            }


            return list;
        }
        private void OpenCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        private void CloseCon()
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }


        public int ExecSQL(string sql)
        {
            int i = 0;

            using (con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                i = command.ExecuteNonQuery();

            }


            return i;
        }

        public DataTable getTableBySql(string sql)
        {

            using (con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sql, con);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }

            return null;

        }


    }
}

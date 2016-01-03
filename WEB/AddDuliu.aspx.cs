using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using DAL;
using System.Text;

public partial class AddDuliu : System.Web.UI.Page
{
    /// <summary>
    /// 数据编辑的主键id
    /// </summary>
    public string strID = "";

    /// <summary>
    /// 用户id  
    /// </summary>
    public string currentUserID = "";
   
    /// <summary>
    /// 浏览权限
    /// </summary>
    public bool canLookFlag = true;
    /// <summary>
    /// 标题
    /// </summary>
    public string OpertionTitle = "添加";

    /// <summary>
    /// 页面加载事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {      

        if (Request.QueryString["id"] != null)
        {
            strID = Request.QueryString["id"].ToString();
        }
        
        if (strID.Equals(""))
        {
            //canLookFlag = OConfig.CheckUserHaveFunctionRight(currentUserID, "020102", "1");
        }
        else
        {
            OpertionTitle = "编辑";
            if (strID.Length == 36)
            {
                //canLookFlag = OConfig.CheckUserHaveFunctionRight(currentUserID, "020102", "2");
            }
        }

        if (!IsPostBack)
        {
            if (!strID.Equals(""))
            {
                SetFormInfo();
            }
        }

    }

    /// <summary>
    /// 页面编辑状态从数据库读取记录加载在页面上
    /// </summary>
    public void SetFormInfo()
    {
        string sql = "select * from tb_dishesDuliu where id = '" + strID + "'";
        DbHelp db = new DbHelp();
        DataSet ds = db.Query(sql);
        if (null != ds && ds.Tables.Count > 0)
        {
            DataTable dt= ds.Tables[0];
            this.txtCode.Text = dt.Rows[0]["Code"].ToString();
            this.txtProductCode.Text = dt.Rows[0]["ProductCode"].ToString();
            this.txtName.Text = dt.Rows[0]["Name"].ToString();
            this.txtUnit.Text = dt.Rows[0]["Unit"].ToString();
            this.txtproducer.Text = dt.Rows[0]["producer"].ToString();
        }
    }

    /// <summary>
    /// 点击保存或者添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Code = this.txtCode.Text.Trim();
        string ProductCode = this.txtProductCode.Text.Trim();
        string Name = this.txtName.Text.Trim();
        string Unit = this.txtUnit.Text.Trim();
        string producer = this.txtproducer.Text.Trim();

        DbHelp db = new DbHelp();

        Dictionary<string, string> dataMap = new Dictionary<string, string>();
        dataMap.Add("Code", Code);
        dataMap.Add("ProductCode", ProductCode);
        dataMap.Add("Name", Name);
        dataMap.Add("Unit", Unit);
        dataMap.Add("producer", producer);

        bool isok = false;
        if (strID.Length < 1)//添加
        {
            //重复的不添加
            DataSet  ds = db.Query("select * from tb_dishesDuliu where name ='" + Name + "' and IsAction = 1");
            if (null!=ds&&ds.Tables[0].Rows.Count>0)
            {
                string script = "";
                script += "<script language='javascript'>";
                script += "alert('菜品已经存在,不能添加!');";            
                script += "</script>";
                Page.RegisterStartupScript("", script);
                return;
            }
            else
            {
                //没有激活就激活
                DataSet ds2 = db.Query("select * from tb_dishesDuliu where name ='" + Name + "' and IsAction = 0");
                if (null != ds2 && ds2.Tables[0].Rows.Count > 0)
                {
                    string id = ds2.Tables[0].Rows[0]["id"].ToString();
                    dataMap.Add("IsAction", "1");
                    isok = db.UpdateDataWkt("tb_dishesDuliu", dataMap, "where id ='" + id + "'");
                }
                else
                {
                    string ColName = PinYinConverter.Get(Name);
                    dataMap.Add("id", Guid.NewGuid().ToString());
                    dataMap.Add("IsAction", "1");
                    dataMap.Add("ColName", ColName);
                    int num = db.InsertData("tb_dishesDuliu", dataMap);                   

                    if (num > 0)
                    {
                        isok = true;
                        //添加列到数据表中
                        StringBuilder strbld = new StringBuilder();
                        strbld.Append("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tb_orderDuliu]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)");
                        strbld.Append("if (NOT exists ( select * from dbo.syscolumns where name = '" + ColName + "' and id in ");
                        strbld.Append("(select id from dbo.sysobjects where id = object_id(N'[dbo].[tb_orderDuliu]') and OBJECTPROPERTY(id, N'IsUserTable') = 1))");
                        strbld.Append(") ");
                        strbld.Append("ALTER TABLE tb_orderDuliu ADD " + ColName + " int NULL");
                        string sql = strbld.ToString();
                        int num2 = db.ExecuteNonQuery(sql);
                    }
                }
            }
        }
        else
        {
            //编辑
            isok = db.UpdateDataWkt("tb_dishesDuliu", dataMap, "where id ='" + strID + "'");
        }



        if (isok)
        {
            string script = "";
            script += "<script language='javascript'>";
            script += "alert('数据保存成功!');";
            script += "  opener.__doPostBack('" + "btnRefresh" + "','');";
            script += "  this.close();";
            script += "</script>";
            Page.RegisterStartupScript("RefreshSourceWindowAndCloseMe", script);
        }
        else
        {
            string script = "";
            script += "<script language='javascript'>";
            script += "alert('数据保存失败!');";
            script += "  opener.__doPostBack('" + "btnRefresh" + "','');";
            script += "  this.close();";
            script += "</script>";
            Page.RegisterStartupScript("RefreshSourceWindowAndCloseMe", script);
        }
    }
}


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

public partial class AddShop : System.Web.UI.Page
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
        string sql = "select * from tb_shop where id = '" + strID + "'";
        DbHelp db = new DbHelp();
        DataSet ds = db.Query(sql);
        if (null != ds && ds.Tables.Count > 0)
        {
            DataTable dt= ds.Tables[0];
            this.txtCode.Text = dt.Rows[0]["Code"].ToString();
            this.txtShopName.Text = dt.Rows[0]["ShopName"].ToString();
            this.txtAdrress.Text = dt.Rows[0]["Adrress"].ToString();
            this.txttel.Text = dt.Rows[0]["tel"].ToString();
            this.txtLeader.Text = dt.Rows[0]["Leader"].ToString();
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
        string ShopName = this.txtShopName.Text.Trim();
        string Adrress = this.txtAdrress.Text.Trim();
        string tel = this.txttel.Text.Trim();
        string Leader = this.txtLeader.Text.Trim();

     

        Dictionary<string, string> dataMap = new Dictionary<string, string>();
        dataMap.Add("Code", Code);
        dataMap.Add("ShopName", ShopName);
        dataMap.Add("Adrress", Adrress);
        dataMap.Add("tel", tel);
        dataMap.Add("Leader", Leader);

        DbHelp db = new DbHelp();
        bool isok = false;
        if (strID.Length < 1)
        {
            dataMap.Add("id", Guid.NewGuid().ToString());
            int num = db.InsertData("tb_shop", dataMap);
            if (num > 0)
            {
                isok = true;
            }
        }
        else
        {
            isok = db.UpdateDataWkt("tb_shop", dataMap, "where id ='" + strID + "'");
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

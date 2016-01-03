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

public partial class AddUser : System.Web.UI.Page
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

            //
            DbHelp db = new DbHelp();
            DataTable dt = db.GetDataTable("select * from tb_shop");
            this.txtshop.DataSource = dt;
            this.txtshop.DataValueField = "id";
            this.txtshop.DataTextField = "ShopName";
            this.txtshop.DataBind();
        }
    }

    /// <summary>
    /// 页面编辑状态从数据库读取记录加载在页面上
    /// </summary>
    public void SetFormInfo()
    {
        string sql = "select * from tb_user where id = '" + strID + "'";
        DbHelp db = new DbHelp();
        DataSet ds = db.Query(sql);
        if (null != ds && ds.Tables.Count > 0)
        {
            DataTable dt= ds.Tables[0];          
            this.txtuserName.Text = dt.Rows[0]["userName"].ToString();
             this.txtpwd.Text = dt.Rows[0]["pwd"].ToString();
            this.txtshop.Text = dt.Rows[0]["shop"].ToString();
            this.txttel.Text = dt.Rows[0]["tel"].ToString();
            this.txtweixingId.Text = dt.Rows[0]["weixingId"].ToString();
        }
    }

    protected void shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        //setDataSource(txtshop.SelectedValue.ToString());
    }

    /// <summary>
    /// 点击保存或者添加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {      
        string userName = this.txtuserName.Text.Trim();
        string shop = this.txtshop.SelectedValue.Trim();
        string tel = this.txttel.Text.Trim();
        string weixingId = this.txtweixingId.Text.Trim();
        string pwd = this.txtpwd.Text.Trim();

        Dictionary<string, string> dataMap = new Dictionary<string, string>();        
        dataMap.Add("userName", userName);
        dataMap.Add("shop", shop);
        dataMap.Add("pwd", pwd);
        dataMap.Add("tel", tel);
        dataMap.Add("weixingId", weixingId);

        DbHelp db = new DbHelp();
        bool isok = false;
        if (strID.Length < 1)
        {
            dataMap.Add("id", Guid.NewGuid().ToString());
            int num = db.InsertData("tb_user", dataMap);
            if (num > 0)
            {
                isok = true;
            }
        }
        else
        {
            isok = db.UpdateDataWkt("tb_user", dataMap, "where id ='" + strID + "'");
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

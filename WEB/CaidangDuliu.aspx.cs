
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Text;

public partial class CaidangDuliu : System.Web.UI.Page
{
  
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            setDataSource();
        }
    }   

    void setDataSource()
    {
        setDataSource("where IsAction = '1'");
    }
    
    void setDataSource(string condition)
    {
        DbHelp db = new DbHelp();
        DataSet ds = db.Query("select * from tb_dishesDuliu  " + condition);
        if (null != ds && ds.Tables.Count > 0)
        {
            gvInfo.DataSource = ds.Tables[0].DefaultView;
            gvInfo.DataBind();
        }
    }

    /// <summary>
    /// 行绑定事件
    /// </summary>   
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgbtn = (ImageButton)e.Row.FindControl("btnDelete");
            Label lbl = (Label)e.Row.FindControl("lblID");

            //获取该行主键
            string str = "您确认要删除序号为【" + lbl.Text + "】的信息吗";
            imgbtn.Attributes.Add("OnClick", "return confirm('" + str + "');");
        }
    }

    /// <summary>
    /// 删除事件
    /// </summary>   
    protected void gvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DbHelp db = new DbHelp();
        string strID = gvInfo.DataKeys[e.RowIndex].Value.ToString();//获取主键列的值       
       
        //
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("IsAction", "0");
        bool isSuc = db.UpdateDataWkt("tb_dishesDuliu", data, " where id = '" + strID + "'");

        if (isSuc)
        {
            string script = "";
            script += "<script language='javascript'>";
            script += "alert('数据删除成功!');";
            script += "  opener.__doPostBack('" + this + "','');";
            script += "  this.close();";
            script += "</script>";
            Page.RegisterStartupScript("RefreshSourceWindowAndCloseMe", script);
            setDataSource();
        }
    }

    /// <summary>
    /// 刷新
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        string condition = "where   IsAction = '1' ";
        if (!this.txtKeyWord.Text.Trim().Equals("请输入关键字"))
        {
            if (this.txtKeyWord.Text.Trim().Length > 0)
            {
                condition += " and (Code like '%" + this.txtKeyWord.Text.Trim() + "%'" + "  or  ProductCode like '%" + this.txtKeyWord.Text.Trim() + "%'" + "  or  producer like '%" + this.txtKeyWord.Text.Trim() + "%'" + "  or  Name like '%" + this.txtKeyWord.Text.Trim() + "%')";
            }
        }
       
        setDataSource(condition);
    }
}
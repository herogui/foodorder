

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DAL;
using System.Data;

public partial class _Default : System.Web.UI.Page
{

    
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btLogin_Click(object sender, EventArgs e)
    {        
        DbHelp db = new DbHelp();
        DataSet ds = db.Query("SELECT a.id,userName,b.shopname,a.tel,pwd FROM tb_user a,tb_shop b where a.shop = b.id and username='" + this.txtUser.Text.Trim() + "' and  pwd = '" + this.TextPwd.Text.Trim() + "'");
        if (null != ds && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                string shop = dt.Rows[0]["shopname"].ToString();// 店名      
                Session["userId"] = dt.Rows[0]["id"].ToString();
                Response.Redirect("Fendian.aspx?user=" + this.txtUser.Text.Trim() + "&shop=" + shop + "");
            }
            {
                string script = "";
                script += "<script language='javascript'>";
                script += "alert(用户名或密码错误!');";
                script += "</script>";
                Page.RegisterStartupScript("", script);
            }
        }
        else
        {
            string script = "";
            script += "<script language='javascript'>";
            script += "alert('用户名或密码错误!');";
            script += "</script>";
            Page.RegisterStartupScript("", script);
        }
      
    }

}
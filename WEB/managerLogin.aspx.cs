using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using DAL;
using System.Data;

public partial class managerLogin : System.Web.UI.Page
{

    
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void btLogin_Click(object sender, EventArgs e)
    {
        //管理员登录密码写死
        if (this.txtUser.Text.Trim()=="admin"&&this.TextPwd.Text.Trim()=="admin")
        {
            Response.Redirect("Manager.aspx");           
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
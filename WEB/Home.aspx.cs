using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static bool Submit(string user, string pwd)
    {         
        //用户名和密码暂时写死
        if (user == "admin" && pwd == "admin")
            return true;
        else return false;
    }
}
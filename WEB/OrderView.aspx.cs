using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Text;

public partial class OrderView : System.Web.UI.Page
{
    //注意  这里的开始时间是 两个分店的下午汇总的最小时间
    //注意  这里的结束时间是 两个分店的下午汇总的最大时间
string strDate1 = DateTime.Now.ToShortDateString() + "  " 
    + System.Configuration.ConfigurationSettings.AppSettings["fudingTme1"];
string strDate2 = DateTime.Now.ToShortDateString() + "  "
    + System.Configuration.ConfigurationSettings.AppSettings["liufangTme4"];
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            
            setDataSource("");

            List<string> listStr = new List<string>();
            listStr.Add("全部");
            listStr.Add("杜六房");
            listStr.Add("福鼎");
            DropDownListFendain.DataSource = listStr;
            DropDownListFendain.DataBind();
        }
    }



    protected void shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        setDataSource(DropDownListFendain.SelectedValue.ToString());
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        setDataSource(DropDownListFendain.SelectedValue.ToString());
    }

    protected void btnEmail_Click(object sender, EventArgs e)
    {
        setTotalToMail();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect("Print.aspx?shop=" + DropDownListFendain.SelectedValue.ToString());
    }

    void setDataSource(string fendianName)
    {
        if(fendianName.Length<1||fendianName.Equals("全部"))
        {
         List<COrderObj> OrderList =GetOrderList(strDate1,strDate2);
         if (OrderList == null)
         {
             gvInfo.DataSource = null;
             gvInfo.DataBind();

             string script = "";
             script += "<script language='javascript'>";
             script += "alert(目前当天没有任何订单!');";
             script += "</script>";
             Page.RegisterStartupScript("aaa", script);
         }
         else
         {
             gvInfo.DataSource = OrderList;
             gvInfo.DataBind();
         }
        }
        else
        {
              List<CTotalObj> TotalObjList = getTotal(strDate1, strDate2);
              foreach(CTotalObj kv in TotalObjList)
              {
                  if (kv.Shop == fendianName)
                  {
                      gvInfo.DataSource = kv.ListOrder;
                      gvInfo.DataBind();
                      break;
                  }
                  else
                  {
                      gvInfo.DataSource = null;
                      gvInfo.DataBind();
                  }
              }
        }
    }

    List<COrderObj> GetOrderList(string strDate1,string strDate2)
    {
        List<COrderObj> listOrder = new List<COrderObj>();

        List<CTotalObj> TotalObjList = getTotal(strDate1, strDate2);
        if (TotalObjList == null || TotalObjList.Count < 1) return null;

         
        listOrder.AddRange(TotalObjList[0].ListOrder);

        //两个店加和
        if (TotalObjList.Count == 2)
        {
            for (int i = 0; i < TotalObjList.Count; i++)
            {               
                    listOrder[i].Num += TotalObjList[1].ListOrder[i].Num;
                    listOrder[i].ShopNum += TotalObjList[1].ListOrder[i].ShopNum;              
            }
        }      

        return listOrder;
    }


    void setTotalToMail()
    {
        StringBuilder strBld = new StringBuilder();             

        string fendian = DropDownListFendain.SelectedValue.ToString();
        string title = fendian + "配货单&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;日期：" + DateTime.Now.ToLongDateString();
        strBld.Append("<div>");
        strBld.Append(" <div>" +title+"</div>");
        List<COrderObj> listOrder = null;
        if (fendian.Length < 1 || fendian.Equals("全部"))
        {
            listOrder = GetOrderList(strDate1, strDate2);            
        }
        else
        {
            List<CTotalObj> TotalObjList = getTotal(strDate1, strDate2);
            foreach (CTotalObj kv in TotalObjList)
            {
                if (kv.Shop == fendian)
                {
                    listOrder = kv.ListOrder;
                }
            }
        }

        foreach (COrderObj kv in listOrder)
        {
            strBld.Append("<div>");         
            strBld.Append(getMyString(kv.Name));
            strBld.Append(kv.Num.ToString());         
            strBld.Append("&nbsp;&nbsp;"+getMyString(kv.Unit));   
            strBld.Append("共&nbsp;");
            strBld.Append(kv.ShopNum.ToString());//份数
            strBld.Append("&nbsp;份");      
            strBld.Append("</div>");
        }

        strBld.Append("</div>");

        string emailContent = strBld.ToString();
        bool isok =   setMail(emailContent, fendian);
        string tip = "";
        if (isok)
        {
            tip = "发送成功！";          
        }
        else
        {
            tip = "发送失败！";
        }

        string script = "";
        script += "<script language='javascript'>";
        script += "alert("+tip+"!');";
        script += "</script>";
        Page.RegisterStartupScript("", script);
    }

    string getMyString(string str)
    {
        string res = str;

        string dis = "&nbsp;&nbsp;&nbsp;";
        int num = 10 - str.Length;
        for (int i = 0; i < num; i++)
        {
            res += dis;
        }


        return res;
    }

    List<CTotalObj> getTotal(string strDate1, string strDate2)
    {
        List<CTotalObj> TotalObjList = new List<CTotalObj>();

        //获取所有激活的列名
        DbHelp db = new DbHelp();
        DataSet ds = db.Query("select Name,ColName from tb_dishes where IsAction = 1");
        StringBuilder strbld = new StringBuilder();
        strbld.Append("select ShopName, ");
        if (null != ds && ds.Tables.Count > 0)
        {
            int num = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                strbld.Append("sum(");
                strbld.Append(dr["ColName"].ToString());
                strbld.Append(")");
                strbld.Append(" as  ");
                strbld.Append(dr["Name"].ToString());
                if ((++num) < ds.Tables[0].Rows.Count) //最后一个不用逗号
                    strbld.Append(",");
            }
        }
        strbld.Append(" from tb_order o,tb_shop s,tb_user u  ");
        strbld.Append(" where  1 =1   ");
        strbld.Append(" and o.UserID = u.id  ");
        strbld.Append(" and u.shop = s.id  ");
        strbld.Append(" and (orderDate >='" + strDate1 + "'  and orderDate <='" + strDate2 + "')");
        strbld.Append(" group by s.ShopName  ");

        string strGroupBy = strbld.ToString();
        DataSet dsGroupby = db.Query(strGroupBy);
        if (null != ds && dsGroupby.Tables.Count > 0)
        {
            DataTable dtGroupBy = dsGroupby.Tables[0];
            //获取单位      
            DataSet dsDishes = db.Query("select Unit,Name from tb_dishes  where IsAction = '1'");
            StringBuilder bldRes = new StringBuilder();
            if (null != ds && dsDishes.Tables.Count > 0)
            {
                DataTable dtDishes = dsDishes.Tables[0];
                for (int i = 0; i < dtGroupBy.Rows.Count; i++)
                {
                    CTotalObj totalObj = new CTotalObj();
                    totalObj.Shop = dtGroupBy.Rows[i][0].ToString();
                    List<COrderObj> orderList = new List<COrderObj>();
                    for (int j = 1; j < dtGroupBy.Columns.Count; j++)//第0列是店名，第一列开始
                    {
                        COrderObj orderObj = new COrderObj();
                        string colName = dtGroupBy.Columns[j].ToString();
                        orderObj.Name = colName;
                        orderObj.Unit = getDanwei(dtDishes, colName);
                        string strNum = dtGroupBy.Rows[i][j].ToString();
                        strNum = strNum.Length > 0 ? strNum : "0";
                        orderObj.Num = int.Parse(strNum);
                        orderList.Add(orderObj);
                    }
                    totalObj.ListOrder = orderList;
                    TotalObjList.Add(totalObj);
                }
            }
        }

        return TotalObjList;
    }



    class CTotalObj
    {
        public CTotalObj() { this.ListOrder = new List<COrderObj>(); }
        public string Shop { get; set; }
        public List<COrderObj> ListOrder { get; set; }
    }

    class COrderObj
    {
        public string Name { get; set; }
        int num = 0;
        public int Num
        {
            get { return this.num; }
            set
            {
                this.num = value;
                this.ShopNum = this.num > 0 ? 1 : 0;
            }
        }
        public string Unit { get; set; }
        public int ShopNum { get; set; }
    }


    private string getDanwei(DataTable dt, string colName)
    {
        string danwei = "";

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["Name"].ToString().Equals(colName))
            {
                danwei = dr["Unit"].ToString();
                break;
            }
        }

        return danwei;
    }

    public bool setMail(string content, string fendian)
    {

        bool isok = false;

        //string senderServerIp = "smtp.126.com";
        string title = fendian + "  配货单;日期：" + DateTime.Now.ToLongDateString();

        string fromMailAddress = System.Configuration.ConfigurationSettings.AppSettings["fromMail"];

        string subjectInfo = title;
        
        string bodyInfo = content;//正文
        string mailUsername = System.Configuration.ConfigurationSettings.AppSettings["username"];
        string mailPassword = System.Configuration.ConfigurationSettings.AppSettings["password"];//发送邮箱的密
        string mailPort = "25";

        //获取接收的邮箱
        DbHelp db = new DbHelp();
        DataSet ds = db.Query("select Email from tb_email  ");
        if (null != ds && ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                string toMailAddress = dr["Email"].ToString();
                CSendMail email = new CSendMail("", toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);
               isok =  email.Send();
            }
        }
        return isok;
    } 
}
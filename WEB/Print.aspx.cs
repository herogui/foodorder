using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Text;

public partial class Print : System.Web.UI.Page
{
      string strDate1 = DateTime.Now.ToShortDateString() + "  " + System.Configuration.ConfigurationSettings.AppSettings["fudingTme1"];
            string strDate2 = DateTime.Now.ToShortDateString() + "  " + System.Configuration.ConfigurationSettings.AppSettings["fudingTme2"];
            string shop = "";
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            if (Request.QueryString["shop"] != null)
            {
                shop = Request.QueryString["shop"].ToString();

                this.lblTitle.Text = shop + "&nbsp;&nbsp;&nbsp;&nbsp;配货单" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + DateTime.Now.ToShortDateString();
            }


            setDataSource(shop);           
        }
    }



    void setDataSource(string fendianName)
    {
        if(fendianName.Length<1||fendianName.Equals("全部"))
        {
         List<COrderObj> OrderList =GetOrderList(strDate1,strDate2);
         gvInfo.DataSource = OrderList;
         gvInfo.DataBind();
        }
        else
        {
              List<CTotalObj> TotalObjList = getTotal(strDate1, strDate2);
              foreach(CTotalObj kv in TotalObjList)
              {
                  if(kv.Shop == fendianName)
                  {
                      gvInfo.DataSource = kv.ListOrder;
                      gvInfo.DataBind();
                      break;
                  }
              }
        }
    }

    List<COrderObj> GetOrderList(string strDate1,string strDate2)
    {
        List<COrderObj> listOrder = new List<COrderObj>();

        List<CTotalObj> TotalObjList = getTotal(strDate1, strDate2);

        //两个店加和      
        listOrder.AddRange(TotalObjList[0].ListOrder);
        for (int i = 0; i < listOrder.Count; i++)
        {
            listOrder[i].Num += TotalObjList[1].ListOrder[i].Num;
            listOrder[i].ShopNum += TotalObjList[1].ListOrder[i].ShopNum;
        }

        return listOrder;
    }


    void setTotalToMail()
    {
        StringBuilder strBld = new StringBuilder();

        List<COrderObj> listOrder = GetOrderList(strDate1,strDate2);
        strBld.Append("<div>");

        foreach (COrderObj kv in listOrder)
        {
            strBld.Append("<div  style='width:200px'>");
            strBld.Append("<span   style='width:50px'>");
            strBld.Append(kv.Name);
            strBld.Append(",  ");
            strBld.Append("</span>");
            strBld.Append("<span   style='width:50px'>");
            strBld.Append(kv.Num);
            strBld.Append("  ");
            strBld.Append(kv.Unit);
            strBld.Append(",  ");
            strBld.Append("</span>");
            strBld.Append("<span   style='width:50px'>");
            strBld.Append(kv.ShopNum);//份数
            strBld.Append("份");
            strBld.Append("</span>");
            strBld.Append("</div>");
        }

        strBld.Append("</div>");

        string emailContent = strBld.ToString();
        setMail(emailContent);
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

    public void setMail(string content)
    {
        //string senderServerIp = "smtp.126.com";

        string fromMailAddress = System.Configuration.ConfigurationSettings.AppSettings["fromMail"];

        string subjectInfo = "分店:" + "" + "_发送人:" + "" + "_" + DateTime.Now.ToLongDateString();

        string title = " <div>日期：" + DateTime.Now.ToLongDateString() + "&nbsp;&nbsp;&nbsp;&nbsp;分店:" + "" + "</div>";
        string bodyInfo = title + content;//正文
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
                email.Send();
            }
        }

    } 
}
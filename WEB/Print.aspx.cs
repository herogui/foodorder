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
      string strDate2 = DateTime.Now.ToShortDateString() + "  " + System.Configuration.ConfigurationSettings.AppSettings["liufangTme4"];
            string shop = "";
          
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            if (Request.QueryString["shop"] != null)
            {
                shop = Request.QueryString["shop"].ToString();

                string shopName = "";
                if (shop == "Fuding") shopName = "福鼎";
                else shopName = "杜六房";

                this.lblTitle.Text = shopName + "&nbsp;&nbsp;&nbsp;&nbsp;配货单" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + DateTime.Now.ToShortDateString();
            }


            setDataSource(shop);           
        }
    }



    void setDataSource(string fendianName)
    {
        List<COrderObj> OrderList = GetOrderList(strDate1, strDate2,shop);
        gvInfo.DataSource = OrderList;
        gvInfo.DataBind();
    }

    List<COrderObj> GetOrderList(string strDate1, string strDate2, string orderTbName)
    {
        List<CTotalObj> TotalObjList = getTotal(strDate1, strDate2, orderTbName);
        List<COrderObj> listOrder = new List<COrderObj>();
        listOrder.AddRange(TotalObjList[0].ListOrder);

        //加和
        if (TotalObjList.Count > 1)
        {
            for (int i = 0; i < TotalObjList.Count; i++)
            {
                for (int j = 0; j < listOrder.Count; j++)
                {
                    listOrder[j].Num += TotalObjList[i].ListOrder[j].Num;
                    listOrder[j].ShopNum += TotalObjList[i].ListOrder[j].ShopNum;
                }
            }
        }

        return listOrder;
    }


    List<CTotalObj> getTotal(string strDate1, string strDate2, string orderTbName)
    {
        List<CTotalObj> TotalObjList = new List<CTotalObj>();

        //获取所有激活的列名
        DbHelp db = new DbHelp();
        DataSet ds = db.Query("select Name,ColName from tb_dishes" + orderTbName + " where IsAction = 1");
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
        strbld.Append(" from tb_order" + orderTbName + "  o,tb_shop s,tb_user u  ");
        strbld.Append(" where  1 =1   ");
        strbld.Append(" and u.shop = s.id  ");
        strbld.Append("  and u.id = o.UserID  ");
        strbld.Append(" and (orderDate >='" + strDate1 + "'  and orderDate <='" + strDate2 + "')");
        strbld.Append(" group by s.ShopName  ");

        string strGroupBy = strbld.ToString();
        DataSet dsGroupby = db.Query(strGroupBy);
        if (null != ds && dsGroupby.Tables.Count > 0)
        {
            DataTable dtGroupBy = dsGroupby.Tables[0];
            //获取单位      
            DataSet dsDishes = db.Query("select Unit,Name from tb_dishes" + orderTbName + "   where IsAction = '1'");
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
}
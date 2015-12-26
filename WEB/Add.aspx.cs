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

public partial class Cost_bgLand_add : System.Web.UI.Page
{
    /// <summary>
    /// ���ݱ༭������id
    /// </summary>
    public string strID = "";

    /// <summary>
    /// �û�id  
    /// </summary>
    public string currentUserID = "";
   
    /// <summary>
    /// ���Ȩ��
    /// </summary>
    public bool canLookFlag = true;
    /// <summary>
    /// ����
    /// </summary>
    public string OpertionTitle = "���";

    /// <summary>
    /// ҳ������¼�
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
            OpertionTitle = "�༭";
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
    /// ҳ��༭״̬�����ݿ��ȡ��¼������ҳ����
    /// </summary>
    public void SetFormInfo()
    {
        string sql = "select * from tb_dishes where id = '" + strID + "'";
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
    /// �������������
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

        Dictionary<string, string> dataMap = new Dictionary<string, string>();
        if(strID.Length<1)
        dataMap.Add("id", Guid.NewGuid().ToString());
        else dataMap.Add("id", strID);
        dataMap.Add("Code", Code);
        dataMap.Add("ProductCode", ProductCode);
        dataMap.Add("Name", Name);
        dataMap.Add("Unit", Unit);
        dataMap.Add("producer", producer);
        DbHelp db = new DbHelp();
        int num = db.InsertData("tb_dishes", dataMap);
        if (num > 0)
        {
            string script = "";
            script += "<script language='javascript'>";
            script += "alert('���ݱ���ɹ�!');";
            script += "  opener.__doPostBack('" + "btnRefresh" + "','');";
            script += "  this.close();";
            script += "</script>";
            Page.RegisterStartupScript("RefreshSourceWindowAndCloseMe", script);
        }
    }
}

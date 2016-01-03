<%@ page language="C#" autoeventwireup="true" inherits="OrderView, App_Web_lpi0exvd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订菜汇总</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
   
  <div class="scroll">
     <div style="margin-top:30px">    <span class="title_m">分店：</span>
                                    <asp:DropDownList ID="DropDownListFendain" runat="server" onchange="changes();" AutoPostBack="True"
                                        OnTextChanged="shop_SelectedIndexChanged">
                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnRefresh"  runat="server" Text="查询" CssClass="sub_bg" OnClick="btnRefresh_Click" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnEmail"  runat="server" Text="发送至邮箱" CssClass="sub_bg" OnClick="btnEmail_Click" />
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnPrint"  runat="server" Text="打印" CssClass="sub_bg" OnClick="btnPrint_Click" />
                                    </div>
                        <asp:GridView ID="gvInfo"   runat="server" AllowSorting="True" AutoGenerateColumns="false"
                         DataKeyNames="Name"                    
                            CssClass="list" BorderWidth="0px" CellSpacing="0">
                            <Columns>
                                <asp:BoundField DataField="platePriceGUID" Visible="False" />
                                <asp:TemplateField HeaderText="序号">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Name" HeaderText="品名" ItemStyle-HorizontalAlign="Center" />   
                                <asp:BoundField DataField="Num" HeaderText="订量" ItemStyle-HorizontalAlign="Center" />   
                                  <asp:BoundField DataField="Unit" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />    
                                    <asp:BoundField DataField="ShopNum" HeaderText="份数" ItemStyle-HorizontalAlign="Center" />                                
                            </Columns>
                            <AlternatingRowStyle CssClass="tr1" />
                            <HeaderStyle CssClass="fixed" />
                        </asp:GridView>
                        <%if (gvInfo.Rows.Count == 0)
                          {%>
                        <table border="0" cellspacing="0" class="list">
                            <tr>
                                <th>
                                    序号</th>
                                <th>
                                    品名</th>
                                <th>
                                    单位</th>        
                                    <th>
                                    定量</th>                                                    
                            </tr>
                        </table>
                        <%  } %>
                    </div>
    </form>
</body>
</html>
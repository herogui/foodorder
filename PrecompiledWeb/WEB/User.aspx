<%@ page language="C#" autoeventwireup="true" inherits="User, App_Web_lpi0exvd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top:30px">   <asp:TextBox ID="txtKeyWord" Style="font-size: 12px; font-color: #efefef; color: #999999;"
                                        value="请输入关键字" onFocus="this.style.color='#000000';this.value=''" runat="server"
                                        CssClass="btnRefresh" Width="150px" MaxLength="20"></asp:TextBox>
                                    <asp:Button ID="btnRefresh" runat="server" Text="查询" CssClass="sub_bg" OnClick="btnRefresh_Click" />
                                    <input name="Submit2"  style="margin-left:150px" type="button" class="sub_bg" onclick="javascript:return FunOpenAddFrm();"
                                    value="添加" /></div>
  <div class="scroll">
                        <asp:GridView ID="gvInfo" runat="server" AllowSorting="True" AutoGenerateColumns="false"
                         DataKeyNames="id"
                        OnRowDeleting="gvInfo_RowDeleting"  OnRowDataBound="gvInfo_RowDataBound"
                            CssClass="list" BorderWidth="0px" CellSpacing="0">
                            <Columns>
                                <asp:BoundField DataField="platePriceGUID" Visible="False" />
                                <asp:TemplateField HeaderText="序号">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:BoundField DataField="userName" HeaderText="用户名" ItemStyle-HorizontalAlign="Center" />  
                                 <asp:BoundField DataField="pwd" HeaderText="密码" ItemStyle-HorizontalAlign="Center" />                              
                                  <asp:BoundField DataField="shop" HeaderText="所在店名" ItemStyle-HorizontalAlign="Center" />   
                                  <asp:BoundField DataField="tel" HeaderText="电话" ItemStyle-HorizontalAlign="Center" />  
                                  <asp:BoundField DataField="weixingId" HeaderText="微信ID" ItemStyle-HorizontalAlign="Center" />                               
                                 <asp:TemplateField HeaderText="编辑">
                                    <ItemTemplate>
                                        <img alt="编辑" src="images/edit.gif" onclick="javascript:return FunOpenEditFrm('<%# Eval("id")%>');"
                                            style="cursor: hand;" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="删除">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="images/del.gif" CommandName="Delete" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>                             
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
                                    用户名</th>                            
                                    <th>
                                    所在店名</th>      
                                    <th>
                                    电话</th>     
                                    <th>
                                    微信ID</th>                                                       
                            </tr>
                        </table>
                        <%  } %>
                    </div>
    </form>
</body>
</html>

   <script type="text/javascript">
       var childWin1;
       function FunOpenEditFrm(id) {
           if (childWin1 != null) {
               if (!childWin1.closed)
                   childWin1.close();
           }

           var myLeft = (window.screen.width - 570) / 2;
           var myTop = (window.screen.height - 183) / 2;
           childWin1 = window.open('addUser.aspx?id=' + id + '', 'width=570,height=183,top=' + myTop + ',left=' + myLeft + ',scrollbars=no');
           return false;

       }

       var childWin;
       function FunOpenAddFrm() {
           if (childWin != null) {
               if (!childWin.closed)
                   childWin.close();
           }

           var myLeft = (window.screen.width - 590) / 2;
           var myTop = (window.screen.height - 185) / 2;
           childWin = window.open('addUser.aspx?width=590,height=185,top=' + myTop + ',left=' + myLeft + ',scrollbars=no');
           return false;

       }
</script>

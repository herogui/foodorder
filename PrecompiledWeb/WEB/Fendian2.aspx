<%@ page language="C#" autoeventwireup="true" inherits="Fendian2, App_Web_ftq3y0yq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="css/css.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
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
                                <asp:BoundField DataField="Name" HeaderText="品名" ItemStyle-HorizontalAlign="Center" />                                
                                <asp:BoundField DataField="Unit" HeaderText="单位" ItemStyle-HorizontalAlign="Center" />                               
                                 <asp:TemplateField HeaderText="减少">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnMinus" runat="server" OnClick="btnMinus_Click" ImageUrl="images/del.gif" CommandName="Delete" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="数量">
                                    <ItemTemplate>
                                        <asp:Label  runat="server"    ID="lblNum" Width="100px" Text="3223322"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="增加">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="images/del.gif" CommandName="Delete" />
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
                                    编号</th>
                                <th>
                                    货号</th>        
                                    <th>
                                    品名</th>
                                <th>
                                    单位</th>      
                                     <th>
                                    生产商</th>                              
                            </tr>
                        </table>
                        <%  } %>
                    </div>
    </form>
</body>
</html>

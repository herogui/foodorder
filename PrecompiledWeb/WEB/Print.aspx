<%@ page language="C#" autoeventwireup="true" inherits="Print, App_Web_hfm0fgtu" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>&nbsp;&nbsp;&nbsp;</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
   <div style=" text-align:center; margin-top:50px;"> <asp:Label ID="lblTitle" runat="server"></asp:Label></div>
  <div class="scroll">
    
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
                                <asp:BoundField DataField="Name" HeaderText="品名"  ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />   
                                <asp:BoundField DataField="Num" HeaderText="订量"  ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center" />   
                                  <asp:BoundField DataField="Unit" HeaderText="单位"  ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center" /> 
                                    <asp:BoundField DataField="platePriceGUID" Visible="False" />
                                <asp:TemplateField HeaderText="实际量">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label  runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="配货">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label1"  runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="到货">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label2"  runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="备注">
                                    <ItemStyle HorizontalAlign="Center"  />
                                    <ItemTemplate>
                                        <asp:Label ID="Label3"  runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
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
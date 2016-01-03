<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddShop.aspx.cs" Inherits="AddShop" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加分店</title>
    <link href="css/css.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        function closeWindow() {
            window.close();
        }

        function check() {
            if (document.getElementById("txtShopName").value == "") {
                alert("请输入店名！");
                document.getElementById("txtShopName").focus();
                return false;
            }
        }
 
 </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="scroll">
            <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" class="add_bg">
                <tr>
                    <td height="42" align="center" class="add_title">
                     <%=OpertionTitle%></td>
                </tr>
                <tr>
                    <td valign="top">
                         <table width="100%" border="0" cellpadding="2" cellspacing="1" bgcolor="#CCCCCC">
                          <tr>
                                <td width="28%" align="right" bgcolor="#DEEFEC">
                                    店名 ：</td>
                                <td width="72%" align="left" bgcolor="#DEEFEC">
                                    <asp:TextBox ID="txtShopName" runat="server"  Width="400px"></asp:TextBox><span
                                        style="color: #ff0000">*</span>
                                </td>
                            </tr>
                            <tr>
                                <td width="28%" align="right" bgcolor="#DEEFEC">
                                    编号 ：</td>
                                <td width="72%" align="left" bgcolor="#DEEFEC">
                                    <asp:TextBox ID="txtCode" runat="server"    MaxLength="200" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="28%" align="right" bgcolor="#DEEFEC">
                                    地址 ：</td>
                                <td width="72%" align="left" bgcolor="#DEEFEC">
                                    <asp:TextBox ID="txtAdrress" runat="server"     Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                           
                            <tr>
                                <td width="28%" align="right" bgcolor="#DEEFEC">
                                    电话 ：</td>
                                <td width="72%" align="left" bgcolor="#DEEFEC">
                                    <asp:TextBox ID="txttel" runat="server"  Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="28%" align="right" bgcolor="#DEEFEC">
                                    负责人 ：</td>
                                <td width="72%" align="left" bgcolor="#DEEFEC">
                                    <asp:TextBox ID="txtLeader" runat="server"  Width="400px"></asp:TextBox>
                                </td>
                            </tr>                           
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="57" align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="sub_bg" Text="保 存" OnClick="btnSave_Click"
                            OnClientClick="return check()" />
                        <input id="btnCancel" name="Submit22" type="button" class="sub_bg" value="关 闭" onclick="closeWindow()" />
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" id="txtTemp" name="txtTemp" runat="server" value="" />
    </form>
</body>
</html>

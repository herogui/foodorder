<%@ page language="C#" autoeventwireup="true" inherits="LoginDuliufang, App_Web_lpi0exvd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>杜六房连锁门店订菜系统</title>
<link href="css/longin.css" rel="stylesheet" type="text/css" media="all" />  
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
  
</head>
<body>

<div class="message warning">
<div >
	<div class="login-head">
		<h1>登录界面</h1>
		 		
	</div>
		<form id="Form1" runat="server">
			<div>
				
                用户名:<asp:TextBox ID="txtUser" runat="server" Text="" Width="100px" Height="20px"  BorderWidth="1px"></asp:TextBox>
			</div>
				
			<div  style="margin-top:20px">   
				
                  密     码:<asp:TextBox ID="TextPwd" TextMode="password" Text="admin" runat="server"  Width="100px"  Height="20px"  BorderWidth="1px"></asp:TextBox>
			</div>		
			<div   style="margin-top:20px; margin-left:100px">
				
                <asp:Button ID="btnLogin" Width="150px" runat="server"  Text="登录" OnClick="btLogin_Click" />
			
			</div>
				
		</form>
		</div>					
	</div>
	

</body>
</html>




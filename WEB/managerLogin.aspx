<%@ Page Language="C#" AutoEventWireup="true" CodeFile="managerLogin.aspx.cs" Inherits="managerLogin" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html>
<head>
<title>杜六房连锁门店订菜系统后台管理</title>
<link href="css/longin.css" rel="stylesheet" type="text/css" media="all" />  
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
  
</head>
<body>
<!-- contact-form -->	
<div class="message warning">
<div >
	<div class="login-head">
		<h1>后台管理 登录界面</h1>
		 		
	</div>
		<form id="Form1" runat="server">
			<div>
				<%--<input id="txtUser" type="text" class="text" value="admin" ><a href="#" class=" icon user"></a>--%>
                用户名:<asp:TextBox ID="txtUser" runat="server" Width="100px" Height="20px"  BorderWidth="1px"></asp:TextBox>
			</div>
				
			<div  style="margin-top:20px">
				<%--<input id = "txtPwd" type="password" value="admin" > <a href="#" class="icon lock"></a>--%>
                  密     码:<asp:TextBox ID="TextPwd" TextMode="password" runat="server"  Width="100px"  Height="20px"  BorderWidth="1px"></asp:TextBox>
			</div>		
			<div   style="margin-top:20px; margin-left:170px">
				<%--<input type="submit" id = "btnLogin" onclick="login()"  value="登   录" >--%>
                <asp:Button ID="btnLogin" Width="150px" runat="server"  Text="登录" OnClick="btLogin_Click" />
			
			</div>
				
		</form>
		</div>					
	</div>
	

</body>
</html>


  <script src="js/my.js" type="text/javascript"></script>
    <script src="js/obj.js" type="text/javascript"></script>
<!-- -->
<script type="text/javascript">    var __links = document.querySelectorAll('a'); function __linkClick(e) { parent.window.postMessage(this.href, '*'); }; for (var i = 0, l = __links.length; i < l; i++) { if (__links[i].getAttribute('data-t') == '_blank') { __links[i].addEventListener('click', __linkClick, false); } }</script>
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
<script type="text/javascript">    $(document).ready(function (c) {
        $('.alert-close').on('click', function (c) {
            $('.message').fadeOut('slow', function (c) {
                $('.message').remove();
            });
        });
    });
</script>

<%--登录界面跳转--%>
<script  type="text/javascript">
    function login() {
        var myjs = new my();
        var user = document.getElementById("txtUser").value;
        var pwd = document.getElementById("txtPwd").value;
        var obj = new userObj(user, pwd);
        var dataJson = myjs.Obj2Json(obj);
        myjs.Ajax("Home.aspx/Submit", dataJson, loginSucHandle, loginErrorHandle);
    }

    var tip = "用户名或密码错误!";
    function loginSucHandle(e) {

        if (e.d.toString().length > 0) {
            var user = document.getElementById("txtUser").value;
            var shop = e.d.toString()
            if (shop == "杜六")
                window.location.href = "duliu.aspx?user=" + user + "&shop=" + shop + "&backurl=" + window.location.href;
            else if (shop == "福鼎")
                window.location.href = "fuding.aspx?user=" + user + "&shop=" + shop + "&backurl=" + window.location.href;
        }
        else { alert(tip); }
    }

    function loginErrorHandle() {
        alert(tip);
    }
    </script>


</html>

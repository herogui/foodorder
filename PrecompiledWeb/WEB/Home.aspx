<%@ page language="C#" autoeventwireup="true" inherits="Home, App_Web_2tvzq3qk" %>

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
<!-- contact-form -->	
<div class="message warning">
<div class="inset">
	<div class="login-head">
		<h1>登录界面</h1>
		 		
	</div>
		<form>
			<li>
				<input id="txtUser" type="text" class="text" value="用户名" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Username';}"><a href="#" class=" icon user"></a>
			</li>
				<div class="clear"> </div>
			<li>
				<input id = "txtPwd" type="password" value="Password" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Password';}"> <a href="#" class="icon lock"></a>
			</li>
			<div class="clear"> </div>
			<div class="submit">
				<input type="submit" id = "btnLogin" onclick="login()"  value="登   录" >
			
						  <div class="clear">  </div>	
			</div>
				
		</form>
		</div>					
	</div>
	</div>
	<div class="clear"> </div>


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
        if (e.d.toString()=="true") {           
            window.location.href = "Default.aspx?backurl=" + window.location.href; 
        }
        else { alert(tip); }
    }

    function loginErrorHandle() {
        alert(tip);
    }
    </script>


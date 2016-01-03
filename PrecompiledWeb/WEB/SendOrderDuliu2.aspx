<%@ page language="C#" autoeventwireup="true" inherits="SendOrderDuliu2, App_Web_lpi0exvd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>杜六房连锁门店订菜系统</title>
<link href="css/longin2.css" rel="stylesheet" type="text/css" media="all" />  
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /> 
  
</head>
<body>
<!-- contact-form -->	
 <div  style="text-align:center">您正在为<span style="font-weight: bold"><%=shop%></span>杜六房订菜,请在<span style="font-weight: bold"><%=limitTime%></span>前完成预定！！</div>

<div class="message warning">`   
		<form id="Form1" runat="server">
			<div  id="content" style="text-align:center">
            </div>		
		</form>
	
	</div>		

</body>
</html>
<%-- 获取配置文件数据--%>
    <script type="text/javascript">
        var user = "<%=user%>";
        var shop = "<%=shop%>";
        var myuserid = "<%=myuserid%>";
        var data = "<%=data%>";

        var limitTime = "<%=limitTime%>";

   
        var liufangTme1 = "<%=liufangTme1%>";
        var liufangTme2 = "<%=liufangTme2%>";
        var liufangTme3 = "<%=liufangTme3%>";
        var liufangTme4 = "<%=liufangTme4%>";
         </script>

<script src="js/obj.js" type="text/javascript"></script>
    <script src="js/my.js" type="text/javascript"></script>
    <script src="js/mainDuliu2.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.backgroundpos.js" type="text/javascript"></script>
    <script src="js/menu.js" type="text/javascript"></script>




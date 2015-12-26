<%@ Page Language="C#"    validateRequest="false"  AutoEventWireup="true" CodeFile="Fendian.aspx.cs" Inherits="Fendian" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>杜六房连锁门店订菜系统</title>
    <style type="text/css">
      html, body, #main {
        height:100%;
        width:100%;
        margin:0;
        padding:0;       
      }    
    </style>
   <%-- 获取配置文件数据--%>
    <script type="text/javascript">     
       var user = "<%=user%>";
       var shop = "<%=shop%>";

        var limitTime = "<%=limitTime%>";

       var fudingTme1 = "<%=fudingTme1%>";
       var fudingTme2 = "<%=fudingTme2%>"; 

       var liufangTme1 = "<%=liufangTme1%>";
       var liufangTme2 = "<%=liufangTme2%>";
       var liufangTme3 = "<%=liufangTme3%>";
       var liufangTme4 = "<%=liufangTme4%>";
         </script>


    <link rel="stylesheet" href="css/menu.css" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <!-- 代码 开始 -->
    <div id="main" class="header" style="margin-right: auto;height:30px; visibility:collapse">
        <ul class="menu" id="ui" >         
            <li><a href="#" target="_self">福鼎订菜</a></li>
            <li><a href="#" target="_self">六房订菜</a></li>
             <li><a href="#" target="_self">预定调料</a></li>          
        </ul>     
        
    </div>
 <!-- 时间 -->
 <div  style="text-align:center"><%=user%>,您正在为<span style="font-weight: bold"><%=shop%></span>订菜,请在<span style="font-weight: bold"><%=limitTime%></span>前完成预定！！</div>

    <!-- 代码 内容 -->
     <div id="content" style="width:100%;  text-align:center">

         <%-- 菜品--%>
     <div id ="divFoods" style="float:left;  width:25%;">
     </div >  

    <%-- 减列--%>
     <div id="divMinus" style="float:left;width:25%; ">     
     </div>

   <%--  数量列--%>
     <div id="divNum" style="float:left;width:25%;">     
     </div>   

    <%-- 加列--%>
     <div  id="divAdd" style="float:left;width:25%;"> 
     </div>
     
     </div>

          <!-- 代码 底部 -->
          <div style="margin-top:50px">
          <div style="margin-top:100px;float:left; margin-left:10%">
            <input type="button" value="重置"  onclick="repeat()" style="width:160px;height:50px;" />
          </div>
          <div style ="margin-top:100px;float:right;margin-right:10%">
           <input type="button" value="提交" onclick="sendMailFendian()" style="width:160px;height:50px;" /></div>
          </div>

         <%-- 菜品--%>
     <div id ="divCaiping" style="float:left;  width:25%;">
          
     </div >  

   <%--  数量列--%>
     <div id="divShuliang" style="float:left;width:25%;">     

     </div>  


</body>
</html>

<script src="js/obj.js" type="text/javascript"></script>
    <script src="js/my.js" type="text/javascript"></script>
    <script src="js/main.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.backgroundpos.js" type="text/javascript"></script>
    <script src="js/menu.js" type="text/javascript"></script>
    
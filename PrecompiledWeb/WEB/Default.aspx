<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_b0wak4g5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>杜六房连锁门店订菜系统</title>
    <style>
      html, body, #main {
        height:100%;
        width:100%;
        margin:0;
        padding:0;       
      }    
    </style>

    <link rel="stylesheet" href="css/menu.css" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />

    <script src="js/main.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.backgroundpos.js" type="text/javascript"></script>
    <script src="js/menu.js" type="text/javascript"></script>

</head>
<body>
    <!-- 代码 开始 -->
    <div id="main" class="header" style="margin-right: auto;height:100px;">
        <ul class="menu" id="ui" >         
            <li><a href="#" target="_self">福鼎订菜</a></li>
            <li><a href="#" target="_self">六房订菜</a></li>
             <li><a href="#" target="_self">预定调料</a></li>          
        </ul>     
        
    </div>
    
     <div id="content" style="width:100%;  text-align:center">
     <div id ="div1" style="float:left;  width:100px;">
     <div class="text1">酱汁肉</div>
      <div class="text1">油爆虾</div>
       <div class="text1">小排</div>
        <div class="text1">臭豆腐</div>
         <div class="text1">话梅萝卜</div>
      <div class="text1">酱鸭</div>
       <div class="text1">糖藕</div>
        <div class="text1">狮子头</div>
        <div class="text1">辣酱</div>
      <div class="text1">醉鸡</div>
       <div class="text1" >目鱼卷</div>
        <div class="text1">扁尖毛豆</div>
         <div class="text1">海蜇丝</div>
      <div class="text1">烤鸭</div>
       <div class="text1">豉油鸡</div>
        <div class="text1">盐焗鸡</div>
         <div class="text1">鸭膀</div>
     </div >  
    <%-- 减列--%>
     <div id="divMinus" style="float:left;width:100px; ">     
     </div>
   <%--  数量列--%>
     <div id="divNum" style="float:left;width:100px;">     
     </div>   
    <%-- 加列--%>
     <div  id="divAdd" style="float:left;width:100px;"> 
     </div>
     
     </div>
          
</body>
</html>

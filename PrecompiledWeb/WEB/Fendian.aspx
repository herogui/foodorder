<%@ page language="C#" validaterequest="false" autoeventwireup="true" inherits="Fendian, App_Web_ftq3y0yq" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�����������ŵ궩��ϵͳ</title>
    <style type="text/css">
      html, body, #main {
        height:100%;
        width:100%;
        margin:0;
        padding:0;       
      }    
    </style>
   <%-- ��ȡ�����ļ�����--%>
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

    <!-- ���� ��ʼ -->
    <div id="main" class="header" style="margin-right: auto;height:30px; visibility:collapse">
        <ul class="menu" id="ui" >         
            <li><a href="#" target="_self">��������</a></li>
            <li><a href="#" target="_self">��������</a></li>
             <li><a href="#" target="_self">Ԥ������</a></li>          
        </ul>     
        
    </div>
 <!-- ʱ�� -->
 <div  style="text-align:center"><%=user%>,������Ϊ<span style="font-weight: bold"><%=shop%></span>����,����<span style="font-weight: bold"><%=limitTime%></span>ǰ���Ԥ������</div>

    <!-- ���� ���� -->
     <div id="content" style="width:100%;  text-align:center">

         <%-- ��Ʒ--%>
     <div id ="divFoods" style="float:left;  width:25%;">
     </div >  

    <%-- ����--%>
     <div id="divMinus" style="float:left;width:20%; ">     
     </div>

   <%--  ������--%>
     <div id="divNum" style="float:left;width:20%;">     
     </div>   

    <%-- ����--%>
     <div  id="divAdd" style="float:left;width:20%;"> 
     </div>
     
     </div>

          <!-- ���� �ײ� -->
          <div style="margin-top:50px">
          <div style="margin-top:100px;float:left; margin-left:5%">
            <input type="button" value="����"  onclick="repeat()" style="width:160px;height:50px;" />
          </div>
          <div style ="margin-top:100px;float:right;margin-right:5%">
           <input type="button" value="�ύ" onclick="sendMailFendian()" style="width:160px;height:50px;" /></div>
          </div>

         <%-- ��Ʒ--%>
     <div id ="divCaiping" style="float:left;  width:20%;">
          
     </div >  

   <%--  ������--%>
     <div id="divShuliang" style="float:left;width:20%;">     

     </div>  


</body>
</html>

<script src="js/obj.js" type="text/javascript"></script>
    <script src="js/my.js" type="text/javascript"></script>
    <script src="js/main.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.backgroundpos.js" type="text/javascript"></script>
    <script src="js/menu.js" type="text/javascript"></script>
    
<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

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

    <link rel="stylesheet" href="css/menu.css" type="text/css" />
    <link href="css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <!-- ���� ��ʼ -->
    <div id="main" class="header" style="margin-right: auto;height:100px;">
        <ul class="menu" id="ui" >         
            <li><a href="#" target="_self">��������</a></li>
            <li><a href="#" target="_self">��������</a></li>
             <li><a href="#" target="_self">Ԥ������</a></li>          
        </ul>     
        
    </div>
    <!-- ���� ���� -->
     <div id="content" style="width:100%;  text-align:center">

         <%-- ��Ʒ--%>
     <div id ="divFoods" style="float:left;  width:25%;">
     </div >  

    <%-- ����--%>
     <div id="divMinus" style="float:left;width:25%; ">     
     </div>

   <%--  ������--%>
     <div id="divNum" style="float:left;width:25%;">     
     </div>   

    <%-- ����--%>
     <div  id="divAdd" style="float:left;width:25%;"> 
     </div>
     
     </div>
          <!-- ���� �ײ� -->
          <div style="margin-top:50px">
          <div style="margin-top:100px;float:left; margin-left:10%">
            <input type="button" value="����"  onclick="repeat()" style="width:160px;height:50px;" />
          </div>
          <div style ="margin-top:100px;float:right;margin-right:10%">
           <input type="button" value="�ύ" onclick="sendMain()" style="width:160px;height:50px;" /></div>
          </div>
</body>
</html>

<script src="js/obj.js" type="text/javascript"></script>
    <script src="js/my.js" type="text/javascript"></script>
    <script src="js/main.js" type="text/javascript"></script>
    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.backgroundpos.js" type="text/javascript"></script>
    <script src="js/menu.js" type="text/javascript"></script>
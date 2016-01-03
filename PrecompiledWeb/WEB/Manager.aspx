<%@ page language="C#" autoeventwireup="true" inherits="Manager, App_Web_lpi0exvd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>杜六房连锁门店订菜系统后台管理</title>
<link type="text/css" rel="stylesheet" href="css/you_public_head.css">

    <script src="js/jquery-1.11.2.min.js" type="text/javascript"></script>
</head>
<body>
<br>
<div class="publicNav" id="publicNav" data="二级导航栏">
    <div class="publicNav_nav">
        <p><a href="OrderView.aspx" class="currHover">订单汇总</a></p>
        <p><a href="CaidangDuliu.aspx"class="currHover">杜六房菜品管理</a></p>
         <p><a href="CaidangFuding.aspx"class="currHover">福鼎菜品管理</a></p>
         <p><a href="shop.aspx"class="currHover">分店管理</a></p>
          <p><a href="user.aspx"class="currHover">用户管理</a></p>
        <p><a href="Email.aspx" class="currHover">邮箱管理</a></p>
       
        <div class="publicNavHover">
            <div class="publicNavHoverF"></div>
            <div class="publicNavHoverM"></div>
            <div class="publicNavHoverE"></div>
        </div>
    </div>
</div>

<script type="text/javascript">

    //导航切换，根据域名判断当前显示
    $(function () {
        function navTab(indexPath) {
            if (window.location.href.indexOf(indexPath) > 0) {
                return true;
            } else {
                return false;
            }
        }
        var currNav;
        if (navTab('flight.118114.cn')) {
            currNav = 1; $(".currHover").eq(currNav).addClass("hover");
        } else if (navTab('hotel.118114.cn')) {
            currNav = 2; $(".currHover").eq(currNav).addClass("hover");
        } else if (navTab('you.118114.cn')) {
            currNav = 3; $(".currHover").eq(currNav).addClass("hover");
        } else if (navTab('food.118114.cn')) {
            currNav = 4; $(".currHover").eq(currNav).addClass("hover");
        } else {
            currNav = 0; $(".currHover").eq(currNav).addClass("hover");
        }
        function navHover(curr) {
            var $navHover = $(".currHover").eq(curr);
            $navHover.parent().css("border-color", "transparent").siblings().children().removeClass("hover");
            $(".publicNavHover").stop(true, false).animate({ "left": $navHover.parent().position().left, "width": $navHover.parent().width() + 41 }, 100, function () { $navHover.addClass("hover").parent().css("border-color", "#e7e7e7") });
            $(".publicNavHoverM").css({ "width": $navHover.parent().width() + 33 });
        }
        navHover(currNav);
        $(".publicNav_nav").children("p").hover(function () {
            var curr = $(this).index();
            navHover(curr);
        }, function () {
            //	$(this).children().removeClass("hover").css("color","#404040");
        }).each(function () {
            if ($(this).attr("class") == "new") {
                var left = $(this).position().left;
                $(".publicNav_nav").append("<img src='images/bg_new.png' style='z-index:9999;position:absolute;top:0;left:" + left + "px' />")
            }
            else if ($(this).attr("class") == "hot") {
                var left = $(this).position().left;
                var width = $(this).children("a").width();
                $(".publicNav_nav").append("<img src='images/bg_hot.png' style='z-index:9999;position:absolute;top:-9px;left:" + (left + width + 40) + "px' />")
            }
        });
        $(".publicNav_nav").hover(function () { }, function () { navHover(currNav) });
    });
    //导航切换结束

    //添加副导航
    function addSubNav(obj) {
        var html = "<div class='publicNavSub'>";
        var len = obj.length
        for (i = 0; i < len; i++) {
            if (i != len - 1)
                html += "<a href='" + obj[i].alink + "'>" + obj[i].atitle + "</a>";
            else
                html += "<a href='" + obj[i].alink + "' class='noborder'>" + obj[i].atitle + "</a>";
        }
        html += "</div>";
        $("#publicNav").after(html);
    }
</script>

</body>
</html>
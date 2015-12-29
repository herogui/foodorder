var txtName = "txtNum";
var foods = [];
var myjs = new my();
window.onload = function () {

    //初始化食物列表
    initFoods()

    //初始化页面
    initUI();
}

//初始化食物列表
function initFoods() {   
    
    var dataJson = "";
    myjs.Ajax("Fendian.aspx/getDishes", dataJson, getDishesSucHandle, getDishesErrorHandle);

    function getDishesSucHandle(e) {
        var strs = e.d.toString().split(",");
        var units = [];
        for (var j = 0; j < strs.length; j++) {

            var name = strs[j].split("@")[0];
            if (name.length < 1) continue;
            foods.push(name);
            units.push(strs[j].split("@")[1]);
        }
      
        var divFoods = document.getElementById("divFoods");
        var htmlNum = "";
        for (var i = 0; i < foods.length; i++) {
            htmlNum += "<div id=\"subDivFood" + i + "\"  class=\"text1\">" + getMyString2(foods[i])+ units[i] + "</div>";
        }
        divFoods.innerHTML = htmlNum;

        //初始化列
        initCol();
    }
    function getDishesErrorHandle(e) {
        
    }
}

//加号事件
function add(num) {
    var txtNum = document.getElementById(txtName + num);
    txtNum.value = parseInt(txtNum.value) + 1;
}

//减号事件
function minus(num) {

    var txtNum = document.getElementById(txtName + num);
    if (txtNum.value == "0") return;
    txtNum.value = parseInt(txtNum.value) - 1;
}

//初始化页面
function initUI() {
    var w = document.body.clientWidth;

    var marginLeft = (w - 307) / 2 + "px";
    var ui = document.getElementById("ui");
    ui.style.marginLeft = marginLeft; //菜单居中

   // var content = document.getElementById("content");
  //  content.style.marginLeft = w * 0.1 + "px"; //内容居左10%

   // var divFoods = document.getElementById("divFoods");
  //  divFoods.style.marginLeft = (w - 381) / 3 + "px"; //第一列居左设置
}


//初始化列
function initCol() {
    //数量列
    var divNum = document.getElementById("divNum");
    var htmlNum = "";
    for (var i = 0; i < foods.length; i++) {
        htmlNum += "<div  class=\"text3\"><input  id = \"" + txtName + i + "\" type=\"text\" class=\"input\" value=\"0\" /></div>";
    }
    divNum.innerHTML = htmlNum;

    //加列
    var divAdd = document.getElementById("divAdd");
    var htmlAdd = "";
    for (var i = 0; i < foods.length; i++) {
        htmlAdd += "<div><input type=\"button\" value=\"增加\" onclick=\"add('" + i + "')\"  class=\"text2\"/></div>"
        //htmlAdd += "<div><img  src=\"images/jiahao.png\" id=\"增加\" onclick=\"add('" + i + "')\"/><div>";
    }
    divAdd.innerHTML = htmlAdd;

    //减列
    var divMinus = document.getElementById("divMinus");
    var htmlMinus = "";
    for (var i = 0; i < foods.length; i++) {
       htmlMinus += " <div><input type=\"button\" value=\"减少\"    onclick=\"minus('" + i + "')\"   class=\"text4\"/></div>";
       // htmlMinus += "<div><img  src=\"images/jianhao.png\" id=\"减少\" onclick=\"minus('" + i + "')\"/><div>";
    }
   
    divMinus.innerHTML = htmlMinus;
}

//重置
function repeat() {
   
    if (confirm("重置后所有操作都失效，是否重置?")) {
        //数量列
        var divNum = document.getElementById("divNum");
        var htmlNum = "";
        for (var i = 0; i < foods.length; i++) {
            htmlNum += "<div  class=\"text3\"><input  id = \"" + txtName + i + "\" type=\"text\" class=\"input\" value=\"0\" /></div>";
        }
        divNum.innerHTML = htmlNum;
    }
}

function getNowFormatDateTime() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
    return currentdate;
}

function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
    var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
           
    return currentdate;
}

//分店预定
function sendMailFendian() {
        var current = getNowFormatDateTime();
        var currentTime = Date.parse(current)

        if(shop == "福鼎") {
            var fuding1 = getNowFormatDate() + " " + fudingTme1 + "";
            var fuding2 = getNowFormatDate() + " " + fudingTme2 + "";
            var time1 = Date.parse(fuding1);
            var time2 = Date.parse(fuding2);
            if (currentTime > time2 || currentTime < time1) {
                alert("超出订菜时间,请在" + limitTime + "前完成订菜！");
                return;
            }
        }

        if (shop == "杜六房") {
            var duliu1 = getNowFormatDate() + " " + liufangTme1 + "";
            var duliu2 = getNowFormatDate() + " " + liufangTme2 + "";
            var duliu3 = getNowFormatDate() + " " + liufangTme3 + "";
            var duliu4 = getNowFormatDate() + " " + liufangTme4 + "";
            var time1 = Date.parse(duliu1);
            var time2 = Date.parse(duliu2);
            var time3 = Date.parse(duliu3);
            var time4 = Date.parse(duliu4);

            if ((currentTime > time1 && currentTime < time2) || (currentTime > time3 && currentTime < time4)) {

            }
            else {
                alert("超出订菜时间,请在" + limitTime + "前完成订菜！");
                return;
            }
        }   
   
    
   //发送到邮箱
    var content = getContentDiv();
    var obj = new sendEmalObj(shop, user, content)//user,shop 从前端界面传递过来
    var dataJson = myjs.Obj2Json(obj);
    myjs.Ajax("Fendian.aspx/SendMail", dataJson, sendEmailSucHandle, sendEmailErrorHandle);

    //数据库保存
    var obj2 = new sendEmalObj(shop, user, getContentList())//user,shop 从前端界面传递过来
    var dataJson2 = myjs.Obj2Json(obj2);
    myjs.Ajax("Fendian.aspx/SaveOrder", dataJson2, saveOrderSucHandle, saveOrderErrorHandle);

    var tip = "发送邮箱";
    function sendEmailSucHandle(e) {
        if (e.d.toString() == "true") {
            alert(tip+"成功");
        }
        else alert(tip + "失败，请联系系统管理员!");    
    }

    function sendEmailErrorHandle(e) {
        alert(tip + "失败，请联系系统管理员!");
    }

    function saveOrderSucHandle(e) {

    }

    function saveOrderErrorHandle(e) {

    }
}

//获取邮件内容
function getContentDiv() {
 var html = "<div>";
    for (var i = 0; i < foods.length; i++) {
        var subDivFood = document.getElementById("subDivFood" + i).innerHTML;
        var sum = document.getElementById(txtName + i).value;;
        html += "<div>";    
        html += subDivFood + getMyString(sum);
        html += "</div>";      
    }
    
   html += "</div>";
    return html;
}

//单位的间隔
function getMyString2(str) {
    var res = str;

    var dis = "&nbsp;&nbsp;&nbsp;";
    var num = 7 - str.length;
    for (var i = 0; i < num; i++) {
        res += dis;
    }
    return res;
}
//数量的间隔
 function getMyString(str)
    {
        var res = str;

        var dis = "&nbsp;&nbsp;&nbsp;";
        var num =7 - str.length; //
        for (var i = 0; i < num; i++) {
            res = dis+res;
        }


        return res;
    }

function getContentList() {
    var List = "";
    for (var i = 0; i < foods.length; i++) {
        var subDivFood = document.getElementById("subDivFood" + i).innerHTML;
        var sum = document.getElementById(txtName + i).value;
        List += subDivFood + "," + sum+";";
    }
  
    return List;
}

//获取邮件内容()左右结构
function getContent2() {
    var html = "<div>";

    html += "<div id =\"divCaiping\" style='float:left;  width:25%;'>";
    for (var i = 0; i < foods.length; i++) {
        var subDivFood = document.getElementById("subDivFood" + i).innerHTML;
        html += "<div>";
        html += getMyString(subDivFood);
        html += "</div>";
    }
    html += "<\div>";

    html += "<div id =\"divShuliang\" style=\"float:left;  width:25%;\">";
    for (var i = 0; i < foods.length; i++) {
        html += "<div>";
        html += document.getElementById(txtName + i).value;

        html += "</div>";
    }
    html += "</div>";

    html += "</div>";
    return html;
}

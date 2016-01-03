var txtName = "txtNum";
var foods = [];
var myjs = new my();

var list1Mail = [];
var list1 = [];
var list2 = [];
var list3 = [];
var list4 = [];
window.onload = function () {

    //初始化食物列表
    initFoods()

    //初始化页面
    initUI();
}
function getData() {
    var List = "";
    for (var i = 0; i < foods.length; i++) {
        var sum = document.getElementById(txtName + i).value;
       
        List += sum + ",";
    }

    return List;
}
//初始化食物列表
function initFoods() {   
    
    var dataJson = "";
    myjs.Ajax("SendOrderFuding.aspx/getDishes", dataJson, getDishesSucHandle, getDishesErrorHandle);

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
       
        for (var i = 0; i < foods.length; i++) {
            var htmlFood = "<span id = 'subDivFood" + i + "'>" + getMyStringWeb(foods[i]) + units[i] + "&nbsp;</span > ";
            list1.push(htmlFood);
            var htmlFoodMail = getMyStringMail(foods[i]) + units[i];

            list1Mail.push(htmlFoodMail); //发邮箱用 
        }       

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
}


//初始化列
function initCol() {
    //数量列
    for (var i = 0; i < foods.length; i++) {
        var htmlNum = "<input  type=\"text\"  value='0' id=\"" + txtName + i + "\" style=\"border-width:1px;border-style:solid;height:20px;width:20px;\">";
        list3.push(htmlNum);    
    }

    //加列
    for (var i = 0; i < foods.length; i++) {
        var htmlAdd = "<input type=\"button\"  value=\"+\"   onclick=\"add('" + i + "')\" style=\"width:60px;margin-left:10px;\">";
        list4.push(htmlAdd);  
    } 

    //减列
    for (var i = 0; i < foods.length; i++) {
        var htmlMinus = "<input type=\"button\"  value=\"-\"   onclick=\"minus('" + i + "')\" style=\"width:60px;;margin-right:10px;\">";
        list2.push(htmlMinus);  
    }

    var allHtml = "";
     for (var i = 0; i < list2.length; i++) {
        var divHthl = "<div style=\"margin-top:30px\">";
        divHthl += list1[i];
        divHthl += list2[i];
        divHthl += list3[i];
        divHthl += list4[i];
        divHthl += "</div>"
        allHtml += divHthl;
    }
    allHtml += " <div style='margin-top:30px;'>";
    allHtml += " <input type='button' value='预览' onclick='preview()' style='width:200px;height:50px;' /></div>";
    allHtml += " </div>";

    var divContent = document.getElementById("content");
    divContent.innerHTML = allHtml;
}

function preview() {
    var data = getData();
    window.location.href = "SendOrderFuding2.aspx?data=" + data + "user=" + user + "shop=" + shop + "userid=" + myuserid + "&backurl=" + window.location.href;
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

       
            var fuding1 = getNowFormatDate() + " " + fudingTme1 + "";
            var fuding2 = getNowFormatDate() + " " + fudingTme2 + "";
            var time1 = Date.parse(fuding1);
            var time2 = Date.parse(fuding2);
            if (currentTime > time2 || currentTime < time1) {
                alert("超出订菜时间,请在" + limitTime + "前完成订菜！");
                return;
            }
      
   
    
   //发送到邮箱
    var content = getContentDiv();
    var obj = new sendEmalObj(shop, user, content)//user,shop 从前端界面传递过来
    var dataJson = myjs.Obj2Json(obj);
    myjs.Ajax("SendOrderFuding.aspx/SendMail", dataJson, sendEmailSucHandle, sendEmailErrorHandle);

    //数据库保存
    var obj2 = new sendEmalObj(shop, user, getContentList())//user,shop 从前端界面传递过来
    var dataJson2 = myjs.Obj2Json(obj2);
    myjs.Ajax("SendOrderFuding.aspx/SaveOrder", dataJson2, saveOrderSucHandle, saveOrderErrorHandle);

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
    for (var i = 0; i < list1Mail.length; i++) {
        var subDivFood = list1Mail[i];
        var sum = document.getElementById(txtName + i).value;
        if (sum == "0") continue;
        html += "<div>";
        html += subDivFood + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + sum;
        html += "</div>";      
    }
    
   html += "</div>";
    return html;
}

//单位的间隔
function getMyStringWeb(str) {
    var res = str;

    var dis = "&nbsp;";
    var num = 5 - str.length;
    for (var i = 0; i < num; i++) {
        res += dis;
    }
    return res;
}
//数量的间隔
 function getMyStringMail(str)
    {
        var res = "";

        var dis = "&nbsp;&nbsp;&nbsp;";
        var num =8- str.length; //
        for (var i = 0; i < num; i++) {
            res += dis;
        }


        return str+res;
    }

    function getContentList() {
        var List = "";
        for (var i = 0; i < foods.length; i++) {
            var sum = document.getElementById(txtName + i).value;
            if (sum == "0") continue;
            List += foods[i] + "@" + sum + ";";
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
        html += getMyStringMail(subDivFood);
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

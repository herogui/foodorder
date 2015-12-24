var txtName = "txtNum";
window.onload = function () {

    //初始化页面
    initUI();

    //初始化列
    initCol();

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

    var content = document.getElementById("content");
    content.style.marginLeft = w * 0.1 + "px"; //内容居左10%

    var div1 = document.getElementById("div1");
    div1.style.marginLeft = (w - 381) / 3 + "px"; //第一列居左设置
}


//初始化列
function initCol() {
    //数量列
    var divNum = document.getElementById("divNum");
    var htmlNum = "";
    for (var i = 0; i < 17; i++) {
        htmlNum += "<div  class=\"text3\"><input  id = \"" + txtName + i + "\" type=\"text\" class=\"input\" value=\"0\" /></div>";
    }
    divNum.innerHTML = htmlNum;

    //加列
    var divAdd = document.getElementById("divAdd");
    var htmlAdd = "";
    for (var i = 0; i < 17; i++) {
        htmlAdd += "<div><input type=\"button\" value=\"+\" onclick=\"add('"+i+"')\"  class=\"text2\"/></div>";
    }
    divAdd.innerHTML = htmlAdd;

    //减列
    var divMinus = document.getElementById("divMinus");
    var htmlMinus = "";
    for (var i = 0; i < 17; i++) {
        htmlMinus += " <div><input type=\"button\" value=\"-\"    onclick=\"minus('" + i + "')\"   class=\"text4\"/></div>";
    }
    divMinus.innerHTML = htmlMinus;
}

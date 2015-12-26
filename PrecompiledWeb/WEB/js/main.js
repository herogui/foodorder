var txtName = "txtNum";
var foods = [];
window.onload = function () {

    //初始化食物列表
    initFoods()

    //初始化页面
    initUI();

    //初始化列
    initCol();

}

//初始化食物列表
function initFoods() {
    
    foods.push("酱汁肉");
    foods.push("油爆虾");
    foods.push("小排");
    foods.push("臭豆腐");
    foods.push("话梅萝卜");
    foods.push("酱鸭");
    foods.push("糖藕");
    foods.push("狮子头");
    foods.push("辣酱");
    foods.push("醉鸡");
    foods.push("目鱼卷");
    foods.push("扁尖毛豆");
    foods.push("海蜇丝");
    foods.push("烤鸭");
    foods.push("豉油鸡");
    foods.push("盐焗鸡");
    foods.push("鸭膀");

    var divFoods = document.getElementById("divFoods");
    var htmlNum = "";
    for (var i = 0; i < foods.length; i++) {
        htmlNum += "<div class=\"text1\">"+foods[i]+"</div>";
    }
    divFoods.innerHTML = htmlNum;

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
        htmlAdd += "<div><input type=\"button\" value=\"+\" onclick=\"add('"+i+"')\"  class=\"text2\"/></div>";
    }
    divAdd.innerHTML = htmlAdd;

    //减列
    var divMinus = document.getElementById("divMinus");
    var htmlMinus = "";
    for (var i = 0; i < foods.length; i++) {
        htmlMinus += " <div><input type=\"button\" value=\"-\"    onclick=\"minus('" + i + "')\"   class=\"text4\"/></div>";
    }
    divMinus.innerHTML = htmlMinus;
}

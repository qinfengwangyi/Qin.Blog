var Qin = {};

Qin.Alert = function (title, msg, isOk, func) {
    var icon = isOk == true ? '../Content/images/msgbox_true.png' : '../Content/images/msgbox_error.png';
    Qin.create_mask();
    var boxhtml = "<div style=\"width:300px;height:100%;border-radius:4px;background-color: #fff;color:#999; font-weight: bold;font-size: 12px;text-align:center;padding-top:18px;\" >"
                + "<p style=\"display:table\">"
                + "<img style=\"display:table-cell;\" src=\"" + icon + "\" width=\"64\" height=\"64\"></img>"
				+ "<span style=\"display:table-cell;text-align:center;color: #888;\">" + msg
                + "</span>"
                + "</p>"
				+ "<input type='button'  style=\"cursor:pointer;border:none;border-radius:3px; background-color:#d71d48; width:60px; height:30px; color:#fff;\" value='确定'id=\"msgconfirmb\"   onclick=\"Qin.remove();" + func + ";\">"
                + "</div>";
    var box = document.createElement("div");
    box.id = "msgbox";
    box.style.position = "absolute";
    box.style.left = (Qin.get_width() - 300) / 2 + "px";
    box.style.top = (Qin.get_height() - 150) / 2 + "px";
    box.style.width = "300px";
    box.style.height = "150px";
    box.style.overflow = "visible";
    box.innerHTML = boxhtml;
    box.style.zIndex = 1001;
    document.body.appendChild(box);
}

//Get Body Width
Qin.get_width = function () {
    return (document.body.clientWidth + document.body.scrollLeft);
}
//Get Body Height
Qin.get_height = function () {
    return (document.body.clientHeight);
}
//Create Mask
Qin.create_mask = function () {
    var mask = document.createElement("div");
    mask.id = "mask";
    mask.style.position = "absolute";
    mask.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=4,opacity=25)";//IE
    mask.style.opacity = 0.4;
    mask.style.background = "black";
    mask.style.top = "0px";
    mask.style.left = "0px";
    mask.style.width = "100%";//Qin.get_width() + "px";
    mask.style.height = Qin.get_height() + "px";
    mask.style.zIndex = "1000";
    document.body.appendChild(mask);
}

//Remove
Qin.remove = function () {
    var mask = document.getElementById("mask");
    var msgbox = document.getElementById("msgbox");
    if (!!mask && !!msgbox) {
        document.body.removeChild(mask);
        document.body.removeChild(msgbox);
    }
}


//弹出层div
Qin.CreateMaskDiv = function (div, w, h, top) {
    Qin.create_mask();
    var box = document.createElement("div");
    box.id = "msgbox";
    box.style.position = "absolute";
    box.style.left = (Qin.get_width() - w) / 2 + "px";
    box.style.top = top || 280 + "px";//(Qin.get_height() - h) / 2 + "px";
    box.style.width = w + "px";
    box.style.height = h + "px";
    box.style.backgroundColor = "#fff";
    box.style.borderRadius = 4 + "px";
    box.style.overflow = "visible";
    box.innerHTML = div;
    box.style.zIndex = 1001;
    document.body.appendChild(box);
}

/***********f
顶层maskmax, 一般用于弹窗上的弹出层 Start
****************/

//Create Mask
Qin.create_maskmax = function () {
    var mask = document.createElement("div");
    mask.id = "maskmax";
    mask.style.position = "absolute";
    mask.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=4,opacity=25)";//IE
    mask.style.opacity = 0.4;
    mask.style.background = "black";
    mask.style.top = "0px";
    mask.style.left = "0px";
    mask.style.width = Qin.get_width() + "px";
    mask.style.height = Qin.get_height() + "px";
    mask.style.zIndex = "1001";
    document.body.appendChild(mask);
}

//create mask over everythins
Qin.AlertMax = function (title, msg, isOk, func) {
    var icon = isOk == true ? '../Content/images/msgbox_true.png' : '../Content/images/msgbox_error.png';
    Qin.create_maskmax();
    var boxhtml = "<div style=\"width:300px;height:100%;border-radius:4px;background-color: #fff;color:#999; font-weight: bold;font-size: 12px;text-align:center;padding-top:18px;\" >"
                + "<p style=\"display:table\">"
                + "<img style=\"display:table-cell;\" src=\"" + icon + "\" width=\"64\" height=\"64\"></img>"
				+ "<span style=\"display:table-cell;text-align:center;color: #888;\">" + msg
                + "</span>"
                + "</p>"
				+ "<input type='button'  style=\"cursor:pointer;border:none;border-radius:3px; background-color:#d71d48; width:60px; height:30px; color:#fff;\" value='确定'id=\"msgconfirmb\"   onclick=\"Qin.remove_max();" + func + ";\">"
                + "</div>";
    var box = document.createElement("div");
    box.id = "msgboxmax";
    box.style.position = "absolute";
    box.style.left = (Qin.get_width() - 300) / 2 + "px";
    box.style.top = 300 + "px";
    box.style.width = "300px";
    box.style.height = "150px";
    box.style.overflow = "visible";
    box.innerHTML = boxhtml;
    box.style.zIndex = 1002;
    document.body.appendChild(box);
}

//remove max mask&under other mask
Qin.remove_max = function () {
    var mask = document.getElementById("maskmax");
    var msgbox = document.getElementById("msgboxmax");
    if (!!mask && !!msgbox) {
        document.body.removeChild(mask);
        document.body.removeChild(msgbox);
        Qin.remove();
    }
}
/*********************** 
顶层Mask End 
************************/


//Person Class
var Person = function (name, address) {
    this.name = name;
    this.address = address;
    this.say = function () {
        alert(this.name);
    }
}


//js数组去重
Qin.uniqueArray = function (arr) {
    var arr = arr || [];
    var a = {};
    for (var i = 0; i < arr.length; i++) {
        var v = arr[i];
        if (typeof (a[v]) == "undefined") {
            a[v] = 1;
        }
    };
    arr.length = 0;
    for (var i in a) {
        arr[arr.length] = i;
    }
    return arr;
}
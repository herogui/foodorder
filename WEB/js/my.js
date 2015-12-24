function my() {

    this.getRootPath = function () {
        var curWwwPath = window.document.location.href;
        //获取主机地址之后的目录，如： /ems/Pages/Basic/Person.jsp
        var pathName = window.document.location.pathname;
        var pos = curWwwPath.indexOf(pathName);
        var localhostPath = curWwwPath.substring(0, pos);

        return localhostPath;
    }

   this.rgbToHex = function (r, g, b) { return "#" + ((r << 16) | (g << 8) | b).toString(16); } 

    this.browseFolder = function () {
        try {
            var Message = "\u8bf7\u9009\u62e9\u6587\u4ef6\u5939"; //选择框提示信息
            var Shell = new ActiveXObject("Shell.Application");
            var Folder = Shell.BrowseForFolder(0, Message, 64, 17); //起始目录为：我的电脑
            //var Folder = Shell.BrowseForFolder(0, Message, 0); //起始目录为：桌面
            if (Folder != null) {
                Folder = Folder.items(); // 返回 FolderItems 对象
                Folder = Folder.item(); // 返回 Folderitem 对象
                Folder = Folder.Path; // 返回路径
                if (Folder.charAt(Folder.length - 1) != "\\") {
                    Folder = Folder + "\\";
                }         
                return Folder;
            }
        }
        catch (e) {
            alert(e.message);
        }
    }   

    this.Lonlat2Pnt = function (lonlat) {
        return new OpenLayers.Geometry.Point(lonlat.lon, lonlat.lat);
    }
     
    this.GetBounds = function(objList) {

        var xMin = 99999999;
        var xMax = 0;
        var yMin = 99999999;
        var yMax = 0;

        var lon = 0;
        var lat = 0;
        for (var i = 0; i < objList.length; i++) {
            lon = parseFloat(objList[i].geom.split(',')[0]);
            lat = parseFloat(objList[i].geom.split(',')[1]);
            xMin = lon < xMin ? lon : xMin;
            xMax = lon > xMax ? lon : xMax;
            yMin = lat < yMin ? lat : yMin;
            yMax = lat > yMax ? lat : yMax;
        }
        var lonLat1 =this.Pnt4326To900913(xMin, yMin);
        var lonLat2 = this.Pnt4326To900913(xMax, yMax);

        return new OpenLayers.Bounds(lonLat1.lon, lonLat1.lat,lonLat2.lon, lonLat2.lat);
    }
    
    this.StrJson2Obj= function(strJson) {
        return eval('('+strJson+')');
    }
     
    this.Lonlat2Wkt = function(lon, lat) {
        return "ST_GeomFromText(\"Point(" + lon + "  " + lat + ")\",0)";
    }

    this.Ajax = function(myUrl, myData,sunHandle,errorHandle) {
        try {
            $.ajax({
                //要用post方式   
                type: "Post",
                //方法所在页面和方法名
                url: myUrl,
                data: myData,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: sunHandle,
                error: errorHandle
            });
        }
        catch (exp) { alert(exp)}
    }

    this.Obj2Json = function (o) {
        if (o == undefined) {
            return "";
        }
        var r = [];
        if (typeof o == "string") return "\"" + o.replace(/([\"\\])/g, "\\$1").replace(/(\n)/g, "\\n").replace(/(\r)/g, "\\r").replace(/(\t)/g, "\\t") + "\"";
        if (typeof o == "object") {
            if (!o.sort) {
                for (var i in o)
                    r.push("\"" + i + "\":" + this.Obj2Json(o[i]));
                if (!!document.all && !/^\n?function\s*toString\(\)\s*\{\n?\s*\[native code\]\n?\s*\}\n?\s*$/.test(o.toString)) {
                    r.push("toString:" + o.toString.toString());
                }
                r = "{" + r.join() + "}"
            } else {
                for (var i = 0; i < o.length; i++)
                    r.push(this.Obj2Json(o[i]))
                r = "[" + r.join() + "]";
            }
            return r;
        }

        var json = o.toString().replace(/\"\:/g, '":""');
        
        return json;
    }

    this.Feature900913ToLonlat4326 = function(feature) {

        var wkt = new OpenLayers.Format.WKT();
        var strWkt = wkt.write(feature);

        var lonlat900913 = this.StrWkt2Lonlat(strWkt);
        var lonlat4326 = this.Pnt900913To4326(lonlat900913);

        return lonlat4326;
    };
    
    this.StrWkt2Lonlat = function(strWkt) {
        var strX = strWkt.split(' ')[0].split('(')[1];
        var strY = strWkt.split(' ')[1].split(')')[0];
        var x = parseFloat(strX);
        var y = parseFloat(strY);

        return new OpenLayers.LonLat(x, y);
    };

    this.Feature2Lonlat = function (feature) {
        var wkt = new OpenLayers.Format.WKT();
        var strWkt = wkt.write(feature);

        var strX = strWkt.split(' ')[0].split('(')[1];
        var strY = strWkt.split(' ')[1].split(')')[0];
        var x = parseFloat(strX);
        var y = parseFloat(strY);

        return new OpenLayers.LonLat(x, y);
    };

    this.Pnt4326To900913 = function(lon, lat) {

        var pnt = new OpenLayers.LonLat(lon, lat);
        var proj_4326 = new OpenLayers.Projection('EPSG:4326');
        var proj_900913 = new OpenLayers.Projection('EPSG:900913');
        pnt.transform(proj_4326, proj_900913);

        return pnt;
    };

    this.Pnt900913To4326 = function(lonlat900913) {
   
        var source = new Proj4js.Proj('EPSG:900913');
        var dest = new Proj4js.Proj('EPSG:4326');
       
        var point = new Proj4js.Point(lonlat900913.lon, lonlat900913.lat);
        Proj4js.transform(source, dest, point);

        return new OpenLayers.LonLat(point.x,point.y);
    };


    
    //判断复选框是否被选中
    //name:复选框名称
    //return:是否勾选
    this.IsChkChecked = function(name) {
        chked = false;

        var obj = document.getElementsByName(name);

        //循环从1开始 0图层时底图
        for (i = 0; i < obj.length; i++) {
            if (obj[i].checked) {
                chked = true;

                break;
            }
        }

        return chked;
    };

    //遍历doc
    this.GetDoc = function(doc) {
        x = doc.documentElement.childNodes;
        for (i = 0; i < x.length; i++) {
            document.write(x[i].nodeName);
            document.write(": ");
            document.write(x[i].childNodes[0].nodeValue);
            document.write("<br />");
        }
    };

    //遍历doc
    this.GetDoc2 = function(doc) {
        for (pro in doc) {
            document.write(pro + ":   " + req[pro] + "    " + "<br>");
        }
    }

}
function Rad(d) {
    return d * Math.PI / 180.0; //经纬度转换成三角函数中度分表形式。
}
// 计算距离，参数分别为第一点的纬度，经度；第二点的纬度，经度
function GetDistance(lat1, lng1, lat2, lng2) {
    var radLat1 = Rad(lat1);
    var radLat2 = Rad(lat2);
    var a = radLat1 - radLat2;
    var b = Rad(lng1) - Rad(lng2);
    var s = 2 * Math.asin(Math.sqrt(Math.pow(Math.sin(a / 2), 2) + Math.cos(radLat1) * Math.cos(radLat2) * Math.pow(Math.sin(b / 2), 2)));
    s = s * 6378.137; // EARTH_RADIUS;
    s = Math.round(s * 10000) / 10000; //输出为公里
    return s.toFixed(1); //返回强制保留以为小数
}

//关于排序
function listSortBy(arr, field, order) {
    var refer = [], result = [], order = order == 'asc' ? 'asc' : 'desc', index;
    for (i = 0; i < arr.length; i++) {
        var newField = parseFloat(field); //转换为doubt类型
        refer[i] = arr[i][newField] + ':' + i;
    }
    refer.sort();
    if (order == 'desc') refer.reverse();
    for (i = 0; i < refer.length; i++) {
        index = refer[i].split(':')[1];
        result[i] = arr[index];
    }
    return result;
}

//排序
function compare(a, b) {
    var resultA = parseFloat(a['description']);
    var resultB = parseFloat(b['description']);
    if (resultA > resultB) {
        return 1;
    }
    else if (resultA < resultB) {
        return -1;
    }
    return 0;
}


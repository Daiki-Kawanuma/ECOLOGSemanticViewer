var map;							//! マップ オブジェクト。
var geo;							//! Geo コード取得用オブジェクト。
var isInitialized = false;			//! 初期化フラグ。
var markers = new Array();	//! マーカーのコレクション。
var nextID = 0;				//! 次に割り当てられるマーカーの識別子。
var selectedID = -1;				//! 選択されているマーカーの識別子。

var currentCircle;

// Marker に id プロパティを追加 ( 初期値は無効値 )
google.maps.Marker.prototype.id = -1;

/**
 * マップの初期化を行います。
 */
function initialize() {
    var mapdiv = document.getElementById("map_canvas");
    var myOptions = { zoom: 16, center: new google.maps.LatLng(35.681643, 139.766073), mapTypeId: google.maps.MapTypeId.ROADMAP, scaleControl: true };
    map = new google.maps.Map(mapdiv, myOptions);
    geo = new google.maps.Geocoder();

    // マップの中央位置が更新された時のイベント
    google.maps.event.addListener(map, 'center_changed', function () {
        
    });

    // マップのタイル読み込みが完了した時のイベント
    google.maps.event.addListener(map, 'tilesloaded', function () {
        // 起動時に一度だけ座標を更新する
        if (!isInitialized) {
            isInitialized = true;
            window.external.OnInitCompleted();
        }
    });
}

function getRandomColor(number) {

    switch (number % 19) {
        case 0:
            return "#E60012";
        case 1:
            return "#8FC31F";
        case 2:
            return "#00A0E9";
        case 3:
            return "#920783";
        case 4:
            return "#EB6100";
        case 5:
            return "#22AC38";
        case 6:
            return "#0086D1";
        case 7:
            return "#BE0081";
        case 8:
            return "#F39800";
        case 9:
            return "#009944";
        case 10:
            return "#0068B7";
        case 11:
            return "#E4007F";
        case 12:
            return "#FCC800";
        case 13:
            return "#009B6B";
        case 14:
            return "#00479D";
        case 15:
            return "#E5006A";
        case 16:
            return "#FFF100";
        case 17:
            return "#009E96";
        case 18:
            return "#1D2088";
    }

}

function addLine(semanticLinkID ,latitude1, longitude1, latitude2, longitude2) {

    // ラインを引く座標の配列を作成 
    var mapPoints = [
        new google.maps.LatLng(latitude1, longitude1),
        new google.maps.LatLng(latitude2, longitude2)
    ];

    // ラインを作成 
    var polyLineOptions = {
        path: mapPoints,
        strokeWeight: 5,
        strokeColor: getRandomColor(semanticLinkID),
        strokeOpacity: "1.0"
    };

    // ラインを設定 
    var poly = new google.maps.Polyline(polyLineOptions);
    poly.id = semanticLinkID;
    poly.setMap(map);

    google.maps.event.addListener(poly, "click", function(){
        
        window.external.OnLineClicked(poly.id);
    });
}

function addCircle(latitude, longitude) {

    // 円を作成
    var circleOptions = {
        center: new google.maps.LatLng(latitude, longitude),
        radius: 3,
        strokeWeight: 1,
        strokeColor: "#000000",
        strokeOpacity: 1.0,
        fillColor: "#000000",
        fillOpacity: 1.0,
        zIndex:2
    };

    // 円を設定
    var circle = new google.maps.Circle(circleOptions);
    circle.setMap(map);
}

function moveCurrentCircle(latitude, longitude) {

    if (currentCircle != null) {
        currentCircle.setMap(null);
    }

    circleOptions = {
        center: new google.maps.LatLng(latitude, longitude),
        radius: 5,
        strokeWeight: 1,
        strokeColor: "#FF0000",
        strokeOpacity: 1.0,
        fillColor: "#FF0000",
        fillOpacity: 1.0,
        zIndex:4
    }

    // 円を設定
    currentCircle = new google.maps.Circle(circleOptions);
    currentCircle.setMap(map);
}

function moveMap(latitude, longitude) {

    var location = new google.maps.LatLng(latitude, longitude);
    map.setCenter(location);
}
var map;							//! マップ オブジェクト。
var geo;							//! Geo コード取得用オブジェクト。
var isInitialized = false;			//! 初期化フラグ。
var markers = new Array();	//! マーカーのコレクション。
var nextID = 0;				//! 次に割り当てられるマーカーの識別子。
var selectedID = -1;				//! 選択されているマーカーの識別子。

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

function addLine(semanticLinkID ,latitude1, longitude1, latitude2, longitude2) {

    // ラインを引く座標の配列を作成 
    var mapPoints = [
        new google.maps.LatLng(latitude1, longitude1),
        new google.maps.LatLng(latitude2, longitude2)
    ];

    //alert(mapPoints);

    // ラインを作成 
    var polyLineOptions = {
        path: mapPoints,
        strokeWeight: 5,
        strokeColor: "#0000ff",
        strokeOpacity: "0.5"
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
        radius: 100,
        strokeWeight: 1,
        strokeColor: "#000000",
        strokeOpacity: 1.0,
        fillColor: "#000000",
        fillOpacity: 1.0
    };

    // 円を設定
    var circle = new google.maps.Circle(circleOptions);
    circle.setMap(map);
}

function moveMap(latitude, longitude) {

    var location = new google.maps.LatLng(latitude, longitude);
    map.setCenter(location);
}
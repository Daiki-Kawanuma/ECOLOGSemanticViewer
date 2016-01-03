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
    var myOptions = { zoom: 16, center: new google.maps.LatLng(35.6894876, 139.6917064), mapTypeId: google.maps.MapTypeId.ROADMAP, scaleControl: true };
    map = new google.maps.Map(mapdiv, myOptions);
    geo = new google.maps.Geocoder();

    // マップの中央位置が更新された時のイベント
    google.maps.event.addListener(map, 'center_changed', function () {
        setTimeout(notifyLocation, 200);
    });

    // マップのタイル読み込みが完了した時のイベント
    google.maps.event.addListener(map, 'tilesloaded', function () {
        // 起動時に一度だけ座標を更新する
        if (!isInitialized) {
            isInitialized = true;
            notifyLocation();
        }
    });
}

/**
 * マップ上にマーカーを追加します。
 * マーカーの初期位置は、現在表示されているマップの中央座標となります。
 */
function addMarker() {
    var center = map.getCenter();
    var marker = new google.maps.Marker({ position: center, map: map, title: "マーカー", draggable: true });
    marker.id = nextID++;

    // 選択
    google.maps.event.addListener(marker, 'click', function () {
        selectedID = marker.id;
        window.external.OnMarkerSelectionChanged(marker.id);
    });

    // 移動開始
    google.maps.event.addListener(marker, 'dragstart', function () {
        selectedID = marker.id;
        window.external.OnMarkerSelectionChanged(marker.id);
    });

    // 移動
    google.maps.event.addListener(marker, 'drag', function () {
        window.external.OnMarkerMoved(marker.id, marker.position.lat(), marker.position.lng());
    });

    markers[markers.length] = marker;
    window.external.OnMarkerAdded(marker.id, marker.position.lat(), marker.position.lng());

    selectedID = marker.id;
    window.external.OnMarkerSelectionChanged(marker.id);
}

/**
 * 現在選択されているマーカーのインデックスを取得します。
 *
 * @return	マーカーのコレクション内におけるインデックス。
 *			選択されているマーカーが無ければ -1。
 */
function getSelecedMarkerIndex() {
    if (selectedID != -1) {
        for (var index = 0; index < markers.length; ++index) {
            if (markers[index].id == selectedID) {
                return index;
            }
        }
    }

    return -1;
}

/**
 * 現在選択されているマーカーを削除します。
 */
function removeMarker() {
    var index = getSelecedMarkerIndex();
    if (index == -1) { return; }

    var id = markers[index].id;

    markers[index].setMap();
    markers[index] = null;
    markers.splice(index, 1);
    selectedID = -1;

    // マーカーが全て消えた場合は、割り当てる ID をリセット
    if (markers.length == 0) { nextID = 0; }

    window.external.OnMarkerRemoved(id);
    window.external.OnMarkerSelectionChanged(-1);
}

/**
 * 現在選択しているマーカーが表示されるようにマップを移動します。
 */
function showMarker() {
    var index = getSelecedMarkerIndex();
    if (index == -1) { return; }

    map.setCenter(markers[index].position);
    notifyLocation();
}

/**
 * 現在位置の緯度・経度を取得してマネージ コードへ通知します。
 */
function notifyLocation() {
    var center = map.getCenter();
    window.external.OnMapMoved(center.lat(), center.lng());
}

/**
 * 指定された住所の座標へ、マップを移動します。
 *
 * @param[in]	address	住所。
 */
function moveMap(address) {
    if (geo) {
        geo.geocode({ 'address': address }, function (results, status) {
            if (results && results[0]) {
                map.setCenter(results[0].geometry.location);
                notifyLocation();
            }
        });
    }
}
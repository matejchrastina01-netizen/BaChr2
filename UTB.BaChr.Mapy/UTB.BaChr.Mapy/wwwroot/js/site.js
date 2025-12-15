// Globální proměnná pro mapu
var myMap;

$(document).ready(function () {
    if ($('#map').length > 0) {
        initMap();
    }
});

function initMap() {
    var w = 2048;
    var h = 2048;

    var map = L.map('map', {
        crs: L.CRS.Simple,
        minZoom: -2,
        maxZoom: 2,
        zoomSnap: 0.5,
        zoomDelta: 0.5,
        zoomControl: false
    });

    L.control.zoom({ position: 'topright' }).addTo(map);
    myMap = map;

    var southWest = map.unproject([0, h], map.getMaxZoom() - 1);
    var northEast = map.unproject([w, 0], map.getMaxZoom() - 1);
    var bounds = new L.LatLngBounds(southWest, northEast);

    L.imageOverlay('/images/Firewatch-World-Map-Clean.jpg', bounds).addTo(map);

    map.setMaxBounds(bounds);
    map.fitBounds(bounds);

    // Vykreslení existujících pinů
    if (typeof mapLocations !== 'undefined' && mapLocations !== null) {
        mapLocations.forEach(function (loc) {
            if (loc.mapX != null && loc.mapY != null) {
                var point = map.unproject([loc.mapX, loc.mapY], map.getMaxZoom() - 1);
                var marker = L.marker(point).addTo(map);
                var popupContent = `<b>${loc.name}</b><br>${loc.description || ''}`;
                marker.bindPopup(popupContent);
            }
        });
    }

    // --- TESTOVACÍ NÁSTROJ PRO ZJIŠŤOVÁNÍ POLOHY ---
    // Po kliknutí do mapy se ukáže bublina se souřadnicemi pro DB

    var popup = L.popup();

    function onMapClick(e) {
        // Převod zeměpisných souřadnic (kliknutí) na pixely obrázku
        var point = map.project(e.latlng, map.getMaxZoom() - 1);

        // Zaokrouhlení na celá čísla
        var x = Math.floor(point.x);
        var y = Math.floor(point.y);

        popup
            .setLatLng(e.latlng)
            .setContent(`
                <div style="text-align:center;">
                    <b>Souřadnice bodu</b><br>
                    MapX: <code>${x}</code><br>
                    MapY: <code>${y}</code>
                </div>
            `)
            .openOn(map);

        console.log(`Kliknuto: MapX = ${x}, MapY = ${y}`);
    }

    map.on('click', onMapClick);
}

// Funkce pro zoomování z menu
function zoomToLocation(x, y) {
    if (myMap && x != null && y != null) {
        var target = myMap.unproject([x, y], myMap.getMaxZoom() - 1);
        myMap.flyTo(target, 1);
    }
}
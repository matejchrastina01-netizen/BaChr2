// Globální proměnné
var myMap;
var markers = {};

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

    // 1. Definice hranic samotného obrázku (přesně 2048x2048)
    var southWestImg = map.unproject([0, h], map.getMaxZoom() - 1);
    var northEastImg = map.unproject([w, 0], map.getMaxZoom() - 1);
    var imageBounds = new L.LatLngBounds(southWestImg, northEastImg);

    // 2. Definice hranic pro pohyb (přidáme 500px "vatu" okolo, aby se neusekávaly popisky)
    var padding = 500;
    var southWestMax = map.unproject([-padding, h + padding], map.getMaxZoom() - 1);
    var northEastMax = map.unproject([w + padding, -padding], map.getMaxZoom() - 1);
    var maxBounds = new L.LatLngBounds(southWestMax, northEastMax);

    // Přidání obrázku (použijeme imageBounds - aby se obrázek nenatahoval do paddingu)
    L.imageOverlay('/images/Firewatch-World-Map-Clean.jpg', imageBounds).addTo(map);

    // Nastavení omezení pohybu (použijeme maxBounds - ty větší hranice)
    map.setMaxBounds(maxBounds);

    // Na startu vycentrujeme na obrázek
    map.fitBounds(imageBounds);

    // Vykreslení pinů
    if (typeof mapLocations !== 'undefined' && mapLocations !== null) {
        mapLocations.forEach(function (loc) {
            if (loc.mapX != null && loc.mapY != null) {
                var point = map.unproject([loc.mapX, loc.mapY], map.getMaxZoom() - 1);

                var marker = L.marker(point).addTo(map);
                var popupContent = `<b>${loc.name}</b><br>${loc.description || ''}`;
                marker.bindPopup(popupContent);

                var key = loc.mapX + "_" + loc.mapY;
                markers[key] = marker;
            }
        });
    }

    // Nástroj pro zjišťování souřadnic
    var popup = L.popup();
    function onMapClick(e) {
        var point = map.project(e.latlng, map.getMaxZoom() - 1);
        var x = Math.floor(point.x);
        var y = Math.floor(point.y);

        popup
            .setLatLng(e.latlng)
            .setContent(`
                <div style="text-align:center;">
                    <b>Souřadnice</b><br>
                    MapX: <code>${x}</code><br>
                    MapY: <code>${y}</code>
                </div>
            `)
            .openOn(map);
        console.log(`Kliknuto: MapX = ${x}, MapY = ${y}`);
    }
    map.on('click', onMapClick);
}

function zoomToLocation(x, y) {
    if (myMap && x != null && y != null) {
        var target = myMap.unproject([x, y], myMap.getMaxZoom() - 1);
        myMap.flyTo(target, 1);

        var key = x + "_" + y;
        var marker = markers[key];
        if (marker) {
            marker.openPopup();
        }
    }
}
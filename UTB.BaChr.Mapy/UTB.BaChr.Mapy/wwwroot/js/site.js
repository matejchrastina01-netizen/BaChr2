// Globální proměnné
var myMap;
var markers = {};

$(document).ready(function () {
    if ($('#map').length > 0) {
        initMap();
    }
});

function initMap() {
    // ... (kód pro inicializaci mapy, imageBounds, maxBounds atd. zůstává stejný) ...
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

    var southWestImg = map.unproject([0, h], map.getMaxZoom() - 1);
    var northEastImg = map.unproject([w, 0], map.getMaxZoom() - 1);
    var imageBounds = new L.LatLngBounds(southWestImg, northEastImg);

    var padding = 500;
    var southWestMax = map.unproject([-padding, h + padding], map.getMaxZoom() - 1);
    var northEastMax = map.unproject([w + padding, -padding], map.getMaxZoom() - 1);
    var maxBounds = new L.LatLngBounds(southWestMax, northEastMax);

    L.imageOverlay('/images/Firewatch-World-Map-Clean.jpg', imageBounds).addTo(map);
    map.setMaxBounds(maxBounds);
    map.fitBounds(imageBounds);

    // === NOVÉ: Definice vlastního vzhledu ikonky ===
    // Použijeme L.divIcon, který vytvoří HTML div s naší CSS třídou
    var firewatchIcon = L.divIcon({
        className: '', // Necháme prázdné, třídu dáme přímo do html
        // Vložíme div s naší CSS třídou 'firewatch-pin'
        html: '<div class="firewatch-pin"></div>',
        iconSize: [30, 30], // Velikost kontejneru
        iconAnchor: [15, 30], // Bod, kterým se pin "zapíchne" do mapy (spodní špička)
        popupAnchor: [0, -35] // Kde se otevře bublina relativně k ukotvení
    });
    // ==============================================


    // Vykreslení pinů
    if (typeof mapLocations !== 'undefined' && mapLocations !== null) {
        mapLocations.forEach(function (loc) {
            if (loc.mapX != null && loc.mapY != null) {
                var point = map.unproject([loc.mapX, loc.mapY], map.getMaxZoom() - 1);

                // UPRAVENO: Přidána možnost { icon: firewatchIcon }
                var marker = L.marker(point, { icon: firewatchIcon }).addTo(map);

                var popupContent = `<b>${loc.name}</b><br>${loc.description || ''}`;
                marker.bindPopup(popupContent);

                var key = loc.mapX + "_" + loc.mapY;
                markers[key] = marker;
            }
        });
    }

    // Nástroj pro zjišťování souřadnic (zůstává stejný)
    var popup = L.popup();
    function onMapClick(e) {
        // ... (zbytek funkce onMapClick) ...
        var point = map.project(e.latlng, map.getMaxZoom() - 1);
        var x = Math.floor(point.x);
        var y = Math.floor(point.y);
        popup
            .setLatLng(e.latlng)
            .setContent(`<div style="text-align:center;"><b>Souřadnice</b><br>MapX: <code>${x}</code><br>MapY: <code>${y}</code></div>`)
            .openOn(map);
    }
    map.on('click', onMapClick);
}

// Funkce zoomToLocation zůstává stejná
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
// Čekáme, až se načte celá stránka
document.addEventListener("DOMContentLoaded", function () {

    // Najdeme všechny prvky, které potřebujeme
    const mapContainer = document.getElementById('map-container');
    const mapImage = document.getElementById('firewatch-map');
    const zoomInBtn = document.getElementById('zoom-in');
    const zoomOutBtn = document.getElementById('zoom-out');
    const zoomResetBtn = document.getElementById('zoom-reset');

    // Pokud na stránce není mapa, zbytek kódu nespouštíme
    if (!mapContainer || !mapImage) {
        return;
    }

    // --- Stav mapy ---
    let currentScale = 1.0; // Aktuální zoom
    let currentX = 0;       // Aktuální posun X
    let currentY = 0;       // Aktuální posun Y

    // --- Základní rozměry obrázku (při scale=1) ---
    let baseImageWidth = 0;
    let baseImageHeight = 0;

    // --- Stav posouvání (Drag) ---
    let isDragging = false;
    let startDragX = 0;
    let startDragY = 0;

    // --- Nastavení zoomu ---
    const zoomStep = 0.2; // Pro tlačítka
    const minScale = 1.0;
    const maxScale = 5.0;

    // --- Funkce pro inicializaci rozměrů ---
    function initializeDimensions() {
        baseImageWidth = mapImage.clientWidth;
        baseImageHeight = mapImage.clientHeight;
        applyTransform();
    }

    if (mapImage.complete) {
        initializeDimensions();
    } else {
        mapImage.onload = initializeDimensions;
    }


    // --- Hlavní funkce pro aplikaci transformací ---
    function applyTransform() {
        if (isDragging) {
            mapImage.style.transition = 'none';
        } else {
            // Povolíme plynulé animace pro zoom (kolečko i tlačítka)
            mapImage.style.transition = 'transform 0.1s ease-out';
        }

        // 1. Omezení Zoomu
        currentScale = Math.max(minScale, Math.min(currentScale, maxScale));

        // 2. Výpočet aktuálních rozměrů
        const scaledWidth = baseImageWidth * currentScale;
        const scaledHeight = baseImageHeight * currentScale;
        const containerWidth = mapContainer.clientWidth;
        const containerHeight = mapContainer.clientHeight;

        // 3. Omezení Posunu (Pan Limits / Bounding Box)
        let minX, maxX, minY, maxY;

        if (scaledWidth < containerWidth) {
            minX = maxX = (containerWidth - scaledWidth) / 2;
        } else {
            minX = containerWidth - scaledWidth;
            maxX = 0;
        }

        if (scaledHeight < containerHeight) {
            minY = maxY = (containerHeight - scaledHeight) / 2;
        } else {
            minY = containerHeight - scaledHeight;
            maxY = 0;
        }

        // 4. Aplikace omezení na aktuální pozici
        currentX = Math.max(minX, Math.min(currentX, maxX));
        currentY = Math.max(minY, Math.min(currentY, maxY));

        // 5. Aplikujeme finální CSS transformaci
        mapImage.style.transform = `translate(${currentX}px, ${currentY}px) scale(${currentScale})`;
    }

    // --- Ovládání Zoomu (Tlačítka) ---
    zoomInBtn.addEventListener('click', () => {
        currentScale += zoomStep;
        applyTransform();
    });

    zoomOutBtn.addEventListener('click', () => {
        currentScale -= zoomStep;
        applyTransform();
    });

    zoomResetBtn.addEventListener('click', () => {
        currentScale = minScale;
        currentX = 0;
        currentY = 0;
        applyTransform();
    });

    // --- Ovládání Posunu (Drag and Pan) ---
    mapContainer.addEventListener('mousedown', (e) => {
        if (baseImageHeight === 0) return;
        isDragging = true;
        mapImage.classList.add('is-dragging');
        startDragX = e.clientX;
        startDragY = e.clientY;
        e.preventDefault();
    });

    mapContainer.addEventListener('mousemove', (e) => {
        if (!isDragging) return;
        const deltaX = e.clientX - startDragX;
        const deltaY = e.clientY - startDragY;
        currentX += deltaX;
        currentY += deltaY;
        applyTransform(); // applyTransform se postará o omezení
        startDragX = e.clientX;
        startDragY = e.clientY;
    });

    function stopDragging() {
        if (isDragging) {
            isDragging = false;
            mapImage.classList.remove('is-dragging');
            applyTransform(); // Znovu zapne animace
        }
    }

    mapContainer.addEventListener('mouseup', stopDragging);
    mapContainer.addEventListener('mouseleave', stopDragging);


    // --- NOVÁ ČÁST: Ovládání Zoomu (Kolečko myši) ---
    mapContainer.addEventListener('wheel', (e) => {
        // Zabráníme výchozímu chování (scrollování celé stránky)
        e.preventDefault();

        // Získáme pozici myši relativně ke kontejneru
        const rect = mapContainer.getBoundingClientRect();
        const mouseX = e.clientX - rect.left;
        const mouseY = e.clientY - rect.top;

        // 1. Najdeme bod na obrázku, který je pod myší
        const imageX_before = (mouseX - currentX) / currentScale;
        const imageY_before = (mouseY - currentY) / currentScale;

        // 2. Vypočítáme nový scale
        const zoomFactor = 1.1;
        const oldScale = currentScale;
        if (e.deltaY < 0) { // Scroll nahoru (zoom in)
            currentScale = oldScale * zoomFactor;
        } else { // Scroll dolů (zoom out)
            currentScale = oldScale / zoomFactor;
        }

        // 3. Omezení zoomu (musíme to udělat před výpočtem posunu)
        currentScale = Math.max(minScale, Math.min(currentScale, maxScale));

        // 4. Vypočítáme nový posun (X, Y) tak, aby
        //    bod pod myší zůstal na svém místě
        currentX = mouseX - (imageX_before * currentScale);
        currentY = mouseY - (imageY_before * currentScale);

        // 5. Aplikujeme všechny změny a necháme funkci
        //    applyTransform(), aby omezila i posun (pan)
        applyTransform();
    });

});
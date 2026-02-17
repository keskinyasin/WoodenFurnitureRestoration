window.initSplitScreen = function () {
    // Elementlerin DOM'da olduğundan emin ol
    var container = document.getElementById('splitContainer');
    if (!container) {
        // Henüz yoksa 100ms sonra tekrar dene
        setTimeout(function () { window.initSplitScreen(); }, 100);
        return;
    }

    var splash = document.getElementById('splashContent');
    var title = document.getElementById('splashTitle');
    var text = document.getElementById('splashText');
    var furnitureBtn = document.getElementById('furnitureBtn');
    var restorationBtn = document.getElementById('restorationBtn');

    if (!furnitureBtn || !restorationBtn) return;

    // Önceki listener'ları temizle (çift bağlanmayı önle)
    furnitureBtn.replaceWith(furnitureBtn.cloneNode(true));
    restorationBtn.replaceWith(restorationBtn.cloneNode(true));

    // Yeniden al (cloneNode sonrası referans değişir)
    var fbtn = document.getElementById('furnitureBtn');
    var rbtn = document.getElementById('restorationBtn');

    var isOpening = false;

    fbtn.addEventListener('click', function () {
        if (isOpening) return;
        isOpening = true;
        title.textContent = '🪑 Furniture';
        text.textContent = 'El yapımı ahşap mobilyalar';
        openDoor('furniture');
    });

    rbtn.addEventListener('click', function () {
        if (isOpening) return;
        isOpening = true;
        title.textContent = '🔧 Restoration';
        text.textContent = 'Ahşap restorasyon hizmetleri';
        openDoor('restoration');
    });

    function openDoor(side) {
        splash.classList.add('visible');
        container.classList.add('open');
        setTimeout(function () {
            window.location.href = '/' + side;
        }, 2500);
    }

    console.log('✅ SplitScreen initialized');
};
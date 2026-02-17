console.log('✅ app.js loaded');

window.getInputValue = function (elementId) {
    const element = document.getElementById(elementId);
    return element ? element.value : '';
};

window.setLocalStorage = function (key, value) {
    localStorage.setItem(key, value);
};

window.getLocalStorage = function (key) {
    return localStorage.getItem(key);
};

window.initSplitScreen = function () {
    console.log('🔄 initSplitScreen called');

    var container = document.getElementById('splitContainer');
    if (!container) {
        setTimeout(window.initSplitScreen, 200);
        return;
    }

    var splash = document.getElementById('splashContent');
    var title = document.getElementById('splashTitle');
    var text = document.getElementById('splashText');
    var navTop = document.getElementById('furnitureBtn');
    var navBottom = document.getElementById('restorationBtn');

    if (!navTop || !navBottom) {
        setTimeout(window.initSplitScreen, 200);
        return;
    }

    var isOpening = false;

    function openDoors(side) {
        if (isOpening) return;
        isOpening = true;

        // Splash içeriği
        title.textContent = side === 'furniture' ? '🪑 Furniture' : '🔧 Restoration';
        text.textContent = side === 'furniture' ? 'El yapımı ahşap mobilyalar' : 'Ahşap restorasyon hizmetleri';

        // Hepsini aynı anda aç
        splash.classList.add('visible');
        container.classList.add('open');
        navTop.classList.add('slide-left');      // yeşil → sola
        navBottom.classList.add('slide-right');   // kırmızı → sağa

        setTimeout(function () {
            window.location.href = '/' + side;
        }, 2500);
    }

    navTop.onclick = function () { openDoors('furniture'); };
    navBottom.onclick = function () { openDoors('restoration'); };

    console.log('✅ SplitScreen initialized');
};
// Furniture butonuna tıklanınca animasyon
document.getElementById('furniture-btn').addEventListener('click', function (event) {
    event.preventDefault();
    document.querySelector('.relative').classList.add('open');

    // 1 saniye sonra Furniture sayfasına yönlendirme
    setTimeout(function () {
        window.location.href = '/furniture';
    }, 1000);
});

// Restoration butonuna tıklanınca animasyon
document.getElementById('restoration-btn').addEventListener('click', function (event) {
    event.preventDefault();
    document.querySelector('.relative').classList.add('open');

    // 1 saniye sonra Restoration sayfasına yönlendirme
    setTimeout(function () {
        window.location.href = '/restoration';
    }, 1000);
});



# WoodenFurnitureRestoration

Ahşap Mobilya Restorasyon Yönetim Sistemi

## İçindekiler
- [Proje Hakkında](#proje-hakkında)
- [Restorasyon Projeleri Yönetimi](#restorasyon-projeleri-yönetimi)
  - [Proje Nasıl Eklenir](#proje-nasıl-eklenir)
  - [Proje Nasıl Düzenlenir](#proje-nasıl-düzenlenir)
  - [Proje Nasıl Silinir](#proje-nasıl-silinir)
- [Kurulum](#kurulum)

## Proje Hakkında

WoodenFurnitureRestoration, ahşap mobilya restorasyon işlemlerini yönetmek için geliştirilmiş bir web uygulamasıdır. Bu sistem ile restorasyon projelerini ekleyebilir, düzenleyebilir, görüntüleyebilir ve silebilirsiniz.

## Restorasyon Projeleri Yönetimi

### Proje Nasıl Eklenir

1. Admin paneline giriş yapın
2. Sol menüden **"Restorasyonlar"** sekmesine tıklayın
3. Sağ üst köşedeki **"Yeni Restorasyon"** butonuna tıklayın
4. Açılan formda gerekli bilgileri doldurun:
   - **Restorasyon Adı** (zorunlu)
   - **Açıklama**
   - **Fiyat** (zorunlu)
   - **Kategori** (zorunlu)
   - **Durum** (Bekliyor, Devam Ediyor, Tamamlandı, İptal)
   - **Görsel URL**
5. **"Kaydet"** butonuna tıklayın
6. İşlem başarılı olduğunda yeşil bir bildirim görüntülenecektir

### Proje Nasıl Düzenlenir

1. Admin panelinde **"Restorasyonlar"** sayfasına gidin
2. Düzenlemek istediğiniz projenin satırındaki **mavi kalem (✏️) ikonuna** tıklayın
3. Açılan formda değiştirmek istediğiniz alanları güncelleyin
4. **"Kaydet"** butonuna tıklayın
5. İşlem başarılı olduğunda güncelleme bildirimi görüntülenecektir

### Proje Nasıl Silinir

Restorasyon projelerini silmek için aşağıdaki adımları izleyin:

1. Admin paneline giriş yapın (`/admin` adresinden)
2. Sol menüden **"Restorasyonlar"** sekmesine tıklayın
3. Silmek istediğiniz projeyi listede bulun
4. İşlemler sütunundaki **kırmızı çöp kutusu (🗑️) ikonuna** tıklayın
5. Açılan onay penceresinde:
   - Proje adını ve detaylarını kontrol edin
   - **"Sil"** butonuna tıklayarak onaylayın
   - İptal etmek isterseniz **"İptal"** butonuna tıklayın
6. Silme işlemi başarılı olduğunda:
   - Yeşil bir bildirim görüntülenir
   - Proje listeden kaldırılır
   - Sayfa otomatik olarak güncellenir

**Önemli Notlar:**
- Silinen projeler veritabanında "soft delete" (yumuşak silme) yöntemiyle işaretlenir
- Silinen projeler kullanıcı arayüzünde görünmez ancak veritabanında korunur
- Bu işlem geri alınamaz, dikkatli olun
- Silme işlemi sırasında herhangi bir hata oluşursa kırmızı bir hata bildirimi görüntülenecektir

### Proje Detaylarını Görüntüleme

1. Herhangi bir projenin satırındaki **mavi göz (👁️) ikonuna** tıklayın
2. Açılan pencerede projenin tüm detaylarını görebilirsiniz:
   - Proje görseli
   - Restorasyon adı ve açıklaması
   - Kategori bilgisi
   - Fiyat
   - Durum
   - Başlangıç ve bitiş tarihleri

### Proje Filtreleme

Restorasyon projelerini aşağıdaki kriterlere göre filtreleyebilirsiniz:

- **Kategori**: Belirli bir kategorideki projeleri görüntüleyin
- **Durum**: Devam Eden, Bekleyen, Tamamlanan veya İptal Edilmiş projeleri filtreleyin
- Filtreleri temizlemek için **"Temizle"** butonuna tıklayın

## Kurulum

### Gereksinimler
- .NET 8.0 SDK
- SQL Server veya uyumlu veritabanı
- Visual Studio 2022 veya VS Code

### Adımlar
1. Repository'yi klonlayın
2. `appsettings.json` dosyasında veritabanı bağlantı dizesini yapılandırın
3. Migration'ları çalıştırın: `dotnet ef database update`
4. API projesini çalıştırın: `dotnet run --project WoodenFurnitureRestoration.API`
5. Blazor projesini çalıştırın: `dotnet run --project WoodenFurnitureRestoration.Blazor`

## Teknolojiler

- **Backend**: ASP.NET Core 8.0 Web API
- **Frontend**: Blazor Server
- **Database**: Entity Framework Core
- **ORM**: Entity Framework Core
- **Architecture**: Clean Architecture (Repository Pattern, Unit of Work)

## Lisans

Bu proje eğitim amaçlı geliştirilmiştir.
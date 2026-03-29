# 📦 Enoca .NET Challenge 

Merhabalar, ben Hamza Almaz. Bu proje, **Enoca .NET Backend Stajyer Değerlendirme Süreci (Study Case)** kapsamında tarafımca geliştirilmiştir. 

Projenin temel amacı; kargo firmalarının ve bu firmalara ait sipariş süreçlerinin yönetilmesini sağlayan bir RESTful API servisi sunmaktır. Geliştirme sürecinde sadece temel CRUD işlemlerine bağlı kalınmamış; modern yazılım mühendisliği prensipleri, sürdürülebilir mimari yaklaşımları ve asenkron arka plan görevleri (Background Jobs) projeye entegre edilmiştir.

## 🚀 Kullanılan Teknolojiler ve Araçlar

* **Framework:** .NET 6.0 / ASP.NET Core Web API
* **Mimari Yaklaşım:** Onion Architecture (Katmanlı Mimari)
* **Veritabanı & ORM:** MS SQL Server & Entity Framework Core (Code-First)
* **Arka Plan İş Yönetimi:** Hangfire
* **API Dokümantasyonu:** Swagger (OpenAPI)
* **Validasyon:** FluentValidation

## 🏗️ Mimari Tasarım (Onion Architecture)

Proje, bağımlılıkların dışarıdan içeriye doğru (Dependency Inversion) aktığı ve iş kurallarının izole edildiği Onion Architecture prensiplerine göre 3 ana katmanda kurgulanmıştır:

1. **Core (Domain & Application):** İş kurallarının, Entity'lerin, DTO'ların ve Repository/Service arayüzlerinin (Interfaces) bulunduğu, dış çerçevelere (framework) bağımlılığı olmayan merkez katmandır.
2. **Infrastructure:** Veritabanı bağlamının (DbContext), Repository implementasyonlarının ve Hangfire görevlerinin (ReportJob) bulunduğu, veri erişim altyapısını sağlayan katmandır.
3. **WebApi:** Kullanıcı isteklerini (HTTP Requests) karşılayan Controller'ların, Dependency Injection (DI) yaşam döngülerinin ve Middleware'lerin yapılandırıldığı sunum katmanıdır.

## ⭐ Öne Çıkan Geliştirmeler

Staj projesi isterlerine ek olarak, sistemin gerçek hayat senaryolarına uygunluğunu artırmak amacıyla aşağıdaki geliştirmeler yapılmıştır:

* **Otomatik Görev Yönetimi (Hangfire):** Sistemde `ReportJob` adında saatlik (Hourly) tetiklenen bir arka plan görevi kurgulanmıştır. Bu görev; o günkü siparişleri kargo firması bazında asenkron olarak gruplar, maliyetleri hesaplar ve `CarrierReports` tablosuna rapor olarak kaydeder/günceller.
* **Performans Optimizasyonu:** Veritabanı sorgularında (LINQ) in-memory filtreleme yerine doğrudan SQL Server'a yansıyacak optimize sorgular (`.Where` filtrelerinin `ToListAsync` öncesinde kullanılması vb.) tercih edilmiştir.
* **Bağımlılıkların Yönetimi (DI):** Servis ve Repository katmanları arayüzler (Interfaces) üzerinden gevşek bağlı (Loosely Coupled) olarak tasarlanmış ve Dependency Injection ile sisteme dahil edilmiştir.

## 🛠️ Kurulum ve Çalıştırma Yönergesi

Projeyi kendi ortamınızda test etmek için aşağıdaki adımları izleyebilirsiniz:

1. **Projeyi Klonlayın:**
   git clone https://github.com/HamzaAlmaz/.enocaDotNetChallange.git

2. **Veritabanı Bağlantısını Ayarlayın:**
   `WebApi` katmanında bulunan `appsettings.json` dosyasını açarak `DefaultConnection` alanına kendi SQL Server adresinizi giriniz.

3. **Veritabanını Ayağa Kaldırın (Migration):**
   Visual Studio'da `Package Manager Console` (PMC) penceresini açın, "Default project" olarak `Infrastructure` katmanını seçin ve şu komutu çalıştırın:
   Update-Database

4. **Projeyi Başlatın:**
   `WebApi` projesini "Startup Project" olarak ayarlayın ve çalıştırın.

## 🧭 İnceleme Ekranları (Endpoints)

Proje çalıştığında aşağıdaki arayüzler üzerinden sistemi deneyimleyebilirsiniz:

* **Swagger API Paneli:** `https://localhost:<port>/swagger` (Endpoint'leri test etmek ve yeni sipariş/kargo firması eklemek için)
* **Hangfire Dashboard:** `https://localhost:<port>/hangfire` (Saatlik çalışan maliyet raporlama görevini izlemek ve manuel (Trigger Now) tetiklemek için)

---
*Vakit ayırıp projemi incelediğiniz için teşekkür ederim.*

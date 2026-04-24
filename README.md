🚀 SmartQuiz: AI-Powered Examination System
SmartQuiz, eğitimcilerin ve kurumların dokümanlarını yapay zeka ile saniyeler içinde profesyonel sınavlara dönüştürmesini sağlayan, Multi-tenant (Çoklu Kiracılı) mimariye sahip kapsamlı bir Backend projesidir.

🌟 Öne Çıkan Özellikler
AI Quiz Generation: Yüklenen dokümanlardan (PDF, DOCX, PPTX) veya serbest metinlerden otomatik soru üretimi.

Word Export: Üretilen soruların profesyonel bir Word belgesi olarak çıktı alınabilmesi.

Multi-Tenancy: Farklı kurumların (Tenants) kendi verilerini izole bir şekilde yönetebilmesi.

Advanced Auth: Role-based (RBAC) yetkilendirme (SuperAdmin, Admin, User).

N-Tier Architecture: Temiz kod prensiplerine uygun katmanlı mimari yapısı.

🛠️ Kullanılan Teknolojiler
Backend: .NET 8 Web API

Frameworks: Autofac (Dependency Injection/AOP), Entity Framework Core

Security: JWT (JSON Web Token), Hashing Helpers

Database: PostgreSQL / MSSQL (N-Tier uyumlu)

AI Integration: Custom AI Prompt Templates & Document Analysis

📁 Proje Yapısı (N-Tier)
Core: Projenin her yerinde kullanılan evrensel araçlar, cross-cutting concerns (Caching, Validation, Performance), güvenlik altyapısı ve temel entity arayüzleri.

WebAPI (Business & Data Access dahil): API Controller'ları, iş mantığı (Business Logic), veritabanı göçleri (Migrations) ve veri erişim katmanı.

🔌 API Endpointleri (Özet)
🤖 AI (Yapay Zeka Servisleri)
POST /api/AI/generate: Doküman veya prompt üzerinden sınav oluşturur.

POST /api/AI/generate-word: Sınavı Word dosyasına döker.

GET /api/AI/prompt-templates: Hazır prompt şablonlarını listeler.

📄 Document (Doküman Yönetimi)
POST /api/Document/upload: PDF, Word veya PowerPoint dosyalarını sisteme yükler.

GET /api/Document/getbytenantid: Kuruma özel belgeleri listeler.

👥 Users & Auth
POST /api/Users/register: Yeni kullanıcı ve tenant kaydı.

POST /api/Users/login: JWT tabanlı güvenli giriş.

🏢 Tenant & Quiz Management
Tenant: Kurum bazlı yönetim ve aktivasyon işlemleri.

Quiz / Question / Answer: Soru bankası ve sınav süreçlerinin tam yönetimi (CRUD).

🚀 Kurulum
Projeyi klonlayın: git clone https://github.com/furkandogan6393/SmartQuiz.git

appsettings.json dosyasındaki Connection String ve JWT ayarlarını yapılandırın.

Terminalde Update-Database veya dotnet ef database update komutunu çalıştırın.

Projeyi başlatın ve https://localhost:[PORT]/swagger adresinden API'yi test edin.

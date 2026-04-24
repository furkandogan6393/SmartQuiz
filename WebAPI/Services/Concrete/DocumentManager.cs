using Core.Entities.Concrete;
using Core.Utilities.Results;
using System.Security.Claims;
using WebAPI.DataAccess.Abstract;
using WebAPI.Services.Abstract;

namespace WebAPI.Services.Concrete
{
    public class DocumentManager : IDocumentService
    {

        readonly IDocumentDal _documentDal;
        readonly IWebHostEnvironment _env;
        public DocumentManager(IDocumentDal documentDal, IWebHostEnvironment env)
        {
            _documentDal = documentDal;
            _env = env;
        }

        public async Task<IDataResult<Document>> AddAsync(IFormFile file, ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var tenantId = user.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;

            if (userId == null)
                return new ErrorDataResult<Document>("Kullanıcı bulunamadı");

            if (file == null || file.Length == 0)
                return new ErrorDataResult<Document>("Dosya boş olamaz");

            var allowedExtensions = new[] { ".pdf", ".doc", ".docx", ".pptx" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return new ErrorDataResult<Document>("Sadece PDF, Word ve PowerPoint dosyası yüklenebilir");

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var document = new Document
            {
                FileName = file.FileName,
                FilePath = filePath,
                UserId = userId,
                TenantId = tenantId,
                UploadedAt = DateTime.UtcNow
            };

            await _documentDal.Add(document);
            return new SuccessDataResult<Document>(document, "Döküman Yüklendi");
        }

        public async Task<IDataResult<Document>> DeleteAsync(int id)
        {
            var deletedDocument = await _documentDal.Get(d => d.Id == id);
            if(deletedDocument == null)
                return new ErrorDataResult<Document>(null, "Silmeye Çalıştığınız Döküman Bulunmamaktadır");
            await _documentDal.Delete(deletedDocument);
            return new SuccessDataResult<Document>(deletedDocument, "Bu Döküman Silindi");

        }

        public async Task<IDataResult<List<Document>>> GetAllAsync()
        {
            var allDocument = await _documentDal.GetAll();
            if (allDocument == null)
                return new ErrorDataResult<List<Document>>(null, "Hiç Dökümanınız Bulunmuyor.");
            return new SuccessDataResult<List<Document>>(allDocument, "Dökümanlar Getirildi");
        }

        public async Task<IDataResult<Document>> GetByIdAsync(int id)
        {
            var getById = await _documentDal.Get(d => d.Id == id);
            if (getById == null)
                return new ErrorDataResult<Document>(null, "Böyle Bir Döküman Yok");
            return new SuccessDataResult<Document>(getById, "Döküman Getirildi");
        }

        public async Task<IDataResult<List<Document>>> GetByUserIdAsync(string userId)
        {
            var getByUserId = await _documentDal.GetDocumentByUserId(userId);
            if (getByUserId == null)
                return new ErrorDataResult<List<Document>>(null, "Dökümanınız Yoğ");
            return new SuccessDataResult<List<Document>>(getByUserId, "Dökümanlar Listelendi");
        }

        public async Task<IDataResult<List<Document>>> GetByTenantIdAsync(string tenantId)
        {
            var documents = await _documentDal.GetDocumentsByTenantIdAsync(tenantId);
            if (documents == null)
                return new ErrorDataResult<List<Document>>(null, "Döküman Bulunamadı");
            return new SuccessDataResult<List<Document>>(documents, "Dökümanlar Getirildi");
        }
    }
}

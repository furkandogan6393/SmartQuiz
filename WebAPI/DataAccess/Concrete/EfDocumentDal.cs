using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebAPI.DataAccess.Abstract;
using Document = Core.Entities.Concrete.Document;
namespace WebAPI.DataAccess.Concrete
{
    public class EfDocumentDal : EfEntityRepositoryBase<Document, UserDbContext>, IDocumentDal
    {
        public EfDocumentDal(UserDbContext context) : base(context)
        {
        }

        public async Task<List<Document>> GetDocumentByUserId(string userId)
        {
            return await _context.Documents
            .Where(d => d.UserId == userId).ToListAsync();
        }

        public async Task<List<Document>> GetDocumentsByTenantIdAsync(string tenantId)
        {
            return await _context.Documents
                .Where(d => d.TenantId == tenantId)
                .ToListAsync();
        }


    }
}

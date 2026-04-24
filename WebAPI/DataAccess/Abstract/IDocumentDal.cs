using Core.DataAccess;
using Core.Entities.Concrete;
using System.Reflection.Metadata;
using Document = Core.Entities.Concrete.Document;

namespace WebAPI.DataAccess.Abstract
{
    public interface IDocumentDal : IEntityRepository<Document>
    {
        Task <List<Document>> GetDocumentByUserId(string userId);
        Task<List<Document>> GetDocumentsByTenantIdAsync(string tenantId);

    }
}

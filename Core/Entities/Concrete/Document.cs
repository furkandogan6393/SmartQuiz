using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Document : IEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UserId { get; set; }
        public string TenantId { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dto
{
    public class RegisterDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TenantId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
    
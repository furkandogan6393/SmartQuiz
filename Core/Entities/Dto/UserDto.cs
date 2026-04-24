using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dto
{
    public class UserDto : IDto
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TenantId { get; set; }
    }
}

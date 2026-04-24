using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Dto
{
    public class LoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

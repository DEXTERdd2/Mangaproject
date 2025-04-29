using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaBackend_Application.DTOS.AuthDtos
{
    public class LoginRequest
    {
       
        public string Email { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}

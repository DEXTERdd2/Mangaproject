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

        public string? usernameOrEmail { get; set; }
        public string? password { get; set; }
    }
}

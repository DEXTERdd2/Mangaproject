using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaBackend_Infrastructure.Auth.JWT
{
    public class JWTSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AuthTokenDurationInMinutes { get; set; }
        public double RefreshTokenDurationInDays { get; set; }
    }
}

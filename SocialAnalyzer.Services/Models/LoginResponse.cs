using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public SignInResult Result { get; set; }
        public string ResultMessage { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Rol { get; set; }
        public int tiempoExpire { get; set; }




        public int Id { get; set; }
        public string UserName { get; set; }
    }
}

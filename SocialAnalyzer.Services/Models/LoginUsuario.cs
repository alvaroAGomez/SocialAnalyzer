using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Models
{
    public class LoginUsuario
    {
        public int Id { get; set; }


        public int? LoginAttempts { get; set; }

        public DateTime? FechaUltimoIntento { get; set; }

        public string ResultMessage { get; set; }
    }
}

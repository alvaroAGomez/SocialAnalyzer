using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre{ get; set; }
        public string Apellido { get; set; }

        public string UserName { get; set; }
        public string EsInterno { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public int? IdEstado { get; set; }
        public int? IdRol { get; set; }

        public Rol Rol { get; set; }
        public EstadoUsuario Estado { get; set; }

        public LoginUsuario LoginUsuario { get; set; }

        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }


        public string ResultMessage { get; set; }

    }
}

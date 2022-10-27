using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Models
{
    public class UsuarioNewDTO
    {
        public int? IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string EsInterno { get; set; }
        public string Password { get; set; }
        public int? IdEstado { get; set; }
        public int? IdRol { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaBaja { get; set; }
        public string Username { get; set; }
    }
}

using SocialAnalyzer.DAL.Models;
using System;

namespace SocialAnalyzer.Models
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string EsInterno { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Username { get; set; }
        public string ResultMessage { get; set; }
        public virtual EstadoUsuarioDTO Estado { get; set; }
        public virtual RolDTO Rol { get; set; }
    }
}

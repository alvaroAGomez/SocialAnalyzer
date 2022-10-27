using SocialAnalyzer.DAL.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class Usuario : IEntity
    {
        public Usuario()
        {
            Planxusuarios = new HashSet<Planxusuario>();
        }

        public int IdUsuario { get; set; }
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
       // public string ResultMessage { get; set; }

        public virtual EstadoUsuario Estado { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Planxusuario> Planxusuarios { get; set; }
    }
}

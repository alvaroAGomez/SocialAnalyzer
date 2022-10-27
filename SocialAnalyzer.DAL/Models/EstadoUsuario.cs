using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class EstadoUsuario
    {
        public EstadoUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}

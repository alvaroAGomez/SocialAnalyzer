using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class EstadoPlanxusuario
    {
        public EstadoPlanxusuario()
        {
            Planxusuarios = new HashSet<Planxusuario>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Planxusuario> Planxusuarios { get; set; }
    }
}

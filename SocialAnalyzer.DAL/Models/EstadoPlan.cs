using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class EstadoPlan
    {
        public EstadoPlan()
        {
            Plans = new HashSet<Plan>();
        }

        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }
    }
}

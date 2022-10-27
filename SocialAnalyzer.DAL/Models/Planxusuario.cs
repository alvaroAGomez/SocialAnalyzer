using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class Planxusuario
    {
        public int IdPlan { get; set; }
        public int IdUsuario { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }
        public decimal? PrecioPago { get; set; }
        public int? IdEstado { get; set; }

        public virtual EstadoPlanxusuario IdEstadoNavigation { get; set; }
        public virtual Plan IdPlanNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual Pago Pago { get; set; }
    }
}

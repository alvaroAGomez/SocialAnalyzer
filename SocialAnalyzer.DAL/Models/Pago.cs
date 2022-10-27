using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class Pago
    {
        public int IdPlan { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoPago { get; set; }
        public int? NumeroOrden { get; set; }

        public virtual Planxusuario Id { get; set; }
    }
}

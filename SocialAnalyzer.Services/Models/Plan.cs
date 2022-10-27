using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.Services.Models
{
    public class Plan
    {
        public int IdPlan { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? MesesVigencia { get; set; }
        public decimal? Precio { get; set; }
        public int? IdEstado { get; set; }
        public string ResultMessage { get; set; }

    }
}

using SocialAnalyzer.DAL.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public  class Plan : IEntity
    {
        public Plan()
        {
            Planxusuarios = new HashSet<Planxusuario>();
        }

        public int IdPlan { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? MesesVigencia { get; set; }
        public decimal? Precio { get; set; }
        public int? IdEstado { get; set; }

        public virtual EstadoPlan IdEstadoNavigation { get; set; }
        public virtual ICollection<Planxusuario> Planxusuarios { get; set; }


        //public int? Id
        //{
        //    get { return IdPlan; }
        //    set { IdPlan = (int)value; }
        //}
    }
}

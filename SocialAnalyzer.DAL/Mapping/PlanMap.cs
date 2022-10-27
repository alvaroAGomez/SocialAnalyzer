using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Mapping
{
    public class PlanMap : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(c => c.IdPlan);
            builder.Property(c => c.Nombre);
            builder.Property(c => c.Descripcion);
            builder.Property(c => c.MesesVigencia);
            builder.Property(c => c.Precio);
            builder.Property(c => c.IdEstado);


        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Mapping
{
    public class LoginUsuariosMap : IEntityTypeConfiguration<LoginUsuarios>
    {
        public void Configure(EntityTypeBuilder<LoginUsuarios> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IdUsuario);
            builder.Property(c => c.FechaUltimoIntento);
        }
    }
}

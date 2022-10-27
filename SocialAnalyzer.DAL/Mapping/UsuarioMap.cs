using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialAnalyzer.DAL.Interfaces;
using SocialAnalyzer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAnalyzer.DAL.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entity)
        {
            entity.HasKey(e => e.IdUsuario)
                               .HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.IdEstado, "estado_usuario");

            entity.HasIndex(e => e.IdRol, "usuario_rol_idx");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.Property(e => e.Apellido)
                .HasMaxLength(60)
                .HasColumnName("apellido");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("email");

            entity.Property(e => e.EsInterno)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("es_interno");

            entity.Property(e => e.FechaAlta).HasColumnName("fecha_alta");

            entity.Property(e => e.FechaBaja).HasColumnName("fecha_baja");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnName("nombre");

            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .HasColumnName("password");

            entity.HasOne(d => d.Estado)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("usuario_estado");

            entity.HasOne(d => d.Rol)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("usuario_rol");
        }
    }
}



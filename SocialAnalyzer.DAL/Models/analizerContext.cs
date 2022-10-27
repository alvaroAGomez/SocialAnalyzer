using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SocialAnalyzer.DAL.Models
{
    public partial class analizerContext : DbContext
    {


        public analizerContext(DbContextOptions<analizerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EstadoPlan> EstadoPlans { get; set; }
        public virtual DbSet<EstadoPlanxusuario> EstadoPlanxusuarios { get; set; }
        public virtual DbSet<EstadoUsuario> EstadoUsuarios { get; set; }
        public virtual DbSet<Pago> Pagos { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<PlanPermiso> PlanPermisos { get; set; }
        public virtual DbSet<Planxusuario> Planxusuarios { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstadoPlan>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estado_plan");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(3000)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<EstadoPlanxusuario>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estado_planxusuario");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(3000)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<EstadoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdEstado)
                    .HasName("PRIMARY");

                entity.ToTable("estado_usuario");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(3000)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => new { e.IdPlan, e.IdUsuario })
                    .HasName("PRIMARY");

                entity.ToTable("pago");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.FechaPago)
                    .HasColumnType("date")
                    .HasColumnName("fecha_pago");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_vencimiento");

                entity.Property(e => e.MontoPago)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("monto_pago");

                entity.Property(e => e.NumeroOrden).HasColumnName("numero_orden");

                entity.HasOne(d => d.Id)
                    .WithOne(p => p.Pago)
                    .HasForeignKey<Pago>(d => new { d.IdPlan, d.IdUsuario })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pago_planxusuario");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.IdPlan)
                    .HasName("PRIMARY");

                entity.ToTable("plan");

                entity.HasIndex(e => e.IdEstado, "estado_plan");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(3000)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.MesesVigencia).HasColumnName("meses_vigencia");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("plan_estado");
            });

            modelBuilder.Entity<PlanPermiso>(entity =>
            {
                entity.HasKey(e => e.IdPlan)
                    .HasName("PRIMARY");

                entity.ToTable("plan_permisos");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.Property(e => e.Permiso1)
                    .HasColumnType("bit(1)")
                    .HasColumnName("permiso1");

                entity.Property(e => e.Permiso2)
                    .HasColumnType("bit(1)")
                    .HasColumnName("permiso2");

                entity.Property(e => e.Permiso3)
                    .HasColumnType("bit(1)")
                    .HasColumnName("permiso3");

                entity.Property(e => e.Permiso4)
                    .HasColumnType("bit(1)")
                    .HasColumnName("permiso4");

                entity.Property(e => e.Permiso5)
                    .HasColumnType("bit(1)")
                    .HasColumnName("permiso5");

                entity.Property(e => e.Permiso6)
                    .HasColumnType("bit(1)")
                    .HasColumnName("permiso6");
            });

            modelBuilder.Entity<Planxusuario>(entity =>
            {
                entity.HasKey(e => new { e.IdPlan, e.IdUsuario })
                    .HasName("PRIMARY");

                entity.ToTable("planxusuario");

                entity.HasIndex(e => e.IdEstado, "estado_planxusuario");

                entity.HasIndex(e => e.IdUsuario, "planxusuario_usuario");

                entity.Property(e => e.IdPlan).HasColumnName("id_plan");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");

                entity.Property(e => e.PrecioPago)
                    .HasColumnType("decimal(8,2)")
                    .HasColumnName("precio_pago");

                entity.Property(e => e.VigenciaDesde)
                    .HasColumnType("date")
                    .HasColumnName("vigencia_desde");

                entity.Property(e => e.VigenciaHasta)
                    .HasColumnType("date")
                    .HasColumnName("vigencia_hasta");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Planxusuarios)
                    .HasForeignKey(d => d.IdEstado)
                    .HasConstraintName("planxusuario_estado");

                entity.HasOne(d => d.IdPlanNavigation)
                    .WithMany(p => p.Planxusuarios)
                    .HasForeignKey(d => d.IdPlan)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("planxusuario_plan");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Planxusuarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("planxusuario_usuario");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PRIMARY");

                entity.ToTable("rol");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(3000)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
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
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

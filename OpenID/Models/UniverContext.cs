using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OpenID.Models
{
    public partial class UniverContext : DbContext
    {
        public virtual DbSet<Alumno> Alumno { get; set; }
        public virtual DbSet<AlumnoInscrito> AlumnoInscrito { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<OfertaEducativa> OfertaEducativa { get; set; }
        public virtual DbSet<OpenIddictApplications> OpenIddictApplications { get; set; }
        public virtual DbSet<OpenIddictAuthorizations> OpenIddictAuthorizations { get; set; }
        public virtual DbSet<OpenIddictScopes> OpenIddictScopes { get; set; }
        public virtual DbSet<OpenIddictTokens> OpenIddictTokens { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioDetalle> UsuarioDetalle { get; set; }

        public UniverContext(DbContextOptions<UniverContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseOpenIddict();

            modelBuilder.Entity<Alumno>(entity =>
            {
                entity.HasIndex(e => e.GeneroId)
                    .HasName("IX_Alumno_GeneroId");

                entity.Property(e => e.AlumnoId).ValueGeneratedNever();

                entity.Property(e => e.GeneroId).HasDefaultValueSql("0");

                entity.Property(e => e.Nombre).IsRequired();

                entity.HasOne(d => d.Genero)
                    .WithMany(p => p.Alumno)
                    .HasForeignKey(d => d.GeneroId);
            });

            modelBuilder.Entity<AlumnoInscrito>(entity =>
            {
                entity.HasKey(e => new { e.AlumnoId, e.OfertaEducativaId })
                    .HasName("PK_AlumnoInscrito");

                entity.HasIndex(e => e.OfertaEducativaId)
                    .HasName("IX_AlumnoInscrito_OfertaEducativaId");

                entity.HasOne(d => d.Alumno)
                    .WithMany(p => p.AlumnoInscrito)
                    .HasForeignKey(d => d.AlumnoId);

                entity.HasOne(d => d.OfertaEducativa)
                    .WithMany(p => p.AlumnoInscrito)
                    .HasForeignKey(d => d.OfertaEducativaId);

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AlumnoInscrito)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_AlumnoInscrito_Usuario");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.Property(e => e.GeneroId).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).IsRequired();
            });

            modelBuilder.Entity<OfertaEducativa>(entity =>
            {
                entity.Property(e => e.OfertaEducativaId).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).IsRequired();
            });

            modelBuilder.Entity<OpenIddictApplications>(entity =>
            {
                entity.HasIndex(e => e.ClientId)
                    .HasName("IX_OpenIddictApplications_ClientId")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<OpenIddictAuthorizations>(entity =>
            {
                entity.HasIndex(e => e.ApplicationId)
                    .HasName("IX_OpenIddictAuthorizations_ApplicationId");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.ApplicationId).HasMaxLength(450);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.OpenIddictAuthorizations)
                    .HasForeignKey(d => d.ApplicationId);
            });

            modelBuilder.Entity<OpenIddictScopes>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(450);
            });

            modelBuilder.Entity<OpenIddictTokens>(entity =>
            {
                entity.HasIndex(e => e.ApplicationId)
                    .HasName("IX_OpenIddictTokens_ApplicationId");

                entity.HasIndex(e => e.AuthorizationId)
                    .HasName("IX_OpenIddictTokens_AuthorizationId");

                entity.Property(e => e.Id).HasMaxLength(450);

                entity.Property(e => e.ApplicationId).HasMaxLength(450);

                entity.Property(e => e.AuthorizationId).HasMaxLength(450);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.OpenIddictTokens)
                    .HasForeignKey(d => d.ApplicationId);

                entity.HasOne(d => d.Authorization)
                    .WithMany(p => p.OpenIddictTokens)
                    .HasForeignKey(d => d.AuthorizationId);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.UsuarioId).HasDefaultValueSql("0");

                entity.Property(e => e.Nombre).IsRequired();

                entity.Property(e => e.Paterno)
                    .IsRequired()
                    .HasDefaultValueSql("N''");
            });

            modelBuilder.Entity<UsuarioDetalle>(entity =>
            {
                entity.HasKey(e => e.UsuarioId)
                    .HasName("PK_UsuarioDetalle");

                entity.Property(e => e.UsuarioId).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.UsuarioDetalle)
                    .HasForeignKey<UsuarioDetalle>(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_UsuarioDetalle_Usuario");
            });
        }
    }
}
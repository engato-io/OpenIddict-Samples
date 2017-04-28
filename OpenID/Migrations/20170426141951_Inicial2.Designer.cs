using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OpenID.Models;

namespace OpenID.Migrations
{
    [DbContext(typeof(UniverContext))]
    [Migration("20170426141951_Inicial2")]
    partial class Inicial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OpenID.Models.Alumno", b =>
                {
                    b.Property<int>("AlumnoId");

                    b.Property<int>("GeneroId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("AlumnoId");

                    b.HasIndex("GeneroId")
                        .HasName("IX_Alumno_GeneroId");

                    b.ToTable("Alumno");
                });

            modelBuilder.Entity("OpenID.Models.AlumnoInscrito", b =>
                {
                    b.Property<int>("AlumnoId");

                    b.Property<int>("OfertaEducativaId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("AlumnoId", "OfertaEducativaId")
                        .HasName("PK_AlumnoInscrito");

                    b.HasIndex("OfertaEducativaId")
                        .HasName("IX_AlumnoInscrito_OfertaEducativaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("AlumnoInscrito");
                });

            modelBuilder.Entity("OpenID.Models.Genero", b =>
                {
                    b.Property<int>("GeneroId");

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.HasKey("GeneroId");

                    b.ToTable("Genero");
                });

            modelBuilder.Entity("OpenID.Models.OfertaEducativa", b =>
                {
                    b.Property<int>("OfertaEducativaId");

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.HasKey("OfertaEducativaId");

                    b.ToTable("OfertaEducativa");
                });

            modelBuilder.Entity("OpenID.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("0");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("Paterno")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("N''");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientSecret");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("Scope");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("Subject");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("OpenID.Models.Alumno", b =>
                {
                    b.HasOne("OpenID.Models.Genero", "Genero")
                        .WithMany("Alumno")
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenID.Models.AlumnoInscrito", b =>
                {
                    b.HasOne("OpenID.Models.Alumno", "Alumno")
                        .WithMany("AlumnoInscrito")
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenID.Models.OfertaEducativa", "OfertaEducativa")
                        .WithMany("AlumnoInscrito")
                        .HasForeignKey("OfertaEducativaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OpenID.Models.Usuario", "Usuario")
                        .WithMany("AlumnoInscrito")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK_AlumnoInscrito_Usuario");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });
        }
    }
}

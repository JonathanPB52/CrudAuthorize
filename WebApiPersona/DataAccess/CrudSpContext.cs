using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApiPersona.Models.Data;

namespace WebApiPersona.DataAccess;

public partial class CrudSpContext : DbContext
{
    public readonly IConfiguration _configuration;
    public CrudSpContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public CrudSpContext(DbContextOptions<CrudSpContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<RolUsuario> RolUsuarios { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ConexcionSQL"));
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-HFUFEJR\\SQLEXPRESS;Database=Crud_SP;Trusted_Connection=SSPI;MultipleActiveResultSets=true;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.ToTable("Persona");

            entity.Property(e => e.Documento)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.PrimerApeliido)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.Property(d => d.IdTipoDocumento);
            entity.Property(d => d.Estado);
            //.WithMany(p => p.Personas)
            //    .HasForeignKey(d => d.IdTipoDocumento)
            //    .HasConstraintName("FK_Persona_TipoDocumento");
        });

        modelBuilder.Entity<RolUsuario>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("RolUsuario");

            entity.Property(e => e.NombreRol)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.ToTable("TipoDocumento");

            entity.Property(e => e.Tdocumento)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("TDocumento");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.ContrasenaU)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.NombreU)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.Property(d => d.IdRol);
            entity.Property(d => d.Estado);
            //.WithMany(p => p.Usuarios)
            //    .HasForeignKey(d => d.IdRol)
            //    .HasConstraintName("FK_Usuario_RolUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

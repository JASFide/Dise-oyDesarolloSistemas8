using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace administracionScoutsCR.Models;

public partial class DatabaseScoutContext : DbContext
{
    public DatabaseScoutContext()
    {
    }

    public DatabaseScoutContext(DbContextOptions<DatabaseScoutContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConfirmacionEvento> ConfirmacionEventos { get; set; }

    public virtual DbSet<ContactoEmergencium> ContactoEmergencia { get; set; }

    public virtual DbSet<Etapa> Etapas { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Insignia> Insignias { get; set; }

    public virtual DbSet<ReqInsignium> ReqInsignia { get; set; }

    public virtual DbSet<Seccion> Seccions { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioxContactoEmergencium> UsuarioxContactoEmergencia { get; set; }

    public virtual DbSet<UsuarioxEtapa> UsuarioxEtapas { get; set; }

    public virtual DbSet<UsuarioxInsignium> UsuarioxInsignia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LocalHost;Database=DatabaseScout;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfirmacionEvento>(entity =>
        {
            entity.HasKey(e => e.IdConfirmacionEvento).HasName("PK__Confirma__F2F2F9E04E542794");

            entity.ToTable("ConfirmacionEvento");

            entity.Property(e => e.Asistencia)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.ConfirmacionEventos)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConfirmacionEvento_Eventos");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ConfirmacionEventos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConfirmacionEvento_Usuario");
        });

        modelBuilder.Entity<ContactoEmergencium>(entity =>
        {
            entity.HasKey(e => e.IdContactoEmergencia).HasName("PK__Contacto__3A12A7662C4BB57D");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroContacto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Parentesco)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Etapa>(entity =>
        {
            entity.HasKey(e => e.IdEtapa).HasName("PK__Etapa__E4B65D8E02E46CDA");

            entity.ToTable("Etapa");

            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Seccion)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__Eventos__034EFC043758CED9");

            entity.Property(e => e.ContactoEncargado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Encargado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Lugar)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });


        modelBuilder.Entity<Insignia>(entity =>
        {
            entity.HasKey(e => e.IdInsignia).HasName("PK__Insignia__90F5F482014A20D0");

            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Seccion)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<ReqInsignium>(entity =>
        {
            entity.HasKey(e => e.IdReqInsignia).HasName("PK__ReqInsig__63856F5906174F79");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdInsigniasNavigation).WithMany(p => p.ReqInsignia)
                .HasForeignKey(d => d.IdInsignias)
                .HasConstraintName("FK_ReqInsignia_Insignias");
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.IdSeccion).HasName("PK__Seccion__CD2B049F800113F0");

            entity.ToTable("Seccion");

            entity.Property(e => e.JefeSeccion)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF979FB41479");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Seccion");
        });

        modelBuilder.Entity<UsuarioxContactoEmergencium>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioxContactoEmergencia).HasName("PK__Usuariox__EBAD584FA589C63B");

  

            entity.HasOne(d => d.IdContactoEmergenciaNavigation).WithMany(p => p.UsuarioxContactoEmergencia)
                .HasForeignKey(d => d.IdContactoEmergencia)
                .HasConstraintName("FK_UsuarioxContactoEmergencia_ContactoEmergencia");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioxContactoEmergencia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_UsuarioxContactoEmergencia_Usuario");
        });

        modelBuilder.Entity<UsuarioxEtapa>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioEtapa).HasName("PK__Usuariox__865ED3013973B362");

            entity.ToTable("UsuarioxEtapa");

      

            entity.HasOne(d => d.IdEtapaNavigation).WithMany(p => p.UsuarioxEtapas)
                .HasForeignKey(d => d.IdEtapa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioxEtapa_Etapa");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioxEtapas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioxEtapa_Usuario");
        });

        modelBuilder.Entity<UsuarioxInsignium>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioxInsignias).HasName("PK__Usuariox__C499478003211EB8");

  
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdInsigniaNavigation).WithMany(p => p.UsuarioxInsignia)
                .HasForeignKey(d => d.IdInsignia)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioxInsignia_Insignias");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioxInsignia)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioxInsignia_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

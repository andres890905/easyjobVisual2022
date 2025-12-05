using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace easyjob22.Models;

public partial class EasyjobContext : DbContext
{
    public EasyjobContext()
    {
    }

    public EasyjobContext(DbContextOptions<EasyjobContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Certificacione> Certificaciones { get; set; }

    public virtual DbSet<Incapacidade> Incapacidades { get; set; }

    public virtual DbSet<Programacion> Programacions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Solicitude> Solicitudes { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Traslado> Traslados { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vacacione> Vacaciones { get; set; }

    public virtual DbSet<Zona> Zonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Certificacione>(entity =>
        {
            entity.HasKey(e => e.IdCertificacion).HasName("PRIMARY");

            entity
                .ToTable("certificaciones")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.Property(e => e.IdCertificacion)
                .HasColumnType("int(11)")
                .HasColumnName("id_certificacion");
            entity.Property(e => e.CodigoCertificacion)
                .HasMaxLength(45)
                .HasColumnName("codigo_certificacion");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.FechaExpiracion).HasColumnName("fecha_expiracion");
            entity.Property(e => e.NombreCertificacion)
                .HasColumnType("text")
                .HasColumnName("nombre_certificacion");
        });

        modelBuilder.Entity<Incapacidade>(entity =>
        {
            entity.HasKey(e => e.IdIncapacidad).HasName("PRIMARY");

            entity
                .ToTable("incapacidades")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.Idusuarios, "fk_incapacidades_usuarios1_idx");

            entity.Property(e => e.IdIncapacidad)
                .HasMaxLength(11)
                .HasColumnName("id_incapacidad");
            entity.Property(e => e.ArchivoSoporte)
                .HasMaxLength(255)
                .HasColumnName("archivo_soporte");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Idusuarios)
                .HasColumnType("int(11)")
                .HasColumnName("idusuarios");
            entity.Property(e => e.Motivo)
                .HasMaxLength(255)
                .HasColumnName("motivo");
            entity.Property(e => e.NombreEmpleado)
                .HasMaxLength(255)
                .HasColumnName("nombre_empleado");
            entity.Property(e => e.NombreEps)
                .HasColumnType("text")
                .HasColumnName("nombre_eps");

            entity.HasOne(d => d.IdusuariosNavigation).WithMany(p => p.Incapacidades)
                .HasForeignKey(d => d.Idusuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_incapacidades_usuarios1");
        });

        modelBuilder.Entity<Programacion>(entity =>
        {
            entity.HasKey(e => e.IdProgramacion).HasName("PRIMARY");

            entity
                .ToTable("programacion")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdSupervisor, "fk_programacion_supervisor");

            entity.HasIndex(e => e.Idusuarios, "fk_programacion_usuarios1_idx");

            entity.Property(e => e.IdProgramacion)
                .HasColumnType("bigint(20)")
                .HasColumnName("id_programacion");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.EsDescanso)
                .HasDefaultValueSql("'0'")
                .HasColumnName("es_descanso");
            entity.Property(e => e.EsDominical)
                .HasDefaultValueSql("'0'")
                .HasColumnName("es_dominical");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.HoraEntrada)
                .HasColumnType("time")
                .HasColumnName("hora_entrada");
            entity.Property(e => e.HoraSalida)
                .HasColumnType("time")
                .HasColumnName("hora_salida");
            entity.Property(e => e.HorasExtra)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("'0.00'")
                .HasColumnName("horas_extra");
            entity.Property(e => e.IdSupervisor)
                .HasColumnType("int(11)")
                .HasColumnName("id_supervisor");
            entity.Property(e => e.Idusuarios)
                .HasColumnType("int(11)")
                .HasColumnName("idusuarios");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.IdSupervisorNavigation).WithMany(p => p.ProgramacionIdSupervisorNavigations)
                .HasForeignKey(d => d.IdSupervisor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_programacion_supervisor");

            entity.HasOne(d => d.IdusuariosNavigation).WithMany(p => p.ProgramacionIdusuariosNavigations)
                .HasForeignKey(d => d.Idusuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_programacion_usuario");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRoles).HasName("PRIMARY");

            entity
                .ToTable("roles")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.Property(e => e.IdRoles)
                .HasColumnType("int(11)")
                .HasColumnName("id_roles");
            entity.Property(e => e.TipoRol)
                .HasMaxLength(45)
                .HasColumnName("tipo_rol");
        });

        modelBuilder.Entity<Solicitude>(entity =>
        {
            entity.HasKey(e => new { e.IdCertificacion, e.UsuariosIdusuarios })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity
                .ToTable("solicitudes")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdCertificacion, "fk_certificaciones_has_usuarios_certificaciones1_idx");

            entity.HasIndex(e => e.UsuariosIdusuarios, "fk_certificaciones_has_usuarios_usuarios1_idx");

            entity.Property(e => e.IdCertificacion)
                .HasColumnType("int(11)")
                .HasColumnName("id_certificacion");
            entity.Property(e => e.UsuariosIdusuarios)
                .HasColumnType("int(11)")
                .HasColumnName("usuarios_idusuarios");
            entity.Property(e => e.CargoUsuario)
                .HasMaxLength(100)
                .HasColumnName("cargo_usuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(255)
                .HasColumnName("nombre_usuario");

            entity.HasOne(d => d.IdCertificacionNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.IdCertificacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_certificaciones_has_usuarios_certificaciones1");

            entity.HasOne(d => d.UsuariosIdusuariosNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.UsuariosIdusuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_certificaciones_has_usuarios_usuarios1");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PRIMARY");

            entity
                .ToTable("sucursal")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdZona, "fk_sucursal_zonas");

            entity.Property(e => e.IdSucursal)
                .HasColumnType("int(11)")
                .HasColumnName("id_sucursal");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .HasColumnName("ciudad");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.IdZona)
                .HasColumnType("int(11)")
                .HasColumnName("id_zona");
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .HasColumnName("nombre_sucursal");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Sucursals)
                .HasForeignKey(d => d.IdZona)
                .HasConstraintName("fk_sucursal_zonas");
        });

        modelBuilder.Entity<Traslado>(entity =>
        {
            entity.HasKey(e => new { e.IdTraslados, e.Idusuarios, e.IdSucursalOrigen })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity
                .ToTable("traslados")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdSucursalDestino, "fk_sucursal_destino");

            entity.HasIndex(e => e.IdSucursalOrigen, "fk_traslados_sucursal1_idx");

            entity.HasIndex(e => e.Idusuarios, "fk_traslados_usuarios1_idx");

            entity.Property(e => e.IdTraslados)
                .ValueGeneratedOnAdd()
                .HasColumnType("int(11)")
                .HasColumnName("id_traslados");
            entity.Property(e => e.Idusuarios)
                .HasColumnType("int(11)")
                .HasColumnName("idusuarios");
            entity.Property(e => e.IdSucursalOrigen)
                .HasColumnType("int(11)")
                .HasColumnName("id_sucursal_origen");
            entity.Property(e => e.IdSucursalDestino)
                .HasColumnType("int(11)")
                .HasColumnName("id_sucursal_destino");

            entity.HasOne(d => d.IdSucursalDestinoNavigation).WithMany(p => p.TrasladoIdSucursalDestinoNavigations)
                .HasForeignKey(d => d.IdSucursalDestino)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sucursal_destino");

            entity.HasOne(d => d.IdSucursalOrigenNavigation).WithMany(p => p.TrasladoIdSucursalOrigenNavigations)
                .HasForeignKey(d => d.IdSucursalOrigen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_traslados_sucursal1");

            entity.HasOne(d => d.IdusuariosNavigation).WithMany(p => p.Traslados)
                .HasForeignKey(d => d.Idusuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_traslados_usuarios1");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuarios).HasName("PRIMARY");

            entity
                .ToTable("usuarios")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.IdRoles, "fk_id_roles");

            entity.HasIndex(e => e.IdSucursal, "fk_usuarios_sucursal1_idx");

            entity.Property(e => e.Idusuarios)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("idusuarios");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("apellido");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'ACTIVO'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdRoles)
                .HasColumnType("int(11)")
                .HasColumnName("id_roles");
            entity.Property(e => e.IdSucursal)
                .HasColumnType("int(11)")
                .HasColumnName("id_sucursal");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Salario)
                .HasPrecision(10, 2)
                .HasColumnName("salario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolesNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRoles)
                .HasConstraintName("fk_id_roles");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuarios_sucursal");
        });

        modelBuilder.Entity<Vacacione>(entity =>
        {
            entity.HasKey(e => e.IdVacacion).HasName("PRIMARY");

            entity.ToTable("vacaciones");

            entity.HasIndex(e => e.Idusuarios, "fk_vacaciones_usuarios");

            entity.Property(e => e.IdVacacion)
                .HasColumnType("int(11)")
                .HasColumnName("id_vacacion");
            entity.Property(e => e.Comentarios)
                .HasColumnType("text")
                .HasColumnName("comentarios");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasDefaultValueSql("'Pendiente'")
                .HasColumnName("estado");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("curdate()")
                .HasColumnName("fecha_solicitud");
            entity.Property(e => e.Idusuarios)
                .HasColumnType("int(11)")
                .HasColumnName("idusuarios");

            entity.HasOne(d => d.IdusuariosNavigation).WithMany(p => p.Vacaciones)
                .HasForeignKey(d => d.Idusuarios)
                .HasConstraintName("fk_vacaciones_usuarios");
        });

        modelBuilder.Entity<Zona>(entity =>
        {
            entity.HasKey(e => e.IdZona).HasName("PRIMARY");

            entity
                .ToTable("zonas")
                .HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            entity.HasIndex(e => e.Idusuarios, "idusuarios");

            entity.Property(e => e.IdZona)
                .HasColumnType("int(11)")
                .HasColumnName("id_zona");
            entity.Property(e => e.Idusuarios)
                .HasColumnType("int(11)")
                .HasColumnName("idusuarios");
            entity.Property(e => e.NombreZona)
                .HasMaxLength(100)
                .HasColumnName("nombre_zona");

            entity.HasOne(d => d.IdusuariosNavigation).WithMany(p => p.Zonas)
                .HasForeignKey(d => d.Idusuarios)
                .HasConstraintName("zonas_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

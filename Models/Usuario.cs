using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Usuario
{
    public int Idusuarios { get; set; }

    public int IdSucursal { get; set; }

    public int? IdRoles { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? Contrasena { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public string? Estado { get; set; }

    public decimal? Salario { get; set; }

    public virtual Role? IdRolesNavigation { get; set; }

    public virtual Sucursal? IdSucursalNavigation { get; set; } 

    public virtual ICollection<Incapacidade> Incapacidades { get; set; } = new List<Incapacidade>();

    public virtual ICollection<Programacion> ProgramacionIdSupervisorNavigations { get; set; } = new List<Programacion>();

    public virtual ICollection<Programacion> ProgramacionIdusuariosNavigations { get; set; } = new List<Programacion>();

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();

    public virtual ICollection<Traslado> Traslados { get; set; } = new List<Traslado>();

    public virtual ICollection<Vacacione> Vacaciones { get; set; } = new List<Vacacione>();

    public virtual ICollection<Zona> Zonas { get; set; } = new List<Zona>();
}

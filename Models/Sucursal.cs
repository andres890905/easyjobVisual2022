using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public int? IdZona { get; set; }

    public string NombreSucursal { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual Zona? IdZonaNavigation { get; set; }

    public virtual ICollection<Traslado> TrasladoIdSucursalDestinoNavigations { get; set; } = new List<Traslado>();

    public virtual ICollection<Traslado> TrasladoIdSucursalOrigenNavigations { get; set; } = new List<Traslado>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

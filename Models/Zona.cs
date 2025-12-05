using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Zona
{
    public int IdZona { get; set; }

    public string NombreZona { get; set; } = null!;

    public int? Idusuarios { get; set; }

    public virtual Usuario? IdusuariosNavigation { get; set; }

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();
}

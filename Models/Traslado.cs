using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Traslado
{
    public int IdTraslados { get; set; }

    public int Idusuarios { get; set; }

    public int IdSucursalOrigen { get; set; }

    public int IdSucursalDestino { get; set; }

    public virtual Sucursal IdSucursalDestinoNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalOrigenNavigation { get; set; } = null!;

    public virtual Usuario? IdusuariosNavigation { get; set; }
}

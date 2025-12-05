using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Solicitude
{
    public int IdCertificacion { get; set; }

    public int UsuariosIdusuarios { get; set; }

    public string? NombreUsuario { get; set; }

    public string? CargoUsuario { get; set; }

    public virtual Certificacione IdCertificacionNavigation { get; set; } = null!;

    public virtual Usuario UsuariosIdusuariosNavigation { get; set; } = null!;
}

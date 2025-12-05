using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Certificacione
{
    public string NombreCertificacion { get; set; } = null!;

    public DateOnly FechaEmision { get; set; }

    public DateOnly FechaExpiracion { get; set; }

    public string CodigoCertificacion { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int IdCertificacion { get; set; }

    public virtual ICollection<Solicitude> Solicitudes { get; set; } = new List<Solicitude>();
}

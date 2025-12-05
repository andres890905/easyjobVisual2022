using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Incapacidade
{
    public string IdIncapacidad { get; set; } = null!;

    public int Idusuarios { get; set; }

    public string NombreEmpleado { get; set; } = null!;

    public string NombreEps { get; set; } = null!;

    public string Motivo { get; set; } = null!;

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public string? ArchivoSoporte { get; set; }

    public virtual Usuario? IdusuariosNavigation { get; set; }
}

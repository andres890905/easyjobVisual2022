using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Vacacione
{
    public int IdVacacion { get; set; }

    public int Idusuarios { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Estado { get; set; } = "Pendiente";

    public DateOnly FechaSolicitud { get; set; }

    public string? Comentarios { get; set; }

    public virtual Usuario? IdusuariosNavigation { get; set; } 
}

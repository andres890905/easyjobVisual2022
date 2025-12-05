using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Programacion
{
    public long IdProgramacion { get; set; }

    public int Idusuarios { get; set; }

    public int IdSupervisor { get; set; }

    public DateOnly? Fecha { get; set; }

    public TimeOnly? HoraEntrada { get; set; }

    public TimeOnly? HoraSalida { get; set; }

    public decimal? HorasExtra { get; set; }

    public bool? EsDescanso { get; set; }

    public bool? EsDominical { get; set; }

    public string? Descripcion { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Usuario IdSupervisorNavigation { get; set; } = null!;

    public virtual Usuario IdusuariosNavigation { get; set; } = null!;
}

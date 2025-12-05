using System;
using System.Collections.Generic;

namespace easyjob22.Models;

public partial class Role
{
    public int IdRoles { get; set; }

    public string TipoRol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

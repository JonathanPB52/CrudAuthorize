using System;
using System.Collections.Generic;

namespace WebApiPersona.Models.Data;

public partial class RolUsuario
{
    public int IdRol { get; set; }

    public string NombreRol { get; set; }

    //public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

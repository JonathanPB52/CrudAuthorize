using System;
using System.Collections.Generic;

namespace WebApiPersona.Models.Data;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreU { get; set; }

    public string ContrasenaU { get; set; }

    public int? IdRol { get; set; }

    public int? Estado { get; set; }

    //public virtual RolUsuario IdRolNavigation { get; set; }
}

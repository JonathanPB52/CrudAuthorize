using System;
using System.Collections.Generic;

namespace WebApiPersona.Models.Data;

public partial class TipoDocumento
{
    public int Id { get; set; }

    public string Tdocumento { get; set; }

    //public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}

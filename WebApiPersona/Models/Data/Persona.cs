using System;
using System.Collections.Generic;

namespace WebApiPersona.Models.Data;

public partial class Persona
{
    public int Id { get; set; }

    public string PrimerNombre { get; set; }

    public string SegundoNombre { get; set; }

    public string PrimerApeliido { get; set; }

    public string SegundoApellido { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string Documento { get; set; }

    public int? IdTipoDocumento { get; set; }

    public int Estado { get; set; }

    //public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; }
}

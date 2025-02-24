using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class ContactoEmergencium
{
    public int IdContactoEmergencia { get; set; }

    public string? Nombre { get; set; }

    public string? Parentesco { get; set; }

    public string? NumeroContacto { get; set; }

    public virtual ICollection<UsuarioxContactoEmergencium> UsuarioxContactoEmergencia { get; set; } = new List<UsuarioxContactoEmergencium>();
}

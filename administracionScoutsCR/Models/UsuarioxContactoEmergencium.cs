using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class UsuarioxContactoEmergencium
{
    public int IdUsuarioxContactoEmergencia { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdContactoEmergencia { get; set; }

    public virtual ContactoEmergencium? IdContactoEmergenciaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class UsuarioxInsignium
{
    public int IdUsuarioxInsignias { get; set; }

    public int IdUsuario { get; set; }

    public int IdInsignia { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Insignia IdInsigniaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

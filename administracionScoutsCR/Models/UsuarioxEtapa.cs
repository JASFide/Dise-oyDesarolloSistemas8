using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class UsuarioxEtapa
{
    public int IdUsuarioEtapa { get; set; }

    public int IdUsuario { get; set; }

    public int IdEtapa { get; set; }

    public virtual Etapa IdEtapaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

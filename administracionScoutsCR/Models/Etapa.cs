using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class Etapa
{
    public int IdEtapa { get; set; }

    public string Nombre { get; set; } = null!;

    public string Seccion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<UsuarioxEtapa> UsuarioxEtapas { get; set; } = new List<UsuarioxEtapa>();
}

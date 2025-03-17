using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class Insignia
{
    public int IdInsignia { get; set; }

    public string Nombre { get; set; }

    public string Seccion { get; set; }

    public string Tipo { get; set; }

    public string Estado { get; set; }

    public virtual ICollection<ReqInsignia> ReqInsignia { get; set; } = new List<ReqInsignia>();

    public virtual ICollection<UsuarioxInsignium> UsuarioxInsignia { get; set; } = new List<UsuarioxInsignium>();
}

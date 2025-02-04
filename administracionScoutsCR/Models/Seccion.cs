using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class Seccion
{
    public int IdSeccion { get; set; }

    public string Nombre { get; set; } = null!;

    public int EdadMinima { get; set; }

    public int EdadMaxima { get; set; }

    public string JefeSeccion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

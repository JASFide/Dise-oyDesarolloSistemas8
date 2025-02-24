using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class Empleado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? Edad { get; set; }

    public bool? Estado { get; set; }

    public int? IdPuesto { get; set; }

    public virtual Puesto? IdPuestoNavigation { get; set; }
}

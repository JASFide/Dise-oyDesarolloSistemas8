using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class ConfirmacionEvento
{
    public int IdConfirmacionEvento { get; set; }

    public string Asistencia { get; set; } = null!;

    public int IdEvento { get; set; }

    public int IdUsuario { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}

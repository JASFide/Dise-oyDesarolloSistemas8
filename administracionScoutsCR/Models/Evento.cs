﻿using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class Evento
{
    public int IdEvento { get; set; }

    public string Titulo { get; set; } = null!;

    public DateTime Fecha { get; set; }

    public string Lugar { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public string Encargado { get; set; } = null!;

    public string ContactoEncargado { get; set; } = null!;

    public virtual ICollection<ConfirmacionEvento> ConfirmacionEventos { get; set; } = new List<ConfirmacionEvento>();
}

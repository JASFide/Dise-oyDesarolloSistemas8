using System;
using System.Collections.Generic;

namespace administracionScoutsCR.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public bool Estado { get; set; }

    public int IdSeccion { get; set; }

    public string Direccion { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string NumeroTelefono { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int? IdRole { get; set; }

    public virtual ICollection<ConfirmacionEvento> ConfirmacionEventos { get; set; } = new List<ConfirmacionEvento>();

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual Seccion IdSeccionNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioxContactoEmergencium> UsuarioxContactoEmergencia { get; set; } = new List<UsuarioxContactoEmergencium>();

    public virtual ICollection<UsuarioxEtapa> UsuarioxEtapas { get; set; } = new List<UsuarioxEtapa>();

    public virtual ICollection<UsuarioxInsignium> UsuarioxInsignia { get; set; } = new List<UsuarioxInsignium>();
}

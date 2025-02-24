using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models;
[Index(nameof(Correo), IsUnique = true)]
public partial class Usuario
{
    public int IdUsuario { get; set; }
  
    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public bool Estado { get; set; }

    public int? IdSeccion { get; set; }

    public string Direccion { get; set; } = null!;
    
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public required string Correo { get; set; } 

    public string NumeroTelefono { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = null!;

    public virtual ICollection<ConfirmacionEvento> ConfirmacionEventos { get; set; } = new List<ConfirmacionEvento>();

    public virtual Seccion? IdSeccionNavigation { get; set; } = null!;

    public virtual ICollection<UsuarioxContactoEmergencium> UsuarioxContactoEmergencia { get; set; } = new List<UsuarioxContactoEmergencium>();

    public virtual ICollection<UsuarioxEtapa> UsuarioxEtapas { get; set; } = new List<UsuarioxEtapa>();

    public virtual ICollection<UsuarioxInsignium> UsuarioxInsignia { get; set; } = new List<UsuarioxInsignium>();
}

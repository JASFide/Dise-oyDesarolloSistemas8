using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class Usuario
	{
		public int IdUsuario { get; set; }

		[Required(ErrorMessage = "El nombre es obligatorio.")]
		[StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
		public string Nombre { get; set; } = null!;

		[Required(ErrorMessage = "El primer apellido es obligatorio.")]
		[StringLength(50, ErrorMessage = "El primer apellido no puede superar los 50 caracteres.")]
		public string Apellido1 { get; set; } = null!;

		[StringLength(50, ErrorMessage = "El segundo apellido no puede superar los 50 caracteres.")]
		public string Apellido2 { get; set; } = null!;

		[Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
		[DataType(DataType.Date)]
		public DateOnly FechaNacimiento { get; set; }

		[Required(ErrorMessage = "Debe indicar el tipo de usuario.")]
		[StringLength(50, ErrorMessage = "El tipo de usuario no puede superar los 50 caracteres.")]
		public string TipoUsuario { get; set; } = "Receptor";

		public bool Estado { get; set; } = true;

		[Required(ErrorMessage = "Debe seleccionar una sección.")]
		public int? IdSeccion { get; set; }

		[Required(ErrorMessage = "La dirección es obligatoria.")]
		[StringLength(100, ErrorMessage = "La dirección no puede superar los 100 caracteres.")]
		public string Direccion { get; set; } = null!;

		[Required(ErrorMessage = "El correo electrónico es obligatorio.")]
		[EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
		public string Correo { get; set; } = null!;

		[Required(ErrorMessage = "El número de teléfono es obligatorio.")]
		[Phone(ErrorMessage = "El formato del teléfono no es válido.")]
		[StringLength(20, ErrorMessage = "El número de teléfono no puede superar los 20 caracteres.")]
		public string NumeroTelefono { get; set; } = null!;

		[Required(ErrorMessage = "La contraseña es obligatoria.")]
		[StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
		public string Contrasena { get; set; } = null!;

		[Required(ErrorMessage = "Debe seleccionar un rol.")]
		public int? IdRole { get; set; }

		public virtual ICollection<ConfirmacionEvento> ConfirmacionEventos { get; set; } = new List<ConfirmacionEvento>();
		public virtual Role? IdRoleNavigation { get; set; } = null!;
		public virtual Seccion? IdSeccionNavigation { get; set; } = null!;
		public virtual ICollection<UsuarioxContactoEmergencium> UsuarioxContactoEmergencia { get; set; } = new List<UsuarioxContactoEmergencium>();
		public virtual ICollection<UsuarioxEtapa> UsuarioxEtapas { get; set; } = new List<UsuarioxEtapa>();
		public virtual ICollection<UsuarioxInsignium> UsuarioxInsignia { get; set; } = new List<UsuarioxInsignium>();
	}
}

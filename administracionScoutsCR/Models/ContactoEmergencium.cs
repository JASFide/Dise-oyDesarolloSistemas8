using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class ContactoEmergencium
	{
		public int IdContactoEmergencia { get; set; }

		[Required(ErrorMessage = "El nombre del contacto es obligatorio.")]
		[StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
		public string? Nombre { get; set; }

		[Required(ErrorMessage = "El parentesco es obligatorio.")]
		[StringLength(50, ErrorMessage = "El parentesco no puede superar los 50 caracteres.")]
		public string? Parentesco { get; set; }

		[Required(ErrorMessage = "El número de contacto es obligatorio.")]
		[Phone(ErrorMessage = "Debe ingresar un número de teléfono válido.")]
		[StringLength(20, ErrorMessage = "El número de contacto no puede superar los 20 caracteres.")]
		public string? NumeroContacto { get; set; }

		public virtual ICollection<UsuarioxContactoEmergencium> UsuarioxContactoEmergencia { get; set; } = new List<UsuarioxContactoEmergencium>();
	}
}

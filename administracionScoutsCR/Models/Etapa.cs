using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class Etapa
	{
		public int IdEtapa { get; set; }

		[Required(ErrorMessage = "El nombre de la etapa es obligatorio.")]
		[StringLength(50, ErrorMessage = "El nombre de la etapa no puede superar los 50 caracteres.")]
		public string Nombre { get; set; } = null!;

		[Required(ErrorMessage = "La sección es obligatoria.")]
		[StringLength(50, ErrorMessage = "El nombre de la sección no puede superar los 50 caracteres.")]
		public string Seccion { get; set; } = null!;

		[Required(ErrorMessage = "El estado es obligatorio.")]
		[RegularExpression("^(Activo|Inactivo)$", ErrorMessage = "El estado debe ser 'Activo' o 'Inactivo'.")]
		public string Estado { get; set; } = null!;

		public virtual ICollection<UsuarioxEtapa> UsuarioxEtapas { get; set; } = new List<UsuarioxEtapa>();
	}
}

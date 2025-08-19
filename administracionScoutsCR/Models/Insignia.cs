using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class Insignia
	{
		public int IdInsignia { get; set; }

		[Required(ErrorMessage = "El nombre de la insignia es obligatorio.")]
		[StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
		public string Nombre { get; set; } = null!;

		[Required(ErrorMessage = "Debe especificar la sección.")]
		[StringLength(50, ErrorMessage = "La sección no puede superar los 50 caracteres.")]
		public string Seccion { get; set; } = null!;

		[Required(ErrorMessage = "Debe especificar el tipo de insignia.")]
		[StringLength(50, ErrorMessage = "El tipo no puede superar los 50 caracteres.")]
		public string Tipo { get; set; } = null!;

		[Required(ErrorMessage = "Debe indicar el estado de la insignia.")]
		[RegularExpression("^(Activa|Inactiva)$", ErrorMessage = "El estado debe ser 'Activa' o 'Inactiva'.")]
		public string Estado { get; set; } = null!;

		public virtual ICollection<ReqInsignia> ReqInsignia { get; set; } = new List<ReqInsignia>();

		public virtual ICollection<UsuarioxInsignium> UsuarioxInsignia { get; set; } = new List<UsuarioxInsignium>();
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class Role
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "El nombre del rol es obligatorio.")]
		[StringLength(50, ErrorMessage = "El nombre del rol no puede superar los 50 caracteres.")]
		public string Nombre { get; set; } = null!;

		public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
	}
}

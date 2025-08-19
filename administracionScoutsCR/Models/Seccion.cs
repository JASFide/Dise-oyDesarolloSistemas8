using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class Seccion : IValidatableObject
	{
		public int IdSeccion { get; set; }

		[Required(ErrorMessage = "El nombre de la sección es obligatorio.")]
		public string Nombre { get; set; } = null!;

		[Range(0, 120, ErrorMessage = "La edad mínima no puede ser negativa.")]
		public int EdadMinima { get; set; }

		[Range(0, 120, ErrorMessage = "La edad máxima no puede ser negativa.")]
		public int EdadMaxima { get; set; }

		[Required(ErrorMessage = "Debe seleccionar un jefe de sección.")]
		public string JefeSeccion { get; set; } = null!;

		public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

		// Validación personalizada: la edad máxima debe ser mayor que la mínima
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (EdadMaxima <= EdadMinima)
			{
				yield return new ValidationResult(
					"La edad máxima debe ser mayor que la edad mínima.",
					new[] { nameof(EdadMaxima) });
			}
		}
	}
}

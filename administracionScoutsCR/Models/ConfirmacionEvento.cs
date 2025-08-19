using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class ConfirmacionEvento
	{
		public int IdConfirmacionEvento { get; set; }

		[Required(ErrorMessage = "Debe especificar el estado de la asistencia.")]
		[StringLength(50, ErrorMessage = "El campo Asistencia no puede tener más de 50 caracteres.")]
		public string Asistencia { get; set; } = null!;

		[Required(ErrorMessage = "Debe seleccionar un evento.")]
		public int? IdEvento { get; set; }

		[Required(ErrorMessage = "Debe especificar el usuario que confirma la asistencia.")]
		public int? IdUsuario { get; set; }

		[ForeignKey("IdEvento")]
		public virtual Evento? IdEventoNavigation { get; set; } = null!;

		[ForeignKey("IdUsuario")]
		public virtual Usuario? IdUsuarioNavigation { get; set; } = null!;
	}
}

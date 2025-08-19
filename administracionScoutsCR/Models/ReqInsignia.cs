using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace administracionScoutsCR.Models
{
	public partial class ReqInsignia
	{
		public int IdReqInsignia { get; set; }

		[Required(ErrorMessage = "La descripción del requisito es obligatoria.")]
		[StringLength(300, ErrorMessage = "La descripción no puede superar los 300 caracteres.")]
		public string? Descripcion { get; set; }

		[Required(ErrorMessage = "Debe asociar el requisito a una insignia.")]
		public int? IdInsignias { get; set; }

		public virtual Insignia? IdInsigniasNavigation { get; set; }
	}
}

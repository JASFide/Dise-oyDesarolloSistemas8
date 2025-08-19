using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace administracionScoutsCR.Models
{
	public partial class Evento
	{
		public int IdEvento { get; set; }

		[Required(ErrorMessage = "El título del evento es obligatorio.")]
		[StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres.")]
		public string Titulo { get; set; } = null!;

		[Required(ErrorMessage = "La fecha del evento es obligatoria.")]
		[DataType(DataType.Date, ErrorMessage = "Debe ingresar una fecha válida.")]
		public DateTime Fecha { get; set; }

		[Required(ErrorMessage = "El lugar del evento es obligatorio.")]
		[StringLength(100, ErrorMessage = "El lugar no puede superar los 100 caracteres.")]
		public string Lugar { get; set; } = null!;

		[Required(ErrorMessage = "La descripción es obligatoria.")]
		[StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
		public string Descripcion { get; set; } = null!;

		[Required(ErrorMessage = "Debe ingresar el nombre del encargado.")]
		[StringLength(100, ErrorMessage = "El nombre del encargado no puede superar los 100 caracteres.")]
		public string Encargado { get; set; } = null!;

		[Required(ErrorMessage = "Debe ingresar un medio de contacto del encargado.")]
		[StringLength(50, ErrorMessage = "El contacto del encargado no puede superar los 50 caracteres.")]
		public string ContactoEncargado { get; set; } = null!;

		public virtual ICollection<ConfirmacionEvento> ConfirmacionEventos { get; set; } = new List<ConfirmacionEvento>();

		public string? RutaImagen { get; set; }

		[NotMapped]
		[DataType(DataType.Upload)]
		[FileExtensions(Extensions = "jpg,jpeg,png", ErrorMessage = "El archivo debe ser una imagen en formato .jpg o .png.")]
		public IFormFile? ImagenEvento { get; set; }
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace administracionScoutsCR.Models
{
	public class EventosViewModel
	{
		public IEnumerable<Evento> ListaEventos { get; set; } = new List<Evento>();
		public Evento NuevoEvento { get; set; } = new Evento();
	}
}
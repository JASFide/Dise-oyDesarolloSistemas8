using System.ComponentModel.DataAnnotations;
namespace administracionScoutsCR.Models
{
    
    public class LoginViewmodel
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public required string Correo { get; set; }

        [DataType(DataType.Password)]
        public string Contrasena { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

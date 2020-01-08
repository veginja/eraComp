using System.ComponentModel.DataAnnotations;

namespace era_Computacion.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo Requerido!")]
        [Display(Name = "Usuario ó Correo")]
        [StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Requerido!")]
        [Display(Name = "Contraseña")]
        [StringLength(30)]
        public string Password { get; set; }
        public string ReturnURL { get; set; }
    }
}

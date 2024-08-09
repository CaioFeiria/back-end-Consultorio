using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Campo Email obrigatório.")]
        [StringLength(50, ErrorMessage = "Máximo de caracteres é 50.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Senha obrigatório.")]
        [StringLength(20, ErrorMessage = "Máximo de caracteres é 20.")]
        public string Senha {  get; set; }
    }
}
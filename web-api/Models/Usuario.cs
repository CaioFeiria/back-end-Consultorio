using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Nome obrigatório.")]
        [StringLength(100, ErrorMessage = "Máximo de caracteres é 100.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Email obrigatório.")]
        [StringLength(50, ErrorMessage = "Máximo de caracteres é 50.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Senha obrigatório.")]
        [StringLength(20, ErrorMessage = "Máximo de caracteres é 20.")]
        public string Senha { get; set; }
    }
}
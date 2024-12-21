using System.ComponentModel.DataAnnotations;

namespace FinancesApi.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo 'E-mail' não pode estar vazio.")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? LoginProvider { get; set; }
        [Required(ErrorMessage = "O campo 'Senha' não pode estar vazio.")]
        [StringLength(150, ErrorMessage = "O campo 'Senha' deve ter de 3 a 150 caracteres", MinimumLength = 3)]
        public string? Password { get; set; }
    }
}

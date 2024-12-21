using ContactSystem.Helpers;
using System.ComponentModel.DataAnnotations;

namespace FinancesApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo 'Nome' não pode estar vazio.")]
        [StringLength(255, ErrorMessage = "O campo 'Nome' deve ter de 3 a 255 caracteres", MinimumLength = 3)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "O campo 'E-mail' não pode estar vazio.")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(150, ErrorMessage = "O campo 'E-mail' deve ter no máximo 150 caracteres")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo 'Senha' não pode estar vazio.")]
        [StringLength(150, ErrorMessage = "O campo 'Senha' deve ter de 3 a 150 caracteres", MinimumLength = 3)]
        public string? Password { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public void SetPasswordHash()
        {
            Password = Password.GenerateHash();
        }

        public bool ComparePasswordHash(string password)
        {
            return password.GenerateHash() == Password;
        }
    }
}

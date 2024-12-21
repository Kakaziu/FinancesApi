using FinancesApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancesApi.Models
{
    public class TransitionModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo 'Descrição' não pode estar vazio.")]
        [StringLength(100, ErrorMessage = "O campo 'Descrição' deve ter de 3 a 100 caracteres", MinimumLength = 3)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "O campo 'Valor' não pode estar vazio")]
        public double Value { get; set; }
        [Required(ErrorMessage = "Selecione um tipo de transição.")]
        public TransitionType TransitionType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? UserId { get; set; }
        public UserModel? User { get; set; }

    }
}

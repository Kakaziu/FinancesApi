using FinancesApi.Enums;
using FinancesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FinancesApi.DTOs
{
    public class TransitionDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Description { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public TransitionType TransitionType { get; set; }
        public int? UserId { get; set; }
    }
}

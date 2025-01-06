using System.ComponentModel.DataAnnotations;

namespace FinancesApi.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string? Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string? Email { get; set; }
        [Required]
        [StringLength(150)]
        public string? Password { get; set; }
    }
}

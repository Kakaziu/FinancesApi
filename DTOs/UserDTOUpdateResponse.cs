using FinancesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FinancesApi.DTOs
{
    public class UserDTOUpdateResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public ICollection<TransitionModel>? Transitions { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}

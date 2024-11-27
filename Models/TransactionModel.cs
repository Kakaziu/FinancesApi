using FinancesApi.Enums;

namespace FinancesApi.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public int? UserId { get; set; }
        public UserModel? User { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

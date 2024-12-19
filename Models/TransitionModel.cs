namespace FinancesApi.Models
{
    public class TransitionModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public int TransitionType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int? UserId { get; set; }
        public UserModel? User { get; set; }

    }
}

using TaskSystem.Helpers;

namespace FinancesApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public void SetPasswordHash()
        {
            Password = Password.GenerateHash();
        }

        public bool ValidPassword(string password)
        {
            return Password == password.GenerateHash();
        }
    }
}

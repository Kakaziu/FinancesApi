using ContactSystem.Helpers;

namespace FinancesApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
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

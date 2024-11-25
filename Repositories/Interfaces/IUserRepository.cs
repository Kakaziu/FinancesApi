using FinancesApi.Models;

namespace FinancesApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> FindAll();
        Task<UserModel> FindById(int id);
        Task<UserModel> FindByEmail(string email);
        Task<UserModel> Insert(UserModel user);
        Task<UserModel> Update(UserModel user, int id);
        Task<UserModel> Delete(int id);
    }
}

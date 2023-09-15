using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<List<User>> GetAllUsers(int parameter);
        public Task<User> GetUserById(int id, int parameter);
        public Task<bool> DeleteUserById(int id, int parameter);
        public Task<bool> UpdateUser(User user, int id);
        public Task<User?> AuthenticateCredentials(AuthenticateDto dto);

    }
}

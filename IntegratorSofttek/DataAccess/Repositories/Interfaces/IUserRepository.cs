using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<List<UserDTO>> GetAllUsers(int parameter);
        public Task<UserDTO> GetUserById(int id, int parameter);
        public Task<bool> DeleteUserById(int id, int parameter);
        public Task<bool> UpdateUser(UserRegisterDTO userRegisterDTO, int id);
        public Task<bool> InsertUser(UserRegisterDTO userRegisterDTO);
        public Task<User?> AuthenticateCredentials(AuthenticateDto dto);

    }
}

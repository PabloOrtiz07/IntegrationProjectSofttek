using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using IntegratorSofttek.Helper;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ContextDB contextDB) : base(contextDB)
        {

        }

        public  async Task<bool> UpdateUser(User user,int id)
        {
            try
            {
                var userFinding = await GetById(id);
                if (userFinding != null) {
                    _contextDB.Update(user);
                    return true;

                }
                return false;
            }
            catch (Exception)
            {
                
                return false;
            }
        }


        public virtual async Task<List<User>> GetAllUsers(int parameter)
        {
            try
            {
                var users = await base.GetAll();
                if (parameter == 1)
                {
                    users = users.Where(user => user.IsDeleted != true).ToList();
                    return users;
                }
                if (parameter == 0)
                {
                    return users;
                }
                return null;
            }
            catch (Exception)
            {
                return null;

            }

        }

        public async Task<User> GetUserById(int id, int parameter)
        {
            try
            {
                User user = await base.GetById(id);
                if (user.IsDeleted != true && parameter == 1)
                {
                    return user;
                }
                if (parameter == 0)
                {
                    return user;
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }


        public  async Task<bool> DeleteUserById(int id,int parameter)
        {
            User user = await GetById(id);
            if (user != null && parameter == 1)
            {
                user.IsDeleted = true;
                user.DeletedTimeUtc = DateTime.UtcNow;

                return true;
            }
            if(user != null && parameter == 0)
            {
                _contextDB.Users.Remove(user);
                return true;
            }

            return false;
        }


        public async Task<User?> AuthenticateCredentials(AuthenticateDto dto)
        {
            return await _contextDB.Users.SingleOrDefaultAsync
                (user =>  user.Email == dto.Email && user.Password == PasswordEncryptHelper.EncryptPassword(dto.Password));
        }

    }
}
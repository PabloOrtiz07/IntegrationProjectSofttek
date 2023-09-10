using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ContextDB contextDB) : base(contextDB)
        {

        }

        public override async Task<bool> Update(User user)
        {
            try
            {
                var userFinding = GetById(user.Id);
                if (userFinding != null) {
                    _contextDB.Update(user);
                    SaveChangesAsync();
                    return true;

                }
                return false;
            }
            catch (Exception)
            {
                
                return false;
            }
        }

        public async Task<User?> AuthenticateCredentials(AuthenticateDto dto)
        {
            return await _contextDB.Users.SingleOrDefaultAsync
                (user =>  user.Dni == dto.Dni && user.Password == dto.Password);
        }

    }
}
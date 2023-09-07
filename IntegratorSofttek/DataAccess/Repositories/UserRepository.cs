using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;
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

    }
}
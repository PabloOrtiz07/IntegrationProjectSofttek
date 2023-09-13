using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {

        public RoleRepository(ContextDB contextDB) : base(contextDB)
        {

        }

    
        public async Task<bool> UpdateRol(Role role, int id)
        {
            try
            {
                var roleFinding = await GetById(id);
                if (roleFinding != null)
                {
                    _contextDB.Update(role);
                    return true;

                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public async Task<bool> DeleteRolById(int id, int parameter)
        {
            Role role = await GetById(id);
            if (role != null && parameter == 1)
            {
                role.IsDeleted = true;
                role.DeletedTimeUtc = DateTime.UtcNow;

                return true;
            }
            if (role != null && parameter == 2)
            {
                _contextDB.Roles.Remove(role);
                return true;
            }

            return false;
        }

    }
}

using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<bool> UpdateRol(Role updateRole, int id);
        public  Task<bool> DeleteRolById(int id, int parameter);


    }
}

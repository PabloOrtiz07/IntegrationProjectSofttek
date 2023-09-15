using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Task<List<Role>> GetAllRoles(int parameter);
        public Task<Role> GetRoleById(int id, int parameter);
        public Task<bool> DeleteRoleById(int id, int parameter);
        public Task<bool> UpdateRole(Role role, int id);

    }
}

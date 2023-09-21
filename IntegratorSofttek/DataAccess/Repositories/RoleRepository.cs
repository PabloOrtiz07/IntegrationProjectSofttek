using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using IntegratorSofttek.Helper;
using AutoMapper;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly IMapper _mapper;

        public RoleRepository(ContextDB contextDB,IMapper mapper) : base(contextDB)
        {
            _mapper = mapper;
        }

        public async Task<bool> UpdateRole(Role role, int id)
        {
            try
            {
                var roleFinding = await GetById(id);
                if (roleFinding != null)
                {
                    _mapper.Map(role, roleFinding);
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

        public virtual async Task<List<Role>> GetAllRoles(int parameter)
        {
            try
            {
                var roles = await base.GetAll();
                if (parameter == 0)
                {
                    roles = roles.Where(role => role.IsDeleted != true).ToList();
                    return roles;

                }
                else if (parameter == 1)
                {
                    return roles;

                }
                return null;

            }
            catch (Exception)
            {
                return null;

            }
        }

        public async Task<Role> GetRoleById(int id, int parameter)
        {
            try
            {
                Role role = await base.GetById(id);
                if (role.IsDeleted != true && parameter == 0)
                {
                    return role;
                }
                if (parameter == 1)
                {
                    return role;
                }
                return null;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteRoleById(int id, int parameter)
        {

            try
            {
                Role role = await GetById(id);
                if (role != null && parameter == 0)
                {
                    role.IsDeleted = true;
                    role.DeletedTimeUtc = DateTime.UtcNow;

                    return true;
                }
                if (role != null && parameter == 1)
                {
                    _contextDB.Roles.Remove(role);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public virtual async Task<bool> InsertRole(RoleDTO roleDTO)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDTO);
                var response = await base.Insert(role);
                return response;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}

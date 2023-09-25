using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using IntegratorSofttek.Helper;
using AutoMapper;
using System.Reflection.Metadata;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private readonly IMapper _mapper;

        public RoleRepository(ContextDB contextDB,IMapper mapper) : base(contextDB)
        {
            _mapper = mapper;
        }

        public async Task<bool> UpdateRole(Role role, int id, int parameter)
        {
            try
            {
                var roleFinding = await GetById(id);
                if (roleFinding == null) {
                    return false;
                }
                if (parameter == 0)
                {
                    _mapper.Map(role, roleFinding);
                    _contextDB.Update(role);
                    return true;

                }
                if (roleFinding.IsDeleted != false && parameter == 1)
                {
                    roleFinding.IsDeleted = false;
                    roleFinding.DeletedTimeUtc = null;
                    _contextDB.Update(roleFinding);
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
                Role roleFinding = await base.GetById(id);
                if (roleFinding == null)
                {
                    return null;
                }
                if (roleFinding.IsDeleted != true && parameter == 0)
                {
                    return roleFinding;
                }
                if (parameter == 1)
                {
                    return roleFinding;
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
                Role roleFinding = await GetById(id);
                if (roleFinding == null)
                {
                    return false;
                }
                if (roleFinding != null && parameter == 0)
                {
                    roleFinding.IsDeleted = true;
                    roleFinding.DeletedTimeUtc = DateTime.UtcNow;

                    return true;
                }
                if (roleFinding != null && parameter == 1)
                {
                    _contextDB.Roles.Remove(roleFinding);
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
            catch (Exception)
            {
                return false;
            }

        }
    }
}

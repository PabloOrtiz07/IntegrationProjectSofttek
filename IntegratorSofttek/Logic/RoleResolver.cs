using AutoMapper;
using IntegratorSofttek.DataAccess;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.Logic
{
    public class RoleResolver : IValueResolver<UserRegisterDTO, User, Role>
    {
        private readonly ContextDB _dbContext;

        public RoleResolver(ContextDB dbContext)
        {
            _dbContext = dbContext;
        }

        public Role Resolve(UserRegisterDTO source, User destination, Role destMember, ResolutionContext context)
        {
            if (source.RoleId != 0)
            {
                // Perform a database lookup to find the role based on RoleId
                var role = _dbContext.Roles.FirstOrDefault(r => r.Id == source.RoleId);
                return role;
            }

            // Handle other cases or return a default role if needed
            return null;
        }
    }

}

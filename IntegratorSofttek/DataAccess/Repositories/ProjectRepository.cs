using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ContextDB contextDB) : base(contextDB)
        {
        }
    }
}

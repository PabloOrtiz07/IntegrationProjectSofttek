using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ContextDB contextDB) : base(contextDB)
        {
        }
        public virtual async Task<bool> Update(Project updatedProject)
        {
            try
            {
                var projectFinding = await GetById(updatedProject.Id);

                if (projectFinding != null)
                {
                    _contextDB.Update(updatedProject); 
                    await SaveChangesAsync();
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

using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project> // Update interface name and entity type
    {
        public Task<List<Project>> GetAllProjects(int parameter,int state); // Update method name
        public Task<Project> GetProjectById(int id, int parameter); // Update method name
        public Task<bool> DeleteProjectById(int id, int parameter); // Update method name
        public Task<bool> UpdateProject(Project project, int id); // Update method name
    }
}

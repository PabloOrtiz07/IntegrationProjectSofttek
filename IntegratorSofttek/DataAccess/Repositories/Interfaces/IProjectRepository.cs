using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;

namespace IntegratorSofttek.DataAccess.Repositories.Interfaces
{
    public interface IProjectRepository : IRepository<Project> // Update interface name and entity type
    {
        public Task<List<ProjectDTO>> GetAllProjects(int parameter,string state); // Update method name
        public Task<ProjectDTO> GetProjectById(int id, int parameter); // Update method name
        public Task<bool> DeleteProjectById(int id, int parameter); // Update method name
        public Task<bool> UpdateProject(ProjectDTO projectDTO, int id, int parameter); // Update method name

        public Task<bool> InsertProject(ProjectDTO projectDTO);

    }
}

using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository // Update class and interface names
    {
        public ProjectRepository(ContextDB contextDB) : base(contextDB)
        {

        }

        public async Task<bool> UpdateProject(Project project, int id) // Update method name
        {
            try
            {
                var projectFinding = await GetById(id); // Update variable name
                if (projectFinding != null)
                {
                    _contextDB.Update(project);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<List<Project>> GetAllProjects(int parameter, int state) // Update method name
        {
            try
            {
                var projects = await base.GetAll(); // Update variable name
                switch (parameter)
                {
                    case 0:
                        return projects;
                    case 1:
                        return projects.Where(projects => !projects.IsDeleted).ToList();
                    case 2:
                        return projects.Where(projects => !projects.IsDeleted && projects.Status==(ProjectStatus)state).ToList();
                    default:
                        return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Project> GetProjectById(int id, int parameter) // Update method name
        {
            try
            {
                Project project = await base.GetById(id); // Update variable name
                if (project.IsDeleted != true && parameter == 1)
                {
                    return project;
                }
                if (parameter == 2)
                {
                    return project;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProjectById(int id, int parameter) // Update method name
        {
            Project project = await GetById(id); // Update variable name
            if (project != null && parameter == 1)
            {
                project.IsDeleted = true;
                project.DeletedTimeUtc = DateTime.UtcNow;
                return true;
            }
            if (project != null && parameter == 2)
            {
                _contextDB.Projects.Remove(project); // Update entity reference
                return true;
            }
            return false;
        }
    }
}

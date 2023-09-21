using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using AutoMapper;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository // Update class and interface names
    {
        private readonly IMapper _mapper;

        public ProjectRepository(ContextDB contextDB, IMapper mapper) : base(contextDB)
        {
            _mapper = mapper;
        }

        public async Task<bool> UpdateProject(ProjectDTO projectDTO, int id) // Update method name
        {
            try
            {
                var project = _mapper.Map<Project>(projectDTO);

                var projectFinding = await GetById(id); // Update variable name
                if (projectFinding != null)
                {
                    _mapper.Map(project, projectFinding);

                    _contextDB.Update(projectFinding);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual async Task<List<ProjectDTO>> GetAllProjects(int parameter, string state) // Update method name
        {
            try
            {
                ProjectStatus status;
                status = _mapper.Map<ProjectStatus>(state.ToLower());
                int intStatus = (int)status;

                var projects = await base.GetAll(); // Update variable name
                switch (parameter)
                {
                    case 0:
                        projects=projects.Where(projects => !projects.IsDeleted).ToList();
                        return _mapper.Map<List<ProjectDTO>>(projects);

                    case 1:
                        return _mapper.Map<List<ProjectDTO>>(projects);
                    case 2:
                        projects=projects.Where(projects => !projects.IsDeleted && projects.Status == (ProjectStatus)intStatus).ToList();
                        return _mapper.Map<List<ProjectDTO>>(projects);
                    default:
                        return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProjectDTO> GetProjectById(int id, int parameter) // Update method name
        {
            try
            {

                Project project = await base.GetById(id); // Update variable name
                if (project.IsDeleted != true && parameter == 0)
                {
                    return _mapper.Map<ProjectDTO>(project); ;
                }
                if (parameter == 1)
                {
                    return _mapper.Map<ProjectDTO>(project); ;
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

            try
            {
                Project project = await GetById(id); // Update variable name
                if (project != null && parameter == 0)
                {
                    project.IsDeleted = true;
                    project.DeletedTimeUtc = DateTime.UtcNow;
                    return true;
                }
                if (project != null && parameter == 1)
                {
                    _contextDB.Projects.Remove(project); // Update entity reference
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
   
        }

        public virtual async Task<bool> InsertProject(ProjectDTO projectDTO)
        {
            try
            {
                var project = _mapper.Map<Project>(projectDTO);
                var response = await base.Insert(project);
                return response;
            }
            catch (Exception ex) {
                return false;
            }
       
        }
    }
}

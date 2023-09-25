using IntegratorSofttek.DataAccess.Repositories.Interfaces;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;
using AutoMapper;

namespace IntegratorSofttek.DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly IMapper _mapper;

        public ProjectRepository(ContextDB contextDB, IMapper mapper) : base(contextDB)
        {
            _mapper = mapper;
        }

        public async Task<bool> UpdateProject(ProjectDTO projectDTO, int id, int parameter)
        {
            try
            {

                var projectFinding = await GetById(id);
                if (projectFinding == null)
                {
                    return false;
                }
                if (parameter == 0)
                {
                    var project = _mapper.Map<Project>(projectDTO);
                    _mapper.Map(project, projectFinding);
                    _contextDB.Update(projectFinding);
                    return true;
                }
                if(projectFinding.IsDeleted != false && parameter == 1) {
                    projectFinding.IsDeleted = false;
                    projectFinding.DeletedTimeUtc = null;
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

        public virtual async Task<List<ProjectDTO>> GetAllProjects(int parameter, string state)
        {
            try
            {
                ProjectStatus status;
                status = _mapper.Map<ProjectStatus>(state.ToLower());
                int intStatus = (int)status;

                var projects = await base.GetAll();
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
            catch (ArgumentException ex)
            {
                Console.WriteLine($"An ArgumentException occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<ProjectDTO> GetProjectById(int id, int parameter)
        {
            try
            {

                Project projectFinding = await base.GetById(id);
                if (projectFinding == null)
                {
                    return null;
                }
                if (projectFinding.IsDeleted != true && parameter == 0)
                {
                    return _mapper.Map<ProjectDTO>(projectFinding); ;
                }
                if (parameter == 1)
                {
                    return _mapper.Map<ProjectDTO>(projectFinding); ;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProjectById(int id, int parameter)
        {

            try
            {
                Project projectFinding = await GetById(id);
                if (projectFinding == null)
                {
                    return false;
                }

                if (parameter == 0)
                {
                    projectFinding.IsDeleted = true;
                    projectFinding.DeletedTimeUtc = DateTime.UtcNow;
                    return true;
                }
                if (parameter == 1)
                {
                    var relatedWork = _contextDB.Works.Where(work => work.ProjectId == id).ToList();
                    _contextDB.Works.RemoveRange(relatedWork);
                    _contextDB.Projects.Remove(projectFinding);
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
            catch (Exception) {
                return false;
            }
       
        }
    }
}

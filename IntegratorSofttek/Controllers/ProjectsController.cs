using Microsoft.AspNetCore.Mvc;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProjectMapper _projectMapper;

        public ProjectController(IUnitOfWork unitOfWork, ProjectMapper projectMapper)
        {
            _unitOfWork = unitOfWork;
            _projectMapper = projectMapper;
        }

        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAll();

            return projects;
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(id);

            if (project != null)
            {
                return Ok(project);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> InsertProject(ProjectDTO projectDTO)
        {
            var project = _projectMapper.MapProjectDTOToProject(projectDTO);
            var projectReturn = await _unitOfWork.ProjectRepository.Insert(project);

            if (projectReturn != false)
            {
                return Ok("The insert operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Authorize]

        public async Task<IActionResult> UpdateProject(ProjectDTO projectDTO, int id)
        {
            var project = _projectMapper.MapProjectDTOToProject(projectDTO);
            var projectReturn = await _unitOfWork.ProjectRepository.Update(project);

            if (projectReturn != false)
            {
                return Ok("The update operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpDelete]
        [Authorize]

        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _unitOfWork.ProjectRepository.DeleteHardById(id); ;

            if (project != null)
            {
                
                return Ok("The project has been eliminated from DataBase");
            }

            return NotFound("The project couldn't be found");
        }
    }
}

using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Logic;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProjectMapper _projectMapper;

        public ProjectsController(IUnitOfWork unitOfWork, ProjectMapper projectMapper)
        {
            _unitOfWork = unitOfWork;
            _projectMapper = projectMapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAll();

            return projects;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(id);

            if (project != null)
            {
                return Ok(project);
            }
            else
            {
                return NotFound("The project couldn't be found");
            }
        }

        [HttpPost]
        [Route("RegisterProject")]
        public async Task<IActionResult> RegisterProject(ProjectDTO projectDTO)
        {
            var project = _projectMapper.MapProjectDTOToProject(projectDTO);
            var projectReturn = await _unitOfWork.ProjectRepository.Insert(project);

            if (projectReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateProject/{id}")]
        public async Task<IActionResult> UpdateProject(Project project)
        {
            var projectReturn = await _unitOfWork.ProjectRepository.Update(project);

            if (projectReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");
            }

            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("DeleteSoftProject/{id}")]
        public async Task<IActionResult> DeleteSoftProject(int id)
        {
            var projectReturn = await _unitOfWork.ProjectRepository.DeleteSoftById(id);

            if (projectReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This project has been dropped down");
            }

            return NotFound("The project couldn't be found");
        }

        [HttpDelete]
        [Route("DeleteHardProject/{id}")]
        public async Task<IActionResult> DeleteHardProject(int id)
        {
            var project = await _unitOfWork.ProjectRepository.DeleteHardById(id);

            if (project != null)
            {
                await _unitOfWork.Complete();
                return Ok("This project has been eliminated from Database");
            }
            else
            {
                return NotFound("The project couldn't be found");
            }
        }
    }
}

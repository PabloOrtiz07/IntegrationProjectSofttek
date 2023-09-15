using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAllProjects( int parameter =0, int state=0)
        {
            var projects = await _unitOfWork.ProjectRepository.GetAllProjects(parameter,state);
            if (projects == null || !projects.Any())
            {
                return NotFound();
            }
            var projectsDTO = _mapper.Map<List<ProjectDTO>>(projects);
            return Ok(projectsDTO);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetProjectById([FromRoute] int id,  int parameter=0)
        {
            var project = await _unitOfWork.ProjectRepository.GetProjectById(id, parameter);

            if (project != null)
            {
                var projectDTO = _mapper.Map<ProjectDTO>(project);
                return Ok(projectDTO);
            }
            else
            {
                return NotFound("The project couldn't be found");
            }
        }

        [HttpPost]
        [Route("RegisterProject")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> RegisterProject(ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);
            var result = await _unitOfWork.ProjectRepository.Insert(project);
            if (result != false)
            {
                await _unitOfWork.Complete();
                return Ok("The register operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("UpdateProject/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> UpdateProject([FromRoute] int id, ProjectDTO projectDTO)
        {
            var project = _mapper.Map<Project>(projectDTO);

            var result = await _unitOfWork.ProjectRepository.Update(project, id);
            if (result != null)
            {
                await _unitOfWork.Complete();
                return Ok("The update operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        [HttpPut]
        [Route("DeleteProject/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> DeleteProject([FromRoute] int id, int parameter=0)
        {
            var projectReturn = await _unitOfWork.ProjectRepository.DeleteProjectById(id, parameter);
            if (projectReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This project has been dropped down");
            }
            return NotFound("The project couldn't be found");
        }
    }
}

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
        /// <summary>
        /// Gets a list of projects based on a parameter.
        /// Requires the Administrator and Consultant policies for access.
        /// </summary>
        /// <param name="parameter">The parameter used to filter projects.
        /// Use parameter 0 to return filtered non-deleted projects,
        /// use parameter 1 to retrieve all projects without filtering,
        /// and paramater 2 to return filtered non-deleted user with filtered state
        /// </param>
        /// <param name="state">Stated used to filter for differented status
        /// Use State 0 is to filter to pending status,
        /// use state 1 is to filter to confirmed status,
        /// and use state 2 is to filter to finished status 
        /// </param>
        /// <returns>Returns a list of projects or null if there were some issues</returns>
        /// 

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

        /// <summary>
        /// Get a project by their id.
        /// Requires the Administrator and Consultant policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a project with this identication.</param>
        /// <param name="parameter">The parameter used to filter projects.
        /// Use parameter 0 to return filtered non-deleted project,
        /// and use parameter 1 to retrieve all projects without filtering </param>
        /// <returns>Returns a project object matching the given ID or null if not found</returns>

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
        /// <summary>
        /// Register a project in the database.
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="projectDTO">A model containing project information to be registered</param>
        /// <returns>Returns "OK" if the registration operation was successful and null if the were some issues.</returns>        /// 

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
        /// <summary>
        /// Update a project in the database
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a project that matche  this identification</param>
        /// <param name="projectDTO">A model which will replace the older project data</param>
        /// <returns>Returns "OK" if the updating operation was succesfull</returns>
        /// 

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
        /// <summary>
        /// Delete a project softly (soft deletion) or permanently (hard deletion).
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a service with this identification </param>
        /// <param name="parameter">The parameter used to select the type of deletion 
        /// Use parameter 0 to soft delete  and,
        /// use parameter 1 to hard delete </param>
        /// <returns>Returns "OK" if the delete operation was succesfull or "BadRequest" if there was and issue</returns>
        /// 

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

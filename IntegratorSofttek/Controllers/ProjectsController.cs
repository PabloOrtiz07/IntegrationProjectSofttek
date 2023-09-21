using AlkemyUmsa.Infrastructure;
using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Helper;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UsersController> _logger;

        public ProjectsController(IUnitOfWork unitOfWork, ILogger<UsersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        /// <summary>
        /// Gets a list of projects.
        /// </summary>
        /// <param name="parameter">
        /// **The parameter is used to filter projects.**
        /// - Use parameter 0 to return filtered non-deleted projects.
        /// 
        /// - Use parameter 1 to retrieve all projects without filtering.
        /// 
        /// - Use paramater 2 to return filtered non-deleted projects with filtered state.
        /// </param>
        /// <param name="state">
        /// *Stated is used to filter projects for different statuses.**
        /// - Use state Pending to filter to pending status.
        /// 
        /// - Use state Confirmed to filter to confirmed status.
        /// 
        /// - Use state Finished to filter to finished status.
        /// </param>
        /// <param name="pageSize">
        /// **The pageSize is used to indicate how many elements you want per page.**
        /// </param>
        /// <param name="pageToShow">
        /// **The pageToShow is used to indicate on which page you will be.**
        /// </param>
        /// <returns>Returns a list of users with an HTTP 200 response.</returns>
        /// 

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetAll(int parameter = 0, string state = "Pending", int pageSize = 10, int pageToShow = 1)
        {
            try
            {
      
                var projectsDTO = await _unitOfWork.ProjectRepository.GetAllProjects(parameter, state);
                if (Request.Query.ContainsKey("page")) int.TryParse(Request.Query["page"], out pageToShow);
                var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
                var paginateProjects = PaginateHelper.Paginate(projectsDTO, pageToShow, url, pageSize);
                return ResponseFactory.CreateSuccessResponse(200, paginateProjects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }

        }

        /// <summary>
        /// Get a project.
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a project with this identication.**</param>
        /// <param name="parameter">
        /// **The parameter used to filter projects.**
        /// - Use parameter 0 to return filtered non-deleted project.
        /// 
        /// - Use parameter 1 to retrieve all projects without filtering.</param>
        /// <returns>
        /// Returns a HTTP 200 response with the user object matching the given ID 
        /// if found, or a HTTP 404 response with an error message if the user is not found.
        /// </returns>
        /// 

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetById([FromRoute] int id, int parameter = 0)
        {
            try
            {
                var projectDTO = await _unitOfWork.ProjectRepository.GetProjectById(id, parameter);

                if (projectDTO != null)
                {
                    return ResponseFactory.CreateSuccessResponse(200, projectDTO);
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(404, "The project couldn't be found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }

        }

        /// <summary>
        /// Register a project.
        /// </summary>
        /// <param name="projectDTO">
        /// **A model containing project information to be registered.**</param>
        /// <returns>Returns an HTTP 201 response if the registration operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Register(ProjectDTO projectDTO)
        {
            try
            {
                var result = await _unitOfWork.ProjectRepository.InsertProject(projectDTO);
                if (result != false)
                {
                    await _unitOfWork.Complete();
                    return ResponseFactory.CreateSuccessResponse(201, "The register operation was successful");
                }
                return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }

        }

        /// <summary>
        /// Update a project.
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a project that matches  this identification**</param>
        /// <param name="projectDTO">
        /// **A model which will replace the older project data**</param>
        /// <returns>Returns an HTTP 200 response if the updating operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPut("{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int id, ProjectDTO projectDTO)
        {
            try
            {

                var result = await _unitOfWork.ProjectRepository.UpdateProject(projectDTO, id);
                if (result != null)
                {
                    await _unitOfWork.Complete();
                    return ResponseFactory.CreateSuccessResponse(200, "The updating operation was successful");
                }
                return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }
        }

        /// <summary>
        /// Delete a project softly (soft deletion) or permanently (hard deletion).
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a service with this identification.**</param>
        /// <param name="parameter">
        /// **The parameter used to select the type of deletion.**
        /// - Use parameter 0 to soft delete.
        /// 
        /// - Use parameter 1 to hard delete. </param>
        /// <returns>Returns an HTTP 200 response if the deletion operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPut("delete/{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete([FromRoute] int id, int parameter = 0)
        {
            try
            {
                var projectReturn = await _unitOfWork.ProjectRepository.DeleteProjectById(id, parameter);
                if (projectReturn != false)
                {
                    await _unitOfWork.Complete();
                    return ResponseFactory.CreateSuccessResponse(200, "The deletion operation was successful");
                }
                return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }

        }
    }
}
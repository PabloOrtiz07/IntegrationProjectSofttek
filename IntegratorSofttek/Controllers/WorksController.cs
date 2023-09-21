using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Helper;
using IntegratorSofttek.Infrastructure;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WorksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public WorksController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }

        /// <summary>
        /// Gets a list of works.
        /// </summary>
        /// <param name="parameter">
        /// **The parameter is used to filter services.**
        /// - Use parameter 0 to return filtered non-deleted works.
        /// 
        /// - Use parameter 1 to retrieve all works without filtering.
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
        public async Task<IActionResult> GetAll(int parameter = 0, int pageSize = 10, int pageToShow = 1)
        {
            try
            {
                var worksDTO = await _unitOfWork.WorkRepository.GetAllWorks(parameter);
                if (Request.Query.ContainsKey("page")) int.TryParse(Request.Query["page"], out pageToShow);
                var url = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}").ToString();
                var paginateWorks = PaginateHelper.Paginate(worksDTO, pageToShow, url, pageSize);
                return ResponseFactory.CreateSuccessResponse(200, paginateWorks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }

   
        }

        /// <summary>
        /// Get a work.
        /// </summary>
        /// <param name="id">
        /// **The ID is used to find a work with this identication.**</param>
        /// <param name="parameter">
        /// **The parameter is used to filter a non-deleted work or deleted work.**
        /// - Use parameter 0 to return filtered non-deleted work.
        /// 
        /// - Use parameter 1 to retrieve all work without filtering.</param>
        /// <returns>
        /// Returns a HTTP 200 response with the user object matching the given ID 
        /// If found, or a HTTP 404 response with an error message if the user is not found.
        /// If any other error occurs, it returns an HTTP 500 Internal Server Error response.
        /// </returns>
        /// 

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]
        public async Task<IActionResult> GetById([FromRoute] int id, int parameter=0)
        {

            try
            {

                var workDTO = await _unitOfWork.WorkRepository.GetWorkById(id, parameter);

                if (workDTO != null)
                {
                    return ResponseFactory.CreateSuccessResponse(200, workDTO);
                }
                else
                {
                    return ResponseFactory.CreateErrorResponse(404, "The work couldn't be found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A surprise error happened");
                return ResponseFactory.CreateErrorResponse(500, "A surprise error happened");
            }

 
        }

        /// <summary>
        /// Register a work.
        /// </summary>
        /// <param name="workDTO">
        /// **A model is used to fill in work information.**</param>
        /// <returns>Returns an HTTP 200 response if the registration operation was successful 
        /// If the operation was canceled, it returns Error HTTP 400 response.
        /// If any other error occurs, it returns an HTTP 500 Internal Server Error response.
        /// </returns>
        /// 

        [HttpPost]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Register(WorkDTO workDTO)
        {
            try
            {
                var projectExists = await _unitOfWork.ProjectRepository.GetById(workDTO.project);
                var serviceExists = await _unitOfWork.ServiceRepository.GetById(workDTO.service);

                if (projectExists == null || serviceExists == null)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Invalid project or service ID.");
                }

                var result = await _unitOfWork.WorkRepository.InsertWork(workDTO);
                if (result != false)
                {
                    await _unitOfWork.Complete();
                    return ResponseFactory.CreateSuccessResponse(200, "The register operation was successful");
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
        /// Update a work.
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a work that matche  this identification.**</param>
        /// <param name="workDTO">
        /// **A model is used to replace the older work data**</param>
        /// <returns>Returns an HTTP 200 response if the updating operation was successful 
        /// If the operation was canceled, it returns Error HTTP 400 response.
        /// If any other error occurs, it returns an HTTP 500 Internal Server Error response.
        /// </returns>
        /// 

        [HttpPut("{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Update([FromRoute] int id, WorkDTO workDTO)
        {
            try
            {

                var projectExists = await _unitOfWork.ProjectRepository.GetById(workDTO.project);
                var serviceExists = await _unitOfWork.ServiceRepository.GetById(workDTO.service);

                if (projectExists==null || serviceExists==null)
                {
                    return ResponseFactory.CreateErrorResponse(400, "Invalid project or service ID.");
                }

                var result = await _unitOfWork.WorkRepository.UpdateWork(workDTO, id);

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
        /// Delete a work softly (soft deletion) or permanently (hard deletion).
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a work with this identification.**</param>
        /// <param name="parameter">
        /// **The parameter is used to select the type of deletion.**
        /// - Use parameter 0 to soft delete.
        /// 
        /// - Use parameter 1 to hard delete. </param>
        /// <returns>Returns an HTTP 200 response if the deletion operation was successful 
        /// If the operation was canceled, it returns Error HTTP 400 response.
        /// If any other error occurs, it returns an HTTP 500 Internal Server Error response.
        /// </returns>
        /// 

        [HttpPut("delete/{id}")]
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete([FromRoute] int id, int parameter=0)
        {
            try
            {
                var workReturn = await _unitOfWork.WorkRepository.DeleteWorkById(id, parameter);
                if (workReturn != false)
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

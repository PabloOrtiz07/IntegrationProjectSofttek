using AlkemyUmsa.Infrastructure;
using AutoMapper;
using IntegratorSofttek.DTOs;
using IntegratorSofttek.Entities;
using IntegratorSofttek.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IntegratorSofttek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WorksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a list of works.
        /// </summary>
        /// <param name="parameter">
        /// **The parameter used to filter services.**
        /// - Use parameter 0 to return filtered non-deleted works.
        /// 
        /// - Use parameter 1 to retrieve all works without filtering.
        /// </param>
        /// <returns>Returns a list of users with a HTTP 200 response .</returns>
        /// 

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetAllWorks(int parameter)
        {

            var works = await _unitOfWork.WorkRepository.GetAllWorks(parameter);
            var worksDTO = _mapper.Map<List<WorkDTO>>(works);
            return ResponseFactory.CreateSuccessResponse(200, worksDTO);
        }

        /// <summary>
        /// Get a work.
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a work with this identication.**</param>
        /// <param name="parameter">
        /// **The parameter used to filter a non-deleted work or deleted work.**
        /// - Use parameter 0 to return filtered non-deleted work.
        /// 
        /// - Use parameter 1 to retrieve all work without filtering.</param>
        /// <returns>
        /// Returns a HTTP 200 response with the user object matching the given ID 
        /// if found, or a HTTP 404 response with an error message if the user is not found.
        /// </returns>
        /// 

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetWorkById([FromRoute] int id, int parameter=0)
        {
            var work = await _unitOfWork.WorkRepository.GetWorkById(id, parameter);

            if (work != null)
            {
                var workDTO = _mapper.Map<WorkDTO>(work);
                return ResponseFactory.CreateSuccessResponse(200, workDTO);
            }
            else
            {
                return ResponseFactory.CreateErrorResponse(404, "The user couldn't be found");
            }
        }

        /// <summary>
        /// Register a work.
        /// </summary>
        /// <param name="workDTO">
        /// **A model used to fill in work information.**</param>
        /// <returns>Returns an HTTP 201 response if the registration operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPost]
        [Route("RegisterWork")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> RegisterWork(WorkDTO workDTO)
        {
            var work = _mapper.Map<Work>(workDTO);
            var result = await _unitOfWork.WorkRepository.Insert(work);
            if (result != false)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(201, "The register operation was successful");
            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }

        /// <summary>
        /// Update a work.
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a work that matche  this identification.**</param>
        /// <param name="workDTO">
        /// **A model which will replace the older work data**</param>
        /// <returns>Returns an HTTP 200 response if the updating operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPut]
        [Route("UpdateWork/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> UpdateWork([FromRoute] int id, WorkDTO workDTO)
        {
            var work = _mapper.Map<Work>(workDTO);

            var result = await _unitOfWork.WorkRepository.Update(work, id);
            if (result != null)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(200, "The updating operation was successful");
            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }

        /// <summary>
        /// Delete a work softly (soft deletion) or permanently (hard deletion).
        /// </summary>
        /// <param name="id">
        /// **The ID used to find a work with this identification.**</param>
        /// <param name="parameter">
        /// **The parameter used to select the type of deletion.**
        /// - Use parameter 0 to soft delete.
        /// 
        /// - Use parameter 1 to hard delete. </param>
        /// <returns>Returns an HTTP 204 response if the deletion operation was successful 
        /// or an Error HTTP 400 response.</returns>
        /// 

        [HttpPut]
        [Route("DeleteWork/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> DeleteWork([FromRoute] int id, int parameter=0)
        {
            var workReturn = await _unitOfWork.WorkRepository.DeleteWorkById(id, parameter);
            if (workReturn != false)
            {
                await _unitOfWork.Complete();
                return ResponseFactory.CreateSuccessResponse(204, "The deletion operation was successful");
            }
            return ResponseFactory.CreateErrorResponse(400, "The operation was canceled");
        }
    }
}

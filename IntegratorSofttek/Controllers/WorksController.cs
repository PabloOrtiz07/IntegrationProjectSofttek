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
        /// Gets a list of works based on a parameter.
        /// Requires the Administrator and Consultant policies for access.
        /// </summary>
        /// <param name="parameter">The parameter used to filter services.
        /// Use parameter 0 to return filtered non-deleted works,
        /// and use parameter 1 to retrieve all works without filtering
        /// </param>
        /// <returns>Returns a list of works.</returns>

        [HttpGet]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<ActionResult<IEnumerable<WorkDTO>>> GetAllWorks(int parameter)
        {

            var works = await _unitOfWork.WorkRepository.GetAllWorks(parameter);
            if (works == null || !works.Any())
            {
                return NotFound();
            }
            var worksDTO = _mapper.Map<List<WorkDTO>>(works);
            return Ok(worksDTO);
        }
        /// <summary>
        /// Get a work by their id.
        /// Requires the Administrator and Consultant policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a work with this identication.</param>
        /// <param name="parameter">The parameter used to filter a non-deleted work or deleted work
        /// Use parameter 0 to return filtered non-deleted work,
        /// and use parameter 1 to retrieve all work without filtering </param>
        /// <returns>Returns a work object matching the given ID or null</returns>

        [HttpGet("{id}")]
        [Authorize(Policy = "AdministratorAndConsultant")]

        public async Task<IActionResult> GetWorkById([FromRoute] int id, int parameter=0)
        {
            var work = await _unitOfWork.WorkRepository.GetWorkById(id, parameter);

            if (work != null)
            {
                var workDTO = _mapper.Map<WorkDTO>(work);
                return Ok(workDTO);
            }
            else
            {
                return NotFound("The work couldn't be found");
            }
        }

        /// <summary>
        /// Register a work in the database.
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="workDTO">A model used to fill in work information</param>
        /// <returns>Returns "OK" if the registeration operation was succesful </returns>

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
                return Ok("The register operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        /// <summary>
        /// Update a work in the database
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a work that matche  this identification</param>
        /// <param name="workDTO">A model which will replace the older work data</param>
        /// <returns>Returns "OK" if the updating operation was succesfull</returns>

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
                return Ok("The update operation was successful");
            }
            return BadRequest("The operation was canceled");
        }

        /// <summary>
        /// Delete a work softly (soft deletion) or permanently (hard deletion).
        /// Requires the Administrator policies for access.
        /// </summary>
        /// <param name="id">The ID used to find a work with this identification </param>
        /// <param name="parameter">The parameter used to select the type of deletion 
        /// Use parameter 0 to soft delete  and,
        /// use parameter 1 to hard delete </param>
        /// <returns>Returns "OK" if the delete operation was succesfull or "BadRequest" if there was and issue</returns>

        [HttpPut]
        [Route("DeleteWork/{id}")]
        [Authorize(Policy = "Administrator")]

        public async Task<IActionResult> DeleteWork([FromRoute] int id, int parameter=0)
        {
            var workReturn = await _unitOfWork.WorkRepository.DeleteWorkById(id, parameter);
            if (workReturn != false)
            {
                await _unitOfWork.Complete();
                return Ok("This work has been dropped down");
            }
            return NotFound("The work couldn't be found");
        }
    }
}
